using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.API.AST.CSharp;
using GraphSharp.Controls;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;

namespace O2.API.Visualization.ExtensionMethods
{
    public static class GraphLayout_O2CodeStream_ExtensionMethods
    {
        public static O2CodeStream show(this O2CodeStream o2CodeStream, GraphLayout graphLayout)
        {
            try
            {                  
                //graphLayout.newGraph();
                graphLayout.tree();                
                foreach (var streamNode in o2CodeStream.StreamNode_First)
                    streamNode.show_StreamNode(graphLayout, null);
            }
            catch (Exception ex)
            {
                ex.log("in O2CodeStream.show GraphLayout");
            }
            return o2CodeStream;
        }

        public static void show_StreamNode(this O2CodeStreamNode streamNode, GraphLayout graphLayout, O2CodeStreamNode previousNode)
        {
            try
            {
                if (streamNode == null)
                    return;

                if (previousNode == null)
                    graphLayout.add_Node(new O2.API.AST.Graph.O2GraphASTNode(streamNode,null));
                else
                    graphLayout.add_Edge(new O2.API.AST.Graph.O2GraphASTNode(previousNode,null), new O2.API.AST.Graph.O2GraphASTNode(streamNode,null));


                foreach (var childNode in streamNode.ChildNodes)
                {
                    if (streamNode != childNode)
                        childNode.show_StreamNode(graphLayout, streamNode);
                    else
                        "in show_StreamNode, streamNode ==  childNode: {0}".error(childNode.Text);
                }
            }
            catch (Exception ex)
            {
                ex.log("in show_StreamNode");
            }
        }
    }
}
