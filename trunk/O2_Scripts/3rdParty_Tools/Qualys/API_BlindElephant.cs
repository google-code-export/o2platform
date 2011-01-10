// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX.classes.MainGUI;
using O2.Views.ASCX.ExtensionMethods;
//O2File:Tool_API.cs

namespace O2.XRules.Database.APIs
{
    public class API_BlindElephant  : Tool_API
    {   
    
    	public string BlindElephant_Py {get;set;}    	    	
    	//public string Uninstall_Exe {get;set;}
    	
    	public API_BlindElephant()
    	{
			config("Blind Elephant", "Blind Elephant 2.0", "");			
			Install_Uri = "https://blindelephant.svn.sourceforge.net/svnroot/blindelephant/trunk".uri();    		    		    		
			BlindElephant_Py = Install_Dir.pathCombine("BlindElephant.py");    		    		
			//Uninstall_Exe = Install_Dir.pathCombine("uninst.exe");
		}
            
    
    	public override bool isInstalled()
    	{
    		return BlindElephant_Py.fileExists();
    	}
    		 
    		 
		public bool install()
		{	
			if (this.isInstalled().isFalse())
			{  
				"[API_BlindElephant] Starting Blind Elephant installation process".info();
				Install_Dir.createDir();
				//var fiddlerInstaller = this.installerFile();
			}
    		return BlindElephant_Py.fileExists();
		}
	}
}
