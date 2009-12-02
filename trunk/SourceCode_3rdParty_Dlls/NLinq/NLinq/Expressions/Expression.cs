using System;
using Antlr.Runtime.Tree;

namespace Evaluant.NLinq.Expressions
{
    public abstract class Expression
    {
        public abstract void Accept(NLinqVisitor visitor);
    }
}
