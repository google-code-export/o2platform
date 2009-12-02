using O2.Kernel;
using O2.Kernel.Interfaces.O2Core;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.RnD.SpringMVCAnalyzer
{
    class DI
    {
        public static IO2Log log = PublicDI.log;
        public static IO2Config config = PublicDI.config;
        public static IReflection reflection = PublicDI.reflection;
    }
}