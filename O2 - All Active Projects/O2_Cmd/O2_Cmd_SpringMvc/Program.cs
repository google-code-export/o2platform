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