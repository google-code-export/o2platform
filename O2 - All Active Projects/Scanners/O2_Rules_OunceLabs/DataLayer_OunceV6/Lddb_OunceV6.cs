// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Data;
using O2.Interfaces.Rules;
using O2.Rules.OunceLabs.DataLayer_OunceV6;

namespace O2.Rules.OunceLabs.DataLayer
{
    public class Lddb_OunceV6
    {
        private static readonly String[] asCachedValues_severity = new[] {"High", "Low", "Medium", "Info"};

        private static readonly String[] asCachedValues_signature = new[]
                                                                        {
                                                                            "Any", "AnyHigh", "AnyLow", "AnyMedium",
                                                                            "ApplicationName1stCommand2ndContainsWhiteSpace"
                                                                            ,
                                                                            "ApplicationName2ndCommand3rdContainsWhiteSpace"
                                                                            , "ChecksGetLastErrorOrLpName2ndNull",
                                                                            "ChecksGetLastErrorOrLpName3rdNull",
                                                                            "ChecksGetLastErrorOrLpName4thNull",
                                                                            "ChecksGetLastErrorOrLpName6thNull",
                                                                            "Dacl3rdDaclFlag2ndNull", "DaclFlag2ndFalse"
                                                                            , "Dst1stFormat2ndArgs3rdCPrintfFormat",
                                                                            "Dst1stFormat2ndArgs3rdCPrintfFormatInternalSrc"
                                                                            , "Dst1stFormat2ndVa_list3rdCPrintfFormat",
                                                                            "Dst1stFourTimesSrc2ndBufferOverflowString",
                                                                            "Dst1stFourTimesSrc2ndBufferOverflowStringInternalSrc"
                                                                            ,
                                                                            "Dst1stSize2ndFormat3rdArgs4thCPrintfFormat"
                                                                            ,
                                                                            "Dst1stSize2ndFormat3rdArgs4thCPrintfFormatInternalSrc"
                                                                            ,
                                                                            "Dst1stSize2ndFormat3rdVa_list4thCPrintfFormat"
                                                                            ,
                                                                            "Dst1stSrc2ndBufferOverflowString",
                                                                            "Dst1stSrc2ndBufferOverflowStringInternalSrc"
                                                                            , "Dst1stSrc2ndSize3rdBufferOverflow",
                                                                            "Dst1stSrc2ndSize3rdBufferOverflowInternalSrc"
                                                                            ,
                                                                            "Dst1stSrcExternalLength2ndCount3rdBufferOverflow"
                                                                            , "Dst1stSrcExternalSize2ndBufferOverflow",
                                                                            "Dst2ndSrc1stSize3rdBufferOverflow",
                                                                            "Dst2ndSrc1stSize3rdBufferOverflowInternalSrc"
                                                                            , "Dst2ndSrcExternalSize3rdBufferOverflow",
                                                                            "Dst2ndSrcInt1stRadixInt3rdSize4",
                                                                            "Dst2ndSrcInt1stRadixInt3rdSize4Unsigned",
                                                                            "Dst2ndSrcInt1stRadixInt3rdSize4Unsigned_copy"
                                                                            , "Dst2ndSrcInt1stRadixInt3rdSize4_copy",
                                                                            "Dst2ndSrcInt1stRadixInt3rdSize8",
                                                                            "Dst2ndSrcInt1stRadixInt3rdSize8Unsigned",
                                                                            "Dst2ndSrcInt1stRadixInt3rdSize8Unsigned_copy"
                                                                            , "Dst2ndSrcInt1stRadixInt3rdSize8_copy",
                                                                            "Dst3rdSrc1stSizeLessThan_CVTBUFSIZE",
                                                                            "Dst3rdSrc1stSizeLessThan_CVTBUFSIZE_copy",
                                                                            "Dst4thSrc1stBufferOverflowString",
                                                                            "Dst4thSrc1stBufferOverflowStringInternalSrc"
                                                                            , "ExecutionPath1stAbsolute",
                                                                            "ExecutionPath1stContainsWhiteSpace",
                                                                            "ExecutionPath1stRelative",
                                                                            "ExecutionPath1stTainted",
                                                                            "ExecutionPath2ndAbsolute",
                                                                            "ExecutionPath2ndRelative",
                                                                            "ExecutionPath2ndTainted",
                                                                            "ExecutionPath3rdAbsolute",
                                                                            "ExecutionPath3rdRelative",
                                                                            "ExecutionPath3rdTainted",
                                                                            "ExitOutsideOfJavaMain",
                                                                            "FileProtectionFlag3rd",
                                                                            "FILE_FLAG_BACKUP_SEMANTICS_6th",
                                                                            "FILE_FLAG_FIRST_PIPE_INSTANCE_2nd",
                                                                            "Format1stArgs2ndCScanfFormat",
                                                                            "Format1stCScanfStringUnboundedSpecifier",
                                                                            "Format1stTainted",
                                                                            "Format1stVa_list2ndCScanfFormat",
                                                                            "Format2ndArgs3rdCScanfFormat",
                                                                            "Format2ndCScanfStringUnboundedSpecifier",
                                                                            "Format2ndTainted",
                                                                            "Format2ndVa_list3rdCScanfFormat",
                                                                            "Format3rdTainted",
                                                                            "FunctionAddressComparison",
                                                                            "ImpersonationFlags6thFileName1stTainted",
                                                                            "ImpersonationReturnChecked",
                                                                            "InputAnyTainted", "InsecureRandom",
                                                                            "LibraryPath1stRelative",
                                                                            "Log4JDeprecatedClass", "NotInFinally",
                                                                            "OPEN_ALWAYS_Flag5th", "OptionBasedTester",
                                                                            "OptionBasedTester_copy1",
                                                                            "OptionBasedTester_copy2",
                                                                            "OutputAfter1stNotValidated",
                                                                            "OutputAnyNotValidated", "OutputAnyTainted",
                                                                            "RaceCondition", "ReturnCheckedMedium",
                                                                            "SecurityDescriptor1stNullName2ndNotNull",
                                                                            "SecurityDescriptor1stNullName3rdNotNull",
                                                                            "SecurityDescriptor1stNullName4thNotNull",
                                                                            "SecurityDescriptor2ndNull",
                                                                            "SecurityDescriptor2ndNullName6thNotNull",
                                                                            "SecurityDescriptor8thNull",
                                                                            "ShellCommand1stTainted",
                                                                            "Src1stAnyInternalSrc",
                                                                            "Src1stFormat2ndArgs3rdCScanfFormat",
                                                                            "Src1stFormat2ndVa_list3rdCScanfFormat",
                                                                            "Src1stFormat2ndVa_list3rdCScanfFormatInternalSrc"
                                                                            , "Src1stTainted", "Src1stUnterminated",
                                                                            "Src2ndAnyInternalSrc",
                                                                            "Src2ndAnyNonInternalSrc", "Src2ndTainted"
                                                                        };

        private static readonly String[] asCachedValues_type = new[] {"Vulnerability", "informational"};

        private static readonly String[] asCachedValues_vuln_type = new[]
                                                                        {
                                                                            "Vulnerability.BufferOverflow",
                                                                            "Vulnerability.PrivilegeEscalation",
                                                                            "Vulnerability.RaceCondition",
                                                                            "Vulnerability.Miscellaneous",
                                                                            "Vulnerability.BufferOverflow.FormatString",
                                                                            "Vulnerability.Quality.Unsupported",
                                                                            "Vulnerability.ErrorHandling.RevealDetails.Message"
                                                                            ,
                                                                            "Vulnerability.CrossSiteScripting.Reflected"
                                                                            ,
                                                                            "Vulnerability.ErrorHandling.RevealDetails.StackTrace"
                                                                            ,
                                                                            "Vulnerability.Validation.EncodingRequired",
                                                                            "Vulnerability.Logging.Required",
                                                                            "Vulnerability.Info",
                                                                            "Vulnerability.Malicious.Trojan",
                                                                            "Vulnerability.Malicious.DynamicCode",
                                                                            "Vulnerability.AppDOS",
                                                                            "Vulnerability.Validation.Required",
                                                                            "Vulnerability.Cryptography.PoorEntropy",
                                                                            "Vulnerability.Injection.OS",
                                                                            "Vulnerability.AppDOS.Shutdown",
                                                                            "Vulnerability.Native.Library",
                                                                            "Vulnerability.Malicious.Trigger",
                                                                            "Vulnerability.ErrorHandling",
                                                                            "Vulnerability.Malicious.Debugger",
                                                                            "Vulnerability.Authentication.Entity",
                                                                            "Vulnerability.Quality.NeverCall",
                                                                            "Vulnerability.Injection.SQL",
                                                                            "Vulnerability.Injection",
                                                                            "Vulnerability.Validation.Required.HiddenFields"
                                                                            , "Vulnerability.Validation.ClientOnly",
                                                                            "Vulnerability.Cryptography.NonStandard",
                                                                            "Vulnerability.Malicious",
                                                                            "Vulnerability.Quality.TestCode",
                                                                            "Vulnerability.Cryptography",
                                                                            "Vulnerability.SessionManagement.Cookies",
                                                                            "Vulnerability.CrossSiteScripting",
                                                                            "Vulnerability.AppDOS.ConnectionClose",
                                                                            "Vulnerability.Privacy",
                                                                            "Vulnerability.Quality.Comments",
                                                                            "Vulnerability.Malicious.DynamicCode.Compiler"
                                                                            ,
                                                                            "Vulnerability.Malicious.DynamicCode.Construction"
                                                                            , "Vulnerability.Authentication",
                                                                            "Vulnerability.Native.Unsafe",
                                                                            "Vulnerability.Malicious.DynamicCode.Loading"
                                                                            , "Vulnerability.AccessControl",
                                                                            "Vulnerability.Authentication.Credentials.Unprotected.Transport"
                                                                            ,
                                                                            "Vulnerability.Authentication.Credentials.Unprotected"
                                                                            , "Vulnerability.AccessControl.Bypass",
                                                                            "Vulnerability.Quality",
                                                                            "Vulnerability.Integrity",
                                                                            "Vulnerability.Native",
                                                                            "Vulnerability.Caching.Browser",
                                                                            "Vulnerability.Malicious.DynamicCode.Execution"
                                                                            ,
                                                                            "Vulnerability.Authentication.Credentials.Weak"
                                                                            ,
                                                                            "Vulnerability.Caching",
                                                                            "Vulnerability.AccessControl.Impersonation",
                                                                            "Vulnerability.Injection.LDAP",
                                                                            "Vulnerability.AppDOS.Flood",
                                                                            "Vulnerability.SessionManagement.Timeout.Absolute"
                                                                            , "Vulnerability.Native.Method",
                                                                            "Vulnerability.Logging",
                                                                            "Vulnerability.Concurrency.Singleton",
                                                                            "Vulnerability.AppDOS.Lockout",
                                                                            "Vulnerability.Malicious.EasterEgg",
                                                                            "Vulnerability.ErrorHandling.Missing",
                                                                            "Vulnerability.Communications.Unencrypted",
                                                                            "Vulnerability.Cryptography.InsecureAlgorithm"
                                                                            , "Vulnerability.Quality.Deprecated",
                                                                            "Vulnerability.Quality.Not_Implemented"
                                                                        };

        private static readonly UInt32[] auCachedValues_trace = new UInt32[] {0, 1, 2, 3};

        public static String action_getActionObjectDetailsForVulnID(UInt32 uVuln_Id)
        {
            String sActionObjectDetails = "";
            if (uVuln_Id > 0)
            {
                String sSql = "Select * from actionobjects where vuln_id =" + uVuln_Id;
                DataTable dtDataTable = OunceMySql.getDataTableFromSqlQuery(sSql);

                foreach (DataRow drRow in dtDataTable.Rows)
                    sActionObjectDetails += "(" + drRow["vuln_type"] + " , " + drRow["signature"] + ")   ";
            }
            return sActionObjectDetails;
        }

        public static UInt32[] action_getDistinct_db_id()
        {
            var lsLanguages = new List<UInt32>();
            String sSqlLanguages = "Select id from hdr";
            DataTable dtDataTable = OunceMySql.getDataTableFromSqlQuery(sSqlLanguages, true);
            foreach (DataRow dtLanguage in dtDataTable.Rows)
                lsLanguages.Add((UInt32) dtLanguage["id"]);
            return lsLanguages.ToArray();
        }

        public static UInt32[] action_getDistinct_trace(bool bUsedCachedValues)
        {
            if (bUsedCachedValues)
                return auCachedValues_trace;
            return null;
            // for performace reasons let's not go to the db at this stage
            /*  List<UInt32> lsLanguages = new List<UInt32>();
              String sSqlLanguages = "Select Distinct (trace) from actionobjects";
              System.Data.DataTable dtDataTable = getDataTableFromSqlQuery(sSqlLanguages, true);
              foreach (DataRow dtLanguage in dtDataTable.Rows)
                  lsLanguages.Add((UInt32)dtLanguage["trace"]);
              return lsLanguages.ToArray(); */
        }

        public static String[] action_getDistinct_signature(bool bUsedCachedValues)
        {
            if (bUsedCachedValues)
                return asCachedValues_signature;
            // if bUserCachedValues = false  do the expensive Sql query to calculate the
            var lsSignatures = new List<String>();
            String sSqlLanguages = "Select Distinct (signature) from actionobjects order by signature ASC";
            DataTable dtDataTable = OunceMySql.getDataTableFromSqlQuery(sSqlLanguages, true);
            foreach (DataRow dtLanguage in dtDataTable.Rows)
                lsSignatures.Add((String) dtLanguage["signature"]);
            return lsSignatures.ToArray();
        }

        public static String[] action_getDistinct_vuln_type(bool bUsedCachedValues)
        {
            if (bUsedCachedValues)
                return asCachedValues_vuln_type;
            return null; // new String[] { };
        }

        public static String[] action_getDistinct_type(bool bUsedCachedValues)
        {
            if (bUsedCachedValues)
                return asCachedValues_type;
            return null;
        }

        public static String[] action_getDistinct_severity(bool bUsedCachedValues)
        {
            if (bUsedCachedValues)
                return asCachedValues_severity;
            return null;
        }

        public static String action_getMethodSignatureFromActionObjectId(UInt32 uActionObjectId)
        {
            if (uActionObjectId > 0)
            {
                String sSqlGetMethodSignature =
                    String.Format(
                        "Select rec.signature from rec , actionobjects  WHERE actionobjects.id = {0} and actionobjects.vuln_id = rec.vuln_id",
                        uActionObjectId);
                Object oSignature = OunceMySql.executeSqlQuery(sSqlGetMethodSignature, true);
                if (oSignature != null)
                {
                    return (String) oSignature;
                }
            }
            return "";
        }

        public static UInt32 action_getDbIDFromActionObjectId(UInt32 uActionObjectId)
        {
            if (uActionObjectId > 0)
            {
                String sSqlGetDbID = String.Format("Select db_id from actionobjects  WHERE id={0}", uActionObjectId);
                Object oData = OunceMySql.executeSqlQuery(sSqlGetDbID, true);
                if (oData != null)
                    return (UInt32) oData;
                else
                    return 0;
            }
            return 0;
        }

        public static UInt32 action_getDbIDfromVuln_id(UInt32 uVulnId)
        {
            if (uVulnId > 0)
            {
                String sSqlGetDbID = String.Format("Select db_id from rec  WHERE vuln_id={0}", uVulnId);
                Object oReturnData = OunceMySql.executeSqlQuery(sSqlGetDbID, true);
                if (oReturnData != null)
                    return (UInt32) oReturnData;
            }
            return 0;
        }

        public static void action_deleteLddbPropertyXref(UInt32 uDbId, UInt32 uVulnId)
        {
            String sSqlDeleteLddbPropertyXref =
                String.Format(
                    "DELETE from property_xref  WHERE db_ref={0} and object_ref={1} and object_type='vulnerability' and added=true",
                    uDbId, uVulnId);
            OunceMySql.executeSqlQuery(sSqlDeleteLddbPropertyXref, true);
        }

        // this is a bit radical since it deletes all existing ActionObjects and taint_info
        public static void action_deleteSignatureAndActionObject(UInt32 uVulnId)
        {
            if (uVulnId == 0)
                return;

            // get the dbId for this action_object
            UInt32 uDbId = action_getDbIDfromVuln_id(uVulnId);
                        
            // delete this vuln ID from rec                
            String sSQLDeleteLddbRec = string.Format("DELETE from rec  WHERE vuln_id={0}", uVulnId);
            OunceMySql.executeSqlQuery(sSQLDeleteLddbRec, true);

            // delete entry from property_xref
            action_deleteLddbPropertyXref(uDbId, uVulnId);

            // delete entry in taint_into
            String sSqlDeleteLddbTaintInfo = String.Format("DELETE from taint_info WHERE vuln_id={0}", uVulnId);
            OunceMySql.executeSqlQuery(sSqlDeleteLddbTaintInfo, true);

            // delete entry in validation_descriptor
            String sSqlDeleteLddbValidationDescriptor =
                String.Format("DELETE from validation_descriptor WHERE record_id={0}", uVulnId);
            OunceMySql.executeSqlQuery(sSqlDeleteLddbValidationDescriptor, true);

            // finally set all vuln_id in actionobjects to 0  (this means we don't need to delete the entries related to actionObjects (sink, source
            String sSqlDeleteActionObject = String.Format("update actionobjects set vuln_id = 0  where vuln_id = {0}",
                                                          uVulnId);
            OunceMySql.executeSqlQuery(sSqlDeleteActionObject, true);
            //      DI.log.info("Deleted Signature with vulnId: {0}", uVulnId);
        }

        public static void action_CreateLddbRec(UInt32 uMaxVuln, UInt32 uDbId, String sVulnName, String sSignature,
                                                UInt32 uCallback)
        {
            String sSqlCreateLddbRec =
                String.Format(
                    "INSERT into rec (vuln_id, db_id, vuln_name, signature, added, modified,callback) VALUES({0},{1},'{2}','{3}',1,0,{4})",
                    uMaxVuln, uDbId, sVulnName, sSignature, uCallback);
            OunceMySql.executeSqlQuery(sSqlCreateLddbRec, true);
        }

        public static bool action_makeMethodACallback(UInt32 uDbId, String sMethodSignature)
        {
            return action_makeMethodACallback(uDbId, sMethodSignature, false, true);
        }

        public static bool action_makeMethodACallback(UInt32 uDbId, String sMethodSignature,
                                                      bool bDeletePreviousRulesForSignature, bool bVerbose)
        {
            if (bVerbose)
                DI.log.info("Making method [{0}] {1} a Callback", uDbId.ToString(), sMethodSignature);
            UInt32 uVulnId = action_getVulnIdThatMatchesSignature(uDbId.ToString(), sMethodSignature, false);
            if (uVulnId > 0)
                if (bDeletePreviousRulesForSignature)
                {
                    DI.log.debug("in action_makeMethodACallback: There was already an entry in the db for this rule, so all related rules (sources,sinks, taint propagation,etc..) will be deleted: {0}", sMethodSignature);
                    action_deleteSignatureAndActionObject(uVulnId);
                }
                else
                {
                    if (bVerbose)
                        DI.log.error(
                            "in action_makeMethodACallback: There was already an entry in the db for this rule, so can't add a callback: {0}",
                            sMethodSignature);
                    return false;
                }
            //if (action_getVulnIdThatMatchesSignature(uDbId.ToString(), sMethodSignature, false) != 0)          
            //{  }
            String sVulnName = sMethodSignature;
            Int32 iIndexOfLeftBracket = sMethodSignature.IndexOf('(');
            if (iIndexOfLeftBracket > -1)
                sVulnName = sMethodSignature.Substring(0, iIndexOfLeftBracket);


            String sMaxVulnId = "SELECT MAX(vuln_id) from rec";
            var uMaxVuln = (UInt32) OunceMySql.executeSqlQuery(sMaxVulnId, true);
            uMaxVuln++;
            UInt32 uCallback = 1;
            action_CreateLddbRec(uMaxVuln, uDbId, sVulnName, sMethodSignature, uCallback);
            return true;
        }


        public static bool action_makeMethod_Validator(UInt32 uDbId, String sMethodSignature)
        {
            DI.log.debug("Making method [{0}] {1} a Validator", uDbId.ToString(), sMethodSignature);
            String sVulnName = "";
            String sSignature = "";
            // Need to figure out what this signature is for? (in the rules created it seems to be blank
            String sTrace_type = "2"; // Need to figure out what the other values are
            Int32 iIndexOfLeftBracket = sMethodSignature.IndexOf('(');
            if (iIndexOfLeftBracket > -1)
                sVulnName = sMethodSignature.Substring(0, iIndexOfLeftBracket);

            UInt32 uMaxVuln = action_getVulnIdThatMatchesSignature(uDbId.ToString(), sMethodSignature, false);
            if (uMaxVuln == 0) // means there is no entry in the db for this signature
            {
                String sMaxVulnId = "SELECT MAX(vuln_id) from rec";
                uMaxVuln = (UInt32) OunceMySql.executeSqlQuery(sMaxVulnId, true);
                uMaxVuln++;
                UInt32 uCallback = 0;
                action_CreateLddbRec(uMaxVuln, uDbId, sVulnName, sMethodSignature, uCallback);
            }
            //check if rule already exists in validation_descriptor
            String sSqlCheckIfRuleAlreadyExists =
                String.Format("Select Count(record_id) from validation_descriptor where record_id = {0}", uMaxVuln);
            if (0 == (Int64) OunceMySql.executeSqlQuery(sSqlCheckIfRuleAlreadyExists, false))
            {
                action_deleteLddbPropertyXref(uDbId, uMaxVuln);

                String sSqlCreateLddbTaintInfo =
                    String.Format(
                        "INSERT into validation_descriptor (record_id, signature, trace_type, added,db_id) VALUES({0},'{1}',{2},{3},{4})",
                        uMaxVuln, sSignature, sTrace_type, 1, uDbId);
                OunceMySql.executeSqlQuery(sSqlCreateLddbTaintInfo, true);
                return true;
            }
            else
            {
                DI.log.error(
                    "in action_makeMethod_TaintPropagator: There was already an entry in the db (validation_descriptor) for this record_id (i.e. vuln_id) {0}:{1}",
                    uMaxVuln.ToString(), sMethodSignature);
                return false;
            }
            //  return true;
        }

        // this next method of code could be consolidated with the one in action_makeMethod_TaintPropagator
        public static bool action_makeMethod_NotPropagateTaint(UInt32 uDbId, String sMethodSignature)
        {
            DI.log.debug("Making method [{0}] {1} a NotPropagateTaint", uDbId.ToString(), sMethodSignature);
            String sVulnName = "";
            Int32 iIndexOfLeftBracket = sMethodSignature.IndexOf('(');
            if (iIndexOfLeftBracket > -1)
                sVulnName = sMethodSignature.Substring(0, iIndexOfLeftBracket);
            UInt32 uMaxVuln = action_getVulnIdThatMatchesSignature(uDbId.ToString(), sMethodSignature, false);
            if (uMaxVuln == 0) // means there is no entry in the db for this signature
            {
                String sMaxVulnId = "SELECT MAX(vuln_id) from rec";
                uMaxVuln = (UInt32) OunceMySql.executeSqlQuery(sMaxVulnId, true);
                uMaxVuln++;
                UInt32 uCallback = 0;
                action_CreateLddbRec(uMaxVuln, uDbId, sVulnName, sMethodSignature, uCallback);                
            }

            String sSqlCheckIfRuleAlreadyExists = String.Format("Select Count(vuln_id) from taint_info where vuln_id = {0}", uMaxVuln);
            if (0 != (Int64) OunceMySql.executeSqlQuery(sSqlCheckIfRuleAlreadyExists, false))
             {
                 //in action_makeMethod_TaintPropagator: There was already an entry in the db (so we have to delete it first            
                 String sSqlDeleteExistingRule =
                String.Format("Delete from taint_info where vuln_id = {0}", uMaxVuln);
                 OunceMySql.executeSqlQuery(sSqlCheckIfRuleAlreadyExists, false);                 
             }

                action_deleteLddbPropertyXref(uDbId, uMaxVuln);

              /*  if (taintInfo_fromArgs == "")
                    taintInfo_fromArgs = "all";
                if (taintInfo_toArgs == "")
                    taintInfo_toArgs = "all";
                if (taintInfo_return == "")
                    taintInfo_return = "0";*/

                String sSqlCreateLddbTaintInfo =
                    String.Format(
                        "INSERT into taint_info (vuln_id, taint_info.from_args, taint_info.to_args, taint_info.return, propagates, db_id, added) VALUES({0},'none','none',0,0,{1},1)",
                        uMaxVuln, uDbId);
                OunceMySql.executeSqlQuery(sSqlCreateLddbTaintInfo, true);

                return true;            
            

            //   return true;
        }

        public static bool action_makeMethod_TaintPropagator(UInt32 uDbId, String sMethodSignature, string taintInfo_fromArgs, string taintInfo_toArgs, string taintInfo_return)
        {
            DI.log.debug("Making method [{0}] {1} a TaintPropagator", uDbId.ToString(), sMethodSignature);
            String sVulnName = "";
            Int32 iIndexOfLeftBracket = sMethodSignature.IndexOf('(');
            if (iIndexOfLeftBracket > -1)
                sVulnName = sMethodSignature.Substring(0, iIndexOfLeftBracket);

            UInt32 uMaxVuln = action_getVulnIdThatMatchesSignature(uDbId.ToString(), sMethodSignature, false);
            if (uMaxVuln == 0) // means there is no entry in the db for this signature
            {
                String sMaxVulnId = "SELECT MAX(vuln_id) from rec";
                uMaxVuln = (UInt32) OunceMySql.executeSqlQuery(sMaxVulnId, true);
                uMaxVuln++;
                UInt32 uCallback = 0;
                action_CreateLddbRec(uMaxVuln, uDbId, sVulnName, sMethodSignature, uCallback);
            }

            //check if rule already exists in taint_info
            String sSqlCheckIfRuleAlreadyExists = String.Format("Select Count(vuln_id) from taint_info where vuln_id = {0}", uMaxVuln);
            if (0 != (Int64) OunceMySql.executeSqlQuery(sSqlCheckIfRuleAlreadyExists, false))
             {
                 //in action_makeMethod_TaintPropagator: There was already an entry in the db (so we have to delete it first            
                 String sSqlDeleteExistingRule =
                String.Format("Delete from taint_info where vuln_id = {0}", uMaxVuln);
                 OunceMySql.executeSqlQuery(sSqlDeleteExistingRule, false);
                 ;
             }
            /*String sSqlCheckIfRuleAlreadyExists =
                String.Format("Select Count(vuln_id) from taint_info where vuln_id = {0}", uMaxVuln);
            if (0 == (Int64) OunceMySql.executeSqlQuery(sSqlCheckIfRuleAlreadyExists, false))
            {*/
                action_deleteLddbPropertyXref(uDbId, uMaxVuln);

                if (taintInfo_fromArgs == "")
                    taintInfo_fromArgs = "all";
                if (taintInfo_toArgs == "")
                    taintInfo_toArgs = "all";
                if (taintInfo_return == "")
                    taintInfo_return = "0";
                String sSqlCreateLddbTaintInfo =
                    String.Format(
                        "INSERT into taint_info (vuln_id, taint_info.from_args, taint_info.to_args, taint_info.return, db_id, propagates, added) VALUES({0},'{1}','{2}',{3},{4},1,1)",
                        uMaxVuln, taintInfo_fromArgs, taintInfo_toArgs, taintInfo_return, uDbId);
                OunceMySql.executeSqlQuery(sSqlCreateLddbTaintInfo, true);
                return true;
            /*}
            else
            {
                DI.log.error(
                    "in action_makeMethod_TaintPropagator: There was already an entry in the db (taint_info) for this vuln_id {0}:{1}",
                    uMaxVuln.ToString(), sMethodSignature);
                return false;
            }*/
        }
       
        public static bool action_makeMethod_Source(String sDbId, String sSignature)
        {
            String sSeverity = "Medium";
            String sVuln_type = "Vulnerability.O2.Sink";
            String sActionObjectSignature = "InputAnyTainted";
            String sVuln_id = "0";
            return action_makeMethod_Source(sDbId, sSignature, sVuln_id, sActionObjectSignature, sSeverity, sVuln_type);
        }

        public static bool action_makeMethod_Source(String sDbId, String sSignature, String sVuln_id,
                                                    String sActionObjectSignature, String sSeverity, String sVuln_type)
        {
            return action_makeMethod_Source(sDbId, sSignature, sVuln_id, sActionObjectSignature, sSeverity, sVuln_type,
                                            true);
        }

        public static bool action_makeMethod_Source(String sDbId, String sSignature, String sVuln_id,
                                                    String sActionObjectSignature, String sSeverity, String sVuln_type,
                                                    bool bVerbose)
        {
            String sAdded = "1";
            String sTrace_type = "2";
            String sSource_Param = "all";
            String sSource_Return = "1";
            String sType = "vulnerability";
            if (bVerbose)
                DI.log.info("Making method [{0}] {1} a Source", sDbId, sSignature);

            UInt32 uMaxVuln = action_getVulnIdThatMatchesSignature(sDbId, sSignature, false);
            sVuln_id = uMaxVuln.ToString();
            //  sVulnId == 0 means there is no lddb entry for this signature
            if (uMaxVuln == 0)
            {
                // Query #1
                String sMaxVulnId = "SELECT MAX(vuln_id) from rec";
                uMaxVuln = (UInt32) OunceMySql.executeSqlQuery(sMaxVulnId, true);
                uMaxVuln++;
                if (uMaxVuln < 3000000) // move new rules to higher values
                    uMaxVuln = 3000000;
                sVuln_id = uMaxVuln.ToString();

                // Query #2
                String sVuln_name = sSignature;
                Int32 iIndexOfLeftBracket = sSignature.IndexOf('(');
                if (iIndexOfLeftBracket > -1)
                    sVuln_name = sSignature.Substring(0, iIndexOfLeftBracket);

                String sModified = "0";

                String sInsertLddbRec =
                    String.Format(
                        "INSERT rec (vuln_id, db_id, vuln_name, signature, added, modified) VALUES ({0},{1},'{2}','{3}',{4},{5})",
                        sVuln_id, sDbId, sVuln_name, sSignature, sAdded, sModified);
                OunceMySql.executeSqlQuery(sInsertLddbRec, true);
            }
            else
            {
                // add code to check if Source already exists (just like the addd sink case)

                String sSqlSetLddbModifiedFlag = String.Format("UPDATE rec set modified=1 where vuln_id='{0}'", sVuln_id);
                OunceMySql.executeSqlQuery(sSqlSetLddbModifiedFlag, true);
            }

            // Query #3
            String sMaxActionObjectId = "SELECT MAX(id) from actionobjects";
            var uMaxActionObjectId = (UInt32) OunceMySql.executeSqlQuery(sMaxActionObjectId, true);
            uMaxActionObjectId++;
            if (uMaxActionObjectId < 3300000) // move new actionObjects to higher values
                uMaxActionObjectId = 3300000;
            // Query #4

            String sActionObjectId = uMaxActionObjectId.ToString();


            //INSERT into actionobjects (id,signature,vuln_id,severity,type,db_id,vuln_type,trace,added) VALUES(10000000,'OutputAnyNotValidated',2000000,'Low','vulnerability',2,'Vulnerability.AccessControl',3,1)
            String sActionObject =
                String.Format(
                    "INSERT into actionobjects (id,signature,vuln_id,severity,db_id,vuln_type,trace,added) VALUES ({0},'{1}',{2},'{3}',{4},'{5}',{6},{7})",
                    sActionObjectId, sActionObjectSignature, sVuln_id, sSeverity, sDbId, sVuln_type, sTrace_type, sAdded);
            OunceMySql.executeSqlQuery(sActionObject, true);

            // Query #5            		      
            OunceMySql.executeSqlQuery(String.Format("DELETE from source_info 		 WHERE ao_id={0}", sActionObjectId));

            // Query #6                
            String sInsertLddbSinkInfo =
                String.Format("INSERT into source_info (ao_id, param,return) VALUES({0},'{1}',{2})", sActionObjectId,
                              sSource_Param, sSource_Return);
            OunceMySql.executeSqlQuery(sInsertLddbSinkInfo);

            // Query #7
            OunceMySql.executeSqlQuery(String.Format("DELETE from sink_info 		 WHERE ao_id={0}", sActionObjectId));

            // Query #8

            String sAdded_String = "true";
            String sDeleteLddProperty =
                String.Format(
                    " DELETE from property_xref 		 WHERE db_ref={0} and object_ref={1} and object_type='{2}' and added={3}",
                    sDbId, sVuln_id, sType, sAdded_String);


            //String sInsertLddbRec = String.Format("INSERT into rec (vuln_id, db_id, vuln_name, signature, added, modified) VALUES ({0},{1},'{2}','{3}',{4},{5})", sVuln_id, sDbId, sVuln_name, sSignature, sAdded, sModified);
            //  DI.log.debug("New rule added");

            return true;
        }

        public static bool action_makeMethod_Sink(String sDbId, String sMethodSignature, String sVuln_id, bool bVerbose)
        {
            //return action_makeMethod_Sink(sDbId, sMethodSignature,sVuln_id, "AnyHigh", "Low", "Vulnerability.F1.Sink");
            return action_makeMethod_Sink(sDbId, sMethodSignature, sVuln_id, "OutputAnyNotValidated", "Medium",
                                          "Vulnerability.O2.Sink", bVerbose);
        }

        public static bool action_makeMethod_Sink(String sDbId, String sMethodSignature, String sVuln_id,
                                                  String sVuln_type, bool bVerbose)
        {
            //return action_makeMethod_Sink(sDbId, sMethodSignature,sVuln_id, "AnyHigh", "Low", "Vulnerability.F1.Sink");
            return action_makeMethod_Sink(sDbId, sMethodSignature, sVuln_id, "OutputAnyNotValidated", "Medium",
                                          sVuln_type, bVerbose);
        }

        // need to remove the sVulnID from this signature
        public static bool action_makeMethod_Sink(String sDbId, String sSignature, String sVuln_id,
                                                  String sActionObjectSignature, String sSeverity, String sVuln_type,
                                                  bool bVerbose)
        {
            String sVuln_name = sSignature;
            String sAdded = "1";
            String sTrace_type = "3";

            //  sVulnId == 0 means there is no lddb entry for this signature
            sVuln_id = action_getVulnIdThatMatchesSignature(sDbId, sSignature, false).ToString();
            if (sVuln_id == "0")
            {
                // Query #1
                String sMaxVulnId = "SELECT MAX(vuln_id) from rec";
                var uMaxVuln = (UInt32) OunceMySql.executeSqlQuery(sMaxVulnId, true);
                uMaxVuln++;
                if (uMaxVuln < 3000000) // move new rules to higher values
                    uMaxVuln = 3000000;
                sVuln_id = uMaxVuln.ToString();

                // Query #2

                Int32 iIndexOfLeftBracket = sSignature.IndexOf('(');
                if (iIndexOfLeftBracket > -1)
                    sVuln_name = sSignature.Substring(0, iIndexOfLeftBracket);

                String sModified = "0";

                String sInsertLddbRec =
                    String.Format(
                        "INSERT into rec (vuln_id, db_id, vuln_name, signature, added, modified) VALUES ({0},{1},'{2}','{3}',{4},{5})",
                        sVuln_id, sDbId, sVuln_name, sSignature, sAdded, sModified);
                OunceMySql.executeSqlQuery(sInsertLddbRec, true);
            }
            else
            {
                // check if this action object already exists
                String sCheckForSameActionObject =
                    String.Format(
                        "Select id from actionobjects where signature Like '{0}' and vuln_id ={1} and db_id={2}",
                        sActionObjectSignature, sVuln_id, sDbId);
                Object oResult = OunceMySql.executeSqlQuery(sCheckForSameActionObject, true);
                if (null != oResult)
                {
                    if (bVerbose)
                        DI.log.info("Sink already exists, no action taken:  [{0}] {1}", sDbId, sSignature);
                    return false;
                }

                String sSqlSetLddbModifiedFlag = String.Format("UPDATE rec set modified=1 where vuln_id='{0}'", sVuln_id);
                OunceMySql.executeSqlQuery(sSqlSetLddbModifiedFlag, true);
            }
            if (bVerbose)
                DI.log.info("Making method [{0}] {1} a Sink", sDbId, sSignature);


            // Query #3
            String sMaxActionObjectId = "SELECT MAX(id) from actionobjects";
            var uMaxActionObjectId = (UInt32) OunceMySql.executeSqlQuery(sMaxActionObjectId, true);
            uMaxActionObjectId++;
            if (uMaxActionObjectId < 3300000) // move new actionObjects to higher values
                uMaxActionObjectId = 3300000;
            // Query #4

            String sActionObjectId = uMaxActionObjectId.ToString();
            //String sActionObjectSignature = "InputAnyTainted"; // "OutputAnyNotValidated";
            // String Severity = "Low";
            String sType = "vulnerability";
            //  String sVuln_type = "F1.Sink"; // "Vulnerability.AccessControl";                

            //INSERT into actionobjects (id,signature,vuln_id,severity,type,db_id,vuln_type,trace,added) VALUES(10000000,'OutputAnyNotValidated',2000000,'Low','vulnerability',2,'Vulnerability.AccessControl',3,1)


            String sActionObject =
                String.Format(
                    "INSERT into actionobjects (id,signature,vuln_id,severity,db_id,vuln_type,trace,added) VALUES ({0},'{1}',{2},'{3}',{4},'{5}',{6},{7})",
                    sActionObjectId, sActionObjectSignature, sVuln_id, sSeverity, sDbId, sVuln_type, sTrace_type, sAdded);
            OunceMySql.executeSqlQuery(sActionObject, true);

            // Query #5            		      
            OunceMySql.executeSqlQuery(String.Format("DELETE from sink_info 		 WHERE ao_id={0}", sActionObjectId));

            // Query #6
            String sSinkIntoParam = "all";
            String sInsertLddbSinkInfo = String.Format(
                "INSERT into sink_info (ao_id, param, kind) VALUES({0},'{1}','')", sActionObjectId, sSinkIntoParam);
            OunceMySql.executeSqlQuery(sInsertLddbSinkInfo);

            // Query #7
            OunceMySql.executeSqlQuery(String.Format("DELETE from source_info 		 WHERE ao_id={0}", sActionObjectId));

            // Query #8

            String sAdded_String = "true";
            String sDeleteLddProperty =
                String.Format(
                    " DELETE from property_xref 		 WHERE db_ref={0} and object_ref={1} and object_type='{2}' and added={3}",
                    sDbId, sVuln_id, sType, sAdded_String);


            //String sInsertLddbRec = String.Format("INSERT into rec (vuln_id, db_id, vuln_name, signature, added, modified) VALUES ({0},{1},'{2}','{3}',{4},{5})", sVuln_id, sDbId, sVuln_name, sSignature, sAdded, sModified);
            //  DI.log.debug("New rule added");

            return true;
        }

        public static String getSqlQueryForRetrivingActionObjectsData(String sActionObjectsToFetch)
        {
            return "Select " +
                   " actionobjects.id,rec.vuln_id,rec.vuln_name, rec.package, rec.class, " +
                   " actionobjects.vuln_type, actionobjects.signature, actionobjects.severity, actionobjects.type" +
                   " from rec, actionobjects where" +
                   //" rec.db_id=4 and (" +
                   " (" +
                   sActionObjectsToFetch +
                   ") and rec.vuln_id = actionobjects.vuln_id ";
        }

        public static String getSqlQueryStringToSeeCustomRules()
        {
            //String sSqlToFetchCustomRules1 = "SELECT rec.vuln_id, rec.vuln_name, actionobjects.signature , rec.callback,  taint_info.from,  taint_info.to,  taint_info.return,  taint_info.id, taint_info.propagates" +
            //                                    " FROM rec,actionobjects,taint_info" +
            //                                    " WHERE  (rec.added=true OR rec.modified=true)" +
            //                                    " AND (rec.vuln_id = actionobjects.vuln_id) AND (rec.vuln_id = taint_info.vuln_id)";
            return
                "SELECT rec.vuln_id, rec.vuln_name, rec.callback,  taint_info.from,  taint_info.to,  taint_info.return,  taint_info.id, taint_info.propagates" +
                " FROM rec,taint_info" +
                " WHERE  (rec.added=true OR rec.modified=true)" +
                " AND (rec.vuln_id = taint_info.vuln_id)";
        }

        public static Int64 getNumberOfRulesInRecTable()
        {
            string sSqlQuery = "Select count(*) from rec";
            object recCount = OunceMySql.executeSqlQuery(sSqlQuery);
            if (recCount != null)
                return (Int64) recCount;
            return 0;
        }

        public static String getActionObjectName(String sActionObjectId)
        {
            String sActionObjectName = "";
            String sSqlQuery = "Select rec.vuln_name from rec, actionobjects where actionobjects.id=" + sActionObjectId +
                               " and rec.vuln_id = actionobjects.vuln_id";
            DataTable dtResults = OunceMySql.getDataTableFromSqlQuery(sSqlQuery, false);
            if (dtResults.Rows.Count == 1)
            {
                object[] oRowData = (dtResults.Rows[0].ItemArray);
                if (oRowData.Length == 1)
                    sActionObjectName = oRowData[0].ToString();
            }
            return sActionObjectName;
        }

        public static void action_DeleteAllCallbacks()
        {
            String sSql = "DELETE from rec where rec.callback=1";
            object oResult = OunceMySql.executeSqlQuery(sSql, true);
        }

        public static void action_DeleteCallback(UInt32 UDb_Id, String sSignatureOfCallbackToDelete)
        {
            String sSql = String.Format("DELETE from rec where rec.callback=1 and db_id ={0} and signature ='{1}'",
                                        UDb_Id, sSignatureOfCallbackToDelete);
            object oResult = OunceMySql.executeSqlQuery(sSql, true);
        }
        

        public static void action_DeleteAllRules()
        {
            DI.log.debug("Deleting all Rules from Database (but 6 required for the callbacks");
            deleteAllRowsFromTable("rec");

            String sSql = @"delete from actionObjects where vuln_id > 5000000";
            OunceMySql.executeSqlQuery(sSql, true);

            sSql = @"delete from sink_info where ao_id > 3000000";
            OunceMySql.executeSqlQuery(sSql, true);

            sSql = @"delete from taint_info where vuln_id > 3000000";
            OunceMySql.executeSqlQuery(sSql, true);

            sSql = @"delete from source_info where ao_id > 3000000";
            OunceMySql.executeSqlQuery(sSql, true);

            AddtoLldbRecCallbacksSourcesAndDummyRecRecord();
            addDummyActionObjectRule();

            //      deleteAllRowsFromTable("actionObjects");
            //      deleteAllRowsFromTable("taint_info");
            //      deleteAllRowsFromTable("sink_info");
            //      deleteAllRowsFromTable("source_info");
            //        addDummyRecRecord();
        }

        public static void addDummyActionObjectRule()
        {
            String sSql = @"INSERT INTO actionObjects (id) VALUES (0)";
            OunceMySql.executeSqlQuery(sSql, true);
        }

        public static void AddtoLldbRecCallbacksSourcesAndDummyRecRecord()
        {
            //     String sSql = String.Format("INSERT into rec (vuln_id, db_id, vuln_name, signature, added, modified,callback) VALUES (5000000,0,'-','-',0,0,0)");   // make the O2 rules start at 5M (the current OSA rules end at about 1M
            //     OunceMySql.executeSqlQuery(sSql, true);

            addLddbRecRow("1", "1083856", "<external_source>", "<external_source>(...):void", "0", "0", "", "", "0", "0");
            addLddbRecRow("2", "1083855", "<external_source>", "<external_source>(...):void", "0", "0", "", "", "0", "0");
            addLddbRecRow("3", "1083853", "<external_source>", "<external_source>(...):void", "0", "0", "", "", "0", "0");
            addLddbRecRow("4", "1083851", "<external_source>", "<external_source>(...):void", "0", "0", "", "", "0", "0");
            addLddbRecRow("5", "1083854", "<external_source>", "<external_source>(...):void", "0", "0", "", "", "0", "0");
//2, 1083855, '<external_source>', '<external_source>(...):void', 0, 0, '', '', 0, 0
//3, 1083853, '<external_source>', '<external_source>(...):void', 0, 0, '', '', 0, 0
//4, 1083851, '<external_source>', '<external_source>(...):void', 0, 0, '', '', 0, 0
//5, 1083854, '<external_source>', '<external_source>(...):void', 0, 0, '', '', 0, 0

            // String sSql = String.Format("INSERT into rec (vuln_id, db_id, vuln_name, signature, added, modified,callback) VALUES (5000000,0,'-','-',0,0,0)");   
            addLddbRecRow("0", "5000000", "", "", "0", "0", "", "", "0", "0");
            //  OunceMySql.executeSqlQuery(sSql, true);
        }

        public static void addLddbRecRow(String sDbID, String sVulnId, String sVulnName, String sSignature,
                                         String sAdded, String sModified, String sPackage, String sClass,
                                         String sVersion, String sCallback)
        {
            String sSql =
                String.Format(
                    "INSERT into rec (db_id,vuln_id, vuln_name, signature, added, modified,package,class,version,callback) VALUES " +
                    "({0},{1},'{2}','{3}',{4},{5},'{6}','{7}',{8},{9})",
                    sDbID, sVulnId, sVulnName, sSignature, sAdded, sModified, sPackage, sClass, sVersion, sCallback);
            //(5000000,0,'-','-',0,0,0)");   // make the O2 rules start at 5M (the current OSA rules end at about 1M
            OunceMySql.executeSqlQuery(sSql, true);
        }

        public static void deleteAllRowsFromTable(String sTableName)
        {
            String sSql = String.Format("DELETE from {0}", sTableName);
            //object oResult = 
            OunceMySql.executeSqlQuery(sSql, true);
            //       if (oResult != null)
            //            DI.log.debug("# of rules deleted from {0}: {0}", sSql, oResult);
        }

        /* public static void action_DeleteAllCustomRules(bool bAlsoDeleteEdited)
        {
            String sSql;
            if (bAlsoDeleteEdited)
                sSql = "DELETE from rec where rec.added=true";
            else
                sSql = "DELETE from rec where rec.added=true and rec.modified=true";
            object oResult = OunceMySql.executeSqlQuery(sSql, true);
            if (oResult != null)
                 DI.log.debug("# of rules deleted: {0}", oResult);
        }*/

        public static void action_DeleteActionId(UInt32 uActionObjectID)
        {
            // finally set all vuln_id in actionobjects to 0
            String sSqlDisableActionObject = String.Format("update actionobjects set vuln_id = 0  where id = {0}",
                                                           uActionObjectID);
            OunceMySql.executeSqlQuery(sSqlDisableActionObject, true);
        }

        public static UInt32 action_getVulnIdThatMatchesSignature(String sDbId, String sSignature, bool bVerbose)
        {
            String sSqlGetVulnIdFromSignature = String.Format("Select vuln_id from rec where signature = '{0}'",
                                                              sSignature);
            if (sDbId != "")
                sSqlGetVulnIdFromSignature += String.Format(" and db_id = {0}", sDbId);
            else if (bVerbose)
                DI.log.error(
                    "in action_getVulnIdThatMatchesSignature  no dbId was provided, the results might include results for multiple languages: {0}",
                    sSignature);
            Object oResult = OunceMySql.executeSqlQuery(sSqlGetVulnIdFromSignature, true);
            if (null != oResult)
                return (UInt32) oResult;
            else
            {
                if (bVerbose)
                    DI.log.debug(
                        "Could not find Vuln_id for signature: {0}  (this must have been previously removed)",
                        sSignature);
                return 0;
            }
        }

        public static Int64 action_getNumberOfActionObjectsThatMatchesVulnId(UInt32 uVulnId)
        {
            String sSqlGetNumberOfActionObjectsThatMatchSignature =
                String.Format("Select Count(vuln_id) from ActionObjects where vuln_id = '{0}'", uVulnId);
            return (Int64) OunceMySql.executeSqlQuery(sSqlGetNumberOfActionObjectsThatMatchSignature, true);
        }

        public static void action_deleteRulesFromDatabase(IEnumerable<IO2Rule> rulesToDelete)
        {
            foreach (IO2Rule o2RuleToDelete in rulesToDelete)
                action_deleteRuleFromDatabase(o2RuleToDelete);
        }

        public static void action_deleteRuleFromDatabase(IO2Rule ruleToDelete)
        {
            var vulnIDSql = String.Format("Select vuln_id from rec where db_id ={0} and signature ='{1}'",
                                     ruleToDelete.DbId, ruleToDelete.Signature);
            var vulnId = OunceMySql.executeSqlQuery(vulnIDSql, true);
            if (vulnId == null)
                DI.log.error("in action_deleteRuleFromDatabase, could not find entry for signature {0} in database {1}",
                    ruleToDelete.Signature, ruleToDelete.DbId);
            else          
                if (vulnId is uint)
                    action_deleteSignatureAndActionObject((uint)vulnId);
        }
    }
}
