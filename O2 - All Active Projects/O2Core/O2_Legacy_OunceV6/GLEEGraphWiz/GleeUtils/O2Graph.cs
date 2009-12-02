// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Microsoft.Glee.Drawing;
using Microsoft.Glee.GraphViewerGdi;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Filters;
using O2.DotNetWrappers.Windows;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Kernel.Interfaces.O2Findings;
using QuickGraph;
using Color=Microsoft.Glee.Drawing.Color;

namespace O2.Legacy.OunceV6.GLEEGraphWiz.GleeUtils
{
    public class O2Graph
    {
        public static void createGraphWizDotFile(AdjacencyGraph<String, MarkedEdge<String, String>> gGraphWizToPopulate,
                                                 TreeNode tnTreeNode, bool bOrder, bool bFilterName, bool bFilterClass,
                                                 int iFilterClassLevel)
        {
            if (bFilterClass)
                tnTreeNode.Text = FilteredSignature.filterName(tnTreeNode.Text, false, false, true, 0, true, true,
                                                               iFilterClassLevel);
            MarkedEdge<String, string> meTemp;
            if (gGraphWizToPopulate.ContainsVertex(tnTreeNode.Text))
            {
            }
            else
                gGraphWizToPopulate.AddVertex(tnTreeNode.Text);

            foreach (TreeNode tnChild in tnTreeNode.Nodes)
            {
                if (bFilterClass)
                    tnChild.Text = FilteredSignature.filterName(tnChild.Text, false, false, true, 0, true, true,
                                                                iFilterClassLevel);
                createGraphWizDotFile(gGraphWizToPopulate, tnChild, bOrder, bFilterName, bFilterClass, iFilterClassLevel);
                if (bOrder)
                {
                    if (false == gGraphWizToPopulate.TryGetEdge(tnTreeNode.Text, tnChild.Text, out meTemp))
                        gGraphWizToPopulate.AddEdge(new MarkedEdge<String, string>(tnTreeNode.Text, tnChild.Text,
                                                                                   "marker"));
                }
                else if (false == gGraphWizToPopulate.TryGetEdge(tnChild.Text, tnTreeNode.Text, out meTemp))
                    gGraphWizToPopulate.AddEdge(new MarkedEdge<String, string>(tnChild.Text, tnTreeNode.Text, "marker"));

                //gGraphToPopulate.AddEdge(tnTreeNode.Text, tnChild.Text);
                //    gGraphToPopulate.AddEdge(Analysis_CallFlow.display.filterName(tnChild.Text, false, false, false), Analysis_CallFlow.display.filterName(tnTreeNode.Text, false, false, false));
                //else
            }
        }

        public static void createGifWithGraph(String sDotFile, string sGifFile, String sWorkingDirectory,
                                              String sExtraArguments, String sEngine)
        {
            var pProcess = new Process();
            //pProcess.StartInfo.FileName = @"C:\Program Files\Graphviz2.16\bin\neato.exe";
            pProcess.StartInfo.FileName = @"C:\Program Files\Graphviz2.16\bin\" + sEngine + ".exe";
            pProcess.StartInfo.Arguments = " -s  -Tgif " + sDotFile + " -o " + sGifFile + sExtraArguments;
            pProcess.StartInfo.WorkingDirectory = sWorkingDirectory;
            //pProcess.StartInfo.CreateNoWindow = true;
            //pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.RedirectStandardOutput = true;
            pProcess.StartInfo.RedirectStandardError = true;

            pProcess.Start();
            //   DI.log.debug("GraphWiz StandardOutput: {0}", pProcess.StandardOutput.ReadToEnd());
            //    DI.log.error("GraphWiz StandardError: {0}",pProcess.StandardError.ReadToEnd());
            pProcess.WaitForExit();
        }

        public static void populateGraphWithTreeNode(ref int iEdgeId, Graph gGraphToPopulate, TreeNode tnTreeNode)
        {
            bool bOrder = false;
            populateGraphWithTreeNode(ref iEdgeId, gGraphToPopulate, tnTreeNode, bOrder);
        }

        public static void populateGraphWithTreeNode(ref int iEdgeId, Graph gGraphToPopulate, TreeNode tnTreeNode,
                                                     bool bOrder)
        {
            bool bFilterName = false;
            bool bOnlyShowClasses = false;
            Int32 iShowClassesLevel = 0;
            populateGraphWithTreeNode(ref iEdgeId, gGraphToPopulate, tnTreeNode, bOrder, bFilterName, bOnlyShowClasses,
                                      iShowClassesLevel);
        }

        public static String populateGraphWithTreeNode(ref int iEdgeId, Graph gGraphToPopulate, TreeNode tnTreeNode,
                                                       bool bOrder, bool bFilterName, bool bOnlyShowClasses,
                                                       Int32 iShowClassesLevel)
        {
            bool bShowParameters = false;
            bool bShowReturnClass = false;
            bool bShowNamespace = false;
            Int32 iNamespaceDepth = 0;
            return populateGraphWithTreeNode(ref iEdgeId, gGraphToPopulate, tnTreeNode, bOrder, bFilterName,
                                             bOnlyShowClasses, iShowClassesLevel, bShowParameters, bShowReturnClass,
                                             bShowNamespace, iNamespaceDepth);
        }

        public static String populateGraphWithTreeNode(ref int iEdgeId, Graph gGraphToPopulate, TreeNode tnTreeNode,
                                                       bool bOrder, bool bFilterName, bool bOnlyShowClasses,
                                                       Int32 iShowClassesLevel,
                                                       bool bShowParameters, bool bShowReturnClass, bool bShowNamespace,
                                                       int iNamespaceDepth)
        {
            String sNormalizedName = FilteredSignature.filterName(tnTreeNode.Text, bShowParameters, bShowReturnClass,
                                                                  bShowNamespace, iNamespaceDepth, bFilterName,
                                                                  bOnlyShowClasses, iShowClassesLevel);
            if (sNormalizedName == "")
                sNormalizedName = "[***Empty***]";
            var nNode = (Node) gGraphToPopulate.AddNode(sNormalizedName);
            nNode.UserData = tnTreeNode;
            nNode.Attr.Shape = Shape.Plaintext;

            String sNodeText_From = tnTreeNode.Text;
            foreach (TreeNode tnChild in tnTreeNode.Nodes)
            {
                String sNodeText_To = FilteredSignature.filterName(tnChild.Text, bShowParameters, bShowReturnClass,
                                                                   bShowNamespace, iNamespaceDepth, bFilterName,
                                                                   bOnlyShowClasses, iShowClassesLevel);
                if (sNodeText_From != sNodeText_To) // don't add edge if they are the same text
                    addEdgeToGraph(gGraphToPopulate, sNodeText_From, sNodeText_To, (iEdgeId++).ToString());
                sNodeText_From = populateGraphWithTreeNode(ref iEdgeId, gGraphToPopulate, tnChild, bOrder, bFilterName,
                                                           bOnlyShowClasses, iShowClassesLevel);

                // add the return value 
                // need to find a way to not do this for sinks
                //         if (sNodeText_From != sNodeText_To)
                //         {
                //             addEdgeToGraph(gGraphToPopulate, sNodeText_From, sNodeText_To, (iEdgeId++).ToString());
                //             sNodeText_From = sNodeText_To;
                //         }

                /*
                String sMethodA = Analysis_CallFlow.display.filterName(tnChild.Text    , bShowParameters, bShowReturnClass, bShowNamespace, bFilterName, bOnlyShowClasses, iShowClassesLevel);
                String sMethodB = Analysis_CallFlow.display.filterName(tnTreeNode.Text , bShowParameters, bShowReturnClass, bShowNamespace, bFilterName, bOnlyShowClasses, iShowClassesLevel);
                if (bOrder)
                    addEdgeToGraph(gGraphToPopulate,sMethodA,sMethodB,true);                    
                else
                    addEdgeToGraph(gGraphToPopulate,sMethodB,sMethodA,false);                             
                 */
            }
            return sNodeText_From;
        }


        public static void addGraphToGraph(Graph gSourceGraph, Graph gHostGraph)
        {
            // add nodes
            foreach (Node nNode in gSourceGraph.Nodes)
            {
                var nNewNew = (Node) gHostGraph.AddNode(nNode.Id);
                nNewNew.UserData = nNode.UserData;
            }
            // add edges
            foreach (Edge eEdge in gSourceGraph.Edges)
            {
                gHostGraph.Edges.Add(eEdge);
            }
        }

        /* this was the previous method to display the data which is more closer to the one shown by OSA but since we can enforce the left to right reading mode
         * the sequence mode should be more readable (specially when showing mode than one trace 
         * - Once might want to add this view as a speciall view

        public static void populateGraphWithTreeNode(Graph gGraphToPopulate, TreeNode tnTreeNode,
            bool bOrder, bool bFilterName, bool bOnlyShowClasses, Int32 iShowClassesLevel,
            bool bShowParameters, bool bShowReturnClass, bool bShowNamespace)
        {
            String sNormalizedName = Analysis_CallFlow.display.filterName(tnTreeNode.Text, bShowParameters, bShowReturnClass, bShowNamespace, bFilterName, bOnlyShowClasses, iShowClassesLevel);
            if (sNormalizedName == "")
                sNormalizedName = "[***Empty***]";
            Node nNode = (Node)gGraphToPopulate.AddNode(sNormalizedName);
            nNode.UserData = tnTreeNode;
            nNode.Attr.Shape = Shape.Plaintext;
            foreach (TreeNode tnChild in tnTreeNode.Nodes)
            {
                populateGraphWithTreeNode(gGraphToPopulate, tnChild, bOrder, bFilterName, bOnlyShowClasses, iShowClassesLevel);
                String sMethodA = Analysis_CallFlow.display.filterName(tnChild.Text, bShowParameters, bShowReturnClass, bShowNamespace, bFilterName, bOnlyShowClasses, iShowClassesLevel);
                String sMethodB = Analysis_CallFlow.display.filterName(tnTreeNode.Text, bShowParameters, bShowReturnClass, bShowNamespace, bFilterName, bOnlyShowClasses, iShowClassesLevel);
                if (bOrder)
                    addEdgeToGraph(gGraphToPopulate, sMethodA, sMethodB, true);
                else
                    addEdgeToGraph(gGraphToPopulate, sMethodB, sMethodA, false);
            }
        } */

        public static void addEdgeToGraph(Graph gTargetGraph, String sMethodA, String sMethodB, String sEdgeText)
        {
            sEdgeText = " " + sEdgeText; // add a space to the left or this text will show connected to the line
            if (sMethodA == "")
                sMethodA = "[**MA_Empty**]";
            if (sMethodB == "")
                sMethodB = "[**MB_Empty**]";
            // String sEdgeName = "";// String.Format("{0}->{1}", sMethodA, sMethodB);
            /*   if (bDropDuplicates)
                   if (null == gTargetGraph.EdgeById(sEdgeName))
                       gTargetGraph.AddEdge(sMethodA,sEdgeName,sMethodB);
                   else
                   {
                        DI.log.debug("Edge already exists {0}", sEdgeName);
                   }
               else
             * */
            gTargetGraph.AddEdge(sMethodA, sEdgeText, sMethodB);
        }


        public static void applyStylesAndFiltersToGraph(Graph gGraph, Shape sShape, O2AssessmentData_OunceV6 fadAssessmentDataOunceV6,
                                                        bool bLDDB_ShowInsideNode_MethodName, bool bGLEE_ShowParameters,
                                                        bool bGLEE_ShowReturnClass,
                                                        bool bGLEE_ShowNamespace, Int32 iNamespaceDepth,
                                                        bool bLDDB_ShowInsideNode_Context,
                                                        bool bLDDB_ShowInsideNode_SourceCode,
                                                        bool bGLEE_onlyShowDataForSourcesAndSinks)
        {
            Hashtable htNodes = gGraph.NodeMap;
            foreach (DictionaryEntry deNode in htNodes)
            {
                var nNode = (Node) deNode.Value;
                nNode.Attr.Shape = sShape;
                nNode.NodeAttribute.LabelMargin = 6; // give it some margin                
                nNode.NodeAttribute.Fillcolor = Color.White;
                if (bLDDB_ShowInsideNode_MethodName)
                    nNode.Attr.Label = FilteredSignature.filterName(nNode.Attr.Label, bGLEE_ShowParameters,
                                                                    bGLEE_ShowReturnClass, bGLEE_ShowNamespace,
                                                                    iNamespaceDepth);
                else
                {
                    if (false == bLDDB_ShowInsideNode_Context && false == bLDDB_ShowInsideNode_SourceCode)
                        makeEmptyNode(nNode);
                    else
                        nNode.Attr.Label = "";
                }

                var tnNodeData = (TreeNode) nNode.UserData;
                if (tnNodeData != null && tnNodeData.Tag != null)
                    switch (tnNodeData.Tag.GetType().Name)
                    {
                        case "CallInvocation":
                            var ciCallInvocation = (CallInvocation) tnNodeData.Tag;


                            if (ciCallInvocation.cxt_id > 0)
                            {
                                // add context
                                if (bLDDB_ShowInsideNode_Context)
                                    nNode.Attr.Label += Environment.NewLine + Environment.NewLine +
                                                        fadAssessmentDataOunceV6.arAssessmentRun.StringIndeces[
                                                            ciCallInvocation.cxt_id - 1].value;
                                if (bLDDB_ShowInsideNode_SourceCode)
                                {
                                    // add source code
                                    AssessmentRunFileIndex arfiFile =
                                        fadAssessmentDataOunceV6.arAssessmentRun.FileIndeces[ciCallInvocation.fn_id - 1];
                                    List<String> lsSourceCode = Files.loadSourceFileIntoList(arfiFile.value);
                                    if (lsSourceCode.Count > 0 && lsSourceCode.Count > ciCallInvocation.line_number)
                                        nNode.Attr.Label += Environment.NewLine + Environment.NewLine +
                                                            getSourceCodeSnippet(lsSourceCode,
                                                                                 (int) ciCallInvocation.line_number);
                                }
                                nNode.Attr.Label = nNode.Attr.Label.Trim();
                            }


                            if (nNode.Attr.Fontcolor != Color.Red)
                            {
                                switch (ciCallInvocation.trace_type)
                                {
                                    case 1: // Analysis.TraceType.Root_Call:        
                                        if (bGLEE_onlyShowDataForSourcesAndSinks)
                                            makeEmptyNode(nNode);
                                        if (nNode.Attr.Fontcolor == Color.Black)
                                            // only change it if it has not been mapped before
                                            nNode.Attr.Fontcolor = nNode.Attr.Color = Color.DarkBlue;
                                        break;
                                    case 5: // Analysis.TraceType.Lost_Sink:
                                        nNode.Attr.Fontcolor = nNode.Attr.Color = Color.DarkOrange;
                                        break;
                                    case 2: // Analysis.TraceType.Source:
                                        nNode.Attr.Fontcolor = nNode.Attr.Color = Color.DarkRed;
                                        break;
                                    case 3: // Analysis.TraceType.Known_Sink:
                                        nNode.Attr.Fontcolor = nNode.Attr.Color = Color.Red;
                                        break;
                                    case 4: // Analysis.TraceType._type_4:
                                        if (bGLEE_onlyShowDataForSourcesAndSinks)
                                            makeEmptyNode(nNode);
                                        nNode.Attr.Fontcolor = nNode.Attr.Color = Color.Green;
                                        break;                                        
                                    default:
                                        if (bGLEE_onlyShowDataForSourcesAndSinks)
                                            makeEmptyNode(nNode);
                                        break;
                                }
                            }
                            break;
                        default:
                            //                        nNode.Attr.Label += " ***";
                            break;
                    }
            }
        }

        public static void makeEmptyNode(Node nNode)
        {
            nNode.Attr.Label = "";
            nNode.Attr.Shape = Shape.Circle;
        }

        public static String getSourceCodeSnippet(List<String> lsSourceCode, int iLinenumber)
        {
            String sSourceCodeSnippet = lsSourceCode[iLinenumber - 1].Replace("\t", "");
            return sSourceCodeSnippet;
        }

        public static void saveGleeGraphAsImage(String sPathToImage, Graph gGraph)
        {
            //String sImagePath = config.getTempFileNameInO2TempDirectory() + ".png";// System.IO.Path.Combine(DI.config.O2TempDir, "test.png");
            // Saving map
            var grGraphRendere = new GraphRenderer(gGraph);
            int iWidth = 1024;
            int iHeight = 1024;
            grGraphRendere.CalculateLayout();
            iWidth = (int) gGraph.Width;
            iHeight = (int) gGraph.Height;
            //     DI.log.debug("Width: {0} , Height: {1}", iWidth.ToString(), iHeight.ToString());
            //Bitmap bitmap = new Bitmap(width, (int)(gGraph.Height * (width / gGraph.Width)),System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            var bitmap = new Bitmap(iWidth, iHeight, PixelFormat.Format32bppPArgb);
            grGraphRendere.Render(bitmap);
            bitmap.Save(sPathToImage);
            //      DI.log.debug("GLEE graph saved as bitmap image to: {0}", sPathToImage);
        }

        public static void onlyShowNodesContainingClass(Graph gGraph, String sStringClass)
        {
            if (sStringClass == "")
                return;
            foreach (Node nNode in gGraph.Nodes)
            {
                if (false == FilteredSignature.isSignatureCached(nNode.Id))
                    DI.log.error(
                        "in onlyShowNodesContainingClass, dFilteredFuntionSignatures did not contain the signature: {0}",
                        nNode.Id);
                else
                {
                    FilteredSignature fFilteredSignature = FilteredSignature.getFromCache(nNode.Id);
                    if (fFilteredSignature.getClassName(0).IndexOf(sStringClass) == -1)
                        if (false == isNodeASourceOrSink(nNode)) // Don't remove sources or sinks
                            makeEmptyNode(nNode);
                    //nNode.NodeAttribute.Label = "TO REMOVE";
                    // DI.log.debug("{0}   :   {1}", fFilteredSignature.getClassName(0), nNode.Id);
                }
            }
        }

        // this will create a graph that consolidates all repeated paths (NodeA -> NodeB) into one path with the label containing the number of traces between those pages (where by default the label contains the sequence number)
        public static void createGraphWithConsolidatedPaths(ref Graph gGraph)
        {
            DI.log.debug("Creating createGraphWithConsolidatedPaths for {0} nodes", gGraph.Nodes.Count);
            String sEdgeSeparator = "-->";
            var dEdges = new Dictionary<string, int>();
            foreach (Edge eEdge in gGraph.Edges)
            {
                String sEdgeString = String.Format("{0}{1}{2}", eEdge.Source, sEdgeSeparator, eEdge.Target);
                if (false == dEdges.ContainsKey(sEdgeString))
                    dEdges.Add(sEdgeString, new Int32());
                dEdges[sEdgeString]++;
            }
            DI.log.debug("dEdges contains {0} items", dEdges);
            var gNewGraph = new Graph("Graph with no Duplicate edges");
            foreach (Node nNode in gGraph.Nodes)
            {
                var nNewNode = (Node) gNewGraph.AddNode(nNode.Id);
                nNewNode.UserData = nNode.UserData;
            }
            foreach (String sEdge in dEdges.Keys)
            {
                String[] sSplitedEdge = sEdge.Split(new[] {sEdgeSeparator}, StringSplitOptions.None);
                gNewGraph.AddEdge(sSplitedEdge[0], dEdges[sEdge].ToString(), sSplitedEdge[1]);
            }
            gGraph = gNewGraph;
        }

        public static void makeLostSourcesVisible(GraphData fgdGraphData, bool bShowParameters, bool bShowReturnClass,
                                                  bool bShowNamespace, int iNamespaceDepth)
        {
            foreach (String sNode in fgdGraphData.dNodes.Keys)
                if (false == fgdGraphData.dNodeIsCalledBy.ContainsKey(sNode))
                {
                    fgdGraphData.dNodes[sNode].NodeAttribute.Label = FilteredSignature.filterSignature(sNode,
                                                                                                       bShowParameters,
                                                                                                       bShowReturnClass,
                                                                                                       bShowNamespace,
                                                                                                       iNamespaceDepth);
                    ;
                    fgdGraphData.dNodes[sNode].NodeAttribute.Shape = Shape.Box;
                    if (fgdGraphData.lnEmptyNodes.Contains(fgdGraphData.dNodes[sNode]))
                        fgdGraphData.lnEmptyNodes.Remove(fgdGraphData.dNodes[sNode]);
                }
        }

        public static void consolidateNonVisibleNodes(GraphData fgdGraphData, bool bVerbose)
        {
            O2Timer tTimer = new O2Timer("Created consolidaded graph").start();
            int iPreviousCount = 0;
            int iItemsToRemove = fgdGraphData.iItemsToRemove; // fgdGraphData.lnEmptyNodes.Count - 2;
            //foreach (Node nNodeToRemove in fgdGraphData.lnEmptyNodes)
            while (fgdGraphData.lnEmptyNodes.Count > 0)
            {
                if (iItemsToRemove > -1 && (0 == iItemsToRemove--))
                    break;
                iPreviousCount = fgdGraphData.lnEmptyNodes.Count;
                deleteNodeFromGraph(fgdGraphData, fgdGraphData.lnEmptyNodes[0], true);
                if (fgdGraphData.lnEmptyNodes.Count >= iPreviousCount)
                {
                    DI.log.error("Something went wrong since fgdGraphData.lnEmptyNodes.Count should go down");
                    break;
                }
            }
            //    createGraphWithConsolidatedPaths(ref fgdGraphData.gGraph);
            fgdGraphData.populateXrefDictionaries();
            if (bVerbose)
                tTimer.stop();
        }

        //foreach(Node nNode in fgdGraphData.gGraph.Nodes)
        //    if(false == isNodeASourceOrSink(nNode))
        //        deleteNodeFromGraph(fgdGraphData, nNode);
        /*
            // process forward sequences
            foreach (Edge eEdge in gGraph.Edges)
            {
                Node nSource = (Node)gGraph.FindNode(eEdge.Source);
                Node nTarget = (Node)gGraph.FindNode(eEdge.Target);
                if (dNodeCalls.ContainsKey(nSource.Id) && dNodeIsCalledBy.ContainsKey(nTarget.Id))
                {
                     DI.log.info("Calls:{0} , iscalledby:{1}   : {2} ", dNodeCalls[nSource.Id].Count, dNodeIsCalledBy[nTarget.Id].Count, eEdge.EdgeAttr.Id);
                    if (dNodeCalls[nSource.Id].Count == 1 && dNodeIsCalledBy[nTarget.Id].Count == 1)
                    {
                        if (dNodeCalls[nSource.Id][0].Id == nTarget.Id && dNodeIsCalledBy[nTarget.Id][0].Id == nSource.Id)
                        {
                            // consolidate these two nodes
                            List<Node> lnNodesCalledByTarget = dNodeCalls[nTarget.Id];
                            Edge eSourceToTarget = eEdge;

                            //if (dEdges_TargetSource[nTarget.Id].Count != 1)
                            //{
                            //    return;
                            //}
                            //Edge eTargetToSource = dEdges_TargetSource[nTarget.Id][0];
                            
                            gGraph.Edges.Remove(eSourceToTarget);
                            gGraph.NodeMap.Remove(nTarget.Id);
                            //gGraph.Edges.Remove(eTargetToSource);                            
                       //    foreach(object oNode in gGraph.NodeMap)
                       //     {
                       //         gGraph.NodeMap.Remove(oNode);
                       //     }
                            foreach (Node nNodeCalledByTarget in lnNodesCalledByTarget)
                                gGraph.AddEdge(nSource.Id, nNodeCalledByTarget.Id);
                            //nSource.NodeAttribute.Label = "source";
                            //nTarget.NodeAttribute.Label = "target";
                            return;
                        }
                    }
                }
            }
            return;
             * */
        /*    foreach (String sNode in dNodeCalls.Keys)
            {
                if (dNodeIsCalledBy.ContainsKey(sNode))
                {
                 DI.log.info("Calls:{0} , iscalledby:{1}   : {2} ", dNodeCalls[sNode].Count, dNodeIsCalledBy[sNode].Count, sNode);
                if (dNodeCalls[sNode].Count == 1 && dNodeIsCalledBy[sNode].Count ==1)
                {
                    Node nNode = (Node)gGraph.FindNode(sNode);
                    nNode.NodeAttribute.Label = "Can be consolidated";
                }
                }
            }*/
        /*foreach (Edge eEdge in gGraph.Edges)
            {
                Node nSource = (Node)gGraph.FindNode(eEdge.Source);
                Node nTarget = (Node)gGraph.FindNode(eEdge.Target);
                if (nSource.NodeAttribute.Label == "" && nTarget.NodeAttribute.Label == "")
                {
              //      nSource.NodeAttribute.Label = "SOURCE TO CONSOLIDATE";
              //      nTarget.NodeAttribute.Label = "Target TO CONSOLIDATE";
                }
            }*/
        //}


        public static void consolidateEdge(GraphData fgdGraphData, Edge eEdge)
        {
        }

        public static void deleteEdgeFromGraph(GraphData fgdGraphData, Edge eEdgeToDelete)
        {
        }


        public static void deleteNodeFromGraph(GraphData fgdGraphData, Node nNodeToDelete, bool bPopulateXRefs)
        {
            String sNodeToDelete = nNodeToDelete.Id;
            Graph g = fgdGraphData.gGraph;
            if (fgdGraphData.gGraph.NodeMap.ContainsKey(sNodeToDelete))
            {
                List<Node> lnNodeCalls = (fgdGraphData.dNodeCalls.ContainsKey(sNodeToDelete))
                                             ? fgdGraphData.dNodeCalls[sNodeToDelete]
                                             : new List<Node>();
                List<Node> lnNodeIsCalledBy = (fgdGraphData.dNodeIsCalledBy.ContainsKey(sNodeToDelete))
                                                  ? fgdGraphData.dNodeIsCalledBy[sNodeToDelete]
                                                  : new List<Node>();
                // List<Nodes> lnNodeIsCalledBy = fgdGraphData.dNodeIsCalledBy[sNodeToDelete]
                foreach (Node nCallsCurrentNode in lnNodeIsCalledBy)
                    foreach (Node nIsCalledByCurrentNode in lnNodeCalls)
                        if (nCallsCurrentNode.Id != nIsCalledByCurrentNode.Id) // don't add a call to itself
                        {
                            // if (nIsCalledByCurrentNode != null && nCallsCurrentNode != null)
                            //if (false == fgdGraphData.dNodeCalls.ContainsKey(nCallsCurrentNode.Id))
                            if (false == fgdGraphData.ContainsEdge(nCallsCurrentNode.Id, nIsCalledByCurrentNode.Id))
                            {
                                //fgdGraphData.dEdges_SourceTarget.Add(nCallsCurrentNode.Id, new List<Edge>());
                                //fgdGraphData.dEdges_SourceTarget[nCallsCurrentNode.Id].Add((Edge)

                                var eNewEdge =
                                    (Edge)
                                    fgdGraphData.gGraph.AddEdge(nCallsCurrentNode.Id, "", nIsCalledByCurrentNode.Id);
                                fgdGraphData.lsEdges.Add(fgdGraphData.getEdgeString(eNewEdge));
                            }
                        }

                if (fgdGraphData.dEdges_SourceTarget.ContainsKey(sNodeToDelete))
                    foreach (Edge eEdge_NodeCalls in fgdGraphData.dEdges_SourceTarget[sNodeToDelete])
                        fgdGraphData.gGraph.Edges.Remove(eEdge_NodeCalls);

                if (fgdGraphData.dEdges_TargetSource.ContainsKey(sNodeToDelete))
                    foreach (Edge eEdge_IsCalledBy in fgdGraphData.dEdges_TargetSource[sNodeToDelete])
                        fgdGraphData.gGraph.Edges.Remove(eEdge_IsCalledBy);

                fgdGraphData.gGraph.NodeMap.Remove(sNodeToDelete);
                if (bPopulateXRefs)
                    fgdGraphData.populateXrefDictionaries(); // check for performance probs in big graphs
            }
            /* 
            if (g.NodeMap.ContainsKey(deleteNodeName))
            {
                //remove node
                g.NodeMap.Remove(deleteNodeName);
                //find all edges referring to the deleted node
                ArrayList removePositions = new ArrayList();
                foreach (Edge edge in g.Edges)
                {
                    if (edge.Source == deleteNodeName || edge.Target == deleteNodeName)
                    {
                        int pos = g.Edges.IndexOf(edge);
                        removePositions.Add(pos);
                    }
                }

                //remove all found edges
                int count = 0;
                foreach (int pos in removePositions)
                {
                    g.Edges.RemoveAt(pos - count);
                    count++;
                }
            } */
        }

        public static bool isNodeASourceOrSink(Node nNode)
        {
            if (((TreeNode) nNode.UserData).Tag.GetType().Name == "CallInvocation")
            {
                var ciCallInvocation = (CallInvocation) ((TreeNode) nNode.UserData).Tag;
                switch (ciCallInvocation.trace_type)
                {
                    case (Int32) TraceType.Source:
                    case (Int32) TraceType.Lost_Sink:
                    case (Int32) TraceType.Known_Sink:
                        return true;
                }
            }
            return false;
        }

        #region Nested type: GraphData

        public class GraphData
        {
            public bool bShouldGraphBeShown = true;
            public Color cSelectedFillColor = Color.Red;
            public Dictionary<String, List<Edge>> dEdges_SourceTarget; // = new Dictionary<string, List<Edge>>();
            public Dictionary<String, List<Edge>> dEdges_TargetSource; // = new Dictionary<string, List<Edge>>();
            public Dictionary<String, List<Node>> dNodeCalls; // = new Dictionary<string, List<Node>>();
            public Dictionary<String, List<Node>> dNodeIsCalledBy; // = new Dictionary<string, List<Node>>();
            public Dictionary<String, Node> dNodes;
            public Graph gGraph;
            public int iItemsToRemove = -1;
            public int iMaxEdges = 2000;
            public int iMaxNodes = 200;
            public int iMaxToPlot = 80;
            public List<Node> lnEmptyNodes; // = new List<Node>();
            public List<String> lsEdges;
            public String sEdgeGlueString = "->";

            public GraphData(Graph gGraph)
            {
                this.gGraph = gGraph;
                populateXrefDictionaries();
            }

            public void populateXrefDictionaries()
            {
                lsEdges = new List<string>();
                dNodes = new Dictionary<string, Node>();
                dNodeCalls = new Dictionary<string, List<Node>>();
                dNodeIsCalledBy = new Dictionary<string, List<Node>>();
                dEdges_TargetSource = new Dictionary<string, List<Edge>>();
                dEdges_SourceTarget = new Dictionary<string, List<Edge>>();
                lnEmptyNodes = new List<Node>();
                foreach (Edge eEdge in gGraph.Edges)
                {
                    lsEdges.Add(getEdgeString(eEdge));
                    if (gGraph.Edges.Count > iMaxEdges)
                    {
                        DI.log.debug("Aborting, graph has reached max number of Edges: {0}", gGraph.Edges.Count);
                        return;
                    }
                    if (gGraph.NodeCount > iMaxNodes)
                    {
                        DI.log.debug("Aborting, graph has reached max number of Nodes: {0}", gGraph.NodeCount);
                        return;
                    }
                    // populate source to target edges dictionary
                    if (false == dEdges_SourceTarget.ContainsKey(eEdge.Source))
                        dEdges_SourceTarget.Add(eEdge.Source, new List<Edge>());
                    if (eEdge != null)
                        dEdges_SourceTarget[eEdge.Source].Add(eEdge);

                    // populate reversed target to source edge mapping (the source -> target mapping is provided by the gGraph.Edges list)                    
                    if (false == dEdges_TargetSource.ContainsKey(eEdge.Target))
                        dEdges_TargetSource.Add(eEdge.Target, new List<Edge>());

                    if (eEdge != null)
                        dEdges_TargetSource[eEdge.Target].Add(eEdge);

                    // populate Dictionary with called made by this node
                    if (false == dNodeCalls.ContainsKey(eEdge.Source))
                        dNodeCalls.Add(eEdge.Source, new List<Node>());
                    var nNodeCall = (Node) gGraph.FindNode(eEdge.Target);
                    if (nNodeCall != null)
                        dNodeCalls[eEdge.Source].Add(nNodeCall);
                    else
                    {
                    }

                    // populate Dictionary with Nodes that call this node
                    if (false == dNodeIsCalledBy.ContainsKey(eEdge.Target))
                        dNodeIsCalledBy.Add(eEdge.Target, new List<Node>());
                    var nNodeIsCalledBy = (Node) gGraph.FindNode(eEdge.Source);
                    if (nNodeIsCalledBy != null)
                        dNodeIsCalledBy[eEdge.Target].Add(nNodeIsCalledBy);
                    else
                    {
                    }
                }
                // calculate empty nodes
                foreach (DictionaryEntry deNode in gGraph.NodeMap)
                {
                    var nNode = (Node) deNode.Value;
                    dNodes.Add(nNode.Id, nNode);
                    if (nNode.NodeAttribute.Label == "")
                        lnEmptyNodes.Add(nNode);
                }
            }

            public String getEdgeString(Edge eEdge)
            {
                return getEdgeString(eEdge.Source, eEdge.Target);
            }

            public String getEdgeString(String sSource, String sTarget)
            {
                return String.Format("{0} -> {1}", sSource, sTarget);
            }

            public bool ContainsEdge(String sSource, String sTarget)
            {
                return lsEdges.Contains(getEdgeString(sSource, sTarget));
            }
        }

        #endregion
    }
}
