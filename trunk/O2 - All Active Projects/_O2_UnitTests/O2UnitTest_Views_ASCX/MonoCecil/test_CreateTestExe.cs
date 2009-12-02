using System.Collections.Generic;
using System.IO;
using Mono.Cecil;
using NUnit.Framework;
using O2.DotNetWrappers.DotNet;
using O2.External.O2Mono.MonoCecil;

namespace O2.UnitTests.Test_O2CoreLib.O2Core.O2CoreLib.MonoCecil
{
    [TestFixture]
    public class test_CreateTestExe
    {
        [Test]
        public void createHelloWorld()
        {
            string testFile = new CreateTestExe().createBasicHelloWorldExe(true).save();
            Assert.IsTrue(File.Exists(testFile), "File was not created");
            List<MethodDefinition> typeDefinitions = CecilUtils.getMethods(testFile);
            Assert.IsTrue(typeDefinitions.Count > 0, "no typeDefinitions in file created");
            List<string> modules = StringsAndLists.getStringListFromList(CecilUtils.getModules(testFile));
            List<string> types = StringsAndLists.getStringListFromList(CecilUtils.getMethods(testFile));
            List<string> methods = StringsAndLists.getStringListFromList(CecilUtils.getTypes(testFile));
            Assert.IsTrue(modules.Count > 0, "no modules in file created");
            Assert.IsTrue(types.Count > 0, "no types in file created");
            Assert.IsTrue(methods.Count > 0, "no methods in file created");
            Assert.IsTrue(modules[0].IndexOf("main") > 0, "main module");
            Assert.IsTrue(types[0] == "System.Void BasicTest.Program::Main()", "Main Type");
            Assert.IsTrue(methods[0] == "<Module>", "Module Method");
            Assert.IsTrue(methods[1] == "BasicTest.Program", "Program Method");

            List<string> methodsCalled = StringsAndLists.getStringListFromList(CecilUtils.getMethodsCalledInsideAssembly(testFile));
            Assert.IsTrue(methodsCalled.Count > 0, "no methodsCalled");
            Assert.IsTrue(methodsCalled[0] == "System.Void System.Console::WriteLine(System.String)", "WriteLine #1");
            Assert.IsTrue(methodsCalled[1] == "System.Void System.Console::WriteLine(System.String)", "WriteLine #2");
            Assert.IsTrue(methodsCalled[2] == "System.String System.Console::ReadLine()", "ReadLine #1");
        }
    }
}