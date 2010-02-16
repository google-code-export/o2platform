using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using O2.Interfaces.DotNet;
using O2.Kernel;
using O2.DotNetWrappers.Windows;
using O2.Kernel.InterfacesBaseImpl;
using O2.DotNetWrappers.Zip;

namespace O2.DotNetWrappers.DotNet
{
    public class GacUtils
    {
        public static string getPathToGac()
        {
            return DI.PathToGac;
        }
        public static List<IGacDll> currentGacAssemblies()
        {
            var gacAssemblies = new List<IGacDll>();
            foreach (var directory in Files.getListOfAllDirectoriesFromDirectory(DI.PathToGac, true))
            {
                if (DI.PathToGac != Path.GetDirectoryName(directory))
                {
                    var name = Path.GetFileName(Path.GetDirectoryName(directory));
                    var version = Path.GetFileName(directory);
                    var fullPath = Path.Combine(directory, name + ".dll");
                    if (File.Exists(fullPath))
                        gacAssemblies.Add(new KGacDll(name, version, fullPath));
                    else
                    {
                        // handle the rare cases when the assembly is an *.exe
                        fullPath = Path.Combine(directory, name + ".exe");
                        if (File.Exists(fullPath))
                            gacAssemblies.Add(new KGacDll(name, version, fullPath));
                        else
                           PublicDI.log.error("ERROR in currentGacAssemblies: could not find: {0}", fullPath);
                    }
                }
            }
            return gacAssemblies;
        }

        public static void backupGac()
        {
            backupGac(PublicDI.config.getTempFileInTempDirectory("zip"));
        }

        public static void backupGac(string zipFileToSaveGacContents)
        {
            O2Thread.mtaThread(
                () =>
                {
                    PublicDI.log.info("Started unzip process of Gac Folder");
                    var timer = new O2Timer("Gac Backup").start();
                    new zipUtils().zipFolder(DI.PathToGac, zipFileToSaveGacContents);
                    var logMessage = String.Format("Contents of \n\n\t{0}\n\n saved to \n\n\t{1}\n\n ", DI.PathToGac, zipFileToSaveGacContents);
                    timer.stop();
                    PublicDI.log.info(logMessage);
                    PublicDI.log.showMessageBox(logMessage);
                });
        }

    }
}
