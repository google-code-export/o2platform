using System;

namespace Evaluant.NLinq.Expressions
{
	public class Statement : Expression
	{
        public Statement(Expression[] expressions)
		{
            this.expressions = expressions;
		}

        private Expression[] expressions;

        public Expression[] Expressions
        {
            get { return expressions; }
            set { expressions = value; }
        }

        public override string ToString()
        {
            string result = String.Empty;

            foreach (Expression e in expressions)
            {
                result += e.ToString() + ".";
            }

            if (result != String.Empty)
            {
                result = result.Substring(0, result.Length - 1);
            }

            return result;
        }

        public Identifier GetFirstIdentifier()
        {
            return ((Identifier)expressions[0]);
        }

        public override void Accept(NLinqVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
