using System;
using System.Collections.Generic;
using System.IO;
using O2.Core.CIR;
using O2.Core.CIR.CirUtils;
using O2.Core.CIR.CirUtils;
using O2.Core.CIR.CirUtils;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Kernel.Interfaces.CIR;

namespace O2.Core.CIR.CirObjects
{
    [Serializable]
    public class CirDataAnalysis : ICirDataAnalysis
    {
        public Dictionary<ICirClass, ICirData> dCirClass { get; set; }
        public Dictionary<string, ICirClass> dCirClass_bySignature { get; set; }
        public Dictionary<string, ICirData> dCirDataFilesLoaded { get; set; }
        public Dictionary<ICirFunction, ICirClass> dCirFunction { get; set; }
        public Dictionary<string, ICirFunction> dCirFunction_bySignature { get; set; }
        public bool onlyShowFunctionsOrClassesWithControlFlowGraphs { get; set; }
        public bool onlyShowExternalFunctionsThatAreInvokedFromCFG { get; set; }
        public bool onlyShowFunctionsWithCallersOrCallees { get; set; }
        //public Dictionary<String, CirData> dTargetO2CirDataFiles = new Dictionary<String, CirData>();
        //public List<CirClass> lccTargetCirClasses = new List<CirClass>();      
        //private List<CirDataSearchResult> fcdSearchResults = new List<CirDataSearchResult>();

        public List<String> lCirClass_bySuperClass { get; set; }
        public String sDb_Id { get; set; }

        public CirDataAnalysis()
        {
            CirDataAnalysisUtils.clear(this);
            onlyShowFunctionsWithCallersOrCallees = false;
            onlyShowFunctionsOrClassesWithControlFlowGraphs = true;         // default to true;
            onlyShowExternalFunctionsThatAreInvokedFromCFG = false;
        }

        public CirDataAnalysis(ICirData cirData) :this()
        {
            CirDataAnalysisUtils.addO2CirDataFile(this, cirData);
        }

               
        public List<T> CirClasses<T>()
        {    
            var cirClasses = new List<T>();            
            switch (typeof(T).Name)
            {
                case "ICirClass":                    
                    foreach (var cirClass in dCirClass_bySignature.Values)
                        cirClasses.Add((T) cirClass);
                    break;
                case "String":
                    foreach (string cirClass in dCirClass_bySignature.Keys)
                    {                        
                        cirClasses.Add((T)(object)cirClass);
                    }
                    break;
            }            
            return cirClasses;                    
        }
        /// <summary>
        /// This will return a List of funtions that match this criteria
        /// if (cirFunction.FunctionIsCalledBy.Count > 0 || cirFunction.FunctionsCalledUniqueList.Count >0)
        ///    if ((false == onlyShowFunctionsOrClassesWithControlFlowGraphs && false == onlyShowExternalFunctionsThatAreInvokedFromCFG) ||
        ///          (onlyShowFunctionsOrClassesWithControlFlowGraphs && cirFunction.HasControlFlowGraph) ||
        ///          (onlyShowExternalFunctionsThatAreInvokedFromCFG && false == cirFunction.HasControlFlowGraph &&
        ///          cirFunction.FunctionIsCalledBy.Count > 0))
        /// </summary>
        public List<T> CirFunctions<T>()
        {
            var cirFunctions = new List<T>();

            // if all are false return Raw list (which are ALL current functions)
            if (onlyShowFunctionsWithCallersOrCallees == false && onlyShowExternalFunctionsThatAreInvokedFromCFG == false && onlyShowFunctionsOrClassesWithControlFlowGraphs == false)
                return CirFunctions_RawList<T>();

            foreach (var cirFunction in dCirFunction_bySignature.Values)
            {
                // first exclude the functions that have no callers or callees  (the onlyShowFunctionsWithCallersOrCallees is done by default)
                if (cirFunction.FunctionIsCalledBy.Count > 0 || cirFunction.FunctionsCalledUniqueList.Count > 0)
                    // then apply the filters
                    if (
                        // if both are false (onlyShowFunctionsOrClassesWithControlFlowGraphs and onlyShowExternalFunctionsThatAreInvokedFromCFG)
                        (false == onlyShowFunctionsOrClassesWithControlFlowGraphs &&
                         false == onlyShowExternalFunctionsThatAreInvokedFromCFG) ||
                        // if onlyShowFunctionsOrClassesWithControlFlowGraphs and this function has a Control Flow Graph
                        (onlyShowFunctionsOrClassesWithControlFlowGraphs && cirFunction.HasControlFlowGraph) ||
                        // if onlyShowExternalFunctionsThatAreInvokedFromCFG there is NO Control flow Graph and, there is at lease one other function that calls this function
                        (onlyShowExternalFunctionsThatAreInvokedFromCFG && false == cirFunction.HasControlFlowGraph &&
                         cirFunction.FunctionIsCalledBy.Count > 0))
                        switch (typeof (T).Name)
                        {
                            case "ICirFunction":
                                cirFunctions.Add((T) cirFunction);
                                break;
                            case "String":
                                cirFunctions.Add((T) (object) cirFunction.FunctionSignature);
                                break;
                        }
            }
            return cirFunctions;
        }

        public bool Save(string savedCirDataFile)
        {
            Files.deleteFile(savedCirDataFile);
            var timer = new O2Timer("Saving CirData").start();
            CirDataAnalysisUtils.saveCirDataAnalysisObjectAsCirDataFile(this, savedCirDataFile);
            timer.stop();
            return File.Exists(savedCirDataFile);
        }

        /// <summary>
        /// This will return a List of all currently loaded functions
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> CirFunctions_RawList<T>()
        {
            var cirFunctions = new List<T>();
            foreach (var cirFunction in dCirFunction_bySignature.Values)
                switch (typeof(T).Name)
                {
                    case "ICirFunction":
                        cirFunctions.Add((T)cirFunction);
                        break;
                    case "String":
                        cirFunctions.Add((T)(object)cirFunction.FunctionSignature);
                        break;
                }
            return cirFunctions;
        }
    }
}