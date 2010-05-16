using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.SharpDevelop.Dom;
using ICSharpCode.NRefactory.Ast;
using O2.API.AST.CSharp;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.External.SharpDevelop.AST;
using O2.External.SharpDevelop.Ascx;
using O2.API.AST.ExtensionMethods;
using System.CodeDom;

namespace O2.External.SharpDevelop.ExtensionMethods
{
    public static class O2MappedAstData_ExtensionMethods
    {
        public static TreeNode show_NRefactoryDom<T>(this TreeNode treeNode, List<T> list)
            where T : IFreezable
        {
            treeNode.TreeView.configureTreeViewForCodeDomViewAndNRefactoryDom();
            treeNode.add_Nodes_WithPropertiesAsChildNodes<T>(list);
            return treeNode;
        }

        public static TreeView show_NRefactoryDom<T>(this TreeView treeView, List<T> list)
            where T : IFreezable
        {
            treeView.configureTreeViewForCodeDomViewAndNRefactoryDom();
            treeView.add_Nodes_WithPropertiesAsChildNodes<T>(list);
            return treeView;
        }

        public static TreeView show_NRefactoryDom<T>(this TreeView treeView, T codeObject)
            where T : IFreezable
        {
            if (codeObject != null)
            {
                treeView.configureTreeViewForCodeDomViewAndNRefactoryDom();
                treeView.add_Nodes_WithPropertiesAsChildNodes<T>(codeObject);
            }
            return treeView;
        }

        public static MethodDeclaration getMethodDeclaration(this O2MappedAstData o2MappedAstData, IMethod iMethodToMap)
        {
            if (o2MappedAstData.MapAstToNRefactory.IMethodToMethodDeclaration.hasKey(iMethodToMap))
                return o2MappedAstData.MapAstToNRefactory.IMethodToMethodDeclaration[iMethodToMap];

            //foreach(var iMethod in o2MappedAstData.iMethods())
            //	if (o2MappedAstData.MapAstToNRefactory.IMethodToMethodDeclaration.hasKey(iMethod).isFalse())
            //		" key not found".error();
            foreach (var iMethod in o2MappedAstData.MapAstToNRefactory.IMethodToMethodDeclaration.Keys)
                if ((iMethod as DefaultMethod).DocumentationTag == (iMethodToMap as DefaultMethod).DocumentationTag)
                {
                    "DocumentationTag: {0}".format((iMethod as DefaultMethod).DocumentationTag).debug();
                    return o2MappedAstData.MapAstToNRefactory.IMethodToMethodDeclaration[iMethod];
                }
            return null;

        }

        public static ConstructorDeclaration getConstructorDeclaration(this O2MappedAstData o2MappedAstData, IMethod iMethodToMap)
        {
            if (o2MappedAstData.MapAstToNRefactory.IMethodToMethodDeclaration.hasKey(iMethodToMap))
            {
                "direct match".error();
                return o2MappedAstData.MapAstToNRefactory.IMethodToConstructorDeclaration[iMethodToMap];
            }

            //foreach(var iMethod in o2MappedAstData.iMethods())
            //	if (o2MappedAstData.MapAstToNRefactory.IMethodToMethodDeclaration.hasKey(iMethod).isFalse())
            //		" key not found".error();
            foreach (var iMethod in o2MappedAstData.MapAstToNRefactory.IMethodToConstructorDeclaration.Keys)
                if ((iMethod as DefaultMethod).DocumentationTag == (iMethodToMap as DefaultMethod).DocumentationTag)
                {
                    "DocumentationTag: {0}".format((iMethod as DefaultMethod).DocumentationTag).debug();
                    return o2MappedAstData.MapAstToNRefactory.IMethodToConstructorDeclaration[iMethod];
                }
            return null;

        }

        public static O2MappedAstData showInTreeView(this O2MappedAstData o2MappedAstData,
                                               System.Windows.Forms.TreeView treeView,
                                               ascx_SourceCodeEditor codeEditor)
        {
            o2MappedAstData.afterSelect_ShowInSourceCodeEditor(treeView, codeEditor);
            return o2MappedAstData.showInTreeView(treeView);
        }


        public static O2MappedAstData showInTreeView(this O2MappedAstData o2MappedAstData, System.Windows.Forms.TreeView treeView)
        {
            treeView.clear();
            // Show AST objects
            var astNode = treeView.add_Node("ast");
            astNode.show_Asts(o2MappedAstData.compilationUnits());
            astNode.show_Asts(o2MappedAstData.typeDeclarations());
            astNode.show_Asts(o2MappedAstData.methodDeclarations());
            // add GetAllNodes to mapAstWithDom object : var name AllAstNodes
            //			var allNodes = new GetAllINodes().loadCode(code); 
            //			treeView.add_Node("All Nodes (by type)").add_Nodes(allNodes.NodesByType);
            // Show CodeDom objects
            var domNode = treeView.add_Node("dom");
            domNode.add_Node("CodeNamespaces").show_CodeDom(o2MappedAstData.codeNamespaces());
            domNode.add_Node("CodeTypeDeclarations").show_CodeDom(o2MappedAstData.codeTypeDeclarations());
            domNode.add_Node("CodeMemberMethods").show_CodeDom(o2MappedAstData.codeMemberMethods());

            treeView.add_Node("NRefactory").show_NRefactoryDom(o2MappedAstData.iCompilationUnits())
                                           .show_NRefactoryDom(o2MappedAstData.iClasses())
                                           .show_NRefactoryDom(o2MappedAstData.iMethods());


            return o2MappedAstData;
        }


        public static TreeView afterSelect_ShowInSourceCodeEditor(this O2MappedAstData o2MappedAstData, TreeView treeView, ascx_SourceCodeEditor codeEditor)
        {
            return (TreeView)codeEditor.invokeOnThread(() =>
            {
                treeView.afterSelect<AstTreeView.ElementNode>((node) =>
                {
                    var element = (INode)node.field("element");                    
                    codeEditor.setSelectionText(element.StartLocation, element.EndLocation);
                });
                treeView.afterSelect<INode>((node) =>
                {
                    codeEditor.setSelectionText(node.StartLocation, node.EndLocation);
                });

                treeView.afterSelect<CodeTypeDeclaration>((codeTypeDeclaration) =>
                {
                    if (o2MappedAstData.MapAstToDom.TypesDomToAst.hasKey(codeTypeDeclaration))
                    {
                        var typeDeclaration = o2MappedAstData.MapAstToDom.TypesDomToAst[codeTypeDeclaration];
                        codeEditor.setSelectionText(typeDeclaration.StartLocation, typeDeclaration.EndLocation);
                    }
                    else
                        "in afterSelect<CodeTypeDeclaration>, key was node found for :{0}".format(codeTypeDeclaration.str());
                });

                treeView.afterSelect<CodeMemberMethod>((codeMemberMethod) =>
                {
                    if (o2MappedAstData.MapAstToDom.MethodsDomToAst.hasKey(codeMemberMethod))
                    {
                        var methodDeclaration = o2MappedAstData.MapAstToDom.MethodsDomToAst[codeMemberMethod];
                        codeEditor.setSelectionText(methodDeclaration.StartLocation, methodDeclaration.EndLocation);
                    }
                    else
                        "in afterSelect<CodeMemberMethod> no key for {0}".format(codeMemberMethod.str()).error();
                });

                treeView.afterSelect<IMethod>((method) =>
                {
                    if (o2MappedAstData.MapAstToNRefactory.IMethodToMethodDeclaration.hasKey(method))
                    {
                        var methodDeclaration = o2MappedAstData.MapAstToNRefactory.IMethodToMethodDeclaration[method];
                        codeEditor.setSelectionText(methodDeclaration.StartLocation, methodDeclaration.EndLocation);
                    }
                    else
                        "in afterSelect<CodeMemberMethod> no key for {0}".format(method.str()).error();
                });
                return treeView;
            });
        }

        

    }
}
