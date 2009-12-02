using O2.Kernel;
using O2.Kernel.Interfaces.O2Core;

namespace O2.ImportExport.OunceLabs
{
    class DI
    {
        static DI()
        {
            log = PublicDI.log;
            reflection = PublicDI.reflection;  // new O2FormsReflectionASCX();
            config = PublicDI.config;
        }

        // DI Targets
        //public static IReflectionASCX reflection { get; set; }
        public static IReflection reflection { get; set; }
        public static IO2Log log { get; set; }        
        public static IO2Config config { get; set; }
        
    }
}