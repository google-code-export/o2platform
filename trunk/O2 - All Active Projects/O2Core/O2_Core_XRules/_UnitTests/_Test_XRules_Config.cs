using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using O2.Core.XRules.XRulesEngine;

namespace O2.Core.XRules._UnitTests
{
    [TestFixture]
    public class _Test_XRules_Config
    {
        [Test]
        public void IsXRulesDatabasePathConfigured()
        {
            Assert.That(null != XRules_Config.xRulesDatabase, "XRules_Config.xRulesDatabase was null");
        }

        [Test]
        public void DoesDirectoryExist_PathTo_XRulesCompiledDlls()
        {
            Assert.That(Directory.Exists(XRules_Config.PathTo_XRulesCompiledDlls));
        }

        [Test]
        public void DoesDirectoryExist_PathTo_PathTo_XRulesDatabase_fromLocalDisk()
        {
            Assert.That(Directory.Exists(XRules_Config.PathTo_XRulesDatabase_fromLocalDisk));
        }

        [Test]
        public void DoesDirectoryExist_PathTo_XRulesDatabase_fromO2()
        {
            Assert.That(Directory.Exists(XRules_Config.PathTo_XRulesDatabase_fromO2));
        }

        [Test]
        public void DoesDirectoryExist_PathTo_PathTo_XRulesTemplates()
        {
            Assert.That(Directory.Exists(XRules_Config.PathTo_XRulesTemplates));
        }        
    }
}
