// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.Core.XRules.XRulesEngine;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.O2Misc;
using O2.DotNetWrappers.Windows;
using O2.External.SharpDevelop.Ascx;

namespace O2.Core.XRules.Ascx
{
    partial class ascx_XRules_Editor
    {
        private bool runOnLoad = true;

        public Dictionary<string, TabPage> filesLoaded = new Dictionary<string, TabPage>();  

        private void onLoad()
        {
            if (DesignMode == false && runOnLoad)
            {
                loadXRuleDatabase();                                
                runOnLoad = false;
            }

        }

        public void loadXRuleDatabase()
        {
            directoryWithXRulesDatabase.openDirectory(XRules_Config.PathTo_XRulesDatabase_fromO2);
            directoryWithLocalXRules.openDirectory(XRules_Config.PathTo_XRulesDatabase_fromLocalDisk);
            XRules_DatabaseSetup.installXRulesDatabase();
            XRules_DatabaseSetup.loadXRulesTemplates(lbCurrentXRulesTemplates);
        }

        public void openXRule(string fileOrDir)
        {
            loadFile(fileOrDir,false);
        }

        public string loadFile(string fileOrDir)
        {
            return loadFile(fileOrDir, false);
        }
        public string loadFile(string fileOrDir, bool compileLoadedFile)
        {
            var fileToOpen = "";
            if (File.Exists(fileOrDir))
                fileToOpen = fileOrDir;
            else if (Directory.Exists(fileOrDir))
                return ""; // no suport for dirs
            else
            {
                fileToOpen = Path.Combine(directoryWithXRulesDatabase.getCurrentDirectory(), fileOrDir);
                if (false == File.Exists(fileToOpen))
                    return "";
            }
            this.invokeOnThread(() =>
                    {

                        var fileName = Path.GetFileName(fileToOpen);
                        // check if there is already a tab with this file loaded
                        if (filesLoaded.ContainsKey(fileToOpen))
                            tcTabControlWithRulesSource.SelectedTab = filesLoaded[fileToOpen];
                        else
                        {
                            var newTabPage = new TabPage(fileName);
                            tcTabControlWithRulesSource.TabPages.Add(newTabPage);
                            loadSourceCodeFileIntoTab(fileToOpen, newTabPage, compileLoadedFile);
                            
                            if (tcTabControlWithRulesSource.TabPages.Contains(tpNoRulesLoaded))
                                tcTabControlWithRulesSource.TabPages.Remove(tpNoRulesLoaded);

                            tcTabControlWithRulesSource.SelectedTab = newTabPage;

                            // finally add to dictionary
                            filesLoaded.Add(fileToOpen, newTabPage);
                        }
                    });
            return fileToOpen;
        }
        

        private void loadSourceCodeFileIntoTab(string fileToOpen, TabPage tabPage, bool compileLoadedFile)
        {
            var sourceCodeEditor = new ascx_SourceCodeEditor {Dock = DockStyle.Fill};
            tabPage.Controls.Add(sourceCodeEditor);
            sourceCodeEditor.loadSourceCodeFile(fileToOpen);
            if (compileLoadedFile)
                sourceCodeEditor.compileSourceCode();
        }

        public void openSourceDirectory(string directoryToOpen)
        {
            directoryWithXRulesDatabase.openDirectory(directoryToOpen);
        }

        public List<String> getXRulesSourceFilesInCurrentDirectory()
        {
            return directoryWithXRulesDatabase.getFiles();
        }

        public string createNewRuleFromTemplate(string templateToUse, string newRuleName)
        {            
            if (File.Exists(templateToUse) == false)
                DI.log.error("In createNewRuleFromTemplate, could not find template file: {0}", templateToUse);
            else
            {
                //var newRuleFile = Path.Combine(XRules_Config.PathTo_XRulesDatabase_fromLocalDisk, newRuleName);
                var newRuleFile = Path.Combine(directoryWithLocalXRules.getCurrentDirectory(), newRuleName); // TODO: move directoryWithLocalXRules.getCurrentDirectory() to the upper level
                if (Path.GetExtension(newRuleFile) != ".cs")
                    newRuleFile += ".cs";
                if (false == File.Exists(newRuleFile))
                {
                    Files.WriteFileContent(newRuleFile, Files.getFileContents(templateToUse));
                    if (File.Exists(newRuleFile))
                        return newRuleFile;
                }
            }
            return "";
        }

        public void reloadFile(ascx_SourceCodeEditor sourceCodeEditor)
        {
            sourceCodeEditor.reloadCurrentFile();
        }

        private void removeFileInTab(TabPage tabToRemove)
        {
            foreach(var loadedFile in filesLoaded)
                if (loadedFile.Value == tabToRemove)
                {
                    tcTabControlWithRulesSource.TabPages.Remove(loadedFile.Value);
                    filesLoaded.Remove(loadedFile.Key);
                    return;
                }            
        }
    }
}
