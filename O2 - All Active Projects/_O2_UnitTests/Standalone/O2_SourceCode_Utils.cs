// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using O2.Kernel;
using O2.Kernel.Interfaces.O2Core;
using O2.DotNetWrappers.Windows;
//O2Tag_AddReferenceFile:nunit.framework.dll
using NUnit.Framework;

namespace O2.UnitTests.Standalone
{
    [TestFixture]
    public class O2_SourceCode_Utils
    {    
        private readonly static IO2Log log = PublicDI.log;    	    	
    	    	
        public static string sourceCodeLicense = "// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)";
 		
        public static string sourceCodeRoot = @"E:\O2\_SourceCode_O2\";
        public static List<string> foldersToApplyLicense = 
            new List<string> {"_O2_UnitTests", 
                              "O2_Cmd", 
                              "O2_Tools", 
                              "O2_XRules_Database", 
                              "O2Core", 
                              "RnD", 
                              "Scanners",
                              "O2 Modules Using 3rd Party Dlls", 
                              "Template Projects"};
 		
        public static List<string> foldersToRemoveLicense = 
            new List<string> {@"O2Core\O2_Debugger_Mdbg\OriginalMdbgCode"};
 		
        [Test]
        public void _Main_applyLicense()
        {
            applySourceCodeLicense();
            checkSourceCodeLicense();
            removeSourceCodeLicense();
            checkRemoveSourceCodeLicense();
        }
        [Test]
        public void applySourceCodeLicense()
        {
            var targetFiles = getTargetFiles(foldersToApplyLicense);
            var filesChanged = 0;
            foreach(var file in targetFiles)
            {
                var lines = Files.getFileLines(file); 				
                if (lines.Count > 0)
                {
                    if (lines[0] != sourceCodeLicense)
                    {
                        lines.Insert(0, sourceCodeLicense);
                        Files.saveAsFile_StringList(file, lines);
                        filesChanged++;						
                    }
                }
            }
            log.debug("in applySourceCodeLicense: There are {0} files modifed", filesChanged);
        }
 		
        [Test]
        public void checkSourceCodeLicense()
        {
            var targetFiles = getTargetFiles(foldersToApplyLicense); 			
            foreach(var file in targetFiles)
            {
                var lines = Files.getFileLines(file); 				
                //if (lines.Count > 0) 				
                Assert.That(lines[0] == sourceCodeLicense, "first line of file was not license: " + file);
            }
        }
 		
 		
        public List<string> getTargetFiles(List<string> targetFolders)
        {
            var targetFiles = new List<string>();
            foreach(var folder in targetFolders)
                targetFiles.AddRange(Files.getFilesFromDir_returnFullPath(sourceCodeRoot + folder,"*.cs", true));
            Assert.That(targetFiles.Count > 0 , "There were no target files");
            log.debug("There are {0} target files", targetFiles.Count);
            return targetFiles;
        }
 		
        [Test]
        public void removeSourceCodeLicense()
        {
            var targetFiles = getTargetFiles(foldersToRemoveLicense);
            var filesChanged = 0;
            foreach(var file in targetFiles)
            {
                var lines = Files.getFileLines(file); 				
                if (lines.Count > 0)
                {
                    if (lines[0] == sourceCodeLicense)
                    {
                        lines.RemoveAt(0);
                        Files.saveAsFile_StringList(file, lines);
                        filesChanged++;						
                    }
                }
            }
            log.debug("|In removeSourceCodeLicense: There are {0} files modifed", filesChanged);
        }
 		
        [Test]
        public void checkRemoveSourceCodeLicense()
        {
            var targetFiles = getTargetFiles(foldersToRemoveLicense);
            foreach(var file in targetFiles)
            {
                var lines = Files.getFileLines(file); 				 				
                Assert.That(lines[0] != sourceCodeLicense, "first line of file was the license: " + file);
            }
        }
 		
    }


}