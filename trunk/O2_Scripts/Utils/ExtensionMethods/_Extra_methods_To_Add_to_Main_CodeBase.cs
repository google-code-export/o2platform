// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Xml;
using System.Drawing;
using System.Threading;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Reflection;
using System.Text;
using O2.Interfaces.O2Core;
using O2.Interfaces.O2Findings;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.O2Findings;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.Network;
using O2.DotNetWrappers.DotNet;
using O2.Views.ASCX;
using O2.External.SharpDevelop.AST;
using O2.External.SharpDevelop.ExtensionMethods;
using O2.External.SharpDevelop.Ascx;
//using O2.External.IE.ExtensionMethods;
//using O2.External.IE.Wrapper;
using O2.API.AST.CSharp;
using O2.API.AST.ExtensionMethods;
using O2.API.AST.ExtensionMethods.CSharp;
using ICSharpCode.TextEditor;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast; 
using ICSharpCode.SharpDevelop.Dom;
using ICSharpCode.SharpDevelop.Dom.CSharp;
using System.CodeDom;
using O2.Views.ASCX.O2Findings;
using O2.Views.ASCX.DataViewers;
using System.Security.Cryptography;

//O2Ref:O2_API_AST.dll

namespace O2.XRules.Database.Utils
{
	public static class ExtraMethodsToAddToO2CodeBase_IO
	{
		public static string safeFileName(this string _stringToConvert, int maxLength)
		{			
			var safeName = _stringToConvert.safeFileName();
			if (maxLength > 10 && safeName.size() > maxLength)
				return "{0} ({1}){2}".format(
							safeName.Substring(0, maxLength - 10),
							3.randomNumbers(),
							_stringToConvert.Substring(_stringToConvert.size()-9).extension());
			return safeName;
		}
		
		public static float availableMemory(this object _object)
		{
			return new System.Diagnostics.PerformanceCounter("Memory", "Available MBytes").NextValue();
		}
		
		public static string infoAvailableMemory(this object _object)
		{
			var availableMemory = new System.Diagnostics.PerformanceCounter("Memory", "Available MBytes")
														.NextValue()
														.str();
			"AvailableMemory: {0}Mb".info(availableMemory);
			return availableMemory;
		}

		public static string askUser(this string question)
		{
			return question.askUser("Question", "");
		}
		
		public static string askUser(this string question, string title, string defaultValue)
    	{
    		var assembly =  "Microsoft.VisualBasic".assembly();
			var intercation = assembly.type("Interaction");
 
			var parameters = new object[] {question,title,defaultValue,-1,-1}; 
			return intercation.invokeStatic("InputBox",parameters).str(); 
    	}
	}	   
}
    	
		
		