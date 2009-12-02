// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using O2.Kernel.Interfaces.Rules;
using O2.Kernel.InterfacesBaseImpl;
using O2.Rules.OunceLabs.DataLayer;
using O2.Rules.OunceLabs.RulesUtils;

namespace O2.Rules.OunceLabs.DataLayer_OunceV6
{

    public class MySqlRules_OunceV6 : IDatabaseRules
    {

        #region Nested type: ActionObject

        public class ActionObject
        {
            public String sSeverity;
            public String sSignature;
            public String sType;
            public String sVuln_type;
            public UInt32 uAdded;
            public UInt32 uDb_id;
            public UInt32 uId;
            public UInt32 uTrace;
            public UInt32 uVuln_id;
        }

        #endregion

        #region Nested type: Rules

        public class Rules
        {
            public String added;
            public String callback;
            public String clazz; // should be class but can't use it since it clashes with C# class keyword
            public String db_id;
            public List<ActionObject> lActionObjects;
            public String modified;
            public String package;
            public String signature;
            public String version;
            public String vuln_id;
            public String vuln_name;
        }

        #endregion

        public void DeleteAllRulesFromDatabase()
        {
            Lddb_OunceV6.action_DeleteAllRules();
        }
               

        public List<IO2Rule> createO2RulesForAllLddbEntriesForLanguage(SupportedLanguage language)
        {
            return createO2RulesForAllLddbEntriesForLanguage(language, true, true, true,true, true, true,true,true);
        }

        public List<IO2Rule> createO2RulesForAllLddbEntriesForLanguage(SupportedLanguage language,
            bool addSources, bool addSinks, bool addCallbacks ,bool addPropagateTaint, bool addDontPropagateTaint, bool addAnyHigh, bool addAnyMedium, bool addAnyLow)
        {
            var o2Rules = new List<IO2Rule>();
            var languageID = MiscUtils_OunceV6.getIdForSuportedLanguage(language);
            if (languageID > -1)
            {
                var ruleDbId = languageID.ToString();

                // note: current the validators are not imported
                if (addSources)
                {
                    var sources = getRules_Sources(ruleDbId);
                    // hard coded script to remove the <external_source> rule which is an hard-coded Ounce rule and cannot be changed (or the callbacks don't work)
                    foreach(var source in sources)
                        if (source.Signature.IndexOf("<external_source>") > -1)
                        {
                            DI.log.info("Removing {0} rule from list of loaded sources", source.Signature);
                            sources.Remove(source);
                            break;
                        }
                    o2Rules.AddRange(sources);
                    DI.log.info("There are {0} sources rules", sources.Count);                    
                }
                if (addSinks)
                {
                    var sinks = getRules_Sinks(ruleDbId);
                    DI.log.info("There are {0} sinks rules", sinks.Count);
                    o2Rules.AddRange(sinks);
                }
                if (addCallbacks)
                {
                    var callbacks = getRules_Callbacks(ruleDbId);
                    DI.log.info("There are {0} callbacks rules", callbacks.Count);
                    o2Rules.AddRange(callbacks);
                }
                if (addPropagateTaint)
                {
                    var propagateTaint = getRules_PropagateTaint(ruleDbId);
                    DI.log.info("There are {0} propagateTaint rules", propagateTaint.Count);
                    o2Rules.AddRange(propagateTaint);
                }

                if (addDontPropagateTaint)
                {
                    var dontPropageTaint = getRules_DontPropagateTaint(ruleDbId);
                    DI.log.info("There are {0} dontPropageTaint rules", dontPropageTaint.Count);
                    o2Rules.AddRange(dontPropageTaint);                    
                }

                if (addAnyHigh)
                {
                    var anyHigh = getRules_AnyHigh(ruleDbId);
                    DI.log.info("There are {0} anyHigh rules", anyHigh.Count);
                    o2Rules.AddRange(anyHigh);
                }

                if (addAnyMedium)
                {
                    var anyMedium = getRules_AnyMedium(ruleDbId);
                    DI.log.info("There are {0} anyMedium rules", anyMedium.Count);
                    o2Rules.AddRange(anyMedium);
                }

                if (addAnyLow)
                {
                    var anyLow = getRules_AnyLow(ruleDbId);
                    DI.log.info("There are {0} anyLow rules", anyLow.Count);
                    o2Rules.AddRange(anyLow);
                }

            }
            DI.log.info(" # rules added:{0}", o2Rules.Count);
            return o2Rules;
        }
        

        public List<IO2Rule> getRules_Sources(string ruleDbId)
        {
            var o2Rules = new List<IO2Rule>();
            var sqlForSource =
                    "select rec.signature as recSignature, actionobjects.signature as actionObjectSignature, " +
                    "actionobjects.severity, actionobjects.vuln_type, source_info.param, source_info.return " +
                    "from source_info , actionobjects , rec " +
                    "where source_info.ao_id = actionobjects.id and actionobjects.vuln_id = rec.vuln_id and " +
                    "actionobjects.db_id=" + ruleDbId;


            var mySqlDataReader = OunceMySql.executeSqlQueryReturnSqlDataReader(sqlForSource);
            if (mySqlDataReader == null)
            {
                DI.log.error("in getRules_Sources, mySqlDataReader was null");
                return o2Rules;
            }
            foreach (DbDataRecord dataRow in mySqlDataReader)
            {
                //var vulnID = dataRow["vuln_id"].ToString();
                var severity = dataRow["severity"].ToString();
                var vulnType = dataRow["vuln_type"].ToString();
                var recSignature = dataRow["recSignature"].ToString();
                var param = dataRow["param"].ToString();
                var _return = dataRow["return"].ToString();
                o2Rules.Add(new O2Rule
                {
                    DbId = ruleDbId,
                    RuleType = O2RuleType.Source,
                    Severity = severity,
                    VulnType = vulnType,
                    Signature = recSignature,
                    Param = param,
                    Return = _return
                });
            }
            mySqlDataReader.Close();
            return o2Rules;
        }
       

        public List<IO2Rule> getRules_Sinks(string ruleDbId)
        {
            var o2Rules = new List<IO2Rule>();
            var sqlForSinks =
                    "select rec.signature as recSignature, actionobjects.signature as actionObjectSignature, " +
                    "actionobjects.severity, actionobjects.vuln_type, sink_info.param " +
                    "from sink_info , actionobjects , rec " +
                    "where sink_info.ao_id = actionobjects.id and actionobjects.vuln_id = rec.vuln_id and " +
                    "actionobjects.db_id=" + ruleDbId;

            var mySqlDataReader = OunceMySql.executeSqlQueryReturnSqlDataReader(sqlForSinks);
            
            foreach (DbDataRecord dataRow in mySqlDataReader)
            {
                //var vulnID = dataRow["vuln_id"].ToString();
                var severity = dataRow["severity"].ToString();
                var vulnType = dataRow["vuln_type"].ToString();
                var recSignature = dataRow["recSignature"].ToString();
                var param = dataRow["param"].ToString();
                o2Rules.Add(new O2Rule
                {
                    DbId = ruleDbId,
                    RuleType = O2RuleType.Sink,
                    Severity = severity,
                    VulnType = vulnType,
                    Signature = recSignature,
                    Param = param
                });
            }
            mySqlDataReader.Close();
            return o2Rules;
        }


        public List<IO2Rule> getRules_Callbacks(string ruleDbId)
        {
            var o2Rules = new List<IO2Rule>();
            var sqlForSinks =
                    "select rec.signature as recSignature from rec " +                     
                    "where rec.callback = 1 and "+ 
                    "rec.db_id=" + ruleDbId;

            var mySqlDataReader = OunceMySql.executeSqlQueryReturnSqlDataReader(sqlForSinks);

            foreach (DbDataRecord dataRow in mySqlDataReader)
            {
                //var vulnID = dataRow["vuln_id"].ToString();
                //var severity = dataRow["severity"].ToString();
                //var vulnType = dataRow["vuln_type"].ToString();
                var recSignature = dataRow["recSignature"].ToString();
                //var param = dataRow["param"].ToString();
                o2Rules.Add(new O2Rule
                {
                    DbId = ruleDbId,
                    RuleType = O2RuleType.Callback,
                  //  Severity = severity,
                   // VulnType = vulnType,
                    Signature = recSignature
                    //Param = param
                });
            }
            mySqlDataReader.Close();
            return o2Rules;
        }
        public List<IO2Rule> getRules_DontPropagateTaint(string ruleDbId)
        {
            var o2Rules = new List<IO2Rule>();
            var sql = "select rec.signature from rec,taint_info " +
                      "where taint_info.vuln_id = rec.vuln_id and propagates=0 and "+
                      "rec.db_id=" + ruleDbId;

            var mySqlDataReader = OunceMySql.executeSqlQueryReturnSqlDataReader(sql);

            foreach (DbDataRecord dataRow in mySqlDataReader)
            {
                //var vulnID = dataRow["vuln_id"].ToString();
                var severity = "";
                var vulnType = "";
                var recSignature = dataRow["signature"].ToString();                                
                o2Rules.Add(new O2Rule
                {
                    DbId = ruleDbId,
                    RuleType = O2RuleType.DontPropagateTaint,
                    Severity = severity,
                    VulnType = vulnType,
                    Signature = recSignature,                    
                });
            }
            mySqlDataReader.Close();
            return o2Rules;
        }

        public List<IO2Rule> getRules_PropagateTaint(string ruleDbId)
        {
            var o2Rules = new List<IO2Rule>();
            var sql = "select rec.signature, taint_info.from_args, taint_info.to_args, taint_info.return " +
                      "from rec,taint_info where taint_info.vuln_id = rec.vuln_id and propagates=1 and " +
                      "rec.db_id=" + ruleDbId;

            var mySqlDataReader = OunceMySql.executeSqlQueryReturnSqlDataReader(sql);

            foreach (DbDataRecord dataRow in mySqlDataReader)
            {                
                var recSignature = dataRow["signature"].ToString();
                var fromArgs = dataRow["from_Args"].ToString();
                var toArgs = dataRow["to_Args"].ToString();
                var _return = dataRow["return"].ToString();
                o2Rules.Add(new O2Rule
                {
                    DbId = ruleDbId,
                    RuleType = O2RuleType.PropageTaint,                    
                    Signature = recSignature,                    
                    Return = _return,
                    FromArgs = fromArgs,
                    ToArgs = toArgs
                });
            }
            mySqlDataReader.Close();
            return o2Rules;
        }

        public List<IO2Rule> addRulesToDatabase(IEnumerable<IO2Rule> o2RulesToAdd)
        {
            var rulesNotProcessed = new List<IO2Rule>();
            var rulesAdded = 0;

            // Handle the deletion of rules (since they have to occur first
            var rulesToDelete = from IO2Rule o2Rule in o2RulesToAdd where (o2Rule.RuleType == O2RuleType.ToBeDeleted) select o2Rule;
            o2RulesToAdd = o2RulesToAdd.Except(rulesToDelete);                        
            foreach (IO2Rule o2Rule in rulesToDelete)
                deleteRuleFromDatabase(o2Rule);
            
            foreach (IO2Rule o2Rule in o2RulesToAdd)
            {                
                if (false == addRuleToDatabase(o2Rule, true))
                    rulesNotProcessed.Add(o2Rule);
                if (rulesAdded ++ % 200 == 0)
                {
                    DI.log.debug("... rules added so far: {0}", rulesAdded);
                }
            }
            if (rulesToDelete.Count() > 0)
                DI.log.info("Total number of rules deleted: {0}", rulesAdded);
            DI.log.info("Total number of rules added: {0}", rulesAdded);
            if (rulesNotProcessed.Count > 0)
                DI.log.info("Total number of rules NOT processed: {0}", rulesNotProcessed.Count);
            return rulesNotProcessed;
        }

        public List<IO2Rule> addRulesToDatabase(bool bDeleteDatabase, O2RulePack o2rulePack)
        {
            var rulesNotProcessed = new List<IO2Rule>();
            if (bDeleteDatabase)
                Lddb_OunceV6.action_DeleteAllRules();
            //Utils.debugBreak();
            //removeRulesFromCache();
            DI.log.info("Adding {0} rules to database", o2rulePack.o2Rules.Count);
            foreach (var o2Rule in o2rulePack.o2Rules)
                if (false == addRuleToDatabase(o2Rule))
                    rulesNotProcessed.Add(o2Rule);
            DI.log.info("Completed adding {0} rules to database", o2rulePack.o2Rules.Count);
            if (rulesNotProcessed.Count > 0)
                DI.log.info("Total number of rules NOT processed: {0}", rulesNotProcessed.Count);
            return rulesNotProcessed;
        }

        
        
        public bool addRuleToDatabase(IO2Rule rRule)
        {
            return addRuleToDatabase(rRule,O2RulePackUtils.bVerbose);
        }

        public void deleteRulesFromDatabase(IEnumerable<IO2Rule> rulesToDelete)
        {
            Lddb_OunceV6.action_deleteRulesFromDatabase(rulesToDelete);
        }

        public void deleteRuleFromDatabase(IO2Rule ruleToDelete)
        {
            Lddb_OunceV6.action_deleteRuleFromDatabase(ruleToDelete);
        }

        public bool addRuleToDatabase(IO2Rule rRule, bool bVerbose)
        {            
            var sVulnId = "0"; // make it 0 since it doesn't matter this value
            switch (rRule.RuleType)
            {
                case O2RuleType.Source:
                    var sActionObjectSignatureForSource = "InputAnyTainted";
                    Lddb_OunceV6.action_makeMethod_Source(rRule.DbId, rRule.Signature, sVulnId,
                                                          sActionObjectSignatureForSource, rRule.Severity, rRule.VulnType,
                                                          bVerbose);
                    break;
                case O2RuleType.Sink:
                    var sActionObjectSignatureForSink = "OutputAnyNotValidated";
                    Lddb_OunceV6.action_makeMethod_Sink(rRule.DbId, rRule.Signature, sVulnId, sActionObjectSignatureForSink,
                                                        rRule.Severity, rRule.VulnType, bVerbose);
                    break;
                case O2RuleType.Callback:                    
                    Lddb_OunceV6.action_makeMethodACallback(UInt32.Parse(rRule.DbId), rRule.Signature,
                                                            true /*bDeletePreviousRulesForSignature*/, bVerbose);
                    break;
                case O2RuleType.PropageTaint:
                    Lddb_OunceV6.action_makeMethod_TaintPropagator(UInt32.Parse(rRule.DbId), rRule.Signature,rRule.FromArgs,rRule.ToArgs,rRule.Return.ToString());
                    break;
                case O2RuleType.DontPropagateTaint:
                    Lddb_OunceV6.action_makeMethod_NotPropagateTaint(UInt32.Parse(rRule.DbId), rRule.Signature);
                    break;                
                default:
                    DI.log.error("Rule type not supported: {0}  Rule changed not processed: {1}",
                        rRule.RuleType, rRule.Signature);
                    return false;
            }
            return true;
        }
       
        public List<IO2Rule> getRules_AnyHigh(string ruleDbId)
        {
            return getRules_VulnType("AnyHigh", "AnyHigh", ruleDbId);
        }

        public List<IO2Rule> getRules_AnyMedium(string ruleDbId)
        {
            return getRules_VulnType("AnyMedium", "AnyMedium", ruleDbId);
        }

        public List<IO2Rule> getRules_AnyLow(string ruleDbId)
        {
            return getRules_VulnType("AnyLow", "AnyLow", ruleDbId);
        }

        public List<IO2Rule> getRules_VulnType(string vulnTypeInMySql, string addAsRuleOfType, string ruleDbId)
        {            
            var o2Rules = new List<IO2Rule>();
            var sqlForSinks =
                    "select rec.signature as recSignature, " +
                    "actionobjects.severity, actionobjects.vuln_type " +
                    "from actionobjects , rec " +
                    "where actionobjects.vuln_id = rec.vuln_id and actionobjects.signature ='" + vulnTypeInMySql  + "' and " +
                    "actionobjects.db_id=" + ruleDbId;

            var mySqlDataReader = OunceMySql.executeSqlQueryReturnSqlDataReader(sqlForSinks);

            foreach (DbDataRecord dataRow in mySqlDataReader)
            {
                var recSignature = dataRow["recSignature"].ToString();                
                var severity = dataRow["severity"].ToString();
                var vulnType = addAsRuleOfType + "." + dataRow["vuln_type"].ToString();                                
                o2Rules.Add(new O2Rule
                {
                    DbId = ruleDbId,
                    RuleType = O2RuleType.Sink,
                    Severity = severity,
                    VulnType = vulnType,
                   
                    Signature = recSignature,                    
                });
            }
            mySqlDataReader.Close();
            return o2Rules;            
        }



        // ounce 6.0 dependent

        public List<Rules> getListOfCustomRules(bool bMapActionObjects)
        {
            var sSqlForRule = "Select * from rec WHERE  (rec.added=true OR rec.modified=true)";
            //DataTable dtResults_forRule = o2.ounce.datalayer.mysql.OunceMySql.getDataTableFromSqlQuery(sSqlForRule);
            var lrCustomRules = new List<Rules>();
            OunceMySql.populateListWithDataTable(sSqlForRule, lrCustomRules);
            foreach (Rules rRule in lrCustomRules)
            {
                rRule.lActionObjects = new List<ActionObject>();
                if (bMapActionObjects)
                {
                    String sSqlForActionObject =
                        String.Format("Select * from actionobjects WHERE  actionobjects.vuln_id = {0}", rRule.vuln_id);
                    OunceMySql.populateListWithDataTable(sSqlForActionObject, rRule.lActionObjects);
                }
            }

            return lrCustomRules;
        }

    }
}
