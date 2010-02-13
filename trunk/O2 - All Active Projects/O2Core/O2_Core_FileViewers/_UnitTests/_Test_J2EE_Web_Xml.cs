// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using O2.Core.FileViewers.Ascx;
using O2.Core.FileViewers.J2EE;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.DotNet;
using O2.External.WinFormsUI.Forms;

namespace O2.Core.FileViewers._UnitTests
{
    [TestFixture]
    public class _Test_J2EE_Web_Xml
    {
        private static readonly string SpringMVC_PetClinic_web_xml = Path.Combine(DI.config.O2TempDir, "SpringMVC_PetClinic_web.xml");        

        [SetUp]
        public void setUp()
        {  
            if (false == File.Exists(SpringMVC_PetClinic_web_xml))
                Files.WriteFileContent(SpringMVC_PetClinic_web_xml, TestFiles.SpringMVC_PetClinic_web);            
            Assert.That(File.Exists(SpringMVC_PetClinic_web_xml), "SpringMVC_PetClinic_web_xml file didn't exist: " + SpringMVC_PetClinic_web_xml);            
        }

        [Test]
        public void loadWebXmlInGui()
        {
            var webXml = Serialize.getDeSerializedObjectFromXmlFile(SpringMVC_PetClinic_web_xml, typeof (webappType));
            Assert.That(webXml != null, "webXml was null");
            O2AscxGUI.openAscxAsForm(typeof(ascx_J2EE_web_xml));
            var ascxControl = (ascx_J2EE_web_xml) O2AscxGUI.getAscx("ascx_J2EE_web_xml");
            ascxControl.invokeOnThread(
                ()=> ascxControl.mapFile(SpringMVC_PetClinic_web_xml));
            O2AscxGUI.waitForAscxGuiClose();
        }
    }
}
