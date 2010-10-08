// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Xml;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
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
using O2.Views.ASCX.classes.MainGUI;
using O2.External.SharpDevelop.AST;
using O2.External.SharpDevelop.ExtensionMethods;
using O2.External.SharpDevelop.Ascx;
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
		// Config files
        public static Panel editLocalConfigFile(this string file)
        {
            var panel = O2Gui.open<Panel>("Editing local config file: {0}".format(file), 700, 300);
            return file.editLocalConfigFile(panel);
        }

		//DateTime extensionMethods
		
		public static string safeFileName(this DateTime dateTime)
		{
			return Files.getSafeFileNameString(dateTime.str()); 
		}
		
		// ASCX TextBox
		
		public static TextBox allowTabs(this TextBox textBox)
		{
			return textBox.acceptsTab();
		}
		public static TextBox acceptsTab(this TextBox textBox)
		{
			return textBox.acceptsTab(true);
		}
		public static TextBox acceptsTab(this TextBox textBox, bool value)
		{
			textBox.invokeOnThread(()=> textBox.AcceptsTab = value);
			return textBox;
		}
		// Collections Dictionary<string,string>
		
		
		//Processes ExtensionMethods API				
		
		
	
		// Controls ExtensionMethods
		
		public static Form opacity(this Form form, double value)			
		{
			form.invokeOnThread(
				()=>{
						form.Opacity = value;
					});
			return form;
		}
		// Controls DataGridView ExtensionMethods
			
	}	   
}
    	
		
		