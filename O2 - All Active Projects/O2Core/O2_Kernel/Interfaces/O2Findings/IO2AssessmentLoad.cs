using O2.Kernel.Interfaces.O2Findings;

namespace O2.Kernel.Interfaces.O2Findings
{
    public interface IO2AssessmentLoad
    {
        string engineName { get; set; }
        bool canLoadFile(string fileToTryToLoad);
        IO2Assessment loadFile(string fileToLoad);
        bool importFile(string fileToLoad, IO2Assessment o2Assessment);
    }
}