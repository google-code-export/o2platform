using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph;
using O2.Kernel.ExtensionMethods;
using O2.API.AST.ExtensionMethods;
using O2.API.AST.ExtensionMethods.CSharp;
using ICSharpCode.NRefactory.Ast;
using ICSharpCode.NRefactory.Visitors;
using ICSharpCode.SharpDevelop.Dom.NRefactoryResolver;
using ICSharpCode.SharpDevelop.Dom;
using O2.Kernel;
using ICSharpCode.NRefactory;
using O2.API.AST.CSharp;
//O2Ref:QuickGraph.dll

namespace O2.API.AST.Graph
{
    public class GraphAstVisitor : AbstractAstVisitor
    {
        public BidirectionalGraph<object, IEdge<object>> Graph { get; set; }
        public O2AstResolver O2AstResolver { get; set; }
        public CompilationUnit CompilationUnit { get; set; }

        public GraphAstVisitor(O2AstResolver o2AstResolver, string file) :
            this(o2AstResolver, o2AstResolver.getCompilationUnit(file))
        { 
            
        }
        public GraphAstVisitor(O2AstResolver o2AstResolver, CompilationUnit compilationUnit)
        {               
            Graph = new BidirectionalGraph<object, IEdge<object>>();
            CompilationUnit = compilationUnit;
            O2AstResolver = o2AstResolver;
        }

        

        public override object VisitMethodDeclaration(MethodDeclaration methodDeclaration, object data)
        {
            O2AstResolver.resolver.RunLookupTableVisitor(methodDeclaration);  // make sure the current variables are mapped to the resolver.lookupTableVisitor
            var method = new O2GraphAstNode(methodDeclaration, "[m] " + methodDeclaration.name(), CompilationUnit);
            Graph.edge(data, method);            
            return base.VisitMethodDeclaration(methodDeclaration, method);
        }

        /*public override object VisitTypeReference(TypeReference typeReference, object data)
        {
            var type = new O2GraphNode(typeReference, "[Type]   " + typeReference.Type);
            Graph.edge(type,data);
            return base.VisitTypeReference(typeReference, type);
        }*/

        public override object VisitLocalVariableDeclaration(LocalVariableDeclaration localVariableDeclaration, object data)
        {
            var localVariable = CompilationUnit.newO2GraphNode(localVariableDeclaration, localVariableDeclaration.TypeReference.str());
            Graph.edge(data,localVariable);
            return base.VisitLocalVariableDeclaration(localVariableDeclaration, localVariable);
        }

        public override object VisitVariableDeclaration(VariableDeclaration variableDeclaration, object data)
        {
            var variable = CompilationUnit.newO2GraphNode(variableDeclaration, variableDeclaration.Name);
            var variableInitializer = CompilationUnit.newO2GraphNode(variableDeclaration.Initializer, "initializer");
            Graph.edge(data, variable);
            Graph.edge(variable, variableInitializer);
            return base.VisitVariableDeclaration(variableDeclaration, variableInitializer);
        }

        public override object VisitPrimitiveExpression(PrimitiveExpression primitiveExpression, object data)
        {
            var expression = CompilationUnit.newO2GraphNode(primitiveExpression, primitiveExpression.StringValue);
            Graph.edge(data, expression);
            return base.VisitPrimitiveExpression(primitiveExpression, expression);
        }

        public override object VisitReturnStatement(ReturnStatement returnStatement, object data)
        {
           // "in VisitReturnStatement".debug();
            var statement = CompilationUnit.newO2GraphNode(returnStatement, "returns");
            Graph.edge(data, statement);
            return base.VisitReturnStatement(returnStatement, statement);
        }

        public override object VisitIdentifierExpression(IdentifierExpression identifierExpression, object data)
        {
            var identifier = CompilationUnit.newO2GraphNode(identifierExpression, "[i] " + identifierExpression.Identifier);            
            Graph.edge(data, identifier);
            identifier.onDoubleClick = (sender) => this.expandNode(sender);
            /*try
            {
                resolver.Initialize(parseInformation, identifierExpression.StartLocation.Line, identifierExpression.StartLocation.Column);
                var resolverResult = resolver.ResolveIdentifier(identifierExpression, ExpressionContext.Default);
                "ok".debug();
                if (resolverResult is MethodGroupResolveResult)
                {
                    var methods = (resolverResult as MethodGroupResolveResult).Methods;
                    foreach (var methodGroup in methods)
                        foreach (var method in methodGroup)
                        {
                            Graph.edge(identifier, method.DotNetName);
                            var type = method.typeFullName();
                        }
                    //show.info(methods);
                    //show.info(((MethodGroupResolveResult)resolverResult).ResolvedType);
                    //((MethodGroupResolveResult)resolverResult).ResolvedType.DotNetName.str();
                }
                else
                {
                    Graph.edge(identifier, resolverResult.typeName());
                }      
                
            }
            catch (Exception ex)
            {
                PublicDI.log.ex(ex, true);
                //ex.log("in VisitIdentifierExpression");
            }*/

            /*if (resolverResult..Parent != null)
                Graph.edge(identifier, "WAS NULL");
            else
                Graph.edge(identifier, "not null");*/
            return base.VisitIdentifierExpression(identifierExpression, identifier);
        }

        public override object VisitAssignmentExpression(AssignmentExpression assignmentExpression, object data)
        {
            "in VisitAssignmentExpression".debug();
            base.VisitExpressionStatement(new ExpressionStatement(assignmentExpression.Left),data);
            var left = Graph.firstOutEdge(data);
            var operation = CompilationUnit.newO2GraphNode(assignmentExpression, assignmentExpression.Op.str());
            Graph.edge(left, operation);
            base.VisitExpressionStatement(new ExpressionStatement(assignmentExpression.Right),operation);
            var right = Graph.firstOutEdge(operation);
            Graph.edge(operation, right);
            return null;
         //   return base.VisitAssignmentExpression(assignmentExpression, operation);
        }

        public override object VisitInvocationExpression(InvocationExpression invocationExpression, object data)
        {
            //var resolvedObject = this.resolve(objectCreateExpression);

            /*resolver.Initialize(parseInformation, invocationExpression.StartLocation.Line, invocationExpression.StartLocation.Column);
            
            var resolved = resolver.ResolveInternal((invocationExpression as Expression), ExpressionContext.Default);
            if (resolved is MemberResolveResult)
            {
                Graph.edge(data, (resolved as MemberResolveResult).ResolvedMember.DotNetName);
            }
            else
                if (resolved !=null)
                    Graph.edge(data,resolved.typeName());
                else
                    Graph.edge(data, new O2GraphNode("[RESOLVED WAS NULL]"));
            
            */

            //var targetObject = new O2GraphNode(invocationExpression.TargetObject, "target");
            //Graph.edge(data, targetObject);
            base.VisitExpressionStatement(new ExpressionStatement(invocationExpression.TargetObject),data);

            var identifierObject = Graph.firstOutEdge(data);
            
            foreach (var argument in invocationExpression.Arguments)
                base.VisitExpressionStatement(new ExpressionStatement(argument), identifierObject);

            //return targetObject;
            return null;
            //var invocation = new O2GraphNode(invocationExpression, "invocation");
            
            //return base.VisitInvocationExpression(invocationExpression, invocation);
        }

        public override object VisitExpressionStatement(ExpressionStatement expressionStatement, object data)
        {
            var expression = CompilationUnit.newO2GraphNode(expressionStatement, "expression");
            Graph.edge(data, expression);            
            return base.VisitExpressionStatement(expressionStatement, expression);
        }

        public override object VisitMemberReferenceExpression(MemberReferenceExpression memberReferenceExpression, object data)
        {
            var memberExpression = CompilationUnit.newO2GraphNode(memberReferenceExpression, "[m] " + memberReferenceExpression.MemberName);
            Graph.edge(data, memberExpression);

            var resolvedExpression = O2AstResolver.resolve(memberReferenceExpression);
            var nodeText = (resolvedExpression as ResolveResult).getNodeText();
            var resolvedNode = CompilationUnit.newO2GraphNode(resolvedExpression, nodeText);
            resolvedNode.onDoubleClick = (sender) => this.expandNode(sender);

            Graph.edge(memberExpression, resolvedNode);                        
                        
                //Graph.edge(data, resolved.typeName());            
            return base.VisitMemberReferenceExpression(memberReferenceExpression, memberExpression);
        }

        public override object VisitParameterDeclarationExpression(ParameterDeclarationExpression parameterDeclarationExpression, object data)
        {
            var parameter = CompilationUnit.newO2GraphNode(parameterDeclarationExpression, "[p] " + parameterDeclarationExpression.ParameterName);
            Graph.edge(parameter, data);
            return base.VisitParameterDeclarationExpression(parameterDeclarationExpression, parameter);
        }

        public override object VisitObjectCreateExpression(ObjectCreateExpression objectCreateExpression, object data)
        {
            var resolvedObject = O2AstResolver.resolve(objectCreateExpression);

            var objectCreate = CompilationUnit.newO2GraphNode(objectCreateExpression, "object create");
            Graph.edge(data, objectCreate);
            Graph.edge(data, CompilationUnit.newO2GraphNode(resolvedObject));
            return base.VisitObjectCreateExpression(objectCreateExpression, objectCreate);
        }
        
    }
}
