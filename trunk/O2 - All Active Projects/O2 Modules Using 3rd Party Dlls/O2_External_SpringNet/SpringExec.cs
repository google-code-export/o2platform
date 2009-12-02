using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Spring.Context.Support;
using Spring.Objects.Factory.Support;

namespace O2.External.SpringNet
{
    public class SpringExec
    {
        public static bool haveCreatedOkAllAllSpringObjects;        


        public static string getCurrentModuleXmlConfigFile()
        {
            try
            {
                return Path.Combine(DI.config.CurrentExecutableDirectory, DI.config.CurrentExecutableFileName + ".xml");
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, " in getExecutingAssembly");
            }
            return "";
        }

        public static void loadDefaultConfigFile()
        {
            string configFile = getCurrentModuleXmlConfigFile();
            configFile = configFile.Replace(".vshost", ""); // for the cases where we are runing using VS Debug Process
            loadConfigAndStartGUI(configFile);
        }

        public static void loadConfigAndStartGUI(String springConfigFileToLoad)
        {
            try
            {
                if (false == File.Exists(springConfigFileToLoad))
                {
                    string errorMessage = "Could not find SpringNet config file: " + springConfigFileToLoad;
                    DI.log.error(errorMessage);
                    DI.log.showMessageBox(errorMessage);
                    return;
                }

                Application.EnableVisualStyles();
                // need to setup these here or we will have an error if we try to create windows objects via DI
                Application.SetCompatibleTextRenderingDefault(false);

                new XmlApplicationContext(new[] {springConfigFileToLoad});
            }
            catch (Exception ex)
            {
                if (haveCreatedOkAllAllSpringObjects == false)
                    // we only want to show this when the error is thown by Spring
                {
                    string errorMessage =
                        String.Format(
                            "In SpringNet Configration -> loadConfigAndStartGUI while loading the config file {0}",
                            springConfigFileToLoad);
                    DI.log.reportCriticalErrorToO2Developers(null, ex, errorMessage);
                }
            }
        }


        public static object createTypeAndInvokeMethod(Type tTargetType, String sMethodToInvoke)
        {
            return createTypeSetPropertiesAndInvokeMethod(tTargetType, sMethodToInvoke, null);
        }

        public static object createTypeSetPropertiesAndInvokeMethod(Type tTargetType, String sMethodToInvoke,
                                                                    Dictionary<String, Object> dProperties)
        {
            string sFactoryObject = "FactoryObject";
            string sInvokeResult = "InvokeResult";
            var ctx = new GenericApplicationContext();

            // create factory Object,  add (if required) its properties to it and register it
            var rodFactoryObject = new RootObjectDefinition {ObjectType = tTargetType};
            if (dProperties != null)
                foreach (String sProperty in dProperties.Keys)
                    rodFactoryObject.PropertyValues.Add(sProperty, dProperties[sProperty]);

            ctx.RegisterObjectDefinition(sFactoryObject, rodFactoryObject);

            // create object to invoke method and register it

            var rodMethodToInvoke = new RootObjectDefinition
                                        {
                                            FactoryMethodName = sMethodToInvoke,
                                            FactoryObjectName = sFactoryObject
                                        };

            ctx.RegisterObjectDefinition(sInvokeResult, rodMethodToInvoke);

            // when we get the rodMethodToInvoke object, the rodFactoryObject will be created
            return ctx.GetObject(sInvokeResult);
        }
    }
}