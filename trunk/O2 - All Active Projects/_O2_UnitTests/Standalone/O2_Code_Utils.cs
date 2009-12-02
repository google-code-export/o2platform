// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using O2.Kernel;
using O2.Kernel.Interfaces.O2Core;
using O2.DotNetWrappers.Windows;
using O2.External.WinFormsUI.Forms;
using O2.Kernel.Interfaces.Views;
using O2.Views.ASCX.CoreControls;
using O2.External.O2Mono.MonoCecil;
//O2Tag_AddReferenceFile:nunit.framework.dll
using NUnit.Framework;

namespace O2.UnitTests.Standalone
{
    [TestFixture]
    public class O2_Code_Utils
    {    
        private readonly static IO2Log log = PublicDI.log;    	    	
    	
        [Test]
        public  void _showO2ObjectModelInNewWindow()
        {
            O2AscxGUI.openAscx(typeof(ascx_O2ObjectModel) , O2DockState.Float, "O2 Object Model");   					
        }
    	
        [Test]
        public int currentProcessId()
        {
            return Processes.getCurrentProcessID();
        }
    	
        [Test]
        public void showAssemblyDependencies()
        {
            var assemblyToProcess =@"E:\O2\_Bin_(O2_Binaries)\_O2_Scanner_DotNet.exe";
            var dependencies = CecilAssemblyDependencies.getListOfDependenciesForAssembly(assemblyToProcess);
            foreach(var dependency in dependencies)
                log.write(@"//O2Tag_AddReferenceFile:{0}", dependency);
        }
    	
        [Test]
        public void deleteO2TempDirContents()
        {
            Files.deleteAllFilesFromDir(PublicDI.config.O2TempDir);
    		
        }
    	
        [Test] 
        public static string executeMethod()
        {
            try
            {
                var dependency = Path.Combine(PublicDI.config.O2TempDir,"_O2_Scanner_DotNet.exe");
                var dependencyAssembly = PublicDI.reflection.getAssembly(dependency);
                PublicDI.log.debug(dependencyAssembly.FullName);
                //return dependencyAssembly.FullName;
    			
                var assemblyToLoad = Path.Combine(PublicDI.config.O2TempDir, @"tmp1AC.tmp.dll");
                //var assemblyToLoad = Path.Combine(PublicDI.config.O2TempDir, @"tmp19C.tmp.dll");
    			
                var assembly = PublicDI.reflection.getAssembly(assemblyToLoad);
                Assert.That(assembly != null, "assembly was null");
                foreach(var method in PublicDI.reflection.getMethods(assembly))
                    log.info(method.Name);
                var methodToInvoke = PublicDI.reflection.getMethod(assemblyToLoad, "runPostSharpOnAssembly");
	    		
                Assert.That(methodToInvoke != null, "methodToInvoke was null");
	    		
                var liveObject = PublicDI.reflection.createObjectUsingDefaultConstructor(methodToInvoke.ReflectedType);
                Assert.That(liveObject != null, "liveObject was null");
                methodToInvoke.Invoke(liveObject, new object[]{});
                //return PublicDI.reflection.invoke(methodToInvoke,null).ToString();
            }
            catch (Exception ex)
            {
                PublicDI.log.error("in executeMethod: {0}", ex.Message);
                if (ex.InnerException != null)
                {
                    PublicDI.log.error("Inner: {0}", ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                        PublicDI.log.error("has inner");
                }
                return "error in execution";
            }
            //var result = 
            //return result 
            return "ok";
        }
    }
}