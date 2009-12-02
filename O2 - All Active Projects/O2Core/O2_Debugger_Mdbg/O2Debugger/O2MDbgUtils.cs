// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using O2.Debugger.Mdbg.Debugging.CorDebug;
using O2.Debugger.Mdbg.Debugging.CorPublish;
using O2.Debugger.Mdbg.O2Debugger.Objects;
using O2.Debugger.Mdbg.OriginalMdbgCode.mdbg;
using O2.DotNetWrappers.DotNet;
using O2.External.O2Mono.MonoCecil;

namespace O2.Debugger.Mdbg.O2Debugger
{
    public class O2MDbgUtils
    {
        public static List<String> getListOfManagedProcess()
        {
            return getListOfManagedProcess(false);
        }

        public static List<CorPublishProcess> getManagedProcessButSelf()
        {
            var managedProcessesButSelf = new List<CorPublishProcess>();
            int currentProcessId = Process.GetCurrentProcess().Id;
            foreach (CorPublishProcess corPublishProcess in new CorPublish().EnumProcesses())
                if (currentProcessId != corPublishProcess.ProcessId)
                    managedProcessesButSelf.Add(corPublishProcess);
            return managedProcessesButSelf;
        }

        public static List<String> getListOfManagedProcess(bool showDetails)
        {
            var managedProcesses = new List<String>();
            int currentProcessId = Process.GetCurrentProcess().Id;
            var corPublish = new CorPublish();
            foreach (CorPublishProcess corPublishProcess in corPublish.EnumProcesses())
                if (currentProcessId != corPublishProcess.ProcessId)
                    // don't include the current process                                                
                    if (showDetails)
                        managedProcesses.Add("[" + corPublishProcess.ProcessId + "] [ver=" +
                                             CorDebugger.GetDebuggerVersionFromPid(corPublishProcess.ProcessId) + "] " +
                                             corPublishProcess.DisplayName);
                    else
                        managedProcesses.Add(corPublishProcess.DisplayName);
            return managedProcesses;
        }

        public static void debugMethod(MethodInfo targetMethod, string loadDllsFrom)
        {
            DI.log.debug("Starting method under debugged: {0}", targetMethod.Name);
            var exeInPackagedDirectory = StandAloneExe.createPackagedDirectoryForMethod(targetMethod, loadDllsFrom);
            startProcessUnderDebugger(exeInPackagedDirectory);
        }

        public static Thread startProcessUnderDebugger(string executableToStart)
        {
            if (executableToStart == null)
                return null;
            DI.log.info("Starting process under debugger: {0}", executableToStart);
            return O2Thread.mtaThread(
                () =>
                    {
                        DI.o2MDbg.execSync(O2MDbgCommands.run(executableToStart));
                        DI.o2MDbg.execSync(O2MDbgCommands.stepInto());

                    });
        }

        public static Thread detachFromDebuggedProcess()
        {
            if (DI.o2MDbg.IsActive)
            {
                DI.log.info("Detaching from process: {0}", DI.o2MDbg.ActiveProcess.Name);
                return O2Thread.mtaThread(() => DI.o2MDbg.execSync(O2MDbgCommands.detach()));
            }
            return null;
        }

        public static Thread breakIntoAttachedProjet()
        {
            if (DI.o2MDbg.IsActive)
            {
                DI.log.info("Breaking into attacked process: {0}", DI.o2MDbg.ActiveProcess.Name);
                return O2Thread.mtaThread(() => DI.o2MDbg.breakIntoDebuggedProcess());
            }
            return null;
        }

        public static Thread continueAttachedProjet()
        {
            if (DI.o2MDbg.IsActive)
            {
                DI.log.info("Continuing (breaked into) attached process: {0}", DI.o2MDbg.ActiveProcess.Name);
                return O2Thread.mtaThread(() => DI.o2MDbg.execSync(O2MDbgCommands.go()));
            }
            return null;
        }

        public static Thread stopDebuggedProcess()
        {
            if (DI.o2MDbg.IsActive)
            {
                DI.log.info("Stopping attached process: {0}", DI.o2MDbg.ActiveProcess.Name);
                return O2Thread.mtaThread(() => DI.o2MDbg.execSync(O2MDbgCommands.quit()));
            }
            return null;
        }

        public static Thread showCurrentLocation()
        {
            return O2Thread.mtaThread(() =>
                                          {
                                              try
                                              {
                                                  setCurrentLocationFromActiveThread();
                                              }
                                              catch (Exception ex)
                                              {
                                                  DI.log.ex(ex, "in showCurrentLocation");
                                              }

                                          });
        }

        public static void setCurrentLocationFromActiveThread()
        {
            try
            {
                DI.o2MDbg.currentLocation =
                    new O2MDbgCurrentLocation(DI.o2MDbg.ActiveProcess.Threads.Active);
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in setCurrentLocationFromActiveThread");
            }
        }

        public static Thread stepInto()
        {
            if (DI.o2MDbg.IsActive)
            {
                DI.log.info("Steping Into attached process: {0}", DI.o2MDbg.ActiveProcess.Name);
                return O2Thread.mtaThread(() =>
                    {
                        DI.o2MDbg.execSync(O2MDbgCommands.stepInto());
                        if (DI.o2MDbg.currentLocation != null && false == DI.o2MDbg.currentLocation.hasSourceCodeDetails) // automatically stepout and over if we don't have the source code of the current location
                        {
                            var threadForStepOut = stepOut();
                            if (threadForStepOut != null)
                            {
                                threadForStepOut.Join();   // wait for its execution before calling the StepOver
                                var threadForStepOver = stepOver();
                                if (threadForStepOver != null)
                                    threadForStepOver.Join();
                            }
                        }
                    });
            }
            return null;
        }

        public static Thread stepOver()
        {
            if (DI.o2MDbg.IsActive)
            {
                DI.log.info("Steping Over attached process: {0}", DI.o2MDbg.ActiveProcess.Name);
                return O2Thread.mtaThread(() => DI.o2MDbg.execSync(O2MDbgCommands.stepOver()));
            }
            return null;
        }

        public static Thread stepOut()
        {
            if (DI.o2MDbg.IsActive)
            {
                DI.log.info("Steping Out attached process: {0}", DI.o2MDbg.ActiveProcess.Name);
                return O2Thread.mtaThread(() => DI.o2MDbg.execSync(O2MDbgCommands.stepBack()));
            }
            return null;
        }

        public static Thread stepOutAnimated()
        {
            //tbtStopAnimation.Enabled = true;
            return O2Thread.mtaThread(() => DI.o2MDbg.animateBack(0));
        }

        public static Thread stopAnimationAndContinue()
        {
            DI.o2MDbg.AnimateOnStepEvent = false;
            return Thread.CurrentThread;
            //btStopAnimation.Enabled = false;
        }

        public static Thread stepOverAnimated()
        {
            //btStopAnimation.Enabled = true;
            return O2Thread.mtaThread(() => DI.o2MDbg.animateOver(0));
        }

        public static Thread stepIntoAnimated()
        {
            //btStopAnimation.Enabled = true;
            return O2Thread.mtaThread(() => DI.o2MDbg.animateInto(0));
        }


        public static Thread attachToProcess(string processItToAttach)
        {
            DI.log.info("Going to try to attach to processID {0}", processItToAttach);
            return O2Thread.mtaThread(() => DI.o2MDbg.attachToProcess(processItToAttach));
        }

        public static bool IsRunning()
        {
            return DI.o2MDbg.IsRunning;
        }

        public static bool IsActive()
        {
            return DI.o2MDbg.IsActive;
        }

        public static string execute(string executeText)
        {
            var sync = new AutoResetEvent(false);
            var returnData = "";
            var execThread = O2Thread.mtaThread(() =>
                mdbgCommandsCustomizedForO2.FuncEvalCmd(executeText, DI.o2MDbg.shell, execResult =>
                    {
                        returnData = execResult;
                        sync.Set();
                    }));
            sync.WaitOne();
            return returnData;
        }

        public static string getPropertyValue(O2MDbgVariable o2MDbgVariable)
        {
            if (o2MDbgVariable.IsProperty)
            {
                if (o2MDbgVariable.parentVariable != null)
                {
                    var variableType = string.Format("{0}.get_{1}", o2MDbgVariable.parentVariable.type, o2MDbgVariable.name);
                    var variableObject = o2MDbgVariable.parentVariable.fullName;
                    var executeString = string.Format("{0} {1}", variableType, variableObject);
                    DI.log.info("executing: {0}", executeString);
                    var propertyValue = execute(executeString);
                    return propertyValue;
                }
            }
            else
            {
                /*  var variableFullType = o2MDbgVariable.parentType + "." + o2MDbgVariable.name;
                  var variableObject = o2MDbgVariable.fullName;
                  var executeString = string.Format("{0} {1}=\"{2}\"", variableFullType, variableObject, newValue);
                  execute(executeString);*/
            }
            return "";
        }

        public static void setVariableValue(O2MDbgVariable o2MDbgVariable, string newValue)
        {
            O2Thread.mtaThread( () => setVariableValue_Thread(o2MDbgVariable, newValue));
        }

        public static void setVariableValue_Thread(O2MDbgVariable o2MDbgVariable, string newValue)
        {
            if (false == string.IsNullOrEmpty(newValue))
                if (newValue[0] != '"' && newValue[newValue.Length - 1] != '"')
                    newValue = '"' + newValue + '"';
            if (o2MDbgVariable.IsProperty)
            {
                if (o2MDbgVariable.parentVariable != null)
                {
                    var variableType = string.Format("{0}.set_{1}", o2MDbgVariable.parentVariable.type,
                                                     o2MDbgVariable.name);
                    var variableObject = o2MDbgVariable.parentVariable.fullName;
                    var executeString = string.Format("{0} {1} {2}", variableType, variableObject,
                                                      newValue);
                    DI.log.info("executing: {0}", executeString);
                    execute(executeString);
                }
            }
            else
            {    
                
                //var variableFullType = o2MDbgVariable.parentType + "." + o2MDbgVariable.name;
                var variableObject = o2MDbgVariable.fullName;
                var cmdToExecute = string.Format("set {0}={1}" , variableObject,newValue);
                //execute(executeString);
                DI.o2MDbg.execSync(cmdToExecute);
            }

        }

        public static void showVariableValue(O2MDbgVariable o2MDbgVariable)
        {
            O2Thread.mtaThread(
                () =>
                    {
                        var variableValue = getVariableValue_Thread(o2MDbgVariable);
                        DI.log.info("{0} = {1}", o2MDbgVariable.name, variableValue);
                    });

        }

        public static string getVariableValue_Thread(O2MDbgVariable o2MDbgVariable)
        {
            String variableValue = "";
            if (o2MDbgVariable.IsProperty)            
                variableValue = getPropertyValue(o2MDbgVariable);
            else
                variableValue = o2MDbgVariable.value;

            if (variableValue != null && variableValue.Length > 0 && variableValue[0] == '"' && variableValue[variableValue.Length - 1] == '"')
            {
                return variableValue.Substring(1, variableValue.Length - 2);
            }
            return variableValue;
        }        
    }
}
