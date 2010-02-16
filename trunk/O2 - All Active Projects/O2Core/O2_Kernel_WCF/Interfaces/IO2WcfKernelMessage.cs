// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.ServiceModel;
using O2.Interfaces.Messages;
using O2.Interfaces.Views;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.Kernel.WCF.Interfaces
{
    [ServiceContract(Namespace = "http://www.o2-ounceopen.com/IWcfO2Messages")]
    [ServiceKnownType(typeof(KO2Message))]
    [ServiceKnownType(typeof(IM_GUIAction))]    
    [ServiceKnownType(typeof(String[]))]
    [ServiceKnownType(typeof(O2DockState))]    
    public interface IO2WcfKernelMessage
    {
        
        [OperationContract]
        bool sendMessage(IO2Message o2Message);

        [OperationContract]
        bool allOK();

        [OperationContract]
        string getName();

        [OperationContract]
        void closeO2KernelProcess();

        [OperationContract]
        int getO2KernelProcessId();

        [OperationContract]
        void o2ShellCommand(string shellCommandToExecute);

        [OperationContract]
        bool createAppDomainWithDlls(string appDomainName, List<string> dllsOfDllsToLoadInNewAppDomain);

        [OperationContract]
        bool unLoadAppDomainAndDeleteTempFolder(string appDomainName);

        [OperationContract]
        object invokeOnAppDomainObject(string appDomainName, string typeToUse, string methodToInvoke, object[] methodParameters);        

        [OperationContract]
        object getProperty(string typeWithProperty, string propertyName);

        [OperationContract]
        object O2MessagesOnAppDomain(string appDomainName, string o2MessagesWrapperToCall, object[] messageParameters);

        [OperationContract]              
        object O2Messages(string o2MessagesWrapperToCall, object[] messageParameters);
        
        [OperationContract]
        List<String> getStringListFromAscxControl(string ascxControlName, string methodToInvoke, object[] methodParameters);
    }
}
