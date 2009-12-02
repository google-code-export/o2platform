// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.O2CmdShell;

namespace O2.Cmd.SpringMvc
{
    class Program
    {
        static void Main(string[] args)
        {
            O2Cmd.O2CmdPublishedDate = "June 2009";
            O2Cmd.O2CmdName = "O2 Cmd Spring MVC - Utilities to analyze Spring MVC apps";

            O2CmdApi.typesWithCommands.Add(typeof (Scripts.TraceCreator));
            O2CmdApi.typesWithCommands.Add(typeof (Gui));

            //if (ClickOnceDeployment.isClickOnceDeployment())
            if (args.Length == 0)
                args = new[] {"gui"};
            O2CmdExec.execArgs(args);
        }
    }
}
