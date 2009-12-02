// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using O2.Kernel;
using O2.Kernel.Interfaces.O2Core;
using O2.Kernel.InterfacesBaseImpl;
using O2.Kernel.O2CmdShell;
using O2.Kernel.Objects;
using O2.Kernel.WCF.classes;
using O2.Kernel.WCF.Interfaces;

namespace O2.Kernel.WCF
{
    public class WCF_DI
    {
        static WCF_DI()
        {
            log = PublicDI.log;
            reflection = PublicDI.reflection; 
            config = PublicDI.config;            
        }

        // DI Targets
        //public static IReflectionASCX reflection { get; set; }
        public static IReflection reflection { get; set; }
        public static IO2Log log { get; set; }
        public static IO2Config config { get; set; }

        public static O2Shell o2Shell { get; set; }
        

        public static O2GenericWcfHost<IO2WcfKernelMessage> o2WcfKernelMessage { get; set; }

    }
}
