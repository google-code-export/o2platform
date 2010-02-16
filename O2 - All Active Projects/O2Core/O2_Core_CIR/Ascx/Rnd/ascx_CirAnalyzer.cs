// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using O2.Core.CIR;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirUtils;
using O2.Core.CIR.Xsd;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Interfaces.CIR;
using O2.Kernel.CodeUtils;

namespace O2.Core.CIR.Ascx.Rnd
{
    public partial class ascx_CirAnalyzer : UserControl
    {
        public bool bAutoLoadDefaultCirDumpsDir = true;
        private Double dMBytesProcessed;
        private DateTime dtStartTime = DateTime.Now;
        public CirData fcdCirData = new CirData();
        public int iMaxItemsToShowOnMainTreeView = 2000;
        private int iNumberOfTestsToExecute;
        private List<String> lFilesThatDontLoad = new List<string>();
        private List<String> lFilesThatLoadOk = new List<string>();
        public String sCurrentBaseDir;
        //  public recursivelyPopulateTreeViewWith_XmlSerializedObjectData rptvTreeViewPopulateEngine = new recursivelyPopulateTreeViewWith_XmlSerializedObjectData();
        //  public recursivelyPopulateTreeViewWith_XmlSerializedObjectData rptvTreeViewPopulateEngine_SubView = new recursivelyPopulateTreeViewWith_XmlSerializedObjectData();


        public ascx_CirAnalyzer()
        {
            InitializeComponent();
        }

        public ascx_CirAnalyzer(bool bAutoLoadDefaultCirDumpsDir)
        {
            this.bAutoLoadDefaultCirDumpsDir = bAutoLoadDefaultCirDumpsDir;
        }

        private void ascx_ClrAnalyzer_Load(object sender, EventArgs e)
        {
            if (false == DesignMode)
            {
                /*   if (bAutoLoadDefaultCirDumpsDir)
                    addFolderToBaseDirectoryComboBox(Config.getDefaultCirDumpsDir());*/
                cbTreeViewRecursiveLevel.Text = "2"; // set the default recursive level to 2         
                tbCirAnalyzer_PathToSavedCirDataFile.Text = Path.Combine(DI.config.O2TempDir,
                                                                         Files.getTempFileName() + "_CirData.CirData");
            }
        }

        public void setMaxItemsToShowOnMainTreeView(int iNewValue)
        {
            iMaxItemsToShowOnMainTreeView = iNewValue;
            DI.log.debug("iMaxItemsToShowOnMainTreeView set to {0}", iNewValue);
        }

/*        private void addFolderToBaseDirectoryComboBox(String sFolder)
        {
            sCurrentBaseDir = sFolder;
            cbBaseDirectory.Items.Add(sCurrentBaseDir);
            cbBaseDirectory.Text = sCurrentBaseDir;
        }
        */

        private void cbBaseDirectory_SelectedIndexChanged(object sender, EventArgs e)
        {
            sCurrentBaseDir = cbBaseDirectory.Text;
            O2Forms.loadListBoxWithFilesFromDir(lbFilesInSelectedDirectory, sCurrentBaseDir, "*.xml");
            O2Forms.loadListBoxWithFilesFromDir(lbO2CirDataFilesInSelectedDir, sCurrentBaseDir, "*.CirData");
            if (lbFilesInSelectedDirectory.Items.Count > 0)
                lbFilesInSelectedDirectory.SelectedIndex = 0;
        }

/*        private void btProcessFile_Click(object sender, EventArgs e)
        {
            processSelectedFileInListBox();
        }
        */

        private void processSelectedFileInListBox()
        {
            try
            {
                lbFilesInSelectedDirectory.Enabled = false;

                // clear dictionary objects
                //     ounceAnalysis_CallFlow.clearDictionariesObjects();            

                ListBox.SelectedObjectCollection lbsocSelectedItems = lbFilesInSelectedDirectory.SelectedItems;
                foreach (String sSelectedItem in lbsocSelectedItems)
                {
                    String sPathToFileToProcess = Path.Combine(sCurrentBaseDir, sSelectedItem);
                    processFile(sPathToFileToProcess, tvAllClassesAndMethods);
                }
                lbFilesInSelectedDirectory.Enabled = true;
            }
            catch (Exception ex)
            {
                DI.log.error("In processSelectedFileInListBox: {0}", ex.Message);
            }
        }

        private void processFiles_(List<String> lsFilesToProcess, TreeView tvTargetTreeView)
        {
            O2Timer tO2Timer = new O2Timer("Processing files").start();
            fcdCirData.init();
            CirLoad.loadCirDumpXmlFiles_andPopulateDictionariesWithXrefs(lsFilesToProcess, fcdCirData,
                                                                         false /*bVerbose*/);
            showO2CirDataInTreeViewAndListBox(fcdCirData, tvTargetTreeView, lbO2CirData_Functions);
            tO2Timer.stop();
        }

        public void showO2CirDataInTreeViewAndListBox(CirData fcdCirData, TreeView tvTargetTreeView,
                                                      ListBox lbTargetListBox)
        {
            var lsFunctionsAdded = new List<string>();
            showO2CirDataInTreeView(fcdCirData, tvTargetTreeView, lsFunctionsAdded);
            showO2CirDataInListBox(fcdCirData, lbTargetListBox, lsFunctionsAdded);
        }

        public void showO2CirDataInTreeView(CirData fcdCirData)
        {
            showO2CirDataInTreeView(fcdCirData, tvAllClassesAndMethods, new List<string>());
        }

        public void showO2CirDataInListBox(CirData fcdCirData, ListBox lbTargetListBox, List<String> lsFunctionsAdded)
        {
            int iItemsProcessed = 0;
            lbTargetListBox.Items.Clear();
            //ascx_RulesCreator1.clearTargetslist();            
            foreach (String sSignature in lsFunctionsAdded)
            {
                if (fcdCirData.dFunctions_bySignature.ContainsKey(sSignature))
                {
                    ICirFunction cfCirFunction = fcdCirData.dFunctions_bySignature[sSignature];
                    cfCirFunction.OnlyShowFunctionNameInToString = !cbViewByFunction_FullSignature.Checked;
                    if (false == lbTargetListBox.Items.Contains(cfCirFunction))
                        lbTargetListBox.Items.Add(cfCirFunction);
                }
                else if (false == lbTargetListBox.Items.Contains(sSignature))
                    lbTargetListBox.Items.Add("*" + sSignature);
                if (iItemsProcessed++ < iMaxItemsToShowOnMainTreeView)
                {
                    DI.log.error("MaxItemsToShow reached {0}, aborting", iMaxItemsToShowOnMainTreeView);
                    break;
                }
            }
            lbTargetListBox.Sorted = true;
            /*
            if (fcdCirData.dClasses_bySignature != null)
                foreach (CirClass ccCirClass in fcdCirData.dClasses_bySignature.Values)
                    if (ccCirClass.bClassHasMethodsWithControlFlowGraphs && ccCirClass.dFunctions.Count > 0)       //only Classes with methods with ControlFlowGraphs
                        foreach (CirFunction cfCirFunction in ccCirClass.dFunctions.Values)
                            if (cfCirFunction.HasControlFlowGraph)
                            {
                                cfCirFunction.OnlyShowFunctionNameInToString = !cbViewByFunction_FullSignature.Checked;
                                lbTargetListBox.Items.Add(cfCirFunction);
                            }
                        */
        }

        public void clearViewers()
        {
            lbCirAnalyzer_Files.Items.Clear();
            tvAllClassesAndMethods.Nodes.Clear();
        }

        public void showO2CirDataInTreeView(CirData fcdCirData, TreeView tvTargetTreeView, List<String> lsFunctionsAdded)
        {
            clearViewers();
            // make it not visible for performance reasons:
            tvTargetTreeView.Visible = false;
            // add list of files process to ListBox        
            lbCirAnalyzer_Files.Visible = false;

            foreach (String sFile in fcdCirData.lFiles)
                lbCirAnalyzer_Files.Items.Add(sFile);
            lbCirAnalyzer_Files.Visible = true;
            // add data to ClassesAndMethod Tree View

            tvTargetTreeView.Sort();
            if (fcdCirData.dClasses_bySignature != null)
                foreach (CirClass ccCirClass in fcdCirData.dClasses_bySignature.Values)
                {
                    if (ccCirClass.bClassHasMethodsWithControlFlowGraphs && ccCirClass.dFunctions.Count > 0)
                        //only Classes with methods with ControlFlowGraphs
                    {
                        bool bFunctionAdded = false;
                        TreeNode tnClass = O2Forms.newTreeNode(ccCirClass.Signature, "", 0, null);
                        // add SuperClasses
                        foreach (String sSuperClass in ccCirClass.dSuperClasses.Keys)
                            tnClass.Nodes.Add(O2Forms.newTreeNode(CirDataUtils.getSymbol(fcdCirData,sSuperClass), "", 5, null));
                        foreach (String sIsSuperClassedBy in ccCirClass.dIsSuperClassedBy.Keys)
                            tnClass.Nodes.Add(
                                O2Forms.newTreeNode("IsSuperClassedBy:" + CirDataUtils.getSymbol(fcdCirData,sIsSuperClassedBy), "", 5,
                                                    null));

                        // add fields
                        if (rbShowFieldsAndVariables.Checked)
                        {
                            foreach (FieldClass fsFieldClass in ccCirClass.dField_Class.Values)
                                tnClass.Nodes.Add(
                                    O2Forms.newTreeNode(
                                        String.Format("fc: {0} {1} - {2}", fsFieldClass.Signature, fsFieldClass.Name,
                                                      fsFieldClass.Signature), "", 4, null));
                            foreach (FieldMember fsFieldMember in ccCirClass.dField_Member.Values)
                                tnClass.Nodes.Add(
                                    O2Forms.newTreeNode(
                                        String.Format("fm: {0} {1}", fsFieldMember.PrintableType, fsFieldMember.Name),
                                        "", 4, null));
                        }
                        // add methods in code, its variables, who it calls
                        foreach (CirFunction cfCirFunction in ccCirClass.dFunctions.Values)
                        {
                            if (processFunctionAndAddItToNode(tnClass.Nodes, cfCirFunction, lsFunctionsAdded))
                                bFunctionAdded = true;
                        }

                        if (bFunctionAdded && tnClass.Nodes.Count > 0)
                            tvTargetTreeView.Nodes.Add(tnClass);
                    }
                    /*  // for C++ files  (handles with the NonClassFunctions)
                    foreach (CirFunction cfCirFunction in fcdCirData.dFunctions.Values)
                    { 
                        processFunctionAndAddItToNode(tvTargetTreeView.Nodes, cfCirFunction);
                    }*/
                    Application.DoEvents(); // refresh GUI
                }

            //tvAllClassesAndMethods.ExpandAll();
            //     if (tvAllClassesAndMethods.Nodes.Count > 0 )
            //         tvAllClassesAndMethods.Nodes[0].Expand();
            //     if (cbCirAnalyzer_TextFilter_MakesCallsTo.Checked || cbCirAnalyzer_TextFilter_Functions.Checked || cbCirAnalyzer_TextFilter_RemoveMakesCallsTo.Checked)
            //         tvTargetTreeView.ExpandAll();
            tvTargetTreeView.Visible = true;
        }

        private bool processFunctionAndAddItToNode(TreeNodeCollection tncTargetNode, CirFunction cfCirFunction,
                                                   List<String> lsFunctionsAdded)
        {
            bool bMakesCallTo = false;
            TreeNode tnMethod = O2Forms.newTreeNode(cfCirFunction.FunctionSignature, "", 2, cfCirFunction);

            // add arguments and return value
            if (cbShowArgsAndReturntype.Checked)
            {
                if (cfCirFunction.ReturnType != "")
                    tnMethod.Nodes.Add(
                        O2Forms.newTreeNode(
                            String.Format("retv: {0}", CirDataUtils.getSymbol(fcdCirData,cfCirFunction.ReturnType)), "", 3, null));
                foreach (ICirFunctionParameter functionParameter in cfCirFunction.FunctionParameters)
                    tnMethod.Nodes.Add(O2Forms.newTreeNode(String.Format("arg: {0}", CirDataUtils.getSymbol(fcdCirData, functionParameter.ParameterType)),
                                                           "", 3, null));
            }
            // add variables
            if (rbShowFieldsAndVariables.Checked)
            {
                foreach (String sSymbolRef in cfCirFunction.UsedTypes)
                    tnMethod.Nodes.Add(O2Forms.newTreeNode(String.Format("v: {0}", CirDataUtils.getSymbol(fcdCirData,sSymbolRef)), "",
                                                           4, null));
            }
            // add functions called by this method & 
            foreach (ICirFunction cirFunctionCalled in cfCirFunction.FunctionsCalledUniqueList)
            {
                var sSignature = cirFunctionCalled.FunctionSignature;
                bool bAddSignature = false;
                String sSignatureWithParams = sSignature.Substring(0, sSignature.IndexOf('('));
                if ((false == cbCirAnalyzer_TextFilter_MakesCallsTo.Checked &&
                     false == cbCirAnalyzer_TextFilter_RemoveMakesCallsTo.Checked))
                    bAddSignature = true;

                bool bMatchOnRemovesMakesCallTo = (cbCirAnalyzer_TextFilter_RemoveMakesCallsTo.Checked &&
                                                   sSignatureWithParams.IndexOf(
                                                       (string) tbCirAnalyszer_TextSearchFilter_RemoveMakesCallsTo.Text) == -1);
                bool bMatchOnMakesCallTo = (cbCirAnalyzer_TextFilter_MakesCallsTo.Checked &&
                                            sSignatureWithParams.IndexOf(
                                                (string) tbCirAnalyszer_TextSearchFilter_MakesCallsTo.Text) > -1);

                if ((bMatchOnRemovesMakesCallTo && false == cbCirAnalyzer_TextFilter_MakesCallsTo.Checked) ||
                    (bMatchOnMakesCallTo && false == cbCirAnalyzer_TextFilter_RemoveMakesCallsTo.Checked) ||
                    (bMatchOnRemovesMakesCallTo && bMatchOnMakesCallTo))
                    bAddSignature = true;
                if (bAddSignature)
                {
                    lsFunctionsAdded.Add(sSignature);
                    bMakesCallTo = true;
                    tnMethod.Nodes.Add(O2Forms.newTreeNode(String.Format("fCalled: {0}", sSignature), "", 10, null));
                }
            }
            // add functions that call this method
            foreach (ICirFunction cirIsCalledFunction in cfCirFunction.FunctionIsCalledBy)
                tnMethod.Nodes.Add(O2Forms.newTreeNode(String.Format("isCalledBy: {0}", cirIsCalledFunction.FunctionSignature), "", 11, null));
            if (cbCirAnalyzer_TextFilter_MakesCallsTo.Checked && false == bMakesCallTo)
                return false;
            tncTargetNode.Add(tnMethod);
            return true;
        }

        public void processFile(String sPathToFileToProcess)
        {
            processFile(sPathToFileToProcess, tvAllClassesAndMethods);
        }

        public void processFile(String sPathToFileToProcess, TreeView tvTargetTreeView)
        {
            O2Timer tO2TimerLoadingCallTracesInTreeView = new O2Timer("Loaded Cir Dump data ").start();

            CommonIRDump cidCommonIrDump =
                CirLoad.loadCirDumpXmlFile_justReturnCommonIRDump(sPathToFileToProcess,
                                                                  true);
            tvTargetTreeView.Tag = cidCommonIrDump;

            if (rb_ClearO2CirDataObjectOnLoad.Checked)
                fcdCirData.init();
            // calculate Xref objects                             
            fcdCirData.bVerbose = true;
            CirDataUtils.populateDictionariesWithXrefs(cidCommonIrDump, fcdCirData);
            showO2CirDataInTreeViewAndListBox(fcdCirData, tvTargetTreeView, lbO2CirData_Functions);

            tO2TimerLoadingCallTracesInTreeView.stop();
        }

        private void lbFilesInSelectedDirectory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAutoLoadOnSelection.Checked)
                processSelectedFileInListBox();
            lbFilesInSelectedDirectory.Select();
        }


        public bool filterNodesOnString(TreeNodeCollection tncNodes, String sFilter, bool bCaseSensitive,
                                        ListBox lbFilterResults)
        {
            bool bHadAMatchInNodes = false;
            if (tncNodes != null)
                foreach (TreeNode tnNode in tncNodes)
                {
                    //Application.DoEvents();
                    tnNode.ForeColor = Color.Black;
                    if (sFilter != "")
                    {
                        // if there was a match set the bHadAMatchInNodes and the tnNode color
                        if (tnNode.Text.IndexOf(sFilter) > -1 ||
                            (bCaseSensitive == false && tnNode.Text.ToLower().IndexOf(sFilter.ToLower()) > -1))
                        {
                            lbFilterResults.Items.Add(tnNode);
                            bHadAMatchInNodes = true;
                        }
                        else
                            tnNode.ForeColor = Color.DarkGray;
                    }
                    // now let's recursively process the subNodes                    

                    if (filterNodesOnString(tnNode.Nodes, sFilter, bCaseSensitive, lbFilterResults))
                        // if there was a match in this Node Nodes
                    {
                        tnNode.ForeColor = Color.Black; // make it black                    
                        bHadAMatchInNodes = true; // and mark the current node for expansion
                        tnNode.Expand();
                    }
                    else
                        tnNode.Collapse();
                }
            return bHadAMatchInNodes;
        }

        private void btSelectBaseDirectory_Click(object sender, EventArgs e)
        {
            String sNewPath = O2Forms.askUserForDirectory(cbBaseDirectory.Text);
            if (sNewPath != "")
            {
                cbBaseDirectory.Items.Add(sNewPath);
                cbBaseDirectory.Text = sNewPath;
            }
        }


        /*           
 private static void addMethodToTreeNode_Static(TreeNode tnTargetTreeNode, String sName, String sUniqueId, Int32 iImageIndex, 
     xsd.CirDump.CommonIRDumpCommonIRClassMethodsClassStaticFunctionBasicBlock[] acidStaticFunction_ControlFlowGraphs, 
     xsd.CirDump.CommonIRDumpCommonIRClassMethodsClassStaticFunctionVariable[] acidStaticFunction_Variables,
     Object oMethodObject)
 {                         
     
     TreeNode tnMethodNode = O2Forms.newTreeNode(sName, sUniqueId, iImageIndex, oMethodObject);
     if (acidStaticFunction_ControlFlowGraphs != null)
         foreach (xsd.CirDump.CommonIRDumpCommonIRClassMethodsClassStaticFunctionBasicBlock cidStaticFunction_ControlFlowGraph in acidStaticFunction_ControlFlowGraphs)
         {                    
             TreeNode tnControlFlowGraph_Static = O2Forms.newTreeNode("ControlFlowGraph", "ControlFlowGraph", 3, cidStaticFunction_ControlFlowGraph);
             if (cidStaticFunction_ControlFlowGraph.DominanceFrontier != null)
                 tnControlFlowGraph_Static.Nodes.Add("DominanceFrontier");

             processControlFlowGraphItems(tnControlFlowGraph_Static, cidStaticFunction_ControlFlowGraph.Items, cidStaticFunction_ControlFlowGraph.GetType().Name);
                    
             if (cidStaticFunction_ControlFlowGraph.Predecessors != null)
                 tnControlFlowGraph_Static.Nodes.Add("Predecessors");                    
             if (cidStaticFunction_ControlFlowGraph.Successors != null)
                 tnControlFlowGraph_Static.Nodes.Add(O2Forms.newTreeNode("Successors:" + cidStaticFunction_ControlFlowGraph.Successors, "", 4, ""));

             tnControlFlowGraph_Static.Nodes.Add(O2Forms.newTreeNode("UniqueID: " + cidStaticFunction_ControlFlowGraph.UniqueID, "", 4, ""));

             tnMethodNode.Nodes.Add(tnControlFlowGraph_Static);
         }

     if (acidStaticFunction_Variables != null)
         foreach (xsd.CirDump.CommonIRDumpCommonIRClassMethodsClassStaticFunctionVariable cidStaticFunction_Variable in acidStaticFunction_Variables)                
             tnMethodNode.Nodes.Add(O2Forms.newTreeNode(cidStaticFunction_Variable.Name + "          -           " + cidStaticFunction_Variable.UniqueID, "ControlFlowGraph", 4, cidStaticFunction_Variable));
                
     if (acidStaticFunction_ControlFlowGraphs != null || acidStaticFunction_Variables != null)
         tnTargetTreeNode.Nodes.Add(tnMethodNode);
             
 }
 */

        /*    private static void addMethodToTreeNode_Member(TreeNode tnTargetTreeNode, String sName, String sUniqueId, Int32 iImageIndex,
                xsd.CirDump.CommonIRDumpCommonIRClassMethodsClassMemberFunctionControlFlowGraphBasicBlock[] acidMemberFunction_ControlFlowGraphs,
                xsd.CirDump.CommonIRDumpCommonIRClassMethodsClassMemberFunctionVariable cidMemberFunction_Variable,
                Object oMethodObject)
            {

                TreeNode tnMethodNode = O2Forms.newTreeNode(sName, sUniqueId, iImageIndex, oMethodObject);
                if (acidMemberFunction_ControlFlowGraphs != null)
                    foreach (xsd.CirDump.CommonIRDumpCommonIRClassMethodsClassMemberFunctionControlFlowGraphBasicBlock cidMemberFunction_ControlFlowGraph in acidMemberFunction_ControlFlowGraphs)
                    {
                        TreeNode tnControlFlowGraph_Member = O2Forms.newTreeNode("ControlFlowGraph", "ControlFlowGraph", 3, cidMemberFunction_ControlFlowGraph);
                        if (cidMemberFunction_ControlFlowGraph.DominanceFrontier != null)
                            tnControlFlowGraph_Member.Nodes.Add("DominanceFrontier");
                        if (cidMemberFunction_ControlFlowGraph.Edge != null)
                            tnControlFlowGraph_Member.Nodes.Add("Edge");
                        if (cidMemberFunction_ControlFlowGraph.EvalExprStmt != null)
                            tnControlFlowGraph_Member.Nodes.Add("EvalExprStmt");

                        if (cidMemberFunction_ControlFlowGraph.GotoStmt != null)
                            tnControlFlowGraph_Member.Nodes.Add("GotoStmt");
                        if (cidMemberFunction_ControlFlowGraph.Predecessors != null)
                            tnControlFlowGraph_Member.Nodes.Add("Predecessors");
                        if (cidMemberFunction_ControlFlowGraph.ReturnStmt != null)
                            tnControlFlowGraph_Member.Nodes.Add("ReturnStmt");
                        if (cidMemberFunction_ControlFlowGraph.Successors != null)
                            tnControlFlowGraph_Member.Nodes.Add(O2Forms.newTreeNode("Successors:" + cidMemberFunction_ControlFlowGraph.Successors,"",4,""));
                    
                        tnControlFlowGraph_Member.Nodes.Add(O2Forms.newTreeNode("UniqueID: " + cidMemberFunction_ControlFlowGraph.UniqueID,"",4,""));

                        tnMethodNode.Nodes.Add(tnControlFlowGraph_Member);
                    }


                if (cidMemberFunction_Variable != null)            
                    //foreach (xsd.CirDump.CommonIRDumpCommonIRClassMethodsClassMemberFunctionVariable cidMemberFunction_Variable in acidMemberFunction_Variables)
                    tnMethodNode.Nodes.Add(O2Forms.newTreeNode(cidMemberFunction_Variable.Name + "          -           " + cidMemberFunction_Variable.UniqueID, "ControlFlowGraph", 4, cidMemberFunction_Variable));            

                if (acidMemberFunction_ControlFlowGraphs != null || cidMemberFunction_Variable != null)
                    tnTargetTreeNode.Nodes.Add(tnMethodNode);
            }
         * */

        public static void processControlFlowGraphItems(TreeNode tnTargetTreeNode, object[] oControlFlowGraphItems,
                                                        String sParentTypeName)
        {
            /*
            if (null != oControlFlowGraphItems)
            {
                TreeNode tnItems = O2Forms.newTreeNode("Items","",5,oControlFlowGraphItems);
                foreach (Object oItem in oControlFlowGraphItems)
                {
                    String sItemType = (oItem.GetType().Name).Replace(sParentTypeName, "");
                    switch (sItemType)
                    { 
                        case "GotoStmt":
                            TreeNode tnGotoStmt = O2Forms.newTreeNode("GotoStmt","",6,oItem);
                            xsd.CirDump.CommonIRDumpCommonIRClassMethodsClassStaticFunctionBasicBlockGotoStmt fbcGotoStmt = (xsd.CirDump.CommonIRDumpCommonIRClassMethodsClassStaticFunctionBasicBlockGotoStmt)oItem;
                            tnGotoStmt.Nodes.Add(O2Forms.newTreeNode("SuccessorBasicBlock: " + fbcGotoStmt.SuccessorBasicBlock.ToString(),"",4,null));
                            tnGotoStmt.Nodes.Add(O2Forms.newTreeNode("SuccessorEdgeIndex: " + fbcGotoStmt.SuccessorEdgeIndex.ToString(), "", 4, null));
                            tnItems.Nodes.Add(tnGotoStmt);
                            break;
                        case "ReturnStmt":
                            TreeNode tnReturnStmt = O2Forms.newTreeNode("ReturnStmt", "", 6, oItem);
                            xsd.CirDump.CommonIRDumpCommonIRClassMethodsClassStaticFunctionBasicBlockReturnStmt fbcReturnStmt = (xsd.CirDump.CommonIRDumpCommonIRClassMethodsClassStaticFunctionBasicBlockReturnStmt)oItem;
                            tnReturnStmt.Nodes.Add(O2Forms.newTreeNode("ReturnBasicBlock: " + fbcReturnStmt.ReturnBasicBlock.ToString(), "", 4, null));
                            tnReturnStmt.Nodes.Add(O2Forms.newTreeNode("ReturnEdgeIndex: " + fbcReturnStmt.ReturnEdgeIndex.ToString(), "", 4, null));
                            if (fbcReturnStmt.VarValue != null && fbcReturnStmt.VarValue.SymbolRef !=null)
                            {
                                tnReturnStmt.Nodes.Add(O2Forms.newTreeNode("VarValue_SymbolRef: " + fbcReturnStmt.VarValue.SymbolRef.ToString(), "", 4, null));
                                tnReturnStmt.Nodes.Add(O2Forms.newTreeNode("VarValue.VariableName: " + fbcReturnStmt.VarValue.VariableName, "", 4, null));
                            }
                            tnItems.Nodes.Add(tnReturnStmt);                            
                            break;
                        case "AssignmentStmt":
                            TreeNode tnAssignmentStmt = O2Forms.newTreeNode("AssignmentStmt", "", 6, oItem);
                            xsd.CirDump.CommonIRDumpCommonIRClassMethodsClassStaticFunctionBasicBlockAssignmentStmt fbcAssignmentStmt = (xsd.CirDump.CommonIRDumpCommonIRClassMethodsClassStaticFunctionBasicBlockAssignmentStmt)oItem;
                            if (fbcAssignmentStmt.ConstNull != null)
                                tnAssignmentStmt.Nodes.Add(O2Forms.newTreeNode("ConstNull: " + fbcAssignmentStmt.ConstNull.ToString(), "", 4, null));
                            if (fbcAssignmentStmt.ConstVarAddress != null)                                                            
                                tnAssignmentStmt.Nodes.Add(O2Forms.newTreeNode("ConstVarAddress: " + 
                                    fbcAssignmentStmt.ConstVarAddress.SymbolRef +  "  :  " +
                                    fbcAssignmentStmt.ConstVarAddress.VariableName 
                                    , "", 4, null));                            
                            if (fbcAssignmentStmt.NaryCall != null)
                                tnAssignmentStmt.Nodes.Add(O2Forms.newTreeNode("NaryCall: " + fbcAssignmentStmt.NaryCall.ToString(), "", 4, null));
                            if (fbcAssignmentStmt.NaryCallVirtual != null)
                            {
                                tnAssignmentStmt.Nodes.Add(O2Forms.newTreeNode("NaryCallVirtual: " +
                                    fbcAssignmentStmt.NaryCallVirtual.SymbolRef + "  :   " +
                                    fbcAssignmentStmt.NaryCallVirtual.FunctionName + "  :   "
                                    //fbcAssignmentStmt.NaryCallVirtual. + "  :   " +
                                    , "", 4, null));
                            }
                            if (fbcAssignmentStmt.NaryOprNewObject != null)
                                tnAssignmentStmt.Nodes.Add(O2Forms.newTreeNode("NaryOprNewObject: " + fbcAssignmentStmt.NaryOprNewObject.ToString(), "", 4, null));
                            if (fbcAssignmentStmt.UnaryOprCast != null)
                                tnAssignmentStmt.Nodes.Add(O2Forms.newTreeNode("UnaryOprCast: " + fbcAssignmentStmt.UnaryOprCast.ToString(), "", 4, null));
                            if (fbcAssignmentStmt.VarValue != null)
                                tnAssignmentStmt.Nodes.Add(O2Forms.newTreeNode("VarValue: " + fbcAssignmentStmt.VarValue.ToString(), "", 4, null));

                            tnItems.Nodes.Add(tnAssignmentStmt);                            
                            break;
                        case "EvalExprStmt":
                            tnItems.Nodes.Add("EvalExprStmt");
                            break;
                        case "Edge":
                            tnItems.Nodes.Add("Edge");
                            break;                        
                        default:
                            tnItems.Nodes.Add(sItemType);
                            break;
                    }                    
                    
                }
             }
                tnTargetTreeNode.Nodes.Add(tnItems);
             * */
        }

        private void lbCirAnalyzer_MethodsWithControlFlowGraph_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        /* private void lbCirAnalyzer_Files_DoubleClick(object sender, EventArgs e)
        {
            Cmd.s("open SourceCodeEditor," + lbCirAnalyzer_Files.Text);
        }*/

        private void cbBaseDirectory_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
                cbBaseDirectory_SelectedIndexChanged(null, null);
        }

        private void btLoadAllFiles_Click(object sender, EventArgs e)
        {
            var lsFilesToLoad = new List<string>();
            foreach (String sFile in lbFilesInSelectedDirectory.Items)
                lsFilesToLoad.Add(Path.Combine(cbBaseDirectory.Text, sFile));
            processFiles_(lsFilesToLoad, tvAllClassesAndMethods);
        }

        private void tbCirAnalyszer_TextSearchFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                calculateValuesFromGuiAnRunTextSearch();
        }

        private void btCirAnalyzer_SaveCirData_Click(object sender, EventArgs e)
        {
            CirDataUtils.saveSerializedO2CirDataObjectToFile(fcdCirData,tbCirAnalyzer_PathToSavedCirDataFile.Text);
        }

        private void btCirAnalyzer_LoadCirDataFile_Click(object sender, EventArgs e)
        {
            CirData fcdLoadedCirData =
                CirLoad.loadSerializedO2CirDataObject(tbCirAnalyzer_PathToSavedCirDataFile.Text);
            if (fcdLoadedCirData != null)
            {
                fcdCirData = fcdLoadedCirData;
                showO2CirDataInTreeViewAndListBox(fcdCirData, tvAllClassesAndMethods, lbO2CirData_Functions);
            }
        }


        private void tbCirAnalyszer_TextSearchFilter_TextChanged(object sender, EventArgs e)
        {
            cbCirAnalyzer_TextFilter_SuperClass.Checked = true;
        }

        public void calculateValuesFromGuiAnRunTextSearch()
        {
            if (tvAllClassesAndMethods.Visible == false) // means we are doing a previous seach
                return;
            String sSuperClass = (cbCirAnalyzer_TextFilter_SuperClass.Checked)
                                     ? tbCirAnalyszer_TextSearchFilter_SuperClass.Text
                                     : "";
            String sClassName = (cbCirAnalyzer_TextFilter_Classes.Checked)
                                    ? tbCirAnalyszer_TextSearchFilter_Class.Text
                                    : "";
            String sFunctionName = (cbCirAnalyzer_TextFilter_Functions.Checked)
                                       ? tbCirAnalyszer_TextSearchFilter_Function.Text
                                       : "";
            String sParameterType = (cbCirAnalyzer_TextFilter_Parameters.Checked)
                                        ? tbCirAnalyszer_TextSearchFilter_Parameter.Text
                                        : "";
            ;
            String sMakesCallsTo = (cbCirAnalyzer_TextFilter_MakesCallsTo.Checked)
                                       ? tbCirAnalyszer_TextSearchFilter_MakesCallsTo.Text
                                       : "";
            ;
            String sRemoveMakesCallsTo = (cbCirAnalyzer_TextFilter_RemoveMakesCallsTo.Checked)
                                             ? tbCirAnalyszer_TextSearchFilter_RemoveMakesCallsTo.Text
                                             : "";
            ;

            if (sSuperClass == "" && sClassName == "" && sFunctionName == "" && sParameterType == "" &&
                sMakesCallsTo == "" && sRemoveMakesCallsTo == "")
                showO2CirDataInTreeViewAndListBox(fcdCirData, tvAllClassesAndMethods, lbO2CirData_Functions);
            else
                runTextSearch(sSuperClass, sClassName, sFunctionName, sParameterType, sMakesCallsTo, sRemoveMakesCallsTo);
        }

        public void runTextSearch(String sSuperClass, String sClassName, String sFunctionName, String sParameterType,
                                  String sMakesCallsTo, String sRemoveMakesCallsTo)
        {
            DI.log.debug("Running Cir Text Search on: ClassName={0} , FunctionName={1} , ParameterType={1}",
                         sClassName, sFunctionName, sParameterType);

            bool bOnlyProcessFunctionsWithControlFlowGraph = true;
            bool bVerbose = true;
            String sStringToSearch = tbCirAnalyszer_TextSearchFilter_SuperClass.Text;
            Dictionary<String, ICirClass> dMatches = CirDataUtils.analysis_getFunctionsThatMatchFilter(fcdCirData,
                sSuperClass, sClassName, sFunctionName, sParameterType, sMakesCallsTo, sRemoveMakesCallsTo,
                bOnlyProcessFunctionsWithControlFlowGraph, bVerbose);

            //    foreach (CirClass ccClass in dMatches.Values)
            //        DI.log.info(ccClass.FunctionSignature);

            var fcdResults = new CirData
                                 {
                                     dClasses_bySignature = dMatches,
                                     dSymbols = fcdCirData.dSymbols,
                                     lFiles = fcdCirData.lFiles
                                 };
            CirDataUtils.resolveDbId(fcdCirData);
            fcdResults.sDbId = fcdCirData.sDbId;
            /*
            foreach (CirClass ccCirClass in fcdCirData.dClasses.Values)
            {
                //String sResolvedClassName = fcdResults.getSymbol(sClassName);
    //            DI.log.info(ccCirClass.FunctionSignature);
                if (ccCirClass.FunctionSignature.ToUpper().IndexOf(sStringToSearch) > -1)      // case insensive search
                    fcdResults.dClasses.Add(ccCirClass.SymbolDef, ccCirClass);
            }*/
            showO2CirDataInTreeViewAndListBox(fcdResults, tvAllClassesAndMethods, lbO2CirData_Functions);
            DI.log.debug("Search completed");
        }

        private void tbCirAnalyszer_TextSearchFilter_Function_TextChanged(object sender, EventArgs e)
        {
            if (tbCirAnalyszer_TextSearchFilter_Function.Text == "")
                cbCirAnalyzer_TextFilter_Functions.Checked = false;
            else
                cbCirAnalyzer_TextFilter_Functions.Checked = true;
        }

        private void tbCirAnalyszer_TextSearchFilter_Parameter_TextChanged(object sender, EventArgs e)
        {
            if (tbCirAnalyszer_TextSearchFilter_Parameter.Text == "")
                cbCirAnalyzer_TextFilter_Parameters.Checked = false;
            else
                cbCirAnalyzer_TextFilter_Parameters.Checked = true;
        }

        private void lbO2CirData_Functions_SelectedIndexChanged(object sender, EventArgs e)
        {
            // DC - removed so that this is not dependent on the MySQl data layer 
            // DC - reimplement this call with the new network communication layer
            /*
            bool bShowCustomRulesDetailEventRaised = false;
            //ascx_RulesCreator1.clearTargetslist();
            foreach (Object oObject in lbO2CirData_Functions.SelectedItems)
            {
                if (oObject.GetType().Name == "CirFunction")
                {
                    String sFunctionSignature = ((CirFunction)oObject).FunctionSignature;
                    //ascx_RulesCreator1.addMethodToTargetsList(fcdCirData.sDbId, sFunctionSignature);
                    if (false == bShowCustomRulesDetailEventRaised)
                    {
                        o2.ounce.datalayer.mysql.MySqlEvents.raiseEvent_ShowCustomRulesDetails_MethodSignature(fcdCirData.sDbId, sFunctionSignature);
                        bShowCustomRulesDetailEventRaised = true;
                    }
                }
                else
                    DI.log.error("Could not add function to auto Rules editor");
            } */
        }

        private void lbO2CirData_Functions_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                while (lbO2CirData_Functions.SelectedItems.Count > 0)
                    lbO2CirData_Functions.Items.Remove(lbO2CirData_Functions.SelectedItems[0]);
        }

        private void btViewByFunction_SelectAll_Click(object sender, EventArgs e)
        {
            //foreach (Object oItem in lbO2CirData_Functions.Items)
            //    lbO2CirData_Functions.SelectedItems.Add(oItem);
            // remove event so that we don't have one fired for every change
            lbO2CirData_Functions.SelectedIndexChanged -= lbO2CirData_Functions_SelectedIndexChanged;

            for (int iIndex = 0; iIndex < lbO2CirData_Functions.Items.Count; iIndex++)
                lbO2CirData_Functions.SelectedIndices.Add(iIndex);

            // add it back
            lbO2CirData_Functions.SelectedIndexChanged += lbO2CirData_Functions_SelectedIndexChanged;
            // and manually trigger this since none of these events have been fired
            lbO2CirData_Functions_SelectedIndexChanged(null, null);
        }

        private void cbViewByFunction_FullSignature_CheckedChanged(object sender, EventArgs e)
        {
            //showO2CirDataInListBox(fcdCirData, lbO2CirData_Functions,);
        }

        private void lbO2CirDataFilesInSelectedDir_DoubleClick(object sender, EventArgs e)
        {
            lbO2CirDataFilesInSelectedDir.Enabled = false;
            Application.DoEvents();
            String sPathToFileToProcess = Path.Combine(sCurrentBaseDir, lbO2CirDataFilesInSelectedDir.Text);
            lbFileLoaded.Text = sPathToFileToProcess;
            CirData fcdLoadedCirData = CirLoad.loadSerializedO2CirDataObject(sPathToFileToProcess);
            if (fcdLoadedCirData != null)
            {
                if (cbOnSelectUpdateIsCalledByMappigns.Checked)
                {
                    if (fcdCirData == null)
                        DI.log.error(
                            "in lbO2CirDataFilesInSelectedDir_DoubleClick: this.fcdCirData == null (you must a project first before consolidating");
                    else
                        CirDataUtils.addIsCalledByMappings(fcdCirData,fcdLoadedCirData);
                }
                else
                    fcdCirData = fcdLoadedCirData;
                if (cbCirAnaLyzer_DontUpdateOnLoad.Checked)
                {
                    tvAllClassesAndMethods.Nodes.Clear();
                    lbO2CirData_Functions.Items.Clear();
                }
                else
                {
                    cbCirAnalyzer_TextFilter_Classes.Checked = false;
                    cbCirAnalyzer_TextFilter_Functions.Checked = false;
                    cbCirAnalyzer_TextFilter_MakesCallsTo.Checked = false;
                    cbCirAnalyzer_TextFilter_Parameters.Checked = false;
                    cbCirAnalyzer_TextFilter_RemoveMakesCallsTo.Checked = false;
                    cbCirAnalyzer_TextFilter_SuperClass.Checked = false;
                    showO2CirDataInTreeViewAndListBox(fcdCirData, tvAllClassesAndMethods, lbO2CirData_Functions);
                }
            }
            lbO2CirDataFilesInSelectedDir.Enabled = true;
            DI.log.info("Done loading CirDataFile");
        }

        private void lbO2CirDataFilesInSelectedDir_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void splitContainer6_Panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void tvAllClassesAndMethods_AfterSelect(object sender, TreeViewEventArgs e)
        {
            /*if (tvAllClassesAndMethods.SelectedNode != null && tvAllClassesAndMethods.SelectedNode.Tag != null)
            {
                String asd = tvAllClassesAndMethods.SelectedNode.Tag.GetType().Name;
            }*/
            // DC - removed so that this is not dependent on the MySQl data layer 
            /*
            if (tvAllClassesAndMethods.SelectedNode != null && tvAllClassesAndMethods.SelectedNode.Tag != null && tvAllClassesAndMethods.SelectedNode.Tag.GetType().Name == "CirFunction")
            {
                String sFunctionSignature = ((CirFunction)tvAllClassesAndMethods.SelectedNode.Tag).FunctionSignature;
                if (fcdCirData.sDbId == "")
                    fcdCirData.resolveDbId();
                //ascx_RulesCreator1.addMethodToTargetsList(fcdCirData.sDbId, sFunctionSignature,true);
                o2.ounce.datalayer.mysql.MySqlEvents.raiseEvent_ShowCustomRulesDetails_MethodSignature(fcdCirData.sDbId, sFunctionSignature);
            }
             */
        }

        private void ascx_DropObject1_eDnDAction_ObjectDataReceived_Event(object oObject)
        {
            //String ads = oObject.GetType().Name;
            if (oObject.GetType().Name == "List`1")
            {
                var lObject = (List<Object>) oObject;
                foreach (Object oItem in lObject)
                {
                    String sdata = oItem.ToString();
                    if (oItem.GetType().Name == "String[]")
                    {
                        var asDataReceived = (String[]) oItem;
                        foreach (String sDataReceived in asDataReceived)
                            if (File.Exists(sDataReceived))
                            {
                                processFile(sDataReceived, tvAllClassesAndMethods);
                                //    loadAssessmentRunXmlFile(sDataReceived);
                                return;
                            }
                    }
                    //     if (File.Exists(oItem.ToString()))
                    //     {
                    //     }
                }
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
        }

        private void btTestToSeeIfAllFilesCanBeLoaded_Click(object sender, EventArgs e)
        {
            int iFilesToTest = lbFilesInSelectedDirectory.Items.Count;
            DI.log.debug("Going to try to load {0} files as Xml documents to confirm they can be deserialized",
                         iFilesToTest);
            var lsFilesToLoad = new List<string>();
            foreach (String sFile in lbFilesInSelectedDirectory.Items)
            {
                lsFilesToLoad.Add(Path.Combine(cbBaseDirectory.Text, sFile));
                //   if (iFilesToTest-- % 50 == 0)
                //       DI.log.info("{0} files left", iFilesToTest);
                //   Application.DoEvents();                
            }
            testCirDumpFileLoading(lsFilesToLoad);
        }

        public void testCirDumpFileLoading(List<String> lsFilesToLoad)
        {
            iNumberOfTestsToExecute = lsFilesToLoad.Count;
            lFilesThatLoadOk = new List<string>();
            lFilesThatDontLoad = new List<string>();
            dtStartTime = DateTime.Now;
            dMBytesProcessed = 0;
            var lThreads = new List<ThreadStart>();
            foreach (String sFileToLoad in lsFilesToLoad)
            {
                var tcdThread = new testCirDumpLoad_thread(sFileToLoad, lFilesThatLoadOk, lFilesThatDontLoad,
                                                           testCompleted);
                WaitCallback wcbWaitCallBack = tcdThread.executeTest;
                ThreadPool.QueueUserWorkItem(wcbWaitCallBack);
            }
        }

        public void testCompleted(int iBytesProcessed)
        {
            iNumberOfTestsToExecute--;
            dMBytesProcessed += iBytesProcessed/(1024*1024);
            if (iNumberOfTestsToExecute == 0 || iNumberOfTestsToExecute%10 == 0)
                DI.log.info("Tests to execute{0} (ok : {1} , failed : {2})   - {3}  : MBytes ok: {4}",
                            iNumberOfTestsToExecute, lFilesThatLoadOk.Count, lFilesThatDontLoad.Count,
                            (DateTime.Now - dtStartTime).ToString(), dMBytesProcessed);
        }

        private void tbCirAnalyszer_TextSearchFilter_SuperClass_TextChanged(object sender, EventArgs e)
        {
            if (tbCirAnalyszer_TextSearchFilter_SuperClass.Text == "")
                cbCirAnalyzer_TextFilter_SuperClass.Checked = false;
            else
                cbCirAnalyzer_TextFilter_SuperClass.Checked = true;
        }

        private void tbCirAnalyszer_TextSearchFilter_Class_TextChanged(object sender, EventArgs e)
        {
            if (tbCirAnalyszer_TextSearchFilter_Class.Text == "")
                cbCirAnalyzer_TextFilter_Classes.Checked = false;
            else
                cbCirAnalyzer_TextFilter_Classes.Checked = true;
        }

        private void tbCirAnalyszer_TextSearchFilter_IsCalledBy_TextChanged(object sender, EventArgs e)
        {
            if (tbCirAnalyszer_TextSearchFilter_MakesCallsTo.Text == "")
                cbCirAnalyzer_TextFilter_MakesCallsTo.Checked = false;
            else
                cbCirAnalyzer_TextFilter_MakesCallsTo.Checked = true;
        }

        private void tbCirAnalyszer_TextSearchFilter_RemoveIsCalledBy_TextChanged(object sender, EventArgs e)
        {
            if (tbCirAnalyszer_TextSearchFilter_RemoveMakesCallsTo.Text == "")
                cbCirAnalyzer_TextFilter_RemoveMakesCallsTo.Checked = false;
            else
                cbCirAnalyzer_TextFilter_RemoveMakesCallsTo.Checked = true;
        }

        private void btMaxView_Click(object sender, EventArgs e)
        {
            setMode_simple();
        }

        public void setMode_simple()
        {
            splitContainer5.Panel1Collapsed = true;
            splitContainer6.Panel2Collapsed = true;
            scCirAnalyzer.Panel2Collapsed = true;
        }

        public void setMode_onlyShowCirTreeView()
        {
            Controls.Clear();
            tvAllClassesAndMethods.Dock = DockStyle.Fill;
            Controls.Add(tvAllClassesAndMethods);
        }

        public void clearFilesInSelectedDirectory()
        {
            lbFilesInSelectedDirectory.Items.Clear();
            clearViewers();
        }

        #region Nested type: testCirDumpLoad_thread

        public class testCirDumpLoad_thread
        {
            private readonly Callbacks.dMethod_Int dTestCompleted;
            private readonly List<String> lFilesThatDontLoad;
            private readonly List<String> lFilesThatLoadOk;
            private readonly String sFileToLoad;

            public testCirDumpLoad_thread(String sFileToLoad, List<String> lFilesThatLoadOk,
                                          List<String> lFilesThatDontLoad, Callbacks.dMethod_Int dCallback)
            {
                this.sFileToLoad = sFileToLoad;
                this.lFilesThatLoadOk = lFilesThatLoadOk;
                this.lFilesThatDontLoad = lFilesThatDontLoad;
                dTestCompleted += dCallback;
            }

            public void executeTest(Object oObject)
            {
                var xdXmlDocument = new XmlDocument();
                try
                {
                    xdXmlDocument.Load(sFileToLoad);
                    lFilesThatLoadOk.Add(sFileToLoad);
                    int iXmlTextSize = xdXmlDocument.DocumentElement.OuterXml.Length;
                    dTestCompleted.Invoke(iXmlTextSize);
                }
                catch (Exception ex)
                {
                    DI.log.error("Could not load file {0} : {1}", sFileToLoad, ex.Message);
                    lFilesThatDontLoad.Add(sFileToLoad);
                    dTestCompleted.Invoke(0);
                }
                xdXmlDocument = null;
            }
        }

        #endregion
    }
}
