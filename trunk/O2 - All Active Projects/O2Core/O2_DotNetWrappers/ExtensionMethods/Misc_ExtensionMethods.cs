using System;
using O2.DotNetWrappers.Network;
using O2.DotNetWrappers.Windows;

namespace O2.DotNetWrappers.ExtensionMethods
{
    public static class Misc_ExtensionMethods
    {
        public static void sleep(this object _object, int miliseconds)
        {
            Processes.Sleep(miliseconds);
        }

        public static void sleep(this object _object, int miliseconds, bool verbose)
        {
            Processes.Sleep(miliseconds, verbose);
        }

        
    }
}
