// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Reflection;
using DotNetGuru.AspectDNG.Joinpoints;
using NUnit.Framework;
using O2.DotNetWrappers.Windows;
using O2.Rnd.AspectDngHook;
using O2.RnD.AspectTests.UnitTests;


namespace O2.RnD.AspectTests.UnitTests
{
    /// <summary>
    ///This is a test class for DngAspectTest and is intended
    ///to contain all DngAspectTest Unit Tests
    ///</summary>
    [TestFixture]
    public class DngAspectTest
    {        
        /// <summary>
        ///A test for LogToFile
        ///</summary>
        [Test]
        public void LogToFileTest()
        {
            DngAspect.logTargetDir = DI.config.O2TempDir;
            String message = "This is a test Message";
            String logFile = DngAspect.LogToFile(message,".txt");
            DI.log.info("logFile:{0}", logFile);
            DI.log.info("DngAspect.logTargetDir:{0}", DngAspect.logTargetDir);
            Assert.IsTrue(File.Exists(logFile), "Log File Existance");
            String savedMessage = Files.getFileContents(logFile);
            Assert.IsTrue(message == savedMessage, "Confirming that message was saved");
            DI.log.info("savedMessage:{0}", savedMessage);
        }

        /// <summary>
        ///A test for Log
        ///</summary>
        [Test]
        public void LogTest()
        {
            // prepare test objects (need to create an instance of MethodJoinPoint
            var duUtils = new DngUtils();
            MethodBase mbMethod = duUtils.GetType().GetMethod("injectHooks");
            var oParams = new object[] {"Param1", "2nd Param", " Parameter #3"};
            var ojpOperationJoinPoint = new MethodJoinPoint(duUtils, oParams, mbMethod);
            Assert.IsNotNull(ojpOperationJoinPoint, "Could not create OperationoinPoint object");
            Assert.IsTrue(ojpOperationJoinPoint.RealTarget == duUtils, "Prob with RealTarget");
            Assert.IsTrue(ojpOperationJoinPoint.NbParameters == oParams.Length, "Prob with NbParameters");
            Assert.IsTrue(ojpOperationJoinPoint.TargetOperation == mbMethod, "Prob with TargetOperation");
            var createdLogFile = DngAspect.Log(ojpOperationJoinPoint, "return Data");
            Assert.That(File.Exists(createdLogFile),"probs with createdLogFile");
        }
    }
}
