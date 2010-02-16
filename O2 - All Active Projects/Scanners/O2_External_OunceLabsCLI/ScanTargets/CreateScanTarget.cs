// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.Zip;
using O2.Interfaces.Controllers;

namespace O2.Scanner.OunceLabsCLI.ScanTargets
{
    public class CreateScanTarget
    {
        public static List<IScanTarget> createScanTargetsFromFileOrFolder(string fileOrFolder)
        {
            return createScanTargetsFromFileOrFolder(fileOrFolder,
                                                     DI.config.O2TempDir,
                                                     true /*autoAppendTargetName*/
                );
        }

        public static List<IScanTarget> createScanTargetsFromFileOrFolder(string fileOrFolder, string workDirectory,
                                                                                  bool autoAppendTargetName)
        {
            return createScanTargetsFromFileOrFolder(fileOrFolder, workDirectory, autoAppendTargetName, false
                /*searchForScanFilesRecursively*/);
        }

        public static List<IScanTarget> createScanTargetsFromFileOrFolder(string fileOrFolder, string workDirectory,
                                                                          bool autoAppendTargetName, bool searchForScanFilesRecursively)
        {
            if (File.Exists(fileOrFolder) && Path.GetExtension(fileOrFolder) == ".zip")
            {
                string folderToUnzipFiles = Path.Combine(workDirectory, Path.GetFileNameWithoutExtension(fileOrFolder));
                new zipUtils().unzipFile(fileOrFolder, folderToUnzipFiles + "\\src");
                fileOrFolder = folderToUnzipFiles;
            }
            var scanTargets = new List<IScanTarget>();
            if (Directory.Exists(fileOrFolder))
            {
            /*    foreach (var scanTargetFile in Files.getFilesFromDir_returnFullPath(fileOrFolder, "*.paf", true))
                {
                                    
                //string sPafFile = Files.getFirstFileFromDirThatMatchesPattern_returnFulPath(fileOrFolder, "*.paf");
                //if (sPafFile != "")
                //{
                    //    if (Path.GetFileName(fileOrFolder) == Path.GetFileNameWithoutExtension(sPafFile.Replace(".paf", "")))
                    var scanTarget_Paf = new ScanTarget_Paf
                                             {
                                                 WorkDirectory = fileOrFolder,
                                                 Target = scanTargetFile
                                             };
                    scanTargets.Add(scanTarget_Paf);
                }
            //}
                else*/
                foreach (string sFile in Files.getFilesFromDir_returnFullPath(fileOrFolder, "*.*", searchForScanFilesRecursively))
                    {
                        IScanTarget scanTarget = createScanTargetsFromFile(sFile, workDirectory, autoAppendTargetName);
                        if (scanTarget != null)
                            scanTargets.Add(scanTarget);
                    }
            }
            else if (File.Exists(fileOrFolder))
            {
                IScanTarget scanTarget = createScanTargetsFromFile(fileOrFolder, workDirectory, autoAppendTargetName);
                if (scanTarget != null)
                    scanTargets.Add(scanTarget);
            }
            return scanTargets;
        }

        public static IScanTarget createScanTargetsFromFile(String fileToProcess, string workDirectory,
                                                            bool autoAppendTargetName)
        {
            IScanTarget scanTarget = null;
            switch (Path.GetExtension(fileToProcess))
            {
                case ".sln":
                case ".paf":
                case ".gaf":
                case ".ewf":
                    scanTarget = new ScanTarget_Paf();
                    break;
                case ".java":
                case ".class":                
                    scanTarget = new ScanTarget_Java();
                    break;
                case ".dll":
                case ".exe":
                    scanTarget = new ScanTarget_DotNet();
                    break;
                case ".epf":
                case ".ppf":
                case ".gpf":
                    scanTarget = new ScanTarget_Paf();
                    fileToProcess = Utils.ScanSupport.createTempApplicationFileForProject(fileToProcess, true, workDirectory);
                    break;
                default:
                    DI.log.debug("in addFileToProcess, file type not supported: {0}:",
                                 Path.GetExtension(fileToProcess));
                    break;
            }
            if (scanTarget != null)
            {
                scanTarget.useFileNameOnWorkDirecory = autoAppendTargetName;
                scanTarget.WorkDirectory = workDirectory;
                scanTarget.Target = fileToProcess;
            }
            return scanTarget;
        }

        public static IScanTarget forJava(string folderWithFilesToScan, string directoryToCreateOunceProjectFiles)
        {
            var scanTarget = new ScanTarget_Java();
            scanTarget.useFileNameOnWorkDirecory = false;
            scanTarget.WorkDirectory = directoryToCreateOunceProjectFiles;
            scanTarget.Target = folderWithFilesToScan;
            DI.log.info("Created Java ScanTraget on {0} for files in {1} ", directoryToCreateOunceProjectFiles, folderWithFilesToScan);
            return scanTarget;
        }
    }
}
