using System;

namespace Evaluant.NLinq.Expressions
{
	public class Indexer : Expression
	{
        public Indexer(Expression innerExpression, Expression parameter)
		{
            this.innerExpression = innerExpression;
            this.parameter = parameter;
		}

        private Expression innerExpression;

        public Expression InnerExpression
        {
            get { return innerExpression; }
            set { innerExpression = value; }
        }
        
        private Expression parameter;

        public Expression Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }

        public override string ToString()
        {
            string result = "[";

            result += parameter.ToString();

            result += "]";

            return result;
        }

        public override void Accept(NLinqVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
