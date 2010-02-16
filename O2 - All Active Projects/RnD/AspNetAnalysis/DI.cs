// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Text;
using O2.Interfaces.O2Core;
using O2.Kernel;

namespace o2.aspnetanalysis
{
    class DI
    {
        static DI()
        {
            log = PublicDI.log;
            config = PublicDI.config;
        }

        public static IO2Log log { get; set; }
        public static IO2Config config { get; set; }
    }
}
