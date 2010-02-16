// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Filters;
using O2.DotNetWrappers.Windows;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Interfaces.CIR;
using O2.Interfaces.O2Findings;
using O2.Legacy.OunceV6.SavedAssessmentFile.classes;

namespace O2.Legacy.OunceV6.JoinTraces.classes.filters
{
    public class onRawData
    {
        #region Nested type: dotNet

        public class dotNet
        {
            public static void mapDotNetWebServices(TreeView tvRawData)
            {
                String sDotNetWebServicesSinkToFind =
                    "System.Web.Services.Protocols.SoapHttpClientProtocol.Invoke(string;object[]):object[]";
                var ltnNodesWithSink = new List<TreeNode>();
                analyzer.calculateListOfNodesWithSink_recursive(tvRawData.Nodes, sDotNetWebServicesSinkToFind,
                                                                ltnNodesWithSink);
                foreach (TreeNode tnTreeNodeWithInvokeSink in ltnNodesWithSink)
                {
                    var otbO2TraceBlockWithSink = (O2TraceBlock_OunceV6) tnTreeNodeWithInvokeSink.Tag;
                    foreach (AssessmentAssessmentFileFinding fFinding in otbO2TraceBlockWithSink.dSources.Keys)
                    {
                        //FindingViewItem fviFindingViewItem
                        //String sSink = o2.analysis.Analysis.getSink(fFinding,otbO2TraceBlockWithSink.dSources[fFinding]);
                        //String sSource = o2.analysis.Analysis.getSource(fFinding, otbO2TraceBlockWithSink.dSources[fFinding]);
                        var lcaReverseListOfCallInvocation = new List<CallInvocation>();
                        AnalysisSearch.findTraceTypeInSmartTrace_Recursive_returnReverseListOfCallInvocation(
                            fFinding.Trace, TraceType.Known_Sink, lcaReverseListOfCallInvocation);
                        if (lcaReverseListOfCallInvocation.Count > 1)
                        {
                            var fsFilteredSignature =
                                new FilteredSignature(
                                    OzasmtUtils_OunceV6.getStringIndexValue(lcaReverseListOfCallInvocation[1].sig_id,
                                                                            otbO2TraceBlockWithSink.dSources[fFinding]));
                            String sSignatureToMatch = fsFilteredSignature.getFilteredSignature(true, false, false, -1);
                            // we really should also check the return class, but there are some diferences in the ways the objects are mapped (object[] on the client and ArrayList() on the server)                    
                            bool bFoundWebServiceSink = false;
                            foreach (TreeNode tnRawNode in tvRawData.Nodes)
                            {
                                var otbO2TraceBlock = (O2TraceBlock_OunceV6) tnRawNode.Tag;
                                String sRawTraceSignature =
                                    new FilteredSignature(otbO2TraceBlock.sSignature).getFilteredSignature(true, false,
                                                                                                           false, -1);
                                if (sRawTraceSignature == sSignatureToMatch)
                                {
                                    if (otbO2TraceBlock.dSources.Count == 0)
                                    {
                                        bFoundWebServiceSink = true;

                                        foreach (
                                            AssessmentAssessmentFileFinding fFindingInMappedSink in
                                                otbO2TraceBlock.dSinks.Keys)
                                        {
                                            String sSource = AnalysisUtils.getSource(fFindingInMappedSink,
                                                                                     otbO2TraceBlock.dSinks[
                                                                                         fFindingInMappedSink]);
                                            String sSink = AnalysisUtils.getSink(fFindingInMappedSink,
                                                                                 otbO2TraceBlock.dSinks[
                                                                                     fFindingInMappedSink]);
                                            DI.log.info("Adding {0} to {1}", sSource,
                                                        otbO2TraceBlockWithSink.sUniqueName);
                                            // add to dGluedSinks
                                            if (false ==
                                                otbO2TraceBlockWithSink.dGluedSinks.ContainsKey(fFindingInMappedSink))
                                            {
                                                otbO2TraceBlockWithSink.dGluedSinks.Add(fFindingInMappedSink,
                                                                                        otbO2TraceBlock.dSinks[
                                                                                            fFindingInMappedSink]);
                                                otbO2TraceBlockWithSink.dSinks.Add(fFindingInMappedSink,
                                                                                   otbO2TraceBlock.dSinks[
                                                                                       fFindingInMappedSink]);
                                            }
                                            // add to dGluedSinks                                        
                                            if (false == otbO2TraceBlock.dGluedSinks.ContainsKey(fFinding))
                                            {
                                                //otbO2TraceBlock.dGluedSinks.Add(fFinding, otbO2TraceBlockWithSink.dSources[fFinding]);
                                                //otbO2TraceBlock.dSinks.Add(fFinding, otbO2TraceBlockWithSink.dSources[fFinding]);
                                            }
                                        }

                                        /*              foreach (AssessmentAssessmentFileFinding fFinding in otbO2TraceBlock.dSinks.Keys)
                                                      {
                                                           DI.log.info("Adding trace to : {0} on Root node: {1}", tnTreeNodeWithInvokeSink.Text, O2Forms.getRootNode(tnTreeNodeWithInvokeSink).Text);
                                                          TreeNode tnTreeNodeToAdd = tnTreeNodeWithInvokeSink;
                                                          if (tnTreeNodeToAdd.Nodes.Count >0)
                                                              tnTreeNodeToAdd = tnTreeNodeToAdd.Nodes[0];
                                                          tnTreeNodeToAdd.Nodes.Add(O2Forms.newTreeNode("TEST", "TEST", 0, new FindingViewItem(fFinding, otbO2TraceBlock.dSinks[fFinding])));
                                                      }
                                         * */
                                        //tnTreeNodeWithInvokeSink
                                        //    DI.log.debug("  Found Possible match : {0}", otbO2TraceBlock.sSignature);
                                    }
                                    else
                                    {
                                        DI.log.debug("  Found FALSE POSITIVE match : {0}", otbO2TraceBlock.sSignature);
                                    }
                                }
                            }
                            if (false == bFoundWebServiceSink)
                                DI.log.error("in mapDotNetWebServices, could not find a match for: {0}",
                                             sSignatureToMatch);
                        }
                    }
                }
            }
        }

        #endregion

        #region Nested type: java

        public class java
        {
            public static void addVelocityMappings(TreeView tvRawData)
            {
                String sFunctionSignature = "ModelMap.addAttribute";
                O2Timer tTimer = new O2Timer("Adding Velocity Mappings : {0} ").start();
                Dictionary<AssessmentAssessmentFileFinding, O2AssessmentData_OunceV6> dMatches =
                    analyzer.getSinksFindingsThatMatchRegEx(tvRawData, sFunctionSignature);
                foreach (AssessmentAssessmentFileFinding fFinding in dMatches.Keys)
                {
                    // resolve addAddtibute name
                    String sSinkContext = AnalysisUtils.getSinkContext(fFinding, dMatches[fFinding]);
                    var fsFilteredSignature = new FilteredSignature(sSinkContext);
                    String sParameters = fsFilteredSignature.sParameters.Replace("\"", "");
                    String sSpringParameter = sParameters.Substring(0, sParameters.IndexOf(',')).Trim();

                    // create a unique name for it:
                    String sSink = AnalysisUtils.getSink(fFinding, dMatches[fFinding]);
                    // String sSinkWithAttributeName = sSink.Replace("(", "_" + sSpringParameter + "(");
                    String sVelocityMapping = String.Format("{0}            0", sSink);

                    TreeNode tnVelocityNode = tvRawData.Nodes[sSink];
                    if (tnVelocityNode != null)
                    {
                        var otbO2TraceBlockWithVelocityMappings = (O2TraceBlock_OunceV6) tnVelocityNode.Tag;

                        String sUniqueSignature = analyzer.getUniqueSignature(fFinding, TraceType.Known_Sink,
                                                                              dMatches[fFinding], true);
                        var otbO2TraceBlockToAddVelocityMappings = (O2TraceBlock_OunceV6) tvRawData.Nodes[sUniqueSignature].Tag;
                        //   sUniqueSignature = sUniqueSignature.Replace("_" + sSpringParameter + "(", "(");
                        //  O2TraceBlock_OunceV6 otbO2TraceBlockToAddVelocityMappings = (O2TraceBlock_OunceV6)tvRawData.Nodes[sUniqueSignature].Tag;

                        if (otbO2TraceBlockWithVelocityMappings.dSinks.Count > 1)
                        {
                        }

                        foreach (
                            AssessmentAssessmentFileFinding fVelocityFinding in
                                otbO2TraceBlockWithVelocityMappings.dSinks.Keys)
                        {
                            if (false == otbO2TraceBlockToAddVelocityMappings.dGluedSinks.ContainsKey(fVelocityFinding))
                                otbO2TraceBlockToAddVelocityMappings.dGluedSinks.Add(fVelocityFinding,
                                                                                     otbO2TraceBlockWithVelocityMappings
                                                                                         .dSinks[fVelocityFinding]);
                            if (false == otbO2TraceBlockToAddVelocityMappings.dSinks.ContainsKey(fVelocityFinding))
                                otbO2TraceBlockToAddVelocityMappings.dSinks.Add(fVelocityFinding,
                                                                                otbO2TraceBlockWithVelocityMappings.
                                                                                    dSinks[fVelocityFinding]);
                        }
                    }
                }
            }

            public static void findSpringAttributes(TreeView tvRawData)
            {
                String sFunctionSignature = "ModelMap.addAttribute";
                O2Timer tTimer = new O2Timer("Resolving attribute based function: {0} ").start();

                Dictionary<AssessmentAssessmentFileFinding, O2AssessmentData_OunceV6> dMatches =
                    analyzer.getSinksFindingsThatMatchRegEx(tvRawData, sFunctionSignature);
                foreach (AssessmentAssessmentFileFinding fFinding in dMatches.Keys)
                {
                    // resolve addAddtibute name
                    String sSinkContext = AnalysisUtils.getSinkContext(fFinding, dMatches[fFinding]);
                    var fsFilteredSignature = new FilteredSignature(sSinkContext);
                    String sParameters = fsFilteredSignature.sParameters.Replace("\"", "");
                    String sSpringParameter = sParameters.Substring(0, sParameters.IndexOf(',')).Trim();

                    // create a unique name for it:
                    String sSink = AnalysisUtils.getSink(fFinding, dMatches[fFinding]);
                    String sSinkWithAttributeName = sSink.Replace("(", "_" + sSpringParameter + "(");
                    // make sure we have not added this already
                    if (sSink.IndexOf(sSpringParameter) == -1)
                    {
                        //     String sSinkWithAttributeName = sSink.Replace("(", "_" + sSpringParameter + "(");
                        //      String sSinkWithAttributeName = sSpringParameter;
                        String sUniqueSignature = analyzer.getUniqueSignature(fFinding, TraceType.Known_Sink,
                                                                              dMatches[fFinding], true);
                        var otbO2TraceBlockOfThisFinding = (O2TraceBlock_OunceV6) tvRawData.Nodes[sUniqueSignature].Tag;

                        CallInvocation ciCallInvocation =
                            AnalysisSearch.findTraceTypeInSmartTrace_Recursive_returnCallInvocation(
                                fFinding.Trace, TraceType.Known_Sink);
                        UInt32 uNewId = OzasmtUtils_OunceV6.addTextToStringIndexes(sSinkWithAttributeName,
                                                                                   dMatches[fFinding].arAssessmentRun);
                        ;
                        ciCallInvocation.sig_id = uNewId;
                        DI.log.debug(" Found spring attribute '{0}' on sinks and modified to {1}", sSpringParameter,
                                     sSinkWithAttributeName);
                        //o2.analysis.Analysis.getSink(fFinding, dMatches[fFinding]));
                        otbO2TraceBlockOfThisFinding.sSignature = sSinkWithAttributeName;
                        otbO2TraceBlockOfThisFinding.sUniqueName = analyzer.getUniqueSignature(fFinding,
                                                                                               TraceType.
                                                                                                   Known_Sink,
                                                                                               dMatches[fFinding], true);

                        List<O2TraceBlock_OunceV6> lotbO2TraceBlockWithVelocityMappings =
                            analyzer.getO2TraceBlocksThatMatchSignature(sSinkWithAttributeName, tvRawData);


/*                        String sVelocityMapping = String.Format("{0}            0", sSinkWithAttributeName);
                        TreeNode tnVelocityNode = tvRawData.Nodes[sVelocityMapping];
                        if (tnVelocityNode != null)
 */
                        foreach (
                            O2TraceBlock_OunceV6 otbO2TraceBlockWithVelocityMappings in lotbO2TraceBlockWithVelocityMappings)
                        {
                            if (otbO2TraceBlockWithVelocityMappings.sFile.IndexOf(".vm") > -1)

                                //O2TraceBlock_OunceV6 otbO2TraceBlockWithVelocityMappings = (O2TraceBlock_OunceV6)tnVelocityNode.Tag;
                                foreach (
                                    AssessmentAssessmentFileFinding fVelocityFinding in
                                        otbO2TraceBlockWithVelocityMappings.dSinks.Keys)
                                {
                                    if (false == otbO2TraceBlockOfThisFinding.dGluedSinks.ContainsKey(fVelocityFinding))
                                        otbO2TraceBlockOfThisFinding.dGluedSinks.Add(fVelocityFinding,
                                                                                     otbO2TraceBlockWithVelocityMappings
                                                                                         .dSinks[fVelocityFinding]);
                                    if (false == otbO2TraceBlockOfThisFinding.dSinks.ContainsKey(fVelocityFinding))
                                        otbO2TraceBlockOfThisFinding.dSinks.Add(fVelocityFinding,
                                                                                otbO2TraceBlockWithVelocityMappings.
                                                                                    dSinks[fVelocityFinding]);
                                }
                        }
                    }
                }
                tTimer.stop();
            }


            public static void mapInterfaces(TreeView tvRawData, ICirData cdCirData, String sHardCodedInterfaceKeywork,
                                             bool bAddGluedTracesAsRealTraces)
            {
                DI.log.debug("Mapping Interfaces");
                O2Timer tTimer = new O2Timer("Interfaces mapping completed").start();
                // Int32 iItemsProcessed = 0;

                Dictionary<AssessmentAssessmentFileFinding, O2AssessmentData_OunceV6> dAllSinkFindings =
                    analyzer.getUniqueListOfSinks(tvRawData);

                foreach (AssessmentAssessmentFileFinding fFinding in dAllSinkFindings.Keys)
                {
                    String sSink = AnalysisUtils.getSink(fFinding, dAllSinkFindings[fFinding]);
                    if (sSink != "" && sSink.IndexOf(sHardCodedInterfaceKeywork) > -1)
                    {
                        if (false == cdCirData.dFunctions_bySignature.ContainsKey(sSink))
                            DI.log.error("in mapInterfaces, could not find signature in loaded CirData file: {0}",
                                         sSink);
                        else
                        {
                            ICirFunction cfCirFunction = cdCirData.dFunctions_bySignature[sSink];
                            ICirClass ccCirClass = cfCirFunction.ParentClass;
                            foreach (ICirClass ccIsSuperClassedBy in ccCirClass.dIsSuperClassedBy.Values)
                            {
                                String sMappedMethodName = cfCirFunction.FunctionSignature.Replace(ccCirClass.Signature,
                                                                                                   ccIsSuperClassedBy.
                                                                                                       Signature);
                                List<O2TraceBlock_OunceV6> lotdMatches =
                                    analyzer.getO2TraceBlocksThatMatchSignature(sMappedMethodName, tvRawData);
                                foreach (O2TraceBlock_OunceV6 otbO2TraceBlockWithTracesToGlue in lotdMatches)
                                {
                                    addFindingAsGlueTrace(otbO2TraceBlockWithTracesToGlue, fFinding,
                                                          dAllSinkFindings[fFinding], tvRawData,
                                                          bAddGluedTracesAsRealTraces);
                                }
                            }
                        }
                    }
                }

                tTimer.stop();
            }

            public static bool ResolveInterfacesOnTreeView_recursive(TreeNode tnStartNode, ICirData cdCirData,
                                                                     Dictionary<String, O2TraceBlock_OunceV6> dO2TraceBlock,
                                                                    Dictionary<String, O2TraceBlock_OunceV6> dRawData,
                                                                     //TreeView tvRawData_,
                                                                     String sHardCodedInterfaceKeywork)
            {
                var stStackTrace = new StackTrace();
                if (stStackTrace.FrameCount > 50)
                {
                    String sMsg =
                        String.Format(
                            "on ResolveInterfacesOnTreeView_recursive, max StackTrace reached, aborting this leaf:{0}",
                            tnStartNode.Text);
                    DI.log.error(sMsg);
                    return false;
                }
                if (tnStartNode != null)
                    foreach (TreeNode tnNode in tnStartNode.Nodes)
                    {
                        if (tnNode.Text.IndexOf(sHardCodedInterfaceKeywork) > -1)
                            if (tnNode.Tag != null)
                            {
                                var fviFindingViewItem = (FindingViewItem) tnNode.Tag;
                                String sSink = AnalysisUtils.getSink(fviFindingViewItem.fFinding,
                                                                     fviFindingViewItem.oadO2AssessmentDataOunceV6);
                                if (cdCirData.dFunctions_bySignature.ContainsKey(sSink))
                                {
                                    ICirFunction cfCirFunction = cdCirData.dFunctions_bySignature[sSink];
                                    ICirClass ccCirClass = cfCirFunction.ParentClass;
                                    foreach (ICirClass ccIsSuperClassedBy in ccCirClass.dIsSuperClassedBy.Values)
                                    {
                                        String sMappedMethodName =
                                            cfCirFunction.FunctionSignature.Replace(ccCirClass.Signature,
                                                                                    ccIsSuperClassedBy.Signature);
                                        List<O2TraceBlock_OunceV6> lotdMatches =
                                            analyzer.getO2TraceBlocksThatMatchSignature(sMappedMethodName, dO2TraceBlock);
                                        foreach (O2TraceBlock_OunceV6 otbO2TraceBlock in lotdMatches)
                                        {
                                            TreeNode tnNewNode_forImplementation =
                                                O2Forms.newTreeNode(otbO2TraceBlock.sUniqueName,
                                                                    otbO2TraceBlock.sUniqueName, 0, null);
                                            tnNode.ForeColor = Color.CadetBlue;
                                            tnNewNode_forImplementation.ForeColor = Color.DarkBlue;
                                            foreach (
                                                AssessmentAssessmentFileFinding fFinding in otbO2TraceBlock.dSinks.Keys)
                                            {
                                                var fviFindingViewItem_ForSink = new FindingViewItem(fFinding,
                                                                                                     fFinding.vuln_name ?? OzasmtUtils_OunceV6.
                                                                                                                               getStringIndexValue
                                                                                                                               (UInt32.Parse
                                                                                                                                    (fFinding
                                                                                                                                         .
                                                                                                                                         vuln_name_id),
                                                                                                                                otbO2TraceBlock.dSinks[fFinding]),
                                                                                                     null,
                                                                                                     otbO2TraceBlock.
                                                                                                         dSinks[fFinding
                                                                                                         ]);
                                                String sUniqueName_ForSink =
                                                    analyzer.getUniqueSignature(fviFindingViewItem_ForSink.fFinding,
                                                                                TraceType.Known_Sink,
                                                                                fviFindingViewItem_ForSink.
                                                                                    oadO2AssessmentDataOunceV6, true);
                                                TreeNode tnImplementation_Sink = O2Forms.newTreeNode(
                                                    sUniqueName_ForSink, sUniqueName_ForSink, 0,
                                                    fviFindingViewItem_ForSink);
                                                tnNewNode_forImplementation.Nodes.Add(tnImplementation_Sink);
                                                if (tnImplementation_Sink.Text != "")
                                                    if (false ==
                                                        analyzer.addCompatibleTracesToNode_recursive(
                                                            tnImplementation_Sink, fviFindingViewItem_ForSink,
                                                        dRawData[tnImplementation_Sink.Text], "Sinks",  dRawData))
//                                                            (O2TraceBlock_OunceV6)
//                                                            tvRawData.Nodes[tnImplementation_Sink.Text].Tag, "Sinks",
  //                                                          tvRawData))
                                                        return false;
                                                // need to see any posible side effects of this (false check was not there on small projects)
                                            }
                                            tnNode.Nodes.Add(tnNewNode_forImplementation);
                                        }
                                    }
                                }
                            }
                        foreach (TreeNode tnChildNode in tnNode.Nodes)
                        {
                            if (false ==
                                ResolveInterfacesOnTreeView_recursive(tnChildNode, cdCirData, dO2TraceBlock, dRawData, //tvRawData,
                                                                      sHardCodedInterfaceKeywork))
                                return false;
                        }
                    }
                return true;
            }

            public static void addSuportForDynamicMethodsOnSinks(TreeView tvRawData, ICirData cdCirData,
                                                                 bool bAddGluedTracesAsRealTraces)
            {
                O2Timer tTimer = new O2Timer("Adding Support for Dynamic Methods on Sinks").start();
                // first get a unique list of Sink Traces
                Dictionary<AssessmentAssessmentFileFinding, O2AssessmentData_OunceV6> dAllSinkFindings =
                    analyzer.getUniqueListOfSinks(tvRawData);
                //List<String> lsMatches = new List<string>();
                foreach (AssessmentAssessmentFileFinding fFinding in dAllSinkFindings.Keys)
                {
                    CallInvocation ciSink =
                        AnalysisSearch.findTraceTypeInSmartTrace_Recursive_returnCallInvocation(fFinding.Trace,
                                                                                                TraceType.
                                                                                                    Known_Sink);
                    if (ciSink != null)
                    {
                        String sContext = OzasmtUtils_OunceV6.getStringIndexValue(ciSink.cxt_id, dAllSinkFindings[fFinding]);
//                        var fsFilteredSignature = new FilteredSignature(sContext);
                        if (sContext.IndexOf("java.lang.Object.getClass()") > -1 && sContext.IndexOf("new") > -1 &&
                            sContext.IndexOf("$") > -1)
                        {
                            // we have a match lets get the class out
                            String sClass = sContext.Substring(sContext.IndexOf("new") + 4);
                            sClass = sClass.Substring(0, sClass.IndexOf(' '));

                            // Find traces that match the found class and add them as GluedSinks
                            foreach (TreeNode tnMatchNode in tvRawData.Nodes)
                                if (tnMatchNode.Text.IndexOf(sClass) > -1)
                                {
                                    addFindingAsGlueTrace((O2TraceBlock_OunceV6) tnMatchNode.Tag, fFinding,
                                                          dAllSinkFindings[fFinding], tvRawData,
                                                          bAddGluedTracesAsRealTraces);
                                }
                        }
                    }
                }

                tTimer.stop();
            }

            public static void addFindingAsGlueTrace(O2TraceBlock_OunceV6 otbO2TraceBlockOunceV6WithTracesToGlue,
                                                     AssessmentAssessmentFileFinding fFinding,
                                                     O2AssessmentData_OunceV6 oadO2AssessmentDataOunceV6, TreeView tvRawData,
                                                     bool bAddGluedTracesAsRealTraces)
            {
                String sUniqueSignature = analyzer.getUniqueSignature(fFinding, TraceType.Known_Sink,
                                                                      oadO2AssessmentDataOunceV6, true);

                var otbO2TraceBlockWithTracesToReceiveTraces = (O2TraceBlock_OunceV6) tvRawData.Nodes[sUniqueSignature].Tag;

                foreach (AssessmentAssessmentFileFinding fFindingToGlue in otbO2TraceBlockOunceV6WithTracesToGlue.dSinks.Keys)
                {
                    if (false == otbO2TraceBlockWithTracesToReceiveTraces.dGluedSinks.ContainsKey(fFindingToGlue))
                        otbO2TraceBlockWithTracesToReceiveTraces.dGluedSinks.Add(fFindingToGlue,
                                                                                 otbO2TraceBlockOunceV6WithTracesToGlue.dSinks[
                                                                                     fFindingToGlue]);
                    if (bAddGluedTracesAsRealTraces) // so that the traces show in the Raw View list
                        if (false == otbO2TraceBlockWithTracesToReceiveTraces.dSinks.ContainsKey(fFindingToGlue))
                            otbO2TraceBlockWithTracesToReceiveTraces.dSinks.Add(fFindingToGlue,
                                                                                otbO2TraceBlockOunceV6WithTracesToGlue.dSinks[
                                                                                    fFindingToGlue]);
                }
            }
        }

        #endregion
    }
}
