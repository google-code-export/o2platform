using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using O2.Debugger.Mdbg.Debugging.CorDebug;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;
using O2.Debugger.Mdbg.Debugging.CorPublish;
using O2.Debugger.Mdbg.Debugging.MdbgEngine;
using O2.Debugger.Mdbg.O2Debugger;
using O2.Debugger.Mdbg.Tools.Mdbg;
using O2.DotNetWrappers.DotNet;

namespace O2.Debugger.Mdbg.OriginalMdbgCode.mdbg
{
    public class mdbgCommandsCustomizedForO2
    {
        public static void FuncEvalCmd(string arguments, IMDbgShell Shell, O2Thread.FuncVoidT1<string> execOnEval)
        {
            try
            {

                var activeProcess = DI.o2MDbg.ActiveProcess; //Debugger.Processes.Active
                const string appDomainOption = "ad";
                var ap = new ArgParser(arguments, appDomainOption + ":1");
                if (!(ap.Count >= 1))
                {
                    throw new MDbgShellException("Not Enough arguments");
                }


                // Currently debugger picks first function -- we have not implementing resolving overloaded functions.
                // Good example is Console.WriteLine -- there is 18 different types:
                // 1) [06000575] Void WriteLine()
                // 2) [06000576] Void WriteLine(Boolean)
                // 3) [06000577] Void WriteLine(Char)
                // 4) [06000578] Void WriteLine(Char[])
                // 5) [06000579] Void WriteLine(Char[], Int32, Int32)
                // 6) [0600057a] Void WriteLine(Decimal)
                // 7) [0600057b] Void WriteLine(Double)
                // 8) [0600057c] Void WriteLine(Single)
                // 9) [0600057d] Void WriteLine(Int32)
                // 10) [0600057e] Void WriteLine(UInt32)
                // 11) [0600057f] Void WriteLine(Int64)
                // 12) [06000580] Void WriteLine(UInt64)
                // 13) [06000581] Void WriteLine(Object)
                // 14) [06000582] Void WriteLine(String)
                // 15) [06000583] Void WriteLine(String, Object)
                // 16) [06000584] Void WriteLine(String, Object, Object)
                // 17) [06000585] Void WriteLine(String, Object, Object, Object)
                // 18) [06000586] Void WriteLine(String, Object, Object, Object, Object, ...)
                // 19) [06000587] Void WriteLine(String, Object[])
                //
                CorAppDomain appDomain;
                if (ap.OptionPassed(appDomainOption))
                {
                    MDbgAppDomain ad = activeProcess.AppDomains[ap.GetOption(appDomainOption).AsInt];
                    if (ad == null)
                    {
                        throw new ArgumentException("Invalid Appdomain Number");
                    }
                    appDomain = ad.CorAppDomain;
                }
                else
                {
                    appDomain = activeProcess.Threads.Active.CorThread.AppDomain;
                }

                MDbgFunction func = activeProcess.ResolveFunctionNameFromScope(ap.AsString(0), appDomain);
                if (null == func)
                {
                    throw new MDbgShellException(String.Format(CultureInfo.InvariantCulture, "Could not resolve {0}",
                                                               new Object[] {ap.AsString(0)}));
                }

                CorEval eval = activeProcess.Threads.Active.CorThread.CreateEval();

                // Get Variables
                var vars = new ArrayList();
                String arg;
                for (int i = 1; i < ap.Count; i++)
                {
                    arg = ap.AsString(i);

                    CorValue v = Shell.ExpressionParser.ParseExpression2(arg, activeProcess,
                                                                         activeProcess.Threads.Active.
                                                                             CurrentFrame);

                    if (v == null)
                    {
                        throw new MDbgShellException("Cannot resolve expression or variable " + ap.AsString(i));
                    }

                    if (v is CorGenericValue)
                    {
                        vars.Add(v);
                    }

                    else
                    {
                        CorHeapValue hv = v.CastToHeapValue();
                        if (hv != null)
                        {
                            // we cannot pass directly heap values, we need to pass reference to heap valus
                            CorReferenceValue myref =
                                eval.CreateValue(CorElementType.ELEMENT_TYPE_CLASS, null).CastToReferenceValue();
                            myref.Value = hv.Address;
                            vars.Add(myref);
                        }
                        else
                        {
                            vars.Add(v);
                        }
                    }
                }

                eval.CallFunction(func.CorFunction, (CorValue[]) vars.ToArray(typeof (CorValue)));
                activeProcess.Go().WaitOne();

                // now display result of the funceval
                if (!(activeProcess.StopReason is EvalCompleteStopReason))
                {
                    // we could have received also EvalExceptionStopReason but it's derived from EvalCompleteStopReason
                    Shell.IO.WriteOutput(MDbgOutputConstants.StdOutput,
                                         "Func-eval not fully completed and debuggee has stopped");
                    Shell.IO.WriteOutput(MDbgOutputConstants.StdOutput,
                                         "Result of funceval won't be printed when finished.");
                }
                else
                {
                    eval = (activeProcess.StopReason as EvalCompleteStopReason).Eval;
                    Debug.Assert(eval != null);

                    CorValue cv = eval.Result;
                    if (cv != null)
                    {
                        var mv = new MDbgValue(activeProcess, cv);
                        if (execOnEval != null) // if this callback is set then execute 
                        {
                            execOnEval(mv.GetStringValue(1));                         
                            return;
                        }
                        Shell.IO.WriteOutput(MDbgOutputConstants.StdOutput, "result = " + mv.GetStringValue(1));
                        if (cv.CastToReferenceValue() != null)
                            if (activeProcess.DebuggerVars.SetEvalResult(cv))
                                Shell.IO.WriteOutput(MDbgOutputConstants.StdOutput, "results saved to $result");

                    }
                }               
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in FuncEvalCmd");
            }
            if (execOnEval != null)                         // need to call this here so that the sync AutoResetEvent is set
                execOnEval(null);
        }


        public static void ProcessEnumCmd(string arguments, O2Thread.FuncVoidT1<CorPublishProcess> handleManagedProcess) // extended with Lambda method
        {
            var cp = new CorPublish();

            CommandBase.WriteOutput("Active processes on current machine:");
            foreach (CorPublishProcess cpp in cp.EnumProcesses())
            {
                if (Process.GetCurrentProcess().Id == cpp.ProcessId) // let's hide our process
                {
                    continue;
                }

                // Try and get the list of AppDomains, but watch for the process terminating
                IEnumerable appDomainEnum;
                try
                {
                    appDomainEnum = cpp.EnumAppDomains();
                }
                catch (COMException e)
                {
                    if ((uint) e.ErrorCode == 0x80131301) //CORDBG_E_PROCESS_TERMINATED
                    {
                        continue; // process was terminated, ignore it
                    }
                    throw; // let error propogate up
                }
                if (handleManagedProcess != null)
                    handleManagedProcess(cpp);
                else
                {
                    CommandBase.WriteOutput("(PID: " + cpp.ProcessId + ") " + cpp.DisplayName);
                    foreach (CorPublishAppDomain cpad in appDomainEnum)
                        CommandBase.WriteOutput("\t(ID: " + cpad.Id + ") " + cpad.Name);
                }
            }
        }
        // O2.Debugger.Mdbg.OriginalMdbgCode.mdbg.mdbgCommandsCustomizedForO2
        // , O2Thread.FuncVoid<string> o2Callback)
        public static bool AttachCmd(string arguments, O2Thread.FuncVoidT1<string> o2Callback)
        {
            try
            {
                var ap = new ArgParser(arguments);
                if (ap.Count > 1)
                {
                    DI.log.error("in AttachCmd: Wrong # of arguments.");
                    return false;
                }

                if (!ap.Exists(0))
                {
                    DI.log.error("in AttachCmd: Please choose some process to attach");
                    MdbgCommands.ProcessEnumCmd("");
                    return false;
                }
                int pid = ap.AsInt(0);

                if (Process.GetCurrentProcess().Id == pid)
                {
                    DI.log.error("in AttachCmd: Cannot attach to myself!");
                    return false;
                }

                MDbgProcess p = CommandBase.Debugger.Attach(pid);
                p.Go().WaitOne();
                return true;
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in AttachCmd");
                return false;
            }                        
        }

        public static void PrintCmd(string arguments, O2Thread.FuncVoidT1<string> o2Callback)
        {
            const string debuggerVarsOpt = "d";
            const string noFuncevalOpt = "nf";
            const string expandDepthOpt = "r";

            var ap = new ArgParser(arguments, debuggerVarsOpt + ";" + noFuncevalOpt + ";" + expandDepthOpt + ":1");
            bool canDoFunceval = ! ap.OptionPassed(noFuncevalOpt);

            int? expandDepth = null; // we use optional here because
            // different codes bellow has different
            // default values.
            if (ap.OptionPassed(expandDepthOpt))
            {
                expandDepth = ap.GetOption(expandDepthOpt).AsInt;
                if (expandDepth < 0)
                    throw new MDbgShellException("Depth cannot be negative.");
            }

            MDbgFrame frame = CommandBase.Debugger.Processes.Active.Threads.Active.CurrentFrame;
            if (ap.OptionPassed(debuggerVarsOpt))
            {
                // let's print all debugger variables
                MDbgProcess p = CommandBase.Debugger.Processes.Active;
                foreach (MDbgDebuggerVar dv in p.DebuggerVars)
                {
                    var v = new MDbgValue(p, dv.CorValue);
                    CommandBase.WriteOutput(dv.Name + "=" + v.GetStringValue(expandDepth == null ? 0 : (int) expandDepth,
                                                                 canDoFunceval));
                }
            }
            else
            {
                if (ap.Count == 0)
                {
                    // get all active variables
                    MDbgFunction f = frame.Function;

                    var vars = new ArrayList();
                    MDbgValue[] vals = f.GetActiveLocalVars(frame);
                    if (vals != null)
                    {
                        vars.AddRange(vals);
                    }

                    vals = f.GetArguments(frame);
                    if (vals != null)
                    {
                        vars.AddRange(vals);
                    }
                    foreach (MDbgValue v in vars)
                    {
                        CommandBase.WriteOutput(v.Name + "=" + v.GetStringValue(expandDepth == null ? 0 : (int) expandDepth,
                                                                    canDoFunceval));
                    }
                }
                else
                {
                    // user requested printing of specific variables
                    for (int j = 0; j < ap.Count; ++j)
                    {
                        MDbgValue var = CommandBase.Debugger.Processes.Active.ResolveVariable(ap.AsString(j), frame);
                        if (var != null)
                        {
                            CommandBase.WriteOutput(ap.AsString(j) + "=" + var.GetStringValue(expandDepth == null
                                                                                      ? 1
                                                                                      : (int) expandDepth, canDoFunceval));
                        }
                        else
                        {
                            throw new MDbgShellException("Variable not found");
                        }
                    }
                }
            }
        }

        public static void WhereCmd(string arguments, O2Thread.FuncVoidT1<string> o2Callback)
        {
            const int default_depth = 100; // default number of frames to print

            const string countOpt = "c";
            const string verboseOpt = "v";

            var ap = new ArgParser(arguments, countOpt + ":1;" + verboseOpt);
            int depth = default_depth;
            if (ap.OptionPassed(countOpt))
            {
                ArgToken countArg = ap.GetOption(countOpt);
                if (countArg.AsString == "all")
                {
                    depth = 0; // 0 means print entire stack0
                }
                else
                {
                    depth = countArg.AsInt;
                    if (depth <= 0)
                    {
                        throw new MDbgShellException("Depth must be positive number or string \"all\"");
                    }
                }
            }
            if (ap.Count != 0 && ap.Count != 1)
            {
                throw new MDbgShellException("Wrong # of arguments.");
            }

            if (ap.Count == 0)
            {
                // print current thread only
                InternalWhereCommand(CommandBase.Debugger.Processes.Active.Threads.Active, depth, ap.OptionPassed(verboseOpt));
            }
            else if (ap.AsString(0).Equals("all"))
            {
                foreach (MDbgThread t in CommandBase.Debugger.Processes.Active.Threads)
                    InternalWhereCommand(t, depth, ap.OptionPassed(verboseOpt));
            }
            else
            {
                MDbgThread t = CommandBase.Debugger.Processes.Active.Threads[ap.AsInt(0)];
                if (t == null)
                {
                    throw new MDbgShellException("Wrong thread number");
                }
                else
                {
                    InternalWhereCommand(t, depth, ap.OptionPassed(verboseOpt));
                }
            }
        }

        public static void InternalWhereCommand(MDbgThread thread, int depth, bool verboseOutput)
        {
            if (thread != null)
            {                

                CommandBase.WriteOutput("Thread [#:" + MdbgCommands.g_threadNickNames.GetThreadName(thread) + "]");

                MDbgFrame af = thread.HaveCurrentFrame ? thread.CurrentFrame : null;
                MDbgFrame f = thread.BottomFrame;
                int i = 0;
                while (f != null && (depth == 0 || i < depth))
                {
                    string line;
                    if (f.IsInfoOnly)
                    {
                        if (!CommandBase.ShowInternalFrames)
                        {
                            // in cases when we don't want to show internal frames, we'll skip them
                            f = f.NextUp;
                            continue;
                        }
                        line = String.Format(CultureInfo.InvariantCulture, "    {0}", f);
                    }
                    else
                    {
                        string frameDescription = f.ToString(verboseOutput ? "v" : null);
                        line = String.Format(CultureInfo.InvariantCulture, "{0}{1}. {2}", f.Equals(af) ? "*" : " ", i,
                                             frameDescription);
                        ++i;
                    }
                    CommandBase.WriteOutput(line);
                    f = f.NextUp;
                }
                if (f != null && depth != 0) // means we still have some frames to show....
                {
                    CommandBase.WriteOutput(String.Format(CultureInfo.InvariantCulture,
                                                          "displayed only first {0} frames. For more frames use -c switch",
                                                          depth));
                }
            }
        }

        public static void ListBreakpoints(O2Thread.FuncVoidT1<string> o2Callback)
        {
            if (CommandBase.Debugger.Processes.HaveActive)
            {
                MDbgBreakpointCollection breakpoints = CommandBase.Debugger.Processes.Active.Breakpoints;
                if (o2Callback != null)
                    CommandBase.WriteOutput("Current breakpoints:");
                bool haveBps = false;
                foreach (MDbgBreakpoint b in breakpoints)
                {
                    if (o2Callback != null)
                        o2Callback(b.ToString());
                    else
                        CommandBase.WriteOutput(b.ToString());
                    haveBps = true;
                }
                if (!haveBps)
                {
                    if (o2Callback != null)
                        DI.log.debug("There are no breakpoints set in the current active process");
                    else
                        CommandBase.WriteOutput("No breakpoints!");
                }
            }
        }

        public static void BreakCmd(string arguments, O2Thread.FuncVoidT1<string> o2Callback)
        {
            if (arguments.Length == 0)
            {
                ListBreakpoints(o2Callback);
                return;
            }

            // We're adding a breakpoint. Parse the argument string.
            MDbgBreakpointCollection breakpoints = CommandBase.Debugger.Processes.Active.Breakpoints;
            ISequencePointResolver bploc = CommandBase.Shell.BreakpointParser.ParseFunctionBreakpoint(arguments);            
            if (bploc == null)
            {
                throw new MDbgShellException("Invalid breakpoint syntax.");
            }

            MDbgBreakpoint bpnew = CommandBase.Debugger.Processes.Active.Breakpoints.CreateBreakpoint(bploc);
            CommandBase.WriteOutput(bpnew.ToString());
        }

        public static void DeleteCmd(string arguments, O2Thread.FuncVoidT1<string> o2Callback)

        {
            var ap = new ArgParser(arguments);
            if (ap.Count != 1)
            {
                CommandBase.WriteOutput("Please choose some breakpoint to delete");
                MdbgCommands.BreakCmd("");
                return;
            }

            MDbgBreakpoint breakpoint = CommandBase.Debugger.Processes.Active.Breakpoints[ap.AsInt(0)];
            if (breakpoint == null)
            {
                throw new MDbgShellException("Could not find breakpint #:" + ap.AsInt(0));
            }
            else
            {
                breakpoint.Delete();
            }
        }

        public static void ShowCmd(string arguments, O2Thread.FuncVoidT1<string> o2Callback)
        {

            MDbgSourcePosition pos = CommandBase.Debugger.Processes.Active.Threads.Active.CurrentSourcePosition;
            
            if (pos == null)
            {
                throw new MDbgShellException("No source location");
            }

            string fileLoc = CommandBase.Shell.FileLocator.GetFileLocation(pos.Path);
            if (fileLoc == null)
            {
                throw new MDbgShellException(String.Format(CultureInfo.InvariantCulture,
                                                           "Source file '{0}' not available.", pos.Path));
            }

            IMDbgSourceFile file = CommandBase.Shell.SourceFileMgr.GetSourceFile(fileLoc);

            var ap = new ArgParser(arguments);
            if (ap.Count > 1)
            {
                throw new MDbgShellException("Wrong # of arguments.");
            }

            int around;
            if (ap.Exists(0))
            {
                around = ap.AsInt(0);
            }
            else
            {
                around = 3;
            }

            int lo, hi;
            lo = pos.Line - around;
            if (lo < 1)
            {
                lo = 1;
            }
            hi = pos.Line + around;
            if (hi > file.Count)
            {
                hi = file.Count;
            }

            for (int i = lo; i < hi; i++)
            {
                CommandBase.WriteOutput(String.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", i, i == pos.Line ? ":*" : "  ",
                                          file[i]));
            }
        }
    }
}
