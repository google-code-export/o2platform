// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
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
