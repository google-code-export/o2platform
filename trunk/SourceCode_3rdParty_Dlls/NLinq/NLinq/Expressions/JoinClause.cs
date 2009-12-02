using System;
using System.Collections.Generic;
using System.Text;

namespace Evaluant.NLinq.Expressions
{
    public class JoinClause : QueryBodyClause
    {
        public JoinClause(string type, Identifier identifier, Expression inIdentifier, Expression on, Expression equals, Identifier into)
        {
            this.type = type;
            this.identifier = identifier;
            this.inIdentifier = inIdentifier;
            this.on = on;
            this.equals = equals;
            this.into = into;
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

        private Expression inIdentifier;

        public Expression InIdentifier
        {
            get { return inIdentifier; }
            set { inIdentifier = value; }
        }

        private Expression on;

        public Expression On
        {
            get { return on; }
            set { on = value; }
        }

        private Expression equals;

        public Expression Eq
        {
            get { return equals; }
            set { equals = value; }
        }

        private Identifier into;

        public Identifier Into
        {
            get { return into; }
            set { into = value; }
        }

        public override void Accept(NLinqVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
