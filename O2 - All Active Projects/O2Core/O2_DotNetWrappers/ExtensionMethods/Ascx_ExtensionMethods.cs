using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Kernel.ExtensionMethods;

namespace O2.DotNetWrappers.ExtensionMethods
{
    public static class Ascx_ExtensionMethods
    {       
        #region Anchor

        public static Control anchor(this Control control)
        {
            control.Anchor = AnchorStyles.None;
            return control;
        }

        public static Control top(this Control control)
        {
            control.Anchor = control.Anchor | AnchorStyles.Top;
            return control;
        }

        public static Control bottom(this Control control)
        {
            control.Anchor = control.Anchor | AnchorStyles.Bottom;
            return control;
        }

        public static Control left(this Control control)
        {
            control.Anchor = control.Anchor | AnchorStyles.Left;
            return control;
        }

        public static Control right(this Control control)
        {
            control.Anchor = control.Anchor | AnchorStyles.Right;
            return control;
        }

        public static Control anchor_TopLeft(this Control control)
        {
            control.anchor().top().left();
            return control;
        }

        public static Control anchor_BottomLeft(this Control control)
        {
            control.anchor().bottom().left();
            return control;
        }

        public static Control anchor_TopRight(this Control control)
        {
            control.anchor().top().right();
            return control;
        }

        public static Control anchor_BottomRight(this Control control)
        {
            control.anchor().bottom().right();
            return control;
        }

        public static Control anchor_TopLeftRight(this Control control)
        {
            control.anchor().top().left().right();
            return control;
        }

        public static Control anchor_BottomLeftRight(this Control control)
        {
            control.anchor().bottom().left().right();
            return control;
        }

        public static Control anchor_All(this Control control)
        {
            control.anchor().top().right().bottom().left();
            return control;
        }

        #endregion

        #region Button

        public static Button add_Button(this Control control, string text)
        {
            return control.add_Button(text, -1);
        }

        public static Button add_Button(this Control control, string text, int top)
        {
            return control.add_Button(text, top, -1);
        }

        public static Button add_Button(this Control control, string text, int top, int left)
        {
            return control.add_Button(text, top, left, -1, -1);
        }

        public static Button add_Button(this Control control, string text, int top, int left, int height)
        {
            return control.add_Button(text, top, left, height, -1);
        }

        public static Button add_Button(this Control control, string text, int top, int left, int height, int width)
        {
            return control.add_Button(text, top, left, height, width, null);
        }

        public static Button add_Button(this Control control, string text, int top, int left, int height, int width, MethodInvoker onClick)
        {
            return (Button)control.invokeOnThread(
                               () =>
                                   {
                                       var button = new Button {Text = text};
                                       if (top > -1)
                                           button.Top = top;
                                       if (left > -1)
                                           button.Left = left;
                                       if (width == -1 && height == -1)
                                           button.AutoSize = true;
                                       else
                                       {
                                           if (width > -1)
                                               button.Width = width;
                                           if (height > -1)
                                               button.Height = height;
                                       }
                                       button.onClick(onClick);
                                       /*if (onClick != null)
                                    button.Click += (sender, e) => onClick();*/
                                       control.Controls.Add(button);
                                       return button;
                                   });
        }

        public static Button onClick(this Button button, MethodInvoker onClick)
        {
            if (onClick != null)
                button.Click += (sender, e) => onClick();
            return button;
        }

        #endregion

        #region Label

        public static Label add_Label(this Control control, string text, int top)
        {
            return control.add_Label(text, top, -1);
        }

        public static Label add_Label(this Control control, string text, int top, int left)
        {
            Label label = control.add_Label(text);
            if (top > -1)
                label.Top = top;
            if (left > -1)
                label.Left = left;
            return label;
        }

        public static Label add_Label(this Control control, string labelText)
        {
            return (Label) control.invokeOnThread(
                               () =>
                                   {
                                       var label = new Label {Text = labelText};
                                       control.Controls.Add(label);
                                       return label;
                                   });
        }

        #endregion

        #region LinkLabel

        public static LinkLabel add_Link(this Control control, string text, int top, int left, MethodInvoker onClick)
        {
            return (LinkLabel)control.invokeOnThread(
                                  () =>
                                      {
                                          var link = new LinkLabel
                                                         {
                                                             AutoSize = true,
                                                             Text = text,
                                                             Top = top,
                                                             Left = left
                                                         };
                                          link.LinkClicked += (sender, e) => { if (onClick != null) onClick(); };
                                          control.Controls.Add(link);
                                          return link;
                                      });

        }


        public static LinkLabel append_Link(this Control control, string text, MethodInvoker onClick)
        {
            return control.Parent.add_Link(text, control.Top, control.Left + control.Width + 5, onClick);
        }

        public static void click(this LinkLabel linkLabel)
        {
            var e = new LinkLabelLinkClickedEventArgs((LinkLabel.Link)(linkLabel.prop("FocusLink")));
            linkLabel.invoke("OnLinkClicked", e);
        }

        #endregion

        #region SplitContainer

        public static SplitContainer add_SplitContainer(this Control control)
        {
            return add_SplitContainer(control, false, false, false);
        }

        public static SplitContainer add_SplitContainer(this Control control, bool setOrientationToHorizontal,
                                                        bool setDockStyleoFill, bool setBorderStyleTo3D)
        {
            return add_SplitContainer(
                control,
                (setOrientationToHorizontal) ? Orientation.Horizontal : Orientation.Vertical,
                setDockStyleoFill,
                setBorderStyleTo3D);
        }
       
        public static SplitContainer add_SplitContainer(this Control control, Orientation orientation,
                                                        bool setDockStyleToFill, bool setBorderStyleTo3D)
        {
            return (SplitContainer) control.invokeOnThread(
                                        () =>
                                            {
                                                var splitContainer = new SplitContainer {Orientation = orientation};
                                                if (setDockStyleToFill)
                                                    splitContainer.Dock = DockStyle.Fill;
                                                if (setBorderStyleTo3D)
                                                    splitContainer.BorderStyle = BorderStyle.Fixed3D;
                                                control.Controls.Add(splitContainer);
                                                return splitContainer;
                                            });
        }

        #endregion

        #region SplitContainer_nxn

        public static List<Control> add_SplitContainer_1x1(this Control control, string title_1, string title_2,
                                                           bool verticalSplit, int spliterDistance)
        {
            SplitContainer splitControl_1 = control.add_SplitContainer(
                verticalSplit, //setOrientationToHorizontal
                true, // setDockStyleoFill
                true); // setBorderStyleTo3D
            splitControl_1.SplitterDistance = spliterDistance;
            GroupBox groupBox_1 = splitControl_1.Panel1.add_GroupBox(title_1);
            GroupBox groupBox_2 = splitControl_1.Panel2.add_GroupBox(title_2);
            return new List<Control> {groupBox_1, groupBox_2};
        }

        public static Control add_SplitContainer_1x1(this Control control, Control childControl_1, string title_2,
                                                     bool verticalSplit, int spliterDistance)
        {
            SplitContainer splitControl_1 = control.add_SplitContainer(
                verticalSplit, //setOrientationToHorizontal
                true, // setDockStyleoFill
                true); // setBorderStyleTo3D
            splitControl_1.SplitterDistance = spliterDistance;
            splitControl_1.Panel1.Controls.Add(childControl_1);
            GroupBox groupBox_2 = splitControl_1.Panel2.add_GroupBox(title_2);
            return groupBox_2;
        }

        public static Control add_SplitContainer_1x1(this Control control, string title_1, Control childControl_2,
                                                     bool verticalSplit, int spliterDistance)
        {
            SplitContainer splitControl_1 = control.add_SplitContainer(
                verticalSplit, //setOrientationToHorizontal
                true, // setDockStyleoFill
                true); // setBorderStyleTo3D
            splitControl_1.SplitterDistance = spliterDistance;
            GroupBox groupBox_1 = splitControl_1.Panel1.add_GroupBox(title_1);
            splitControl_1.Panel2.Controls.Add(childControl_2);
            return groupBox_1;
        }

        public static Control add_SplitContainer_1x1(this Control control, Control childControl_1,
                                                     Control childControl_2, bool verticalSplit, int spliterDistance)
        {
            SplitContainer splitControl_1 = control.add_SplitContainer(
                verticalSplit, //setOrientationToHorizontal
                true, // setDockStyleoFill
                true); // setBorderStyleTo3D
            splitControl_1.SplitterDistance = spliterDistance;
            splitControl_1.Panel1.Controls.Add(childControl_1);
            splitControl_1.Panel2.Controls.Add(childControl_2);
            return splitControl_1;
        }

        public static List<Control> add_SplitContainer_1x2(this Control control, string title_1, string title_2,
                                                           string title_3, bool verticalSplit, int spliterDistance_1,
                                                           int spliterDistance_2)
        {
            var tempPanel = new Panel();
            SplitContainer splitControl_2 = tempPanel.add_SplitContainer(
                !verticalSplit, //setOrientationToHorizontal
                true, // setDockStyleoFill
                true); // setBorderStyleTo3D			
            splitControl_2.SplitterDistance = spliterDistance_2;
            GroupBox groupBox_2 = splitControl_2.Panel1.add_GroupBox(title_2);
            GroupBox groupBox_3 = splitControl_2.Panel2.add_GroupBox(title_3);

            Control groupBox_1 = control.add_SplitContainer_1x1(title_1, splitControl_2, verticalSplit,
                                                                spliterDistance_1);

            var controls = new List<Control> {groupBox_1, groupBox_2, groupBox_3};
            return controls;
        }

        public static List<Control> add_SplitContainer_2x2(this Control control, string title_1, string title_2,
                                                           string title_3, string title_4, bool verticalSplit,
                                                           int spliterDistance_1, int spliterDistance_2,
                                                           int spliterDistance_3)
        {
            var tempPanel = new Panel();
            SplitContainer splitControl_2 = tempPanel.add_SplitContainer(
                !verticalSplit, //setOrientationToHorizontal
                true, // setDockStyleoFill
                true); // setBorderStyleTo3D			

            SplitContainer splitControl_3 = tempPanel.add_SplitContainer(
                !verticalSplit, //setOrientationToHorizontal
                true, // setDockStyleoFill
                true); // setBorderStyleTo3D			

            splitControl_2.SplitterDistance = spliterDistance_2;
            splitControl_3.SplitterDistance = spliterDistance_3;

            GroupBox groupBox_1 = splitControl_2.Panel1.add_GroupBox(title_1);
            GroupBox groupBox_2 = splitControl_2.Panel2.add_GroupBox(title_2);
            GroupBox groupBox_3 = splitControl_3.Panel1.add_GroupBox(title_3);
            GroupBox groupBox_4 = splitControl_3.Panel2.add_GroupBox(title_4);


            control.add_SplitContainer_1x1(splitControl_2, splitControl_3, verticalSplit, spliterDistance_1);

            var controls = new List<Control> {groupBox_1, groupBox_2, groupBox_3, groupBox_4};
            return controls;
        }

        #endregion

        #region GroupBox

        public static GroupBox add_GroupBox(this Control control, string groupBoxText)
        {
            return (GroupBox) control.invokeOnThread(
                                  () =>
                                      {
                                          var groupBox = new GroupBox {Text = groupBoxText, Dock = DockStyle.Fill};
                                          control.Controls.Add(groupBox);
                                          return groupBox;
                                      });
        }

        #endregion

        #region TabControl

        public static TabControl add_TabControl(this Control control)
        {
            return (TabControl) control.invokeOnThread(
                                    () =>
                                        {
                                            var tabControl = new TabControl {Dock = DockStyle.Fill};
                                            control.Controls.Add(tabControl);
                                            return tabControl;
                                        });
        }

        public static TabPage add_Tab(this TabControl tabControl, string tabTitle)
        {
            return (TabPage) tabControl.invokeOnThread(
                                 () =>
                                     {
                                         var tabPage = new TabPage {Text = tabTitle};
                                         tabControl.TabPages.Add(tabPage);
                                         return tabPage;
                                     });
        }

        #endregion

        #region TextBox

        public static TextBox add_TextArea(this Control control)
        {
            return control.add_TextBox(true);
        }

        public static TextBox add_TextBox(this Control control)
        {
            return control.add_TextBox(false);
        }

        public static TextBox add_TextBox(this Control control, bool multiLine)
        {
            return control.add_TextBox(-1, -1, multiLine);
        }
        public static TextBox add_TextBox(this Control control,int top, int left, bool multiLine)
        {
            return (TextBox) control.invokeOnThread(
                                 () =>
                                     {
                                         var textBox = new TextBox();
                                         if (multiLine)
                                         {
                                             textBox.Dock = DockStyle.Fill;
                                             textBox.Multiline = true;
                                             textBox.ScrollBars = ScrollBars.Both;
                                         }
                                         if (top > 0)
                                             textBox.Top = top;
                                         if (left > 0)
                                             textBox.Left = left;
                                         control.Controls.Add(textBox);
                                         return textBox;
                                     });
        }

        public static void select(this TextBox textBox, int start, int length)
        {
            textBox.invokeOnThread(() => textBox.Select(start, length));
        }

        public static void append_Line(this TextBox textBox, string textFormat, params object[] parameters)
        {
            textBox.append_Line(string.Format(textFormat, parameters));
        }

        public static void append_Line(this TextBox textBox, string text)
        {
            textBox.append_Text(text + Environment.NewLine);
        }

        public static void append_Text(this TextBox textBox, string text)
        {
            textBox.invokeOnThread(
                () =>
                    {
                        textBox.Text += text;
                        textBox.goToEnd();
                    });
        }

        public static void goToEnd(this TextBox textBox)
        {
            textBox.invokeOnThread(() =>
                                       {
                                           textBox.Select(textBox.Text.Length, 0);
                                           textBox.ScrollToCaret();
                                       });
        }

        #endregion

        #region TreeView

        public static TreeView add_TreeView(this Control control)
        {
            return (TreeView) control.invokeOnThread(
                                  () =>
                                      {
                                          var treeView = new TreeView {Dock = DockStyle.Fill};
                                          control.Controls.Add(treeView);
                                          return treeView;
                                      });
        }

        public static TreeNode add_Node(this TreeView treeView, TreeNode rootNode, string nodeText, Color textColor)
        {
            TreeNode newNode = treeView.add_Node(rootNode, nodeText); //, nodeText,0,textColor,null);
            newNode.ForeColor = textColor;
            return newNode;
        }

        public static TreeNode add_Node(this TreeView treeView, string nodeText, int imageId, Color color,
                                        object nodeTag)
        {
            return (TreeNode) treeView.invokeOnThread((()
                                                       =>
                                                           {
                                                               TreeNode treeNode = treeView.add_Node(nodeText, nodeTag);
                                                               treeNode.ForeColor = color;
                                                               treeNode.ImageIndex = imageId;
                                                               treeNode.SelectedImageIndex = imageId;
                                                               return treeNode;
                                                           }));
        }

        public static TreeNode add_Node(this TreeView treeView, string nodeText, object nodeTag)
        {
            return (TreeNode) treeView.invokeOnThread((()
                                                       =>
                                                           {
                                                               var treeNode = new TreeNode
                                                                                  {
                                                                                      Name = nodeText,
                                                                                      Text = nodeText,
                                                                                      Tag = nodeTag
                                                                                  };
                                                               treeView.Nodes.Add(treeNode);
                                                               return treeNode;
                                                           }));
        }

        public static int add_Node(this TreeView treeView, TreeNode treeNode)
        {
            return (int) treeView.invokeOnThread((()=> treeView.Nodes.Add(treeNode)));
        }

        public static TreeNode add_Node(this TreeView treeView, string nodeText)
        {
            return (TreeNode) treeView.invokeOnThread((()
                                                       => treeView.Nodes.Add(nodeText)));
        }

        public static TreeNode add_Node(this TreeView treeView, TreeNode treeNode, string nodeText)
        {
            return (TreeNode) treeView.invokeOnThread((()
                                                       => O2Forms.newTreeNode(treeNode.Nodes, nodeText, 0, null, false)));
        }

        public static TreeNode add_Node(this TreeView treeView, TreeNode treeNode, string nodeText, object nodeTag,
                                        bool addDummyNode)
        {
            return (TreeNode) treeView.invokeOnThread((()
                                                       =>
                                                           {
                                                               TreeNode newNode = O2Forms.newTreeNode(treeNode.Nodes,
                                                                                                      nodeText, 0,
                                                                                                      nodeTag);
                                                               if (addDummyNode)
                                                                   newNode.Nodes.Add("DummyNode_1");
                                                               return newNode;
                                                           }));
        }

        public static TreeNode add_Node(this TreeView treeView, string nodeText, object nodeTag, bool addDummyNode)
        {
            return (TreeNode) treeView.invokeOnThread((()
                                                       =>
                                                           {
                                                               TreeNode treeNode = treeView.add_Node(nodeText, nodeTag);
                                                               if (addDummyNode)
                                                                   treeNode.Nodes.Add("DummyNode_2");
                                                               return treeNode;
                                                           }));
        }

        public static TreeNode add_Node(this TreeView treeView, object tag)
        {
            return treeView.add_Node(tag.ToString(), tag);
        }

        public static void selectNode(this TreeView treeView, int nodeToSelect)
        {
            treeView.invokeOnThread(
                () =>
                    {
                        if (treeView.Nodes.Count > 0)
                            treeView.SelectedNode = treeView.Nodes[0];
                    });
        }

        public static void clear(this TreeView treeView, TreeNode treeNode)
        {
            treeView.invokeOnThread(() => treeNode.Nodes.Clear());
        }

        public static void clear(this TreeView treeView)
        {
            treeView.invokeOnThread(()
                                    =>
                                        {
                                            treeView.Nodes.Clear();
                                            return; // makes this Sync call
                                        });
        }

        public static void expandAll(this TreeView treeView)
        {
            treeView.invokeOnThread(() => treeView.ExpandAll());
        }

        public static void setTextColor(this TreeView treeView, TreeNode treeNode, Color color)
        {
            treeView.invokeOnThread(()
                                    => { treeNode.ForeColor = color; });
        }


        public static void afterSelect<T>(this TreeView treeView, Action<T> callback)
        {
            treeView.AfterSelect += (sender, e)
                                    =>
                                        {
                                            if (e.Node.Tag is T)

                                                callback((T)e.Node.Tag);
                                        };
        }
        #endregion

        #region RichTextBox

        public static RichTextBox add_RichTextBox(this Control control)
        {
            return control.add_RichTextBox("");
        }

        public static RichTextBox add_RichTextBox(this Control control, string text)
        {
            return (RichTextBox) control.invokeOnThread(
                                     () =>
                                         {
                                             var richTextBox = new RichTextBox {Dock = DockStyle.Fill, Text = text};
                                             control.Controls.Add(richTextBox);
                                             return richTextBox;
                                         });
        }

        public static void set_Text(this RichTextBox richTextBox, string contents)
        {
            richTextBox.invokeOnThread(() => richTextBox.Text = contents);
        }

        public static void append_Line(this RichTextBox richTextBox, string contents)
        {
            richTextBox.invokeOnThread(() => richTextBox.append_Text(Environment.NewLine + contents));
        }

        public static void append_Text(this RichTextBox richTextBox, string contents)
        {
            richTextBox.invokeOnThread(() => richTextBox.AppendText(contents));
        }

        public static RichTextBox textColor(this RichTextBox richTextBox, Color color)
        {
            richTextBox.ForeColor = color;
            return richTextBox;
        }

        #endregion

        #region CheckBox

        public static CheckBox add_CheckBox(this Control control, string text, int top, int left, Action<bool> onChecked)
        {
            return (CheckBox) control.invokeOnThread(
                                  () =>
                                      {
                                          var checkBox = new CheckBox {Text = text};
                                          checkBox.CheckedChanged += (sender, e) => onChecked(checkBox.Checked);
                                          if (top > -1)
                                              checkBox.Top = top;
                                          if (left > -1)
                                              checkBox.Left = left;
                                          control.Controls.Add(checkBox);
                                          return checkBox;
                                      });
        }

        #endregion

        #region ContextMenuStrip
        public static ContextMenuStrip add_ContextMenu(this Control control)
        {
            var contextMenu = new ContextMenuStrip();
            control.ContextMenuStrip = contextMenu;
            return contextMenu;
        }

        public static ToolStripMenuItem add(this ContextMenuStrip contextMenu, string text, Action<ToolStripMenuItem> onClick)
        {
            var menuItem = new ToolStripMenuItem {Text = text};
            contextMenu.Items.Add(menuItem);
            contextMenu.Click += (sender, e) => onClick(menuItem);
            return menuItem;
        }
        #endregion

        #region PictureBox

        public static PictureBox add_PictureBox(this Control control)
        {
            return control.add_PictureBox(-1, -1);
        }

        public static PictureBox add_PictureBox(this Control control, int top, int left)
        {
            return (PictureBox)control.invokeOnThread(
                                   () =>
                                       {
                                           var pictureBox = new PictureBox
                                                                {
                                                                    BackgroundImageLayout = ImageLayout.Stretch
                                                                };
                                           if (top == -1 && left == -1)
                                               pictureBox.fill();
                                           else
                                           {
                                               if (top > -1)
                                                   pictureBox.Top = top;
                                               if (left > -1)
                                                   pictureBox.Left = left;
                                           }
                                           control.Controls.Add(pictureBox);
                                           return pictureBox;
                                       });
        }

        public static void load(this PictureBox pictureBox, Image image)
        {
            pictureBox.BackgroundImage = image;
        }
        #endregion

        #region ProgressBar
        // order of params is: int top, int left, int width, int height)
        public static ProgressBar add_ProgressBar(this Control control, params int[] position)
        {
            return control.add_Control<ProgressBar>(position);
        }

        #endregion

        #region DataGridView

        public static DataGridView add_DataGridView(this Control control, params int[] position)
        {
            var dataGridView = control.add_Control<DataGridView>(position);
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            return dataGridView;
        }

        public static void columnWidth(this DataGridView dataGridView, int id, int width)
        {
            if (width > -1)
                dataGridView.Columns[id].Width = width;
            else
                dataGridView.Columns[id].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        public static int add_Column(this DataGridView dataGridView, string title)
        {
            return dataGridView.add_Column(title, -1);
        }

        public static int add_Column(this DataGridView dataGridView, string title, int width)
        {
            int id = dataGridView.Columns.Add(title, title);
            dataGridView.columnWidth(id, width);
            return id;
        }

        public static int add_Column_Link(this DataGridView dataGridView, string title)
        {
            return dataGridView.add_Column_Link(title, -1, false);
        }

        public static int add_Column_Link(this DataGridView dataGridView, string title, bool useColumnTextForLinkValue)
        {
            return dataGridView.add_Column_Link(title, -1, useColumnTextForLinkValue);
        }

        public static int add_Column_Link(this DataGridView dataGridView, string title, int width, bool useColumnTextForLinkValue)
        {
            var links = new DataGridViewLinkColumn
                            {
                                HeaderText = title,
                                DataPropertyName = title,
                                ActiveLinkColor = Color.White,
                                LinkBehavior = LinkBehavior.SystemDefault,
                                LinkColor = Color.Blue,
                                TrackVisitedState = true
                            };
            if (useColumnTextForLinkValue)
            {
                links.UseColumnTextForLinkValue = true;
                links.Text = title;
            }
            //links.VisitedLinkColor = Color.Blue;	
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            var id = dataGridView.Columns.Add(links);
            dataGridView.columnWidth(id, width);
            return id;
        }

        public static void add_Columns(this DataGridView dataGridView, Type type)
        {
            type.properties().ForEach(property => dataGridView.add_Column(property.Name));
        }

        public static int add_Row(this DataGridView dataGridView, params object[] cells)
        {
            int id = dataGridView.Rows.Add(cells);
            //DataGridViewRow dgvr = dgvDataGridView.Rows[id];
            //dgvr.Tag = oTagObject;
            return id;
        }

        public static void add_Rows<T>(this DataGridView dataGridView, List<T> collection)
        {
            collection.ForEach(
                item =>
                    {
                        var values = new List<object>();
                        foreach (var property in item.type().properties())
                            values.Add(item.prop(property.Name));
                        dataGridView.add_Row(values.ToArray());
                    });
        }


        public static object value(this DataGridView dataGridView, int row, int column)
        {
            var data = dataGridView.Rows[row].Cells[column].Value;
            if (data != null)
                return data;
            return "";			// default to returning "" if data is null
        }

        public static void onClick(this DataGridView dataGridView, Action<int, int> cellClicked)
        {
            dataGridView.CellContentClick += (sender, e) => cellClicked(e.RowIndex, e.ColumnIndex);
        }

        public static void remove_Rows(this DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
        }

        public static void remove_Columns(this DataGridView dataGridView)
        {
            dataGridView.Columns.Clear();
        }

        public static List<List<object>> rows(this DataGridView dataGridView)
        {
            var rows = new List<List<object>>();
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                var rowData = new List<object>();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    rowData.Add(cell.Value ?? "");
                }
                rows.Add(rowData);
            }
            return rows;
        }

        #endregion		

    }
}