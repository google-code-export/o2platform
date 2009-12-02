using System;
using System.Collections.Generic;
using System.Text;

namespace Evaluant.NLinq.Expressions
{
    public abstract class SelectOrGroupClause
    {
        public abstract void Accept(NLinqVisitor visitor);
    }
}
