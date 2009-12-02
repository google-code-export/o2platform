using O2.Kernel;
using O2.Kernel.Interfaces;
using O2.Kernel.Interfaces.O2Core;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.Legacy.OunceV6
{
    class DI
    {
        static DI()
        {
            log = PublicDI.log;
            config = PublicDI.config;
        }


        public static IO2Log log;
        public static IO2Config config;
        
    }
}