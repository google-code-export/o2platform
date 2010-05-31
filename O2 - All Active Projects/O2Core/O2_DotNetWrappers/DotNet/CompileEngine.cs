// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.CSharp;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.Filters;
using O2.DotNetWrappers.O2Misc;
using O2.DotNetWrappers.Windows;
using O2.Kernel;
using O2.Kernel.CodeUtils;

namespace O2.DotNetWrappers.DotNet
{
    public class CompileEngine
    {
        static string onlyAddReferencedAssemblies = "O2Tag_OnlyAddReferencedAssemblies";
        static List<string> specialO2Tag_ExtraReferences = new List<string>() {"//O2Tag_AddReferenceFile:", "//O2Ref:"};
        static List<string> specialO2Tag_ExtraSourceFile = new List<string>() { "//O2Tag_AddSourceFile:", "//O2File:" };
        static List<string> specialO2Tag_ExtraFolder = new List<string>() { "//O2Tag_AddSourceFolder:", "//O2Folder:", "//O2Dir:" };
        static List<string> specialO2Tag_DontCompile= new List<string>() { "//O2NoCompile"};

        public Assembly compiledAssembly;
        public CompilerResults crCompilerResults;
        public StringBuilder sbErrorMessage;
        public bool DebugMode;

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

        public void addErrorsListToListBox(ListBox lbSourceCode_CompilationResult)
        {
            lbSourceCode_CompilationResult.invokeOnThread(
                () =>
                    {
                        if (sbErrorMessage == null)
                            return;
                        lbSourceCode_CompilationResult.Items.Clear();
                        addErrorsListToListBox(sbErrorMessage.ToString(), lbSourceCode_CompilationResult);
                    });
        }
        // note that this functionality was moved into a TreeView (in the current version of SourceCodeEditor)        
        public void addErrorsListToListBox(string sErrorMessages, ListBox lbSourceCode_CompilationResult)
        {
            if (sErrorMessages == null)
                return;
            
            String[] sSplitedErrorMessage = sErrorMessages.Split(new[] {Environment.NewLine},
                                                                 StringSplitOptions.RemoveEmptyEntries);
            foreach (string sSplitMessage in sSplitedErrorMessage)
            {
                // this doesn't really work from here due to multi-threading issues when loading up the page
                /*string[] sSplitedLine = sSplitMessage.Split(new [] {"::"}, StringSplitOptions.None);
                if (sSplitedLine.Length == 5)
                {
                    int iLine = Int32.Parse(sSplitedLine[0]);
                    int iColumn = Int32.Parse(sSplitedLine[1]);
                    O2Messages.fileErrorHighlight(sSplitedLine[4], iLine, iColumn);                    
                }*/
                if (false == sSplitMessage.Contains("conflicts with the imported type"))        // ignore these warning which mostlikely will be caused by adding extra files (via O2) to this script
                    lbSourceCode_CompilationResult.Items.Add(sSplitMessage);
            }
        }

        public void addErrorsListToTreeView(TreeView tvCompilationErrors)
        {
            tvCompilationErrors.invokeOnThread(
             () =>
             {
                 if (sbErrorMessage == null)
                     return;
                 tvCompilationErrors.Nodes.Clear();
                 addErrorsListToTreeView(sbErrorMessage.ToString(), tvCompilationErrors);
             });
        }

        private void addErrorsListToTreeView(string errorMessages, TreeView tvCompilationErrors)
        {
            if (errorMessages == null)
                return;

            String[] sSplitedErrorMessage = errorMessages.Split(new[] { Environment.NewLine },
                                                                 StringSplitOptions.RemoveEmptyEntries);
            foreach (string sSplitMessage in sSplitedErrorMessage)
            {
                var newNode = O2Forms.newTreeNode(tvCompilationErrors.Nodes, sSplitMessage, 0, sSplitMessage);
                newNode.ToolTipText = sSplitMessage;

                if (sSplitMessage.Contains("conflicts with the imported type"))   
                    newNode.ForeColor = Color.Gray;
                else
                    newNode.ForeColor = Color.Red;                     
            }
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
            sourceCodeFiles = checkForNoCompileFiles(sourceCodeFiles);
            if (sourceCodeFiles.Count == 0)
                return null;
            string errorMessages = "";
            compiledAssembly = null;
            var referencedAssemblies = getListOfReferencedAssembliesToUse();
            // see if there are any extra DLL references in the code
            
            mapReferencesIncludedInSourceCode(sourceCodeFiles, referencedAssemblies);

            if (compileSourceFiles(sourceCodeFiles, referencedAssemblies.ToArray(), ref compiledAssembly, ref errorMessages, false
                /*verbose*/, mainClass, outputAssemblyName))
            {
                PublicDI.log.debug("Compilated OK to: {0}", compiledAssembly.Location);
                return compiledAssembly;
            }
            PublicDI.log.error("Compilation failed: {0}", errorMessages);
            return null;
        }

        public void mapReferencesIncludedInSourceCode(string sourceCodeFile, List<string> referencedAssemblies)
        {
            var sourceCodeFiles = new List<string> { sourceCodeFile };
            addSourceFileOrFolderIncludedInSourceCode(sourceCodeFiles);
            addReferencesIncludedInSourceCode(sourceCodeFiles, referencedAssemblies);
            
        }

        public void mapReferencesIncludedInSourceCode(List<string> sourceCodeFiles, List<string> referencedAssemblies)
        {
            addSourceFileOrFolderIncludedInSourceCode(sourceCodeFiles);
            addReferencesIncludedInSourceCode(sourceCodeFiles, referencedAssemblies);

            if (sourceCodeFiles.Count > 1)
            {
                PublicDI.log.debug("There are {0} files to compile", sourceCodeFiles.Count);
                foreach (var file in sourceCodeFiles)
                    PublicDI.log.debug("   {0}", file);
            }
            if (referencedAssemblies.Count > 1)
            {                
                PublicDI.log.debug("There are {0} referencedAssemblies used", referencedAssemblies.Count);
                if (DebugMode)
                    foreach (var referencedAssembly in referencedAssemblies)
                        PublicDI.log.debug("   {0}", referencedAssembly);
            }
        }

        public static List<string> checkForNoCompileFiles(List<string> sourceCodeFiles)
        {
            var filesToNotCompile = new List<string>();
            foreach(var sourceCodeFile in sourceCodeFiles)
                if ("" != StringsAndLists.InFileTextStartsWithStringListItem(sourceCodeFile, specialO2Tag_DontCompile))
                    filesToNotCompile.Add(sourceCodeFile);
            foreach (var fileToNotCompile in filesToNotCompile)
            {
                PublicDI.log.debug("Removing from list of files to compile the file: {0}", fileToNotCompile);
                sourceCodeFiles.Remove(fileToNotCompile);
            }
            return sourceCodeFiles;                
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
                {
                    var match = StringsAndLists.TextStartsWithStringListItem(fileLine, specialO2Tag_ExtraSourceFile);
                    if (match != "")
                    {
                     //   var file = fileLine.Replace(specialO2Tag_ExtraSourceFile, "").Trim();
                        var file = fileLine.Replace(match, "").Trim();
                        if (false == sourceCodeFiles.Contains(file) && false == filesToAdd.Contains(file))
                        {
                            filesToAdd.Add(file);                          
                        }
                    }
                    //else if (fileLine.StartsWith(specialO2Tag_ExtraFolder))
                    else 
                    {
                        match = StringsAndLists.TextStartsWithStringListItem(fileLine, specialO2Tag_ExtraFolder);
                        if (match != "")
                        {
                            var folder = fileLine.Replace(match, "").Trim();
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
                }
            }

            // add them to the list (checking if the file exist)
            if (filesToAdd.Count > 0)
            {                
                PublicDI.log.info("There are {0} extra files to add to the list of source code files to compile: {0}", filesToAdd.Count);
                foreach (var file in filesToAdd)
                {                    
                    var filePath = "";
                    if (File.Exists(file))
                        filePath = file;
                    else
                    {
                        
                        // try to find the file in the current sourceCodeFiles directories                        
                        foreach (var directory in currentSourceDirectories)
                            if (File.Exists(Path.Combine(directory, file)))
                            {
                                filePath = Path.Combine(directory, file);
                                break;
                            }

                        foreach (var localScriptFile in @"C:\O2\O2Scripts_Database\_Scripts".files(true,"*.cs"))
                        {
                            if (localScriptFile.fileName().ToLower().StartsWith(file.ToLower()))
                            //if (fileToResolve.lower() == localScriptFile.fileName().lower())
                            {
                                PublicDI.log.debug("in CompileEngin, file reference '{0}' was mapped to local O2 Script file '{1}'",file, localScriptFile);
                                filePath =  localScriptFile;
                                break;
                            }
                        }
                        if (filePath == "")
                            PublicDI.log.error("in addSourceFileOrFolderIncludedInSourceCode, could not file file to add: {0}", file);
                    }
                    if (filePath != "" )
                    {
                        filePath = Path.GetFullPath(filePath);
                        if (false == sourceCodeFiles.Contains(filePath))
                        {
                            sourceCodeFiles.Add(filePath);
                            addSourceFileOrFolderIncludedInSourceCode(sourceCodeFiles); // we need to recursively add new new dependencies 
                        }
                    }
                }               
            }
        }

        /*public static void insertSourceFileInListOfSourceFiles(List<string> sourceCodeFiles, string fileToInsert)
        {
            if (sourceCodeFiles.Contains(fileToInsert))         // check if the file is already in the list of sourceCodeFiles
                sourceCodeFiles.Remove(fileToInsert);           // remove it if required
            sourceCodeFiles.Insert(0, fileToInsert);            // and insert the file at the top of the list (so that it is compiled first)
        }*/

        public static void addReferencesIncludedInSourceCode(string sourceCodeFile, List<string> referencedAssemblies)
        {
            addReferencesIncludedInSourceCode(new List<string> { sourceCodeFile }, referencedAssemblies);
        }

        public static void addReferencesIncludedInSourceCode(List<string> sourceCodeFiles, List<string> referencedAssemblies)
        {
            // the onlyAddReferencedAssemblies check needs to be done seperately for all files
            foreach( var sourceCodeFile in sourceCodeFiles)
            {
                // handle special case where we want (for performace & other reasons) be explicit on the references we add to the current script
                var sourceCode = Files.getFileContents(sourceCodeFile);
                if (sourceCode.Contains(onlyAddReferencedAssemblies))
                {
                    referencedAssemblies.Clear();
                    break;              // once one the files has the onlyAddReferencedAssemblies ref, we can clear the referencedAssemblies and break the loop
                }
            }
            foreach (var sourceCodeFile in sourceCodeFiles)
            {
                // extract the names from referencedAssemblies
                var referencedAssembliesFileNames = new List<String>();
                foreach(var referencedAssembly in referencedAssemblies)
                    referencedAssembliesFileNames.Add(Path.GetFileName(referencedAssembly));
                // search for references in the source code
                var fileLines = Files.getFileLines(sourceCodeFile);

                foreach (var fileLine in fileLines)
                {
                    var match = StringsAndLists.TextStartsWithStringListItem(fileLine, specialO2Tag_ExtraReferences);
                    //if (fileLine.StartsWith(specialO2Tag_ExtraReferences))
                    if (match!="")
                    {
                        var extraReference = fileLine.Replace(match, "").Trim();
                        //if (File.Exists(extraReference) && false == referencedAssemblies.Contains(extraReference))
                        var extraReferenceFileName = Path.GetFileName(extraReference);
                        if (false == referencedAssembliesFileNames.Contains(extraReferenceFileName))
                        {
                            if (true == extraReference.fileExists() &&
                                false ==Path.Combine(PublicDI.config.O2TempDir,extraReference.fileName()).fileExists())
                                Files.Copy(extraReference, PublicDI.config.O2TempDir, true);
                            /*var assembly = PublicDI.reflection.getAssembly(extraReference);
                            if (assembly == null)
                                DI.log.error("(this could be a problem for execution) in addReferencesIncludedInSourceCode could not load assembly :{0}", extraReference);
                             */
                            referencedAssembliesFileNames.
                                Add(extraReferenceFileName);
                            referencedAssemblies.Add(extraReference);
                        }
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

        private CSharpCodeProvider cscpCSharpCodeProvider;
        public bool compileSourceFiles(List<String> sourceCodeFiles, String[] sReferenceAssembliesToAdd,
                                             ref Assembly aCompiledAssembly, ref String sErrorMessages,
                                             bool bVerbose, string exeMainClass, string outputAssemblyName)
        {
            try
            {
                showErrorMessageIfPathHasParentheses(sourceCodeFiles);
                if (outputAssemblyName == "")
                    outputAssemblyName = PublicDI.config.TempFileNameInTempDirectory;
                if (!Directory.Exists(Path.GetDirectoryName(outputAssemblyName)))
                    outputAssemblyName = Path.Combine(PublicDI.config.O2TempDir, outputAssemblyName);
                if (bVerbose)
                    PublicDI.log.debug("Dynamically compiling Source code...");
                String sDefaultPathToResolveReferenceAssesmblies = PublicDI.config.O2TempDir;
                //if (bVerbose)
                //    PublicDI.log.debug("Compiling source code \n\n {0} \n\n", sSourceCode);
                if (cscpCSharpCodeProvider == null)
                {
                    var providerOptions = new Dictionary<string, string> {{"CompilerVersion", "v3.5"}};
                    cscpCSharpCodeProvider = new CSharpCodeProvider(providerOptions);
                }
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


                // need to add a solution to add LinkedResources to these dynamic compilation dlls
                //                cpCompilerParameters.LinkedResources.Add();
                

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
                //cpCompilerParameters.LinkedResources.Add(@"C:\O2\_XRules_Local\CodeCompletion\O2CodeComplete.resx");                
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
                    sbErrorMessage.AppendLine(String.Format("{0}::{1}::{2}::{3}::{4}", ceCompilerError.Line,
                                                            ceCompilerError.Column, ceCompilerError.ErrorNumber,
                                                            ceCompilerError.ErrorText, ceCompilerError.FileName));
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
        
        private void showErrorMessageIfPathHasParentheses(List<string> sourceCodeFiles)
        {
            foreach(var file in sourceCodeFiles)
                if(file.Contains("(") || file.Contains(")"))
                    PublicDI.log.error("File to compile had a parentheses so error messages will not have line numbers (see http://forums.asp.net/p/1009965/1556589.aspx): {0}", file);
        }

        public static void addExtraFileReferencesToSelectedNode(TreeView treeView, string file)
        {
            if (treeView != null)
                treeView.invokeOnThread(
                    ()=>
                    {
                        addExtraFileReferencesToTreeNode(treeView.SelectedNode,file);
                    });
        }

        public static void addExtraFileReferencesToTreeNode(TreeNode treeNode, string file)
        {
            if (treeNode != null && File.Exists(file))
            {
                treeNode.Nodes.Clear();
                // this will get the list of files to compile (which includes the extra files referenced in the source code that we want to add to this treeview)
                var filesToCompile = new List<string> {file};
                addSourceFileOrFolderIncludedInSourceCode(filesToCompile);
                filesToCompile.Remove(file);
                foreach (var extraFile in filesToCompile)
                {                    
                    O2Forms.newTreeNode(treeNode, Path.GetFileName(extraFile), 5, extraFile);                    
                }
                treeNode.ExpandAll();
            }
        }

        
    }
}
