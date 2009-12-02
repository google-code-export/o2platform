// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Threading;
using System.Windows.Forms;
using NUnit.Framework;
using O2.DotNetWrappers.DotNet;
using O2.External.O2Mono.Ascx;

namespace O2.UnitTests.Test_O2ViewsASCX
{
    [TestFixture]
    public class Test_ascx_O2Reflector
    {
        public static bool continousLoop = true;            // use for debugging

        private ascx_O2Reflector o2Reflector;
        private Form hostForm;

        [SetUp]
        public void createTestForm()
        {
            o2Reflector = new ascx_O2Reflector {Dock = DockStyle.Fill};
            hostForm = new Form {Width = o2Reflector.Width, Height = o2Reflector.Height};
            hostForm.Controls.Add(o2Reflector);            
            
            Assert.That(o2Reflector != null, "o2Reflector was null");
            Assert.That(hostForm != null, "hostForm was null");
            Assert.That(hostForm.Controls.Contains(o2Reflector), "hostForm didn't contain o2Reflector");

            // Inject dependency

            /*O2.Views.Controlers.DI.cecilUtils = new CecilUtils();
            O2.Views.Controlers.DI.cecilDecompiler = new CecilDecompiler();
            O2.Views.Controlers.DI.cecilASCX = new CecilASCX();
            O2.Views.Controlers.DI.reflectionASCX = new O2FormsReflectionASCX();
            
            O2.Views.ASCX.DI.assemblyAnalysis = new AssemblyAnalysis();*/
            // Load form
            O2Thread.staThread(() => hostForm.ShowDialog());            
        }

        private int maxWaitForFormClose = 2000;
        [Test]
        public void test_runForm()
        {            
            var onFormClose = new AutoResetEvent(false);        
            bool formClosed = false;            
            hostForm.Closed += (sender, e) => onFormClose.Set();
            if (maxWaitForFormClose >0)
                onFormClose.WaitOne(maxWaitForFormClose);
            else
                onFormClose.WaitOne();
            
            DI.log.i("Test Completed");            
        }
        

    }
}
