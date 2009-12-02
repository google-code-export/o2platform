using System;

namespace Evaluant.NLinq.Expressions
{
	public class MethodCall : Expression
	{
        public MethodCall(Identifier identifier, Expression[] parameters)
        {
            this.identifier = identifier;
            this.parameters = parameters;
        }

        public MethodCall(Identifier identifier, Expression[] parameters, Identifier anonIndentifier, Identifier indexIdentifier, Expression lambdaExpression)
        {
            this.identifier = identifier;
            this.parameters = parameters;
            this.anonIndentifier = anonIndentifier;
            this.indexIdentifier = indexIdentifier;
            this.lambdaExpression = lambdaExpression;
        }

        private Identifier anonIndentifier;

        public Identifier AnonIdentifier
        {
            get { return anonIndentifier; }
            set { anonIndentifier = value; }
        }

        private Identifier indexIdentifier;

        public Identifier IndexIdentifier
        {
            get { return indexIdentifier; }
            set { indexIdentifier = value; }
        }

        private Expression lambdaExpression;

        public Expression LambdaExpression
        {
            get { return lambdaExpression; }
            set { lambdaExpression = value; }
        }

        private Identifier identifier;

        public Identifier Identifier
        {
            get { return identifier; }
            set { identifier = value; }
        }

        private Expression[] parameters;

        public Expression[] Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        public override string ToString()
        {
            string result = identifier.ToString() + "(";

            foreach (Expression e in parameters)
            {
                result += e.ToString() + ",";
            }

            if (result.EndsWith(","))
            {
                result = result.Substring(0, result.Length - 1);
            }

            result += ")";

            return result;
        }

        public override void Accept(NLinqVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
