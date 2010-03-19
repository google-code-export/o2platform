// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Kernel.CodeUtils;
using System.Collections;

namespace O2.Views.ASCX.DataViewers
{
    public partial class ascx_TableList : UserControl
    {
        public event O2Thread.FuncVoidT1<DragEventArgs> _onTableListDrop;

        private bool resizeColumnsWidth = true;

        public DataTable currentDataTable;

        public string defaultColumnsTitles = "";

        public ascx_TableList()
        {
            InitializeComponent();
            setUpColumnSort();
        }

        public void setDataTable(DataTable dataTable)
        {
            setDataTable(dataTable, true);
        }

        public void setDataTable(DataTable dataTable, bool clearData)
        {
            currentDataTable = dataTable;
            refreshTable(clearData);
        }

        public void refreshTable(bool clearData)
        {
            clearData = true;
            this.invokeOnThread(
                () =>
                    {
                        if (clearData)
                        {
                            lvData.Columns.Clear();
                            lvData.Items.Clear();    
                        }
                        

                        if (currentDataTable ==null)
                            return;

                        lvData.Visible = false;
                        if (clearData || currentDataTable.Columns.Count==0)
                        {
                            var columnWidth = lvData.Width/currentDataTable.Columns.Count;
                            foreach (DataColumn column in currentDataTable.Columns)
                            {
                                var columnHeader = lvData.Columns.Add(column.ColumnName);
                                columnHeader.Width = columnWidth;
                                //columnHeader.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                            }
                        }
                        foreach (DataRow row in currentDataTable.Rows)
                        {
                            var listViewItem = new ListViewItem();
                            //foreach (ColumnHeader column in lvData.Columns)
                            var items = new List<String>();
                            foreach (DataColumn column in currentDataTable.Columns)
                            {
                                items.Add(row[column.ColumnName].ToString());
                            }
                            listViewItem.Text = items[0]; // hack because SubItems starts adding on the 2nd Column :(
                            items.RemoveAt(0);
                            listViewItem.SubItems.AddRange(items.ToArray());
                            lvData.Items.Add(listViewItem);
                            /*
                
            foreach (var rowDataItem in row.ItemArray)
            {

                listViewItem.SubItems.Add(rowDataItem.ToString());
            }
            */
                        }


                        //lvData.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

                        lvData.Visible = true;
                        //O2Forms.loadDataTableIntoListViewInDetailsMode;
                        //foreach (DataColumn column in currentDataTable.Columns)
                        //lvData.DataBindings.Add("ID", currentDataTable, "ID"); // currentDataTable;
                    });
        }        

        //public void loadList(object filters)
        //{
        //    setDataTable(null);
        //}

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [Bindable(true)]
        public string _Title
        {
            get { return lbTableListTitle.Text; }
            set { lbTableListTitle.Text = value;}                    
//            this.invokeOnThread(() => lbTableListTitle.Text = title);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [Bindable(true)]
        public string _DefaultColumnsTitles
        {
            get { return defaultColumnsTitles; }
            set
            {
                defaultColumnsTitles = value;
                if (defaultColumnsTitles != "")
                {
                    lvData.Columns.Clear();
                    var titles = defaultColumnsTitles.Split(',');

                    var columnWidth = lvData.Width/titles.Length;
                    foreach (var title in titles)
                    {
                        var columnHeader = lvData.Columns.Add(title);
                        columnHeader.Width = columnWidth;
                    }
                }
            }

        }

        private void lvData_Resize(object sender, EventArgs e)
        {
            if (lvData.Columns.Count > 0 && resizeColumnsWidth)
            {
                var columnWidth = lvData.Width/lvData.Columns.Count;
                foreach (ColumnHeader columnHeader in lvData.Columns)
                    columnHeader.Width = columnWidth;
            }
        }

        private void llMakeColumnWithMatchCellWidth_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            resizeColumnsWidth = false;
            lvData.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            resizeColumnsWidth = true;
        }



        public ListView getListViewControl()
        {
            return lvData;
        }

        private void llClearLoadedItems_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lvData.Columns.Clear();
        }

        private void lvData_DragEnter(object sender, DragEventArgs e)
        {
            Dnd.setEffect(e);
        }

        private void lvData_DragDrop(object sender, DragEventArgs e)
        {
            handleDrop(e);
        }

        private void handleDrop(DragEventArgs e)
        {
            Callbacks.raiseRegistedCallbacks(_onTableListDrop, new object[] {e});
        }
        
        public void setUpColumnSort()
        {            
            lvData.ColumnClick +=
                (sender, e) =>
                {
                    if (lvData.Sorting == SortOrder.Ascending)
                        lvData.Sorting = SortOrder.Descending;
                    else
                        lvData.Sorting = SortOrder.Ascending;
                    lvData.ListViewItemSorter = new ListViewItemComparer(e.Column, lvData.Sorting);
                };
            
        }
    }

    class ListViewItemComparer : IComparer
    {
        private int column;
        private SortOrder sortOrder;
        public ListViewItemComparer(int _column, SortOrder _sortOrder)
        {
            column = _column;
            sortOrder = _sortOrder;
        }
        public int Compare(object x, object y)
        {
            if (sortOrder == SortOrder.Ascending)
                return String.Compare(((ListViewItem)x).SubItems[column].Text, ((ListViewItem)y).SubItems[column].Text);
            return String.Compare(((ListViewItem)y).SubItems[column].Text, ((ListViewItem)x).SubItems[column].Text);
        }
    }
}
