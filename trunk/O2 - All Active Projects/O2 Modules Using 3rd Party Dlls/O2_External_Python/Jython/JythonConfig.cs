// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.IO;
using O2.External.Python;
using O2.External.Python.Jython;

namespace O2.External.Python.Jython
{
    public class JythonConfig
    {
        public static string _jythonRuntimeDir = Path.Combine(DI.config.O2TempDir, "_Jython_Runtime"); //_Jython_Runtime
        public static string zippedJythonRunTime = "_Jython_RunTime.zip";        
        public static string jythonJavaExecution_PythonScript = "-jar \"{0}/jython.jar\" -Dpython.path=\"{0}/javassist.jar\" \"{1}\" {2}";
        public static string jythonJavaExecution_PythonShell = "-jar \"{0}/jython.jar\" -Dpython.path=\"{0}/javassist.jar\"";
        //public static string jythonExecutable = Path.Combine(_jythonRuntimeDir, "jython.jar");

        public static string JythonInstallationDir
        {
            get
            {
                JythonInstall.checkJythonInstallation();
                return _jythonRuntimeDir;
            }
            set
            {
                JythonInstall.uninstallJython();
                _jythonRuntimeDir = value;
                JythonInstall.installJython();
            }
        }

    }
}
