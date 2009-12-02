using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.Kernel.Interfaces.DotNet;
using O2.Scanner.DotNet.PostSharp;
using O2.DotNetWrappers.DotNet;

namespace O2.Scanner.DotNet.Ascx
{
    public partial class ascx_DotNetGac : UserControl
    {
        public ascx_DotNetGac()
        {
            InitializeComponent();
        }

        private void btLoadListOfGacAssemblies_Click(object sender, EventArgs e)
        {
            gacBrowser.loadListOfGacAssemblies();            
        }

        

        private void cbEnableBackupButton_CheckedChanged(object sender, EventArgs e)
        {
            btBackupGacToZipFile.Enabled = true;
        }

        private void lbSelectedGacAssembly_fullPath_Click(object sender, EventArgs e)
        {
            var clipboardText = lbSelectedGacAssembly_fullPath.Text;
            DI.log.info("Copied to Clipboard: {0}", clipboardText); ;
            Clipboard.SetText(clipboardText);
        }

        private void btStopIIS_Click(object sender, EventArgs e)
        {
            PostSharp.IISDeployment.kill_IIS_Process_W3wp();            
        }

        

        private void ascx_DotNetGac_Load(object sender, EventArgs e)
        {
            onLoad();
        }

        private void btUnInstallPostSharpHooks_Click(object sender, EventArgs e)
        {
            UnInstallPostSharpHook(gacBrowser.getSelectedGacAssembly());
            /*if (tvListOfGacAssemblies.SelectedNode != null && tvListOfGacAssemblies.SelectedNode.Tag is IGacDll)
                UnInstallPostSharpHook((IGacDll)tvListOfGacAssemblies.SelectedNode.Tag);*/
        }        

        private void btInstallPostSharpHooks_Click(object sender, EventArgs e)
        {
            /*if (tvListOfGacAssemblies.SelectedNode != null && tvListOfGacAssemblies.SelectedNode.Tag is IGacDll)*/

            InstallPostSharpHook(gacBrowser.getSelectedGacAssembly(),
                    (cbHookOnType.Checked) ? tbHookOn_Type.Text : "", 
                    (cbHookOnMethod.Checked) ? tbHookOn_Method.Text : "");
        }

       

        private void btInstallHooksOnAllFiltered_Click(object sender, EventArgs e)
        {
            installHooksOn(gacBrowser.getListOfCurrentFilteredAssemblies());
        }

       

        private void btUnInstallHooksOnAllFiltered_Click(object sender, EventArgs e)
        {
            unInstallHooksOn(gacBrowser.getListOfCurrentFilteredAssemblies());
        }
        

        private void btLoadDllsFromDirectory_Click(object sender, EventArgs e)
        {
            loadAssembliesFromDirectory(tbDirectoryToLoadAssembliesFrom.Text);
        }

        private void btDeployHostAssemblyToGac_Click(object sender, EventArgs e)
        {
            IISDeployment.deployToGac(true);
        }

        private void lbSelectedTypeToHook_DragEnter(object sender, DragEventArgs e)
        {
            Dnd.setEffect(e);
        }

        private void lbSelectedTypeToHook_DragDrop(object sender, DragEventArgs e)
        {
            var cirClass = Dnd.tryToGetObjectFromDroppedObject(e);//, typeof(O2.Kernel.Interfaces.CIR.ICirFunction));
        }

        private void cbHookOnMethod_CheckedChanged(object sender, EventArgs e)
        {
            if (cbHookOnMethod.Checked)
                cbHookOnType.Checked = true;
        }

        private void tbHookOn_Type_TextChanged(object sender, EventArgs e)
        {
            cbHookOnType.Checked = true;
        }

        private void tbHookOn_Method_TextChanged(object sender, EventArgs e)
        {
            cbHookOnMethod.Checked = true;
        }

        private void btTestDllCopy_Click(object sender, EventArgs e)
        {
            var selectedGacAssembly = gacBrowser.getSelectedGacAssembly();
            if (selectedGacAssembly != null)
                testDllCopy(selectedGacAssembly);
        }

        private void tvListOfGacAssemblies_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void gacBrowser__onGacDllSelected(IGacDll selectedGacAssembly)
        {
            showGacAssemblyDetails(selectedGacAssembly, cbLoadCirDataForSelectedAssembly.Checked);
        }                      
    }
}
