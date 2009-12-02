using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace O2.External.IKVM.Ascx
{
    public partial class ascx_JavaExecution : UserControl
    {
        public ascx_JavaExecution()
        {
            InitializeComponent();
        }

        private void ascx_JavaExecution_Load(object sender, EventArgs e)
        {
            onLoad();
        }

        private void llLoadDefaultSetOfFilesToConvert_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            loadDefaultSetOfFilesToConvert();
        }

        private void btCreateJarStubFiles_Click(object sender, EventArgs e)
        {
            createJarStubFiles();
        }

        private void llDeleteJarStubs_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            deleteJarStubs();
        }

        private void llDeleteEmptyJarStubs_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            deleteEmptyJarStubs();
        }

        private void directoryToDropJarsToConvertIntoDotNetAssemblies__onTreeViewDrop(string droppedFileOrFolder)
        {
            convertJarToDotNetAssembly(droppedFileOrFolder, directoryToDropJarsToConvertIntoDotNetAssemblies.getCurrentDirectory());
        }                                
    }
}
