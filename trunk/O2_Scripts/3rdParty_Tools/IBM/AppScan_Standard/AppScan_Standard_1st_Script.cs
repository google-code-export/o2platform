﻿// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (<a href="http://www.apache.org/licenses/LICENSE-2.0">http://www.apache.org/licenses/LICENSE-2.0</a>)
using System;
using System.Windows.Forms;
using O2.Kernel.ExtensionMethods;  
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Views.ASCX.Ascx.MainGUI; 
using O2.Views.ASCX.classes.MainGUI;

using O2.External.SharpDevelop.ExtensionMethods;
//using O2.DotNetWrappers.H2Scripts;
using O2.XRules.Database.Utils;

//O2Ref:O2_Kernel.dll
//O2Ref:O2_Interfaces.dll
//O2Ref:O2_DotNetWrappers.dll
//O2Ref:O2_Views_ASCX.dll
//O2Ref:O2_XRules_Database.exe
//O2Ref:O2_External_SharpDevelop.dll
//O2Ref:O2SharpDevelop.dll
//O2Ref:O2_API_AST.dll
//O2Ref:log4net.dll

//O2Ref:System.dll
//O2Ref:System.Windows.Forms.dll

//O2Ref:System.Drawing.dll
//O2Ref:System.Xml.dll
//O2Ref:System.Core.dll
//O2Ref:System.Data.dll 
//O2Ref:System.Xml.Linq.dll

//O2File:Scripts_ExtensionMethods.cs
//O2File:_Extra_methods_Reflection.cs

namespace V2.O2.Platform
{
    public class Launcher
    {   
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
       
        public void Main()
        {
            //if (Control.ModifierKeys == Keys.Shift)
                showLogViewer();                                    
            "Current AppDomain: {0}".info(AppDomain.CurrentDomain.BaseDirectory);
            //showScriptEditor();
            
            startAppScan();
            
            //O2Thread.staThread(()=>{NUnit.Gui.AppEntry.Main(new string [] {});});
            O2Thread.mtaThread(
                ()=>{       
                		"Trying to get lastFormLoaded".info();
                        var maxAttempts = 25;
                        Form appScanForm = null;
                        while (maxAttempts -- > 0)
                        {
                        	this.sleep(3000);	
                        	appScanForm ="".lastFormLoaded();
                        	if (appScanForm.notNull())
                        	{
                        		"Waiting for main window to load".debug();//: {0}".info(appScanForm.get_Text());
                        		if (appScanForm.get_Text() == "Untitled - IBM Rational AppScan")
                        			break;
                        		else
                        			appScanForm = null;
                        	}
                        	
                        }
                        if (appScanForm.isNull())
                        {
                            "Could not get a reference to the main window (opening up a Script Editor to help debugging the problem)".error();
                            showScriptEditor();
                         }
                        else
                        {
                            "Got reference to the main window".info();
                            
                            var scriptToExecute = @"C:\O2\O2Scripts_Database\_Scripts\3rdParty_Tools\IBM\AppScan_7.9\In AppScan - Create O2 Gui.h2";
                            "Executing Script: {0}".info(scriptToExecute);
                            scriptToExecute.compile_H2Script().executeFirstMethod();
                            //var tab =      nUnit.controls<TabControl>(true)[1];     
                            //var scriptEditor = tab.add_Tab("O2Script").add_Script();
                           
                        /*    var scriptEditor = O2Gui.open<Panel>("NUnit Scripts",600,300).add_Script().parentForm().top(600);                       
                           
                            @"E:\_XRules_Local\NUnit_Injection\Scripts\inject error viewer.h2".compile_H2Script().executeFirstMethod();
                            @"E:\_XRules_Local\NUnit_Injection\Scripts\Run NUnit - BusinessLogic Class.h2".compile_H2Script().executeFirstMethod();
                            
*/                                 
                           
						}
               
                    });

        }   
       
        public static ascx_LogViewer showLogViewer()
        {
            return O2Gui.open<ascx_LogViewer>();                       
        }
        
        public static void showScriptEditor()
        {        	
			var scriptEditor = O2Gui.open<Panel>("AppScan Script editor",600,300).add_Script(false);
			scriptEditor.Code = "var appScan =  \"\".lastFormLoaded();".line() + 
								"appScan.set_Text(\"AppScan Standard - O2 Version\");".line() + 
								"return appScan;";
			scriptEditor.parentForm().top(600);
        }
        
		public static void startAppScan()
        {
        	O2Thread.staThread(
				()=>{
						"AppScan.exe".assembly()
								.type("MainForm")
								.method("Main").invoke(new object[] {new string[] {} }) ;  
					});
        }
    }
}