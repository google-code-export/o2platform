// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
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
