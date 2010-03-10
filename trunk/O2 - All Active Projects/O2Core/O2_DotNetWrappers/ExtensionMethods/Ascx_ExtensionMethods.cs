using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using O2.DotNetWrappers.Windows;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using System.Collections;

namespace O2.DotNetWrappers.ExtensionMethods
{
    public static class Ascx_ExtensionMethods
    {       
       
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

        public static Control add_Button(this Control control, string text, int top, int left, MethodInvoker onClick)
        {
            return control.add_Button(text, top, left, -1, -1, onClick);
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
                                       var label = new Label
                                                       {
                                                           Text = labelText, 
                                                           AutoSize = true
                                                       };
                                       control.Controls.Add(label);
                                       return label;
                                   });
        }

        public static Label set_Text(this Label label, string text)
        {
            return (Label)label.invokeOnThread(
                                    () =>
                                    {
                                        label.Text = text;
                                        return label;
                                    });
        }

        public static Label textColor(this Label label, Color color)
        {
            return (Label)label.invokeOnThread(
                () =>
                {
                    label.ForeColor = color;
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


        public static SplitContainer border3D(this SplitContainer control)
        {
            return (SplitContainer)control.invokeOnThread(
                () =>
                    {
                        control.BorderStyle = BorderStyle.Fixed3D;
                        return control;
                    });
        }

        public static void panel2Collapsed(this SplitContainer control, bool value)
        {
            control.invokeOnThread(() => control.Panel2Collapsed = value);
        }

        public static void panel1Collapsed(this SplitContainer control, bool value)
        {
            control.invokeOnThread(() => control.Panel2Collapsed = value);
        }

        #endregion

        #region SplitContainer_nxn

        public static List<Control> add_1x1(this Control control, string title1, string title2)
        {
            return control.add_1x1(title1, title2, true, 100);
        }

        public static List<Control> add_1x1(this Control control, string title1, string title2, bool align, int distance1)
        {
            return control.add_SplitContainer_1x1(title1, title2, align, distance1);
        }

        public static List<Control> add_1x1(this Control hostControl, string title_1, string title_2, Control control_1, Control control_2)
        {
            var groupBoxes = hostControl.add_1x1(title_1, title_2);
            groupBoxes[0].Controls.Add(control_1);
            groupBoxes[1].Controls.Add(control_2);
            groupBoxes.Add(control_1);		// also add these controls to the list to controls returned
            groupBoxes.Add(control_2);
            return groupBoxes;
        }

        public static List<Control> add_1x2(this Control control, string title1, string title2, string title3)
        {
            return control.add_1x2(title1, title2, title3, true, 100, 100);
        }

        public static List<Control> add_1x2(this Control control, string title1, string title2, string title3, bool align, int distance1, int distance2)
        {
            return control.add_SplitContainer_1x2(title1, title2, title3, align, distance1, distance2);
        }


        public static List<Control> add_SplitContainer_1x1(this Control control, string title_1, string title_2,
                                                           bool verticalSplit, int spliterDistance)
        {
            SplitContainer splitControl_1 = control.add_SplitContainer(
                verticalSplit, //setOrientationToHorizontal
                true, // setDockStyleoFill
                true); // setBorderStyleTo3D
            return (List<Control>)splitControl_1.invokeOnThread(
                () =>
                {
                    splitControl_1.SplitterDistance = spliterDistance;
                    GroupBox groupBox_1 = splitControl_1.Panel1.add_GroupBox(title_1);
                    GroupBox groupBox_2 = splitControl_1.Panel2.add_GroupBox(title_2);
                    return new List<Control> { groupBox_1, groupBox_2 };
                });
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

        public static TextBox select(this TextBox textBox, int start, int length)
        {
            textBox.invokeOnThread(() => textBox.Select(start, length));
            return textBox;
        }

        public static TextBox set_Text(this TextBox textBox, string text)
        {
            textBox.invokeOnThread(() => textBox.Text = text);
            return textBox;
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

        public static TextBox onTextChange(this TextBox textBox, Action<string> callback)
        {
            return (TextBox)textBox.invokeOnThread(
                () =>
                {
                    textBox.TextChanged += (sender, e) => callback(textBox.Text);
                    return textBox;
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
                                          //change the default dehaviour of treeviews of not selecting on mouse click (big problem when using right click) 
                                          treeView.NodeMouseClick += (sender, e) => { treeView.SelectedNode = e.Node; };
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

        public static void expand(this TreeView treeView, TreeNode treeNode)
        {
            treeView.invokeOnThread(() => treeNode.Expand());
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

        public static void beforeExpand<T>(this TreeView treeView, Action<T> callback)
        {
            treeView.BeforeExpand += (sender, e)
                                    =>
            {
                treeView.SelectedNode = e.Node;
                if (e.Node.Tag is T)

                    callback((T)e.Node.Tag);
            };
        }


        public static TreeView add_Nodes(this TreeView treeView, IEnumerable collection)
        {
            return treeView.add_Nodes(collection, false, "");
        }

        public static TreeView add_Nodes(this TreeView treeView, IEnumerable collection, bool clearTreeView)
        {
            return treeView.add_Nodes(collection, clearTreeView, "");
        }

        public static TreeView add_Nodes(this TreeView treeView, IEnumerable collection, bool clearTreeView, string filter)
        {
            return (TreeView)treeView.invokeOnThread(
                () =>
                {
                    if (clearTreeView)
                        treeView.clear();
                    foreach (var item in collection)
                        if (filter == "" || item.str().regEx(filter))
                            treeView.add_Node(item.str(), item);
                    return treeView;
                });
        }

        public static TreeView add_Nodes(this TreeView treeView, IDictionary dictionary, bool clearTreeView, string filter)
        {
            return (TreeView)treeView.invokeOnThread(
                () =>
                {
                    if (clearTreeView)
                        treeView.clear();
                    foreach (var key in dictionary.Keys)
                    {
                        var text = key.str();
                        if (filter == "" || text.regEx(filter))
                            treeView.add_Node(text, dictionary[key]);
                    }
                    return treeView;
                });
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

        public static RichTextBox set_Text(this RichTextBox richTextBox, string contents)
        {
            return (RichTextBox)richTextBox.invokeOnThread(
                () =>
                    {
                        richTextBox.Text = contents;
                        return richTextBox;
                    });
            
        }

        public static RichTextBox append_Line(this RichTextBox richTextBox, string contents)
        {
            return (RichTextBox) richTextBox.invokeOnThread(
                () =>
                    {
                        richTextBox.append_Text(Environment.NewLine + contents);
                       return richTextBox;
                    });
            
        }

        public static RichTextBox append_Text(this RichTextBox richTextBox, string contents)
        {
            return (RichTextBox)richTextBox.invokeOnThread(
                () =>
                    {
                        richTextBox.AppendText(contents);
                        return richTextBox;
                    });            
        }

        public static RichTextBox textColor(this RichTextBox richTextBox, Color color)
        {
            return (RichTextBox) richTextBox.invokeOnThread(
                                     () =>
                                         {
                                             richTextBox.ForeColor = color;
                                             return richTextBox;
                                         });
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
            menuItem.Click += (sender, e) => onClick(menuItem);
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

        public static PictureBox add_PictureBox(this Control control, string pathToImage)
        {
            var pictureBox = control.add_PictureBox();
            return pictureBox.load(pathToImage);
        }

        public static PictureBox load(this PictureBox pictureBox, string pathToImage)
        {
            if (pathToImage.fileExists())
            {
                var image = Image.FromFile(pathToImage);
                pictureBox.load(image);
                return pictureBox;
            }
            return null;
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

        public static void set(this DataGridView dataGridView, int row, int column, object value)
        {
            dataGridView.invokeOnThread(
                () =>
                {
                    dataGridView.Rows[row].Cells[column].Value = value;
                });
        }
    
        public static DataGridViewRow get_Row(this DataGridView dataGridView, int rowId)
        {
            return (DataGridViewRow)dataGridView.invokeOnThread(() => dataGridView.Rows[rowId]);
        }

        public static object value(this DataGridView dataGridView, int rowId, int columnId)
        {
            try
            {
                var data = dataGridView.Rows[rowId].Cells[columnId].Value;
                if (data != null)
                    return data;
            }
            catch (Exception ex)
            {
                PublicDI.log.ex(ex, "in DataGridView.value");
            }            
            return "";			// default to returning "" if data is null
        }

        public static void onClick(this DataGridView dataGridView, Action<int, int> cellClicked)
        {
            dataGridView.CellContentClick += (sender, e) => cellClicked(e.RowIndex, e.ColumnIndex);
        }

        public static void remove_Row(this DataGridView dataGridView, DataGridViewRow row)
        {
            dataGridView.invokeOnThread(() =>
                dataGridView.Rows.Remove(row));
        }

        public static void remove_Column(this DataGridView dataGridView, DataGridViewColumn column)
        {
            dataGridView.invokeOnThread(() =>
                dataGridView.Columns.Remove(column));
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

        public static void noSelection(this DataGridView dataGridView)
        {
            dataGridView.SelectionChanged += (sender, e) => dataGridView.ClearSelection();
        }

        public static void column_backColor(this DataGridView dataGridView, int columnId, Color color)
        {
            dataGridView.Columns[columnId].DefaultCellStyle.BackColor = color;
        }

        public static void showFileStrings(this DataGridView dataGridView, string file, bool ignoreCharZeroAfterValidChar, int minimumStringSize)
        {
            dataGridView.Columns.Clear();
            dataGridView.add_Column("string");

            foreach (var _string in file.contentsAsBytes().strings(ignoreCharZeroAfterValidChar, minimumStringSize))
                dataGridView.add_Row(_string);
        }

        public static void showFileContents(this DataGridView dataGridView, string file, Func<byte, string> encoding)
        {
            showFileContents(dataGridView, file, 16, encoding);   
        }
        public static void showFileContents(this DataGridView dataGridView, string file, int splitPoint, Func<byte, string> encoding)
        {
            dataGridView.invokeOnThread(
                () =>
                {
                    dataGridView.Columns.Clear();
                    dataGridView.add_Column("");
                    for (byte b = 0; b < splitPoint; b++)
                        dataGridView.add_Column(b.hex());
                    dataGridView.column_backColor(0, Color.LightGray);
                    if (!file.fileExists())
                    {
                        PublicDI.log.error("provided file to load doesn't exists :{0}", file);
                        return;
                    }
                    var bytes = file.contentsAsBytes();
                    var row = new List<string>();
                    var rowId = 0;
                    row.add(rowId.hex());
                    foreach (var value in bytes)
                    {
                        row.add(encoding(value));
                        if (row.Count == splitPoint)
                        {
                            dataGridView.add_Row(row.ToArray());
                            row.Clear();
                            row.add((rowId++).hex());
                        }
                    }
                    dataGridView.add_Row(row.ToArray());
                    //dataGridView.add_Row(row);
                });
        }

        #endregion		

        #region PropertyGrid

        public static PropertyGrid add_PropertyGrid(this Control control)
        {
            return control.add_Control<PropertyGrid>();
        }

        public static void show(this PropertyGrid propertyGrid, Object _object)
        {
            propertyGrid.invokeOnThread(() => propertyGrid.SelectedObject = _object);
        }

        #endregion

        #region Panel

        public static Control add_Panel(this Control control)
        {
            return control.add_Control<Panel>();
        }

        #endregion

        #region ToolStripStatus

        public static ToolStripStatusLabel add_StatusStrip(this UserControl _control)
        {
            return (ToolStripStatusLabel)_control.invokeOnThread(
                () =>
                {
                    if (_control.ParentForm == null)
                    {
                        "could not add Status Strip since there is no Parent Form for this control".error();
                        return null;
                    }
                    return _control.ParentForm.add_StatusStrip();
                });
        }

        public static ToolStripStatusLabel add_StatusStrip(this Form form)
        {
            return (ToolStripStatusLabel)form.invokeOnThread(
                () =>
                {
                    var label = new ToolStripStatusLabel();
                    var statusStrip = new StatusStrip();
                    statusStrip.Items.Add(label);
                    form.Controls.Add(statusStrip);
                    return label;
                });
        }

        public static ToolStripStatusLabel set_Text(this ToolStripStatusLabel label, string message)
        {
            //return (ToolStripStatusLabel)label.invokeOnThread(
            //	()=>{
            label.Text = message;
            return label;
            //		});
        }

        public static string get_Text(this ToolStripStatusLabel label)
        {
            return label.Text;
        }

        #endregion

    }
}