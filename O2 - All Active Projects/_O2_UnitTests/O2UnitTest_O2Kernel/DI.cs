using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Kernel;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.UnitTests.Test_O2Kernel
{
    public static class DI
    {
        static DI()
        {
            config = PublicDI.config;
            log = PublicDI.log;
            reflection = PublicDI.reflection;
            o2MessageQueue = PublicDI.o2MessageQueue;
        }

        public static KO2Log log { get; set; }
        public static KO2Config config { get; set; }
        public static KReflection reflection { get; set; }
        public static KO2MessageQueue o2MessageQueue { get; set; }

        public static string hardCodedO2LocalBuildDir = @"E:\O2\_Bin_(O2_Binaries)\";
    }
}