using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.NRefactory.Ast;
using ICSharpCode.NRefactory;

namespace O2.API.AST.ExtensionMethods.CSharp
{
    public static class UsingDeclaration_ExtensionMethods
    {        
        public static List<Using> usings(this IParser parser)
        {
            return parser.CompilationUnit.usings();
        }

        public static List<Using> usings(this CompilationUnit compilationUnit)
        {
            var usings = from child in compilationUnit.Children
                         where child is UsingDeclaration
                         from @using in ((UsingDeclaration)child).Usings
                         select @using;
            return usings.ToList();
        }

        public static List<string> values(this List<Using> usings)
        {
            var values = from @using in usings
                         select @using.Name;
            return values.ToList();
        }

        public static Using @using(this IParser parser, string name)
        {
            return parser.CompilationUnit.@using(name);
        }

        public static Using @using(this CompilationUnit compilationUnit, string name)
        {
            foreach (var @using in compilationUnit.usings())
                if (@using.Name == name)
                    return @using;
            return null;
        }

        public static IParser add_Using(this IParser parser, string @namespace)
        {
            parser.CompilationUnit.add_Using(@namespace);
            return parser;
        }

        public static CompilationUnit add_Using(this CompilationUnit compilationUnit, string @namespace)
        {
            var newUsing = new UsingDeclaration(@namespace);
            //compilationUnit.Children.add(newUsing);
            compilationUnit.Children.Insert(0, newUsing);
            return compilationUnit;
        }        
    }
}
