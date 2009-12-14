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
            var treeNode = treeView.addNode(nodeText, nodeTag);
            treeNode.ForeColor = color;
            treeNode.ImageIndex = imageId;
            treeNode.SelectedImageIndex = imageId;
            return treeNode;
        }

        public static TreeNode addNode(this TreeView treeView, string nodeText, object nodeTag)
        {
            return (TreeNode)treeView.invokeOnThread((()
                =>
            {
                var treeNode = new TreeNode();
                treeNode.Name = nodeText;
                treeNode.Text = nodeText;
                treeNode.Tag = nodeTag;
                treeView.Nodes.Add(treeNode);
                return treeNode;
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

        public static void clear(this TreeView treeView)
        {
            treeView.invokeOnThread(()
                =>
                {
                    treeView.Nodes.Clear();
                    return; // makes this Sync call
                });
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
