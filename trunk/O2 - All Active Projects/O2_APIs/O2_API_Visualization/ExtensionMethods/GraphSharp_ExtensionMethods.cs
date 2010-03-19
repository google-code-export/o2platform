// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Linq;
using System.Collections.Generic;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
//O2Ref:GraphSharp.Controls.dll 
using GraphSharp.Controls;
//O2Ref:GraphSharp.dll
//O2Ref:QuickGraph.dll
using QuickGraph;
//O2Ref:PresentationCore.dll
//O2Ref:PresentationFramework.dll
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System;

//O2File:WPF_Threading_ExtensionMethods.cs
//O2File:GraphFactory.cs

namespace O2.API.Visualization.ExtensionMethods
{
    public static class GraphSharp_ExtensionMethods
    {    
    	#region thread related
    	
    	public static T newInThread<T>(this GraphLayout graphLayout) where T : UIElement
		{
			return (T)graphLayout.wpfInvoke(
    			()=>{ 
    					return (T)typeof(T).ctor();    					
    				});
		}  
		
    	#endregion
        
        #region GraphLayout

        public static BidirectionalGraph<object, IEdge<object>> get_Graph(this GraphLayout graphLayout)
    	{
    		return (BidirectionalGraph<object, IEdge<object>>)
    			graphLayout.wpfInvoke(
    						()=> {
    								return graphLayout.Graph; 
    							 });
    	}
    	
    	public static GraphLayout add(this GraphLayout graphLayout,object vertexToAdd)
    	{
            if (graphLayout == vertexToAdd)     // can add graphLayout as an Node
                return graphLayout;
    		var graph = graphLayout.get_Graph();
    		if (graph == null)
    		{
    			"in GraphLayout.add, get_Graph() returned null".error();
    			return graphLayout;
    		}
    		return (GraphLayout)graphLayout.wpfInvoke(
    			()=>{
    					graph.add(vertexToAdd);
    					return graphLayout;
    				});
    	}
    	
    	public static GraphLayout edge(this GraphLayout graphLayout,object fromVertex, object toVertex)
    	{
            if (graphLayout == fromVertex || graphLayout == toVertex)  // can add graphLayout as an edge
                return graphLayout;
    		var graph = graphLayout.get_Graph();
    		return (GraphLayout)graphLayout.wpfInvoke(
    			()=>{
    					graph.edge(fromVertex,toVertex);
    					return graphLayout;
    				});
    	}
    	
    	public static GraphLayout set(this GraphLayout graphLayout, BidirectionalGraph<object, IEdge<object>> graph)
		{
			return (GraphLayout)graphLayout.wpfInvoke(
    			()=>{
    					graphLayout.Graph = graph;
    					return graphLayout;
    				});
		}    	    	    	    	    				    	    	    	    	
    	        	
    	public static GraphLayout background(this GraphLayout graphLayout, Brush brush)
    	{
	    	return (GraphLayout)graphLayout.wpfInvoke(
    			()=>{  
			    		graphLayout.Background = brush;		    		
			    		return graphLayout;    		
			    	});
    	}
    	
    	public static GraphLayout wait(this GraphLayout graphLayout, int miliSeconds)
    	{
    		return graphLayout.sleep(miliSeconds);
    	}
    	
    	public static GraphLayout sleep(this GraphLayout graphLayout, int miliSeconds)
    	{
    		System.Threading.Thread.Sleep(miliSeconds);
    		return graphLayout;
    	}
    	
    	#endregion
    	
    	#region GraphLayout - defaults
    	
    	public static GraphLayout clear(this GraphLayout graphLayout)
    	{
    		graphLayout.invoke("RemoveAllGraphElement");
			graphLayout.newGraph();
			return graphLayout;
		}
    	
    	public static GraphLayout newGraph(this GraphLayout graphLayout)
    	{
    		return (GraphLayout)graphLayout.wpfInvoke(
    			()=>{   
    					graphLayout.Graph = new BidirectionalGraph<object, IEdge<object>>();
			    		return graphLayout;
			    	});
    	}
    	
    	
    	public static GraphLayout defaultLayout(this GraphLayout graphLayout)
    	{
    		return (GraphLayout)graphLayout.wpfInvoke(
    			()=>{
                        try
                        {
                            graphLayout.LayoutAlgorithmType = "Circular";
                            graphLayout.OverlapRemovalAlgorithmType = "FSA";
                            graphLayout.HighlightAlgorithmType = "Simple";
                            // this one was throwing an error on new graphs when there is not graph loaded (but the value seems to be set)
                            graphLayout.OverlapRemovalConstraint = GraphSharp.Controls.AlgorithmConstraints.Must; //Automatic;
                            
                        }
                        catch (Exception ex)
                        {
                            "in defaultLayout: {0}".format(ex.Message).debug();
                        }
			           	return graphLayout;
			         });
        }

        public static GraphLayout overlapRemovalParameters(this GraphLayout graphLayout, float horizontalGap, float verticalGap)
        {
            return (GraphLayout)graphLayout.wpfInvoke(
                () =>
                {
                    var overlapRemovalParameters = new GraphSharp.Algorithms.OverlapRemoval.OverlapRemovalParameters();
                    overlapRemovalParameters.HorizontalGap = horizontalGap;
                    overlapRemovalParameters.VerticalGap = verticalGap;
                    graphLayout.OverlapRemovalParameters = overlapRemovalParameters;
                    return graphLayout;
                });
        }
        
/*    	public static GraphLayout betterLayout(this GraphLayout graphLayout)
    	{
    		return (GraphLayout)graphLayout.wpfInvoke(
    			()=>{
                        try
                        {                            
                            // this one was throwing an error on new graphs
                            
                        }
                        catch(Exception ex)
                        {
                            ex.log("in betterLayout");
                        }
			           	return graphLayout;
			         });
        }*/
        
       	public static GraphLayout testGraph(this GraphLayout graphLayout)
    	{    	 	        
            return graphLayout.set(GraphFactory.testGraph());            
    	 }
    	
    	
    	
    	#endregion
    	
    	#region GraphLayout - LayoutAlgorithmType
    	
    	public static List<string> layouts(this GraphLayout graphLayout)
    	{
    		return new List<string>( 
    			new [] {
				            "Circular",
				            "Tree",
				            "FR",
				            "BoundedFR",
				            "KK",
				            "ISOM",
				            "LinLog",
				            "CompoundFDP"
				            //,
				         //   "EfficientSugiyama"
				        });
    	}
    	    	
    	public static GraphLayout tree(this GraphLayout graphLayout)
    	{
    		return graphLayout.layout("Tree");
    	}
    	
    	public static GraphLayout circular(this GraphLayout graphLayout)
    	{
    		return graphLayout.layout("Circular");
    	}
    	
    	public static GraphLayout fr(this GraphLayout graphLayout)
    	{
    		return graphLayout.layout("FR");
    	}
    	
    	public static GraphLayout boundedFR(this GraphLayout graphLayout)
    	{
    		return graphLayout.layout("BoundedFR");
    	}    
    	
    	public static GraphLayout efficientSugiyama(this GraphLayout graphLayout)
    	{
    		return graphLayout.layout("EfficientSugiyama");
    	}
    	
    	public static GraphLayout allL(this GraphLayout graphLayout)
    	{
    		return graphLayout.showAllLayouts();
    	}
    	
    	public static GraphLayout allLayouts(this GraphLayout graphLayout)
    	{
    		return graphLayout.showAllLayouts();
    	}
    	public static GraphLayout showAllLayouts(this GraphLayout graphLayout)
    	{
    		return graphLayout.showAllLayouts(1000);
    	}
    	public static GraphLayout showAllLayouts(this GraphLayout graphLayout, int miliSeconds) 
    	{
    		foreach(var layoutType in graphLayout.layouts())
    		{
    			"showing layout: {0}".format(layoutType).debug();  
    			graphLayout.layout(layoutType).sleep(miliSeconds);
    		}
			return graphLayout;
    	}
    	
    	public static GraphLayout layout(this GraphLayout graphLayout, string type)
    	{
    		return (GraphLayout)graphLayout.wpfInvoke(()=>
	    		{
	    			try
	    			{
	    				graphLayout.LayoutAlgorithmType = type;
	    			}
	    			catch (System.Exception ex)
	    			{
	    				ex.log("in GraphLayout layout ({0}) ".format(type));
	    			}
	    			return graphLayout;
	    		});
    	}
    	
    	#endregion
    	
    	#region BidirectionalGraph
    	
    	public static void add(this BidirectionalGraph<object, IEdge<object>> graph,object vertexToAdd)
    	{
    		graph.AddVertex(vertexToAdd);
    	}    	
    	
    	public static void edge(this BidirectionalGraph<object, IEdge<object>> graph,object fromVertex, object toVertex)
    	{
    		try
    		{
    			graph.AddVertex(fromVertex);
    			graph.AddVertex(toVertex);
    			graph.AddEdge(new Edge<object>(fromVertex,toVertex) );
    		}
    		catch(System.Exception ex) 
    		{ex.log("in edge");}
    	}
    	  	
    	public static void edges(this BidirectionalGraph<object, IEdge<object>> bidirectionalGraph,  List<IEdge<object>> edges)
    	{
    		try
    		{
    			bidirectionalGraph.AddVerticesAndEdgeRange(edges.ToArray());
    		}
    		catch(System.Exception ex)
    		{ex.log("in edges");}
    	}

        public static void addList<T>(this BidirectionalGraph<object, IEdge<object>> graph, IEnumerable<T> list, string nodeText)
        {
            foreach (var item in list)
                graph.edge(nodeText, item.str());
        }

    	#endregion
    	
    }
}
