using System;
using System.Collections.Generic;
using O2.Kernel.Interfaces.Views;
using O2.Kernel.WCF.Interfaces;

namespace O2.Kernel.WCF.classes
{
    public class O2WcfClient
    {
        public string WcfHostName { get; set; }
        public IO2WcfKernelMessage ClientProxy { get; set; }
        public string RemoteAppDomainName { get; set; }
        public O2GuiAscxWrapper o2GuiAscx;

        public O2WcfClient(string wcfHostName)
        {
            WcfHostName = wcfHostName;
            RemoteAppDomainName = WcfHostName;  // use this value by default for the appDomain name
            setup();
        }

        private void setup()
        {
            ClientProxy = O2WcfUtils.createClientProxy(WcfHostName);
            o2GuiAscx = new O2GuiAscxWrapper(this);
        }

        public bool AllOK
        {
            get
            {
                return ClientProxy.allOK();
            }
        }

        public void createRemoteAppDomainWithAssemblies(string appDomainName, List<String> assembliesToLoadInAppDomain)
        {
            RemoteAppDomainName = appDomainName;
            ClientProxy.createAppDomainWithDlls(RemoteAppDomainName, assembliesToLoadInAppDomain);
        }

        public void unloadRemoteAppDomain()
        {
            if (RemoteAppDomainName != null)
                ClientProxy.unLoadAppDomainAndDeleteTempFolder(RemoteAppDomainName);
        }

        public void close()
        {
            unloadRemoteAppDomain();
        }

        /*public object invoke(Callbacks.dMethod method, object[] methodParameters)
        {
            invoke(method.Method.DeclaringType.FullName, method.Method.Name, methodParameters);
        }*/

        public object invoke(Type targetType, String methodToInvoke)
        {
            return invoke(targetType, methodToInvoke, new object[0]);
        }

        public object invoke(Type targetType, String method, object[] methodParameters)
        {
            return invoke(targetType.FullName, method, methodParameters);
        }

        public object invoke(string targetType, String method)
        {
            return invoke(targetType, method, new object[0]);
        }        

        public object invoke(string targetType, String methodToInvoke, object[] methodParameters)
        {
            return ClientProxy.invokeOnAppDomainObject(RemoteAppDomainName, targetType, methodToInvoke, methodParameters);
        }

        public List<String> getStringListFromAscxControl(string ascxControlName, string methodToInvoke, object[] methodParameters)
        {
            return ClientProxy.getStringListFromAscxControl(ascxControlName,methodToInvoke,methodParameters);
        }

        public object getProperty(Type typeWithProperty, string propertyName)
        {
            return getProperty(typeWithProperty.FullName, propertyName);
        }
        public object getProperty(string typeWithProperty, string propertyName)
        {
            return ClientProxy.getProperty(typeWithProperty, propertyName);
        }

        public class O2GuiAscxWrapper
        {
            private readonly O2WcfClient o2WcfClient;

            public O2GuiAscxWrapper(O2WcfClient _o2WcfClient)
            {
                o2WcfClient = _o2WcfClient;
            }

            public void launch()
            {
                o2WcfClient.invoke("O2AscxGUI", "launch");
            }

            public void close()
            {
                o2WcfClient.invoke("O2AscxGUI", "close");
            }

            //                clientProxy.
            //

            //                        o2WcfProxy.O2Messages("openAscxAsForm", new object[] { typeof(ascx_CirAnalysis).FullName, ascxControlName });

            //            o2WcfProxy.O2Messages("executeOnAscxSync", new object[] {ascxControlName, "loadO2CirDataFile" , new [] { DI.config.ExecutingAssembly }});

            //            }

            public void openAscx(Type controlType, O2DockState o2DockState, string ascxControlName)
            {
                o2WcfClient.invoke("O2AscxGUI", "openAscx",
                                   new object[] { controlType.FullName, o2DockState, ascxControlName });
            }

            public void waitForAscxGuiClose()
            {
                o2WcfClient.invoke("O2AscxGUI", "waitForAscxGuiClose");
            }

            public void openAscxAsForm(Type controlType, string ascxControlName)
            {
                openAscxAsForm(controlType.FullName, ascxControlName);
            }

            public void openAscxAsForm(string controlType, string ascxControlName)
            {
                o2WcfClient.invoke("O2AscxGUI", "openAscxAsForm", new object[] { controlType, ascxControlName });
            }

            public void closeAscxParent(string ascxControlName)
            {
                o2WcfClient.invoke("O2AscxGUI", "closeAscxParent", new object[] { ascxControlName });
            }

            public bool isAscxLoaded(string ascxControlName)
            {
                return (bool)o2WcfClient.invoke("O2AscxGUI", "isAscxLoaded", new object[] { ascxControlName });
            }

            public object invoke(string ascxControlName, string methodToInvoke)
            {
                return invoke(ascxControlName, methodToInvoke, new object[0]);
            }
            public object invoke(string ascxControlName, string methodToInvoke, object methodParameters)
            {
                return o2WcfClient.invoke("O2AscxGUI", "invokeOnAscxControl", new [] { ascxControlName, methodToInvoke ,methodParameters});                    
            }

            public List<string> invokeAndGetStringList(string ascxControlName, string methodToInvoke)
            {
                return invokeAndGetStringList(ascxControlName, methodToInvoke, new object[0]);
            }

            public List<string> invokeAndGetStringList(string ascxControlName, string methodToInvoke, object[] methodParameters)
            {
                //return o2WcfClient.getStringListFromAscxControl("O2AscxGUI", "invokeOnAscxControl", new[] { ascxControlName, methodToInvoke, methodParameters });                    
                return o2WcfClient.getStringListFromAscxControl(ascxControlName, methodToInvoke, methodParameters);                    
            }

            public void clickButton(string ascxControlName, string buttonToClick)
            {
                invoke(ascxControlName, buttonToClick + "_Click", new object[] {null, null});                
            }
        }
        
    }
}
