// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Kernel.CodeUtils;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.External.WinFormsUI.Ascx
{
    partial class ascx_ViewO2Config
    {

        private void loadCurrentO2ConfigEnvironment()
        {
            try
            {
                // load main config file details
                lbLocationOfLocalConfigFile.Text = O2ConfigLoader.defaultLocationOfO2ConfigFile();
                tbMainO2ConfigFile.Text = Files.getFileContents(O2ConfigLoader.defaultLocationOfO2ConfigFile());
                webBrowserMainO2ConfigFile.Navigate(O2ConfigLoader.defaultLocationOfO2ConfigFile());


                // load local O2 config file details

                if (false == File.Exists(DI.config.O2ConfigFile))
                {
                    DI.log.error("in loadCurrentO2ConfigEnvironment, could not find main O2 Config file: {0}",
                                 DI.config.O2ConfigFile);
                    tbLocationOfLocalO2ConfigFile.Text = DI.config.O2ConfigFile;
                }
                else
                {
                    if (DI.config.O2ConfigFile == O2ConfigLoader.defaultLocationOfO2ConfigFile())
                    {
                        var parentDirectoryOfCurrentO2TempDir = Path.GetFullPath(Path.Combine(DI.config.O2TempDir, ".."));
                        // means there is no Local O2 Config file defined
                        tbLocationOfLocalO2ConfigFile.Text = Path.Combine(
                            (Directory.Exists(parentDirectoryOfCurrentO2TempDir)
                                 ? parentDirectoryOfCurrentO2TempDir + "\\_local_O2_data"
                                 : DI.config.O2TempDir)
                            , Environment.MachineName + "o2.config");
                    }
                    else
                    {
                        tbLocationOfLocalO2ConfigFile.Text = DI.config.O2ConfigFile;
                        tbLocalO2ConfigFile.Text = Files.getFileContents(DI.config.O2ConfigFile);
                        btSaveLocalConfigFile.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                DI.log.error("in loadCurrentO2ConfigEnvironment: {0}", ex.Message);
            }
        }

        private void saveMainO2ConfigFile()
        {            
            Files.WriteFileContent(O2ConfigLoader.defaultLocationOfO2ConfigFile(), tbMainO2ConfigFile.Text);
            var kO2Config = O2ConfigLoader.getKO2Config(); // get the updated KO2Config object
            O2ConfigLoader.mergeO2ConfigFiles(kO2Config,DI.config);    // and merge it with the current one
            loadCurrentO2ConfigEnvironment();
        }

        private void createLocalO2ConfigFile()
        {
            var targetFile = tbLocationOfLocalO2ConfigFile.Text;
            var targetDirectory = Path.GetDirectoryName(targetFile);
            Files.checkIfDirectoryExistsAndCreateIfNot(targetDirectory);
            O2ConfigLoader.createOrSetLocalConfigFile(targetFile);
            loadCurrentO2ConfigEnvironment();
        }

        private void setManualEditMode(bool manualEditMode)
        {
            tbMainO2ConfigFile.Visible = manualEditMode;
            webBrowserMainO2ConfigFile.Visible = !manualEditMode;
            btSaveMainO2ConfigFile.Visible = manualEditMode;            
        }

        private void showMemoryViewOfO2ConfigFiles()
        {
            var tempSettingsFile = DI.config.getTempFileInTempDirectory("xml");
            if (Serialize.createSerializedXmlFileFromObject(DI.config, tempSettingsFile))            
                webBrowser_O2ConfigFilesInMemory.Navigate(tempSettingsFile);
            lbDependencyInjectionTestValue.Text = KO2Config.dependencyInjectionTest ?? "";
        }

        private void saveLocalO2ConfigFile(string localO2ConfigFileLocation, string localO2ConfigFileContent)
        {
            if (false == doesTextBreaksO2ConfigSchema(localO2ConfigFileContent))
            {
                Files.WriteFileContent(localO2ConfigFileLocation, localO2ConfigFileContent);
                DI.config.dependenciesInjection.Clear(); // hack to handle reload of new config
                O2ConfigLoader.mergeO2ConfigFiles(O2ConfigLoader.loadO2Config(localO2ConfigFileLocation,false),DI.config);
                loadCurrentO2ConfigEnvironment();
            }
        }
        /// <summary>
        /// Returns true if the text provided cannot be serialized into an KO2Config object
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private bool doesTextBreaksO2ConfigSchema(string content)
        {
            return (null == Serialize.getDeSerializedObjectFromString(content, typeof (KO2Config),false));
        }    
    }
}
