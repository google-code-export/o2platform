using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Mono.Cecil;
using NUnit.Framework;
using O2.Core.CIR.CirCreator.DotNet;
using O2.Core.CIR.CirObjects;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.External.O2Mono.MonoCecil;
using O2.Kernel.Interfaces.CIR;

namespace O2.UnitTests.Test_O2CoreCIR.Test_CirCreator
{
    public class testType
    {
        public void testMethodA()
        {
            Console.WriteLine("Calls Console.WriteLine with contents of " + testMethodB());
            Console.WriteLine("Now getting testMethodC test" + testMethodC("with extra test"));
        }

        public string testMethodB()
        {
            return "testMethodB contents";
        }

        public string testMethodB(string testMethodBwithParameter)
        {
            return "testMethodB contents";
        }

        public string testMethodC(string extraText)
        {
            return String.Format("testMethodC contents with extra test: {0}", extraText);
        }

        public void testMethodD(string text, Type type)
        {
            Console.WriteLine(String.Format("This is a string: {0}", text.GetType().FullName));
            Console.WriteLine(String.Format("This is a type: {0}", type.GetType().FullName));
        }

        public void infiniteLoop(string param)
        {
            infiniteLoop(param + "1");
        }
    }

    [TestFixture]
    public class Test_CirCreatorEngineDotNet
    {
        //public static string targetAssembly = DI.config.ExecutingAssembly;
        private readonly CirFactory cirFactory = new CirFactory();

        public AssemblyDefinition getTestAssembly()
        {
            DI.log.info("using assembly:{0}", Assembly.GetExecutingAssembly().Location);
            return CecilUtils.getAssembly(Assembly.GetExecutingAssembly().Location);
        }

        public void Test_getMemberFunction(CirData cirData, TypeDefinition testType, string methodToTest,
                                           Type[] methodParameters)
        {
            MethodDefinition cecilMethodDefinition = CecilUtils.getMethod(testType, methodToTest, methodParameters);
            Assert.IsNotNull(cecilMethodDefinition, "cecilMethodDefinition was null for method", methodToTest);
            ICirFunction cirFunction = cirFactory.processMethodDefinition(cirData, cecilMethodDefinition,null);
            Assert.IsNotNull(cirFunction, "cecilMethodDefinition was null for method", cecilMethodDefinition);
            Assert.That(CirCecilCompare.areEqual_MethodDefinitionAndCirFunction(cecilMethodDefinition, cirFunction),
                        "areEqual_MethodDefinitionAndCirFunction failed for method: " + cecilMethodDefinition);
        }

        public void Test_processTypeDefinition(ICirData cirData, ICirClass cirClass)
        {
            Assert.That(cirClass != null, "cirClass was null");
            Assert.That(cirClass.dFunctions.Count == 7, "cirClass.dFunctions.Count != 7 , it was " + cirClass.dFunctions.Count);

            // check if we can get the functions by name
            ICirFunction testMethodA = cirClass.getFunction("testMethodA()");
            Assert.IsNotNull(testMethodA, "could not get testMethodA from cirClass object");
            ICirFunction testMethodB = cirClass.getFunction("testMethodB()");
            Assert.IsNotNull(testMethodB, "could not get testMethodB from cirClass object");
            Assert.IsNotNull(cirClass.getFunction("testMethodB(System.String)"),
                             "could not get testMethodB(string) from cirClass object");
            Assert.IsNotNull(cirClass.getFunction("testMethodD(System.String,System.Type)"),
                             "could not get testMethodD(System.String,System.Type) from cirClass object");

            ICirFunction testMethodC = cirClass.getFunction("testMethodC");

            // check if we have the calls and isCalledBy
            Assert.That(testMethodA.FunctionsCalledUniqueList.Contains(testMethodB),
                        "failed on testMethodA.FunctionsCalledUniqueList.Contains(testMethodB)");
            Assert.That(testMethodA.FunctionsCalledUniqueList.Contains(testMethodC),
                        "failed on testMethodA.FunctionsCalledUniqueList.Contains(testMethodB)");
            var found = false;
            foreach (var calledByFunction in testMethodB.FunctionIsCalledBy)
                if (calledByFunction.cirFunction == testMethodA)
                    found = true;
            Assert.That(found,
                        "failed on testMethodB.FunctionIsCalledBy.Contains(testMethodA)");
        }


        public void checkThatAllFunctionsMatch(ICirData cirData, AssemblyDefinition testAssembly)
        {
            //var assertChecks = true;
            foreach (MethodDefinition methodDefinition in CecilUtils.getMethods(testAssembly))
            {
                string functionSignature =
                    CirFactoryUtils.getFunctionUniqueSignatureFromMethodReference(methodDefinition);

                //if (assertChecks)
                //{
                Assert.That(cirData.dFunctions_bySignature.ContainsKey(functionSignature),
                            "Could not find functionSignature in cirData object: " + functionSignature);
                //}
                /*else if (cirData.dFunctions_bySignature.ContainsKey(functionSignature) == false)
                {
                    var BaseClass = methodDefinition.DeclaringType.FullName;

                    DI.log.error("    ****       Could not find functionSignature in cirData object: " +
                                 functionSignature);
                    return;
                }*/


                ICirFunction cirFunction = cirData.dFunctions_bySignature[functionSignature];

                //if (assertChecks)
                {
                    Assert.That(CirCecilCompare.areEqual_MethodDefinitionAndCirFunction(methodDefinition, cirFunction),
                                "Function's didn't match: " + functionSignature);
                }
                /*else if (CirCecilCompare.areEqual_MethodDefinitionAndCirFunction(methodDefinition, cirFunction) == false)
                {
                    DI.log.error("    ****   Function's didn't match: " + functionSignature + "   for   " +
                                 testAssembly.Name);
                }*/


                //
            }
        }

        public void Test_LoadingAssembly(ICirData cirData, string assemblyToLoad, bool verify, bool verbose)
        {
            AssemblyDefinition assemblyDefinition = CecilUtils.getAssembly(assemblyToLoad);
            if (assemblyDefinition == null)
                return;

            var loadTimer = new O2Timer("Assembly " + Path.GetFileName(assemblyToLoad) + " Loaded in");
            if (verbose)
                loadTimer.start();
            cirFactory.processAssemblyDefinition(cirData, assemblyDefinition,assemblyToLoad);
            if (verbose)
                loadTimer.stop();
            if (verify)
            {
                var checkTimer = new O2Timer("       functions checked in  ");
                if (verbose)
                    checkTimer.start();
                checkThatAllFunctionsMatch(cirData, assemblyDefinition);
                if (verbose)
                    checkTimer.stop();
            }
        }

        public void Test_ProcessingAllO2AssembliesFromDirectory(ICirData cirData, string targetDirectory, bool verify,
                                                                bool verbose)
        {
            O2Timer checkTimer =
                new O2Timer("Test_ProcessingAllAssembliesFromDirectory " + targetDirectory + " took ").start();
            List<string> targetAssemblies = Files.getFilesFromDir_returnFullPath(targetDirectory, "*.exe");
            targetAssemblies.AddRange(Files.getFilesFromDir_returnFullPath(targetDirectory, "*.dll"));
            //targetAssemblies.AddRange();
            DI.log.debug("Testing processing of {0} dlls and exes directory {1}", targetAssemblies.Count,
                         targetDirectory);
            if (!verify)
                DI.log.debug("Note, verification is disabled");
            foreach (string targetDll in targetAssemblies)
            {
                if (CecilUtils.isDotNetAssembly(targetDll, false))
                    Test_LoadingAssembly(cirData, targetDll, verify, verbose);
            }
            CirFactoryUtils.showCirDataStats(cirData);
            checkTimer.stop();
        }

        [Test]
        public void specificAssemblyTest()
        {
            var pathToDotNetFrameworkAssemblies = @"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727";
            string assemblyToTest = Path.Combine(pathToDotNetFrameworkAssemblies, "System.EnterpriseServices.dll");
            ICirData cirData = new CirData();
            Test_LoadingAssembly(cirData, assemblyToTest, true, true);
        }

        [Test]
        public void Test_CecilGetMethod()
        {
            AssemblyDefinition testAssembly = getTestAssembly();
            Assert.IsNotNull(testAssembly, "testAssembly was null");
            TypeDefinition testType = CecilUtils.getType(testAssembly, "testType");
            Assert.IsNotNull(testType, "testType was null");

            // This should be moved to a CecilUtil UnitTest since here we are just making just CecilUtils.getMethod is working as expected
            // testMethod1
            MethodDefinition testMethod1 = CecilUtils.getMethod(testType, "testMethodA", new object[0]);
            Assert.IsNotNull(testMethod1, "testMethod1 was null");
            // testMethod2
            MethodDefinition testMethod2 = CecilUtils.getMethod(testType, "testMethodC", new object[] {"test"});
            Assert.IsNotNull(testMethod2, "testMethod2 was null");
            // testMethod3
            MethodDefinition testMethod3 = CecilUtils.getMethod(testType, "testMethodC", new[] {typeof (string)});
            Assert.That(testMethod3.Name == "testMethodC", "Wrong testMethod3.Name", testMethod3.Name);
            Assert.IsNotNull(testMethod3, "testMethod3   was null");
            // testMethod4
            MethodDefinition testMethod4 = CecilUtils.getMethod(testType, "testMethodD", new[] {typeof (string)});
            Assert.IsNull(testMethod4, "testMethod4 should be null");
            // testMethod4a
            MethodDefinition testMethod4a = CecilUtils.getMethod(testType, "testMethodD",
                                                                 new object[] {"string", "stringType".GetType()});
            // not that this should actually work, the problem is that "stringType".GetType() is going to become System.RuntimeType (instead of System.Type)
            Assert.IsNull(testMethod4a, "testMethod4a was null");
            //Assert.IsNotNull(testMethod4a, "testMethod4a was null");
            //Assert.That(testMethod4a.Name == "testMethodD", "Wrong testMethod4a.Name" + testMethod4a.Name);            
            // testMethod5
            MethodDefinition testMethod5 = CecilUtils.getMethod(testType, "testMethodD",
                                                                new[] {typeof (string), typeof (Type)});
            Assert.IsNotNull(testMethod5, "testMethod5 was null");
            Assert.That(testMethod5.Name == "testMethodD", "Wrong testMethod5.Name" + testMethod5.Name);
        }

        [Test]
        public void Test_MultipleProcessingOfSameTypesAndAssembly()
        {
            ICirData cirData = new CirData();
            AssemblyDefinition testAssembly = getTestAssembly();            
            // add assembly Object
            cirFactory.processAssemblyDefinition(cirData, testAssembly,null);
            ICirClass cirClass = cirData.getClass("testType");
            // test it
            Test_processTypeDefinition(cirData, cirClass);

            // add each type individually
            foreach (TypeDefinition typeDefinition in CecilUtils.getTypes(testAssembly))
                cirFactory.processTypeDefinition(cirData, typeDefinition);

            // test it again
            ICirClass cirClass2 = cirData.getClass("testType");
            Test_processTypeDefinition(cirData, cirClass);
            Test_processTypeDefinition(cirData, cirClass2);
        }

        [Test]
        public void Test_processAssemblyDefinition()
        {
            ICirData cirData = new CirData();
            AssemblyDefinition testAssembly = getTestAssembly();
            cirFactory.processAssemblyDefinition(cirData, testAssembly,null);
            Assert.That(cirData.dClasses_bySignature.Count > 0, "cirData.dClasses_bySignature.Count == 0");

            // check that all functions from this assembly match
            checkThatAllFunctionsMatch(cirData, testAssembly);

            // check that the type is there 
            TypeDefinition testTypeDefinition = CecilUtils.getType(testAssembly, "testType");
            Assert.That(
                cirData.dClasses_bySignature.ContainsKey(
                    CirFactoryUtils.getTypeUniqueSignatureFromTypeReference(testTypeDefinition)),
                "testTypeDefinition.FullName was not there");
            Assert.IsNotNull(cirData.getClass(testTypeDefinition.FullName),
                             "when using testTypeDefinition.FullName, cirClass was null");

            ICirClass cirClass = cirData.getClass("testType");
            Assert.IsNotNull(cirClass, "when using 'testType',  cirClass was null");
            Test_processTypeDefinition(cirData, cirClass);
        }

        [Test,
         Ignore("Run manually since it takes a bit of time to do it with verification (which should run with no errors)")]
        public void Test_ProcessingAllNetFrameworkAssemblies_V2_0_50727_withNoVerify()
        {
            ICirData cirData = new CirData();
            var pathToDotNetFrameworkAssemblies = @"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727";

            Test_ProcessingAllO2AssembliesFromDirectory(cirData, pathToDotNetFrameworkAssemblies, false, true
                /*verbose*/);
        }

        [Test, Ignore("Run manually since thi is quite resource (takes 2m 44s) (this should run with no errors)")]
        public void Test_ProcessingAllNetFrameworkAssemblies_V2_0_50727_withVerify()
        {
            ICirData cirData = new CirData();
            var pathToDotNetFrameworkAssemblies = @"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727";

            Test_ProcessingAllO2AssembliesFromDirectory(cirData, pathToDotNetFrameworkAssemblies, true, true /*verbose*/);
        }

        [Test]
        public void Test_ProcessingAllO2AssembliesWithNoVerify()
        {
            ICirData cirData = new CirData();
            Test_ProcessingAllO2AssembliesFromDirectory(cirData, DI.config.hardCodedO2LocalBuildDir, false, false /*verbose*/);
        }

        [Test,
         Ignore("Run manually since it takes a bit of time to do it with verification (which should run with no errors)")]
        public void Test_ProcessingAllO2AssembliesWithVerify()
        {
            ICirData cirData = new CirData();
            Test_ProcessingAllO2AssembliesFromDirectory(cirData, DI.config.hardCodedO2LocalBuildDir, true, false /*verbose*/);
        }

        [Test]
        public void Test_ProcessingO2Kernel()
        {
            DI.log.info("Testing with O2Kernel");
            ICirData cirData = new CirData();
            string O2KernelExe = DI.config.ExecutingAssembly; // this gets O2_Kernel.Exe
            Test_LoadingAssembly(cirData, O2KernelExe, true /*verify*/, false /*verbose*/);
            CirFactoryUtils.showCirDataStats(cirData);
        }

        [Test]
        public void Test_processMethodDefinition()
        {
            var cirData = new CirData();
            AssemblyDefinition testAssembly = getTestAssembly();
            TypeDefinition testType = CecilUtils.getType(testAssembly, "testType");


            Test_getMemberFunction(cirData, testType, "testMethodA", new Type[0]);
            Test_getMemberFunction(cirData, testType, "testMethodC", new[] {typeof (string)});
            Test_getMemberFunction(cirData, testType, "testMethodD", new[] {typeof (string), typeof (Type)});
        }

        [Test]
        public void Test_processTypeDefinition()
        {
            ICirData cirData = new CirData();
            AssemblyDefinition testAssembly = getTestAssembly();
            TypeDefinition testTypeDefinition = CecilUtils.getType(testAssembly, "testType");
            ICirClass cirClass = cirFactory.processTypeDefinition(cirData, testTypeDefinition);
            Test_processTypeDefinition(cirData, cirClass);
        }
    }
}