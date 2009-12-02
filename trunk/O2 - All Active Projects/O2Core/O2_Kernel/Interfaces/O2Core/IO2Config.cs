// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections;
using System.Collections.Generic;

namespace O2.Kernel.Interfaces.O2Core
{
    public interface IO2Config
    {
        string hardCodedO2LocalTempFolder { get; set; }
        string hardCodedO2LocalBuildDir { get; set; }
        string hardCodedO2LocalSourceCodeDir { get; set; }
        
        string O2TempDir { get; set; }
        string O2ConfigFile { get; set; }
        string CurrentExecutableDirectory { get; }
        string CurrentExecutableFileName { get; }
        string ExecutingAssembly { get; }
        string TempFileNameInTempDirectory { get; }
        string TempFolderInTempDirectory { get; }
        string O2KernelAssemblyName { get; }
        //String setDefaultDir_TempFolder();

        string getTempFileInTempDirectory(string extension);
        string getTempFolderInTempDirectory(string stringToAddToTempDirectoryName);

        // DI helper
        bool setDI(Type typeToInjectDependency, string propertyToInject, Object dependencyObject);
        bool setDI(string assemblyName, string typeToInjectDependency, string propertyToInject, Object dependencyObject);

        // need to see if this is needed
        void addPathToCurrentExecutableEnvironmentPathVariable(String sPathToAdd);

        // misc global vars
        string O2FindingsFileExtension { get; set;}         
    }
    
}
