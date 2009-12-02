// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Kernel;
using O2.Kernel.Interfaces.O2Core;

namespace O2.UnitTests.Test_Ozasmt
{
    class DI
    {
        static DI()
        {
            config = PublicDI.config;
            log = PublicDI.log;            
        }

        public static IO2Config config { get; set; }
        public static IO2Log log { get; set; }
    
    }
}
