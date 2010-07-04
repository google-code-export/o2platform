// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using O2.Interfaces.O2Core;
using O2.Kernel.CodeUtils;

namespace O2.Kernel.InterfacesBaseImpl
{
    public class KO2Config : IO2Config
    {
        public static string defaultLocalScriptFolder = @"C:\O2\O2Scripts_Database\_Scripts";
        public static string defaultLocallyDevelopedScriptsFolder = @"C:\O2\_XRules_Local";
        public static string LocallyDevelopedScriptsTemplateFolder = defaultLocalScriptFolder + @"\_Templates";
        public static string defaultSvnO2RootFolder = @"http://o2platform.googlecode.com/svn/trunk/";
        public static string defaultSvnO2DatabaseRulesFolder = @"http://o2platform.googlecode.com/svn/trunk/O2_Scripts/";
        public static string defaultO2SVN_ExternalDlls = "http://o2platform.googlecode.com/svn/trunk/O2 - All Active Projects/_3rdPartyDlls/";
        public static string defaultO2SVN_Binaries = "http://o2platform.googlecode.com/svn/trunk/O2_Binaries/";
        
        const string  defaultO2LocalTempFolder = @"C:\O2\_tempDir\";

        public KO2Config()
        {
            hardCodedO2LocalTempFolder = defaultO2LocalTempFolder;
            O2TempDir = hardCodedO2LocalTempFolder;                 // default to this one since there are a couple cases where in Visual Studio the loading of o2.config files causes a problem
            hardCodedO2LocalBuildDir = @"E:\O2\_Bin_(O2_Binaries)\";
            hardCodedO2LocalSourceCodeDir = @"E:\O2\_SourceCode_O2";            
            O2FindingsFileExtension = ".O2Findings";
            extraSettings = new List<Setting>();
            dependenciesInjection = new List<DependencyInjection>();
            LocalScriptsFolder = defaultLocalScriptFolder;
            LocallyDevelopedScriptsFolder = defaultLocallyDevelopedScriptsFolder;
            LocallyDevelopedTemplatesFolder = LocallyDevelopedScriptsTemplateFolder;            
            SvnO2RootFolder = defaultSvnO2RootFolder;
            SvnO2DatabaseRulesFolder = defaultSvnO2DatabaseRulesFolder;
            O2SVN_ExternalDlls = defaultO2SVN_ExternalDlls;
            O2SVN_Binaries = defaultO2SVN_Binaries;
        }
                       
        public KO2Config(string o2ConfigFile) :this ()
        {       
            O2ConfigFile = o2ConfigFile;            
        }

        // non-interface property
        public List<Setting> extraSettings { get; set; }
        public List<DependencyInjection> dependenciesInjection { get; set; }

        // non-interface properties
        public string O2ConfigFile { get; set; }        
        public string O2FindingsFileExtension { get; set; }        
        public string hardCodedO2LocalTempFolder { get; set; }
        public string hardCodedO2LocalBuildDir { get; set; }
        public string hardCodedO2LocalSourceCodeDir { get; set; }
        public static string dependencyInjectionTest { get; set; }

        //interaface properties

        public string O2TempDir
        {
            get
            {
                if (hardCodedO2LocalTempFolder == null)
                    return null;
                O2Kernel_Files.checkIfDirectoryExistsAndCreateIfNot(hardCodedO2LocalTempFolder);
                return hardCodedO2LocalTempFolder;
            }
            set {
                //note: need to find a better solution for making today's data part of the temp folder 
                if (value.StartsWith(defaultO2LocalTempFolder))
                    hardCodedO2LocalTempFolder = Path.Combine(defaultO2LocalTempFolder, DateTime.Today.ToShortDateString().Replace("/", "-"));
                else
                    hardCodedO2LocalTempFolder = value; 

            }
        }

        public string LocalScriptsFolder { get; set; }
        public string LocallyDevelopedScriptsFolder { get; set; }
        public string LocallyDevelopedTemplatesFolder { get; set; }
        public string SvnO2RootFolder { get; set; }
        public string SvnO2DatabaseRulesFolder { get; set; }
        public string O2SVN_ExternalDlls { get; set; }
        public string O2SVN_Binaries { get; set; }

        public string Version 
        {
            get 
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        

        public string CurrentExecutableDirectory
        {
            get
            {
                var entryAssembly = Assembly.GetEntryAssembly();
                if (entryAssembly != null)
                {
                    var entryAssemblyLocation = Assembly.GetEntryAssembly().Location;
                    return Path.GetDirectoryName(entryAssemblyLocation);
                }
                return AppDomain.CurrentDomain.BaseDirectory;                
            }
            set { }
        }

        public String CurrentExecutableFileName
        {
            get { return Path.GetFileName(Process.GetCurrentProcess().ProcessName); }
            set { }
        }

        public string ExecutingAssembly
        {
            get { return Assembly.GetExecutingAssembly().Location; }
            set { }
        }

        public string TempFileNameInTempDirectory
        {
            get { return Path.Combine(O2TempDir, O2Kernel_Files.getTempFileName()); }
            set { }
        }

        public string TempFolderInTempDirectory
        {
            get
            {
                string tempFolder = Path.Combine(O2TempDir, O2Kernel_Files.getTempFolderName());
                Directory.CreateDirectory(tempFolder);
                return tempFolder;
            }
            set { }
        }

        public string O2KernelAssemblyName
        {
            get { return "O2_Kernel.dll"; }
            set { }
        }

        /// <summary>
        /// returns a tempfile in the temp directory with the provided extension 
        ///(no need to include the dot in the extension)
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public string getTempFileInTempDirectory(string extension)
        {
            return TempFileNameInTempDirectory +
                   (extension.StartsWith(".") ? extension : ("." + extension));
        }

        public string getTempFolderInTempDirectory(string stringToAddToTempDirectoryName)
        {
            var tempFolder = TempFolderInTempDirectory;
            var tempFolderWithExtraString = Path.Combine(Path.GetDirectoryName(tempFolder), stringToAddToTempDirectoryName+ "_" + Path.GetFileName(tempFolder));
            Directory.CreateDirectory(tempFolderWithExtraString);
            Directory.Delete(tempFolder);
            return tempFolderWithExtraString;

        }

        public bool setDI(Type typeToInjectDependency, string propertyToInject, Object dependencyObject)
        {
            //var diType = DI.reflection.getType(typeToInjectDependency, "DI");
            if (typeToInjectDependency != null)
                if (DI.reflection.setProperty(propertyToInject, typeToInjectDependency, dependencyObject))
                    return true;
            return false;
        }

        public bool setDI(string assemblyName, string typeToInjectDependency, string propertyToInject,
                          Object dependencyObject)
        {
            Type diType = DI.reflection.getType(assemblyName, typeToInjectDependency);
            if (diType != null)
                if (DI.reflection.setProperty(propertyToInject, diType, dependencyObject))
                {
                    DI.log.info("setDI of object {0} into property {1} in Type {2} in Assembly {3}",
                                dependencyObject.GetType().Name, propertyToInject, typeToInjectDependency, assemblyName);
                    return true;
                }
            return false;
        }

        // Todo: NEED TO CHECK IF THIS IS NEEDED
        public void addPathToCurrentExecutableEnvironmentPathVariable(String sPathToAdd)
        {
            string sCurrentEnvironmentPath = Environment.GetEnvironmentVariable("Path");
            if (sCurrentEnvironmentPath != null && sCurrentEnvironmentPath.IndexOf(sPathToAdd) == -1)
            {
                String sUpdatedPathValue = String.Format("{0};{1}", sCurrentEnvironmentPath, sPathToAdd);
                Environment.SetEnvironmentVariable("Path", sUpdatedPathValue);
            }
        }


        public void closeO2Process()
        {
            PublicDI.log.info("Received request to close down current O2 Process");
            O2Kernel_Processes.KillCurrentO2Process(2000); // wait 2 seconds before killing the process
            System.Windows.Forms.Application.Exit();            
        }
    }

    [Serializable]
    public class Setting
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    [Serializable]
    public class DependencyInjection
    {
        [XmlAttribute]
        public string Type { get; set; }
        [XmlAttribute]
        public string Parameter { get; set; }
        [XmlAttribute]
        public string Value { get; set; }
    }
}
