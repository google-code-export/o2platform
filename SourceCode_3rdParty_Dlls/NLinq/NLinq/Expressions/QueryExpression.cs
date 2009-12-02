using System;
using System.Collections.Generic;
using System.Text;

namespace Evaluant.NLinq.Expressions
{
    public class QueryExpression : Expression
    {
        public QueryExpression(FromClause from, QueryBody queryBody)
        {
            if (from == null)
                throw new ArgumentNullException("from");

            if (queryBody == null)
                throw new ArgumentNullException("queryBody");

            this.from = from;
            this.queryBody = queryBody;
        }

        private FromClause from;

        public FromClause From
        {
            get { return from; }
            set { from = value; }
        }

        private QueryBody queryBody;

        public QueryBody QueryBody
        {
            get { return queryBody; }
            set { queryBody = value; }
        }

        public override void Accept(NLinqVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
