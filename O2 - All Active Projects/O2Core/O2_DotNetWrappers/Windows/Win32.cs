using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace O2.DotNetWrappers.Windows
{
    public class Win32
    {
        public static string findFileOnLocalPath(string fileToFind)
        {
            var pathEnvironmentVariable = Environment.GetEnvironmentVariable("Path");
            if (pathEnvironmentVariable != null)
                foreach (var directory in pathEnvironmentVariable.Split(';'))
                {
                    var file = Path.Combine(directory, fileToFind);
                    DI.log.info(file);
                    if (File.Exists(file))
                        return file;
                }
            DI.log.error("in findFileOnLocalPath, could not find file {0} in environment Path directories: {1}", fileToFind, pathEnvironmentVariable);
            return "";
        }
    }
}
