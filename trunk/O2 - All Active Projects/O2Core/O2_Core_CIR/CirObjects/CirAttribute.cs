using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Kernel.Interfaces.CIR;

namespace O2.Core.CIR.CirObjects
{
    public class CirAttribute : ICirAttribute
    {
        public string AttributeClass { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public CirAttribute(string attributeClass)
        {
            AttributeClass = attributeClass;
            Parameters = new Dictionary<string, string>();
        }
    }
}
