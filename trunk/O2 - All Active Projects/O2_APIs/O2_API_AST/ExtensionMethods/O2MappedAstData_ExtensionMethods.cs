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

        public static Dictionary<string, List<INode>> iNodes_By_Type(this O2MappedAstData astData)
        {
            return astData.iNodes_By_Type("");
        }

        public static Dictionary<string, List<INode>> iNodes_By_Type(this O2MappedAstData astData, string iNodeType_RegExFilter)
        {
            var iNodesByType = new Dictionary<string, List<INode>>();
            foreach (var iNode in astData.iNodes())
            {
                var typeName = iNode.typeName();
                if (iNodeType_RegExFilter.valid().isFalse() || typeName.regEx(iNodeType_RegExFilter))
                    iNodesByType.add(typeName, iNode);
            }
            return iNodesByType;
        }

        #endregion

        #region ISpecial

        public static Dictionary<string, List<ISpecial>> iSpecials_By_Type(this O2MappedAstData astData)
        {
            var iSpecialsByType = new Dictionary<string, List<ISpecial>>();
            foreach (var iSpecial in astData.iSpecials())
            {
                var typeName = iSpecial.typeName();
                iSpecialsByType.add(typeName, iSpecial);
            }

            return iSpecialsByType;
        }


        public static List<ISpecial> comments(this O2MappedAstData astData)
        {
            var iSpecialByType = astData.iSpecials_By_Type();
            if (iSpecialByType.hasKey("Comment"))
                return iSpecialByType["Comment"];
            return new List<ISpecial>();
        }

        public static Dictionary<string, List<ISpecial>> comments_IndexedByTextValue(this O2MappedAstData astData)
        {
            return astData.comments_IndexedByTextValue("");
        }

        public static Dictionary<string, List<ISpecial>> comments_IndexedByTextValue(this O2MappedAstData astData, string commentsFilter)
        {
            return astData.comments()
                          .indexOnProperty("CommentText", commentsFilter);
        }


        public static List<ISpecial> iSpecials(this O2MappedAstData astData)
        {
            var iSpecials = new List<ISpecial>();
            foreach (var item in astData.FileToSpecials)
                iSpecials.AddRange(item.Value);
            return iSpecials;

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

        public static List<ICSharpCode.NRefactory.Ast.Attribute> attributes(this O2MappedAstData o2MappedAstData)
        {
            return o2MappedAstData.iNodes<ICSharpCode.NRefactory.Ast.Attribute>();
        }

        public static List<INode> calledINodesReferences(this O2MappedAstData o2MappedAstData, IMethod iMethod)
        {
            var calledIMethodsRefs = new List<INode>();
            if (iMethod != null)
            {
                "-------------------".info();
                var methodDeclaration = o2MappedAstData.methodDeclaration(iMethod);

                // handle invocations via MemberReferenceExpression
                calledIMethodsRefs.add(methodDeclaration.iNodes<INode, MemberReferenceExpression>());
                calledIMethodsRefs.add(methodDeclaration.iNodes<INode, InvocationExpression>());
                calledIMethodsRefs.add(methodDeclaration.iNodes<INode, ObjectCreateExpression>());
            }
            return calledIMethodsRefs;
        }

        #endregion

        #region methodStreams

        public static Dictionary<IMethod, string> methodStreams(this O2MappedAstData astData, Action<string> statusMessage)
        {
            var methodStreams = new Dictionary<IMethod, string>();
            foreach (var iMethod in astData.iMethods())
                methodStreams.Add(iMethod, astData.createO2MethodStream(iMethod).csharpCode());
            return methodStreams;
        }

        public static string methodStream(this O2MappedAstData astData, IMethod iMethod)
        {
            return astData.createO2MethodStream(iMethod).csharpCode();
        }

        public static Dictionary<IMethod, string> showMethodStreams(this O2MappedAstData astData, Control control)
        {
            return astData.showMethodStreams(control, null);
        }

        public static Dictionary<IMethod, string> showMethodStreams(this O2MappedAstData astData, Control control, ProgressBar progressBar)
        {
            var iMethods = astData.iMethods();
            return astData.showMethodStreams(iMethods, control, progressBar);

        }

        public static Dictionary<IMethod, string> showMethodStreams(this O2MappedAstData astData, List<IMethod> iMethods, Control control, ProgressBar progressBar)
        {
            var treeView = control.add_MethodStreamViewer();
            return astData.showMethodStreams(iMethods, treeView, progressBar);
        }

        public static Dictionary<IMethod, string> showMethodStreams(this O2MappedAstData astData, List<IMethod> iMethods, TreeView treeView)
        {
            return astData.showMethodStreams(iMethods, treeView, null);
        }

        public static Dictionary<IMethod, string> showMethodStreams(this O2MappedAstData astData, List<IMethod> iMethods, TreeView treeView, ProgressBar progressBar)
        {
            treeView.Tag = iMethods;
            progressBar.maximum(iMethods.size());
            progressBar.value(0);

            var methodStreams = new Dictionary<IMethod, string>();
            foreach (var iMethod in iMethods)
            {
                var methodStreamCSharpCode = astData.createO2MethodStream(iMethod).csharpCode();
                methodStreams.Add(iMethod, methodStreamCSharpCode);
                var nodeText = "{0}          ({1} chars)".format(iMethod.name(), methodStreamCSharpCode.size());
                treeView.add_Node(nodeText, methodStreamCSharpCode);
                progressBar.increment(1);
            }
            treeView.selectFirst();
            return methodStreams;
        }

        public static TreeView add_MethodStreamViewer(this Control control)
        {
            control.clear();
            var codeViewer = control.add_SourceCodeViewer();
            var treeView = codeViewer.insert_Left<TreeView>(control.width() / 3);
            treeView.showSelection();
            treeView.afterSelect<string>((code) => codeViewer.set_Text(code));
            return treeView;
        }

        public static string methodStream_SharpCode(this O2MappedAstData o2MappedAstData, MethodDeclaration methodDeclaration)
        {
            var iMethod = o2MappedAstData.iMethod(methodDeclaration);
            var methodStream = o2MappedAstData.createO2MethodStream(iMethod);
            return methodStream.csharpCode();
        }

        public static Dictionary<IMethod, string> methodStreams(this O2MappedAstData astData)

        #endregion

        #region codeStreams

        public static TreeView add_CodeStreamViewer(this Control control)
        {
            control.clear();
            var codeViewer = control.add_SourceCodeViewer();
            var treeView = codeViewer.insert_Left<TreeView>(control.width() / 3);
            treeView.showSelection();
            treeView.afterSelect<O2CodeStreamNode>
                    ((streamNode) =>
                    {
                        "in afterSelect".info();
                        codeViewer.editor().setSelectionText(streamNode.INode.StartLocation, streamNode.INode.EndLocation);
                    });
            treeView.afterSelect<string>((code) => codeViewer.set_Text(code));
            treeView.beforeExpand<string>((code) => codeViewer.set_Text(code));
            treeView.beforeExpand<List<INode>>((iNodes) => codeViewer.editor().colorINodes(iNodes));
            treeView.afterSelect<List<INode>>((iNodes) => codeViewer.editor().colorINodes(iNodes));
            return treeView;
        }

        public static TreeView add_CodeStreams(this O2MappedAstData astData, TreeView treeView, List<IMethod> iMethods)
        {
            foreach (var iMethod in iMethods)
                astData.add_CodeStreams(treeView, iMethod);
            return treeView;
        }

        public static TreeView add_CodeStreams(this O2MappedAstData astData, TreeView treeView, IMethod iMethod)
        {
            var methodName = iMethod.name();
            var methodStream = astData.methodStream(iMethod);
            var codeStreams = methodStream.codeStreams();
            return treeView.add_CodeStreams(methodName, methodStream, codeStreams);
        }

        public static TreeView add_CodeStreams(this TreeView treeView, string rootFunctionName, string methodStreamCode, List<O2CodeStream> codeStreams)
        {
            return treeView.add_CodeStreams(rootFunctionName, methodStreamCode, codeStreams.codeStreams_UniquePaths());
        }
        public static TreeView add_CodeStreams(this TreeView treeView, string rootFunctionName, string methodStreamCode, List<List<O2CodeStreamNode>> codeStreamsPaths)
        {
            var count = 1;
            var treeNode = treeView.add_Node(rootFunctionName, methodStreamCode);
            //foreach(var codeStream in codeStreams.codeStreams_UniquePaths())
            //show.info(codeStreams[0]);
            foreach (var uniquePath in codeStreamsPaths)
            {
                //foreach(var uniquePath in codeStream.
                //var codeStreamNodes = codeStream.O2CodeStreamNodes.Values.toList();


                var iNodes = uniquePath.iNodes();// codeStream.O2CodeStreamNodes.Keys.toList();
                var nodeText = "Path #{0}       ({1} steps)".format(count++, uniquePath.size());
                treeNode.add_Node(nodeText, iNodes).add_Nodes(uniquePath);
            }
            return treeView;
        }

        public static List<IO2Finding> createAndShowCodeStreams(this O2MappedAstData astData, TreeView codeStreamViewer)
        {
            return astData.createAndShowCodeStreams(astData.iMethods(), codeStreamViewer);
        }

        public static List<IO2Finding> createAndShowCodeStreams(this O2MappedAstData astData, List<IMethod> iMethods, TreeView codeStreamViewer)
        {
            var o2Findings = new List<IO2Finding>();
            var processedMethods = new List<string>();		// hack deal with the problem that in some cases new IMethods mappings have to be added to astData (for more info search for "add extra mapping to IMethodToMethodDeclaration for:")
            foreach (var iMethod in iMethods)
            {
                if (processedMethods.Contains(iMethod.fullName()).isFalse())
                {
                    o2Findings.add(astData.createAndShowCodeStreams(iMethod, codeStreamViewer));
                    processedMethods.add(iMethod.fullName());
                }
            }
            return o2Findings;
        }

        public static List<IO2Finding> createAndShowCodeStreams(this O2MappedAstData astData, IMethod iMethod, TreeView codeStreamViewer)
        {
            var o2Findings = new List<IO2Finding>();
            var methodName = iMethod.name();
            var methodStream = astData.methodStream(iMethod);

            var codeStreams = methodStream.codeStreams_UniquePaths();

            codeStreamViewer.add_CodeStreams(methodName, methodStream, codeStreams);
            o2Findings.add_CodeStreams(methodStream, codeStreams);
            return o2Findings;
        }

        // add the abilty to define where the taint starts (method parameter, external caller, etc..)
        public static List<O2CodeStream> codeStreams(this string methodStreamFile)
        {
            if (methodStreamFile.fileExists().isFalse())
                methodStreamFile = methodStreamFile.saveWithExtension(".MethodStream.cs");

            var codeStreams = new List<O2CodeStream>();
            var AstData_MethodStream = new O2MappedAstData();
            AstData_MethodStream.loadFile(methodStreamFile);
            var iMethods = AstData_MethodStream.iMethods();
            if (iMethods.size() > 0)
            //if (AstData_MethodStream.iNodes().size() > 10)
            {
                var iMethod = iMethods[0];
                if (AstData_MethodStream.methodDeclaration(iMethod) != null)
                {
                    var parameters = AstData_MethodStream.methodDeclaration(iMethod).parameters();
                    var TaintRules = new O2CodeStreamTaintRules();
                    foreach (var parameter in parameters)
                    {
                        var codeStream = new O2CodeStream(AstData_MethodStream, TaintRules, methodStreamFile);
                        codeStream.createStream(parameter, null);
                        codeStreams.add(codeStream);
                    }
                }
            }
            return codeStreams;
        }

        public static List<List<O2CodeStreamNode>> codeStreams_UniquePaths(this List<O2CodeStream> codeStreams)
        {
            var uniqueCodeStreams = new List<List<O2CodeStreamNode>>();
            foreach (var codeStream in codeStreams)
                foreach (var uniquePath in codeStream.getUniqueStreamPaths(100))
                    uniqueCodeStreams.Add(uniquePath);
            return uniqueCodeStreams;
        }
        

        public static List<List<O2CodeStreamNode>> codeStreams_UniquePaths(this O2MappedAstData astData, IMethod iMethod)
    	{			
    		var methodStream =  astData.methodStream(iMethod);
    		return methodStream.codeStreams_UniquePaths();
    	}
    	
    	public static List<List<O2CodeStreamNode>> codeStreams_UniquePaths(this string methodStream)
    	{    		
    		var codeStreams = methodStream.codeStreams();
    		return codeStreams.codeStreams_UniquePaths();
    	}

        public static List<IO2Finding> add_CodeStreams(this List<IO2Finding> o2Findings, string methodStream, List<List<O2CodeStreamNode>> codeStreams)
        {
            o2Findings.AddRange(codeStreams.o2Findings(methodStream, "vulnName", "vulnType", "Source of Tainted Data"));
            return o2Findings;
        }

        public static List<INode> iNodes(this List<O2CodeStreamNode> codeStreamNodes)
    	{
    		var iNodes = from node in codeStreamNodes select node.INode;
    		return iNodes.toList();
    	}

        public static bool contains(this List<O2CodeStreamNode> codeStream, string stringToFind)
    	{
    		foreach(var streamNode in codeStream)
    		{    			
    			if (streamNode.str().contains(stringToFind))
    				return true;
    		}
    		return false;
    	}

        #endregion

        #region codeStreams & O2Findings

        public static List<IO2Finding> o2Findings(this List<O2CodeStream> codeStreams, string vulnName, string vulnType, string sourceNodeText)
        {
            var o2Findings = new List<IO2Finding>();
            foreach (var codeStream in codeStreams)                
                o2Findings.add(codeStream.o2Findings(vulnName, vulnType, sourceNodeText));
            return o2Findings;
        }

        public static List<IO2Finding> o2Findings(this O2CodeStream o2CodeStream, string vulnName, string vulnType, string sourceNodeText)
        {
            var uniqueStreamPaths = o2CodeStream.getUniqueStreamPaths(100);
            return uniqueStreamPaths.o2Findings(o2CodeStream.SourceFile, vulnName, vulnType, sourceNodeText);
        }

        public static List<IO2Finding> o2Findings(this List<List<O2CodeStreamNode>> codeStreamPaths, string methodStreamFile, string vulnName, string vulnType, string sourceNodeText)
        {
            if (methodStreamFile.fileExists().isFalse())
                methodStreamFile = methodStreamFile.saveWithExtension(".MethodStream.cs");
            var o2Findings = new List<IO2Finding>();
            foreach (var uniquePath in codeStreamPaths)
            {
                var o2Finding = new O2Finding();
                o2Finding.vulnName = vulnName;
                o2Finding.vulnType = vulnType;
                var o2Trace = o2Finding.addTrace(sourceNodeText);
                o2Trace.traceType = TraceType.Source;
                foreach (var streamNode in uniquePath)
                {
                    var newTrace = new O2Trace(streamNode.str());
                    o2Trace.childTraces.Add(newTrace);
                    o2Trace = newTrace;
                    o2Trace.file = methodStreamFile;
                    o2Trace.lineNumber = streamNode.INode.StartLocation.Line.uInt();
                }
                o2Trace.traceType = TraceType.Known_Sink;

                o2Finding.lineNumber = o2Trace.lineNumber;
                o2Finding.file = o2Trace.file;
                o2Findings.Add(o2Finding);
            }
            return o2Findings;
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

        /*public static string file(this O2MappedAstData o2MappedAstData, IMethod iMethod)
        {
            var methodDeclaration = o2MappedAstData.methodDeclaration(iMethod);
            return o2MappedAstData.file(methodDeclaration);
        }*/
        //replaced with the one below
        public static string file(this O2MappedAstData o2MappedAstData, IMethod iMethod)
        {
            var iNode = o2MappedAstData.methodDeclaration(iMethod) as INode;
            if (iNode == null)
                iNode = o2MappedAstData.constructorDeclaration(iMethod) as INode;
            return o2MappedAstData.file(iNode);
        }

        //not very optimized (see if it is an issue in big data sets
        public static string file(this O2MappedAstData astData, ISpecial iSpecialToMap)
        {
            foreach (var item in astData.FileToSpecials)
                foreach (var iSpecial in item.Value)
                    if (iSpecialToMap == iSpecial)
                        return item.Key;
            return "";
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
