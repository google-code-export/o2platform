// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using FluentSharp.O2.DotNetWrappers.Windows;
using FluentSharp.O2.Kernel.CodeUtils;
using FluentSharp.O2.Kernel.InterfacesBaseImpl;
using FluentSharp.O2.Views.ASCX;
using FluentSharp.O2.Views.ASCX.CoreControls;
using FluentSharp.O2.Views.ASCX.SourceCodeEdit;

namespace V2.O2.External.SharpDevelop.Ascx
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
