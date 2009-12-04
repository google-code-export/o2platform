// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Win32;

namespace O2.Scanner.OunceLabsCLI.Utils
{
    public class OunceCore
    {
        public static string DefaultPathToOunceInstallDir { get; set;}
        public static string CliExecutableName { get; set; }
        public static string OunceCoreLogin_UserName { get; set; }
        public static string OunceCoreLogin_Password { get; set; }
        public static string OunceCoreLogin_IPAddress { get; set; }        

        static OunceCore()
        {
            setOunceCoreDetailsFromAppConfig();
        }

        public static void setOunceCoreDetailsFromAppConfig()
        {

            DefaultPathToOunceInstallDir = ConfigurationManager.AppSettings["DefaultPathToOunceInstallDir"];
            CliExecutableName = ConfigurationManager.AppSettings["CliExecutableName"];
            OunceCoreLogin_UserName = ConfigurationManager.AppSettings["OunceCoreLogin_UserName"];
            OunceCoreLogin_Password = ConfigurationManager.AppSettings["OunceCoreLogin_Password"];
            OunceCoreLogin_IPAddress = ConfigurationManager.AppSettings["OunceCoreLogin_IPAddress"];
            
            // can't use the code bellow since ClickOnce doesnt support multiple *.config files
    //O2_Scanner_OunceLabsCLI.dll.config
            //  var configFile = new XDocument()
/*
 * var configFile = Path.Combine(DI.config.CurrentExecutableDirectory, "O2_Scanner_OunceLabsCLI.dll.config");
            if (File.Exists(configFile))
            {
                var xDocument = XDocument.Load(configFile);
                                var configSettings = new Dictionary<string, string>();
                foreach (var xElement in xDocument.Elements("configuration").Elements("appSettings").Elements("add"))
                {   
                    // somethings wrong with ReSharper on this case since I shouldn't need to supress this alert
                    // ReSharper disable PossibleNullReferenceException
                    if (xElement.Attribute("key") != null && xElement.Attribute("value") != null)
                    {
                        var keyName = xElement.Attribute("key").Value;
                        var keyValue = xElement.Attribute("value").Value;
                        configSettings.Add(keyName, keyValue);
                    }
                    // ReSharper restore PossibleNullReferenceException
                }
                DefaultPathToOunceInstallDir = configSettings["DefaultPathToOunceInstallDir"];
                OunceCoreLogin_UserName = configSettings["OunceCoreLogin_UserName"];
                OunceCoreLogin_Password = configSettings["OunceCoreLogin_Password"];
                OunceCoreLogin_IPAddress = configSettings["OunceCoreLogin_IPAddress"];
                ;
                
                
                
                 
            }
            else
                DI.log.error("Could not find config file: {0}", configFile);
 * 
 */ 
        }
        public static String getPathToOunceInstallDirectory()
        {
            if (false == Directory.Exists(DefaultPathToOunceInstallDir))
            {
                try
                {
                    RegistryKey rkRegistryKey =
                        Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Ounce License Manager");
                    if (rkRegistryKey != null)
                    {
                        string sValue = rkRegistryKey.GetValue("ImagePath").ToString();
                        if (sValue.IndexOf("Ounce Labs") > -1)
                        {
                            var ounceInstallDirectory = Path.GetDirectoryName(Path.GetDirectoryName(sValue));
                            if (Directory.Exists(ounceInstallDirectory))
                                DefaultPathToOunceInstallDir = ounceInstallDirectory;
                            return DefaultPathToOunceInstallDir;
                        }
                    }
                    DI.log.error("in getOunceInstallDirectory, could not find ounce dir from the registry");
                }
                catch (Exception ex)
                {
                    DI.log.error("in getOunceInstallDirectory :{0}", ex.Message);
                }
            }
            return DefaultPathToOunceInstallDir;
        }

        public static String getPathToOunceConfig()
        {
            string sOunceConfigDir = Path.Combine(getPathToOunceInstallDirectory(), "config");
            if (Directory.Exists(sOunceConfigDir))
                return sOunceConfigDir;

            return "";
        }

        public static string getCliExecutableName()
        {
            return CliExecutableName;
        }
    }
}
