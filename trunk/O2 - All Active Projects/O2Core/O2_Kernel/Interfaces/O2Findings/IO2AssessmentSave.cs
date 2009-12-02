using System.Collections.Generic;
using O2.Kernel.Interfaces.O2Findings;

namespace O2.Kernel.Interfaces.O2Findings
{
    public interface IO2AssessmentSave
    {
        string engineName { get; set; }

        string save(List<IO2Finding> o2Findings);
        bool save(List<IO2Finding> o2Findings, string sPathToSaveAssessment);        
        bool save(string assessmentName, IEnumerable<IO2Finding> o2Findings, string sPathToSaveAssessment);
    }
}