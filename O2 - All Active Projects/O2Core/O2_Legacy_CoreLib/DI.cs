// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Legacy.CoreLib.O2Core.O2Environment;
using O2.Kernel;
using O2.Kernel.Interfaces.O2Core;
using O2.Kernel.Interfaces.Views;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.Legacy.CoreLib
{
    internal class DI
    {
        public static IO2Log log = PublicDI.log;
        public static O2CorLibConfig o2CorLibConfig = new O2CorLibConfig();

        public static IReflection reflection = PublicDI.reflection;
        
    }
}
