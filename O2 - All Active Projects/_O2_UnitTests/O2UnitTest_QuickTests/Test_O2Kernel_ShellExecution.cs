// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using O2.DotNetWrappers.Windows;
using O2.Kernel.O2CmdShell;

namespace O2.UnitTests.QuickTests
{
    [TestFixture]
    public class Test_O2Kernel_ShellExecution
    {
        private ShellIO shellIO;
        private ShellExecution shellExecution;

        [SetUp]
        public void startO2CmdShell()
        {
            var stringWriter = new StringWriter();
            shellIO = new ShellIO(stringWriter);
            shellExecution = new ShellExecution(shellIO);
            var testMessage = "This is a Unit Test for O2 Kernels cmdShell";
            shellIO.writeLine(testMessage);
            var shellOutputText = stringWriter.ToString();                        
            Assert.That(testMessage == shellOutputText.Trim(), "testMessage != shellOutputText.Trim()");
            DI.log.info("O2 Kernel Shell text message: {0}", shellOutputText);
            // assign a new StringWriter so that we clear the output buffer
            shellIO.outputTextWriter = new StringWriter();            
        }

        public void test_BaseCommand(string cmdToExecute, object[] cmdParameters)
        {
            var methodInfoOfCmdToExecute = DI.reflection.getMethod(typeof(ShellCommands), cmdToExecute, cmdParameters);
            Assert.That(methodInfoOfCmdToExecute != null, "Could not find methodInfoOfCmdToExecute for: ", cmdToExecute);
            foreach (var cmdParameter in cmdParameters)
                cmdToExecute += " " + cmdParameter.ToString();
            shellExecution.execute(cmdToExecute);
            var responseViaShell = shellIO.outputTextWriter.ToString();
            Assert.That(responseViaShell != null, "responseViaShell == null");
            var responseViaReflection = methodInfoOfCmdToExecute.Invoke(null, cmdParameters).ToString();
            // to check the invokation result use the value in shellIO.lastExecutionResult
            Assert.That(shellIO.lastExecutionResult == responseViaReflection, "responseViaShell !=  responseViaReflection , \n\n " + responseViaShell + "   !=    " +  responseViaReflection);
            DI.log.d("cmd execution response: " + responseViaShell.Trim());          
        }

        public void test_CmdExeExecution(string cmdToExecute)
        {
            shellExecution.execute(cmdToExecute);
            var responseViaShell = shellIO.lastExecutionResult;
            Assert.That(responseViaShell != null, "responseViaShell == null");
            cmdToExecute = "/c " + cmdToExecute.Substring(1);
            var responseVirDirectExecution = Processes.startProcessAsConsoleApplicationAndReturnConsoleOutput(
                "cmd.exe", cmdToExecute);
            //DI.log.i("responseViaDirectExecution: " + responseVirDirectExecution);
            //DI.log.i("responseViaShell: " + responseViaShell);
            Assert.That(responseViaShell == responseVirDirectExecution, "responseViaShell != responseVirDirectExecution");
            
        }

   //     public void test_DirectMethodExecution(string cmdToExecute)
   //     {
   //         shellExecution.execute(cmdToExecute);
   //        }

        [Test]
        public void test_ShellCommands()
        {
            Assert.That(shellExecution != null, "shellExecution was null"); 

            //var cmdToExecute = "hello";
            //var cmdParameters = new object[0];
      //      test_BaseCommand("hello", new object[0]);
            test_BaseCommand("sendMessage", new object[] {"this_is_a_message"});
 //           test_CmdExeExecution(">dir");
        //    test_DirectMethodExecution("DI.log.info this_is_an_info");
            
        }
    }
}
