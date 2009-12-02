using System;

namespace Evaluant.NLinq.Expressions
{
	public class UnaryExpression : Expression
    {
		public UnaryExpression(UnaryExpressionType type, Expression expression)
		{
            this.type = type;
            this.expression = expression;
		}

		private Expression expression;
		public Expression Expression
		{
			get { return expression; }
			set { expression = value; }
		}

		private UnaryExpressionType type;
		public UnaryExpressionType Type
		{
			get { return type; }
			set { type = value; }
		}

        public override void Accept(NLinqVisitor visitor)
        {
            visitor.Visit(this);
        }
	}

	public enum UnaryExpressionType
	{
		Not,
        Negate
	}
}
