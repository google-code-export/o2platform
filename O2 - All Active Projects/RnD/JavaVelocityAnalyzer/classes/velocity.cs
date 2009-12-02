using System;
using O2.Rnd.JavaVelocityAnalyzer;


namespace O2.Rnd.JavaVelocityAnalyzer.classes
{
    public class velocity
    {
        // list of NodeTypes from http://velocity.apache.org/engine/devel/apidocs/org/apache/velocity/runtime/parser/node/SimpleNode.html
        // and http://velocity.apache.org/engine/devel/apidocs/org/apache/velocity/runtime/parser/node/ASTMathNode.html

        #region NodeType enum

        public enum NodeType
        {
            ASTAndNode,
            ASTAssignment,
            ASTBlock,
            ASTComment,
            ASTDirective,
            ASTElseIfStatement,
            ASTElseStatement,
            ASTEQNode,
            ASTEscape,
            ASTEscapedDirective,
            ASTExpression,
            ASTFalse,
            ASTFloatingPointLiteral,
            ASTGENode,
            ASTGTNode,
            ASTIdentifier,
            ASTIfStatement,
            ASTIncludeStatement,
            ASTIntegerLiteral,
            ASTIntegerRange,
            ASTLENode,
            ASTLTNode,
            ASTMap,
            ASTMathNode,
            ASTMethod,
            ASTNENode,
            ASTNotNode,
            ASTObjectArray,
            ASTOrNode,
            ASTParameters,
            ASTprocess,
            ASTReference,
            ASTSetDirective,
            ASTStop,
            ASTStringLiteral,
            ASTText,
            ASTTrue,
            ASTVariable,
            ASTWord,
            ASTAddNode,
            ASTDivNode,
            ASTModNode,
            ASTMulNode,
            ASTSubtractNode
        }

        #endregion

        #region Nested type: VelocityNode

        public class VelocityNode
        {
            public int iDepth;
            public NodeType ntNodeType;
            public String sAllTokens;
            public String sOtherType;
            public String sRootToken;

            public VelocityNode(String sNodeType, String iDepth, String sToken)
            {
                ntNodeType = (NodeType) Enum.Parse(typeof (NodeType), sNodeType);
                this.iDepth = Int32.Parse(iDepth);
            }

            public VelocityNode(String sLine)
            {
                String[] lsSplittedLine = sLine.Split('\t');
                if (lsSplittedLine.Length != 4)
                    DI.log.error("in VelocityNode.ctor: Error loading line, there should be 3 fields on it: {0}",
                                 sLine);
                else
                {
                    ntNodeType = (NodeType) Enum.Parse(typeof (NodeType), lsSplittedLine[0]);
                    iDepth = Int32.Parse(lsSplittedLine[1]);
                    sRootToken = lsSplittedLine[2];
                    sAllTokens = lsSplittedLine[3];
                }
            }

            public override string ToString()
            {
                return String.Format("{0} [{1}]: {2}     - {3}", ntNodeType, iDepth, sRootToken, sAllTokens);
            }
        }

        #endregion
    }
}