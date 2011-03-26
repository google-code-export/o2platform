// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using ICSharpCode.SharpDevelop.Dom;
//using O2.API.AST.CSharp;
using ICSharpCode.NRefactory.Ast;
//using O2.API.AST.ExtensionMethods.CSharp;
using O2.XRules.Database.Utils;

//__O2File:Ast_Engine_ExtensionMethods.cs
//__O2File:FileCache.cs

namespace O2.XRules.Database.Languages_and_Frameworks.DotNet
{
    public class AspNet_Page
    {   
    	public string File_Path { get; set;}
		public string Virtual_Path { get; set; }				
								
		public Uri Server { get; set; }		
		public Uri Web_Address { get; set; }
		    	
    	public AspNet_Page()
    	{
    		
    	}
    	
    	public AspNet_Page(string file_Path, string virtual_Path, Uri server)
    	{
    		Virtual_Path = virtual_Path;
    		File_Path = file_Path;
    		Server = server;
    		Web_Address= new Uri(server , virtual_Path);
    		
    	}
    	
    	public override string ToString()
    	{
    		return Virtual_Path;
    	}
    	
	}    	
}
