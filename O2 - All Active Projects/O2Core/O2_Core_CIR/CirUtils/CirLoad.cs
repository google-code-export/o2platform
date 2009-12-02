using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.Xsd;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Filters;
using O2.DotNetWrappers.O2Misc;
using O2.Kernel.Interfaces.CIR;

namespace O2.Core.CIR.CirUtils
{
    public class CirLoad
    {
        public static bool isFileACirDumpFile(String sPathToFileToCheck)
        {
            //O2Timer tTimerFile = new O2Timer("File load").start();
            bool bresult = false;
            TextReader trTextReader = new StreamReader(sPathToFileToCheck);
            if (trTextReader.ReadLine().IndexOf("<CommonIRDump") > -1)
                bresult = true;
            if (trTextReader.ReadLine().IndexOf("<CommonIRDump") > -1)
                bresult = true;
            trTextReader.Close();
            return bresult;
            //tTimerFile.stop();
            /*   (it was faster to use a StreamReader than an XmlTextReader
            O2Timer tTimerXml = new O2Timer("Xml load").start();
            XmlTextReader xtrXmlTextReader = new XmlTextReader(sPathToFileToCheck);
            xtrXmlTextReader.Read();
            xtrXmlTextReader.Read();
            xtrXmlTextReader.Read();
            if (xtrXmlTextReader.Name == "CommonIRDump")
                DI.log.error("CommonIRDump found via Xml Load ");
            tTimerXml.stop();
            return true;*/
        }

        public static CirData loadFile(String sPathToFileToLoad)
        {
            CirData cirData;
            if (Path.GetExtension(sPathToFileToLoad) == ".CirData")
            {
                cirData = loadSerializedO2CirDataObject(sPathToFileToLoad);
            }
            else
            {
                DI.log.debug("Loading CirDumpFile file: {0}", sPathToFileToLoad);
                cirData = new CirData();
                // always resolve these xrefs since older o2CirData might have probs (note that this is a one time only effort (per O2 session)) 
                loadCirDumpXmlFile_andPopulateDictionariesWithXrefs(sPathToFileToLoad, cirData, false);
            }
            fixFunctionsCalledXRefs(cirData);       // required for the new objects added by the CirCreator.CirFactory funtionality
            return cirData;
        }

        private static void fixFunctionsCalledXRefs(ICirData cirData)
        {
            if (cirData != null && cirData.dFunctions_bySignature != null)
            {
                var timer = new O2Timer("fixFunctionsCalledXRefs").start();
                foreach (CirFunction cirFunction in cirData.dFunctions_bySignature.Values)
                {
                    if (cirFunction.FunctionName == null)
                    {
                        var filteredSignature = new FilteredSignature(cirFunction.FunctionSignature);
                        cirFunction.FunctionName = filteredSignature.sFunctionName;
                        cirFunction.FunctionNameAndParameters = filteredSignature.sFunctionNameAndParams;
                        cirFunction.ClassNameFunctionNameAndParameters = filteredSignature.sFunctionClass + "." +
                                                                         filteredSignature.sFunctionNameAndParams;
                    }
                }
                timer.stop();
            }
        }

        public static bool loadCirDumpXmlFile(String sPathToFileToProcess, ref CommonIRDump cidCommonIrDump,
                                              bool bShowError)
        {
            try
            {
                cidCommonIrDump =
                    (CommonIRDump) Serialize.getDeSerializedObjectFromXmlFile(sPathToFileToProcess, typeof (CommonIRDump));
                if (cidCommonIrDump != null)
                    return true;
            }
            catch (Exception ex)
            {
                if (bShowError)
                    DI.log.error("in loadCirDumpXmlFile: {0}", ex.Message);
            }
            return false;
        }

        public static void loadCirDumpXmlFile_andPopulateDictionariesWithXrefs(String sFileToProcess, CirData fcdCirData,
                                                                               bool bVerbose)
        {
            CommonIRDump cidCommonIrDump = loadCirDumpXmlFile_justReturnCommonIRDump(sFileToProcess, bVerbose);
            fcdCirData.bVerbose = bVerbose;
            CirDataUtils.populateDictionariesWithXrefs(cidCommonIrDump, fcdCirData);
        }

        public static void loadCirDumpXmlFiles_andPopulateDictionariesWithXrefs(List<String> sFilesToProcess,
                                                                                CirData fcdCirData, bool bVerbose)
        {
            for (int iFileIndex = 0; iFileIndex < sFilesToProcess.Count; iFileIndex++)
            {
                O2Timer tO2Timer =
                    new O2Timer(String.Format("[{0}/{1}] Loading CirDumpFile : {2}", sFilesToProcess.Count,
                                              iFileIndex, sFilesToProcess[iFileIndex])).start();
                CommonIRDump cidCommonIrDump = loadCirDumpXmlFile_justReturnCommonIRDump(sFilesToProcess[iFileIndex],
                                                                                         bVerbose);
                fcdCirData.bVerbose = true;
                CirDataUtils.populateDictionariesWithXrefs(cidCommonIrDump, fcdCirData);
                tO2Timer.stop();
            }
        }

        public static CirData loadSerializedO2CirDataObject(String sTargetFile)
        {
            return loadSerializedO2CirDataObject(sTargetFile, true);
        }

        public static CirData loadSerializedO2CirDataObject(String sTargetFile, bool bUseCachedVersionIfAvailable)
        {
            DI.log.debug("Loading o2CirData file: {0}", sTargetFile);
            // add cache support               
            // return cached object (if available)
            if (bUseCachedVersionIfAvailable && vars.get(sTargetFile) != null)
                return (CirData) vars.get(sTargetFile);
            CirData fcdLoadedo2CirData = null;
            try
            {
                O2Timer tO2Timer =
                    new O2Timer("Loading DeSerialized O2CirData from " + Path.GetFileName(sTargetFile)).start();
                var bfBinaryFormatter = new BinaryFormatter();
                var fsFileStream = new FileStream(sTargetFile, FileMode.Open);
                //ounceLabs.O2.classes.
                Object oObject = bfBinaryFormatter.Deserialize(fsFileStream);

                if (oObject.GetType().Name == "CirData")
                {
                    fcdLoadedo2CirData = (CirData) oObject;
                    if (fcdLoadedo2CirData.sDbId == "")
                        CirDataUtils.resolveDbId(fcdLoadedo2CirData); // in case was not originally resolved
                    if (fcdLoadedo2CirData.dClasses_bySignature == null)
                        DI.log.error("Something is wrong with this file since the dClasses dictionary is null");

                    // store loaded object as a local o2 var (which will act as a cache)
                    vars.set_(sTargetFile, fcdLoadedo2CirData);
                }
                else
                    DI.log.error("Loaded data is not of type o2CirData");
                fsFileStream.Close();
                tO2Timer.stop();
            }
            catch (Exception ex)
            {
                DI.log.error("In loadSerializedO2CirDataObject: {0}", ex.Message);
            }
            return fcdLoadedo2CirData;
        }

        public static CommonIRDump loadCirDumpXmlFile_justReturnCommonIRDump(String sPathToFileToProcess, bool bVerbose)
        {
            if (false == isFileACirDumpFile(sPathToFileToProcess))
                //  Path.GetExtension(sPathToFileToProcess) != ".xml")
            {
                DI.log.error("in loadCirDumpXmlFile_justReturnCommonIRDump, only CommonIRDump are supported");
                return null;
            }
            O2Timer tO2TimerSerializedData = null;
            if (bVerbose)
            {
                DI.log.debug("Processing file {0}", Path.GetFileName(sPathToFileToProcess));
                tO2TimerSerializedData = new O2Timer("Creating object with serialized data ").start();
            }

            //CommonIRDump cidCommonIrDump = (xsd.CirDump.CommonIRDump)serialize.getDeSerializedObjectFromXmlFile(sPathToFileToProcess, typeof(xsd.CirDump.CommonIRDump));
            CommonIRDump cidCommonIrDump = null;
            if (false == loadCirDumpXmlFile(sPathToFileToProcess, ref cidCommonIrDump, false))
            {
                Hack.tryToFixCirDumpProblems(sPathToFileToProcess);
                loadCirDumpXmlFile(sPathToFileToProcess, ref cidCommonIrDump, true);
            }


            if (bVerbose)
                tO2TimerSerializedData.stop();
            return cidCommonIrDump;
        }
    }
}