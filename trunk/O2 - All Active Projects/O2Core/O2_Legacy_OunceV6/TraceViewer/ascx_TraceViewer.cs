using System;
using System.Windows.Forms;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Legacy.OunceV6.SavedAssessmentFile.classes;

namespace O2.Legacy.OunceV6.TraceViewer
{
    public partial class ascx_TraceViewer : UserControl
    {
        private Analysis.SmartTraceFilter stfSmartTraceFilter = Analysis.SmartTraceFilter.MethodName;

        public ascx_TraceViewer()
        {
            InitializeComponent();
        }

        private void rbSmartTraceFilter_Context_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSmartTraceFilter_Context.Checked)
            {
                stfSmartTraceFilter = Analysis.SmartTraceFilter.Context;
                refreshSmartTraceTreeView();
            }
        }

        private void rbSmartTraceFilter_MethodName_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSmartTraceFilter_MethodName.Checked)
            {
                stfSmartTraceFilter = Analysis.SmartTraceFilter.MethodName;
                refreshSmartTraceTreeView();
            }
        }

        private void rbSmartTraceFilter_SourceCode_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSmartTraceFilter_SourceCode.Checked)
            {
                stfSmartTraceFilter = Analysis.SmartTraceFilter.SourceCode;
                refreshSmartTraceTreeView();
            }
        }

        private void tvSmartTrace_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var fviFindingViewItem = (FindingViewItem) tvSmartTrace.Tag;
            O2AssessmentData_OunceV6 fadAssessmentDataOunceV6 = fviFindingViewItem.oadO2AssessmentDataOunceV6;
            switch (e.Node.Tag.GetType().Name)
            {
                case "CallInvocation":
                    var cCall = (CallInvocation) e.Node.Tag;
                    if (fadAssessmentDataOunceV6.arAssessmentRun.FileIndeces.Length < cCall.fn_id)
                        break;
                    String sSourceCodeFile = OzasmtUtils_OunceV6.getFileIndexValue(cCall.fn_id,
                                                                                   fviFindingViewItem.oadO2AssessmentDataOunceV6); //
                    //  fadAssessmentDataOunceV6.arAssessmentRun.FileIndeces[cCall.fn_id - 1].value;

                    ascx_SourceCodeEditor1.gotoLine(sSourceCodeFile, (int) cCall.line_number);

                    ascx_Glee1.showCallInGlee(e.Node.Text);

                    FindingsView.showCallInvocationDetailsInDataGridView(dgvCallInvocationDetails, cCall,
                                                                         fviFindingViewItem.oadO2AssessmentDataOunceV6);

                    //String sSignature = o2.analysis.Analysis.getStringIndexValue(cCall.sig_id, fadAssessmentDataOunceV6);
                    //         ascx_RulesCreator1.addMethodToTargetsList(fadAssessmentDataOunceV6.sDb_id, sSignature, true);
                    //         o2.ounce.datalayer.mysql.MySqlEvents.raiseEvent_ShowCustomRulesDetails_MethodSignature(fadAssessmentDataOunceV6.sDb_id, sSignature);

                    break;
                    //case "AssessmentAssessmentFileFinding":
                    //    break;
                default:
                    DI.log.error("in tvSmartTrace_AfterSelect: not supported type: {0}", e.Node.Tag.GetType().Name);
                    break;
            }
            tvSmartTrace.Focus();
        }

        public void refreshSmartTraceTreeView()
        {
            tvSmartTrace.Nodes.Clear();
            if (tvSmartTrace.Tag == null)
                return;
            var fviFindingViewItem = (FindingViewItem) tvSmartTrace.Tag;
            if (fviFindingViewItem.fFinding.Trace != null)
            {
                //String sNodeText = fviFindingViewItem.fFinding.caller_name;
                String sNodeText = "O2 Trace";
                //(fviFindingViewItem.fFinding.caller_name != null) ? fviFindingViewItem.fFinding.caller_name : o2.analysis.Analysis.getStringIndexValue(UInt32.Parse(fviFindingViewItem.fFinding.caller_name_id), fviFindingViewItem.oadO2AssessmentDataOunceV6);
                var tnRootNode = new TreeNode(sNodeText);
                tnRootNode.Tag = fviFindingViewItem.fFinding;
                AnalysisUtils.addCallsToNode_Recursive(fviFindingViewItem.fFinding.Trace, tnRootNode,
                                                       fviFindingViewItem.oadO2AssessmentDataOunceV6, stfSmartTraceFilter);
                tvSmartTrace.Nodes.Add(tnRootNode.Nodes[0]);
                tvSmartTrace.ExpandAll();
                if (ascx_SelectVisiblePanels1.getCheckBox(4).Checked)
                {
                    ascx_Glee1.setAssessmentData(fviFindingViewItem.oadO2AssessmentDataOunceV6);
                    ascx_Glee1.addTreeNodeToComboxWithNodesToPlot(tvSmartTrace.Nodes[0], fviFindingViewItem.fFinding,
                                                                  fviFindingViewItem.oadO2AssessmentDataOunceV6);
                    //   the way the Smart traces are build we want to add the 2nd child
                    ascx_Glee1.showLoadedTracesInGleeViewer();
                }
            }

            ascx_SourceCodeEditor1.gotoLine(
                fviFindingViewItem.oadO2AssessmentDataOunceV6.dFindings[fviFindingViewItem.fFinding].filename,
                (Int32) fviFindingViewItem.fFinding.line_number);
        }

        public void setTraceDataAndRefresh(FindingViewItem fviFindingViewItem)
        {
            tvSmartTrace.Tag = fviFindingViewItem;
            refreshSmartTraceTreeView();
            FindingsView.showFindingDetailsInDataGridView(dgvFindingsDetails, fviFindingViewItem.fFinding,
                                                          fviFindingViewItem.oadO2AssessmentDataOunceV6);
        }

        private void ascx_TraceViewer_Load(object sender, EventArgs e)
        {
            if (false == DesignMode)
            {
                ascx_SelectVisiblePanels1.setVisibleControlsColapseState_4Panels_TopRight("Trace Viewer", scHost, scLeft,
                                                                                          scRight, "Trace",
                                                                                          "Finding Details",
                                                                                          "Source Code", "Glee Viewer");
                ascx_SelectVisiblePanels1.setCheckBox_Checked(3, true);
            }
        }

        private void ascx_SourceCodeEditor1_Load(object sender, EventArgs e)
        {
        }

        private void scLeft_Panel2_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}