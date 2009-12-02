using System.Windows.Forms;
using NUnit.Framework;

namespace O2.UnitTests.Test_O2CoreLib.O2Core.O2CoreLib.O2Environment
{
    [TestFixture]
    public class Test_DebugMsg
    {
        [Test]
        [Explicit(
            "(not working at the momement since it is going to the Kernel log) This will send an email on every test, there is no need to do it all the time, but do so test regularly")]
        public void test_reportCriticalErrorToO2Developers()
        {
            string subject = "testing reportCriticalErrorToO2Developers";
            string message = "this is the error that happened";
            //DialogResult result = 
                DI.log.reportCriticalErrorToO2Developers(null, null,subject +  message);

            //Assert.That(result == DialogResult.OK, "reportCriticalErrorToO2Developers didn'result dialogResult.OK! it returned : " + result);
        }
    }
}