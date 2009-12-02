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
