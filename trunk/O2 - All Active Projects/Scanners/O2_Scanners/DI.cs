using O2.Kernel;
using O2.Kernel.Interfaces;
using O2.Kernel.Interfaces.O2Core;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.Scanners
{
    class DI
    {
        static DI()
        {
            log = PublicDI.log;
            config = PublicDI.config;
        }

        public static IO2Log log { get; set; }
        public static IO2Config config { get; set; }
    }
}