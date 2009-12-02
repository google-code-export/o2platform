using System;
using System.Collections.Generic;
using O2.Kernel.Interfaces.CIR;

namespace O2.Core.CIR.CirObjects
{
    [Serializable]
    public class FieldClass // in java these are static local vars
        : ICirFieldClass
    {
        public Dictionary<String, String> dFieldData { get; set;}
        public int GuaranteedInitBeforeUsed { get; set; }
        public String Name { get; set;}
        public String PrintableType { get; set; }
        public String Signature { get; set;}
        public String SymbolRef { get; set; }

        public FieldClass()
        {
            dFieldData = new Dictionary<string, string>();
        }
    }
}