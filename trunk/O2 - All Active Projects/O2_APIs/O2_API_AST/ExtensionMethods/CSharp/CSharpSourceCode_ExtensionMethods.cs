using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.NRefactory.Ast;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.PrettyPrinter;

namespace O2.API.AST.ExtensionMethods.CSharp
{
    public static class CSharpSourceCode_ExtensionMethods
    {
        public static string csharpCode(this INode iNode)
        {
            var outputVisitor = new CSharpOutputVisitor();
            iNode.AcceptVisitor(outputVisitor, null);
            return outputVisitor.Text;
        }

        public static string csharpCode(this IParser parser)
        {
            return parser.CompilationUnit.csharpCode();
        }

        /*public static string csharpCode(this CompilationUnit compilationUnit)
        {
            var outputVisitor = new CSharpOutputVisitor();
            //using (SpecialNodesInserter.Install(specials, outputVisitor)) {
            compilationUnit.AcceptVisitor(outputVisitor, null);
            //}            
            return outputVisitor.Text;
        }*/
    }
}
