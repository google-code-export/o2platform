using System;
using System.Text;
using System.Collections;
using System.Globalization;
using Evaluant.NLinq.Expressions;
using System.Collections.Generic;
using System.Reflection;

namespace Evaluant.NLinq.Memory
{
    public class LinqToMemory : NLinqVisitor
    {
        protected IPropertyGetter propertyGetter = new CachedReflectionPropertyGetter();
        protected IMethodInvoker methodInvoker = new CachedMethodInvoker();

        protected object result;
        protected NLinqQuery query;
        protected Dictionary<string, IEnumerable> namedSources = new Dictionary<string, IEnumerable>();
        protected List<object> currentTuple = null;

        protected List<string> sourceNames = new List<string>();
        protected List<List<object>> tuples = new List<List<object>>();

        protected Stack<List<string>> sourceNamesStack = new Stack<List<string>>();
        protected Stack<List<List<object>>> tuplesStack = new Stack<List<List<object>>>();

        protected void EnterScope()
        {
            sourceNamesStack.Push(sourceNames);
            sourceNames = new List<string>();

            tuplesStack.Push(tuples);

            tuples = new List<List<object>>();

            foreach (object item in (IEnumerable)result)
            {
                List<object> row = new List<object>();
                row.Add(item);
                tuples.Add(row);
            } 
        }

        protected void ExitScope()
        {
            sourceNames = sourceNamesStack.Pop();
            tuples = tuplesStack.Pop();
        }

        public LinqToMemory(NLinqQuery query)
        {
            this.query = query;
        }

        public void AddSource(string name, IEnumerable source)
        {
            List<object> s = new List<object>();
            foreach(object item in source)
                s.Add(item);
            namedSources.Add(name, s);
        }

        private object Evaluate(Expression expression)
        {
            expression.Accept(this);
            return result;
        }

        public override void Visit(QueryExpression expression)
        {
            expression.From.Accept(this);
            expression.QueryBody.Accept(this);
        }

        public override void Visit(QueryBody expression)
        {
            foreach (QueryBodyClause clause in expression.Clauses)
            {
                clause.Accept(this);
            }

            if (expression.SelectOrGroup != null)
            {
                expression.SelectOrGroup.Accept(this);
            }

            if (expression.Continuation != null)
            {
                expression.Continuation.Accept(this);
            }
        }

        public override void Visit(FromClause expression)
        {

            sourceNames.Add(expression.Identifier.Text);

            // First list
            if(tuples.Count == 0)
            {
                expression.Expression.Accept(this);

                foreach (object item in (IEnumerable)result)
                {
                    List<object> row = new List<object>();
                    row.Add(item);
                    tuples.Add(row);
                }
            }
            else // Creates a scalar product
            {
                List<List<object>> newTuples = new List<List<object>>();

                for(int i=0; i<tuples.Count; i++)
                {
                    currentTuple = tuples[i];

                    expression.Expression.Accept(this);

                    foreach (object item in (IEnumerable)result)
                    {
                        List<object> newTuple = new List<object>(currentTuple);
                        newTuple.Add(item);
                        newTuples.Add(newTuple);
                    }
                }

                tuples = newTuples;
            }
        }

        public override void Visit(WhereClause expression)
        {
            List<List<object>> newTuples = new List<List<object>>();

            for(int i=0; i<tuples.Count; i++)
            {
                currentTuple = tuples[i];

                expression.Expression.Accept(this);

                if ((bool)result)
                {
                    newTuples.Add(currentTuple);
                }
            }

            tuples = new List<List<object>>(newTuples);
        }

        public override void Visit(GroupClause expression)
        {
            Dictionary<object, NLinqGroup> groups = new Dictionary<object, NLinqGroup>();

            for(int i=0; i<tuples.Count; i++)
            {
                currentTuple = tuples[i];
                expression.Expression.Accept(this);

                object key = result;

                if (!groups.ContainsKey(key))
                {
                    groups.Add(key, new NLinqGroup(key));
                }

                Statement s = new Statement(new Expression[] { expression.Identifier });
                s.Accept(this);

                groups[key].Group.Add(result);
            }

            tuples.Clear();

            foreach (NLinqGroup g in groups.Values)
            {
                List<object> row = new List<object>();
                row.Add(g);
                tuples.Add(row);
            }

        }

        public override void Visit(JoinClause expression)
        {
            sourceNames.Add(expression.Identifier.Text);
            List<List<object>> newTuples = new List<List<object>>();

            for(int i=0; i<tuples.Count; i++)
            {
                currentTuple = tuples[i];
                expression.InIdentifier.Accept(this);

                foreach (object item in (IEnumerable)result)
                {
                    List<object> newTuple = new List<object>(tuples[i]);
                    newTuple.Add(item);

                    currentTuple = newTuple;

                    expression.On.Accept(this);
                    object left = result;
                    expression.Eq.Accept(this);
                    object right = result;

                    if (left.Equals(right))
                    {
                        newTuples.Add(newTuple);
                    }

                }
            }

            tuples = newTuples;

            if (expression.Into != null)
            {
                sourceNames.Clear();
                sourceNames.Add(expression.Into.Text);
            }
        }

        public override void Visit(LetClause expression)
        {
            sourceNames.Add(expression.Left.Text);

            // Creates a new computed column in the matrix
            for (int i = tuples.Count - 1; i >= 0; i--)
            {
                List<object> tuple = tuples[i];

                currentTuple = tuple;
                expression.Right.Accept(this);
                tuple.Add(result);
            }
        }

        public override void Visit(AnonymousNew expression)
        {
            if (expression.Type == null)
            {
                Variant v = new Variant();

                foreach (AnonymousParameter p in expression.Parameters)
                {
                    p.Expression.Accept(this);
                    v[p.GetPropertyName()] = result;
                }

                result = v;
            }
            else
            {
                Type type = TypeResolver.ResolveType(expression.Type);

                if (type == null)
                {
                    throw new NLinqException("Unknonwn type: " + expression.Type);
                }

                // Throws an exception if no default constructor is found
                object instance = Activator.CreateInstance(type);

                foreach (AnonymousParameter p in expression.Parameters)
                {
                    p.Expression.Accept(this);

                    PropertyInfo propertyInfo = type.GetProperty(p.GetPropertyName());

                    if (propertyInfo == null)
                    {
                        throw new NLinqException("Property not found [" + p.GetPropertyName() + "] in " + type);
                    }

                    propertyInfo.SetValue(instance, result, null);

                    result = instance;
                }
            }
        }

        public override void Visit(TypedNew expression)
        {
            Type type = TypeResolver.ResolveType(expression.Type);

            if (type == null)
            {
                throw new NLinqException("Unknonwn type: " + expression.Type);
            }

            object[] values = new object[expression.Parameters.Length];

            for (int i = 0; i < expression.Parameters.Length; i++)
            {
                expression.Parameters[i].Accept(this);
                values[i] = result;
            }

            object instance = Activator.CreateInstance(type, values);

            if (instance == null)
            {
                throw new NLinqException("Matching constructor not found for " + expression.Type);
            }

            result = instance;
        }

        public override void Visit(OrderByClause expression)
        {
            tuples.Sort(new Comparison<List<object>>(delegate(List<object> a, List<object> b)
            {

                foreach (OrderByCriteria criteria in expression.Criterias)
                {
                    currentTuple = a;
                    criteria.Expression.Accept(this);
                    object xa = result;

                    currentTuple = b;
                    criteria.Expression.Accept(this);
                    object xb = result;

                    int v = Comparer.Default.Compare(xa, xb);
                    if (v != 0)
                    {
                        if (!criteria.Ascending)
                        {
                            return -1 * v;
                        }
                        else
                        {
                            return v;
                        }
                    }
                }

                return 0;
            }));

        }

        public override void Visit(QueryBodyClause expression)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void Visit(QueryContinuation expression)
        {
            sourceNames.Clear();
            sourceNames.Add(expression.Identifier.Text);

            expression.QueryBody.Accept(this);
        }

        public override void Visit(SelectClause expression)
        {
            Dictionary<object, object> index = new Dictionary<object, object>();
            List<object> selectedItems = new List<object>();

            for(int i=0; i<tuples.Count; i++)
            {
                currentTuple = tuples[i];
                expression.Expression.Accept(this);

                // Implements the distincts
                if (!index.ContainsKey(result))
                {
                    index.Add(result, null);
                    selectedItems.Add(result);
                }
            }

            result = selectedItems;

           
        }

        public override void Visit(MethodCall expression)
        {
            if (expression.LambdaExpression != null)
            {
                result = CallOperator(expression);

                if (result == null)
                {
                    throw new NLinqException("Unknown operator: " + expression.Identifier.Text);
                }
            }
            else
            {
                object[] parameters = new object[expression.Parameters.Length];
                Type[] types = new Type[expression.Parameters.Length];

                if (expression.Parameters.Length > 0)
                {
                    object oldResult = result;

                    for (int j = 0; j < expression.Parameters.Length; j++)
                    {
                        expression.Parameters[j].Accept(this);
                        parameters[j] = result;
                    }

                    result = oldResult;
                }

                if (result == null)
                {
                    return;
                }

                if (!methodInvoker.Invoke(result, expression.Identifier.Text, parameters, ref result))
                {
                    result = CallOperator(expression);

                    if (result == null)
                    {
                        throw new NLinqException("Method not found with specified arguments: " + expression.Identifier.Text);
                    }
                }
            }
        }

        public override void Visit(TernaryExpression expression)
        {
            result = null;

            // Evaluates the left expression and saves the value
            expression.LeftExpression.Accept(this);
            object left = result;

            result = null;

            if ((bool)left)
            {
                // Evaluates the middle expression
                expression.MiddleExpression.Accept(this);
            }
            else
            {
                // Evaluates the right expression
                expression.RightExpression.Accept(this);
            }
        }

        public override void Visit(BinaryExpression expression)
        {
            object old = result;

            // Evaluates the left expression and saves the value
            expression.LeftExpression.Accept(this);
            object left = result;

            result = null;

            // Evaluates the right expression and saves the value
            expression.RightExpression.Accept(this);
            object right = result;

            result = old;

            switch (expression.Type)
            {
                case BinaryExpressionType.And:
                    result = Convert.ToBoolean(left) && Convert.ToBoolean(right);
                    break;

                case BinaryExpressionType.Or:
                    result = Convert.ToBoolean(left) || Convert.ToBoolean(right);
                    break;

                case BinaryExpressionType.Div:
                    result = Convert.ChangeType(Convert.ToDecimal(left) / Convert.ToDecimal(right), left.GetType());
                    break;

                case BinaryExpressionType.Equal:
                    if (left == null && right == null)
                    {
                        result = true;
                    }
                    else
                    {
                        // Use the type of the left operand to make the comparison
                        result = Comparer.Default.Compare(left,
                            Convert.ChangeType(right, left.GetType())) == 0;
                    }
                    break;

                case BinaryExpressionType.Greater:
                    // Use the type of the left operand to make the comparison
                    result = Comparer.Default.Compare(left,
                        Convert.ChangeType(right, left.GetType())) > 0;
                    break;

                case BinaryExpressionType.GreaterOrEqual:
                    // Use the type of the left operand to make the comparison
                    result = Comparer.Default.Compare(left,
                        Convert.ChangeType(right, left.GetType())) >= 0;
                    break;

                case BinaryExpressionType.Lesser:
                    // Use the type of the left operand to make the comparison
                    result = Comparer.Default.Compare(left,
                        Convert.ChangeType(right, left.GetType())) < 0;
                    break;

                case BinaryExpressionType.LesserOrEqual:
                    // Use the type of the left operand to make the comparison
                    result = Comparer.Default.Compare(left,
                        Convert.ChangeType(right, left.GetType())) <= 0;
                    break;

                case BinaryExpressionType.Minus:
                    result = Convert.ChangeType(Convert.ToDecimal(left) - Convert.ToDecimal(right), left.GetType());
                    break;

                case BinaryExpressionType.Modulo:
                    result = Convert.ChangeType(Convert.ToDecimal(left) % Convert.ToDecimal(right), left.GetType());
                    break;

                case BinaryExpressionType.NotEqual:

                    if (left == null && right == null)
                    {
                        result = false;
                    }
                    else
                    {
                        // Use the type of the left operand to make the comparison
                        result = Comparer.Default.Compare(left,
                            Convert.ChangeType(right, left.GetType())) != 0;
                    }
                    break;

                case BinaryExpressionType.Plus:
                    if (left.GetType() == typeof(string))
                    {
                        result = String.Concat(left, right);
                    }
                    else
                    {
                        result = Convert.ChangeType(Convert.ToDecimal(left) + Convert.ToDecimal(right), left.GetType());
                    }
                    break;

                case BinaryExpressionType.Times:
                    result = Convert.ChangeType(Convert.ToDecimal(left) * Convert.ToDecimal(right), left.GetType());
                    break;

                case BinaryExpressionType.Pow:
                    result = Convert.ChangeType(Math.Pow(Convert.ToDouble(left), Convert.ToDouble(right)), left.GetType());
                    break;

            }
        }

        public override void Visit(UnaryExpression expression)
        {
            // Recursively evaluates the underlying expression
            expression.Expression.Accept(this);

            switch (expression.Type)
            {
                case UnaryExpressionType.Not:
                    result = !Convert.ToBoolean(result);
                    break;

                case UnaryExpressionType.Negate:
                    result = -Convert.ToDecimal(result);
                    break;
            }
        }

        public override void Visit(ValueExpression expression)
        {
            result = expression.Value;
        }

        public override void Visit(Identifier identifier)
        {
            if (namedSources.ContainsKey(identifier.Text))
            {
                result = namedSources[identifier.Text];
                return;
            }

            if (result == null)
            {
                try
                {
                    result = currentTuple[sourceNames.IndexOf(identifier.Text)];
                }
                catch
                {
                    throw new NLinqException("Source not found: " + identifier.Text);
                }
            }
            else
            {
                result = propertyGetter.GetValue(result, identifier.Text);
            }
        }

        public override void Visit(Parameter parameter)
        {
        }

        public override object Evaluate()
        {
            query.Expression.Accept(this);

            return result;
        }

        public override T Evaluate<T>()
        {
            return (T)Evaluate();
        }

        public override void Visit(Indexer expression)
        {
            object old = result;

            expression.InnerExpression.Accept(this);

            expression.Parameter.Accept(this);

            result = old.GetType().InvokeMember("Item", BindingFlags.GetProperty, null, old, new object[] { result });
        }

        public override void Visit(Statement expression)
        {
            result = null;

            foreach(Expression e in expression.Expressions)
            {
                e.Accept(this);

                if (result == null)
                    return;
            }
        }

        #region Operators

        private object CallOperator(MethodCall methodCall)
        {
            switch (methodCall.Identifier.Text)
            {
                case "Count" : return Count(methodCall);
                case "Where": return Where(methodCall);
                case "Take": return Take(methodCall);
                case "Skip": return Skip(methodCall);
                case "TakeWhile": return TakeWhile(methodCall);
                case "SkipWhile": return SkipWhile(methodCall);
                case "Select": return Select(methodCall);
                case "Reverse": return Reverse(methodCall);
                case "Distinct": return Distinct(methodCall);
                case "Union": return Union(methodCall);
                case "Intersect": return Intersect(methodCall);
                case "Except": return Except(methodCall);
                case "First": return First(methodCall);
                case "ElementAt": return ElementAt(methodCall);
                case "Any": return Any(methodCall);
                case "All": return All(methodCall);
                case "Sum": return Sum(methodCall);
                case "Min": return Min(methodCall);
                case "Max": return Max(methodCall);
                case "Average": return Average(methodCall);
         
                default:
                    return null;
            }
        }

        public double Average(MethodCall methodCall)
        {
            int count = ((IList)result).Count;
            double sum = (double)Sum(methodCall);

            return sum / count;
        }

        public int Max(MethodCall methodCall)
        {
            int max = int.MinValue;

            if (methodCall.LambdaExpression == null)
            {

                foreach (int value in (IEnumerable)result)
                {
                    max = Math.Max(max, value);
                }

                return max;
            }

            EnterScope();

            if (methodCall.AnonIdentifier != null)
            {
                sourceNames.Add(methodCall.AnonIdentifier.Text);
            }

            if (methodCall.IndexIdentifier != null)
            {
                sourceNames.Add(methodCall.IndexIdentifier.Text);
            }

            int i = 0;
            foreach (object item in (IEnumerable)result)
            {
                currentTuple = new List<object>();

                if (methodCall.AnonIdentifier != null)
                {
                    currentTuple.Add(item);
                }

                if (methodCall.IndexIdentifier != null)
                {
                    currentTuple.Add(i++);
                }

                methodCall.LambdaExpression.Accept(this);

                max = Math.Max(max, (int)result);
            }

            ExitScope();
            return max;
        }

        public int Min(MethodCall methodCall)
        {
            int min = int.MaxValue;

            if (methodCall.LambdaExpression == null)
            {

                foreach (int value in (IEnumerable)result)
                {
                    min = Math.Min(min, value);
                }

                return min;
            }

            EnterScope();

            if (methodCall.AnonIdentifier != null)
            {
                sourceNames.Add(methodCall.AnonIdentifier.Text);
            }

            if (methodCall.IndexIdentifier != null)
            {
                sourceNames.Add(methodCall.IndexIdentifier.Text);
            }

            int i = 0;
            foreach (object item in (IEnumerable)result)
            {
                currentTuple = new List<object>();

                if (methodCall.AnonIdentifier != null)
                {
                    currentTuple.Add(item);
                }

                if (methodCall.IndexIdentifier != null)
                {
                    currentTuple.Add(i++);
                }

                methodCall.LambdaExpression.Accept(this);

                min = Math.Min(min, (int)result);
            }

            ExitScope();

            return min;
        }

        public int Sum(MethodCall methodCall)
        {
            int sum = 0;

            if (methodCall.LambdaExpression == null)
            {

                foreach (int value in (IEnumerable)result)
                {
                    sum += value;
                }

                return sum;
            }

            EnterScope();

            if (methodCall.AnonIdentifier != null)
            {
                sourceNames.Add(methodCall.AnonIdentifier.Text);
            }

            if (methodCall.IndexIdentifier != null)
            {
                sourceNames.Add(methodCall.IndexIdentifier.Text);
            }

            int i = 0;
            foreach (object item in (IEnumerable)result)
            {            
                currentTuple = new List<object>();

                if (methodCall.AnonIdentifier != null)
                {
                    currentTuple.Add(item);
                }

                if (methodCall.IndexIdentifier != null)
                {
                    currentTuple.Add(i++);
                }

                methodCall.LambdaExpression.Accept(this);

                sum += (int)result;
            }

            ExitScope();
            
            return sum;
        }

        public object ElementAt(MethodCall methodCall)
        {
            object o = result;

            methodCall.Parameters[0].Accept(this);

            if( ((IList)o).Count > (int)result)
            {
                return ((IList)o)[(int)result];
            }

            return null;
        }

        public bool Any(MethodCall methodCall)
        {
            if (methodCall.LambdaExpression == null)
            {
                throw new ArgumentException("Expecting a lambda expression in Any operator");
            }

            EnterScope();

            if (methodCall.AnonIdentifier != null)
            {
                sourceNames.Add(methodCall.AnonIdentifier.Text);
            }

            if (methodCall.IndexIdentifier != null)
            {
                sourceNames.Add(methodCall.IndexIdentifier.Text);
            }

            int i = 0;
            foreach (object item in (IEnumerable)result)
            {
                currentTuple = new List<object>();

                if (methodCall.AnonIdentifier != null)
                {
                    currentTuple.Add(item);
                }

                if (methodCall.IndexIdentifier != null)
                {
                    currentTuple.Add(i++);
                }

                methodCall.LambdaExpression.Accept(this);

                if ((bool)result)
                {
                    ExitScope();
                    return true;
                }
            }

            ExitScope();
            return false;
        }

        public bool All(MethodCall methodCall)
        {
            if (methodCall.LambdaExpression == null)
            {
                throw new ArgumentException("Expecting a lambda expression in Any operator");
            }

            EnterScope();

            if (methodCall.AnonIdentifier != null)
            {
                sourceNames.Add(methodCall.AnonIdentifier.Text);
            }

            if (methodCall.IndexIdentifier != null)
            {
                sourceNames.Add(methodCall.IndexIdentifier.Text);
            }

            int i = 0;
            foreach (object item in (IEnumerable)result)
            {
                currentTuple = new List<object>();

                if (methodCall.AnonIdentifier != null)
                {
                    currentTuple.Add(item);
                }

                if (methodCall.IndexIdentifier != null)
                {
                    currentTuple.Add(i++);
                }

                methodCall.LambdaExpression.Accept(this);

                if (!(bool)result)
                {
                    ExitScope();
                    return false;
                }
            }

            ExitScope();
            return true;
        }

        public object First(MethodCall methodCall)
        {
            if (methodCall.LambdaExpression == null)
            {
                if (((IList)result).Count > 0)
                {
                    return ((IList)result)[0];
                }
                else
                {
                    return null;
                }
            }

            EnterScope();

            if (methodCall.AnonIdentifier != null)
            {
                sourceNames.Add(methodCall.AnonIdentifier.Text);
            }

            if (methodCall.IndexIdentifier != null)
            {
                sourceNames.Add(methodCall.IndexIdentifier.Text);
            }

            object evaluation = null;

            int i = 0;
            bool go = false;
            foreach (object item in (IEnumerable)result)
            {
                currentTuple = new List<object>();

                if (methodCall.AnonIdentifier != null)
                {
                    currentTuple.Add(item);
                }

                if (methodCall.IndexIdentifier != null)
                {
                    currentTuple.Add(i++);
                }

                methodCall.LambdaExpression.Accept(this);

                if ((bool)result)
                {
                    evaluation = item;
                    break;
                }

            }

            ExitScope();
            return evaluation;
        }

        public IList<object> Union(MethodCall methodCall)
        {
            IEnumerable aList = (IEnumerable)result;

            methodCall.Parameters[0].Accept(this);
            IList bList = (IList)result;

            List<object> selected = new List<object>();
            int i = 0;

            foreach (object item in aList)
            {
                if (!selected.Contains(item))
                {
                    selected.Add(item);
                }
            }

            foreach (object item in bList)
            {
                if (!selected.Contains(item))
                {
                    selected.Add(item);
                }
            }

            return selected;
        }

        public IList<object> Intersect(MethodCall methodCall)
        {
            IEnumerable aList = (IEnumerable)result;

            methodCall.Parameters[0].Accept(this);
            IList bList = (IList)result;

            List<object> selected = new List<object>();
            int i = 0;
            foreach (object item in aList)
            {
                if (bList.Contains(item))
                {
                    selected.Add(item);
                }
            }

            return selected;
        }

        public IList<object> Except(MethodCall methodCall)
        {
            IEnumerable aList = (IEnumerable)result;

            methodCall.Parameters[0].Accept(this);
            IList bList = (IList)result;

            List<object> selected = new List<object>();
            int i = 0;
            foreach (object item in aList)
            {
                if (!bList.Contains(item))
                {
                    selected.Add(item);
                }
            }

            return selected;
        }

        public IList<object> Distinct(MethodCall methodCall)
        {
            IEnumerable items = (IEnumerable)result;

            List<object> selected = new List<object>();

            foreach (object item in items)
            {
                if (selected.Contains(item))
                {
                    continue;
                }

                selected.Add(item);
            }

            return selected;
        }

        public IList<object> Reverse(MethodCall methodCall)
        {
            List<object> evaluation = new List<object>();

            for (int i = ((IList)result).Count - 1; i >= 0; i++ )
            {
                evaluation.Add(((IList)result)[i]);
            }

            return evaluation;
        }

        public IList<object> Select(MethodCall methodCall)
        {
            if (methodCall.LambdaExpression == null)
            {
                throw new ArgumentException("Expecting a lambda expression in TakeWhile operator");
            }

            EnterScope();

            if (methodCall.AnonIdentifier != null)
            {
                sourceNames.Add(methodCall.AnonIdentifier.Text);
            }

            if (methodCall.IndexIdentifier != null)
            {
                sourceNames.Add(methodCall.IndexIdentifier.Text);
            }

            List<object> evaluation = new List<object>();

            int i = 0;
            foreach (object item in (IEnumerable)result)
            {
                currentTuple = new List<object>();

                if (methodCall.AnonIdentifier != null)
                {
                    currentTuple.Add(item);
                }

                if (methodCall.IndexIdentifier != null)
                {
                    currentTuple.Add(i++);
                }

                methodCall.LambdaExpression.Accept(this);

                evaluation.Add(result);
            }

            ExitScope();
            return evaluation;
        }

        public IList<object> Take(MethodCall methodCall)
        {
            IEnumerable items = (IEnumerable)result;

            methodCall.Parameters[0].Accept(this);
            int max = (int)result;

            List<object> selected = new List<object>();
            int i = 0;
            foreach (object item in items)
            {
                if (i++ >= max)
                    break;

                selected.Add(item);
            }

            return selected;
        }

        public IList<object> TakeWhile(MethodCall methodCall)
        {
            if (methodCall.LambdaExpression == null)
            {
                throw new ArgumentException("Expecting a lambda expression in TakeWhile operator");
            }

            EnterScope();

            if (methodCall.AnonIdentifier != null)
            {
                sourceNames.Add(methodCall.AnonIdentifier.Text);
            }

            if (methodCall.IndexIdentifier != null)
            {
                sourceNames.Add(methodCall.IndexIdentifier.Text);
            }

            List<object> evaluation = new List<object>();

            int i = 0;
            foreach (object item in (IEnumerable)result)
            {
                currentTuple = new List<object>();

                if (methodCall.AnonIdentifier != null)
                {
                    currentTuple.Add(item);
                }

                if (methodCall.IndexIdentifier != null)
                {
                    currentTuple.Add(i++);
                }

                methodCall.LambdaExpression.Accept(this);

                if ((bool)result)
                {
                    evaluation.Add(item);
                }
                else
                {
                    break;
                }
            }

            ExitScope();
            return evaluation;
        }

        public IList<object> Skip(MethodCall methodCall)
        {
            IEnumerable items = (IEnumerable)result;

            methodCall.Parameters[0].Accept(this);
            int max = (int)result;

            List<object> selected = new List<object>();
            int i = 0;
            foreach (object item in items)
            {
                if (i++ < max)
                    continue;

                selected.Add(item);
            }

            return selected;
        }

        public IList<object> SkipWhile(MethodCall methodCall)
        {
            if (methodCall.LambdaExpression == null)
            {
                throw new ArgumentException("Expecting a lambda expression in SkipWhile operator");
            }

            EnterScope();

            if (methodCall.AnonIdentifier != null)
            {
                sourceNames.Add(methodCall.AnonIdentifier.Text);
            }

            if (methodCall.IndexIdentifier != null)
            {
                sourceNames.Add(methodCall.IndexIdentifier.Text);
            }

            List<object> evaluation = new List<object>();

            int i = 0;
            bool go = false;
            foreach (object item in (IEnumerable)result)
            {
                currentTuple = new List<object>();

                if (methodCall.AnonIdentifier != null)
                {
                    currentTuple.Add(item);
                }

                if (methodCall.IndexIdentifier != null)
                {
                    currentTuple.Add(i++);
                }

                if (!go)
                {
                    methodCall.LambdaExpression.Accept(this);

                    if ((bool)result)
                    {
                        continue;
                    }
                    else
                    {
                        go = true;
                    }

                }

                evaluation.Add(item); 
            }

            ExitScope();
            return evaluation;
        }

        public int Count(MethodCall methodCall)
        {
            if (methodCall.LambdaExpression == null)
            {
                return ((IList)result).Count;
            }

            return Where(methodCall).Count;
        }

        public IList<object> Where(MethodCall methodCall)
        {
            if (methodCall.LambdaExpression == null)
            {
                throw new ArgumentException("Expecting a lambda expression in Where operator");
            }

            EnterScope();

            if (methodCall.AnonIdentifier != null)
            {
                sourceNames.Add(methodCall.AnonIdentifier.Text);
            }

            if (methodCall.IndexIdentifier != null)
            {
                sourceNames.Add(methodCall.IndexIdentifier.Text);
            }

            List<object> evaluation = new List<object>();

            int i=0;
            foreach (object item in (IEnumerable)result)
            {
                currentTuple = new List<object>();
                
                if (methodCall.AnonIdentifier != null)
                {
                    currentTuple.Add(item);
                }

                if (methodCall.IndexIdentifier != null)
                {
                    currentTuple.Add(i++);
                }

                methodCall.LambdaExpression.Accept(this);

                if ((bool)result)
                {
                    evaluation.Add(item);
                }
            }

            ExitScope();
            return evaluation;
        }

        #endregion

    }
}
