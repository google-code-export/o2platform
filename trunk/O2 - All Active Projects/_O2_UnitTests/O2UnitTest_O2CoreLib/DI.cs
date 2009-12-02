// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
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
