// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.External.SharpDevelop.Ascx;
using O2.Interfaces.O2Core;
using O2.Interfaces.Views;
using O2.Kernel;
//O2Tag_AddReferenceFile:nunit.framework.dll
using NUnit.Framework;
using O2.External.WinFormsUI.Forms;

namespace O2.UnitTests.Standalone.UnderDevelopment
{
    [TestFixture]
    public class Test_AddBreakpointsToSourceCodeEditor 
    {    
        private readonly static IO2Log log = PublicDI.log;    	    	
    
        [Test]
        public static void openTempGUI()
        {
            log.info("in openTempGUI");
            if (O2AscxGUI.launch("test Breakpoings", 1000, 800))	
            {
                O2AscxGUI.openAscx(typeof(ascx_SourceCodeEditor),O2DockState.Document,"sourceCodeEditor");
                O2AscxGUI.waitForAscxGuiClose();
            }
        }
    }
}