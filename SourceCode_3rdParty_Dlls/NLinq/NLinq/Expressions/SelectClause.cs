using System;
using System.Collections.Generic;
using System.Text;

namespace Evaluant.NLinq.Expressions
{
    public class SelectClause : SelectOrGroupClause
    {
        public SelectClause(Expression expression)
        {
            this.expression = expression;
        }

        private Expression expression;

        public Expression Expression
        {
            get { return expression; }
            set { expression = value; }
        }

        public override void Accept(NLinqVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
