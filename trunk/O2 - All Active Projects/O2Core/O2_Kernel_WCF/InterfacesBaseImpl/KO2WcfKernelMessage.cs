// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using O2.Interfaces.Messages;
using O2.Kernel.CodeUtils;
using O2.Kernel.Objects;
using O2.Kernel.WCF;
using O2.Kernel.WCF.classes;
using O2.Kernel.WCF.Interfaces;

namespace O2.Kernel.WCF.InterfacesBaseImpl
{
    /// <summary>
    /// Class that is executed by the remote WCF client
    /// </summary>
    public class KO2WcfKernelMessage : IO2WcfKernelMessage
    {
        public bool allOK()
        {
            return true;
        }

        public bool sendMessage(IO2Message o2Message)
        {
            throw new System.NotImplementedException();
        }

        public string getName()
        {
            O2WcfUtils.wcfMessageReceived("getName()");
            return PublicDI.O2KernelProcessName; 
            //set { DI.O2KernelProcessName = value; }
        }

        public void closeO2KernelProcess()
        {
            O2WcfUtils.wcfMessageReceived("closeO2KernelProcess()");
            O2Kernel_Processes.KillCurrentO2Process(1000);
        }

        public int getO2KernelProcessId()
        {
            O2WcfUtils.wcfMessageReceived("WcfRequest: getCurrentProcessId()");
            return O2Kernel_Processes.getCurrentProcessId();
        }

        public void o2ShellCommand(string shellCommandToExecute)
        {
            O2WcfUtils.wcfMessageReceived("WcfRequest: shellCommandToExecute( "+ shellCommandToExecute +" )");
            if (WCF_DI.o2Shell != null)
                WCF_DI.o2Shell.shellExecution.execute(shellCommandToExecute);
            else
                WCF_DI.log.error("received sendO2ShellCommand command but no O2Shell is currently available");
        }


        public bool createAppDomainWithDlls(string appDomainName, List<string> dllsOfDllsToLoadInNewAppDomain)
        {
            try
            {
                O2WcfUtils.wcfMessageReceived("WcfRequest: createAppDomainWithDlls( " + appDomainName + " , with " + dllsOfDllsToLoadInNewAppDomain.Count + " dlls)");
                var appDomainTempDirectory = WCF_DI.config.TempFolderInTempDirectory;
                new O2AppDomainFactory(appDomainName, appDomainTempDirectory, dllsOfDllsToLoadInNewAppDomain);
                return true;                
            }
            catch (Exception)
            {
                return false;                    
            }
            
            
        }

        public bool unLoadAppDomainAndDeleteTempFolder(string appDomainName)
        {
            var o2AppDomainFactory = PublicDI.appDomainsControledByO2Kernel[appDomainName];
            if (o2AppDomainFactory != null)
                return o2AppDomainFactory.unLoadAppDomainAndDeleteTempFolder();
            return false;
        }

        /*public bool invokeOnAppDomainObject(string appDomainName, string typeToUse, string methodToInvoke)
        {
            return invokeOnAppDomainObject(appDomainName, typeToUse, methodToInvoke);
        }*/

        public object invokeOnAppDomainObject(string appDomainName, string typeToUse, string methodToInvoke, object[] methodParameters)
        {
            if (PublicDI.appDomainsControledByO2Kernel != null && PublicDI.appDomainsControledByO2Kernel.ContainsKey(appDomainName))
            {
               
                var o2AppDomainFactory = PublicDI.appDomainsControledByO2Kernel[appDomainName];
              //  DI.log.info("Inside DI with name: {0}", o2AppDomainFactory.Name);
                var proxytObject = o2AppDomainFactory.getProxyObject(typeToUse);
                if (proxytObject == null)
                    WCF_DI.log.error("in invokeOnAppDomainObject Could not create proxy for: {0}", typeToUse);
                else
                {                    
                 //   DI.log.info("proxytObject type: {0}", proxytObject.GetType().FullName);
                    return o2AppDomainFactory.invoke(proxytObject, methodToInvoke, (methodParameters ?? new object[0]));
                    //DI.log.info("invoked method: {0}", methodToInvoke);
                    //return true;
                }
                
               // return o2AppDomainFactory.getProxyObject(objectToGet);
            }
            WCF_DI.log.error("Could not Find o2AppDomainFactory (& appDomain): {0}", appDomainName);
            return null;
        }
        
        public object getProperty(string typeWithProperty, string propertyName)
        {
            var type = WCF_DI.reflection.getType(typeWithProperty);
            return WCF_DI.reflection.getProperty(propertyName, type);
        }
        public object O2MessagesOnAppDomain(string appDomainName, string o2MessagesWrapperToCall, object[] messageParameters)
        {
            try
            {
                WCF_DI.log.info("DI.O2KernelProcessName: {0}", appDomainName);
                var o2AppDomainFactory = PublicDI.appDomainsControledByO2Kernel[appDomainName];
                if (o2AppDomainFactory != null)
                {
                    var proxytObject = o2AppDomainFactory.getProxyObject("O2Messages");
                    if (proxytObject != null)
                        return o2AppDomainFactory.invoke(proxytObject, o2MessagesWrapperToCall,
                                                         (messageParameters ?? new object[0]));
                }
            }
            catch (Exception ex)
            {
                WCF_DI.log.ex(ex, "in KO2WcfKernelMessage.O2Messages");
            }
            return null;
        }

        //(string appDomainName, 
        public object O2Messages(string o2MessagesWrapperToCall, object[] messageParameters)
        {
            var appDomainName = PublicDI.O2KernelProcessName;
            return O2MessagesOnAppDomain(appDomainName, o2MessagesWrapperToCall, messageParameters);                                    
        }

        public List<String> getStringListFromAscxControl(string ascxControlName, string methodToInvoke, object[] methodParameters)
        {
            var ascxControl = (Control)O2Messages("getAscxSync", new object[] {ascxControlName});
            if (ascxControl ==  null)
                WCF_DI.log.error(" in getCirDataAnalysisFromAscxControl , could not get ascx called: {0}", ascxControlName);
            else
            {
                var returnData = WCF_DI.reflection.invoke(ascxControl, methodToInvoke, methodParameters);
                if (returnData != null && returnData is List<String>)
                    return (List<String>)returnData;
                    //return ((ICirDataAnalysis) returnData).CirClasses<string>();
            }            
            return null;
        }
    }
}
