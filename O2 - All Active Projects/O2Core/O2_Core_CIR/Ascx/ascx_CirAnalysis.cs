using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.Core.CIR.CirObjects;
using O2.DotNetWrappers.DotNet;
using O2.Kernel;

namespace O2.Core.CIR.Ascx
{
    public partial class ascx_CirAnalysis : UserControl
    {
        public ascx_CirAnalysis()
        {
            InitializeComponent();
        }

        private void ascx_DropObject1_eDnDAction_ObjectDataReceived_Event(object oObject)
        {
            
                                       loadFileOrFolder(oObject);
                                   
        }

        public void loadFileOrFolder(object fileOrFolder)
        {
            this.invokeOnThread(
                () =>
                    {
                        if (cbClearPreviousO2CirData.Checked)
                        {
                            cirDataAnalysis = new CirDataAnalysis();
                            lbLoadedO2CirDataFiles.Items.Clear();
                        }
                        O2Thread.mtaThread(
                            () =>
                                {

                                    if (Directory.Exists(fileOrFolder.ToString()))
                                    {
                                        foreach (String sFile in Directory.GetFiles(fileOrFolder.ToString()))
                                        {
                                            loadO2CirDataFile(sFile, false);
                                            Application.DoEvents();
                                        }
                                        raiseSetCirDataAnalysisO2Message();
                                    }
                                    else
                                        loadO2CirDataFile(fileOrFolder.ToString(), true);
                                });
                    });
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {
            if (false == DesignMode)
            {
                PublicDI.o2MessageQueue.onMessages += o2MessageQueue_onMessages;
            }
        }

        private void ascx_DropObject1_Load(object sender, EventArgs e)
        {

        }

        private void lbLoadedO2CirDataFiles_DragDrop(object sender, DragEventArgs e)
        {
            loadFileOrFolder(Dnd.tryToGetFileOrDirectoryFromDroppedObject(e));
        }

        private void lbLoadedO2CirDataFiles_DragEnter(object sender, DragEventArgs e)
        {
            Dnd.setEffect(e);            
        }

        private void llClearLoadedData_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clearLoadedData();
        }
               
    }
}
