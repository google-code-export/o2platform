//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------

using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using O2.Debugger.Mdbg.Debugging.CorDebug;
using O2.Debugger.Mdbg.Debugging.CorDebug;
using O2.Debugger.Mdbg.Debugging.CorDebug;
using CorFunctionBreakpoint=O2.Debugger.Mdbg.Debugging.CorDebug.CorFunctionBreakpoint;
using HResult=O2.Debugger.Mdbg.Debugging.CorDebug.HResult;

namespace O2.Debugger.Mdbg.Debugging.MdbgEngine
{
    /// <summary>
    /// Event arguments for when a breakpoint is added or removed from a breakpoint collection
    /// </summary>
    /// <seealso cref="MDbgBreakpointCollection"/>
    public class BreakpointCollectionChangedEventArgs : EventArgs
    {
        private readonly MDbgBreakpoint m_breakpoint;

        internal BreakpointCollectionChangedEventArgs(MDbgBreakpoint breakpoint)
        {
            m_breakpoint = breakpoint;
        }

        /// <summary>
        /// Get the breakpoint that was added or removed.
        /// </summary>
        public MDbgBreakpoint Breakpoint
        {
            get { return m_breakpoint; }
        }
    }

    /// <summary>
    /// Contains a collection of breakpoints created in debugged program.
    /// </summary>
    public sealed class MDbgBreakpointCollection : MarshalByRefObject, IEnumerable
    {
        private readonly ArrayList m_items = new ArrayList();
        internal int m_freeBreakpointNumber = 1;
        internal MDbgProcess m_process;

        internal MDbgBreakpointCollection(MDbgProcess process)
        {
            Debug.Assert(process != null);
            m_process = process;
        }

        /// <value>
        ///     Number of breakpoints in the collection
        /// </value>
        public int Count
        {
            get { return m_items.Count; }
        }

        /// <summary>
        /// Returns an appdomain from its number. 
        /// </summary>
        /// <value>
        ///     Returns null if the breakpoint doesn't exist.
        /// </value>
        public MDbgBreakpoint this[int breakpointNum]
        {
            get
            {
                foreach (MDbgBreakpoint b in m_items)
                {
                    if (b.Number == breakpointNum)
                        return b;
                }
                return null;
            }
        }

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_items.GetEnumerator();
        }

        #endregion

        /// <summary>
        /// Fired when a breakpoint is added to this collection. Once this is fired, the breakpoint is in the collection
        /// and the Number property is initialized, 
        /// but the derived properties (including Location) are not yet set. Instead, the derived breakpoint will
        /// fire a ChangedBreakpoint event when it sets its properties.
        /// </summary>
        public event EventHandler<BreakpointCollectionChangedEventArgs> AddBreakpoint;

        /// <summary>
        /// Fired when a breakpoint is removed from this collection. 
        /// Once this is fired, the breakpoint is no longer in the collection.
        /// </summary>
        public event EventHandler<BreakpointCollectionChangedEventArgs> RemoveBreakpoint;


        /// <summary>
        /// Fired by a derived breakpoint to notify that it has changed its properties.
        /// 
        /// </summary>
        public event EventHandler<BreakpointCollectionChangedEventArgs> ChangedBreakpoint;


        /// <summary>
        /// Locates MDbgBreakpoint object from the CorBreakpoint object.
        /// </summary>
        /// <param name="corBreakpoint">breakpoint object from CorXXX layer.</param>
        /// <returns>MDbgBreakpoint object</returns>
        /// <remarks>
        ///     returns null if there the breakpoint was not known to the breakpoint
        ///     collection.
        /// </remarks>
        public MDbgBreakpoint Lookup(CorDebug.CorBreakpoint corBreakpoint)
        {
            foreach (MDbgBreakpoint b in m_items)
            {
                foreach (CorDebug.CorBreakpoint cb in b.CorBreakpoints)
                {
                    if (cb == corBreakpoint)
                    {
                        return b;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Creates a breakpoint in debugged program based on source location.
        /// </summary>
        /// <param name="fileName">name of source file.</param>
        /// <param name="lineNumber">line nuber in the source file.</param>
        /// <returns>created breakpoint.</returns>
        /// <remarks>
        ///     The breakpoint is created even if the location of the breakpoint
        ///     cannot be determined at the time of the call. In such cases
        ///     the breakpoint will be created as "unbound". 
        ///     Once the code for the breakpoint will be loaded, the breakpoint
        ///     will be automatically bound.
        /// </remarks>
        public MDbgBreakpoint CreateBreakpoint(string fileName, int lineNumber)
        {
            return new MDbgFunctionBreakpoint(this, new BreakpointLineNumberLocation(fileName, lineNumber));
        }

        /// <summary>
        /// Creates a breakpoint in debugged program based on method and IL offset
        /// within the method.
        /// </summary>
        /// <param name="managedModule">name of the module</param>
        /// <param name="className">Name of the class for the module</param>
        /// <param name="methodName">Name of the method in the class</param>
        /// <param name="offset">IL offset from the beginning of the function. 
        /// Offset 0 means start of the function.
        /// </param>
        /// <returns>created breakpoint.</returns>
        /// <remarks>
        ///     The breakpoint is created even if the location of the breakpoint
        ///     cannot be determined at the time of the call. In such cases
        ///     the breakpoint will be created as "unbound". 
        ///     Once the code for the breakpoint will be loaded, the breakpoint
        ///     will be automatically bound.
        /// </remarks>
        public MDbgBreakpoint CreateBreakpoint(string managedModule, string className, string methodName, int offset)
        {
            return new MDbgFunctionBreakpoint(this,
                                              new BreakpointFunctionLocation(managedModule, className, methodName,
                                                                             offset));
        }

        /// <summary>
        /// Creates a breakpoint in the debugged program based on managed Function 
        /// and IL offset within the method.
        /// </summary>
        /// <param name="managedFunction">object representing loaded managed function.</param>
        /// <param name="offset">IL offset from the beginning of the function.
        /// Offset 0 menas start of the function.</param>
        /// <returns>created breakpoint.</returns>
        /// <remarks>
        ///     Since this method takes MDbgFunction object, breakpoints can be only created on code that has 
        ///     been loaded into the debugged application.
        /// </remarks>
        public MDbgBreakpoint CreateBreakpoint(MDbgFunction managedFunction, int offset)
        {
            return new MDbgFunctionBreakpoint(this, new BreakpointFunctionToken(managedFunction, offset));
        }

        /// <summary>
        /// Create breakpoint from in location object. 
        /// </summary>
        /// 
        /// <param name="location">location of the breakpoint.</param>
        /// <returns>created breakpoint.</returns>
        /// <remarks>
        /// Location object must be retrieved from the breakpoint
        /// using Location property.
        /// <see cref="MDbgBreakpoint.Location"/>
        /// </remarks>
        // location has to be retrieved from existing breakpoint
        public MDbgBreakpoint CreateBreakpoint(object location)
        {
            var l = location as ISequencePointResolver;
            Debug.Assert(l != null);
            if (l == null)
                throw new ArgumentException("incorrect value", "location");
            return new MDbgFunctionBreakpoint(this, l);
        }

        /// <summary>
        /// Deletes all breakpoints in the collection
        /// </summary>
        public void DeleteAll()
        {
            var al = (ArrayList) m_items.Clone();
            for (int i = 0; i < al.Count; i++)
                ((MDbgBreakpoint) al[i]).Delete();
        }

        internal void Clear()
        {
            m_items.Clear();
            m_freeBreakpointNumber = 1;
        }

        /// <summary>
        /// Fire the ChangedBreakpoint event for the given breakpoint.
        /// </summary>
        /// <param name="breakpoint">Breakpoint that has changed</param>
        internal void NotifyChanged(MDbgBreakpoint breakpoint)
        {
            if (ChangedBreakpoint != null)
            {
                ChangedBreakpoint(this, new BreakpointCollectionChangedEventArgs(breakpoint));
            }
        }

        internal void BindBreakpoints(MDbgModule loadedModule)
        {
            foreach (MDbgBreakpoint b in m_items)
            {
                b.BindToModule(loadedModule);
            }
        }

        internal int Register(MDbgBreakpoint breakpoint)
        {
            Debug.Assert(breakpoint != null);
            m_items.Add(breakpoint);

            // At this point, derived properties have not yet been set.
            // Let MDbgBreakpoint do a little more initialization and then it will fire the Add 
            // event via calling FireAddBreakpointEvent

            return m_freeBreakpointNumber++;
        }

        // Called by MDbgBreakpoint after it has set the Number property. 
        internal void FireAddBreakpointEvent(MDbgBreakpoint breakpoint)
        {
            if (AddBreakpoint != null)
            {
                AddBreakpoint(this, new BreakpointCollectionChangedEventArgs(breakpoint));
            }
        }

        internal void UnRegister(MDbgBreakpoint breakpoint)
        {
            Debug.Assert(breakpoint != null);
            m_items.Remove(breakpoint);

            if (RemoveBreakpoint != null)
            {
                RemoveBreakpoint(this, new BreakpointCollectionChangedEventArgs(breakpoint));
            }
        }
    }

    /// <summary>
    /// Class representing a breakpoint in the debugged program.
    /// </summary>
    /// <remarks>
    /// This class doesn't have any public constructor. To create a new breakpoint use
    /// CreateBreakpoint methods on MDbgBreakpoint collection.
    /// <see cref="MDbgBreakpointCollection"/>
    /// </remarks>
    public abstract class MDbgBreakpoint : MarshalByRefObject
    {
        // if breakpointCollection is null, the breakpoint is considered a free breakoint (not associated with any
        // collection)
        private readonly int m_breakpointNum;
        internal MDbgBreakpointCollection m_breakpointCollection;
        internal ArrayList m_breakpoints;

        internal MDbgBreakpoint(MDbgBreakpointCollection breakpointCollection)
        {
            m_breakpointCollection = breakpointCollection;
            if (m_breakpointCollection != null)
            {
                m_breakpointNum = m_breakpointCollection.Register(this);

                // Now that the number + breakpoint collection properties have been set, 
                // this bp is sufficiently initialized to fire the "Add" event. 
                m_breakpointCollection.FireAddBreakpointEvent(this);
            }
            else
            {
                m_breakpointNum = 0;
            }
        }


        /// <summary>
        /// Gets or sets if the breakpoint is enabled.
        /// </summary>
        /// <value>
        /// Sets or gets value showing if the breakpoint is enabled or not.
        /// </value>
        /// <remarks>
        ///     When breakpoint is not enabled, it won't be hit.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// Throws an InvalidOperationException if the breakpoint is not yet bound and therefore cannot 
        /// be enabled.
        /// </exception>
        public bool Enabled
        {
            get
            {
                if (m_breakpoints == null)
                {
                    // we don't have any breakpoints, so they are "by definition" disabled.
                    return false;
                }
                else
                {
                    Debug.Assert(m_breakpoints.Count > 0); // we have to have at least one breakpoint in the array;

                    var bp = (CorDebug.CorBreakpoint) m_breakpoints[0];
                    bool active = bp.IsActive;
#if DEBUG
                    // all breakpoints have to have same Active Status
                    for (int i = 1; i < m_breakpoints.Count; i++)
                    {
                        Debug.Assert(((CorDebug.CorBreakpoint) m_breakpoints[i]).IsActive == active);
                    }
#endif
                    return active;
                }
            }
            set
            {
                if (m_breakpoints == null
                    && value)
                {
                    throw new InvalidOperationException("Cannot enable not bound breakpoints.");
                }

                if (m_breakpoints != null)
                {
                    for (int i = 0; i < m_breakpoints.Count; ++i)
                    {
                        try
                        {
                            (m_breakpoints[i] as CorDebug.CorBreakpoint).Activate(value);
                        }
                        catch (COMException e)
                        {
                            if (e.ErrorCode != (int) CorDebug.HResult.CORDBG_E_PROCESS_TERMINATED)
                                throw;
                            // currently CORDBG_E_PROCESS_TERMINATED means that the breakpoint
                            // reference is invalid. This can happen for e.g. in case when an
                            // appdomain gets unloaded with a breakpoint set in it. Any operation
                            // on such breakpoint resuts in CORDBG_E_PROCESS_TERMINATED exception.

                            // in this situation we should remove the breakpoint from our collection.
                            m_breakpoints.RemoveAt(i);

                            // since we have removed element at current position, the next item is
                            // also at position i. We need to compensate for the ++i in for statement
                            --i;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets if the breakpoint is bound.
        /// </summary>
        /// <value>
        ///     Is true if the breakpoint has been bound. Bound breakpoints
        ///     are breakpoints for which a code that they should break in 
        ///     has already been loaded into the process.
        /// </value>
        public bool IsBound
        {
            get { return (m_breakpoints != null); }
        }

        /// <summary>
        /// Breakpoint logical number. Breakpoints in debugged process are assigned
        /// unique logical number. First created breakpoint in the process gets logical
        /// number 0, second receives number 1, etc...
        /// Logical numbers are never reused in a process.
        /// </summary>
        /// <value>
        ///     Logical number for the breakpoint.
        /// </value>
        public int Number
        {
            get { return m_breakpointNum; }
        }

        /// <summary>
        /// Breakpoint objects representing current breakpoint.
        /// </summary>
        /// <remarks>
        ///     The returned value is a collection of CorXXX objects representing
        ///     different breakpoints. If the code has been loaded into multiple
        ///     appdomains than the breakpoints.
        /// </remarks>
        public IEnumerable CorBreakpoints
        {
            get
            {
                if (m_breakpoints == null)
                    return new ArrayList();
                else
                    return m_breakpoints;
            }
        }

        /// <summary>
        /// Returns CorBreakpoint object.
        /// </summary>
        /// If the breakpoint is represented by more than one occurance of
        /// breakpoint object, only the first instance is returned.
        public CorDebug.CorBreakpoint CorBreakpoint
        {
            get
            {
                if (m_breakpoints == null)
                    return null;
                else
                    return (CorDebug.CorBreakpoint) m_breakpoints[0];
            }
        }

        /// <summary>
        /// Gets the location of the breakpoint.
        /// </summary>
        /// <value>
        /// Returns an object that represents a location of the breakpoint. The breakpoint can be
        /// re-created in different debugging sessions by calling CreateBreakpoint and passing in
        /// the location object.
        /// </value>
        public abstract Object Location { get; }

        /// <summary>
        /// Deletes the breakpoint.
        /// </summary>
        /// Breakpoint is also removed from the breakpoint list.
        public void Delete()
        {
            Enabled = false;
            if (m_breakpointCollection != null)
                m_breakpointCollection.UnRegister(this);
        }

        /// <summary>
        /// Obtains a string representation of this instance.
        /// </summary>
        /// <returns>
        ///     The friendly name of the breakpoint.
        /// </returns>
        public override string ToString()
        {
            var s = new StringBuilder("Breakpoint #");
            s.Append(m_breakpointNum).Append((IsBound ? " bound" : " unbound"));
            return s.ToString();
        }

        /// <summary>
        /// Function tries to bind a breakpoint to the specified module.
        /// </summary>
        /// <param name="managedModule">A module the breakpoint should be bound to.</param>
        /// <returns>true if breakpoint was successfully bound</returns>
        /// <remarks>
        ///     This function is called by breakpoint manager for every brekapoint whenever a new module
        ///     gets loaded into the debugged process. 
        /// </remarks>
        public abstract bool BindToModule(MDbgModule managedModule);

        /// <summary>
        /// Binds the breakpoint.
        /// </summary>
        /// <returns>true on success, else false.</returns>
        protected abstract bool Bind();
    }

    /// <summary>
    /// Interface for querying properties of the MdbgBreakpoint object.
    /// </summary>
    /// <remarks>
    /// If the object supports it, the MdbgBreakpoint.Location can be cast to this interface
    /// to query breakpoint location by source line.
    /// <see cref="MDbgBreakpointCollection"/>
    /// </remarks>
    public interface IBreakpointBySourceLine
    {
        /// <summary>
        /// Gets the file name where breakpoint has been set.
        /// </summary>
        /// <value>
        /// Returns a file name of the file on which the breakpoint has been set.
        /// </value>
        string FileName { get; }

        /// <summary>
        /// Gets the location of the breakpoint within the source file.
        /// </summary>
        /// <value>
        /// Returns the line within a file at which the breakpoint is set.
        /// </value>
        int LineNumber { get; }
    }

    /// <summary>
    /// Interface for querying properties of the MdbgBreakpoint object.
    /// </summary>
    /// <remarks>
    /// If the object supports it, the MdbgBreakpoint.Location can be cast to this interface
    /// to query breakpoint location by function on which it was set.
    /// <see cref="MDbgBreakpointCollection"/>
    /// </remarks>
    public interface IBreakpointByFunctionName
    {
        /// <summary>
        /// Gets the name of the module in which breakpoint has been set.
        /// </summary>
        /// <value>
        /// Returns a module name in which breakpoint is located.
        /// </value>
        string ModuleName { get; }

        /// <summary>
        /// Gets the name of the class in which breakpoint has been set.
        /// </summary>
        /// <value>
        /// Returns a class name of method in which breakpoint is located.
        /// </value>
        string ClassName { get; }

        /// <summary>
        /// Gets the name of the method in which breakpoint has been set.
        /// </summary>
        /// <value>
        /// Returns a method name where breakpoint is located.
        /// </value>
        string MethodName { get; }

        /// <summary>
        /// Gets the IL offset of breakpoint location.
        /// </summary>
        /// <value>
        /// Returns an offset of IL instruction where where breakpoint is located.
        /// </value>
        int ILOffset { get; }
    }

    /// <summary>
    /// Interface used for resolution of breakpoint location.
    /// </summary>
    public interface ISequencePointResolver
    {
        /// <summary>
        /// Function tries to resolve the breakpoint from breakpoint description.
        /// </summary>
        /// <param name="functionBreakpoint">A breakpoint object.</param>
        /// <param name="managedModule">A module that the breakpoint should be resolved at.</param>
        /// <param name="managedFunction">A function that is resolved from the breakpoint description.</param>
        /// <param name="ilOffset">An il offset within a function resolved from the breakpoint description.</param>
        /// <returns>true if breakpoint was successfully resolved</returns>
        /// <remarks>
        ///     Resolved is usually called for every loaded module.
        /// </remarks>
        bool ResolveLocation(MDbgFunctionBreakpoint functionBreakpoint, MDbgModule managedModule,
                             out MDbgFunction managedFunction, out int ilOffset);
    }

    /// <summary>
    /// Class represents a location of breakpoint identified by file name and line number.
    /// </summary>
    public class BreakpointLineNumberLocation : ISequencePointResolver, IBreakpointBySourceLine
    {
        private readonly string m_file;
        private readonly int m_lineNo;

        /// <summary>
        /// Constructs new BreakpointLineNumberLocation object.
        /// </summary>
        /// <param name="file">A name of the file where the breakpoint has been set.</param>
        /// <param name="lineNumber">A line number within a file.</param>
        public BreakpointLineNumberLocation(string file, int lineNumber)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("file has to be specified.");
            if (lineNumber < 1)
                throw new ArgumentException("lineNo has to be greater than 0");

            m_file = file;
            m_lineNo = lineNumber;
        }

        #region IBreakpointBySourceLine Members

        /// <value>
        ///     File where the breakpoint is defined.
        /// </value>
        public string FileName
        {
            get { return m_file; }
        }

        /// <value>
        ///     Line where the breakpoint is defined.
        /// </value>
        public int LineNumber
        {
            get { return m_lineNo; }
        }

        #endregion

        #region ISequencePointResolver Members

        /// <summary>
        /// Function tries to resolve the breakpoint from breakpoint description.
        /// </summary>
        /// <param name="functionBreakpoint">A breakpoint object.</param>
        /// <param name="managedModule">A module that the breakpoint should be resolved at.</param>
        /// <param name="managedFunction">A function that is resolved from the breakpoint description.</param>
        /// <param name="ILoffset">An il offset within a function resolved from the breakpoint description.</param>
        /// <returns>true if breakpoint was successfully resolved</returns>
        /// <remarks>
        ///     Resolved is usually called for every loaded module.
        /// </remarks>
        public bool ResolveLocation(MDbgFunctionBreakpoint functionBreakpoint, MDbgModule managedModule,
                                    out MDbgFunction managedFunction, out int ILoffset)
        {
            Debug.Assert(m_lineNo > 0 && m_file.Length > 0);

            managedFunction = null;
            ILoffset = 0;

            if (managedModule.SymReader == null)
                // no symbols for current module, skip it.
                return false;

            foreach (ISymbolDocument doc in managedModule.SymReader.GetDocuments())
            {
                if (String.Compare(doc.URL, m_file, true, CultureInfo.InvariantCulture) == 0 ||
                    String.Compare(Path.GetFileName(doc.URL), m_file, true, CultureInfo.InvariantCulture) == 0)
                {
                    int lineNo = 0;
                    try
                    {
                        lineNo = doc.FindClosestLine(m_lineNo);
                    }
                    catch (COMException e)
                    {
                        if (e.ErrorCode == (int) CorDebug.HResult.E_FAIL)

                            // we continue, because this location is not in this file, let's
                            // keep trying to search for next file.
                            continue;
                    }

                    ISymbolMethod symMethod = managedModule.SymReader.GetMethodFromDocumentPosition(doc, lineNo, 0);
                    managedFunction = managedModule.GetFunction(symMethod.Token.GetToken());
                    ILoffset = managedFunction.GetIPFromPosition(doc, lineNo);

                    // If this IL 
                    if (ILoffset == -1)
                    {
                        return false;
                    }
                    Debug.Assert(ILoffset != -1);
                    return true;
                }
            }
            managedFunction = null;
            ILoffset = -1;
            return false;
        }

        #endregion

        /// <summary>
        /// Obtains a string representation of this instance.
        /// </summary>
        /// <returns>
        ///     The friendly name of the breakpoint.
        /// </returns>
        public override string ToString()
        {
            // Using the full path makes debugging output inconsistant during automated test runs.
            // For testing purposes we'll get rid of them.
            //return "line "+m_lineNo+" in "+m_file;
            return "line " + m_lineNo + " in " + Path.GetFileName(m_file);
        }
    }

    /// <summary>
    /// This class represents a location of the function breakpoint.
    /// </summary>
    public class BreakpointFunctionLocation : ISequencePointResolver, IBreakpointByFunctionName
    {
        private readonly string m_className;
        private readonly int m_ILoffset = -1;
        private readonly string m_methodName;
        private readonly string m_moduleName;

        /// <summary>
        /// Constructs new BreakpointFunctionLocation object.
        /// </summary>
        /// <param name="moduleName">A name of the module.</param>
        /// <param name="className">A name of class.</param>
        /// <param name="methodName">A name of method.</param>
        /// <param name="ilOffset">An il offset within a method description.</param>
        public BreakpointFunctionLocation(string moduleName, string className,
                                          string methodName, int ilOffset)
        {
            if (methodName == null || methodName.Length == 0)
                throw new ArgumentException("methodName must be specified");
            if (ilOffset < 0)
                throw new ArgumentException("ilOffset cannot be negative");

            m_moduleName = moduleName;
            m_className = className;
            m_methodName = methodName;
            m_ILoffset = ilOffset;
        }

        #region IBreakpointByFunctionName Members

        /// <value>
        ///     Name of the module.
        /// </value>
        public string ModuleName
        {
            get { return m_moduleName; }
        }

        /// <value>
        ///     Name of the class.
        /// </value>
        public string ClassName
        {
            get { return m_className; }
        }

        /// <value>
        ///     Name of the method.
        /// </value>
        public string MethodName
        {
            get { return m_methodName; }
        }

        /// <value>
        ///     IL offset within the method.
        /// </value>
        public int ILOffset
        {
            get { return m_ILoffset; }
        }

        #endregion

        #region ISequencePointResolver Members

        /// <summary>
        /// Function tries to resolve the breakpoint from breakpoint description.
        /// </summary>
        /// <param name="functionBreakpoint">A breakpoint object.</param>
        /// <param name="managedModule">A module that the breakpoint should be resolved at.</param>
        /// <param name="managedFunction">A function that is resolved from the breakpoint description.</param>
        /// <param name="ilOffset">An il offset within a function resolved from the breakpoint description.</param>
        /// <returns>true if breakpoint was successfully resolved</returns>
        /// <remarks>
        ///     Resolved is usually called for every loaded module.
        /// </remarks>
        public bool ResolveLocation(MDbgFunctionBreakpoint functionBreakpoint, MDbgModule managedModule,
                                    out MDbgFunction managedFunction, out int ilOffset)
        {
            managedFunction = null;
            ilOffset = m_ILoffset;

            if (m_moduleName != null && m_moduleName.Length > 0)
            {
                if (!managedModule.MatchesModuleName(m_moduleName))
                    return false;
            }

            managedFunction = functionBreakpoint.m_breakpointCollection.m_process.ResolveFunctionName(managedModule,
                                                                                                      m_className,
                                                                                                      m_methodName);

            return managedFunction != null;
        }

        #endregion

        /// <summary>
        /// Obtains a string representation of this instance.
        /// </summary>
        /// <returns>
        ///     The friendly name of the breakpoint.
        /// </returns>
        public override string ToString()
        {
            return ((m_moduleName.Length == 0 ? "" : m_moduleName + "!") + m_className + "::" + m_methodName + "(+" +
                    m_ILoffset + ")");
        }
    }


    /// <summary>
    /// This class represents a location of the function breakpoint. The location is specified as token and IL
    /// offset.
    /// </summary>
    public class BreakpointFunctionToken : ISequencePointResolver
    {
        private readonly MDbgFunction m_function;
        private readonly int m_ILoffset = -1;

        /// <summary>
        /// Constructs new BreakpointFunctionToken object.
        /// </summary>
        /// <param name="managedFunction">A function breakpoint is created at.</param>
        /// <param name="ilOffset">An il offset within a method description.</param>
        public BreakpointFunctionToken(MDbgFunction managedFunction, int ilOffset)
        {
            Debug.Assert(managedFunction != null);
            m_function = managedFunction;
            m_ILoffset = ilOffset;
        }

        #region ISequencePointResolver Members

        /// <summary>
        /// Function tries to resolve the breakpoint from breakpoint description.
        /// </summary>
        /// <param name="functionBreakpoint">A breakpoint object.</param>
        /// <param name="managedModule">A module that the breakpoint should be resolved at.</param>
        /// <param name="managedFunction">A function that is resolved from the breakpoint description.</param>
        /// <param name="ilOffset">An il offset within a function resolved from the breakpoint description.</param>
        /// <returns>true if breakpoint was successfully resolved</returns>
        /// <remarks>
        ///     Resolved is usually called for every loaded module.
        /// </remarks>
        public bool ResolveLocation(MDbgFunctionBreakpoint functionBreakpoint, MDbgModule managedModule,
                                    out MDbgFunction managedFunction, out int ilOffset)
        {
            managedFunction = null;
            ilOffset = -1;

            // check if the function is from the module we specified.
            if (m_function.Module != managedModule)
                return false;

            managedFunction = m_function;
            ilOffset = m_ILoffset;
            return true;
        }

        #endregion

        /// <summary>
        /// Obtains a string representation of this instance.
        /// </summary>
        /// <returns>
        ///     The friendly name of the breakpoint.
        /// </returns>
        public override string ToString()
        {
            return (m_function.FullName + "(+" + m_ILoffset + ")");
        }
    }


    /// <summary>
    /// Class representing a breakpoint at an IL offset in the debugged program.
    /// </summary>
    /// <remarks>
    /// This can be subclassed to provide specific additional logic on the breakpoints (such as a conditional breakpoint or hit count)
    /// This also includes all source-level breakpoints because all source lines should map to some IL offset.
    /// This does not include breakpoints at native-offsets.
    /// <see cref="MDbgBreakpointCollection"/>
    /// </remarks>
    public class MDbgFunctionBreakpoint : MDbgBreakpoint
    {
        private readonly ISequencePointResolver m_location;

        /// <summary>
        /// Constructs a new breakoint in the debugged process.
        /// </summary>
        /// <param name="breakpointCollection">A collection that holds programs breakpoints.</param>
        /// <param name="location">An object that is capable of resolving breakpoint location.</param>
        public MDbgFunctionBreakpoint(MDbgBreakpointCollection breakpointCollection, ISequencePointResolver location)
            : base(breakpointCollection)
        {
            m_location = location;
            Bind();

            if (breakpointCollection != null)
            {
                breakpointCollection.NotifyChanged(this);
            }
        }

        /// <value>
        ///     Returns the location of the breakpoint.
        /// </value>
        public override Object Location
        {
            get { return m_location; }
        }

        /// <summary>
        /// Notify that this breakpoint has change properties. 
        /// This will cause the NotifyChange event to fire on the BreakpointCollection
        /// </summary>
        protected void NotifyChanged()
        {
            if (m_breakpointCollection != null)
            {
                m_breakpointCollection.NotifyChanged(this);
            }
        }


        /// <summary>
        /// Function tries to bind a breakpoint to the specified module.
        /// </summary>
        /// <param name="managedModule">A module the breakpoint should be bound to.</param>
        /// <returns>true if breakpoint was successfully bound or false if it failed or was already bound.</returns>
        /// <remarks>
        ///     This function is called by breakpoint manager for every brekapoint whenever a new module
        ///     gets loaded into the debugged process or whenever a dynamic module loads a new class
        ///     or new symbols. This adds any missing bindings, but will not duplicate any that already exist.
        /// </remarks>
        public override sealed bool BindToModule(MDbgModule managedModule)
        {
            MDbgFunction func;
            int ILoffset;

            // Note that in some cases (eg. source/line breakpoints) we may actually
            // want to bind to multiple locations in this module instead of just one.
            if (!m_location.ResolveLocation(this, managedModule, out func, out ILoffset))
                return false;

            if (m_breakpoints != null)
            {
                // Assume all breakpoints are CorFunctionBreakpoints.
                // If this ever becomes invalid, we'll need a new check here to avoid 
                // duplicating that type of breakpoint.
                foreach (CorDebug.CorFunctionBreakpoint cb in m_breakpoints)
                {
                    // If we find a CorBreakpoint that already matches this location
                    // don't add a new one, or the debugger will stop twice when it's hit.
                    // Note that CorFunction instances are 1:1 with a specific function in a 
                    // specific module and AppDomain (but represents all generic instantiations).
                    if (cb.Function == func.CorFunction && cb.Offset == ILoffset)
                        return false;
                }
            }

            // Use the resolved information to get a raw CorBreakpoint object.
            CorBreakpoint breakpoint = null;
            try
            {
                if (ILoffset == 0)
                {
                    breakpoint = func.CorFunction.CreateBreakpoint();
                }
                else
                {
                    // we need to set a breakpoint on code rather than directly on function
                    CorCode code = func.CorFunction.ILCode;

                    if (code == null)
                    {
                        throw new MDbgException(String.Format(CultureInfo.InvariantCulture,
                                                              "IL Code for function {0} is null",
                                                              new Object[] {func.FullName}));
                    }
                    breakpoint = code.CreateBreakpoint(ILoffset);
                }
            }
            catch (COMException)
            {
                return false;
            }

            // Add the new CorBreakpoint object to our internal list and register a handler for it.
            Debug.Assert(breakpoint != null);
            breakpoint.Activate(true);
            if (m_breakpoints == null)
            {
                m_breakpoints = new ArrayList();
            }
            m_breakpoints.Add(breakpoint);

            MDbgProcess p = managedModule.Process;
            CustomBreakpointEventHandler handler = InternalOnHitHandler;
            p.RegisterCustomBreakpoint(breakpoint, handler);

            return true;
        }

        /// <summary>
        /// Binds the breakpoint.
        /// </summary>
        /// <returns>true on success, else false.</returns>
        protected override bool Bind()
        {
            Debug.Assert(m_breakpoints == null, "We should not bind already active breakpoints");

            if (m_breakpointCollection == null)
            {
                return false;
            }

            bool isBound = false;

            // called first time we create breakpoints (we need to iterate through all modules).
            // If a module is loaded into multiple appdomains, there will be multiple MDbgModule objects
            // for it in the collection.
            foreach (MDbgModule m in m_breakpointCollection.m_process.Modules)
            {
                if (BindToModule(m))
                {
                    isBound = true;
                }
            }
            return isBound;
        }

        // Common on-hit handler.
        private void InternalOnHitHandler(object sender, CustomBreakpointEventArgs e)
        {
            // Mark call to derived class.
            object stopreason = OnHitHandler(e);

            // If stop-reason supplied, then stop the shell.
            if (stopreason != null)
            {
                e.Controller.Stop(e.BreakpointHitCallbackArgs.Thread, stopreason);
            }
        }

        /// <summary> Callback invoked when a breakpoint is hit. </summary>
        /// <returns> Return null to continue running past this breakpoint. Returns a non-null object
        /// to use as stop-reason to stop the process. </returns>
        /// <remarks> Derived classes can override this callback. This is fired during a raw debug managed 
        /// debug event (directly from ICorDebugManagedCallback) and so the implementor can only do inspection
        /// operations. It can't resume the process (return null instead of a stop-reason to do that), and it
        /// certainly can't expect any other debuge events to come in. 
        /// For example, DON'T do soemthing like setup a func-eval, continue the process, and wait for the 
        /// eval complete event.
        /// </remarks>
        public virtual object OnHitHandler(CustomBreakpointEventArgs e)
        {
            // Default impl stops at all breakpoints.            
            return new BreakpointHitStopReason(this);
        }


        /// <summary>
        /// Returns a human-readable representation of the breakpoint.
        /// </summary>
        /// <returns>The name is in mdbg's format as displayed in mdbg's 'break' command.</returns>
        public override string ToString()
        {
            string location = (m_location == null)
                                  ?
                                      "?"
                                  :
                                      m_location.ToString();
            return base.ToString() + " (" + location + ")";
        }
    }
}
