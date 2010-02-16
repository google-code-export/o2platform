// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Core.CIR.CirObjects;
using O2.Interfaces.CIR;

namespace O2.Core.CIR.CirUtils
{
    public class CirSearch
    {
        public static void executeSearch(ICirDataSearchResult cirDataSearchResult)
        {
            executeSearch(cirDataSearchResult, new List<String>(cirDataSearchResult.fcdAnalysis.dCirClass_bySignature.Keys));
        }


        public static void executeSearch(ICirDataSearchResult cirDataSearchResult, List<String> lsTargetCirClasses)
        {            
            if (cirDataSearchResult.fcdAnalysis == null)
                DI.log.error("in CirDataSearchResult.executeSearch , fcdAnalysis == null");
            else
            {
                cirDataSearchResult.clearResultVars();
                foreach (String sClass in lsTargetCirClasses)
                {
                    if (cirDataSearchResult.fcdAnalysis.dCirClass_bySignature.ContainsKey(sClass))
                    {
                        cirDataSearchResult.lsResult_Classes.Add(sClass);
                        if (cirDataSearchResult.fcdAnalysis.dCirClass_bySignature[sClass].bClassHasMethodsWithControlFlowGraphs &&
                            false == cirDataSearchResult.lsResult_Classes_WithControlFlowGraphs.Contains(sClass))
                            cirDataSearchResult.lsResult_Classes_WithControlFlowGraphs.Add(sClass);
                        foreach (
                            CirFunction cfCirFunction in cirDataSearchResult.fcdAnalysis.dCirClass_bySignature[sClass].dFunctions.Values)
                        // if (cfCirFunction.HasControlFlowGraph)
                        {
                            cirDataSearchResult.lsResult_Functions.Add(cfCirFunction.FunctionSignature);
                            if (cfCirFunction.HasControlFlowGraph &&
                                false == cirDataSearchResult.lsResult_Functions_WithControlFlowGraphs.Contains(cfCirFunction.FunctionSignature))
                                cirDataSearchResult.lsResult_Functions_WithControlFlowGraphs.Add(cfCirFunction.FunctionSignature);
                            foreach (ICirFunction functionCalled in cfCirFunction.FunctionsCalledUniqueList)
                            {
                                if (false == cirDataSearchResult.lsResult_CallsMade.Contains(functionCalled.FunctionSignature))
                                    // add all functions to this one
                                    cirDataSearchResult.lsResult_CallsMade.Add(functionCalled.FunctionSignature);
                                if (false == cirDataSearchResult.fcdAnalysis.dCirFunction_bySignature.ContainsKey(functionCalled.FunctionSignature))
                                    // only check for functions that we don't have the CIR for
                                    if (false == cirDataSearchResult.lsResult_CallsMadeToExternalMethods.Contains(functionCalled.FunctionSignature))
                                        cirDataSearchResult.lsResult_CallsMadeToExternalMethods.Add(functionCalled.FunctionSignature);
                            }
                        }
                    }
                }            
            }
        }

        public static List<ICirFunction> calculateListOfAllFunctionsCalled(ICirFunction cirFunction)
        {
            var functionsCalled = new List<ICirFunction>();
            if (cirFunction!=null)
                foreach (var functionCalled in cirFunction.FunctionsCalledUniqueList)
                {
                    if (false == functionsCalled.Contains(functionCalled))
                        functionsCalled.Add(functionCalled);
                    functionsCalled.AddRange(calculateListOfAllFunctionsCalled(functionCalled));
                }
            return functionsCalled;
        }
    }
}
