using System;
using System.Collections.Generic;
using System.Text;

namespace Evaluant.NLinq.Expressions
{
    public class QueryBodyClause
    {
        public virtual void Accept(NLinqVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
