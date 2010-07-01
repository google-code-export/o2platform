﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.SharpDevelop.Dom;
using ICSharpCode.NRefactory.Ast;
using O2.API.AST.CSharp;
using O2.API.AST.ExtensionMethods;
using O2.API.AST.ExtensionMethods.CSharp;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.External.SharpDevelop.ExtensionMethods;
using O2.External.SharpDevelop.AST;
using O2.External.SharpDevelop.Ascx;

using System.CodeDom;
using ICSharpCode.NRefactory;
using System.Drawing;

//O2File:Ast_Engine_ExtensionMethods.cs

namespace O2.XRules.Database.Languages_and_Frameworks.DotNet
{
    public static class SharpDevelop_O2MappedAstData_ExtensionMethods
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

        #region afterSelect events

        public static TreeView afterSelect_ShowInSourceCodeEditor(this O2MappedAstData o2MappedAstData, TreeView treeView, ascx_SourceCodeEditor codeEditor)
        {
            return (TreeView)codeEditor.invokeOnThread(() =>
            {
                treeView.afterSelect<AstTreeView.ElementNode>((node) =>
                {
                    var element = (INode)node.field("element");
                    var file = o2MappedAstData.file(element);
                    if (file != null)
                    {
                        codeEditor.open(file);
                        codeEditor.setSelectionText(element.StartLocation, element.EndLocation);
                    }
                });

                // if it is a list select the first one
                treeView.afterSelect<List<INode>>((nodes) =>
                {
                    if (nodes.size() > 0)
                    {
                        var node = nodes[0];
                        var file = o2MappedAstData.file(node);
                        if (file != null)
                        {
                            codeEditor.open(file);
                            codeEditor.setSelectionText(node.StartLocation, node.EndLocation);
                        }
                    }
                });

                treeView.afterSelect<INode>((node) =>
                {
                    var file = o2MappedAstData.file(node);
                    if (file != null)
                    {
                        codeEditor.open(file);
                        codeEditor.setSelectionText(node.StartLocation, node.EndLocation);
                    }
                });

                treeView.afterSelect<ISpecial>((iSpecial) =>
                {
                    var file = o2MappedAstData.file(iSpecial);
                    if (file != null)
                    {
                        codeEditor.open(file);
                        codeEditor.setSelectionText(iSpecial.StartPosition, iSpecial.EndPosition);
                    }
                });

                // if it is a list select the first one
                treeView.afterSelect<List<ISpecial>>((iSpecials) =>
                {
                    if (iSpecials.size() > 0)
                    {
                        var iSpecial = iSpecials[0];
                        var file = o2MappedAstData.file(iSpecial);
                        if (file != null)
                        {
                            codeEditor.open(file);
                            codeEditor.setSelectionText(iSpecial.StartPosition, iSpecial.EndPosition);
                        }
                    }
                });

                treeView.afterSelect<CodeTypeDeclaration>((codeTypeDeclaration) =>
                {
                    if (o2MappedAstData.MapAstToDom.TypesDomToAst.hasKey(codeTypeDeclaration))
                    {
                        var typeDeclaration = o2MappedAstData.MapAstToDom.TypesDomToAst[codeTypeDeclaration];
                        var file = o2MappedAstData.file(typeDeclaration);
                        if (file != null)
                        {
                            codeEditor.open(file);
                            codeEditor.setSelectionText(typeDeclaration.StartLocation, typeDeclaration.EndLocation);
                        }
                    }
                    else
                        "in afterSelect<CodeTypeDeclaration>, key was node found for :{0}".format(codeTypeDeclaration.str());
                });

                treeView.afterSelect<CodeMemberMethod>((codeMemberMethod) =>
                {
                    if (o2MappedAstData.MapAstToDom.MethodsDomToAst.hasKey(codeMemberMethod))
                    {
                        var methodDeclaration = o2MappedAstData.MapAstToDom.MethodsDomToAst[codeMemberMethod];
                        var file = o2MappedAstData.file(methodDeclaration);
                        if (file != null)
                        {
                            codeEditor.open(file);
                            codeEditor.setSelectionText(methodDeclaration.StartLocation, methodDeclaration.EndLocation);
                        }
                    }
                    else
                        "in afterSelect<CodeMemberMethod> no key for {0}".format(codeMemberMethod.str()).error();
                });

                treeView.afterSelect<IMethod>((method) =>
                {
                    var file = o2MappedAstData.file(method);
                    if (file != null)
                    {
                        codeEditor.open(file);
                        if (o2MappedAstData.MapAstToNRefactory.IMethodToMethodDeclaration.hasKey(method))
                        {
                            var methodDeclaration = o2MappedAstData.MapAstToNRefactory.IMethodToMethodDeclaration[method];
                            codeEditor.setSelectionText(methodDeclaration.StartLocation, methodDeclaration.EndLocation);
                        }
                        else
                            if (o2MappedAstData.MapAstToNRefactory.IMethodToConstructorDeclaration.hasKey(method))
                            {
                                var constructorDeclaration = o2MappedAstData.MapAstToNRefactory.IMethodToConstructorDeclaration[method];
                                codeEditor.setSelectionText(constructorDeclaration.StartLocation, constructorDeclaration.EndLocation);
                            }
                        "in afterSelect<CodeMemberMethod> no key for {0}".format(method.str()).error();
                    };
                });
                return treeView;
            });
        }

        public static TreeView afterSelect_ShowMethodSignatureInSourceCode(this TreeView treeView, O2MappedAstData astData, ascx_SourceCodeViewer codeViewer)
        {
            treeView.afterSelect(
                (treeNode) =>
                {                    
                    var text = O2.DotNetWrappers.ExtensionMethods.Ascx_ExtensionMethods.get_Text(treeNode);
                    var methodDeclaration = astData.methodDeclaration_withSignature(treeNode.get_Text());
                    if (methodDeclaration != null)
                    {
                        treeNode.color(Color.DarkGreen);
                        var file = astData.file(methodDeclaration);
                        codeViewer.open(file);
                        codeViewer.editor().clearBookmarksAndMarkers();
                        codeViewer.editor().setSelectionText(methodDeclaration.StartLocation, methodDeclaration.EndLocation);
                    }
                    else
                        treeNode.color(Color.Red);
                });
            return treeView;
        }

        #endregion

        #region methodStream and codeStream related

        public static TreeView add_MethodStreamViewer(this Control control)
        {
            control.clear();
            var codeViewer = control.add_SourceCodeViewer();
            var treeView = codeViewer.insert_Left<TreeView>(control.width() / 3);
            treeView.showSelection();
            treeView.afterSelect<string>((code) => codeViewer.set_Text(code));
            return treeView;
        }

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
        
        #endregion

    }
}
