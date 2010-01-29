using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using System.Drawing;

namespace O2.DotNetWrappers.Windows
{
    public static class Ascx_ExtensionMethods
    {
        public static Label add_Label(this Control control, string labelText)
        {
            return (Label) control.invokeOnThread(
                               () =>
                                   {
                                       var label = new Label();
                                       label.Text = labelText;
                                       control.Controls.Add(label);
                                       return label;
                                   });
        }

        public static SplitContainer add_SplitContainer(this Control control)
        {
            return add_SplitContainer(control, false, false, false);
        }

        public static SplitContainer add_SplitContainer(this Control control, bool setOrientationToHorizontal, bool setDockStyleoFill, bool setBorderStyleTo3D)
        {
            return add_SplitContainer(
                        control,
                        (setOrientationToHorizontal) ? Orientation.Horizontal : Orientation.Vertical,
                        setDockStyleoFill,
                        setBorderStyleTo3D);
        }
        
        //public static SplitContainer addSplitContainer(this UserControl userControl, Orientation orientation, bool setDockStyleToFill, bool setBorderStyleTo3D)
        public static SplitContainer add_SplitContainer(this Control control, Orientation orientation, bool setDockStyleToFill, bool setBorderStyleTo3D)
        {
            return (SplitContainer) control.invokeOnThread(
                                        () =>
                                            {
                                                var splitContainer = new SplitContainer();
                                                splitContainer.Orientation = orientation;
                                                if (setDockStyleToFill)
                                                    splitContainer.Dock = DockStyle.Fill;
                                                if (setBorderStyleTo3D)
                                                    splitContainer.BorderStyle = BorderStyle.Fixed3D;
                                                control.Controls.Add(splitContainer);
                                                return splitContainer;
                                            });
        }

        public static GroupBox add_GroupBox(this Control control, string groupBoxText)
        {
            return (GroupBox) control.invokeOnThread(
                                  () =>
                                      {
                                          var groupBox = new GroupBox();
                                          groupBox.Text = groupBoxText;
                                          groupBox.Dock = DockStyle.Fill;
                                          control.Controls.Add(groupBox);
                                          return groupBox;
                                      });
        }


        public static TabControl add_TabControl(this Control control)
        {
            return (TabControl)control.invokeOnThread(
               () =>
               {
                   var tabControl = new TabControl();
                   tabControl.Dock = DockStyle.Fill;
                   control.Controls.Add(tabControl);
                   return tabControl;
               });
        }

        public static TabPage add_Tab(this TabControl tabControl, string tabTitle)
        {
            return (TabPage)tabControl.invokeOnThread(
               () =>
               {
                   var tabPage = new TabPage();
                   tabPage.Text = tabTitle;
                   tabControl.TabPages.Add(tabPage);
                   return tabPage;
               });
        }


        public static TextBox add_TextBox(this Control control)
        {
            return (TextBox)control.invokeOnThread(
               () =>
               {
                   var textBox = new TextBox
                                     {
                                         Dock = DockStyle.Fill, 
                                         Multiline = true, 
                                         ScrollBars = ScrollBars.Both
                                     };
                   control.Controls.Add(textBox);
                   return textBox;
               });
        }

        public static TreeView add_TreeView(this Control control)
        {
            return (TreeView) control.invokeOnThread(
                                  () =>
                                      {
                                          var treeView = new TreeView();
                                          treeView.Dock = DockStyle.Fill;
                                          control.Controls.Add(treeView);
                                          return treeView;
                                      });
        }

        public static TreeNode add_Node(this TreeView treeView, TreeNode rootNode, string nodeText, Color textColor)
        {            
            var newNode = treeView.add_Node(rootNode, nodeText);//, nodeText,0,textColor,null);
            newNode.ForeColor = textColor;
            return newNode;
        }

        public static TreeNode add_Node(this TreeView treeView, string nodeText, int imageId, Color color, object nodeTag)
        {
            return (TreeNode)treeView.invokeOnThread((()
                => {
			            var treeNode = treeView.add_Node(nodeText, nodeTag);
			            treeNode.ForeColor = color;
			            treeNode.ImageIndex = imageId;
			            treeNode.SelectedImageIndex = imageId;
			            return treeNode;
			      }));
        }

        public static TreeNode add_Node(this TreeView treeView, string nodeText, object nodeTag)
        {
            return (TreeNode)treeView.invokeOnThread((()
                =>  {
		                var treeNode = new TreeNode();
		                treeNode.Name = nodeText;
		                treeNode.Text = nodeText;
		                treeNode.Tag = nodeTag;
		                treeView.Nodes.Add(treeNode);
		                return treeNode;
		            }));
        }

        public static int add_Node(this TreeView treeView, TreeNode treeNode)
        {
            return (int)treeView.invokeOnThread((()
                =>  {
		                return treeView.Nodes.Add(treeNode);
		            }));
        }

        public static TreeNode add_Node(this TreeView treeView, string nodeText)
        {
            return (TreeNode)treeView.invokeOnThread((()
                => treeView.Nodes.Add(nodeText)));
        }

        public static TreeNode add_Node(this TreeView treeView, TreeNode treeNode, string nodeText)
        {
            return (TreeNode)treeView.invokeOnThread((()
                => O2Forms.newTreeNode(treeNode.Nodes, nodeText, 0, null, false)));
        }
        public static TreeNode add_Node(this TreeView treeView, TreeNode treeNode, string nodeText, object nodeTag, bool addDummyNode)
        {
            return (TreeNode)treeView.invokeOnThread((()
                =>
                {
                    var newNode = O2Forms.newTreeNode(treeNode.Nodes, nodeText, 0, nodeTag);
                    if (addDummyNode)
                        newNode.Nodes.Add("DummyNode_1");
                    return newNode;
                }));
        }

        public static TreeNode add_Node(this TreeView treeView, string nodeText, object nodeTag, bool addDummyNode)
        {
            return (TreeNode)treeView.invokeOnThread((()
                =>
                {
                    var treeNode = treeView.add_Node(nodeText, nodeTag);
                    if (addDummyNode)
                        treeNode.Nodes.Add("DummyNode_2");
                    return treeNode;
                }));
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
                =>
                {
                    treeNode.ForeColor = color;
                });
        }        

    }
}
