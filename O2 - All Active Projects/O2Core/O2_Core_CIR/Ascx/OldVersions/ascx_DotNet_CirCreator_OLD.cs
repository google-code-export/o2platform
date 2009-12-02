// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Windows.Forms;
using O2.Core.CIR;
using O2.Core.CIR.CirCreator;
using O2.Core.CIR.CirCreator;
using O2.Core.CIR.Xsd;
using O2.DotNetWrappers.Windows;

namespace O2.Core.CIR.Ascx
{
    public partial class ascx_DotNet_CirCreator_OLD : UserControl
    {
        private Assembly aLoadedAssesmbly;
        private String sDllToProcess = "";

        public ascx_DotNet_CirCreator_OLD()
        {
            InitializeComponent();
        }


        private void lbTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            listFunctions((Type) lbTypes.SelectedItem);
        }

        private void lbFunctions_SelectedIndexChanged(object sender, EventArgs e)
        {
    //        listFunction_CalledFunctions((methodResolver) lbFunctions.SelectedItem);
    //        listFunction_Variables((methodResolver) lbFunctions.SelectedItem);
    //        listFunction_OpCode((methodResolver) lbFunctions.SelectedItem);
        }

        private void lbVariables_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void lbFunctionsCalled_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

  /*      public void loadAssembly(String sAssemblyToLoad)
        {
            try
            {
                sDllToProcess = sAssemblyToLoad;

                aLoadedAssesmbly =
                    new CirCreatorEngineForDotnet.AssemblyLoader(sAssemblyToLoad, ad_Directory.getCurrentDirectory()).
                        loadAssembly();
                if (aLoadedAssesmbly != null)
                {
                    lbAssemblyLoaded.Text = sAssemblyToLoad;
                    listTypes(aLoadedAssesmbly);
                }
            }
            catch (Exception ex)
            {
                DI.log.error("in loadAssembly: {0}", ex.Message);
            }
        }*/

        public void listTypes(string file)
        {
        }

        public void listTypes(Assembly aAssembly)
        {
            lbTypes.Items.Clear();
            foreach (Type tType in aAssembly.GetTypes())
                lbTypes.Items.Add(tType);
        }

        public void listFunctions(Type tType)
        {
       /*     lbFunctions.Items.Clear();
            foreach (
                MethodInfo mMethodInfo in
                    tType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic |
                                     BindingFlags.Public | BindingFlags.DeclaredOnly))
                lbFunctions.Items.Add(new methodResolver(mMethodInfo));*/
        }

   /*     public void listFunction_CalledFunctions(methodResolver mrMethodResolver)
        {
            lbFunctionsCalled.Items.Clear();

            foreach (methodCall mcMethodCall in mrMethodResolver.lmcMethodsCalled)
            {
                lbFunctionsCalled.Items.Add(mcMethodCall.strName + " ... " + mcMethodCall.mbMethodBase);
            }
        }

        public void listFunction_Variables(methodResolver mrMethodResolver)
        {
            lbVariables.Items.Clear();
            if (mrMethodResolver.miMethodInfo == null)
                return;
            MethodBody mbMethodBody = mrMethodResolver.miMethodInfo.GetMethodBody();
            if (mbMethodBody != null)
                foreach (LocalVariableInfo ilviLocalVariablesInfo in mbMethodBody.LocalVariables)
                    lbVariables.Items.Add(ilviLocalVariablesInfo.LocalType);
        }*/


       /* public void listFunction_OpCode(methodResolver mrMethodResolver)
        {
            tbOpCodes.Items.Clear();

            foreach (OperandType otOperandType in mrMethodResolver.lopOperandTypes)
                if (otOperandType != OperandType.InlineNone)
                    tbOpCodes.Items.Add(otOperandType);
            tbOpCodes.Items.Add("-----");
            foreach (OpCode ocOpCode in mrMethodResolver.locOpCodes)
                if (ocOpCode.ToString() != "nop")
                    tbOpCodes.Items.Add(ocOpCode);
        }
        */

        private void ascx_DotNet_CirCreator_Load(object sender, EventArgs e)
        {
            if (false == DesignMode)
            {
                ad_Directory.eDirectoryEvent_DoubleClick += ad_Directory_eDirectoryEvent_DoubleClick;
                ad_Directory.simpleMode();
                ascx_DropObject1.setText("Drag File or Directory To Load Here");

                ad_DirectoryToSaveCreatedFiles.simpleMode_withAddressBar();
                ad_DirectoryToSaveCreatedFiles.openDirectory(DI.config.O2TempDir);
            }
        }

        private void ad_Directory_eDirectoryEvent_DoubleClick(string sValue)
        {
            if (false == Directory.Exists(sValue))
            {
                if (cbAddAllAssembliesFromSelectedDirectory.Checked)
                    addFilesFromDirectoryToTargetsList(Path.GetDirectoryName(sValue));
                else
                    addFileToTargetsList(sValue);
            }
        }

        private void btCreateCir_Click(object sender, EventArgs e)
        {
     //       String sCirDumpFile = new CirCreatorEngineForDotnet().createCirForAssembly(sDllToProcess, tbCirDumpCreate_filter.Text,
     //                                                                    getListOfPathsToSearchForReferencedAssemblies());
/*            if (File.Exists(sCirDumpFile))
            {
                if (cbLoadCreatedCirFileIn_LoadInExtrenalF1Viewer.Checked)
                {
                    int iRemotePort;
                    if (Int32.TryParse(tbTcpPortRemoteF1.Text, out iRemotePort))
                    {
                        DI.log.debug("Opening created CirDump file in External F1 Viewer (on port:{0})",
                                        tbTcpPortRemoteF1.Text);
                        remoteAccess.sendMessage(Int32.Parse(tbTcpPortRemoteF1.Text),
                                                 "$ascx_CirViewer_F1CirData.loadF1CirDataFile_ThreadSafe " +
                                                 sCirDumpFile);
                    }
                }
            }*/
        }

        public List<String> getListOfPathsToSearchForReferencedAssemblies()
        {
            var lsPathsToSearchForReferencedAssemblies = new List<string>();
            foreach (
                String sLine in
                    tbPathsToSearchForReferencedAssemblies.Text.Split(new[] {Environment.NewLine},
                                                                      StringSplitOptions.None))
            {
                lsPathsToSearchForReferencedAssemblies.Add(sLine);
            }
            return lsPathsToSearchForReferencedAssemblies;
            //ad_Directory.getCurrentDirectory()
        }

        private void ascx_DropObject1_eDnDAction_ObjectDataReceived_Event(object oObject)
        {
            addToTargetsList(oObject.ToString());
        }

        public void addToTargetsList(String sItemToAdd)
        {
            if (File.Exists(sItemToAdd))
                addFileToTargetsList(sItemToAdd);
            else if (Directory.Exists(sItemToAdd))
                addFilesFromDirectoryToTargetsList(sItemToAdd);
        }

        public void addFileToTargetsList(string sFileToAdd)
        {
            try
            {
                Assembly aAssembly = null;
                    //new CirCreatorEngineForDotnet.AssemblyLoader(sFileToAdd, getListOfPathsToSearchForReferencedAssemblies()).
                    //    loadAssembly();
                if (aAssembly != null)
                {
                    if (false == lbTargetDotNetAssesmblies.Items.Contains(aAssembly))
                    {
                        lbTargetDotNetAssesmblies.Items.Add(aAssembly);
                        DI.log.debug(
                            "Sucessfully loaded Assesmbly and added it to the list of Files to process:{0}", sFileToAdd);
                    }
                    else
                        DI.log.debug("Assembly was already on the list of files to process:{0}", sFileToAdd);
                }
                else
                    DI.log.error("Could not load Assembly: {0}", sFileToAdd);
            }
            catch (Exception ex)
            {
                DI.log.error("in loadAssembly: {0}", ex.Message);
            }
        }

        public void addFilesFromDirectoryToTargetsList(string sDirectoryToProcess)
        {
            var lsFilesToProcess = new List<string>();
            bool bSearchRecursively = cbAddAllAssembliesFromSelectedDirectory_RecursiveSearch.Checked;
            Files.getListOfAllFilesFromDirectory(lsFilesToProcess, sDirectoryToProcess, bSearchRecursively, "*.dll",
                                                 false);
            Files.getListOfAllFilesFromDirectory(lsFilesToProcess, sDirectoryToProcess, bSearchRecursively, "*.exe",
                                                 false);
            foreach (String sFileToProcess in lsFilesToProcess)
            {
                addFileToTargetsList(sFileToProcess);
                Application.DoEvents();
            }
            DI.log.debug("Completed Adding files from Directory to Target list");
        }

        private void lbTargetDotNetAssesmblies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbTargetDotNetAssesmblies.SelectedItem != null)
                listTypes((Assembly) lbTargetDotNetAssesmblies.SelectedItem);
        }

        private void btClearTargetsList_Click(object sender, EventArgs e)
        {
            lbTargetDotNetAssesmblies.Items.Clear();
        }

        private void lbTargetDotNetAssesmblies_DoubleClick(object sender, EventArgs e)
        {
        }

        private void cbAddAllAssembliesFromSelectedDirectory_CheckedChanged(object sender, EventArgs e)
        {
            cbAddAllAssembliesFromSelectedDirectory_RecursiveSearch.Enabled =
                cbAddAllAssembliesFromSelectedDirectory.Checked;
        }

        private void btCreateCirDumpFile_Click(object sender, EventArgs e)
        {
            //ad_DirectoryToSaveCreatedFiles.simpleMode_withAddressBar();
            foreach (Assembly aAssembly in lbTargetDotNetAssesmblies.Items)
            {
                {
                    String sTargetFile = Path.Combine(ad_DirectoryToSaveCreatedFiles.getCurrentDirectory(),
                                                      Path.GetFileName(aAssembly.Location) + ".xml");
                    if (File.Exists(sTargetFile))
                        DI.log.info("Skipping conversion, target file already exists: {0}", sTargetFile);
                    else
                    {
           /*             CommonIRDump cirCommonCirDump = new CirCreatorEngineForDotnet().createCirForAssembly(aAssembly);
                        if (cirCommonCirDump != null)
                        {
                            new CirCreatorEngineForDotnet().saveCirDumpObject(cirCommonCirDump, sTargetFile);
                            ad_DirectoryToSaveCreatedFiles.refreshDirectoryView();
                        }
            */
                    }
                }
                Application.DoEvents();
                // String sCirDumpFile2 = CirCreatorEngineForDotnet.createCirForAssembly(sDllToProcess, tbCirDumpCreate_filter.Text, ad_Directory.getCurrentDirectory());
            }
            DI.log.debug("Create CirDumpFile process completed");
        }

        private void btAddSelectedDirectory_Click(object sender, EventArgs e)
        {
            addFilesFromDirectoryToTargetsList(ad_Directory.getCurrentDirectory());
        }
    }
}
