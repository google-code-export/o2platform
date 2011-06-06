// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Collections.Generic;
using System.Threading;
using FluentSharp.O2.Interfaces.O2Findings;

namespace FluentSharp.O2.Interfaces.XRules
{
    public interface IXRule_Generic
    {
        List<IO2Finding> execute(params object[] artifacts);
    }
}