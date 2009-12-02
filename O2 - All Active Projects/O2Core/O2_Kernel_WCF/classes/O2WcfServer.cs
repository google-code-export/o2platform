using O2.Kernel.WCF.Interfaces;

namespace O2.Kernel.WCF.classes
{
    public class O2WcfServer
    {
        O2GenericWcfHost<IO2WcfKernelMessage> WcfHost { get; set; }
        public string WcfHostName { get; set; }

        public O2WcfServer(string wcfHostName)
        {
            WcfHostName = wcfHostName;
            setup();
        }

        private void setup()
        {
            WcfHost = O2WcfUtils.createWcfHostAndStartIt(WcfHostName);
        }

        public bool AllOK
        {
            get
            {                
                return (false == WcfHost.hostIsReady.SafeWaitHandle.IsClosed);
            }
        }

        public void close()
        {
            WcfHost.stopHost();
        }
    }
}
