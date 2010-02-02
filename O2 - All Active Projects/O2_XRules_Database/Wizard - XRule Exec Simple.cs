// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using O2.Kernel;
using O2.Kernel.Interfaces.O2Core;
using O2.DotNetWrappers.DotNet;
using O2.Views.ASCX.MerlinWizard;
// extra references and the namespaces they import
//O2Tag_AddReferenceFile:nunit.framework.dll
//O2Tag_AddReferenceFile:merlin.dll
using Merlin;
using O2.Views.ASCX.MerlinWizard.O2Wizard_ExtensionMethods;
using O2.DotNetWrappers.Network;
//O2File:C:\O2\O2 - All Active Projects\O2 - All Active Projects\O2Core\O2_Views_ASCX\MerlinWizard\MerlinWizard_ExtensionMethods.cs

namespace O2.XRules.Database
{
    [O2Wizard]
    public class Wizard_XRule_Exec_Simple
    {    
        private static IO2Log log = PublicDI.log;
        public static string scriptToExecute = "";
//        public static string scriptToAutoCompile { get; set; }
				
        [StartWizard]
        public void startWizard()
        {			
            var o2Wizard = new O2Wizard("Execute XRule");
            scriptToExecute = getClickOnceScriptPath();
            //scriptToExecute = @"C:\O2\XRulesDatabase\_Rules\_Samples\HelloWorld.cs.o2";
            //if (false == File.Exists(scriptToExecute))
            addStep_SelectXRuleToExecute(o2Wizard);
            addStep_CompileScript(o2Wizard);
            
            o2Wizard.start();
        }
        
        private string getClickOnceScriptPath()
        {
            if (AppDomain.CurrentDomain.SetupInformation.ActivationArguments != null &&
                AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData != null &&
                AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData.Count() > 0)
            {                
                var file = AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData[0];
                PublicDI.log.debug("ClickOnce file raw: {0}", file);
                file = file.Replace("file:///", "");
                file = WebEncoding.urlDecode(file);
                file = file.Replace( "/","\\");
                PublicDI.log.debug("ClickOnce file final: {0}", file);
                return file;
            }
            
            return "";
        }

        private string testFileToUse()
        {
            return Web.checkIfFileExistsAndDownloadIfNot("HelloWorld.cs",
                                                         @"http://o2platform.googlecode.com/svn/trunk/O2%20-%20All%20Active%20Projects/O2_XRules_Database/_Rules/_Samples/HelloWorld.cs.o2");
            return @"C:\O2\XRulesDatabase\_Rules\_Samples\HelloWorld.cs";
        }
 		
        public void addStep_SelectXRuleToExecute(O2Wizard o2Wizard)
        {
            TextBox textboxWithFileName = null;
            IStep step = null;
            step = o2Wizard.Steps.add_SelectFile("Chose XRule File to Execute",scriptToExecute, 30,20,70,
                                                 (newText)=> {setScriptToExecute(step, newText);},
                                                 (textBox, Button) => textboxWithFileName = textBox );
        	 	
            step.AllowNextStrategy = () => {return false;};
        	 
            //var panel = (Panel)step.FirstControl;
            var firstLabel = step.add_Label("You can select a local XRule file to execute", 5,5);
            firstLabel.AutoSize = true;
            step.add_Label("Or you can drop a script to execute on the drop area below", 70,5).AutoSize = true;
            //var step = o2Wizard.Steps.add_Panel("Select XRule to execute");
			 			 
            step.add_DropArea(20, 90, 165,10, 
                              (fileName) => {
                                                setScriptToExecute(step, fileName);
                                                if (textboxWithFileName != null)
                                                    textboxWithFileName.set_Text(scriptToExecute);
                              });
            step.add_Link("download test file", 5, firstLabel.Width + 30, 
                          ()=> textboxWithFileName.set_Text(testFileToUse()));			 				 
			 	
            step.OnComponentAction= (_step) =>
                                        {
                                            if (File.Exists(scriptToExecute))
                                                _step.allowNext(true);		
                                        };
        }
        
		
        public void setScriptToExecute(IStep step, string newValue)
        {				
            if (step == null)
                return;
            scriptToExecute = newValue;
            if (File.Exists(scriptToExecute))
                step.allowNext(true);		
            else
                step.allowNext(false);		
        }
        public void addStep_CompileScript(O2Wizard o2Wizard)
        {			
            var panel = o2Wizard.Steps.add_Panel("Compiling and Executing script","",onCompileScript);																			
        }
		
        public void onCompileScript(IStep step)
        {
            O2Thread.mtaThread(
                ()=>
                    {
                        step.clear();
                        //var panel = step.FirstControl; 
                        step.add_Label("Compiling Script:" +  scriptToExecute);

                        var compileEngine = new CompileEngine();
                        var compiledAssembly = compileEngine.compileSourceFile(scriptToExecute);
                        if (compiledAssembly != null)					
                        {
                            var label = step.add_Label("Compilation was ok", 20);
                            label.ForeColor = Color.DarkGreen;
                            step.add_Label("Which method to you want to execute?", 40);
                            var top = 40;
                            foreach(var method in PublicDI.reflection.getMethods(compiledAssembly))
                            {
                                if (PublicDI.reflection.getParameters(method).Count ==0 && 
                                    method.Name.Contains(">b") == false)
                                {
                                    var methodToExecute = method;
                                    step.add_Link(method.Name , top+=20 ,10 ,
                                                  ()=> PublicDI.reflection.invoke(methodToExecute));
                                };
                            }							
                        }
                        else
                        {
                            var label = step.add_Label("Compilation Failed", 20);
                            label.ForeColor = Color.DarkRed;
                            step.add_Label(compileEngine.sbErrorMessage.ToString(), 40);                            
                        }
						
                    });			   
        }
		
				
		
        // MOVE to Extension Methods class
				
    	    	    	    	    	   
    }
}