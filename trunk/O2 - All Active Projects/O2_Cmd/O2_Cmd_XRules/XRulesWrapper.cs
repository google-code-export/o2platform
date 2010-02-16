// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using O2.Core.XRules.XRulesEngine;
using O2.DotNetWrappers.O2CmdShell;
using O2.DotNetWrappers.Windows;
using O2.Interfaces.XRules;
using O2.Kernel.Objects;
using O2_Cmd_XRules;

namespace O2.Cmd.XRules
{
    public class XRulesWrapper
    {
        //public static string currentTask = "";
        public static bool compilationComplete = false;
        public static bool executionComplete = false;

        public static void help()
        {
            O2Cmd.log.write("Help information: You need to enter the rule you want to execute");            ;
        }

        public static void compileXRules()
        {
            XRules_Compilation.compileXRules(addXRulesToView, setCurrentTask, setRulesCompilationProgressBarMaxValue, incrementRulesCompilationProgressbar);   
            while(compilationComplete == false)
            {
                Processes.Sleep(100,false);
            }
        }
        public static void executeXRule(string ruleToExecute, string methodToExecute)
        {
            executeXRule(ruleToExecute, methodToExecute, new string[] { });
        }

        public static void executeXRule(string ruleToExecute, string methodToExecute, string param1)
        {
            executeXRule(ruleToExecute, methodToExecute, new string[] { param1 });
        }
        public static void executeXRule(string ruleToExecute, string methodToExecute, string param1, string param2)
        {
            executeXRule(ruleToExecute, methodToExecute, new string[] { param1, param2 });
        }
        public static void executeXRule(string ruleToExecute, string methodToExecute, string param1, string param2, string param3)
        {
            executeXRule(ruleToExecute, methodToExecute, new string[] { param1, param2, param3 });
        }

        public static void executeXRule(string ruleToExecute, string methodToExecute, string param1, string param2, string param3, string param4)
        {
            executeXRule(ruleToExecute, methodToExecute, new string[] { param1, param2, param3, param4 });
        }

        public static void listXRules()
        {
            listCompiledXRules();
        }

        public static void listCompiledXRules()
        {
            O2Cmd.log.write("Loading compiled XRules");
            var xRules = XRules_Compilation.getCompiledXRules();
            O2Cmd.log.write("{0} XRules loaded: {0}", xRules.Count);
            addXRulesToView(xRules);
        }

        public static void executeXRule(string ruleToExecute, string methodToExecute, string[] methodParameters)
        {
            compilationComplete = false;
            executionComplete = false;

            /*XRules_Compilation.compileXRules(
                xRules => onCompilationComplete(xRules, ruleToExecute, methodToExecute, methodParameters),
                setCurrentTask, setRulesCompilationProgressBarMaxValue, incrementRulesCompilationProgressbar);*/
            XRules_Compilation.loadXRules(
                            xRules => onCompilationComplete(xRules, ruleToExecute, methodToExecute, methodParameters),
                            setCurrentTask, setRulesCompilationProgressBarMaxValue, incrementRulesCompilationProgressbar);
            while (compilationComplete == false)
            {
                Processes.Sleep(100,false);
            }
        }

        private static void onCompilationComplete(List<IXRule> xRules, string ruleToExecute, string methodToExecute, string[] methodParameters)
        {            
            var loadedXRules = XRules_Execution.getLoadedXRules(xRules);
            Console.WriteLine("\nCompilation Done, now finding rule '{0}' and method '{1}'", ruleToExecute,
                              methodToExecute);
            MethodInfo resolvedMethod = null;
            IXRule resolvedXRule = null;
            foreach (var loadedXRule in loadedXRules)
            {
                if (loadedXRule.XRule.Name == ruleToExecute)
                    foreach (var method in loadedXRule.methods)
                        if (method.Key.Name == methodToExecute)
                        {
                            resolvedXRule = loadedXRule.XRule;
                            resolvedMethod = method.Value;
                            break;
                        }
                if (resolvedMethod != null)
                    break;
            }
            if (resolvedMethod != null)
            {
                executeMethod(resolvedXRule, resolvedMethod, methodParameters);
            }                
            else
                Console.WriteLine("\nERROR: Could NOT findmethod to execute");
            compilationComplete = true;
        }

        private static void executeMethod(IXRule resolvedXRule, MethodInfo resolvedMethod, object[] methodParameters)
        {
           /* var appDomainFactory = new O2AppDomainFactory("test", XRules_Config.PathTo_XRulesCompiledDlls);
            
            O2Cmd.log.info("executing via AppDomain");            
            */
            Console.WriteLine("\nExecuting XRule's method: {0}  [{1}]\n", resolvedMethod.Name, resolvedMethod);
            DI.reflection.invokeASync(resolvedXRule, resolvedMethod, methodParameters,
                                   results =>
                                       {
                                           O2Cmd.log.write("\nExecution Results: {0}\n" , results ?? "[null]" );
                                           executionComplete = true;
                                       });            
            while (executionComplete == false)
            {
                Processes.Sleep(100,false);
            }
            Console.WriteLine("\nXRule's method execution complete");
        }

        /*private static void onXRuleExecutionCompletion(object obj)
        {
            throw new NotImplementedException();
        }*/

        private static void incrementRulesCompilationProgressbar()
        {
            Console.Write(".");
        }

        private static void setRulesCompilationProgressBarMaxValue(int arg1)
        {
            Console.Write("");
        }

        private static void setCurrentTask(string currentTask)
        {
            Console.Write("\nTask: " + currentTask);
        }

        private static void addXRulesToView(List<IXRule> xRules)
        {
            Console.WriteLine("\nMapping Rules:");
            var loadedXRules = XRules_Execution.getLoadedXRules(xRules);

            Console.WriteLine("\nCurrent loaded Rules:");
            foreach (var xRule in loadedXRules)
            {                
                O2Cmd.log.write("\n     rule: {0} ", xRule.XRule.Name);
                foreach(var method in xRule.methods)
                    Console.WriteLine("                   method: {0} ", method.Key.Name);
            }            

            compilationComplete = true;
        }
    }
}
