using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.API.AST.CSharp;
using GraphSharp.Controls;

namespace O2.API.Visualization.ExtensionMethods
{
    public static class GraphLayout_O2CodeStream_ExtensionMethods
    {
        public static O2CodeStream show(this O2CodeStream o2CodeStream, GraphLayout graphLayout)
        {
            graphLayout.newGraph();
            graphLayout.tree();
            foreach (var streamNode in o2CodeStream.StreamNode_First)
                streamNode.show_StreamNode(graphLayout, null);
            return o2CodeStream;
        }

        public static void show_StreamNode(this O2CodeStreamNode streamNode, GraphLayout graphLayout, O2CodeStreamNode previousNode)
        {
            if (streamNode == null)
                return;

            if (previousNode == null)
                graphLayout.add_Node(streamNode);
            else
                graphLayout.add_Edge(previousNode, streamNode);

            foreach (var childNode in streamNode.ChildNodes)
            {
                childNode.show_StreamNode(graphLayout, streamNode);
            }
        }
    }
}
