using O2.Debugger.Mdbg.O2Debugger.Objects;
using O2.Kernel;
using O2.Kernel.Interfaces;
using O2.Kernel.Interfaces.ExternalDlls;
using O2.Kernel.Interfaces.O2Core;
using O2.Kernel.InterfacesBaseImpl;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.Debugger.Mdbg
{
    internal class DI
    {
        static DI()
        {
            log = PublicDI.log;            
            config = new KO2Config();
            reflection = new KReflection();
            showMdbgDebugMessages = false;
        }

        // DI Targets
        public static IReflection reflection { get; set; }
        public static IO2Log log { get; set; }        
        public static IO2Config config { get; set; }

        public static IO2MonoCecil o2MonoCecil;


        // Global Objects
        public static O2MDbg o2MDbg { get; set; }
        public static bool showMdbgDebugMessages { get; set; }
    }
}