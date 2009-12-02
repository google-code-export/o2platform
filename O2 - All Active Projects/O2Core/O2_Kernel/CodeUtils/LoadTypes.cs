using System;
using System.Reflection;
using O2.Kernel.Objects;

namespace O2.Kernel.CodeUtils
{
    public class LoadTypes
    {        
        //public static object createObject()

        public static object loadTypeAndExecuteMethodInAppDomain(AppDomain appDomain, string dllWithType,
                                                                 string typeToCreateAndUnwrap, string methodToExecute,
                                                                 object[] methodParameters)
        {
            var appDomainHelper = new O2AppDomainFactory(appDomain);
            object proxyObject = appDomainHelper.createAndUnWrap(dllWithType, typeToCreateAndUnwrap);
            if (proxyObject == null)
                DI.log.error("in loadTypeAndExecuteMethodInAppDomain, proxy == null");            
                ///    var proxy = appDomain.CreateInstanceAndUnwrap(dllToLoad, typeToCreateAndUnwrap);
                //if (proxy == null)
                //    log.error("in loadTypeAndExecuteMethodInAppDomain, proxy == null");
            else
            {
                MethodInfo methodInfo = DI.reflection.getMethod(proxyObject.GetType(), methodToExecute, methodParameters);
                if (methodInfo == null)
                    DI.log.error("in loadTypeAndExecuteMethodInAppDomain, methodInfo == null");
                else
                    return DI.reflection.invoke(proxyObject, methodInfo, methodParameters);
            }
            return null;
        }
    }
}