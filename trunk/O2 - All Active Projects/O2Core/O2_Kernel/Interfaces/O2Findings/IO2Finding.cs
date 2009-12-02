using System;
using System.Collections.Generic;

namespace O2.Kernel.Interfaces.O2Findings
{
    public interface IO2Finding
    {
        uint actionObject { get; set; }
        string callerName { get; set; }
        uint columnNumber { get; set; }
        byte confidence { get; set; }
        string context { get; set; }
        bool exclude { get; set; }
        String file { get; set; }
        uint lineNumber { get; set; }
        string method { get; set; }
        List<IO2Trace> o2Traces { get; set; }
        uint ordinal { get; set; }
        string projectName { get; set; }
        string propertyIds { get; set; }
        uint recordId { get; set; }
        byte severity { get; set; }
        List<String> text { get; set; }
        string vulnName { get; set; }
        string vulnType { get; set; }
        string ToString();
    }
}