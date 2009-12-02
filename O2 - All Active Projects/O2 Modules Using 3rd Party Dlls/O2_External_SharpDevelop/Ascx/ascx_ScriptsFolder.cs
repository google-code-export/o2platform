using System.IO;
using System.Reflection;
using System.Windows.Forms;
using O2.DotNetWrappers.Windows;
using O2.Kernel.CodeUtils;
using O2.Kernel.CodeUtils;
using O2.Kernel.InterfacesBaseImpl;
using O2.Views.ASCX;
using O2.Views.ASCX.CoreControls;
using O2.Views.ASCX.SourceCodeEdit;

namespace O2.External.SharpDevelop.Ascx
{
    public partial class ascx_ScriptsFolder : UserControl
    {
        public ascx_ScriptsFolder()
        {
            InitializeComponent();
            directoryWithSourceCodeFiles.eDirectoryEvent_Click += directoryWithSourceCodeFiles_eDirectoryEvent_Click;
            loadDefaultScriptLocation();
            //loadSampleScripts();
        }        

        void directoryWithSourceCodeFiles_eDirectoryEvent_Click(string sValue)
        {
            openSourceCodeFile(sValue);
        }                

        private void tvSampleScripts_AfterSelect(object sender, TreeViewEventArgs e)
        {
            processSelectedSampleScript();
        }

        private void tvSampleScripts_DoubleClick(object sender, System.EventArgs e)
        {
            processSelectedSampleScript();
        }        
    }
}