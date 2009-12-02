//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------
using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using O2.Debugger.Mdbg.Debugging.CorDebug;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;
using O2.Debugger.Mdbg.Debugging.MdbgEngine;
using O2.Debugger.Mdbg.mdbg;
using O2.Debugger.Mdbg.O2Debugger;
using O2.Debugger.Mdbg.O2Debugger.Objects;
using O2.Debugger.Mdbg.OriginalMdbgCode.mdbg;

namespace O2.Debugger.Mdbg.Tools.Mdbg
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    public class MDbgShell : IMDbgShell
    {
        private readonly IMDbgCommandCollection m_commands = new MDbgCommandSetCollection();
        private readonly IMDbgFileLocator m_fileLocator = new MDbgFileLocator();
        private readonly Hashtable m_properties = new Hashtable();
        private readonly MDbgSourceFileMgr m_sourceFileMgr = new MDbgSourceFileMgr();
        private MDbgEngine m_debugger;
        private int m_exitCode; // exit code when we quit.
        private IMDbgIO m_io;
        private bool m_run = true;

        #region IMDbgShell Members

        public IMDbgIO IO
        {
            get { return m_io; }
            set
            {
                Debug.Assert(value != null);
                m_io = value;
            }
        }

        public MDbgEngine Debugger
        {
            get { return m_debugger; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException();
                }
                m_debugger = value;
            }
        }

        public IMDbgCommandCollection Commands
        {
            get { return m_commands; }
        }

        // Quit w/ an exit code.
        public void QuitWithExitCode(int exitCode)
        {
            m_run = false;
            m_exitCode = exitCode;
        }

        public IMDbgFileLocator FileLocator
        {
            get { return m_fileLocator; }
        }

        public IMDbgSourceFileMgr SourceFileMgr
        {
            get { return m_sourceFileMgr; }
        }

        public IDictionary Properties
        {
            get { return m_properties; }
        }

        /// <summary>
        /// Get the default Breakpoint parser for this collection.
        /// </summary>
        /// <remarks> The breakpoint collection maintains a default breakpoint parser. 
        /// Extensions can get the parser so that they can use the share the parsing implementation to get the 
        /// same breakpoint syntax as the rest of the shell. This encourages a uniform breakpoint syntax.
        /// Extensions can also set the parser so that they can override and even extend the breakpoint syntax.
        /// This may be null (and is in fact null by default), though it is reasonable for extensions to 
        /// expect that the shell supplies a parser.
        /// </remarks>
        public IBreakpointParser BreakpointParser { get; set; }

        public IExpressionParser ExpressionParser { get; set; }


        public event CommandExecutedEventHandler OnCommandExecuted;

        public virtual void DisplayCurrentLocation()
        {
            if (!Debugger.Processes.HaveActive)
            {
                CommandBase.WriteOutput("STOP: Process Exited");
                return; // don't try to display current location
            }
            else
            {
                if (false == Debugger.Processes.HaveActive)
                    return;
                //Debug.Assert(Debugger.Processes.HaveActive);
                Object stopReason = Debugger.Processes.Active.StopReason;
                if (stopReason == null)
                    return;
                Type stopReasonType = stopReason.GetType();
                if (stopReasonType == typeof (StepCompleteStopReason))
                {
                    // just ignore those
                }
                else if (stopReasonType == typeof (ThreadCreatedStopReason))
                {
                    CommandBase.WriteOutput("STOP: Thread Created");
                }
                else if (stopReasonType == typeof (BreakpointHitStopReason))
                {
                    MDbgBreakpoint b = (stopReason as BreakpointHitStopReason).Breakpoint;
                    if (b.Number == 0) // specal case to keep compatibility with our test scripts.
                    {
                        CommandBase.WriteOutput("STOP: Breakpoint Hit");
                    }
                    else
                    {                        
                        CommandBase.WriteOutput(String.Format(CultureInfo.InvariantCulture, "STOP: Breakpoint {0} Hit",
                                                              new Object[] {b.Number}));
                    }
                }

                else if (stopReasonType == typeof (ExceptionThrownStopReason))
                {
                    var ex = (ExceptionThrownStopReason) stopReason;
                    CommandBase.WriteOutput("STOP: Exception thrown");
                    PrintCurrentException();
                    if (Debugger.Options.StopOnExceptionEnhanced
                        || ex.ExceptionEnhancedOn)
                    {
                        // when we are in ExceptionEnhanced mode, we print more information
                        CommandBase.WriteOutput("\tOffset:    " + ex.Offset);
                        CommandBase.WriteOutput("\tEventType: " + ex.EventType);
                        CommandBase.WriteOutput("\tIntercept: " + (ex.Flags != 0));
                    }
                }

                else if (stopReasonType == typeof (UnhandledExceptionThrownStopReason))
                {
                    CommandBase.WriteOutput("STOP: Unhandled Exception thrown");
                    PrintCurrentException();
                    CommandBase.WriteOutput("");
                    CommandBase.WriteOutput("This is unhandled exception, continuing will end the process");
                }

                else if (stopReasonType == typeof (ExceptionUnwindStopReason))
                {
                    CommandBase.WriteOutput("STOP: Exception unwind");
                    CommandBase.WriteOutput("EventType: " + (stopReason as ExceptionUnwindStopReason).EventType);
                }

                else if (stopReasonType == typeof (ModuleLoadedStopReason))
                {
                    CommandBase.WriteOutput("STOP: Module loaded: " +
                                            (stopReason as ModuleLoadedStopReason).Module.CorModule.Name);
                }
                else if (stopReasonType == typeof (AssemblyLoadedStopReason))
                {
                    CommandBase.WriteOutput("STOP: Assembly loaded: " +
                                            (stopReason as AssemblyLoadedStopReason).Assembly.Name);
                }
                else if (stopReasonType == typeof (MDANotificationStopReason))
                {
                    CorMDA mda = (stopReason as MDANotificationStopReason).CorMDA;

                    CommandBase.WriteOutput("STOP: MDANotification");
                    CommandBase.WriteOutput("Name=" + mda.Name);
                    CommandBase.WriteOutput("XML=" + mda.XML);
                }
                else if (stopReasonType == typeof (MDbgErrorStopReason))
                {
                    CommandBase.WriteOutput("STOP: MdbgErorr: " +
                                            (stopReason as MDbgErrorStopReason).ExceptionThrown.Message);
#if DEBUG
                    CommandBase.WriteOutput((stopReason as MDbgErrorStopReason).ExceptionThrown.ToString());
#endif
                }
                else
                {
                    CommandBase.WriteOutput("STOP " + Debugger.Processes.Active.StopReason);
                }
            }

            if (!Debugger.Processes.Active.Threads.HaveActive)
            {
                return; // we won't try to show current location
            }

            MDbgThread thr = Debugger.Processes.Active.Threads.Active;

            //DC 
            DI.o2MDbg.currentLocation = new O2MDbgCurrentLocation(thr);            

            MDbgSourcePosition pos = thr.CurrentSourcePosition;
            if (pos == null)
            {
                MDbgFrame f = thr.CurrentFrame;
                if (f.IsManaged)
                {
                    CorDebugMappingResult mappingResult;
                    uint ip;
                    f.CorFrame.GetIP(out ip, out mappingResult);
                    string s = "IP: " + ip + " @ " + f.Function.FullName + " - " + mappingResult;
                    CommandBase.WriteOutput(s);
                }
                else
                {
                    CommandBase.WriteOutput("<Located in native code.>");
                }
            }
            else
            {
                string fileLoc = FileLocator.GetFileLocation(pos.Path);
                if (fileLoc == null)
                {
                    // Using the full path makes debugging output inconsistant during automated test runs.
                    // For testing purposes we'll get rid of them.
                    //CommandBase.WriteOutput("located at line "+pos.Line + " in "+ pos.Path);
                    CommandBase.WriteOutput("located at line " + pos.Line + " in " + Path.GetFileName(pos.Path));
                }
                else
                {
                    IMDbgSourceFile file = SourceFileMgr.GetSourceFile(fileLoc);
                    string prefixStr = pos.Line.ToString(CultureInfo.InvariantCulture) + ":";

                    if (pos.Line < 1 || pos.Line > file.Count)
                    {
                        CommandBase.WriteOutput("located at line " + pos.Line + " in " + pos.Path);
                        throw new MDbgShellException(
                            string.Format("Could not display current location; file {0} doesn't have line {1}.",
                                          file.Path, pos.Line));
                    }
                    Debug.Assert((pos.Line > 0) && (pos.Line <= file.Count));
                    string lineContent = file[pos.Line];

                    if (pos.StartColumn == 0 && pos.EndColumn == 0
                        || !(CommandBase.Shell.IO is IMDbgIO2)) // or we don't have support for IMDbgIO2
                    {
                        // we don't know location in the line
                        CommandBase.Shell.IO.WriteOutput(MDbgOutputConstants.StdOutput, prefixStr + lineContent + "\n");
                    }
                    else
                    {
                        int hiStart;
                        if (pos.StartColumn > 0)
                        {
                            hiStart = pos.StartColumn - 1;
                        }
                        else
                        {
                            hiStart = 0;
                        }

                        int hiLen;
                        if (pos.EndColumn == 0 // we don't know ending position
                            || (pos.EndLine > pos.StartLine)) // multi-line statement, select whole 1st line
                        {
                            hiLen = lineContent.Length;
                        }
                        else
                        {
                            hiLen = pos.EndColumn - 1 - hiStart;
                        }
                        Debug.Assert(CommandBase.Shell.IO is IMDbgIO2); // see if condition above
                        (CommandBase.Shell.IO as IMDbgIO2).WriteOutput(MDbgOutputConstants.StdOutput,
                                                                       prefixStr + lineContent + "\n",
                                                                       hiStart + prefixStr.Length, hiLen);
                    }
                }
            }
        }


        public virtual void ReportException(Exception e)
        {
            if (e == null)
            {
                // If we don't know anything about the exception, report a standard error
                // This should only be used for handling Non-CLS compliant exceptions.  The
                // FxCop rule CatchNonClsCompliantExceptionsInGeneralHandlers requires that
                // we catch non-CLS exceptions whenever we are catching all other exceptions.
                // Ideally, we should just be more selective in the errors we throw and catch
                IO.WriteOutput(MDbgOutputConstants.StdError, "An Unknown error has occurred.\n");
            }
            else if (e is ThreadInterruptedException)
            {
                IO.WriteOutput(MDbgOutputConstants.StdError, "Interrupted!\n");
            }
            else
            {
                IO.WriteOutput(MDbgOutputConstants.StdError, e.GetBaseException().Message + "\n");
                if (CommandBase.AssertOnErrors)
                {
                    IO.WriteOutput(MDbgOutputConstants.StdError, e.GetBaseException() + "\n");
                    Debug.Assert(false, "Failure. Assert on Error turned on (mode ae on)\n" + e.GetBaseException());
                }
            }
        }

        #endregion

        public int Start(string[] commandLineArguments)
        {
            Init(commandLineArguments);

            PrintStartupLogo();
            //return RunInputLoop();
            return 0;
        }

        //////////////////////////////////////////////////////////////////////////////////
        //
        // Customization methods (to be overriden in aditional skins).
        //
        //////////////////////////////////////////////////////////////////////////////////

        protected virtual void Init(string[] commandLineArguments)
        {
            string[] initialCommands = null;

            // process startup commands
            if (commandLineArguments.Length != 0)
            {
                var startupCommands = new ArrayList();
                if (commandLineArguments[0].Length > 1 && commandLineArguments[0][0] == '!')
                {
                    // ! commands on command line
                    int i = 0;
                    while (i < commandLineArguments.Length)
                    {
                        var sb = new StringBuilder();
                        Debug.Assert(commandLineArguments[i][0] == '!');
                        sb.Append(commandLineArguments[i].Substring(1));
                        ++i;
                        while (i < commandLineArguments.Length &&
                               !(commandLineArguments[i].Length > 1 && commandLineArguments[i][0] == '!'))
                        {
                            sb.Append(' ');
                            sb.Append(commandLineArguments[i]);
                            ++i;
                        }
                        startupCommands.Add(sb.ToString());
                    }
                }
                else
                {
                    // it is name of executable on the command line
                    var sb = new StringBuilder("run");
                    for (int i = 0; i < commandLineArguments.Length; i++)
                    {
                        sb.Append(' ');
                        string arg = commandLineArguments[i];
                        if (arg.IndexOf(' ') != -1)
                        {
                            // argument contains spaces, need to quote it
                            sb.Append('\"').Append(arg).Append('\"');
                        }
                        else
                        {
                            sb.Append(arg);
                        }
                    }
                    startupCommands.Add(sb.ToString());
                }

                initialCommands = (string[]) startupCommands.ToArray(typeof (string));
            }


            IO = new MDbgIO(this, initialCommands);

            CommandBase.Shell = this;

            m_debugger = new MDbgEngine();
            MdbgCommands.Initialize();

            OnCommandExecuted += MdbgCommands.WhenHandler;

            LoadPlatformDependentExtension();

            ProcessAutoExec();
        }

        protected virtual void PrintStartupLogo()
        {
            string myVersion = GetBinaryVersion();

            IO.WriteOutput(MDbgOutputConstants.StdOutput,
                           "Welcome to O2's version of Microsoft's sample app Mdbg (CLR Managed Debugger) " +
                           Environment.NewLine + Environment.NewLine +
                           "by Dinis Cruz, Feb 29 " + Environment.NewLine +
                           "Http://www.o2-ounceopen.com" + Environment.NewLine + Environment.NewLine);

            IO.WriteOutput(MDbgOutputConstants.StdOutput,
                           "MDbg (Managed debugger) v" + myVersion + " started.\n");
            /*IO.WriteOutput(MDbgOutputConstants.StdOutput,
                           "Copyright (C) Microsoft Corporation. All rights reserved.\n");*/
            IO.WriteOutput(MDbgOutputConstants.StdOutput,
                           "\nFor information about commands type \"help\";\nto exit program type \"quit\".\n\n");

            // Check for and output any debugging warnings
            try
            {
                Debugger.Processes.DefaultLocalDebugger.CanLaunchOrAttach(0, false);
            }
            catch (COMException e)
            {
                IO.WriteOutput(MDbgOutputConstants.StdOutput, "WARNING: " + e.Message + "\n\n");
            }
        }

        //////////////////////////////////////////////////////////////////////////////////
        //
        // Helper methods
        //
        //////////////////////////////////////////////////////////////////////////////////

        protected static string GetBinaryVersion()
        {
            string assemblyName = Assembly.GetAssembly(typeof (MDbgShell)).Location;
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assemblyName);
            string myVersion = fvi.FileVersion;
            return myVersion;
        }

        // loads extension MdbgDis located next to the debugger.
        protected static bool LoadPlatformDependentExtension()
        {
            const string NativeDasmExtName = "Mdbgdis.dll";

            string extdir = AppDomain.CurrentDomain.BaseDirectory;

            string path = extdir + Path.DirectorySeparatorChar + NativeDasmExtName;
            if (!File.Exists(path))
            {
                return false;
            }

            Assembly asext = null;
            try
            {
                asext = Assembly.LoadFrom(path);
            }
            catch (FileLoadException)
            {
                // we could not load the disassembler.
                return false;
            }

            Debug.Assert(asext != null);


            const string ExtMethod = "LoadExtensionSilently";
            const string ExtNameSpace = "Microsoft.Samples.Tools.Mdbg.Extension.";

            string assemblyName = asext.GetName().Name;
            // get assemblyName into the correct format (see "load" command for more details")
            assemblyName = assemblyName.Substring(0, 1).ToUpper(CultureInfo.CurrentUICulture)
                           + assemblyName.Substring(1, assemblyName.Length - 1).ToLower(CultureInfo.CurrentUICulture);

            string extType = ExtNameSpace + assemblyName + "Extension";

            Type ext = asext.GetType(extType);

            if (ext == null)
                return false; // we could not load it

            MethodInfo mi = ext.GetMethod(ExtMethod);
            if (mi == null)
                return false; // we could not load it
            mi.Invoke(null, null);
            return true;
        }

        //////////////////////////////////////////////////////////////////////////////////
        //
        // Private implementation
        //
        //////////////////////////////////////////////////////////////////////////////////
        /* // NOT USED by O2MDbg
        private int RunInputLoop()
        {
            // Run the event loop
            string input;
            IMDbgCommand cmd = null;
            string cmdArgs = null;

            int stopCount = -1;

            while (true)
            {
                Thread.Sleep(1000);
                Debug.WriteLine("MSdn Shell, waiting for commands");
            }
            
            while (m_run && IO.ReadCommand(out input))
            {
                try
                {
                    if (Debugger.Processes.HaveActive)
                    {
                        stopCount = Debugger.Processes.Active.StopCounter;
                    }

                    if (input.Length == 0)
                    {
                        if (cmd == null || !cmd.IsRepeatable)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        Commands.ParseCommand(input, out cmd, out cmdArgs);
                    }
                    cmd.Execute(cmdArgs);

                    int newStopCount = Debugger.Processes.HaveActive
                                           ? Debugger.Processes.Active.StopCounter
                                           : Int32.MaxValue;
                    bool movementCommand = newStopCount > stopCount;
                    stopCount = newStopCount;

                    if (OnCommandExecuted != null)
                    {
                        OnCommandExecuted(this, new CommandExecutedEventArgs(this, cmd, cmdArgs, movementCommand));
                    }

                    newStopCount = Debugger.Processes.HaveActive
                                       ? Debugger.Processes.Active.StopCounter
                                       : Int32.MaxValue;
                    movementCommand = newStopCount > stopCount;

                    while (newStopCount > stopCount)
                    {
                        stopCount = newStopCount;

                        if (OnCommandExecuted != null)
                        {
                            OnCommandExecuted(this, new CommandExecutedEventArgs(this, null, null, movementCommand));
                        }

                        newStopCount = Debugger.Processes.HaveActive
                                           ? Debugger.Processes.Active.StopCounter
                                           : Int32.MaxValue;
                        movementCommand = newStopCount > stopCount;
                    }
                    stopCount = newStopCount;
                }
                catch (Exception e)
                {
                    ReportException(e);
                }
            } // end while
            return m_exitCode;
        }
         */


        private void PrintCurrentException()
        {
            MDbgValue ex = Debugger.Processes.Active.Threads.Active.CurrentException;
            CommandBase.WriteOutput("Exception=" + ex.GetStringValue(0));
            foreach (MDbgValue f in ex.GetFields())
            {
                string outputType;
                string outputValue;

                if (f.Name == "_xptrs" || f.Name == "_xcode" || f.Name == "_stackTrace" ||
                    f.Name == "_remoteStackTraceString" || f.Name == "_remoteStackIndex" ||
                    f.Name == "_exceptionMethodString")
                {
                    outputType = MDbgOutputConstants.Ignore;
                }
                else
                {
                    outputType = MDbgOutputConstants.StdOutput;
                }

                outputValue = f.GetStringValue(0);
                // remove new line characters in string
                if (outputValue != null && (f.Name == "_exceptionMethodString" || f.Name == "_remoteStackTraceString"))
                {
                    outputValue = outputValue.Replace('\n', '#');
                }

                CommandBase.WriteOutput(outputType, "\t" + f.Name + "=" + outputValue);
            }
        }


        private void ProcessAutoExec()
        {
            const string autoLoadListFileName = "mdbgAutoExec.txt";
            string mdbgDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Common case is if the file does not exist, so check that first to avoid an exception.
            string filename = mdbgDirectory + Path.DirectorySeparatorChar + autoLoadListFileName;
            if (!File.Exists(filename))
            {
                return;
            }

            StreamReader sr = null;

            try
            {
                sr = new StreamReader(filename);

                string s = sr.ReadLine();
                while (s != null)
                {
                    try
                    {
                        if ((s.Length != 0)
                            && !s.StartsWith("#"))
                            CommandBase.ExecuteCommand(s);
                    }
                    catch (Exception e)
                    {
                        ReportException(e);
                    }
                    s = sr.ReadLine();
                }
            }
            catch (IOException ex)
            {
                // we'll ignore IO exceptions
                ReportException(ex);
            }
            finally
            {
                if (sr != null)
                    sr.Close(); // free resources in advance
            }
        }

        //////////////////////////////////////////////////////////////////////////////////
        //
        // Variables
        //
        //////////////////////////////////////////////////////////////////////////////////
    }


    //////////////////////////////////////////////////////////////////////////////////
    //
    // MDbgOutputImpl
    //
    //////////////////////////////////////////////////////////////////////////////////

    public class MDbgIO : IMDbgIO, IMDbgIO2, IMDbgIO3
    {
        #region HighlighType enum

        public enum HighlighType
        {
            StatementLocation,
            Error,
            None
        }

        #endregion

        private const string TEXT_ERROR = "Error: ";
        private const string TEXT_PROMPT_STRING = "mdbg> ";
        private readonly MDbgShell m_shell;
        protected bool m_isConsoleBreakHandlerExecuted;
        protected Queue m_startupCommands;

        public MDbgIO(MDbgShell shell, string[] startupCommands)
        {
            Debug.Assert(shell != null);
            m_shell = shell;

            Console.CancelKeyPress += ConsoleBreakHandler;

            if (startupCommands != null && startupCommands.Length > 0)
            {
                m_startupCommands = new Queue(startupCommands);
            }
        }

        #region IMDbgIO Members

        public virtual void WriteOutput(string outputType, string text)
        {
            WriteOutput(outputType, text, 0, 0);
        }

        public virtual bool ReadCommand(out string cmd)
        {
            if (m_startupCommands != null)
            {
                var c = (string) m_startupCommands.Dequeue();
                if (m_startupCommands.Count == 0)
                {
                    m_startupCommands = null;
                }
                cmd = c;
                WriteOutput(MDbgOutputConstants.StdOutput, c + "\r\n");
                return true;
            }

            retry:
            m_isConsoleBreakHandlerExecuted = false;
            WriteMdbgPrompt(true);
            cmd = Console.ReadLine();
            if (cmd == null)
            {
                Thread.Sleep(100);

                if (m_isConsoleBreakHandlerExecuted)
                    // means we have not hit an EOF.
                    goto retry;
            }

            return (cmd == null ? false : true);
        }

        #endregion

        #region IMDbgIO2 Members

        public virtual void WriteOutput(string outputType, string text, int hilightStart, int hilightLen)
        {
            Debug.Assert(null != outputType && null != text);

            if (outputType.Equals(MDbgOutputConstants.StdError))
            {
                string s = TEXT_ERROR + text;
                OriginalMDbgMessages.Display(s, HighlighType.Error, 0, s.Length);
            }
            else
            {
                Debug.Assert(hilightStart >= 0);
                Debug.Assert(hilightLen >= 0);
                if (hilightStart < 0)
                {
                    hilightStart = 0;
                }
                if (hilightLen < 0)
                {
                    hilightLen = 0;
                }
                OriginalMDbgMessages.Display(text, HighlighType.StatementLocation, hilightStart, hilightLen);
            }
        }

        #endregion

        #region IMDbgIO3 Members

        public virtual ConsoleKeyInfo ReadKey(bool intercept)
        {
            return Console.ReadKey(intercept);
        }

        public virtual string ReadLine()
        {
            return Console.ReadLine();
        }

        #endregion

        //////////////////////////////////////////////////////////////////////////////////
        //
        // Private Implementation part
        //
        //////////////////////////////////////////////////////////////////////////////////

        private void ConsoleBreakHandler(Object sender, ConsoleCancelEventArgs e)
        {
            // When Control+C is pressed, the Console.Readline() returns immediatelly with
            // null. There is no way how to distinguish between EOF and Ctrl+C. Therefore we set
            // a m_isConsoleBreakHandlerExecuted to true whenever the handler is executed.
            // The code around Console.ReadLine() check when null is returned if the
            // m_isConsoleBreakHandlerExecuted flag is set. If yes, we know that this is caused by
            // Ctrl+C, the code clrears the flag and repeats the read.

            m_isConsoleBreakHandlerExecuted = true;

            switch (e.SpecialKey)
            {
                case ConsoleSpecialKey.ControlBreak:
                    Console.WriteLine();
                    WriteOutput(MDbgOutputConstants.StdError,
                                "Immediate debugger termination reqested through <Ctrl+Break>");
                    WriteOutput(MDbgOutputConstants.StdError, "To break into debugger use Ctrl+C instead.");
                    //
                    // When ControlBreak is pressed, we cannot set e.Cancel=true....
                    // 
                    break;
                case ConsoleSpecialKey.ControlC:
                    try
                    {
                        WriteOutput(MDbgOutputConstants.StdOutput, "\n<Ctrl+C>");
                        MDbgProcess p = m_shell.Debugger.Processes.Active;
                        if (p.IsRunning)
                        {
                            p.AsyncStop().WaitOne();
                        }
                    }
                    catch
                    {
                    }
                    e.Cancel = true;
                    break;
                default:
                    break;
            }
        }

        private void WriteMdbgPrompt(bool processStopped)
        {
            string s;
            if (processStopped)
            {
                MDbgProcess p = null;
                if (m_shell.Debugger.Processes.HaveActive)
                {
                    p = m_shell.Debugger.Processes.Active;
                    if (p.Threads.HaveActive)
                    {
                        s = "[p#:" + p.Number + ", t#:" + p.Threads.Active.Number + "] ";
                    }
                    else
                    {
                        s = "[p#:" + p.Number + ", t#:no active thread] ";
                    }
                }
                else
                {
                    s = "";
                }
            }
            else
            {
                s = "[process running] ";
            }
            Console.Write(s + TEXT_PROMPT_STRING);
        }
    }

    //////////////////////////////////////////////////////////////////////////////////
    //
    // MDbgCommandSetImpl
    //
    //////////////////////////////////////////////////////////////////////////////////

    public class MDbgCommandSetCollection : IMDbgCommandCollection
    {
        private readonly ArrayList m_commands = new ArrayList();
        private bool m_needSorting;

        #region IMDbgCommandCollection Members

        public void Add(IMDbgCommand command)
        {
            if (command == null)
            {
                Debug.Assert(false);
                throw new Exception();
            }

            if (command.MinimumAbbrev > command.CommandName.Length)
            {
                throw new MDbgShellException("Cannot add command '" + command.CommandName + "'. Abbreviation is " +
                                             command.MinimumAbbrev + " characters. Can't be more than " +
                                             command.CommandName.Length);
            }


            // extensions are allowed to override a command, i.e. this means we will delete previously defined command
            // with such name.
            foreach (IMDbgCommand c in m_commands)
            {
                if (c.CommandName.Equals(command.CommandName))
                {
                    m_commands.Remove(c);
                    break; // there should never be 2 command with same name in a collection
                    // therefore we can break-out from the search.
                }
            }

            m_commands.Add(command);
            m_needSorting = true;
        }

        public IMDbgCommand Lookup(string commandName)
        {
            var al = new ArrayList();
            foreach (IMDbgCommand c in m_commands)
            {
                if (commandName.Length < c.MinimumAbbrev)
                {
                    continue;
                }
                if (String.Compare(c.CommandName, 0, commandName, 0, commandName.Length, true,
                                   CultureInfo.CurrentUICulture // command names could get localized in the future
                        ) == 0)
                {
                    al.Add(c);
                }
            }
            if (al.Count == 0)
            {
                throw new MDbgShellException("Command '" + commandName + "' not found.");
            }
            else if (al.Count == 1)
            {
                return (IMDbgCommand) al[0];
            }
            else
            {
                var s = new StringBuilder("Command prefix too short. \nPossible completitions:");
                foreach (IMDbgCommand c in al)
                {
                    s.Append("\n").Append(c.CommandName);
                }
                throw new MDbgShellException(s.ToString());
            }
        }

        public IEnumerator GetEnumerator()
        {
            if (m_needSorting)
            {
                m_needSorting = false;
                m_commands.Sort();
            }

            return m_commands.GetEnumerator();
        }

        public void ParseCommand(string commandLineText, out IMDbgCommand command, out string commandArguments)
        {
            try
            {
                commandLineText = commandLineText.Trim();
                int n = commandLineText.Length;
                int i = 0;
                while (i < n && !Char.IsWhiteSpace(commandLineText, i))
                {
                    i++;
                }
                string cmdName = commandLineText.Substring(0, i);
                commandArguments = commandLineText.Substring(i).Trim();
                command = Lookup(cmdName);
            }
            catch (Exception ex)
            {
                OriginalMDbgMessages.WriteLine("In ParseCommand, Exception while executing " + commandLineText + "    :   " +
                                      ex.Message + "\n\n" + ex.StackTrace);
                command = null;
                commandArguments = null;
            }
        }

        #endregion
    }


    //////////////////////////////////////////////////////////////////////////////////
    //
    // MDbgFileLocatorImpl
    //
    //////////////////////////////////////////////////////////////////////////////////

    public class MDbgFileLocator : IMDbgFileLocator
    {
        private readonly Hashtable m_fileLocations = new Hashtable();
        private string m_srcPath;

        internal MDbgFileLocator()
        {
        }

        #region IMDbgFileLocator Members

        public string Path
        {
            get { return m_srcPath; }
            set
            {
                foreach (string pathPart in GetPathComponents(value))
                {
                    if (!Directory.Exists(pathPart))
                    {
                        throw new MDbgShellException("path doesn't exist: '" + pathPart + "'");
                    }
                }
                m_srcPath = value;
                m_fileLocations.Clear();
            }
        }

        public string GetFileLocation(string file)
        {
            string idx = String.Intern(file);
            if (m_fileLocations.Contains(idx))
            {
                return (string) m_fileLocations[idx];
            }

            string realPath = null;

            if (File.Exists(file))
            {
                realPath = file;
            }
            else
            {
                foreach (string p in GetPathComponents(m_srcPath))
                {
                    // can't modify foreach iteration variables directly
                    string pathPart = p;

                    // ensure the path has a trailing \, but don't add one if it's already there
                    // (otherwise we may get \\ which is especially bad at the beginning)
                    // Note that it would be more efficient to do this at path set time instead of every
                    // time we use it, but then that would change some output and break a bunch of tests.
                    if (!pathPart.EndsWith(new String(System.IO.Path.DirectorySeparatorChar, 1)))
                    {
                        pathPart += System.IO.Path.DirectorySeparatorChar;
                    }

                    string filePath = file;
                    do
                    {
                        realPath = pathPart + filePath;
                        if (File.Exists(realPath))
                            goto Found;

                        int i = filePath.IndexOfAny(new[]
                                                        {
                                                            System.IO.Path.DirectorySeparatorChar,
                                                            System.IO.Path.AltDirectorySeparatorChar
                                                        });
                        if (i != -1)
                            filePath = filePath.Substring(i + 1);
                        else
                            break;
                    } while (true);
                }
                realPath = null;
            }
            Found:
            m_fileLocations.Add(idx, realPath);
            return realPath;
        }

        public void Associate(string originalName, string newName)
        {
            Debug.Assert(originalName != null && originalName.Length > 0);
            Debug.Assert(newName != null && newName.Length > 0);

            string idx = String.Intern(originalName);
            if (m_fileLocations.Contains(idx))
                m_fileLocations.Remove(idx);
            m_fileLocations.Add(idx, newName);
        }

        #endregion

        protected string[] GetPathComponents(string path)
        {
            if (path == null)
            {
                return new string[0];
            }
            string[] strs = path.Split(new[] {';'});
            return strs;
        }
    }
}