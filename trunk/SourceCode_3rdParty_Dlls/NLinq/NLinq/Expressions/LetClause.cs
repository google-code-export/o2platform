using System;
using System.Collections.Generic;
using System.Text;

namespace Evaluant.NLinq.Expressions
{
    public class LetClause : QueryBodyClause
    {
        public LetClause(Identifier left, Expression right)
        {
            this.left = left;
            this.right = right;
        }

        private Identifier left;

        public Identifier Left
        {
            get { return left; }
            set { left = value; }
        }

        private Expression right;

        public Expression Right
        {
            get { return right; }
            set { right = value; }
        }

        public override void Accept(NLinqVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
