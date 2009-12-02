using System;
using System.Collections.Generic;
using System.Text;

namespace Evaluant.NLinq.Expressions
{
    public class OrderByCriteria
    {
        public OrderByCriteria(Expression expression, bool ascending)
        {
            this.expression = expression;
            this.ascending = ascending;
        }

        private Expression expression;

        public Expression Expression
        {
            get { return expression; }
            set { expression = value; }
        }

        private bool ascending;

        public bool Ascending
        {
            get { return ascending; }
            set { ascending = value; }
        }

    }
}
