using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.Zip;
using O2.Kernel.Interfaces.DotNet;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.Core.CIR.Ascx.DotNet
{
    partial class ascx_GAC_Browser
    {

        public Func<string, bool> treeViewColorFilter = null;
        public event O2Thread.FuncVoidT1<IGacDll> _onGacDllSelected;

        public void setTreeViewcolorFilter( Func<string, bool>  filter)
        {
            treeViewColorFilter = filter;
        }

        static public List<IGacDll> currentGacAssemblies()
        {
            var gacAssemblies = new List<IGacDll>();
            foreach (var directory in Files.getListOfAllDirectoriesFromDirectory(DI.PathToGac, true))
            {
                if (DI.PathToGac != Path.GetDirectoryName(directory))
                {
                    var name = Path.GetFileName(Path.GetDirectoryName(directory));
                    var version = Path.GetFileName(directory);
                    var fullPath = Path.Combine(directory, name + ".dll");
                    if (File.Exists(fullPath))
                        gacAssemblies.Add(new KGacDll(name, version, fullPath));
                    else
                    {
                        // handle the rare cases when the assembly is an *.exe
                        fullPath = Path.Combine(directory, name + ".exe");
                        if (File.Exists(fullPath))
                            gacAssemblies.Add(new KGacDll(name, version, fullPath));
                        else
                            DI.log.error("ERROR in currentGacAssemblies: could not find: {0}", fullPath);
                    }
                }
            }
            return gacAssemblies;
        }

        public static void backupGac()
        {
            backupGac(DI.config.getTempFileInTempDirectory("zip"));
        }

        public static void backupGac(string zipFileToSaveGacContents)
        {
            O2Thread.mtaThread(
                () =>
                {
                    DI.log.info("Started unzip process of Gac Folder");
                    var timer = new O2Timer("Gac Backup").start();
                    new zipUtils().zipFolder(DI.PathToGac, zipFileToSaveGacContents);
                    var logMessage = String.Format("Contents of \n\n\t{0}\n\n saved to \n\n\t{1}\n\n ", DI.PathToGac, zipFileToSaveGacContents);
                    timer.stop();
                    DI.log.info(logMessage);
                    DI.log.showMessageBox(logMessage);
                });
        }

        public string getBackupFilePath(string targetFolder)
        {
            return Path.Combine(targetFolder, string.Format("GAC_Backup_{0}.zip", DateTime.Now.ToShortDateString()));
        }

        public void refreshListOfGacAssemblies()
        {
            loadListOfGacAssemblies(tvListOfGacAssemblies, tbGacAssemblyFilter.Text);
        }

        public void loadListOfGacAssemblies(string filter, List<IGacDll> assembliesToLoad)
        {
            this.invokeOnThread(() => loadListOfGacAssemblies(tvListOfGacAssemblies, filter, assembliesToLoad));
        }

        public void loadListOfGacAssemblies()
        {
            this.invokeOnThread(() => loadListOfGacAssemblies(tvListOfGacAssemblies, tbGacAssemblyFilter.Text));
        }

        public void loadListOfGacAssemblies(TreeView lbListOfGacAssemblies, string filter)
        {
            loadListOfGacAssemblies(lbListOfGacAssemblies, filter, currentGacAssemblies());
        }
       
        public void loadListOfGacAssemblies(TreeView lbListOfGacAssemblies, string filter, List<IGacDll> assembliesToLoad)
        {
            this.invokeOnThread(
                () =>
                {
                    tvListOfGacAssemblies.Nodes.Clear();
                    foreach (var gacAssembly in assembliesToLoad)
                        if (RegEx.findStringInString(gacAssembly.name, filter))
                        {
                            var newTreeNode = new TreeNode(gacAssembly.name)
                                                  {
                                                      Tag = gacAssembly
                                                  };
                            if (treeViewColorFilter != null && filter != "")
                                // for performance reasons only apply this when there is a filter
                                if (treeViewColorFilter(gacAssembly.fullPath))
                                    // move this code to the consumers of this assembly
                                    /* if (PostSharpUtils.containsO2PostSharpHooks(gacAssembly.fullPath))*/
                                    newTreeNode.ForeColor = Color.DarkGreen;
                                else
                                    newTreeNode.ForeColor = Color.DarkRed;
                            tvListOfGacAssemblies.Nodes.Add(newTreeNode);
                        }
                });
        }
        

        private void tbGacAssemblyFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loadListOfGacAssemblies(tvListOfGacAssemblies, tbGacAssemblyFilter.Text);                
            }
        }

        public List<IGacDll> getListOfCurrentFilteredAssemblies()
        {
            var results = new List<IGacDll>();
            foreach (TreeNode treeNode in tvListOfGacAssemblies.Nodes)
                if (treeNode.Tag != null && treeNode.Tag is IGacDll)
                    results.Add((IGacDll)treeNode.Tag);
            return results;
        }


        public IGacDll getSelectedGacAssembly()
        {
            if (tvListOfGacAssemblies.SelectedNode != null && tvListOfGacAssemblies.SelectedNode.Tag is IGacDll)
                return (IGacDll) tvListOfGacAssemblies.SelectedNode.Tag;
            return null;
        }
    }
}
