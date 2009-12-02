using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Kernel;
using O2.Kernel.Interfaces.O2Core;

namespace O2.Tool.XRules
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
