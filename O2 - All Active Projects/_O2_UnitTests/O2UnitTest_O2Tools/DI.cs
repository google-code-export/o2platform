using O2.Kernel;
using O2.Kernel.Interfaces.O2Core;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.UnitTests.Test_O2Tools
{
    internal class DI
    {
        public static KO2Config config = PublicDI.config;        
        public static IO2Log log = PublicDI.log;
        public static IReflection reflection = PublicDI.reflection;
        
    }
}