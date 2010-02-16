// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirUtils;
using O2.DotNetWrappers.DotNet;
using O2.Interfaces.CIR;
using O2.Interfaces.O2Findings;
using O2.Interfaces.Rules;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.Rules.OunceLabs.RulesUtils
{
    public class O2RulePackUtils
    {
        public static bool bVerbose;

        public static void addRule(IO2Rule rRule, O2RulePack o2rulePack)
        {
            o2rulePack.o2Rules.Add((O2Rule)rRule);
        }
        public static string saveRulePack(O2RulePack o2rulePackToSave)
        {
            return saveRulePack(DI.config.TempFileNameInTempDirectory, "",
                                o2rulePackToSave);
        }

        public static String saveRulePack(String pathToSaveFile, String sTypeOfPack, O2RulePack o2rulePackToSave)
        {
            if (false == File.Exists(pathToSaveFile) && false == Directory.Exists(Path.GetDirectoryName(pathToSaveFile)))
                pathToSaveFile = DI.config.TempFileNameInTempDirectory + "_" + pathToSaveFile;
            string rulePackFile = pathToSaveFile + "_" + o2rulePackToSave.RulePackName + "_" + "(" +
                                  o2rulePackToSave.o2Rules.Count + ")_" + sTypeOfPack + ".O2RulePack";
            return saveRulePack(rulePackFile, o2rulePackToSave);
        }

        public static string saveRulePack(string rulePackFile, O2RulePack o2rulePackToSave)
        {
            if (o2rulePackToSave.o2Rules.Count > 0)
            {
                Serialize.createSerializedXmlFileFromObject(o2rulePackToSave, rulePackFile);
                DI.log.info("Saved O2RulePack {0} with {1} rules", rulePackFile, o2rulePackToSave.o2Rules.Count);
                return rulePackFile;
            }
            return "";
        }

        /*public static String saveRulePack(String sCirDataFile, String sTypeOfPack, O2RulePack orpRulePack)
        {
            return "NOT IMPLEMENTED";
        }*/

        public static O2RulePack loadRulePack(String sFileToLoad)
        {
            return (O2RulePack)Serialize.getDeSerializedObjectFromXmlFile(sFileToLoad, typeof(O2RulePack));
        }

        public static O2RulePack createRules_SourcesAndSinks(String sCirDataFile)
        {
            var rpRulePack = new O2RulePack();
            if (false == File.Exists(sCirDataFile))
                DI.log.error("in createRules_SourcesAndSinks, provide CirData file not found: {0}", sCirDataFile);
            else
            {
                List<String> lsFunctions = MiscUtils.getFunctionsSignaturesFrom02CirData(sCirDataFile);
                // in this type of scan, there are two rules	    	
                // if functions make no calls then they are maked as both Sources and Sinks
                // other cases receive no marking
                // sinks have preference (for the cases there there are no calls into and from

                CirData fcdCirData = CirLoad.loadSerializedO2CirDataObject(sCirDataFile);

                foreach (string sFunction in lsFunctions)
                {
                    ICirFunction cfCirFunction = fcdCirData.dFunctions_bySignature[sFunction];
                    if (cfCirFunction.FunctionsCalledUniqueList.Count == 0)
                    {
                        addRule(createRule(O2RuleType.Sink, sFunction, fcdCirData.sDbId), rpRulePack);
                        addRule(createRule(O2RuleType.Source, sFunction, fcdCirData.sDbId), rpRulePack);
                    }
                }
            }
            return rpRulePack;
        }

        public static O2RulePack createRules_CallBacksOnEdges_And_ExternalSinks(String sCirDataFile)
        {
            var rpRulePack = new O2RulePack();
            if (false == File.Exists(sCirDataFile))
                DI.log.error(
                    "in createRules_CallBacksOnEdges_And_ExternalSinks, provide CirData file not found: {0}",
                    sCirDataFile);
            else
            {
                List<String> lsFunctions = MiscUtils.getFunctionsSignaturesFrom02CirData(sCirDataFile);
                // in this type of scan, there are two rules	    	
                // if functions make no calls it is a Sink
                // if nobody calls the function it is a callback
                // sinks have preference (for the cases there there are no calls into and from

                ICirData fcdCirData = CirLoad.loadSerializedO2CirDataObject(sCirDataFile);

                foreach (string sFunction in lsFunctions)
                {
                    ICirFunction cfCirFunction = fcdCirData.dFunctions_bySignature[sFunction];
                    if (cfCirFunction.FunctionsCalledUniqueList.Count == 0)
                        addRule(createRule(O2RuleType.Sink, sFunction, fcdCirData.sDbId), rpRulePack);                        
                        // DI.log.error("   Make no Calls (make sink): {0}" , cfCirFunction.sSignature);
                    else if (cfCirFunction.FunctionIsCalledBy.Count == 0)
                        addRule(createRule(O2RuleType.Callback, sFunction, fcdCirData.sDbId), rpRulePack);                    
                }
            }
            return rpRulePack;
        }

        public static O2RulePack createRules_CallBacksOnControlFlowGraphs_And_ExternalSinks(String sCirDataFile)
        {
            var rpRulePack = new O2RulePack();
            if (false == File.Exists(sCirDataFile))
                DI.log.error(
                    "in createRules_CallBacksOnControlFlowGraphs_And_ExternalSinks, provide CirData file not found: {0}",
                    sCirDataFile);
            else
            {
                List<String> lsFunctions = MiscUtils.getFunctionsSignaturesFrom02CirData(sCirDataFile);
                // in this type of scan, there are two rules	    	
                // if functions have a ControlFlowGraph they are Callbacks
                // everything else is a sink	    	

                ICirData fcdCirData = CirLoad.loadSerializedO2CirDataObject(sCirDataFile);
                Dictionary<String, ICirFunction> dFunctionsWithControlFlowGraphs =
                    CirDataUtils.getFunctionsWithControlFlowGraph(fcdCirData);
                foreach (string sFunction in lsFunctions)
                {
                    if (dFunctionsWithControlFlowGraphs.ContainsKey(sFunction))
                        addRule(createRule(O2RuleType.Callback, sFunction, fcdCirData.sDbId), rpRulePack);                        
                    else
                        addRule(createRule(O2RuleType.Sink, sFunction, fcdCirData.sDbId), rpRulePack);                        
                }
            }
            return rpRulePack;
        }

        public static O2Rule createRule(O2RuleType rtRuleType, String sSignature, String sDbID)
        {            
            var rRule = new O2Rule
                            {
                                Severity = "Medium",
                                DbId = sDbID,
                                Signature = sSignature,
                                RuleType = rtRuleType,
                                VulnType = String.Format("_O2.{0}", rtRuleType)
                            };
            return rRule;
        }

        internal static O2RuleType getO2RuleTypeFromRuleType(TraceType traceType)
        {
            switch (traceType)
            {
                case TraceType.Known_Sink:
                    return O2RuleType.Sink;
                case TraceType.Lost_Sink:
                    return O2RuleType.LostSink;
                case TraceType.Source:
                    return O2RuleType.Source;
                default:                
                    return O2RuleType.NotMapped;                
            }
        }

        public static List<IO2Rule> getRulesThatAreFromDB(O2RulePack o2RulePack)
        {
            return (from IO2Rule o2Rule in o2RulePack.o2Rules where o2Rule.FromDb select o2Rule).ToList();
        }

        internal static IO2Rule cloneRule(IO2Rule o2RuleToClone)
        {
            var clonedRule = new O2Rule
                                 {
                                     Comments = o2RuleToClone.Comments,
                                     DbId = o2RuleToClone.DbId,
                                     FromArgs = o2RuleToClone.FromArgs,
                                     Param = o2RuleToClone.Param,
                                     Return = o2RuleToClone.Return,
                                     FromDb = o2RuleToClone.FromDb,
                                     RuleType = o2RuleToClone.RuleType,
                                     Severity = o2RuleToClone.Severity,
                                     Signature = o2RuleToClone.Signature,
                                     Tagged = o2RuleToClone.Tagged,
                                     ToArgs = o2RuleToClone.ToArgs,
                                     VulnType = o2RuleToClone.VulnType                                                                          
                                 };
            return clonedRule;
        }
    }
}
