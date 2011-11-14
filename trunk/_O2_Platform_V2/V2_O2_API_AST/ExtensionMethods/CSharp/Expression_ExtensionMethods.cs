using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.NRefactory.Ast;

namespace V2.O2.API.AST.ExtensionMethods.CSharp
{
    public static class Expression_ExtensionMethods
    {
        public static ExpressionStatement expressionStatement(this Expression expression)
        {
            return new ExpressionStatement(expression);
        }
    }
}
