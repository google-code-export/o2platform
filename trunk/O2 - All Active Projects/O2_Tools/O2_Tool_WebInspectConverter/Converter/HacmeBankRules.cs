using System;
using System.Collections.Generic;
using O2.DotNetWrappers.O2Findings;
using O2.Kernel.Interfaces.O2Findings;

namespace O2.Tool.WebInspectConverter.Converter
{
    public class HacmeBankRules
    {
        public static void mapFunctionInUrlToAscx(List<IO2Finding> findingsToProcess)
        {
            foreach (var o2Finding in findingsToProcess)
            {
                var source = OzasmtUtils.getSource(o2Finding.o2Traces);
                var indexOfFunction = source.signature.IndexOf("function=");
                if (indexOfFunction > 0)
                {
                    var functionCalled = source.signature.Substring(indexOfFunction + 9);
                    functionCalled = functionCalled.ToLower();
                    var currentSink = OzasmtUtils.getKnownSink(o2Finding.o2Traces);
                    currentSink.traceType = TraceType.Root_Call;

                    string newSinkSignature = String.Format("ASP.ascx_{0}_ascx_{1}", functionCalled.Replace('\\', '_'),
                                                            currentSink.signature);
                    currentSink.childTraces.Add(new O2Trace(newSinkSignature, TraceType.Known_Sink));
                }
            }
        }
    }
}