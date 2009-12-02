// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using O2.DotNetWrappers.Windows;
using O2.Kernel.Interfaces.Controllers;


namespace O2.Scanner.OunceLabsCLI.ScanTargets
{
    public class ScanTarget_Paf : IScanTarget
    {
        private String application;
        private String workDirectory;

        #region IScanTarget Members

        public string Target
        {
            get { return application; }
            set
            {
                application = value;
                if (useFileNameOnWorkDirecory)
                    workDirectory = Path.Combine(workDirectory, Path.GetFileNameWithoutExtension(application));
                Files.checkIfDirectoryExistsAndCreateIfNot(workDirectory);
                if (workDirectory == null || Directory.Exists(workDirectory) == false)
                    workDirectory =
                        Files.checkIfDirectoryExistsAndCreateIfNot(Path.Combine(DI.config.O2TempDir,
                                                                                Path.GetFileNameWithoutExtension(
                                                                                    application)));
            }
        }

        public string WorkDirectory
        {
            get { return workDirectory; }
            set { workDirectory = value; }
        }

        public string ApplicationFile
        {
            get { return application; }
            set { application = value; }
        }

        public bool useFileNameOnWorkDirecory { get; set; }

        public List<string> missingDependencies { get; set; }
     
        public override string ToString()
        {
            return Path.GetFileNameWithoutExtension(application);
        }

        #endregion
    }
}
