using System;
using System.Collections.Generic;
using System.Text;

namespace Jint.Expressions
{
    [Serializable]
    public class BlockStatement : Statement
    {
        public LinkedList<Statement> Statements { get; set; }

        public BlockStatement()
        {
            Statements = new LinkedList<Statement>();
        }

        [System.Diagnostics.DebuggerStepThrough]
        public override void Accept(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }


        private bool reordered = false;

        public LinkedList<Statement> ReorderStatements()
        {
            if (!reordered)
            {
                var iter = Statements.First;
                while (iter != null)
                {
                    var next = iter.Next;
                    if (iter.Value is FunctionDeclarationStatement)
                    {
                        Statements.Remove(iter);
                        Statements.AddFirst(iter.Value);
                    }

                    iter = next;
                }

                reordered = true;
            }

            return Statements;
        }
    }
}
