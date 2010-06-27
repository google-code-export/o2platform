// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
//O2Ref:O2_API_Visualization.dll
//O2Ref:PresentationCore.dll
//O2Ref:PresentationFramework.dll
//O2Ref:WindowsBase.dll   
//O2Ref:System.Core.dll
//O2Ref:WindowsFormsIntegration.dll
//O2Ref:GraphSharp.dll
//O2Ref:QuickGraph.dll
//O2Ref:GraphSharp.Controls.dll
//O2Ref:WPFExtensions.dll
//O2Ref:O2_XRules_Database.exe
//O2Ref:O2SharpDevelop.dll
//O2Ref:O2_External_SharpDevelop.dll
//O2Ref:O2_External_IE.dll
using System;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Forms.Integration;
using System.Windows;
using QuickGraph;
using GraphSharp;
using GraphSharp.Controls;

using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.External.IE.Wrapper;
using O2.External.IE.ExtensionMethods;
using O2.External.SharpDevelop.ExtensionMethods;
using O2.API.Visualization.Ascx;
using O2.API.Visualization.ExtensionMethods;
using O2.Views.ASCX.classes.MainGUI;
using O2.XRules.Database.Utils.O2;
//O2File:Scripts_ExtensionMethods.cs 
//O2File:ascx_Simple_Script_Editor.cs.o2 

namespace O2.XRules.Database.Utils.O2
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
			var controls = this.add_1x1("Graph","Inspector",false,200);
			
			var graph = controls[0].add_Graph();
			
			//graph.testGraph();
			var xamlHost = (ascx_Xaml_Host)(controls[0].Controls[0]);
            var script = controls[1].add_Script();

            script.Code = "graph.testGraph();".line() +
                          "return \"comment (using //) this line to see more graph actions\";".line()
                          .line() +
						  "graph.node(100);".line() + 
						  "graph.edge(100,\"A\");".line() + 
						  "graph.edge(100,\"F\");" .line() +
                          "graph.edge(200,\"F\");".line() + 
                          "graph.edge(200,300);" .line() + 
                          "var blueLabel = graph.add_UIElement<WPF.Label>();" .line() + 
                          "blueLabel.set_Content(\"blue label\");" .line() + 
                          "blueLabel.color(\"Blue\");" .line() + 
                          "graph.edge(200,blueLabel);" .line() + 
                          "var redLabel = graph.add_UIElement<WPF.TextBox>().set_Text(\"red textbox\").color(\"Red\");" .line() +
                          "graph.edge(blueLabel,redLabel);".line() + 
                          "//graph.edgeToAll(200);".line() + 
                          "graph.defaultLayout();".line() + 
                          "//graph.overlapRemovalParameters(20, 50);".line() +  
						  "graph.showAllLayouts(2000);".line() + 
						  "graph.circular();".line().line() + 
                          "return graph;".line() + 
						  "//using System.Xml.Linq".line()+
						  "//using System.Linq".line()+
						  "//using WPF=System.Windows.Controls".line() + 
						  "//using O2.API.Visualization.ExtensionMethods".line()+
						  "//O2Ref:System.Xml.dll".line()+
						  "//O2Ref:System.Xml.Linq.dll".line()+
						  "//O2Ref:O2_API_Visualization.dll".line()+
						  "//O2Ref:PresentationCore.dll".line()+
						  "//O2Ref:PresentationFramework.dll".line()+
						  "//O2Ref:WindowsBase.dll   ".line()+
						  "//O2Ref:System.Core.dll".line()+
						  "//O2Ref:WindowsFormsIntegration.dll".line()+
						  "//O2Ref:GraphSharp.dll".line()+
						  "//O2Ref:QuickGraph.dll".line()+
						  "//O2Ref:GraphSharp.Controls.dll".line()+
						  "//O2Ref:ICSharpCode.AvalonEdit.dll".line();

			script.InvocationParameters.Add("graph", graph);
			script.InvocationParameters.Add("elementHost", xamlHost.element());
            script.onCompileExecuteOnce();
            script.compileCodeSnippet(script.Code);
        }    	    	    	    	    
    }
}