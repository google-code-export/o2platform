// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
//O2Tag_OnlyAddReferencedAssemblies
  
using System;
using System.Windows.Forms;

using FluentSharp.O2.Kernel.ExtensionMethods;
using FluentSharp.O2.DotNetWrappers.ExtensionMethods;
using FluentSharp.O2.DotNetWrappers.Windows;
using FluentSharp.O2.Views.ASCX.Ascx.MainGUI;
using FluentSharp.O2.Views.ASCX.classes.MainGUI;
using V2.O2.External.SharpDevelop.ExtensionMethods;
using V2.O2.External.SharpDevelop.Ascx;  

//O2Ref:FluentSharp_O2.dll
//O2Ref:V2_O2_External_SharpDevelop.dll
//O2Ref:V2_O2SharpDevelop.dll

//O2Ref:System.dll
//O2Ref:System.Windows.Forms.dll

//O2Ref:System.Drawing.dll
//O2Ref:System.Xml.dll
//O2Ref:System.Core.dll
//O2Ref:System.Data.dll  
//O2Ref:System.Xml.Linq.dll


namespace V2.O2.Platform
{
    public class Launcher
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        
        public void Main()
        {
        	var logViewer = showLogViewer();         	        	
			"This is O2's first Script!!".info(); 
			"Current AppDomain: {0}".info(AppDomain.CurrentDomain.BaseDirectory); 
				
			Console.WriteLine("type: {0}".format(typeof(ascx_SourceCodeEditor)));
			
			var topPanel = O2Gui.open<Panel>("Test gui",400,400);
			//topPanel.insert_LogViewer();
			topPanel.add_SourceCodeEditor();
			Console.WriteLine("type: {0}".format(new ascx_SourceCodeEditor()));
//			Processes.startProcess("O2_XRules_Database.exe", "\"Util - O2 Available scripts.h2\"");
			
//			logViewer.parentForm().close();
        }    
        
        public static ascx_LogViewer showLogViewer()
        {
        	return O2Gui.open<ascx_LogViewer>();			        	
        }
    }
}