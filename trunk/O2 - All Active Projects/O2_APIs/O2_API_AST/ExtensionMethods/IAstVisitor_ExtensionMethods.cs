using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast;
using O2.DotNetWrappers.ExtensionMethods;
using O2.API.AST.ExtensionMethods.CSharp;

namespace O2.API.AST.ExtensionMethods
{
    public static class IAstVisitor_ExtensionMethods
    {
        // IAST VIsitor extensionMethods

        public static IAstVisitor loadCode(this IAstVisitor astVisitor, string code)
        {
            return astVisitor.loadCode(code, null);
        }

        public static IAstVisitor loadCode(this IAstVisitor astVisitor, string code, object data)
        {
            code = (code.fileExists()) ? code.fileContents() : code;
            var parser = code.csharpAst();
            astVisitor.loadINode(parser.CompilationUnit, data);
            return astVisitor;
        }

        public static IAstVisitor loadINode<T>(this IAstVisitor astVisitor, T node) where T : INode
        {
            return astVisitor.loadINode(node, null);
        }

        public static IAstVisitor loadINode<T>(this IAstVisitor astVisitor, T node, object data) where T : INode
        {
            node.AcceptVisitor(astVisitor, data);
            return astVisitor;
        }
    }
}
