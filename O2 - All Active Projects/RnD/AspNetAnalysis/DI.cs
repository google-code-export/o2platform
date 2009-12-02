using System;
using System.Collections.Generic;
using System.Text;
using O2.Kernel;
using O2.Kernel.Interfaces.O2Core;

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
