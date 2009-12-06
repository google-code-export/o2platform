// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using O2.Kernel;
using System.Windows.Forms;
using O2.Kernel.Interfaces.O2Core;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Views.ASCX;
using O2.Kernel.Interfaces.Views;
using O2.External.WinFormsUI.Forms;
//O2Tag_AddReferenceFile:nunit.framework.dll
using NUnit.Framework; 
using O2.Views.ASCX.classes;
//O2Tag_AddSourceFile:ascx_SvnBrower.cs
namespace O2.Script
{	
	
	[TestFixture]
    public class SvnTest
    {    
    	private static IO2Log log = PublicDI.log;
    	
    	public static string svnServer = @"http://o2platform.googlecode.com/svn/trunk";
    	public static string XRulesDatabase = svnServer + @"/O2%20-%20All%20Active%20Projects/O2_XRules_Database/_Rules/a";
    	
        public SvnTest()
    	{    	
    		
    	}    	    	    	    	    
    	
    	[Test] 
    	public bool browseSvnArchive()
    	{    		
    		O2AscxGUI.openAscx(typeof(ascx_SvnBrowser),  O2DockState.Float, "Svn Browser");
    		return true;
    	}
    	
    	[Test]
    	public bool checkIfSvnServerIsOnline()
    	{
    		var urlContents = WebRequests.getUrlContents(svnServer);
    		Assert.That(false == string.IsNullOrEmpty(urlContents), "urlContents was empty");    		
    		return true;
    	}
    	
    	[Test]
    	public bool checkIfRulesDatabaseIsAvailable()
    	{
			var urlContents = WebRequests.getUrlContents(XRulesDatabase);
    		Assert.That(false == string.IsNullOrEmpty(urlContents), "urlContents was empty");    		
    		return true;
    	}    	
    	
    	
    }
}
