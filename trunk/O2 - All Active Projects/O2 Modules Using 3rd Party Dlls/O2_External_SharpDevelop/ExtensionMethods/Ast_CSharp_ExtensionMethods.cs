using System;
using System.Linq;
using System.Collections.Generic;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Kernel.ExtensionMethods;
using O2.External.SharpDevelop.AST;

namespace O2.External.SharpDevelop.ExtensionMethods
{
    public static class Ast_CSharp_ExtensionMethods
    {
        public static List<INode> children(this CompilationUnit compilationUnit)
        {
            return compilationUnit.CurrentBock.Children;
        }

        public static CompilationUnit insert(this CompilationUnit compilationUnit, INode node)
        {
            compilationUnit.children().Insert(0, node);
            return compilationUnit;
        }

        public static TypeDeclaration add_Type(this CompilationUnit compilationUnit, string typeName)
        {
            const Modifiers modifiers = Modifiers.None | Modifiers.Public;
            var newType = new TypeDeclaration(modifiers, new List<AttributeSection>())
            {
                Name = typeName
            };
            compilationUnit.AddChild(newType);
            return newType;
        }

        public static MethodDeclaration add_Method(this TypeDeclaration typeDeclaration, string methodName, BlockStatement body)
        {
            return typeDeclaration.add_Method(null, body);
        }

        public static MethodDeclaration add_Method(this TypeDeclaration typeDeclaration, string methodName, Dictionary<string, object> invocationParameters, BlockStatement body)
        {
            var newMethod = new MethodDeclaration
            {
                Name = methodName,
                //Modifier = Modifiers.None | Modifiers.Public | Modifiers.Static,
                Modifier = Modifiers.None | Modifiers.Public,
                Body = body
            };
            newMethod.setReturnType();
            if (invocationParameters != null)

                foreach (var invocationParameter in invocationParameters)
                {
                    var parameterType = new TypeReference(invocationParameter.Value.typeFullName(), true);
                    var parameter = new ParameterDeclarationExpression(parameterType, invocationParameter.Key);
                    newMethod.Parameters.Add(parameter);

                }
            typeDeclaration.AddChild(newMethod);
            return newMethod;
        }

        public static void setReturnType(this MethodDeclaration methodDeclaration)
        {
            var blockStatement = methodDeclaration.Body;
            if (false == blockStatement.hasReturnStatement())
                methodDeclaration.TypeReference = new TypeReference("void", true);
            else
            {
                var returnValue = blockStatement.getLastReturnValue() ?? new object();
                methodDeclaration.TypeReference = new TypeReference(returnValue.typeFullName(), true);
            }
        }

        public static CompilationUnit add_Using(this CompilationUnit compilationUnit, string usingNamespace)
        {
            var usingDeclaration = new UsingDeclaration(usingNamespace);
            compilationUnit.insert(usingDeclaration);
            return compilationUnit;
        }

        public static string errors(this SnippetParser snippetParser)
        {
            if (snippetParser.Errors.Count > 0)
                return snippetParser.Errors.ErrorOutput;
            return "";
        }

        public static bool hasReturnStatement(this AbstractNode abstractNode)
        {
            return abstractNode.isLastChild(typeof(ReturnStatement));
        }

        public static INode lastChild(this AbstractNode abstractNode)
        {
            var childrenCount = abstractNode.Children.Count;
            if (childrenCount > 0)
                return abstractNode.Children[childrenCount - 1];
            return null;
        }

        public static bool isLastChild(this AbstractNode abstractNode, Type type)
        {
            var lastChild = abstractNode.lastChild();

            return (lastChild != null) ?
                        lastChild.GetType() == type :
                        false;
        }

        public static object getLastReturnValue(this AbstractNode abstractNode)
        {
            if (abstractNode.hasReturnStatement())
            {
                var returnStatement = (ReturnStatement)abstractNode.lastChild();
                if (returnStatement.Expression is PrimitiveExpression)
                {
                    var primitiveExpression = (PrimitiveExpression)returnStatement.Expression;
                    return primitiveExpression.Value;
                }
                return new object();
            }
            return null;
        }

        public static void runForEachCompilationError(this string compilationErrors, Action<int, int> runOnError)
        {
            foreach (var items in compilationErrors.lines().split("::"))
                if (items.size() > 2)
                    runOnError(items[0].toInt(), items[1].toInt());
        }

        public static void runForEachAstParsingError(this string astParsingErrors, Action<int, int> runOnError)
        {
            foreach (var items in astParsingErrors.lines().split_onSpace())
                if (items.size() > 5 && items[1].eq("line") && items[3].eq("col"))
                {
                    runOnError(items[2].toInt(), items[4].toInt());
                }
        }

        #region AstValue

        public static bool validBody(this MethodDeclaration methodDeclaration)
        {
            return (methodDeclaration.Body != null && methodDeclaration.Body.Children != null && methodDeclaration.Body.Children.Count > 0);
        }
        public static Location firstLineOfCode(this MethodDeclaration methodDeclaration)        
        {
            if (methodDeclaration.validBody())
                return methodDeclaration.Body.Children[0].StartLocation;
            return new Location(0, 0);
        }
        #endregion

    }
}