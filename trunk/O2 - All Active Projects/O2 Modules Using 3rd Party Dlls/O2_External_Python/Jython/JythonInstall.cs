// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.IO;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.Zip;
using O2.External.Python;
using O2.External.Python.Jython;

namespace O2.External.Python.Jython
{
    public class JythonInstall
    {        
        public static bool checkJythonInstallation()
        {
            if (false == Directory.Exists(JythonConfig._jythonRuntimeDir))
                installJython();
            return Directory.Exists(JythonConfig._jythonRuntimeDir);
        }

        public static void installJython()
        {
            if (false == File.Exists(JythonConfig.zippedJythonRunTime))
                DI.log.error("in installJython, could not find zippedJythonRunTime: {0}", JythonConfig.zippedJythonRunTime);
            else
            {
                DI.log.info("Installing Jython to: {0}", JythonConfig._jythonRuntimeDir);
                new zipUtils().unzipFile(JythonConfig.zippedJythonRunTime, JythonConfig._jythonRuntimeDir);
                if (Directory.Exists(JythonConfig._jythonRuntimeDir))
                    DI.log.info("Jyhton sucessfully installed");
                else
                    DI.log.error("Problem installing/unziping _jythonRuntimeDir: {0}", JythonConfig._jythonRuntimeDir);

                JythonShell.testJython();   // need to run Jython once so that the _Jython_Runtime\cachedir\packages is created and populated
            }
        }

        public static void uninstallJython()
        {
            if (Directory.Exists(JythonConfig._jythonRuntimeDir))
                Files.deleteFolder(JythonConfig._jythonRuntimeDir, true);
        }
    }
}
