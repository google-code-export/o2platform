// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using NUnit.Framework;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Kernel.InterfacesBaseImpl;
using System.IO;
using O2.Kernel.Objects;

namespace O2.UnitTests.Test_DotNetWrappers.Test_DotNet
{
    [TestFixture]
    public class Test_XmlHelpers
    {
        [Test]
        public void test_getRootElementText()
        {
            // create some temp Xml Files
            var stringtSerializedXmlFile = Serialize.createSerializedXmlFileFromObject(new String(new[] {'a'}));
            var o2ProxySerializedXmlFile = Serialize.createSerializedXmlFileFromObject(new O2Proxy());
            Assert.That(File.Exists(stringtSerializedXmlFile), "stringSerializedXmlFile didn't exist");
            Assert.That(File.Exists(o2ProxySerializedXmlFile), "o2ProxySerializedXmlFile didn't exist");
            test_getRootElementTextOnFile(o2ProxySerializedXmlFile);
            test_getRootElementTextOnFile(stringtSerializedXmlFile);      
        }

        public void test_getRootElementTextOnFile(string fileToTest)
        {
            var rootElement = XmlHelpers.getRootElementText(fileToTest);
            Assert.That(rootElement != "", "XmlHelpers.getRootElementText returned \"\" for : " + fileToTest);
            DI.log.info("Root element for {0} : {1}", Path.GetFileName(fileToTest), rootElement);
        }
    }    
}
