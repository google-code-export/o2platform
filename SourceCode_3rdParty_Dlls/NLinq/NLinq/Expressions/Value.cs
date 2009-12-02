using System;

namespace Evaluant.NLinq.Expressions
{
    public class ValueExpression : Expression
    {
        public ValueExpression(object value, TypeCode typeCode)
        {
            this.value = value;
            this.typeCode = typeCode;
        }

        private object value;

        public object Value
        {
            get { return value; }
            set { this.value = value; }
        }

        private TypeCode typeCode;

        public TypeCode TypeCode
        {
            get { return typeCode; }
            set { typeCode = value; }
        }

        public override void Accept(NLinqVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
