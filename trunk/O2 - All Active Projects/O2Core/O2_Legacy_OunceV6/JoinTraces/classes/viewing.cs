using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Kernel.Interfaces.O2Findings;
using O2.Legacy.OunceV6.SavedAssessmentFile.classes;

namespace O2.Legacy.OunceV6.JoinTraces.classes
{
    internal class viewing
    {
        public static TreeNode getRootNodeToView(String sRootFunction, String sMode, TreeView tvRawData)
        {            

            var otbO2TraceBlockOfRootNode = (O2TraceBlock_OunceV6) tvRawData.Nodes[sRootFunction].Tag;

            return getRootNodeToView(otbO2TraceBlockOfRootNode, sMode);
        }

        public static TreeNode getRootNodeToView(O2TraceBlock_OunceV6 otbO2TraceBlockOfRootNode, String sMode)
        {
            //var tnTreeNode = new TreeNode(sRootFunction);
            var tnTreeNode = new TreeNode(otbO2TraceBlockOfRootNode.sUniqueName);
            if (sMode == "Sinks")
            {
                Dictionary<AssessmentAssessmentFileFinding, O2AssessmentData_OunceV6> dSinks = otbO2TraceBlockOfRootNode.dSinks;
                foreach (AssessmentAssessmentFileFinding fFinding in dSinks.Keys)
                {
                    var fviFindingViewItem = new FindingViewItem(fFinding, fFinding.vuln_name, null, dSinks[fFinding]);
                    String sNodeText = analyzer.getUniqueSignature(fFinding, TraceType.Known_Sink,
                                                                   dSinks[fFinding], true);
                    if (sNodeText != null)
                    {
                        TreeNode tnChildNode = O2Forms.newTreeNode(sNodeText, sNodeText, 0, fviFindingViewItem);
                        tnTreeNode.Nodes.Add(tnChildNode);
                    }
                }
            }
            return tnTreeNode;
        }


        public static void ProcessNormalizedTracesAndDisplayResults(TreeView tvProcessedNormalizedTraces,
                                                                    TreeView tvNormalizedTracesView)
        {
            tvProcessedNormalizedTraces.Nodes.Clear();
            var normalizedTracesView = new List<TreeNode>();
            foreach (TreeNode node in tvNormalizedTracesView.Nodes)
                normalizedTracesView.Add(node);
 
            // calculate all unique traces
            List<TreeNode> ltnTraces = analyzer.getListOfNormalizedTraces(normalizedTracesView);


            // and show them indexed by Source and by Sink
            // calculate Sources
            var tnSources = new TreeNode("Sources");
            foreach (TreeNode tnTrace in ltnTraces)
            {
                String sFunction = tnTrace.Text;
                if (sFunction != "")
                {
                    if (tnSources.Nodes[sFunction] == null)
                    {
                        TreeNode tnSourceNode = O2Forms.newTreeNode(sFunction, sFunction, 0, null);
                        tnSourceNode.ForeColor = Color.DarkRed; // Source
                        tnSources.Nodes.Add(tnSourceNode);
                    }
                    if (tnTrace.Nodes.Count > 0)
                        tnSources.Nodes[sFunction].Nodes.Add((TreeNode) tnTrace.Nodes[0].Clone());
                    else
                        tnSources.Nodes[sFunction].Nodes.Add((TreeNode) tnTrace.Clone());
                }
            }
            tnSources.Expand();
            tvProcessedNormalizedTraces.Nodes.Add(tnSources);
            // calculate Sinks
            var tnSinks = new TreeNode("Sinks");
            foreach (TreeNode tnTrace in ltnTraces)
            {
                String sFunction = getChildNode_AlwaysFollowingFirstChild(tnTrace).Text;
                if (sFunction != "")
                {
                    if (tnSinks.Nodes[sFunction] == null)
                    {
                        TreeNode tnSourceNode = O2Forms.newTreeNode(sFunction, sFunction, 0, null);
                        tnSourceNode.ForeColor = Color.Red; // Source
                        tnSinks.Nodes.Add(tnSourceNode);
                    }
                    //if (tnTrace.Nodes.Count > 0)
                    //    tnSinks.Nodes[sFunction].Nodes.Add((TreeNode)tnTrace.Nodes[0].Clone());
                    //else
                    tnSinks.Nodes[sFunction].Nodes.Add((TreeNode) tnTrace.Clone());
                }
            }
            tnSinks.Expand();
            tvProcessedNormalizedTraces.Nodes.Add(tnSinks);

            //tvProcessedNormalizedTraces.Nodes.AddRange(ltnTraces.ToArray());
            //tvProcessedNormalizedTraces.ExpandAll();
        }


        public static List<TreeNode> FromTreeViewCalculateViewForSpecificRootNode(String sRootNodeToTrace,
                                                                                  TreeView tvTracesData, String sKeyword,
                                                                                  TreeView tvRawData)
        {
            O2Timer tTimer = new O2Timer("Calculating Data TreeView for " + sKeyword).start();
            //tvTargetTreeView.Nodes.Clear();

            var ltnCalculatedTreeNodes = new List<TreeNode>();
            var lsFunctionsInTrace = new List<string>();
            TreeNode tnNewTreeNode = O2Forms.newTreeNode(tvTracesData.Nodes[sRootNodeToTrace].Text,
                                                         tvTracesData.Nodes[sRootNodeToTrace].Text, 0,
                                                         tvTracesData.Nodes[sRootNodeToTrace].Tag);
            addNodesToView_recursive(tnNewTreeNode, tvTracesData.Nodes[sRootNodeToTrace], tvTracesData, sKeyword,
                                     lsFunctionsInTrace, tvRawData);
            if (tnNewTreeNode != null)
                foreach (TreeNode tnTreeNode in tnNewTreeNode.Nodes)
                    ltnCalculatedTreeNodes.Add((TreeNode) tnTreeNode.Clone());
            tTimer.stop();
            if (tnNewTreeNode != null)
                DI.log.info("Trace with {0}Data Calculated: It has {1} ({2}) Nodes (SubNodes)", sKeyword,
                            tnNewTreeNode.GetNodeCount(false), tnNewTreeNode.GetNodeCount(true));
            return ltnCalculatedTreeNodes;
        }

        public static bool addNodesToView_recursive(TreeNode tnTreeNodeToAddTraces, TreeNode tnTreeNodeWithTracesToAdd,
                                                    TreeView tvTracesData, String sKeyword,
                                                    List<String> lsFunctionsInTrace, TreeView tvRawData)
        {
            if (tnTreeNodeWithTracesToAdd == null)
                return false;
            //TreeNode tnNewNode = forms.O2Forms.newTreeNode(tnTreeNodeToAdd.Text, tnTreeNodeToAdd.Text, 0, tnTreeNodeToAdd.Tag);            

            /*List<String> lsTracesFromSameSequence =
                GetTracesThatAreFromTheSameTraceSequence(
                    (O2TraceBlock_OunceV6) tvTracesData.Nodes[tnTreeNodeToAddTraces.Text].Tag,
                    (O2TraceBlock_OunceV6) tvTracesData.Nodes[tnTreeNodeWithTracesToAdd.Text].Tag, sKeyword);*/
            return false;

            /*foreach (TreeNode tnChildNode in tnTreeNodeWithTracesToAdd.Nodes[sKeyword].Nodes)
            {
                var stStackTrace = new StackTrace();
                if (stStackTrace.FrameCount > 50)
                {
                    String sMsg = String.Format("INFO: Max StackTrace reached, aborting this leaf:{0}", tnChildNode.Text);
                    DI.log.info(sMsg);
                    return false;
                }
                else if (tnChildNode.Text != "" && tnChildNode.Text != tnTreeNodeToAddTraces.Text)
                {
                    tnTreeNodeToAddTraces.Nodes.Add(O2Forms.newTreeNode(tnChildNode.Text, tnChildNode.Text, 0,
                                                                        tvTracesData.Nodes[tnChildNode.Text].Tag));
                }
            }
            foreach (TreeNode tnChildNode in tnTreeNodeToAddTraces.Nodes)
            {
                if (false ==
                    addNodesToView_recursive(tnChildNode, tvTracesData.Nodes[tnChildNode.Text], tvRawData, sKeyword,
                                             lsFunctionsInTrace, tvRawData))
                    return false;
            }
            return true;*/
        }

        public static TreeNode getChildNode_AlwaysFollowingFirstChild(TreeNode tnTreeNode)
        {
            if (tnTreeNode.Parent != null && tnTreeNode.Nodes.Count > 0)
                return getChildNode_AlwaysFollowingFirstChild(tnTreeNode.Nodes[0]);
            return tnTreeNode;
        }

        public static List<String> GetTracesThatAreFromTheSameTraceSequence(O2TraceBlock_OunceV6 otbO2TraceBlockOunceV6Root,
                                                                            O2TraceBlock_OunceV6 otbO2TraceBlockOunceV6Child,
                                                                            String sKeyword)
        {
            var lsTracesFromSameSequence = new List<string>();
            //      TreeNode tnTreeForTrace_Root = new TreeNode();
            //      o2.analysis.Analysis.SmartTraceFilter stfSmartTraceFilter = o2.analysis.Analysis.SmartTraceFilter.MethodName;
            //            o2.analysis.Analysis.addCallsToNode_Recursive(otbO2TraceBlockOunceV6Root, tnTreeForTrace_Root, fviFindingViewItem.oadO2AssessmentDataOunceV6, stfSmartTraceFilter);
            return lsTracesFromSameSequence;
        }
    }
}