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