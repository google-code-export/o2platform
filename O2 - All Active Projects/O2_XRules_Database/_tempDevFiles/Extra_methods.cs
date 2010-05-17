// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Reflection;
using System.Text;
using O2.Interfaces.O2Core;
using O2.Interfaces.O2Findings;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.O2Findings;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.Network;
using O2.DotNetWrappers.DotNet;
using O2.Views.ASCX;
using O2.External.SharpDevelop.AST;
using O2.External.SharpDevelop.ExtensionMethods;
using O2.External.SharpDevelop.Ascx;
using O2.XRules.Database._Rules._Interfaces;
using O2.External.IE.ExtensionMethods;
using O2.External.IE.Wrapper;
using O2.API.AST.CSharp;
using O2.API.AST.ExtensionMethods;
using O2.API.AST.ExtensionMethods.CSharp;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast; 
using ICSharpCode.SharpDevelop.Dom;
using System.CodeDom;

//O2Ref:QuickGraph.dll

namespace O2.Script
{
    public static class ExtraMethods2
    {   
    	//treeView 
    	public static TreeView add_Nodes<T>(this TreeView treeView, Dictionary<string,List<T>> items)
    	{
    		return treeView.add_Nodes(items, -1, null);
    	}
    	
    	public static TreeView add_Nodes<T>(this TreeView treeView, Dictionary<string,List<T>> items, int maxNodeTextSize, ProgressBar progressBar)
    	{    		
    		return treeView.rootNode().add_Nodes(items, maxNodeTextSize, progressBar).treeView();
    	}
    	
    	public static TreeNode add_Nodes<T>(this TreeNode treeNode, Dictionary<string,List<T>> items)
    	{
    		return treeNode.add_Nodes(items, -1, null);
    	}    	
    	    	
    	
    	public static TreeNode add_Nodes<T>(this TreeNode treeNode, Dictionary<string,List<T>> items, int maxNodeTextSize, ProgressBar progressBar)
    	{
    		treeNode.treeView().invokeOnThread(
    			()=>{    					
    					progressBar.maximum(items.size());
    					foreach(var item in items)
    					{
    						var nodeText = (maxNodeTextSize > 1 && item.Key.size() > maxNodeTextSize) 
    											? item.Key.Substring(0,maxNodeTextSize).add("...") 
    											: item.Key;
    						treeNode.add_Node(nodeText,item.Value, item.Value.size() > 1);    						
    						progressBar.increment(1);
    					}    					
    				});
    		return treeNode;
   
    	}
    		
    	//colections ExtensionMethods
    	
    	public static Dictionary<string,List<T>> indexOnToString<T>(this List<T> items)
    	{
    		return items.indexOnToString("");
    	}
    	
    	public static Dictionary<string,List<T>> indexOnToString<T>(this List<T> items, string string_RegExFilter)
    	{
    		var result = new Dictionary<string,List<T>>();
    		foreach(var item in items)
    		{
    			if (item != null)
    			{
    				var str = item.str();
    				if (string_RegExFilter.valid().isFalse() || str.regEx(string_RegExFilter))  
    					result.add(str,item);
    			}
    		}
    		return result;
    	}
    	
    	public static Dictionary<string,List<T>> indexOnProperty<T>(this List<T> items, string propertyName, string string_RegExFilter)
    	{
    		var result = new Dictionary<string,List<T>>();
    		foreach(var item in items)
    		{
    			if (item != null)
    			{
    				var propertyValue = item.prop(propertyName);
    				
    				if (propertyValue != null)
    				{
    					var str = propertyValue.str();
    					if (string_RegExFilter.valid().isFalse() || str.regEx(string_RegExFilter))  
    						result.add(str,item);
    				}
    			}
    		}
    		return result;
    	}
    
    	// control extensionmethods
    	
    	public static T front<T>(this T control)    	
    		where T : Control
    	{
    		return control.bringToFront();
    	}
    	public static T bringToFront<T>(this T control)
    		where T : Control
    	{
    		control.invokeOnThread(()=> control.BringToFront());
    		return control;
    	}
    	
    	public static T back<T>(this T control)    	
    		where T : Control
    	{
    		return control.sendToBack();
    	}
    	    
    	public static T sendToBack<T>(this T control)
    		where T : Control
    	{
    		control.invokeOnThread(()=> control.SendToBack());
    		return control;
    	}
    	
    	public static bool @checked(this CheckBox checkBox)
    	{
			return checkBox.value();    		
    	}    	
    	
    	public static bool value(this CheckBox checkBox)    	    		
    	{
    		return (bool)checkBox.invokeOnThread(
    			()=>{
    					return checkBox.Checked;    					
    				});
    	}
    	
    	public static CheckBox @checked(this CheckBox checkBox, bool value)    	    		
    	{
    		return checkBox.value(value);
    	}
    	
    	public static CheckBox value(this CheckBox checkBox, bool value)    	    		
    	{
    		return (CheckBox)checkBox.invokeOnThread(
    			()=>{
    					checkBox.Checked = value;
    					return checkBox;
    				});
    	}
    	
    	public static CheckBox check(this CheckBox checkBox)
    	{
			return checkBox.value(true);    		
    	}
    	
    	public static CheckBox uncheck(this CheckBox checkBox)
    	{
			return checkBox.value(false);    		
    	}

		public static CheckBox tick(this CheckBox checkBox)
    	{
			return checkBox.value(true);    		
    	}
    	
    	public static CheckBox untick(this CheckBox checkBox)
    	{
			return checkBox.value(false);    		
    	}    	
    	
    	
    	public static CheckBox autoSize(this CheckBox checkBox)    	    		
    	{
    		return checkBox.autoSize(true);
    	}
    	    
    	public static CheckBox autoSize(this CheckBox checkBox, bool value)    		
    	{
    		checkBox.invokeOnThread(()=> checkBox.AutoSize = value);
    		return checkBox;
    	}
    	
    	// IMethod Extension methods
    	
    	
    	//public static List<INode> invocations(this Imethod
    //
    	// O2MappedAstData  in O2.External.SharpDevelop.ExtensionMethods Extension Methods
    	//replace original with this version
    	public static TreeView afterSelect_ShowInSourceCodeEditor2(this O2MappedAstData o2MappedAstData, TreeView treeView, ascx_SourceCodeEditor codeEditor)
        {
            return (TreeView)codeEditor.invokeOnThread(() =>
            {
                treeView.afterSelect<AstTreeView.ElementNode>((node) =>
                {                             	
                    var element = (INode)node.field("element");
                    var file = o2MappedAstData.file(element);
                    if (file!=null)
                    {
	                    codeEditor.open(file);                    
                    	codeEditor.setSelectionText(element.StartLocation, element.EndLocation);
                    }
                });
                
                // if it is a list select the first one
                treeView.afterSelect<List<INode>>((nodes) =>
                {
                	if (nodes.size() >0)
                	{
                		var node = nodes[0];
                		var file = o2MappedAstData.file(node);
                    	if (file!=null)
                    	{
	                	    codeEditor.open(file);                    
                    		codeEditor.setSelectionText(node.StartLocation, node.EndLocation);
                    	}
                    }	
                });
                
                treeView.afterSelect<INode>((node) =>
                {
                	var file = o2MappedAstData.file(node);
                    if (file!=null)
                    {
	                    codeEditor.open(file);                    
                    	codeEditor.setSelectionText(node.StartLocation, node.EndLocation);
                    }
                });
                
                treeView.afterSelect<ISpecial>((iSpecial) =>
                {
                	var file = o2MappedAstData.file(iSpecial);
                    if (file!=null)
                    {                    	
	                    codeEditor.open(file);                    
                    	codeEditor.setSelectionText(iSpecial.StartPosition, iSpecial.EndPosition);
                    }
                });
                
                // if it is a list select the first one
                treeView.afterSelect<List<ISpecial>>((iSpecials) =>
                {
                	if (iSpecials.size() >0)
                	{
                		var iSpecial = iSpecials[0];
                		var file = o2MappedAstData.file(iSpecial);
	                    if (file!=null)
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
                        if (file!=null)
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
                		if (file!=null)
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
                	"in IMethod".error();
                	var file = o2MappedAstData.file2(method);
                	if (file!=null)
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

		//O2MappedAstData Extension methods
    	
    	public static bool contains(this List<O2CodeStreamNode> codeStream, string stringToFind)
    	{
    		foreach(var streamNode in codeStream)
    		{    			
    			if (streamNode.str().contains(stringToFind))
    				return true;
    		}
    		return false;
    	}
    	
    	public static List<O2CodeStream> codeStreams(this string methodStreamFile)
    	{
    		var codeStreams = new List<O2CodeStream>();
    		var AstData_MethodStream = new O2MappedAstData(); 							
			AstData_MethodStream.loadFile(methodStreamFile);			
			if (AstData_MethodStream.iNodes().size() > 10)
			{	
				var iMethod = AstData_MethodStream.iMethods()[0];				
				if (AstData_MethodStream.methodDeclaration(iMethod) != null)
				{					
					var parameters = AstData_MethodStream.methodDeclaration(iMethod).parameters();
					var TaintRules = new O2CodeStreamTaintRules();  
					foreach(var parameter in parameters)
					{								
						var codeStream = new O2CodeStream(AstData_MethodStream,TaintRules, methodStreamFile);
						codeStream.createStream(parameter,null);
						codeStreams.add(codeStream);
						//return CodeStream.getUniqueStreamPaths(100);												
					 
		//				treeView.add_Nodes(uniqueStreamPaths); 
					}				
				}
			}
			return codeStreams;			
    	}
    	
    	public static List<List<O2CodeStreamNode>> codeStreams_UniquePaths(this List<O2CodeStream> codeStreams)
    	{
    		var uniqueCodeStreams = new List<List<O2CodeStreamNode>>();
    		foreach(var codeStream in codeStreams)
    			foreach(var uniquePath in codeStream.getUniqueStreamPaths(100))						
					uniqueCodeStreams.Add(uniquePath);
			return uniqueCodeStreams;
    	}
    	
    	public static List<IO2Finding> o2Findings(this List<O2CodeStream> codeStreams, string vulnName, string vulnType, string sourceNodeText)
    	{
    		var o2Findings = new List<IO2Finding>();
    		foreach(var codeStream in codeStreams)			
			//parameterNode.add_Node(codeStreams.size().str());
				o2Findings.add(codeStream.o2Findings(vulnName, vulnType, sourceNodeText));
			return o2Findings;
    	}
    	    	
    	
    	
    	public static List<IO2Finding> o2Findings(this O2CodeStream o2CodeStream, string vulnName, string vulnType, string sourceNodeText)
    	{
    		var uniqueStreamPaths = o2CodeStream.getUniqueStreamPaths(100);
    		var o2Findings = new List<IO2Finding>();    		
    		foreach(var uniquePath in uniqueStreamPaths)
    		{	    			    		
	    		var o2Finding = new O2Finding();			
	    		o2Finding.vulnName = vulnName;
				o2Finding.vulnType = vulnType;
				var o2Trace = o2Finding.addTrace(sourceNodeText);				
				o2Trace.traceType = TraceType.Source;
				foreach(var streamNode in uniquePath)
				{
					var newTrace = new O2Trace(streamNode.str());
					o2Trace.childTraces.Add(newTrace);
					o2Trace = newTrace;
					o2Trace.file = o2CodeStream.SourceFile;
				}
				o2Trace.traceType = TraceType.Known_Sink;
				
				o2Findings.Add(o2Finding);
			}
	    	return o2Findings;	    	
    	}
    	
    	public static string methodStream_SharpCode(this O2MappedAstData o2MappedAstData, MethodDeclaration methodDeclaration)
    	{
    		var iMethod = o2MappedAstData.iMethod(methodDeclaration); 
			var methodStream = o2MappedAstData.createO2MethodStream(iMethod);
			return methodStream.csharpCode(); 
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
                
                /*foreach (var memberReferenceExpression in memberReferenceExpressions)
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
*/
                // handle 
                //var objIMethod = astData.fromObjectCreateExpressionGetIMethod(obj[0]);
            }
            return calledIMethodsRefs;
        }
    	
    	// replace original with this one
    	
    	public static string file2(this O2MappedAstData o2MappedAstData, IMethod iMethod)
        {
            var iNode = o2MappedAstData.methodDeclaration(iMethod) as INode;
            if (iNode ==null)            	
            	iNode = o2MappedAstData.constructorDeclaration(iMethod) as INode;
            return o2MappedAstData.file(iNode);            
        }
    	
    	//not very optimized (see if it is an issue in big data sets
    	public static string file(this O2MappedAstData astData, ISpecial iSpecialToMap)
    	{
    		foreach(var item in astData.FileToSpecials)
    			foreach(var iSpecial in item.Value)
    				if (iSpecialToMap == iSpecial)
    					return item.Key;
    		return "";    		
    	}
    	
    	//public static List<INode
    	
    	public static Dictionary<string,List<INode>> iNodes_By_Type(this O2MappedAstData astData)
    	{
    		return astData.iNodes_By_Type("");
    	}
    	
    	public static Dictionary<string,List<INode>> iNodes_By_Type(this O2MappedAstData astData, string iNodeType_RegExFilter)
    	{    		
    		var iNodesByType =  new Dictionary<string,List<INode>>();
    		foreach(var iNode in astData.iNodes())
    		{
    			var typeName = iNode.typeName();
    			if (iNodeType_RegExFilter.valid().isFalse() || typeName.regEx(iNodeType_RegExFilter))				
					iNodesByType.add(typeName, iNode);
    				//if (iNodesByType.ContainsKey(typeName).isFalse())
    				//	iNodesByType.Add(typeName, new List<INode>());
    				//iNodesByType[typeName].Add(iNode);
    				
    		}	
    		return iNodesByType;
    	}
    	
    	public static Dictionary<string,List<ISpecial>> iSpecials_By_Type(this O2MappedAstData astData)
    	{    		
    		var iSpecialsByType =  new Dictionary<string,List<ISpecial>>();
    		foreach(var iSpecial in astData.iSpecials())
    		{
    			var typeName = iSpecial.typeName();
    			iSpecialsByType.add(typeName, iSpecial);
    			/*if (iSpecialsByType.ContainsKey(typeName).isFalse())
    				iSpecialsByType.Add(typeName, new List<ISpecial>());
    			iSpecialsByType[typeName].Add(iNode);*/
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
    	
    	public static Dictionary<string,List<ISpecial>> comments_IndexedByTextValue(this O2MappedAstData astData)
    	{
			return astData.comments_IndexedByTextValue("");
    	}
    	
    	public static Dictionary<string,List<ISpecial>> comments_IndexedByTextValue(this O2MappedAstData astData, string commentsFilter)
    	{
    		return astData.comments()
    					  .indexOnProperty("CommentText",commentsFilter);    		    	
    	}
    
    
    	public static List<ISpecial> iSpecials(this O2MappedAstData astData)
    	{
    		var iSpecials = new List<ISpecial>();
    		foreach(var item in astData.FileToSpecials)
    			iSpecials.AddRange(item.Value);
    		return iSpecials;
    		
    	}
    
    	// Control extension methods
    	
    	// TextBox Extension methods
    	
    	public static TextBox onTextChange_AlertOnRegExFail(this TextBox textBox)
    	{
    		textBox.onTextChange((text)=>{											
				   							textBox.backColor(text.regExOk() 
								   									? Color.White
								   									: Color.Red
				   											  );
				   						 });
			return textBox;
		}
    	
    
    	// TreeView Extension methods
    	
    	public static TreeView showToolTip(this TreeView treeView)
    	{
    		if (treeView != null)
    			treeView.invokeOnThread(()=> treeView.ShowNodeToolTips = true);
    		return treeView;
    	}
    	
    	public static TreeNode toolTip(this TreeNode treeNode, string toolTipText)
    	{
    		if (treeNode != null)
    			treeNode.treeView().invokeOnThread(()=> treeNode.ToolTipText = toolTipText);
    		return treeNode;
    	}
    	
    	public static TreeNode foreColor(this TreeNode treeNode, Color color)
    	{
    		if (treeNode != null)
    			treeNode.treeView().invokeOnThread(()=> treeNode.ForeColor = color);
    		return treeNode;
    	}
    	
    	public static TreeNode backColor(this TreeNode treeNode, Color color)
    	{
    		if (treeNode != null)
    			treeNode.treeView().invokeOnThread(()=> treeNode.BackColor = color);
    		return treeNode;
    	}
    	
        public static TreeView beforeExpand<T>(this TreeView treeView, Action<TreeNode, T> callback)        
        {	
        	treeView.beforeExpand<T>(
        		(tagData)=>{
        						var selectedNode = treeView.selected();
        						selectedNode.clear();
        						callback(selectedNode, tagData);
        				   });
        	return treeView;
        }
        
        public static TreeView beforeExpand_PopulateWithList<T>(this TreeView treeView)        
        {
        	treeView.beforeExpand<List<T>>(
        		(treeNode, items)=> treeNode.add_Nodes(items));
        	return treeView;
        }
        
        
        //O2Finding rules
        
        public static List<IO2Finding> applyRule(this List<IO2Finding> o2Findings, string sinkToFind, string vulnName)
        {
        	var results = new List<IO2Finding>();
        	foreach(var iO2Finding in o2Findings)
        	{
        		var o2Finding = (O2Finding)iO2Finding;
        		if ((o2Finding).Sink.contains(sinkToFind))
        		{
        			o2Finding.vulnName = vulnName;
        			results.add(o2Finding);
        		}
        	}	
        	return results;
        }
        
    }
    
    
    
 
    
   
    
    
    
   
}
		