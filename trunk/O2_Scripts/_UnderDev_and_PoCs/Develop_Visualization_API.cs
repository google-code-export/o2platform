// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
//O2Ref:System.Core.dll
//O2Ref:PresentationCore.dll
//O2Ref:PresentationFramework.dll
//O2Ref:WindowsFormsIntegration.dll
//O2Ref:WindowsBase.dll
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Forms = System.Windows.Forms;
using System.Windows.Controls;
using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX;
using O2.Views.ASCX.DataViewers;
using O2.Views.ASCX.classes.MainGUI;
using O2.API.Visualization.Ascx;
//O2Ref:GraphSharp.Controls.dll 
using GraphSharp.Controls;
//O2Ref:GraphSharp.dll
//O2Ref:QuickGraph.dll
using QuickGraph;
//O2Ref:WPFExtensions.dll
using WPFExtensions.Controls;
using O2.API.Visualization.ExtensionMethods;
//O2Ref:O2_API_Visualization.dll
using O2.XRules.Database.Utils.O2;
//O2File:ascx_Simple_Script_Editor.cs.o2
//O2File:Scripts_ExtensionMethods.cs

namespace O2.Script
{
    public class Develop_Visualization_API : Forms.Control
    {    
    	public void test()
    	{
    		
	    	var panel = O2Gui.load<Forms.Panel>("Test Visualization API", 700,450);		// weird layout bug happens if the height is set to 400
	    		
    		var controls = panel.add_1x1("1","2",true,200);
    		
    	//	controls[1].add_ScriptExecution();
    		    	    		    		
    		//var graphLayout = controls[0].add_WPF_Control<GraphLayout>();
    		
    		var xamlHost = controls[0].add_Control<ascx_Xaml_Host>();    	    		
    		var graphLayout = xamlHost.add_Control<GraphLayout>();
    		
    		var scriptExecution =  (ascx_Simple_Script_Editor)controls[1].add_ScriptExecution();
    		scriptExecution.InvocationParameters.Add("graph",graphLayout);
    		scriptExecution.InvocationParameters.Add("elementHost",xamlHost.element());
    		scriptExecution.Code = defaultCode();
    		    		    		    	
    		graphLayout.wpfInvoke(()=>
    		{
    			graphLayout.LayoutAlgorithmType = "Circular";
    			graphLayout.OverlapRemovalConstraint = GraphSharp.Controls.AlgorithmConstraints.Automatic;			    			
    			graphLayout.OverlapRemovalAlgorithmType="FSA";
                graphLayout.HighlightAlgorithmType="Simple";  
                graphLayout.Graph = getGraph();
                
    		});
    		return;
			    		//controls[0].add_WPF_Control<ZoomControl>();			    					    	
			    		
			    		var panel2 = controls[1].add_WPF_Control<Canvas>();
			    		if (panel2 == null)
			    			"panel was null".error();
			    		else
			    			panel2.wpfInvoke(
			    			()=> {
			    					var b = new Label();
			    					//show.info(b);
			    					b.Content = "bottom label";
			    					panel2.Children.Add(b);
			    					panel2.Children.Add(new Button());
			    				 });
			    				 
			    		//var ui = new System.Windows.UIElement();
			    		//var ascxHost1 = controls[0].add_Control<ascx_Xaml_Host>();
			    		//ascxHost1.element().Child = ui;
			    		/*var ascxHost1 = controls[0].add_Control<ascx_Xaml_Host>();
			    		var label_ = ascxHost1.add_Label("my first wpf label");*/
			    		//label_.set_Text("asds");
			    		//var label2 = ascxHost1.showLabel();
			    		
			    		
			    		//var scriptExecution =  (ascx_Simple_Script_Editor)controls[1].add_ScriptExecution();
			    		//scriptExecution.typeName().info();
			    		//var aa = new O2.XRules.Database.O2Utils.ascx_Simple_Script_Editor();
			    		//scriptExecution.InvocationParameters.Add("host",ascxHost1);
			    		//show.info(scriptExecution);
			    		
			    		//controls[1].add_TextBox();
			    		//var propertyGrid = controls[1].add_Control<PropertyGrid>();
			    
			    		/*ascxHost1.invokeOnThread(
			    			()=>{			    								    				
				    				//var label = new O2.API.Visualization.Xaml.myLabel();
				    				var label = ascxHost1.showLabel();
				    				label.Content = "AA";
				    				scriptExecution.InvocationParameters.Add("label",label);
				    				//ascxHost1.element().Child = label;
				    				"after".debug();
				    				//propertyGrid.show(label);
				    				//show.info(label.type().methods());
				    				//label.getLabel().typeFullName().debug();
				    			});
				    			*/
				    	/*ascxHost2.invokeOnThread(
			    			()=>{		
				    				var label = new O2.API.Visualization.Xaml.Label();
				    				//var pg = show.propertyGrid();
				    			//	pg.typeName().debug();
				    				var showInfo = (ascx_ShowInfo)show.propertyGrid();
				    				//pg.invoke("show",label);
				    				
				    				showInfo.propertyGrid.SelectedObject = label;
				    				return;
				    				if (label != null)
				    					"label was not null".debug();
				    					//ascxHost2.showLabel();
			    					ascxHost2.element().Child = label;
			    					
			    					"at end".debug();
				    			});*/
			    		//
			    		//var label = new O2.API.Visualization.Xaml.Label();
			    		//ascxHost.element().Child = label;
			    //	});
	    		//});
    		}
    	 
		public string defaultCode()
		{
			var code = @"//include C:\O2\_XRules_Local\_ScriptIncludes\Wpf.txt";
			code = code.line().add("return \"ok\";");
			return code;			
		}

    	 public IBidirectionalGraph<object, IEdge<object>> getGraph()
    	 {    	 	
            var g = new BidirectionalGraph<object, IEdge<object>>();
            var vertices = new object[] { "A", "B", "C", "D", "E", "F" };
            var edges = new IEdge<object>[] {
                    new Edge<object>(vertices[0], vertices[1]),
                    new Edge<object>(vertices[1], vertices[2]),
                    new Edge<object>(vertices[1], vertices[3]),
                    new Edge<object>(vertices[3], vertices[4]),
                    new Edge<object>(vertices[0], vertices[4]),
                    new Edge<object>(vertices[4], vertices[5])
                };
            g.AddVerticesAndEdgeRange(edges);
            return g;
    	 }
    }
}
