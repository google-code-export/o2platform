// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace O2.Debugger.Mdbg.O2Debugger.Objects
{
    public class O2MDbgCommands
    {
        public static string help()
        {
            return "help";
        }

        public static string quit()
        {
            return "quit";
        }

        public static string functionExecute(string functionName)
        {
            return string.Format("f {0}", functionName);
        }

        public static string addBreakPoint(string breakpointSignature)
        {
            return string.Format("b {0}", breakpointSignature);
        }

        public static string addBreakPoint(string assemblyName, string typeName, string methodName)
        {
            return string.Format("b {0}!{1}.{2}", assemblyName, typeName, methodName);
        }

        public static string go()
        {
            return "go";
        }

        public static string run(string assemblyName)
        {
            return string.Format("run {0}", assemblyName);
        }

        public static string stepOver()
        {
            return "next";
        }

        public static string stepInto()
        {
            return "step";
        }

        public static string stepBack()
        {
            return "out";
        }

        public static string printLocalVariables()
        {
            return "print";
        }

        public static string listManagedProcesses()
        {
            return "process";
        }

        public static string attachToRunningProcess(string processID)
        {
            return string.Format("a {0}", processID);
        }

        public static string detach()
        {
            return "detach";
        }
    }
}
