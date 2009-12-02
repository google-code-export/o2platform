// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.IO;

namespace O2.External.Python.CPython
{
    public class CPythonConfig
    {
        public static string _CPythonRuntimeDir = @"C:\Python24";         
        public static string _CPythonScript = "python.exe \"{1}\" {2}";
        //public static string _CPythonShell = "python.exe";
        public static string _CPythonExecutable = Path.Combine(_CPythonRuntimeDir, "python.exe");
        //public static string jythonExecutable = Path.Combine(_jythonRuntimeDir, "jython.jar");

        public static string JythonInstallationDir
        {
            get
            {                
                return _CPythonRuntimeDir;
            }            
            set
            {
                _CPythonRuntimeDir = value;
            }
        }

    }
}
