// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using NUnit.Framework;
using O2.Kernel.Objects;
using O2.UnitTests.Test_O2CoreLib;
using O2.UnitTests.Test_O2ViewsASCX;
using System.Reflection;


namespace O2.UnitTests.Test_O2CoreLib.O2Core.O2CoreLib.Reflection
{
    public class ReflectTest
    {
        private readonly string test;

        public ReflectTest()
        {
            test = "in ReflectTest";
        }

        public ReflectTest(string testValue)
        {
            test = "in ReflectTest(string testValue): " + testValue;
        }

        public ReflectTest(string[] testValue)
        {
            test = "in ReflectTest(string testValue[]): " + testValue[0];
        }

        public string getTest()
        {
            return test;
        }

        public string getTest(string extraText)
        {
            return test + " " + extraText;
        }
    }

    [TestFixture]
    public class Test_Reflection
    {
        public void testObjects(string testDescription, string assemblyWithTypeToCreate, string typeToCreate,
                                object[] typeConstructorArguments, string expectedValue,
                                string methodThatReturnsValueToTest)
        {
            // using DefaultConstructor            
            object newObject = DI.reflection.createObject(assemblyWithTypeToCreate, typeToCreate, typeConstructorArguments);
            Assert.That(newObject != null, testDescription + " : newObject was null");
            object testValue = DI.reflection.invokeMethod_InstanceStaticPublicNonPublic(newObject, methodThatReturnsValueToTest, null);
            Assert.That(testValue != null, testDescription + " : testValue was null");
            Assert.That(testValue.ToString() == expectedValue,
                        testDescription + " : testValue != originalTestValueDefaultConstructor : " + testValue +
                        "  !=  " + expectedValue);
        }

        public static void staticTestFor_testObjectBuilder()
        {
            new Test_Reflection().testObjectBuilder();
        }

        [Test]
        public void testObjectBuilder()
        {
            var testString = "testString";
            var methodToCall = "getTest";
            var extraText = "extraText";
            var o2Assembly = new O2ObjectFactory(Assembly.GetExecutingAssembly().Location);

            var constructorParameters = new object[] {};

            O2Object usingFullTypeName = o2Assembly.ctor("O2.UnitTests.Test_O2CoreLib.O2Core.O2CoreLib.Reflection.ReflectTest");
            Assert.That(usingFullTypeName != null, "reflectTestClass == null");
            O2Object usingShortTypeName = o2Assembly.ctor("ReflectTest", constructorParameters);
            Assert.That(usingShortTypeName != null, "usingShortTypeName  == null");
            object methodToCallValue = usingShortTypeName.call(methodToCall);
            Assert.That(methodToCallValue != null, "usingShortTypeName.methodToCallValue == null");
            string reflectionValue = new ReflectTest().getTest();
            Assert.That(methodToCallValue.ToString() == reflectionValue, methodToCallValue + " != " + reflectionValue);

            O2Object usingShortTypeWithStringParam = o2Assembly.ctor("ReflectTest", new object[] {testString});
            Assert.That(usingShortTypeWithStringParam != null, "usingShortTypeWithStringParam  == null");
            methodToCallValue = usingShortTypeWithStringParam.call(methodToCall);
            Assert.That(methodToCallValue != null, "usingShortTypeWithStringParam.methodToCallValue == null");
            reflectionValue = new ReflectTest(testString).getTest();
            Assert.That(methodToCallValue.ToString() == reflectionValue, methodToCallValue + " != " + reflectionValue);

            methodToCallValue = usingShortTypeWithStringParam.call(methodToCall, new object[] {extraText});
            reflectionValue = new ReflectTest(testString).getTest(extraText);
            Assert.That(methodToCallValue.ToString() == reflectionValue,
                        "(extraText) " + methodToCallValue + " != " + extraText);
        }

        [Test]
        public void testObjectCreationUsingConstructor()
        {
            var typeToCreate = "O2.UnitTests.Test_O2CoreLib.O2Core.O2CoreLib.Reflection.ReflectTest";
            var testString = "testString";
            string originalTestValueDefaultConstructor = new ReflectTest().getTest();
            string originalTestValueStringConstructor = new ReflectTest(testString).getTest();
            string originalTestValueStringArrayConstructor = new ReflectTest(new[] {testString}).getTest();
            Assert.That(originalTestValueDefaultConstructor == "in ReflectTest", "testValue value din't match");
            string assemblyWithTypeToCreate = Assembly.GetExecutingAssembly().Location; // DI.config.hardCodedPathToO2UnitTestsDll;
            
            var typeConstructorArguments = new object[] {};
            testObjects("DefaultConstructor", assemblyWithTypeToCreate, typeToCreate, typeConstructorArguments,
                        originalTestValueDefaultConstructor, "getTest");
            typeConstructorArguments = new object[] {testString};
            testObjects("StringConstructor", assemblyWithTypeToCreate, typeToCreate, typeConstructorArguments,
                        originalTestValueStringConstructor, "getTest");
            typeConstructorArguments = new object[] {new[] {testString}};
            testObjects("StringArrayConstructor", assemblyWithTypeToCreate, typeToCreate, typeConstructorArguments,
                        originalTestValueStringArrayConstructor, "getTest");
        }
    }
}
