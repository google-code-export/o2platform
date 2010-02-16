// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.IO;
using System.Collections.Generic;
using O2.Interfaces.O2Core;
using O2.Interfaces.XRules;
using O2.Kernel;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
//O2Tag_AddReferenceFile:E:\O2\_Bin_(O2_Binaries)\_O2_Scanner_DotNet.exe
//O2Tag_AddReferenceFile:E:\O2\_Bin_(O2_Binaries)\O2_External_PostSharp.dll
using O2.Scanner.DotNet.PostSharp;
//O2Tag_AddReferenceFile:nunit.framework.dll
using NUnit.Framework; 
//O2Tag_AddReferenceFile:mono.cecil.dll
using O2.External.O2Mono.MonoCecil;
using Mono.Cecil;

namespace O2.UnitTests.Standalone.O2DotNetScanner
{
    [TestFixture]
    public class _PostSharpHooks : KXRule
    {    
        private readonly static IO2Log log = PublicDI.log;    	    	
    	
        private readonly string scriptToInstrument = Path.Combine(PublicDI.config.hardCodedO2LocalSourceCodeDir, @"_O2_UnitTests\_testScripts\TestScript_MultipleCalls.cs");
    	    		      	
    	
        [Test]
        public string checkIfTargetExists()
        {
            Assert.That(File.Exists(scriptToInstrument));
            log.debug("File Exists: {0}", scriptToInstrument);
            return scriptToInstrument;
        }
    	
        [Test]
        public string compileFileToExe()
        {   
            var assembly = new  CompileEngine().compileSourceFiles(new List<string> {scriptToInstrument} ,"O2_Scanner_DotNet._TestScripts.TestScript_MultipleCalls");
            Assert.That(assembly!= null);
            Assert.That(Path.GetExtension(assembly.Location) == ".exe");
            log.debug("File Successfully compiled: {0}", assembly.Location);    		
            log.info("compileFileToExe executed ok");
            return assembly.Location;
        } 
    	
        public static void forDebug()
        {
            new _PostSharpHooks().runPostSharpOnAssembly();
        }
    	
        [Test]
        public string runPostSharpOnAssembly()
        {
            log.info("in runPostSharpOnAssembly");
    		
            var assemblyOriginal = compileFileToExe();
            var assemblyCopy = Files.Copy(assemblyOriginal, assemblyOriginal + "_PS.exe");
    		    		
            var assembly = CecilUtils.getAssembly(assemblyCopy);

            InjectAttributes.addO2PostSharpHookAttribute(assembly);            
            InjectAttributes.addOnMethodInvocationttribute(assembly, assembly.Name.Name,"*","TestScript_MultipleCalls","validate");
            
            assembly.Name.PublicKey = null;
            AssemblyFactory.SaveAssembly(assembly, assemblyCopy);
            Hacks.patchAssemblyForCecilPostSharpBug(assemblyCopy);
            
            InjectAttributes.addO2PostSharpHookAttribute(assemblyCopy);    		    		    
    		    		
            Assert.That(PostSharpExecution.runPostSharpOnAssembly(assemblyCopy)); 
            Assert.That(PostSharpUtils.arePostSharpDllsAddedAsReferences(assemblyCopy));
            Assert.That(false == PostSharpUtils.arePostSharpDllsAddedAsReferences(assemblyOriginal));
            log.info("runPostSharpOnAssembly executed ok");
            return "assemblyCopy";
        }
    	
        [Test]
        public void executeAssembly()
        {
            var assemblyWithPostSharpHooks = runPostSharpOnAssembly();    		
            var args = "TEST_PARAMETER";
            var result = Processes.startProcessAsConsoleApplicationAndReturnConsoleOutput(assemblyWithPostSharpHooks,args);
            Assert.That(false == string.IsNullOrEmpty(result));
            log.info(result);
        }
    	
    	
        /*[Test]
    	public void logTest3()
    	{   
    		throw new Exception();
    	}*/   
    }
}