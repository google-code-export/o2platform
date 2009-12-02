// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using NUnit.Framework;
using O2.Cmd.SpringMvc.PythonScripts;
using O2.Cmd.SpringMvc.Xsd;
using O2.DotNetWrappers.DotNet;

namespace O2.Cmd.SpringMvc._UnitTests
{
    [TestFixture]
    public class _Test_LoadingDeSerializedJavaAttributesXmlFile
    {
        readonly string testSpringMvcClassFile = Path.GetFullPath(@"_UnitTests\TestFiles\EditOwnerForm.class");
        const string expectedClassName = "org.springframework.samples.petclinic.web.EditOwnerForm";
        readonly string expectedSpringMvcAttributeXmlFile = Path.Combine(AnnotationsHelper.tempFolderForAnnotationsXmlFiles,
                                                   expectedClassName + ".JavaAttributes.xml");
        [Test]
        public void checkJavaAttributesDeserialization()
        {
            if (File.Exists(expectedSpringMvcAttributeXmlFile) == false)
            {
                File.Delete(expectedSpringMvcAttributeXmlFile);
                AnnotationsHelper.executeJythonScript(testSpringMvcClassFile);
            }
            Assert.That(File.Exists(expectedSpringMvcAttributeXmlFile), "expectedSpringMvcAttributeXmlFile file did not exist: {0}", expectedSpringMvcAttributeXmlFile);
            var javaAttributeMappings = Serialize.getDeSerializedObjectFromXmlFile(expectedSpringMvcAttributeXmlFile, typeof(JavaAttributeMappings));
            Assert.That(javaAttributeMappings != null, "javaAttributeMappings was null");
            Assert.That(checkThatDeSerializedObjectMatchesXmlFile(expectedSpringMvcAttributeXmlFile, javaAttributeMappings), "checkThatDeSerializedObjectMatchesXmlFile failed");
            DI.log.info("All ok");
        }


        // when completed more to O2.DotNetWrappers.DotNet.Serialize
        public static bool checkThatDeSerializedObjectMatchesXmlFile(string xmlFile, object objectToCheck)
        {
            try
            {
                // ReSharper disable PossibleNullReferenceException            
                var xDocument = XDocument.Load(xmlFile);
                var objectType = objectToCheck.GetType();
                // check that the root element name of the xml file matches the type name of the current object

                if (objectType.Name != xDocument.Root.Name)
                {
                    DI.log.error("in checkThatDeSerializedObjectMatchesXmlFile: RootNames didn't match: {0} != {1}",
                        objectType.Name, xDocument.Root.Name);
                    return false;
                }
                if (false == matchXmlElementAttributeWithObjectProperties(xDocument.Root, objectToCheck))
                    return false;
                if (false == matchXmlElementChildElementsWithObject(xDocument.Root, objectToCheck))
                    return false;
                // ReSharper restore PossibleNullReferenceException
                return true;
            }
            catch (Exception ex)
            {
                DI.log.ex(ex);
            }
            return false;
        }

        private static bool matchXmlElementChildElementsWithObject(XElement xElement, object objectToCheck)
        {
            if (objectToCheck.GetType().IsArray)
                // this is not the most optimized version of doing this (the problem is matching the lists of elements in XML with the Arrays in CSharp
                foreach (var objectToCheckArrayItem in (IEnumerable)objectToCheck)                      
                    matchXmlElementChildElementsWithObject(xElement, objectToCheckArrayItem);                      
            else
                foreach (var childElement in xElement.Elements())
                {
                    var childElementName = childElement.Name.LocalName;

                    // we have to check this manually since the value of the property could be null 
                    var foundProperty = false;
                    foreach (var property in DI.reflection.getProperties(objectToCheck.GetType()))
                        if (property.Name == childElementName)
                        {
                            foundProperty = true;
                            break;
                        }
                    //if (elementObject == null)
                    if (foundProperty == false)
                    {
                        DI.log.error("in matchXmlElementChildElementsWithObject, could not find child element: {0}",
                                     childElementName);
                        return false;
                    }
                    // now get the live object and if it is not null continue the checks
                    var elementObject = DI.reflection.getProperty(childElementName, objectToCheck);
                    if (elementObject != null)
                        matchXmlElementChildElementsWithObject(childElement, elementObject);
                    //DI.log.info("{0}", childElementName);
                }
            return true;
        }

        private static bool matchXmlElementAttributeWithObjectProperties(XElement xElement, object objectToCheck)
        {
            foreach(var attribute in xElement.Attributes())
                DI.log.info("{0}", attribute.Name);
            return true;
        }
    }
}
