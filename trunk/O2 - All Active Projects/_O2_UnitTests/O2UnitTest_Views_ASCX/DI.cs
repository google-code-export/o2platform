using O2.Kernel;
using O2.Kernel.Interfaces.O2Core;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.UnitTests.Test_O2ViewsASCX
{
    class DI
    {
        static DI()
        {
            config = PublicDI.config;
            log = PublicDI.log;
            reflection = PublicDI.reflection;
        }

        public static IO2Config config;
        public static IO2Log log;
        public static IReflection reflection;
    }
}