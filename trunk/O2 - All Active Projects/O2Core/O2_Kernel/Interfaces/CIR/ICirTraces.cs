using System.Collections.Generic;
using O2.Kernel.Interfaces.O2Findings;

namespace O2.Kernel.Interfaces.CIR
{
    // will hold the findings that 
    public interface ICirTraces
    {
        List<IO2Finding> IsSink { get; set; }
        List<IO2Finding> IsSource { get; set; }
    }
}
