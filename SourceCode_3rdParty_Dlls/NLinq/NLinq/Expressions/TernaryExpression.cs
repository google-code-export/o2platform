using System;

namespace Evaluant.NLinq.Expressions
{
	public class TernaryExpression : Expression
	{
        public TernaryExpression(Expression leftExpression, Expression middleExpression, Expression rightExpression)
		{
            this.leftExpression = leftExpression;
            this.middleExpression = middleExpression;
            this.rightExpression = rightExpression;
		}

		private Expression leftExpression;
		
        public Expression LeftExpression
		{
			get { return leftExpression; }
			set { leftExpression = value; }
		}

        private Expression middleExpression;

        public Expression MiddleExpression
        {
            get { return middleExpression; }
            set { middleExpression = value; }
        }
	
        private Expression rightExpression;
		
        public Expression RightExpression
		{
			get { return rightExpression; }
			set { rightExpression = value; }
		}

        public override void Accept(NLinqVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return leftExpression.ToString() + " (" + middleExpression.ToString() + ", " + rightExpression.ToString() + ")";
        }
    }
    
}
