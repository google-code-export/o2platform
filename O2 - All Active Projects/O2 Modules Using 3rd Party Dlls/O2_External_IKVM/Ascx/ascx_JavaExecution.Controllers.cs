// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Collections.Generic;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.Windows;
using O2.External.IKVM.IKVM;
using O2.Kernel.CodeUtils;
using System.IO;

namespace O2.External.IKVM.Ascx
{
    public partial class ascx_JavaExecution
    {
        public bool runOnLoad = true;

        private void onLoad()
        {
            if (DesignMode == false && runOnLoad)
            {
                loadDefaultSetOfFilesToConvert();
                directoryWithJarStubFiles.openDirectory(IKVMConfig.jarStubsCacheDir);
                directoryToDropJarsToConvertIntoDotNetAssemblies.openDirectory(IKVMConfig.convertedJarsDir);
            }
        }

        public void loadDefaultSetOfFilesToConvert()
        {
            dotNetAssembliesToConvert.clearMappings();
            dotNetAssembliesToConvert.setExtensionsToShow(".dll .exe");
            dotNetAssembliesToConvert.addFiles(CompileEngine.getListOfO2AssembliesInExecutionDir());
            dotNetAssembliesToConvert.addFiles(AppDomainUtils.getDllsInCurrentAppDomain_FullPath());
            runOnLoad = false;
        }

        private void createJarStubFiles()
        {
            O2Thread.mtaThread(
                () =>
                    {
                        // reset progress bar values
                        this.invokeOnThread(() =>
                                                {
                                                    progressBarForJarStubCreation.Maximum = dotNetAssembliesToConvert.loadedFiles.Count;
                                                    progressBarForJarStubCreation.Value = 0;
                                                    btCreateJarStubFiles.Enabled = false;
                                                });
                        // process all files in dotNetAssembliesToConvert
                        foreach (var fileToProcess in dotNetAssembliesToConvert.loadedFiles)
                        {
                            var jarStubFile = JavaCompile.createJarStubForDotNetDll(fileToProcess, IKVMConfig.jarStubsCacheDir);
                            if (!File.Exists(jarStubFile))
                                DI.log.error("Jar stub file not created for :{0}", jarStubFile);
                            this.invokeOnThread(() => progressBarForJarStubCreation.Value++);
                        }
                        deleteEmptyJarStubs();
                        this.invokeOnThread(() => btCreateJarStubFiles.Enabled = true);                                                
                    });            
        }

        private void deleteJarStubs()
        {
            Files.deleteFiles(directoryWithJarStubFiles.getFiles(),true);            
        }

        private void deleteEmptyJarStubs()
        {
            var filesToDelete = new List<string>();
            foreach (var file in directoryWithJarStubFiles.getFiles())
            {
                var fileSize = Files.getFileSize(file);
                if (fileSize == 0)
                    filesToDelete.Add(file);
            }
            Files.deleteFiles(filesToDelete, true);
        }


        private void convertJarToDotNetAssembly(string droppedFileOrFolder, string targetDirectory)
        {
            if (File.Exists(droppedFileOrFolder))
                O2Thread.mtaThread(
                    () =>
                        {
                            directoryToDropJarsToConvertIntoDotNetAssemblies.setDisableMove(false);
                            IKVMUtils.convertJarFileIntoDotNetAssembly(droppedFileOrFolder, targetDirectory);
                            directoryToDropJarsToConvertIntoDotNetAssemblies.setDisableMove(true);
                        });
            else
                DI.log.error("in convertJarToDotNetAssembly, only files supported");
        }
    }
}
