using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX.DataViewers;

namespace O2.Views.ASCX.ExtensionMethods
{
    public static class DataViewers_ExtensionMethods
    {
        public static ascx_TableList clearTable(this ascx_TableList tableList)
        {
            var listViewControl = tableList.getListViewControl();
            listViewControl.invokeOnThread(() => listViewControl.Clear());

            return tableList;
        }


        public static ascx_TableList add_Column(this ascx_TableList tableList, string columnName)
        {
            var listViewControl = tableList.getListViewControl();
            listViewControl.invokeOnThread(() => listViewControl.Columns.Add(columnName));

            return tableList;
        }


        public static ascx_TableList add_Columns(this ascx_TableList tableList, params string[] columnsName)
        {
            tableList.add_Columns(columnsName.toList());
            return tableList;
        }

        public static ascx_TableList add_Row(this ascx_TableList tableList, params string[] cellValues)
        {
            tableList.add_Row(cellValues.toList());
            return tableList;
        }
    }
}
