using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using O2.Cmd.SpringMvc.Ascx;
using O2.Cmd.SpringMvc.Classes;
using O2.Cmd.SpringMvc.Objects;
using O2.Cmd.SpringMvc.PythonScripts;
using O2.DotNetWrappers.DotNet;
using O2.External.WinFormsUI.Forms;
using O2.DotNetWrappers.Windows;

namespace O2.Cmd.SpringMvc._UnitTests
{
    [TestFixture]
    public class _test_LoadSpringMvcData
    {
        string testSpringMvcClassFile = Path.GetFullPath(@"_UnitTests\TestFiles\EditOwnerForm.class");

        const string expectedClassName = "org.springframework.samples.petclinic.web.EditOwnerForm";

        string expectedSpringMvcAttributeXmlFile = Path.Combine(AnnotationsHelper.tempFolderForAnnotationsXmlFiles,
                                                   expectedClassName + ".JavaAttributes.xml");

        
        [Test]
        public void convertClassFileInto()
        {
            // check that target class and Annotation.py exists
            Assert.That(File.Exists(testSpringMvcClassFile), "could not find test class to for test: {0}", testSpringMvcClassFile);
            Assert.That(File.Exists(AnnotationsHelper.jythonAnnotationScript), "could not find Annotations.py file {0}", AnnotationsHelper.jythonAnnotationScript);

            // exsure expectedSpringMvcAttributeXmlFile is not there
            if (File.Exists(expectedSpringMvcAttributeXmlFile))
                File.Delete(expectedSpringMvcAttributeXmlFile);            
            Assert.That(false == File.Exists(expectedSpringMvcAttributeXmlFile), "(at this stage) expected file should not exist: {0}", expectedSpringMvcAttributeXmlFile);            

            // convert class file
            var executionResult = AnnotationsHelper.executeJythonScript(testSpringMvcClassFile);
            DI.log.debug("Execution Result: {0}", executionResult);
            
            Assert.That(File.Exists(expectedSpringMvcAttributeXmlFile), "expected file did not exist: {0}", expectedSpringMvcAttributeXmlFile);            
        }

        [Test]
        public void createSpringMvcControllerFromXmlAttributeFile()
        {
            Assert.That(File.Exists(expectedSpringMvcAttributeXmlFile), "expected expectedSpringMvcAttributeXmlFile does not exist: {0}", expectedSpringMvcAttributeXmlFile);
            var springMvcControllers = LoadSpringMvcData.createSpringMvcControllersFromXmlAttributeFile(expectedSpringMvcAttributeXmlFile);
            Assert.That(springMvcControllers.Count > 1, "there should be one springMvcController returned");
            var springMvcController = springMvcControllers[0];
            Assert.That(springMvcController.JavaClass == expectedClassName, "springMvcController.ClassName did not match expectedClassName: {0}", expectedClassName);
            
        }

        [Test]  // the errors that will appear in the logs are because we are not loading the CIR data file, for example "getTreeNodeWithAutoWiredObject, loaded cirData did not contained signature :org.springframework.samples.petclinic.web.EditOwnerForm.setupForm(int;org.springframework.ui.Model):java.lang.String"
        public void viewSpringMvcControllersObjectOnGui()
        {
            if (false == File.Exists(expectedSpringMvcAttributeXmlFile))
                AnnotationsHelper.executeJythonScript(testSpringMvcClassFile);
            // create controllers
            var springMvcControllers = LoadSpringMvcData.createSpringMvcControllersFromXmlAttributeFile(expectedSpringMvcAttributeXmlFile);
            // create Gui
            var viewSpringMvcControllerName = "ascx_CreateSpringMvcMappings";
            O2AscxGUI.openAscxAsForm(typeof(ascx_SpringMvcMappings), viewSpringMvcControllerName);
            var springMvcMappings = (ascx_SpringMvcMappings)O2AscxGUI.getAscx(viewSpringMvcControllerName);
            // load controllers on gui
            springMvcMappings.showSpringMvcControllers(springMvcControllers);            
            O2AscxGUI.close();
            //viewSpringMvcController
        }

        // this test contains internal hard-coded references to the current O2 Dev environment (ignore so that we have to explicitly run this)
        [Test,Ignore]
        public void createCSharpFileFromXsd()
        {
            var o2DevEnvironment = @"E:\O2\_SourceCode_O2\";
            var xsdFile = o2DevEnvironment + @"O2Core\O2_Core_CIR\Xsd\JavaAttributeMappings.xsd";
            var expectedCSharpFile = o2DevEnvironment + @"\O2Core\O2_Core_CIR\Xsd\JavaAttributeMappings.cs";

            Files.deleteFile(expectedCSharpFile);
            Assert.That(File.Exists(expectedCSharpFile) == false, "expectedCSharpFile should not exist");
            var xsdCreationProcess = DevUtils.createCSharpFileFromXsd(xsdFile, "O2.Cmd.SpringMvc.Xsd    ");
            Assert.That(xsdCreationProcess != null, "xsdCreationProcess was null");
            xsdCreationProcess.WaitForExit();
            Assert.That(File.Exists(expectedCSharpFile), "expectedCSharpFile should exist");
        }
    }
}
