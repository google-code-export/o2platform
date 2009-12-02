using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Kernel;
using O2.Kernel.Interfaces.O2Core;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.Core.FileViewers
{
    class DI
    {
        static DI()
        {
            log = PublicDI.log;// new WinFormsUILog();            
            config = PublicDI.config; //new KO2Config();    
            reflection = PublicDI.reflection;
        }

        public static IO2Log log { get; set; }
        public static IO2Config config { get; set; }
        public static IReflection reflection { get; set; }

        
    }
}