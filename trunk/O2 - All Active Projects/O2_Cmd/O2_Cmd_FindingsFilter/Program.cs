
using O2.Cmd.FindingsFilter.Filters;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.O2CmdShell;

namespace O2.Cmd.FindingsFilter
{
    class Program
    {
        static void Main(string[] args)
        {            
            O2CmdApi.typesWithCommands.Add(typeof (Filters.OzasmtFilters));
            O2CmdApi.typesWithCommands.Add(typeof(GuiHelpers));
            O2CmdApi.typesWithCommands.Add(typeof (Filters.OzasmtLinqUtils));
            O2CmdApi.typesWithCommands.Add(typeof (Filters.PublishToCore));
            O2CmdApi.typesWithCommands.Add(typeof(Filters.RemoveDuplicateTypeIIs));
            if (ClickOnceDeployment.isClickOnceDeployment())
                if (args.Length == 0)
                    args = new[] { "gui" };
            O2CmdExec.execArgs(args);
        }
    }
}