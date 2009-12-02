using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using O2.Debugger.Mdbg.Debugging.CorDebug;
using O2.Debugger.Mdbg.Debugging.MdbgEngine;
using O2.Debugger.Mdbg.OriginalMdbgCode.mdbg;
using O2.Debugger.Mdbg.Tools.Mdbg;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Kernel.CodeUtils;
using O2.Kernel.Interfaces.Messages;

namespace O2.Debugger.Mdbg.O2Debugger.Objects
{
    public class O2MDbg
    {
        public MDbgShell shell  { set; get; }
        public O2MDbgSessionData sessionData  { set; get; }
        public O2MDbgBreakPoint BreakPoints { get; set; }
        public event Callbacks.dMethod_String OnCommandExecuted;
        public event Callbacks.dMethod onBreakEvent;
        public Dictionary<string,List<Func<O2MDbgCurrentLocation,Dictionary<string,O2MDbgVariable>,bool>>>  registeredOnBreakEventCallbacks;
        public string lastCommandExecutionMessage = "";
        public bool LogInternalMDbgMessage { get; set; }
        public bool LogCommandExecutionMessage  {get;set;}
        public bool AnimateOnStepEvent { get; set; }
        public bool AutoContinueOnBreakPointEvent { get; set; }
        public bool LogBreakpointEvent { get; set; }

        public bool debugggerActive;
        public bool debugggerRunning;

        public AutoResetEvent o2MdbgIsReady = new AutoResetEvent(false);
        //public O2Timer attachedTime;
        //public bool goAfterAttach = true;
        //public bool isAttached;
        //public string pathToMainAssembly;
        //public MDbgEngine mDbgEngine = new MDbgEngine();        

        public O2MDbgCurrentLocation currentLocation { get; set; }
        
        public O2MDbg() : this(null)
        {            
        }

        public O2MDbg(O2Thread.FuncVoid onShellStart)            
        {
            if (DI.o2MDbg != null)
                DI.log.error("DI.o2MDbg != null, and we should only have one instance of the O2MDbg per AppDomain, so this will override that one (and some data might be lost)");
            DI.o2MDbg = this;                              
            
            sessionData = new O2MDbgSessionData(this);
            BreakPoints = new O2MDbgBreakPoint(this);
            LogInternalMDbgMessage = false;
            LogCommandExecutionMessage = true;
            AnimateOnStepEvent = false;
            AutoContinueOnBreakPointEvent = false;
            LogBreakpointEvent = true;
            debugggerActive = false;
            debugggerRunning = false;

            startMDbg(onShellStart);   
        }

        public static O2MDbg getO2MDbg()
        {
            return DI.o2MDbg;
        }

        public void startMDbg(O2Thread.FuncVoid onShellStart)
        {
            if (shell == null)
                O2Thread.mtaThread(() =>
                {
                    setMDbgEventsMessagesCallbacks();
                    shell = new MDbgShell();                    
                    shell.Start(new string[0]);
                    setO2MDbgShellCallbacks();
                    o2MdbgIsReady.Set();
                    if (onShellStart != null)
                        onShellStart();
                });
        }

       

        public void stopMDbg()
        {
            o2MdbgIsReady.Reset();
            DI.o2MDbg = null;
            shell.QuitWithExitCode(0);         
        }

        public void attachToProcess(string processIdToAttach)
        {
            if (mdbgCommandsCustomizedForO2.AttachCmd(processIdToAttach,null))
            {
                DI.log.info("Successfully Attached to Process ID {0}", processIdToAttach);
            }
        }

        private void setMDbgEventsMessagesCallbacks()
        {
            OriginalMDbgMessages.commandExecutionMessage += handleCommandExecutionMessage;
            OriginalMDbgMessages.internalMDbgMessage += handleInternalMDbgMessage;            
        }

        private void setO2MDbgShellCallbacks()
        {
            DI.o2MDbg.shell.Debugger.Processes.ProcessAdded += Processes_ProcessAdded;
        }

        void Processes_ProcessAdded(object sender, ProcessCollectionChangedEventArgs e)
        {
            // now that a process has been added, lets add a onBreakpoint event to it
            setOnBreakpointEvent();

            // and let others that we have a new debug sessions
            O2Messages.raiseO2MDbgAction(IM_O2MdbgActions.startDebugSession);
        }
        
        public void execSync(string commandToExecute)
        {
            try
            {
                string input = (commandToExecute != "") ? commandToExecute : "help";

                IMDbgCommand cmd;
                string cmdArgs;
                shell.Commands.ParseCommand(input, out cmd, out cmdArgs);
                if (cmd != null)
                    cmd.Execute(cmdArgs);
               if (OnCommandExecuted!=null)
                    OnCommandExecuted(commandToExecute);
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in O2MDbg.execSync");
            }
        }

        public Thread executeMDbgCommand(string commandToExecute)
        {            
            return O2Thread.mtaThread(() => execSync(commandToExecute));
        }

        public void breakIntoDebuggedProcess()
        {
            if (shell.Debugger.Processes.Active!=null)
                shell.Debugger.Processes.Active.AsyncStop().WaitOne();            
        }

        public bool IsRunning
        {
            get {
                //if (shell == null || shell.Debugger == null || shell.Debugger.Processes == null)
                //    return false;
                //return shell.Debugger.Processes.HaveActive && shell.Debugger.Processes.Active.CorProcess!= null && shell.Debugger.Processes.Active.CorProcess.IsRunning();
                try
                {
                    return shell.Debugger.Processes.HaveActive && shell.Debugger.Processes.Active.CorProcess.IsRunning();
                }
                catch (Exception ex)
                {
                    DI.log.ex(ex, "in O2MDbg.IsRunning");
                    return false;
                }
            }
        }

        public bool IsActive
        {
            get
            {
                try
                {
                    return shell.Debugger.Processes.HaveActive;
                }
                catch (Exception ex)
                {
                    DI.log.ex(ex, "in O2MDbg.IsActive");
                    return false;
                }
            }
        }

        public MDbgProcess ActiveProcess
        {
            get
            {
                if (shell.Debugger.Processes.HaveActive)
                    return shell.Debugger.Processes.Active;           
                return null;
            }
        }

        public void updateDebuggerRunningAndActiveStatus(O2Thread.FuncVoid updateCompleteCallback)
        {
            O2Thread.mtaThread(() =>
            {
                if (DI.o2MDbg != null)
                {
                    debugggerRunning = DI.o2MDbg.IsRunning;
                    debugggerActive = DI.o2MDbg.IsActive;
                    if (updateCompleteCallback != null)
                        updateCompleteCallback();
                }
            });
        }

        public Form showShellTestGuiAsNewWindowsForm(bool loadAsApplication)
        {
            var o2MDbgShell = new ascx_O2MdbgShell();
            return O2Forms.loadAscxAsMainForm(o2MDbgShell, loadAsApplication);
        }

        public bool handleCommandExecutionMessage(string message)
        {
            if (LogCommandExecutionMessage)
                DI.log.info("[MDbg.CommandExecutionMessage]: {0}", message);
            lastCommandExecutionMessage = message;
            O2Messages.raiseO2MDbgCommandExecutionMessage(IM_O2MdbgActions.commandExecutionMessage, lastCommandExecutionMessage);
            return true;
        }

        public bool handleInternalMDbgMessage(string message)
        {
            if (LogInternalMDbgMessage)
                DI.log.info("[MDbg.InternalMessage]: {0}", message);
            return true;
        }


        public List<string> animateOver(int sleepInterval)
        {
            DI.log.info("Start: Animate Over");
            AnimateOnStepEvent = true;
            var instructions = new List<String> {lastCommandExecutionMessage};

            if (IsActive && !IsRunning)
            {
                while (continueAnimation())
                {
                    var stepOverThread = O2MDbgUtils.stepOver();
                    stepOverThread.Join();
                    /*execSync(O2MDbgCommands.stepOver());
                    instructions.Add(lastCommandExecutionMessage);
                    if (ignoreThisSignature(lastCommandExecutionMessage))
                        execSync(O2MDbgCommands.stepBack());
                    else*/

                    instructions.Add(lastCommandExecutionMessage);
                    if (sleepInterval > 0)
                        Processes.Sleep(sleepInterval,false);
                }
            }
            else
            {
                DI.log.e("in animateOver either the process is not active or is running");
            }
            DI.log.info("End: Animate Over");
            return instructions;
        }

        public List<string> animateInto(int sleepInterval)
        {            
            DI.log.info("Start: Animate Into");
            AnimateOnStepEvent = true;
            var instructions = new List<String> {lastCommandExecutionMessage};

            if (IsActive && !IsRunning)
            {
                while (continueAnimation())
                {             
                   // if (ignoreThisSignature(lastCommandExecutionMessage))
                   //     execSync(O2MDbgCommands.stepBack());
                   // else
                    
                     //execSync(O2MDbgCommands.stepInto());
                    var threadStepInto = O2MDbgUtils.stepInto();
                    threadStepInto.Join();

                    //var caller = shell.Debugger.Processes.Active.Threads.Active.CurrentFrame.CorFrame.Caller;
                    //var callee = shell.Debugger.Processes.Active.Threads.Active.CurrentFrame.CorFrame.Callee;
                    // don't step into code that we don't have the debug symbols for (namely the .Net framework)
                                                                                                            
                    instructions.Add(lastCommandExecutionMessage);                                            
                    if (sleepInterval >0)
                        Processes.Sleep(sleepInterval, false);
                }                
            }
            else
            {
                DI.log.e("in animateInto either the process is not active or is running");
            }
            DI.log.info("Stop: Animate Into");
            return instructions;
        }

        public bool continueAnimation()
        {
            return AnimateOnStepEvent && IsActive && !IsRunning;
        }

        public bool ignoreThisSignature(string signatureToAnalyze)
        {
            return (signatureToAnalyze.IndexOf("MAPPING_EXACT") > -1 || signatureToAnalyze.IndexOf("MAPPING_APPROXIMATE") > -1);
        }

        public List<string> animateBack(int sleepInterval)        
        {
            DI.log.info("Start: Animate Back");
            var instructions = new List<String> {lastCommandExecutionMessage};

            if (IsActive && !IsRunning)
            {
                while (continueAnimation())
                {
                    var stepOutThread = O2MDbgUtils.stepOut();
                    stepOutThread.Join();
                    //execSync(O2MDbgCommands.stepBack());

                    instructions.Add(lastCommandExecutionMessage);
                    if (sleepInterval > 0)
                        Processes.Sleep(sleepInterval,false);
                }
            }
            else
            {
                DI.log.e("in animateBack either the process is not active or is running");
            }
            DI.log.info("Stop: Animate Over");
            return instructions;        
        }

        private void setOnBreakpointEvent()
        {
            if (DI.o2MDbg.ActiveProcess == null)
            {
                DI.log.error("ActiveProcess == null, cannot set onBreakpoint event");
            }
            else
            {
                DI.log.info("adding OnBreakpoint callback for active process");
                DI.o2MDbg.ActiveProcess.CorProcess.OnBreakpoint += CorProcess_OnBreakpoint;
            }
        }

        private void CorProcess_OnBreakpoint(object sender, Debugging.CorDebug.CorBreakpointEventArgs e)
        {
            //executeMDbgCommand("w");
           // var sync = new AutoResetEvent(false);
        /*    O2Thread.mtaThread(() =>
                                   {
                                       var activeThread = CommandBase.Debugger.Processes.Active.Threads.Active;
                                       var filename = activeThread.BottomFrame.SourcePosition.Path;
                                       var line = activeThread.BottomFrame.SourcePosition.Line;
                                       O2Messages.raiseO2MDbgBreakEvent(filename, line);
                                       //sync.Set();
                                   });*/
            // first find the current thread (we need to use this.ActiveProcess.Threads since e.Thread doesn't give us the info we need
            //MDbgThread e.Thread.id
        //    if (DateTime.Now.Millisecond < 100)
          /*  if (DI.o2MDbg.LogBreakpointEvent)
                DI.log.info("*** BREAKPOINT >>  " + O2MDbgBreakPoint.getActiveFrameFunctionName(e));

            CorFrame activeFrame = e.Thread.ActiveFrame;
            activeFrame.
            //var activeThread = this.ActiveProcess.Threads.Active;

          //  MDbgSourcePosition pos = CommandBase.Debugger.Processes.Active.Threads.Active.CurrentSourcePosition;
activeFrame.Code.*/

           // var filename = "filenameOfBreak";
           // int line = 11;
            //sync.WaitOne();
            e.Continue = DI.o2MDbg.AutoContinueOnBreakPointEvent; // can't do this here since CommandBase.Debugger.Processes.Active.Threads.Active is only set after this is executed
        }

        public void resetOnCommandExecuted()
        {            
            DI.log.info("Reseting OnCommand execution callbacks");
            foreach (Callbacks.dMethod_String invocationMethod in DI.o2MDbg.OnCommandExecuted.GetInvocationList())
                DI.o2MDbg.OnCommandExecuted -= invocationMethod;   
            registeredOnBreakEventCallbacks = new Dictionary<string, List<Func<O2MDbgCurrentLocation, Dictionary<string, O2MDbgVariable>, bool>>>();
        }

        internal void raiseOnBreakEvent(O2MDbgCurrentLocation o2MDbgCurrentLocation)
        {
            if (registeredOnBreakEventCallbacks == null)
                registeredOnBreakEventCallbacks = new Dictionary<string, List<Func<O2MDbgCurrentLocation, Dictionary<string, O2MDbgVariable>, bool>>>();
            Callbacks.raiseRegistedCallbacks(onBreakEvent);
            // make sure all breakpoints are registed
            var activeBreakPoints = BreakPoints.getBreakPointsAsStringList();
            foreach (var breakpointSignature in registeredOnBreakEventCallbacks.Keys)
                if (false == activeBreakPoints.Contains(breakpointSignature))
                    BreakPoints.addBreakPoint(breakpointSignature);
            // check if we need to invoke a callback
            //if (o2MDbgCurrentLocation.hasSourceCodeDetails)
            //{
            var currentLocationString = o2MDbgCurrentLocation.ToString();
            if (registeredOnBreakEventCallbacks.ContainsKey(currentLocationString))
            {
                Dictionary<string, O2MDbgVariable> variablesDictionary = DI.o2MDbg.sessionData.getVariablesDictionary();
                foreach (var callback in registeredOnBreakEventCallbacks[currentLocationString])
                    if (callback(o2MDbgCurrentLocation, variablesDictionary))
                    {
                        // means execution is supposed to continue
                        O2MDbgUtils.continueAttachedProjet();
                        return;
                    }
            }
            if (DI.o2MDbg.AutoContinueOnBreakPointEvent)
                O2MDbgUtils.continueAttachedProjet();
        }

        public void registerOnBreakEventCallback(string breakpointSignature, Func<O2MDbgCurrentLocation, Dictionary<string, O2MDbgVariable>, bool> callback)
        {
            if (registeredOnBreakEventCallbacks == null)
                registeredOnBreakEventCallbacks = new Dictionary<string, List<Func<O2MDbgCurrentLocation, Dictionary<string, O2MDbgVariable>, bool>>>();
            BreakPoints.addBreakPoint(breakpointSignature);
            if (false == registeredOnBreakEventCallbacks.ContainsKey(breakpointSignature))
                registeredOnBreakEventCallbacks.Add(breakpointSignature, new List<Func<O2MDbgCurrentLocation, Dictionary<string, O2MDbgVariable>, bool>>());
            registeredOnBreakEventCallbacks[breakpointSignature].Add(callback);            
        }
    }
}
