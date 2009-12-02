// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.External.SharpDevelop.Ascx;
using O2.Legacy.OunceV6.GLEEGraphWiz.Ascx;
using O2.Views.ASCX.CoreControls;
using O2.Views.ASCX.SourceCodeEdit;

namespace O2.Legacy.OunceV6.TraceViewer
{
    partial class ascx_TraceViewer
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
            this.rbSmartTraceFilter_MethodName = new System.Windows.Forms.RadioButton();
            this.tvSmartTrace = new System.Windows.Forms.TreeView();
            this.rbSmartTraceFilter_Context = new System.Windows.Forms.RadioButton();
            this.ascx_SourceCodeEditor1 = new ascx_SourceCodeEditor();
            this.scHost = new System.Windows.Forms.SplitContainer();
            this.scLeft = new System.Windows.Forms.SplitContainer();
            this.rbSmartTraceFilter_SourceCode = new System.Windows.Forms.RadioButton();
            this.dgvFindingsDetails = new System.Windows.Forms.DataGridView();
            this.scRight = new System.Windows.Forms.SplitContainer();
            this.ascx_SelectVisiblePanels1 = new ascx_SelectVisiblePanels();
            this.scFindingAndTraceDetails = new System.Windows.Forms.SplitContainer();
            this.dgvCallInvocationDetails = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ascx_Glee1 = new ascx_Glee();
            this.scHost.Panel1.SuspendLayout();
            this.scHost.Panel2.SuspendLayout();
            this.scHost.SuspendLayout();
            this.scLeft.Panel1.SuspendLayout();
            this.scLeft.Panel2.SuspendLayout();
            this.scLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFindingsDetails)).BeginInit();
            this.scRight.Panel1.SuspendLayout();
            this.scRight.Panel2.SuspendLayout();
            this.scRight.SuspendLayout();
            this.scFindingAndTraceDetails.Panel1.SuspendLayout();
            this.scFindingAndTraceDetails.Panel2.SuspendLayout();
            this.scFindingAndTraceDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCallInvocationDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // rbSmartTraceFilter_MethodName
            // 
            this.rbSmartTraceFilter_MethodName.AutoSize = true;
            this.rbSmartTraceFilter_MethodName.Checked = true;
            this.rbSmartTraceFilter_MethodName.Location = new System.Drawing.Point(1, 1);
            this.rbSmartTraceFilter_MethodName.Name = "rbSmartTraceFilter_MethodName";
            this.rbSmartTraceFilter_MethodName.Size = new System.Drawing.Size(92, 17);
            this.rbSmartTraceFilter_MethodName.TabIndex = 25;
            this.rbSmartTraceFilter_MethodName.TabStop = true;
            this.rbSmartTraceFilter_MethodName.Text = "Method Name";
            this.rbSmartTraceFilter_MethodName.UseVisualStyleBackColor = true;
            this.rbSmartTraceFilter_MethodName.CheckedChanged += new System.EventHandler(this.rbSmartTraceFilter_MethodName_CheckedChanged);
            // 
            // tvSmartTrace
            // 
            this.tvSmartTrace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                              | System.Windows.Forms.AnchorStyles.Left)
                                                                             | System.Windows.Forms.AnchorStyles.Right)));
            this.tvSmartTrace.Location = new System.Drawing.Point(1, 24);
            this.tvSmartTrace.Name = "tvSmartTrace";
            this.tvSmartTrace.Size = new System.Drawing.Size(740, 181);
            this.tvSmartTrace.TabIndex = 22;
            this.tvSmartTrace.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSmartTrace_AfterSelect);
            // 
            // rbSmartTraceFilter_Context
            // 
            this.rbSmartTraceFilter_Context.AutoSize = true;
            this.rbSmartTraceFilter_Context.Location = new System.Drawing.Point(99, 1);
            this.rbSmartTraceFilter_Context.Name = "rbSmartTraceFilter_Context";
            this.rbSmartTraceFilter_Context.Size = new System.Drawing.Size(61, 17);
            this.rbSmartTraceFilter_Context.TabIndex = 23;
            this.rbSmartTraceFilter_Context.Text = "Context";
            this.rbSmartTraceFilter_Context.UseVisualStyleBackColor = true;
            this.rbSmartTraceFilter_Context.CheckedChanged += new System.EventHandler(this.rbSmartTraceFilter_Context_CheckedChanged);
            // 
            // ascx_SourceCodeEditor1
            // 
            this.ascx_SourceCodeEditor1.AllowDrop = true;
            this.ascx_SourceCodeEditor1.BackColor = System.Drawing.SystemColors.Control;
            this.ascx_SourceCodeEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ascx_SourceCodeEditor1.ForeColor = System.Drawing.Color.Black;
            this.ascx_SourceCodeEditor1.Location = new System.Drawing.Point(0, 0);
            this.ascx_SourceCodeEditor1.Name = "ascx_SourceCodeEditor1";
            this.ascx_SourceCodeEditor1.Size = new System.Drawing.Size(793, 283);
            this.ascx_SourceCodeEditor1.TabIndex = 0;
            this.ascx_SourceCodeEditor1.Load += new System.EventHandler(this.ascx_SourceCodeEditor1_Load);
            // 
            // scHost
            // 
            this.scHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                        | System.Windows.Forms.AnchorStyles.Left)
                                                                       | System.Windows.Forms.AnchorStyles.Right)));
            this.scHost.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scHost.Location = new System.Drawing.Point(3, 42);
            this.scHost.Name = "scHost";
            // 
            // scHost.Panel1
            // 
            this.scHost.Panel1.Controls.Add(this.scLeft);
            // 
            // scHost.Panel2
            // 
            this.scHost.Panel2.Controls.Add(this.scRight);
            this.scHost.Size = new System.Drawing.Size(1549, 695);
            this.scHost.SplitterDistance = 748;
            this.scHost.TabIndex = 1;
            // 
            // scLeft
            // 
            this.scLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scLeft.Location = new System.Drawing.Point(0, 0);
            this.scLeft.Name = "scLeft";
            this.scLeft.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scLeft.Panel1
            // 
            this.scLeft.Panel1.Controls.Add(this.rbSmartTraceFilter_SourceCode);
            this.scLeft.Panel1.Controls.Add(this.rbSmartTraceFilter_MethodName);
            this.scLeft.Panel1.Controls.Add(this.tvSmartTrace);
            this.scLeft.Panel1.Controls.Add(this.rbSmartTraceFilter_Context);
            // 
            // scLeft.Panel2
            // 
            this.scLeft.Panel2.Controls.Add(this.scFindingAndTraceDetails);
            this.scLeft.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.scLeft_Panel2_Paint);
            this.scLeft.Size = new System.Drawing.Size(748, 695);
            this.scLeft.SplitterDistance = 212;
            this.scLeft.TabIndex = 0;
            // 
            // rbSmartTraceFilter_SourceCode
            // 
            this.rbSmartTraceFilter_SourceCode.AutoSize = true;
            this.rbSmartTraceFilter_SourceCode.Location = new System.Drawing.Point(167, 2);
            this.rbSmartTraceFilter_SourceCode.Name = "rbSmartTraceFilter_SourceCode";
            this.rbSmartTraceFilter_SourceCode.Size = new System.Drawing.Size(87, 17);
            this.rbSmartTraceFilter_SourceCode.TabIndex = 26;
            this.rbSmartTraceFilter_SourceCode.TabStop = true;
            this.rbSmartTraceFilter_SourceCode.Text = "Source Code";
            this.rbSmartTraceFilter_SourceCode.UseVisualStyleBackColor = true;
            this.rbSmartTraceFilter_SourceCode.CheckedChanged += new System.EventHandler(this.rbSmartTraceFilter_SourceCode_CheckedChanged);
            // 
            // dgvFindingsDetails
            // 
            this.dgvFindingsDetails.AllowUserToAddRows = false;
            this.dgvFindingsDetails.AllowUserToDeleteRows = false;
            this.dgvFindingsDetails.AllowUserToOrderColumns = true;
            this.dgvFindingsDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                    | System.Windows.Forms.AnchorStyles.Left)
                                                                                   | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFindingsDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFindingsDetails.Location = new System.Drawing.Point(3, 16);
            this.dgvFindingsDetails.Name = "dgvFindingsDetails";
            this.dgvFindingsDetails.ReadOnly = true;
            this.dgvFindingsDetails.Size = new System.Drawing.Size(738, 216);
            this.dgvFindingsDetails.TabIndex = 1;
            // 
            // scRight
            // 
            this.scRight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scRight.Location = new System.Drawing.Point(0, 0);
            this.scRight.Name = "scRight";
            this.scRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scRight.Panel1
            // 
            this.scRight.Panel1.Controls.Add(this.ascx_SourceCodeEditor1);
            // 
            // scRight.Panel2
            // 
            this.scRight.Panel2.Controls.Add(this.ascx_Glee1);
            this.scRight.Size = new System.Drawing.Size(797, 695);
            this.scRight.SplitterDistance = 287;
            this.scRight.TabIndex = 0;
            // 
            // ascx_SelectVisiblePanels1
            // 
            this.ascx_SelectVisiblePanels1.BackColor = System.Drawing.SystemColors.Control;
            this.ascx_SelectVisiblePanels1.ForeColor = System.Drawing.Color.Black;
            this.ascx_SelectVisiblePanels1.Location = new System.Drawing.Point(2, -4);
            this.ascx_SelectVisiblePanels1.Name = "ascx_SelectVisiblePanels1";
            this.ascx_SelectVisiblePanels1.Size = new System.Drawing.Size(570, 40);
            this.ascx_SelectVisiblePanels1.TabIndex = 2;
            // 
            // scFindingAndTraceDetails
            // 
            this.scFindingAndTraceDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scFindingAndTraceDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scFindingAndTraceDetails.Location = new System.Drawing.Point(0, 0);
            this.scFindingAndTraceDetails.Name = "scFindingAndTraceDetails";
            this.scFindingAndTraceDetails.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scFindingAndTraceDetails.Panel1
            // 
            this.scFindingAndTraceDetails.Panel1.Controls.Add(this.label1);
            this.scFindingAndTraceDetails.Panel1.Controls.Add(this.dgvFindingsDetails);
            // 
            // scFindingAndTraceDetails.Panel2
            // 
            this.scFindingAndTraceDetails.Panel2.Controls.Add(this.label2);
            this.scFindingAndTraceDetails.Panel2.Controls.Add(this.dgvCallInvocationDetails);
            this.scFindingAndTraceDetails.Size = new System.Drawing.Size(748, 479);
            this.scFindingAndTraceDetails.SplitterDistance = 239;
            this.scFindingAndTraceDetails.TabIndex = 2;
            // 
            // dgvCallInvocationDetails
            // 
            this.dgvCallInvocationDetails.AllowUserToAddRows = false;
            this.dgvCallInvocationDetails.AllowUserToDeleteRows = false;
            this.dgvCallInvocationDetails.AllowUserToOrderColumns = true;
            this.dgvCallInvocationDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                          | System.Windows.Forms.AnchorStyles.Left)
                                                                                         | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCallInvocationDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCallInvocationDetails.Location = new System.Drawing.Point(3, 20);
            this.dgvCallInvocationDetails.Name = "dgvCallInvocationDetails";
            this.dgvCallInvocationDetails.ReadOnly = true;
            this.dgvCallInvocationDetails.Size = new System.Drawing.Size(738, 209);
            this.dgvCallInvocationDetails.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Finding Details ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Trace Details  (CallInvocation)";
            // 
            // ascx_Glee1
            // 
            this.ascx_Glee1.BackColor = System.Drawing.SystemColors.Control;
            this.ascx_Glee1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ascx_Glee1.ForeColor = System.Drawing.Color.Black;
            this.ascx_Glee1.Location = new System.Drawing.Point(0, 0);
            this.ascx_Glee1.Name = "ascx_Glee1";
            this.ascx_Glee1.Size = new System.Drawing.Size(793, 400);
            this.ascx_Glee1.TabIndex = 0;
            // 
            // ascx_TraceViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.ascx_SelectVisiblePanels1);
            this.Controls.Add(this.scHost);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ascx_TraceViewer";
            this.Size = new System.Drawing.Size(1555, 740);
            this.Load += new System.EventHandler(this.ascx_TraceViewer_Load);
            this.scHost.Panel1.ResumeLayout(false);
            this.scHost.Panel2.ResumeLayout(false);
            this.scHost.ResumeLayout(false);
            this.scLeft.Panel1.ResumeLayout(false);
            this.scLeft.Panel1.PerformLayout();
            this.scLeft.Panel2.ResumeLayout(false);
            this.scLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFindingsDetails)).EndInit();
            this.scRight.Panel1.ResumeLayout(false);
            this.scRight.Panel2.ResumeLayout(false);
            this.scRight.ResumeLayout(false);
            this.scFindingAndTraceDetails.Panel1.ResumeLayout(false);
            this.scFindingAndTraceDetails.Panel1.PerformLayout();
            this.scFindingAndTraceDetails.Panel2.ResumeLayout(false);
            this.scFindingAndTraceDetails.Panel2.PerformLayout();
            this.scFindingAndTraceDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCallInvocationDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvSmartTrace;
        private System.Windows.Forms.RadioButton rbSmartTraceFilter_Context;
        private System.Windows.Forms.RadioButton rbSmartTraceFilter_MethodName;     
        private ascx_SourceCodeEditor ascx_SourceCodeEditor1;
        private ascx_Glee ascx_Glee1;
        private System.Windows.Forms.SplitContainer scHost;
        private System.Windows.Forms.SplitContainer scLeft;
        private System.Windows.Forms.SplitContainer scRight;
        private ascx_SelectVisiblePanels ascx_SelectVisiblePanels1;
        private System.Windows.Forms.RadioButton rbSmartTraceFilter_SourceCode;
        private System.Windows.Forms.DataGridView dgvFindingsDetails;
        private System.Windows.Forms.SplitContainer scFindingAndTraceDetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvCallInvocationDetails;
    }
}
