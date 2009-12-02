using System;

namespace Evaluant.NLinq.Expressions
{
	public class Identifier : Expression
	{
		public Identifier(string text)
		{
            this.text = text;
		}

        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public override string ToString()
        {
            return text;
        }

        public override void Accept(NLinqVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
