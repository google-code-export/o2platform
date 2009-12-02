using System;

namespace O2.Kernel.Interfaces.CIR
{
    public interface ICirFunctionVariable
    {
        String defSymbol { get; set; }
        int iGuaranteedInitBeforeUsed { get; set; }
        String refSymbol { get; set; }
        String sName { get; set; }
        String sPrintableType { get; set; }
        String sSymbolDef { get; set; }
        String sSymbolRef { get; set; }
        String sUniqueID { get; set; }
    }
}