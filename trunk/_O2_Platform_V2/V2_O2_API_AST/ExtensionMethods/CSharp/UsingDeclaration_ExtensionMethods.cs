using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.NRefactory.Ast;
using ICSharpCode.NRefactory;
using FluentSharp.O2.Kernel.ExtensionMethods;
using FluentSharp.O2.DotNetWrappers.ExtensionMethods;

namespace V2.O2.API.AST.ExtensionMethods.CSharp
{
    public static class UsingDeclaration_ExtensionMethods
    {
        #region create        

        /*public static UsingDeclaration add_Using(this CompilationUnit compilationUnit, string @namespace)
        {
            var newUsing = new UsingDeclaration(@namespace);
            //compilationUnit.Children.add(newUsing);
            //compilationUnit.Children.Insert(0, newUsing);
            compilationUnit.insert(newUsing);
            return newUsing;
        }*/

        public static CompilationUnit add_Using(this CompilationUnit compilationUnit, UsingDeclaration usingDeclaration)
        {
            compilationUnit.insert(usingDeclaration);
            return compilationUnit;
        }

        public static UsingDeclaration add_Using(this CompilationUnit compilationUnit, string @namespace)
        {
            @namespace = @namespace.remove(";").trim();
            var currentUsingDeclaration = compilationUnit.@using(@namespace);
            if (currentUsingDeclaration != null)
                // these UsingDeclaration & Using classes are a bit of a mess (need to normalize this later)
                return new UsingDeclaration(currentUsingDeclaration.wrapOnList());

            var newUsing = new UsingDeclaration(@namespace);
            compilationUnit.insert(newUsing);
            return newUsing;
        }

        /*public static CompilationUnit add_Using(this CompilationUnit compilationUnit, string usingNamespace)
        {
            var usingDeclaration = new UsingDeclaration(usingNamespace);
            compilationUnit.insert(usingDeclaration);
            return compilationUnit;
        }*/

        #endregion

        #region query

        public static bool hasUsing(this CompilationUnit compilationUnit, string @namespace)
        {
            foreach (var @using in compilationUnit.usings())
                if (@using.Name == @namespace)
                    return true;
            return false;
        }

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

        #endregion
    }
}
