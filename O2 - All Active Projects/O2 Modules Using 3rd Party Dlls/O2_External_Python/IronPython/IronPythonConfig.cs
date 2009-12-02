// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using O2.External.Python;
using O2.External.Python.IronPython;

namespace O2.External.Python.IronPython
{
    public class IronPythonConfig
    {
        public static string _IronPythonRuntimeDir = Path.Combine(DI.config.O2TempDir, "_IronPython_Runtime"); //_IronPython_Runtime
        public static string zippedIronPythonRunTime = "_IronPython_RunTime.zip";
        public static string ironPythonExecutable = Path.Combine(_IronPythonRuntimeDir, "ipy.exe");
        public static string IronPythonJavaExecution_PythonScript = "\"{0}\" {1}";        

        public static string IronPythonInstallationDir
        {
            get
            {
                IronPythonInstall.checkIronPythonInstallation();
                return _IronPythonRuntimeDir;
            }
            set
            {
                IronPythonInstall.uninstallIronPython();
                _IronPythonRuntimeDir = value;
                IronPythonInstall.installIronPython();
            }
        }


    }
}
