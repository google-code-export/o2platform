using System;
using System.Collections.Generic;
using System.Text;
using Jint.Expressions;
using System.Collections;
using System.Reflection;
using Jint.Native;
using Jint.Debugger;
using System.Security;
using System.Runtime.Serialization;

namespace Jint
{
    [Serializable]
    public class ExecutionVisitor : IStatementVisitor, IJintVisitor, IDeserializationCallback
    {
        [NonSerialized]
        protected internal IFieldGetter fieldGetter;
        [NonSerialized]
        protected internal IPropertyGetter propertyGetter;
        [NonSerialized]
        protected internal IMethodInvoker methodInvoker;
        [NonSerialized]
        protected internal IConstructorInvoker constructorInvoker;
        [NonSerialized]
        protected internal ITypeResolver typeResolver;

        public IGlobal Global { get; private set; }
        public JsDictionaryObject GlobalScope { get; private set; }

        protected Stack<JsDictionaryObject> Scopes = new Stack<JsDictionaryObject>();

        protected bool exit = false;
        protected JsInstance returnInstance = null;

        public event EventHandler<DebugInformation> Step;
        public Stack<string> CallStack { get; set; }
        public Statement CurrentStatement { get; set; }

        public bool DebugMode { get; set; }

        public JsInstance Result { get; set; }
        public JsInstance Returned { get { return returnInstance; } }
        public bool AllowClr { get; set; }
        public PermissionSet PermissionSet { get; set; }
        [NonSerialized]
        private StringBuilder typeFullname = new StringBuilder();
        private string lastIdentifier = String.Empty;

        /// <summary>
        /// Use to keep the previous evaluated member to call the method on (e.g., myArray/.push/()). When
        /// evaluating the MethodCall, the latest Result is a JsFunction, and myArray is lost. Here it will be 
        /// in callTarget
        /// </summary>
        private JsDictionaryObject callTarget;

        public JsDictionaryObject CallTarget { get { return callTarget; } }

        public ExecutionVisitor(Options options)
        {
            this.methodInvoker = new CachedMethodInvoker(this);
            this.propertyGetter = new CachedReflectionPropertyGetter(methodInvoker);
            this.constructorInvoker = new CachedConstructorInvoker(methodInvoker);
            this.typeResolver = new CachedTypeResolver();
            this.fieldGetter = new CachedReflectionFieldGetter(methodInvoker);

            GlobalScope = new JsObject();
            Global = new JsGlobal(this, options);
            GlobalScope.Prototype = Global as JsDictionaryObject;
            EnterScope(GlobalScope);
            CallStack = new Stack<string>();
        }

        public void OnStep(DebugInformation info)
        {
            if (Step != null && info.CurrentStatement != null && info.CurrentStatement.Source != null)
            {
                Step(this, info);
            }
        }

        public DebugInformation CreateDebugInformation(Statement statement)
        {
            DebugInformation info = new DebugInformation();
            info.CurrentStatement = statement;
            info.CallStack = CallStack;
            info.Locals = new JsObject() { Prototype = JsUndefined.Instance };
            DebugMode = false;
            foreach (JsDictionaryObject scope in Scopes.ToArray())
            {
                foreach (var property in scope.GetKeys())
                {
                    if (!info.Locals.HasProperty(property))
                    {
                        info.Locals[property] = scope[property];
                    }
                }
            }
            DebugMode = true;

            return info;
        }

        public JsDictionaryObject CurrentScope
        {
            get { return Scopes.Peek(); }
        }

        protected void EnterScope(JsDictionaryObject scope)
        {
            Scopes.Push(scope);
        }

        protected void ExitScope()
        {
            Scopes.Pop();
        }


        public void Visit(Program program)
        {
            foreach (var statement in program.ReorderStatements())
            {
                CurrentStatement = statement;

                if (DebugMode)
                {
                    OnStep(CreateDebugInformation(statement));
                }

                statement.Accept(this);

                if (exit)
                {
                    exit = false;
                    return;
                }
            }
        }


        public void Visit(AssignmentExpression statement)
        {
            switch (statement.AssignmentOperator)
            {
                case AssignmentOperator.Assign: statement.Right.Accept(this);
                    break;
                case AssignmentOperator.Multiply: new BinaryExpression(BinaryExpressionType.Times, statement.Left, statement.Right).Accept(this);
                    break;
                case AssignmentOperator.Divide: new BinaryExpression(BinaryExpressionType.Div, statement.Left, statement.Right).Accept(this);
                    break;
                case AssignmentOperator.Modulo: new BinaryExpression(BinaryExpressionType.Modulo, statement.Left, statement.Right).Accept(this);
                    break;
                case AssignmentOperator.Add: new BinaryExpression(BinaryExpressionType.Plus, statement.Left, statement.Right).Accept(this);
                    break;
                case AssignmentOperator.Substract: new BinaryExpression(BinaryExpressionType.Minus, statement.Left, statement.Right).Accept(this);
                    break;
                case AssignmentOperator.ShiftLeft: new BinaryExpression(BinaryExpressionType.LeftShift, statement.Left, statement.Right).Accept(this);
                    break;
                case AssignmentOperator.ShiftRight: new BinaryExpression(BinaryExpressionType.RightShift, statement.Left, statement.Right).Accept(this);
                    break;
                case AssignmentOperator.UnsignedRightShift: new BinaryExpression(BinaryExpressionType.UnsignedRightShift, statement.Left, statement.Right).Accept(this);
                    break;
                case AssignmentOperator.And: new BinaryExpression(BinaryExpressionType.BitwiseAnd, statement.Left, statement.Right).Accept(this);
                    break;
                case AssignmentOperator.Or: new BinaryExpression(BinaryExpressionType.BitwiseOr, statement.Left, statement.Right).Accept(this);
                    break;
                case AssignmentOperator.XOr: new BinaryExpression(BinaryExpressionType.BitwiseXOr, statement.Left, statement.Right).Accept(this);
                    break;
                default: throw new NotSupportedException();
            }

            JsInstance right = Result;

            MemberExpression left = statement.Left as MemberExpression;
            if (left == null)
            {
                left = new MemberExpression(statement.Left, null);
            }

            Assign(left, right);

            Result = right;
        }

        public void Assign(MemberExpression left, JsInstance value)
        {
            string propertyName;
            Descriptor d = null;

            if (!(left.Member is IAssignable))
            {
                throw new JintException("The left member of an assignment must be a member");
            }

            if (left.Previous != null)
            {
                left.Previous.Accept(this);

                if (!(Result is JsDictionaryObject))
                {
                    throw new JintException("Attempt to assign to an undefined variable.");
                }
            }
            else
            {
                // resolve which CurrentScope to use
                propertyName = ((Identifier)left.Member).Text;

                Result = GlobalScope;

                foreach (JsDictionaryObject scope in Scopes.ToArray())
                {
                    if (scope != GlobalScope && scope.TryGetDescriptor(propertyName, out d))
                    {
                        Result = scope;
                        break;
                    }
                }
            }

            if (left.Member is Identifier)
            {
                propertyName = ((Identifier)left.Member).Text;
            }
            else
            {
                JsInstance temp = Result;
                Indexer indexer = left.Member as Indexer;
                indexer.Index.Accept(this);

                // The left member might be a CLR instance
                if (temp.IsClr)
                {
                    if (temp.Value.GetType().IsArray)
                    {
                        Array array = (Array)temp.Value;
                        array.SetValue(Convert.ChangeType(value.Value, array.GetType().GetElementType()), (int)Result.ToNumber());
                        return;
                    }
                    else // Search custom indexer
                    {
                        // Converts to CLR objects
                        var parameters = JsClr.ConvertParameters(Result, value);

                        PropertyInfo pi = propertyGetter.GetValue(temp.Value, "Item", parameters);

                        if (pi != null)
                        {
                            pi.GetSetMethod().Invoke(temp.Value, parameters);
                            Result = Global.ObjectClass.New(temp.Value);
                            return;
                        }
                    }
                }

                propertyName = Result.Value.ToString();

                Result = temp;
            }

            if (!(Result is JsDictionaryObject))
            {
                throw new JintException("The property is not valid in the current context: " + propertyName);
            }
            if (d == null)
                d = ((JsDictionaryObject)Result).GetDescriptor(propertyName);

            //Assigning function Name
            if (value.Class == JsFunction.TYPEOF)
                ((JsFunction)value).Name = propertyName;


            // Assignment to Clr property
            //if (Result.IsClr)
            //{
            //    var parameters = JsClr.ConvertParameters(value);
            //    var pi = propertyGetter.GetValue(Result.Value, propertyName);

            //    if (pi != null)
            //    {
            //        var setMethod = pi.GetSetMethod();
            //        methodInvoker.GetAppropriateParameters(parameters, setMethod.GetParameters(), Result.Value);
            //        setMethod.Invoke(Result.Value, parameters);
            //    }
            //    else
            //    {
            //        var fi = fieldGetter.GetValue(Result.Value, propertyName);
            //        if (fi != null)
            //        {
            //            methodInvoker.GetAppropriateParameters(parameters, new Type[] { fi.FieldType }, Result.Value);
            //            fi.SetValue(Result.Value, parameters[0]);
            //        }
            //    }
            //}

            JsDictionaryObject oldCallTarget = callTarget;
            callTarget = (JsDictionaryObject)Result;
            JsDictionaryObject target = callTarget;
            //Descriptor d = target.GetDescriptor(propertyName);

            if (d != null && (d.DescriptorType != DescriptorType.Value || (d.Owner.Class != JsScope.TYPEOF && (d.DescriptorType == DescriptorType.Value && !d.Owner.IsPrototypeOf(target))) || d.Owner.Class == JsArguments.TYPEOF))
                d.Set(target, value);
            else
            {
                if (target.Class == JsFunction.TYPEOF)
                    target = ((JsFunction)target).Scope;
                target.DefineOwnProperty(propertyName, value);
            }

            callTarget = oldCallTarget;
        }

        public void Visit(CommaOperatorStatement statement)
        {
            foreach (var s in statement.Statements)
            {
                if (DebugMode)
                {
                    OnStep(CreateDebugInformation(s));
                }

                s.Accept(this);

                if (StopStatementFlow())
                {
                    return;
                }
            }
        }

        public void Visit(BlockStatement statement)
        {
            Statement oldStatement = CurrentStatement;
            foreach (var s in statement.ReorderStatements())
            {
                CurrentStatement = s;

                if (DebugMode)
                {
                    OnStep(CreateDebugInformation(s));
                }

                s.Accept(this);

                if (StopStatementFlow())
                {
                    return;
                }
            }
            CurrentStatement = oldStatement;
        }

        protected ContinueStatement continueStatement = null;
        public void Visit(ContinueStatement statement)
        {
            continueStatement = statement;
        }

        protected BreakStatement breakStatement = null;
        public void Visit(BreakStatement statement)
        {
            breakStatement = statement;
        }

        public void Visit(DoWhileStatement statement)
        {
            JsObject scope = new JsObject();
            EnterScope(scope);
            try
            {
                do
                {
                    statement.Statement.Accept(this);

                    ResetContinueIfPresent(statement.Label);

                    if (StopStatementFlow())
                    {
                        if (breakStatement != null && statement.Label == breakStatement.Label)
                        {
                            breakStatement = null;
                        }

                        //ExitScope();
                        return;
                    }

                    statement.Condition.Accept(this);

                } while (Result.ToBoolean());
            }
            finally
            {
                ExitScope();
            }
        }

        public void Visit(EmptyStatement statement)
        {
            return;
        }

        [System.Diagnostics.DebuggerStepThrough]
        public void Visit(ExpressionStatement statement)
        {
            statement.Expression.Accept(this);
        }

        private JsDictionaryObject SetInScopes(string key, JsInstance value)
        {
            foreach (JsDictionaryObject scope in Scopes.ToArray())
            {
                Descriptor d = null;
                if (scope.TryGetDescriptor(key, out d))
                {
                    d.Set(scope, value);
                    return scope;
                }
            }

            GlobalScope[key] = value;
            return GlobalScope;
        }

        public void Visit(ForEachInStatement statement)
        {
            bool globalDeclaration = true;
            string identifier = String.Empty;

            if (statement.InitialisationStatement is VariableDeclarationStatement)
            {
                globalDeclaration = ((VariableDeclarationStatement)statement.InitialisationStatement).Global;
                identifier = ((VariableDeclarationStatement)statement.InitialisationStatement).Identifier;
            }
            else if (statement.InitialisationStatement is Identifier)
            {
                globalDeclaration = true;
                identifier = ((Identifier)statement.InitialisationStatement).Text;
            }
            else
            {
                throw new NotSupportedException("Only variable declaration are allowed in a for in loop");
            }

            statement.Expression.Accept(this);

            var dictionary = Result as JsDictionaryObject;

            if (Result.Value is IEnumerable)
            {
                foreach (object value in (IEnumerable)Result.Value)
                {
                    if (globalDeclaration)
                    {
                        SetInScopes(identifier, Global.WrapClr(value));
                    }
                    else
                    {
                        CurrentScope[identifier] = Global.WrapClr(value);
                    }

                    statement.Statement.Accept(this);

                    ResetContinueIfPresent(statement.Label);

                    if (StopStatementFlow())
                    {
                        if (breakStatement != null && statement.Label == breakStatement.Label)
                        {
                            breakStatement = null;
                        }

                        return;
                    }

                    ResetContinueIfPresent(statement.Label);
                }
            }
            else if (dictionary != null)
            {
                List<string> keys = new List<string>(dictionary.GetKeys());

                // Uses a for loop as it might be changed by the inner statements
                for (int i = 0; i < keys.Count; i++)
                {
                    string value = keys[i];

                    if (globalDeclaration)
                    {
                        SetInScopes(identifier, Global.StringClass.New(value));
                    }
                    else
                    {
                        CurrentScope[identifier] = Global.StringClass.New(value);
                    }

                    statement.Statement.Accept(this);

                    ResetContinueIfPresent(statement.Label);

                    if (StopStatementFlow())
                    {
                        if (breakStatement != null && statement.Label == breakStatement.Label)
                        {
                            breakStatement = null;
                        }

                        return;
                    }

                    ResetContinueIfPresent(statement.Label);
                }
            }
            else
            {
                throw new InvalidOperationException("The property can't be enumerated");
            }

        }

        public void Visit(WithStatement statement)
        {
            statement.Expression.Accept(this);

            if (!(Result is JsDictionaryObject))
            {
                throw new JsException(Global.StringClass.New("Invalid expression in 'with' statement"));
            }

            EnterScope((JsDictionaryObject)Result);

            try
            {
                statement.Statement.Accept(this);
            }
            finally
            {
                ExitScope();
            }
        }

        public void Visit(ForStatement statement)
        {
            if (statement.InitialisationStatement != null)
                statement.InitialisationStatement.Accept(this);

            if (statement.ConditionExpression != null)
                statement.ConditionExpression.Accept(this);
            else
                Result = Global.BooleanClass.New(true);
            while (Result.ToBoolean())
            {
                statement.Statement.Accept(this);

                ResetContinueIfPresent(statement.Label);

                if (StopStatementFlow())
                {
                    if (breakStatement != null && statement.Label == breakStatement.Label)
                    {
                        breakStatement = null;
                    }

                    return;
                }

                // Goes back in the scopes so that the variables are accessible after the statement
                if (statement.IncrementExpression != null)
                    statement.IncrementExpression.Accept(this);

                if (statement.ConditionExpression != null)
                    statement.ConditionExpression.Accept(this);
                else
                    Result = Global.BooleanClass.New(true);

            }
        }

        public JsFunction CreateFunction(IFunctionDeclaration functionDeclaration)
        {
            JsFunction f = Global.FunctionClass.New();
            f.Statement = functionDeclaration.Statement;
            f.Name = functionDeclaration.Name;
            foreach (JsDictionaryObject scope in Scopes)
            {
                f.DeclaringScopes.Add(scope);
            }

            // add a return undefined; statement at the end of each method
            BlockStatement block = (BlockStatement)f.Statement;
            if (block.Statements.Count == 0)
            {
                block.Statements.AddLast(new ReturnStatement(new PropertyExpression(JsUndefined.TYPEOF)));
            }

            f.Arguments = functionDeclaration.Parameters;
            if (HasOption(Options.Strict))
            {
                foreach (string arg in f.Arguments)
                {
                    if (arg == "eval" || arg == "arguments")
                        throw new JsException(Global.StringClass.New("The parameters do not respect strict mode"));
                }
            }

            return f;
        }

        public void Visit(FunctionDeclarationStatement statement)
        {
            JsFunction f = CreateFunction(statement);
            // Closures ?
            if (CurrentScope.Class == JsFunction.TYPEOF)
            {
                ((JsFunction)CurrentScope).Scope[statement.Name] = f;
            }
            else
            {
                //if (Scopes.Count == 2)
                GlobalScope[statement.Name] = f;
                //else
                //    CurrentScope[statement.Name] = f;
            }
        }

        public void Visit(IfStatement statement)
        {
            statement.Expression.Accept(this);
            if (Result.ToBoolean())
            {
                statement.Then.Accept(this);
            }
            else
            {
                if (statement.Else != null)
                {
                    statement.Else.Accept(this);
                }
            }
        }

        public void Visit(ReturnStatement statement)
        {
            returnInstance = null;

            if (statement.Expression != null)
            {
                statement.Expression.Accept(this);
                Return(Result);
            }

            exit = true;
        }

        public JsInstance Return(JsInstance instance)
        {
            returnInstance = instance;
            return returnInstance;
        }

        public void Visit(SwitchStatement statement)
        {
            CurrentStatement = statement.Expression;

            //statement.Expression.Accept(this);
            //JsInstance caseValue = Result;

            bool found = false;
            foreach (var clause in statement.CaseClauses)
            {
                CurrentStatement = clause.Expression;

                //clause.Expression.Accept(this);

                new BinaryExpression(BinaryExpressionType.Equal, (Expression)statement.Expression, clause.Expression).Accept(this);

                if (Result.ToBoolean())
                {
                    clause.Statements.Accept(this);

                    found = true;
                    break;
                }
            }

            if (!found)
            {
                statement.DefaultStatements.Accept(this);
            }
        }

        public void Visit(ThrowStatement statement)
        {
            Result = JsUndefined.Instance;

            if (statement.Expression != null)
            {
                statement.Expression.Accept(this);
            }

            throw new JsException(Result);
        }

        public void Visit(TryStatement statement)
        {
            EnterScope(new JsObject());

            try
            {
                statement.Statement.Accept(this);
            }
            catch (JsException jsException)
            {
                ExitScope();

                EnterScope(new JsObject());

                // handle thrown exception assignment to a local variable: catch(e)
                if (statement.Catch.Identifier != null)
                {
                    // if catch is called, Result contains the thrown value
                    Assign(new MemberExpression(new PropertyExpression(statement.Catch.Identifier), null), jsException.Value);
                }

                statement.Catch.Statement.Accept(this);
            }
            finally
            {
                ExitScope();

                if (statement.Finally != null)
                {
                    JsObject catchScope = new JsObject();
                    EnterScope(catchScope);

                    statement.Finally.Statement.Accept(this);

                    ExitScope();
                }
            }

        }

        public void Visit(VariableDeclarationStatement statement)
        {
            Result = JsUndefined.Instance;

            // if the right expression is not defined, declare the variable as undefined
            if (statement.Expression != null)
            {
                statement.Expression.Accept(this);
            }

            if (statement.Global)
            {
                SetInScopes(statement.Identifier, Result);
            }
            else
            {
                CurrentScope.DefineOwnProperty(statement.Identifier, Result);
            }

        }

        public void Visit(WhileStatement statement)
        {
            JsObject scope = new JsObject();
            EnterScope(scope);
            try
            {
                statement.Condition.Accept(this);

                while (Result.ToBoolean())
                {
                    statement.Statement.Accept(this);

                    ResetContinueIfPresent(statement.Label);

                    if (StopStatementFlow())
                    {
                        if (breakStatement != null && statement.Label == breakStatement.Label)
                        {
                            breakStatement = null;
                        }

                        return;
                    }

                    statement.Condition.Accept(this);
                }
            }
            finally
            {
                ExitScope();
            }
        }

        public void Visit(NewExpression expression)
        {
            int scopes = this.Scopes.Count;
            foreach (var property in expression.Identifiers)
            {
                property.Accept(this);
                if (Result == JsUndefined.Instance)
                {
                    break;
                }
                EnterScope((JsDictionaryObject)Result);
            }
            while (scopes < this.Scopes.Count)
            {
                ExitScope();
            }

            if (Result != null && Result.Class == JsFunction.TYPEOF)
            {
                JsFunction function = (JsFunction)Result;

                // Process parameters
                JsInstance[] parameters = new JsInstance[expression.Arguments.Count];

                for (int i = 0; i < expression.Arguments.Count; i++)
                {
                    expression.Arguments[i].Accept(this);
                    parameters[i] = Result;
                }

                // Calls the constructor on a brand new object
                JsObject instance = new JsObject();
                instance.Prototype = function.Prototype;

                // Once 'new' is called, the result is the new instance, given by the Execute() method on the proper constructor
                ExecuteFunction(function, instance, parameters);

                return;
            }

            if (Result.IsClr && Result.Value is Type)
            {
                Type type = (Type)Result.Value;

                object[] parameters = new object[expression.Arguments.Count];

                for (int i = 0; i < expression.Arguments.Count; i++)
                {
                    expression.Arguments[i].Accept(this);
                    parameters[i] = JsClr.ConvertParameter(Result);
                }

                ConstructorInfo constructor = null;

                constructor = constructorInvoker.Invoke(type, parameters);

                if (constructor == null)
                {
                    // Struct don't reflect their default constructor
                    if (type.IsValueType)
                    {
                        PermissionSet.PermitOnly();

                        try
                        {
                            Result = Global.WrapClr(Activator.CreateInstance(type));
                        }
                        finally
                        {
                            CodeAccessPermission.RevertPermitOnly();
                        }
                    }
                    else
                    {
                        throw new JintException("Matching constructor not found for: " + type.Name);
                    }
                }

                PermissionSet.PermitOnly();

                try
                {
                    Result = Global.WrapClr(constructor.Invoke(parameters));
                }
                finally
                {
                    CodeAccessPermission.RevertPermitOnly();
                }
            }

            // Try to get identifiers as a CLR type
            if (Result == JsUndefined.Instance)
            {
                EnsureClrAllowed();

                // Process parameters
                object[] parameters = new object[expression.Arguments.Count];

                for (int i = 0; i < expression.Arguments.Count; i++)
                {
                    expression.Arguments[i].Accept(this);
                    parameters[i] = JsClr.ConvertParameter(Result);
                }

                StringBuilder typeBuilder = new StringBuilder();
                foreach (var property in expression.Identifiers)
                {
                    typeBuilder.Append(property.Text).Append(".");
                }

                typeBuilder.Remove(typeBuilder.Length - 1, 1);

                if (expression.Generics.Count > 0)
                {
                    List<string> types = new List<string>();
                    foreach (Expression generic in expression.Generics)
                    {
                        generic.Accept(this);

                        if (!(Result.Value is Type))
                        {
                            throw new JintException("Invalid generic type");
                        }

                        types.Add(Result.Value.ToString());
                    }
                    typeBuilder.Append("`").Append(types.Count);
                    typeBuilder.Append("[");
                    typeBuilder.Append(String.Join(",", types.ToArray()));
                    typeBuilder.Append("]");
                }

                string typeName = typeBuilder.ToString();
                Type type = typeResolver.ResolveType(typeName);

                if (type == null)
                {
                    throw new JintException("Unknown type: " + typeName);
                }

                ConstructorInfo constructor = null;

                constructor = constructorInvoker.Invoke(type, parameters);

                if (constructor == null)
                {
                    // Struct don't reflect their default constructor
                    if (type.IsValueType)
                    {
                        PermissionSet.PermitOnly();

                        try
                        {
                            Result = Global.WrapClr(Activator.CreateInstance(type));
                        }
                        finally
                        {
                            CodeAccessPermission.RevertPermitOnly();
                        }
                    }
                    else
                    {
                        throw new JintException("Matching constructor not found for: " + typeName);
                    }
                }
                else
                {
                    PermissionSet.PermitOnly();

                    try
                    {
                        Result = Global.WrapClr(constructor.Invoke(parameters));
                    }
                    finally
                    {
                        CodeAccessPermission.RevertPermitOnly();
                    }
                }
            }
        }

        public void Visit(TernaryExpression expression)
        {
            Result = null;

            // Evaluates the left expression and saves the value
            expression.LeftExpression.Accept(this);
            JsInstance left = (JsInstance)Result;

            Result = null;

            if (left.ToBoolean())
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

        public void Visit(BinaryExpression expression)
        {
            JsInstance old = Result;

            // Evaluates the left expression and saves the value
            expression.LeftExpression.Accept(this);
            JsInstance left = Result;

            //prevents execution of the right hand side if false
            if (expression.Type == BinaryExpressionType.And && !left.ToBoolean())
            {
                Result = left;
                return;
            }

            //prevents execution of the right hand side if true
            if (expression.Type == BinaryExpressionType.Or && left.ToBoolean())
            {
                Result = left;
                return;
            }

            Result = null;

            // Evaluates the right expression and saves the value
            expression.RightExpression.Accept(this);
            JsInstance right = Result;

            Result = old;

            switch (expression.Type)
            {
                case BinaryExpressionType.And:

                    if (left.ToBoolean())
                    {
                        Result = right;
                    }
                    else
                    {
                        Result = JsBoolean.False;
                    }

                    break;

                case BinaryExpressionType.Or:
                    if (left.ToBoolean())
                    {
                        Result = left;
                    }
                    else
                    {
                        Result = right;
                    }

                    break;

                case BinaryExpressionType.Div:
                    var rightNumber = right.ToNumber();
                    var leftNumber = left.ToNumber();

                    if (right == Global.NumberClass["NEGATIVE_INFINITY"] || right == Global.NumberClass["POSITIVE_INFINITY"])
                    {
                        Result = new JsNumber(0);
                    }
                    else if (rightNumber == 0)
                    {
                        Result = leftNumber > 0 ? Global.NumberClass["POSITIVE_INFINITY"] : Global.NumberClass["NEGATIVE_INFINITY"];
                    }
                    else
                    {
                        Result = Global.NumberClass.New(leftNumber / rightNumber);
                    }
                    break;

                case BinaryExpressionType.Equal:
                    if (left == JsUndefined.Instance && right == JsUndefined.Instance || left == JsNull.Instance && right == JsNull.Instance)
                    {
                        Result = JsBoolean.True;
                    }
                    else
                    {
                        if (left == JsUndefined.Instance && right != JsUndefined.Instance || left == JsNull.Instance && right != JsNull.Instance)
                        {
                            Result = JsBoolean.False;
                        }
                        else
                            if (left != JsUndefined.Instance && right == JsUndefined.Instance || left != JsNull.Instance && right == JsNull.Instance)
                            {
                                Result = JsBoolean.False;
                            }
                            else
                            {
                                if (left.Class == JsNumber.TYPEOF || left.Class == JsBoolean.TYPEOF ||
                                    right.Class == JsNumber.TYPEOF || right.Class == JsBoolean.TYPEOF)
                                {
                                    Result = Global.BooleanClass.New(left.ToNumber() == right.ToNumber());
                                }
                                else if (left.Class == JsString.TYPEOF || right.Class == JsString.TYPEOF)
                                {
                                    Result = Global.BooleanClass.New(left.ToString() == right.ToString());
                                }
                                else if (left.Value != null)
                                {
                                    Result = Global.BooleanClass.New(left.Value.Equals(right.Value));
                                }
                                else
                                {
                                    Result = Global.BooleanClass.New(left == right);
                                }
                            }

                    }
                    break;

                case BinaryExpressionType.Greater:
                    // Use the type of the left operand to make the comparison
                    Result = Global.BooleanClass.New(left.ToNumber() > right.ToNumber());
                    break;

                case BinaryExpressionType.GreaterOrEqual:
                    // Use the type of the left operand to make the comparison
                    Result = Global.BooleanClass.New(left.ToNumber() >= right.ToNumber());
                    break;

                case BinaryExpressionType.Lesser:
                    // Use the type of the left operand to make the comparison
                    Result = Global.BooleanClass.New(left.ToNumber() < right.ToNumber());
                    break;

                case BinaryExpressionType.LesserOrEqual:
                    // Use the type of the left operand to make the comparison
                    Result = Global.BooleanClass.New(left.ToNumber() <= right.ToNumber());
                    break;

                case BinaryExpressionType.Minus:
                    Result = Global.NumberClass.New(left.ToNumber() - right.ToNumber());
                    break;

                case BinaryExpressionType.Modulo:
                    if (right == Global.NumberClass["NEGATIVE_INFINITY"] || right == Global.NumberClass["POSITIVE_INFINITY"])
                    {
                        Result = Global.NumberClass["POSITIVE_INFINITY"];
                    }
                    else if (right.ToNumber() == 0)
                    {
                        Result = Global.NumberClass["NaN"];
                    }
                    else
                    {
                        Result = Global.NumberClass.New(left.ToNumber() % right.ToNumber());
                    }
                    break;

                case BinaryExpressionType.NotEqual:

                    if (left == JsUndefined.Instance && right == JsUndefined.Instance || left == JsNull.Instance && right == JsNull.Instance)
                    {
                        Result = JsBoolean.False;
                    }
                    else
                    {
                        if (left == JsUndefined.Instance && right != JsUndefined.Instance || left == JsNull.Instance && right != JsNull.Instance)
                        {
                            Result = JsBoolean.True;
                        }
                        else
                            if (left != JsUndefined.Instance && right == JsUndefined.Instance || left != JsNull.Instance && right == JsNull.Instance)
                            {
                                Result = JsBoolean.True;
                            }
                            else
                            {
                                Result = Global.BooleanClass.New(!left.Value.Equals(right.Value));
                            }
                    }
                    break;

                case BinaryExpressionType.Plus:
                    if (left.Class == JsString.TYPEOF || right.Class == JsString.TYPEOF)
                    {
                        Result = Global.StringClass.New(String.Concat(left.ToString(), right.ToString()));
                    }
                    else
                    {
                        Result = Global.NumberClass.New(left.ToNumber() + right.ToNumber());
                    }
                    break;

                case BinaryExpressionType.Times:
                    Result = Global.NumberClass.New(left.ToNumber() * right.ToNumber());
                    break;

                case BinaryExpressionType.Pow:
                    Result = Global.NumberClass.New(Math.Pow(left.ToNumber(), right.ToNumber()));
                    break;

                case BinaryExpressionType.BitwiseAnd:
                    Result = Global.NumberClass.New(Convert.ToUInt64(left.ToNumber()) & Convert.ToUInt64(right.ToNumber()));
                    break;

                case BinaryExpressionType.BitwiseOr:
                    Result = Global.NumberClass.New(Convert.ToUInt64(left.ToNumber()) | Convert.ToUInt64(right.ToNumber()));
                    break;

                case BinaryExpressionType.BitwiseXOr:
                    Result = Global.NumberClass.New(Convert.ToUInt64(left.ToNumber()) ^ Convert.ToUInt64(right.ToNumber()));
                    break;

                case BinaryExpressionType.Same:
                    // 11.9.6 The Strict Equality Comparison Algorithm
                    if (left.Class != right.Class)
                    {
                        Result = JsBoolean.False;
                    }
                    else if (left.Class == JsUndefined.TYPEOF)
                    {
                        Result = JsBoolean.True;
                    }
                    else if (left.Class == JsNull.TYPEOF)
                    {
                        Result = JsBoolean.True;
                    }
                    else if (left.Class == JsNumber.TYPEOF)
                    {
                        if (left == Global.NumberClass["NaN"] || right == Global.NumberClass["NaN"])
                        {
                            Result = JsBoolean.False;
                        }
                        else if (left.ToNumber() == right.ToNumber())
                        {
                            Result = JsBoolean.True;
                        }
                        else
                            Result = JsBoolean.False;
                    }
                    else if (left.Class == JsString.TYPEOF)
                    {
                        Result = new JsBoolean(left.ToString() == right.ToString());
                    }
                    else if (left.Class == JsBoolean.TYPEOF)
                    {
                        Result = new JsBoolean(left.ToBoolean() == right.ToBoolean());
                    }
                    else if (left == right)
                    {
                        Result = JsBoolean.True;
                    }
                    else
                    {
                        Result = JsBoolean.False;
                    }

                    break;

                case BinaryExpressionType.NotSame:
                    new BinaryExpression(BinaryExpressionType.Same, expression.LeftExpression, expression.RightExpression).Accept(this);
                    Result = new JsBoolean(!Result.ToBoolean());
                    break;

                case BinaryExpressionType.LeftShift:
                    Result = Global.NumberClass.New(Convert.ToUInt64(left.ToNumber()) << Convert.ToUInt16(right.ToNumber()));
                    break;

                case BinaryExpressionType.RightShift:
                    Result = Global.NumberClass.New(Convert.ToUInt64(left.ToNumber()) >> Convert.ToUInt16(right.ToNumber()));
                    break;

                case BinaryExpressionType.UnsignedRightShift:
                    Result = Global.NumberClass.New(Convert.ToUInt64(left.ToNumber()) >> Convert.ToUInt16(right.ToNumber()));
                    break;

                case BinaryExpressionType.InstanceOf:
                    Result = new JsBoolean(left.Class == right.ToString());
                    break;

                case BinaryExpressionType.In:
                    if (right is JsDictionaryObject)
                    {
                        ((JsDictionaryObject)right).HasProperty(left);
                    }
                    else
                    {
                        throw new JintException("Cannot apply 'in' operator to the specified member.");
                    }

                    break;

                default:
                    throw new NotSupportedException("Unkown binary operator");
            }
        }

        public void Visit(UnaryExpression expression)
        {
            MemberExpression member;

            switch (expression.Type)
            {
                case UnaryExpressionType.TypeOf:

                    expression.Expression.Accept(this);
                    Result = Global.StringClass.New(Result.Class.ToLower());

                    break;

                case UnaryExpressionType.Not:
                    expression.Expression.Accept(this);
                    Result = Global.BooleanClass.New(!Result.ToBoolean());
                    break;

                case UnaryExpressionType.Negate:
                    expression.Expression.Accept(this);
                    Result = Global.NumberClass.New(-Result.ToNumber());
                    break;

                case UnaryExpressionType.Positive:
                    expression.Expression.Accept(this);
                    Result = Global.NumberClass.New(+Result.ToNumber());
                    break;

                case UnaryExpressionType.PostfixPlusPlus:

                    expression.Expression.Accept(this);
                    JsInstance temp = Result;

                    member = expression.Expression as MemberExpression ?? new MemberExpression(expression.Expression, null);

                    Assign(member, Global.NumberClass.New(temp.ToNumber() + 1));

                    Result = temp;
                    break;

                case UnaryExpressionType.PostfixMinusMinus:

                    expression.Expression.Accept(this);
                    temp = Result;

                    member = expression.Expression as MemberExpression ?? new MemberExpression(expression.Expression, null);

                    Assign(member, Global.NumberClass.New(temp.ToNumber() - 1));

                    Result = temp;
                    break;

                case UnaryExpressionType.PrefixPlusPlus:

                    expression.Expression.Accept(this);
                    temp = Global.NumberClass.New(Result.ToNumber() + 1);

                    member = expression.Expression as MemberExpression ?? new MemberExpression(expression.Expression, null);
                    Assign(member, temp);

                    Result = temp;
                    break;

                case UnaryExpressionType.PrefixMinusMinus:

                    expression.Expression.Accept(this);
                    temp = Global.NumberClass.New(Result.ToNumber() - 1);

                    member = expression.Expression as MemberExpression ?? new MemberExpression(expression.Expression, null);
                    Assign(member, temp);

                    Result = temp;
                    break;

                case UnaryExpressionType.Delete:

                    member = expression.Expression as MemberExpression;
                    if (member == null)
                        throw new NotImplementedException("delete");
                    member.Previous.Accept(this);
                    temp = Result;
                    string propertyName = null;
                    if (member.Member is PropertyExpression)
                        propertyName = ((PropertyExpression)member.Member).Text;
                    if (member.Member is Indexer)
                    {
                        ((Indexer)member.Member).Index.Accept(this);
                        propertyName = Result.ToString();
                    }
                    if (string.IsNullOrEmpty(propertyName))
                        throw new JsException(Global.TypeErrorClass.New());
                    try
                    {
                        ((JsDictionaryObject)temp).Delete(propertyName);
                    }
                    catch (JintException e)
                    {
                        throw new JsException(Global.TypeErrorClass.New());
                    }
                    Result = temp;
                    break;

                case UnaryExpressionType.Void:

                    throw new NotImplementedException("void");

                case UnaryExpressionType.Inv:

                    expression.Expression.Accept(this);
                    Result = Global.NumberClass.New(~Convert.ToUInt64(Result.ToNumber()));

                    break;

            }
        }

        public void Visit(ValueExpression expression)
        {
            switch (expression.TypeCode)
            {
                case TypeCode.Boolean: Result = Global.BooleanClass.New((bool)expression.Value); break;
                case TypeCode.Int32:
                case TypeCode.Single:
                case TypeCode.Double: Result = Global.NumberClass.New(Convert.ToDouble(expression.Value)); break;
                case TypeCode.String: Result = Global.StringClass.New((string)expression.Value); break;
                default: Result = expression.Value as JsInstance;
                    break;
            }
        }

        public void Visit(FunctionExpression fe)
        {
            Result = CreateFunction(fe);
        }

        public void Visit(Statement expression)
        {
            throw new NotImplementedException();
        }

        public void Visit(MemberExpression expression)
        {
            bool enterScope = false;
            if (expression.Previous != null)
            {
                expression.Previous.Accept(this);

                enterScope = Result is JsDictionaryObject;// && !(Result is JsCallFunction); // && !(Result is JsFunction && CurrentScope is JsFunction); // if a function inside a function (closure) then don't enter scope
                //if (Result == JsUndefined.Instance && System.Diagnostics.Debugger.IsAttached)
                //    System.Diagnostics.Debugger.Break();
                if (enterScope)
                {
                    EnterScope((JsDictionaryObject)Result);
                }
            }
            try
            {
                expression.Member.Accept(this);

                callTarget = null;

                #region Retain member if result is a function
                if (Result != null && Result.Class == JsFunction.TYPEOF)
                {
                    callTarget = CurrentScope;
                }

                if (Result != null && Result.Class == JsClrMethodInfo.TYPEOF)
                {
                    callTarget = CurrentScope;
                }
                #endregion

                // Try to evaluate a CLR type
                if (Result == JsUndefined.Instance && typeFullname.Length > 0)
                {
                    EnsureClrAllowed();

                    Type type = typeResolver.ResolveType(typeFullname.ToString());

                    if (type != null)
                    {
                        Result = Global.WrapClr(type);
                    }
                }
            }
            finally
            {
                if (enterScope)
                {
                    ExitScope();
                }
            }
        }

        public void Visit(Indexer indexer)
        {
            JsDictionaryObject temp = (JsDictionaryObject)Result;

            JsDictionaryObject currentScope = CurrentScope;

            if (currentScope.IsClr)
                ExitScope();
            try
            {
                indexer.Index.Accept(this);
            }
            finally
            {
                if (currentScope.IsClr)
                    EnterScope(currentScope);
            }

            if (temp.IsClr) // && ((JsValue)Result).Type == JsValueType.CLRObject)
            {
                EnsureClrAllowed();

                PermissionSet.PermitOnly();

                try
                {
                    if (temp.Value.GetType().IsArray)
                    {
                        Result = Global.ObjectClass.New(((Array)temp.Value).GetValue((int)Result.ToNumber()));
                        return;
                    }
                    else
                    {
                        var parameters = JsClr.ConvertParameters(Result);

                        PropertyInfo pi = propertyGetter.GetValue(temp.Value, "Item", parameters);

                        if (pi != null)
                        {
                            Result = Global.WrapClr(pi.GetValue(temp.Value, parameters));
                            return;
                        }
                        else
                        {
                            pi = propertyGetter.GetValue(temp.Value, Result.ToString());

                            if (pi != null)
                            {
                                Result = Global.WrapClr(pi.GetValue(temp.Value, null));
                                return;
                            }

                            FieldInfo fi = fieldGetter.GetValue(temp.Value, Result.ToString());

                            if (fi != null)
                            {
                                Result = Global.WrapClr(fi.GetValue(temp.Value));
                                return;
                            }
                            else
                            {
                                throw new JintException("Index not found: " + Result.ToString());
                            }
                        }
                    }
                }
                finally
                {
                    CodeAccessPermission.RevertPermitOnly();
                }
            }

            if (temp.Class == JsString.TYPEOF)
            {
                Result = Global.StringClass.New(temp.ToString()[Convert.ToInt32(Result.ToNumber())].ToString());
            }
            else
            {
                if (temp.Class == JsFunction.TYPEOF)
                    Result = ((JsFunction)temp).Scope[Result.ToString()];
                else
                    Result = temp[Result.ToString()];
            }
        }

        public void Visit(MethodCall methodCall)
        {
            if (Result == JsUndefined.Instance || Result == null)
            {
                if (!String.IsNullOrEmpty(lastIdentifier))
                {
                    throw new JsException(Global.TypeErrorClass.New("Object expected: " + lastIdentifier));
                }
                else
                {
                    throw new JsException(Global.TypeErrorClass.New("Object expected"));
                }
            }

            #region Evaluates parameters
            JsInstance[] parameters = new JsInstance[methodCall.Arguments.Count];
            Type[] types = new Type[methodCall.Arguments.Count];

            if (methodCall.Arguments.Count > 0)
            {
                JsInstance oldResult = Result;
                JsDictionaryObject oldCallTarget = callTarget;
                callTarget = null;
                JsDictionaryObject currentScope = CurrentScope;
                if (currentScope.Class != JsScope.TYPEOF)
                    ExitScope();
                try
                {
                    for (int j = 0; j < methodCall.Arguments.Count; j++)
                    {
                        methodCall.Arguments[j].Accept(this);
                        parameters[j] = Result;
                    }
                }
                finally
                {
                    if (currentScope.Class != JsScope.TYPEOF)
                        EnterScope(currentScope);
                }
                Result = oldResult;
                callTarget = oldCallTarget;
            }
            #endregion

            if (Result.Class == JsFunction.TYPEOF)
            {
                JsFunction function = (JsFunction)Result;

                if (DebugMode)
                {
                    string stack = function.Name + "(";
                    string[] paramStrings = new string[parameters.Length];

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        if (parameters[i] != null)
                            paramStrings[i] = parameters[i].ToSource();
                        else
                            paramStrings[i] = "null";
                    }

                    stack += String.Join(", ", paramStrings);
                    stack += ")";
                    CallStack.Push(stack);
                }

                returnInstance = JsUndefined.Instance;

                ExecuteFunction(function, callTarget, parameters);

                if (DebugMode)
                {
                    CallStack.Pop();
                }

                Result = returnInstance;
                return;
            }

            if (Result.Class == JsClrMethodInfo.TYPEOF)
            {
                // Fallback to CLR methods
                object result = null;

                EnsureClrAllowed();

                #region Converts parameters
                object[] clrParameters = JsClr.ConvertParameters(parameters);
                #endregion

                JsClrMethodInfo clrMethod = (JsClrMethodInfo)Result;
                try
                {

                    List<Type> generics = new List<Type>();
                    if (methodCall.Generics.Count > 0)
                    {
                        JsDictionaryObject oldCallTarget = callTarget;
                        foreach (Expression generic in methodCall.Generics)
                        {
                            generic.Accept(this);

                            if (!(Result.Value is Type))
                            {
                                PermissionSet.PermitOnly();
                                throw new JintException("Invalid type in generics specifier");
                            }

                            generics.Add((Type)Result.Value);
                        }
                        callTarget = oldCallTarget;
                    }

                    MethodInfo methodInfo = methodInvoker.Invoke(callTarget.Value, clrMethod.Value.ToString(), clrParameters, generics.ToArray());

                    PermissionSet.PermitOnly();

                    if (methodInfo == null)
                    {
                        throw new JintException("Method not found with specified arguments: " + clrMethod.Value.ToString());
                    }

                    try
                    {
                        result = methodInfo.Invoke(callTarget.Value, clrParameters);
                    }
                    catch (Exception e)
                    {
                        if (e.InnerException is JsException)
                        {
                            throw e.InnerException;
                        }

                        throw;
                    }
                }
                catch (TargetInvocationException e)
                {
                    // Extract SecurityExpression if thrown
                    throw e.InnerException;
                }
                finally
                {
                    CodeAccessPermission.RevertPermitOnly();
                }

                Result = Global.WrapClr(result);
            }

        }

        public void ExecuteFunction(JsFunction function, JsDictionaryObject that, JsInstance[] parameters)
        {
            if (function == null)
            {
                return;
            }

            JsScope functionScope = new JsScope();
            JsArguments args = new JsArguments(Global, function, parameters);
            functionScope.Prototype = args;
            if (HasOption(Options.Strict))
                functionScope.DefineOwnProperty(JsInstance.ARGUMENTS, args);
            else
                functionScope.Prototype.DefineOwnProperty(JsInstance.ARGUMENTS, args);

            if (that != null)
                functionScope.DefineOwnProperty(JsInstance.THIS, that);
            functionScope.Extensible = false;

            //for (int i = function.DeclaringScopes.Count - 1; i >= 0; i--)
            //{
            //    EnterScope(function.DeclaringScopes[i]);
            //}

            EnterScope(function);
            EnterScope(functionScope);

            try
            {
                PermissionSet.PermitOnly();

                Result = function.Execute(this, that, parameters);

                // Resets the return flag
                if (exit)
                {
                    exit = false;
                }
            }
            finally
            {
                ExitScope();
                ExitScope();
                CodeAccessPermission.RevertPermitOnly();
            }
        }

        private bool HasOption(Options options)
        {
            return Global.HasOption(options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="function">Function to exectue</param>
        /// <param name="that">Object to call the function on</param>
        /// <param name="parameters">Parameters of the execution</param>
        public void CallFunction(JsFunction function, JsDictionaryObject that, JsInstance[] parameters)
        {
            function.Statement.Accept(this);
        }

        public void Visit(PropertyExpression expression)
        {

            Result = null;

            string propertyName = lastIdentifier = expression.Text;

            if (propertyName == JsDictionaryObject.PROTOTYPE)
            {
                Result = CurrentScope.Prototype;
                return;
            }

            JsInstance result = null;

            JsDictionaryObject oldCallTarget = callTarget;
            callTarget = CurrentScope;
            try
            {// Closure ?
                callTarget = CurrentScope;
                if (CurrentScope.Class == JsFunction.TYPEOF)
                {
                    JsScope scope = ((JsFunction)CurrentScope).Scope;
                    if (scope.TryGetProperty(propertyName, out result))
                    {
                        Result = result;
                        return;
                    }
                }

                callTarget = CurrentScope;
                if (CurrentScope.TryGetProperty(propertyName, out result))
                {
                    Result = result;
                    return;
                }
            }
            finally
            {
                callTarget = oldCallTarget;
            }


            // Search for .NET property or method
            if (CurrentScope.IsClr && CurrentScope.Value != null)
            {
                EnsureClrAllowed();

                // enum ?
                var type = CurrentScope.Value as Type;
                if (type != null && type.IsEnum)
                {
                    Result = new JsClr(this, Enum.Parse(type, propertyName));
                    return;
                }

                var propertyInfo = propertyGetter.GetValue(CurrentScope.Value, propertyName);
                if (propertyInfo != null)
                {
                    Result = Global.WrapClr(propertyInfo.GetValue(CurrentScope.Value, null));
                    return;
                }


                var fieldInfo = fieldGetter.GetValue(CurrentScope.Value, propertyName);
                if (fieldInfo != null)
                {
                    Result = Global.WrapClr(fieldInfo.GetValue(CurrentScope.Value));
                    return;
                }

                // Not a property, then must be a method
                Result = new JsClrMethodInfo(propertyName);
                return;

                throw new JintException("Invalid property name: " + propertyName);
            }


            // Search for a static CLR call
            if (Result == null && typeFullname.Length > 0)
            {
                Type type = typeResolver.ResolveType(typeFullname.ToString());

                if (type != null)
                {
                    EnsureClrAllowed();

                    var propertyInfo = propertyGetter.GetValue(type, propertyName);
                    if (propertyInfo != null)
                    {
                        Result = Global.WrapClr(propertyInfo.GetValue(type, null));
                        return;
                    }

                    var fieldInfo = fieldGetter.GetValue(type, propertyName);
                    if (fieldInfo != null)
                    {
                        Result = Global.WrapClr(fieldInfo.GetValue(type));
                        return;
                    }

                    // Not a property, then must be a method
                    Result = new JsClrMethodInfo(propertyName);
                    return;

                    throw new JintException("Invalid property name: " + propertyName);
                }
            }

            if (Result == null && typeFullname.Length > 0)
            {
                typeFullname.Append('.').Append(propertyName);
            }

            Result = JsUndefined.Instance;
        }

        public void Visit(PropertyDeclarationExpression expression)
        {
            switch (expression.Mode)
            {
                case PropertyExpressionType.Data:
                    expression.Expression.Accept(this);
                    Result = new ValueDescriptor(CurrentScope, expression.Name, Result);
                    break;
                case PropertyExpressionType.Get:
                case PropertyExpressionType.Set:
                    JsFunction get = null, set = null;
                    if (expression.GetExpression != null)
                    {
                        expression.GetExpression.Accept(this);
                        get = (JsFunction)Result;
                    }
                    if (expression.SetExpression != null)
                    {
                        expression.SetExpression.Accept(this);
                        set = (JsFunction)Result;
                    }
                    Result = new PropertyDescriptor(Global, CurrentScope, expression.Name) { GetFunction = get, SetFunction = set, Enumerable = true };
                    break;
                default:
                    break;
            }
        }

        public void Visit(Identifier expression)
        {
            Result = null;

            string propertyName = lastIdentifier = expression.Text;


            // Search for .NET property or method
            if (CurrentScope.IsClr && CurrentScope.Value != null)
            {
                EnsureClrAllowed();

                var propertyInfo = propertyGetter.GetValue(CurrentScope.Value, propertyName);
                if (propertyInfo != null)
                {
                    Result = Global.WrapClr(propertyInfo.GetValue(CurrentScope.Value, null));
                    return;
                }

                var fieldInfo = fieldGetter.GetValue(CurrentScope.Value, propertyName);
                if (fieldInfo != null)
                {
                    Result = Global.WrapClr(fieldInfo.GetValue(CurrentScope.Value));
                    return;
                }

                // Not a property, then must be a method
                Result = new JsClrMethodInfo(propertyName);
                return;

                throw new JintException("Invalid property name: " + propertyName);
            }

            // escalade scopes
            JsDictionaryObject oldCallTarget = callTarget;
            try
            {
                foreach (JsDictionaryObject scope in Scopes.ToArray())
                {
                    callTarget = scope;
                    JsInstance result = null;
                    if (scope.TryGetProperty(propertyName, out result))
                    {
                        Result = result;
                        if (Result != null)
                            return;
                    }
                }
            }
            finally
            {
                callTarget = oldCallTarget;
            }

            if (propertyName == "null")
            {
                Result = JsNull.Instance;
                return;
            }

            if (propertyName == "undefined")
            {
                Result = JsUndefined.Instance;
                return;
            }

            // Try to record full path in case it's a type
            if (Result == null)
            {
                typeFullname.Append(propertyName);
            }

            Result = JsUndefined.Instance;
        }

        private void EnsureClrAllowed()
        {
            if (!AllowClr)
            {
                throw new SecurityException("Use of Clr is not allowed");
            }
        }

        public void Visit(JsonExpression json)
        {
            JsObject instance = Global.ObjectClass.New();

            JsScope scope = new JsScope() { Prototype = CurrentScope };
            scope.DefineOwnProperty(JsInstance.THIS, instance);
            EnterScope(scope);
            try
            {
                foreach (var item in json.Values)
                {
                    item.Value.Accept(this);
                    instance.DefineOwnProperty(item.Key, Result);
                }
            }
            finally
            {
                ExitScope();
            }

            Result = instance;
        }

        /// <summary>
        /// Called by a loop to stop the "continue" keyword escalation
        /// </summary>
        protected void ResetContinueIfPresent(string label)
        {
            if (continueStatement != null && continueStatement.Label == label)
            {
                continueStatement = null;
            }
        }

        protected bool StopStatementFlow()
        {
            return exit ||
            breakStatement != null ||
            continueStatement != null;
        }

        public void Visit(ArrayDeclaration expression)
        {
            var array = Global.ArrayClass.New();

            // Process parameters
            JsInstance[] parameters = new JsInstance[expression.Parameters.Count];

            for (int i = 0; i < expression.Parameters.Count; i++)
            {
                expression.Parameters[i].Accept(this);
                array[i.ToString()] = Result;
            }

            Result = array;
        }

        public void Visit(RegexpExpression expression)
        {
            Result = Global.RegExpClass.New(expression.Regexp, expression.Options.Contains("g"), expression.Options.Contains("i"), expression.Options.Contains("m"));
        }


        #region IDeserializationCallback Members

        public void OnDeserialization(object sender)
        {
            this.methodInvoker = new CachedMethodInvoker(this);
            this.propertyGetter = new CachedReflectionPropertyGetter(methodInvoker);
            this.constructorInvoker = new CachedConstructorInvoker(methodInvoker);
            this.typeResolver = new CachedTypeResolver();
            this.fieldGetter = new CachedReflectionFieldGetter(methodInvoker);
        }

        #endregion

        #region IJintVisitor Members

        public IPropertyGetter PropertyGetter
        {
            get { return propertyGetter; }
        }

        public IMethodInvoker MethodGetter
        {
            get { return methodInvoker; }
        }

        public IFieldGetter FieldGetter
        {
            get { return fieldGetter; }
        }

        #endregion
    }
}
