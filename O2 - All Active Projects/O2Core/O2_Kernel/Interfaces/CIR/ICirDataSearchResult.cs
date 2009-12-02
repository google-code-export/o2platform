using System;
using System.Collections.Generic;

namespace O2.Kernel.Interfaces.CIR
{
    public interface ICirDataSearchResult
    {
        ICirDataAnalysis fcdAnalysis { get; set; }
        List<ICirClass> lccTargetCirClasses { get; set; }
        List<string> lsResult_CallsMade { get; set; }
        List<string> lsResult_CallsMadeToExternalMethods { get; set; }
        List<string> lsResult_CallsMadeToExternalMethods_DontHaveDbMapping { get; set; }
        List<string> lsResult_CallsMadeToExternalMethods_HaveDbMapping { get; set; }
        List<string> lsResult_Classes { get; set; }
        List<string> lsResult_Classes_WithControlFlowGraphs { get; set; }
        List<string> lsResult_Functions { get; set; }
        List<string> lsResult_Functions_DontHaveDbMapping { get; set; }
        List<string> lsResult_Functions_HaveDbMapping { get; set; }
        List<string> lsResult_Functions_WithControlFlowGraphs { get; set; }
        void clearResultVars();       
    }
}