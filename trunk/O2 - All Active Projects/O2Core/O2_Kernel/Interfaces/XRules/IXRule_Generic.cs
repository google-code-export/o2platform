using System.Collections.Generic;
using System.Threading;
using O2.Kernel.Interfaces.O2Findings;

namespace O2.Kernel.Interfaces.XRules
{
    public interface IXRule_Generic
    {
        List<IO2Finding> execute(params object[] artifacts);
    }
}