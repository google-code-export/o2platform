// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace O2.Views.ASCX.classes
{
    public class CreateDataTable
    {
        public static DataTable fromDictionary_StringString(Dictionary<string, string> dictionary, string column1Title, string column2Title)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add(column1Title);
            dataTable.Columns.Add(column2Title);
            if (dictionary != null)
                foreach (var item in dictionary)
                    dataTable.Rows.Add(new[] { item.Key, item.Value });
            return dataTable;
        }
    }
}
