using System;

namespace O2.Kernel.Interfaces.CIR
{
    public interface ICirFieldMember
    {
        String Name { get; set; }
        String PrintableType { get; set; }
        String SymbolRef { get; set; }
    }
}