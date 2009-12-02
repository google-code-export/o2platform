using System;

namespace Evaluant.NLinq.Expressions
{
	public class TypedNew : Expression
	{
        public TypedNew(string type, Expression[] parameters)
		{
            this.type = type;
            this.parameters = parameters;
		}

        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private Expression[] parameters;

        public Expression[] Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        public override void Accept(NLinqVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
