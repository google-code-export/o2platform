// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.API.Visualization.Ascx;
using System.Windows.Forms;
using O2.Views.ASCX.classes.MainGUI;
using O2.External.SharpDevelop.ExtensionMethods;
using O2.XRules.Database.O2Utils;
//O2File:extra.cs 
//O2File:WPF_Controls_ExtensionMethods.cs
//O2File:WPF_WinFormIntegration_ExtensionMethods.cs

namespace O2.Script
{
    public class ascx_GraphWithInspector : Control
    {        

		public static void runControl()
		{		
		 	O2Gui.load<ascx_GraphWithInspector>("Graph"); 		 
		}
		
		public ascx_GraphWithInspector()
		{						
			this.Width = 600;
			this.Height = 400;
			/*var editor = this.add_SourceCodeViewer();
			editor.set_Text("asd");
			return;
			*/
			var controls = this.add_1x1("Graph","Inspector",true,200);
			
			var graph = controls[0].add_Graph();
			
			//graph.testGraph();
			var xamlHost = (ascx_Xaml_Host)(controls[0].Controls[0]);
			var script = controls[1].add_Script().cast<ascx_Simple_Script_Editor>();  
			
			script.Code = "graph.add(100);"
						  .line().add(
						  "graph.edge(100,\"A\");"
						  .line().add(
						  "graph.edge(100,\"F\");" 
						  .line()
						  .line()
						  .add(@"//include C:\O2\_XRules_Local\_ScriptIncludes\Wpf.txt")));
			script.InvocationParameters.Add("graph", graph);
			script.InvocationParameters.Add("elementHost", xamlHost.element());			
        }
    	    	    	    	    
    }
}