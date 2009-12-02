// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.IO;
using NUnit.Framework;
using O2.Core.FileViewers.Ascx;
using O2.Core.FileViewers.XSD;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.DotNet;
using O2.External.WinFormsUI.Forms;

namespace O2.Core.FileViewers._UnitTests
{
    [TestFixture]
    public class _Test_Struts_Config_Xml
    {
        private static readonly string Spring_JPetStore_struts_config_xml = Path.Combine(DI.config.O2TempDir, "Spring_JPetStore_struts-config.xml");
        private static readonly string Struts_Sample_Validator = Path.Combine(DI.config.O2TempDir, "struts_sample_validation.xml");        

        [SetUp]
        public void setUp()
        {
            if (false == File.Exists(Spring_JPetStore_struts_config_xml))
                Files.WriteFileContent(Spring_JPetStore_struts_config_xml, TestFiles.Spring_JPetStore_struts_config);
            Assert.That(File.Exists(Spring_JPetStore_struts_config_xml), "Spring_JPetStore_struts_config_xml file didn't exist: " + Spring_JPetStore_struts_config_xml);

            if (false == File.Exists(Struts_Sample_Validator))
               Files.WriteFileContent(Struts_Sample_Validator, TestFiles.struts_sample_validation);
            Assert.That(File.Exists(Struts_Sample_Validator), "struts_sample_validation.xml file didn't exist: " + Spring_JPetStore_struts_config_xml);            
        }

        [Test]
        public void loadStrutsConfigXml()
        {
            var strutsConfig = Serialize.getDeSerializedObjectFromXmlFile(Spring_JPetStore_struts_config_xml, typeof(strutsconfig));
            Assert.That(strutsConfig != null, "strutsConfig was null");
            O2AscxGUI.openAscxAsForm(typeof(ascx_Struts_config_xml));
            var ascxControl = (ascx_Struts_config_xml)O2AscxGUI.getAscx("ascx_Struts_config_xml");
            ascxControl.invokeOnThread(
                () => ascxControl.mapFile(Spring_JPetStore_struts_config_xml));
            O2AscxGUI.waitForAscxGuiClose();
        }


        [Test]
        public void loadStrutsValidator()
        {
            var strutsConfig = Serialize.getDeSerializedObjectFromXmlFile(Struts_Sample_Validator, typeof(formvalidation));
            Assert.That(strutsConfig != null, "validator.xml was null");            
            O2AscxGUI.openAscxAsForm(typeof(ascx_Validation_xml));
            var ascxControl = (ascx_Validation_xml)O2AscxGUI.getAscx("ascx_Validation_xml");
            ascxControl.invokeOnThread(() => ascxControl.mapFile(Struts_Sample_Validator));
            O2AscxGUI.waitForAscxGuiClose();
        }

        [Test]
        public void loadTilesDefinition()
        {
            //var strutsConfig = Serialize.getDeSerializedObjectFromXmlFile(Struts_Sample_Validator, typeof(formvalidation));
            //Assert.That(strutsConfig != null, "validator.xml was null");
            O2AscxGUI.openAscxAsForm(typeof(ascx_TilesDefinition_xml));
            var ascxControl = (ascx_TilesDefinition_xml)O2AscxGUI.getAscx("ascx_TilesDefinition_xml");
            //ascxControl.invokeOnThread(() => ascxControl.mapFile(Struts_Sample_Validator));
            O2AscxGUI.waitForAscxGuiClose();
        }

        [Test]
        public void loadShowStrutsConfigFiles()
        {
            //var strutsConfig = Serialize.getDeSerializedObjectFromXmlFile(Struts_Sample_Validator, typeof(formvalidation));
            //Assert.That(strutsConfig != null, "validator.xml was null");
            O2AscxGUI.openAscxAsForm(typeof(ascx_ShowStrutsConfigFiles));
            var ascxControl = (ascx_ShowStrutsConfigFiles)O2AscxGUI.getAscx("ascx_ShowStrutsConfigFiles");
            //ascxControl.invokeOnThread(() => ascxControl.mapFile(Struts_Sample_Validator));
            O2AscxGUI.waitForAscxGuiClose();
        }

    }
}
