// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Reflection;
using O2.Debugger.Mdbg.Debugging.MdbgEngine;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Kernel.Interfaces.Messages;

namespace O2.Debugger.Mdbg.O2Debugger.Ascx
{
    partial class ascx_BreakpointCreator
    {
        List<Type> typesInModule = new List<Type>();
        List<MethodInfo> methodsInModule = new List<MethodInfo>();

        private void addBreakpointsToAllMethodsInModule(string moduleToProcess, bool verbose)
        {            
            DI.log.debug("Adding {0} breakpoints", methodsInModule.Count);
            O2Thread.mtaThread(() =>
                                   {
                                       var numberOfBreakpointsAdded = 0;
                                       var breakpointOffset = 0;
                                       var timer = new O2Timer("Breakpoint added in").start();
                                       MDbgModule module = DI.o2MDbg.sessionData.getModule(moduleToProcess);
                                       if (module != null)
                                           foreach (Type typeInModule in module.Importer.DefinedTypes)
                                               // no point in adding interfaces or abstract types since they will not be bound (double check on abstract!!)
                                               //if (false == typeInModule.IsInterface && false == typeInModule.IsAbstract)   // these are not implemented in the original MDbg :(
                                                   foreach (MethodInfo methodInType in typeInModule.GetMethods())
                                                   {
                                                       DI.o2MDbg.BreakPoints.add(moduleToProcess, typeInModule.FullName,
                                                                                 methodInType.Name, breakpointOffset,
                                                                                 verbose);
                                                       if (numberOfBreakpointsAdded++%500 == 0)
                                                           DI.log.info(
                                                               "  update: {0} breakpoints added so far of a max of {1}",
                                                               numberOfBreakpointsAdded, methodsInModule.Count);
                                                   }        
                                       timer.stop();
                                       DI.log.info("There where {0} breakpoints added of a max of {1}", numberOfBreakpointsAdded, methodsInModule.Count);
                                   });
        }
        
        private void calculateStatsForModule(string moduleToProcess)
        {
            typesInModule = new List<Type>();
            methodsInModule = new List<MethodInfo>();
            MDbgModule module;
            O2Thread.mtaThread(() =>
                                       {
                                           module = DI.o2MDbg.sessionData.getModule(moduleToProcess);
                                           if (module != null)                                           
                                               foreach (Type typeInModule in module.Importer.DefinedTypes)                                               
                                               //    if (false == typeInModule.IsInterface && false == typeInModule.IsAbstract)  // these are not implemented in the original MDbg :(
                                                   {
                                                       typesInModule.Add(typeInModule);
                                                       foreach (MethodInfo methodInType in typeInModule.GetMethods())                                                             
                                                           methodsInModule.Add(methodInType);                                                       
                                                   }
                                           O2Forms.setObjectTextValueThreadSafe(string.Format("{0} types in module {1}", typesInModule.Count, moduleToProcess),laNumberOfTypes);                                           
                                           O2Forms.setObjectTextValueThreadSafe(string.Format("{0} types in module {1}", methodsInModule.Count, moduleToProcess),laNumberOfMethods);
                                       });                       
        }


        public void calculateBreakpoints()
        {
            DI.o2MDbg.updateDebuggerRunningAndActiveStatus(() =>
                                                               {
                                                                   if (false == DI.o2MDbg.debugggerActive &&
                                                                       DI.o2MDbg.debugggerRunning)
                                                                       DI.log.error(
                                                                           "There is no active process or it is not stopped");
                                                                   else
                                                                   {
                                                                       //    O2MDbgUtils.breakIntoAttachedProjet();
                                                                       O2Thread.mtaThread(
                                                                           () =>
                                                                           O2Forms.populateWindowsControlWithList(
                                                                               lbModulesInDebuggeeProcess,
                                                                               DI.o2MDbg.sessionData.getModules()));
                                                                       DI.log.info("done");
                                                                   }
                                                               });
        }


        void ascx_BreakpointCreator_onMessages(IO2Message o2Message)
        {
            if (o2Message is IM_O2MdbgAction)
            {
                var o2MdbgAction = (IM_O2MdbgAction) o2Message;
                if (o2MdbgAction.o2MdbgAction == IM_O2MdbgActions.startDebugSession)
                    calculateBreakpoints();
            }
        }
    }
}
