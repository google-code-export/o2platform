using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.DotNet;
using O2.Kernel;

namespace O2.DotNetWrappers.ExtensionMethods
{
    public static class Sleep_ExtensionMethods
    {                    
        public static void sleep(this object _object, int miliseconds)
        {
            Processes.Sleep(miliseconds);
        }

        public static void sleep(this object _object, int miliseconds, bool verbose)
        {
            Processes.Sleep(miliseconds, verbose);
        }

        public static void sleep(this object _object, int miliSeconds, MethodInvoker toInvokeAfterSleep)
        {
            _object.sleep(miliSeconds, false, toInvokeAfterSleep);
        }

        public static void sleep(this object _object, int miliSeconds, bool verbose, MethodInvoker toInvokeAfterSleep)
        {
            O2Thread.mtaThread(
                () =>
                {
                    _object.sleep(miliSeconds, verbose);
                    toInvokeAfterSleep();
                });
        }

        
    }
}
