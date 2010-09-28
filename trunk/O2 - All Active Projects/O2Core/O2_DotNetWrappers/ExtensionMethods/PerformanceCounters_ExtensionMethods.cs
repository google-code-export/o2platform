﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Kernel.ExtensionMethods;

namespace O2.DotNetWrappers.ExtensionMethods
{
    public static class PerformanceCounters_ExtensionMethods
    {
        public static float availableMemory(this object _object)
        {
            return new System.Diagnostics.PerformanceCounter("Memory", "Available MBytes").NextValue();
        }

        public static string infoAvailableMemory(this object _object)
        {
            var availableMemory = new System.Diagnostics.PerformanceCounter("Memory", "Available MBytes")
                                                        .NextValue()
                                                        .str();
            "AvailableMemory: {0}Mb".info(availableMemory);
            return availableMemory;
        }
    }
}
