using System;
using System.IO;
using NUnit.Framework;
using O2.Kernel.Interfaces;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.DotNet;

namespace O2.Core.FileViewers._UnitTests
{
    [TestFixture]
    public class CreateCSharpXSDs
    {
        public static string workDirectory = @"E:\O2\_SourceCode_O2\O2Core\O2_Core_FileViewers";
        //public static IO2Log log = O2.Kernel.PublicDI.log;
                               
        /*public static string workDirectory = @"F:\Java_Apps\struts\struts-1.3.10\O2_Scripts\";
        public static string struts_config_XSD = workDirectory + @"struts-config.xsd";
        public static string struts_config_CSFile = workDirectory  + @"struts-config.cs";
        public static string validation_XSD = workDirectory + @"validation.xsd";
        public static string validation_CSFile = workDirectory + @"validation.cs";
                 
                   
        public static void createCSharpFileForXSDs()                
        {
            createCSharpFileForXSD(struts_config_XSD,struts_config_CSFile,  "O2.PoC.Struts.Xsd");
            createCSharpFileForXSD(validation_XSD,validation_CSFile,  "O2.PoC.Struts.Xsd");
        }
		*/
        //

        [Test, Ignore]
        public void createXSharpFor_TitlesDefinition()
        {
            var xsdFile = Path.Combine(workDirectory, @"XSD\tiles-definition.xsd");
            var expectedCSFile = Path.Combine(workDirectory, @"XSD\tiles-definition.cs");
            Assert.That(File.Exists(xsdFile), "xsdFile didn't exist:" + xsdFile);
            createCSharpFileForXSD(xsdFile, expectedCSFile, "O2.Core.FileViewers.XSD");
            Assert.That(File.Exists(expectedCSFile), "xsdFile didn't exist:" + expectedCSFile);
        }

        [Test, Ignore]
        public void createXSharpFor_ValidatorXml()
        {
            var xsdFile = Path.Combine(workDirectory, @"XSD\validation.xsd");
            var expectedCSFile = Path.Combine(workDirectory, @"XSD\validation.cs");
            Assert.That(File.Exists(xsdFile), "xsdFile didn't exist:" + xsdFile);
            createCSharpFileForXSD(xsdFile, expectedCSFile, "O2.Core.FileViewers.XSD");
            Assert.That(File.Exists(expectedCSFile), "xsdFile didn't exist:" + expectedCSFile);
        }

        [Test, Ignore]
        public void createXSharpFor_StrutsConfig()
        {
            var xsdFile = Path.Combine(workDirectory, @"XSD\struts-config.xsd");
            var expectedCSFile = Path.Combine(workDirectory, @"XSD\struts-config.cs");
            Assert.That(File.Exists(xsdFile), "xsdFile didn't exist:" + xsdFile);
            createCSharpFileForXSD(xsdFile, expectedCSFile, "O2.Core.FileViewers.XSD");
            Assert.That(File.Exists(expectedCSFile), "xsdFile didn't exist:" + expectedCSFile);

        }

        [Test,Ignore]
        public void createXSharpForJ2EE_web_xml()
        {
            var xsdFile = Path.Combine(workDirectory, @"XSD\web-app_2_4.xsd");
            var expectedCSFile = Path.Combine(workDirectory, @"XSD\web-app_2_4.cs");            
            Assert.That(File.Exists(xsdFile), "xsdFile didn't exist:" + xsdFile);
            createCSharpFileForXSD(xsdFile, expectedCSFile, "O2.Core.FileViewers.J2EE");
            Assert.That(File.Exists(expectedCSFile), "xsdFile didn't exist:" + expectedCSFile);            
            
        }

        public static void createCSharpFileForXSD(string xsdFile, string espectedCSharpFile, string targetNamespace)
        {            
            Files.deleteFile(espectedCSharpFile);            
            var xsdCreationProcess = DevUtils.createCSharpFileFromXsd(xsdFile, targetNamespace);            
            xsdCreationProcess.WaitForExit();
            Assert.That(File.Exists(espectedCSharpFile));
            DI.log.info("CSharp file created");            
        }   
    }
}