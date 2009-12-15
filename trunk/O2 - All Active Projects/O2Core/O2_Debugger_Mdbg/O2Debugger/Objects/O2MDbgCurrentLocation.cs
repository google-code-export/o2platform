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

        public O2MDbgCurrentLocation()
        {
            functionName = "";
        }

        public O2MDbgCurrentLocation(MDbgThread mdbgThread) : this()
        {
            loadCurrentLocationFromMDbgThread(mdbgThread);
            raiseOnBreakEvent();
        }

        public O2MDbgCurrentLocation(MDbgSourcePosition mdbgsourcePosition) : this()
        {
            loadDataFromMDbgSourcePosition(mdbgsourcePosition);            
        }

        public void loadCurrentLocationFromMDbgThread(MDbgThread mdbgThread)
        {
             
            functionName = (mdbgThread.CurrentFrame != null && mdbgThread.CurrentFrame.Function != null)
                               ? mdbgThread.CurrentFrame.Function.FullName
                               : "";
            //mdbgsourcePosition = mdbgThread.BottomFrame.SourcePosition;
            loadDataFromMDbgSourcePosition(mdbgThread.BottomFrame.SourcePosition);            
        }

        private void loadDataFromMDbgSourcePosition(MDbgSourcePosition mDbgSourcePosition)
        {
            hasSourceCodeDetails = (mDbgSourcePosition != null);

            if (mDbgSourcePosition != null)
            {
                FileName = mDbgSourcePosition.Path;
                Line = mDbgSourcePosition.Line;
                IsSpecial = mDbgSourcePosition.IsSpecial;
                StartColumn = mDbgSourcePosition.StartColumn;
                EndColumn = mDbgSourcePosition.EndColumn;
                StartLine = mDbgSourcePosition.StartLine;
                EndLine = mDbgSourcePosition.EndLine;
            }
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
