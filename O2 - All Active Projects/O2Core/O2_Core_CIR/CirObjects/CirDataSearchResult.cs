using System;
using System.Collections.Generic;
using O2.Kernel.Interfaces.CIR;

namespace O2.Core.CIR.CirObjects
{
    [Serializable]
    public class CirDataSearchResult : ICirDataSearchResult
    {
        public ICirDataAnalysis fcdAnalysis { get; set; }
        public List<ICirClass> lccTargetCirClasses { get; set; }
        public List<string> lsResult_CallsMade { get; set; }
        public List<string> lsResult_CallsMadeToExternalMethods { get; set; }
        public List<string> lsResult_CallsMadeToExternalMethods_DontHaveDbMapping { get; set; }
        public List<string> lsResult_CallsMadeToExternalMethods_HaveDbMapping { get; set; }

        public List<string> lsResult_Classes { get; set; }
        public List<string> lsResult_Classes_WithControlFlowGraphs { get; set; }
        public List<string> lsResult_Functions { get; set; }
        public List<string> lsResult_Functions_DontHaveDbMapping { get; set; }
        public List<string> lsResult_Functions_HaveDbMapping { get; set; }
        public List<string> lsResult_Functions_WithControlFlowGraphs { get; set; }


        public CirDataSearchResult(ICirDataAnalysis fcdAnalysis)
        {
            this.fcdAnalysis = fcdAnalysis;
        }

        public void clearResultVars()
        {
            lsResult_Classes = new List<string>();
            lsResult_Classes_WithControlFlowGraphs = new List<string>();
            lsResult_Functions = new List<string>();
            lsResult_Functions_WithControlFlowGraphs = new List<string>();
            lsResult_Functions_HaveDbMapping = new List<string>();
            lsResult_Functions_DontHaveDbMapping = new List<string>();
            lsResult_CallsMade = new List<string>();
            lsResult_CallsMadeToExternalMethods = new List<string>();
            lsResult_CallsMadeToExternalMethods_HaveDbMapping = new List<string>();
            lsResult_CallsMadeToExternalMethods_DontHaveDbMapping = new List<string>();
        }

     
    }
}