// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Text;
using O2.Debugger.Mdbg.Debugging.MdbgEngine;
using O2.Debugger.Mdbg.Debugging.MdbgEngine;
using System.Collections.Generic;

namespace O2.Debugger.Mdbg.O2Debugger.Objects
{
    public class O2MDbgThread
    {
        public MDbgThread MdbgThread {get ; set;} // need to calculate if there is a performace impact in storing this pointer

        public List<string> stackTrace = new List<string>();
        public StringBuilder stackTraceString { get; set; }
        public List<O2MDbgCurrentLocation> sourceCodeMappings { get; set; }
        public O2MDbgCurrentLocation currentLocation { get; set; }
        public List<O2MDbgVariable> o2MDbgvariables { get; set; }        
        public bool Active { get; set; }
        public int Number { get; set; }
        public int Id { get; set; }
        public bool HaveCurrentFrame { get; set; }
        public string currentException { get; set; }
        public string currentException_expandedView { get; set; }

        public O2MDbgThread()
        {
            stackTraceString = new StringBuilder();
            sourceCodeMappings = new List<O2MDbgCurrentLocation>();
        }

        public O2MDbgThread(MDbgThread mdbgThread) : this()
        {
            MdbgThread = mdbgThread;            
            Id = mdbgThread.Id;
            Number = mdbgThread.Number;
            HaveCurrentFrame = mdbgThread.HaveCurrentFrame;            
            currentLocation = new O2MDbgCurrentLocation(mdbgThread.CurrentSourcePosition);
            if (mdbgThread.CurrentException != null && mdbgThread.CurrentException.IsNull == false)
            {
                currentException = mdbgThread.CurrentException.GetStringValue(false);
                currentException_expandedView = mdbgThread.CurrentException.GetStringValue(true);
            }
            calculateStackTrace();

            o2MDbgvariables = DI.o2MDbg.sessionData.getCurrentFrameVariables(0 /*expandDepth*/, false /*canDoFunceval*/);
        }

        private void calculateStackTrace()
        {
            /*var stackTrace = new List<string>();
            foreach(MDbgILFrame mdbgILFrame in mdbgThread.Frames)
                stackTrace.Add(mdbgILFrame.Function.FullName);
            */
            foreach (MDbgILFrame mdbgILFrame in MdbgThread.Frames)
            {
                var stackTraceFunction = mdbgILFrame.Function.FullName;
                stackTraceString.AppendLine(stackTraceFunction);
                stackTrace.Add(stackTraceFunction);
                if (mdbgILFrame.SourcePosition != null)
                    sourceCodeMappings.Add(new O2MDbgCurrentLocation(mdbgILFrame.SourcePosition));
            }
            
        }
    }
}
