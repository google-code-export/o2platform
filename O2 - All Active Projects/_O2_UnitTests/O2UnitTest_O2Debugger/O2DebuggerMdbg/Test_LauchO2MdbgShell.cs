// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Windows.Forms;
using NUnit.Framework;
using O2.Debugger.Mdbg.O2Debugger;
using O2.Debugger.Mdbg.O2Debugger.Objects;
using O2.DotNetWrappers.Windows;
using System.Threading;

namespace O2.UnitTests.Test_O2Debugger.O2DebuggerMdbg
{
    [TestFixture]
    public class Test_LauchO2MdbgShell
    {
        
        [Test]        
        public void test_createControlAndShowItOnSimpleForm()
        {
            const bool loadAsApplication = false;
            var o2Debuggger = new O2MDbg();
            var o2MDbgShellForm = o2Debuggger.showShellTestGuiAsNewWindowsForm(loadAsApplication);
            
            if (loadAsApplication == false)
            {
                Assert.That(o2MDbgShellForm != null, "o2MdbgShell == null");
                Assert.That(o2MDbgShellForm.Visible, "o2MdbgShell was not Visible!");
                o2MDbgShellForm.Close();
            }
            o2Debuggger.stopMDbg();
        }
    }
}
