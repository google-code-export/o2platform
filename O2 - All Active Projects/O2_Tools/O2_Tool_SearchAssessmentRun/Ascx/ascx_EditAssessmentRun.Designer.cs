// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Kernel.CodeUtils;
using O2.Kernel.CodeUtils;
using O2.Legacy.OunceV6.TraceViewer;
using O2.Views.ASCX.CoreControls;

namespace O2.Tool.SearchAssessmentRun.Ascx
{
    partial class ascx_EditAssessmentRun
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
            this.btResetList = new System.Windows.Forms.Button();
            this.ascx_DropObject1 = new ascx_DropObject();
            this.tbSavedAssessmentFileName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btCreateAssessmentRunWithSearchResults = new System.Windows.Forms.Button();
            this.cbSearchResults_Findings_UniqueList = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbSearchResults_Findings_Filter = new System.Windows.Forms.ComboBox();
            this.lbSearchResults_Findings = new System.Windows.Forms.ListBox();
            this.atvTraceViewer = new ascx_TraceViewer();
            this.cbCreateFileWithAllTraces = new System.Windows.Forms.CheckBox();
            this.cbIgnoreRootCallInvocation = new System.Windows.Forms.CheckBox();
            this.cbDropDuplicateSmartTraces = new System.Windows.Forms.CheckBox();
            this.cbCreateFileWithUniqueTraces = new System.Windows.Forms.CheckBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cbCreateFileWithAllTraces);
            this.splitContainer1.Panel1.Controls.Add(this.cbIgnoreRootCallInvocation);
            this.splitContainer1.Panel1.Controls.Add(this.cbDropDuplicateSmartTraces);
            this.splitContainer1.Panel1.Controls.Add(this.cbCreateFileWithUniqueTraces);
            this.splitContainer1.Panel1.Controls.Add(this.btResetList);
            this.splitContainer1.Panel1.Controls.Add(this.ascx_DropObject1);
            this.splitContainer1.Panel1.Controls.Add(this.tbSavedAssessmentFileName);
            this.splitContainer1.Panel1.Controls.Add(this.label9);
            this.splitContainer1.Panel1.Controls.Add(this.btCreateAssessmentRunWithSearchResults);
            this.splitContainer1.Panel1.Controls.Add(this.cbSearchResults_Findings_UniqueList);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.cbSearchResults_Findings_Filter);
            this.splitContainer1.Panel1.Controls.Add(this.lbSearchResults_Findings);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.atvTraceViewer);
            this.splitContainer1.Size = new System.Drawing.Size(686, 442);
            this.splitContainer1.SplitterDistance = 394;
            this.splitContainer1.TabIndex = 0;
            // 
            // btResetList
            // 
            this.btResetList.BackColor = System.Drawing.SystemColors.Control;
            this.btResetList.Location = new System.Drawing.Point(144, 413);
            this.btResetList.Name = "btResetList";
            this.btResetList.Size = new System.Drawing.Size(75, 23);
            this.btResetList.TabIndex = 35;
            this.btResetList.Text = "reset";
            this.btResetList.UseVisualStyleBackColor = false;
            this.btResetList.Click += new System.EventHandler(this.btResetList_Click);
            // 
            // ascx_DropObject1
            // 
            this.ascx_DropObject1.AllowDrop = true;
            this.ascx_DropObject1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                                 | System.Windows.Forms.AnchorStyles.Right)));
            this.ascx_DropObject1.BackColor = System.Drawing.Color.Maroon;
            this.ascx_DropObject1.ForeColor = System.Drawing.Color.White;
            this.ascx_DropObject1.Location = new System.Drawing.Point(3, 0);
            this.ascx_DropObject1.Name = "ascx_DropObject1";
            this.ascx_DropObject1.Size = new System.Drawing.Size(382, 35);
            this.ascx_DropObject1.TabIndex = 34;
            this.ascx_DropObject1.Load += new System.EventHandler(this.ascx_DropObject1_Load);
            this.ascx_DropObject1.eDnDAction_ObjectDataReceived_Event += new Callbacks.dMethod_Object(this.ascx_DropObject1_eDnDAction_ObjectDataReceived_Event);
            // 
            // tbSavedAssessmentFileName
            // 
            this.tbSavedAssessmentFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                                          | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSavedAssessmentFileName.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tbSavedAssessmentFileName.Location = new System.Drawing.Point(4, 56);
            this.tbSavedAssessmentFileName.Name = "tbSavedAssessmentFileName";
            this.tbSavedAssessmentFileName.Size = new System.Drawing.Size(381, 20);
            this.tbSavedAssessmentFileName.TabIndex = 33;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                       | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(142, 13);
            this.label9.TabIndex = 32;
            this.label9.Text = "Saved Assesment File Name";
            // 
            // btCreateAssessmentRunWithSearchResults
            // 
            this.btCreateAssessmentRunWithSearchResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                                                       | System.Windows.Forms.AnchorStyles.Right)));
            this.btCreateAssessmentRunWithSearchResults.BackColor = System.Drawing.SystemColors.Control;
            this.btCreateAssessmentRunWithSearchResults.Location = new System.Drawing.Point(3, 80);
            this.btCreateAssessmentRunWithSearchResults.Name = "btCreateAssessmentRunWithSearchResults";
            this.btCreateAssessmentRunWithSearchResults.Size = new System.Drawing.Size(382, 51);
            this.btCreateAssessmentRunWithSearchResults.TabIndex = 31;
            this.btCreateAssessmentRunWithSearchResults.Text = "Create Assessment Run file with Saved Results";
            this.btCreateAssessmentRunWithSearchResults.UseVisualStyleBackColor = false;
            this.btCreateAssessmentRunWithSearchResults.Click += new System.EventHandler(this.btCreateAssessmentRunWithSearchResults_Click);
            // 
            // cbSearchResults_Findings_UniqueList
            // 
            this.cbSearchResults_Findings_UniqueList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbSearchResults_Findings_UniqueList.AutoSize = true;
            this.cbSearchResults_Findings_UniqueList.Checked = true;
            this.cbSearchResults_Findings_UniqueList.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSearchResults_Findings_UniqueList.Location = new System.Drawing.Point(6, 416);
            this.cbSearchResults_Findings_UniqueList.Name = "cbSearchResults_Findings_UniqueList";
            this.cbSearchResults_Findings_UniqueList.Size = new System.Drawing.Size(79, 17);
            this.cbSearchResults_Findings_UniqueList.TabIndex = 30;
            this.cbSearchResults_Findings_UniqueList.Text = "Unique List";
            this.cbSearchResults_Findings_UniqueList.UseVisualStyleBackColor = true;
            this.cbSearchResults_Findings_UniqueList.CheckedChanged += new System.EventHandler(this.cbSearchResults_Findings_UniqueList_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 188);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Findings";
            // 
            // cbSearchResults_Findings_Filter
            // 
            this.cbSearchResults_Findings_Filter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSearchResults_Findings_Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSearchResults_Findings_Filter.FormattingEnabled = true;
            this.cbSearchResults_Findings_Filter.Location = new System.Drawing.Point(277, 185);
            this.cbSearchResults_Findings_Filter.Name = "cbSearchResults_Findings_Filter";
            this.cbSearchResults_Findings_Filter.Size = new System.Drawing.Size(108, 21);
            this.cbSearchResults_Findings_Filter.TabIndex = 29;
            this.cbSearchResults_Findings_Filter.SelectedIndexChanged += new System.EventHandler(this.cbSearchResults_Findings_Filter_SelectedIndexChanged);
            // 
            // lbSearchResults_Findings
            // 
            this.lbSearchResults_Findings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                          | System.Windows.Forms.AnchorStyles.Left)
                                                                                         | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSearchResults_Findings.FormattingEnabled = true;
            this.lbSearchResults_Findings.HorizontalScrollbar = true;
            this.lbSearchResults_Findings.Location = new System.Drawing.Point(3, 212);
            this.lbSearchResults_Findings.Name = "lbSearchResults_Findings";
            this.lbSearchResults_Findings.Size = new System.Drawing.Size(382, 199);
            this.lbSearchResults_Findings.Sorted = true;
            this.lbSearchResults_Findings.TabIndex = 27;
            this.lbSearchResults_Findings.SelectedIndexChanged += new System.EventHandler(this.lbSearchResults_Findings_SelectedIndexChanged);
            this.lbSearchResults_Findings.DoubleClick += new System.EventHandler(this.lbSearchResults_Findings_DoubleClick);
            // 
            // atvTraceViewer
            // 
            this.atvTraceViewer.BackColor = System.Drawing.SystemColors.Control;
            this.atvTraceViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.atvTraceViewer.ForeColor = System.Drawing.Color.Black;
            this.atvTraceViewer.Location = new System.Drawing.Point(0, 0);
            this.atvTraceViewer.Name = "atvTraceViewer";
            this.atvTraceViewer.Size = new System.Drawing.Size(284, 438);
            this.atvTraceViewer.TabIndex = 0;
            // 
            // cbCreateFileWithAllTraces
            // 
            this.cbCreateFileWithAllTraces.AutoSize = true;
            this.cbCreateFileWithAllTraces.Location = new System.Drawing.Point(6, 153);
            this.cbCreateFileWithAllTraces.Name = "cbCreateFileWithAllTraces";
            this.cbCreateFileWithAllTraces.Size = new System.Drawing.Size(159, 17);
            this.cbCreateFileWithAllTraces.TabIndex = 39;
            this.cbCreateFileWithAllTraces.Text = "Create File With ALL Traces";
            this.cbCreateFileWithAllTraces.UseVisualStyleBackColor = true;
            // 
            // cbIgnoreRootCallInvocation
            // 
            this.cbIgnoreRootCallInvocation.AutoSize = true;
            this.cbIgnoreRootCallInvocation.Location = new System.Drawing.Point(203, 153);
            this.cbIgnoreRootCallInvocation.Name = "cbIgnoreRootCallInvocation";
            this.cbIgnoreRootCallInvocation.Size = new System.Drawing.Size(155, 17);
            this.cbIgnoreRootCallInvocation.TabIndex = 38;
            this.cbIgnoreRootCallInvocation.Text = "Ignore Root Call Invocation";
            this.cbIgnoreRootCallInvocation.UseVisualStyleBackColor = true;
            // 
            // cbDropDuplicateSmartTraces
            // 
            this.cbDropDuplicateSmartTraces.AutoSize = true;
            this.cbDropDuplicateSmartTraces.Checked = true;
            this.cbDropDuplicateSmartTraces.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDropDuplicateSmartTraces.Location = new System.Drawing.Point(202, 136);
            this.cbDropDuplicateSmartTraces.Name = "cbDropDuplicateSmartTraces";
            this.cbDropDuplicateSmartTraces.Size = new System.Drawing.Size(160, 17);
            this.cbDropDuplicateSmartTraces.TabIndex = 37;
            this.cbDropDuplicateSmartTraces.Text = "Drop Duplicate SmartTraces";
            this.cbDropDuplicateSmartTraces.UseVisualStyleBackColor = true;
            // 
            // cbCreateFileWithUniqueTraces
            // 
            this.cbCreateFileWithUniqueTraces.AutoSize = true;
            this.cbCreateFileWithUniqueTraces.Checked = true;
            this.cbCreateFileWithUniqueTraces.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCreateFileWithUniqueTraces.Location = new System.Drawing.Point(4, 136);
            this.cbCreateFileWithUniqueTraces.Name = "cbCreateFileWithUniqueTraces";
            this.cbCreateFileWithUniqueTraces.Size = new System.Drawing.Size(174, 17);
            this.cbCreateFileWithUniqueTraces.TabIndex = 36;
            this.cbCreateFileWithUniqueTraces.Text = "Create File With Unique Traces";
            this.cbCreateFileWithUniqueTraces.UseVisualStyleBackColor = true;
            // 
            // ascx_EditAssessmentRun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.splitContainer1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ascx_EditAssessmentRun";
            this.Size = new System.Drawing.Size(686, 442);
            this.Load += new System.EventHandler(this.ascx_EditAssessmentRun_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private ascx_TraceViewer atvTraceViewer;
        private System.Windows.Forms.CheckBox cbSearchResults_Findings_UniqueList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbSearchResults_Findings_Filter;
        private System.Windows.Forms.ListBox lbSearchResults_Findings;
        private ascx_DropObject ascx_DropObject1;
        private System.Windows.Forms.TextBox tbSavedAssessmentFileName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btCreateAssessmentRunWithSearchResults;
        private System.Windows.Forms.Button btResetList;
        private System.Windows.Forms.CheckBox cbCreateFileWithAllTraces;
        private System.Windows.Forms.CheckBox cbIgnoreRootCallInvocation;
        private System.Windows.Forms.CheckBox cbDropDuplicateSmartTraces;
        private System.Windows.Forms.CheckBox cbCreateFileWithUniqueTraces;
    }
}
