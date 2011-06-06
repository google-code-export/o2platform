// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;

namespace FluentSharp.O2.Interfaces.CIR
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