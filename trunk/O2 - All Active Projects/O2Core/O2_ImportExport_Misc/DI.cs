using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Kernel;
using O2.Kernel.Interfaces.O2Core;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.ImportExport.Misc
{
    class DI
    {
        static DI()
        {
            config = PublicDI.config;   // need to use local copy
            log = PublicDI.log;
            reflection = PublicDI.reflection;
        }

        // DI which will need to be injected 

        public static IO2Config config { get; set; }
        public static IO2Log log { get; set; }
        public static IReflection reflection;

    }
}
