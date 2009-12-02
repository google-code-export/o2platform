using System;
using System.Collections.Generic;
using System.Text;

namespace Evaluant.NLinq.Expressions
{
    public class QueryContinuation
    {
        public QueryContinuation(Identifier identifier, QueryBody queryBody)
        {
            this.identifier = identifier;
            this.queryBody = queryBody;
        }

        private Identifier identifier;

        public Identifier Identifier
        {
            get { return identifier; }
            set { identifier = value; }
        }

        private QueryBody queryBody;

        public QueryBody QueryBody
        {
            get { return queryBody; }
            set { queryBody = value; }
        }

        public void Accept(NLinqVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
