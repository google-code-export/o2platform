// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using O2.Core.CIR.Xsd;
using O2.Interfaces.CIR;

namespace O2.Core.CIR.CirObjects
{
    [Serializable]
    public class FunctionVariable : ICirFunctionVariable
    {
        public String defSymbol {get;set;}
        public int iGuaranteedInitBeforeUsed { get; set; }
        public String refSymbol { get; set; }
        public String sName { get; set; }
        public String sPrintableType { get; set; }
        public String sSymbolDef { get; set; }
        public String sSymbolRef { get; set; }
        public String sUniqueID { get; set; }

        public FunctionVariable(Variable vVariable, String refSymbol, String defSymbol)
        {
            iGuaranteedInitBeforeUsed = vVariable.GuaranteedInitBeforeUsed;
            sName = vVariable.Name;
            sPrintableType = vVariable.PrintableType;
            sSymbolDef = vVariable.SymbolDef;
            sSymbolRef = vVariable.SymbolRef;
            sUniqueID = vVariable.UniqueID;
            this.refSymbol = refSymbol;
            this.defSymbol = defSymbol;
        }
    }
}
