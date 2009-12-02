// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using O2.Core.CIR.CirCreator;

namespace O2.Rules.OunceLabs.RulesUtils
{
    public class OunceCallbacks
    {
        #region SearchMode enum

        public enum SearchMode
        {
            WebMethods,
            PublicMethods
        }

        #endregion

        #region Nested type: DotNet

        public class DotNet
        {
            /* public static void addCustomRulesToWebMethods(String sFileOrDirectoryToProcess)
            {
                DI.log.error("NOT IMPLEMENTED YET");
            }*/


            public static List<String> calculateListOfMethodsFromFiles(List<String> sFileList, SearchMode smSearchMode)
            {
                return calculateListOfMethodsFromFiles(sFileList, smSearchMode, true);
            }

            public static List<String> calculateListOfMethodsFromFiles(List<String> sFileList, SearchMode smSearchMode,
                                                                       bool bOnlyAddFunctionsWithNoCustomRule)
            {
                bool bVerbose = false;
                var lsResolvedMethodsSignature = new List<string>();
                var lmiMethodsToResolve = new List<MethodInfo>();
                foreach (string sFile in sFileList)
                {
                    switch (smSearchMode)
                    {
                        case SearchMode.PublicMethods:
                            calculateListWithPublicMethods(sFile, lmiMethodsToResolve);
                            break;
                        case SearchMode.WebMethods:
                            calculateListWithWebMethods(sFile, lmiMethodsToResolve);
                            break;
                    }
                }

                foreach (MethodInfo miMethodInfo in lmiMethodsToResolve)
                {
                    String sMethodSignature = OunceLabsScannerHacks.fixDotNetSignature(miMethodInfo);
                    // DC adding all methods (for the reason below)
                    lsResolvedMethodsSignature.Add(sMethodSignature);


                    // DC removing the code below since queries to the DB are now taking a long time:
                    // For example: Select vuln_id, 'signature', 'signature' from rec where signature = 'HacmeBank_v2_WS.WS_UserManagement.Login(string;string):string';
                    // [2:03 AM] DEBUG: sql query:  in 2s:218     
                    // [2:03 AM] DEBUG: sql query:  in 2s:0
                    // [2:03 AM] DEBUG: sql query:  in 1s:968
                    // [2:03 AM] DEBUG: sql query:  in 1s:875
                    // [2:03 AM] DEBUG: sql query:  in 1s:843
                    // [2:03 AM] DEBUG: sql query:  in 1s:921
                    // [2:03 AM] DEBUG: sql query:  in 1s:937
                    // [2:03 AM] DEBUG: sql query:  in 1s:937
                    // [2:03 AM] DEBUG: sql query:  in 1s:921
                    /*
                    if (false == bOnlyAddFunctionsWithNoCustomRule)                  // check if we want the full list of possible webmethods or only the list of methods that don't have a custom rule
                        lsResolvedMethodsSignature.Add(sMethodSignature);

                    else if (sMethodSignature.IndexOf("()") == -1)                   // remove methods with no parameters
                        if (0 == Lddb_OunceV6.action_getVulnIdThatMatchesSignature("", sMethodSignature, false))
                            lsResolvedMethodsSignature.Add(sMethodSignature);
                        else
                            if (bVerbose)
                                 DI.log.info("Method {0} is already marked as a callback", sMethodSignature);
                     */
                }
                return lsResolvedMethodsSignature;
            }

            public static void loadAllDllsFromDirectory(string strDirectory)
            {
                foreach (string strFileName in Directory.GetFiles(strDirectory, "*.dll"))
                {
                    Assembly aTargetDll = Assembly.LoadFrom(strFileName);
                }
            }


            public static void calculateListWithPublicMethods(string strTargetDll, List<MethodInfo> lmiMethods)
            {
                loadAllDllsFromDirectory(Path.GetDirectoryName(strTargetDll));
                try
                {
                    //Assembly aTargetDll = Assembly.LoadFile(strTargetDll);
                    Assembly aTargetDll = Assembly.LoadFrom(strTargetDll);
                    foreach (Module mModule in aTargetDll.GetModules())

                        foreach (Type tType in mModule.GetTypes())
                            if (tType.IsPublic && tType.IsInterface == false && tType.IsAbstract == false)
                                foreach (
                                    MethodInfo mMethod in
                                        tType.GetMethods(BindingFlags.Public | BindingFlags.Static |
                                                         BindingFlags.Instance))
                                    if (mMethod.IsPublic)
                                        // if (mMethod.IsSpecialName == true)
                                        //     Console.WriteLine(mMethod.Name);
                                        // else
                                        if (mMethod.Name != "get_Item" && mMethod.Name != "set_Item")
                                            // this one seems to never work (so skipping for now)                                    
                                            if (mMethod.Name != "GetType" && mMethod.Name != "ToString" &&
                                                mMethod.Name != "Equals" && mMethod.Name != "GetHashCode")
                                                //ignore these since they come from System.Object
                                                lmiMethods.Add(mMethod);
                }
                catch (ReflectionTypeLoadException rtleException)
                {
                    Exception[] eExceptions = rtleException.LoaderExceptions;
                    foreach (Exception eException in eExceptions)
                        DI.log.error(eException.Message);
                }
            }

            public static void calculateListWithWebMethods(string strTargetDll, List<MethodInfo> lmiWebMethods)
            {
                try
                {
                    //Assembly aTargetDll = Assembly.LoadFile(strTargetDll);
                    if (File.Exists(strTargetDll))
                    {
                        loadAllDllsFromDirectory(Path.GetDirectoryName(strTargetDll));
                        Assembly aTargetDll = Assembly.LoadFrom(strTargetDll);
                        foreach (Module mModule in aTargetDll.GetModules())
                            foreach (Type tType in mModule.GetTypes())
                                foreach (MethodInfo mMethod in tType.GetMethods())
                                {
                                    //if (mMethod.GetCustomAttributes(false).Length > 0)
                                    //    Console.WriteLine(mMethod.Name);
                                    foreach (object mCustomAttribute in mMethod.GetCustomAttributes(false))
                                    {
                                        //    Console.WriteLine("\t\t" + mCustomAttribute.GetType().ToString());
                                        if (mCustomAttribute.GetType().ToString() ==
                                            "System.Web.Services.WebMethodAttribute")
                                            lmiWebMethods.Add(mMethod);
                                    }
                                }
                    }
                }

                catch (Exception ex)
                {
                    DI.log.ex(ex, "in calculateListWithWebMethods");
                }
            }
        }

        #endregion
    }
}
