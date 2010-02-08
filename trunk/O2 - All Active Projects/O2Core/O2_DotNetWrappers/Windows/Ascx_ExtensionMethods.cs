using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.Kernel;
using O2.Kernel.CodeUtils;

namespace O2.DotNetWrappers.Windows
{
    public static class Ascx_ExtensionMethods
    {
        #region Control

        public static Control add_Control(this Control control, Type childControlType)
        {
            return (Control) control.invokeOnThread(
                                 () =>
                                     {
                                         var childControl =
                                             (Control)
                                             PublicDI.reflection.createObjectUsingDefaultConstructor(childControlType);
                                         if (control != null)
                                         {
                                             childControl.Dock = DockStyle.Fill;
                                             control.Controls.Add(childControl);
                                             return childControl;
                                         }
                                         return null;
                                     });
        }

        public static void clear(this Control control)
        {
            control.invokeOnThread(
                () =>
                    {
                        control.Controls.Clear();
                        return;
                    });
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
                                                var link = new LinkLabel();
                                                link.AutoSize = true;
                                                link.Text = text;
                                                link.Top = top;
                                                link.Left = left;
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
    }
}