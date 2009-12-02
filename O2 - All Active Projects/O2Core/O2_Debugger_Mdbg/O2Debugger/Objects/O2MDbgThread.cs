// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Text;
using O2.Debugger.Mdbg.Debugging.MdbgEngine;
using O2.Debugger.Mdbg.Debugging.MdbgEngine;

namespace O2.Debugger.Mdbg.O2Debugger.Objects
{
    public class O2MDbgThread
    {
        public MDbgThread mdbgThread; // need to calculate if there is a performace impact in storing this pointer

        // public List<String> stackTrace = new List<string>();
        public StringBuilder stackTraceString = new StringBuilder();

        public O2MDbgThread(MDbgThread _mDbgThread)
        {
            mdbgThread = _mDbgThread;
            calculateStackTrace();
        }

        private void calculateStackTrace()
        {
            /*var stackTrace = new List<string>();
            foreach(MDbgILFrame mdbgILFrame in mdbgThread.Frames)
                stackTrace.Add(mdbgILFrame.Function.FullName);
            */
            foreach (MDbgILFrame mdbgILFrame in mdbgThread.Frames)
                stackTraceString.AppendLine(mdbgILFrame.Function.FullName);
        }
    }
}
