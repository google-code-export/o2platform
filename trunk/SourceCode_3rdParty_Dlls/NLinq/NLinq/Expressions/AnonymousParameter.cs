using System;
using System.Collections.Generic;
using System.Text;

namespace Evaluant.NLinq.Expressions
{
    public class AnonymousParameter
    {
        public AnonymousParameter(Identifier identifier, Expression expression)
        {
            this.identifier = identifier;
            this.expression = expression;
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

        public string GetPropertyName()
        {
            if (identifier != null)
                return identifier.Text;
            
            Statement s = Expression as Statement;
            if(s == null)
            {
                throw new NLinqException("In anonymous types, a property name mus be set");
            }

            return (s.Expressions[s.Expressions.Length - 1] as Identifier).Text;
        }
    }

}
