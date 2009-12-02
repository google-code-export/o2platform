using System;

namespace Evaluant.NLinq.Expressions
{
	public class AnonymousNew : Expression
	{
        public AnonymousNew(string type, AnonymousParameter[] parameters)
		{
            this.type = type;
            this.parameters = parameters;
		}

        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private AnonymousParameter[] parameters;

        public AnonymousParameter[] Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

 
        public override void Accept(NLinqVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
