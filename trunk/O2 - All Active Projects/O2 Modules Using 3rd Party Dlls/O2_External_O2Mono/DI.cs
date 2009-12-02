using O2.DotNetWrappers.Windows;
using O2.External.O2Mono.MonoCecil;
using O2.Kernel;
using O2.Kernel.Interfaces.Controllers;
using O2.Kernel.Interfaces.ExternalDlls;
using O2.Kernel.Interfaces.O2Core;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.External.O2Mono
{
    internal class DI
    {
        static DI()
        {
            config = PublicDI.config;
            log = PublicDI.log;
            reflection = new O2FormsReflectionASCX();
            cecilUtils = new CecilUtils();
            monoCecil = new O2MonoCecil();
            assemblyAnalysis = new AssemblyAnalysisImpl();
        }

        public static IO2Config config { get; set; }
        public static IO2Log log { get; set; }
        public static O2FormsReflectionASCX reflection { get; set; }

        // local classes
        public static CecilUtils cecilUtils { get; set; }
        public static IAssemblyAnalysis assemblyAnalysis;// = new AssemblyAnalysisImpl();                
        public static IO2MonoCecil monoCecil { get; set; }

    }
}