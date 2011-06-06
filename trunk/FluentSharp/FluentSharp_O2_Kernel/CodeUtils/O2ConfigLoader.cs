// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.IO;
using FluentSharp.O2.Kernel.CodeUtils;
using FluentSharp.O2.Kernel.InterfacesBaseImpl;

namespace FluentSharp.O2.Kernel.CodeUtils
{
    public class O2ConfigLoader
    {
        public static string defaultLocationOfO2ConfigFile()
        {
            if (Environment.CurrentDirectory.IndexOf(@"E:\O2\_SourceCode_O2") > -1)
                return @"C:\O2\devbox.o2.config";
            return Path.Combine(Environment.CurrentDirectory, "o2.config");
        }

        static public KO2Config getKO2Config()
        {            
            
            // the default location is the current executable directory
            var defaultLocationOfO2ConfigXmlFile = defaultLocationOfO2ConfigFile();
            var kO2Config = O2ConfigLoader.loadO2Config(defaultLocationOfO2ConfigXmlFile, true);
            if (kO2Config == null)
            {
                DI.log.showMessageBox(string.Format(
                    "CRITICAL ERROR: could not load (or create) default O2 Config file: {0} /n/n aborting O2 execution.",
                    defaultLocationOfO2ConfigXmlFile));
                //Process.GetCurrentProcess().Kill();
                return null;
            }
            // but it can point to another O2 configuration file
            if (string.IsNullOrEmpty(kO2Config.O2ConfigFile))
                kO2Config.O2ConfigFile = defaultLocationOfO2ConfigXmlFile;
            else if (kO2Config.O2ConfigFile != defaultLocationOfO2ConfigXmlFile)
            {
                DI.log.info("there was another O2ConfigFile configured, so trying to load it: {0}",
                            kO2Config.O2ConfigFile);
                var otherKO2Config = loadO2Config(kO2Config.O2ConfigFile, false);
                if (otherKO2Config != null)
                    mergeO2ConfigFiles(otherKO2Config, kO2Config);
                else
                    DI.log.error("Could not load the other O2Config fire, returning the default one");
            }                 
            return kO2Config;
        }

        private static void applyO2ConfigDependeciesInjection(KO2Config kO2Config)
        {

            if (kO2Config != null && kO2Config.dependenciesInjection != null)
            {
                DI.log.info("Applying O2Config Depedency Injections");
                foreach (var dependency in kO2Config.dependenciesInjection)
                {
                    DI.log.debug("[setting dependecy injection]: {0}.{1} = {2}", dependency.Type, dependency.Parameter, dependency.Value);
                    var targetType = DI.reflection.getType(dependency.Type);
                    if (targetType == null)
                        DI.log.error(" [in applyO2ConfigDependeciesInjection]  Could not get type: {0}", dependency.Type);
                    else
                    {
                        var propertyInfo = DI.reflection.getPropertyInfo(dependency.Parameter, targetType);
                        if (propertyInfo == null)
                            DI.log.error(" [in applyO2ConfigDependeciesInjection]  Could not get property {0} from type: {1}", dependency.Parameter,  dependency.Type);
                        else
                        {
                            if (false == DI.reflection.setProperty(propertyInfo, dependency.Value))                             
                                DI.log.error(" [in applyO2ConfigDependeciesInjection]  Could not set property {0} from type: {1} with value {2}",
                                     dependency.Parameter,  dependency.Type, dependency.Value);                                                        
                        }
                    }

                }
            }
                
        }

        public static KO2Config loadO2Config(string pathToO2ConfigFile, bool createFileIfDoesntExit)
        {
            if (false == File.Exists(pathToO2ConfigFile))
                if (createFileIfDoesntExit)
                    return createO2ConfigFile(pathToO2ConfigFile);
                else
                    return null;

            var kO2Config = (KO2Config)O2Kernel_Serialize.getDeSerializedObjectFromXmlFile(pathToO2ConfigFile, typeof(KO2Config));
            if (kO2Config != null)
            {
                applyO2ConfigDependeciesInjection(kO2Config);
                return kO2Config;
            }
            DI.log.error("in loadO2Config, could not load KO2Config file: {0}", pathToO2ConfigFile);
            return null;
        }

        public static void createDefaultO2ConfigFile()
        {
            createO2ConfigFile(defaultLocationOfO2ConfigFile());
        }

        public static KO2Config createO2ConfigFile(string pathToO2ConfigFile)
        {
            var newO2ConfigFile = new KO2Config(pathToO2ConfigFile);
            saveO2ConfigFile(newO2ConfigFile, pathToO2ConfigFile);
            return newO2ConfigFile;
        }

        private static void saveO2ConfigFile()
        {
            saveO2ConfigFile((KO2Config)DI.config, defaultLocationOfO2ConfigFile());
        }

        private static void saveO2ConfigFile(KO2Config o2ConfigFileToSave, string path)
        {
            O2Kernel_Serialize.createSerializedXmlFileFromObject(o2ConfigFileToSave, path, null);
        }

        public static void createOrSetLocalConfigFile(string pathToLocalConfigFile)
        {
            if (File.Exists(pathToLocalConfigFile))
            {
                DI.config.O2ConfigFile = pathToLocalConfigFile;
                saveO2ConfigFile();                
            }
            else
            {
                var newO2ConfigDirectory = Path.GetDirectoryName(pathToLocalConfigFile);
                O2Kernel_Files.checkIfDirectoryExistsAndCreateIfNot(newO2ConfigDirectory);
                saveO2ConfigFile((KO2Config)DI.config, pathToLocalConfigFile);     // create local config file from current O2Configfile
                //createO2ConfigFile(pathToLocalConfigFile);
                DI.config.O2ConfigFile = pathToLocalConfigFile;
            }
        }



        public static void mergeO2ConfigFiles(KO2Config source, KO2Config target)
        {
            if (source!=null & target!=null)
            {
                foreach (var property in DI.reflection.getProperties(source))
                {
                    var sourceValue = DI.reflection.getProperty(property.Name, source,false);
                    if (sourceValue != null)
                    {
                        switch (property.Name)
                        {        
                            case "dependenciesInjection":
                                var sourceDependenciesInjection = (List<DependencyInjection>)DI.reflection.getProperty(property.Name, source);
                                var targetDependenciesInjection = (List<DependencyInjection>)DI.reflection.getProperty(property.Name, target);
                                targetDependenciesInjection.AddRange(sourceDependenciesInjection);
                                break;
                            case "dependencyInjectionTest":         // we don't want this to be set from here (and it couldn't be anyway since it is static)
                                KO2Config.dependencyInjectionTest = "";
                                break;
                            default:
                                DI.reflection.setProperty(property.Name, target, sourceValue);
                                break;
                        }
                    }
                }

            }
            else
                DI.log.error("in mergeO2ConfigFiles false == (source!=null & target!=null)");
        }
    }
}
