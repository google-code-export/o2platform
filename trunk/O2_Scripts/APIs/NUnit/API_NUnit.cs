// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
using O2.XRules.Database.Utils;

//O2File:Installer_NUnit.cs

namespace O2.XRules.Database.APIs
{
    public class API_NUnit
    {        			
    	public Installer_NUnit installer;
    	
    	public API_NUnit()
    	{
    		installer = new Installer_NUnit();	//this will download NUnit on the first time it is called
    		
    	}
    }
    
    public static class API_NUnit_ExtensionMethods
    {
    	//original GUI tests 
    	/*public static API_NUnit openNUnitGui(this API_NUnit nUnitApi)
    	{    		
			var nunitGuiRunner = nUnitApi.installer.Executable.parentFolder().pathCombine("lib\\nunit-gui-runner.dll");
			nunitGuiRunner.loadAssemblyAndAllItsDependencies();
			nunitGuiRunner.type("AppEntry").method("Main").invokeStatic_StaThread(new string[] {} )  ;
			return nUnitApi;
    	}
    	
    	public static API_NUnit openNUnitGui_in_SeparateAppDomain(this API_NUnit nUnitApi)
    	{
    		var script = @"
    						var nunitAPi = new API_NUnit();
							nunitAPi.openNUnitGui();
							//O2File:API_NUnit.cs";

			script.execute_InScriptEditor_InSeparateAppDomain();
			return nUnitApi;
		}*/				
    }
}