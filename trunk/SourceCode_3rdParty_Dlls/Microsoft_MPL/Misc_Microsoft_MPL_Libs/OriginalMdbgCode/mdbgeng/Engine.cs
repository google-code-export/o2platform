//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------
using System;
using O2.Debugger.Mdbg.Debugging.CorDebug;
using O2.Debugger.Mdbg.Debugging.CorDebug;
using O2.Debugger.Mdbg.Debugging.CorDebug;
using O2.Debugger.Mdbg.Debugging.CorPublish;
using O2.Debugger.Mdbg.Debugging.CorPublish;
using O2.Debugger.Mdbg.Debugging.CorPublish;

namespace O2.Debugger.Mdbg.Debugging.MdbgEngine
{
    /// <summary>
    /// MDbgOptions class.  This controls when the debugger should stop.
    /// </summary>
    public sealed class MDbgOptions : MarshalByRefObject
    {
        private bool m_ShowFullPaths = true;
        private bool m_StopOnUnhandledException = true; // default is on.

        internal MDbgOptions() // only one instance in mdbgeng
        {
        }

        /// <summary>
        /// Gets or sets if it should stop when modules are loaded.
        /// </summary>
        /// <value>true if it should stop, else false.</value>
        public bool StopOnModuleLoad { get; set; }

        /// <summary>
        /// Gets or sets if it should stop when classes are loaded.
        /// </summary>
        /// <value>true if it should stop, else false.</value>
        public bool StopOnClassLoad { get; set; }

        /// <summary>
        /// Gets or sets if it should stop when assemblies are loaded.
        /// </summary>
        /// <value>true if it should stop, else false.</value>
        public bool StopOnAssemblyLoad { get; set; }

        /// <summary>
        /// Gets or sets if it should stop when assemblies are unloaded.
        /// </summary>
        /// <value>true if it should stop, else false.</value>
        public bool StopOnAssemblyUnload { get; set; }

        /// <summary>
        /// Gets or sets if it should stop when new threads are created.
        /// </summary>
        /// <value>true if it should stop, else false.</value>
        public bool StopOnNewThread { get; set; }

        /// <summary>
        /// Gets or sets if it should stop on Exception callbacks.
        /// </summary>
        /// <value>true if it should stop, else false.</value>
        public bool StopOnException { get; set; }

        /// <summary>
        /// Gets or sets if it should stop on Exception callbacks.
        /// </summary>
        /// <value>true if it should stop, else false.</value>
        public bool StopOnUnhandledException
        {
            get { return m_StopOnUnhandledException; }
            set { m_StopOnUnhandledException = value; }
        }

        /// <summary>
        /// Gets or sets if it should stop on Enhanced Exception callbacks.
        /// </summary>
        /// <value>true if it should stop, else false.</value>
        public bool StopOnExceptionEnhanced { get; set; }

        /// <summary>
        /// Gets or sets if it should stop when messages are logged.
        /// You must still enable log messages per process by calling CorProcess.EnableLogMessage(true)
        /// </summary>
        /// <value>true if it should stop, else false.</value>
        public bool StopOnLogMessage { get; set; }


        /// <summary>
        /// Gets or sets the Symbol path.
        /// </summary>
        /// <value>The Symbol path.</value>
        public string SymbolPath { get; set; }

        /// <summary>
        /// Gets or sets if processes are created with a new console.
        /// </summary>
        /// <value>Default is false.</value>
        public bool CreateProcessWithNewConsole { get; set; }

        /// <summary>
        /// Gets or sets if memory addresses are displayed.
        /// Normally of little value in pure managed debugging, and causes
        /// unpredictable output for automated testing.
        /// </summary>
        /// <value>Default is false.</value>
        public bool ShowAddresses { get; set; }

        /// <summary>
        /// Gets or sets if full paths are displayed in stack traces.
        /// </summary>
        /// <value>Default is true.</value>
        public bool ShowFullPaths
        {
            get { return m_ShowFullPaths; }
            set { m_ShowFullPaths = value; }
        }
    }

    /// <summary>
    /// A delegate that returns a default implementation for stack walking frame factory.
    /// </summary>
    /// <returns>Frame factory used for newly created processes.</returns>
    public delegate IMDbgFrameFactory StackWalkingFrameFactoryProvider();

    /// <summary>
    /// The MDbgEngine class.
    /// </summary>
    public sealed class MDbgEngine : MarshalByRefObject
    {
        private readonly MDbgOptions m_options = new MDbgOptions();
        private readonly MDbgProcessCollection m_processMgr;
        internal StackWalkingFrameFactoryProvider m_defaultStackWalkingFrameFactoryProvider;

        /// <summary>
        /// Initializes a new instance of the MDbgEngine class.
        /// </summary>
        public MDbgEngine()
        {
            m_processMgr = new MDbgProcessCollection(this);
        }

        /// <summary>
        /// Gets the MDbgProcessCollection.
        /// </summary>
        /// <value>The MDbgProcessCollection.</value>
        public MDbgProcessCollection Processes
        {
            get { return m_processMgr; }
        }

        /// <summary>
        /// Gets the current MDbgOptions.
        /// </summary>
        /// <value>The MDbgOptions.</value>
        public MDbgOptions Options
        {
            get { return m_options; }
        }

        /// <summary>
        /// Function that extensions can call to register a FrameFactory used for all new processes
        /// </summary>
        /// <param name="provider">A delegate that creates a new FrameFactory</param>
        /// <param name="updateExistingProcesses">If set, all currently debugged programs will be refreshed with new FrameFactory
        /// from the supplied provider.</param>
        public void RegisterDefaultStackWalkingFrameFactoryProvider(StackWalkingFrameFactoryProvider provider,
                                                                    bool updateExistingProcesses)
        {
            m_defaultStackWalkingFrameFactoryProvider = provider;
            if (updateExistingProcesses)
            {
                foreach (MDbgProcess p in Processes)
                {
                    // force reloading of new frame factories...
                    p.Threads.FrameFactory = null;
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////////////
        //
        // Controlling Commands
        //
        //////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// creates a new debugged process.
        /// </summary>
        /// <param name="commandLine">The command to run.</param>
        /// <param name="commandArguments">The arguments for the command.</param>
        /// <param name="debugMode">The debug mode to run with.</param>
        /// <param name="deeVersion">The version of debugging interfaces that should be used for
        ///   debuging of the started program. If this value is null, the default (latest) version
        ///   of interface is used.
        /// </param>
        /// <returns>The resulting MDbgProcess.</returns>
        public MDbgProcess CreateProcess(string commandLine, string commandArguments,
                                         DebugModeFlag debugMode, string deeVersion)
        {
            MDbgProcess p = m_processMgr.CreateLocalProcess(deeVersion);
            p.DebugMode = debugMode;
            p.CreateProcess(commandLine, commandArguments);
            return p;
        }

        /// <summary>
        /// Attach to a process with the given Process ID
        /// </summary>
        /// <param name="processId">The Process ID to attach to.</param>
        /// <returns>The resulting MDbgProcess.</returns>
        public MDbgProcess Attach(int processId)
        {
            string deeVersion;
            try
            {
                deeVersion = CorDebugger.GetDebuggerVersionFromPid(processId);
            }
            catch
            {
                // GetDebuggerVersionFromPid isn't implemented on Win9x and so will
                // throw NotImplementedException.  We'll also get an ArgumentException
                // if the specified process doesn't have the CLR loaded yet.  
                // Rather than be selective (and potentially brittle), we'll handle all errors.
                // Ideally we'd assert (or log) that we're only getting the errors we expect,
                // but it's complex and ugly to do that in C#.

                // Fall back to guessing the version based on the filename.
                // Environment variables (eg. COMPLUS_Version) may have resulted 
                // in a different choice.
                try
                {
                    var cp = new CorPublish.CorPublish();
                    CorPublishProcess cpp = cp.GetProcess(processId);
                    string programBinary = cpp.DisplayName;

                    deeVersion = CorDebugger.GetDebuggerVersionFromFile(programBinary);
                }
                catch
                {
                    // This will also fail if the process doesn't have the CLR loaded yet.
                    // It could also fail if the image EXE has been renamed since it started.
                    // For whatever reason, fall back to using the default CLR
                    deeVersion = null;
                }
            }

            MDbgProcess p = m_processMgr.CreateLocalProcess(deeVersion);
            p.Attach(processId);
            return p;
        }


        //////////////////////////////////////////////////////////////////////////////////
        //
        // Info about debugger process
        //
        //////////////////////////////////////////////////////////////////////////////////
    }
}
