using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.TextEditor;
using O2.API.AST.CSharp;
using ICSharpCode.NRefactory.Ast;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using ICSharpCode.SharpDevelop.Dom;

namespace O2.API.AST.ExtensionMethods
{
    public static class O2MappedAstData_ExtensionMethods
    {
        public static INode iNode(this O2MappedAstData o2MappedAstData, string file, Caret caret)
        {
            if (o2MappedAstData.FileToINodes.hasKey(file))
            {
                var allINodes = o2MappedAstData.FileToINodes[file];
                var adjustedLine = caret.Line + 1;
                var adjustedColumn = caret.Column + 1;
                var iNode = allINodes.getINodeAt(adjustedLine, adjustedColumn);
                if (iNode != null)
                    return iNode;
                "Could not find iNOde for position {0}:{1} in file:{2}".format(adjustedLine, adjustedColumn, file).error();
            }
            "o2MappedAstData did not have INodes for file:{0}".format(file).error();
            return null;
        }

        public static MethodDeclaration fromMemberReferenceExpressionGetMethodDeclaration(this O2MappedAstData o2MappedAstData, MemberReferenceExpression memberReferenceExpression)
        {
            var compilationUnit = memberReferenceExpression.compilationUnit();
            o2MappedAstData.O2AstResolver.setCurrentCompilationUnit(compilationUnit);
            "Trying to resolved MemberReferenceExpression into MethodDeclaration:{0}".format(memberReferenceExpression).info();
            var resolved = o2MappedAstData.O2AstResolver.resolve(memberReferenceExpression);
            if (resolved != null)
            {
                //"resolved was:{0}".format(resolved.typeName()).info();
                if (resolved is MethodGroupResolveResult)
                {
                    var resolvedIMethods = new List<IMethod>();
                    //using ICSharpCode.SharpDevelop.Dom
                    foreach (var groupResult in (resolved as MethodGroupResolveResult).Methods)
                        foreach (var method in groupResult)
                        {
                            resolvedIMethods.Add(method);
                            //"method: {0}".format(method).debug();
                            //methodToFollow = method;
                            //break;
                        }
                    if (resolvedIMethods.Count == 1)
                    {
                        var resolvedIMethod = resolvedIMethods[0];
                        if (o2MappedAstData.MapAstToNRefactory.IMethodToMethodDeclaration.ContainsKey(resolvedIMethod))
                        {
                            var methodDeclaration = o2MappedAstData.MapAstToNRefactory.IMethodToMethodDeclaration[resolvedIMethod];
                            "found methodDeclaration: {0}".format(resolvedIMethod).debug(); ;
                            return methodDeclaration;
                            //"methodDeclaration : {0}".format(methodDeclaration).info();
                            //show.info(methodDeclaration);
                        }
                        else
                            "in fromMemberReferenceExpressionGetMethodDeclaration: no IMethod -> MethodDeclaration mapping".error();
                    }
                    else
                        "in fromMemberReferenceExpressionGetMethodDeclaration: could not find valid IMethod".error();

                }
            }
            else
                "infromMemberReferenceExpressionGetMethodDeclaration: Resolved (for MemberReferenceExpression) WAS null".error();
            return null;
        }

        public static string fromINodeGetFile(this O2MappedAstData o2MappedAstData, INode iNode)
        {
            if (iNode != null)
            {
                var compilationUnit = iNode.compilationUnit();
                return o2MappedAstData.fromCompilationUnitGetFile(compilationUnit);
            }
            return "";
            //var methodDeclaration = o2MappedData.fromMemberReferenceExpressionGetMethodDeclaration(iNode as MemberReferenceExpression);
        }

        public static string fromCompilationUnitGetFile(this O2MappedAstData o2MappedAstData, CompilationUnit compilationUnit)
        {
            if (compilationUnit != null)
            {
                foreach (var item in o2MappedAstData.FileToCompilationUnit)
                    if (compilationUnit == item.Value)
                        return item.Key;
                "in fromCompilationUnitGetFile: could not map compilation unit to file".error();
            }
            return "";
        }

    }
}
