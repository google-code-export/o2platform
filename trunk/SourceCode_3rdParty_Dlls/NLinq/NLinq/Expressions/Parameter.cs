using System;

namespace Evaluant.NLinq.Expressions
{
	public class Parameter : Expression
	{
		public Parameter(string name)
		{
            this.name = name;
		}

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public override void Accept(NLinqVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
