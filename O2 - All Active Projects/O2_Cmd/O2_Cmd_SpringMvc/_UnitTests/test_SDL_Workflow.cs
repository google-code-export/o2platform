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
