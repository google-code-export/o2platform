// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Debugger.Mdbg.O2Debugger;
using O2.Kernel.Interfaces.Messages;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.Debugger.Mdbg
{
    public class HandleO2MessageOnMdbg
    {

        //HandleO2MessageOnSD.o2MessageHelper_Handle_IM_FileOrFolderSelected(o2Message); 
        public static void o2KernelQueue_onMessages(IO2Message o2Message)
        {            
            if (o2Message is IM_O2MdbgAction)
            {
                IM_O2MdbgAction o2MDbgAction = (IM_O2MdbgAction)o2Message;
                switch (o2MDbgAction.o2MdbgAction)
                {
                    case IM_O2MdbgActions.breakEvent:
                        {
                            string filename = o2MDbgAction.filename;
                            int line = o2MDbgAction.line;
                            DI.log.info("SOURCECODE REF -> {0} : {1})", new object[] { line, filename });
                            O2.Kernel.CodeUtils.O2Messages.fileOrFolderSelected(filename, line);
                            //HandleO2MessageOnSD.setSelectedLineNumber(filename, line);
                            break;
                        }
                    case IM_O2MdbgActions.debugProcessRequest:
                        O2MDbgUtils.startProcessUnderDebugger(o2MDbgAction.filename);
                        break;

                    case IM_O2MdbgActions.debugMethodInfoRequest:
                        O2MDbgUtils.debugMethod(o2MDbgAction.method, o2MDbgAction.loadDllsFrom);
                        break;
                    case IM_O2MdbgActions.setBreakpointOnFile:
                        O2MDbgUtils.setBreakPointOnFile(o2MDbgAction.filename, o2MDbgAction.line);
                        break;
                }

            }
        }

        public static void setO2MessageMdbgListener()
        {
            KO2MessageQueue.getO2KernelQueue().onMessages += o2KernelQueue_onMessages;
        }
    }
}
