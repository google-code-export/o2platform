using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.O2Findings;
using O2.Kernel.Interfaces.O2Findings;
using O2.Kernel.Interfaces.Rules;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.Rules.OunceLabs.RulesUtils
{
    public class RulesAndFindingsUtils
    {

        public static void mapInRulePack_FindingsSourcesAndSinks(O2RulePack o2RulePack, Dictionary<string, List<IO2Rule>> indexedO2Rules, List<IO2Finding> o2Findings, string languageDBId)
        {
            var timer = new O2Timer("mapInRulePack_FindingsSourcesAndSinks").start();
            foreach (O2Finding o2Finding in o2Findings)
                updateO2RulePackWithFindingsTraceTypes(o2RulePack, indexedO2Rules, o2Finding.o2Traces, languageDBId,
                                                       O2RuleType.Source);

            /*updateSourceOrSinkRule(o2RulePack,indexedO2Rules, o2Finding.Source, languageDBId, O2RuleType.Source);
            updateSourceOrSinkRule(o2RulePack,indexedO2Rules, o2Finding.KnownSink, languageDBId, O2RuleType.Sink);
            updateSourceOrSinkRule(o2RulePack, indexedO2Rules, o2Finding.LostSink, languageDBId, O2RuleType.LostSink);                */
            timer.stop();


        }

        /// <summary>
        ///  recursive mapping of traces tracetype (required since there could be multiple sources
        /// </summary>
        /// <param name="o2RulePack"></param>
        /// <param name="indexedO2Rules"></param>
        /// <param name="o2Traces"></param>
        /// <param name="languageDBId"></param>
        /// <param name="o2NewRuleType"></param>
        private static void updateO2RulePackWithFindingsTraceTypes (O2RulePack o2RulePack,IDictionary<string, List<IO2Rule>> indexedO2Rules, List<IO2Trace> o2Traces, string languageDBId, O2RuleType o2NewRuleType)
        {
            foreach (var o2Trace in o2Traces)
            {
                updateSourceOrSinkRule(o2RulePack, indexedO2Rules, o2Trace.signature, languageDBId, o2Trace.traceType);
                updateO2RulePackWithFindingsTraceTypes(o2RulePack, indexedO2Rules, o2Trace.childTraces, languageDBId, o2NewRuleType);
            }
        }

        //rename this method since we do more than just Source or Sinks here
        private static void updateSourceOrSinkRule(O2RulePack o2RulePack,IDictionary<string, List<IO2Rule>> indexedO2Rules, string ruleSignature, string languageDBId, TraceType traceType)
        {
            if (indexedO2Rules != null && ruleSignature != null)
            {
                //bool createNotMappedRule = false;
                var createRuleWithType = O2RuleType.NotMapped;
                // only proccess Sources, Sinks and Lost Sinks
                if (traceType == TraceType.Source || traceType == TraceType.Known_Sink || traceType == TraceType.Lost_Sink)
                {
                    O2RuleType o2NewRuleType = O2RulePackUtils.getO2RuleTypeFromRuleType(traceType);
                    createRuleWithType = o2NewRuleType;

             /*       if (indexedO2Rules != null && false == string.IsNullOrEmpty(ruleSignature))
                        if (indexedO2Rules.ContainsKey(ruleSignature))
                        {
                            bool thereIsAlreadyARuleWithTheSameRuleType = false;
                            foreach (var o2Rule in indexedO2Rules[ruleSignature])
                            {
                                if (o2Rule.RuleType == O2RuleType.NotMapped)
                                // if it is not mapped change it to o2NewRuleType
                                {
                                    o2Rule.RuleType = o2NewRuleType;
                                    return;
                                }
                                if (o2Rule.RuleType == o2NewRuleType)
                                    // if it is already a rule of type o2NewRuleType, mark it so we can ignore it below
                                    thereIsAlreadyARuleWithTheSameRuleType = true;
                            }
                            if (false == thereIsAlreadyARuleWithTheSameRuleType)
                            // if we got this far, create a new rule of o2NewRuleType                                        
                            {
                                var newRule = O2RulePackUtils.createRule(o2NewRuleType, ruleSignature, languageDBId);
                                indexedO2Rules[ruleSignature].Add(newRule);
                                // add it to the index so that we don't have to calculate it again
                                o2RulePack.o2Rules.Add(newRule);
                            }
                        }
                    */
                }
                bool createNewRule = true;
                IO2Rule notMappedRule = null;

                if (indexedO2Rules.ContainsKey(ruleSignature))
                {
                    foreach (var o2Rule in indexedO2Rules[ruleSignature])
                    {
                        if (o2Rule.RuleType == createRuleWithType)          // dont create if there is already a rule of this type
                        {
                            o2Rule.Tagged = true;
                            createNewRule = false;
                        }
                        if (o2Rule.RuleType == O2RuleType.NotMapped)
                            notMappedRule = o2Rule;
                    }
                }
                // handle the case where we have already added a signature but it is not a NotMapped one
                if (createRuleWithType == O2RuleType.NotMapped &&  createNewRule && notMappedRule == null && indexedO2Rules.ContainsKey(ruleSignature))
                    createNewRule = false;

                // if required, Create  rule
                if (createNewRule)
                {
                    var vulnType = "O2.FindingRule." + createRuleWithType.ToString();
                    var newRule = new O2Rule(createRuleWithType,vulnType, ruleSignature, languageDBId,true);
                    o2RulePack.o2Rules.Add(newRule);
                    if (false == indexedO2Rules.ContainsKey(ruleSignature))
                        indexedO2Rules.Add(ruleSignature, new List<IO2Rule>());
                    indexedO2Rules[ruleSignature].Add(newRule);
                    if (notMappedRule != null)
                        indexedO2Rules[ruleSignature].Remove(notMappedRule);
                }
            }
        }

        public static O2RulePack createRulePackThatMatchFindings(O2RulePack o2rulePack, List<IO2Finding> o2Findings, string languageDBId)
        {
            return createRulePackThatMatchFindings(o2rulePack, o2Findings, "RulePackThatMatchFindings", languageDBId);
        }

        public static O2RulePack createRulePackThatMatchFindings(O2RulePack o2rulePack, List<IO2Finding> o2Findings, string rulePackName, string languageDBId)
        {
            var uniqueSignatures = getListOfUniqueSignatures(o2Findings);            
            return createRulePackWithSignatures(o2rulePack, uniqueSignatures, rulePackName, true, languageDBId);
        }

        public static O2RulePack createRulePackWithSignatures(O2RulePack o2rulePack, List<String> signaturesToFind, string rulePackName, bool addNonMatchingSignaturesAsNewRule, string languageDBId)
        {
            DI.log.info("in createRulePackWithSignatures");
            var o2RulesThatMatchSignatures = new List<IO2Rule>();
            var indexedRules = IndexedO2Rules.indexAll(o2rulePack.getIO2Rules());
            foreach (var signature in signaturesToFind)
                if (indexedRules.ContainsKey(signature))
                    foreach (var o2Rule in indexedRules[signature])
                        o2RulesThatMatchSignatures.Add(o2Rule);
                else
                    if (addNonMatchingSignaturesAsNewRule)                                            
                        o2RulesThatMatchSignatures.Add(O2RulePackUtils.createRule(O2RuleType.NotMapped, signature, languageDBId));                    
            DI.log.info("createRulePackWithSignatures completed, there were {0} rules found", o2RulesThatMatchSignatures.Count);
            return new O2RulePack(rulePackName, o2RulesThatMatchSignatures);
        }



        // the method of calculating first the partial lists is a little bit faster than using a bit list (see getListOfUniqueSignatures_UsingUniqueList below)
        public static List<String> getListOfUniqueSignatures(List<IO2Finding> o2Findings)
        {            
            var allSignatures = new List<String>();
            foreach (var o2Finding in o2Findings)
            {
                var findingUniqueList = new List<string>();
                getListOfUniqueSignatures(o2Finding.o2Traces, findingUniqueList);
                allSignatures.AddRange(findingUniqueList);
            }
            var numberOfUniqueSignatures  = 0;
            var uniqueViaLinq = O2Linq.getUniqueListOfStrings(allSignatures, ref numberOfUniqueSignatures);
            DI.log.info("Via Linq there were {0} unique signatures calculated", numberOfUniqueSignatures);
            return uniqueViaLinq.ToList();
            var uniqueSignatures = new List<String>();
            var partialLists = new List<List<String>>();
            // calculate partial signatures             
            foreach (var o2Finding in o2Findings)
            {
                var findingUniqueList = new List<string>();
                getListOfUniqueSignatures(o2Finding.o2Traces, findingUniqueList);
                partialLists.Add(findingUniqueList);                
            }
            DI.log.info("Partial lists calculated, consolidating them");
            var itemsProcessed = 0;
            var itemsToProcess = o2Findings.Count;
            foreach (var partialList in partialLists)
            {
                foreach (var signature in partialList)
                    if (false == uniqueSignatures.Contains(signature))
                        uniqueSignatures.Add(signature);

                if ((itemsProcessed++)%5000 == 0)
                    DI.log.info("on [{0}/{1}] there are {2} unique signatures", itemsProcessed, itemsToProcess,
                                uniqueSignatures.Count);
            }
            /*if ((itemsProcessed++) % 5000 == 0)
                DI.log.info("on [{0}/{1}] there are {2} unique signatures", itemsProcessed, itemsToProcess, uniqueSignatures.Count);*/
            DI.log.info("There are {0} unique signatures", uniqueSignatures.Count);

         /*   foreach(var uniquesig in uniqueSignatures)
                if (false == uniqueViaLinq.Contains(uniquesig))
                { 
                }
            */
            return uniqueSignatures;
        }


        public static void getListOfUniqueSignatures(List<IO2Trace> o2Traces, List<String> uniqueSignatures)
        {
            foreach(var o2Trace in o2Traces)
            {
                if (false == uniqueSignatures.Contains(o2Trace.signature))
                    uniqueSignatures.Add(o2Trace.signature);
                getListOfUniqueSignatures(o2Trace.childTraces   , uniqueSignatures);
            }
        }

        /*public static List<String> getListOfUniqueSignatures_UsingUniqueList(List<IO2Finding> o2Findings)
        {
            // mode 1 use one big list
            var uniqueSignatures = new List<String>();
            // calculate unique signatures 
            var itemsProcessed = 0;
            var itemsToProcess = o2Findings.Count;
            foreach (var o2Finding in o2Findings)
            {
                getListOfUniqueSignatures(o2Finding.o2Traces, uniqueSignatures);                
                if ((itemsProcessed++) % 5000 == 0)
                    DI.log.info("on [{0}/{1}] there are {2} unique signatures", itemsProcessed, itemsToProcess, uniqueSignatures.Count);
            }
            DI.log.info("There are {0} unique signatures", uniqueSignatures.Count);
            return uniqueSignatures;
        }*/




        internal static O2RulePack mapFindingsToCurrentRulePack(
            O2RulePack currentO2RulePack, List<IO2Finding> o2Findings, string languageId, bool keepRulesLoadedFromDatabase)
        {
            var newCallbackSignature = "O2.AutoMapping";
            DI.log.info("converting {0} Findings into rules", o2Findings.Count);

            // get rules that we will keep (if there are rules loaded from Db and keepRulesLoadedFromDatabase is set)
            var newO2Rules = (keepRulesLoadedFromDatabase)
                                 ? O2RulePackUtils.getRulesThatAreFromDB(currentO2RulePack)
                                 : new List<IO2Rule>();

            //update index
            var indexedCurrentO2Rules = IndexedO2Rules.indexAll(newO2Rules);
            // and if there are any rules from the database, remove any Tagged values                       
            if (keepRulesLoadedFromDatabase)
                foreach (var o2Rule in newO2Rules)
                    o2Rule.Tagged = false;

            // create rulepack 
            var newRulePack = new O2RulePack("Rule Pack", newO2Rules);

            // and call the function that does the mappings
            mapInRulePack_FindingsSourcesAndSinks(newRulePack, indexedCurrentO2Rules , o2Findings, languageId);
            
            DI.log.info("{0} rules created", newRulePack.o2Rules.Count);
            
            return newRulePack;

        }
    }
}
