using System;
using System.Collections.Generic;
using O2.Kernel.Interfaces.O2Findings;

namespace O2.Kernel.Interfaces.O2Findings
{
    public interface IO2Assessment
    {                
        List<IO2Finding> o2Findings { get; set; }
        string name { get; set; }

        string save(IO2AssessmentSave o2AssessmentSave);
        bool save(IO2AssessmentSave o2AssessmentSave, string sPathToSaveAssessment);
        bool saveAsO2Format(string targetFile);
    }
}