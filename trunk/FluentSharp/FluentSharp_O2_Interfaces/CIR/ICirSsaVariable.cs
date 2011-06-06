// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;

namespace FluentSharp.O2.Interfaces.CIR
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