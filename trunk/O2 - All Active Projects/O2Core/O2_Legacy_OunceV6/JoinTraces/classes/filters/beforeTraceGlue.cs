namespace O2.Legacy.OunceV6.JoinTraces.classes.filters
{
    internal class beforeTraceGlue
    {
        #region Nested type: java

        public class java
        {
            /*
             * public static bool ResolveInterfacesOnTreeView_recursive(TreeNode tnStartNode, CirData cdCirData, Dictionary<String, O2TraceBlock_OunceV6> dO2TraceBlock, TreeView tvRawData, String sHardCodedInterfaceKeywork)
            {                
                System.Diagnostics.StackTrace stStackTrace = new System.Diagnostics.StackTrace();
                if (stStackTrace.FrameCount > 50)
                {
                    String sMsg = String.Format("on ResolveInterfacesOnTreeView_recursive, max StackTrace reached, aborting this leaf:{0}", tnStartNode.Text);
                     DI.log.error(sMsg);
                    return false;
                }
                if (tnStartNode != null)
                    foreach (TreeNode tnNode in tnStartNode.Nodes)
                    {
                        if (tnNode.Text.IndexOf(sHardCodedInterfaceKeywork) > -1)
                            if (tnNode.Tag != null)
                            {
                                FindingViewItem fviFindingViewItem = (FindingViewItem)tnNode.Tag;
                                String sSink = o2.analysis.Analysis.getSink(fviFindingViewItem.fFinding, fviFindingViewItem.oadO2AssessmentDataOunceV6);
                                if (cdCirData.dFunctions_bySignature.ContainsKey(sSink))
                                {
                                    cir.CirFunction cfCirFunction = cdCirData.dFunctions_bySignature[sSink];
                                    cir.CirClass ccCirClass = cfCirFunction.ccParentClass;
                                    foreach (cir.CirClass ccIsSuperClassedBy in ccCirClass.dIsSuperClassedBy.Values)
                                    {
                                        String sMappedMethodName = cfCirFunction.sSignature.Replace(ccCirClass.sSignature, ccIsSuperClassedBy.sSignature);
                                        List<O2TraceBlock_OunceV6> lotdMatches = analyzer.getO2TraceBlocksThatMatchSignature(sMappedMethodName, dO2TraceBlock);
                                        foreach (O2TraceBlock_OunceV6 otbO2TraceBlock in lotdMatches)
                                        {
                                            TreeNode tnNewNode_forImplementation = O2Forms.newTreeNode(otbO2TraceBlock.sUniqueName, otbO2TraceBlock.sUniqueName, 0, null);
                                            tnNode.ForeColor = Color.CadetBlue;
                                            tnNewNode_forImplementation.ForeColor = Color.DarkBlue;
                                            foreach (AssessmentAssessmentFileFinding fFinding in otbO2TraceBlock.dSinks.Keys)
                                            {
                                                FindingViewItem fviFindingViewItem_ForSink = new FindingViewItem(fFinding, fFinding.vuln_name, null, otbO2TraceBlock.dSinks[fFinding]);
                                                String sUniqueName_ForSink = analyzer.getUniqueSignature(fviFindingViewItem_ForSink.fFinding, o2.analysis.Analysis.TraceType.Known_Sink, fviFindingViewItem_ForSink.oadO2AssessmentDataOunceV6, true);
                                                TreeNode tnImplementation_Sink = O2Forms.newTreeNode(sUniqueName_ForSink, sUniqueName_ForSink, 0, fviFindingViewItem_ForSink);
                                                tnNewNode_forImplementation.Nodes.Add(tnImplementation_Sink);
                                                if (tnImplementation_Sink.Text != "")
                                                    if (false == analyzer.addCompatibleTracesToNode_recursive(tnImplementation_Sink, fviFindingViewItem_ForSink, (O2TraceBlock_OunceV6)tvRawData.Nodes[tnImplementation_Sink.Text].Tag, "Sinks", tvRawData))
                                                        return false;  // need to see any posible side effects of this (false check was not there on small projects)
                                            }
                                            tnNode.Nodes.Add(tnNewNode_forImplementation);
                                        }

                                    }

                                }
                            }
                        foreach (TreeNode tnChildNode in tnNode.Nodes)
                        {
                            if (false == ResolveInterfacesOnTreeView_recursive(tnChildNode, cdCirData, dO2TraceBlock, tvRawData, sHardCodedInterfaceKeywork))
                                return false;
                        }
                    }
                return true; 
            }*/
        }

        #endregion
    }
}