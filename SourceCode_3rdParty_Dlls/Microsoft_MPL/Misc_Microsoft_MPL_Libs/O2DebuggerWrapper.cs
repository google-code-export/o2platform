using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misc_Microsoft_MPL_Libs
{

    public class IO2DebuggerWrapper
    {
        public void OriginalMDbgMessages_WriteLine(string message);
    }

    public class O2DebuggerWrapper : IO2DebuggerWrapper
    {
        static IO2DebuggerWrapper O2DebuggerMDbg { get; set; }

        public static O2DebuggerWrapper(IO2DebuggerWrapper o2DebuggerMDbg)
        {
            O2DebuggerMDbg = o2DebuggerMDbg;
        }

        public static override void OriginalMDbgMessages_WriteLine(string message)
        {
            O2DebuggerMDbg.OriginalMDbgMessages_WriteLine(message);
        }
    }
}
