/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using DotNetGuru.AspectDNG.MetaAspects;
using DotNetGuru.AspectDNG.Config;
using DotNetGuru.AspectDNG.XPath;
using DotNetGuru.AspectDNG.Util;

[assembly: AssemblyVersion("0.9")]
[assembly: AssemblyTitle("AspectDNG")]
[assembly: AssemblyDescription("Static and generic aspect weaver")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyProduct("AspectDNG")]
[assembly: AssemblyCopyright("(C) 2006, DotNetGuru SARL")]
[assembly: AssemblyCulture("")]

namespace DotNetGuru.AspectDNG {
    public class EntryPoint {
        private void PerformWeave() {
            foreach (AdviceSpec spec in AspectDngConfig.Instance.Advice) 
                MetaAspectFacade.Instance.Execute(spec);
            MetaAspectFacade.Instance.Cleanup();
        }

        private String CopyToBackup(string filePath) {
            string backupPath = filePath + ".backup";
            File.Copy(filePath, backupPath, true);
            return backupPath;
        }

        private void Weave(string configFile) {
            AspectDngConfig conf = new AspectDngConfig(configFile);
            if (!File.Exists(conf.TargetAssembly)) throw new ConfigurationException("File doesn't exist: " + conf.TargetAssembly);
            if (!File.Exists(conf.AspectsAssembly)) throw new ConfigurationException("File doesn't exist: " + conf.AspectsAssembly);

            string backup = CopyToBackup(conf.TargetAssembly);
            if (conf.TargetAssembly == conf.AspectsAssembly || conf.AspectsAssembly == null || conf.AspectsAssembly == "")
                Cil.Init(backup, backup);
            else
                Cil.Init(backup, conf.AspectsAssembly);

            PerformWeave();
            Cil.SaveTo(conf.WeavedAssembly);
        }

        private void DirectWeave(string targetAssemblyPath, string separateAspectsAssemblyPath) {
            if (!File.Exists(targetAssemblyPath)) throw new ConfigurationException("File doesn't exist: " + targetAssemblyPath);
            if (!File.Exists(separateAspectsAssemblyPath)) throw new ConfigurationException("File doesn't exist: " + separateAspectsAssemblyPath);

            string backup = CopyToBackup(targetAssemblyPath);
            Cil.Init(backup, separateAspectsAssemblyPath);
            PerformWeave();
            Cil.SaveTo(targetAssemblyPath);
        }

        private void DirectWeave(string targetAssemblyPath) {
            if (!File.Exists(targetAssemblyPath)) throw new ConfigurationException("File doesn't exist: "+ targetAssemblyPath);

            string backup = CopyToBackup(targetAssemblyPath);
            Cil.Init(backup, backup);
            PerformWeave();
            Cil.SaveTo(targetAssemblyPath);
        }

        private void PrintUsage() {
            Console.WriteLine("\nAspectDNG Copyright (C) 2005 Thomas GIL (DotNetGuru SARL).\n" +
                "AspectDNG comes with absolutely no warranty.\n" +
                "This is free software under GNU General Public Licence\n" +
                "and you are welcome to redistribute it under certain conditions.\n");

            Console.WriteLine("Usage : \n" +
                " aspectdng ...aspectdng.xml\n" +
                " aspectdng [{0}] ...Target.exe/.dll [...Aspects.dll]\n" +
                " aspectdng [{1}] ...Target.exe/.dll XPathQuery \n" +
                " aspectdng [{2}] ...Target.exe/.dll ...Target.xml",
                DebugOption, QueryOption, IlmlDumpOption);
        }

        private const string DebugOption = "-debug";
        private const string QueryOption = "-query";
        private const string IlmlDumpOption = "-ilml";

        public static int Main(string[] argv) {
#if TEST
            AutoTest.TestSuite();
#endif
            int result = 1;
            StringCollection parameters = new StringCollection();
            parameters.AddRange(argv);

            AppDomain.CurrentDomain.AppendPrivatePath(Environment.CurrentDirectory);
            AppDomain.CurrentDomain.AppendPrivatePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            try {
                EntryPoint entryPoint = new EntryPoint();

                if (parameters.Contains(DebugOption)) { // Handle debug mode
                    AspectDngConfig.Instance.debug = true;
                    parameters.Remove(DebugOption);
                }

                if (parameters.Contains(QueryOption) && parameters.Count >= 3) { // Handle query mode
                    parameters.Remove(QueryOption);
                    Cil.Init(parameters[0], parameters[0]);
                    parameters.RemoveAt(0);

                    string[] array = new string[parameters.Count];
                    parameters.CopyTo(array, 0);
                    string xpath = string.Join(" ", array);
                    Console.WriteLine("\nReal XPath query:\n{0}", xpath);

                    ICollection results = Cil.TargetNavigator.SelectList(xpath);
                    Console.WriteLine("{0} results", results.Count);
                    foreach (Navigator nav in results) {
                        Console.WriteLine("[" + nav.Name + "] " + nav);
                    }
                } else if (parameters.Contains(IlmlDumpOption) && parameters.Count == 3) { // Handle ilml mode
                    parameters.Remove(IlmlDumpOption);
                    Cil.Init(parameters[0], parameters[0]);
                    parameters.RemoveAt(0);

                    // Apply XSLT to Assembly
                    XslTransform ilmlDump = new XslTransform();
                    ilmlDump.Load(new XmlTextReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("IlmlDump.xsl")), null, null);
                    XmlTextWriter writer = new XmlTextWriter(parameters[0], Encoding.Unicode);
                    writer.Formatting = Formatting.Indented;
                    ilmlDump.Transform(new Navigator(Cil.TargetAssembly), null, writer, null);
                    writer.Close();
                } else { // Handle weave mode
                    long Start = DateTime.Now.Ticks;
                    // Interpret parameters
                    if (parameters.Count > 0) {
                        string firstArg = parameters[0];
                        if (firstArg.EndsWith(".xml")) {
                            Log.Debug("Weaving as specified in " + firstArg);
                            entryPoint.Weave(firstArg);
                            result = 0;
                        } else if (firstArg.EndsWith(".dll") || firstArg.EndsWith(".exe")) {
                            if (parameters.Count == 1) 
                                entryPoint.DirectWeave(firstArg);
                            else if (parameters.Count == 2) {
                                string secondArg = parameters[1];
                                entryPoint.DirectWeave(firstArg, secondArg);
                            }
                            result = 0;
                        } else entryPoint.PrintUsage();
                    } else entryPoint.PrintUsage();

                    if (result == 0) {
                        Log.Debug("aspectdng took in {0} millis to weave {1} aspects", (DateTime.Now.Ticks - Start) / 10000, AspectDngConfig.Instance.Advice.Count);
                        Log.Save();
                    }
                }
            } catch (ConfigurationException e) {
                Log.Error(e.Message);
            } catch (AdviceException e) {
                Log.Error(e.Message);
            } catch (Exception e) {
                Log.Error(e.Message);
                Log.Error(e.StackTrace);
            }
            return result;
        }
    }
}
