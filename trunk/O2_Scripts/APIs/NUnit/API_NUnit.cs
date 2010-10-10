// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX.classes.MainGUI;
using O2.Views.ASCX.Ascx.MainGUI;
using O2.Views.ASCX.ExtensionMethods;

using O2.Core.XRules.Ascx;
//O2Ref:O2_Core_XRules.dll

//O2File:_Extra_methods_To_Add_to_Main_CodeBase.cs

namespace O2.XRules.Database.APIs
{
    public class API_NUnit 
    {        	

        public static void executeCurrentScript()
        {
        	executeScript(PublicDI.CurrentScript);
        }
        public static void executeScript(string script)
        {
			var topPanel = O2Gui.open<Panel>("Unit Test Execution", 400,500);
			topPanel.insert_Below<ascx_LogViewer>(120).fill();
			var unitTest = topPanel.add_Control<ascx_XRules_UnitTests>();  
			unitTest.loadFile(script);
			unitTest.getXRulesTreeView().expand();
			unitTest.invoke("executeAllLoadedTests");
        }
    	    	    	    	    
    }
}
