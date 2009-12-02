using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using O2.Core.CIR.CirUtils;
using O2.Core.CIR.Xsd;
using O2.DotNetWrappers.DotNet;
using O2.Kernel.Interfaces.CIR;
using O2.Kernel.Interfaces.CIR;

namespace O2.Core.CIR.CirObjects
{
    public class CirDataUtils
    {
        public static void resolveDbId(ICirData cirData)
        {
            if (cirData.lFiles.Count > 0) // && (sDbId == "" || sDbId == null))
                foreach (String sFile in cirData.lFiles)
                {
                    String sResult = CirDumpsUtils.fromFileExtension_get_DbId(Path.GetExtension(sFile));
                    if (!string.IsNullOrEmpty(sResult))
                    {
                        cirData.sDbId = sResult;
                        return;
                    }
                }
        }


        public static void addIsCalledByMappings(ICirData cirData, ICirData fcdO2CirDataToProcess)
        {
            DI.log.debug("in addIsCalledByMappings");

            foreach (CirFunction cfCirFunction in fcdO2CirDataToProcess.dFunctions_bySignature.Values)
                foreach (ICirFunctionCall cirFunctionCalled in cfCirFunction.FunctionsCalled)
                {
                    if (cirData.dFunctions_bySignature.ContainsKey(cirFunctionCalled.cirFunction.FunctionSignature))
                    {
                        if (cirData.dFunctions_bySignature[cirFunctionCalled.cirFunction.FunctionSignature].HasControlFlowGraph)            // not sure about this
                        {
                           /* DI.log.info("Added isCalledByMapping: {0} is called by {1}",
                                        cirData.dFunctions_bySignature[cirFunctionCalled.FunctionSignature].FunctionSignature, cfCirFunction.FunctionSignature);*/
                            cirData.dFunctions_bySignature[cirFunctionCalled.cirFunction.FunctionSignature].FunctionIsCalledBy.Add(cirFunctionCalled);
                        }
                    }
                }
        }

        // this doesn't seem to be working since when I save this back into disk, the size of the .CirData file increases!
/*        public static void removeClassesWithNoControlFlowGraphs(ICirData cirData)
        {
            cirData.dClasses_bySymbolDef.Clear();
            cirData.dFunctions_bySymbolDef.Clear();
            cirData.dSymbols.Clear();
            var signaturesToRemove = new List<String>();
            int originalSignatureCount = cirData.dClasses_bySignature.Keys.Count;
            // remove CirClasses with no ControlFlowGraphs
            foreach (CirClass cirClass in cirData.dClasses_bySignature.Values)
                if (false == cirClass.bClassHasMethodsWithControlFlowGraphs)
                    signaturesToRemove.Add(cirClass.Signature);
            foreach (string signature in signaturesToRemove)
                cirData.dClasses_bySignature.Remove(signature);
            DI.log.info(
                "Originally there were {0} CirClass Objects in CirData file and now there are {1} ({2} were deleted)",
                originalSignatureCount, cirData.dClasses_bySignature.Keys.Count,
                originalSignatureCount - cirData.dClasses_bySignature.Keys.Count);

            // remove CirFunctions with no ControlFlowGraphs
            signaturesToRemove.Clear();
            originalSignatureCount = cirData.dFunctions_bySignature.Values.Count;
            foreach (CirFunction cirFunction in cirData.dFunctions_bySignature.Values)
                if (false == cirFunction.HasControlFlowGraph)
                    signaturesToRemove.Add(cirFunction.FunctionSignature);
            foreach (string signature in signaturesToRemove)
                cirData.dFunctions_bySignature.Remove(signature);
            DI.log.info(
                "Originally there were {0} CirFunction Objects in CirData file and now there are {1} ({2} were deleted)",
                originalSignatureCount, cirData.dFunctions_bySignature.Keys.Count,
                originalSignatureCount - cirData.dFunctions_bySignature.Keys.Count);
        }*/

        public static void saveSerializedO2CirDataObjectToFile(ICirData cirData, String sTargetFile)
        {
            if (cirData == null)
                return;

            if (sTargetFile == "")
                sTargetFile = DI.config.TempFileNameInTempDirectory + ".xml";

            try
            {
                var bfBinaryFormatter = new BinaryFormatter();

                var fsFileStream = new FileStream(sTargetFile, FileMode.Create);
                bfBinaryFormatter.Serialize(fsFileStream, cirData);
                fsFileStream.Close();
                DI.log.debug("Serialized O2CirData Saved to: {0}", sTargetFile);
            }
            catch (Exception ex)
            {
                DI.log.error("In saveSerializedO2CirDataObjectToFile: {0}", ex.Message);
            }
        }

        public static void populateDictionariesWithXrefs(CommonIRDump cidCirDump, ICirData cirData)
        {
            try
            {
                if (cidCirDump == null)
                    return;

                O2Timer tTimer = new O2Timer("Populate Dictionaries With Xrefs").start();

                // files
                addCompilationUnitFiles(cirData,cidCirDump.CommonIR.CompilationUnit);
                
                //addSymbolsFromCirDump_Functions(cirData,cidCirDump.CommonIR.FunctionSigType);

                // calculate the classes

                if (cidCirDump.CommonIR.ClassMethods != null)
                    foreach (CommonIRDumpCommonIRClassMethods cmClass in cidCirDump.CommonIR.ClassMethods)
                    {
                        ICirClass ccCirClass = addClass(cirData,cmClass);

                        // process static methods
                        if (null != cmClass.ClassStaticFunction)
                            foreach (
                                CommonIRDumpCommonIRClassMethodsClassStaticFunction csfStaticFunction in
                                    cmClass.ClassStaticFunction)
                            {
                                addFunction(cirData,csfStaticFunction, ccCirClass);
                            }

                        // process member methods (i.e. non static)
                        if (null != cmClass.ClassMemberFunction)
                            foreach (
                                CommonIRDumpCommonIRClassMethodsClassMemberFunction cmfMemberFunction in
                                    cmClass.ClassMemberFunction)
                            {
                                addFunction(cirData,cmfMemberFunction, ccCirClass);
                            }
                    }
                // calculate the NonClassFunction

                if (cidCirDump.CommonIR.NonClassFunction != null)
                {
                    var signatureForNoClassDef = "<no class def>";
                    var cirNoClass = new CirClass(signatureForNoClassDef);
                    cirData.dClasses_bySignature.Add(signatureForNoClassDef, cirNoClass);
                    foreach (
                        CommonIRDumpCommonIRNonClassFunction ncfNonClassFunction in cidCirDump.CommonIR.NonClassFunction
                        )
                    {
                        /*if (ncfNonClassFunction.UniqueID == "VB6_Builtin.StringConcat(...):void")
                        {
                        }
                        DI.log.debug("in populateDictionariesWithXrefs, adding NonClassdefFunction:{0}", ncfNonClassFunction.UniqueID);
                        */

                        addFunction(cirData, ncfNonClassFunction, cirNoClass);



                        // fcdCirData.addFunction(ncfNonClassFunction, null);
                    }
                }

                //   resolveIsCalledByXrefsProbs();

                // add this here since the Functions and classes signatures should be resolved by now
                addSymbolsFromCommonIr(cirData, cidCirDump.CommonIR);
                mapClassesMetadata(cirData, cidCirDump.SymbolData);
                mapFunctionsMetadata(cirData, cidCirDump.CommonIR.FunctionSigType);                

                resolveDbId(cirData);
                if (cirData.bVerbose)
                    tTimer.stop();
            }
            catch (Exception ex)
            {
                DI.log.error("in populateDictionariesWithXrefs: {0}", ex.Message);
            }
        }

        // this might not be neeed
        /*        private void resolveIsCalledByXrefsProbs()
                {
                    //     O2Timer tTimer = new O2Timer("resolveIsCalledByXrefsProbs").start();

                    // first I have to build an Xref table with the functions names:
                    / *     Dictionary<String, String> dFunctionsNames = new Dictionary<string, string>();
                         foreach (CirFunction cfCirFunction in this.dFunctions.Values)
                             if (false == dFunctionsNames.ContainsKey(cfCirFunction.FunctionSignature))
                                 dFunctionsNames.Add(cfCirFunction.FunctionSignature, cfCirFunction.SymbolDef);
                         * /

                    var lIsCalledBy_Resolved = new List<string>();
                    foreach (CirFunction cfCirFunction in dFunctions_bySignature.Values)
                    {
                        if (cfCirFunction.HasControlFlowGraph)
                        {
                            //String ss = getSymbol(cfCirFunction.SymbolDef);
                            foreach (String sSignatureOfFunctionCalled in cfCirFunction.FunctionsCalledUniqueList)
                            {
                                //String sSignatureOfFunctionCalled = getSymbol(sSymbolRef);
                                // if (FunctionSignature == CONST_NEED_SIGNATURE)
                                // { }
                                //if (dFunctionsNames.ContainsKey(sSignatureOfFunctionCalled))
                                //{
                                //String sCalledFunctionSymbolRef = dFunctionsNames[sSignatureOfFunctionCalled];
                                if (dFunctions_bySignature.ContainsKey(sSignatureOfFunctionCalled))
                                {
                                    CirFunction cfCalledFunction = dFunctions_bySignature[sSignatureOfFunctionCalled];
                                    if (false == cfCalledFunction.FunctionIsCalledBy.Contains(sSignatureOfFunctionCalled))
                                        cfCalledFunction.FunctionIsCalledBy.Add(sSignatureOfFunctionCalled);
                                }
                                else
                                {
                                    DI.log.error(" ERROR: could not resolve Signature: {0}", sSignatureOfFunctionCalled);
                                    lIsCalledBy_Resolved.Add(String.Format(" ERROR: could not resolve Signature: {0}",
                                                                           sSignatureOfFunctionCalled));
                                }
                            }
                        }
                    }
                    //       if (this.bVerbose)
                    //           tTimer.stop();
                }
                */

        public static void addCompilationUnitFiles(ICirData cirData, CommonIRDumpCommonIRCompilationUnit[] cuCompilationUnit)
        {
            // calculate files
            if (cuCompilationUnit != null)
                foreach (CommonIRDumpCommonIRCompilationUnit cidCompilationUnit in cuCompilationUnit)
                    if (cirData.lFiles.Contains(cidCompilationUnit.File.Path) == false)
                        cirData.lFiles.Add(cidCompilationUnit.File.Path);
        }

        public static void addSymbolsFromCommonIr(ICirData cirData, CommonIRDumpCommonIR ciCommonIr)
        {
          /*  if (ciCommonIr.ClassMethods !=null)
                foreach (var classMethod in ciCommonIr.ClassMethods)
                {
                    addSymbol(cirData, classMethod.SymbolRef, classMethod.ClassType);
                    if (classMethod.ClassMemberFunction != null)
                        foreach (var memberFunction in classMethod.ClassMemberFunction)
                            addSymbol(cirData, memberFunction.SymbolRef, memberFunction.UniqueID);
                    if (classMethod.ClassStaticFunction != null)
                        foreach (var staticFunction in classMethod.ClassStaticFunction)
                            addSymbol(cirData, staticFunction.SymbolRef, staticFunction.UniqueID);
                }*/
            // using the changed name (bool instead of Boolean, etc.) to match other references in SymbolRef (check if this is Java dependent)
            addHardCodedSymbol(cirData,ciCommonIr.Boolean, "bool");
            addHardCodedSymbol(cirData, ciCommonIr.Char16, "wchar_t");
            addHardCodedSymbol(cirData, ciCommonIr.Char8, "char");
            addHardCodedSymbol(cirData, ciCommonIr.Float32, "float");
            addHardCodedSymbol(cirData, ciCommonIr.Float64, "double");
            addHardCodedSymbol(cirData, ciCommonIr.SignedInt16, "short");
            addHardCodedSymbol(cirData, ciCommonIr.SignedInt32, "int");
            addHardCodedSymbol(cirData, ciCommonIr.SignedInt64, "long");
            addHardCodedSymbol(cirData, ciCommonIr.SignedInt8, "char");
            addHardCodedSymbol(cirData, ciCommonIr.Void, "void");
            //addSymbol(ciCommonIr.Char16SymbolDef, "Boolean");
        }

        private static void addHardCodedSymbol(ICirData cirData, object oSymbolData, String sSymbolName)
        {
            if (oSymbolData == null)
                return;
            String sSymbolDef = DI.reflection.getProperty("SymbolDef", oSymbolData).ToString();
            addSymbol(cirData, sSymbolDef, sSymbolName);
            Object oPointerType = DI.reflection.getProperty("PointerType", oSymbolData, false);
            if (oPointerType != null)
            {
                sSymbolDef = DI.reflection.getProperty("SymbolDef", oPointerType).ToString();
                String sPrintableType = DI.reflection.getProperty("PrintableType", oPointerType).ToString();
                addSymbol(cirData, sSymbolDef, String.Format("{0}*", sPrintableType));
            }
        }

        public static void mapClassesMetadata(ICirData cirData, CommonIRDumpClassSymbols[] acsClassSymbols)
        {
            if (acsClassSymbols != null)
                foreach (CommonIRDumpClassSymbols csClassSymbol in acsClassSymbols)
                {
                    //addSymbol(cirData, csClassSymbol.SymbolDef, csClassSymbol.UniqueID);
                    //dSymbols_Class.Add(csClassSymbol.SymbolDef, csClassSymbol);
                    addClass(cirData,csClassSymbol);
                }
        }

        public static void mapFunctionsMetadata(ICirData cirData, CommonIRDumpCommonIRFunctionSigType[] afsSymbols)
        {
            if (afsSymbols != null)
            {
                // first get all current mappings on afsSymbols;
                var tempCirFunctions = new Dictionary<string,ICirFunction>();
                foreach (CommonIRDumpCommonIRFunctionSigType fsSymbol in afsSymbols)
                {
                    //dSymbols.Add(fsSymbol.SymbolDef, fsSymbol.UniqueID);      // fsSymbol doesn't have the signature just the arguments and returntype
                    //                        dSymbols_Function.Add(fsSymbol.SymbolDef, fsSymbol);                        
                    tempCirFunctions.Add(fsSymbol.SymbolDef, createTempCirFunctionWithReturnTypeAndArguments(cirData, fsSymbol));
                }
                // then map them to the current functions
                foreach(var cirFunction in cirData.dFunctions_bySignature.Values)
                {
                    if (tempCirFunctions.ContainsKey(cirFunction.SymbolDef))
                    {
                        cirFunction.ReturnType = tempCirFunctions[cirFunction.SymbolDef].ReturnType;
                        cirFunction.FunctionParameters = tempCirFunctions[cirFunction.SymbolDef].FunctionParameters;                        
                    }
                }
            }
        }


        public static void addSymbol(ICirData cirData, String sSymbolRef, String sSignature)
        {
            if (cirData.dSymbols.ContainsKey(sSymbolRef))
            {
                if (cirData.dSymbols[sSymbolRef] == DI.CONST_NEED_SIGNATURE)
                    // case when the sSymbol is populated using the FunctionSigType
                    cirData.dSymbols[sSymbolRef] = sSignature;
                else if (cirData.dSymbols[sSymbolRef] != sSignature)
                    DI.log.error(
                        "dSymbols already contained Symbol but the signatures don't match: dSymbols[{0}]{1} != {2}",
                        sSymbolRef, cirData.dSymbols[sSymbolRef], sSignature);
            }
            else
                cirData.dSymbols.Add(sSymbolRef, sSignature);
        }

        public static String getSymbol(ICirData cirData, String sSymbolRef)
        {
            if (cirData.dSymbols.ContainsKey(sSymbolRef))
                return cirData.dSymbols[sSymbolRef];
            if (cirData.bVerbose)
                DI.log.error("Requested symbol not found: {0}", sSymbolRef);
            return sSymbolRef;
        }

        public static ICirClass getClass(ICirData cirData, String sSymbolRef, String sSignature)
        {
            if (cirData.dClasses_bySignature.ContainsKey(sSignature))
                return cirData.dClasses_bySignature[sSignature];

            return addClass(cirData, sSymbolRef, sSignature);
        }

        public static ICirFunction getFunction_bySignature(ICirData cirData, String sSymbolRef, String sSignature)
        {
            if (cirData.dFunctions_bySignature.ContainsKey(sSignature))
            {
                //if (dFunctions[sSymbolRef].FunctionSignature != FunctionSignature)
                //    DI.log.error("in getFunction signatures don't match but the signatures don't match: dFunctions[sSymbolRef].FunctionSignature != FunctionSignature : ", dFunctions[sSymbolRef].FunctionSignature, FunctionSignature);
                return cirData.dFunctions_bySignature[sSignature];
            }
            return addFunction(cirData, sSymbolRef, sSignature);
        }

        public static ICirFunction createTempCirFunctionWithReturnTypeAndArguments(ICirData cirData, CommonIRDumpCommonIRFunctionSigType fsSymbol)
        {
            //String sSymbolDef = fsSymbol.SymbolDef;
            ICirFunction cfCirFunction = new CirFunction {};
           
            //ICirFunction cfCirFunction;
           /* var functionSignature = getSymbol(cirData, sSymbolDef);

            if (cirData.dTemp_Functions_bySymbolDef.ContainsKey(sSymbolDef))
                cfCirFunction = cirData.dTemp_Functions_bySymbolDef[sSymbolDef];                
            else
            {
                cfCirFunction = new CirFunction { SymbolDef = sSymbolDef };
                cirData.dFunctions_bySignature.Add(functionSignature, cfCirFunction);
                cirData.dTemp_Functions_bySymbolDef.Add(sSymbolDef, cfCirFunction);
                //addSymbol(cirData, sSymbolDef, DI.CONST_NEED_SIGNATURE); // create an SymbolRef with no signature
            }
            /*if (cirData.dFunctions_bySymbolDef.ContainsKey(sSymbolDef))            
                cfCirFunction = cirData.dFunctions_bySymbolDef[sSymbolDef];                
            else
            {
                cfCirFunction = new CirFunction { SymbolDef = sSymbolDef };
                cirData.dFunctions_bySignature.Add(functionSignature, cfCirFunction);
                cirData.dFunctions_bySymbolDef.Add(sSymbolDef, cfCirFunction);
                //addSymbol(cirData, sSymbolDef, DI.CONST_NEED_SIGNATURE); // create an SymbolRef with no signature
            }*/
            //if (cfCirFunction.ReturnType == "") // check if these values have been set
            //{
                //cfCirFunction.ReturnType = fsSymbol.ReturnType.SymbolRef;
                cfCirFunction.ReturnType = fsSymbol.ReturnType.PrintableType;            
                //addSymbol(cirData, fsSymbol.ReturnType.SymbolRef, fsSymbol.ReturnType.PrintableType);
                if (fsSymbol.ArgumentTypes != null)
                    foreach (CommonIRDumpCommonIRFunctionSigTypeArgument fsArgumentType in fsSymbol.ArgumentTypes)
                    {
                        if (false == ViewHelpers.getCirParameterTypeStringList(cfCirFunction.FunctionParameters).Contains(fsArgumentType.SymbolRef))
                        // if there are probs with more arguments than reality, i will need to add 
                        // a more thorough check here (one that handles the differences between the first time data is added (which makes it an 'add' ) 
                        // and the 2nd time (which makes it an 'confirm that they are the same' )
                        {
                            cfCirFunction.FunctionParameters.Add(new CirFunctionParameter { ParameterName = "", ParameterType = fsArgumentType.PrintableType });//fsArgumentType.SymbolRef});
                            //addSymbol(cirData, fsArgumentType.SymbolRef, fsArgumentType.PrintableType);
                        }
                    }
            //}
            /*else if (cfCirFunction.ReturnType != fsSymbol.ReturnType.SymbolRef)
                // double check that nothing major is wrong, since these values should match
                DI.log.error(
                    "in addToFunction_ArgumentsAndReturnType, cfCirFunction.ReturnType != fsSymbol.ReturnType.SymbolRef: {0} != {1}",
                    cfCirFunction.ReturnType, fsSymbol.ReturnType.SymbolRef);*/
            return cfCirFunction;
        }

        public static ICirClass addClass(ICirData cirData, String sSymbolRef, String sSignature)
        {
            if (cirData.dClasses_bySignature.ContainsKey(sSignature))
            {
                DI.log.error("in addClass, class already exists: {0}: {1}", sSymbolRef, getSymbol(cirData, sSymbolRef));
                return cirData.dClasses_bySignature[sSignature];
            }
            var ccCirClass = new CirClass { SymbolDef = sSymbolRef, Signature = sSignature };
            cirData.dClasses_bySignature.Add(ccCirClass.Signature, ccCirClass);
            addSymbol(cirData,sSymbolRef, sSignature);
            return ccCirClass;
        }

        public static ICirClass addClass(ICirData cirData, CommonIRDumpClassSymbols csClassSymbols)
        {
            ICirClass ccCirClass;
            if (cirData.dClasses_bySignature.ContainsKey(csClassSymbols.UniqueID))
                // check if I need to check for new data (i.e. data that is not already in dClasses.ContainsKey(csClassSymbols.UniqueID)
                ccCirClass = cirData.dClasses_bySignature[csClassSymbols.UniqueID];
            else
            {
                ccCirClass = new CirClass
                {
                    Signature = csClassSymbols.UniqueID,
                    SymbolDef = csClassSymbols.SymbolDef
                };
                cirData.dClasses_bySignature.Add(ccCirClass.Signature, ccCirClass);
                addSymbol(cirData, ccCirClass.SymbolDef, ccCirClass.Signature);
            }

            // add SuperClasses
            if (ccCirClass.dSuperClasses.Count ==0)
                mapSuperClasses(cirData, csClassSymbols, ccCirClass);

            //if (ccCirClass.Name != "")
            //    return ccCirClass;
            
            ccCirClass.IsAnonymous = csClassSymbols.Anonymous;
            ccCirClass.Name = csClassSymbols.Name;

            
            /*if (csClassSymbols.Superclasses != null)
                foreach (CommonIRDumpClassSymbolsSuperclass csSuperClass in csClassSymbols.Superclasses)
                    ccNewCirClass.dSuperClasses.Add(csSuperClass.SymbolRef, csSuperClass.ClassType);*/

            // add class fields
            if (csClassSymbols.ClassFields != null && ccCirClass.dField_Class.Count == 0)
                foreach (CommonIRDumpClassSymbolsClassField csClassField in csClassSymbols.ClassFields)
                {
                    var fsFieldClass = new FieldClass
                    {
                        GuaranteedInitBeforeUsed = csClassField.GuaranteedInitBeforeUsed,
                        Name = csClassField.Name,
                        PrintableType = csClassField.PrintableType,
                        SymbolRef = csClassField.SymbolRef,
                        Signature = csClassField.UniqueID
                    };

                    foreach (PropertyInfo pProperty in csClassField.GetType().GetProperties())
                    {
                        if (pProperty.Name != "GuaranteedInitBeforeUsed" && pProperty.Name != "Name" &&
                            pProperty.Name != "PrintableType" && pProperty.Name != "SymbolRef" &&
                            pProperty.Name != "UniqueID")
                        {
                            object oPropertyObject = DI.reflection.getProperty(pProperty.Name, csClassField, false);
                            if (oPropertyObject != null)
                            {
                                object oPropertyValue = DI.reflection.getProperty("Value", oPropertyObject, false);
                                if (oPropertyValue != null)
                                    fsFieldClass.dFieldData.Add(pProperty.Name, oPropertyValue.ToString());
                                else
                                    fsFieldClass.dFieldData.Add(pProperty.Name, oPropertyObject.ToString());
                            }
                        }
                    }
                    ccCirClass.dField_Class.Add(fsFieldClass.Signature, fsFieldClass);
                }
            // add member fields
            if (csClassSymbols.MemberFields != null && ccCirClass.dField_Member.Count == 0)
                foreach (CommonIRDumpClassSymbolsMemberField csMemberField in csClassSymbols.MemberFields)
                {
                    var fsFieldMember = new FieldMember
                    {
                        Name = csMemberField.Name,
                        PrintableType = csMemberField.PrintableType,
                        SymbolRef = csMemberField.SymbolRef
                    };
                    ccCirClass.dField_Member.Add(fsFieldMember.Name, fsFieldMember);
                    addSymbol(cirData, fsFieldMember.SymbolRef, fsFieldMember.PrintableType);
                }


            return ccCirClass;
        }

        public static ICirClass addClass(ICirData cirData, CommonIRDumpCommonIRClassMethods cmClass)
        {
            if (cirData.dClasses_bySignature.ContainsKey(cmClass.ClassType))
                return cirData.dClasses_bySignature[cmClass.ClassType];


            var ccNewCirClass = new CirClass { Signature = cmClass.ClassType, SymbolDef = cmClass.SymbolRef };
            cirData.dClasses_bySignature.Add(ccNewCirClass.Signature, ccNewCirClass);

            addSymbol(cirData, cmClass.SymbolRef, cmClass.ClassType);
            return ccNewCirClass;
        }

        public static ICirFunction addFunction(ICirData cirData, String sSymbolRef, String sSignature)
        {
            if (cirData.dFunctions_bySignature.ContainsKey(sSignature))
            {
                // if (dSymbols[sSymbolRef] == CONST_NEED_SIGNATURE)
                //     dSymbols[sSymbolRef] = FunctionSignature;
                return cirData.dFunctions_bySignature[sSignature];
            }

            var cfCirFunction = new CirFunction { SymbolDef = sSymbolRef, FunctionSignature = sSignature };
            addSymbol(cirData, sSymbolRef, sSignature);
            cirData.dFunctions_bySignature.Add(sSignature, cfCirFunction);
            //mapFunctionCall(cirData, cfCirFunction, sSymbolRef, sSignature);
            return cfCirFunction;
        }

        public static ICirFunction addFunction(ICirData cirData, Object oFunction, ICirClass ccCirClass)
        {
            ICirFunction cfCirFunction = null;
            try
            {
                String sSignature = DI.reflection.getProperty("UniqueID", oFunction).ToString();
                String sSymbolRef = DI.reflection.getProperty("SymbolRef", oFunction).ToString();

                if (cirData.dFunctions_bySignature.ContainsKey(sSignature))
                    cfCirFunction = cirData.dFunctions_bySignature[sSignature];

                if (cfCirFunction == null)
                {
                    // create method object
                    cfCirFunction = new CirFunction { SymbolDef = sSymbolRef };
                    // ccCirClass.dFunctions.Add(sSymbolRef, cfCirFunction);
                    cirData.dFunctions_bySignature.Add(sSignature, cfCirFunction);
                    //cirData.dTemp_Functions_bySymbolDef.Add(sSymbolRef, cfCirFunction);
                    //addSymbol(cirData,sSymbolRef, sSignature);                    
                    

                    /*if (cirData.dFunctions_bySymbolDef.ContainsKey(sSymbolRef))
                    {
                        cfCirFunction.FunctionParameters = cirData.dFunctions_bySymbolDef[sSymbolRef].FunctionParameters;
                        cfCirFunction.ReturnType = cirData.dFunctions_bySymbolDef[sSymbolRef].ReturnType;       // _TODO: _BUG: I think there is a bug here where the ReturnType is not correctly set for ALL funtions
                    }*/
                }
                cfCirFunction.ParentClass = ccCirClass;

                if (cfCirFunction.FunctionSignature != "" && cfCirFunction.FunctionSignature != sSignature) // error
                {
                    DI.log.error("In addFunction cfCirFunction.FunctionSignature != FunctionSignature  : {0} {1} != {2}",
                                 sSymbolRef, cfCirFunction.FunctionSignature, sSignature);
                    return cfCirFunction;
                }

                //if (ccCirClass.dFunctions.ContainsKey(sSymbolRef))   // means we already processed this method
                //    return cfCirFunction;


                //cmNewcirFunction.FunctionSignature = cmfMemberFunction.UniqueID;                    
                // there are generic for both Static and Member Functions so we can use reflection to get them
                cfCirFunction.FunctionSignature = sSignature;
                //   addSymbol(sSymbolRef, FunctionSignature);          // this will make sure the dSymbol href table is correctly populated
                //cfCirFunction.sSymbolRef = sSymbolRef;
                // add Variables
                processFunctionVariables(cirData,cfCirFunction, oFunction);

                // process ControlFlowGraphs
                if (processControlFlowGraph(cirData,cfCirFunction, oFunction))
                    if (null != ccCirClass)
                        ccCirClass.bClassHasMethodsWithControlFlowGraphs = true;
                // keep track if this class has methods with ControlFlowGraphs                    

                // add method to dClasses   
                if (null != ccCirClass && false == ccCirClass.dFunctions.ContainsKey(sSignature))
                {
                    ccCirClass.dFunctions.Add(sSignature, cfCirFunction);
                }
            }
            catch (Exception ex)
            {
                DI.log.error("in addFunction: {0}", ex.Message);
            }
            return cfCirFunction;
        }

        public static void mapSuperClasses(ICirData cirData, CommonIRDumpClassSymbols csClassSymbols, ICirClass ccNewCirClass)
        {
            if (csClassSymbols.Superclasses != null)
                foreach (CommonIRDumpClassSymbolsSuperclass csSuperClass in csClassSymbols.Superclasses)
                {
                    // add SuperClasses to current Class
                    ccNewCirClass.dSuperClasses.Add(csSuperClass.SymbolRef,
                                                    getClass(cirData,csSuperClass.SymbolRef, csSuperClass.ClassType));
                    // add XRef (i.e. add current class to the SuperClass d...)
                    ICirClass ccCirClass = getClass(cirData,csSuperClass.SymbolRef, csSuperClass.ClassType);
                    ccCirClass.dIsSuperClassedBy.Add(ccNewCirClass.SymbolDef, ccNewCirClass);
                }
        }

        public static void mapFunctionCall(ICirData cirData, ICirFunction cfCirFunction, String sSymbolRef, String sSignature)
        {            
            ICirFunction cfCalledCirFunction = getFunction_bySignature(cirData, sSymbolRef, sSignature);

            // add 'functions called' mapping
            if (false == cfCirFunction.FunctionsCalledUniqueList.Contains(cfCalledCirFunction))
                cfCirFunction.FunctionsCalledUniqueList.Add(cfCalledCirFunction);
            
            // map the FunctionCalled and FunctionIsCalledBy
            var cirFunctionCall = new CirFunctionCall(cfCalledCirFunction);
            cfCirFunction.FunctionsCalled.Add(cirFunctionCall);
            //if (false == cfCalledCirFunction.FunctionIsCalledBy.Contains(cfCirFunction))
            cfCalledCirFunction.FunctionIsCalledBy.Add(cirFunctionCall);
            /*
            if (false == cfCalledCirFunction.FunctionIsCalledBy.Contains(FunctionSignature))
            {

                cfCalledCirFunction.FunctionIsCalledBy.Add(FunctionSignature);
            }*/
        }

        public static void processFunctionVariables(ICirData cirData,ICirFunction cfCirFunction, Object oFunction)
        {
            if (cfCirFunction.dVariables.Count > 0) // only do this once
                return;
            Object oVariables = DI.reflection.getProperty("Variable", oFunction);
            if (oVariables != null)
            {
                var aVariables = (Variable[])oVariables;
                foreach (Variable vVariable in aVariables)
                {
                    //this.addSymbol(vVariable.SymbolDef, vVariable.UniqueID);
                    //     this.addSymbol(vVariable.SymbolRef, vVariable.PrintableType);           // also add this mapping since there doesn't seem to be a SymbolDef for this symbol (and they all seem to be the same)
                    bool oldVerbose = cirData.bVerbose; // so that we don't get a pile of errors for Symbold not defined
                    cirData.bVerbose = false;
                    String sDef = getSymbol(cirData, vVariable.SymbolDef);
                    String sRef = getSymbol(cirData, vVariable.SymbolRef);
                    cirData.bVerbose = oldVerbose;
                    cfCirFunction.dVariables.Add(vVariable.SymbolDef,
                                                 new FunctionVariable(vVariable, sRef, sDef));
                }
            }
        }

        public static bool processControlFlowGraph(ICirData cirData, ICirFunction cfCirFunction, Object oFunction)
        {
            if (cfCirFunction.HasControlFlowGraph) // only do this once
                return true;
            Object oControlFlowGraph = DI.reflection.getProperty("ControlFlowGraph", oFunction);
            if (oControlFlowGraph != null)
            {
                // cast ControlFlowGraph object
                cfCirFunction.HasControlFlowGraph = true;
                var cfgControlFlowGraph = (ControlFlowGraph)oControlFlowGraph;
                // add SsaVariables Objects
                if (cfgControlFlowGraph.SsaVariable != null)
                    foreach (ControlFlowGraphSsaVariable cfgSsaVariable in cfgControlFlowGraph.SsaVariable)
                    {
                        // this.addSymbol(cfgSsaVariable.SymbolDef, cfgSsaVariable.Name);                  // I can't ADD this data since the SymbolDef is not unique

                        addSymbol(cirData, cfgSsaVariable.SymbolRef, cfgSsaVariable.PrintableType);
                        // also add this mapping since there doesn't seem to be a SymbolDef for this symbol (and they all seem to be the same)
                        cfCirFunction.dSsaVariables.Add(cfgSsaVariable.SymbolDef,
                                                        new SsaVariable(cfgSsaVariable));
                    }
                // populate BasicBlocks Object
                if (cfgControlFlowGraph.BasicBlock != null)
                    foreach (ControlFlowGraphBasicBlock cfgBasicBlock in cfgControlFlowGraph.BasicBlock)
                    {
                        if (cirData.bStoreControlFlowBlockRawDataInsideCirDataFile)
                            ((CirFunction)cfCirFunction).lcfgBasicBlocks.Add(cfgBasicBlock);

                        // find methods called in ControlFlowBlock
                        if (null != cfgBasicBlock.Items)
                        {
                            Object[] oBlocks = cfgBasicBlock.Items;
                            foreach (Object oBlock in oBlocks)
                                fromBasicBlock_populateDictionaryWithCalls_Recursive(cirData, oBlock, cfCirFunction);
                        }
                    }


                fromControlFlowGraph_resolveVariablesUsedInFunction(cirData, cfCirFunction);
            }
            else
                cfCirFunction.HasControlFlowGraph = false;
            return cfCirFunction.HasControlFlowGraph;
        }

        // not the best way to discover these calls , but this recursive search seems to work quite well
        public static void fromBasicBlock_populateDictionaryWithCalls_Recursive(ICirData cirData, Object oBlock, ICirFunction cfCirFunction)
        {
            String sSymbolRef;
            if (oBlock == null)
                return;
            // look for a match for 'call' in the current object
            if (oBlock.GetType().Name.ToUpper().IndexOf("CALL") > -1)
            {
                sSymbolRef = DI.reflection.getProperty("SymbolRef", oBlock).ToString();

                mapFunctionCall(cirData, cfCirFunction, sSymbolRef, DI.reflection.getProperty("FunctionName", oBlock).ToString());
                //cfCirFunction.dInCode_MethodsCalled.Add(sSymbolRef, Reflection.getProperty("FunctionName", oBlock).ToString());
                return;
            }
            // if it is not on the curret object, lets look in its properties
            PropertyInfo[] pProperties = oBlock.GetType().GetProperties();
            foreach (PropertyInfo pProperty in pProperties)
            {
                //     DI.log.info(oBlock.GetType().Name + " . " + pProperty.Name);
                // get property live  object
                Object oLiveObjectOfProperty = DI.reflection.getProperty(pProperty.Name, oBlock);
                if (null != oLiveObjectOfProperty) // if it is not null
                    if (oLiveObjectOfProperty.GetType().IsArray) // if it is an array seach on them
                    {
                        foreach (Object oObject in (Object[])oLiveObjectOfProperty)
                            fromBasicBlock_populateDictionaryWithCalls_Recursive(cirData, oObject, cfCirFunction);
                    }
                    else // look for a match for 'call' in the current property
                    {
                        if (pProperty.Name.ToUpper().IndexOf("CALL") > -1)
                        {
                            //                DI.log.info(oBlock.GetType().Name + "  . **   " + pProperty.Name);
                            sSymbolRef = DI.reflection.getProperty("SymbolRef", oLiveObjectOfProperty).ToString();
                            mapFunctionCall(cirData, cfCirFunction, sSymbolRef,
                                            DI.reflection.getProperty("FunctionName", oLiveObjectOfProperty).ToString());
                            //cfCirFunction.dInCode_MethodsCalled.Add(sSymbolRef, Reflection.getProperty("FunctionName", oLiveObjectOfProperty).ToString());
                        }
                    }
            }
        }

        // not 100% with the data created by these
        public static void fromControlFlowGraph_resolveVariablesUsedInFunction(ICirData cirData, ICirFunction cfCirFunction)
        {
            foreach (SsaVariable cmSsaVariable in cfCirFunction.dSsaVariables.Values)
                if (false == cfCirFunction.UsedTypes.Contains(cmSsaVariable.sSymbolRef))
                    cfCirFunction.UsedTypes.Add(cmSsaVariable.sSymbolRef);

            foreach (FunctionVariable fvVariable in cfCirFunction.dVariables.Values)
                if (false == cfCirFunction.UsedTypes.Contains(fvVariable.sSymbolRef))
                    cfCirFunction.UsedTypes.Add(fvVariable.sSymbolRef);
        }


        // these Functions contains multiple analysis on o2CirDump data            
        /*   public enum searchFilter
           {
               byClassName,
               byFunctionName,
               byParameterName
           }*/

        public static Dictionary<String, ICirFunction> analysis_getFunctionsThatHaveACallPathIntoFunction(
            CirFunction cfTargetFunction)
        {
            return analysis_getFunctionsThatHaveACallPathIntoFunction(cfTargetFunction, "");
        }

        public static Dictionary<String, ICirFunction> analysis_getFunctionsThatHaveACallPathIntoFunction(
            CirFunction cfTargetFunction, String sParameterFilter)
        {
            var dMatches = new Dictionary<string, ICirFunction>();
            return dMatches;
        }

        public static Dictionary<String, ICirClass> analysis_getFunctionsThatMatchFilter(CirData cirData, 
            String sSuperClass, String sClassName, String sFunctionName, String sParameterType,
            String sMakesCallsTo, String sRemoveMakesCallsTo,
            bool bOnlyProcessFunctionsWithControlFlowGraph, bool _verbose)
        {
            cirData.bVerbose = _verbose;
            var dMatches = new Dictionary<string, ICirClass>();
            if (cirData.dClasses_bySignature == null)
            {
                DI.log.debug("in analysis_getFunctionsThatMatchFilter: this.dClasses_bySignature == null ");
                return dMatches;
            }
            sClassName = sClassName.ToUpper();
            sFunctionName = sFunctionName.ToUpper();
            sParameterType = sParameterType.ToUpper();

            //  Filter By ClassName                
            foreach (ICirClass ccCirClass in cirData.dClasses_bySignature.Values)
            {
                if (bOnlyProcessFunctionsWithControlFlowGraph == false ||
                    ccCirClass.bClassHasMethodsWithControlFlowGraphs)
                {
                    if (sClassName == "")
                        dMatches.Add(ccCirClass.Signature, ccCirClass.clone_SimpleMode());
                    //  add all methods  (as clones)
                    else if (ccCirClass.Signature.ToUpper().IndexOf(sClassName) > -1)
                        // case insensive search                    
                        dMatches.Add(ccCirClass.Signature, ccCirClass.clone_SimpleMode());
                }
            }

            // handle NonClassFunction  (this code bellow is the wrong way to do this)
            /*

            CirClass ccCirClass_NonClassFunction = new CirClass();
            ccCirClass_NonClassFunction.bClassHasMethodsWithControlFlowGraphs = true;
            ccCirClass_NonClassFunction.SymbolDef = "ccCirClass_NonClassFunction";
            ccCirClass_NonClassFunction.FunctionSignature = "ccCirClass_NonClassFunction";
            foreach (CirFunction cfCirFunction in dFunctions.Values)
                if (cfCirFunction.ParentClass == null && cfCirFunction.HasControlFlowGraph)
                    if (sClassName == "")
                        ccCirClass_NonClassFunction.dFunctions.Add(cfCirFunction.SymbolDef,cfCirFunction);                         
                    else if (cfCirFunction.FunctionSignature.ToUpper().IndexOf(sFunctionName) > -1)                  // case insensive search                    
                        ccCirClass_NonClassFunction.dFunctions.Add(cfCirFunction.SymbolDef,cfCirFunction);

            if (ccCirClass_NonClassFunction.dFunctions.Count > 0)
            {
                dMatches.Add("ccCirClass_NonClassFunction", ccCirClass_NonClassFunction.clone_SimpleMode());
                this.dClasses.Add("ccCirClass_NonClassFunction", ccCirClass_NonClassFunction);
            }
            */
            //  Filter By FunctionName
            //if (sFunctionName != "")
            foreach (CirClass ccCirClass in dMatches.Values)
                if (cirData.dClasses_bySignature.ContainsKey(ccCirClass.Signature))
                    foreach (CirFunction cfCirFunction in cirData.dClasses_bySignature[ccCirClass.Signature].dFunctions.Values)
                    // we have to use the dClasses list of functions since we are going to change the members of ccCirClass)
                    {
                        if (sFunctionName == "")
                        {
                            if (false == ccCirClass.dFunctions.ContainsKey(cfCirFunction.FunctionSignature))
                                ccCirClass.dFunctions.Add(cfCirFunction.FunctionSignature, cfCirFunction);
                        }
                        else if (cfCirFunction.FunctionSignature.ToUpper().IndexOf(sFunctionName) != -1 &&
                                 false == ccCirClass.dFunctions.ContainsKey(cfCirFunction.FunctionSignature))
                            ccCirClass.dFunctions.Add(cfCirFunction.FunctionSignature, cfCirFunction);
                    }

            //  Filter By ParameterType
            if (sParameterType != "")
                foreach (CirClass ccCirClass in dMatches.Values)
                    foreach (CirFunction cfCirFunction in cirData.dClasses_bySignature[ccCirClass.Signature].dFunctions.Values)
                        // we have to use the dClasses list of functions since we are going to change the members of ccCirClass)                    
                        if (ccCirClass.dFunctions.ContainsKey(cfCirFunction.FunctionSignature))
                        {
                            bool bFunctionHasArgument = false;
                            foreach (ICirFunctionParameter functionParameter in cfCirFunction.FunctionParameters)
                                if (getSymbol(cirData, functionParameter.ParameterType).ToUpper().IndexOf(sParameterType) != -1)
                                {
                                    bFunctionHasArgument = true;
                                }
                            if (false == bFunctionHasArgument)
                                ccCirClass.dFunctions.Remove(cfCirFunction.FunctionSignature);
                        }

            /* this doesn't work here since we are going to change the orginal data set
            // filter by Makes calls to:
            if (sMakesCallsTo != "")
                foreach (CirClass ccCirClass in dMatches.Values)
                    foreach (CirFunction cfCirFunction in this.dClasses_bySignature[ccCirClass.FunctionSignature].dFunctions.Values)     // we have to use the dClasses list of functions since we are going to change the members of ccCirClass)                    
                        if (ccCirClass.dFunctions.ContainsKey(cfCirFunction.FunctionSignature))
                        { 
                            CirFunction cfTargetFunction = ccCirClass.dFunctions[cfCirFunction.FunctionSignature];                                
                            List<String> lFunctionsToKeep = new List<string>();
                            foreach (String sCallSignature in cfTargetFunction.FunctionsCalledUniqueList)
                                if (sCallSignature.IndexOf(sMakesCallsTo) > -1)
                                    lFunctionsToKeep.Add(sCallSignature);
                            cfTargetFunction.FunctionsCalledUniqueList = lFunctionsToKeep;                                
                            if (cfTargetFunction.FunctionsCalledUniqueList.Count == 0)
                                ccCirClass.dFunctions.Remove(cfCirFunction.FunctionSignature);
                        }
            //    if (ccCirClass_NonClassFunction.dFunctions.Count > 0)
            //        this.dClasses.Remove("ccCirClass_NonClassFunction");
             */
            return dMatches;
        }

        public static Dictionary<String, ICirFunction> getFunctionsWithControlFlowGraph(ICirData cirData)
        {
            var dFunctionsWithControlFlowGraphs = new Dictionary<String, ICirFunction>();
            foreach (ICirClass ccCirClass in cirData.dClasses_bySignature.Values)
                if (ccCirClass.bClassHasMethodsWithControlFlowGraphs)
                    foreach (ICirFunction cfCirFunction in ccCirClass.dFunctions.Values)
                        if (cfCirFunction.HasControlFlowGraph &&
                            false == dFunctionsWithControlFlowGraphs.ContainsKey(cfCirFunction.FunctionSignature))
                            dFunctionsWithControlFlowGraphs.Add(cfCirFunction.FunctionSignature, cfCirFunction);
            return dFunctionsWithControlFlowGraphs;
        }

        internal static void addSuperClassMapping(ICirData cirData,ICirClass cirClass, string superClassToMap)
        {
            cirClass.dSuperClasses.Add(superClassToMap, cirData.getClass(superClassToMap));
            var superClassedClass = cirData.getClass(superClassToMap);
            superClassedClass.dIsSuperClassedBy.Add(cirClass.Signature, cirClass);
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
    }
}
