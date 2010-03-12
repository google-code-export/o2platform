// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Linq;
using System.Collections.Generic;
//O2Ref:WindowsBase.dll
//O2Ref:PresentationCore.dll
//O2Ref:PresentationFramework.dll
using System.Windows;
using System.Windows.Controls;
//O2Ref:GraphSharp.Controls.dll 
using GraphSharp.Controls;
//O2Ref:GraphSharp.dll
//O2Ref:QuickGraph.dll
using QuickGraph;
//O2File:GraphSharp_ExtensionMethods.cs
using O2.DotNetWrappers.ExtensionMethods;

namespace O2.API.Visualization.ExtensionMethods
{
    public static class VerticesAndEdges_ExtensionMethods
    {    
    
    	public static List<object> nodes(this GraphLayout graphLayout)
    	{    	 	        
            return graphLayout.vertices();
    	}
    	
		public static object node(this GraphLayout graphLayout, int nodeId)
    	{    	 	        
            var vertices = graphLayout.vertices();
            if (vertices.size() > nodeId)
            	return vertices[nodeId];
            return null;
    	}    	
    	public static List<object> vertices(this GraphLayout graphLayout)
    	{    	 	        
            return graphLayout.get_Graph().Vertices.ToList();
    	}
    	
    	public static GraphLayout edgeToAll(this GraphLayout graphLayout, object vertexToLink)
    	{
    		foreach(var vertex in graphLayout.vertices())
				graphLayout.edge(vertexToLink,vertex);
			return graphLayout;
    	}
    	
    	public static GraphLayout  edgeFromAll(this GraphLayout graphLayout, object vertexToLink)
    	{
    		foreach(var vertex in graphLayout.vertices())
				graphLayout.edge(vertex,vertexToLink);
			return graphLayout;
    	}
    	
    	public static GraphLayout  edgeToFirst(this GraphLayout graphLayout, object vertexToLink)
    	{
    		var vertices = graphLayout.vertices();
    		graphLayout.edge(vertexToLink, vertices[0]);
    		return graphLayout;    		
    	}
    	
    	public static GraphLayout  edgeFromLast(this GraphLayout graphLayout, object vertexToLink)
    	{
    		var vertices = graphLayout.vertices();
    		graphLayout.edge(vertices[vertices.size()-2],vertexToLink);
    		return graphLayout;    		
    	}
    	    	    		    
    }
}
