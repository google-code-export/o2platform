using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Cmd.XRules;
using O2.Core.XRules.XRulesEngine;
using O2.DotNetWrappers.O2CmdShell;

namespace O2.Cmd.XRules
{
    public class program
    {
        static void Main(string[] args)
        {
            XRules_Config.xRulesDatabase = new KXRulesDatabase_O2Cmd();
            XRules_DatabaseSetup.installXRulesDatabase();

            O2Cmd.O2CmdName = "O2 Cmd XRules  (Command line interface for the O2 XRules)";
            O2Cmd.O2CmdPublishedDate = "November 2009";
            O2CmdApi.typesWithCommands.Add(typeof(XRulesWrapper));

            //if (args.Length == 0)
             //   args = new[] { "help" };

            O2CmdExec.execArgs(args);
        }
    }
}