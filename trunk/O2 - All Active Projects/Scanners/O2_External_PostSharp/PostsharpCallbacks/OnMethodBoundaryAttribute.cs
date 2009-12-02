#region This code was based on the PostSharp.Samples.XTrace Sample code
/*which had the following license:
  
  Released to Public Domain by Gael Fraiteur
/*----------------------------------------------------------------------------*
 *   This file is part of samples of PostSharp.                                *
 *                                                                             *
 *   This sample is free software: you have an unlimited right to              *
 *   redistribute it and/or modify it.                                         *
 *                                                                             *
 *   This sample is distributed in the hope that it will be useful,            *
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of            *
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.                      *
 *                                                                             *
 *----------------------------------------------------------------------------*/
#endregion

using System;
using System.Diagnostics;
using System.Reflection;
using PostSharp.Laos;

namespace O2.External.PostSharp.PostsharpCallbacks
{
    /// <summary>
    /// Custom attribute that, when applied (or multicasted) to a method,
    /// writes an informational record to the <see cref="Trace"/> class.
    /// </summary>
    [Serializable]
    public sealed class OnMethodBoundaryAttribute : OnMethodBoundaryAspect
    {
        /* Note that all these fields are initialized at compile time
         * and are deserialized at runtime.                             */        
        private string prefix;
        private MethodFormatStrings formatStrings;
        private bool isVoid;

        /// <summary>
        /// Gets or sets the prefix string, printed before every trace message.
        /// </summary>
        /// <value>
        /// For instance <c>[Trace]</c>.
        /// </value>
        public string Prefix { get { return this.prefix; } set { this.prefix = value; } }

        /// <summary>
        /// Initializes the current object. Called at compile time by PostSharp.
        /// </summary>
        /// <param name="method">Method to which the current instance is
        /// associated.</param>
        public override void CompileTimeInitialize( MethodBase method )
        {
            this.prefix = Formatter.NormalizePrefix( this.prefix );
            this.formatStrings = Formatter.GetMethodFormatStrings( method );
            MethodInfo methodInfo = method as MethodInfo;
            if ( methodInfo != null )
            {
                this.isVoid = methodInfo.ReturnType == typeof(void);
            }
            else
            {
                this.isVoid = true;
            }
        }

        /// <summary>
        /// Method executed <b>before</b> the body of methods to which this aspect is applied.
        /// We just trace and increment indentation.
        /// </summary>
        /// <param name="context">Event arguments specifying which method
        /// is being executed and which are its arguments.</param>
        public override void OnEntry( MethodExecutionEventArgs context )
        {            
            //this.prefix_ += ".";
            //Trace.Write(">>>> " + this.prefix + context.Method.DeclaringType.FullName + " - " + context.Method);

            //return;


            //Trace.TraceInformation(
            Trace.Write(
                this.prefix + "> Entering " +
                this.formatStrings.Format(
                    context.Instance,
                    context.Method,
                    context.GetReadOnlyArgumentArray()));

            //Patches.applyPatch(context.GetReadOnlyArgumentArray(), context);

        }

        
        /// <summary>
        /// Method executed <b>after</b> the body of methods 
        /// to which this aspect is applied, when the method succeeds.
        /// </summary>
        /// <param name="eventArgs">Event arguments specifying which method
        /// is being executed and which are its arguments.</param>
        public override void OnSuccess( MethodExecutionEventArgs eventArgs )
        {
            //if (this.prefix.Length > 0)
            //    this.prefix = this.prefix.Substring(0, this.prefix.Length - 1);
            //Trace.Write("< " + this.prefix + eventArgs.Instance + " - " + eventArgs.Method);
            
            //Trace.Write("< " + this.prefix + eventArgs.Method.DeclaringType.FullName + " - " + eventArgs.Method);
            //return;            //Trace.TraceInformation(

            Trace.Write(
                this.prefix + "< Leaving " +
                this.formatStrings.Format(
                    eventArgs.Instance,
                    eventArgs.Method,
                    eventArgs.GetReadOnlyArgumentArray()) +
                ( this.isVoid ? "" : Formatter.FormatString( " : {{{0}}}.", eventArgs.ReturnValue ) ) );
        }

        /// <summary>
        /// Method executed <b>after</b> the body of methods 
        /// to which this aspect is applied, when the method ends with an exception.
        /// </summary>
        /// <param name="eventArgs">Event arguments specifying which method
        /// is being executed and which are its arguments.</param>
        public override void OnException( MethodExecutionEventArgs eventArgs )
        {
#if !MONO
            //Trace.Unindent();
#endif
            
            Trace.TraceWarning(
                this.prefix + "Leaving " +
                this.formatStrings.Format(
                    eventArgs.Instance,
                    eventArgs.Method,
                    eventArgs.GetReadOnlyArgumentArray()
                    ) +
                Formatter.FormatString( " with exception {0} : {{{1}}}.", eventArgs.Exception.GetType().Name,
                                        eventArgs.Exception.Message ) );
        }
    }
}
