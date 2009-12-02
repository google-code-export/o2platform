using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using O2.Kernel.Interfaces.Rules;
using O2.Kernel.InterfacesBaseImpl;
using O2.Rules.OunceLabs;
using O2.Rules.OunceLabs.DataLayer;
using O2.Rules.OunceLabs.DataLayer_OunceV6;
using O2.Rules.OunceLabs.RulesUtils;
using System.IO;

namespace O2.Tool.RulesManager._UnitTests
{
    [TestFixture]
    public class Test_MySqlRulesExporter
    {
        readonly IDatabaseRules mySqlRules_OunceV6 = new MySqlRules_OunceV6();

        [Test]
        public void test_createRulePackWithAllRulesFromLddbFor_DotNet()
        {
            
            var numberOfRules = Lddb_OunceV6.getNumberOfRulesInRecTable();
            Assert.That(numberOfRules > 0, "numberOfRules == 0");
            DI.log.info("There {0} rules", numberOfRules);

            var o2Rules = mySqlRules_OunceV6.createO2RulesForAllLddbEntriesForLanguage(SupportedLanguage.DotNet);
            var o2RulePack = new O2RulePack("MySql_Dump", o2Rules);

            Assert.That(o2Rules.Count > 0, "o2Rules.Count ==0");
            var rulePackFile = O2RulePackUtils.saveRulePack(o2RulePack);
            Assert.That(File.Exists(rulePackFile), "rulePacklFile file didn't exist: " + rulePackFile);
            DI.log.info("Rule pack (with {0} rules) saved to {1}", o2Rules.Count, rulePackFile);
        }

    [Test]
        public void test_createRulePackWithSourcesAndSinksRulesFromLddbFor_DotNet()
    {
        var ruleDbId = MiscUtils_OunceV6.getIdForSuportedLanguage(SupportedLanguage.DotNet).ToString();
            // create just sources and sinks
            var sourcesRulePack = new O2RulePack("MySql_Sources", mySqlRules_OunceV6.getRules_Sources(ruleDbId));
            var sourcesRulePackFile =  O2RulePackUtils.saveRulePack(sourcesRulePack);
            Assert.That(File.Exists(sourcesRulePackFile), "sourcesRulePackFile doesn't exist");

            var sinksRulePack = new O2RulePack("MySql_Sinks", mySqlRules_OunceV6.getRules_Sinks(ruleDbId));
            var sinksRulePackFile =  O2RulePackUtils.saveRulePack(sinksRulePack);
            Assert.That(File.Exists(sinksRulePackFile), "sinksRulePackFile doesn't exist");            

        }

        [Test]
        public void test_connectToDatabase()
        {
            Services_OunceV6.startService_MySql();
            // check that the connection is working
            Assert.That(OunceMySql.isConnectionOpen());

            // load config and recheck connectoin
            MySqlConfig.setMySqlConnectionDetailsFromAppConfig();
            Assert.That(OunceMySql.refreshDbConnection());
            Assert.That(OunceMySql.isConnectionOpen());
            // set up a bad pwd
            OunceMySql.MySqlLoginUsername = "{WRONG USERNAME";           
            Assert.That(false == OunceMySql.refreshDbConnection());

            // reload config and recheck connection
            MySqlConfig.setMySqlConnectionDetailsFromAppConfig();
            Assert.That(OunceMySql.refreshDbConnection());
        }

        [Test]
        public void test_startAndStopMySqlService()
        {
            Services_OunceV6.stopService_MySql();
            Assert.That(Services_OunceV6.isRunning_MySql() == false, "isRunning_MySql() was true");
            Services_OunceV6.startService_MySql();
            Assert.That(Services_OunceV6.isRunning_MySql(), "isRunning_MySql() was false;");
        }
    }
}
