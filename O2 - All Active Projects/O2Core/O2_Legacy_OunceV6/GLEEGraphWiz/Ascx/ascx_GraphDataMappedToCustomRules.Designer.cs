// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
namespace O2.Legacy.OunceV6.GLEEGraphWiz.Ascx
{
    partial class ascx_GraphDataMappedToCustomRules
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lboxNodes = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lBoxEdges = new System.Windows.Forms.ListBox();
            this.btMarkMethodAsSink = new System.Windows.Forms.Button();
            this.btMarkAllUnmarked = new System.Windows.Forms.Button();
            this.btDeleteSinkVbdActionObject = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btMarkMethodAsCallBack = new System.Windows.Forms.Button();
            this.cbLanguageDbId = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbSelectedSinkMethod = new System.Windows.Forms.Label();
            this.scGraphData = new System.Windows.Forms.SplitContainer();
            this.splitContainer8 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvGraphStats = new System.Windows.Forms.DataGridView();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.btNodes_CopyToClipboard = new System.Windows.Forms.Button();
            this.btEdges_CopyToClipboard = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.scGraphData.Panel1.SuspendLayout();
            this.scGraphData.Panel2.SuspendLayout();
            this.scGraphData.SuspendLayout();
            this.splitContainer8.Panel1.SuspendLayout();
            this.splitContainer8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGraphStats)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lboxNodes
            // 
            this.lboxNodes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                           | System.Windows.Forms.AnchorStyles.Left)
                                                                          | System.Windows.Forms.AnchorStyles.Right)));
            this.lboxNodes.FormattingEnabled = true;
            this.lboxNodes.Location = new System.Drawing.Point(3, 34);
            this.lboxNodes.Name = "lboxNodes";
            this.lboxNodes.Size = new System.Drawing.Size(361, 355);
            this.lboxNodes.TabIndex = 28;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Nodes";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Edges";
            // 
            // lBoxEdges
            // 
            this.lBoxEdges.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                           | System.Windows.Forms.AnchorStyles.Left)
                                                                          | System.Windows.Forms.AnchorStyles.Right)));
            this.lBoxEdges.FormattingEnabled = true;
            this.lBoxEdges.Location = new System.Drawing.Point(3, 34);
            this.lBoxEdges.Name = "lBoxEdges";
            this.lBoxEdges.Size = new System.Drawing.Size(386, 355);
            this.lBoxEdges.TabIndex = 31;
            // 
            // btMarkMethodAsSink
            // 
            this.btMarkMethodAsSink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btMarkMethodAsSink.Location = new System.Drawing.Point(475, 41);
            this.btMarkMethodAsSink.Name = "btMarkMethodAsSink";
            this.btMarkMethodAsSink.Size = new System.Drawing.Size(118, 23);
            this.btMarkMethodAsSink.TabIndex = 31;
            this.btMarkMethodAsSink.Text = "Mark Method as Sink";
            this.btMarkMethodAsSink.UseVisualStyleBackColor = true;
            // 
            // btMarkAllUnmarked
            // 
            this.btMarkAllUnmarked.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btMarkAllUnmarked.Location = new System.Drawing.Point(378, 70);
            this.btMarkAllUnmarked.Name = "btMarkAllUnmarked";
            this.btMarkAllUnmarked.Size = new System.Drawing.Size(149, 23);
            this.btMarkAllUnmarked.TabIndex = 34;
            this.btMarkAllUnmarked.Text = "Mark as Sinks all unmarked";
            this.btMarkAllUnmarked.UseVisualStyleBackColor = true;
            // 
            // btDeleteSinkVbdActionObject
            // 
            this.btDeleteSinkVbdActionObject.Location = new System.Drawing.Point(378, 41);
            this.btDeleteSinkVbdActionObject.Name = "btDeleteSinkVbdActionObject";
            this.btDeleteSinkVbdActionObject.Size = new System.Drawing.Size(72, 23);
            this.btDeleteSinkVbdActionObject.TabIndex = 33;
            this.btDeleteSinkVbdActionObject.Text = "Del Act.Obj.";
            this.btDeleteSinkVbdActionObject.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 35;
            this.label3.Text = "Language (dbId)";
            // 
            // btMarkMethodAsCallBack
            // 
            this.btMarkMethodAsCallBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btMarkMethodAsCallBack.Location = new System.Drawing.Point(575, 70);
            this.btMarkMethodAsCallBack.Name = "btMarkMethodAsCallBack";
            this.btMarkMethodAsCallBack.Size = new System.Drawing.Size(174, 23);
            this.btMarkMethodAsCallBack.TabIndex = 30;
            this.btMarkMethodAsCallBack.Text = "Mark Sink Method as Callback";
            this.btMarkMethodAsCallBack.UseVisualStyleBackColor = true;
            // 
            // cbLanguageDbId
            // 
            this.cbLanguageDbId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanguageDbId.FormattingEnabled = true;
            this.cbLanguageDbId.Items.AddRange(new object[] {
                                                                "1",
                                                                "2",
                                                                "3",
                                                                "4",
                                                                "5"});
            this.cbLanguageDbId.Location = new System.Drawing.Point(108, 3);
            this.cbLanguageDbId.Name = "cbLanguageDbId";
            this.cbLanguageDbId.Size = new System.Drawing.Size(35, 21);
            this.cbLanguageDbId.TabIndex = 36;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(349, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "Selected sink method:";
            // 
            // lbSelectedSinkMethod
            // 
            this.lbSelectedSinkMethod.AutoSize = true;
            this.lbSelectedSinkMethod.Location = new System.Drawing.Point(472, 14);
            this.lbSelectedSinkMethod.Name = "lbSelectedSinkMethod";
            this.lbSelectedSinkMethod.Size = new System.Drawing.Size(16, 13);
            this.lbSelectedSinkMethod.TabIndex = 57;
            this.lbSelectedSinkMethod.Text = "...";
            // 
            // scGraphData
            // 
            this.scGraphData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scGraphData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scGraphData.Location = new System.Drawing.Point(0, 0);
            this.scGraphData.Name = "scGraphData";
            // 
            // scGraphData.Panel1
            // 
            this.scGraphData.Panel1.Controls.Add(this.splitContainer8);
            this.scGraphData.Panel1Collapsed = true;
            // 
            // scGraphData.Panel2
            // 
            this.scGraphData.Panel2.Controls.Add(this.splitContainer5);
            this.scGraphData.Size = new System.Drawing.Size(769, 402);
            this.scGraphData.SplitterDistance = 59;
            this.scGraphData.TabIndex = 26;
            // 
            // splitContainer8
            // 
            this.splitContainer8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer8.Location = new System.Drawing.Point(0, 0);
            this.splitContainer8.Name = "splitContainer8";
            this.splitContainer8.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer8.Panel1
            // 
            this.splitContainer8.Panel1.Controls.Add(this.label2);
            this.splitContainer8.Panel1.Controls.Add(this.dgvGraphStats);
            this.splitContainer8.Size = new System.Drawing.Size(59, 100);
            this.splitContainer8.SplitterDistance = 68;
            this.splitContainer8.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Stats";
            // 
            // dgvGraphStats
            // 
            this.dgvGraphStats.AllowUserToAddRows = false;
            this.dgvGraphStats.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Black;
            this.dgvGraphStats.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvGraphStats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                               | System.Windows.Forms.AnchorStyles.Left)
                                                                              | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvGraphStats.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGraphStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGraphStats.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvGraphStats.Location = new System.Drawing.Point(3, 16);
            this.dgvGraphStats.Name = "dgvGraphStats";
            this.dgvGraphStats.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGraphStats.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvGraphStats.RowHeadersWidth = 4;
            this.dgvGraphStats.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGraphStats.Size = new System.Drawing.Size(49, 45);
            this.dgvGraphStats.TabIndex = 10;
            // 
            // splitContainer5
            // 
            this.splitContainer5.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer5.Panel1.Controls.Add(this.btNodes_CopyToClipboard);
            this.splitContainer5.Panel1.Controls.Add(this.lboxNodes);
            this.splitContainer5.Panel1.Controls.Add(this.label4);
            this.splitContainer5.Panel1.ForeColor = System.Drawing.Color.Black;
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer5.Panel2.Controls.Add(this.btEdges_CopyToClipboard);
            this.splitContainer5.Panel2.Controls.Add(this.label5);
            this.splitContainer5.Panel2.Controls.Add(this.lBoxEdges);
            this.splitContainer5.Panel2.ForeColor = System.Drawing.Color.Black;
            this.splitContainer5.Size = new System.Drawing.Size(769, 402);
            this.splitContainer5.SplitterDistance = 371;
            this.splitContainer5.TabIndex = 0;
            // 
            // btNodes_CopyToClipboard
            // 
            this.btNodes_CopyToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btNodes_CopyToClipboard.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btNodes_CopyToClipboard.Location = new System.Drawing.Point(222, 3);
            this.btNodes_CopyToClipboard.Name = "btNodes_CopyToClipboard";
            this.btNodes_CopyToClipboard.Size = new System.Drawing.Size(143, 26);
            this.btNodes_CopyToClipboard.TabIndex = 34;
            this.btNodes_CopyToClipboard.Text = "Copy Nodes to Clipboard";
            this.btNodes_CopyToClipboard.UseVisualStyleBackColor = false;
            this.btNodes_CopyToClipboard.Click += new System.EventHandler(this.btNodes_CopyToClipboard_Click);
            // 
            // btEdges_CopyToClipboard
            // 
            this.btEdges_CopyToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btEdges_CopyToClipboard.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btEdges_CopyToClipboard.Location = new System.Drawing.Point(243, 3);
            this.btEdges_CopyToClipboard.Name = "btEdges_CopyToClipboard";
            this.btEdges_CopyToClipboard.Size = new System.Drawing.Size(143, 26);
            this.btEdges_CopyToClipboard.TabIndex = 33;
            this.btEdges_CopyToClipboard.Text = "Copy Edges to Clipboard";
            this.btEdges_CopyToClipboard.UseVisualStyleBackColor = false;
            this.btEdges_CopyToClipboard.Click += new System.EventHandler(this.btEdges_CopyToClipboard_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.scGraphData);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lbSelectedSinkMethod);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.cbLanguageDbId);
            this.splitContainer1.Panel2.Controls.Add(this.btMarkMethodAsCallBack);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.btDeleteSinkVbdActionObject);
            this.splitContainer1.Panel2.Controls.Add(this.btMarkAllUnmarked);
            this.splitContainer1.Panel2.Controls.Add(this.btMarkMethodAsSink);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(769, 402);
            this.splitContainer1.SplitterDistance = 278;
            this.splitContainer1.TabIndex = 27;
            // 
            // ascx_GraphDataMappedToCustomRules
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.splitContainer1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ascx_GraphDataMappedToCustomRules";
            this.Size = new System.Drawing.Size(769, 402);
            this.scGraphData.Panel1.ResumeLayout(false);
            this.scGraphData.Panel2.ResumeLayout(false);
            this.scGraphData.ResumeLayout(false);
            this.splitContainer8.Panel1.ResumeLayout(false);
            this.splitContainer8.Panel1.PerformLayout();
            this.splitContainer8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGraphStats)).EndInit();
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel1.PerformLayout();
            this.splitContainer5.Panel2.ResumeLayout(false);
            this.splitContainer5.Panel2.PerformLayout();
            this.splitContainer5.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lboxNodes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lBoxEdges;
        private System.Windows.Forms.Button btMarkMethodAsSink;
        private System.Windows.Forms.Button btMarkAllUnmarked;
        private System.Windows.Forms.Button btDeleteSinkVbdActionObject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btMarkMethodAsCallBack;
        private System.Windows.Forms.ComboBox cbLanguageDbId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbSelectedSinkMethod;
        private System.Windows.Forms.SplitContainer scGraphData;
        private System.Windows.Forms.SplitContainer splitContainer8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvGraphStats;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btNodes_CopyToClipboard;
        private System.Windows.Forms.Button btEdges_CopyToClipboard;
    }
}
