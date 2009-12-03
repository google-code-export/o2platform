// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Glee.Drawing;
using Microsoft.Glee.GraphViewerGdi;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Filters;
using O2.DotNetWrappers.Windows;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Legacy.OunceV6.GLEEGraphWiz.GleeUtils;
using O2.Legacy.OunceV6.SavedAssessmentFile.classes;

namespace O2.Legacy.OunceV6.GLEEGraphWiz.Ascx
{
    public partial class ascx_Glee : UserControl
    {
        #region viewMode enum

        public enum viewMode
        {
            oneTrace,
            allTraces
        }

        #endregion

        private Color cPreviouslySelectedGleeNode_LineColor;

        private O2AssessmentData_OunceV6 fadAssessmentDataOunceV6;
        private O2Graph.GraphData fgdGraphData;
        private Int32 iPreviouslySelectedGleeNode_LineWidth;

        private List<AssessmentAssessmentFileFinding> lfFindingsToGraph = new List<AssessmentAssessmentFileFinding>();
        // not used anymore, to remove

        private Node nPreviouslySelectedNode;
        private String sPreviouslySelectedGleeNode_Id = "";
        private String sPreviouslySelectedGleeNode_Label = "";

        public viewMode vmViewMode = viewMode.oneTrace; // default to oneTrace

        public ascx_Glee()
        {
            InitializeComponent();
        }

        public ascx_Glee(Graph gGraph)
        {
            if (gGraph != null)
                gvSmartTrace.Graph = gGraph;
        }

        public void loadGraph()
        {
            loadGraph(null);
        }

        public void loadGraph(Graph gGraph)
        {
            try
            {
                DI.log.info("Loading Graph in Glee");
                gvSmartTrace.Enabled = false;
                gvSmartTrace.Graph = gGraph;
            }
            catch (Exception ex)
            {
                DI.log.error("in loadGraph, when making gvSmartTrace.Graph = gGraph :{0}", ex.Message);
            }
            DI.log.info("Done Loading Graph in Glee");
            gvSmartTrace.Enabled = true;
        }

        private void ascx_Glee_Load(object sender, EventArgs e)
        {
            if (false == DesignMode)
            {
                updateViewBasedOnViewMode();
                //             this.ascx_InvokeDynamicFilters1.configureTargets("glee_object", "gleeFilters", this);
                updateVisibleControlsColapseState();
            }
        }


        public void setViewMode(viewMode vmViewMode)
        {
            this.vmViewMode = vmViewMode;
            updateViewBasedOnViewMode();
        }

        public void updateViewBasedOnViewMode()
        {
            switch (vmViewMode)
            {
                case viewMode.oneTrace:
                    scTopLeftAndRight.Panel1Collapsed = true;
                    scTracesAndScripts.Panel2Collapsed = true;
                    scGleeAndAnalysis.Panel2Collapsed = true;
                    break;
                case viewMode.allTraces:
                    //scGleeAndAnalysis.Panel2Collapsed = false;
                    scGleeAndAnalysis.Panel2Collapsed = true;

                    scTopLeftAndRight.Panel1Collapsed = false;
                    scTracesAndScripts.Panel2Collapsed = false;

                    // make the graph as simple and fast as possible
                    cbGLEE_ShowNamespace.Checked = false;
                    cbGLEE_ShowParameters.Checked = false;
                    cbGLEE_ShowReturnClass.Checked = false;
                    cbLDDB_ShowInsideNode_Context.Checked = false;
                    cbLDDB_ShowInsideNode_SourceCode.Checked = false;

                    cbLDDB_ShowInsideNode_MethodName.Checked = true;
                    cbOnlyShowDataFor_SourcesAndSinks.Checked = true;
                    break;
            }
        }

        public void showCallInGlee(String sNodeId)
        {
            if (gvSmartTrace.Graph == null)
                return;
            // first restore the previous selected node to the default

            if (sPreviouslySelectedGleeNode_Id != "" &&
                gvSmartTrace.Graph.FindNode(sPreviouslySelectedGleeNode_Id) != null)
            {
                var nPreviouslySelectedNode = (Node) gvSmartTrace.Graph.FindNode(sPreviouslySelectedGleeNode_Id);
                nPreviouslySelectedNode.NodeAttribute.Label = sPreviouslySelectedGleeNode_Label;
                nPreviouslySelectedNode.NodeAttribute.LineWidth = iPreviouslySelectedGleeNode_LineWidth;
                nPreviouslySelectedNode.NodeAttribute.Color = cPreviouslySelectedGleeNode_LineColor;
            }

            // then process the current selected node
            var nSelectedNode = (Node) gvSmartTrace.Graph.FindNode(sNodeId);
            if (nSelectedNode != null)
            {
                // save node's values
                sPreviouslySelectedGleeNode_Id = nSelectedNode.NodeAttribute.Id;
                sPreviouslySelectedGleeNode_Label = nSelectedNode.NodeAttribute.Label;
                iPreviouslySelectedGleeNode_LineWidth = nSelectedNode.NodeAttribute.LineWidth;
                cPreviouslySelectedGleeNode_LineColor = nSelectedNode.NodeAttribute.Color;

                nSelectedNode.NodeAttribute.LineWidth = 5;
                nSelectedNode.NodeAttribute.Color = Color.Sienna;
            }
            // and redraw the control
            gvSmartTrace.Refresh();
        }

        public void setAssessmentData(O2AssessmentData_OunceV6 fadAssessmentDataOunceV6)
        {
            this.fadAssessmentDataOunceV6 = fadAssessmentDataOunceV6;
        }

        public void showLoadedTracesInGleeViewer()
        {
            loadSmartTraceGraphInGleeViewer(rbGraphAllNodes.Checked);
        }

        public void loadSmartTraceGraphInGleeViewer(O2AssessmentData_OunceV6 fadAssessmentDataOunceV6)
        {
            setAssessmentData(fadAssessmentDataOunceV6);
            loadSmartTraceGraphInGleeViewer(rbGraphAllNodes.Checked);
        }

        public void loadSmartTraceGraphInGleeViewer(bool bLoadAllTraces)
        {
            if (fadAssessmentDataOunceV6 == null)
            {
                DI.log.error("in loadSmartTraceGraphInGleeViewer this.fadO2AssessmentData == null");
                return;
            }
            //if (tvGLEE_NodesToGraph.Nodes.Count > 0)
            //{
            populateComboBoxWithGleeNodeShapes();

            Graph gGraph;
            //gvSmartTrace.Graph.Cluster = cbGLEE_test.Checked;  // doesn't seem to have an impact (need to check what this does)        

            if (bLoadAllTraces)
                gGraph = getGraphWithAllNodesInTreeView();
            else
                gGraph = getGraphWithSelectedNodesInTreeView();

            if (cbGLEE_ConsolidateTraces.Checked)
                O2Graph.createGraphWithConsolidatedPaths(ref gGraph);
            loadGraphIntoViewer(gGraph);
            //}
        }

        public O2AssessmentData_OunceV6 getObjectAssessmentData()
        {
            return fadAssessmentDataOunceV6;
        }

        public O2Graph.GraphData getObjectGraphData()
        {
            return fgdGraphData;
        }

        public Graph getGraphWithAllNodesInTreeView()
        {
            bool bCachedValue_CheckBox_VisibleControls_GraphStats = cbVisibleControls_GraphStats.Checked;
            cbVisibleControls_GraphStats.Checked = false;
            var gMainGraph = new Graph("SmartTrace with all Nodes");
            foreach (TreeNode tnTreeNodeToGraph in tvGLEE_NodesToGraph.Nodes)
            {
                int iEdgeId = 0;
                var gSubGraph = new Graph(tnTreeNodeToGraph.ToString());
                //graph.populateGraphWithTreeNode(ref iEdgeId, gGraph, tnTreeNodeToGraph, cbGLEE_order.Checked);
                O2Graph.populateGraphWithTreeNode(ref iEdgeId, gSubGraph, tnTreeNodeToGraph, cbGLEE_order.Checked);
                applyFiltersToGraph(gSubGraph, false);
                O2Graph.addGraphToGraph(gSubGraph, gMainGraph);
            }
            // finally process graph with all traces
            //      this.applyFiltersToGraph(gMainGraph, false);
            if (bCachedValue_CheckBox_VisibleControls_GraphStats)
            {
                cbVisibleControls_GraphStats.Checked = bCachedValue_CheckBox_VisibleControls_GraphStats;
                //        gdmcrGraphDataMappedToCustomRules.showO2GraphData(this.fgdGraphData);
            }
            return gMainGraph;
        }

        public Graph getGraphWithSelectedNodesInTreeView()
        {
            var gGraph = new Graph("SmartTrace with Selected Nodes");
            if (tvGLEE_NodesToGraph.SelectedNode != null)
            {
                int iEdgeId = 0;
                O2Graph.populateGraphWithTreeNode(ref iEdgeId, gGraph, tvGLEE_NodesToGraph.SelectedNode,
                                                  cbGLEE_order.Checked);
            }
            return gGraph;
        }

        public bool applyFiltersToGraph(Graph gGraph, bool bVerbose)
        {
            try
            {
                int iNamespaceDepth = Int32.Parse(cBoxShowFunctionClass_Depth.Text);
                O2Graph.applyStylesAndFiltersToGraph(
                    gGraph,
                    (Shape) cbGlee_NodeShape.SelectedItem,
                    fadAssessmentDataOunceV6,
                    cbLDDB_ShowInsideNode_MethodName.Checked,
                    cbGLEE_ShowParameters.Checked,
                    cbGLEE_ShowReturnClass.Checked,
                    cbGLEE_ShowNamespace.Checked,
                    iNamespaceDepth,
                    cbLDDB_ShowInsideNode_Context.Checked,
                    cbLDDB_ShowInsideNode_SourceCode.Checked,
                    cbOnlyShowDataFor_SourcesAndSinks.Checked);


                if (cbOnlyShowDataFor_Class.Checked)
                    O2Graph.onlyShowNodesContainingClass(gGraph, tbOnlyShowDataFor_Class.Text);

                fgdGraphData = new O2Graph.GraphData(gGraph);
                //tbMaxToPlot.Text = fgdGraphData.iMaxToPlot.ToString();
                Int32.TryParse(tbMaxToPlot.Text, out fgdGraphData.iMaxToPlot);
                fgdGraphData.iItemsToRemove = Int32.Parse(cbConsolidateDepth.Text);
                // -1 means all (used to debug graph building

                if (cbOnlyShowDataFor_LostSources.Checked)
                    O2Graph.makeLostSourcesVisible(fgdGraphData, cbGLEE_ShowParameters.Checked,
                                                   cbGLEE_ShowReturnClass.Checked, cbGLEE_ShowNamespace.Checked,
                                                   iNamespaceDepth);
                if (cbOnlyShowDataFor_ConsolidateNonVisibleNodes.Checked)
                    O2Graph.consolidateNonVisibleNodes(fgdGraphData, bVerbose);
                if (bVerbose)
                    DI.log.info("New Graph object Created: : Nodes={0}, Edges={1}", fgdGraphData.gGraph.NodeCount,
                                fgdGraphData.gGraph.EdgeCount);
                if (cbVisibleControls_GraphStats.Checked)
                    gdmcrGraphDataMappedToCustomRules.showO2GraphData(fgdGraphData);
                if (fgdGraphData.gGraph.NodeCount + fgdGraphData.gGraph.EdgeCount > fgdGraphData.iMaxToPlot)
                {
                    DI.log.error("To many nodes and edges to view in Glee : Nodes={0}, Edges={1}, MaxToPlot={2}",
                                 fgdGraphData.gGraph.NodeCount, fgdGraphData.gGraph.EdgeCount,
                                 fgdGraphData.iMaxToPlot);
                    // clearGraph();
                    // fgdGraphData.gGraph = new Graph("To many nodes");
                    var nAlertNode =
                        (Node)
                        fgdGraphData.gGraph.AddNode(
                            "To many nodes to view - \n NOT ALL NODES WHERE SHOWN \n (add filters to reduce working set)");
                    nAlertNode.NodeAttribute.Fillcolor = Color.Red;
                    fgdGraphData.bShouldGraphBeShown = false;
                }
            }
            catch (Exception ex)
            {
                DI.log.error("in applyFiltersToGraph: {0}", ex.Message);
            }
            return fgdGraphData.bShouldGraphBeShown;
        }

        public void loadGraphIntoViewer(Graph gGraph)
        {
            try
            {
                fgdGraphData = new O2Graph.GraphData(gGraph);
                Int32.TryParse(tbMaxToPlot.Text, out fgdGraphData.iMaxToPlot);
                if (applyFiltersToGraph(fgdGraphData.gGraph, true) && cbDrawGleeGraph.Checked)
                    gvSmartTrace.Graph = fgdGraphData.gGraph;
            }
            catch (Exception ex)
            {
                DI.log.error("in loadGraphIntoViewer: {0}", ex.Message);
            }
        }

        public void populateComboBoxWithGleeNodeShapes()
        {
            //            cbGlee_NodeShape.Items.Clear();
            if (cbGlee_NodeShape.Items.Count == 0)
            {
                foreach (Shape sShape in Enum.GetValues(typeof (Shape)))
                    cbGlee_NodeShape.Items.Add(sShape);
                cbGlee_NodeShape.SelectedItem = Shape.Box;
            }
        }


        private void cbGlee_NodeShape_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSmartTraceGraphInGleeViewer(rbGraphAllNodes.Checked);
        }

        private void cbGLEE_ShowParameters_CheckedChanged(object sender, EventArgs e)
        {
            loadSmartTraceGraphInGleeViewer(rbGraphAllNodes.Checked);
        }


        private void cbGLEE_ShowNamespace_CheckedChanged(object sender, EventArgs e)
        {
            loadSmartTraceGraphInGleeViewer(rbGraphAllNodes.Checked);
        }

        private void cbGLEE_ShowReturnClass_CheckedChanged(object sender, EventArgs e)
        {
            loadSmartTraceGraphInGleeViewer(rbGraphAllNodes.Checked);
        }

        private void cbLDDB_ShowInsideNode_Context_CheckedChanged(object sender, EventArgs e)
        {
            loadSmartTraceGraphInGleeViewer(rbGraphAllNodes.Checked);
        }

        private void cbLDDB_ShowInsideNode_SourceCode_CheckedChanged(object sender, EventArgs e)
        {
            loadSmartTraceGraphInGleeViewer(rbGraphAllNodes.Checked);
        }

        private void cbLDDB_ShowInsideNode_MethodName_CheckedChanged(object sender, EventArgs e)
        {
            loadSmartTraceGraphInGleeViewer(rbGraphAllNodes.Checked);
        }

        private void gvSmartTrace_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            gvSmartTrace.ZoomF++;
        }

        private void gvSmartTrace_SelectionChanged(object sender, EventArgs e)
        {
            return;
            /*
            if (gvSmartTrace.SelectedObject != null && "Node" == gvSmartTrace.SelectedObject.GetType().Name)
            {
                Node nSelectedNode = (Node)gvSmartTrace.SelectedObject;
                lbGLEE_SelectedNode.Text = nSelectedNode.Id;

                //nSelectedNode.NodeAttribute.Label = "sNodeId";

                //if (nSelectedNode.NodeAttribute.Label == ".")
                //                    {
                //                      nSelectedNode.NodeAttribute.Label += Environment.NewLine + nSelectedNode.NodeAttribute.Id;
                //                    nSelectedNode.Attr.Fontsize = 20;
                //                  gvSmartTrace.DrawingPanel.Width += 100;
                //nSelectedNode.NodeAttribute.LabelMargin = 10;
                // nSelectedNode.NodeAttribute.Shape = Shape.Circle;
                nSelectedNode.NodeAttribute.Fillcolor = Microsoft.Glee.Drawing.Color.SlateGray;
                if (nPreviouslySelectedNode != null)
                    nPreviouslySelectedNode.NodeAttribute.Fillcolor = Microsoft.Glee.Drawing.Color.White;
                nPreviouslySelectedNode = nSelectedNode;
                //    }
                //nSelectedNode.NodeAttribute.Color = Microsoft.Glee.Drawing.Color.SlateGray;
                gvSmartTrace.Refresh();
            }
            */
        }

        private void gvSmartTrace_MouseMove(object sender, MouseEventArgs e)
        {
            //    Control c = gvSmartTrace.GetChildAtPoint(e.Location);
            //     DI.log.debug(c.Text);
        }

        private void cbGLEE_test_CheckedChanged(object sender, EventArgs e)
        {
            loadSmartTraceGraphInGleeViewer(rbGraphAllNodes.Checked);
        }

        private void gvSmartTrace_Load(object sender, EventArgs e)
        {
        }

        private void tvGLEE_NodesToGraph_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //     if (false == rbGraphAllNodes.Checked)
            //       loadSmartTraceGraphInGleeViewer(rbGraphAllNodes.Checked);
            //showCallInGlee(e.Node.Text);


            // highlight current selection
            var nSelectedNode = (Node) gvSmartTrace.Graph.FindNode(e.Node.Text);
            if (nSelectedNode == null)
                return;
            lbGLEE_SelectedNode.Text = nSelectedNode.Id;

            nSelectedNode.NodeAttribute.Fillcolor = Color.SlateGray;
            if (nPreviouslySelectedNode != null)
                nPreviouslySelectedNode.NodeAttribute.Fillcolor = Color.White;
            nPreviouslySelectedNode = nSelectedNode;

            gvSmartTrace.Refresh();

            // animate trace
            animateTraceInGleeViewer_Recursive(e.Node);
        }

        public void animateTraceInGleeViewer_Recursive(TreeNode tnTreeNode)
        {
            showCallInGlee(tnTreeNode.Text);
            foreach (TreeNode tnChildNode in tnTreeNode.Nodes)
            {
                Thread.Sleep(100);
                animateTraceInGleeViewer_Recursive(tnChildNode);
            }
        }

        private void tvGLEE_NodesToGraph_DoubleClick(object sender, EventArgs e)
        {
            tvGLEE_NodesToGraph_AfterSelect(sender, (TreeViewEventArgs) e);
        }

        private void tvGLEE_NodesToGraph_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b') //(char)Keys.Delete)
            {
                O2Forms.removeSelectedNodeFromTreeView(tvGLEE_NodesToGraph);
                loadSmartTraceGraphInGleeViewer(rbGraphAllNodes.Checked);
            }
        }


        public TreeView _getObject_tvGLEE_NodesToGraph()
        {
            return tvGLEE_NodesToGraph;
        }

        public GViewer _getObject_gvSmartTrace()
        {
            return gvSmartTrace;
        }

        //public void addNodeToGraph(TreeNode tnNodeToAdd)
        public void addNodeToGraph(TreeNode tnNodeToAdd, AssessmentAssessmentFileFinding fFinding)
        {
            //lfFindingsToGraph.Add(fFinding);
            tvGLEE_NodesToGraph.Nodes.Add(tnNodeToAdd);
        }


        public void clearGraph()
        {
            gvSmartTrace.Graph = new Graph("Clean graph");
            tvGLEE_NodesToGraph.Nodes.Clear();
            //lfFindingsToGraph.Clear();
        }

        public void addTreeNodeToComboxWithNodesToPlot(TreeNode tnTreeNodeToAdd,
                                                       AssessmentAssessmentFileFinding fFinding,
                                                       O2AssessmentData_OunceV6 fadAssessmentDataOunceV6)
        {
            try
            {


                if (false == cbGLEE_MultiNodes.Checked)
                    tvGLEE_NodesToGraph.Nodes.Clear();
                foreach (TreeNode tnTreeNode in tvGLEE_NodesToGraph.Nodes)
                    if (tnTreeNode.Tag == tnTreeNodeToAdd.Tag)
                    {
                        DI.log.debug("Trace was already in list of nodes to graph");
                        return;
                    }
                //lfFindingsToGraph.Add(fFinding);
                tvGLEE_NodesToGraph.Nodes.Add((TreeNode) tnTreeNodeToAdd.Clone());
                //     loadSmartTraceGraphInGleeViewer(fadO2AssessmentData);
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in addTreeNodeToComboxWithNodesToPlot");
            }
        }


        private void gvSmartTrace_DoubleClick(object sender, EventArgs e)
        {
            gvSmartTrace.ZoomF++;
        }


        private void cbGLEE_order_CheckedChanged(object sender, EventArgs e)
        {
            loadSmartTraceGraphInGleeViewer(rbGraphAllNodes.Checked);
        }

        private void cb_GLEE_GraphAllNodes_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void cbGLEE_GraphSelectedNode_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void rbGraphAllNodes_CheckedChanged(object sender, EventArgs e)
        {
            showLoadedTracesInGleeViewer();
        }

        private void rbGraphSelectedNode_CheckedChanged(object sender, EventArgs e)
        {
            showLoadedTracesInGleeViewer();
        }


        private void cbGLEE_MultiNodes_CheckedChanged(object sender, EventArgs e)
        {
            cbGLEE_ConsolidateTraces.Checked = cbGLEE_MultiNodes.Checked;
        }


        private void cbVisibleControls_GleeDrawingOptions_CheckedChanged(object sender, EventArgs e)
        {
            updateVisibleControlsColapseState();
        }


        private void cbVisibleControls_GraphStats_CheckedChanged(object sender, EventArgs e)
        {
            updateVisibleControlsColapseState();
        }

        private void cbVisibleControls_CustomFilters_CheckedChanged(object sender, EventArgs e)
        {
            updateVisibleControlsColapseState();
        }

        private void cbVisibleControls_TracesGraphed_CheckedChanged(object sender, EventArgs e)
        {
            updateVisibleControlsColapseState();
        }


        private void updateVisibleControlsColapseState()
        {
            if (cbVisibleControls_GleeDrawingOptions.Checked)
                scTopAndBottom.Panel2Collapsed = false;
            else
                scTopAndBottom.Panel2Collapsed = true;

            if (false == cbVisibleControls_TracesGraphed.Checked && false == cbVisibleControls_CustomFilters.Checked)
                scTopLeftAndRight.Panel1Collapsed = true;
            else
            {
                scTopLeftAndRight.Panel1Collapsed = false;
                if (cbVisibleControls_TracesGraphed.Checked)
                    scTracesAndScripts.Panel1Collapsed = false;
                else
                    scTracesAndScripts.Panel1Collapsed = true;
                if (cbVisibleControls_CustomFilters.Checked)
                    scTracesAndScripts.Panel2Collapsed = false;
                else
                    scTracesAndScripts.Panel2Collapsed = true;
            }
            if (false == cbVisibleControls_GraphStats.Checked && false == cbDrawGleeGraph.Checked)
            {
                scTopLeftAndRight.Panel2Collapsed = true;
            }
            else
            {
                scTopLeftAndRight.Panel2Collapsed = false;
                if (cbVisibleControls_GraphStats.Checked)
                    scGleeAndAnalysis.Panel2Collapsed = false;
                else
                    scGleeAndAnalysis.Panel2Collapsed = true;

                if (cbDrawGleeGraph.Checked)
                    scGleeAndAnalysis.Panel1Collapsed = false;
                else
                    scGleeAndAnalysis.Panel1Collapsed = true;
            }
        }

        private void btRefreshGraph_Click(object sender, EventArgs e)
        {
            showLoadedTracesInGleeViewer();
        }

        private void btClearGraph_Click(object sender, EventArgs e)
        {
            clearGraph();
        }

        public bool inMultiNodeMode()
        {
            return cbGLEE_MultiNodes.Checked;
        }

        private void cbGLEE_ConsolidateTraces_CheckedChanged(object sender, EventArgs e)
        {
            showLoadedTracesInGleeViewer();
        }

        private void cBoxShowFunctionClass_Depth_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 iValue = 0;
            if (false == Int32.TryParse(cBoxShowFunctionClass_Depth.Text, out iValue))
            {
                DI.log.debug("Value provided must be a number");
            }

            cBoxShowFunctionClass_Depth.Text = iValue.ToString();
        }

        private void cBoxShowFunctionClass_Depth_KeyPress(object sender, KeyPressEventArgs e)
        {
            /* if (e.KeyChar == (Char)Keys.Down)
            {
                Int32 iCurrentValue = Int32.Parse(cBoxShowFunctionClass_Depth.Text);
                if (iCurrentValue >0)
                    iCurrentValue--;

                cBoxShowFunctionClass_Depth.Text = iCurrentValue.ToString();
            }*/
        }

        private void cBoxShowFunctionClass_DataFromSameClass_KeyPress(object sender, KeyPressEventArgs e)
        {
        }


        private void cbOnlyShowDataFor_ConsolidateNonVisibleNodes_CheckedChanged(object sender, EventArgs e)
        {
            showLoadedTracesInGleeViewer();
        }

        private void cbOnlyShowDataFor_Class_CheckedChanged(object sender, EventArgs e)
        {
            if (cbOnlyShowDataFor_SourcesAndSinks.Checked == false)
                showLoadedTracesInGleeViewer();
            else
                cbOnlyShowDataFor_SourcesAndSinks.Checked = ! cbOnlyShowDataFor_Class.Checked;
        }

        private void cbOnlyShowDataFor_SourcesAndSinks_CheckedChanged(object sender, EventArgs e)
        {
            if (cbOnlyShowDataFor_Class.Checked == false)
                showLoadedTracesInGleeViewer();
            else
                cbOnlyShowDataFor_Class.Checked = !cbOnlyShowDataFor_SourcesAndSinks.Checked;
        }

        private void btAnimateTrace_Click(object sender, EventArgs e)
        {
        }

        private void btRemoveSelectedNode_Click(object sender, EventArgs e)
        {
            if (nPreviouslySelectedNode != null)
            {
                //String sTy = nPreviouslySelectedNode.GetType().Name;
                O2Graph.deleteNodeFromGraph(fgdGraphData, nPreviouslySelectedNode, true);
                loadGraph(fgdGraphData.gGraph);
                //Node nSelectedNode 
            }
        }

        private void gvSmartTrace_Click(object sender, EventArgs e)
        {
        }

        private void gvSmartTrace_MouseClick(object sender, MouseEventArgs e)
        {
            if (gvSmartTrace.SelectedObject != null && "Node" == gvSmartTrace.SelectedObject.GetType().Name)
            {
                gvSmartTrace.Graph = new Graph("temp graph");
                if (e.Button == MouseButtons.Right)
                    rbOnFunctionSelect_Clear.Checked = true; // automaticaly make it clear on right-click
                else if (e.Button == MouseButtons.Left && rbOnFunctionSelect_Clear.Checked)
                    rbOnFunctionSelect_Show.Checked = true;
                // automaticaly (when clear is selected) make it show on left-click
                var nSelectedNode = (Node) gvSmartTrace.SelectedObject;
                executeAction_onFunctionSelect(nSelectedNode);

                lbGLEE_SelectedNode.Text = nSelectedNode.Id;

                nSelectedNode.NodeAttribute.Fillcolor = Color.SlateGray;
                if (nPreviouslySelectedNode != nSelectedNode)
                {
                    if (nPreviouslySelectedNode != null)
                        nPreviouslySelectedNode.NodeAttribute.Fillcolor = Color.White;

                    nPreviouslySelectedNode = nSelectedNode;
                }

                loadGraph(fgdGraphData.gGraph);
            }
        }

        private void gvSmartTrace_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
        }

        private void gvSmartTrace_MouseDown(object sender, MouseEventArgs e)
        {
            // it is posible to move the selected shape
            /* if (gvSmartTrace.SelectedObject != null && "Node" == gvSmartTrace.SelectedObject.GetType().Name)
            {
                Node nSelectedNode = (Node)gvSmartTrace.SelectedObject;
                float iPosX, iPosY;
                nSelectedNode.Attr.Position(out iPosX, out iPosY);
                nSelectedNode.Attr.Pos = new Microsoft.Glee.Splines.Point(iPosX + 10, iPosY + 20);
            }*/
        }

        private void gvSmartTrace_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void gvSmartTrace_MouseMove_1(object sender, MouseEventArgs e)
        {
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
        }

        private void cbOnlyShowDataFor_LostSources_CheckedChanged(object sender, EventArgs e)
        {
            showLoadedTracesInGleeViewer();
        }


        public void executeAction_onFunctionSelect(Node nSelectedNode)
        {
            if (rbOnFunctionSelect_Show.Checked)
            {
                Int32 iNameSpaceDepth;
                Int32.TryParse(cBoxShowFunctionClass_Depth.Text, out iNameSpaceDepth);
                nSelectedNode.NodeAttribute.Label = FilteredSignature.filterSignature(nSelectedNode.Id,
                                                                                      cbGLEE_ShowParameters.Checked,
                                                                                      cbGLEE_ShowReturnClass.Checked,
                                                                                      cbGLEE_ShowNamespace.Checked,
                                                                                      iNameSpaceDepth);
                nSelectedNode.NodeAttribute.Shape = Shape.Box;
            }
            else if (rbOnFunctionSelect_Clear.Checked)
                O2Graph.makeEmptyNode(nSelectedNode);
            else if (rbOnFunctionSelect_Remove.Checked)
            {
                O2Graph.deleteNodeFromGraph(fgdGraphData, nSelectedNode, true);
            }
            else if (rbOnFunctionSelect_HighlightAllCalls_To.Checked)
            {
                applyFiltersToGraph(fgdGraphData.gGraph, true);
                highlighCallsToNode_Recursive(nSelectedNode);
                highlighCallsFromNode_Recursive(nSelectedNode);
            }
            else
            {
            }
            //else if (rbOnFunctionSelect_SClear.Checked)
            /*
             */
        }

        public void highlighCallsToNode_Recursive(Node nTargetNode)
        {
            if (fgdGraphData.dNodeIsCalledBy.ContainsKey(nTargetNode.Id))
                foreach (Node nNode in fgdGraphData.dNodeIsCalledBy[nTargetNode.Id])
                {
                    //  DI.log.debug("{0} is called by {1}", nTargetNode.Id, nNode.Id);
                    if (nNode != nTargetNode && nNode.NodeAttribute.Fillcolor != fgdGraphData.cSelectedFillColor)
                    {
                        nNode.NodeAttribute.Fillcolor = fgdGraphData.cSelectedFillColor;
                        highlighCallsToNode_Recursive(nNode);
                    }
                }
        }

        public void highlighCallsFromNode_Recursive(Node nTargetNode)
        {
            if (fgdGraphData.dNodeCalls.ContainsKey(nTargetNode.Id))
                foreach (Node nNode in fgdGraphData.dNodeCalls[nTargetNode.Id])
                {
                    //   DI.log.debug("{0} calls {1}", nTargetNode.Id, nNode.Id);
                    if (nNode != nTargetNode && nNode.NodeAttribute.Fillcolor != Color.LightGreen)
                    {
                        nNode.NodeAttribute.Fillcolor = Color.LightGreen;
                        highlighCallsFromNode_Recursive(nNode);
                    }
                }
        }

        private void btRemoveNodes_NotSelected_Click(object sender, EventArgs e)
        {
            gvSmartTrace.Graph = new Graph("temp graph");
            var lNodesToDelete = new List<Node>();
            foreach (Node nNode in fgdGraphData.dNodes.Values)
            {
                if (nNode.NodeAttribute.Fillcolor == Color.White)
                    lNodesToDelete.Add(nNode);
            }
            foreach (Node nNode in lNodesToDelete)
                O2Graph.deleteNodeFromGraph(fgdGraphData, nNode, true);
            loadGraph(fgdGraphData.gGraph);
        }

        private void btCreateCustomSavedAssessmentRunFile_Click(object sender, EventArgs e)
        {
        }

        private void cbDrawGleeGraph_CheckedChanged(object sender, EventArgs e)
        {
            updateVisibleControlsColapseState();
        }

        private void ascx_InvokeDynamicFilters1_Load(object sender, EventArgs e)
        {
        }

        private void gdmcrGraphDataMappedToCustomRules_Load(object sender, EventArgs e)
        {
        }

        private void tbMaxToPlot_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                    if (Int32.TryParse(tbMaxToPlot.Text, out fgdGraphData.iMaxToPlot))
                        DI.log.debug("GLEE Graph Max to Plot set to: {0}", fgdGraphData.iMaxToPlot);
            }
            catch (Exception ex)
            {
                DI.log.ex(ex);
                fgdGraphData.iMaxToPlot = 80;
                tbMaxToPlot.Text = fgdGraphData.iMaxToPlot.ToString();
            }
            
        }

        private void tvGLEE_NodesToGraph_DragEnter(object sender, DragEventArgs e)
        {
            Dnd.setEffect(e);
        }

        private void tvGLEE_NodesToGraph_DragDrop(object sender, DragEventArgs e)
        {
            //tvGLEE_NodesToGraph.invokeOnThread(
            //    () =>
            //        {
            return; // code is not working (we need to have full trace trees calculated  for each item dropped

/*            try
            {

                var droppedObject = Dnd.tryToGetObjectFromDroppedObject(e);
                if (droppedObject is TreeNode)
                {
                    tvGLEE_NodesToGraph.Nodes.Clear();
                    var droppedTreeNode = (TreeNode) droppedObject;
                    var tagObject = droppedTreeNode.Tag;
                    var typeName = tagObject.GetType();
                    if (tagObject is List<FindingViewItem>)
                    {
                        var nodesToAdd = new List<TreeNode>();
                        var itemsToView = (List<FindingViewItem>) tagObject;
                        foreach (FindingViewItem itemToView in itemsToView)

                            nodesToAdd.Add(O2Forms.newTreeNode(itemToView.sText, itemToView.sText, 0,
                                                               itemToView));
                        tvGLEE_NodesToGraph.Nodes.AddRange(nodesToAdd.ToArray());
                        showLoadedTracesInGleeViewer();
                        / *                 }
                                                       if (droppedTreeNode.Nodes.Count == 0)
                                                            tvGLEE_NodesToGraph.Nodes.Add(droppedTreeNode);
                                                        else
                                                        {
                                                            var nodesToAdd = new List<TreeNode>();
                                                            foreach (TreeNode treeNode in droppedTreeNode.Nodes)
                                                                nodesToAdd.Add((TreeNode)treeNode.Clone());
                                                            tvGLEE_NodesToGraph.Nodes.AddRange(nodesToAdd.ToArray());
                                                        }
                                                        showLoadedTracesInGleeViewer();
                                                    }* /
                    }
                }
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in tvGLEE_NodesToGraph_DragDrop");
            }
              //      });
            */
        }    

        /*
        public void filter_ShowAllTraces_removeDuplicates(ref Microsoft.Glee.Drawing.Graph gGraph)
        {
      //      foreach (Node nNode in gGraph.Nodes)
      //          nNode.Id = nNode.Id.Replace("Requ", "XXXXXXXXX");
            String sEdgeSeparator = "-->";
            Dictionary<String, Int32> dEdges = new Dictionary<string, int>();
            foreach (Edge eEdge in gGraph.Edges)
            {
                String sEdgeString = String.Format("{0}{1}{2}", eEdge.Source, sEdgeSeparator, eEdge.Target);
                if (false == dEdges.ContainsKey(sEdgeString))
                    dEdges.Add(sEdgeString, new Int32());
                dEdges[sEdgeString]++;
                
            }
             DI.log.debug("dEdges contains {0} items", dEdges);
            Graph gNewGraph = new Graph("Graph with no Duplicate edges");
            foreach(Node nNode in gGraph.Nodes)
            {
                Node nNewNode = (Node)gNewGraph.AddNode(nNode.Id);                
                nNewNode.UserData = nNode.UserData;
            }
            foreach (String sEdge in dEdges.Keys)
            {
                String[] sSplitedEdge = sEdge.Split(new String[] { sEdgeSeparator },StringSplitOptions.None);
                gNewGraph.AddEdge(sSplitedEdge[0], dEdges[sEdge].ToString(), sSplitedEdge[1]);
            }
            gGraph = gNewGraph;
            
        }
*/


        /*
         * 
         *       Object oCurrentObject;

        public ascx_GLEE()
        {
            InitializeComponent();
        }

        private void ascx_DropObject1_eDnDAction_ObjectDataReceived_Event(object oVar)
        {
            Graph g = (Graph)oVar;

            gViewer1.Graph = g;

            oCurrentObject = oVar;
            //makeExternalGleeViewHandlerCallBack(oVar);            
        }

        public void makeExternalGleeViewHandlerCallBack(Object oObjectToSend)
        { 
             // add a call back to external code to handle this type
            Object oGleeViewHandlers = vars.get("externalGleeViewHandler");
           // Object vars
            if (oGleeViewHandlers == null)
                 DI.log.error("Could not find object or class externalGleeViewHandler");
            else
            {
                Type tViewHandler = (Type)oGleeViewHandlers;

                MethodInfo mGetGraphForObject = tViewHandler.GetMethod("getGraphForObject");
                if (mGetGraphForObject == null)
                     DI.log.error("Method externalGleeViewHandler.getGraphForObject does not exist");
                {
                    Object oResponse = mGetGraphForObject.Invoke(oGleeViewHandlers, new Object[] { oObjectToSend });

                    if (oResponse == null)
                         DI.log.error("No data returned from externalGleeViewHandler.getGraphForObject");
                    else
                        if (oResponse.GetType().Name != "Graph")
                             DI.log.error("Object returned was not a Graph");
                        else
                            gViewer1.Graph = (Graph)oResponse;
                }
            }
        }

        public void loadObjectDataInGraph(GViewer gvTargetGViewer, Object oObjectToLoad)
        {
            if(oObjectToLoad != null)
                gvTargetGViewer.Graph = (Graph)oObjectToLoad;
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            loadObjectDataInGraph(gViewer1, oCurrentObject);
        }*/
    }
}
