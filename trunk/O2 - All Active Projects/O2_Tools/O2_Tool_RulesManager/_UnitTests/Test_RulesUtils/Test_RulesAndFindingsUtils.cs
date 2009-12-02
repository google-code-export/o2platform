using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using O2.DotNetWrappers.DotNet;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Kernel.InterfacesBaseImpl;
using O2.Rules.OunceLabs.DataLayer_OunceV6;
using O2.Rules.OunceLabs.RulesUtils;
using System.IO;

namespace O2.Tool.RulesManager._UnitTests.Test_RulesUtils
{
    [TestFixture]
    public class Test_RulesAndFindingsUtils
    {
        public string testLanguageDBId = "-1";
        private const string testOzasmtFile1 = @"E:\O2\Demodata\Joinned_HacmeBank_WebServices.ozasmt";
        private const string testOzasmtFile2 = @"E:\O2\Demodata\wg.ozasmt";
        private const string testOzasmtFile3 = @"E:\O2\Demodata\wg60M.ozasmt";
        
        private const string rulePackToUse = @"E:\O2\Demodata\RulePacks\from_601_Sinks_(2995)_.O2RulePack";

        [Test]
        public void test_calculateRulePack()
        {
            var o2RulePackToUse = O2RulePackUtils.loadRulePack(rulePackToUse);

            calculateRulePack_forFile(testOzasmtFile1,o2RulePackToUse);                        
        }

        public void calculateRulePack_forFile(string ozamstFileToTest, O2RulePack o2RulePackToUse)
        {
            DI.log.debug("\n\ntesting file: {0}", ozamstFileToTest);
            var timer = new O2Timer("Calculate Rule Pack ").start();
            // load assesment file and get unique signatures
            var o2Findings = new O2AssessmentLoad_OunceV6().loadFile(ozamstFileToTest).o2Findings;
            var uniqueSignatures = RulesAndFindingsUtils.getListOfUniqueSignatures(o2Findings);
            Assert.That(uniqueSignatures.Count > 0, "uniqueSignatures ==0");
            // calculate rulepack for this assessment

            var o2RulePackForOzasmt = RulesAndFindingsUtils.createRulePackWithSignatures(o2RulePackToUse, uniqueSignatures,
                                                               Path.GetFileName(ozamstFileToTest), true, testLanguageDBId);
            Assert.That(o2RulePackForOzasmt.o2Rules.Count > 0, "There were no rules in created o2RulePackForOzasmt");
            timer.stop();
        }

        [Test]
        public void test_CreateUniqueListOfSignatures()
        {
        //    test_CreateUniqueListOfSignatures_forFile(testOzasmtFile4);
        //    return;
            createUniqueListOfSignatures_forFile(testOzasmtFile1);
            createUniqueListOfSignatures_forFile(testOzasmtFile2);
            createUniqueListOfSignatures_forFile(testOzasmtFile3);
        }

        public void createUniqueListOfSignatures_forFile(string ozamstFileToTest)
        {
            DI.log.debug("\n\ntesting file: {0}", ozamstFileToTest);
            var timer = new O2Timer("Unique Signatures calculated").start();                        
            var o2Findings = new O2AssessmentLoad_OunceV6().loadFile(ozamstFileToTest).o2Findings;            
            var uniqueSignatures = RulesAndFindingsUtils.getListOfUniqueSignatures(o2Findings);
            Assert.That(uniqueSignatures.Count > 0, "uniqueSignatures ==0");
            DI.log.info("Unique Signatures calculated = {0}", uniqueSignatures.Count);            
            timer.stop();
        }
    }
}
