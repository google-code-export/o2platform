using System.Collections.Generic;
using O2.Kernel.Interfaces.FrameworkSupport.J2EE;
using O2.Kernel.Interfaces.O2Findings;

namespace O2.Kernel.Interfaces.XRules
{
    public interface IXRule_J2EE_Struts
    {
        List<IO2Finding> execute(IStrutsConfigXml strutsConfigXml);
    }
}