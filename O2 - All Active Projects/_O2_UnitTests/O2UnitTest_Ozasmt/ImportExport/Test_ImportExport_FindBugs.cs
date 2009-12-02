// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using O2.DotNetWrappers.O2Misc;
using O2.External.WinFormsUI.Forms;
using O2.ImportExport.Misc.CodeCrawler;
using O2.ImportExport.Misc.FindBugs;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.UnitTests.Test_Ozasmt._SampleScans;
using O2.Views.ASCX.O2Findings;

namespace O2.UnitTests.Test_Ozasmt.ImportExport
{
    [TestFixture]
    public class Test_ImportExport_FindBugs
    {
        readonly string findBugsExample = SampleScripts.getFileWithSampleScript(typeof(SampleScans), "FindBugs_WebGoat", ".xml");
        [Test]
        public void loadCodeFile_FindBugs()
        {
            Assert.That(File.Exists(findBugsExample), "File Didn't exist");
            var loadEngine = new O2AssesmentLoad_FindBugs();
            Assert.That(loadEngine.canLoadFile(findBugsExample), "load engine could not load file");
            var o2Assessment = loadEngine.loadFile(findBugsExample);
            Assert.That(o2Assessment != null, "o2Assessment was null");
            Assert.That(o2Assessment.o2Findings.Count >0 ,"There were no findings imported");
            var savedFile = o2Assessment.save(new O2AssessmentSave_OunceV6());
            Assert.That(File.Exists(savedFile), "savedFile didnt exist:" + savedFile);
            DI.log.info("Imported file saved to :{0}", savedFile);

            ascx_FindingsViewer.o2AssessmentLoadEngines.Add(new O2AssessmentLoad_OunceV6());
            var findingsViewerControlName = "FindingsViewer";
            O2AscxGUI.openAscxAsForm(typeof(ascx_FindingsViewer), findingsViewerControlName);
            var findingsViewerControl = (ascx_FindingsViewer) O2AscxGUI.getAscx(findingsViewerControlName);
            findingsViewerControl.loadO2Assessment(savedFile);
            //O2AscxGUI.waitForAscxGuiClose();
            O2AscxGUI.close();
        }
    }
 
}
