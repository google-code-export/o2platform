using O2.Kernel.CodeUtils;
using O2.Kernel.O2CmdShell;
using O2.Kernel.WCF;
using O2.Kernel.WCF.classes;

namespace O2.Kernel
{
    public class startO2CmdShell
    {
        public static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                AppDomainUtils.renameCurrentO2KernelProcessName(args[0]);                
                O2WcfUtils.createWcfHostForThisO2KernelProcess();
            }

            WCF_DI.o2Shell = new O2Shell();
            WCF_DI.o2Shell.startShell();

            if (WCF_DI.o2WcfKernelMessage != null)
                WCF_DI.o2WcfKernelMessage.stopHost();
        }        
    }
}
