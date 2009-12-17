// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
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
            loadListOfGacAssemblies(lbListOfGacAssemblies, filter, GacUtils.currentGacAssemblies());
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
               refreshView();
            }
        }
        
        public void refreshView()
        {
             this.invokeOnThread(()=>
                loadListOfGacAssemblies(tvListOfGacAssemblies, tbGacAssemblyFilter.Text));
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

        public string getGacAssemblyFilter()
        {
            return (string)this.invokeOnThread(() => { return tbGacAssemblyFilter.Text; });
        }

        public void setGacAssemblyFilter(string value)
        { 
            this.invokeOnThread(
                ()=> { 
                        tbGacAssemblyFilter.Text = value;
                    refreshView();
                });
        }
    }
}
