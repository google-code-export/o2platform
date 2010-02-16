// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Reflection;
using O2.Debugger.Mdbg.corapi;
using O2.Debugger.Mdbg.Debugging.CorDebug;
using O2.Debugger.Mdbg.Debugging.MdbgEngine;
using O2.Debugger.Mdbg.OriginalMdbgCode.mdbg;
using O2.DotNetWrappers.DotNet;
using O2.Interfaces.Messages;

//var breakpoint = o2MDbgOLD.O2BreakPoints.add("mscorlib.dll", "System.Console", "WriteLine", 0);  // is set but wasn't fireing (I could have placed the BP on the wrong method

namespace O2.Debugger.Mdbg.O2Debugger.Objects
{
    public class O2MDbgBreakPoint
    {
        
        public O2MDbg o2MDbg;
        public List<string> archivedBreakpoints_InSourceCode = new List<string>();

        /*     #region OnBreakPointAction enum

             public enum OnBreakPointAction
             {
                 Stop,
                 Continue,
                 StepOver,
                 StepInto,
                 StepOut
             }

             #endregion*/



        //public bool handleBreakpoints = true;


        //public OnBreakPointAction onBreakPointAction = OnBreakPointAction.Continue;
        //public bool logOnBreakpoint = true;
        //public MDbgEngine mdbgEngine;
        //public MDbgProcess mdbgProcess;
        //private int iStepCount;



        public O2MDbgBreakPoint(O2MDbg _o2MDbg)
        {
            o2MDbg = _o2MDbg;

            //mdbgProcess = o2MDbgOLD.mdbgProcess;
            //  mdbgEngine = o2MDbgOLD.mDbgEngine;

            //o2MDbgOLD.mdbgProcess.CorProcess.OnBreakpoint += CorProcess_OnBreakpoint;
            //o2MDbgOLD.mdbgProcess.CorProcess.OnBreakpointSetError += CorProcess_OnBreakpointSetError;
            //    o2MDbgOLD.mdbgProcess.CorProcess.OnStepComplete += (CorProcess_OnStepComplete);
        }

        /*private void CorProcess_OnStepComplete(object sender, CorStepCompleteEventArgs e)
        {
            // if (iCount++ % 100 == 0 && logOnBreakpoint)
            DI.log.info("[{0}] CorProcess_OnStepComplete {1}", iStepCount++, getActiveFrameFunctionName(e));
            e.Continue = handleDebugFlowAction();
        }

        private void CorProcess_OnBreakpointSetError(object sender, CorBreakpointEventArgs e)
        {
            if (logOnBreakpoint)
                DI.log.info("CorProcess_OnBreakpointSetError {0}", getActiveFrameFunctionName(e));
            e.Continue = true;
        }

        private void CorProcess_OnBreakpoint(object sender, CorBreakpointEventArgs e)
        {
            DI.log.info("in CorProcess_OnBreakpoint");
            /*(  if (handleBreakpoints)
            {
                if (logOnBreakpoint)
                    log.info("Breakpoint on {0}", getActiveFrameFunctionName(e));
                e.Continue = handleDebugFlowAction();
            }* /
            e.Continue = true;
        }

        private bool handleDebugFlowAction()
        {
            switch (onBreakPointAction)
            {
                case OnBreakPointAction.StepOut:
                    o2MDbgOLD.mdbgProcess.StepOut();
                    break;
                case OnBreakPointAction.StepInto:
                    o2MDbgOLD.mdbgProcess.StepInto(false);
                    break;
                case OnBreakPointAction.StepOver:
                    o2MDbgOLD.mdbgProcess.StepOver(false);
                    break;

                case OnBreakPointAction.Stop:
                    return false;
                case OnBreakPointAction.Continue:
                    return true;
            }
            return true;
        }
        */

        public static string getActiveFrameFunctionName(CorEventArgs e)
        {
            try
            {
                if (e.Thread.ActiveFrame == null)
                    return "e.Thread.ActiveFrame == null";
                //var corFunctionBreakpoint = (CorFunctionBreakpoint)e.Breakpoint;
                var corMetadataImport = new CorMetadataImport(e.Thread.ActiveFrame.Function.Class.Module);
                MethodInfo methodInfo = corMetadataImport.GetMethodInfo(e.Thread.ActiveFrame.Function.Token);

                return (methodInfo.DeclaringType.FullName ?? "(null)") + " :: " + (methodInfo.Name ?? "(null)");
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "getActiveFrameFunctionName");
                return "";
            }
        }

        /*   private static MethodInfo getBreakpointMethodInfo(CorBreakpointEventArgs e)
        {
            var corMetadataImport = new CorMetadataImport(e.Thread.ActiveFrame.Function.Class.Module);
            var function = corMetadataImport.GetMethodInfo(e.Thread.ActiveFrame.Function.Token);
            return function;

            /*var corFunctionBreakpoint = (CorFunctionBreakpoint)e.Breakpoint;
            var corMetadataImport = new CorMetadataImport(corFunctionBreakpoint.Function.Class.Module);
            return corMetadataImport.GetMethodInfo(corFunctionBreakpoint.Function.Token);                * /
            //var corFunction = thread.mdbgThread.CurrentFrame.Function;
            //log.d(" - setBreakPointOnMethodToPatch :   " + corFunction.FullName);
        }*/

        /*
        internal void setBreakPointOnAssemblyEntryPoint()
        {
            if (o2MDbgOLD.pathToMainAssembly == null)
                DI.log.error(
                    "pathToMainAssembly is not available, most likely because this is O2MDbg_OLD object was created via an attach into a live process");

            /*MethodDefinition entryPoint = DI.o2MonoCecil.getAssemblyEntryPoint(o2MDbgOLD.pathToMainAssembly);
            string module = entryPoint.DeclaringType.Module.Name;
            string type = entryPoint.DeclaringType.Name;
            string method = entryPoint.Name;* /
            object entryPointMethodDefinition = DI.o2MonoCecil.getAssemblyEntryPoint(o2MDbgOLD.pathToMainAssembly);
            string module = DI.o2MonoCecil.getMethodDefinitionDeclaringTypeModuleName(entryPointMethodDefinition);
            string type = DI.o2MonoCecil.getMethodDefinitionDeclaringTypeName(entryPointMethodDefinition);
            string method = DI.o2MonoCecil.getMethodDefinitionName(entryPointMethodDefinition);

            int offset = 0;
            add(module, type, method, offset);
        }*/


        public void addArchivedBreakpoints()
        {
            if (o2MDbg != null && o2MDbg.ActiveProcess != null)
                foreach (var breakpointSignature in archivedBreakpoints_InSourceCode)                
                    addBreakPoint(breakpointSignature);
                    //o2MDbg.ActiveProcess.Breakpoints.CreateBreakpoint(archivedBreakpoint);                                   
        }

        public MDbgBreakpoint addBreakPoint(string filepath, string lineNumber)
        {
            string breakpointSignature = string.Format("{0}:{1}", filepath, lineNumber);
            archivedBreakpoints_InSourceCode.Add(breakpointSignature);
            if (o2MDbg.ActiveProcess != null)
                addBreakPoint(breakpointSignature);
                //return o2MDbg.ActiveProcess.Breakpoints.CreateBreakpoint(location);                   
            return null;
        }

        public MDbgBreakpoint addBreakPoint(string module, string type, string method, int offset)
        {
            return add(module, type, method, offset);
        }

        public MDbgBreakpoint add(string module, string type, string method, int offset)
        {
            return add(module, type, method, offset, true /*verbose*/);
        }

        public MDbgBreakpoint add(string module, string type, string method, int offset, bool verbose)
        {
            if (verbose)
                DI.log.info("adding BP: {0} {1} {2} {3}", offset, method, type, module);
            return o2MDbg.ActiveProcess.Breakpoints.CreateBreakpoint(module, type, method, offset);            
        }

        public List<MDbgBreakpoint> add(List<MethodInfo> methodsToAddAsBreakPoints)
        {
            var breakPointsAdded = new List<MDbgBreakpoint>();
            foreach (MethodInfo method in methodsToAddAsBreakPoints)
                breakPointsAdded.Add(add(method));
            return breakPointsAdded;
        }

        public MDbgBreakpoint add(MethodInfo methodToAddAsBreakPoint)
        {
            return add(methodToAddAsBreakPoint.Module.FullyQualifiedName,
                       methodToAddAsBreakPoint.ReflectedType.FullName,
                       methodToAddAsBreakPoint.Name,
                       0);
        }


        //public void addBreakPointsOnAllMethods()
        // {
        //     add(DI.reflection.getMethods(o2MDbgOLD.pathToMainAssembly));
        // }

        // the format is {Dll}!{Type}.{method}  for example App_Web_vx-ae8ro.dll!HacmeBank_v2_Website.Login.btnSubmit_Click
        public string getBreakpointSignatureFromO2SelectedTypeOrMethodMessage(IO2Message o2Message)
        {

            string breakPointSignature = "";
            if (o2Message is IM_SelectedTypeOrMethod)
            {
                var selectedTypeOrMethod = (IM_SelectedTypeOrMethod)o2Message;
                if (selectedTypeOrMethod.methodInfo != null)
                {
                    var moduleName = selectedTypeOrMethod.methodInfo.Module.FullyQualifiedName;
                    var typeName = selectedTypeOrMethod.methodInfo.ReflectedType.FullName;
                    var methodName = selectedTypeOrMethod.methodInfo.Name;

                    //    o2MDbg.shell.Debugger.Processes.Active.Breakpoints.CreateBreakpoint(moduleName, typeName, methodName, 0);

                    breakPointSignature = string.Format("{0}!{1}.{2}", moduleName, typeName, methodName);
                }
            }
            return breakPointSignature;
        }

        public List<String> getBreakPointsAsStringList()
        {
            var currentBreakPoints = getBreakPoints();
            var breakpointsAsList = new List<String>();
            foreach(var breakpoint in currentBreakPoints)
            {
                if (breakpoint.Location!=null)
                {
                    if (breakpoint.Location is BreakpointLineNumberLocation)
                    {
                        var breakpointLineNumberLocation = (BreakpointLineNumberLocation) breakpoint.Location;

                        breakpointsAsList.Add(string.Format("{0}:{1}", System.IO.Path.GetFileName(breakpointLineNumberLocation.FileName),
                                                            breakpointLineNumberLocation.LineNumber));
                    }
                    else if (breakpoint.Location is BreakpointFunctionLocation)
                    {
                        var breakpointFunctionLocation = (BreakpointFunctionLocation)breakpoint.Location;

                        breakpointsAsList.Add(string.Format("{0}!{1}::{2}:{3}", breakpointFunctionLocation.ModuleName,
                                                            breakpointFunctionLocation.ClassName, breakpointFunctionLocation.MethodName, breakpointFunctionLocation.ILOffset));
                    }
                }
            }
            return breakpointsAsList;
        }

        public List<MDbgBreakpoint> getBreakPoints()
        {
            var currentBreakPoints = new List<MDbgBreakpoint>();
            if (o2MDbg.IsActive)
            {
                MDbgBreakpointCollection breakpoints = o2MDbg.ActiveProcess.Breakpoints;
                foreach (MDbgBreakpoint breakpoint in breakpoints)
                    currentBreakPoints.Add(breakpoint);                
            }
            return currentBreakPoints;
        }

        public List<string> getActiveBreakpoints()
        {
            var breakPoints = new List<string>();
            try
            {
                mdbgCommandsCustomizedForO2.BreakCmd("", breakPoints.Add);

                /*
                if (o2MDbg.ActiveProcess != null)
                {
                    foreach (MDbgFunctionBreakpoint breakPoint in o2MDbg.ActiveProcess.Breakpoints)
                        if (breakPoint.CorBreakpoint.IsActive)
                            breakPoints.Add(breakPoint.Location.ToString());
                }*/
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in getActiveBreakpoints");
            }
            return breakPoints;
        }

        public void deleteAddBreakpoints()
        {
            //CommandBase.Debugger.Processes.Active.Breakpoints
            try
            {
                // delete active breakpoints
                var timer = new O2Timer("Deleted active breakpoints in ").start();
                var breakpointsToDelete = new List<MDbgBreakpoint>();
                foreach(MDbgBreakpoint mdbgBreakpoint in o2MDbg.ActiveProcess.Breakpoints)
                    breakpointsToDelete.Add(mdbgBreakpoint);
                DI.log.info("There are {0} breakpoints to delete");
                foreach(MDbgBreakpoint mdbgBreakpoint in breakpointsToDelete)
                    mdbgBreakpoint.Delete();

                // delete archived breakpoints
                archivedBreakpoints_InSourceCode = new List<string>();
                timer.stop();
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in deleteAddBreakpoints");
            }

        }

        // if we only pass a signatures then use the internal MDbg addbreakpoint feature
        internal void addBreakPoint(string breakpointSignature)
        {
            o2MDbg.executeMDbgCommand(O2MDbgCommands.addBreakPoint(breakpointSignature));
        }        
    }
}
