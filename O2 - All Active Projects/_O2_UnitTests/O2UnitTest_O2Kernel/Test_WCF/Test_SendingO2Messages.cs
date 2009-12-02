// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.ServiceModel;
using NUnit.Framework;
using O2.Kernel.Interfaces.Messages;
using O2.Kernel.InterfacesBaseImpl;
using O2.Kernel.WCF.classes;

namespace O2.UnitTests.Test_O2Kernel.Test_WCF
{
    [TestFixture]
    public class Test_SendingO2Messages
    {
        private O2GenericWcfHost<IWcfUnitTest_SendO2Message> o2GenericWcfHost;
        private IWcfUnitTest_SendO2Message iO2WcfProxy;

        private const string proxyName = "WcfTest_SendO2Message";

        
        [ServiceContract(Namespace = "Test_IO2Wcf")]
        private interface IWcfUnitTest_SendO2Message
        {
            [ServiceKnownType(typeof(KO2Message))]
            [OperationContract]
            bool sendMessage(IO2Message o2Message);

            [OperationContract]
            bool allOK();
        }


        private class KWcfUnitTest_SendO2Message : IWcfUnitTest_SendO2Message
        {

            public bool allOK()
            {
                return true;
            }

            public bool sendMessage(IO2Message o2Message)
            {
                DI.log.info("Received message of Type:{0}", o2Message.GetType().Name);
                return true;
            }
        }        

        [SetUp]
        public void createHostAndClientProxy()
        {
            DI.log.info("in startWcfHost");
            o2GenericWcfHost = new O2GenericWcfHost<IWcfUnitTest_SendO2Message>(proxyName, typeof(KWcfUnitTest_SendO2Message));            
            // create host
            DI.log.info("Starting Host");
            var hostStartedOk = o2GenericWcfHost.startHost();
            Assert.That(hostStartedOk, " hostStartedOk was false");
            iO2WcfProxy = o2GenericWcfHost.getClientProxy();
            Assert.That(iO2WcfProxy.allOK(), "iO2WcfProxy.allOK returned false");
        }

        [Test, Ignore("Manually test this one until issue with quick WCF requests is solved")]
        public void sendMessage()
        {
            DI.log.info("Proxy AllOk = {0}", iO2WcfProxy.allOK());
            var o2Message = new KO2Message();

            iO2WcfProxy.sendMessage(o2Message);

        }

        [TearDown]
        public void closeProxy()
        {
            DI.log.info("Closing down host");
            o2GenericWcfHost.stopHost();
            o2GenericWcfHost.hostClosed.WaitOne();
            DI.log.info("Host closed is set, so finishing off test");
        }

        /*
                [Test]
                public void startWcfHost()
                {
                    DI.log.info("in startWcfHost");
                    var o2GenericWcfHost = new O2GenericWcfHost<IO2WcfUnitTest>("test_startWcfHost", typeof(KO2WcfUnitTest));
                    // create host
                    DI.log.info("Starting Host");
                    var hostStartedOk = o2GenericWcfHost.startHost();
                    Assert.That(hostStartedOk, " hostStartedOk was false");

                    DI.log.info("Connecting Client");


                    
                    DI.log.info("Sending Message");
                    DI.log.info(iO2WcfProxy.helloThere("This is my name"));

                    DI.log.info("Closing down host");
                    o2GenericWcfHost.stopHost();
                    o2GenericWcfHost.hostClosed.WaitOne();
                    DI.log.info("Host closed is set, so finishing off test");
            
                }
                */

    }
}
