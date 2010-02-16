// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using O2.Core.FileViewers.Ascx;
using O2.Core.FileViewers.Ascx.tests;
using O2.Core.FileViewers.Struts_1_5;
using O2.DotNetWrappers.Windows;
using O2.External.WinFormsUI.Forms;
using O2.ImportExport.OunceLabs;
using O2.Interfaces.Views;
using O2.Views.ASCX.O2Findings;

namespace O2.Core.FileViewers._UnitTests
{
    [TestFixture]
    public class _Test_Struts_Mappings
    {
        private static readonly string web_xml = Path.Combine(DI.config.O2TempDir, "Ibatis_JPetstore_web.xml");
        private static readonly string validation_xml = Path.Combine(DI.config.O2TempDir, "Ibatis_JPetstore_validation.xml");
        private static readonly string struts_config_xml = Path.Combine(DI.config.O2TempDir, "Ibatis_JPetstore_struts-config.xml");        
        

        [SetUp]
        public void ensureTestFilesAreAvailable()
        {            
            Assert.That(Files.createFile_IfItDoesntExist(web_xml, TestFiles.Ibatis_JPetstore_web), "web_xml file didn't exist: " + web_xml);
            Assert.That(Files.createFile_IfItDoesntExist(validation_xml, TestFiles.Ibatis_JPetstore_validation), "validation_xml file didn't exist: " + validation_xml);
            Assert.That(Files.createFile_IfItDoesntExist(struts_config_xml, TestFiles.Ibatis_JPetstore_struts_config), "struts_config_xml file didn't exist: " + struts_config_xml);

            OunceAvailableEngines.addAvailableEnginesToControl(typeof(ascx_FindingsViewer));
        }

        [Test]
        public void testIndividualViewers()
        {            

               // view webXml mappings 
            var webXmlControl = (ascx_J2EE_web_xml)O2AscxGUI.openAscx(typeof(ascx_J2EE_web_xml));            
            Assert.That(webXmlControl != null, "webXmlControl was null");
            webXmlControl.mapFile(web_xml);
             
            
            // view strutsConfig mappings
            var strusConfigControl = (ascx_Struts_config_xml)O2AscxGUI.openAscx(typeof(ascx_Struts_config_xml));
            Assert.That(strusConfigControl != null, "strusConfigControl was null");
            strusConfigControl.mapFile(struts_config_xml);

            // view validation mappings
            var validationControl = (ascx_Validation_xml)O2AscxGUI.openAscx(typeof(ascx_Validation_xml));
            Assert.That(validationControl != null, "validationControl was null");
            validationControl.mapFile(validation_xml);

            // view validation mappings
            var tilesDefinitions = (ascx_TilesDefinition_xml)O2AscxGUI.openAscx(typeof(ascx_TilesDefinition_xml));
            Assert.That(validationControl != null, "ascx_TilesDefinition_xml was null");
                        
            O2AscxGUI.waitForAscxGuiClose();
            O2AscxGUI.close();
        }

        [Test]
        public void testStrutsMapping()
        {

            O2AscxGUI.launch("test Struts Mappings");
            var strutsMappingsControl = (ascx_StrutsMappings)O2AscxGUI.openAscx(typeof(ascx_StrutsMappings), O2DockState.DockLeft, "Struts Mappings");
            Assert.That(strutsMappingsControl != null, "strutsMappingsControl was null");
            var strutsMappings = StrutsMappingsHelpers.calculateStrutsMapping(web_xml, struts_config_xml, "", "");
            Assert.That(strutsMappings != null, "strutsMappings was null");            
            strutsMappingsControl.showStrutsMappings(web_xml, struts_config_xml, "", "");
            
            O2AscxGUI.waitForAscxGuiClose();
            O2AscxGUI.close();
        }

        [Test]
        public void testStrutsMapping_ManualTest()
        {

            O2AscxGUI.launch("test Struts Mappings - Manual Test");
            var strutsMappingsControl =
                (ascx_StrutsMappings_ManualMapping)
                O2AscxGUI.openAscx(typeof (ascx_StrutsMappings_ManualMapping), O2DockState.Document, "Struts Mappings");

            O2AscxGUI.waitForAscxGuiClose();
            O2AscxGUI.close();
        }
    }
}
