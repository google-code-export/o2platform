// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.Cmd.SpringMvc.Objects;
using O2.DotNetWrappers.O2Findings;
using O2.DotNetWrappers.Windows;
using O2.Interfaces.CIR;
using O2.Interfaces.O2Findings;
using O2.Views.ASCX.O2Findings;

namespace O2.Cmd.SpringMvc.Classes
{
    public class CreateFindingsFromMvcData
    {
        public static List<string> javaxGetParameterSignatures = new List<string>{
            "javax.servlet.ServletRequest.getParameter(java.lang.String):java.lang.String", 
            "javax.servlet.http.HttpServletRequest.getParameter(java.lang.String):java.lang.String"};

        public static void createFindingsFromSpringMvcMappings(bool createFindingForUsesOfModelAttribute,
                                                         bool createFindingForUsesOfGetParameter,
                                                         ascx_FindingsViewer findingsViewer,
                                                         Dictionary<SpringMvcController, TreeNode> treeNodesForloadedSpringMvcControllers, ICirData cirData) //IEnumerable<SpringMvcController> springMvcControllers)
        {
            var findingsCreated = new List<IO2Finding>();

            if (createFindingForUsesOfModelAttribute)
            {
                foreach (SpringMvcController springMvcController in treeNodesForloadedSpringMvcControllers.Keys)
                {
                    var modelAttributeParameter = SpringMvcUtils.getMethodUsedInController(springMvcController, "ModelAttribute");
                    if (modelAttributeParameter != null)
                    {
                        var findingType = "SpringMvc.Use of ModelAttribute";
                        var findingText = string.Format("{0} {1} {2}", springMvcController.HttpRequestMethod,
                                                        springMvcController.HttpRequestUrl,
                                                        springMvcController.HttpMappingParameter);
                        var o2Finding = new O2Finding(findingText, findingType)
                        {
                            file = springMvcController.FileName,
                            lineNumber = springMvcController.LineNumber
                        };
                        var rootTrace = new O2Trace(findingType);
                        var sourceTrace = new O2Trace(springMvcController.HttpRequestUrl) { traceType = TraceType.Source };
                        var modelAttribute = new O2Trace("ModelAttribute Class: " + modelAttributeParameter.className);
                        var sinkTrace = new O2Trace(springMvcController.JavaClass) { traceType = TraceType.Known_Sink };
                        var postTrace = new O2Trace(springMvcController.JavaClassAndFunction);
                        rootTrace.childTraces.Add(sourceTrace);
                        sourceTrace.childTraces.Add(modelAttribute);
                        modelAttribute.childTraces.Add(sinkTrace);
                        sinkTrace.childTraces.Add(postTrace);
                        o2Finding.o2Traces.Add(rootTrace);

                        rootTrace.file = sourceTrace.file = sinkTrace.file = o2Finding.file;
                        rootTrace.lineNumber = sourceTrace.lineNumber = sinkTrace.lineNumber = o2Finding.lineNumber;

                        findingsCreated.Add(o2Finding);
                        //tvControllers.Nodes.Add(
                        //    O2Forms.cloneTreeNode(treeNodesForloadedSpingMvcControllers[springMcvController]));
                    }
                }
            }

            if (createFindingForUsesOfGetParameter)
            {
                try
                {
                    var nodesWithGetParameter = getNodes_ThatUseGetParameter_RecursiveSearch(cirData, treeNodesForloadedSpringMvcControllers);
                    foreach (var treeNode in nodesWithGetParameter)
                    {
                        var springMvcController = (SpringMvcController)treeNode.Tag;
                        /*var o2Finding = new O2Finding(springMvcController.JavaFunction, "SpringMvc.Use of GetParameter")
                                            {
                                                file = springMvcController.FileName,
                                                lineNumber = springMvcController.LineNumber
                                            };
                        findingsCreated.Add(o2Finding);*/
                        var findingType = "SpringMvc.Use of GetParameter";
                        var findingText = string.Format("{0} {1} {2}", springMvcController.HttpRequestMethod,
                                                        springMvcController.HttpRequestUrl,
                                                        springMvcController.HttpMappingParameter);
                        var o2Finding = new O2Finding(findingText, findingType)
                        {
                            file = springMvcController.FileName,
                            lineNumber = springMvcController.LineNumber
                        };
                        var rootTrace = new O2Trace(findingType);
                        var sourceTrace = new O2Trace(springMvcController.HttpRequestUrl) { traceType = TraceType.Source };
                        var sinkTrace = new O2Trace(springMvcController.JavaClass) { traceType = TraceType.Known_Sink };
                        var postTrace = new O2Trace(springMvcController.JavaClassAndFunction);
                        rootTrace.childTraces.Add(sourceTrace);
                        sourceTrace.childTraces.Add(sinkTrace);
                        sinkTrace.childTraces.Add(postTrace);
                        o2Finding.o2Traces.Add(rootTrace);

                        rootTrace.file = sourceTrace.file = sinkTrace.file = o2Finding.file;
                        rootTrace.lineNumber = sourceTrace.lineNumber = sinkTrace.lineNumber = o2Finding.lineNumber;

                        findingsCreated.Add(o2Finding);
                    }
                }
                catch (Exception ex)
                {
                    DI.log.ex(ex, "in createFindingForUsesOfGetParameter");
                }

            }


            //            findingsCreated.Add(o2Finding);

            findingsViewer.clearO2Findings();
            findingsViewer.loadO2Findings(findingsCreated);
        }


        public static TreeNode[] getNodes_ThatUseGetParameter_RecursiveSearch(ICirData cirdata, Dictionary<SpringMvcController, TreeNode> treeNodesForloadedSpringMvcControllers)
        {
            var nodes = new List<TreeNode>();
            foreach (SpringMvcController springMcvController in treeNodesForloadedSpringMvcControllers.Keys)
                if (SpringMvcUtils.doesControllerFunctionCallFunction(cirdata, springMcvController, javaxGetParameterSignatures, true))
                    nodes.Add(O2Forms.cloneTreeNode(treeNodesForloadedSpringMvcControllers[springMcvController]));
            return nodes.ToArray();
        }
    }
}
