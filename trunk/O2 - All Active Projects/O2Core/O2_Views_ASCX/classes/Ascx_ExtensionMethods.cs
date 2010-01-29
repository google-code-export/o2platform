using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.Views.ASCX.DataViewers;

namespace O2.Views.ASCX.classes
{
    public static class Ascx_ExtensionMethods
    {
        public static ascx_TableList add_TableList(this Control control)
        {
            return control.add_TableList("");
        }
        public static ascx_TableList add_TableList(this Control control, string tableTitle)
        {
            var tableList = new ascx_TableList();
            tableList._Title = tableTitle;
            tableList.Dock = DockStyle.Fill;
            control.Controls.Add(tableList);
            return tableList;
        }

        public static void add_Columns(this ascx_TableList tableList, List<string> columnNames)
        {
            ListView listView = tableList.getListViewControl();
            listView.Columns.Clear();
            listView.AllowColumnReorder = true;
            foreach (var columnName in columnNames)
                listView.Columns.Add(columnName);

        }

        public static void add_Row(this ascx_TableList tableList, List<string> rowData)
        {
            if (rowData.Count > 0)
            {
                var listView = tableList.getListViewControl();
                var listViewItem = new ListViewItem();
                listViewItem.Text = rowData[0]; // hack because SubItems starts adding on the 2nd Column :(
                rowData.RemoveAt(0);
                listViewItem.SubItems.AddRange(rowData.ToArray());
                listView.Items.Add(listViewItem);
            }
        }
    }
}
