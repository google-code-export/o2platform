using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.NRefactory.Ast;
using O2.Kernel.ExtensionMethods;

namespace O2.API.AST.ExtensionMethods.CSharp
{
    public static class InvocationExpression_ExtensionMethods
    {
        public static int argumentPosition(this InvocationExpression invocationExpression, IdentifierExpression identifierExpression)
        {
            if (invocationExpression != null && identifierExpression != null)
                for (int i = 0; i < invocationExpression.Arguments.Count; i++)								// for each arguments
                    foreach (var iNode in invocationExpression.Arguments[i].iNodes<IdentifierExpression>())	// get the IdentifierExpression
                        if (iNode == identifierExpression)													// and compare the values
                            return i;
            "in InvocationExpression.parameterPosition could not find provided IdentifierExpression as a current parameter".error();
            return -1;
        } 
    }
}
