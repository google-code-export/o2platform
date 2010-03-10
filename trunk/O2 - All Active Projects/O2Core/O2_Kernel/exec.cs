﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Kernel.ExtensionMethods;
using System.IO;
namespace O2.Kernel
{
    public class exec
    {   
        // generic exection methods
        public static void cmd(string commandToExecute)
        {
            cmd(commandToExecute, "");
        }

        public static void cmd(string commandToExecute, string arguments)
        {
            "O2_DotNetWrappers".type("Processes").invokeStatic("startProcess",commandToExecute, arguments);
        }

        public static string cmdViaConsole(string commandToExecute)
        {
            return cmdViaConsole(commandToExecute, "");
        }

        public static string cmdViaConsole(string commandToExecute, string arguments)
        {
            var returnData = "O2_DotNetWrappers".type("Processes").invokeStatic("startProcessAsConsoleApplicationAndReturnConsoleOutput", commandToExecute, arguments);
            return returnData != null ? returnData.ToString() : "";
        }

        // speciif ones
        public static string msBuild(string projectOrSolution)
        {
            if (false == File.Exists(projectOrSolution))
            {
                PublicDI.log.error("in exec.msBuild: provided file doesn't exist: {0}", projectOrSolution);
                return "";
            }
            var msBuildExe = @" C:\WINDOWS\Microsoft.NET\Framework\v3.5\msbuild.exe";
            var extraParams = " /target:Rebuild";

            return cmdViaConsole(msBuildExe, "\"" + projectOrSolution + "\"" + extraParams);
        }
    }
}
