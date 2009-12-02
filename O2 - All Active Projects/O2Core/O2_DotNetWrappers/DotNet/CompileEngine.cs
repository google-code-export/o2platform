// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.CSharp;
using O2.DotNetWrappers.Filters;
using O2.DotNetWrappers.O2Misc;
using O2.DotNetWrappers.Windows;
using O2.Kernel;

namespace O2.DotNetWrappers.DotNet
{
    public class CompileEngine
    {
        static string specialO2Tag_ExtraReferences = "//O2Tag_AddReferenceFile:";
        static string specialO2Tag_ExtraSourceFile = "//O2Tag_AddSourceFile:";
        static string specialO2Tag_ExtraFolder = "//O2Tag_AddSourceFolder:";

        public Assembly compiledAssembly;
        public CompilerResults crCompilerResults;
        public StringBuilder sbErrorMessage;

        public List<String> lsGACExtraReferencesToAdd = new List<string>(new []
                                                                                 {
                                                                                     "System.Windows.Forms.dll",
                                                                                     "System.Drawing.dll",
                                                                                     "System.Data.dll",
                                                                                     "System.Xml.dll",
                                                                                     "System.Web.dll",
                                                                                     "System.Core.dll",
                                                                                     "System.Xml.Linq.dll",
                                                                                     "System.Xml.dll",
                                                                                     "System.dll"});
      /*  public List<String> lsExtraReferenceAssembliesToAdd = new List<string>
                                                                  {
                                                                      "O2_Kernel.dll",
                                                                      "System.Dll",
                                                                      "System.Core.dll",
                                                                      "System.Data.dll",
                                                                      "System.Drawing.dll",
                                                                      "System.Windows.Forms.dll",
                                                                      "System.Xml.dll",
                                                                      "System.Xml.Linq.dll",
                                                                      "Mono.Cecil.dll",
                                                                      "Skybound.Gecko.dll",
                                                                      "System.Configuration.dll",
                                                                      "nunit.framework.dll",
                                                                      "WeifenLuo.WinFormsUI.Docking.dll"
                                                                  };*/

        /*public static List<String> lsExtraReferencesToAdd = new List<string>(new String[] {                    
                    "System.Windows.Forms.dll",
                    "System.Drawing.dll",
                    "System.Data.dll",
                    "System.Xml.dll",
                    "System.Web.dll",
                    "System.dll"
            //,                                        
                    //@"C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.0\System.Workflow.ComponentModel.dll",
              //      @"C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.0\System.Workflow.Runtime.dll",
              //      @"C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.0\System.Workflow.Activities.dll"
                    });
         */
        

        /*   public static void compileSourceCodeFile(String sFileToCompile, List<string> lsExtraReferencesToAdd)
        {
            String[] asExtraReferencesToAdd = lsExtraReferencesToAdd.ToArray();
            String sErrorMessages = "";
            Assembly aCompiledAssembly = null;
            String sSourceCode = Files.getFileContents(sFileToCompile);
            var verbose = false;
            var exeMainClass = "";            
            var outputAssemblyName = ""; 
            if ("" != sSourceCode)
            {
                if (compileSourceCode(sSourceCode, asExtraReferencesToAdd, ref aCompiledAssembly, ref sErrorMessages, verbose, exeMainClass, outputAssemblyName))
                {
                    PublicPublicDI.log.debug("File Compile OK");
                    // put compiled types into o2GlobalVars
                    foreach (Type tType in aCompiledAssembly.GetTypes())
                        vars.set_(tType.Name, tType);
                }
                else
                {
                    PublicPublicDI.log.error("File Compilation returned the following errors");
                    foreach (
                        String sError in
                            sErrorMessages.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))
                        PublicDI.log.error("   {0}", sError);
                }
            }
        }*/


        /*  public static bool compileSourceCode(String sSourceCodeToCompile, String[] asExtraReferencesToAdd,
                                                     ref Assembly aCompiledAssembly, ref String sErrorMessages)
        {
            return compileSourceCode(sSourceCodeToCompile, asExtraReferencesToAdd, ref aCompiledAssembly, ref sErrorMessages);
        }*/

        /* public static bool compileSourceCode(String sSourceCodeToCompile, String[] asExtraReferencesToAdd,
                                             ref Assembly aCompiledAssembly, ref String sErrorMessages, string mainClass)            
        {
            

            //String sSourceCodeToCompile = ascx_SourceCodeEditor1.getSourceCode(); 


            //asceSourceCodeEditor.cleanHighLights();
            return compileSourceCode_CSharp(sSourceCodeToCompile, asExtraReferencesToAdd,
                                            ref aCompiledAssembly, ref sErrorMessages, false, mainClass, outputAssemblyName );
        }*/


        public void addErrorsListToListBox(ListBox lbSourceCode_CompilationResult)
        {
            if (sbErrorMessage == null)
                return;
            lbSourceCode_CompilationResult.Items.Clear();
            addErrorsListToListBox(sbErrorMessage.ToString(), lbSourceCode_CompilationResult);
        }

        public void addErrorsListToListBox(string sErrorMessages, ListBox lbSourceCode_CompilationResult)
        {
            lbSourceCode_CompilationResult.invokeOnThread(
                () =>
                    {
                        if (sErrorMessages == null)
                            return;

                        String[] sSplitedErrorMessage = sErrorMessages.Split(new[] {Environment.NewLine},
                                                                             StringSplitOptions.RemoveEmptyEntries);
                        foreach (string sSplitMessage in sSplitedErrorMessage)
                        {
                            /* string[] sSplitedLine = sSplitMessage.Split(':');
             if (sSplitedLine.Length == 4)
             {
                 int iLine = Int32.Parse(sSplitedLine[0]);
                 int iColumn = Int32.Parse(sSplitedLine[1]);
                 //asceSourceCodeEditor.highlightLineWithColor(iLine, Color.Red);
             }*/
                            lbSourceCode_CompilationResult.Items.Add(sSplitMessage);
                        }
                    });
        }

        public Thread loadAssesmblyDataIntoTreeView(Assembly aAssemblyToLoad, TreeView tvTargetTreeView,
                                                         Label lbLastMethodExecuted, bool bOnlyShowStaticMethods)
        {
            tvTargetTreeView.Visible = false;
            tvTargetTreeView.Nodes.Clear();
            tvTargetTreeView.Sorted = true;
            int iTypesAdded = 0;

            return O2Thread.mtaThread(() =>
                                   {
                                       try
                                       {
                                           var treeNodesToAdd = new List<TreeNode>();
                                           foreach (Type tType in aAssemblyToLoad.GetTypes())
                                           {
                                               if ((iTypesAdded++) % 500 == 0)
                                                   PublicDI.log.info("{0} types processed", iTypesAdded);
                                               //vars.set_(tType.Name, tType); // set global variable of compiled code
                                               //Callbacks.raiseEvent_ScriptCompiledSuccessfully(tType.Name);                
                                               TreeNode tnType = O2Forms.newTreeNode(tType.Name, tType.Name, 1, tType);
                                               foreach (
                                                   MethodInfo mMethod in
                                                       tType.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly |
                                                       ((bOnlyShowStaticMethods) ? BindingFlags.Static : BindingFlags.Static | BindingFlags.Instance)))
                                               {
                                                   if (mMethod.Name == lbLastMethodExecuted.Text)
                                                       lbLastMethodExecuted.Tag = mMethod;
                                                   //TreeNode tnMethod = O2Forms.newTreeNode(mMethod.Name, mMethod.Name, 2, mMethod);
                                                   TreeNode tnMethod =
                                                       O2Forms.newTreeNode(
                                                           new FilteredSignature(mMethod).getReflectorView(),
                                                           mMethod.Name, 2, mMethod);
                                                   tnType.Nodes.Add(tnMethod);
                                               }
                                               if (tnType.Nodes.Count > 0)
                                                   treeNodesToAdd.Add(tnType);
                                                   //O2Forms.addNodeToTreeNodeCollection(tvTargetTreeView, tvTargetTreeView.Nodes, tnType);      // thread safe way to add nodes                                                                                    
                                           }
                                           PublicDI.log.info("{0} types processed , now loading them into treeView", iTypesAdded);                                           
                                           tvTargetTreeView.invokeOnThread(() =>
                                           {
                                               foreach (var treeNode in treeNodesToAdd)
                                                   tvTargetTreeView.Nodes.Add(treeNode);
                                               PublicDI.log.info("All nodes loaded");
                                               if (tvTargetTreeView.Nodes.Count > 0)
                                                   tvTargetTreeView.Nodes[0].Expand();
                                               tvTargetTreeView.Visible = true;
                                           });

                                       }
                                       catch (Exception ex)
                                       {
                                           PublicDI.log.ex(ex, "in loadAssesmblyDataIntoTreeView");
                                       }
                                   });
            
            
            //if (tvTargetTreeView.GetNodeCount(true) < 20)
            //    tvTargetTreeView.ExpandAll();
            //tvTargetTreeView.Visible = true;
        }

        public Assembly compileSourceCode(String sourceCodeFile)
        {
            return compileSourceCode(sourceCodeFile, "", "");
        }
        public Assembly compileSourceCode(String sourceCodeFile, string mainClass,
                                                 string outputAssemblyName)
        {
            var tempSourceCodeFile = PublicDI.config.getTempFileInTempDirectory("_" +outputAssemblyName + ".cs");
            Files.WriteFileContent(tempSourceCodeFile, sourceCodeFile);
            return compileSourceFiles(new List<string> { tempSourceCodeFile }, mainClass, outputAssemblyName);
        }

        public Assembly compileSourceFile(String sourceCodeFile)
        {
            return compileSourceFiles(new List<string> { sourceCodeFile });
        }

        public Assembly compileSourceFiles(List<String> sourceCodeFiles)
        {
            return compileSourceFiles(sourceCodeFiles, "" /*mainClass*/);
        }

        public Assembly compileSourceFiles(List<String> sourceCodeFiles, string mainClass)
        {
            return compileSourceFiles(sourceCodeFiles, mainClass, "" /*outputAssemblyName*/);
        }

        public Assembly compileSourceFiles(List<String> sourceCodeFiles, string mainClass,
                                                 string outputAssemblyName)
        {
            string errorMessages = "";
            compiledAssembly = null;
            var referencedAssemblies = getListOfReferencedAssembliesToUse();
            // see if there are any extra DLL references in the code
            addReferencesIncludedInSourceCode(sourceCodeFiles, referencedAssemblies);

            addSourceFileOrFolderIncludedInSourceCode(sourceCodeFiles);

            if (compileSourceFiles(sourceCodeFiles, referencedAssemblies.ToArray(), ref compiledAssembly, ref errorMessages, false
                                  /*verbose*/, mainClass, outputAssemblyName))
                return compiledAssembly;
            PublicDI.log.error("Compilation failed: {0}", errorMessages);
            return null;
        }

        public static void addSourceFileOrFolderIncludedInSourceCode(List<string> sourceCodeFiles)
        {
            var currentSourceDirectories = new List<string>(); // in case we need to resolve file names below
            foreach (var file in sourceCodeFiles)
            {
                var directory = Path.GetDirectoryName(file);
                if (false == currentSourceDirectories.Contains(directory))
                    currentSourceDirectories.Add(directory);
            }

            var filesToAdd = new List<string>();
            // find the extra files to add
            foreach (var sourceCodeFile in sourceCodeFiles)
            {
                var fileLines = Files.getFileLines(sourceCodeFile);
                foreach (var fileLine in fileLines)
                    if (fileLine.StartsWith(specialO2Tag_ExtraSourceFile))
                    {
                        var file = fileLine.Replace(specialO2Tag_ExtraSourceFile, "").Trim();
                        if (false == sourceCodeFiles.Contains(file) && false == filesToAdd.Contains(file))
                            filesToAdd.Add(file);
                    }
                    else if (fileLine.StartsWith(specialO2Tag_ExtraFolder))
                    {
                        var folder = fileLine.Replace(specialO2Tag_ExtraFolder, "").Trim();
                        if (false == Directory.Exists(folder))
                            foreach(var path in currentSourceDirectories)
                                if(Directory.Exists(Path.Combine(path,folder)))
                                {
                                    folder = Path.Combine(path,folder);
                                    break;
                                }
                        foreach(var file in Files.getFilesFromDir_returnFullPath(folder,"*.cs",true))
                            if (false == sourceCodeFiles.Contains(file) && false == filesToAdd.Contains(file))
                                filesToAdd.Add(file);
                    }

            }

            // add them to the list (checking if the file exist)
            if (filesToAdd.Count > 0)
            {                
                PublicDI.log.info("There are {0} extra files to add to the list of source code files to compile: {0}", filesToAdd.Count);
                foreach (var file in filesToAdd)
                {
                    if (File.Exists(file))
                        sourceCodeFiles.Add(file);
                    else
                    {
                        bool resolvedFilePath = false;
                        // try to find the file in the current sourceCodeFiles directories                        
                        foreach (var directory in currentSourceDirectories)
                            if (File.Exists(Path.Combine(directory, file)))
                            {
                                sourceCodeFiles.Add(Path.Combine(directory, file));
                                resolvedFilePath = true;
                                break;
                            }
                        if (false == resolvedFilePath)
                            PublicDI.log.error("in addSourceFileOrFolderIncludedInSourceCode, could not file file to add: {0}", file);
                    }
                }
                if (sourceCodeFiles.Count > 1)
                {
                    PublicDI.log.debug("There are {0} files to compile");
                    foreach (var file in sourceCodeFiles)
                        PublicDI.log.debug("   {0}", file);
                }
            }
        }

        /*public static void insertSourceFileInListOfSourceFiles(List<string> sourceCodeFiles, string fileToInsert)
        {
            if (sourceCodeFiles.Contains(fileToInsert))         // check if the file is already in the list of sourceCodeFiles
                sourceCodeFiles.Remove(fileToInsert);           // remove it if required
            sourceCodeFiles.Insert(0, fileToInsert);            // and insert the file at the top of the list (so that it is compiled first)
        }*/

        public static void addReferencesIncludedInSourceCode(List<string> sourceCodeFiles, List<string> referencedAssemblies)
        {            
            foreach( var sourceCodeFile in sourceCodeFiles)
            {
                var fileLines = Files.getFileLines(sourceCodeFile);
                foreach(var fileLine in fileLines)
                    if (fileLine.StartsWith(specialO2Tag_ExtraReferences))
                    {
                        var extraReference = fileLine.Replace(specialO2Tag_ExtraReferences, "").Trim();
                        if (File.Exists(extraReference) && false == referencedAssemblies.Contains(extraReference))
                        {
                            Files.Copy(extraReference, PublicDI.config.O2TempDir);
                            /*var assembly = PublicDI.reflection.getAssembly(extraReference);
                            if (assembly == null)
                                DI.log.error("(this could be a problem for execution) in addReferencesIncludedInSourceCode could not load assembly :{0}", extraReference);
                             */                             
                            referencedAssemblies.Add(extraReference);
                        }
                    }
            }
        }

        public static List<string> getListOfO2AssembliesInExecutionDir()
        {            
            var o2AssembliesInExecutionDir = new List<string>();
            o2AssembliesInExecutionDir.AddRange(Files.getFilesFromDir_returnFullPath(PublicDI.config.CurrentExecutableDirectory, "*O2*.dll"));
            o2AssembliesInExecutionDir.AddRange(Files.getFilesFromDir_returnFullPath(PublicDI.config.CurrentExecutableDirectory, "*O2*.exe"));            
            return o2AssembliesInExecutionDir;
        }
        public List<string> getListOfReferencedAssembliesToUse()
        {
            // let's add everything in the current executabled dir :)
            var referencedAssemblies = new List<string>();
            referencedAssemblies.AddRange(getListOfO2AssembliesInExecutionDir());
            referencedAssemblies.AddRange(lsGACExtraReferencesToAdd); // the a couple from the GAC
            return referencedAssemblies;
        }

        public bool compileSourceFiles(List<String> sourceCodeFiles, String[] sReferenceAssembliesToAdd,
                                             ref Assembly aCompiledAssembly, ref String sErrorMessages,
                                             bool bVerbose, string exeMainClass, string outputAssemblyName)
        {
            try
            {
                if (outputAssemblyName == "")
                    outputAssemblyName = PublicDI.config.TempFileNameInTempDirectory;
                if (!Directory.Exists(Path.GetDirectoryName(outputAssemblyName)))
                    outputAssemblyName = Path.Combine(PublicDI.config.O2TempDir, outputAssemblyName);
                if (bVerbose)
                    PublicDI.log.debug("Dynamically compiling Source code...");
                String sDefaultPathToResolveReferenceAssesmblies = PublicDI.config.O2TempDir;
                //if (bVerbose)
                //    PublicDI.log.debug("Compiling source code \n\n {0} \n\n", sSourceCode);
                var providerOptions = new Dictionary<string, string> {{"CompilerVersion", "v3.5"}};
                var cscpCSharpCodeProvider = new CSharpCodeProvider(providerOptions);
                var cpCompilerParameters = new CompilerParameters
                                               {
                                                   GenerateInMemory = false,
                                                   IncludeDebugInformation = true,
                                                   OutputAssembly = outputAssemblyName
                                               };
                if (exeMainClass == "")
                    cpCompilerParameters.OutputAssembly += ".dll";
                else
                {
                    cpCompilerParameters.OutputAssembly += ".exe";
                    cpCompilerParameters.MainClass = exeMainClass;
                    cpCompilerParameters.GenerateExecutable = true;
                }                

                // I was doing this all in memory but with the Add-ons there were probs running some of the O2JavaScript d
                //                cpCompilerParameters.GenerateInMemory = true;                
                if (null != sReferenceAssembliesToAdd)
                    foreach (String sReferenceAssembly in sReferenceAssembliesToAdd)
                    {
                        try
                        {
                            // first try to resolve it in the current directory
                            String sResolvedAssemblyName = Path.Combine(PublicDI.config.CurrentExecutableDirectory,
                                                                        sReferenceAssembly);
                            if (File.Exists(sResolvedAssemblyName))
                                cpCompilerParameters.ReferencedAssemblies.Add(sResolvedAssemblyName);
                            else
                            {
                                // if not try the _temp directory
                                sResolvedAssemblyName = Path.Combine(sDefaultPathToResolveReferenceAssesmblies,
                                                                     sReferenceAssembly);
                                if (File.Exists(sResolvedAssemblyName))
                                    cpCompilerParameters.ReferencedAssemblies.Add(sResolvedAssemblyName);
                                else
                                    // in case it is in the GAC, just add the name directly
                                    cpCompilerParameters.ReferencedAssemblies.Add(sReferenceAssembly);
                            }
                        }
                        catch (Exception)
                        { }

                        //PublicDI.log.error("in compileSourceCode_CSharp, could not resolve path to reference assembly :{0}", sReferenceAssembly);
                    }
                //cpCompilerParameters.ReferencedAssemblies.AddRange(sReferenceAssembliesToAdd);                
                //CompilerResults crCompilerResults =
                //    cscpCSharpCodeProvider.CompileAssemblyFromSource(cpCompilerParameters, sSourceCode);
                crCompilerResults = cscpCSharpCodeProvider.CompileAssemblyFromFile(cpCompilerParameters, sourceCodeFiles.ToArray());
                

                if (crCompilerResults.Errors.Count == 0)
                {
                    if (bVerbose)
                        PublicDI.log.debug("There were no errors...");
                    aCompiledAssembly = crCompilerResults.CompiledAssembly;
                    return true;                    
                }

                sbErrorMessage = new StringBuilder();
                foreach (CompilerError ceCompilerError in crCompilerResults.Errors)
                {
                    sbErrorMessage.AppendLine(String.Format("{0}:{1}:{2}:{3}", ceCompilerError.Line,
                                                            ceCompilerError.Column, ceCompilerError.ErrorNumber,
                                                            ceCompilerError.ErrorText));
                    //sErrorMessages += ceCompilerError.ErrorText + Environment.NewLine;
                }
                sErrorMessages = sbErrorMessage.ToString();
                if (bVerbose)
                    PublicDI.log.error("Compilatation errors \n\n {0}", sErrorMessages);                
            }
            catch (Exception ex)
            {
                PublicDI.log.error("In compileSourceCode_CSharp: {0}", ex.Message);
            }
            return false;
        }
    }
}
