using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirUtils;
using O2.Core.CIR.Xsd;
using O2.DotNetWrappers.Filters;
using O2.DotNetWrappers.Windows;
using O2.Kernel.Interfaces.CIR;
using O2.RnD.SpringMVCAnalyzer.classes;
//using O2.Rules.OunceLabs.DataLayer;

namespace O2.RnD.SpringMVCAnalyzer.ascx
{
    public partial class ascx_SpringMvcAnalyzer : UserControl
    {
        public ascx_SpringMvcAnalyzer()
        {
            InitializeComponent();
        }

        private void btTest_Click(object sender, EventArgs e)
        {
            tbO2CirDataOfProject.Text = @"E:\_AppsToScan\Done\jpetStore_Spring\JPetStore-Spring.paf.CirData";
            tbWebRoot.Text = @"E:\_AppsToScan\Done\jpetStore_Spring\WebContent";
            loadData();
        }

        public CirData getO2CirData()
        {
            String sO2CirDataFile = tbO2CirDataOfProject.Text;
            CirData fadCirData = CirLoad.loadSerializedO2CirDataObject(sO2CirDataFile);
            return fadCirData;
        }

        public void loadData()
        {
            CirData fadCirData = getO2CirData();
            ShowBeans();

            List<String> lsClasses = findSpringMvcClasses(fadCirData);
            showSpringMvcClases(lsClasses, fadCirData);
            showSpringSuperClasses(lsClasses, fadCirData);
            mapCommandNamesFromTreeNodes(tvSpringMvcClasses.Nodes);
            calculateCustomRuleToAdd(fadCirData);
        }

        private void test_Load(object sender, EventArgs e)
        {
            if (DesignMode == false)
            {
                // btTest_Click(null, null);
                asvp_VisiblePanels_CirAnalysis.setVisibleControlsColapseState_4Panels_TopRight(scHost, scTop, scBottom,
                                                                                               "Beans",
                                                                                               "SpringMvc Functions - Call View",
                                                                                               "SpringMvc Functions - SuperClasses",
                                                                                               "");
                asvp_VisiblePanels_CirAnalysis.setCheckBox_Checked(2, true);
                asvp_VisiblePanels_CirAnalysis.setCheckBox_Checked(3, true);
                asvp_VisiblePanels_CirAnalysis.setCheckBox_Checked(4, true);
            }
        }

        public void calculateCustomRuleToAdd(CirData fadCirData)
        {
            var lsRulesToAdd = new List<string>();
            fromTreeNodeListCalculateRulesToAdd_recursive(tvCommandNameObjectFields.Nodes, lsRulesToAdd, fadCirData);
            lbRulesToAdd.Items.Clear();
            lbRulesToAdd.Items.AddRange(lsRulesToAdd.ToArray());
        }

        public void fromTreeNodeListCalculateRulesToAdd_recursive(TreeNodeCollection tncTreeNodes,
                                                                  List<String> lsRulesToAdd, CirData fadCirData)
        {
            foreach (TreeNode tnTreeNode in tncTreeNodes)
            {
                if (fadCirData.dClasses_bySignature.ContainsKey(tnTreeNode.Text))
                {
                    ICirClass ccCirClass = fadCirData.dClasses_bySignature[tnTreeNode.Text];
                    foreach (ICirFunction cfCirFunction in ccCirClass.dFunctions.Values)
                        foreach (TreeNode tnChildNode in tnTreeNode.Nodes)
                        {
                            String sGetterVersion = tnChildNode.Text.Replace("set", "get");
                            if (new FilteredSignature(cfCirFunction.FunctionSignature).sFunctionName == sGetterVersion)
                            {
                                if (false == lsRulesToAdd.Contains(cfCirFunction.FunctionSignature))
                                    lsRulesToAdd.Add(cfCirFunction.FunctionSignature);
                            }
                            if (tnChildNode.Nodes.Count > 0)
                                fromTreeNodeListCalculateRulesToAdd_recursive(tnChildNode.Nodes, lsRulesToAdd,
                                                                              fadCirData);
                        }
                }
                //   String sClass = tnTreeNode.Text;
            }
        }

        public void mapCommandNamesFromTreeNodes(TreeNodeCollection tncNodes)
        {
            dgvResolvedCommandName.Columns.Clear();
            O2Forms.addToDataGridView_Column(dgvResolvedCommandName, "class", -1);
            O2Forms.addToDataGridView_Column(dgvResolvedCommandName, "function", -1);
            O2Forms.addToDataGridView_Column(dgvResolvedCommandName, "value", -1);
            addToDataGridView_CommandMappingInTreeNode_Recursive(tncNodes, dgvResolvedCommandName);
        }

        public void addToDataGridView_CommandMappingInTreeNode_Recursive(TreeNodeCollection tncNodes,
                                                                         DataGridView dgvTargetDataGridView)
        {
            foreach (TreeNode tnTreeNode in tncNodes)
            {
                if (tnTreeNode.Text.IndexOf("setCommandName") > -1 && tnTreeNode.Nodes.Count > 0)
                {
                    String sMethod = new FilteredSignature(tnTreeNode.Text).sFunctionNameAndParams;
                    String sClass = tnTreeNode.Parent.Parent.Text;
                    String sValue = tnTreeNode.Nodes[0].Text;
                    O2Forms.addToDataGridView_Row(dgvTargetDataGridView, null,
                                                  new[] {sClass, sMethod, sValue});
                }
                addToDataGridView_CommandMappingInTreeNode_Recursive(tnTreeNode.Nodes, dgvTargetDataGridView);
            }
        }

        public void showSpringSuperClasses(List<String> lsClasses, ICirData fadCirData)
        {
            tvSpringMvc_SuperClasses.Nodes.Clear();
            foreach (String sClass in lsClasses)
            {
                ICirClass ccCirClass = fadCirData.dClasses_bySignature[sClass];
                var tnClass = new TreeNode(ccCirClass.Signature);
                addIsSuperClassedByToTreeNode_ReCursive(tnClass, ccCirClass);
                tvSpringMvc_SuperClasses.Nodes.Add(tnClass);
            }
        }

        public void addIsSuperClassedByToTreeNode_ReCursive(TreeNode tnTargetTreeNode, ICirClass ccCirClass)
        {
            foreach (ICirClass ccSuperClass in ccCirClass.dSuperClasses.Values)
            {
                var tnSuperClass = new TreeNode(ccSuperClass.Signature);
                addIsSuperClassedByToTreeNode_ReCursive(tnSuperClass, ccSuperClass);
                tnTargetTreeNode.Nodes.Add(tnSuperClass);
            }
        }

        public void showSpringMvcClases(List<String> lsClasses, CirData fadCirData)
        {
            tvSpringMvcClasses.Nodes.Clear();
            foreach (String sClass in lsClasses)
            {
                ICirClass ccCirClass = fadCirData.dClasses_bySignature[sClass];
                var tnClass = new TreeNode(ccCirClass.Signature);
                foreach (CirFunction cfCirFunction in ccCirClass.dFunctions.Values)
                {
                    var fsSignature = new FilteredSignature(cfCirFunction.FunctionSignature);
                    var tnFunction = new TreeNode(fsSignature.sFunctionNameAndParams);
                    foreach (String sFunctionCalled in ViewHelpers.getCirFunctionStringList(cfCirFunction.FunctionsCalledUniqueList))
                    {
                        var tnFunctionCalled = new TreeNode(sFunctionCalled);
                        //if (sFunctionCalled.IndexOf("setCommandName") > -1)                                                   
                        String sCommandNameValue = getValueFromControlFlowGraphCall(cfCirFunction.lcfgBasicBlocks,
                                                                                    sFunctionCalled);
                        if (sCommandNameValue != "")
                            tnFunctionCalled.Nodes.Add(sCommandNameValue);
                        tnFunction.Nodes.Add(tnFunctionCalled);
                    }

                    tnClass.Nodes.Add(tnFunction);
                }
                tvSpringMvcClasses.Nodes.Add(tnClass);
            }
        }

        public String getValueFromControlFlowGraphCall(List<ControlFlowGraphBasicBlock> lcfgBasicBlocks,
                                                       String sFunctionToFind)
        {
            foreach (ControlFlowGraphBasicBlock cfgBasicBlock in lcfgBasicBlocks)
            {
                if (cfgBasicBlock.Items != null)
                    foreach (Object oBasicBlockItem in cfgBasicBlock.Items)
                    {
                        switch (oBasicBlockItem.GetType().Name)
                        {
                            case "ControlFlowGraphBasicBlockEvalExprStmt":
                                var cfgNaryCall = (ControlFlowGraphBasicBlockEvalExprStmt) oBasicBlockItem;
                                if (cfgNaryCall.NaryCallVirtual != null &&
                                    cfgNaryCall.NaryCallVirtual.FunctionName == sFunctionToFind)
                                {
                                    if (cfgNaryCall.NaryCallVirtual.Items.Length > 1 &&
                                        cfgNaryCall.NaryCallVirtual.Items[1].GetType().Name ==
                                        "ControlFlowGraphBasicBlockEvalExprStmtNaryCallVirtualUnaryOprCast")
                                    {
                                        var cfgUnaryOprCas =
                                            (ControlFlowGraphBasicBlockEvalExprStmtNaryCallVirtualUnaryOprCast)
                                            cfgNaryCall.NaryCallVirtual.Items[1];
                                        return cfgUnaryOprCas.ConstNarrowString.Value;
                                    }
                                    
                                    return "";
                                }
                                break;
                        }
                    }
            }
            return "";
        }

        public List<String> findSpringMvcClasses(CirData fadCirData)
        {
            List<String> lsClasses = UsingCirData.findClassesThatImplementTheSpringMvc(fadCirData);
            return lsClasses;
        }


        public void ShowBeans()
        {
            try
            {
                String sWebRoot = tbWebRoot.Text;
                Dictionary<String, XmlNode> dBeans = BeanUtils.getAllBeans_RecursiveSearch(sWebRoot);
                var sbSpringBeans = new SpringBeans(dBeans);
                showSpringBeansInDataGrdView(sbSpringBeans);
            }
            catch (Exception ex)
            {
                DI.log.ex(ex);
            }            
        }

        public void showSpringBeansInDataGrdView(SpringBeans sbSpringBeans)
        {
            dgvBeans.Columns.Clear();
            foreach (var property in DI.reflection.getProperties(typeof(SpringMappingItem)))
                O2Forms.addToDataGridView_Column(dgvBeans, property.Name, -1);
            /*O2Forms.addToDataGridView_Column(dgvBeans, "bean", -1);
            O2Forms.addToDataGridView_Column(dgvBeans, "class", -1);
            O2Forms.addToDataGridView_Column(dgvBeans, "innerXml", -1);*/
            foreach (SpringMappingItem spiSpringMappingItem in sbSpringBeans.dSpringMappingItems.Values)
            {
                var items = new List<object>();
                foreach (var property in DI.reflection.getProperties(typeof(SpringMappingItem)))
                    items.Add(DI.reflection.getProperty(property.Name, spiSpringMappingItem));
                O2Forms.addToDataGridView_Row(dgvBeans, null, items.ToArray());
                /*
                                              new[]
                                                  {
                                                      spiSpringMappingItem.sBean, spiSpringMappingItem.sClass,
                                                      spiSpringMappingItem.sInnerXml
                                                  });*/
            }
        }

        private void dgvResolvedCommandName_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvResolvedCommandName.SelectedRows.Count == 1)
            {
                CirData fadCirData = getO2CirData();
                forClassLoadBindableFieldsIntoTreeView(tvCommandNameObjectFields,
                                                       dgvResolvedCommandName.SelectedRows[0].Cells["value"].Value.
                                                           ToString(), cbHideGetAndSetStrings.Checked, fadCirData);
            }
        }

        public void forClassLoadBindableFieldsIntoTreeView(TreeView tvTargetTreeView, String sClassToFind,
                                                           bool bHideGetAndSetStrings, CirData fadCirData)
        {
            tvTargetTreeView.Nodes.Clear();
            // make the first char upper case
            addBindableFieldsIntoTreeView_Recursive(tvTargetTreeView.Nodes, sClassToFind, bHideGetAndSetStrings,
                                                    fadCirData);
            tvTargetTreeView.ExpandAll();
        }

        public void addBindableFieldsIntoTreeView_Recursive(TreeNodeCollection tncTreeNodes, String sClassToFind,
                                                            bool bHideGetAndSetStrings, ICirData fadCirData)
        {
            String sFixedClassToFind = sClassToFind[0].ToString().ToUpper() + sClassToFind.Substring(1);
            foreach (ICirClass ccCirClass in fadCirData.dClasses_bySignature.Values)
            {
                if (ccCirClass.Name == sFixedClassToFind)
                {
                    var tnClass = new TreeNode(ccCirClass.Signature);

                    foreach (ICirFunction cfCirFunction in ccCirClass.dFunctions.Values)
                    {
                        String sFunctionName = new FilteredSignature(cfCirFunction.FunctionSignature).sFunctionName;
                        switch (sFunctionName.Substring(0, 3))
                        {
                            case "get":
                                var tnSubObject = new TreeNode(sFunctionName);
                                String sFixedSubObjectName = sFunctionName.Replace("get", "");
                                if (bHideGetAndSetStrings)
                                    tnSubObject.Text = sFixedSubObjectName;

                                // tnSubObject = tnClass;
                                addBindableFieldsIntoTreeView_Recursive(tnSubObject.Nodes, sFixedSubObjectName,
                                                                        bHideGetAndSetStrings, fadCirData);
                                if (tnSubObject.Nodes.Count > 0)
                                    tnClass.Nodes.Add(tnSubObject);
                                break;
                            case "set":
                                String sSetNodeText = new FilteredSignature(cfCirFunction.FunctionSignature).sFunctionName;
                                if (bHideGetAndSetStrings)
                                    sSetNodeText = sSetNodeText.Replace("set", "");
                                tnClass.Nodes.Add(sSetNodeText);
                                break;
                            default:
                                break;
                        }
                    }

                    tncTreeNodes.Add(tnClass);
                }
                //    String sClassName = 
            }
        }

        private void btMakeRulesSourcesOfTaintedData_Click(object sender, EventArgs e)
        {
    /*        const string sDbId = "2";
            var sVuln_id = "0";
            var sActionObjectSignature = "InputAnyTainted";
            var sSeverity = "High";
            var sVuln_type = "MVC AutoBind Issue";
            foreach (String sSignatureToAdd in lbRulesToAdd.Items)
                Lddb_OunceV6.action_makeMethod_Source(sDbId, sSignatureToAdd, sVuln_id,
                                              sActionObjectSignature, sSeverity, sVuln_type);
            */
            //  String sDbId = "2";
            //  String sVulnId = "0";
            //  String sVulnType = "MVC AutoBind Issue";
            //  foreach (String sSignatureToAdd in lbRulesToAdd.Items)
            //     lddb.action_makeMethod_Source(sDbId, sSignatureToAdd, sVulnId,sVulnType, true);
        }

        private void ascx_DropAreaForCirDataObject_eDnDAction_ObjectDataReceived_Event(object oObject)
        {
            String sItemReceived = oObject.ToString();
            if (File.Exists(sItemReceived))
                if (Path.GetExtension(sItemReceived).ToLower() != ".cirdata")
                    DI.log.error("File dropped must be of type .CirData: {0}", sItemReceived);
                else
                {
                    tbO2CirDataOfProject.Text = sItemReceived;
                    loadData();
                }
            else if (Directory.Exists(oObject.ToString()))
            {
                tbWebRoot.Text = oObject.ToString();
                loadData();
            }
        }

        private void btLoadDataFromWebRoot_Click(object sender, EventArgs e)
        {
            loadData();
        }

     
    }
}