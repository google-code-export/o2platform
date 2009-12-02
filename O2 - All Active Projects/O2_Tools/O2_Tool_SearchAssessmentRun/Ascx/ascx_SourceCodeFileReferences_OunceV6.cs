using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using O2.DotNetWrappers.O2Misc;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.Xsd;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Kernel.CodeUtils;
using O2.Legacy.OunceV6.SavedAssessmentFile.classes;

namespace O2.Tool.SearchAssessmentRun.Ascx
{
    public partial class ascx_SourceCodeFileReferences : UserControl
    {
        public Callbacks.dMethod dCallbackToRefreshFilesMappings;
        private O2AssessmentData_OunceV6 oadO2AssessmentData;
        private SourceCodeMappingsUtils.resolvedFileMapping rfmResolvedFileMapping;

        public ascx_SourceCodeFileReferences()
        {
            InitializeComponent();
        }
        

        public void loadAssessmentFile(O2AssessmentData_OunceV6 _oadO2AssessmentData)
        {
            oadO2AssessmentData = _oadO2AssessmentData;
            lbLoadedAssessmentFile.Text = oadO2AssessmentData.ToString();
            loadFileMappingsIntoListBoxes();
        }

      /*  public void loadAssessmentFile(String sAssessmentFileToLoad)
        {
            //lbLoadedAssessmentFile.Text = sAssessmentFileToLoad;
            //oadO2AssessmentData = new O2AssessmentLoad_OunceV6();
            .loadFile(sAssessmentFileToLoad);

                //Analysis.loadAssessmentFile(sAssessmentFileToLoad, false /*bVerbose* /, true
                ///*bResolveReferences* /);
            loadFileMappingsIntoListBoxes();
        }*/

        private void loadFileMappingsIntoListBoxes()
        {
            if (oadO2AssessmentData != null)
            {
                List<String> lsUniqueFilesInAssessment = SourceCodeFiles.getListOfUniqueFiles(oadO2AssessmentData);

                lbFilesFoundOnThisComputer.Items.Clear();
                lbFilesNOTFoundOnThisComputer.Items.Clear();

                foreach (String sFile in lsUniqueFilesInAssessment)
                    if (File.Exists(sFile))
                        lbFilesFoundOnThisComputer.Items.Add(sFile);
                    else
                        lbFilesNOTFoundOnThisComputer.Items.Add(sFile);
                if (lbFilesFoundOnThisComputer.Items.Count > 0)
                    lbFilesFoundOnThisComputer.SelectedIndex = 0;
                btAllDoneCloseModule.Visible = (lbFilesNOTFoundOnThisComputer.Items.Count == 0 &&
                                                lbFilesFoundOnThisComputer.Items.Count > 0);
            }
        }


        private void ascx_SourceCodeFileReferences_Load(object sender, EventArgs e)
        {
            ascx_DropObject1.setText("Drop Saved Assessment File (*.ozasmt or *.xml )here");
            SourceCodeMappingsUtils.loadSourceCodeMappings(dataGridView1);
            //  btTest_Click(null, null);
        }

        private void ascx_DropObject1_eDnDAction_ObjectDataReceived_Event(object oObject)
        {
          //  loadAssessmentFile(oObject.ToString());
        }

        private void btFileInLocalDisk_Click(object sender, EventArgs e)
        {
            String sFileToMap = lbSelectedFile.Text;
            rfmResolvedFileMapping = new SourceCodeMappingsUtils.resolvedFileMapping(sFileToMap);
            if (rfmResolvedFileMapping.resolveFileMapping())
            {
                lbMappedFile.Text = rfmResolvedFileMapping.sMappedFile;
                lbFix_PathToFind.Text = rfmResolvedFileMapping.sFix_PathToFind;
                lbFix_PathToReplace.Text = rfmResolvedFileMapping.sFix_PathToReplace;
                O2Forms.addRowToDataGridViewThreadSafe(dataGridView1,
                                                       new object[]
                                                           {
                                                               rfmResolvedFileMapping.sFix_PathToFind,
                                                               rfmResolvedFileMapping.sFix_PathToReplace
                                                           });
                btExistingMappingsSaveChanges.Visible = true;
            }
        }

        private void lbFilesNOTFoundOnThisComputer_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbSelectedFile.Text = lbFilesNOTFoundOnThisComputer.SelectedItem.ToString();
        }

        private void btFixAllFilesWithMapping_Click(object sender, EventArgs e)
        {
            try
            {
                SourceCodeFiles.fixAllFileReferencesOnAssessmentDataObject(oadO2AssessmentData, rfmResolvedFileMapping);
                loadAssessmentFile(oadO2AssessmentData);
            }
            catch (Exception ex)
            {
                DI.log.error("in btFixAllFilesWithMapping_Click:{0}" , ex.Message);
            }
            
        }

        private void btSaveAssessmentFileWithFixes_Click(object sender, EventArgs e)
        {
        }

        private void btAllDoneCloseModule_Click(object sender, EventArgs e)
        {
            if (Parent.GetType().Name == "Form")
            {
                if (dCallbackToRefreshFilesMappings != null)
                    dCallbackToRefreshFilesMappings.Invoke();
                ((Form) (Parent)).Close();
            }
        }

        private void btExistingMappingsSaveChanges_Click(object sender, EventArgs e)
        {
            SourceCodeMappingsUtils.saveSourceCodeMappings(SourceCodeMappingsUtils.getSourceCodeMappingsFromDataGridView(dataGridView1));
        }

       
    }
}