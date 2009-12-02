// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Kernel.WCF.Interfaces;
using O2.Kernel.WCF.InterfacesBaseImpl;

namespace O2.Kernel.WCF.classes
{
    public class O2WcfUtils
    {
        public static void wcfMessageReceived(string wcfMessage)
        {
            var messageToPrint = string.Format("WcfMessageReceived: {0}", wcfMessage);
            WCF_DI.log.info(messageToPrint);
            if (WCF_DI.o2Shell != null)
                WCF_DI.o2Shell.shellIO.writeLine("\n{0}" , messageToPrint);

        }


        public static O2GenericWcfHost<IO2WcfKernelMessage> createWcfHost(string wcfHostName)
        {
            return new O2GenericWcfHost<IO2WcfKernelMessage>(wcfHostName, typeof(KO2WcfKernelMessage));
        }

        public static O2GenericWcfHost<IO2WcfKernelMessage> createWcfHostAndStartIt(string wcfHostName)
        {
            var wcfHost = createWcfHost(wcfHostName);
            wcfHost.startHost();
            return wcfHost;
        }

        public static O2GenericWcfHost<IO2WcfKernelMessage> createWcfHostForThisO2KernelProcess()
        {
            WCF_DI.o2WcfKernelMessage = createWcfHostAndStartIt(PublicDI.O2KernelProcessName);
            return WCF_DI.o2WcfKernelMessage;
        }

        public static IO2WcfKernelMessage getClientProxyForThisO2KernelProcess()
        {
            return createClientProxy(PublicDI.O2KernelProcessName);
        }

        public static IO2WcfKernelMessage createClientProxy(O2GenericWcfHost<IO2WcfKernelMessage> wcfHost)
        {
            return wcfHost.getClientProxy();
        }

        public static IO2WcfKernelMessage createClientProxy(string o2KernelProcessName)
        {
            var wcfHost = new O2GenericWcfHost<IO2WcfKernelMessage>(o2KernelProcessName, typeof(KO2WcfKernelMessage));
            return wcfHost.getClientProxy();
        }

    /*    public static void getWcfKernelMessageForWcfHost()
        {
            PublicDI.o2WcfKernelMessage = createWcfHostAndStartIt(DI.O2KernelProcessName);
        }*/
    }
}
