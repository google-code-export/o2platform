// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using O2.DotNetWrappers.Windows;
using O2.Kernel.CodeUtils;
using O2.Kernel.Objects;

namespace O2.UnitTests.Test_O2Kernel.Test_O2AppDomainFactory
{
    [TestFixture]
    public class Test_SimpleAppDomainProxy
    {
        private const string assemblyName = "O2_Kernel";
        private const string typeToCreate = "O2.Kernel.Objects.O2Proxy";
        private const string typeToCreateSimpleName = "O2Proxy";
        private const string methodToInvoke = "nameOfCurrentDomain";
        private readonly object[] methodParams = new object[0];

        public string copyO2ProxyToTempFolder(string pathToProxyDll, string subDirectoryToUse)
        {
            if (File.Exists(pathToProxyDll))
            {
                string tempProxyFolder = Path.Combine(DI.config.O2TempDir, subDirectoryToUse);
                Files.checkIfDirectoryExistsAndCreateIfNot(tempProxyFolder);
                Files.Copy(pathToProxyDll, tempProxyFolder);
                DI.log.i("copied proxy to : " + tempProxyFolder);
                return tempProxyFolder;
            }
            return "";
        }


        [Test]
        public void Test_AppdomainsWithUniqueBaseDirectories()
        {
            string pathToproxyDll = DI.config.ExecutingAssembly; // Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyName + ".exe");            
            string appDomain1TempDir = copyO2ProxyToTempFolder(pathToproxyDll, "appDomain1TempDir");
            string appDomain2TempDir = copyO2ProxyToTempFolder(pathToproxyDll, "appDomain2TempDir");
            Assert.That(
                Directory.Exists(appDomain1TempDir) && Directory.Exists(appDomain2TempDir) &&
                appDomain1TempDir != appDomain2TempDir, "Something is wrong with temp dirs");
            var appDomain1 = new O2AppDomainFactory("appDomain1", appDomain1TempDir);
            var appDomain2 = new O2AppDomainFactory("appDomain2", appDomain2TempDir);
            Assert.That(appDomain1.BaseDirectory != appDomain2.BaseDirectory,
                        "Something is wrong with AppDomain's temp dirs");
            object appDomain1invocationResult =
                appDomain1.invokeMethod(methodToInvoke + " " + typeToCreate + " " + assemblyName);
            object appDomain2invocationResult =
                appDomain2.invokeMethod(methodToInvoke + " " + typeToCreate + " " + assemblyName);
            Assert.That(appDomain1invocationResult != null && appDomain2invocationResult != null,
                        "appDomainXinvocationResult == null");
            DI.log.info("{0} != {1}", appDomain1invocationResult, appDomain2invocationResult);
            Assert.That(appDomain1invocationResult != appDomain2invocationResult,
                        "appDomainXinvocationResult were not different");
        }

        

        [Test]
        public void test_InstanceInvocation()
        {
            // test by invoking itseft
            object proxy = O2AppDomainFactory.getProxy(assemblyName, typeToCreateSimpleName + " " + assemblyName);
            Assert.That(proxy != null, "proxy was null");
            // test via direct cast
            var simpleAppDomainProxy = (O2Proxy) proxy;
            string resultUsingDirectCall = simpleAppDomainProxy.nameOfCurrentDomain();
            var resultUsingReflectionCall =
                (string) simpleAppDomainProxy.instanceInvocation("O2Proxy", "nameOfCurrentDomainStatic", new object[0]);
            Assert.That(resultUsingDirectCall != null && resultUsingReflectionCall != "",
                        "direct cast : result values were null ");
            DI.log.info("{0} == {1}", resultUsingDirectCall, resultUsingReflectionCall);
            Assert.That(resultUsingDirectCall == resultUsingReflectionCall, "direct cast : return values are not equal");
        }

        [Test]
        public void Test_LoadingCodeIntoMultipleAppDomains()
        {
            // test current AppDomain            
            string pathToproxyDll = DI.config.ExecutingAssembly; // Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyName + ".exe");
            string testAppDomainDirectory = copyO2ProxyToTempFolder(pathToproxyDll, assemblyName);

            object resultInCurrentAppDomain = LoadTypes.loadTypeAndExecuteMethodInAppDomain(AppDomain.CurrentDomain,
                                                                                            assemblyName, typeToCreate,
                                                                                            methodToInvoke, methodParams);
            Assert.IsNotNull(resultInCurrentAppDomain, "result");
            DI.log.debug("{0} = {1}", "resultIncurrentAppDomain", resultInCurrentAppDomain);

            // test creating new AppDomain
            var newAppDomain = new O2AppDomainFactory("newAppDomain", testAppDomainDirectory);
            Assert.IsNotNull(newAppDomain, "newAppDomain");
            newAppDomain.load(assemblyName);
            object resultInNewAppDomain = LoadTypes.loadTypeAndExecuteMethodInAppDomain(newAppDomain.appDomain,
                                                                                        assemblyName, typeToCreate,
                                                                                        methodToInvoke, methodParams);
            Assert.IsNotNull(resultInNewAppDomain, "result");
            DI.log.debug("{0} = {1}", "resultInNewAppDomain", resultInNewAppDomain);
            Assert.That(resultInCurrentAppDomain != resultInNewAppDomain,
                        "resultIncurrentAppDomain == resultInNewAppDomain");

            // test creating new AppDomain just using the O2AppDomainFactory
            var appDomainFactory = new O2AppDomainFactory("appDomainFactory", testAppDomainDirectory);
            appDomainFactory.load(assemblyName);
            List<string> loadedAssemblies = appDomainFactory.getAssemblies(false);
            Assert.That(loadedAssemblies.Count > 0 && loadedAssemblies.Contains(assemblyName),
                        "Loaded assembly was not there");
            object proxyObject = appDomainFactory.getProxyObject((typeToCreate));
            Assert.IsNotNull(proxyObject, "proxyObject was null");
            object resultInAppDomainFactory = appDomainFactory.invoke(proxyObject, methodToInvoke, methodParams);
            DI.log.debug("{0} = {1}", "resultInAppDomainFactory", resultInAppDomainFactory);
            Assert.That(
                (resultInCurrentAppDomain != resultInAppDomainFactory) &&
                (resultInNewAppDomain != resultInAppDomainFactory), "All results should be different");

            // test creating objects using the format {type} {assembly}
            var appDomainFactory2 = new O2AppDomainFactory("appDomainFactory2", testAppDomainDirectory);
            appDomainFactory.load(assemblyName);
            object proxyObject2 = appDomainFactory2.getProxyObject(typeToCreate + " " + assemblyName);
            Assert.IsNotNull(proxyObject2, "proxyObject2 was null");
            DI.log.debug("{0} = {1}", "appDomainFactory2", appDomainFactory.invoke(proxyObject2, methodToInvoke));

            // test if we can get a MethodInfo using using the format {method} {type} {assembly}
            var appDomainFactory3 = new O2AppDomainFactory("appDomainFactory3", testAppDomainDirectory);
            appDomainFactory.load(assemblyName);
            var methodInfoFromappDomainFactory3 =
                (MethodInfo) appDomainFactory3.getProxyMethod(methodToInvoke + " " + typeToCreate + " " + assemblyName);
            Assert.That(methodInfoFromappDomainFactory3 != null, "methodInfoFromappDomainFactory3 was null");
            DI.log.debug("{0} MethodInfo = {1}", "from appDomainFactory3", methodInfoFromappDomainFactory3.ToString());
            // we can't invoke this guy because we don't have the proxy

            // test if we can invoke a MethodInfo using using the format {method} {type} {assembly}
            var appDomainFactory4 = new O2AppDomainFactory("appDomainFactory4", testAppDomainDirectory);
            appDomainFactory.load(assemblyName);
            object invocationResult4 =
                appDomainFactory4.invokeMethod(methodToInvoke + " " + typeToCreate + " " + assemblyName);
            Assert.That(invocationResult4 != null, "invocationResult4 was null");
            DI.log.debug("[{0}] {1} {2} {3} = {4}", "from appDomainFactory4", assemblyName, typeToCreate, methodToInvoke,
                         invocationResult4); // we can't invoke this guy because we don't have the proxy
        }

        [Test]
        public void Test_PropertiesAndMethodsWithParameters()
        {
            var propertyValue = "value";
            var intvalue = 2009;
            var appDomain = new O2AppDomainFactory("testAppDomain", AppDomain.CurrentDomain.BaseDirectory);
            appDomain.load(assemblyName);

            // invoke using direct cast
            var proxy = (O2Proxy) appDomain.getProxyObject(typeToCreateSimpleName);
            Assert.That(proxy != null, "proxy was null");
            proxy.Property = propertyValue;
            Assert.That(proxy.Property == propertyValue, "proxy.Property != propertyValue");
            Assert.That(proxy.returnConcatParamData(proxy.Property, intvalue) == proxy.Property + intvalue,
                        "error in returnConcatParamData");

            // invoke using O2AppDomainFactory methods

            Assert.Ignore(
                "todo: invoke using O2AppDomainFactory methods (need to implement Set and Get wrapper methods)");
        }
    }
}
