// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using O2.Cmd.SpringMvc.Classes;
using O2.Cmd.SpringMvc.Objects;
using O2.Cmd.SpringMvc.PythonScripts;
using O2.Core.CIR.CirCreator.Java;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirUtils;
using O2.DotNetWrappers.DotNet;

namespace O2.Cmd.SpringMvc.Ascx
{
    public partial class ascx_CreateSpringMvcMappings : UserControl
    {
        public ascx_CreateSpringMvcMappings()
        {
            InitializeComponent();
        }

        private void ascx_ViewSpringMvcControler_Load(object sender, EventArgs e)
        {
            onLoad();
        }

        private void lboxClassFilesAnalysed_DragEnter(object sender, DragEventArgs e)
        {
            Dnd.setEffect(e);
        }
        

        /*private void btLoadXmlFilesFromTempDir_Click(object sender, EventArgs e)
        {
            convertXmlAttributeFilesToSpringMvcControllersObjects(AnnotationsHelper.tempFolderForAnnotationsXmlFiles);
        }*/

       

        private void btCreateFimdingsFromSpringMvcMappings_Click(object sender, EventArgs e)
        {
            CreateFindingsFromMvcData.createFindingsFromSpringMvcMappings(cbCreateFindingForUsesOfModelAttribute.Checked, cbCreateFindingForUsesOfGetParameter.Checked, findingsViewer, springMvcMappings.treeNodesForloadedSpingMvcControllers, springMvcMappings.cirData);
        }

        private void dropObject_eDnDAction_ObjectDataReceived_Event(object oObject)
        {

            handleDropOnDropControl(oObject,cbOnDropProcessJarFiles.Checked, cbDeleteTempFiles.Checked);
        }
        

        private void btSaveMappedControllers_Click(object sender, EventArgs e)
        {
            springMvcMappings.saveMappedControllers(tbFolderToSaveMappedMvcControllers.Text);
        }

        



        






       

        

        /*private void llMapFindingsToSourceCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            mapFindingsToSourceCode();
        }*/

        
                            
    }
}
