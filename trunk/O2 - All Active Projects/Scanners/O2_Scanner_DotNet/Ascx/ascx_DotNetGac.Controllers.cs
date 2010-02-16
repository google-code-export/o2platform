// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Interfaces.DotNet;
using O2.Kernel.InterfacesBaseImpl;
using O2.DotNetWrappers.DotNet;
using O2.Core.CIR.CirUtils;
using O2.Core.CIR.CirCreator.DotNet;
using O2.Core.CIR.CirObjects;
using O2.External.O2Mono.MonoCecil;
using O2.Scanner.DotNet.PostSharp;
using O2.DotNetWrappers.Windows;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;

namespace O2.Scanner.DotNet.Ascx
{
	partial class ascx_DotNetGac
	{
        
        // btInstallHooksOnAllFiltered.Enabled = btUnInstallHooksOnAllFiltered.Enabled = (tbGacAssemblyFilter.Text != "");

        /*private void loadListOfGacAssemblies(TreeView tvListOfGacAssemblies, string p, List<IGacDll> assembliesToLoad)
        {
            throw new NotImplementedException();
        }

        private void refreshListOfGacAssemblies()
        {
            throw new NotImplementedException();
        }

        private void loadListOfGacAssemblies(TreeView tvListOfGacAssemblies, string p)
        {
            throw new NotImplementedException();
        }*/

        private bool runOnLoad = true;

        private void onLoad()
        {
            if (DesignMode == false && runOnLoad)
            {
                gacBrowser.loadListOfGacAssemblies();//tvListOfGacAssemblies, tbGacAssemblyFilter.Text);
                runOnLoad = false;
                tbDirectoryToLoadAssembliesFrom.Text = Path.Combine(DI.config.O2TempDir, "_testAssembliesFor_O2DotNetScanner");
                // set Gac_Browser treefilter
                gacBrowser.treeViewColorFilter = PostSharpUtils.containsO2PostSharpHooks;
            }
        }

        private void loadAssembliesFromDirectory(string pathToDirectoryToLoad)
        {
            var filesToLoad = new List<string>();
            filesToLoad.AddRange(Files.getFilesFromDir_returnFullPath(pathToDirectoryToLoad, "*.dll"));
            filesToLoad.AddRange(Files.getFilesFromDir_returnFullPath(pathToDirectoryToLoad, "*.exe"));
            var assembliesToLoad = new List<IGacDll>();
            foreach(var file in filesToLoad)
                assembliesToLoad.Add(new KGacDll(Path.GetFileName(file),"..", file));
            gacBrowser.loadListOfGacAssemblies("", assembliesToLoad);            
        }
        
        

        private void showPostSharpInstallStatus(IGacDll gacDll)
        {
            //if (gacDll.PostSharpHooks == PostSharpHookStatus.NotCalculated)
            if (PostSharpUtils.containsO2PostSharpHooks(gacDll.fullPath))
                gacDll.PostSharpHooks = PostSharpHookStatus.Installed;
            else
                gacDll.PostSharpHooks = PostSharpHookStatus.NotInstalled;
            switch (gacDll.PostSharpHooks)
            { 
                case PostSharpHookStatus.Installed:
                    lbPostSharpHooksState.Text = "YES";
                    lbPostSharpHooksState.ForeColor = Color.DarkGreen;
                    btInstallPostSharpHooks.Enabled = false;
                    btUnInstallPostSharpHooks.Enabled = true;
                    break;
                case PostSharpHookStatus.NotInstalled:
                    lbPostSharpHooksState.Text = "NO";
                    lbPostSharpHooksState.ForeColor = Color.DarkRed;
                    btInstallPostSharpHooks.Enabled = true;
                    btUnInstallPostSharpHooks.Enabled = false;
                    break;
                case PostSharpHookStatus.NotCalculated:
                    lbPostSharpHooksState.Text = "...";
                    lbPostSharpHooksState.ForeColor = Color.Black;
                    btInstallPostSharpHooks.Enabled = false;
                    btUnInstallPostSharpHooks.Enabled = false;
                    break;

            }
            
        }

        public void loadAndShowCirForAssembly(IGacDll gacDll)
        {
            O2Thread.mtaThread(
                () =>
                {
                    gacDll.cirData = new CirData();
                    //var assemblyToConvert = CecilUtils.getAssembly(gacDll.fullPath);

                    new CirFactory().processAssemblyDefinition(gacDll.cirData, gacDll.fullPath);
                                        
                    cirDataViewer.loadCirData(gacDll.cirData, true);
                    cirDataViewer.showLoadedFunctions();
                });
        }        

        public void UnInstallPostSharpHook(IGacDll iGacDll)
        {
            if (false == BackupRestoreFiles.doesBackupExist(iGacDll.fullPath))
            {
                DI.log.error("Error in UnInstallPostSharpHook, could not find backup file for file: {0}", iGacDll.fullPath);
            }
            else
            {
                IISDeployment.kill_IIS_Process_W3wp();                    
                if (BackupRestoreFiles.restore(iGacDll.fullPath))
                    DI.log.debug("Gac Assembly uninstalled/restored: {0} (in {1})", iGacDll.name, iGacDll.fullPath);
            } 
            showGacAssemblyDetails(iGacDll,false);
            //throw new NotImplementedException();
        }

        public void InstallPostSharpHook(IGacDll iGacDll, string typeToHook, string methodToHook)
        {
            if (false == PostSharpUtils.containsO2PostSharpHooks(iGacDll.fullPath))
            {                
                IISDeployment.kill_IIS_Process_W3wp();                    
                BackupRestoreFiles.backup(iGacDll.fullPath);
                if (PostSharpExecution.InsertHooksAndRunPostSharpOnAssembly(iGacDll.fullPath, typeToHook, methodToHook))
                    DI.log.debug("PostSharp hooks installed on Gac Assembly: {0} (in {1})", iGacDll.name, iGacDll.fullPath);

            }
            showGacAssemblyDetails(iGacDll, false);
            //throw new NotImplementedException();
        }

        private void unInstallHooksOn(List<IGacDll> gacDllsToUnInstall)
        {
            O2Thread.mtaThread(
                () =>
                {
                    foreach (var gacDll in gacDllsToUnInstall)
                    {
                        UnInstallPostSharpHook(gacDll);
                    }
                    gacBrowser.refreshListOfGacAssemblies();
                });
        }

        private void installHooksOn(List<IGacDll> gacDllsToInstall)
        {
            O2Thread.mtaThread(
                () =>
                {
                    foreach (var gacDll in gacDllsToInstall)
                    {
                        InstallPostSharpHook(gacDll,"","");
                    }
                    gacBrowser.refreshListOfGacAssemblies();
                });
        }
        

        

        private void testDllCopy(IGacDll iGacDll)
        {
            IISDeployment.kill_IIS_Process_W3wp();
            var tempDllLocation = iGacDll.fullPath + ".temp";
            if (File.Exists(iGacDll.fullPath) && false == File.Exists(tempDllLocation))
            {
                Files.MoveFile(iGacDll.fullPath, tempDllLocation);
                if (false == File.Exists(iGacDll.fullPath) && File.Exists(tempDllLocation))
                {
                    Files.MoveFile(tempDllLocation, iGacDll.fullPath);
                    if (File.Exists(iGacDll.fullPath) && false == File.Exists(tempDllLocation))
                        DI.log.debug("testDllCopy worked");
                    else
                        DI.log.error("in testDllCopy, restore failed");
                }
                else
                    DI.log.error("in testDllCopy, move failed");

            }
            else
                DI.log.error("in testDllCopy, could not find gacDll or tempfile already exists");
            if (File.Exists(iGacDll.fullPath) && File.Exists(tempDllLocation))
                Files.deleteFile(tempDllLocation);
        }

/*        private void tvListOfGacAssemblies_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvListOfGacAssemblies.SelectedNode != null && tvListOfGacAssemblies.SelectedNode.Tag is IGacDll)
                showGacAssemblyDetails((IGacDll)tvListOfGacAssemblies.SelectedNode.Tag, cbLoadCirDataForSelectedAssembly.Checked);
        }*/

        public void showGacAssemblyDetails(IGacDll gacDll, bool loadCirDataForSelectedAssembly)
        {
            this.invokeOnThread(
                () =>
                {
                    directoryOfSelectedAssembly.openDirectory(Path.GetDirectoryName(gacDll.fullPath));
                    lbSelectedGacAssembly_name.Text = gacDll.name;
                    lbSelectedGacAssembly_version.Text = gacDll.version;
                    lbSelectedGacAssembly_fullPath.Text = gacDll.fullPath;
                    showPostSharpInstallStatus(gacDll);
                    if (loadCirDataForSelectedAssembly)
                    {
                        loadAndShowCirForAssembly(gacDll);
                    }
                });
        }
	}
}
