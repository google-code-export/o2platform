// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using O2.Core.CIR.CirCreator.DotNet;
using O2.Core.CIR.CirObjects;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Filters;
using O2.External.O2Mono.MonoCecil;
using O2.Kernel.Interfaces.CIR;

namespace O2.Core.CIR.CirUtils
{
    public class CirDataAnalysisUtils
    {
        public static ICirData addO2CirDataFile(ICirDataAnalysis cirDataAnalysis, String sO2CirFileToAdd, bool useCachedVersionIfAvailable)
        {
            ICirData cirData;
            if (useCachedVersionIfAvailable && cirDataAnalysis.dCirDataFilesLoaded.ContainsKey(sO2CirFileToAdd))
            {
                //DI.log.debug("O2CirData file already loaded: {0}", sO2CirFileToAdd);
                cirData = cirDataAnalysis.dCirDataFilesLoaded[sO2CirFileToAdd];
            }
            else
            {
                //    vars.set_(sO2CirFileToAdd, null);           // force reload of the o2cirdata file
                //cirDataAnalysis.dCirDataFilesLoaded.Clear();                
                cirData = CirLoad.loadFile(sO2CirFileToAdd);
                if (cirData == null || cirData.dClasses_bySignature == null) // || cirData.lFiles.Count == 0)
                    return null;
                // add to main list
                if (cirDataAnalysis.dCirDataFilesLoaded.ContainsKey(sO2CirFileToAdd))
                    cirDataAnalysis.dCirDataFilesLoaded[sO2CirFileToAdd] = cirData;
                else 
                    cirDataAnalysis.dCirDataFilesLoaded.Add(sO2CirFileToAdd, cirData);
            }
            return addO2CirDataFile(cirDataAnalysis, cirData);
        }

        public static ICirData addO2CirDataFile(ICirDataAnalysis cirDataAnalysis, ICirData cirData)
        {
            return addO2CirDataFile(cirDataAnalysis, cirData, true /* runRemapXrefs */);
        }

        public static ICirData addO2CirDataFile(ICirDataAnalysis cirDataAnalysis, ICirData cirData, bool runRemapXrefs)
        {
            if (cirData == null)
                return null;
            // check for sDbId (will be needed to get data from Ounce Db
            if (!String.IsNullOrEmpty(cirDataAnalysis.sDb_Id) && cirDataAnalysis.sDb_Id != cirData.sDbId)
                DI.log.error(
                    "The O2CirData files have different Db_Ids, The rules creation will use the last one loaded:{0}",
                    cirData.sDbId);
            if (String.IsNullOrEmpty(cirDataAnalysis.sDb_Id))
                cirDataAnalysis.sDb_Id = cirData.sDbId;
            // add classes with ControlFlowGraphs to list of classes to process

            var sClasses = new List<string>(cirData.dClasses_bySignature.Keys);
            sClasses.Sort();
            //cirDataAnalysis.dCirClass.Clear();
            foreach (ICirClass ccCirClass in cirData.dClasses_bySignature.Values)
            {
                //if (ccCirClass.bClassHasMethodsWithControlFlowGraphs && false == cirDataAnalysis.dCirClass.ContainsKey(ccCirClass))
                try
                {
                    // if (false == cirDataAnalysis.dCirClass.ContainsKey(ccCirClass))
                    //     cirDataAnalysis.dCirClass.Add(ccCirClass, cirData);

                    if (false == cirDataAnalysis.dCirClass_bySignature.ContainsKey(ccCirClass.Signature))
                    {
                        cirDataAnalysis.dCirClass_bySignature.Add(ccCirClass.Signature, ccCirClass);
                        addClass(cirDataAnalysis, ccCirClass);
                        if (false == cirDataAnalysis.dCirClass.ContainsKey(ccCirClass))
                            cirDataAnalysis.dCirClass.Add(ccCirClass, cirData);
                    }
                    else
                    {
                        var classHost = cirDataAnalysis.dCirClass_bySignature[ccCirClass.Signature];
                        var classToMerge = ccCirClass;
                        mergeClasses(cirDataAnalysis, classHost, classToMerge);
                    }
                }
                catch (Exception ex)
                {
                    DI.log.debug("in addO2CirDataFile : {0}", ex.Message);
                    //DC: to-do: debug the cases where this exception is triggered
                }
            }
            /*   var methodtofind = "System.Data!System.Int32 System.Data.IDataRecord::GetOrdinal(System.String)";
               if (cirData.dFunctions_bySignature.ContainsKey(methodtofind))
               {
               }
               if (cirDataAnalysis.dCirFunction_bySignature.ContainsKey(methodtofind))
               {
               }*/

            //if (true)

            if (runRemapXrefs)
            {
                remapXrefs(cirDataAnalysis);
                showStatsOfLoadedData(cirDataAnalysis);
            }
            else                            // if we are not remapping the XRefs then remove any FunctionIsCalledBy that are there (since some might be wrong)
            {
                foreach (var cirFunction in cirData.dFunctions_bySignature.Values)
                    cirFunction.FunctionIsCalledBy = new List<ICirFunctionCall>();
            }
            
            return cirData;
        }

        public static void remapXrefs(ICirDataAnalysis cirDataAnalysis)
        {
            DI.log.debug("Remapping Methods and Classes XReferences");
            var timer = new O2Timer("Remapping XRefs took: {0}").start();
            remapIsCalledByXrefs(cirDataAnalysis); // make sure everythings is pointing to the correct place
            calculateXrefs_SuperClases(cirDataAnalysis);
            
            // ensure all the methods are mapped to the correct class
            foreach(var cirClass in cirDataAnalysis.dCirClass_bySignature.Values)
                foreach(var cirFunction in cirClass.dFunctions.Values)
                {
                    cirFunction.ParentClass = cirClass;
                    cirFunction.ParentClassFullName = cirClass.FullName;
                    cirFunction.ParentClassName = cirClass.Name;
                }
            timer.stop();
        }

        private static void addClass(ICirDataAnalysis cirDataAnalysis, ICirClass ccCirClass)
        {
            if (cirDataAnalysis.dCirClass_bySignature.ContainsKey(ccCirClass.Signature))
            {
                var mainRefCirClass = cirDataAnalysis.dCirClass_bySignature[ccCirClass.Signature];
                if (mainRefCirClass != ccCirClass)          // if they are diferent we need to sync the superclasses dictionary
                {
                    foreach (var superclass in ccCirClass.dSuperClasses.Keys)
                        if (false == mainRefCirClass.dSuperClasses.ContainsKey(superclass))
                            mainRefCirClass.dSuperClasses.Add(superclass, ccCirClass.dSuperClasses[superclass]);
                }
            }
            else
            {
            }
            foreach (ICirFunction cfCirFunction in ccCirClass.dFunctions.Values)
            //                if (cfCirFunction.HasControlFlowGraph)
            {
                if (false == cirDataAnalysis.dCirFunction.ContainsKey(cfCirFunction))
                    cirDataAnalysis.dCirFunction.Add(cfCirFunction, ccCirClass);                
                // we need this cross check with ccCirClass since we will it to resolve cfCirFunction data
                if (false ==
                    cirDataAnalysis.dCirFunction_bySignature.ContainsKey(cfCirFunction.FunctionSignature))
                    addFunction(cirDataAnalysis, cfCirFunction);
                else
                {
                    var functionHost =
                        cirDataAnalysis.dCirFunction_bySignature[cfCirFunction.FunctionSignature];
                    var functionToMerge = cfCirFunction;
                    mergeFunctions(cirDataAnalysis, functionHost, functionToMerge);
                }
            }
        }


        // need to check of other class values
        public static void mergeClasses(ICirDataAnalysis cirDataAnalysis, ICirClass classHost, ICirClass classToMerge)
        {
            foreach (var propertyInfo in DI.reflection.getProperties(typeof(ICirClass)))
                if (DI.reflection.getProperty(propertyInfo.Name, classHost) == null && DI.reflection.getProperty(propertyInfo.Name, classToMerge) != null)
                    DI.reflection.setProperty(propertyInfo.Name, classHost,
                                              DI.reflection.getProperty(propertyInfo.Name, classToMerge));

            addClass(cirDataAnalysis, classToMerge);
        }

        // for now  only add the mappings, but need to check for all other variables
        public static void mergeFunctions(ICirDataAnalysis cirDataAnalysis, ICirFunction functionHost, ICirFunction functionToMerge)
        {
            // map properties
            foreach (var propertyInfo in DI.reflection.getProperties(typeof(ICirFunction)))
                // assign value if for this property:
                //   functionHost == null && functionToMerge != null
                //   functionHost == false && functionToMerge == true
                if ((DI.reflection.getProperty(propertyInfo.Name, functionHost) == null && DI.reflection.getProperty(propertyInfo.Name, functionToMerge) != null) ||
                    (DI.reflection.getProperty(propertyInfo.Name, functionHost) != null && DI.reflection.getProperty(propertyInfo.Name, functionHost) is bool && (bool)DI.reflection.getProperty(propertyInfo.Name, functionHost) == false &&
                     DI.reflection.getProperty(propertyInfo.Name, functionToMerge) != null && (bool)DI.reflection.getProperty(propertyInfo.Name, functionToMerge) == true))

                    DI.reflection.setProperty(propertyInfo.Name, functionHost,
                                              DI.reflection.getProperty(propertyInfo.Name, functionToMerge));

            // ensure the class bClassHasMethodsWithControlFlowGraphs value is uptodate
            if (functionHost.HasControlFlowGraph && functionHost.ParentClass != null)
                functionHost.ParentClass.bClassHasMethodsWithControlFlowGraphs = true;
                
            for (int i = 0; i < functionToMerge.FunctionIsCalledBy.Count; i++)
            {
                var isCalledBy = functionToMerge.FunctionIsCalledBy[i];

                var cirFunctionRef = getFunctionRef(cirDataAnalysis, isCalledBy.cirFunction);       // big change here when isCalledBy was changed to be a CirFunctionCall object (check if this works ok)
                bool found = false;
                foreach (var functionCall in functionHost.FunctionIsCalledBy)
                    if (functionCall.cirFunction == cirFunctionRef)
                        found = true;
                if (found == false)
                    functionHost.FunctionIsCalledBy.Add(new CirFunctionCall(cirFunctionRef));

                // make sure the is calledBy is there
                //if (false == functionHost.FunctionIsCalledBy.Contains(cirFunctionRef))
                //    functionHost.FunctionIsCalledBy.Add(cirFunctionRef);

                /* if (isCalledBy != cirFunctionRef)  // means they are different and we need to fix the xRef
                     functionToMerge.FunctionIsCalledBy[i] = cirFunctionRef;*/
            }

            if (functionToMerge.FunctionsCalled.Count > 0 && functionHost.FunctionsCalled.Count != functionToMerge.FunctionsCalled.Count)
            {
                if (functionHost.FunctionsCalled.Count == 0)
                    functionHost.FunctionsCalled = functionToMerge.FunctionsCalled;
                else
                    DI.log.error("something is wrong in mergeFunctions (on functionHost.FunctionsCalledSequence.Count == 0) for {0}", functionHost.FunctionSignature);
            }
            if (functionToMerge.FunctionsCalledUniqueList.Count > 0 && functionHost.FunctionsCalledUniqueList.Count != functionToMerge.FunctionsCalledUniqueList.Count)
            {
                if (functionHost.FunctionsCalledUniqueList.Count == 0)
                    functionHost.FunctionsCalledUniqueList = functionToMerge.FunctionsCalledUniqueList;
                else
                    DI.log.error("something is wrong in mergeFunctions (on functionHost.FunctionsCalledUniqueList.Count == 0) for {0}", functionHost.FunctionSignature);
            }
            /*foreach (var isCalledBy in functionToMerge.)
            {
                var cirFunctionRef = getFunctionRef(cirDataAnalysis, isCalledBy);
                if (false == functionHost.FunctionIsCalledBy.Contains(cirFunctionRef))
                    functionHost.FunctionIsCalledBy.Add(cirFunctionRef);
            }*/
        }

        public static ICirFunction getFunctionRef(ICirDataAnalysis cirDataAnalysis, ICirFunction cirFunction)
        {
            if (false == cirDataAnalysis.dCirFunction_bySignature.ContainsKey(cirFunction.FunctionSignature))
                cirDataAnalysis.dCirFunction_bySignature.Add(cirFunction.FunctionSignature, cirFunction);
            return cirDataAnalysis.dCirFunction_bySignature[cirFunction.FunctionSignature];
        }

        private static void addFunction(ICirDataAnalysis cirDataAnalysis, ICirFunction cirFunction)
        {
            cirDataAnalysis.dCirFunction_bySignature.Add(cirFunction.FunctionSignature, cirFunction);
        }

        public static void calculateXrefs_SuperClases(ICirDataAnalysis cirDataAnalysis)
        {
            try
            {
                // first make sure the super classes are pointing to null
           /*     foreach (ICirClass cirClass in cirDataAnalysis.dCirClass_bySignature.Values.ToList())
                    foreach (string superClassSignature in cirClass.dSuperClasses.Keys.ToList())
                        if (cirClass.dSuperClasses[superClassSignature] == null)
                            if (cirDataAnalysis.dCirClass_bySignature.ContainsKey(superClassSignature))
                                cirClass.dSuperClasses[superClassSignature] = cirDataAnalysis.dCirClass_bySignature[superClassSignature];
                            else
                            {
                                cirDataAnalysis.dCirClass_bySignature.Add(superClassSignature,new CirClass(superClassSignature));
                                cirClass.dSuperClasses[superClassSignature]  = cirDataAnalysis.dCirClass_bySignature[superClassSignature];                            
                            }*/

                bool ignoreJavaLangObjectCount = true;
                int iIgnoredJavaLangObjectCount = 0;
                foreach (ICirClass ccCirClass in cirDataAnalysis.dCirClass_bySignature.Values)
                    foreach (ICirClass ccSuperClass in ccCirClass.dSuperClasses.Values)
                    {
                        if (ignoreJavaLangObjectCount && (ccSuperClass.Signature == "java.lang.Object" || ccSuperClass.Signature == "System.Object"))
                            iIgnoredJavaLangObjectCount++;
                        else
                        {
                            calculateXrefs_SuperClases_recursive(cirDataAnalysis, ccSuperClass.Signature, ccCirClass);


                        //dCirClass_bySuperClass_Simple[sSuperClassCustomSignature].Add(ccCirClass);

                        //                            foreach (CirClass ccIsSuperClassedBy in ccSuperClass.dIsSuperClassedBy.Values)
                        //                           {
                        //                               String sIsSuperClassedByCustomSignature = String.Format("{0}.<-.{1}", sSuperClassCustomSignature, ccIsSuperClassedBy.FunctionSignature.Replace('.', '_'));
                        //                               dCirClass_bySuperClass_Simple.Add(sIsSuperClassedByCustomSignature, null);
                        //                           }
                        }


                        /*
                        if (false == dCirClass_bySuperClass_Recursive.ContainsKey(ccSuperClass.FunctionSignature))
                            dCirClass_bySuperClass_Recursive.Add(ccSuperClass.FunctionSignature, new List<CirClass>());
                        dCirClass_bySuperClass_Recursive[ccSuperClass.FunctionSignature].Add(ccCirClass);
                         */
                        //          calculateXrefs_SuperClases_recursive(sSuperClass);
                    }
                if (iIgnoredJavaLangObjectCount > 0)
                    DI.log.debug("# of Ignored java.lang.Object:{0}", iIgnoredJavaLangObjectCount);
                if (cirDataAnalysis.lCirClass_bySuperClass.Count > 0)
                    DI.log.debug("# of SuperClass mappings:{0}", cirDataAnalysis.lCirClass_bySuperClass.Count);
            }
            catch (Exception ex)
            {
                DI.log.error("in calculateXrefs_SuperClases:{0}", ex.Message);
            }
        }

        public static void remapSuperClassesXrefs(ICirDataAnalysis cirDataAnalysis)
        {
            // first clear all xrefs
            foreach (ICirClass cirClass in cirDataAnalysis.dCirClass_bySignature.Values.ToList())
            {                
                cirClass.dIsSuperClassedBy = new Dictionary<string, ICirClass>();
            }

            // now map the supperclases and IsSuperClassedBy
            foreach (ICirClass cirClass in cirDataAnalysis.dCirClass_bySignature.Values.ToList())
            {                
                foreach (string superClassSignature in cirClass.dSuperClasses.Keys.ToList())
                {
                    // make sure the dSuperClasses is mapped
                    //if (cirClass.dSuperClasses[superClassSignature] == null)
                    if (cirDataAnalysis.dCirClass_bySignature.ContainsKey(superClassSignature))
                        if (cirClass.dSuperClasses[superClassSignature] == null)
                            cirClass.dSuperClasses[superClassSignature] = cirDataAnalysis.dCirClass_bySignature[superClassSignature];
                        else
                        {
                            if (cirClass.dSuperClasses[superClassSignature] != cirDataAnalysis.dCirClass_bySignature[superClassSignature])
                            {
                            }
                        }
                    else
                    {
                        cirDataAnalysis.dCirClass_bySignature.Add(superClassSignature, new CirClass(superClassSignature));
                        cirClass.dSuperClasses[superClassSignature] = cirDataAnalysis.dCirClass_bySignature[superClassSignature];
                    }
                    // now map dIsSuperClassedBy
                    cirDataAnalysis.dCirClass_bySignature[superClassSignature].dIsSuperClassedBy.Add(cirClass.Signature, cirClass);


                }
            }

            /*foreach (ICirClass cirClass in cirDataAnalysis.dCirClass_bySignature.Values.ToList())
        foreach (string superClassSignature in cirClass.dSuperClasses.Keys.ToList())
            if (cirClass.dSuperClasses[superClassSignature] == null)
                if (cirDataAnalysis.dCirClass_bySignature.ContainsKey(superClassSignature))
                    cirClass.dSuperClasses[superClassSignature] = cirDataAnalysis.dCirClass_bySignature[superClassSignature];
                else
                {
                    cirDataAnalysis.dCirClass_bySignature.Add(superClassSignature, new CirClass(superClassSignature));
                    cirClass.dSuperClasses[superClassSignature] = cirDataAnalysis.dCirClass_bySignature[superClassSignature];
                }*/
        }

        public static void remapIsCalledByXrefs(ICirDataAnalysis cirDataAnalysis)
        {
            try
            {
                remapSuperClassesXrefs(cirDataAnalysis);
                var timer = new O2Timer("remapIsCalledByXrefs").start();

                // first clear all Xref 

                foreach (var cirFunction in cirDataAnalysis.dCirFunction_bySignature.Values)
                    cirFunction.FunctionIsCalledBy = new List<ICirFunctionCall>();

                // make sure all FunctionsCalledUniqueList and FunctionsCalled are syncronized with dCirFunction_bySignature 
                var functionsToMap = cirDataAnalysis.dCirFunction_bySignature.Values.ToList().Count;
                var functionsProcessed = 0;
                foreach (var cirFunction in cirDataAnalysis.dCirFunction_bySignature.Values.ToList())
                {
                    for (int i = 0; i < cirFunction.FunctionsCalledUniqueList.Count; i++)
                        cirFunction.FunctionsCalledUniqueList[i] = syncFunctions(cirDataAnalysis, cirFunction.FunctionsCalledUniqueList[i]);
                    for (int i = 0; i < cirFunction.FunctionsCalled.Count; i++)
                        cirFunction.FunctionsCalled[i].cirFunction = syncFunctions(cirDataAnalysis, cirFunction.FunctionsCalled[i].cirFunction);
                    if ((functionsProcessed++) % 500 == 0)
                        DI.log.info("  processed {0} / {1}", functionsProcessed, functionsToMap);
                }

                // check the FunctionsCalledUniqueList is calledby mappngs
                foreach (var cirFunction in cirDataAnalysis.dCirFunction_bySignature.Values)
                    foreach (var functionCalled in cirFunction.FunctionsCalled)
                    {
                        var functionCalledXref = getFunctionRef(cirDataAnalysis, functionCalled.cirFunction);
                        /*if (false == cirDataAnalysis.dCirFunction_bySignature.ContainsValue(functionCalled))                    
                            DI.log.error("in remapIsCalledByXrefs something is wrong because the called fucntions does not have a cirFunction mapping: {0}", functionCalled.FunctionSignature);
                        else
                        //{*/
                        bool found = false;
                        foreach (var functionCall in functionCalled.cirFunction.FunctionIsCalledBy)
                            if (functionCall.cirFunction == functionCalledXref)
                                found = true;
                        if (found == false)
                            functionCalled.cirFunction.FunctionIsCalledBy.Add(new CirFunctionCall(cirFunction, functionCalled.fileName, functionCalled.lineNumber));
                        //if (false == functionCalledXref.FunctionIsCalledBy.Contains(cirFunction))
                        //    functionCalledXref.FunctionIsCalledBy.Add(cirFunction);
                    }

                // make sure all functions are syncronized with dCirFunction_bySignature 
                foreach (var cirFunction in cirDataAnalysis.dCirFunction_bySignature.Values)
                    for (int i = 0; i < cirFunction.FunctionsCalledUniqueList.Count; i++)
                        cirFunction.FunctionsCalledUniqueList[i] = syncFunctions(cirDataAnalysis, cirFunction.FunctionsCalledUniqueList[i]);

                // endure all iscalledby are correcly mapped
                foreach (var cirFunction in cirDataAnalysis.dCirFunction_bySignature.Values)
                    for (int i = 0; i < cirFunction.FunctionIsCalledBy.Count; i++)
                        cirFunction.FunctionIsCalledBy[i].cirFunction = syncFunctions(cirDataAnalysis, cirFunction.FunctionIsCalledBy[i].cirFunction);

                // make sure there is a reference to this function on the Classes Dictionanry
                foreach (var cirFunction in cirDataAnalysis.dCirFunction_bySignature.Values)
                    foreach (var functionCalled in cirFunction.FunctionsCalled)
                        if (functionCalled.cirFunction.ParentClass == null)
                        {
                            var functionSignature = new FilteredSignature(functionCalled.cirFunction.FunctionSignature);
                            var parentClassName = functionSignature.sFunctionClass;                            
                            if (false == cirDataAnalysis.dCirClass_bySignature.ContainsKey(parentClassName))
                                cirDataAnalysis.dCirClass_bySignature.Add(parentClassName, new CirClass(parentClassName));
                            var parentCirClass = cirDataAnalysis.dCirClass_bySignature[parentClassName];
                            var functionAlreadyMappedToClass = false;
                            foreach (var cirFunctionMappedToClass in parentCirClass.dFunctions.Values)
                                if (cirFunctionMappedToClass.FunctionSignature == functionCalled.cirFunction.FunctionSignature)
                                    functionAlreadyMappedToClass = true;
                            if (false == functionAlreadyMappedToClass)
                                parentCirClass.dFunctions.Add(functionCalled.cirFunction.FunctionSignature, functionCalled.cirFunction);
                            functionCalled.cirFunction.ParentClass = parentCirClass;
                            functionCalled.cirFunction.ParentClassName = parentCirClass.Name;
                            functionCalled.cirFunction.ParentClassFullName = parentCirClass.FullName;
                        }

                timer.stop();
            }
            catch (Exception ex)
            {
                DI.log.error("in remapIsCalledByXrefs: {0}", ex.Message);
            }

        }

        private static ICirFunction syncFunctions(ICirDataAnalysis cirDataAnalysis, ICirFunction cirFunctionToSync)
        {            
            try
            {
                var functionRef = getFunctionRef(cirDataAnalysis, cirFunctionToSync);
                //var functionCalled = cirFunction.FunctionsCalledSequence[i];

                if (cirFunctionToSync.GetHashCode() != functionRef.GetHashCode())
                    mergeFunctions(cirDataAnalysis, functionRef, cirFunctionToSync);
                //cirFunction.FunctionsCalledSequence[i] = functionRef;            
                return functionRef;
            }
            catch (Exception ex)
            {
                DI.log.error("in CirDataAnalysisUtils.syncFunctions, for function {1} : {1}",
                             cirFunctionToSync.FunctionSignature, ex.Message);
            }
            return cirFunctionToSync;
        }


        public static void calculateXrefs_SuperClases_recursive(ICirDataAnalysis cirDataAnalysis, String sSuperClassCustomSignature, ICirClass ccClassToFollow)
        {
            sSuperClassCustomSignature = String.Format("{0}. <- {1}", sSuperClassCustomSignature,
                                                       ccClassToFollow.Signature.Replace('.', '_'));
            if (false == cirDataAnalysis.lCirClass_bySuperClass.Contains(sSuperClassCustomSignature))
                cirDataAnalysis.lCirClass_bySuperClass.Add(sSuperClassCustomSignature);
            foreach (CirClass ccIsSuperClassedBy in ccClassToFollow.dIsSuperClassedBy.Values)
                calculateXrefs_SuperClases_recursive(cirDataAnalysis, sSuperClassCustomSignature, ccIsSuperClassedBy);
        }


        public static void removeO2CirDataFile(ICirDataAnalysis cirDataAnalysis, String sO2CirDataFileToRemove)
        {
            if (cirDataAnalysis.dCirDataFilesLoaded.ContainsKey(sO2CirDataFileToRemove))
            {
                ICirData fcdCirData = cirDataAnalysis.dCirDataFilesLoaded[sO2CirDataFileToRemove];
                cirDataAnalysis.dCirDataFilesLoaded.Remove(sO2CirDataFileToRemove);

                foreach (ICirClass ccCirClass in fcdCirData.dClasses_bySignature.Values)
                    if (cirDataAnalysis.dCirClass.ContainsKey(ccCirClass))
                    {
                        cirDataAnalysis.dCirClass.Remove(ccCirClass);
                        cirDataAnalysis.dCirClass_bySignature.Remove(ccCirClass.Signature);

                        foreach (ICirFunction cfCirFunction in ccCirClass.dFunctions.Values)
                            if (cirDataAnalysis.dCirFunction.ContainsKey(cfCirFunction))
                            {
                                cirDataAnalysis.dCirFunction.Remove(cfCirFunction);
                                cirDataAnalysis.dCirFunction_bySignature.Remove(cfCirFunction.FunctionSignature);
                            }
                            else
                            {
                            }
                    }
                    else
                    {
                    }
                showStatsOfLoadedData(cirDataAnalysis);
            }
        }

        public static void showStatsOfLoadedData(ICirDataAnalysis cirDataAnalysis)
        {
            DI.log.debug("There are {0} CirData files loaded with {1} classes and {2} functions",
                         cirDataAnalysis.dCirDataFilesLoaded.Count, cirDataAnalysis.dCirClass.Count, cirDataAnalysis.dCirFunction.Count);
        }

        public static ICirDataSearchResult executeSearch(ICirDataAnalysis cirDataAnalysis)
        {
            var fcdSearchResult = new CirDataSearchResult(cirDataAnalysis);
            CirSearch.executeSearch(fcdSearchResult);
            return fcdSearchResult;
        }


        public static void clear(CirDataAnalysis cirDataAnalysis)
        {
            cirDataAnalysis.dCirDataFilesLoaded = new Dictionary<string, ICirData>();
            cirDataAnalysis.dCirClass = new Dictionary<ICirClass, ICirData>();
            cirDataAnalysis.dCirClass_bySignature = new Dictionary<string, ICirClass>();
            cirDataAnalysis.dCirFunction = new Dictionary<ICirFunction, ICirClass>();
            cirDataAnalysis.dCirFunction_bySignature = new Dictionary<string, ICirFunction>();
            cirDataAnalysis.lCirClass_bySuperClass = new List<string>();
        }

        public static void loadFileIntoCirDataAnalysisObject(string sFileToLoad, ICirDataAnalysis cirDataAnalysis)
        {
            loadFileIntoCirDataAnalysisObject(sFileToLoad, cirDataAnalysis, true,true /*useCachedVersionIfAvailable*/ , true /*runRemapXrefs*/);
        }

        public static void loadFileIntoCirDataAnalysisObject(string sFileToLoad, ICirDataAnalysis cirDataAnalysis, bool showNotSupportedExtensionError, bool useCachedVersionIfAvailable, bool runRemapXrefs)
        {
            try
            {
                switch (Path.GetExtension(sFileToLoad).ToLower())
                {
                    case ".cirdata":
                        addO2CirDataFile(cirDataAnalysis, sFileToLoad, useCachedVersionIfAvailable);
                        break;
                    case ".dll":
                    case ".exe":
                        if (CecilUtils.isDotNetAssembly(sFileToLoad, false))
                        {
                            ICirData assemblyCirData = new CirData();
                            new CirFactory().processAssemblyDefinition(assemblyCirData,
                                                                       CecilUtils.getAssembly(sFileToLoad),sFileToLoad);
                            if (assemblyCirData.dClasses_bySignature.Count == 0)
                                DI.log.error("There were no classes imporeted from the file: {0}", sFileToLoad);
                            else
                            {
                                var fileName = Path.GetFileName(sFileToLoad);
                                if (false == cirDataAnalysis.dCirDataFilesLoaded.ContainsKey(fileName))
                                {
                                    cirDataAnalysis.dCirDataFilesLoaded.Add(Path.GetFileName(sFileToLoad),
                                                                            assemblyCirData);
                                }
                                addO2CirDataFile(cirDataAnalysis, assemblyCirData, runRemapXrefs);
                            }
                        }
                        else
                            DI.log.error("Droped *.exe or *.dll file was not a .Net assembly: {0}", sFileToLoad);
                        break;
                    case "*.xml":
                        if (CirLoad.isFileACirDumpFile(sFileToLoad))
                            addO2CirDataFile(cirDataAnalysis, sFileToLoad,useCachedVersionIfAvailable);
                        break;
                    default:
                        if (showNotSupportedExtensionError)
                            DI.log.error(
                                "Could not process file dropped (it it not a CirData file or a .NET assembly: {0}",
                                sFileToLoad);
                        break;
                }

            }
            catch (Exception ex)
            {
                DI.log.error(
                    "in loadFileIntoCirDataAnalysisObject, error {0} while loading {1} : ", sFileToLoad, ex.Message);
            }

        }

        public static List<ICirFunction> getListOfInheritedMethods(ICirClass targetClass, bool ignoreCoreObjectClass)
        {
            var inheritedMethods = new List<ICirFunction>();
            if (targetClass!=null) // && targetClass.Signature != "java.lang.Object")
            {
                foreach (var cirFunction in targetClass.dFunctions.Values)
                    inheritedMethods.Add(cirFunction);
                foreach(var cirClass in targetClass.dSuperClasses.Values)
                    inheritedMethods.AddRange(getListOfInheritedMethods(cirClass, ignoreCoreObjectClass));
            }
            return inheritedMethods;
        }

        public static void saveCirDataAnalysisObjectAsCirDataFile(ICirDataAnalysis cirDataAnalysis, string savedCirDataFile)
        {
            var cirData = createCirDataFromCirDataAnalysis(cirDataAnalysis);
            CirDataUtils.saveSerializedO2CirDataObjectToFile(cirData, savedCirDataFile);            

        }

        public static ICirData createCirDataFromCirDataAnalysis(ICirDataAnalysis cirDataAnalysis)
        {
            if (cirDataAnalysis == null)
                return null;
            var cirData = new CirData
                              {
                                  dClasses_bySignature = cirDataAnalysis.dCirClass_bySignature,
                                  dFunctions_bySignature = cirDataAnalysis.dCirFunction_bySignature,                                  
                              };
            
            return cirData;
        }
    }
}
