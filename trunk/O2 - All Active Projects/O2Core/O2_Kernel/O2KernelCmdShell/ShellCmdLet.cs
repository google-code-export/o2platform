using System.Reflection;

namespace O2.Kernel.O2CmdShell
{
    public class ShellCmdLet
    {
        public MethodInfo methodToExecute { get; set; }
        public string cmdInstruction { get; set; }
        public object[] cmdParameters { get; set; }
        
        //public VoidFunc { get; set; }
        public ShellCmdLet(MethodInfo _methodToExecute, string _cmdInstruction, object[] _cmdParameters)
        {
            methodToExecute = _methodToExecute;
            cmdInstruction = _cmdInstruction;
            cmdParameters = _cmdParameters;
        }
    }
}
