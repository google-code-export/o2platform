// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using O2.External.Firefox;
using Skybound.Gecko;

namespace O2.External.Firefox.WebAutomation
{
    public static class FirefoxXul
    {
        public static string keyFirefoxInstallFileToFind = "xul.dll";

        public static string pathToFirefoxInstallDir = "";
        public static string pathTox64Os = @"Program Files (x86)\Mozilla Firefox";

        public static List<string> possibleFirefoxInstallFolder = new List<string>
                                                                      {
                                                                          @"C:\Program Files\Mozilla Firefox"
                                                                      };

        public static bool isFirefoxInstalled()
        {
            if (is64BitOs())
                return false;
            DI.log.info("Checking if FireFox is installed");
            if (File.Exists(Path.Combine(pathToFirefoxInstallDir, keyFirefoxInstallFileToFind)))
            {
                DI.log.debug("Found FireFox and {0} at {1}", keyFirefoxInstallFileToFind, pathToFirefoxInstallDir);
                return true;
            }
            foreach (string folder in possibleFirefoxInstallFolder)
            {
                pathToFirefoxInstallDir = folder;
                if (File.Exists(Path.Combine(folder, keyFirefoxInstallFileToFind)))
                {
                    DI.log.debug("Found FireFox and {0} at {1}", keyFirefoxInstallFileToFind, pathToFirefoxInstallDir);
                    return true;
                }
            }
            return false;
        }

        public static bool is64BitOs()
        {
            return Directory.Exists(pathTox64Os); // there is probably a more scientific way to find this out :)
        }


        internal static GeckoWebBrowser createGeckoWebBrowser()
        {
            try
            {
                if (isFirefoxInstalled())
                {
                    Xpcom.Initialize(pathToFirefoxInstallDir);
                    DI.log.info("Xpcom.Initialize was sucessfully initialized, creating Gecko Browser object");
                    return new GeckoWebBrowser {Dock = DockStyle.Fill};
                }
                DI.log.debug("can't create GeckoWebBrowser object because couldn't find Firefox Install dir");
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in createGeckoWebBrowser");
            }
            return null;
        }
    }
}
