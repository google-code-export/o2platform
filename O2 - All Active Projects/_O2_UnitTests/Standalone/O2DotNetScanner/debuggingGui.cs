// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Kernel;
using O2.Kernel.Interfaces.Views;
using O2.Kernel.Interfaces.O2Core;
using O2.DotNetWrappers.Windows;
using O2.External.WinFormsUI.O2Environment;
using O2.External.WinFormsUI.Forms;
//O2Tag_AddReferenceFile:nunit.framework.dll
using NUnit.Framework; 
using O2.Debugger.Mdbg.O2Debugger;
//O2Tag_AddSourceFile:E:\O2\_SourceCode_O2\O2Core\O2_DotNetWrappers\Windows\O2Forms.cs

namespace O2.UnitTests.Standalone.O2DotNetScanner
{
    [TestFixture]
    public class DebuggingGUI
    {    
        private readonly static IO2Log log = PublicDI.log;    	    	
    	
        // this script will setup the Debugging GUI
        [Test]
        public string setupGUI()
        {
            var currentAscx = O2DockUtils.getListAvailableAscx();
            foreach(var ascx in currentAscx)
                log.info("[a] {0}",ascx);
    			
            //var o2Debugger = O2AscxGUI.getAscx("O2 Debugger");
            //Assert.That(o2Debugger != null);
            var o2Debugger = O2AscxGUI.openAscx(typeof(ascx_O2MdbgShell),O2DockState.Float, "O2 debugger");
    		
            O2Forms.setAscxPosition(o2Debugger,100,10,100,10);
    		
            /*   		Assert.That(o2Debugger != null, "o2Debugger != null");
    		var parentForm = O2Forms.findParentThatHostsControl(o2Debugger);
			parentForm.Top = 500;
			//parentForm.Lef = 10;*/
            return "ok";
        }
    	
    	    	
    }
}