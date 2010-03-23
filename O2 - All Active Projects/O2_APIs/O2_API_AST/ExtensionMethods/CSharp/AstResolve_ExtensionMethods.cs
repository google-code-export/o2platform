using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.NRefactory.Ast;
using O2.API.AST.Graph;
using ICSharpCode.SharpDevelop.Dom;
using O2.Kernel.ExtensionMethods;
using O2.API.AST.CSharp;

namespace O2.API.AST.ExtensionMethods.CSharp
{
    public static class AstResolve_ExtensionMethods
    {
        public static void initialize(this O2AstResolver o2AstResolver, Expression expression)
        {
            o2AstResolver.resolver.Initialize(o2AstResolver.parseInformation, expression.StartLocation.Line, expression.StartLocation.Column);
        }

        public static object resolve(this O2AstResolver o2AstResolver, Expression expression)
        {
            o2AstResolver.initialize(expression);            
            //resolver.Initialize(parseInformation, memberReferenceExpression.StartLocation.Line, memberReferenceExpression.StartLocation.Column);
            //resolver.RunLookupTableVisitor(memberReferenceExpression);

            return o2AstResolver.resolver.ResolveInternal(expression, ExpressionContext.Default);            
        }
        
        public static object resolve(this O2AstResolver o2AstResolver, MemberReferenceExpression memberReferenceExpression)
        {
            o2AstResolver.initialize(memberReferenceExpression as Expression);
            return o2AstResolver.resolve((memberReferenceExpression as Expression));
           
        }
        public static string getNodeText(this ResolveResult resolveResult)
        {
            if (resolveResult is MemberResolveResult)
                return (resolveResult as MemberResolveResult).ResolvedMember.DotNetName;

            else if (resolveResult is MethodGroupResolveResult)
            {
                var  resolvedNames = new List<string>();
                foreach (var groupResult in (resolveResult as MethodGroupResolveResult).Methods)
                    foreach (var method in groupResult)
                        resolvedNames.Add(method.DotNetName);
                if (resolvedNames.Count == 1)
                    return resolvedNames[0];
                return resolvedNames.str(); ;
            }
            else
                if (resolveResult != null)
                    return resolveResult.typeName();
                else
                    return "[RESOLVED WAS NULL]";                         
        }


        public static List<INode> getAstPath(this INode node, int line, int column)
        {
            var findLocation = new FindLocationInAst(line, column);
            node.AcceptVisitor(findLocation, null);
            return findLocation.Matches;
        }
    }
}
