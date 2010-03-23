using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph;
using O2.Kernel.ExtensionMethods;

namespace O2.API.AST.ExtensionMethods
{
    public static class BidirectionalGraph_ExtensionMethods
    {        
        public static void add(this BidirectionalGraph<object, IEdge<object>> graph, object vertexToAdd)
        {
            graph.AddVertex(vertexToAdd);
        }

        public static void edge(this BidirectionalGraph<object, IEdge<object>> graph, object fromVertex, object toVertex)
        {
            try
            {
                graph.AddVertex(fromVertex);
                graph.AddVertex(toVertex);
                graph.AddEdge(new Edge<object>(fromVertex, toVertex));
            }
            catch (System.Exception ex)
            { ex.log("in edge"); }
        }

        public static void edges(this BidirectionalGraph<object, IEdge<object>> bidirectionalGraph, List<IEdge<object>> edges)
        {
            try
            {
                bidirectionalGraph.AddVerticesAndEdgeRange(edges.ToArray());
            }
            catch (System.Exception ex)
            { ex.log("in edges"); }
        }

        public static void addList<T>(this BidirectionalGraph<object, IEdge<object>> graph, IEnumerable<T> list, string nodeText)
        {
            foreach (var item in list)
                graph.edge(nodeText, item.str());
        }


        #region search
        public static object firstOutEdge(this BidirectionalGraph<object, IEdge<object>> graph, object node)
        {
            var edges = graph.OutEdges(node).ToList();

            if (edges.Count > 0)
                return edges[0].Target;

            "in graph.firstOutEdge there were no edges found for provided node".error();
            return null;
        }
        #endregion
    }
}
