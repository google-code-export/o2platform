// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

using O2.Interfaces.O2Findings;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.API.AST.ExtensionMethods.CSharp;
using O2.API.AST.CSharp;

using ICSharpCode.NRefactory.Ast;
using ICSharpCode.SharpDevelop.Dom;
using O2.DotNetWrappers.O2Findings;


namespace O2.API.AST.ExtensionMethods
{
    public static class O2CodeStream_ExtensionMethods
    {    
    	public static O2CodeStreamNode map_IMethod(this O2CodeStream o2CodeStream, IMethod iMethod)
    	{
    		var methodDeclaration = o2CodeStream.O2MappedAstData.methodDeclaration(iMethod); 
    		return o2CodeStream.map_MethodDeclaration(methodDeclaration);
    	}
    
    	public static O2CodeStreamNode map_MethodDeclaration(this O2CodeStream o2CodeStream, MethodDeclaration methodDeclaration)
    	{
    		var methodNode = o2CodeStream.add_INode(methodDeclaration, null);
    		foreach(var parameter in methodDeclaration.parameters()) 
				o2CodeStream.createStream(parameter,methodNode);  
			return methodNode;
    	}
    
    	public static INode createStream(this O2CodeStream o2CodeStream, INode iNode, O2CodeStreamNode parentStreamNode)
    	{
    		if (o2CodeStream.has_INode(iNode).isFalse())    			
	    		switch(iNode.typeName())
	    		{
	    			case "ParameterDeclarationExpression":
	    				var parametedNode = o2CodeStream.add_INode(iNode, parentStreamNode);
	    				o2CodeStream.expandTaint(iNode as ParameterDeclarationExpression, parametedNode);	    				
	    				break;    			
	    			case "IdentifierExpression":	    			
	    				var identifierNode = o2CodeStream.add_INode(iNode, parentStreamNode);
	    				o2CodeStream.expandTaint(iNode as Expression,null, identifierNode);
	    				break;
	    			case "VariableDeclaration":
	    				var variableNode = o2CodeStream.add_INode(iNode , parentStreamNode);
	    				o2CodeStream.expandTaint(iNode as VariableDeclaration, variableNode);	    				
	    				break;
	    			case "ReturnStatement":       				    				
	    				o2CodeStream.expandTaint(iNode as ReturnStatement,parentStreamNode);  
	    				break;    			
	    			default:
	    				"Unsupported stream Node type:{0}".error(iNode.typeName());
	    				break;
	    		}
    		return iNode;
    	}
    	
    	public static List<O2CodeStreamNode> streamNodes(this O2CodeStream o2CodeStream)
    	{
    		return o2CodeStream.O2CodeStreamNodes.Values.ToList();
    	}
    	
    	public static List<INode> iNodes(this O2CodeStream o2CodeStream)
    	{
    		return o2CodeStream.O2CodeStreamNodes.Keys.ToList();
    	}
    	
    	
    	public static bool has_INode(this O2CodeStream o2CodeStream, INode iNode)
    	{
    		return o2CodeStream.O2CodeStreamNodes.ContainsKey(iNode);
    	}
    	
    	public static O2CodeStreamNode add_INode(this O2CodeStream o2CodeStream, INode iNode, O2CodeStreamNode parentStreamNode)
    	{    		
    		if (o2CodeStream.has_INode(iNode).isFalse())
    		{
    			"adding INode: {0}".info(iNode.typeName());    			
    			o2CodeStream.INodeStack.Push(iNode);
    			var text = o2CodeStream.getTextForINode(iNode);;
    			    			
    			var streamNode = new O2CodeStreamNode(text,iNode);
				o2CodeStream.O2CodeStreamNodes.add(iNode, streamNode);
				if (parentStreamNode == null)
					o2CodeStream.StreamNode_First.Add(streamNode);
				if (parentStreamNode!= null)
					parentStreamNode.ChildNodes.Add(streamNode);				
				return streamNode;
    		}   
    		var existingStreamNode = o2CodeStream.O2CodeStreamNodes[iNode];
			if (parentStreamNode!= null)
				parentStreamNode.ChildNodes.Add(existingStreamNode);					
    		return existingStreamNode;
    	}
    	
    	public static string getTextForINode(this O2CodeStream o2CodeStream, INode iNode)
    	{
	    	var typeName = iNode.typeName();
	    	var text = typeName;
	    	switch(typeName)
			{
				case "ParameterDeclarationExpression":    					
					text = "parameter: {0}".format((iNode as ParameterDeclarationExpression).name());
					break;
				case "MethodDeclaration":   
					var iMethod = o2CodeStream.O2MappedAstData.iMethod(iNode as MethodDeclaration);
					text = "method: {0}".format(iMethod.fullName()); 
					// o2CodeStream.O2MappedAstData.iMethod(iNode as MethodDeclaration).name());
					break;
				case "InvocationExpression":    
					var invocationMethod = o2CodeStream.O2MappedAstData.iMethod(iNode as InvocationExpression);
					text = "invocation: {0}".format(invocationMethod.fullName());
					break;
				case "IdentifierExpression":
					text = "identifier: {0}".format((iNode as IdentifierExpression).Identifier);
					break;
				case "VariableDeclaration":
					text = "variable: {0}".format((iNode as VariableDeclaration).Name);
					break;
				case "ConstructorDeclaration":
					var ctorIMethod = o2CodeStream.O2MappedAstData.iMethod(iNode as ConstructorDeclaration);
					text = "constructor: {0}".format(ctorIMethod.fullName());// (iNode as ConstructorDeclaration).Name);
					break;
 
			}
			return text;
		}
    	 
    	public static O2CodeStream expandTaint(this O2CodeStream o2CodeStream, ReturnStatement returnStatement, O2CodeStreamNode parentStreamNode)
    	{
    		"Tainting the return data".error();
    		var lastMethodDeclaration = o2CodeStream.popStack<MethodDeclaration>();
    		if (lastMethodDeclaration != default(MethodDeclaration))
    		{    			    			
    			//"lastMethodDeclaration: {0}".debug(lastMethodDeclaration);    		
    			
    			// find who calls this and create stream from it
    			var iNode  = o2CodeStream.popStack();    			
    			var iNodeToTaint = o2CodeStream.getParentINodeThatIsNotAdded(iNode);
    			if (iNodeToTaint != null)
    			{    			
    				"FOUND iNodeToTaint:{0}".info(iNodeToTaint);
    				"FOUND iNodeToTaint.Parent:{0}".info(iNodeToTaint.Parent);
    				o2CodeStream.createStream(iNodeToTaint.Parent, parentStreamNode);
    			}    			    			
    			
    			// and add to TaintRules as taint propagator
    			var lastMethodDeclaration_IMethod = o2CodeStream.O2MappedAstData.iMethod(lastMethodDeclaration);
				o2CodeStream.TaintRules.add_TaintPropagator(lastMethodDeclaration_IMethod.fullName());    			
				o2CodeStream.TaintRules.add_TaintPropagator(lastMethodDeclaration_IMethod.DotNetName);
    		}
    		return o2CodeStream;
    	}
    	
    	public static O2CodeStream expandTaint(this O2CodeStream o2CodeStream, ParameterDeclarationExpression parameter, O2CodeStreamNode parentStreamNode)
    	{    		
    		if (parameter.Parent is MethodDeclaration)
    		{
    			var parameterName = parameter.name();
    			var methodDeclaration = parameter.Parent as MethodDeclaration;
    			foreach(var identifier in methodDeclaration.iNodes<IdentifierExpression>())
    				if (identifier.Identifier == parameterName)
    					o2CodeStream.createStream(identifier, parentStreamNode);	    				    				 			
    		}
    		return o2CodeStream;
    	}
    	
    	public static O2CodeStream expandTaint(this O2CodeStream o2CodeStream, VariableDeclaration variable, O2CodeStreamNode parentStreamNode)
    	{
    		if (variable== null)
    			return o2CodeStream;
    		"expandTaint for VariableDeclaration:{0}".info( variable.Name);
    		var parentMethod = variable.parent<MethodDeclaration>();
    		if (parentMethod != null)
    		{
    			foreach(var identifier in parentMethod.iNodes<IdentifierExpression>())
    				if (identifier.Identifier == variable.Name)
    					o2CodeStream.createStream(identifier, parentStreamNode);
    				//"identifier: {0}".error(identifier.Identifier);
    			//"got method".error();
    		}
    		else
    			"in VariableDeclaration.expandTaint could not find parent method for provided variable: {0}".error(variable.Name);
    		
    		return o2CodeStream;
    	}
    	
    	public static O2CodeStream expandTaint(this O2CodeStream o2CodeStream, Expression expression, IdentifierExpression identifier, O2CodeStreamNode parentStreamNode)
    	{      
    		if (expression == null)
    		{
    			"in expression.expandTaint, expression was NULL".error();
    			return o2CodeStream;
    		}
    			
    		"MAPPING FOR: {0}   parent : {1}   identifier: {2}".info(
    			expression.typeName(), 
    			expression.Parent.typeName(),
    			(identifier != null) ? identifier.Identifier : "[NULL]");
    		
    			
    		switch(expression.typeName())
    		{
    			case "IdentifierExpression":
    			
    				//var identifier = expression as IdentifierExpression; 		
		    		
		    		var parent = expression.Parent;
		    		if (parent == expression)		// just in case so that we don't have a non-ending recursive loop
		    			return o2CodeStream;			    								
										
		    		switch(parent.typeName())
		    		{
		    			case "InvocationExpression":
			    		case "BinaryOperatorExpression":			    		
			    		case "ObjectCreateExpression":
			    		case "CollectionInitializerExpression":
			    		case "ArrayCreateExpression":
			    			o2CodeStream.expandTaint(parent as Expression, expression as IdentifierExpression, parentStreamNode);
			    			break;
			    		case "ReturnStatement":
			    		case "VariableDeclaration":
			    			o2CodeStream.createStream(parent, parentStreamNode);			    			
			    			break;			    					    		
			    	 	default:
	 		    			"in Expression.IdentifierExpression.expandTaint unsupported INode parent type: {0}".error(parent.typeName());
			    			break;
		    		}
		    		break;
		    		
	    		case "InvocationExpression":    				
		    			var invocationExpression = (InvocationExpression)expression;
		    			var argumentPosition = invocationExpression.argumentPosition(identifier);
		    			"argument Position:{0}".info(argumentPosition);
		    			var calledIMethod = o2CodeStream.O2MappedAstData.iMethod(invocationExpression);
		    					    					    					    		
		    			if (calledIMethod != null)
		    			{		    				
			    			if (o2CodeStream.TaintRules.isTaintPropagator(calledIMethod.DotNetName))
			    			{
			    				"Handling Taint Propagator:{0}".info(calledIMethod.DotNetName);	
			    				if (invocationExpression.Parent is InvocationExpression)
			    				{
			    					var taintNode = o2CodeStream.add_INode(invocationExpression, parentStreamNode);			    					
			    					o2CodeStream.expandTaint(invocationExpression.Parent as Expression, identifier, taintNode);
			    				}
			    				else
				    				o2CodeStream.createStream(invocationExpression.Parent, parentStreamNode);			    				
			    			}
			    			else
			    			{
			    				var methodDeclaration = o2CodeStream.O2MappedAstData.methodDeclaration(calledIMethod);
			     				if( methodDeclaration != null)
			    				{			    					
			    					if (argumentPosition > -1 && methodDeclaration.parameters().Count > argumentPosition)    						
			    					{		    						
			    						var invocationNode = o2CodeStream.add_INode(methodDeclaration, parentStreamNode);
			    						o2CodeStream.createStream(methodDeclaration.parameters()[argumentPosition], invocationNode);
			    					}
			    					else
			    					{
			    						"in IdentifierExpression.InvocationExpression.expandTaint, methodDeclaration.parameters().Count > parameterPosition (adding the method as iNode)".error();
			    						o2CodeStream.add_INode(methodDeclaration, parentStreamNode);
			    					}
			    				}
		    				}
		    			}
		    			break;
		    			
		    	case "ObjectCreateExpression":
		    		var objectCreateExpression = (ObjectCreateExpression)expression;
		    		var parameterPosition = objectCreateExpression.parameterPosition(identifier);
		    		"parameter Position:{0}".info(parameterPosition);
		    		var ctorIMethod = o2CodeStream.O2MappedAstData.iMethod(objectCreateExpression);
		    		if (ctorIMethod != null)
		    		{		    			
		    			var constructorDeclaration = o2CodeStream.O2MappedAstData.constructorDeclaration(ctorIMethod);
		    			if( constructorDeclaration != null)
		    			{
		    				if (parameterPosition > -1 && constructorDeclaration.parameters().Count > parameterPosition)    						
		    						o2CodeStream.createStream(constructorDeclaration.parameters()[parameterPosition], parentStreamNode);
		    					else
		    					{
		    						"in IdentifierExpression.ObjectCreateExpression.expandTaint, constructorDeclaration.parameters().Count > parameterPosition (adding the constructor as iNode)".error();
		    						o2CodeStream.add_INode(constructorDeclaration, parentStreamNode);
		    					}
		    			}
		    		}
		    		break;
		    		
		    	case "BinaryOperatorExpression":
		    	case "CollectionInitializerExpression":
		    	case "ArrayCreateExpression":
		    		if (expression.Parent is Expression)
						o2CodeStream.expandTaint(expression.Parent as Expression, identifier, parentStreamNode);					 
					else
						o2CodeStream.createStream(expression.Parent, parentStreamNode);					 					
					break;
					
		    	default:
	    			"in Expression.expandTaint unsupported INode type: {0}".error(expression.typeName());
	    			break;
	    	}    			
    		return o2CodeStream;
    	}
    	
    	public static O2CodeStream expandTaint(this O2CodeStream o2CodeStream, O2CodeStreamNode streamNode, string identifierWithTaint, O2CodeStreamNode parentStreamNode)
    	{
    		"identifierWithTaint:{0}".info(identifierWithTaint);
    		"type {0}".info(streamNode.INode.typeName());
    		"size {0}".info(streamNode.INode.iNodes().size()); 
    		foreach(var iNode in streamNode.INode.iNodes())
    			if (iNode != null && iNode is IdentifierExpression)    			    				
    				if (((IdentifierExpression)iNode).Identifier == identifierWithTaint)
	    				o2CodeStream.add_INode(iNode, parentStreamNode);	    				    		
    		return o2CodeStream;
    	}
    	    	
    	
    	public static INode getParentINodeThatIsNotAdded(this O2CodeStream o2CodeStream, INode iNode)
    	{
    		while (iNode!= null && iNode.Parent != null)
    		{    			    			    			
    			if(o2CodeStream.has_INode(iNode).isFalse())
    				return iNode;    			
    			iNode = iNode.Parent;
    		}
    		return null;
    	}
    	
    	public static INode popStack(this O2CodeStream o2CodeStream)
    	{
    		if (o2CodeStream.INodeStack.Count >0)
    			return o2CodeStream.INodeStack.Pop();
    		return null;
    	}
    	
    	
    	public static T popStack<T>(this O2CodeStream o2CodeStream)
    		where T : INode
    	{
    		while (o2CodeStream.INodeStack.Count >0)
    		{
    			var iNode = o2CodeStream.INodeStack.Pop();
    			if (iNode is T)
    				return (T)iNode;
    		}
    		return default(T); 
    	}
    	
    	// had to do this because of the 'late calculation' properties of List<T>
    	/*public static List<O2CodeStreamNode> copy(this List<O2CodeStreamNode> streamPath)
    	{
    		var copy = new List<O2CodeStreamNode>();
    		//copy.AddRange(streamPath);
    		foreach(var path in streamPath)
    			copy.Add(path);
    		return copy;
    	}*/
    	
    	public static List<List<O2CodeStreamNode>> getUniqueStreamPaths(this O2CodeStream o2CodeStream, int maxDepth)
    	{
    		var uniqueStreamPaths = new List<List<O2CodeStreamNode>>();
    		foreach(var streamNode in o2CodeStream.StreamNode_First)
    			uniqueStreamPaths.createUniqueStreamPath(maxDepth, new List<O2CodeStreamNode>(),streamNode);
    		return uniqueStreamPaths;
    	}
    	
    	public static void createUniqueStreamPath(this List<List<O2CodeStreamNode>> uniqueStreamPaths, int maxDepth, List<O2CodeStreamNode> streamPath, O2CodeStreamNode streamNode)
    	{
			if (streamNode != null)
			{
				streamPath.add(streamNode);				
				if (streamPath.size() >= maxDepth)
				{
					"in createUniqueStreamPath, maxDepth reached: {0}".error(maxDepth);
					uniqueStreamPaths.Add(streamPath);
				}
				else if (streamNode.ChildNodes.size() == 0)
					uniqueStreamPaths.Add(streamPath);
				else
				{
					var streamPaths = new Dictionary<List<O2CodeStreamNode>, O2CodeStreamNode>();
					for(int i = 0; i <  streamNode.ChildNodes.size(); i ++)
					{ 
						var childStream = streamNode.ChildNodes[i];
						if (i > 0)								//create a copy of the current stream for branches																				
							streamPaths.Add(new List<O2CodeStreamNode>(streamPath),childStream);
						else
							streamPaths.Add(streamPath, childStream);													
					}				
					foreach(var item in streamPaths) 
						uniqueStreamPaths.createUniqueStreamPath(maxDepth,item.Key, item.Value);
				}
			}			 
    	}
    	 
    	public static O2CodeStream show(this O2CodeStream o2CodeStream, TreeView treeView)
    	{
    		treeView.clear();     		
    		// show all INodes
    		var allINodes = treeView.add_Node("All INodes");
    		foreach(var node in o2CodeStream.streamNodes())    		    			    			
    			allINodes.add_Node(node.Text, node);
    		//allINodes.expand();
    		    		
    		// show tree (starting on parent)
    		treeView.removeEventHandlers_BeforeExpand();
    		
    		var parentNodes = treeView.add_Node("Parent(s)");
    		foreach(var streamNode in o2CodeStream.StreamNode_First)
    			parentNodes.add_Node(streamNode.Text,  
    			    			 	 streamNode,
    			    			 	 streamNode.ChildNodes.size() > 0);

    		treeView.beforeExpand<O2CodeStreamNode>(
    			(streamNode)=>  {
    								var currentNode = treeView.current();
    								currentNode.clear();
    								foreach(var childStreamNode in streamNode.ChildNodes)
    									currentNode.add_Node(childStreamNode.Text, childStreamNode, childStreamNode.ChildNodes.size() > 0);
    								//currentNode.add_Node("There are {0} childNodes".format(streamNode.ChildNodes.size()));
    							});

			
			// show unique paths

			var uniqueStreamPaths = o2CodeStream.getUniqueStreamPaths(100);
			
			var uniquePaths = treeView.add_Node("UniquePaths");
			for(int i = 0 ; i < uniqueStreamPaths.size() ; i ++)
			{
				var uniquePath = uniquePaths.add_Node("path #" + (i+1));
				foreach(var streamNode in uniqueStreamPaths[i])
					uniquePath.add_Node(streamNode.str(),streamNode);
			}
			uniquePaths.expand();
			
    							
			//treeView.expandAll();
			treeView.selectFirst();
    		return o2CodeStream;
    	}    	    	    	     	 
    	//REMOVE 
    	public static List<IO2Finding> o2Findings(this O2CodeStream o2CodeStream)
    	{
    		var uniqueStreamPaths = o2CodeStream.getUniqueStreamPaths(100);
    		var o2Findings = new List<IO2Finding>();    		
    		foreach(var uniquePath in uniqueStreamPaths)
    		{	    			    		
	    		var o2Finding = new O2Finding();			
	    		o2Finding.vulnName = "WebServices -> SqlInjection";
				o2Finding.vulnType = "O2 Code Stream Finding";
				var o2Trace = o2Finding.addTrace("WebService EntryPoint");				
				o2Trace.traceType = TraceType.Source;
				foreach(var streamNode in uniquePath)
				{
					var newTrace = new O2Trace(streamNode.str());
					o2Trace.childTraces.Add(newTrace);
					o2Trace = newTrace;
				}
				o2Trace.traceType = TraceType.Known_Sink;
				
				o2Findings.Add(o2Finding);
			}
	    	return o2Findings;	    	
    	}
    }
}
