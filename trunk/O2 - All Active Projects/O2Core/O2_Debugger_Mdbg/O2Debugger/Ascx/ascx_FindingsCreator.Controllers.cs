// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.O2Findings;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Kernel.Interfaces.Messages;
using O2.Kernel.Interfaces.O2Findings;
using O2.Kernel.InterfacesBaseImpl;
using O2.DotNetWrappers.Windows;
using O2.Views.ASCX.O2Findings;

namespace O2.Debugger.Mdbg.O2Debugger.Ascx
{

    public partial class ascx_FindingsCreator
    {
        private IO2Trace currentO2Trace;
        private IO2Finding currentDynamicO2Finding;

        bool runOnLoad = true;
        private void onLoad()
        {
            if (DesignMode == false && runOnLoad)
            {
                
                runOnLoad = false;
                KO2MessageQueue.getO2KernelQueue().onMessages += handleKernelMessage;
                createNewDynamicO2Finding();
            }
        }

        private void createNewDynamicO2Finding()
        {
            currentDynamicO2Finding = new O2Finding();
            currentO2Trace = null;
            reloadTraceViewer();
        }

        void handleKernelMessage(IO2Message o2Message)
        {
            if (o2Message is IM_O2MdbgAction)
            {
                var o2MDbgAction = (IM_O2MdbgAction)o2Message;
                switch (o2MDbgAction.o2MdbgAction)
                {
                    case IM_O2MdbgActions.breakEvent:

                        var filename = o2MDbgAction.filename;
                        var line = o2MDbgAction.line;
                        ///       DI.log.info("SOURCECODE REF -> {0} : {1})", line, filename);
                        addTraceForLine(filename, line);
                        break;
                    case IM_O2MdbgActions.commandExecutionMessage:
                        if (o2MDbgAction.lastCommandExecutionMessage.IndexOf("STOP: Breakpoint") == -1)                            
                            addTrace(null, 0, o2MDbgAction.lastCommandExecutionMessage);
                        break;
                }
            }

        }

        private void addTraceForLine(string filename, int line)
        {
            try
            {
                string traceText = null;
                if (filename != null)
                {
                    var sourceCodeLine = Files.getLineFromSourceCode(filename, (uint) line);
                    if (sourceCodeLine.Trim() == "{")
                    {
                        line--;
                        sourceCodeLine = Files.getLineFromSourceCode(filename, (uint) line);
                    }
                    sourceCodeLine = sourceCodeLine.Replace("\t", "");
                    if (sourceCodeLine == "")
                        return;
                    if (currentO2Trace != null && sourceCodeLine == currentO2Trace.signature)
                        return;
                    traceText = sourceCodeLine;
                }
                
                addTrace(filename, line, traceText);                
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in addTraceForLine");
            }
        }

        private void addTrace(string filename, int line, string traceText)
        {
            if (traceText == null)
                return;
            var o2Trace = new O2Trace(traceText);
            o2Trace.file = filename ?? "";
            o2Trace.lineNumber = (uint)line;
            if (true || currentO2Trace == null)
            {
                o2Trace.traceType = TraceType.Source;
                currentDynamicO2Finding.o2Traces.Add(o2Trace);
                currentO2Trace = o2Trace;
            }
            else
            {
                currentO2Trace.childTraces.Add(o2Trace);
                currentO2Trace = o2Trace;
            }
            reloadTraceViewer();
        }

        private void reloadTraceViewer()
        {
            traceViewer.loadO2Finding(currentDynamicO2Finding);
        }

        private void makeDynamicTraceIntoRealFinding()
        {
            currentO2Trace.traceType = TraceType.Lost_Sink;
            currentDynamicO2Finding.vulnType = "Dynamic.Trace.Generated.FInding";
            currentDynamicO2Finding.vulnName = currentO2Trace.signature;
            findingsViewer.loadO2Findings(new List<IO2Finding> { currentDynamicO2Finding });
            createNewDynamicO2Finding();
        }
    }

}
