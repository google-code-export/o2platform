using System;
using System.Collections.Generic;
using o2.aspnetanalysis;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.Xsd;
using O2.DotNetWrappers.O2Findings;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Kernel.Interfaces.O2Findings;

namespace o2.CirAnalysis.DotNet
{
    public class AspNetAnalysis
    {
        public static List<IO2Finding> findParameterStaticValueInMethodX(CirData cirData)
        {
            string functionToFind = "System.Web.UI.WebControls.Button.add_Click(System.EventHandler):void";
            var createdO2Findings = new List<IO2Finding>();
            try
            {
                if (cirData.dFunctions_bySignature.ContainsKey(functionToFind))
                {
                    var function = cirData.dFunctions_bySignature[functionToFind];
                    foreach (CirFunction functionXRef in function.FunctionIsCalledBy)
                    {
                        //var functionXRef = cirData.dFunctions_bySignature[functionXRefName];
                        foreach (var basicBlock in functionXRef.lcfgBasicBlocks)
                        {
                            if (basicBlock != null && basicBlock.Items != null)
                                for (var i = 0; i < basicBlock.Items.Length; i++)
                                {
                                    var item = basicBlock.Items[i];
                                    if (item.GetType().Name == "ControlFlowGraphBasicBlockEvalExprStmt")
                                    {
                                        var evalExprStmt = (ControlFlowGraphBasicBlockEvalExprStmt)item;
                                        if (evalExprStmt.NaryCallVirtual != null && evalExprStmt.NaryCallVirtual.FunctionName == functionToFind)
                                        {
                                            // go to the previous block
                                            var evalExprStmtWithStaticParameterValue = (ControlFlowGraphBasicBlockEvalExprStmt)basicBlock.Items[i - 1];
                                            if (evalExprStmtWithStaticParameterValue.NaryCall != null && evalExprStmtWithStaticParameterValue.NaryCall.UnaryOprCast != null && evalExprStmtWithStaticParameterValue.NaryCall.UnaryOprCast.Length == 2)
                                            {

                                                string staticParameterValue =
                                                    evalExprStmtWithStaticParameterValue.NaryCall.UnaryOprCast[1].
                                                        ConstFunctionAddress.FunctionName;
                                                var o2Finding = new O2Finding(staticParameterValue, "Asp.NET Event Mapping") { o2Traces = new List<IO2Trace>{ new O2Trace(functionXRef.FunctionSignature, TraceType.Root_Call)} };
                                                var sourceTrace = new O2Trace(functionXRef.ParentClass.Signature, functionXRef.FunctionSignature, TraceType.Source);
                                                var sinkTrace = new O2Trace(staticParameterValue, TraceType.Known_Sink)
                                                                    {
                                                                        context =
                                                                            "this . HacmeBank_v2_Website.ascx.PostMessageForm.btnPostMessage_Click ( sender, e )"
                                                                    };
                                                sourceTrace.childTraces.Add(sinkTrace);
                                                o2Finding.o2Traces[0].childTraces.Add(sourceTrace);
                                                createdO2Findings.Add(o2Finding);
                                                DI.log.debug("{0} -- > {1}", functionXRef.FunctionSignature, staticParameterValue);
                                            }
                                        }
                                    }
                                }
                        }

                    }                  
                    return createdO2Findings;
                }
            }
            catch (Exception ex)
            {
                DI.log.debug("in findParameterStaticValueInMethodX :{0}:", ex.Message);
            }


            return null;
        }



        public static List<IO2Finding> glueClickButtonTraces(String ClickButtonMappingOzasmt, String webLayerOzasmt, String webServicesLayerOzasmt)
        {
            var results = new List<IO2Finding>();

            var clickButton = new O2Assessment(new O2AssessmentLoad_OunceV6(), ClickButtonMappingOzasmt);
            var webLayer = new O2Assessment(new O2AssessmentLoad_OunceV6(), webLayerOzasmt);
//            var webServices = new O2Assessment(new O2AssessmentLoad_OunceV6(), webServicesLayerOzasmt);

            var webLayerAllTraces = OzasmtUtils.getDictionaryWithO2AllSubTraces(webLayer);
            var count = webLayerAllTraces.Count;
            foreach (var clickButtonFinding in clickButton.o2Findings)
            {
                var sinkToFind = OzasmtUtils.getKnownSink(clickButtonFinding.o2Traces).signature;
                if (webLayerAllTraces.ContainsKey(sinkToFind))
                    foreach (var webLayerO2Trace in webLayerAllTraces[sinkToFind])
                        results.Add(OzasmtGlue.createCopyAndGlueTraceSinkWithSource(clickButtonFinding, webLayerO2Trace));
            }
            DI.log.debug(" {0} findings in result ", results.Count);
            return results;
        }

        public static List<IO2Finding> mapTextBoxWebControlsAsSinks(List<IO2Finding> findingsToMap)
        {
            var signatureToFind = "System.Web.UI.WebControls.TextBox.get_Text():string";
            var results = new List<IO2Finding>();
            foreach(var o2Finding in findingsToMap)
            {
                var o2Match = OzasmtSearch.findO2TraceSignature(o2Finding.o2Traces, signatureToFind);
                if (o2Match != null)
                {
                    var variable = o2Match.context.Substring(0, o2Match.context.IndexOf(' '));
                    variable = variable.Replace("this->", "");

                    var source = OzasmtUtils.getSource(o2Finding.o2Traces);
                    source.signature += "_" + variable;
                    //var o2NewO2Trace = new O2Trace(variable);
                    //o2NewO2Trace.childTraces.Add(o2Finding.o2Trace);
                    //o2Finding.o2Trace = o2NewO2Trace;
                    results.Add(o2Finding);
                }
            }
            return results;
        }

    }
}
