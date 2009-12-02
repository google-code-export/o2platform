using System;
using System.Collections.Generic;
using System.IO;
using O2.Core.CIR.CirObjects;
using O2.DotNetWrappers.Windows;

namespace O2.Core.CIR.CirUtils
{
    public class CirDumpsUtils
    {
        public static bool createConsolidatedCirDataFile(String sPathToCirDumpFiles, String sTargetCirDataFile,
                                                         bool bStoreControlFlowBlockRawDataInsideCirDataFile)
        {
            DI.log.debug("Creating Consolidated CirData file");
            try
            {
                List<String> lsCirDumpFiles = Files.getFilesFromDir_returnFullPath(sPathToCirDumpFiles);
                var fcdCirData = new CirData
                                     {
                                         bStoreControlFlowBlockRawDataInsideCirDataFile =
                                             bStoreControlFlowBlockRawDataInsideCirDataFile
                                     };
                if (lsCirDumpFiles.Count == 0)
                    DI.log.error("No CirDump to process (were created during scan?)");
                else
                {
                    DI.log.debug("in loadCirDumpXmlFile_andPopulateDictionariesWithXrefs");
                    foreach (String sFile in lsCirDumpFiles)
                        try
                        {
                            fcdCirData.dSymbols = new Dictionary<string, string>();
                            fcdCirData.dTemp_Functions_bySymbolDef = new Dictionary<string, O2.Kernel.Interfaces.CIR.ICirFunction>();
                            CirLoad.loadCirDumpXmlFile_andPopulateDictionariesWithXrefs(sFile, fcdCirData, true);
                        }
                        catch (Exception ex)
                        {
                            DI.log.error("In createConsolidatedCirDataFile, error while processing file {0}: {1}",
                                         sFile, ex.Message);
                        }
                    DI.log.debug("in saveSerializedO2CirDataObjectToFile");
                    CirDataUtils.saveSerializedO2CirDataObjectToFile(fcdCirData,sTargetCirDataFile);
                    return true;
                }
            }
            catch (Exception ex)
            {
                DI.log.error("In createConsolidatedCirDataFile: {0}:", ex.Message);
            }
            return false;
        }


        public static String fromCommonIRDump_get_DbId(List<String> lsFiles, List<String> lCompilationUnit)
        {
            foreach (String sFile in lsFiles)
            {
                String sFileExtension = fromFileExtension_get_DbId(Path.GetExtension(sFile));
                if (sFileExtension != "")
                    return sFileExtension;
            }

            foreach (String sFile in lCompilationUnit)
            {
                String sFileExtension = fromFileExtension_get_DbId(Path.GetExtension(sFile));
                if (sFileExtension != "")
                    return sFileExtension;
            }
            return "--";
        }

        public static String fromFileExtension_get_DbId(String sFileExtension)
        {
            switch (sFileExtension)
            {
                case ".cs":
                case ".dll":
                case ".exe":
                    return "3";
                case ".java":
                    return "2";
                case ".cpp":
                case ".c":
                case ".cxx":
                    return "1";
                case ".asp":
                    return "4";
                default:
                    return "";
            }
        }
    }
}