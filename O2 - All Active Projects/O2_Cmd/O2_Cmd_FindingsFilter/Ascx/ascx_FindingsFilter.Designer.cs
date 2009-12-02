namespace O2.Cmd.FindingsFilter.Ascx
{
    partial class ascx_FindingsFilter
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.findingsViewer_SourceFindings = new O2.Views.ASCX.O2Findings.ascx_FindingsViewer();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tvAvailableFilters = new System.Windows.Forms.TreeView();
            this.label5 = new System.Windows.Forms.Label();
            this.btApplyFilter = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btEditFilters = new System.Windows.Forms.Button();
            this.findingsViewer_Results = new O2.Views.ASCX.O2Findings.ascx_FindingsViewer();
            this.label4 = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainer1.Panel2.Controls.Add(this.findingsViewer_Results);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Size = new System.Drawing.Size(682, 443);
            this.splitContainer1.SplitterDistance = 264;
            this.splitContainer1.TabIndex = 2;
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
            this.splitContainer2.Panel1.Controls.Add(this.findingsViewer_SourceFindings);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btEditFilters);
            this.splitContainer2.Panel2.Controls.Add(this.tvAvailableFilters);
            this.splitContainer2.Panel2.Controls.Add(this.label5);
            this.splitContainer2.Panel2.Controls.Add(this.btApplyFilter);
            this.splitContainer2.Panel2.Controls.Add(this.label3);
            this.splitContainer2.Size = new System.Drawing.Size(264, 443);
            this.splitContainer2.SplitterDistance = 221;
            this.splitContainer2.TabIndex = 3;
            // 
            // findingsViewer_SourceFindings
            // 
            this.findingsViewer_SourceFindings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.findingsViewer_SourceFindings.Location = new System.Drawing.Point(6, 51);
            this.findingsViewer_SourceFindings.Name = "findingsViewer_SourceFindings";
            this.findingsViewer_SourceFindings.Size = new System.Drawing.Size(251, 163);
            this.findingsViewer_SourceFindings.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 27);
            this.label2.TabIndex = 2;
            this.label2.Text = "Drag and Drop files or folders in the Findings Viewer below";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(238, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Step 1) Provide Target assessments files";
            // 
            // tvAvailableFilters
            // 
            this.tvAvailableFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvAvailableFilters.HideSelection = false;
            this.tvAvailableFilters.Location = new System.Drawing.Point(7, 17);
            this.tvAvailableFilters.Name = "tvAvailableFilters";
            this.tvAvailableFilters.Size = new System.Drawing.Size(250, 112);
            this.tvAvailableFilters.TabIndex = 6;
            this.tvAvailableFilters.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvAvailableFilters_NodeMouseDoubleClick);
            this.tvAvailableFilters.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvAvailableFilters_AfterSelect);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.Location = new System.Drawing.Point(4, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(238, 29);
            this.label5.TabIndex = 5;
            this.label5.Text = " (you can also double click on listbox to invoke the filter)";
            // 
            // btApplyFilter
            // 
            this.btApplyFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btApplyFilter.Location = new System.Drawing.Point(3, 135);
            this.btApplyFilter.Name = "btApplyFilter";
            this.btApplyFilter.Size = new System.Drawing.Size(254, 23);
            this.btApplyFilter.TabIndex = 4;
            this.btApplyFilter.Text = "Step 3) Apply Filter";
            this.btApplyFilter.UseVisualStyleBackColor = true;
            this.btApplyFilter.Click += new System.EventHandler(this.btApplyFilter_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(209, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Step 2) Choose which filter to apply";
            // 
            // btEditFilters
            // 
            this.btEditFilters.Enabled = false;
            this.btEditFilters.Location = new System.Drawing.Point(182, 188);
            this.btEditFilters.Name = "btEditFilters";
            this.btEditFilters.Size = new System.Drawing.Size(75, 23);
            this.btEditFilters.TabIndex = 3;
            this.btEditFilters.Text = "Edit Filters";
            this.btEditFilters.UseVisualStyleBackColor = true;
            this.btEditFilters.Visible = false;
            this.btEditFilters.Click += new System.EventHandler(this.btEditFilters_Click);
            // 
            // findingsViewer_Results
            // 
            this.findingsViewer_Results.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.findingsViewer_Results.Location = new System.Drawing.Point(1, 51);
            this.findingsViewer_Results.Name = "findingsViewer_Results";
            this.findingsViewer_Results.Size = new System.Drawing.Size(406, 385);
            this.findingsViewer_Results.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Step 3) View results";
            // 
            // ascx_FindingsFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ascx_FindingsFilter";
            this.Size = new System.Drawing.Size(682, 443);
            this.Load += new System.EventHandler(this.ascx_FindingsFilter_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btEditFilters;
        private O2.Views.ASCX.O2Findings.ascx_FindingsViewer findingsViewer_Results;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btApplyFilter;
        private O2.Views.ASCX.O2Findings.ascx_FindingsViewer findingsViewer_SourceFindings;
        private System.Windows.Forms.TreeView tvAvailableFilters;
    }
}
