using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using V2.O2.API.AST.CSharp;
using ICSharpCode.NRefactory.Ast;

namespace V2.O2.API.AST.Visitors
{
    public class FindIdentifierUsage : O2AstVisitor
    {
        public String IdentifierName { get; set; }
        public List<IdentifierExpression> Usages { get; set; }

        public FindIdentifierUsage(string identifierName)
        {
            IdentifierName = identifierName;
            Usages = new List<IdentifierExpression>();

            identifierExpressionVisit = (identifierExpression, data) =>
            {
                if (identifierExpression.Identifier == IdentifierName)
                    Usages.Add(identifierExpression);
                return data;
            };
        }
    }
}
