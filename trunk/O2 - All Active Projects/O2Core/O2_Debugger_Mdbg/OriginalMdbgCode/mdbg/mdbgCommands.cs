//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using O2.Debugger.Mdbg.Debugging.CorDebug;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;
using O2.Debugger.Mdbg.Debugging.CorPublish;
using O2.Debugger.Mdbg.Debugging.MdbgEngine;
using O2.Debugger.Mdbg.OriginalMdbgCode.Mdbg;
using O2.Debugger.Mdbg.Tools.Mdbg;

namespace O2.Debugger.Mdbg.OriginalMdbgCode.mdbg
{
    public sealed class MdbgCommands : CommandBase
    {
        public static string g_lastSavedRunCommand;
        public static ResourceManager g_rm;
        public static ThreadNickNames g_threadNickNames;
        public static ArrayList m_events;
        public static MDbgSymbolCache SymbolCache;

        // Initialize this extension. We can only initialize global (non-process specific) state right now
        // since we may not have an active process.
        // However, we can subscribe to the ProcessAdded event to register per-process state.
        public static void Initialize()
        {
            //g_rm = new System.Resources.ResourceManager("mdbgCommands",System.Reflection.Assembly.GetExecutingAssembly());
            g_rm = new ResourceManager(typeof (mdbgCommands));

            // DC: function helper when moving functions to mdbgCommandsCustomizedForO2
            // , O2Thread.FuncVoid<string> o2Callback)
            //O2.Debugger.Mdbg.OriginalMdbgCode.mdbg.mdbgCommandsCustomizedForO2


            CommandDescriptionAttribute.RegisterResourceMgr(typeof (MdbgCommands), g_rm);
            MDbgAttributeDefinedCommand.AddCommandsFromType(Shell.Commands, typeof (MdbgCommands));

            g_lastSavedRunCommand = null;
            g_threadNickNames = new ThreadNickNames();
            SymbolCache = new MDbgSymbolCache();
            m_events = new ArrayList();

            // initialization of various properties
            InitModeShellProperty();
            InitStopOptionsProperty();

            Debug.Assert(Shell.Debugger != null);

            // Install our default breakpoint parser.
            if (Shell.BreakpointParser == null)
            {
                Shell.BreakpointParser = new DefaultBreakpointParser();
            }
            if (Shell.ExpressionParser == null)
            {
                Shell.ExpressionParser = new DefaultExpressionParser();
            }

            // We could subscribe to process-specific event handlers via the the Shell.Debugger.Processes.ProcessAdded event.
            Shell.Debugger.Processes.ProcessAdded += Processes_ProcessAdded;
        }

        private static void Processes_ProcessAdded(object sender, ProcessCollectionChangedEventArgs e)
        {
            e.Process.PostDebugEvent += PostDebugEventHandler;
        }

        private static void PostDebugEventHandler(object sender, CustomPostCallbackEventArgs e)
        {
            var stopOptions = Shell.Properties[MDbgStopOptions.PropertyName]
                              as MDbgStopOptions;

            stopOptions.ActOnCallback(sender as MDbgProcess, e);
        }

        [CommandDescription(
                CommandName = "exit",
                MinimumAbbrev = 2,
                IsRepeatable = false,
                ResourceManagerKey = typeof (MdbgCommands),
                UseHelpFrom = "quit"
                ),
            CommandDescription(
                CommandName = "quit",
                MinimumAbbrev = 1,
                IsRepeatable = false,
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void QuitCmd(string arguments)
        {
            // Look for optional exit code.
            var ap = new ArgParser(arguments);

            int exitCode;
            if (ap.Exists(0))
            {
                exitCode = ap.AsInt(0);
            }
            else
            {
                exitCode = 0;
            }

            // we cannot modify the collection during enumeration, so
            // we need to collect all processes to kill in advance.
            var processesToKill = new ArrayList();
            foreach (MDbgProcess p in Debugger.Processes)
            {
                processesToKill.Add(p);
            }

            foreach (MDbgProcess p in processesToKill)
            {
                if (p.IsAlive)
                {
                    Debugger.Processes.Active = p;
                    WriteOutput("Terminating current process...");
                    try
                    {
                        p.Kill().WaitOne();
                    }
                    catch
                    {
                        // some processes cannot be killed (e.g. the one that have not loaded runtime)
                        try
                        {
                            Process np = Process.GetProcessById(p.CorProcess.Id);
                            np.Kill();
                        }
                        catch
                        {
                        }
                    }
                }
            }

            Shell.QuitWithExitCode(exitCode);
        }


        [
            CommandDescription(
                CommandName = "?",
                MinimumAbbrev = 1,
                IsRepeatable = false,
                ResourceManagerKey = typeof (MdbgCommands),
                UseHelpFrom = "help"
                ),
            CommandDescription(
                CommandName = "help",
                MinimumAbbrev = 1,
                IsRepeatable = false,
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void HelpCmd(string arguments)
        {
            if (arguments.Length == 0)
            {
                var sb = new StringBuilder(
                    "Following commands are available:\n"
                    );
                Assembly currentSection = null;
                foreach (IMDbgCommand c in Shell.Commands)
                {
                    if (c.LoadedFrom != currentSection)
                    {
                        bool bPrintExtensionName = currentSection != null;

                        currentSection = c.LoadedFrom;
                        if (bPrintExtensionName)
                        {
                            WriteOutput(MDbgOutputConstants.StdOutput, "\nExtension: " + currentSection.GetName().Name,
                                        0, Int32.MaxValue);
                        }
                    }

                    string name;
                    if (c.CommandName.Length != c.MinimumAbbrev)
                    {
                        name = c.CommandName.Substring(0, c.MinimumAbbrev) + "[" +
                               c.CommandName.Substring(c.MinimumAbbrev) + "]";
                    }
                    else
                    {
                        name = c.CommandName;
                    }
                    const int indentSize = 14;
                    if (name.Length <= indentSize)
                    {
                        sb.Append(String.Format(CultureInfo.InvariantCulture, "{0,-" + indentSize + "}{1}",
                                                new Object[] {name, c.ShortHelp}));
                    }
                    else
                    {
                        sb.Append(String.Format(CultureInfo.InvariantCulture, "{0}\n{1,-" + indentSize + "}{2}",
                                                new Object[] {name, " ", c.ShortHelp}));
                    }
                    WriteOutput(sb.ToString());
                    sb.Length = 0; // clear the string builder
                }
            }
            else
            {
                IMDbgCommand c = Shell.Commands.Lookup(arguments);
                string name;
                if (c.CommandName.Length != c.MinimumAbbrev)
                {
                    name = c.CommandName.Substring(0, c.MinimumAbbrev) + "[" + c.CommandName.Substring(c.MinimumAbbrev) +
                           "]";
                }
                else
                {
                    name = c.CommandName;
                }

                string longHelp = "<unavailable>";
                try
                {
                    // Extension may not define help strings.
                    longHelp = c.LongHelp;
                }
                catch (MissingManifestResourceException e)
                {
                    longHelp = "No resource for help. " + e.Message;
                }

                WriteOutput("abbrev: " + name + "\n" + longHelp);
            }
        }

        [
            CommandDescription(
                CommandName = "run",
                MinimumAbbrev = 1,
                IsRepeatable = false,
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void RunCmd(string arguments)
        {
            ArrayList bpLocations = null;

            if (arguments.Length == 0 && g_lastSavedRunCommand != null)
            {
                // we have requested to restart the process.
                // We should kill the existing one.
                if (Debugger.Processes.HaveActive)
                {
                    // save locations of current breakpoints
                    foreach (MDbgBreakpoint b in Debugger.Processes.Active.Breakpoints)
                    {
                        if (bpLocations == null)
                        {
                            bpLocations = new ArrayList();
                        }
                        bpLocations.Add(b.Location);
                    }
                    ExecuteCommand("kill");
                }
                arguments = g_lastSavedRunCommand;
            }

            DebugModeFlag debugMode;
            string programToRun;
            string programArguments;
            string deeVersion;
            PreParseRunArguments(arguments, out debugMode, out deeVersion, out programToRun, out programArguments);

            if (programToRun == null)
            {
                throw new MDbgShellException("Must specify a program to run");
            }

            if (deeVersion == null)
            {
                WriteOutput("Could not detect debuggee version -- using latest debugger API.");
            }

            // create correct process object
            MDbgProcess p = Debugger.Processes.CreateLocalProcess(deeVersion);

            p.DebugMode = debugMode;
            p.CreateProcess(programToRun, programArguments);

            // recreate debugger breakpoints
            if (bpLocations != null)
            {
                foreach (object location in bpLocations)
                {
                    p.Breakpoints.CreateBreakpoint(location);
                }
            }
            p.Go().WaitOne();
            g_lastSavedRunCommand = arguments;

            Shell.DisplayCurrentLocation();
        }

        [
            CommandDescription(
                CommandName = "go",
                MinimumAbbrev = 1,
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void GoCmd(string arguments)
        {
            Debugger.Processes.Active.Go().WaitOne();
            Shell.DisplayCurrentLocation();
        }

        [
            CommandDescription(
                CommandName = "kill",
                MinimumAbbrev = 1,
                IsRepeatable = false,
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void KillCmd(string arguments)
        {
            MDbgProcess p = Debugger.Processes.Active;
            try
            {
                p.Kill().WaitOne();
            }
            catch
            {
                // some processes cannot be killed (e.g. the one that have not loaded runtime)
                try
                {
                    Process np = Process.GetProcessById(p.CorProcess.Id);
                    np.Kill();
                }
                catch
                {
                }
            }

            // Normally, the Jit will consider the end of the lifespan to be the last spot a var is used.
            // However, the debugger can extend variable lifespans for the entire function, so we null out p
            // for debug builds.
            p = null;
            {
                // SYmbolStore interfaces should implement IDisposable pattern
                // this will hopefully release all locked .pdb's
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }


        [
            CommandDescription(
                CommandName = "setip",
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void SetIPCmd(string arguments)
        {
            const string IlOpt = "il";
            var ap = new ArgParser(arguments, IlOpt);
            if (ap.OptionPassed(IlOpt))
            {
                Debugger.Processes.Active.Threads.Active.CurrentFrame.CorFrame.SetIP(ap.AsInt(0));
            }
            else
            {
                int ilOffset;
                if (
                    !Debugger.Processes.Active.Threads.Active.CurrentFrame.Function.GetIPFromLine(ap.AsInt(0),
                                                                                                  out ilOffset))
                {
                    throw new MDbgShellException("cannot find correct function mapping");
                }

                int hresult;
                if (!Debugger.Processes.Active.Threads.Active.CurrentFrame.CorFrame.CanSetIP(ilOffset, out hresult))
                {
                    string reason;
                    switch ((HResult) hresult)
                    {
                        case HResult.CORDBG_S_BAD_START_SEQUENCE_POINT:
                            reason = "Attempt to SetIP from non-sequence point.";
                            break;
                        case HResult.CORDBG_S_BAD_END_SEQUENCE_POINT:
                            reason = "Attempt to SetIP from non-sequence point.";
                            break;
                        case HResult.CORDBG_S_INSUFFICIENT_INFO_FOR_SET_IP:
                            reason = "Insufficient information to fix program flow.";
                            break;
                        case HResult.CORDBG_E_CANT_SET_IP_INTO_FINALLY:
                            reason = "Attempt to SetIP into finally block.";
                            break;
                        case HResult.CORDBG_E_CANT_SET_IP_OUT_OF_FINALLY:
                        case HResult.CORDBG_E_CANT_SET_IP_OUT_OF_FINALLY_ON_WIN64:
                            reason = "Attempt to SetIP out of finally block.";
                            break;
                        case HResult.CORDBG_E_CANT_SET_IP_INTO_CATCH:
                            reason = "Attempt to SetIP into catch block.";
                            break;
                        case HResult.CORDBG_E_CANT_SET_IP_OUT_OF_CATCH_ON_WIN64:
                            reason = "Attempt to SetIP out of catch block.";
                            break;
                        case HResult.CORDBG_E_SET_IP_NOT_ALLOWED_ON_NONLEAF_FRAME:
                            reason = "Attempt to SetIP on non-leaf frame.";
                            break;
                        case HResult.CORDBG_E_SET_IP_IMPOSSIBLE:
                            reason = "The operation cannot be completed.";
                            break;
                        case HResult.CORDBG_E_CANT_SETIP_INTO_OR_OUT_OF_FILTER:
                            reason = "Attempt to SetIP into or out of filter.";
                            break;
                        case HResult.CORDBG_E_SET_IP_NOT_ALLOWED_ON_EXCEPTION:
                            reason = "SetIP is not allowed on exception.";
                            break;
                        default:
                            reason = "Reason unknown.";
                            break;
                    }
                    throw new MDbgShellException("Cannot set IP as requested. " + reason);
                }

                Debugger.Processes.Active.Threads.Active.CurrentFrame.CorFrame.SetIP(ilOffset);
            }
            Debugger.Processes.Active.Threads.RefreshStack();
            Shell.DisplayCurrentLocation();
        }

        [CommandDescription(CommandName = "where",MinimumAbbrev = 1,ResourceManagerKey = typeof (MdbgCommands))]
        public static void WhereCmd(string arguments)
        {
            mdbgCommandsCustomizedForO2.WhereCmd(arguments, null);
        }

        

        [CommandDescription(CommandName = "next", MinimumAbbrev = 1,ResourceManagerKey = typeof (MdbgCommands))]
        public static void NextCmd(string arguments)
        {
            Debugger.Processes.Active.StepOver(false).WaitOne();
            Shell.DisplayCurrentLocation();
        }

        [
            CommandDescription(
                CommandName = "step",
                MinimumAbbrev = 1,
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void StepCmd(string arguments)
        {
            Debugger.Processes.Active.StepInto(false).WaitOne();
            Shell.DisplayCurrentLocation();
        }

        [
            CommandDescription(
                CommandName = "out",
                MinimumAbbrev = 1,
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void StepOutCmd(string arguments)
        {
            Debugger.Processes.Active.StepOut().WaitOne();
            Shell.DisplayCurrentLocation();
        }
        
        [CommandDescription(CommandName = "show",MinimumAbbrev = 2,ResourceManagerKey = typeof (MdbgCommands))]
        public static void ShowCmd(string arguments)
        {
            mdbgCommandsCustomizedForO2.ShowCmd(arguments, null);
        }


        // Default breakpoint parser for the MDbg shell. 
        // We don't expose this class publically, but extensions can grab the interface pointer from the Breakpoint Collection's parser property.
        // end class DefaultBreakpointParser        
        
        // Display current breakpoints
        public static void ListBreakpoints()
        {
            mdbgCommandsCustomizedForO2.ListBreakpoints(null);
        }

        // Add IL-level breakpoints.
        // If no parameters specified, prints the current breakpoint list.
        [CommandDescription(CommandName = "break",MinimumAbbrev = 1,ResourceManagerKey = typeof (MdbgCommands))]
        public static void BreakCmd(string arguments)
        {
            mdbgCommandsCustomizedForO2.BreakCmd(arguments, null);
        }        

        [CommandDescription(CommandName = "delete",MinimumAbbrev = 3,ResourceManagerKey = typeof (MdbgCommands))]
        public static void DeleteCmd(string arguments)
        {
            mdbgCommandsCustomizedForO2.DeleteCmd(arguments, null);
        }

        [CommandDescription(CommandName = "thread",MinimumAbbrev = 1,ResourceManagerKey = typeof (MdbgCommands))]
        public static void ThreadCmd(string arguments)
        {
            const string nickNameStr = "nick";
            var ap = new ArgParser(arguments, nickNameStr + ":1");
            if (ap.Count == 0)
            {
                if (ap.OptionPassed(nickNameStr))
                {
                    g_threadNickNames.SetThreadNickName(ap.GetOption(nickNameStr).AsString,
                                                        Debugger.Processes.Active.Threads.Active);
                }
                else
                {
                    // we want to display active threads
                    MDbgProcess p = Debugger.Processes.Active;

                    WriteOutput("Active threads:");
                    foreach (MDbgThread t in p.Threads)
                    {
                        string stateDescription = GetThreadStateDescriptionString(t);
                        WriteOutput(string.Format("th #:{0} (ID:{1}){2}",
                                                  g_threadNickNames.GetThreadName(t),
                                                  t.Id,
                                                  stateDescription.Length == 0 ? String.Empty : " " + stateDescription));
                    }
                }
            }
            else
            {
                MDbgThread t = g_threadNickNames.GetThreadByNickName(ap.AsString(0));

                if (t == null)
                {
                    throw new MDbgShellException("No such thread");
                }
                Debugger.Processes.Active.Threads.Active = t;

                string currentThreadStateString = GetThreadStateDescriptionString(t);
                if (currentThreadStateString.Length > 0)
                    currentThreadStateString = currentThreadStateString.Insert(0, " ");

                WriteOutput(string.Format(CultureInfo.InvariantCulture, "Current thread is #{0}{1}.",
                                          t.Number, currentThreadStateString));
                Shell.DisplayCurrentLocation();
            }
        }

        private static string GetThreadStateDescriptionString(MDbgThread thread)
        {
            CorThread t = thread.CorThread;
            var debuggerState = new StringBuilder();
            var clientState = new StringBuilder();

            if (t.DebugState == CorDebugThreadState.THREAD_SUSPEND)
                debuggerState.Append("(SUSPENDED)");

            try
            {
                CorDebugUserState userState = t.UserState;

                if ((userState & CorDebugUserState.USER_SUSPENDED) != 0)
                    clientState.Append("user suspended");

                if ((userState & CorDebugUserState.USER_WAIT_SLEEP_JOIN) != 0)
                {
                    if (clientState.Length > 0)
                        clientState.Append(", ");
                    clientState.Append("waiting");
                }
            }
            catch (COMException e)
            {
                if (e.ErrorCode != (int) HResult.CORDBG_E_BAD_THREAD_STATE)
                    throw;

                clientState.Append("in bad thread state");
            }

            StringBuilder result = debuggerState;
            if (clientState.Length > 0)
            {
                if (result.Length > 0)
                    result.Append(" ");
                result.Append("[").Append(clientState.ToString()).Append("]");
            }
            return result.ToString();
        }

        [
            CommandDescription(
                CommandName = "suspend",
                MinimumAbbrev = 2,
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void SuspendCmd(string arguments)
        {
            ThreadResumeSuspendHelper(arguments, CorDebugThreadState.THREAD_SUSPEND);
        }

        [
            CommandDescription(
                CommandName = "resume",
                MinimumAbbrev = 2,
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void ResumeCmd(string arguments)
        {
            ThreadResumeSuspendHelper(arguments, CorDebugThreadState.THREAD_RUN);
        }

        // Helper to set the debug state and ignore certain errors. 
        // Throws on error.
        private static void SetDebugStateWrapper(MDbgThread t, CorDebugThreadState newState)
        {
            try
            {
                t.CorThread.DebugState = newState;
            }
            catch (COMException e)
            {
                if (e.ErrorCode == (int) HResult.CORDBG_E_BAD_THREAD_STATE) // CORDBG_E_BAD_THREAD_STATE
                {
                    WriteOutput(MDbgOutputConstants.Ignore, "Warning: Thread " + t.Id + " can't be set to " + newState);
                    return; // thread is unavailable, ignore it
                }
                throw; // let error propogate up.
            }
        }

        private static void ThreadResumeSuspendHelper(string arguments, CorDebugThreadState newState)
        {
            int threadNumber;
            var ap = new ArgParser(arguments);
            if (ap.Exists(0))
            {
                if (ap.AsString(0) == "*")
                {
                    // do an action on all threads
                    foreach (MDbgThread t in Debugger.Processes.Active.Threads)
                    {
                        SetDebugStateWrapper(t, newState);
                    }
                }
                else if (ap.AsString(0).StartsWith("~"))
                {
                    threadNumber = Int32.Parse(ap.AsString(0).Substring(1), CultureInfo.CurrentUICulture);
                    // it's ~number syntax -- do on all threads except this one.
                    foreach (MDbgThread t in Debugger.Processes.Active.Threads)
                    {
                        if (t.Number != threadNumber)
                        {
                            SetDebugStateWrapper(t, newState);
                        }
                    }
                }
                else
                {
                    MDbgThread t = g_threadNickNames.GetThreadByNickName(ap.AsString(0));
                    if (t == null)
                    {
                        throw new ArgumentException();
                    }
                    SetDebugStateWrapper(t, newState);
                }
            }
            else
            {
                SetDebugStateWrapper(Debugger.Processes.Active.Threads.Active, newState);
            }
        }

        [
            CommandDescription(
                CommandName = "intercept",
                MinimumAbbrev = 3,
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void InterceptCmd(string arguments)
        {
            var ap = new ArgParser(arguments);

            if (ap.Count > 1)
            {
                throw new MDbgShellException("Wrong number of arguments.");
            }

            if (!ap.Exists(0))
            {
                throw new MDbgShellException("You must supply a frame number.");
            }
            int frameID = ap.AsInt(0);

            MDbgFrame f = Debugger.Processes.Active.Threads.Active.BottomFrame;

            while (--frameID >= 0)
            {
                f = f.NextUp;
                if (f == null)
                {
                    break;
                }
            }
            if (f == null)
            {
                throw new MDbgShellException("Invalid frame number.");
            }

            CorThread t = Debugger.Processes.Active.Threads.Active.CorThread;

            t.InterceptCurrentException(f.CorFrame);
            WriteOutput("Interception point is set.");
        }

        [
            CommandDescription(
                CommandName = "catch",
                MinimumAbbrev = 2,
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void CatchCmd(string arguments)
        {
            ModifyStopOptions(MDbgStopOptionPolicy.DebuggerBehavior.Stop, arguments);
        }

        [
            CommandDescription(
                CommandName = "ignore",
                MinimumAbbrev = 2,
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void IgnoreCmd(string arguments)
        {
            ModifyStopOptions(MDbgStopOptionPolicy.DebuggerBehavior.Ignore, arguments);
        }

        [
            CommandDescription(
                CommandName = "log",
                MinimumAbbrev = 3,
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void LogCmd(string arguments)
        {
            ModifyStopOptions(MDbgStopOptionPolicy.DebuggerBehavior.Log, arguments);
        }

        private static void DisplayStopOptions()
        {
            var stopOptions = Shell.Properties[MDbgStopOptions.PropertyName]
                              as MDbgStopOptions;
            stopOptions.PrintOptions();
        }

        private static void ModifyStopOptions(MDbgStopOptionPolicy.DebuggerBehavior command, string arguments)
        {
            var stopOptions = Shell.Properties[MDbgStopOptions.PropertyName]
                              as MDbgStopOptions;

            if (arguments.Length < 2)
            {
                DisplayStopOptions();
            }
            else
            {
                // Break up arguments string into the event type acronym and the arguments to 
                // send to the actual stop option policy. For example, if the arguments string
                // is "ex System.Exception System.ArgumentException", this will be split into:
                // eventType = "ex"
                // args = "System.Exception System.ArgumentException"
                // If there are no arguments to send to the stop option policy, args is set to null.
                string eventType = arguments.Split()[0].Trim();
                string args = null;
                if (arguments.Length > eventType.Length)
                {
                    args = arguments.Substring(eventType.Length).Trim();
                }
                stopOptions.ModifyOptions(eventType, command, args);
            }
        }

        private static void InitStopOptionsProperty()
        {
            var stopOptions = new MDbgStopOptions();
            Shell.Properties.Add(MDbgStopOptions.PropertyName, stopOptions);

            stopOptions.Add(new SimpleStopOptionPolicy("ml", "ModuleLoad"), ManagedCallbackType.OnModuleLoad);
            stopOptions.Add(new SimpleStopOptionPolicy("cl", "ClassLoad"), ManagedCallbackType.OnClassLoad);
            stopOptions.Add(new SimpleStopOptionPolicy("al", "AssemblyLoad"), ManagedCallbackType.OnAssemblyLoad);
            stopOptions.Add(new SimpleStopOptionPolicy("au", "AssemblyUnload"), ManagedCallbackType.OnAssemblyUnload);
            stopOptions.Add(new SimpleStopOptionPolicy("nt", "NewThread"), ManagedCallbackType.OnCreateThread);
            stopOptions.Add(new SimpleStopOptionPolicy("lm", "LogMessage & MDAs"),
                            new[] {ManagedCallbackType.OnLogMessage, ManagedCallbackType.OnMDANotification});
            var e = new ExceptionStopOptionPolicy();
            stopOptions.Add(e, ManagedCallbackType.OnException2);
            stopOptions.Add(e, ManagedCallbackType.OnExceptionUnwind2);

            var stopPolicy = new ExceptionEnhancedStopOptionPolicy(e);
            stopOptions.Add(stopPolicy, ManagedCallbackType.OnException2);
            stopOptions.Add(stopPolicy, ManagedCallbackType.OnExceptionUnwind2);
        }


        private static void InitModeShellProperty()
        {
            var modeSettings = new MDbgModeSettings();
            Shell.Properties.Add(MDbgModeSettings.PropertyName, modeSettings);

            GetModeValueEvent modeValueCallback = delegate(MDbgModeItem item)
                                                      {
                                                          switch (item.ShortCut)
                                                          {
                                                              case "nc":
                                                                  return Debugger.Options.CreateProcessWithNewConsole;
                                                              case "if":
                                                                  return ShowInternalFrames;
                                                              case "ma":
                                                                  return Debugger.Options.ShowAddresses;
                                                              case "fp":
                                                                  return Debugger.Options.ShowFullPaths;
                                                              default:
                                                                  Debug.Assert(false, "invalid Shortcut name");
                                                                  return false;
                                                          }
                                                      };
            ModeChangedEvent modeChangedCallback = delegate(MDbgModeItem item, bool onOff)
                                                       {
                                                           switch (item.ShortCut)
                                                           {
                                                               case "nc":
                                                                   Debugger.Options.CreateProcessWithNewConsole = onOff;
                                                                   break;

                                                               case "if":
                                                                   ShowInternalFrames = onOff;
                                                                   break;

                                                               case "ma":
                                                                   Debugger.Options.ShowAddresses = onOff;
                                                                   break;

                                                               case "fp":
                                                                   Debugger.Options.ShowFullPaths = onOff;
                                                                   break;

                                                               default:
                                                                   Debug.Assert(false, "invalid Shortcut name");
                                                                   break;
                                                           }
                                                       };

            modeSettings.Items.Add(new MDbgModeItem("nc", "Create process with new console",
                                                    modeValueCallback, modeChangedCallback));
            modeSettings.Items.Add(new MDbgModeItem("if", "Internal frames in call-stacks",
                                                    modeValueCallback, modeChangedCallback));
            modeSettings.Items.Add(new MDbgModeItem("ma", "Show memory addresses",
                                                    modeValueCallback, modeChangedCallback));
            modeSettings.Items.Add(new MDbgModeItem("fp", "Display full paths",
                                                    modeValueCallback, modeChangedCallback));
        }

        [
            CommandDescription(
                CommandName = "mode",
                MinimumAbbrev = 2,
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void ModeCmd(string arguments)
        {
            // get mode settings first
            var modeSettings = Shell.Properties[MDbgModeSettings.PropertyName]
                               as MDbgModeSettings;
            Debug.Assert(modeSettings != null);
            if (modeSettings == null)
                throw new MDbgShellException("corrupted internal state.");


            var ap = new ArgParser(arguments);
            if (!ap.Exists(0))
            {
                WriteOutput("Debugging modes:");
                foreach (MDbgModeItem item in modeSettings.Items)
                {
                    WriteOutput(string.Format(CultureInfo.InvariantCulture, "({0}) {1}: {2}", item.ShortCut,
                                              item.Description.PadRight(50), item.OnOff ? "On" : "Off"));
                }
            }
            else
            {
                bool on = ap.AsCommand(1, new CommandArgument("on", "off")) == "on";
                string shortcut = ap.AsString(0);

                // now find the correct modeItem
                MDbgModeItem item = null;
                foreach (MDbgModeItem i in modeSettings.Items)
                    if (i.ShortCut == shortcut)
                    {
                        item = i;
                        break;
                    }
                if (item == null)
                    throw new MDbgShellException("Invalid mode option.  Modes are in (here).");

                item.OnOff = on;
            }
        }

        [
            CommandDescription(
                CommandName = "load",
                MinimumAbbrev = 2,
                ResourceManagerKey = typeof (MdbgCommands)
                ),
            SecurityPermission(SecurityAction.Demand, Unrestricted = true)
            // we are loading custom assembly -- need high permissions
        ]
        public static void LoadCmd(string arguments)
        {
            var ap = new ArgParser(arguments);
            string extName = ap.AsString(0);

            Assembly asext = FindExtensionAssembly(extName);
            if (asext == null)
            {
                throw new MDbgShellException("Extension not found");
            }

            ExecuteExtension(asext);
        }

        // Find the extension that the assembly lives in.
        // This will use the extension search path, and also expand env vars.
        // Will return null if not found.
        private static Assembly FindExtensionAssembly(string extName)
        {
            // Ensure we have a '.dll' extension.
            if (!extName.ToLower().EndsWith(".dll"))
            {
                extName += ".dll";
            }

            if (Path.IsPathRooted(extName))
            {
                // Supplied full path (eg "c:\test\MyExtension.dll"), try to load it straight up.
                return LoadExtensionAssembly(extName);
            }

            // Non rooted path. May be relative, or no name at all. eg:
            //   MyExt.dll
            //   mydir\subdir\MyExt.dll
            //   ..\..\dir\MyExt.dll
            //   .\MyExt.dll
            // Try each directory in our extension path
            foreach (string path in ExtensionPath.Split(Path.PathSeparator))
            {
                if (path.Length == 0) // skip empty dirs
                    continue;

                string expandedPath = Environment.ExpandEnvironmentVariables(path);
                string extPath = Path.Combine(expandedPath, extName);

                Assembly asmExt = LoadExtensionAssembly(extPath);
                if (asmExt != null)
                    return asmExt; // found it
            }
            return null; // not found
        }

        // Attempt to load the extention at the given path.  If no such file exists, returns null.
        // Throws on error.
        private static Assembly LoadExtensionAssembly(string extensionPath)
        {
            WriteOutput(MDbgOutputConstants.Ignore, "trying to load: " + extensionPath);
            if (File.Exists(extensionPath))
            {
                return Assembly.LoadFrom(extensionPath);
            }
            return null; // not found
        }

        // Given the assembly for an extension, find the main type in that assembly.
        // This will throw on error. It will not return null.
        private static Type FindMainTypeInExtension(Assembly asext)
        {
            // 1) Try to load old-style extensions. 
            // These have goofy requirements that don't make sense for non-samples.
            {
                const string ExtNameSpace = "Microsoft.Samples.Tools.Mdbg.Extension.";
                string AssemblyName = asext.GetName().Name;
                AssemblyName = AssemblyName.Substring(0, 1).ToUpper(CultureInfo.InvariantCulture) +
                               AssemblyName.Substring(1, AssemblyName.Length - 1).ToLower(CultureInfo.InvariantCulture);
                string ExtType = ExtNameSpace + AssemblyName + "Extension";

                Type ext = asext.GetType(ExtType);
                if (ext != null)
                {
                    return ext;
                }
            }

            // 2) Try to load it as a new-style extension.
            // This just requires having some type with the [MDbgExtensionEntryPointClass] attribute.
            {
                // we'll try to scan also all the types in the extension to see if any of the types
                // has a custom attribute [MDbgExtensionEntryPointClass].
                foreach (Type t in asext.GetTypes())
                {
                    foreach (object o in t.GetCustomAttributes(false))
                    {
                        if (o is MDbgExtensionEntryPointClassAttribute)
                        {
                            return t;
                        }
                    }
                }
            }

            throw new MDbgShellException("Assembly is not in mdbg extension format.");
        }

        // Given an Assembly for an extension command, execute it.
        // This will throw if the extension is an invalid format.
        private static void ExecuteExtension(Assembly asext)
        {
            Debug.Assert(asext != null);

            Type ext = FindMainTypeInExtension(asext);

            // Now that we've found the type, execute the method.
            const string ExtMethod = "LoadExtension";
            MethodInfo mi = ext.GetMethod(ExtMethod);
            if (mi == null)
            {
                throw new MDbgShellException("Assembly is not in mdbg extension format. (mising method " + ExtMethod +
                                             ")");
            }
            mi.Invoke(null, null);
        }


        [CommandDescription(
                CommandName = "print",
                MinimumAbbrev = 1,
                ResourceManagerKey = typeof (MdbgCommands)
                )]
        public static void PrintCmd(string arguments)
        {
            mdbgCommandsCustomizedForO2.PrintCmd(arguments, null);
        }        

        [CommandDescription(
                CommandName = "funceval",
                MinimumAbbrev = 1,
                ResourceManagerKey = typeof (MdbgCommands)
                )]

        public static void FuncEvalCmd(string arguments)
        {
            mdbgCommandsCustomizedForO2.FuncEvalCmd(arguments, Shell, null);
        }

        [
            CommandDescription(
                CommandName = "newobj",
                MinimumAbbrev = 4,
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void NewObjCmd(string arguments)
        {
            var ap = new ArgParser(arguments);

            string className = ap.AsString(0);
            MDbgFunction func = Debugger.Processes.Active.ResolveFunctionName(null, className, ".ctor",
                                                                              Debugger.Processes.Active.Threads.Active.
                                                                                  CorThread.AppDomain);
            if (null == func)
                throw new MDbgShellException(String.Format(CultureInfo.InvariantCulture, "Could not resolve {0}",
                                                           ap.AsString(0)));

            CorEval eval = Debugger.Processes.Active.Threads.Active.CorThread.CreateEval();

            var callArguments = new ArrayList();
            // parse the arguments to newobj
            int i = 1;
            while (ap.Exists(i))
            {
                string arg = ap.AsString(i);
                // this is a normal argument
                MDbgValue rsMVar = Debugger.Processes.Active.ResolveVariable(arg,
                                                                             Debugger.Processes.Active.Threads.Active.
                                                                                 CurrentFrame);
                if (rsMVar == null)
                {
                    // cordbg supports also limited literals -- currently only NULL & I4.
                    if (string.Compare(arg, "null", true) == 0)
                    {
                        callArguments.Add(eval.CreateValue(CorElementType.ELEMENT_TYPE_CLASS, null));
                    }
                    else
                    {
                        int v;
                        if (!Int32.TryParse(arg, out v))
                            throw new MDbgShellException(string.Format(CultureInfo.InvariantCulture,
                                                                       "Argument '{0}' could not be resolved to variable or number",
                                                                       arg));

                        CorGenericValue gv = eval.CreateValue(CorElementType.ELEMENT_TYPE_I4, null).CastToGenericValue();
                        Debug.Assert(gv != null);
                        gv.SetValue(v);
                        callArguments.Add(gv);
                    }
                }
                else
                {
                    callArguments.Add(rsMVar.CorValue);
                }
                ++i;
            }

            eval.NewParameterizedObject(func.CorFunction, null, (CorValue[]) callArguments.ToArray(typeof (CorValue)));
            Debugger.Processes.Active.Go().WaitOne();

            // now display result of the funceval
            if (! (Debugger.Processes.Active.StopReason is EvalCompleteStopReason))
            {
                // we could have received also EvalExceptionStopReason but it's derived from EvalCompleteStopReason
                WriteOutput(MDbgOutputConstants.StdOutput,"Newobj command not fully completed and debuggee has stopped");
                WriteOutput(MDbgOutputConstants.StdOutput,"Result of Newobj won't be printed when finished.");
            }
            else
            {
                eval = (Debugger.Processes.Active.StopReason as EvalCompleteStopReason).Eval;
                Debug.Assert(eval != null);

                CorValue cv = eval.Result;
                if (cv != null)
                {
                    var mv = new MDbgValue(Debugger.Processes.Active, cv);
                    WriteOutput("result = " + mv.GetStringValue(1));
                    if (Debugger.Processes.Active.DebuggerVars.SetEvalResult(cv))
                        WriteOutput(MDbgOutputConstants.StdOutput,"results saved to $result");
                }
            }
            Shell.DisplayCurrentLocation();
        }

        [
            CommandDescription(
                CommandName = "set",
                MinimumAbbrev = 3,
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void SetCmd(string arguments)
        {
            // Arguments has to be in the form of variable=varName, variable=value or variable=(<type>)value, 
            // where we use the ldasm naming convention (e.g. "int", "sbyte", "ushort", etc...) for <type>.
            // Example inputs: var=myInt, var=45, var=(long)45
            int idx = arguments.IndexOf('=');
            if (idx == -1)
            {
                throw new MDbgShellException("Wrong arguments.");
            }

            string varName = arguments.Substring(0, idx).Trim();
            string value = arguments.Substring(idx + 1).Trim();

            MDbgValue lsMVar = null;
            MDbgDebuggerVar lsDVar = null;
            if (varName.StartsWith("$"))
            {
                //variable is a debugger variable
                lsDVar = Debugger.Processes.Active.DebuggerVars[varName];
            }
            else
            {
                //variable is a program variable
                lsMVar = Debugger.Processes.Active.ResolveVariable(varName,
                                                                   Debugger.Processes.Active.Threads.Active.CurrentFrame);
                if (lsMVar == null)
                {
                    throw new MDbgShellException("Cannot resolve variable " + varName);
                }
            }

            if (value.Length == 0)
            {
                if (varName.StartsWith("$"))
                {
                    Debugger.Processes.Active.DebuggerVars.DeleteVariable(varName);
                    return;
                }
                else
                {
                    throw new MDbgShellException("Missing value");
                }
            }


            CorValue val = Shell.ExpressionParser.ParseExpression2(value, Debugger.Processes.Active,
                                                                   Debugger.Processes.Active.Threads.Active.CurrentFrame);

            if (val == null)
            {
                throw new MDbgShellException("cannot resolve value or expression " + value);
            }
            var valGeneric = val as CorGenericValue;
            bool bIsReferenceValue = val is CorReferenceValue;

            if (lsDVar != null)
            {
                //variable is a debugger variable
                if ((valGeneric != null) || bIsReferenceValue)
                {
                    lsDVar.Value = val;
                }

                else
                {
                    CorHeapValue rsHeapVal = val.CastToHeapValue();
                    if (rsHeapVal != null)
                    {
                        lsDVar.Value = rsHeapVal;
                    }
                    else
                    {
                        lsDVar.Value = val.CastToReferenceValue();
                    }
                }
            }

            else if (lsMVar != null)
            {
                //variable is a program variable
                if (valGeneric != null)
                {
                    CorValue lsVar = lsMVar.CorValue;
                    if (lsVar == null)
                    {
                        throw new MDbgShellException("cannot set constant values to unavailable variables");
                    }

                    // val is a primitive value                    
                    CorGenericValue lsGenVal = lsVar.CastToGenericValue();
                    if (lsGenVal == null)
                    {
                        throw new MDbgShellException("cannot set constant values to non-primitive values");
                    }
                    try
                    {
                        // We want to allow some type coercion. Eg, casting between integer precisions.
                        lsMVar.Value = val; // This may do type coercion
                    }
                    catch (MDbgValueWrongTypeException)
                    {
                        throw new MDbgShellException(String.Format("Type mismatch. Can't convert from {0} to {1}",
                                                                   val.Type, lsGenVal.Type));
                    }
                }
                else if (bIsReferenceValue)
                {
                    //reget variable
                    lsMVar = Debugger.Processes.Active.ResolveVariable(varName,
                                                                       Debugger.Processes.Active.Threads.Active.
                                                                           CurrentFrame);
                    lsMVar.Value = val;
                }
                else
                {
                    if (val.CastToHeapValue() != null)
                    {
                        throw new MDbgShellException("Heap values should be assigned only to debugger variables");
                    }
                    if (val.CastToGenericValue() != null)
                    {
                        lsMVar.Value = val.CastToGenericValue();
                    }
                    else
                    {
                        lsMVar.Value = val.CastToReferenceValue();
                    }
                }
            }


            // as a last thing we do is to print new value of the variable

            lsMVar = Debugger.Processes.Active.ResolveVariable(varName,
                                                               Debugger.Processes.Active.Threads.Active.CurrentFrame);
            WriteOutput(varName + "=" + lsMVar.GetStringValue(1));
        }

        [
            CommandDescription(
                CommandName = "list",
                MinimumAbbrev = 1,
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void ListCmd(string arguments)
        {
            const string verboseOpt = "v";
            bool bVerbose;
            var ap = new ArgParser(arguments, verboseOpt);
            string listWhat = ap.AsCommand(0, new CommandArgument("modules", "appdomains", "assemblies"));
            switch (listWhat)
            {
                case "modules":
                    bVerbose = ap.OptionPassed(verboseOpt);
                    if (ap.Exists(1))
                    {
                        // user specified module to display info for
                        MDbgModule m = Debugger.Processes.Active.Modules.Lookup(ap.AsString(1));
                        if (m == null)
                        {
                            throw new MDbgShellException("No such module.");
                        }
                        ListModuleInternal(m, true);
                    }
                    else
                    {
                        // we list all modules
                        WriteOutput("Loaded Modules:");
                        foreach (MDbgModule m in Debugger.Processes.Active.Modules)
                        {
                            ListModuleInternal(m, bVerbose);
                        }
                    }
                    break;
                case "appdomains":
                    WriteOutput("Current appDomains:");
                    foreach (MDbgAppDomain ad in Debugger.Processes.Active.AppDomains)
                    {
                        WriteOutput(ad.Number + ". - " + ad.CorAppDomain.Name);
                    }
                    break;

                case "assemblies":
                    WriteOutput("Current assemblies:");
                    foreach (MDbgAppDomain ad in Debugger.Processes.Active.AppDomains)
                    {
                        foreach (CorAssembly assem in ad.CorAppDomain.Assemblies)
                        {
                            WriteOutput("\t" + assem.Name);
                        }
                    }

                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        private static void ListModuleInternal(MDbgModule module, bool verbose)
        {
            string symbolsStatus;
            if (module.SymReader == null)
            {
                symbolsStatus = " (no symbols loaded)";
            }
            else
            {
                symbolsStatus = String.Empty;
            }

            CorAppDomain ad = module.CorModule.Assembly.AppDomain;
            int adNumber = Debugger.Processes.Active.AppDomains.Lookup(ad).Number;
            if (verbose)
            {
                WriteOutput(string.Format(CultureInfo.InvariantCulture, ":{0}\t{1}#{2} {3}", module.Number,
                                          module.CorModule.Name, adNumber, symbolsStatus));
            }
            else
            {
                string moduleBaseName;
                try
                {
                    moduleBaseName = Path.GetFileName(module.CorModule.Name);
                }
                catch
                {
                    moduleBaseName = module.CorModule.Name;
                }

                WriteOutput(string.Format(CultureInfo.InvariantCulture, ":{0}\t{1}#{2} {3}", module.Number,
                                          moduleBaseName, adNumber, symbolsStatus));
            }
        }

        [
            CommandDescription(
                CommandName = "symbol",
                MinimumAbbrev = 2,
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        /*
         * We want to have following commnads:
         *
         * symbol path "value"    -- sets symbol paths
         * symbol addpath "value" -- adds symbol path
         * symbol reload [module] -- reloads symbol for a module
         * symbol list [module]   -- shows currently loaded symbols
         */
        public static void SymbolCmd(string arguments)
        {
            var ap = new ArgParser(arguments);
            if (!ap.Exists(0))
            {
                ExecuteCommand("help symbol");
                return;
            }
            switch (ap.AsCommand(0, new CommandArgument("path", "addpath", "reload", "list")))
            {
                case "path":
                    if (!ap.Exists(1))
                    {
                        // we want to print current path
                        string p = Debugger.Options.SymbolPath;
                        WriteOutput("Current symbol path: " + p);
                    }
                    else
                    {
                        // we are setting path
                        Debugger.Options.SymbolPath = ap.AsString(1);
                        WriteOutput("Current symbol path: " + Debugger.Options.SymbolPath);
                    }
                    break;

                case "addpath":
                    Debugger.Options.SymbolPath = Debugger.Options.SymbolPath + Path.PathSeparator + ap.AsString(1);
                    WriteOutput("Current symbol path: " + Debugger.Options.SymbolPath);
                    break;

                case "reload":
                    {
                        IEnumerable modules;
                        if (ap.Exists(1))
                        {
                            // we want to reload only one module

                            MDbgModule m = Debugger.Processes.Active.Modules.Lookup(ap.AsString(1));
                            if (m == null)
                            {
                                throw new MDbgShellException("No such module.");
                            }
                            modules = new[] {m};
                        }
                        else
                        {
                            modules = Debugger.Processes.Active.Modules;
                        }

                        foreach (MDbgModule m in modules)
                        {
                            WriteOutput("Reloading symbols for module " + m.CorModule.Name);
                            m.ReloadSymbols(true);
                            WriteModuleStatus(m, true);
                        }
                    }
                    break;
                case "list":
                    {
                        IEnumerable modules;
                        if (ap.Exists(1))
                        {
                            // we want to list only one module
                            MDbgModule m = Debugger.Processes.Active.Modules.Lookup(ap.AsString(1));
                            if (m == null)
                            {
                                throw new MDbgShellException("No such module.");
                            }
                            modules = new[] {m};
                        }
                        else
                        {
                            modules = Debugger.Processes.Active.Modules;
                        }

                        foreach (MDbgModule m in modules)
                        {
                            WriteModuleStatus(m, false);
                        }
                    }
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        private static void WriteModuleStatus(MDbgModule module, bool reloadMode)
        {
            string symbolLocation = null;

            bool bHaveSyms = module.SymReader != null;
            if (bHaveSyms)
            {
                symbolLocation = module.SymbolFilename;
                if (symbolLocation == null || symbolLocation.Length == 0)
                    symbolLocation = "<loaded from unknown location>";
            }
            string outputString;
            if (reloadMode)
            {
                if (bHaveSyms)
                    outputString = string.Format(CultureInfo.InvariantCulture, "Symbols loaded from: {0}\n",
                                                 symbolLocation);
                else
                    outputString = "No symbols could be loaded.\n";
            }
            else
            {
                outputString = string.Format(CultureInfo.InvariantCulture, "Module: {0}\nSymbols: {1}\n",
                                             module.CorModule.Name,
                                             bHaveSyms ? symbolLocation : "<no available>");
            }

            WriteOutput(outputString);
        }


        [CommandDescription(
            CommandName = "processenum",
            MinimumAbbrev = 3,
            ResourceManagerKey = typeof (MdbgCommands)
            )]
        public static void ProcessEnumCmd(string arguments)
        {
            mdbgCommandsCustomizedForO2.ProcessEnumCmd(arguments, null);
        }


        [CommandDescription(
            CommandName = "attach",
            MinimumAbbrev = 1,
            ResourceManagerKey = typeof (MdbgCommands)
            )
        ]
        public static void AttachCmd(string arguments)
        {
            mdbgCommandsCustomizedForO2.AttachCmd(arguments, null);
        }

        [
            CommandDescription(
                CommandName = "detach",
                MinimumAbbrev = 2,
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void DetachCmd(string arguments)
        {
            Debugger.Processes.Active.Breakpoints.DeleteAll();
            Debugger.Processes.Active.Detach().WaitOne();
        }

        [CommandDescription(
                CommandName = "up",
                MinimumAbbrev = 1,
                ResourceManagerKey = typeof (MdbgCommands)
                )]

        public static void UpCmd(string arguments)
        {
            string frameNum = "f";
            var ap = new ArgParser(arguments, frameNum);

            if (ap.OptionPassed(frameNum))
            {
                SwitchToFrame(ap.AsInt(0));
            }
            else
            {
                int count = 1;
                if (ap.Exists(0))
                {
                    count = ap.AsInt(0);
                }
                while (--count >= 0)
                {
                    Debugger.Processes.Active.Threads.Active.MoveCurrentFrame(false);
                }
            }
            if (Debugger.Processes.Active.Threads.Active.CurrentFrame.IsManaged)
            {
                WriteOutput("Current Frame:" +
                            Debugger.Processes.Active.Threads.Active.CurrentFrame.Function.FullName
                    );
            }
            Shell.DisplayCurrentLocation();
        }


        [
            CommandDescription(
                CommandName = "down",
                MinimumAbbrev = 1,
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void DownCmd(string arguments)
        {
            string frameNum = "f";
            var ap = new ArgParser(arguments, frameNum);
            if (ap.OptionPassed(frameNum))
            {
                SwitchToFrame(ap.AsInt(0));
            }
            else
            {
                int count = 1;
                if (ap.Exists(0))
                {
                    count = ap.AsInt(0);
                }
                while (--count >= 0)
                {
                    Debugger.Processes.Active.Threads.Active.MoveCurrentFrame(true);
                }
            }

            if (Debugger.Processes.Active.Threads.Active.CurrentFrame.IsManaged)
            {
                WriteOutput("Current Frame:" +
                            Debugger.Processes.Active.Threads.Active.CurrentFrame.Function.FullName
                    );
            }
            Shell.DisplayCurrentLocation();
        }

        private static void SwitchToFrame(int frameNum)
        {
            if (frameNum < 0)
                throw new ArgumentException("Invalid frame number.");

            Debug.Assert(frameNum >= 0);
            int idx = 0;
            foreach (MDbgFrame f in Debugger.Processes.Active.Threads.Active.Frames)
            {
                if (f.IsInfoOnly)
                    continue;

                if (frameNum == idx)
                {
                    // we want to switch to this frame
                    Debugger.Processes.Active.Threads.Active.CurrentFrame = f;
                    return;
                }
                ++idx;
            }
            throw new MDbgShellException("No such frame");
        }

        [
            CommandDescription(
                CommandName = "path",
                MinimumAbbrev = 2,
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void PathCmd(string arguments)
        {
            var ap = new ArgParser(arguments);
            if (!ap.Exists(0))
            {
                WriteOutput("path: " + Shell.FileLocator.Path);
            }
            else
            {
                Shell.FileLocator.Path = arguments;
                WriteOutput("Path set to: " + Shell.FileLocator.Path);

                if (Debugger.Processes.HaveActive)
                {
                    ShowCmd("");
                }
            }
        }


        [
            CommandDescription(
                CommandName = "x",
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void XCmd(string arguments)
        {
            if (arguments.Length == 0)
            {
                WriteOutput("Please specify module.");
                ListCmd("mo");
            }
            else
            {
                const int default_count = 100; // default number of frames to print

                const string countOpt = "c";

                var ap = new ArgParser(arguments, countOpt + ":1");
                int count = default_count;
                if (ap.OptionPassed(countOpt))
                {
                    ArgToken countArg = ap.GetOption(countOpt);
                    if (countArg.AsString == "all")
                    {
                        count = 0; // 0 means print all symbols
                    }
                    else
                    {
                        count = countArg.AsInt;
                        if (count <= 0)
                        {
                            throw new MDbgShellException("Count must be positive number or string \"all\"");
                        }
                    }
                }

                string moduleName, substrPart;

                string expr = ap.AsString(0);
                int i = expr.IndexOf('!');
                if (i == -1)
                {
                    moduleName = expr;
                    substrPart = null;
                }
                else
                {
                    moduleName = expr.Substring(0, i);
                    substrPart = expr.Substring(i + 1);
                }

                SymbolCache.Clear();
                // enum functions from the module
                MDbgModule m = Debugger.Processes.Active.Modules.Lookup(moduleName);
                if (m == null)
                {
                    throw new MDbgShellException("module not found!");
                }

                bool shouldPrint = substrPart == null;
                Regex r = null;
                if (substrPart != null)
                {
                    r = new Regex(ConvertSimpleExpToRegExp(substrPart));
                }


                foreach (Type t in m.Importer.DefinedTypes)
                {
                    foreach (MethodInfo mi in t.GetMethods())
                    {
                        var sb = new StringBuilder();
                        sb.Append(t.Name).Append(".").Append(mi.Name).Append("(");
                        bool needComma = false;
                        foreach (ParameterInfo pi in mi.GetParameters())
                        {
                            if (needComma)
                            {
                                sb.Append(",");
                            }
                            sb.Append(pi.Name);
                            needComma = true;
                        }
                        sb.Append(")");
                        string fullFunctionName = sb.ToString();
                        if (r != null)
                        {
                            shouldPrint = r.IsMatch(fullFunctionName);
                        }
                        if (shouldPrint)
                        {
                            int idx = SymbolCache.Add(new MdbgSymbol(m.Number, t.Name, mi.Name, 0));
                            WriteOutput("~" + idx + ". " + fullFunctionName);
                            if (count != 0 && idx >= count)
                            {
                                WriteOutput(string.Format(CultureInfo.CurrentUICulture,
                                                          "displayed only first {0} hits. For more symbols use -c switch",
                                                          count));
                                return;
                            }
                        }
                    }
                }
            }
        }

        // converts a dos-like regexp to true regular expresion.
        // This enables simple filters for types as e.g.:
        // x mod!System.String*
        //
        // currently function supports just 2 special chars: * (match
        // 0-unlim chars) and ? (match 1 char).
        public static string ConvertSimpleExpToRegExp(string simpleExp)
        {
            var sb = new StringBuilder();
            sb.Append("^");
            foreach (char c in simpleExp)
            {
                switch (c)
                {
                    case '\\':
                    case '{':
                    case '|':
                    case '+':
                    case '[':
                    case '(':
                    case ')':
                    case '^':
                    case '$':
                    case '.':
                    case '#':
                    case ' ':
                        sb.Append('\\').Append(c);
                        break;
                    case '*':
                        sb.Append(".*");
                        break;
                    case '?':
                        sb.Append(".");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            sb.Append("$");
            return sb.ToString();
        }

        [
            CommandDescription(
                CommandName = "aprocess",
                MinimumAbbrev = 2,
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void ActiveProcess(string arguments)
        {
            var ap = new ArgParser(arguments);
            if (ap.Count > 1)
            {
                throw new MDbgShellException("Wrong # of arguments.");
            }

            if (ap.Exists(0))
            {
                int logicalPID = ap.AsInt(0);
                bool found = false;
                foreach (MDbgProcess ps in Debugger.Processes)
                    if (ps.Number == logicalPID)
                    {
                        Debugger.Processes.Active = ps;
                        found = true;
                        break;
                    }
                if (found)
                {
                    Shell.DisplayCurrentLocation();
                }
                else
                {
                    throw new MDbgShellException("Invalid process number");
                }
            }
            else
            {
                MDbgProcess ActiveProcess = Debugger.Processes.HaveActive ? Debugger.Processes.Active : null;

                WriteOutput("Active Process:");
                bool haveProcesses = false;

                CorPublish corPublish = null;
                foreach (MDbgProcess p in Debugger.Processes)
                {
                    haveProcesses = true;
                    string processName = p.Name;
                    string launchMode;
                    if (processName == null)
                    {
                        // in case we're attached (as opposed to launching),
                        // we don't know process name.
                        // Let's find it through CorPublishApi
                        try
                        {
                            if (corPublish == null)
                            {
                                corPublish = new CorPublish();
                            }
                            processName = corPublish.GetProcess(p.CorProcess.Id).DisplayName;
                        }
                        catch
                        {
                            processName = "N/A";
                        }
                        launchMode = "attached";
                    }
                    else
                    {
                        launchMode = "launched";
                    }
                    WriteOutput((ActiveProcess == p ? "*" : " ") +
                                string.Format(CultureInfo.InvariantCulture, "{0}. [PID: {1}, {2}] {3}", p.Number,
                                              p.CorProcess.Id, launchMode, processName));
                }
                if (!haveProcesses)
                {
                    WriteOutput("No Active Process!");
                }
            }
        }

        [
            CommandDescription(
                CommandName = "foreach",
                MinimumAbbrev = 2,
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void ForEachCmd(string arguments)
        {
            if (arguments.Length != 0)
            {
                MDbgProcess p = Debugger.Processes.Active;
                MDbgThreadCollection c = p.Threads;

                // Remember current thread so that we can restore it.
                MDbgThread tOriginal = c.HaveActive ? c.Active : null;
                foreach (MDbgThread t in c)
                {
                    try
                    {
                        p.Threads.Active = t;
                    }
                    catch (COMException e)
                    {
                        // we'll ignore neutered threads -- they mean that the threads
                        // just got destroyed and we cannot make them active.
                        if (e.ErrorCode != (int) HResult.CORDBG_E_OBJECT_NEUTERED)
                        {
                            throw;
                        }
                    }
                    ExecuteCommand(arguments);
                }

                // Restore back to original thread.
                if (tOriginal != null)
                {
                    p.Threads.Active = tOriginal;
                }
            }
            else
            {
                ExecuteCommand("? foreach");
            }
        }

        //////////////////////////////////////////////////////////////////////////////////
        //
        // When command implementation
        //
        //////////////////////////////////////////////////////////////////////////////////


        [
            CommandDescription(
                CommandName = "when",
                MinimumAbbrev = 4,
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void WhenCmd(string arguments)
        {
            var ap = new ArgParser(arguments);
            if (ap.Count == 0)
            {
                // we want to list all actions
                foreach (ExecuteCmdAction a in m_events)
                {
                    WriteOutput(a.Id + ".\t" + a);
                }
            }
            else if (ap.AsString(0) == "delete")
            {
                if (ap.AsString(1) == "all")
                {
                    // delete all actions
                    m_events.Clear();
                }
                else
                {
                    int idx = 1;
                    while (true)
                    {
                        int actionToRemove = ap.AsInt(idx);

                        foreach (ExecuteCmdAction a in m_events)
                        {
                            if (a.Id == actionToRemove)
                            {
                                m_events.Remove(a);
                                break; // once we remove an item, we cannot iterate further,
                                // this doesn't matter because id's are unique.
                            }
                        }
                        idx++;
                        if (!ap.Exists(idx))
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                //we want to create an when action
                ExecuteCmdAction action = null;
                string cmdString;
                int argCount;
                GetDoPart(ap, out cmdString, out argCount);
                switch (ap.AsString(0))
                {
                    case "StepComplete":
                        action = new ExecuteCmdAction(Shell, cmdString, typeof (StepCompleteStopReason));
                        switch (argCount)
                        {
                            case 0:
                                break;
                            default:
                                throw new ArgumentException();
                        }
                        break;
                    case "ProcessExited":
                        action = new ExecuteCmdAction(Shell, cmdString, typeof (ProcessExitedStopReason));
                        switch (argCount)
                        {
                            case 0:
                                break;
                            default:
                                throw new ArgumentException();
                        }
                        break;
                    case "ThreadCreated":
                        action = new ExecuteCmdAction(Shell, cmdString, typeof (ThreadCreatedStopReason));
                        switch (argCount)
                        {
                            case 0:
                                break;
                            case 1:
                                action.Number = ap.AsInt(1);
                                break;
                            default:
                                throw new ArgumentException();
                        }
                        break;
                    case "BreakpointHit":
                        action = new ExecuteCmdAction(Shell, cmdString, typeof (BreakpointHitStopReason));
                        switch (argCount)
                        {
                            case 0:
                                break;
                            case 1:
                                action.Number = ap.AsInt(1);
                                break;
                            default:
                                throw new ArgumentException();
                        }
                        break;
                    case "ModuleLoaded":
                        action = new ExecuteCmdAction(Shell, cmdString, typeof (BreakpointHitStopReason));
                        switch (argCount)
                        {
                            case 0:
                                break;
                            case 1:
                                action.Name = ap.AsString(1);
                                break;
                            default:
                                throw new ArgumentException();
                        }
                        break;
                    case "ClassLoaded":
                        action = new ExecuteCmdAction(Shell, cmdString, typeof (ClassLoadedStopReason));
                        switch (argCount)
                        {
                            case 0:
                                break;
                            case 1:
                                action.Name = ap.AsString(1);
                                break;
                            default:
                                throw new ArgumentException();
                        }
                        break;
                    case "AssemblyLoaded":
                        action = new ExecuteCmdAction(Shell, cmdString, typeof (AssemblyLoadedStopReason));
                        switch (argCount)
                        {
                            case 0:
                                break;
                            case 1:
                                action.Name = ap.AsString(1);
                                break;
                            default:
                                throw new ArgumentException();
                        }
                        break;
                    case "AssemblyUnloaded":
                        throw new NotImplementedException();

                    case "ControlCTrapped":
                        action = new ExecuteCmdAction(Shell, cmdString, typeof (ControlCTrappedStopReason));
                        switch (argCount)
                        {
                            case 0:
                                break;
                            default:
                                throw new ArgumentException();
                        }
                        break;
                    case "ExceptionThrown":
                        action = new ExecuteCmdAction(Shell, cmdString, typeof (ExceptionThrownStopReason));
                        switch (argCount)
                        {
                            case 0:
                                break;
                            case 1:
                                action.Name = ap.AsString(1);
                                break;
                            default:
                                throw new ArgumentException();
                        }
                        break;
                    case "UnhandledExceptionThrown":
                        action = new ExecuteCmdAction(Shell, cmdString, typeof (UnhandledExceptionThrownStopReason));
                        switch (argCount)
                        {
                            case 0:
                                break;
                            case 1:
                                action.Name = ap.AsString(1);
                                break;
                            default:
                                throw new ArgumentException();
                        }
                        break;
                    case "AsyncStop":
                        action = new ExecuteCmdAction(Shell, cmdString, typeof (AsyncStopStopReason));
                        switch (argCount)
                        {
                            case 0:
                                break;
                            default:
                                throw new ArgumentException();
                        }
                        break;
                    case "AttachComplete":
                        if (argCount != 0)
                        {
                            throw new ArgumentException();
                        }
                        action = new ExecuteCmdAction(Shell, cmdString, typeof (AttachCompleteStopReason));
                        break;
                    case "UserBreak":
                        if (argCount != 0)
                        {
                            throw new ArgumentException();
                        }
                        action = new ExecuteCmdAction(Shell, cmdString, typeof (UserBreakStopReason));
                        break;
                    case "EvalComplete":
                        if (argCount != 0)
                        {
                            throw new ArgumentException();
                        }
                        action = new ExecuteCmdAction(Shell, cmdString, typeof (EvalCompleteStopReason));
                        break;
                    case "EvalException":
                        if (argCount != 0)
                        {
                            throw new ArgumentException();
                        }
                        action = new ExecuteCmdAction(Shell, cmdString, typeof (EvalExceptionStopReason));
                        break;
                    case "RemapOpportunityReached":
                        if (argCount != 0)
                        {
                            throw new ArgumentException();
                        }
                        action = new ExecuteCmdAction(Shell, cmdString, typeof (RemapOpportunityReachedStopReason));
                        break;
                    default:
                        throw new ArgumentException("invalid event name");
                }
                m_events.Add(action);
            }
        }

        private static void GetDoPart(ArgParser ap, out string cmdString, out int argCount)
        {
            const int startPart = 1;
            Debug.Assert(ap != null);
            // all commands start with "when XXX [arguments...] do cmds
            int i;
            for (i = startPart; i < ap.Count; i++)
            {
                if (ap.AsString(i) == "do")
                {
                    break;
                }
            }
            if (i == ap.Count)
            {
                throw new ArgumentException("Invalid command syntax");
            }
            var sb = new StringBuilder();
            int j = i + 1;
            sb.Append(ap.AsString(j));
            for (j++; j < ap.Count; j++)
            {
                sb.Append("\n").Append(ap.AsString(j));
            }
            cmdString = sb.ToString();
            argCount = i - startPart;
        }


        internal static void WhenHandler(object sender, CommandExecutedEventArgs e)
        {
            if (!e.MovementCommand)
            {
                return;
            }
            foreach (ExecuteCmdAction a in m_events)
            {
                if (!a.IsRunning && a.ShouldExecuteNow)
                {
                    try
                    {
                        a.IsRunning = true;
                        WriteOutput("exec> " + a);
                        a.Execute();
                    }
                    finally
                    {
                        a.IsRunning = false;
                    }
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////////////
        //
        // end of When command implementation
        //
        //////////////////////////////////////////////////////////////////////////////////

        [
            CommandDescription(
                CommandName = "echo",
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void EchoCmd(string arguments)
        {
            WriteOutput(arguments);
        }

        [
            CommandDescription(
                CommandName = "uwgchandle",
                MinimumAbbrev = 4,
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void UnwrapGCHandleCmd(string arguments)
        {
            var ap = new ArgParser(arguments);
            if (ap.Count != 1)
            {
                WriteError(
                    "Wrong arguments, should be name or address of a \"System.Runtime.InteropServices.GCHandle\" object.");
                return;
            }

            long handleAdd = 0;

            // First try to resolve the argument as a variable in the current frame
            MDbgValue var = Debugger.Processes.Active.ResolveVariable(
                ap.AsString(0),
                Debugger.Processes.Active.Threads.Active.CurrentFrame);
            if (var != null)
            {
                if (var.TypeName != "System.Runtime.InteropServices.GCHandle")
                {
                    WriteError("Variable is not of type \"System.Runtime.InteropServices.GCHandle\".");
                    return;
                }

                foreach (MDbgValue field in var.GetFields())
                {
                    if (field.Name == "m_handle")
                    {
                        handleAdd = Int64.Parse(field.GetStringValue(0));
                        break;
                    }
                }
            }
            else
            {
                // Trying to resolve as a raw address now
                try
                {
                    handleAdd = ap.GetArgument(0).AsAddress;
                }
                catch (FormatException)
                {
                    WriteError("Couldn't recognize the argument as a variable name or address");
                    return;
                }
            }

            var add = new IntPtr(handleAdd);
            CorReferenceValue result;

            try
            {
                result = Debugger.Processes.Active.CorProcess.GetReferenceValueFromGCHandle(add);
            }
            catch (COMException e)
            {
                if (e.ErrorCode == (int) HResult.CORDBG_E_BAD_REFERENCE_VALUE)
                {
                    WriteError("Invalid handle address.");
                    return;
                }
                else
                {
                    throw;
                }
            }

            CorValue v = result.Dereference();
            var mv = new MDbgValue(Debugger.Processes.Active, v);
            if (mv.IsComplexType)
            {
                WriteOutput(string.Format("GCHandle to <{0}>",
                                          InternalUtil.PrintCorType(Debugger.Processes.Active, v.ExactType)));

                // now print fields as well
                foreach (MDbgValue f in mv.GetFields())
                    WriteOutput(" " + f.Name + "=" + f.GetStringValue(0));
            }
            else
            {
                WriteOutput(string.Format("GCHandle to {0}", mv.GetStringValue(0)));
            }
        }

        [
            CommandDescription(
                CommandName = "config",
                MinimumAbbrev = 4,
                ResourceManagerKey = typeof (MdbgCommands)
                )
        ]
        public static void ConfigCmd(string arguments)
        {
            const string extPathCmd = "extpath";
            const string extPathAddCmd = "extpath+";

            var ap = new ArgParser(arguments);
            if (!ap.Exists(0))
            {
                WriteOutput("Current configuration:");
                WriteOutput(string.Format("\tExtensionPath={0}", ExtensionPath));
                return;
            }

            switch (ap.AsCommand(0, new CommandArgument(extPathCmd, extPathAddCmd)))
            {
                case extPathCmd:
                    ExtensionPath = ap.AsString(1);
                    PrintExt:
                    WriteOutput(string.Format("ExtensionPath={0}", ExtensionPath));
                    break;
                case extPathAddCmd:
                    ExtensionPath = ExtensionPath + Path.PathSeparator + ap.AsString(1);
                    goto PrintExt;
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        #region Nested type: DefaultBreakpointParser

        private class DefaultBreakpointParser : IBreakpointParser
        {
            // Parse a function breakpoint.

            #region IBreakpointParser Members

            ISequencePointResolver IBreakpointParser.ParseFunctionBreakpoint(string arguments)
            {
                Regex r;
                Match m;
                // maybe it's in the form:
                // "b ~number"
                r = new Regex(@"^~(\d+)$");
                m = r.Match(arguments);
                string symNum = m.Groups[1].Value;
                if (symNum.Length > 0)
                {
                    int intSymNum = Int32.Parse(symNum, CultureInfo.CurrentUICulture);
                    MdbgSymbol symbol = SymbolCache.Retrieve(intSymNum);

                    return new BreakpointFunctionLocation(
                        string.Format(CultureInfo.InvariantCulture, ":{0}", symbol.ModuleNumber),
                        symbol.ClassName,
                        symbol.Method,
                        symbol.Offset);
                }


                // maybe it's in the form:
                // "b mdbg!Mdbg.Main+3"
                r = new Regex(@"^([^\!]+\!)?((?:\w+\.)*)([^\d]\w*)(\+\d+)?$");
                m = r.Match(arguments);
                string module = m.Groups[1].Value;
                string className = m.Groups[2].Value;
                string method = m.Groups[3].Value;
                string offset = m.Groups[4].Value;
                int intOffset = 0;

                if (method.Length > 0)
                {
                    if (module.Length > 0)
                    {
                        module = module.Substring(0, module.Length - 1);
                    }

                    if (className.Length > 0)
                    {
                        className = className.Substring(0, className.Length - 1);
                    }

                    if (offset.Length > 0)
                    {
                        intOffset = Int32.Parse(offset.Substring(1), CultureInfo.CurrentUICulture);
                    }

                    return new BreakpointFunctionLocation(module, className, method, intOffset);
                }

                // maybe it's in the form:
                // "b Mdbg.cs:34"
                r = new Regex(@"^(\S+:)?(\d+)$");
                m = r.Match(arguments);
                string fname = m.Groups[1].Value;
                string lineNo = m.Groups[2].Value;
                int intLineNo = 0;

                if (lineNo.Length > 0)
                {
                    if (fname.Length > 0)
                    {
                        fname = fname.Substring(0, fname.Length - 1);
                    }
                    else
                    {
                        MDbgSourcePosition pos = null;

                        MDbgThread thr = Debugger.Processes.Active.Threads.Active;
                        if (thr != null)
                        {
                            pos = thr.CurrentSourcePosition;
                        }

                        if (pos == null)
                        {
                            throw new MDbgShellException("Cannot determine current file");
                        }

                        fname = pos.Path;
                    }


                    intLineNo = Int32.Parse(lineNo, CultureInfo.CurrentUICulture);

                    return new BreakpointLineNumberLocation(fname, intLineNo);
                }

                // We don't recognize the syntax. Return null. If the parser is chained, it gives 
                // our parent a chance to handle it.
                return null;
            }

            #endregion

// end function ParseFunctionBreakpoint
        }

        #endregion

        #region Nested type: DefaultExpressionParser

        private class DefaultExpressionParser : IExpressionParser
        {
            private Dictionary<string, PrimitiveType> m_primitiveTypes;

            public DefaultExpressionParser()
            {
                InitPrimitiveTypes();
            }

            #region IExpressionParser Members

            public MDbgValue ParseExpression(string variableName, MDbgProcess process, MDbgFrame scope)
            {
                Debug.Assert(process != null);
                return process.ResolveVariable(variableName, scope);
            }

            public CorValue ParseExpression2(string value, MDbgProcess process, MDbgFrame scope)
            {
                if (value.Length == 0)
                {
                    return null;
                }
                if (RepresentsPrimitiveValue(value))
                {
                    //value is a primitive type
                    return CreatePrimitiveValue(value);
                }
                if (value[0] == '"' && value[value.Length - 1] == '"')
                {
                    //value is a string
                    return CreateString(value);
                }
                //value is some variable
                Debug.Assert(process != null);
                MDbgValue var = process.ResolveVariable(value, scope);
                return (var == null ? null : var.CorValue);
            }

            #endregion

            /// <summary>
            /// Creates a CorGenericValue object for primitive types, for use in function 
            /// evaluations and setting debugger variables.
            /// </summary>
            /// <param name="input">input has to be in the form "input" or "(type)input", where
            /// we use the ldasm naming convention (e.g. "int", "sbyte", "ushort", etc...) OR 
            /// full type names (e.g. System.Char, System.Int32) for type.
            /// Example inputs: 45, 'a', true, 556.3, (long)45, (sbyte)5, (System.Int64)65 </param>
            /// <returns>A CorGenericValue for input, or null if no type has been specified
            /// and the input string cannot be parsed.</returns>
            public CorGenericValue CreatePrimitiveValue(string input)
            {
                CorEval eval = Debugger.Processes.Active.Threads.Active.CorThread.CreateEval();
                CorValue val = null;
                CorGenericValue gv = null;
                string type = null;
                string value = null;

                ParsePrimitiveValueExpression(input, out type, out value);

                if (type == null || value == null)
                {
                    return null;
                }

                PrimitiveType inputType = m_primitiveTypes[type];
                val = eval.CreateValue(inputType.elementType, null);
                gv = val.CastToGenericValue();
                gv.SetValue(Convert.ChangeType(value, inputType.type));

                return gv;
            }

            /// <summary>
            /// Creates a CorReferenceValue for an input string.
            /// </summary>
            /// <param name="value">The input string. Must be surrounded by quotation marks,
            /// e.g. "mystring".</param>
            /// <returns>A CorReferenceValue representing the input string.</returns>
            public static CorReferenceValue CreateString(string value)
            {
                // ensure that input is in the correct format. Input must be surrounded by quotation marks.
                if (value.Length < 2 || !value.StartsWith("\"") || !value.EndsWith("\""))
                {
                    throw new MDbgShellException(
                        "Cannot create string; input is not in correct format. Input must be surrounded by quotation marks.");
                }
                CorEval eval = Debugger.Processes.Active.Threads.Active.CorThread.CreateEval();
                eval.NewString(value.Substring(1, value.Length - 2)); // strip surrounding quotation marks
                Debugger.Processes.Active.Go().WaitOne();
                if (Debugger.Processes.Active.StopReason == null)
                    DI.log.error("in CreateString, Debugger.Processes.Active.StopReason == null");
               // Debug.Assert(Debugger.Processes.Active.StopReason != null);
                if (!(Debugger.Processes.Active.StopReason is EvalCompleteStopReason))
                {
                    throw new MDbgShellException("Wrong stop reason when creating string!");
                }
                CorValue corValue = (Debugger.Processes.Active.StopReason as EvalCompleteStopReason).Eval.Result;
                return corValue.CastToReferenceValue();
            }

            /// <summary>
            /// Returns true if an input string represents a primitive value, according
            /// to the DefaultExpressionParser convention.
            /// </summary>
            /// <param name="expression">An expression is a primitive value expression if it is 
            /// of the form "value" or "(type)value", where we use the ldasm naming convention (e.g. 
            /// "int", "sbyte",  "ushort", etc...) OR full type names (e.g. "System.Char", "System.Int32") 
            /// for type.
            /// Example primitive value expressions: 45, 'a', true, 556.3, (long)45, (sbyte)5, (System.Int64)65</param>
            /// <returns></returns>
            public static bool RepresentsPrimitiveValue(string expression)
            {
                if (expression.Length == 0)
                {
                    return false;
                }
                return (Char.IsNumber(expression[0]) ||
                        expression[0] == '(' ||
                        expression[0] == '+' || expression[0] == '-' ||
                        expression[0] == '.' || expression[0] == '\'' ||
                        string.Compare(expression, "true", true) == 0 ||
                        string.Compare(expression, "false", true) == 0);
            }

            /// <summary>
            /// Parses an input string representing a primitive value.
            /// </summary>
            /// <param name="input">Expression representing a primitive value.</param>
            /// <param name="type">The type of the primitive value the input expression represents.</param>
            /// <param name="value">The value of the primitive value the input expression represents.</param>
            private void ParsePrimitiveValueExpression(string input, out string type, out string value)
            {
                if (input.Length == 0)
                {
                    type = null;
                    value = null;
                }
                else if (input[0] == '(')
                {
                    // type is specified in value
                    int index = input.IndexOf(')');
                    if (index == -1)
                    {
                        // input has no closing parenthesis
                        throw new MDbgShellException("Input is not in correct format");
                    }

                    type = input.Substring(1, index - 1).Trim();
                    if (!m_primitiveTypes.ContainsKey(type))
                    {
                        // type is not recognized as a primitive type
                        throw new MDbgShellException(String.Format(
                                                         "\"{0}\" is not recognized as a primitive type name", type));
                    }

                    value = input.Substring(index + 1).Trim();
                }
                else if (input[0] == '\'' && input[input.Length - 1] == '\'')
                {
                    // if value is of form '*', assume it represents a character.
                    value = input.Substring(1, 1);
                    type = "char";
                }
                else if (input.Contains("."))
                {
                    // if value does not represent a character, and it contains a period, 
                    // assume it represents a double.
                    type = "double";
                    value = input;
                }
                else if (Char.IsNumber(input[0]) || input[0] == '+' || input[0] == '-')
                {
                    // if value does represent a character or a double, and it begins with
                    // a number, assume it represents an integer.
                    type = "int";
                    value = input;
                }
                else if (input.ToLowerInvariant() == "true" || input.ToLowerInvariant() == "false")
                {
                    // if value is "true" or "false", assume it represents a boolean.
                    type = "bool";
                    value = input;
                }
                else
                {
                    throw new MDbgShellException("Input is not recognized as a primitive value");
                }
            }

            private void InitPrimitiveTypes()
            {
                m_primitiveTypes = new Dictionary<string, PrimitiveType>();

                var sbyteType = new PrimitiveType(typeof (SByte), CorElementType.ELEMENT_TYPE_I1);
                m_primitiveTypes.Add("sbyte", sbyteType);
                m_primitiveTypes.Add("System.SByte", sbyteType);

                var byteType = new PrimitiveType(typeof (Byte), CorElementType.ELEMENT_TYPE_U1);
                m_primitiveTypes.Add("byte", byteType);
                m_primitiveTypes.Add("System.Byte", byteType);

                var shortType = new PrimitiveType(typeof (Int16), CorElementType.ELEMENT_TYPE_I2);
                m_primitiveTypes.Add("short", shortType);
                m_primitiveTypes.Add("System.Int16", shortType);

                var intType = new PrimitiveType(typeof (Int32), CorElementType.ELEMENT_TYPE_I4);
                m_primitiveTypes.Add("int", intType);
                m_primitiveTypes.Add("System.Int32", intType);

                var longType = new PrimitiveType(typeof (Int64), CorElementType.ELEMENT_TYPE_I8);
                m_primitiveTypes.Add("long", longType);
                m_primitiveTypes.Add("System.Int64", longType);

                var ushortType = new PrimitiveType(typeof (UInt16), CorElementType.ELEMENT_TYPE_U2);
                m_primitiveTypes.Add("ushort", ushortType);
                m_primitiveTypes.Add("System.UInt16", ushortType);

                var uintType = new PrimitiveType(typeof (UInt32), CorElementType.ELEMENT_TYPE_U4);
                m_primitiveTypes.Add("uint", uintType);
                m_primitiveTypes.Add("System.UInt32", uintType);

                var ulongType = new PrimitiveType(typeof (UInt64), CorElementType.ELEMENT_TYPE_U8);
                m_primitiveTypes.Add("ulong", ulongType);
                m_primitiveTypes.Add("System.UInt64", ulongType);

                var floatType = new PrimitiveType(typeof (Single), CorElementType.ELEMENT_TYPE_R4);
                m_primitiveTypes.Add("float", floatType);
                m_primitiveTypes.Add("System.Single", floatType);

                var doubleType = new PrimitiveType(typeof (Double), CorElementType.ELEMENT_TYPE_R8);
                m_primitiveTypes.Add("double", doubleType);
                m_primitiveTypes.Add("System.Double", doubleType);

                var boolType = new PrimitiveType(typeof (Boolean), CorElementType.ELEMENT_TYPE_BOOLEAN);
                m_primitiveTypes.Add("bool", boolType);
                m_primitiveTypes.Add("System.Boolean", boolType);

                var charType = new PrimitiveType(typeof (Char), CorElementType.ELEMENT_TYPE_CHAR);
                m_primitiveTypes.Add("char", charType);
                m_primitiveTypes.Add("System.Char", charType);
            }

            #region Nested type: PrimitiveType

            private struct PrimitiveType
            {
                public readonly CorElementType elementType;
                public readonly Type type;

                public PrimitiveType(Type type, CorElementType elementType)
                {
                    this.type = type;
                    this.elementType = elementType;
                }
            }

            #endregion

            // Dictionary mapping primitive type names to PrimitiveValue objects
        }

        #endregion

        #region Nested type: ExecuteCmdAction

        private class ExecuteCmdAction
        {
            private static int g_id;
            private readonly string m_actionString;
            private readonly int m_id;
            private readonly IMDbgShell m_mdbg;
            private readonly Type m_stopReasonType;
            private bool bHaveCondition;
            public bool IsRunning;
            private string m_name;
            private int m_number;

            public ExecuteCmdAction(IMDbgShell mdbg,
                                    string actionString,
                                    Type stopReasonType)
            {
                Debug.Assert(mdbg != null);
                Debug.Assert(actionString != null);
                Debug.Assert(stopReasonType != null);
                m_mdbg = mdbg;
                m_actionString = actionString;
                m_stopReasonType = stopReasonType;
                m_id = g_id++;
            }

            public int Id
            {
                get { return m_id; }
            }

            public string Name
            {
                get { return m_name; }
                set
                {
                    Debug.Assert(value != null);
                    m_name = value;
                    bHaveCondition = true;
                }
            }

            public int Number
            {
                get { return m_number; }
                set
                {
                    m_number = value;
                    bHaveCondition = true;
                }
            }

            public bool ShouldExecuteNow
            {
                get
                {
                    if (!m_mdbg.Debugger.Processes.HaveActive)
                    {
                        if (m_stopReasonType.Name == "ProcessExitedStopReason")
                        {
                            // special case when process exits
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    Object stopReason = m_mdbg.Debugger.Processes.Active.StopReason;
                    bool correctStopReason = m_stopReasonType == stopReason.GetType();
                    if (!correctStopReason)
                    {
                        return false;
                    }
                    if (!bHaveCondition)
                    {
                        return true;
                    }

                    if (m_stopReasonType == typeof (ThreadCreatedStopReason))
                    {
                        //execute on ThreadCreated [num] do
                        return Number == (stopReason as ThreadCreatedStopReason).Thread.Number;
                    }

                    else if (m_stopReasonType == typeof (BreakpointHitStopReason))
                    {
                        //execute on BreakpointHit [num] do
                        return Number == (stopReason as BreakpointHitStopReason).Breakpoint.Number;
                    }

                    else if (m_stopReasonType == typeof (ModuleLoadedStopReason))
                    {
                        //execute on ModuleLoaded [name] do
                        return IdentifierEquals(Name, Path.GetFileName((stopReason as ModuleLoadedStopReason).
                                                                           Module.CorModule.Name));
                    }
                    else if (m_stopReasonType == typeof (ClassLoadedStopReason))
                    {
                        //execute on ClassLoaded [name] do
                        throw new NotImplementedException();
                    }

                    else if (m_stopReasonType == typeof (AssemblyLoadedStopReason))
                    {
                        //execute on AssemblyLoaded [name] do 
                        return IdentifierEquals(Name, Path.GetFileName((stopReason as AssemblyLoadedStopReason).
                                                                           Assembly.Name));
                    }

                    else if (m_stopReasonType == typeof (AssemblyUnloadedStopReason))
                    {
                        //execute on AssemblyUnloaded [name] do
                        throw new NotImplementedException();
                    }

                    else if (m_stopReasonType == typeof (ExceptionThrownStopReason))
                    {
                        //execute on ExceptionThrown [name] do
                        MDbgValue ex = Debugger.Processes.Active.Threads.Active.CurrentException;
                        return IdentifierEquals(Name, ex.TypeName);
                    }
                    else if (m_stopReasonType == typeof (UnhandledExceptionThrownStopReason))
                    {
                        //execute on UnhandledExceptionThrown [name] do
                        MDbgValue ex = Debugger.Processes.Active.Threads.Active.CurrentException;
                        return IdentifierEquals(Name, ex.TypeName);
                    }
                    else
                    {
                        Debug.Assert(false);
                    }
                    return false;
                }
            }

            // returns wheather conditionString matches identifier.
            // currently legal operation for conditionString are:
            //     identifier     - matches identifier with the same name
            //     !identifier    - matches identifier that is of different name than identifier
            private bool IdentifierEquals(string conditionString, string identifier)
            {
                bool negation = conditionString.StartsWith("!");
                if (negation)
                {
                    conditionString = conditionString.Substring(1); // get rid of !
                }

                bool match = String.Compare(conditionString, identifier,
                                            true, //ignoreCase
                                            CultureInfo.InvariantCulture
                                 ) == 0;
                if (negation)
                {
                    match = !match;
                }

                return match;
            }

            public void Execute()
            {
                string[] cmds = m_actionString.Split(new[] {'\n'}); // this will split string by "\n".
                int i;
                for (i = 0; i < cmds.Length; i++)
                {
                    ExecuteCommand(cmds[i]);
                }
            }


            public override string ToString()
            {
                string condition = "";
                if (bHaveCondition)
                {
                    switch (m_stopReasonType.Name)
                    {
                        case "BreakpointHitStopReason":
                        case "ThreadCreatedStopReason":
                            condition = "Number=" + Number;
                            break;

                        case "ModuleLoadedStopReason":
                        case "ClassLoadStopReason":
                        case "AssemblyLoadedStopReason":
                        case "AssemblyUnloadedStopReason":
                        case "ExceptionThrownStopReason":
                        case "UnhandledExceptionThrownStopReason":
                            condition = "Name=\"" + Name + "\"";
                            break;

                        default:
                            condition = "unknown";
                            Debug.Assert(false);
                            break;
                    }
                    condition = " (" + condition + ")";
                }

                string astring = m_actionString.Replace("\n", ";");
                return "when " + m_stopReasonType.Name.Replace("StopReason", "") + condition + " do: " + astring;
            }
        }

        #endregion

        #region Nested type: MdbgSymbol

        public class MdbgSymbol
        {
            public readonly string ClassName;
            public readonly string Method;
            public readonly int ModuleNumber;
            public readonly int Offset;

            public MdbgSymbol(int moduleNumber, string className, string method, int offset)
            {
                Debug.Assert(className != null & method != null);

                ModuleNumber = moduleNumber;
                ClassName = className;
                Method = method;
                Offset = offset;
            }
        }

        #endregion

        #region Nested type: MDbgSymbolCache

        public class MDbgSymbolCache
        {
            private readonly ArrayList m_list = new ArrayList();

            public MdbgSymbol Retrieve(int symbolNumber)
            {
                if (symbolNumber < 0 || symbolNumber >= m_list.Count)
                {
                    throw new ArgumentException();
                }
                return (MdbgSymbol) m_list[symbolNumber];
            }

            public void Clear()
            {
                m_list.Clear();
            }

            public int Add(MdbgSymbol symbol) // will return symbol id.
            {
                m_list.Add(symbol);
                return m_list.Count - 1;
            }
        }

        #endregion

        #region Nested type: ThreadNickNames

        public class ThreadNickNames
        {
            private Hashtable m_threadNickNames;

            private Hashtable NickNamesHash
            {
                get
                {
                    if (m_threadNickNames == null)
                    {
                        m_threadNickNames = new Hashtable();
                    }
                    return m_threadNickNames;
                }
            }

            public MDbgThread GetThreadByNickName(string nickName)
            {
                Debug.Assert(nickName != null);
                if (nickName == null)
                {
                    throw new ArgumentException();
                }
                if (IsNumber(nickName))
                {
                    return Debugger.Processes.Active.Threads[Int32.Parse(nickName, CultureInfo.InvariantCulture)];
                }
                if (m_threadNickNames != null && m_threadNickNames.ContainsKey(nickName))
                {
                    return Debugger.Processes.Active.Threads[(int) NickNamesHash[nickName]];
                }
                else
                {
                    return null;
                }
            }

            public string GetThreadName(MDbgThread thread)
            {
                Debug.Assert(thread != null);
                if (thread == null)
                {
                    throw new ArgumentException();
                }
                string nick = GetNickNameFromThreadNumber(thread.Number);
                if (nick.Length == 0)
                {
                    // no nick name
                    return thread.Number.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    return nick;
                }
            }

            public void SetThreadNickName(string nickName, MDbgThread thread)
            {
                Debug.Assert(thread != null);
                if (thread == null) throw new ArgumentException();
                {
                    NickNamesHash.Remove(GetNickNameFromThreadNumber(thread.Number)); // remove old nick-name, if any
                }
                if (nickName == null || nickName.Length == 0)
                {
                    return; // we just want to remove nickname
                }

                if (IsNumber(nickName))
                {
                    throw new MDbgShellException("invalid nickname");
                }

                if (NickNamesHash.ContainsKey(nickName))
                {
                    throw new MDbgShellException("nickname already exists");
                }
                NickNamesHash.Add(nickName, Debugger.Processes.Active.Threads.Active.Number);
            }

            private string GetNickNameFromThreadNumber(int threadNumber)
            {
                if (m_threadNickNames == null)
                {
                    return "";
                }
                foreach (DictionaryEntry e in NickNamesHash)
                {
                    if (threadNumber == (int) e.Value)
                    {
                        return (string) e.Key;
                    }
                }
                return "";
            }

            private static bool IsNumber(string text)
            {
                int value;
                return Int32.TryParse(text, NumberStyles.Integer, CultureInfo.CurrentUICulture, out value);
            }
        }

        #endregion
    }
}
