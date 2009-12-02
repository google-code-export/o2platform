// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Debugger.Mdbg.Debugging.MdbgEngine;
using O2.Kernel.CodeUtils;

namespace O2.Debugger.Mdbg.O2Debugger.Objects
{
    public class O2MDbgCurrentLocation
    {
        public MDbgSourcePosition mdbgsourcePosition { get; set; }
        public bool hasSourceCodeDetails { get; set; }
        public string functionName { get; set; }
        public string FileName { get; set; }
        public int Line { get; set; }
        public bool IsSpecial { get; set; }
        public int StartColumn { get; set; }
        public int EndColumn { get; set; }
        public int StartLine { get; set; }
        public int EndLine { get; set; }

        public O2MDbgCurrentLocation(MDbgThread mdbgThread)
        {
            loadCurrentLocationFromMDbgThread(mdbgThread);
            raiseOnBreakEvent();
        }

        public void loadCurrentLocationFromMDbgThread(MDbgThread mdbgThread)
        {
            mdbgsourcePosition = mdbgThread.BottomFrame.SourcePosition;
            hasSourceCodeDetails = (mdbgsourcePosition != null);
            functionName = (mdbgThread.CurrentFrame != null && mdbgThread.CurrentFrame.Function != null)
                               ? mdbgThread.CurrentFrame.Function.FullName
                               : "";
            if (mdbgsourcePosition != null)
            {
                FileName = mdbgThread.BottomFrame.SourcePosition.Path;
                Line = mdbgThread.BottomFrame.SourcePosition.Line;
                IsSpecial = mdbgThread.BottomFrame.SourcePosition.IsSpecial;
                StartColumn = mdbgThread.BottomFrame.SourcePosition.StartColumn;
                EndColumn = mdbgThread.BottomFrame.SourcePosition.EndColumn;
                StartLine = mdbgThread.BottomFrame.SourcePosition.StartLine;
                EndLine = mdbgThread.BottomFrame.SourcePosition.EndLine;                
            }
          //  else
          //      DI.log.info("at O2MDbgCurrentLocation, no source code for current function: {0}", functionName);
        }

        public void raiseOnBreakEvent()
        {
         //   if (hasSourceCodeDetails)
            O2Messages.raiseO2MDbgBreakEvent(FileName, Line);
            DI.o2MDbg.raiseOnBreakEvent(this);
        }

        public override string ToString()
        {
            if (hasSourceCodeDetails)
                return string.Format("{0}:{1}", System.IO.Path.GetFileName(FileName), StartLine);
            return string.Format("{0}", functionName);
        }
    }
}
