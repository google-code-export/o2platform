using System;
using System.Collections.Generic;
using System.Text;

namespace Evaluant.NLinq.Expressions
{
    public class FromClause : QueryBodyClause
    {
        public FromClause(string type, Identifier identifier, Expression expression)
        {
            this.type = type;
            this.identifier = identifier;
            this.expression = expression;
        }

        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private Identifier identifier;

        public Identifier Identifier
        {
            get { return identifier; }
            set { identifier = value; }
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
