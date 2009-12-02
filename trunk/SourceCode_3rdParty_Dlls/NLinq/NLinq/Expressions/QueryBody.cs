using System;
using System.Collections.Generic;
using System.Text;

namespace Evaluant.NLinq.Expressions
{
    public class QueryBody
    {
        public QueryBody(QueryBodyClause[] clauses, SelectOrGroupClause selectOrGroup, QueryContinuation continuation)
        {
            this.clauses = clauses;
            this.selectOrGroup = selectOrGroup;
            this.continuation = continuation;
        }

        private QueryBodyClause[] clauses;

        public QueryBodyClause[] Clauses
        {
            get { return clauses; }
            set { clauses = value; }
        }

        private SelectOrGroupClause selectOrGroup;

        public SelectOrGroupClause SelectOrGroup
        {
            get { return selectOrGroup; }
            set { selectOrGroup = value; }
        }

        private QueryContinuation continuation;

        public QueryContinuation Continuation
        {
            get { return continuation; }
            set { continuation = value; }
        }

        public void Accept(NLinqVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
