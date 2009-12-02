// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.ServiceModel;
using O2.Kernel.CodeUtils;
using System.Threading;

namespace O2.Kernel.WCF.classes
{    
    public class O2GenericWcfHost<WcfInterface>
    {
        public AutoResetEvent hostIsReady = new AutoResetEvent(false);
        public AutoResetEvent terminateHost = new AutoResetEvent(false);
        public AutoResetEvent hostClosed = new AutoResetEvent(false);

        public NetTcpBinding WcfBinding { get; set; }
        public string WcfBindingAddress { get; set; }        
        public Type WcfImplementation { get; set; }

        private int maxMessageSize = 1024 * 1024; // 1048576 (the original value was 64k

        private O2GenericWcfHost()
        {
            WcfBindingAddress = "net.tcp://127.0.0.1:9000/O2Wcf/";
            WcfBinding = new NetTcpBinding { MaxReceivedMessageSize = maxMessageSize, MaxBufferSize = maxMessageSize };
        }

        public O2GenericWcfHost(string hostName, Type wcfImplementation)
            : this()
        {
            WcfBindingAddress += hostName;            
            WcfImplementation = wcfImplementation;            
        }
            
        public bool startHost()
        {
            try
            {
                O2Kernel_O2Thread.mtaThread(() =>
                {
                    using (var host = new ServiceHost(WcfImplementation))
                    {
                        host.AddServiceEndpoint(typeof(WcfInterface),WcfBinding , WcfBindingAddress);                        
                        
                        //host.ChannelDispatchers[0].Listener.max

                        host.Open();
                        WCF_DI.log.info("Wcf Host has started on: {0}", WcfBindingAddress);
                        hostIsReady.Set();
                        terminateHost.WaitOne();
                        WCF_DI.log.info("terminateHost is set, so terminating thread with Wcf Host");                        
                        host.Close();
                        WCF_DI.log.info("Host Closed");
                        hostClosed.Set();
                    }
                });
                hostIsReady.WaitOne();
                return true;
            }
            catch (Exception ex)
            {
                WCF_DI.log.ex(ex, "in O2GenericWcfHost.startHost");
                return false;
            }
            
        }

        public WcfInterface getClientProxy()
        {            
            var factory = new ChannelFactory<WcfInterface>(WcfBinding);
            return factory.CreateChannel(new EndpointAddress(WcfBindingAddress));
        }

        public void stopHost()
        {
            terminateHost.Set();
        }

    }
}
