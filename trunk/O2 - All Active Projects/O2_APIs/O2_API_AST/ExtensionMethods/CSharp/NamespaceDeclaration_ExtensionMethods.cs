using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.NRefactory.Ast;
using ICSharpCode.NRefactory;

namespace O2.API.AST.ExtensionMethods.CSharp
{
    public static class NamespaceDeclaration_ExtensionMethods
    {        
        public static List<NamespaceDeclaration> namespaces(this IParser parser)
        {
            return parser.CompilationUnit.namespaces();
        }

        public static List<NamespaceDeclaration> namespaces(this CompilationUnit compilationUnit)
        {
            var namespaces = from child in compilationUnit.Children
                             where child is NamespaceDeclaration
                             select (NamespaceDeclaration)child;
            return namespaces.ToList();
        }


        public static IParser add_Namespace(this IParser parser, string @namespace)
        {
            parser.CompilationUnit.add_Namespace(@namespace);
            return parser;
        }

        public static CompilationUnit add_Namespace(this CompilationUnit compilationUnit, string @namespace)
        {
            var newNamespace = new NamespaceDeclaration(@namespace);
            compilationUnit.Children.Add(newNamespace);
            return compilationUnit;
        }        
    }
}
