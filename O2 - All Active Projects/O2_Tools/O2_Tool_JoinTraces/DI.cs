using O2.Kernel;
using O2.Kernel.Interfaces;
using O2.Kernel.Interfaces.O2Core;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.Tool.JoinTraces
{
    class DI
    {
        public static IO2Log log = PublicDI.log;// new KO2Log();
        public static IO2Config config = PublicDI.config; //new KO2Config();
    }
}