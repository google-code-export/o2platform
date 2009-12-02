using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Mono.Cecil;
using NUnit.Framework;
using O2.DotNetWrappers.Windows;
using O2.External.O2Mono.MonoCecil;
using O2.UnitTests.Test_O2ViewsASCX;


namespace O2.UnitTests.Test_O2CoreLib.O2Core.O2CoreLib.MonoCecil
{
    public class testClass
    {
        public void test()
        {
            string text = getText();
        }

        public static void staticTest()
        {
            string text = getText();
            Console.WriteLine(text);
        }

        public static string getText()
        {
            return "This is a text";
        }
    }

    [TestFixture]
    public class Test_AssemblyBuilder
    {
        private readonly string currentAssembly = DI.config.ExecutingAssembly;

        //private readonly string nameOfTypeToExtract = "Test_AssemblyBuilder";
        private readonly List<string> typesToExtract = new List<string>(new[] {"testClass"}); //, "O2Trace" });        

        [Test]
        [Ignore("This test creates tons of errors due to the way the class CLONNING is done")]
        public void test_BatchDllCreationUsingExtractedTypes()
        {
            string folderWithCecilDlls =
                Files.checkIfDirectoryExistsAndCreateIfNot(
                    Path.Combine(DI.config.O2TempDir, "CecilDLls"), true);
            string folderWithReflectionDlls =
                Files.checkIfDirectoryExistsAndCreateIfNot( 
                    Path.Combine(DI.config.O2TempDir, "ReflectionDlls"), true);


            // create assemblies with extracted types
            var newAssemblies = new List<string>();
            List<TypeDefinition> cecilTypes = CecilUtils.getTypes(currentAssembly);
            foreach (TypeDefinition typeToExtract in cecilTypes)
            {
                string newAssembly = CecilUtils.CreateAssemblyFromType(typeToExtract, folderWithCecilDlls);
                Assert.That(File.Exists(newAssembly), "newAssembly file was not created");
                newAssemblies.Add(newAssembly);
                //  DI.log.info("New assembly created with extracted type: {0}", newAssembly);
                //break;
            }

            // see if they can be loaded using reflection
            foreach (string assemblyFile in newAssemblies)
            {
                if (DI.reflection.loadAssemblyAndCheckIfAllTypesCanBeLoaded(assemblyFile))
                {
                     DI.log.info("Loaded OK: {0}", assemblyFile);
                    Files.Copy(assemblyFile, folderWithReflectionDlls);
                }
                else
                {
                     DI.log.error("ERROR Loading {0}", assemblyFile);
                }
            }
        }

        [Test]
        public void test_CloningAssembly()
        {
            AssemblyDefinition sourceAssembly = CecilUtils.getAssembly(currentAssembly);
            Assert.IsNotNull(sourceAssembly, "sourceAsembly was null");
            var newAssembly = new CecilAssemblyBuilder("Cloned Assembly");
            //  foreach (var type in CecilUtils.getTypes(sourceAssembly))
            //      newAssembly.addType(type);
            string file = newAssembly.Save(DI.config.O2TempDir);
            Assert.IsTrue(File.Exists(file), "file didn't exist");
        }


        [Test]
        public void test_ExtractMethodFromAssembly()
        {
            AssemblyDefinition sourceCecilAssembly = CecilUtils.getAssembly(Assembly.GetExecutingAssembly().Location);//DI.config.hardCodedPathToO2UnitTestsDll);
            Assert.IsNotNull(sourceCecilAssembly, "sourceAsembly was null");

            // create assembly using Cecil
            var cecilNewAssembly = new CecilAssemblyBuilder("ExtractedType");
            cecilNewAssembly.addDummyType();
            // todo: checkout why It looks like I need this in order for the type cloning to work 

            // extract types from loaded CecilAssembly and add them to the new CecilAssembly
            foreach (string typeToExtract in typesToExtract)
            {
                TypeDefinition cecilTypeToExtract = CecilUtils.getType(sourceCecilAssembly, typeToExtract);
                Assert.IsNotNull(cecilTypeToExtract, "typeToExtract was null");
                cecilNewAssembly.addType(cecilTypeToExtract);
            }
            // save cecilNewAssembly
            string cecilAssemblyFile = cecilNewAssembly.Save(DI.config.O2TempDir);
            Assert.IsTrue(File.Exists(cecilAssemblyFile), "file didn't exist");

            // create assembly try to open it using Reflector

            Assembly reflectionAssembly = DI.reflection.getAssembly(cecilAssemblyFile);
            Assert.IsNotNull(reflectionAssembly, "assembly was null");
            List<Type> reflectionTypes = DI.reflection.getTypes(reflectionAssembly);
            Assert.IsNotNull(reflectionTypes, "reflectionTypes was null");
            Assert.That(reflectionTypes.Count() > 0, "reflectionTypes.Count == 0");
        }
    }
}