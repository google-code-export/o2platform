// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
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
    	public Process NUnitProcess;
    	
    	public API_NUnit()
    	{
    		installer = new Installer_NUnit();	//this will download NUnit on the first time it is called
    		
    	}
    }
    
    public static class API_NUnit_ExtensionMethods_NUnitGui
    {
    
    	public static API_NUnit openProject(this API_NUnit nUnitApi)
    	{    	
    		return nUnitApi.openProject(null);
    	}
    	
    	public static API_NUnit openProject(this API_NUnit nUnitApi, string projectOrAssembly)
    	{
    		return nUnitApi.openNUnitGui(projectOrAssembly);
    	}    	
    	
    	public static API_NUnit runProject(this API_NUnit nUnitApi, string projectOrAssembly)
    	{
    		return nUnitApi.openNUnitGui(projectOrAssembly, "/run");
    	}    	
    	
    	public static API_NUnit start(this API_NUnit nUnitApi)
    	{
    		return nUnitApi.openNUnitGui();
    	}
    	
    	public static API_NUnit openNUnitGui(this API_NUnit nUnitApi)
    	{
    		return nUnitApi.openNUnitGui(null);
    	}
    	
    	public static API_NUnit openNUnitGui(this API_NUnit nUnitApi, string projectOrAssembly)
    	{
    		return nUnitApi.openNUnitGui(projectOrAssembly, null);
    	}
    	
    	public static API_NUnit openNUnitGui(this API_NUnit nUnitApi, string projectOrAssembly, string extraStartupOptions)
    	{
    		var startUpOpptions = "\"{0}\" {1}".format(projectOrAssembly ?? "" , extraStartupOptions ?? "");
    		nUnitApi.NUnitProcess = new API_NUnit().installer.Executable.startProcess(startUpOpptions);
    		return nUnitApi;
    	}
    	
    	public static bool compileAndRun(this API_NUnit nUnitApi, string fileToCompile)
    	{
    		return nUnitApi.compileAndOpen(fileToCompile, "/run");
    	}
    	
    	public static bool compileAndOpen(this API_NUnit nUnitApi, string fileToCompile, string extraStartupOptions)
    	{
    		var assembly = new CompileEngine().compileSourceFile(fileToCompile);
			if (assembly.notNull())
			{
				var location = assembly.Location; 			
				nUnitApi.openNUnitGui(location, extraStartupOptions);			
				return true;
			}
			return false;
    	}
    	
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
    
    public static class API_NUnit_ExtensionMethods_NUnitConsole
    {
    	public static Process executeNUnitConsole(this API_NUnit nUnitApi)
    	{
    		return nUnitApi.executeNUnitConsole("", (logLine)=> logLine.info());
    	}
    	
    	public static Process executeNUnitConsole(this API_NUnit nUnitApi, string parameters)
    	{
    		return nUnitApi.executeNUnitConsole(parameters, (logLine)=> logLine.info());
    	}
    		
    	public static Process executeNUnitConsole(this API_NUnit nUnitApi, string parameters, Action<string> logOut)
    	{
    		var nUnitConsole = nUnitApi.installer.Executable.directoryName().pathCombine("nunit-console.exe");    	
    		return nUnitConsole.startProcess(parameters,logOut);
    	}
    }
    
}