﻿using System;
using System.Collections.Generic;
using System.Text;
using Jint.Expressions;
using System.Threading;
using System.Diagnostics;
using Antlr.Runtime;
using Jint.Native;
using Jint.Delegates;
using Jint.Debugger;
using System.Security;
using System.Security.Permissions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Jint.Delegates
{
    public delegate void Action();
    public delegate void Action<T1, T2>(T1 t1, T2 t2);
    public delegate void Action<T1, T2, T3>(T1 t1, T2 t2, T3 t3);

    public delegate TResult Func<TResult>();
    public delegate TResult Func<T, TResult>(T t);
    public delegate TResult Func<T1, T2, TResult>(T1 t1, T2 t2);
    public delegate TResult Func<T1, T2, T3, TResult>(T1 t1, T2 t2, T3 t3);
    public delegate TResult Func<T1, T2, T3, T4, TResult>(T1 t1, T2 t2, T3 t3, T4 t4);
}

namespace Jint
{
    [Serializable]
    public class JintEngine
    {
        protected ExecutionVisitor visitor;

        [System.Diagnostics.DebuggerStepThrough]
        public JintEngine()
            : this(Options.Ecmascript5 | Options.Strict)
        {
        }

        [System.Diagnostics.DebuggerStepThrough]
        public JintEngine(Options options)
        {
            visitor = new ExecutionVisitor(options);
            AllowClr = true;
            permissionSet = new PermissionSet(PermissionState.None);

            visitor.GlobalScope.Prototype["ToBoolean"] = visitor.Global.FunctionClass.New(new Func<object, Boolean>(Convert.ToBoolean));
            visitor.GlobalScope.Prototype["ToByte"] = visitor.Global.FunctionClass.New(new Func<object, Byte>(Convert.ToByte));
            visitor.GlobalScope.Prototype["ToChar"] = visitor.Global.FunctionClass.New(new Func<object, Char>(Convert.ToChar));
            visitor.GlobalScope.Prototype["ToDateTime"] = visitor.Global.FunctionClass.New(new Func<object, DateTime>(Convert.ToDateTime));
            visitor.GlobalScope.Prototype["ToDecimal"] = visitor.Global.FunctionClass.New(new Func<object, Decimal>(Convert.ToDecimal));
            visitor.GlobalScope.Prototype["ToDouble"] = visitor.Global.FunctionClass.New(new Func<object, Double>(Convert.ToDouble));
            visitor.GlobalScope.Prototype["ToInt16"] = visitor.Global.FunctionClass.New(new Func<object, Int16>(Convert.ToInt16));
            visitor.GlobalScope.Prototype["ToInt32"] = visitor.Global.FunctionClass.New(new Func<object, Int32>(Convert.ToInt32));
            visitor.GlobalScope.Prototype["ToInt64"] = visitor.Global.FunctionClass.New(new Func<object, Int64>(Convert.ToInt64));
            visitor.GlobalScope.Prototype["ToSByte"] = visitor.Global.FunctionClass.New(new Func<object, SByte>(Convert.ToSByte));
            visitor.GlobalScope.Prototype["ToSingle"] = visitor.Global.FunctionClass.New(new Func<object, Single>(Convert.ToSingle));
            visitor.GlobalScope.Prototype["ToString"] = visitor.Global.FunctionClass.New(new Func<object, String>(Convert.ToString));
            visitor.GlobalScope.Prototype["ToUInt16"] = visitor.Global.FunctionClass.New(new Func<object, UInt16>(Convert.ToUInt16));
            visitor.GlobalScope.Prototype["ToUInt32"] = visitor.Global.FunctionClass.New(new Func<object, UInt32>(Convert.ToUInt32));
            visitor.GlobalScope.Prototype["ToUInt64"] = visitor.Global.FunctionClass.New(new Func<object, UInt64>(Convert.ToUInt64));

            BreakPoints = new List<BreakPoint>();
        }

        /// <summary>
        /// Gets or sets whether script are allowed to access Clr classes. <value>True</value> by default.
        /// </summary>
        /// <remarks>
        /// Eventhough it's value is <value>true</value> by default, PermissionSet is set to None to run in a very secured environment.
        /// </remarks>
        public bool AllowClr
        {
            get { return visitor.AllowClr; }
            set { visitor.AllowClr = value; }
        }

        private PermissionSet permissionSet;

        public static Program Compile(string source, bool debugInformation)
        {
            Program program = null;
            if (!string.IsNullOrEmpty(source))
            {
                var lexer = new ES3Lexer(new ANTLRStringStream(source));
                var parser = new ES3Parser(new CommonTokenStream(lexer)) { DebugMode = debugInformation };

                program = parser.program().value;

                if (parser.Errors != null && parser.Errors.Count > 0)
                {
                    throw new JintException(String.Join(Environment.NewLine, parser.Errors.ToArray()));
                }
            }

            return program;
        }

        /// <summary>
        /// Pre-compiles the expression in order to check syntax errors.
        /// If errors are detected, the Error property contains the message.
        /// </summary>
        /// <returns>True if the expression syntax is correct, otherwiser False</returns>
        public static bool HasErrors(string script, out string errors)
        {
            try
            {
                errors = null;
                Program program = Compile(script, false);

                // In case HasErrors() is called multiple times for the same expression
                return program != null;
            }
            catch (Exception e)
            {
                errors = e.Message;
                return true;
            }
        }

        /// <summary>
        /// Runs a set of JavaScript statements and optionally returns a value if return is called
        /// </summary>
        /// <param name="script">The script to execute</param>
        /// <returns>Optionaly, returns a value from the scripts</returns>
        /// <exception cref="System.ArgumentException" />
        /// <exception cref="System.Security.SecurityException" />
        /// <exception cref="Jint.JintException" />
        public object Run(string script)
        {
            return Run(script, true);
        }

        /// <summary>
        /// Runs a set of JavaScript statements and optionally returns a value if return is called
        /// </summary>
        /// <param name="program">The expression tree to execute</param>
        /// <returns>Optionaly, returns a value from the scripts</returns>
        /// <exception cref="System.ArgumentException" />
        /// <exception cref="System.Security.SecurityException" />
        /// <exception cref="Jint.JintException" />
        public object Run(Program program)
        {
            return Run(program, true);
        }

        /// <summary>
        /// Runs a set of JavaScript statements and optionally returns a value if return is called
        /// </summary>
        /// <param name="reader">The TextReader to read script from</param>
        /// <returns>Optionaly, returns a value from the scripts</returns>
        /// <exception cref="System.ArgumentException" />
        /// <exception cref="System.Security.SecurityException" />
        /// <exception cref="Jint.JintException" />
        public object Run(TextReader reader)
        {
            return Run(reader.ReadToEnd());
        }

        /// <summary>
        /// Runs a set of JavaScript statements and optionally returns a value if return is called
        /// </summary>
        /// <param name="reader">The TextReader to read script from</param>
        /// <param name="unwrap">Whether to unwrap the returned value to a CLR instance. <value>True</value> by default.</param>
        /// <returns>Optionaly, returns a value from the scripts</returns>
        /// <exception cref="System.ArgumentException" />
        /// <exception cref="System.Security.SecurityException" />
        /// <exception cref="Jint.JintException" />
        public object Run(TextReader reader, bool unwrap)
        {
            return Run(reader.ReadToEnd(), unwrap);
        }

        /// <summary>
        /// Runs a set of JavaScript statements and optionally returns a value if return is called
        /// </summary>
        /// <param name="script">The script to execute</param>
        /// <param name="unwrap">Whether to unwrap the returned value to a CLR instance. <value>True</value> by default.</param>
        /// <returns>Optionaly, returns a value from the scripts</returns>
        /// <exception cref="System.ArgumentException" />
        /// <exception cref="System.Security.SecurityException" />
        /// <exception cref="Jint.JintException" />
        public object Run(string script, bool unwrap)
        {

            if (script == null)
                throw new
                    ArgumentException("Script can't be null", "script");

            Program program = Compile(script, DebugMode);

            if (program == null)
                return null;

            return Run(program, unwrap);
        }

        /// <summary>
        /// Runs a set of JavaScript statements and optionally returns a value if return is called
        /// </summary>
        /// <param name="program">The expression tree to execute</param>
        /// <param name="unwrap">Whether to unwrap the returned value to a CLR instance. <value>True</value> by default.</param>
        /// <returns>Optionaly, returns a value from the scripts</returns>
        /// <exception cref="System.ArgumentException" />
        /// <exception cref="System.Security.SecurityException" />
        /// <exception cref="Jint.JintException" />
        public object Run(Program program, bool unwrap)
        {
            if (program == null)
                throw new
                    ArgumentException("Script can't be null", "script");

            visitor.DebugMode = this.DebugMode;
            visitor.PermissionSet = permissionSet;

            if (DebugMode)
            {
                visitor.Step += OnStep;
            }

            try
            {
                visitor.Visit(program);
            }
            catch (SecurityException)
            {
                throw;
            }
            catch (JsException e)
            {
                string message = e.Message;
                if (e.Value.Class == JsError.TYPEOF)
                    message = ((JsError)e.Value).Value.ToString();
                StringBuilder stackTrace = new StringBuilder();
                string source = String.Empty;

                if (DebugMode)
                {
                    while (visitor.CallStack.Count > 0)
                    {
                        stackTrace.AppendLine(visitor.CallStack.Pop());
                    }

                    if (stackTrace.Length > 0)
                    {
                        stackTrace.Insert(0, Environment.NewLine + "------ Stack Trace:" + Environment.NewLine);
                    }
                }

                if (visitor.CurrentStatement.Source != null)
                {
                    source = Environment.NewLine + visitor.CurrentStatement.Source.ToString()
                            + Environment.NewLine + visitor.CurrentStatement.Source.Code;
                }

                throw new JintException(message + source + stackTrace, e);
            }
            catch (Exception e)
            {
                StringBuilder stackTrace = new StringBuilder();
                string source = String.Empty;

                if (DebugMode)
                {
                    while (visitor.CallStack.Count > 0)
                    {
                        stackTrace.AppendLine(visitor.CallStack.Pop());
                    }

                    if (stackTrace.Length > 0)
                    {
                        stackTrace.Insert(0, Environment.NewLine + "------ Stack Trace:" + Environment.NewLine);
                    }
                }

                if (visitor.CurrentStatement.Source != null)
                {
                    source = Environment.NewLine + visitor.CurrentStatement.Source.ToString()
                            + Environment.NewLine + visitor.CurrentStatement.Source.Code;
                }

                throw new JintException(e.Message + source + stackTrace, e.InnerException);
            }
            finally
            {
                visitor.Step -= OnStep;
            }

            return visitor.Result == null ? null : unwrap ? JsClr.ConvertParameter(visitor.Result) : visitor.Result;
        }

        #region Debugger
        public event EventHandler<DebugInformation> Step;
        public event EventHandler<DebugInformation> Break;
        public List<BreakPoint> BreakPoints { get; private set; }
        public bool DebugMode { get; private set; }
        public List<string> WatchList { get; set; }

        public JintEngine SetDebugMode(bool debugMode)
        {
            DebugMode = debugMode;
            return this;
        }

        #endregion

        #region SetParameter overloads

        /// <summary>
        /// Defines an external object to be available inside the script
        /// </summary>
        /// <param name="name">Local name of the object duting the execution of the script</param>
        /// <param name="value">Available object</param>
        /// <returns>The current JintEngine instance</returns>
        public JintEngine SetParameter(string name, object value)
        {
            visitor.GlobalScope[name] = visitor.Global.WrapClr(value);
            return this;
        }

        /// <summary>
        /// Defines an external Double value to be available inside the script
        /// </summary>
        /// <param name="name">Local name of the Double value during the execution of the script</param>
        /// <param name="value">Available Double value</param>
        /// <returns>The current JintEngine instance</returns>
        public JintEngine SetParameter(string name, double value)
        {
            visitor.GlobalScope[name] = visitor.Global.WrapClr(value);
            return this;
        }

        /// <summary>
        /// Defines an external String instance to be available inside the script
        /// </summary>
        /// <param name="name">Local name of the String instance during the execution of the script</param>
        /// <param name="value">Available String instance</param>
        /// <returns>The current JintEngine instance</returns>
        public JintEngine SetParameter(string name, string value)
        {
            visitor.GlobalScope[name] = visitor.Global.WrapClr(value);
            return this;
        }

        /// <summary>
        /// Defines an external Int32 value to be available inside the script
        /// </summary>
        /// <param name="name">Local name of the Int32 value during the execution of the script</param>
        /// <param name="value">Available Int32 value</param>
        /// <returns>The current JintEngine instance</returns>
        public JintEngine SetParameter(string name, int value)
        {
            visitor.GlobalScope[name] = visitor.Global.WrapClr(value);
            return this;
        }

        /// <summary>
        /// Defines an external Boolean value to be available inside the script
        /// </summary>
        /// <param name="name">Local name of the Boolean value during the execution of the script</param>
        /// <param name="value">Available Boolean value</param>
        /// <returns>The current JintEngine instance</returns>
        public JintEngine SetParameter(string name, bool value)
        {
            visitor.GlobalScope[name] = visitor.Global.WrapClr(value);
            return this;
        }

        /// <summary>
        /// Defines an external DateTime value to be available inside the script
        /// </summary>
        /// <param name="name">Local name of the DateTime value during the execution of the script</param>
        /// <param name="value">Available DateTime value</param>
        /// <returns>The current JintEngine instance</returns>
        public JintEngine SetParameter(string name, DateTime value)
        {
            visitor.GlobalScope[name] = visitor.Global.WrapClr(value);
            return this;
        }
        #endregion

        public JintEngine AddPermission(IPermission perm)
        {
            permissionSet.AddPermission(perm);
            return this;
        }

        public JintEngine SetFunction(string name, JsFunction function)
        {
            visitor.GlobalScope[name] = function;
            return this;
        }

        public object CallFunction(string name, params object[] args)
        {
            return CallFunction((JsFunction)visitor.CurrentScope[name], args);
        }

        public object CallFunction(JsFunction function, params object[] args)
        {
            visitor.ExecuteFunction(function, null, JsClr.ConvertParametersBack(visitor, args));
            return JsClr.ConvertParameter(visitor.Returned);
        }

        public JintEngine SetFunction(string name, Delegate function)
        {
            visitor.GlobalScope[name] = visitor.Global.FunctionClass.New(function);
            return this;
        }

        /// <summary>
        /// Escapes a JavaScript string literal
        /// </summary>
        /// <param name="value">The string literal to espace</param>
        /// <returns>The escaped string literal, without sinlge quotes, back slashes and line breaks</returns>
        public static string EscapteStringLiteral(string value)
        {
            return value.Replace("\\", "\\\\").Replace("'", "\\'").Replace(Environment.NewLine, "\\r\\n");
        }

        protected void OnStep(object sender, DebugInformation info)
        {
            if (Step != null)
            {
                Step(this, info);
            }

            if (Break != null)
            {
                BreakPoint breakpoint = BreakPoints.Find(l =>
                {
                    bool afterStart, beforeEnd;

                    afterStart = l.Line > info.CurrentStatement.Source.Start.Line
                        || (l.Line == info.CurrentStatement.Source.Start.Line && l.Char >= info.CurrentStatement.Source.Start.Char);

                    if (!afterStart)
                    {
                        return false;
                    }

                    beforeEnd = l.Line < info.CurrentStatement.Source.Stop.Line
                        || (l.Line == info.CurrentStatement.Source.Stop.Line && l.Char <= info.CurrentStatement.Source.Stop.Char);

                    if (!beforeEnd)
                    {
                        return false;
                    }

                    if (!String.IsNullOrEmpty(l.Condition))
                    {
                        return Convert.ToBoolean(this.Run(l.Condition));
                    }

                    return true;
                });


                if (breakpoint != null)
                {
                    Break(this, info);
                }
            }
        }

        protected void OnBreak(object sender, DebugInformation info)
        {
            if (Break != null)
            {
                Break(this, info);
            }
        }

        public JintEngine DisableSecurity()
        {
            permissionSet = new PermissionSet(PermissionState.Unrestricted);
            return this;
        }

        public JintEngine EnableSecurity()
        {
            permissionSet = new PermissionSet(PermissionState.None);
            return this;
        }

        public void Save(Stream s)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(s, visitor);
        }

        public static void Load(JintEngine engine, Stream s)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            var visitor = (ExecutionVisitor)formatter.Deserialize(s);
            engine.visitor = visitor;
        }

        public static JintEngine Load(Stream s)
        {
            JintEngine engine = new JintEngine();
            Load(engine, s);
            return engine;
        }
    }
}
