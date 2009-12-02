using System;
using O2.Kernel.Interfaces.CIR;

namespace O2.Core.CIR.CirObjects
{
    [Serializable]
    public class FieldMember : ICirFieldMember // in java these are static non local vars
    {
        public String Name { get; set; }
        public String PrintableType { get; set; }
        public String SymbolRef { get; set; }
    }
}