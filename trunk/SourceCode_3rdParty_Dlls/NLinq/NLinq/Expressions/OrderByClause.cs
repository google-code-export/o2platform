using System;
using System.Collections.Generic;
using System.Text;

namespace Evaluant.NLinq.Expressions
{
    public class OrderByClause : QueryBodyClause
    {
        public OrderByClause(OrderByCriteria[] criterias)
        {
            this.criterias = criterias;
        }

        private OrderByCriteria[] criterias;

        public OrderByCriteria[] Criterias
        {
            get { return criterias; }
            set { criterias = value; }
        }

        public override void Accept(NLinqVisitor visitor)
        {
            visitor.Visit(this);
        }


    }
}
