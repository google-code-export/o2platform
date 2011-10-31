// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.O2Findings;
using O2.Interfaces.O2Findings;
//O2File:OzasmUtils_OunceV7_0.cs

namespace O2.XRules.ThirdPary.IBM
{
    public class O2AssessmentLoad_OunceV7_0 : IO2AssessmentLoad
    {
        public string engineName { get; set; }

        public O2AssessmentLoad_OunceV7_0()
        {
            engineName = "O2AssessmentLoad_OunceV7_0";
        }

        public bool canLoadFile(string fileToTryToLoad)
        {
            var expectedRootElementRegEx7x = "<AssessmentRun.*name.*version=\"7.0.0\">";
            var expectedRootElementRegEx8x =  "<AssessmentRun.*version=\"8.0.0.*\">" ;
            
            string rootElementText = XmlHelpers.getRootElementText(fileToTryToLoad);
            if (RegEx.findStringInString(rootElementText, expectedRootElementRegEx7x) ||
            	RegEx.findStringInString(rootElementText, expectedRootElementRegEx8x))
                return true;
            "in {0} engine, could not load file {1} since the root element value didnt match the Regex: {2}!={3}".error(
                         engineName, fileToTryToLoad, rootElementText, expectedRootElementRegEx7x);
            return false;
        }

        public IO2Assessment loadFile(string fileToLoad)
        {
            var o2Assessment = new O2Assessment();
            if (importFile(fileToLoad, o2Assessment))
                return o2Assessment;
            return null;
        }

        public bool importFile(string fileToLoad, IO2Assessment o2Assessment)
        {
            if (canLoadFile(fileToLoad))
                if (OzasmtUtils_OunceV7_0.importOzasmtAssessmentIntoO2Assessment(fileToLoad, o2Assessment))
                    return true;
            return false;
        }
    }
}
