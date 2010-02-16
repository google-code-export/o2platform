// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Interfaces.CIR;

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
