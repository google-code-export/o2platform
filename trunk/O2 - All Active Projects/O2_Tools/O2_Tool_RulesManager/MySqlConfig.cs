// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using O2.Rules.OunceLabs.DataLayer_OunceV6;

namespace O2.Tool.RulesManager
{
    public class MySqlConfig
    {
        public static void setMySqlConnectionDetailsFromAppConfig()
        {
            OunceMySql.MySqlDatabaseName = ConfigurationManager.AppSettings["MySqlDatabaseName"];
            OunceMySql.MySqlLoginPassword = ConfigurationManager.AppSettings["MySqlLoginPassword"];
            OunceMySql.MySqlLoginUsername = ConfigurationManager.AppSettings["MySqlLoginUsername"];
            OunceMySql.MySqlServerIP = ConfigurationManager.AppSettings["MySqlServerIP"];
            OunceMySql.MySqlServerPort = ConfigurationManager.AppSettings["MySqlServerPort"];
        }
    }
}
