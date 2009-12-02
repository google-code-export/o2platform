using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using O2.Kernel;
using O2.Kernel.Interfaces.O2Core;
using O2.Kernel.Interfaces.O2Findings;
using O2.Kernel.Interfaces.XRules;
using O2.DotNetWrappers.O2Findings;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
//O2Tag_AddReferenceFile:E:\O2\_Bin_(O2_Binaries)\_O2_Scanner_DotNet.exe
using O2.Scanner.DotNet.PostSharp;
//O2Tag_AddReferenceFile:nunit.framework.dll
using NUnit.Framework; 
//O2Tag_AddReferenceFile:mono.cecil.dll
using O2.External.O2Mono.MonoCecil;
using Mono.Cecil;
using O2.External.WinFormsUI.Forms;
using O2.Kernel.Interfaces.Views;
using O2.Core.CIR.Ascx;
using O2.Views.ASCX.DataViewers;

namespace O2.Scanner.DotNet._UnitTests
{
    [TestFixture]
    public class _PostSharpHooks : KXRule
    {    
        private static IO2Log log = PublicDI.log;    	    	
    	
        string scriptToInstrument = @"C:\O2\_XRules_Local\TempFiles\TestScript_MultipleCalls.cs";
    	
    	[Test]
    	public  bool _showO2ObjectModel()
    	{
    		var o2Assemblies = CompileEngine.getListOfO2AssembliesInExecutionDir();
    		var methods = new List<MethodInfo>();
    		foreach(var assembly in o2Assemblies)
    		{    			
    			methods.AddRange(PublicDI.reflection.getMethods(assembly).ToArray());
    		}
    		log.info("there are {0} O2 assemblies", o2Assemblies.Count);
    		log.info("there are {0} methods", methods.Count);    		
    		var methodsSignature = new List<String>();
    		foreach(var method in methods)
    			methodsSignature.Add(method.ToString());
    		log.info("there are {0} methods sigantures", methodsSignature.Count);    		
    		var functionsViewer = (ascx_FunctionsViewer)O2AscxGUI.openAscx(typeof(ascx_FunctionsViewer), O2DockState.Float, "O2 Object Model");
    		functionsViewer.showSignatures(methodsSignature);
    		return true;
    	}
    	 
    	public static void _showO2ObjectModel_Deep()
    	{
    		var cirDataViewer = (ascx_CirDataViewer)O2AscxGUI.openAscx(typeof(ascx_CirDataViewer), O2DockState.Float, "O2 Object Model");
    		cirDataViewer.loadO2ObjectModel();
    	}
   	
    	
        public static string testStaticExecution(string filename)
        {            
            log.debug("File Exists: {0}", filename);
            log.showMessageBox("test");
            return "target file exists: " + filename;
        }
        
        [Test]
        public  string checkIfTargetExists()
        {
            Assert.That(File.Exists(scriptToInstrument));            
            log.debug("File Exists: {0}", scriptToInstrument);
            return "target file exists: " + scriptToInstrument;
        }
    	
        [Ignore]
        public string compileFileToExe()
        {   
            var assembly = new  CompileEngine().compileSourceFiles(new List<string> {scriptToInstrument} ,"O2_Scanner_DotNet._TestScripts.TestScript_MultipleCalls");
            Assert.That(assembly!= null);
            Assert.That(Path.GetExtension(assembly.Location) == ".exe");
            log.debug("File Successfully compiled: {0}", assembly.Location);    		
            log.info("compileFileToExe executed ok");
            return assembly.Location;
        } 
    	
        [Ignore]
        public string runPostSharpOnAssembly()
        {
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
            Assert.That(true == PostSharpUtils.arePostSharpDllsAddedAsReferences(assemblyCopy));
            Assert.That(false == PostSharpUtils.arePostSharpDllsAddedAsReferences(assemblyOriginal));
            log.info("runPostSharpOnAssembly executed ok");
            return assemblyCopy;
        }
    	
        [Ignore]
        public void executeAssembly()
        {
            var assemblyWithPostSharpHooks = runPostSharpOnAssembly();    		
            var args = "TEST_PARAMETER";
            var result = Processes.startProcessAsConsoleApplicationAndReturnConsoleOutput(assemblyWithPostSharpHooks,args);
            Assert.That(result != null && result != "");
            log.info(result);
        }
    	
    	
        /*[Test]
    	public void logTest3()
    	{   
    		throw new Exception();
    	}*/   
    }
}