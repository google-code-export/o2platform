// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using O2.DotNetWrappers.NetSDK;
using System.IO;

namespace O2.UnitTests.Test_DotNetWrappers.Test_NetSDK
{
    [TestFixture]
    public class Test_IL
    {
        [Test]
        public void Test_ILAsmDasm()
        {
            Assert.That(""!= IL.getILAsmExe() , "getILasmExe was empty");
            Assert.That("" != IL.getILDasmExe(), "getILasmExe was empty");
            var ilFile = IL.createILforAssembly(DI.config.ExecutingAssembly);
            Assert.That(File.Exists(ilFile), "ilFile was not created");
            var exeFile = IL.createExeFromIL(ilFile,true,true);
            Assert.That(File.Exists(exeFile), "exeFile   was not created");
        }
    }
}
