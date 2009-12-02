using System;
using System.Collections.Generic;

namespace O2.Kernel.Interfaces.CIR
{
    public interface ICirFieldClass
    {
        Dictionary<String, String> dFieldData { get; set; }
        int GuaranteedInitBeforeUsed { get; set; }
        String Name { get; set; }
        String PrintableType { get; set; }
        String Signature { get; set; }
        String SymbolRef { get; set; }
    }
}