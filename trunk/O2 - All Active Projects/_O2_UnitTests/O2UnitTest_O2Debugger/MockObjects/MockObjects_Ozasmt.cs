// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
//using O2.Core.Ozasmt.Ozasmt_OunceV6;
using O2.DotNetWrappers.O2Findings;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Interfaces.O2Findings;

namespace O2.UnitTests.Test_O2Debugger.MockObjects
{
    public class MockObjects_Ozasmt
    {
        // this should  
        public static string getOzasmtXmlFile()
        {
            return getO2Assessment().save(new O2AssessmentSave_OunceV6());
        }

        // todo: add code to create bigger ozasmt objects (with tons of traces for example)
        public static IO2Assessment getO2Assessment()
        {
            return new O2Assessment();
        }
    }
}
