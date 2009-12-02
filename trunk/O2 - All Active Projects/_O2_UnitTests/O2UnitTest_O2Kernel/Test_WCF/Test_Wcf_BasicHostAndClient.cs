// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.ServiceModel;
using NUnit.Framework;
using O2.Kernel.WCF.classes;

namespace O2.UnitTests.Test_O2Kernel.Test_WCF
{
    [TestFixture]
    public class Test_Wcf_BasicHostAndClient
    {
        [ServiceContract(Namespace = "Test_IO2Wcf")]
        public interface IO2WcfUnitTest
        {
            [OperationContract]
            string helloThere(string name);

            [OperationContract]
            bool allOK();
        }


        public class KO2WcfUnitTest : IO2WcfUnitTest
        {

            public bool allOK()
            {
                return true;
            }

            public string helloThere(string name)
            {
                return "Hi " + name;
            }
        }

        [Test , Ignore("Manually test this one until issue with quick WCF requests is solved")]
        public void startWcfHost()
        {
            DI.log.info("in startWcfHost");
            var o2WcfHost = new O2GenericWcfHost<IO2WcfUnitTest>("test_startWcfHost", typeof(KO2WcfUnitTest));
            // create host
            DI.log.info("Starting Host");
            var hostStartedOk = o2WcfHost.startHost();
            Assert.That(hostStartedOk, " hostStartedOk was false");

            DI.log.info("Connecting Client");


            var iO2WcfProxy = o2WcfHost.getClientProxy();
            Assert.That(iO2WcfProxy.allOK(), "iO2WcfProxy.allOK returned false");
            DI.log.info("Sending Message");
            DI.log.info(iO2WcfProxy.helloThere("This is my name"));

            DI.log.info("Closing down host");
            o2WcfHost.stopHost();
            o2WcfHost.hostClosed.WaitOne();
            DI.log.info("Host closed is set, so finishing off test");
            //      Thread.Sleep(10000); 
        }
    }
}
