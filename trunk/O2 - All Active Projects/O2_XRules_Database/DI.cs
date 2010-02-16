// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Interfaces.O2Core;
using O2.Kernel;

namespace O2.XRules.Database
{
    internal class DI
    {
        static DI()
        {
            log = PublicDI.log;
            config = PublicDI.config;
            reflection = PublicDI.reflection;
        }

        public static IO2Log log { get; set; }
        public static IO2Config config { get; set; }

        public static IReflection reflection { get; set; }
    }
}