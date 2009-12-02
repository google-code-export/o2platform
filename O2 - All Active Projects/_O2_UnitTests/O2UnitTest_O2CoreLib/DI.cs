using O2.Kernel;
using O2.Kernel.Interfaces.O2Core;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.UnitTests.Test_O2CoreLib
{
    internal class DI
    {
        public static IO2Config config = PublicDI.config;
        public static IO2Log log = PublicDI.log;
        public static IReflection reflection = PublicDI.reflection;

        //public static string hardCodedO2DevelopmentLib = @"E:\O2\_SourceCode_O2";
    }
}