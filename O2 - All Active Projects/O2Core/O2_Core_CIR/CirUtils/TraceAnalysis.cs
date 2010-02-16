// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using O2.Core.CIR.CirObjects;
using O2.DotNetWrappers.Windows;
using O2.Interfaces.CIR;

namespace O2.Core.CIR.CirUtils
{
    public class TraceAnalysis
    {
        public static void calculateAllTracesFromFunction(String sSignature, TreeNodeCollection tncTraces,
                                                          List<String> lFunctionsCalled,
                                                          String sFilter_Signature, String sFilter_Parameter,
                                                          bool bUseIsCalledBy, ICirDataAnalysis fcdAnalysis)
        {
            TreeNode tnNewTreeNode = O2Forms.newTreeNode(sSignature, sSignature, 0, sSignature);
            tncTraces.Add(tnNewTreeNode);

            if (fcdAnalysis.dCirFunction_bySignature.ContainsKey(sSignature))
            {
                ICirFunction cfCirFunction = fcdAnalysis.dCirFunction_bySignature[sSignature];
                List<ICirFunction> lsFunctions = new List<ICirFunction>();
                if (bUseIsCalledBy)
                    foreach(var cirFunctionCall in cfCirFunction.FunctionIsCalledBy)
                        lsFunctions.Add(cirFunctionCall.cirFunction);
                else
                    lsFunctions.AddRange(cfCirFunction.FunctionsCalledUniqueList);                                               

                foreach (ICirFunction cirFunction in lsFunctions)
                    if (false == lFunctionsCalled.Contains(cirFunction.FunctionSignature))
                    {
                        lFunctionsCalled.Add(cirFunction.FunctionSignature);
                        calculateAllTracesFromFunction(cirFunction.FunctionSignature, tnNewTreeNode.Nodes, lFunctionsCalled,
                                                       sFilter_Signature, sFilter_Parameter, bUseIsCalledBy,
                                                       fcdAnalysis);
                    }
                    else
                        tnNewTreeNode.Nodes.Add("(Circular ref) : " + cirFunction.FunctionSignature);
            }
        }
    }
}
