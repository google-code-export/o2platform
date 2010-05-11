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
using O2.API.AST.Visitors;
using O2.API.AST.ExtensionMethods.CSharp;

namespace O2.API.AST.ExtensionMethods
{
    public static class O2MappedAstData_ExtensionMethods
    {

        #region load

        public static O2MappedAstData loadFiles(this O2MappedAstData o2MappedAstData, List<string> filesToLoad)
        {
            return o2MappedAstData.loadFiles(filesToLoad, false);
        }
        
        public static O2MappedAstData loadFiles(this O2MappedAstData o2MappedAstData, List<string> filesToLoad, bool verbose)
        {
            //"Loading {0} files".format().debug();
            var totalFilesCount = filesToLoad.size();
            var filesLoaded = 0;
            foreach (var fileToLoad in filesToLoad)
            {
                if (verbose)
                    "loading file in O2MappedAstData object:{0}".format(fileToLoad).info();
                o2MappedAstData.loadFile(fileToLoad);
                if ((filesLoaded++) % 20 == 0)
                    " [{0}/{1}] Loading files into O2MappedAstData".format(filesLoaded, totalFilesCount).debug();
            }
            return o2MappedAstData;
        }
        
        #endregion

        #region iNode(s)

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

        public static List<INode> iNodes(this O2MappedAstData o2MappedAstData)
        {
            var iNodes = new List<INode>();
            foreach (var item in o2MappedAstData.FileToINodes)
                iNodes.add(item.Value.AllNodes);
            return iNodes;
        }

        public static List<T> iNodes<T>(this O2MappedAstData o2MappedAstData)
            where T : INode
        {
            var results = from iNode in o2MappedAstData.iNodes() where iNode is T select (T)iNode;
            return results.ToList();
        }

        public static List<T> iNodes<T>(this INode iNode)
        {
            var iNodesInT = new List<T>();
            var childINodes = iNode.iNodes();
            foreach (var childINode in childINodes)
                if (childINode is T)
                    iNodesInT.add((T)childINode);
            return iNodesInT;
        }

        public static List<INode> iNodes(this INode iNode)            
        {
            if (iNode == null)
                return null;
            var allINodes = new GetAllINodes();
            iNode.AcceptVisitor(allINodes, null);
            return allINodes.AllNodes;
        }

        public static List<T2> iNodes<T1, T2>(this T1 iNode)
            where T1 : INode
            where T2 : INode
        {
            if (iNode == null)
                return null;
            var results = from node in iNode.iNodes() where node is T2 select (T2)node;
            return results.ToList();
        }

        public static List<INode> iNodes(this O2MappedAstData o2MappedAstData, string file)
        {
            if (o2MappedAstData.FileToINodes.hasKey(file))
                return o2MappedAstData.FileToINodes[file].AllNodes;
            return null;
        }

        public static List<T> iNodes<T>(this O2MappedAstData o2MappedAstData, string file)
            where T : INode
        {
            if (o2MappedAstData.FileToINodes.hasKey(file))
                return o2MappedAstData.FileToINodes[file].allByType<T>();
            return null;
        }

        #endregion



        #region iMethod(s)

        public static IMethod iMethod(this O2MappedAstData o2MappedAstData, ConstructorDeclaration constructorDeclaration)
        {
            if (constructorDeclaration != null)
                if (o2MappedAstData.MapAstToNRefactory.ConstructorDeclarationToIMethod.ContainsKey(constructorDeclaration))
                    return o2MappedAstData.MapAstToNRefactory.ConstructorDeclarationToIMethod[constructorDeclaration];
            return null;
        }

        public static ConstructorDeclaration constructorDeclaration(this O2MappedAstData o2MappedAstData, IMethod iMethod)
        {
            if (iMethod != null)
                if (o2MappedAstData.MapAstToNRefactory.IMethodToConstructorDeclaration.hasKey(iMethod))
                    return o2MappedAstData.MapAstToNRefactory.IMethodToConstructorDeclaration[iMethod];
            return null;
        }

        public static IMethod iMethod(this O2MappedAstData o2MappedAstData, MethodDeclaration methodDeclaration)
        {

            if (methodDeclaration != null)
                if (o2MappedAstData.MapAstToNRefactory.MethodDeclarationToIMethod.ContainsKey(methodDeclaration))
                    return o2MappedAstData.MapAstToNRefactory.MethodDeclarationToIMethod[methodDeclaration];
            return null;
        }

        public static IMethod iMethod(this O2MappedAstData o2MappedAstData, MemberReferenceExpression memberReferenceExpression)
        {
            return o2MappedAstData.fromMemberReferenceExpressionGetIMethod(memberReferenceExpression);
        }

        public static IMethod iMethod(this O2MappedAstData o2MappedAstData, ObjectCreateExpression objectCreateExpression)
        {
            return o2MappedAstData.fromExpressionGetIMethod(objectCreateExpression as Expression);
        }

        public static IMethod iMethod(this O2MappedAstData o2MappedAstData, InvocationExpression invocationExpression)
        {
            return o2MappedAstData.fromExpressionGetIMethod(invocationExpression as Expression);
        }

        public static List<IMethod> iMethods(this O2MappedAstData o2MappedAstData, string file)
        {
            var methodDeclarations = o2MappedAstData.iNodes<MethodDeclaration>(file);
            return o2MappedAstData.iMethods(methodDeclarations);
        }

        public static List<IMethod> iMethods(this O2MappedAstData o2MappedAstData, List<MethodDeclaration> methodDeclarations)
        {
            var iMethods = new List<IMethod>();
            if (methodDeclarations != null)
                foreach (var methodDeclaration in methodDeclarations)
                {
                    var iMethod = o2MappedAstData.iMethod(methodDeclaration);
                    if (iMethod != null && iMethod is IMethod)
                        iMethods.add(iMethod);
                }
            return iMethods;
        }


        //merge this one with fromMemberReferenceExpressionGetIMethod (and add support for use of Fields and Properties)

        public static IMethod fromExpressionGetIMethod(this O2MappedAstData o2MappedAstData, Expression expression)
        {
            if (expression == null)
                return null;
            var compilationUnit = expression.compilationUnit();
            if (compilationUnit == null)
                return null;
            o2MappedAstData.O2AstResolver.setCurrentCompilationUnit(compilationUnit);
            var resolved = o2MappedAstData.O2AstResolver.resolve(expression);
            if (resolved is MethodGroupResolveResult)
            {
                var resolvedIMethods = new List<IMethod>();
                foreach (var groupResult in (resolved as MethodGroupResolveResult).Methods)
                    foreach (var method in groupResult)
                    {
                        resolvedIMethods.Add(method);
                    }
                if (resolvedIMethods.Count == 1)
                    return resolvedIMethods[0];
            }
            if (resolved != null)
                if (resolved is MemberResolveResult)
                {
                    var memberResolveResult = (MemberResolveResult)resolved;
                    if (memberResolveResult.ResolvedMember is IMethod)
                        return memberResolveResult.ResolvedMember as IMethod;
                    //else
                    //    "in fromExpressionGetIMethod, could not resolve Expression".error();
                }
            return null;
        }

        public static IMethod fromMemberReferenceExpressionGetIMethod(this O2MappedAstData o2MappedAstData, MemberReferenceExpression memberReferenceExpression)
        {
            return o2MappedAstData.fromExpressionGetIMethod(memberReferenceExpression as Expression);  
/*            var compilationUnit = memberReferenceExpression.compilationUnit();
            o2MappedAstData.O2AstResolver.setCurrentCompilationUnit(compilationUnit);            
            var resolved = o2MappedAstData.O2AstResolver.resolve(memberReferenceExpression);
            if (resolved != null)
            {                
                if (resolved is MethodGroupResolveResult)
                {
                    var resolvedIMethods = new List<IMethod>();                    
                    foreach (var groupResult in (resolved as MethodGroupResolveResult).Methods)
                        foreach (var method in groupResult)
                        {
                            resolvedIMethods.Add(method);                 
                        }
                    if (resolvedIMethods.Count == 1)
                        return resolvedIMethods[0];
                    else
                        "in fromMemberReferenceExpressionGetIMethod: could not find valid IMethod (resolvedIMethods.Count = {0}".format(resolvedIMethods.Count).error();
                }
                else
                    "in fromMemberReferenceExpressionGetIMethod: expected MethodGroupResolveResult and got: {0}".format(resolved.typeName()).error();
            }
            else
                "fromMemberReferenceExpressionGetIMethod: Resolved (for MemberReferenceExpression) WAS null".error();
            return null;                        
 * */
        }

        public static MethodDeclaration fromMemberReferenceExpressionGetMethodDeclaration(this O2MappedAstData o2MappedAstData, MemberReferenceExpression memberReferenceExpression)
        {
            var resolvedIMethod = o2MappedAstData.fromMemberReferenceExpressionGetIMethod(memberReferenceExpression);
            
            if (o2MappedAstData.MapAstToNRefactory.IMethodToMethodDeclaration.ContainsKey(resolvedIMethod))
            {
                var methodDeclaration = o2MappedAstData.MapAstToNRefactory.IMethodToMethodDeclaration[resolvedIMethod];
                //"found methodDeclaration: {0}".format(resolvedIMethod).debug(); ;
                return methodDeclaration;
                //"methodDeclaration : {0}".format(methodDeclaration).info();
                //show.info(methodDeclaration);
            }
            else
                "in fromMemberReferenceExpressionGetMethodDeclaration: no IMethod -> MethodDeclaration mapping".error();
            return null;
        }

        public static Dictionary<MethodDeclaration, IMethod> getMappedIMethods(this O2MappedAstData o2MappedAstData, List<MethodDeclaration> methodDeclarations)
        {
            var mappedIMethods = new Dictionary<MethodDeclaration, IMethod>();
            foreach (var methodDeclaration in methodDeclarations)
            {
                var iMethod = o2MappedAstData.MapAstToNRefactory.MethodDeclarationToIMethod[methodDeclaration];
                mappedIMethods.add(methodDeclaration, iMethod);
            }
            return mappedIMethods;
        }

        public static bool has_IMethod(this O2MappedAstData o2MappedAstData, IMethod iMethod)
        {
            // try to find by IMethod reference
            if (iMethod != null)
            {
                var directMapping = o2MappedAstData.MapAstToNRefactory.IMethodToMethodDeclaration.hasKey(iMethod);
                if (directMapping)
                    return true;
                // try to find by signature (this shouldn't be needed but there must be a bug which is making the resolved calledIMethod (current for InvocationExpression) to not be mapped)				
                foreach (var item in o2MappedAstData.MapAstToNRefactory.IMethodToMethodDeclaration)
                    if (item.Key.fullName() == iMethod.fullName())
                    {
                        "add extra mapping to IMethodToMethodDeclaration for: {0}".format(item.Key.fullName()).error();
                        o2MappedAstData.MapAstToNRefactory.IMethodToMethodDeclaration.add(iMethod, item.Value);
                        return true;
                    }
            }
            return false;
        }

        // REWRITE (lots of redundant code
        public static List<IMethod> calledIMethods(this O2MappedAstData o2MappedAstData, IMethod iMethod)
        {
            "in Called IMETHODS".debug();
            var calledIMethods = new List<IMethod>();
            if (iMethod != null)
            {
                "-------------------".info();
                var methodDeclaration = o2MappedAstData.methodDeclaration(iMethod);

                // handle invocations via MemberReferenceExpression
                var memberReferenceExpressions = methodDeclaration.iNodes<INode, MemberReferenceExpression>();
                foreach (var memberReferenceExpression in memberReferenceExpressions)
                    calledIMethods.add(o2MappedAstData.iMethod(memberReferenceExpression));

                // handle invocations via InvocationExpression
                var invocationExpressions = methodDeclaration.iNodes<INode, InvocationExpression>();
                foreach (var invocationExpression in invocationExpressions)
                    calledIMethods.add(o2MappedAstData.iMethod(invocationExpression));

                // handle contructors
                var objectCreateExpressions = methodDeclaration.iNodes<INode, ObjectCreateExpression>();
                "objectCreateExpressions: {0}".format(objectCreateExpressions.Count).info();
                foreach (var objectCreateExpression in objectCreateExpressions)
                    calledIMethods.add(o2MappedAstData.iMethod(objectCreateExpression));

                // handle 
                //var objIMethod = astData.fromObjectCreateExpressionGetIMethod(obj[0]);
            }
            return calledIMethods;
        }

        #endregion

        #region mist info

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

        public static string fullName(this O2MappedAstData o2MappedAstData, MethodDeclaration methodDeclaration)
        {
            var iMethod = o2MappedAstData.iMethod(methodDeclaration);
            return iMethod.fullName();
        }

        public static string fullName(this O2MappedAstData o2MappedAstData, MemberReferenceExpression memberReferenceExpression)
        {
            var iMethod = o2MappedAstData.iMethod(memberReferenceExpression);
            return iMethod.fullName();
        }
        
        public static CompilationUnit compilationUnit(this O2MappedAstData o2MappedAstData, string file)
        {
            if (o2MappedAstData.FileToCompilationUnit.hasKey(file))
                return o2MappedAstData.FileToCompilationUnit[file];
            return null;
        }

        public static MethodDeclaration methodDeclaration(this O2MappedAstData o2MappedAstData, IMethod iMethod)
        {
            if (iMethod != null)
                if (o2MappedAstData.MapAstToNRefactory.IMethodToMethodDeclaration.hasKey(iMethod))
                    return o2MappedAstData.MapAstToNRefactory.IMethodToMethodDeclaration[iMethod];
            return null;
        }

        public static List<String> files(this O2MappedAstData astData)
        {
            return astData.FileToCompilationUnit.Keys.toList();
        }

        public static string file(this O2MappedAstData o2MappedAstData, CompilationUnit compilationUnit)
        {
            foreach (var file in o2MappedAstData.FileToCompilationUnit)
                if (file.Value == compilationUnit)
                    return file.Key;
            return null;
        }

        public static string file(this O2MappedAstData o2MappedAstData, IMethod iMethod)
        {
            var methodDeclaration = o2MappedAstData.methodDeclaration(iMethod);
            return o2MappedAstData.file(methodDeclaration);
        }

        public static string file(this O2MappedAstData o2MappedAstData, INode iNode)
        {
            if (iNode != null)
            {
                var compilationUnit = iNode.compilationUnit();
                return o2MappedAstData.file(compilationUnit);
            }
            return null;
        }

        #endregion

        #region sourceCode

        public static string sourceCodeWrappedOnClass(this O2MappedAstData o2MappedAstData, IMethod iMethod)
        {
            var code = ("namespace {0}".line() +
                       "{{".line() +
                       "\tpartial class {1}".line() +
                       "\t{{".line() +
                       "\t\t{2}".line() +
                       "\t}}".line() +
                       @"}}".line() +
                //@"\}".line()
                       "")
                       .format(iMethod.DeclaringType.Namespace,
                               iMethod.DeclaringType.Name,
                               o2MappedAstData.sourceCode(iMethod));
            return code;

        }

        public static string sourceCode(this O2MappedAstData o2MappedAstData, IMethod iMethod)
        {
            var methodDeclaration = o2MappedAstData.methodDeclaration(iMethod);
            return o2MappedAstData.sourceCode(methodDeclaration);
        }

        public static string sourceCode(this O2MappedAstData o2MappedAstData, MethodDeclaration methodDeclaration)
        {
            var file = o2MappedAstData.file(methodDeclaration);
            if (file != null)
                return methodDeclaration.sourceCode(file);
            return "";
        }

        

        #endregion
    }
}
