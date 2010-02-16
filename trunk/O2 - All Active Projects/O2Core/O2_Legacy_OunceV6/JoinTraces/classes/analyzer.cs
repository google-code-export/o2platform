// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Interfaces.CIR;
using O2.Interfaces.O2Findings;
using O2.Legacy.OunceV6.JoinTraces.classes.filters;
using O2.Legacy.OunceV6.SavedAssessmentFile.classes;

namespace O2.Legacy.OunceV6.JoinTraces.classes
{
    internal class analyzer
    {
        public static void proccessLoadedFilesIntoO2TraceBlockDictionary(Dictionary<String, O2TraceBlock_OunceV6> dO2TraceBlock,
                                                                         List<O2AssessmentData_OunceV6> lO2AssessmentData,
                                                                         bool bMakeAllLostSinksIntoKnownSinks)
        {
            foreach (O2AssessmentData_OunceV6 oadO2AssessmentData in lO2AssessmentData)
            {
                if (bMakeAllLostSinksIntoKnownSinks)
                    onSavedAssessmentData.MakeAllLostSinksIntoKnownSinks(oadO2AssessmentData);
                foreach (AssessmentAssessmentFileFinding fFinding in oadO2AssessmentData.dFindings.Keys)
                {
                    O2TraceBlock_OunceV6 tbToGlueSource = getTraceBlockToGlueFinding(fFinding, TraceType.Source,
                                                                                     oadO2AssessmentData, dO2TraceBlock);
                    if (tbToGlueSource != null)
                        tbToGlueSource.dSinks.Add(fFinding, oadO2AssessmentData);

                    O2TraceBlock_OunceV6 tbToGlueSink = getTraceBlockToGlueFinding(fFinding, TraceType.Known_Sink,
                                                                                   oadO2AssessmentData, dO2TraceBlock);
                    if (tbToGlueSink != null)
                        tbToGlueSink.dSources.Add(fFinding, oadO2AssessmentData);
                }
            }
        }

        public static bool doesRawTraceHasSinks(TreeView tvRawData, String sSignatureToFind)
        {
            TreeNode tnTraceNode = tvRawData.Nodes[sSignatureToFind];
            if (tnTraceNode != null)
            {
                TreeNode sSinksNode = tnTraceNode.Nodes["Sinks"];
                if (sSinksNode != null && sSinksNode.Nodes.Count > 0)
                    return true;
            }
            return false;
        }

        public static bool doesRawTraceHasSources(TreeView tvRawData, String sSignatureToFind)
        {
            TreeNode tnTraceNode = tvRawData.Nodes[sSignatureToFind];
            if (tnTraceNode != null)
            {
                TreeNode sSourcesNode = tnTraceNode.Nodes["Sources"];
                if (sSourcesNode != null && sSourcesNode.Nodes.Count > 0)
                    return true;
            }
            return false;
        }

        public static List<TreeNode> ResolveNormalizeTraceFor(String sTextToSearch, Dictionary<String, O2TraceBlock_OunceV6> dRawData,//TreeView tvRawData,
                                                    ICirData cdCirData, Dictionary<String, O2TraceBlock_OunceV6> dO2TraceBlock,
                                                    bool bOnlyProcessTracesWithNoCallers)
        {
            DI.log.info("in ResolveNormalizeTraceFor");
            //var otbO2TraceBlockOfRootNode = (O2TraceBlock_OunceV6)tvRawData.Nodes[sRootFunction].Tag;
            var traceBlocksToProcess = new List<O2TraceBlock_OunceV6>();
            var lsTargetRootFunctions = new List<string>();            
            //foreach (TreeNode TnTreeNode in tvRawData.Nodes)
            foreach (var o2TraceBlock in dRawData.Values)
                if (sTextToSearch == "" || RegEx.findStringInString(o2TraceBlock.sUniqueName, sTextToSearch)) //  RegEx.findStringInString(TnTreeNode.Text, sTextToSearch))
                {
                    //traceBlocksToProcess.Add((O2TraceBlock_OunceV6)TnTreeNode.Tag);
                    traceBlocksToProcess.Add(o2TraceBlock);
                   // lsTargetRootFunctions.Add(TnTreeNode.Text);
                }

            DI.log.debug("Resolving Normalized traces for {0} root functions (which match {1})",
                         lsTargetRootFunctions.Count, sTextToSearch);

            //tvNormalizedTracesView.Nodes.Clear();            
            var normalizedTracesView = new List<TreeNode>();
            //foreach (String sFunction in lsTargetRootFunctions)
            foreach (var traceBlock in traceBlocksToProcess)
            {
                if (bOnlyProcessTracesWithNoCallers == false || false == traceBlock.dSources.Count > 0)
                {
                    TreeNode tnSinksRootNode = viewing.getRootNodeToView(traceBlock, "Sinks");//, tvRawData);
                    normalizedTracesView.Add(tnSinksRootNode);
                    //tvNormalizedTracesView.Nodes.Add(tnSinksRootNode);
                }

                /*if (bOnlyProcessTracesWithNoCallers == false ||  false == doesRawTraceHasSources(tvRawData, sFunction))
                {
                    TreeNode tnSinksRootNode = viewing.getRootNodeToView(sFunction, "Sinks", tvRawData);
                    normalizedTracesView.Add(tnSinksRootNode);
                    //tvNormalizedTracesView.Nodes.Add(tnSinksRootNode);
                }*/
                //if ( (nodesProcessed++) % 1000 == 0)
                //DI.log.info("Sinks Nodes Mapped [{0}/{1}", nodesProcessed, nodesToProcess);
                //tnRootNode.Expand();                
            }
            
            DI.log.debug("There are {0} Sinks to process", normalizedTracesView.Count);
            //DI.log.debug("There are {0} Sinks to process", tvNormalizedTracesView.GetNodeCount(true));
         //   Processes.Sleep(5000);
            // recursively glue traces we have
            //var nodesProcessed = 0;
            var nodesToProcess = lsTargetRootFunctions.Count;
            int iItemsProcessed = 0;
            //foreach (TreeNode tnRootNode in tvNormalizedTracesView.Nodes)
            foreach (TreeNode tnRootNode in normalizedTracesView)
                foreach (TreeNode tnChildNode in tnRootNode.Nodes)
                {
                    if (iItemsProcessed++%2500 == 0)
                        DI.log.debug(" Items Processed :{0}", iItemsProcessed);
                    if (tnChildNode.Text != "")
                    {
                        var fviFindingViewItem = (FindingViewItem) tnChildNode.Tag;
                        //if (tvRawData.Nodes[tnChildNode.Text] != null)
                        if (dRawData.ContainsKey(tnChildNode.Text))
                        {
                            var otbO2TraceBlockOfChildNode = dRawData[tnChildNode.Text];
                                //(O2TraceBlock_OunceV6) tvRawData.Nodes[tnChildNode.Text].Tag;

                            // need to check the side effecs of breaking the trace when addCompatibleTracesToNode_recursive is false (since that means this trace had to many child nodes
                            if (false ==
                                addCompatibleTracesToNode_recursive(tnChildNode, fviFindingViewItem,
                                                                    otbO2TraceBlockOfChildNode, "Sinks", dRawData)) // tvRawData))
                            {
                                // do nothing 
                            }
                        }
                        //break;                        
                    }
                }
            /*if (tvNormalizedTracesView.Nodes.Count > 0)
            {
                tvNormalizedTracesView.ExpandAll();
                tvNormalizedTracesView.SelectedNode = tvNormalizedTracesView.Nodes[0]; 
            }*/
            return normalizedTracesView;
        }

        public static List<O2TraceBlock_OunceV6> getO2TraceBlocksThatMatchSignature(string sSignatureToFind, TreeView tvRawData)
        {
            var lotdMatches = new List<O2TraceBlock_OunceV6>();
            foreach (TreeNode tnTreeNode in tvRawData.Nodes)
            {
                var otbO2TraceBlock = (O2TraceBlock_OunceV6) tnTreeNode.Tag;
                if (otbO2TraceBlock.sSignature == sSignatureToFind)
                    lotdMatches.Add(otbO2TraceBlock);
            }
            return lotdMatches;
        }

        public static List<O2TraceBlock_OunceV6> getO2TraceBlocksThatMatchSignature(string sSignatureToFind,
                                                                                    Dictionary<String, O2TraceBlock_OunceV6>
                                                                                        dO2TraceBlock)
        {
            var lotdMatches = new List<O2TraceBlock_OunceV6>();
            foreach (O2TraceBlock_OunceV6 otbO2TraceBlock in dO2TraceBlock.Values)
            {
                if (otbO2TraceBlock.sSignature == sSignatureToFind)
                    lotdMatches.Add(otbO2TraceBlock);
            }
            return lotdMatches;
        }

        public static void CreateUniqueTracesFromTreeView(TreeNode tnTreeNodeToProcess, List<TreeNode> ltnTraces)
        {
            if (tnTreeNodeToProcess == null)
                return;
            if (tnTreeNodeToProcess.Nodes.Count == 0)
            {
                var tnTrace = new TreeNode();
                tnTrace.ForeColor = Color.Red; // Sink
                //  DI.log.debug("Starting trace for: {0}", tnTreeNodeToProcess.Text);
                ltnTraces.Add(CreateTraceFromChildToParent_recursive(tnTreeNodeToProcess, tnTrace));
            }
            foreach (TreeNode tnChildNode in tnTreeNodeToProcess.Nodes)
                CreateUniqueTracesFromTreeView(tnChildNode, ltnTraces);
        }

        public static TreeNode CreateTraceFromChildToParent_recursive(TreeNode tnTreeNodeToProcess, TreeNode tnNewTrace)
        {
            if (tnTreeNodeToProcess == null)
                if (tnNewTrace.Nodes.Count > 0)
                {
                    tnNewTrace.Nodes[0].ForeColor = Color.DarkRed; // Source
                    return tnNewTrace.Nodes[0];
                }
                else
                    return tnNewTrace;
            tnNewTrace.Text = tnTreeNodeToProcess.Text.Split(' ')[0];
            tnNewTrace.Tag = tnTreeNodeToProcess.Tag;
            tnNewTrace.ForeColor = tnTreeNodeToProcess.ForeColor;
            var tnNewTraceParent = new TreeNode();
            //tnNewTraceParent.Text = tnTreeNodeToProcess.Text;
            tnNewTraceParent.Nodes.Add(tnNewTrace);
            /* DC tnNewTraceParent.Nodes.Add(tnNewTrace); // this code create a race condition and hangs on the 2nd call            
            tnNewTrace.Nodes.Add(tnNewTraceParent); */
            return CreateTraceFromChildToParent_recursive(tnTreeNodeToProcess.Parent, tnNewTraceParent);
        }


        public static void howManyTimesTextExistInList(List<String> lsListToProcess, String sText,
                                                       ref Int32 iNumberOfOccurences)
        {
            foreach (String sItem in lsListToProcess)
                if (sItem == sText)
                    iNumberOfOccurences++;
        }

        public static void calculateO2TraceBlocksIntoTreeView(Dictionary<String, O2TraceBlock_OunceV6> dO2TraceBlock,
                                                              ref TreeView tvTargetTreeView)
        {
            
            DI.log.info("incalculateO2TraceBlocksIntoTreeView: {0}", dO2TraceBlock.Keys.Count);
            O2Timer tTimer = new O2Timer("Calculating Raw Data TreeView").start();
            tvTargetTreeView = new TreeView();
            var itemsProcessed = 0;
            var itemsToProcess = dO2TraceBlock.Keys.Count;
            var updateInterval = 1000;
            foreach (var sSignature in dO2TraceBlock.Keys)
            {       
                if (itemsProcessed++ % updateInterval == 0)
                    DI.log.info("[{0}/{1}]", itemsProcessed, itemsToProcess);
                O2TraceBlock_OunceV6 otdO2TraceBlockOunceV6 = dO2TraceBlock[sSignature];
                //String sRootNodeText = String.Format("{0} {1} {2}", otdO2TraceBlockOunceV6.sSignature, System.IO.Path.GetFileName(otdO2TraceBlockOunceV6.sFile),otdO2TraceBlockOunceV6.sLineNumber);
                String sRootNodeText = String.Format("{0}      {1}      {2}", otdO2TraceBlockOunceV6.sSignature,
                                                     otdO2TraceBlockOunceV6.sFile, otdO2TraceBlockOunceV6.sLineNumber);
                TreeNode tnRootNode = O2Forms.newTreeNode(sRootNodeText, sRootNodeText, 0, otdO2TraceBlockOunceV6);
                // handle Sinks
                TreeNode tnSinks = O2Forms.newTreeNode("Sinks", "Sinks", 0, null);
                foreach (AssessmentAssessmentFileFinding fFinding in otdO2TraceBlockOunceV6.dSinks.Keys)
                {
                    String sSink = getUniqueSignature(fFinding, TraceType.Known_Sink,
                                                      otdO2TraceBlockOunceV6.dSinks[fFinding], true);
                    // analysis.Analysis.getSink(fFinding, otdO2TraceBlockOunceV6.dSinks[fFinding]);
                    if (!string.IsNullOrEmpty(sSink) || sSink != sRootNodeText)
                    {
                        TreeNode tnSink = O2Forms.newTreeNode(sSink, sSink, 0, null);
                        tnSinks.Nodes.Add(tnSink);
                    }
                }
                tnRootNode.Nodes.Add(tnSinks);
                TreeNode tnSources = O2Forms.newTreeNode("Sources", "Sources", 0, null);
                foreach (AssessmentAssessmentFileFinding fFinding in otdO2TraceBlockOunceV6.dSources.Keys)
                {
                    String sSource = getUniqueSignature(fFinding, TraceType.Source,
                                                        otdO2TraceBlockOunceV6.dSources[fFinding], true);
                    if (sSource == "" || sSource == null || sSource != sRootNodeText)
                    {
                        TreeNode tnSource = O2Forms.newTreeNode(sSource, sSource, 0, null);
                        tnSources.Nodes.Add(tnSource);
                    }
                }
                tnRootNode.Nodes.Add(tnSources);
                tvTargetTreeView.Nodes.Add(tnRootNode);
            }
            DI.log.info("[{0}/{1}]", itemsProcessed, itemsToProcess);
            //tvTargetTreeView.Sort();                                  // removed for performance reasons
            //tvAllTraces.Nodes.AddRange(tvRawData.Nodes);

            //tvAllTraces = tvRawData;
            tTimer.stop();
            DI.log.info("Tree View with Raw Data Calculated: It has {0} ({1}) Nodes (SubNodes)",
                        tvTargetTreeView.GetNodeCount(false), tvTargetTreeView.GetNodeCount(true));
        }

        public static O2TraceBlock_OunceV6 getTraceBlockToGlueFinding(AssessmentAssessmentFileFinding fFinding,
                                                                      TraceType ttTraceType,
                                                                      O2AssessmentData_OunceV6 oadO2AssessmentDataOunceV6,
                                                                      Dictionary<String, O2TraceBlock_OunceV6> dO2TraceBlock)
        {
            CallInvocation ciCallInvocation =
                AnalysisSearch.findTraceTypeInSmartTrace_Recursive_returnCallInvocation(fFinding.Trace, ttTraceType);
            if (ciCallInvocation == null)
                return null;
            String sSourceSignature = OzasmtUtils_OunceV6.getStringIndexValue(ciCallInvocation.sig_id, oadO2AssessmentDataOunceV6);
            String sFile = OzasmtUtils_OunceV6.getFileIndexValue(ciCallInvocation.fn_id, oadO2AssessmentDataOunceV6);
            String sLineNumber = ciCallInvocation.line_number.ToString();
            String sTraceRootText = OzasmtUtils_OunceV6.getStringIndexValue(fFinding.Trace[0].sig_id, oadO2AssessmentDataOunceV6);
            String sUniqueName = String.Format("{0}      {1}      {2}", sSourceSignature, sFile, sLineNumber);
            // need to find a better way to clue the final sinks since at the moment I am getting a couple sinks trown by the cases when a sink also become a source of tainted data
            //String sUniqueName = String.Format("{0} {1} {2} {3}", sSourceSignature, sFile, sLineNumber, sTraceRootText);

            if (false == dO2TraceBlock.ContainsKey(sUniqueName))
            {
                dO2TraceBlock.Add(sUniqueName, new O2TraceBlock_OunceV6());
                dO2TraceBlock[sUniqueName].sSignature = sSourceSignature;
                dO2TraceBlock[sUniqueName].sFile = sFile;
                dO2TraceBlock[sUniqueName].sLineNumber = sLineNumber;
                dO2TraceBlock[sUniqueName].sTraceRootText = sTraceRootText;
                dO2TraceBlock[sUniqueName].sUniqueName = sUniqueName;
            }
            return dO2TraceBlock[sUniqueName];
        }

        public static String getUniqueSignature(AssessmentAssessmentFileFinding fFinding, TraceType ttTraceType,
                                                O2AssessmentData_OunceV6 oadO2AssessmentDataOunceV6, bool bShowFullPathForFileName)
        {
            CallInvocation ciCallInvocation =
                AnalysisSearch.findTraceTypeInSmartTrace_Recursive_returnCallInvocation(fFinding.Trace, ttTraceType);
            if (ciCallInvocation == null)
                return null;
            String sSourceSignature = OzasmtUtils_OunceV6.getStringIndexValue(ciCallInvocation.sig_id, oadO2AssessmentDataOunceV6);
            String sFile = OzasmtUtils_OunceV6.getFileIndexValue(ciCallInvocation.fn_id, oadO2AssessmentDataOunceV6);
            String sLineNumber = ciCallInvocation.line_number.ToString();
            if (bShowFullPathForFileName)
                return String.Format("{0}      {1}      {2}", sSourceSignature, sFile, sLineNumber);
            else
                return String.Format("{0}      {1}      {2}", sSourceSignature, Path.GetFileName(sFile), sLineNumber);
        }

        public static bool addCompatibleTracesToNode_recursive(TreeNode tnTargetNode, FindingViewItem fviFindingViewItem,
                                                               O2TraceBlock_OunceV6 otbO2TraceBlockOunceV6OfToProcess,
                                                               String sMode,
                                                               Dictionary<String, O2TraceBlock_OunceV6> dRawData)
                                                               //TreeView tvRawData)
        {
            //TreeNode tnParentNode = O2Forms.getRootNode(tnTargetNode);
            //int iNumberOfNodes = tnParentNode.GetNodeCount(true);
            var iNumberOfNodes = O2Forms.getRootNode(tnTargetNode).GetNodeCount(true);

            if (O2Forms.getRootNode(tnTargetNode).GetNodeCount(true) > 1500)
            {
                DI.log.info(String.Format("Max number of subnodes reached (250), aborting this root node: {0}",
                                          O2Forms.getRootNode(tnTargetNode).Text));
                return false;
            }
            if (new StackTrace().FrameCount > 50)
            {
                DI.log.info(String.Format("Max StackTrace reached (50), aborting this leaf:{0}", tnTargetNode.Text));
                return false;
            }

            var tnTreeFor_Root = new TreeNode();
            AnalysisUtils.addCallsToNode_Recursive(fviFindingViewItem.fFinding.Trace, tnTreeFor_Root,
                                                   fviFindingViewItem.oadO2AssessmentDataOunceV6,
                                                   Analysis.SmartTraceFilter.MethodName);
            if (sMode == "Sinks")
            {
                // first add the normal sinks
                foreach (AssessmentAssessmentFileFinding fFinding in otbO2TraceBlockOunceV6OfToProcess.dSinks.Keys)
                {
                    var tnTreeFor_ChildTrace = new TreeNode();
                    var fviFindingViewItemForChildTrace = new FindingViewItem(fFinding,
                                                                              fFinding.vuln_name ?? OzasmtUtils_OunceV6.getStringIndexValue(
                                                                                                        UInt32.Parse(
                                                                                                            fFinding.vuln_name_id),
                                                                                                        otbO2TraceBlockOunceV6OfToProcess.
                                                                                                            dSinks[fFinding]), null,
                                                                              otbO2TraceBlockOunceV6OfToProcess.dSinks[fFinding
                                                                                  ]);
                    AnalysisUtils.addCallsToNode_Recursive(fviFindingViewItemForChildTrace.fFinding.Trace,
                                                           tnTreeFor_ChildTrace,
                                                           fviFindingViewItemForChildTrace.oadO2AssessmentDataOunceV6,
                                                           Analysis.SmartTraceFilter.MethodName);

                    TreeNode tnRootNode_Sink = getTreeNodeOfTraceType_recursive(tnTreeFor_Root,
                                                                                TraceType.Known_Sink);
                    TreeNode tnRootNode_Source = getTreeNodeOfTraceType_recursive(tnTreeFor_ChildTrace,
                                                                                  TraceType.Source);


                    if (AreNodesCompatible(tnRootNode_Sink, tnRootNode_Source))
                    {
                        String sNodeText = getUniqueSignature(fFinding, TraceType.Known_Sink,
                                                              otbO2TraceBlockOunceV6OfToProcess.dSinks[fFinding], true);

                        // ensures we don't add the same source more that once per line (needs to be more optimized
                        List<String> ltnAllNodesAddedSofar = O2Forms.getStringListWithAllParentNodesText(tnTargetNode);
                        if (false == ltnAllNodesAddedSofar.Contains(sNodeText))
                        {
                            if (sNodeText != tnTargetNode.Text) // don't add if the child call is the same as the parent
                                tnTargetNode.Nodes.Add(O2Forms.newTreeNode(sNodeText, sNodeText, 0,
                                                                           fviFindingViewItemForChildTrace));
                            if (sNodeText == null)
                            {
                                return false;
                            }
                        }
                    }
                }
                // then add the Glued Sinks

                foreach (AssessmentAssessmentFileFinding fFinding in otbO2TraceBlockOunceV6OfToProcess.dGluedSinks.Keys)
                {
                    var fviFindingViewItemForChildTrace = new FindingViewItem(fFinding,
                                                                              fFinding.vuln_name ?? OzasmtUtils_OunceV6.getStringIndexValue(
                                                                                                        UInt32.Parse(
                                                                                                            fFinding.vuln_name_id),
                                                                                                        otbO2TraceBlockOunceV6OfToProcess.
                                                                                                            dGluedSinks[fFinding]), null,
                                                                              otbO2TraceBlockOunceV6OfToProcess.dGluedSinks[
                                                                                  fFinding]);
                    String sNodeText = getUniqueSignature(fFinding, TraceType.Known_Sink,
                                                          otbO2TraceBlockOunceV6OfToProcess.dGluedSinks[fFinding], true);
                    tnTargetNode.Nodes.Add(O2Forms.newTreeNode(sNodeText, sNodeText, 0, fviFindingViewItemForChildTrace));
                }
            }
            foreach (TreeNode tnChildNode in tnTargetNode.Nodes)
            {
                //   int iNodeCount = tnChildNode.GetNodeCount(true);
                //    DI.log.info(iNodeCount + "   " + tnChildNode.Text);
                //if (tvRawData.Nodes[tnChildNode.Text] != null)
                if (dRawData.ContainsKey(tnChildNode.Text))
                    // (now back to false) was true (check side effects)
                    if (false ==
                        addCompatibleTracesToNode_recursive(tnChildNode, (FindingViewItem) tnChildNode.Tag,
                                            dRawData[tnChildNode.Text],
                                            //                (O2TraceBlock_OunceV6) tvRawData.Nodes[tnChildNode.Text].Tag,
                                                            "Sinks", dRawData))
                                                            //tvRawData))
                        return false;
            }
            return true;
        }

        public static bool AreNodesCompatible(TreeNode tnMasterTrace, TreeNode tnSubTrace)
            //the parents of the SubTrace must exacly match the MasterTrace
        {
            if ((tnMasterTrace == null || tnMasterTrace.Tag == null || tnMasterTrace.Text == null) &&
                (tnSubTrace == null || tnSubTrace.Tag == null || tnSubTrace.Text == ""))
                // both traces reached the top. Parent's Traces are equal                
                return true;
            if ((tnMasterTrace != null && tnMasterTrace.Tag != null && tnMasterTrace.Text != null) &&
                (tnSubTrace == null || tnSubTrace.Tag == null || tnSubTrace.Text == ""))
                // SubTrace reached the top. Subtrace Parent' trace is part ofMasterTrace one                                                                            
                return true;
            if (tnMasterTrace.Text != tnSubTrace.Text) // or they are not equal
                return false;
            return AreNodesCompatible(tnMasterTrace.Parent, tnSubTrace.Parent);
        }

        public static TreeNode getTreeNodeOfTraceType_recursive(TreeNode tnTreeNodeToSearch,
                                                                TraceType ttTraceType)
        {
            if (tnTreeNodeToSearch == null)
                return null;
            switch (ttTraceType)
            {
                case TraceType.Root_Call:
                    if (tnTreeNodeToSearch.ForeColor == Color.DarkBlue)
                        return tnTreeNodeToSearch;
                    break;
                case TraceType.Lost_Sink:
                    if (tnTreeNodeToSearch.ForeColor == Color.DarkOrange)
                        return tnTreeNodeToSearch;
                    break;
                case TraceType.Source:
                    if (tnTreeNodeToSearch.ForeColor == Color.DarkRed)
                        return tnTreeNodeToSearch;
                    break;
                case TraceType.Known_Sink:
                    if (tnTreeNodeToSearch.ForeColor == Color.Red)
                        return tnTreeNodeToSearch;
                    break;
                case TraceType.Type_4:
                    if (tnTreeNodeToSearch.ForeColor == Color.Green)
                        return tnTreeNodeToSearch;
                    break;
            }
            foreach (TreeNode tnChildNode in tnTreeNodeToSearch.Nodes)
            {
                TreeNode tnFoundNode = getTreeNodeOfTraceType_recursive(tnChildNode, ttTraceType);
                if (tnFoundNode != null)
                    return tnFoundNode;
            }


            return null;
        }


        public static List<TreeNode> getListOfNormalizedTraces(List<TreeNode> normalizedTracesView)
        {
            var ltnTraces = new List<TreeNode>();
            DI.log.debug("Calculating List Of Normalized Traces for {0} ", normalizedTracesView.Count);
            int iTracesProcessed = 0;
            foreach (TreeNode tnNormalizedTrace in normalizedTracesView)
            {
                CreateUniqueTracesFromTreeView(tnNormalizedTrace, ltnTraces);
                iTracesProcessed++;
                if (iTracesProcessed % 1500 == 0)
                    DI.log.debug(" so far {0} (out of {1}) traces have generated {2} normalized Traces ", iTracesProcessed,normalizedTracesView.Count,ltnTraces.Count);
            }
            DI.log.debug(" Normalization complete: {0} (out of {1}) traces generated {2} normalized Traces ", iTracesProcessed, normalizedTracesView.Count, ltnTraces.Count);
            return ltnTraces;
        }

        public static void calculateListOfNodesWithSink_recursive(TreeNodeCollection tnTreeNodeCollection,
                                                                  String sSinkToFind, List<TreeNode> ltnNodesWithSink)
        {
            foreach (TreeNode tnTreeNode in tnTreeNodeCollection)
            {
                if (tnTreeNode.Tag != null)
                {
                    var otbO2TraceBlock = (O2TraceBlock_OunceV6) tnTreeNode.Tag;
                    //foreach(FindingViewItem fviFindingViewItem in otbO2TraceBlock.
                    //String sSink = o2.analysis.Analysis.getSink(fviFindingViewItem.fFinding, fviFindingViewItem.oadO2AssessmentDataOunceV6);                    

                    //if (sSink.IndexOf(sSinkToFind) > -1 )
                    if (otbO2TraceBlock.sSignature == sSinkToFind)
                    {
                        ltnNodesWithSink.Add(tnTreeNode);
                    }
                }
                calculateListOfNodesWithSink_recursive(tnTreeNode.Nodes, sSinkToFind, ltnNodesWithSink);
            }
        }


        public static Dictionary<AssessmentAssessmentFileFinding, O2AssessmentData_OunceV6> getUniqueListOfSinks(
            TreeView tvRawData)
        {
            var dAllSinkFindings = new Dictionary<AssessmentAssessmentFileFinding, O2AssessmentData_OunceV6>();
            foreach (TreeNode tnTreeNode in tvRawData.Nodes)
            {
                var otbO2TraceBlock = (O2TraceBlock_OunceV6) tnTreeNode.Tag;
                foreach (AssessmentAssessmentFileFinding fFinding in otbO2TraceBlock.dSinks.Keys)

                    if (false == dAllSinkFindings.ContainsKey(fFinding))
                        dAllSinkFindings.Add(fFinding, otbO2TraceBlock.dSinks[fFinding]);
            }
            return dAllSinkFindings;
        }


        public static Dictionary<AssessmentAssessmentFileFinding, O2AssessmentData_OunceV6> getSinksFindingsThatMatchRegEx(
            TreeView tvRawData, String sRegExToSearch)
        {
            var dMatches = new Dictionary<AssessmentAssessmentFileFinding, O2AssessmentData_OunceV6>();
            Dictionary<AssessmentAssessmentFileFinding, O2AssessmentData_OunceV6> dAllSinkFindings =
                getUniqueListOfSinks(tvRawData);

            foreach (AssessmentAssessmentFileFinding fFinding in dAllSinkFindings.Keys)
                if (RegEx.findStringInString(AnalysisUtils.getSink(fFinding, dAllSinkFindings[fFinding]), sRegExToSearch))
                    dMatches.Add(fFinding, dAllSinkFindings[fFinding]);
            return dMatches;
        }
    }
}
