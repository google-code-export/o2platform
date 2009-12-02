// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using O2.DotNetWrappers.Windows;

namespace O2.Scanner.DotNet.PostSharp
{
    public class IISDeployment
    {
        public static void deployToGac(bool kill_IIS_Process)
        {
            deployToGac();
            if (true == kill_IIS_Process)
                kill_IIS_Process_W3wp();
        }

        public static void deployToGac()
        { 
            if (false == File.Exists(DI.PathToExternalPostSharpAssembly))
                DI.log.error("Could not file pathToExternalPostSharpAssembly: {0}", DI.PathToExternalPostSharpAssembly);
            else
            {
                var processName = @"C:\Program Files\Microsoft Visual Studio 8\SDK\v2.0\Bin\gacutil.exe";
                var parameters = "/i \"" + DI.PathToExternalPostSharpAssembly + "\"";
                var execOutput = Processes.startProcessAsConsoleApplicationAndReturnConsoleOutput(processName, parameters);
                DI.log.info(execOutput);
            }
        }

        public static void kill_IIS_Process_W3wp()
        {
            DI.log.info("Killling IIS W3WP processes");
            while (Processes.getProcessesCalled("w3wp").Count > 0)
            {
                Processes.killProcess("w3wp", true);
                Processes.Sleep(200);
            }            
        }

    }
}
