// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Views.ASCX.DataViewers;

namespace O2.Tool.SearchAssessmentRun.Ascx
{
    partial class ascx_FindingsSearchViewer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ascx_FindingsSearchViewer));
            this.lbFilterList = new System.Windows.Forms.ListBox();
            this.tvSearchFilters = new System.Windows.Forms.TreeView();
            this.tvFilteredFindings = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.llCurrentFilter_RemoveSelected = new System.Windows.Forms.LinkLabel();
            this.llCurrentFilter_RemoveAll = new System.Windows.Forms.LinkLabel();
            this.cbFilterList_UseDoubleClickToAddFilter = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbFilteredFindings_MaxNodesToShow = new System.Windows.Forms.TextBox();
            this.cbUniqueListWhenDroppingFilter = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.ascx_FunctionsViewer1 = new O2.Views.ASCX.DataViewers.ascx_FunctionsViewer();
            this.llFilteredFindings_RemoveSelected = new System.Windows.Forms.LinkLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbFilterList
            // 
            this.lbFilterList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFilterList.FormattingEnabled = true;
            this.lbFilterList.Location = new System.Drawing.Point(6, 35);
            this.lbFilterList.Name = "lbFilterList";
            this.lbFilterList.Size = new System.Drawing.Size(162, 212);
            this.lbFilterList.TabIndex = 1;
            this.lbFilterList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbFilterList_MouseDoubleClick);
            this.lbFilterList.SelectedIndexChanged += new System.EventHandler(this.lbFilterList_SelectedIndexChanged);
            this.lbFilterList.DoubleClick += new System.EventHandler(this.lbFilterList_DoubleClick);
            this.lbFilterList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbFilterList_MouseDown);
            // 
            // tvSearchFilters
            // 
            this.tvSearchFilters.AllowDrop = true;
            this.tvSearchFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvSearchFilters.HideSelection = false;
            this.tvSearchFilters.Location = new System.Drawing.Point(4, 39);
            this.tvSearchFilters.Name = "tvSearchFilters";
            this.tvSearchFilters.Size = new System.Drawing.Size(164, 79);
            this.tvSearchFilters.TabIndex = 2;
            this.tvSearchFilters.DoubleClick += new System.EventHandler(this.tvCurrentFilters_DoubleClick);
            this.tvSearchFilters.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvCurrentFilters_DragDrop);
            this.tvSearchFilters.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvCurrentFilters_AfterSelect);
            this.tvSearchFilters.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tvCurrentFilters_MouseMove);
            this.tvSearchFilters.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvCurrentFilters_MouseDown);
            this.tvSearchFilters.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvCurrentFilters_DragEnter);
            this.tvSearchFilters.DragOver += new System.Windows.Forms.DragEventHandler(this.tvCurrentFilters_DragOver);
            // 
            // tvFilteredFindings
            // 
            this.tvFilteredFindings.AllowDrop = true;
            this.tvFilteredFindings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvFilteredFindings.HideSelection = false;
            this.tvFilteredFindings.ImageIndex = 0;
            this.tvFilteredFindings.ImageList = this.imageList1;
            this.tvFilteredFindings.Location = new System.Drawing.Point(3, 26);
            this.tvFilteredFindings.Name = "tvFilteredFindings";
            this.tvFilteredFindings.SelectedImageIndex = 0;
            this.tvFilteredFindings.Size = new System.Drawing.Size(140, 315);
            this.tvFilteredFindings.TabIndex = 3;
            this.tvFilteredFindings.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvFilteredFindings_DragDrop);
            this.tvFilteredFindings.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvFilteredFindings_AfterSelect);
            this.tvFilteredFindings.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvFilteredFindings_DragEnter);
            this.tvFilteredFindings.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvFilteredFindings_ItemDrag);
            this.tvFilteredFindings.DragOver += new System.Windows.Forms.DragEventHandler(this.tvFilteredFindings_DragOver);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Findings_SmartAudit_NoResults.ico");
            this.imageList1.Images.SetKeyName(1, "Findings_High.ico");
            this.imageList1.Images.SetKeyName(2, "Findings_Info.ico");
            this.imageList1.Images.SetKeyName(3, "Findings_Medium.ico");
            this.imageList1.Images.SetKeyName(4, "Findings_SmartAudit_Info.ico");
            this.imageList1.Images.SetKeyName(5, "Findings_VulnType.ico");
            this.imageList1.Images.SetKeyName(6, "Findings_TypeI.ico");
            this.imageList1.Images.SetKeyName(7, "Findings_TypeII.ico");
            this.imageList1.Images.SetKeyName(8, "Findings_Vulnerability.ico");
            this.imageList1.Images.SetKeyName(9, "Explorer_File.ico");
            this.imageList1.Images.SetKeyName(10, "SmartTrace.ico");
            // 
            // llCurrentFilter_RemoveSelected
            // 
            this.llCurrentFilter_RemoveSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llCurrentFilter_RemoveSelected.AutoSize = true;
            this.llCurrentFilter_RemoveSelected.Location = new System.Drawing.Point(1, 121);
            this.llCurrentFilter_RemoveSelected.Name = "llCurrentFilter_RemoveSelected";
            this.llCurrentFilter_RemoveSelected.Size = new System.Drawing.Size(92, 13);
            this.llCurrentFilter_RemoveSelected.TabIndex = 4;
            this.llCurrentFilter_RemoveSelected.TabStop = true;
            this.llCurrentFilter_RemoveSelected.Text = "Remove Selected";
            this.llCurrentFilter_RemoveSelected.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llCurrentFilter_RemoveSelected_LinkClicked);
            // 
            // llCurrentFilter_RemoveAll
            // 
            this.llCurrentFilter_RemoveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llCurrentFilter_RemoveAll.AutoSize = true;
            this.llCurrentFilter_RemoveAll.Location = new System.Drawing.Point(99, 121);
            this.llCurrentFilter_RemoveAll.Name = "llCurrentFilter_RemoveAll";
            this.llCurrentFilter_RemoveAll.Size = new System.Drawing.Size(61, 13);
            this.llCurrentFilter_RemoveAll.TabIndex = 5;
            this.llCurrentFilter_RemoveAll.TabStop = true;
            this.llCurrentFilter_RemoveAll.Text = "Remove All";
            this.llCurrentFilter_RemoveAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llCurrentFilter_RemoveAll_LinkClicked);
            // 
            // cbFilterList_UseDoubleClickToAddFilter
            // 
            this.cbFilterList_UseDoubleClickToAddFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbFilterList_UseDoubleClickToAddFilter.Location = new System.Drawing.Point(6, 249);
            this.cbFilterList_UseDoubleClickToAddFilter.Name = "cbFilterList_UseDoubleClickToAddFilter";
            this.cbFilterList_UseDoubleClickToAddFilter.Size = new System.Drawing.Size(167, 24);
            this.cbFilterList_UseDoubleClickToAddFilter.TabIndex = 6;
            this.cbFilterList_UseDoubleClickToAddFilter.Text = "Use Double Click to add Filter";
            this.cbFilterList_UseDoubleClickToAddFilter.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "\'Double Click and Hold\' to Reorder";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Drag && Drop to add";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 395);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Max Nodes to Show:";
            // 
            // tbFilteredFindings_MaxNodesToShow
            // 
            this.tbFilteredFindings_MaxNodesToShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbFilteredFindings_MaxNodesToShow.Enabled = false;
            this.tbFilteredFindings_MaxNodesToShow.Location = new System.Drawing.Point(112, 391);
            this.tbFilteredFindings_MaxNodesToShow.Name = "tbFilteredFindings_MaxNodesToShow";
            this.tbFilteredFindings_MaxNodesToShow.Size = new System.Drawing.Size(46, 20);
            this.tbFilteredFindings_MaxNodesToShow.TabIndex = 11;
            this.tbFilteredFindings_MaxNodesToShow.Text = "1000";
            // 
            // cbUniqueListWhenDroppingFilter
            // 
            this.cbUniqueListWhenDroppingFilter.AutoSize = true;
            this.cbUniqueListWhenDroppingFilter.Checked = true;
            this.cbUniqueListWhenDroppingFilter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUniqueListWhenDroppingFilter.Location = new System.Drawing.Point(3, 3);
            this.cbUniqueListWhenDroppingFilter.Name = "cbUniqueListWhenDroppingFilter";
            this.cbUniqueListWhenDroppingFilter.Size = new System.Drawing.Size(179, 17);
            this.cbUniqueListWhenDroppingFilter.TabIndex = 13;
            this.cbUniqueListWhenDroppingFilter.Text = "Unique list when dropping Filter?";
            this.cbUniqueListWhenDroppingFilter.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Panel2.Controls.Add(this.llFilteredFindings_RemoveSelected);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.tbFilteredFindings_MaxNodesToShow);
            this.splitContainer1.Size = new System.Drawing.Size(486, 417);
            this.splitContainer1.SplitterDistance = 175;
            this.splitContainer1.TabIndex = 14;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label4);
            this.splitContainer2.Panel1.Controls.Add(this.llCurrentFilter_RemoveAll);
            this.splitContainer2.Panel1.Controls.Add(this.tvSearchFilters);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.llCurrentFilter_RemoveSelected);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.label5);
            this.splitContainer2.Panel2.Controls.Add(this.cbFilterList_UseDoubleClickToAddFilter);
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Panel2.Controls.Add(this.lbFilterList);
            this.splitContainer2.Size = new System.Drawing.Size(175, 417);
            this.splitContainer2.SplitterDistance = 138;
            this.splitContainer2.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Search Filters";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Add Search Filters";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer3.Location = new System.Drawing.Point(4, 39);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.ascx_FunctionsViewer1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tvFilteredFindings);
            this.splitContainer3.Panel2.Controls.Add(this.cbUniqueListWhenDroppingFilter);
            this.splitContainer3.Size = new System.Drawing.Size(298, 346);
            this.splitContainer3.SplitterDistance = 147;
            this.splitContainer3.TabIndex = 16;
            // 
            // ascx_FunctionsViewer1
            // 
            this.ascx_FunctionsViewer1.BackColor = System.Drawing.SystemColors.Control;
            this.ascx_FunctionsViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ascx_FunctionsViewer1.ForeColor = System.Drawing.Color.Black;
            this.ascx_FunctionsViewer1.Location = new System.Drawing.Point(0, 0);
            this.ascx_FunctionsViewer1.Name = "ascx_FunctionsViewer1";
            this.ascx_FunctionsViewer1.NamespaceDepthValue = 2;
            this.ascx_FunctionsViewer1.Size = new System.Drawing.Size(143, 342);
            this.ascx_FunctionsViewer1.TabIndex = 0;
            // 
            // llFilteredFindings_RemoveSelected
            // 
            this.llFilteredFindings_RemoveSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llFilteredFindings_RemoveSelected.AutoSize = true;
            this.llFilteredFindings_RemoveSelected.Location = new System.Drawing.Point(164, 395);
            this.llFilteredFindings_RemoveSelected.Name = "llFilteredFindings_RemoveSelected";
            this.llFilteredFindings_RemoveSelected.Size = new System.Drawing.Size(92, 13);
            this.llFilteredFindings_RemoveSelected.TabIndex = 15;
            this.llFilteredFindings_RemoveSelected.TabStop = true;
            this.llFilteredFindings_RemoveSelected.Text = "Remove Selected";
            this.llFilteredFindings_RemoveSelected.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llFilteredFindings_RemoveSelected_LinkClicked);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Filtered Findings";
            // 
            // ascx_FindingsSearchViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ascx_FindingsSearchViewer";
            this.Size = new System.Drawing.Size(486, 417);
            this.Load += new System.EventHandler(this.ascx_FindingsSearchViewer_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbFilterList;
        private System.Windows.Forms.TreeView tvSearchFilters;
        private System.Windows.Forms.TreeView tvFilteredFindings;
        private System.Windows.Forms.LinkLabel llCurrentFilter_RemoveSelected;
        private System.Windows.Forms.LinkLabel llCurrentFilter_RemoveAll;
        private System.Windows.Forms.CheckBox cbFilterList_UseDoubleClickToAddFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFilteredFindings_MaxNodesToShow;
        private System.Windows.Forms.CheckBox cbUniqueListWhenDroppingFilter;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.LinkLabel llFilteredFindings_RemoveSelected;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private ascx_FunctionsViewer ascx_FunctionsViewer1;
    }
}
