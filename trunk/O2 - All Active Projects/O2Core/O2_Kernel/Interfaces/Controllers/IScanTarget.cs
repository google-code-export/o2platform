using System.Collections.Generic;

namespace O2.Kernel.Interfaces.Controllers
{
    public interface IScanTarget
    {
        string Target { get; set; }
        string WorkDirectory { get; set; }
        string ApplicationFile { get; set; }
        bool useFileNameOnWorkDirecory { get; set; }
        List<string> missingDependencies { get; set; }
        string ToString();
        /*public abstract void setTarget(String sFile);
        public abstract void setTarget(String sFile, String sWorkDirectory, bool bUseFileNameOnWorkDirecory);
        public abstract string getTarget();
        public abstract string getApplicationFile();
        public abstract string getWorkDirectory();
        public abstract string setWorkDirectory(String sWorkDirectory, bool bUseFileNameOnWorkDirecory);
        public abstract bool areThereMissingDependencies();
        public abstract List<String> getMissingDependencies();*/
    }
}