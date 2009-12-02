// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace O2.Cmd.SpringMvc._UnitTests
{
    [TestFixture]
    public class test_SDL_Workflow
    {
        [Test]
        public void stopMySqlService()
        {
        }

        [Test]
        public void startMySqlService()
        {
        }

        [Test]
        public void restoreMySqlDatabase()
        {
            DI.log.info("Restoring Ounce MySqlDatabase");
        }
    }
}
