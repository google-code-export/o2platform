using System;

namespace O2.Kernel.Interfaces.CIR
{
    public interface ICirSsaVariable
    {
        String sBaseName { get; set; }
        String sName { get; set; }
        String sPrintableType { get; set; }
        String sSymbolDef { get; set; }
        String sSymbolRef { get; set; }
    }
}