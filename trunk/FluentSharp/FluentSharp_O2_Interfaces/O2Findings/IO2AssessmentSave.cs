// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Collections.Generic;

namespace FluentSharp.O2.Interfaces.O2Findings
{
    public interface IO2AssessmentSave
    {
        string engineName { get; set; }

        string save(List<IO2Finding> o2Findings);
        bool save(List<IO2Finding> o2Findings, string sPathToSaveAssessment);        
        bool save(string assessmentName, IEnumerable<IO2Finding> o2Findings, string sPathToSaveAssessment);
    }
}