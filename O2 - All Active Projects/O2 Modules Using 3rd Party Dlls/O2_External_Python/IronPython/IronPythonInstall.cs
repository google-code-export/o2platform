// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.IO;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.Zip;

namespace O2.External.Python.IronPython
{
    class IronPythonInstall
    {

        public static bool checkIronPythonInstallation()
        {
            if (false == Directory.Exists(IronPythonConfig._IronPythonRuntimeDir))
                installIronPython();
            return Directory.Exists(IronPythonConfig._IronPythonRuntimeDir);
        }

        public static void installIronPython()
        {
            if (false == File.Exists(IronPythonConfig.zippedIronPythonRunTime))
                DI.log.error("in installIronPython, could not find zippedIronPythonRunTime: {0}", IronPythonConfig.zippedIronPythonRunTime);
            else
            {
                DI.log.info("Installing IronPython to: {0}", IronPythonConfig._IronPythonRuntimeDir);
                new zipUtils().unzipFile(IronPythonConfig.zippedIronPythonRunTime, IronPythonConfig._IronPythonRuntimeDir);
                if (Directory.Exists(IronPythonConfig._IronPythonRuntimeDir))
                    DI.log.info("IronPython sucessfully installed");
                else
                    DI.log.error("Problem installing/unziping _IronPythonRuntimeDir: {0}", IronPythonConfig._IronPythonRuntimeDir);
                //IronPythonShell.testIronPython();
            }
        }

        public static void uninstallIronPython()
        {
            if (Directory.Exists(IronPythonConfig._IronPythonRuntimeDir))
                Files.deleteFolder(IronPythonConfig._IronPythonRuntimeDir, true);
        }
        
    }
}
