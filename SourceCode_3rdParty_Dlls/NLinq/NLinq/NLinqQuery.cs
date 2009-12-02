using System;
using System.Collections;
using System.Text;
using Antlr.Runtime.Tree;
using Evaluant.NLinq.Expressions;
using Antlr.Runtime;
using System.Collections.Generic;

namespace Evaluant.NLinq
{
    public class NLinqQuery
    {
        protected string expression;
        protected Expression linqExpression;

        public NLinqQuery(string expression)
        {
            if (expression == null || expression == String.Empty)
                throw new 
                    ArgumentException("Expression can't be empty", "expression");

            this.expression = expression;

            Parse();
        }

        protected void Parse()
        {
            NLinqLexer lexer = new NLinqLexer(new ANTLRStringStream(expression));
            NLinqParser parser = new NLinqParser(new CommonTokenStream(lexer));

            linqExpression = parser.linqExpression().value;
        }

        public Expression Expression
        {
            get { return linqExpression; }
        }
    }
}
