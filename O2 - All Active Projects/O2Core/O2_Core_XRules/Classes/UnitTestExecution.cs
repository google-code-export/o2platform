// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.Kernel.Interfaces.XRules;

namespace O2.Core.XRules.Classes
{
    public class UnitTestExecution
    {
        public static void executeXRuleInSelectedTreeViewNode(TreeView tvXRules, Action<bool, object> executionResult, Action onComplete)
        {
            var methodsToExecute = UnitTestSupport.getMethodsToExecuteFromSelectedTreeViewNode(tvXRules);
            executeXRuleMethods(methodsToExecute, executionResult, onComplete);

            /*if (tvXRules.SelectedNode != null && tvXRules.SelectedNode.Tag != null)
            {
                if (tvXRules.SelectedNode.Tag is ILoadedXRule)
                    executeXRuleMethods((ILoadedXRule)tvXRules.SelectedNode.Tag, executionResult, onComplete);
                else if (tvXRules.SelectedNode.Tag is MethodInfo)
                    executeXRuleMethod((MethodInfo)tvXRules.SelectedNode.Tag, executionResult);
            }*/
        }

        public static void executeXRuleMethods(ILoadedXRule iLoadedXRule, Action<bool, object> executionResult, Action onComplete)
        {
            O2Thread.mtaThread(
                () =>
                    {
                        DI.log.info("executing XRule: {0}", iLoadedXRule.XRule.Name);
                        foreach (var method in iLoadedXRule.methods)
                            executeXRuleMethod(method.Value, executionResult);
                        onComplete();
                    });
            //executionResult(false);
            
        }

        public static void executeXRuleMethods(IEnumerable<MethodInfo> methodsToExecute, Action<bool, object> executionResult, Action onComplete)
        {
            O2Thread.mtaThread(
                () =>
                    {
                        foreach (var methodToExecute in methodsToExecute)
                            if (DI.reflection.getParametersType(methodToExecute).Count == 0)
                                executeXRuleMethod(methodToExecute, executionResult);
                        onComplete();
                    });
        }

        public static void executeXRuleMethod(MethodInfo methodToExecute, Action<bool,object> executionResult)
        {
            if (methodToExecute == null)
            {                
                executionResult(false,null);
            }
            else
                try
                {
                    DI.log.info("executing method: {0}", methodToExecute.Name);
                    // create method's type using default constructor
                    var liveObject = DI.reflection.createObjectUsingDefaultConstructor(methodToExecute.DeclaringType);
                    // and execute the method
                    var returnData = methodToExecute.Invoke(liveObject, new object[] { });            // don't use the DI.reflection methods since we want the exceptions to be thrown
                    //DI.reflection.invoke(liveObject, methodInfo, new object[] {});
                    executionResult(true, returnData);
                }
                catch (Exception ex)
                {
                    DI.log.error("in UnitTestSupport.executeXRuleMethod: {0} threw error: {1} ", methodToExecute.ToString(),
                                 ex.Message);
                    if (ex.InnerException != null)
                    {
                        var innerExceptionMessage = ex.InnerException.Message;
                        DI.log.error("   InnerException value: {0}", innerExceptionMessage);
                        executionResult(false, innerExceptionMessage);
                    }
                    else
                        executionResult(false, null);
                }
        }
    }
}
