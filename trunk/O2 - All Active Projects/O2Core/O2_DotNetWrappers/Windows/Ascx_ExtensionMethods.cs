using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using System.Drawing;

namespace O2.DotNetWrappers.Windows
{
    public static class Ascx_ExtensionMethods
    {
        public static Label addLabel(this Control control, string labelText)
        {
            var label = new Label();
            label.Text = labelText; ;
            control.Controls.Add(label);
            return label;
        }

        public static SplitContainer addSplitContainer(this Control control)
        {
            return addSplitContainer(control, false, false, false);
        }

        public static SplitContainer addSplitContainer(this Control control, bool setOrientationToHorizontal, bool setDockStyleoFill, bool setBorderStyleTo3D)
        {
            return addSplitContainer(
                        control,
                        (setOrientationToHorizontal) ? Orientation.Horizontal : Orientation.Vertical,
                        setDockStyleoFill,
                        setBorderStyleTo3D);
        }
        
        //public static SplitContainer addSplitContainer(this UserControl userControl, Orientation orientation, bool setDockStyleToFill, bool setBorderStyleTo3D)
        public static SplitContainer addSplitContainer(this Control control, Orientation orientation, bool setDockStyleToFill, bool setBorderStyleTo3D)
        {
            var splitContainer = new SplitContainer();
            splitContainer.Orientation = orientation;
            if (setDockStyleToFill)
                splitContainer.Dock = DockStyle.Fill;
            if (setBorderStyleTo3D)
                splitContainer.BorderStyle = BorderStyle.Fixed3D;
            control.Controls.Add(splitContainer);
            return splitContainer;
        }

        public static GroupBox addGroupBox(this Control control, string groupBoxText)
        {
            var groupBox = new GroupBox();
            groupBox.Text = groupBoxText;
            groupBox.Dock = DockStyle.Fill;
            control.Controls.Add(groupBox);
            return groupBox;
        }

        public static TreeView addTreeView(this Control control)
        {
            var treeView = new TreeView();
            treeView.Dock = DockStyle.Fill;
            control.Controls.Add(treeView);
            return treeView;
        }

        public static TreeNode addNode(this TreeView treeView, string nodeText, int imageId, Color color, object nodeTag)
        {
            return (TreeNode)treeView.invokeOnThread((()
                => {
			            var treeNode = treeView.addNode(nodeText, nodeTag);
			            treeNode.ForeColor = color;
			            treeNode.ImageIndex = imageId;
			            treeNode.SelectedImageIndex = imageId;
			            return treeNode;
			      }));
        }

        public static TreeNode addNode(this TreeView treeView, string nodeText, object nodeTag)
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

        public static int addNode(this TreeView treeView, TreeNode treeNode)
        {
            return (int)treeView.invokeOnThread((()
                =>  {
		                return treeView.Nodes.Add(treeNode);
		            }));
        }

        public static TreeNode addNode(this TreeView treeView, string nodeText)
        {
            return (TreeNode)treeView.invokeOnThread((()
                =>
                {
                    return treeView.Nodes.Add(nodeText);
                }));
        }

        public static TreeNode addNode(this TreeView treeView, TreeNode treeNode, string nodeText)
        {
            return (TreeNode)treeView.invokeOnThread((()
                =>
                {
                    return O2Forms.newTreeNode(treeNode.Nodes, nodeText, 0, null, false); ;
                }));
        }
        public static TreeNode addNode(this TreeView treeView, TreeNode treeNode, string nodeText, object nodeTag, bool addDummyNode)
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

        public static TreeNode addNode(this TreeView treeView, string nodeText, object nodeTag, bool addDummyNode)
        {
            return (TreeNode)treeView.invokeOnThread((()
                =>
                {
                    var treeNode = treeView.addNode(nodeText, nodeTag);
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
