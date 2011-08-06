﻿// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (<a href="http://www.apache.org/licenses/LICENSE-2.0">http://www.apache.org/licenses/LICENSE-2.0</a>)
//O2Tag_OnlyAddReferencedAssemblies
using System;    
using System.Windows.Forms;    
using O2.Kernel.ExtensionMethods; 
using O2.Kernel.Objects;   
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.ExtensionMethods;   
using O2.Views.ASCX.Ascx.MainGUI;
using O2.Views.ASCX.classes.MainGUI; 

 
//O2Ref:O2_Interfaces.dll
//O2Ref:O2_Kernel.dll
//O2Ref:O2_DotNetWrappers.dll
//O2Ref:O2_Views_ASCX.dll
//O2Ref:O2_XRules_Database.exe

namespace O2.Platform
{ 
    static class Launcher   
    { 
        /// <summary>
        /// The main entry point for the application.
        /// </summary></pre>


        [STAThread] 
        static void Main(string[] args)
        {         	
        	//AppDomain.CurrentDomain.UnhandledException += new  UnhandledExceptionEventHandler((sender,e)=> { Console.WriteLine(e.ExceptionObject); } );
        	 
            if (Control.ModifierKeys == Keys.Shift)
            	showLogViewer();       //.parentForm().width(1000).height(400);                
            var firstScript = AppDomain.CurrentDomain.BaseDirectory.pathCombine("AppScan_Standard_1st_Script.cs");           
            Console.WriteLine("Welcome to the O2 Platform ...");
            Console.WriteLine("Launching IBM AppScan Standard ...");
            
            "Current AppDomain: {0}".info(AppDomain.CurrentDomain.BaseDirectory);
           	
            //CompileEngine.lsGACExtraReferencesToAdd.Clear();
            var assembly = new CompileEngine().compileSourceFile(firstScript);
            if (assembly.notNull())           
            {
                Console.WriteLine("Executing script {0} from location {1}".info(firstScript, assembly.Location));
                if (assembly.methods().size()>0)
                {
                    assembly.methods()[0].invoke();
                    Console.WriteLine("Invocation complete".info());
                }
                else 
                    Console.WriteLine("Error: there were no methods in compiled assembly".error());           
            }
            else
                Console.WriteLine("Error: could not find, compile or execute first script ({0})".error(firstScript));            
        
        }               
       
        public static ascx_LogViewer showLogViewer()
        {
        	try  
        	{
            	return O2Gui.open<ascx_LogViewer>();
            }
            catch(Exception ex)
            {
            	Console.WriteLine("Error: in showLogViewer: {0}".format(ex.Message));
            	return null;
            }
        }            
         
    }
}