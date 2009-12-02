
using System.Collections.Generic;
using System.Collections;
namespace Evaluant.NLinq.Expressions
{
    public abstract class NLinqVisitor
    {
        public abstract void Visit(FromClause expression);
        public abstract void Visit(JoinClause expression);
        public abstract void Visit(LetClause expression);
        public abstract void Visit(AnonymousNew expression);
        public abstract void Visit(TypedNew expression);
        public abstract void Visit(OrderByClause expression);
        public abstract void Visit(QueryBody expression);
        public abstract void Visit(QueryBodyClause expression);
        public abstract void Visit(QueryContinuation expression);
        public abstract void Visit(QueryExpression expression);
        public abstract void Visit(GroupClause expression);
        public abstract void Visit(SelectClause expression);
        public abstract void Visit(WhereClause expression);
        public abstract void Visit(BinaryExpression expression);
        public abstract void Visit(TernaryExpression expression);
        public abstract void Visit(UnaryExpression expression);
	    public abstract void Visit(ValueExpression expression);
        public abstract void Visit(Identifier expression);
        public abstract void Visit(MethodCall expression);
        public abstract void Visit(Indexer expression);
        public abstract void Visit(Statement expression);
        public abstract void Visit(Parameter expression);

        public abstract T Evaluate<T>();
        public abstract object Evaluate();

        public virtual IEnumerable<T> Enumerate<T>()
        {
            foreach (T t in Enumerate())
            {
                yield return t;
            }
        }

        public virtual IEnumerable Enumerate()
        {
            return (IEnumerable)Evaluate();
        }

        public virtual List<T> List<T>()
        {
            List<T> list = new List<T>();
            foreach (T t in List())
            {
                list.Add(t);
            }

            return list;
        }

        public virtual IList List()
        {
            ArrayList list = new ArrayList();
            foreach (object o in Enumerate())
            {
                list.Add(o);
            }

            return list;
        }
    }
}
