using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirUtils;
using O2.Kernel.Interfaces.CIR;

namespace O2.Rules.OunceLabs.RulesUtils
{
    public class MiscUtils
    {
        public static List<String> getFunctionsSignaturesFrom02CirData(String sCirDataFile)
        {
            DI.log.debug("sCirDataFile = {0}", sCirDataFile);
            var lsFunctions = new List<string>();
            if (false == File.Exists(sCirDataFile))
            {
                DI.log.error("Provided CirData file does not exist:{0}", sCirDataFile);
                return lsFunctions;
            }


            ICirData fcdCirData = CirLoad.loadSerializedO2CirDataObject(sCirDataFile);

            //var lsResolvedWebMethods = new List<string>();

            Dictionary<String, ICirFunction> dFunctionsWithControlFlowGraphs =
                CirDataUtils.getFunctionsWithControlFlowGraph(fcdCirData);
            // start with all functions that we have a control flow graph for
            foreach (ICirFunction cfCirFunction in dFunctionsWithControlFlowGraphs.Values)
            {
                // first add the current function
                if (false == lsFunctions.Contains(cfCirFunction.FunctionSignature))
                    lsFunctions.Add(cfCirFunction.FunctionSignature);
                // then add all function's called
                foreach (ICirFunction cirFunction in cfCirFunction.FunctionsCalledUniqueList)
                    //if ((false == bOnlyAddSinksIfExternalToCurrentCirDataFile || false == dFunctionsWithControlFlowGraphs.ContainsKey(sCalledFunction)) && false == lsSinksToAdd.Contains(sCalledFunction))
                    if (false == lsFunctions.Contains(cirFunction.FunctionSignature))
                        //     if (sCalledFunction.IndexOf("()") == -1)            // don't add methods that receive no parameters
                        lsFunctions.Add(cirFunction.FunctionSignature);
            }
            return lsFunctions;
        }
    }
}
