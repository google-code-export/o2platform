namespace O2.Core.XRules.Ascx
{
    partial class ascx_XRules_Execution
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
            this.btReCompileRules = new System.Windows.Forms.Button();
            this.splitContainer9 = new System.Windows.Forms.SplitContainer();
            this.cbRecompileRulesOnGlobalRecompileEvent = new System.Windows.Forms.CheckBox();
            this.lbCurrentTask = new System.Windows.Forms.Label();
            this.progressBar_RulesCompilation = new System.Windows.Forms.ProgressBar();
            this.lbCompiledXRules = new System.Windows.Forms.ListBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.cbLoadFileAsObject = new System.Windows.Forms.CheckBox();
            this.llClearLoadedList = new System.Windows.Forms.LinkLabel();
            this.lbLoadedArtifacts = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cbAutoExecuteLastMethod = new System.Windows.Forms.CheckBox();
            this.lbXRule_MethodsAvailable = new System.Windows.Forms.ListBox();
            this.btExecuteXRule = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.findingsViewer_XRulesExecution = new O2.Views.ASCX.O2Findings.ascx_FindingsViewer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbXRuleExecutionLog = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.splitContainer9.Panel1.SuspendLayout();
            this.splitContainer9.Panel2.SuspendLayout();
            this.splitContainer9.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btReCompileRules
            // 
            this.btReCompileRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btReCompileRules.Location = new System.Drawing.Point(3, 228);
            this.btReCompileRules.Name = "btReCompileRules";
            this.btReCompileRules.Size = new System.Drawing.Size(128, 27);
            this.btReCompileRules.TabIndex = 2;
            this.btReCompileRules.Text = "Re-compile XRules";
            this.btReCompileRules.UseVisualStyleBackColor = true;
            this.btReCompileRules.Click += new System.EventHandler(this.btCompileRules_Click);
            // 
            // splitContainer9
            // 
            this.splitContainer9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer9.Location = new System.Drawing.Point(0, 0);
            this.splitContainer9.Name = "splitContainer9";
            this.splitContainer9.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer9.Panel1
            // 
            this.splitContainer9.Panel1.Controls.Add(this.cbRecompileRulesOnGlobalRecompileEvent);
            this.splitContainer9.Panel1.Controls.Add(this.lbCurrentTask);
            this.splitContainer9.Panel1.Controls.Add(this.progressBar_RulesCompilation);
            this.splitContainer9.Panel1.Controls.Add(this.lbCompiledXRules);
            this.splitContainer9.Panel1.Controls.Add(this.btReCompileRules);
            // 
            // splitContainer9.Panel2
            // 
            this.splitContainer9.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer9.Size = new System.Drawing.Size(699, 623);
            this.splitContainer9.SplitterDistance = 262;
            this.splitContainer9.TabIndex = 11;
            // 
            // cbRecompileRulesOnGlobalRecompileEvent
            // 
            this.cbRecompileRulesOnGlobalRecompileEvent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbRecompileRulesOnGlobalRecompileEvent.AutoSize = true;
            this.cbRecompileRulesOnGlobalRecompileEvent.Location = new System.Drawing.Point(372, 241);
            this.cbRecompileRulesOnGlobalRecompileEvent.Name = "cbRecompileRulesOnGlobalRecompileEvent";
            this.cbRecompileRulesOnGlobalRecompileEvent.Size = new System.Drawing.Size(196, 17);
            this.cbRecompileRulesOnGlobalRecompileEvent.TabIndex = 6;
            this.cbRecompileRulesOnGlobalRecompileEvent.Text = "Recompile Rules on \'Compile Event\'";
            this.cbRecompileRulesOnGlobalRecompileEvent.UseVisualStyleBackColor = true;
            this.cbRecompileRulesOnGlobalRecompileEvent.CheckedChanged += new System.EventHandler(this.cbRecompileRulesOnGlobalRecompileEvent_CheckedChanged);
            // 
            // lbCurrentTask
            // 
            this.lbCurrentTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbCurrentTask.AutoSize = true;
            this.lbCurrentTask.Location = new System.Drawing.Point(143, 243);
            this.lbCurrentTask.Name = "lbCurrentTask";
            this.lbCurrentTask.Size = new System.Drawing.Size(16, 13);
            this.lbCurrentTask.TabIndex = 5;
            this.lbCurrentTask.Text = "...";
            // 
            // progressBar_RulesCompilation
            // 
            this.progressBar_RulesCompilation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar_RulesCompilation.Location = new System.Drawing.Point(137, 228);
            this.progressBar_RulesCompilation.Name = "progressBar_RulesCompilation";
            this.progressBar_RulesCompilation.Size = new System.Drawing.Size(555, 12);
            this.progressBar_RulesCompilation.TabIndex = 4;
            // 
            // lbCompiledXRules
            // 
            this.lbCompiledXRules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCompiledXRules.FormattingEnabled = true;
            this.lbCompiledXRules.Location = new System.Drawing.Point(0, -1);
            this.lbCompiledXRules.Name = "lbCompiledXRules";
            this.lbCompiledXRules.Size = new System.Drawing.Size(695, 212);
            this.lbCompiledXRules.Sorted = true;
            this.lbCompiledXRules.TabIndex = 3;
            this.lbCompiledXRules.SelectedIndexChanged += new System.EventHandler(this.lbCompiledXRules_SelectedIndexChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.cbLoadFileAsObject);
            this.splitContainer2.Panel1.Controls.Add(this.llClearLoadedList);
            this.splitContainer2.Panel1.Controls.Add(this.lbLoadedArtifacts);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer2.Size = new System.Drawing.Size(699, 357);
            this.splitContainer2.SplitterDistance = 232;
            this.splitContainer2.TabIndex = 6;
            // 
            // cbLoadFileAsObject
            // 
            this.cbLoadFileAsObject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbLoadFileAsObject.AutoSize = true;
            this.cbLoadFileAsObject.Location = new System.Drawing.Point(4, 332);
            this.cbLoadFileAsObject.Name = "cbLoadFileAsObject";
            this.cbLoadFileAsObject.Size = new System.Drawing.Size(112, 17);
            this.cbLoadFileAsObject.TabIndex = 3;
            this.cbLoadFileAsObject.Text = "Load file as object";
            this.cbLoadFileAsObject.UseVisualStyleBackColor = true;
            // 
            // llClearLoadedList
            // 
            this.llClearLoadedList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llClearLoadedList.AutoSize = true;
            this.llClearLoadedList.Location = new System.Drawing.Point(114, 7);
            this.llClearLoadedList.Name = "llClearLoadedList";
            this.llClearLoadedList.Size = new System.Drawing.Size(105, 13);
            this.llClearLoadedList.TabIndex = 2;
            this.llClearLoadedList.TabStop = true;
            this.llClearLoadedList.Text = "clear loaded artifacts";
            this.llClearLoadedList.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llClearLoadedList_LinkClicked);
            // 
            // lbLoadedArtifacts
            // 
            this.lbLoadedArtifacts.AllowDrop = true;
            this.lbLoadedArtifacts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLoadedArtifacts.FormattingEnabled = true;
            this.lbLoadedArtifacts.Location = new System.Drawing.Point(3, 27);
            this.lbLoadedArtifacts.Name = "lbLoadedArtifacts";
            this.lbLoadedArtifacts.Size = new System.Drawing.Size(222, 303);
            this.lbLoadedArtifacts.Sorted = true;
            this.lbLoadedArtifacts.TabIndex = 1;
            this.lbLoadedArtifacts.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbLoadedArtifacts_DragDrop);
            this.lbLoadedArtifacts.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbLoadedArtifacts_DragEnter);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Loaded Artifacts";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(459, 353);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selected Rule Details";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cbAutoExecuteLastMethod);
            this.splitContainer1.Panel1.Controls.Add(this.lbXRule_MethodsAvailable);
            this.splitContainer1.Panel1.Controls.Add(this.btExecuteXRule);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Size = new System.Drawing.Size(453, 334);
            this.splitContainer1.SplitterDistance = 218;
            this.splitContainer1.TabIndex = 5;
            // 
            // cbAutoExecuteLastMethod
            // 
            this.cbAutoExecuteLastMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbAutoExecuteLastMethod.AutoSize = true;
            this.cbAutoExecuteLastMethod.Location = new System.Drawing.Point(6, 306);
            this.cbAutoExecuteLastMethod.Name = "cbAutoExecuteLastMethod";
            this.cbAutoExecuteLastMethod.Size = new System.Drawing.Size(151, 17);
            this.cbAutoExecuteLastMethod.TabIndex = 5;
            this.cbAutoExecuteLastMethod.Text = "Auto execute Last Method";
            this.cbAutoExecuteLastMethod.UseVisualStyleBackColor = true;
            // 
            // lbXRule_MethodsAvailable
            // 
            this.lbXRule_MethodsAvailable.AllowDrop = true;
            this.lbXRule_MethodsAvailable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbXRule_MethodsAvailable.FormattingEnabled = true;
            this.lbXRule_MethodsAvailable.Location = new System.Drawing.Point(3, 61);
            this.lbXRule_MethodsAvailable.Name = "lbXRule_MethodsAvailable";
            this.lbXRule_MethodsAvailable.Size = new System.Drawing.Size(208, 238);
            this.lbXRule_MethodsAvailable.Sorted = true;
            this.lbXRule_MethodsAvailable.TabIndex = 2;
            this.lbXRule_MethodsAvailable.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbXRule_MethodsAvailable_MouseDoubleClick);
            this.lbXRule_MethodsAvailable.SelectedIndexChanged += new System.EventHandler(this.lbXRule_MethodsAvailable_SelectedIndexChanged);
            // 
            // btExecuteXRule
            // 
            this.btExecuteXRule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btExecuteXRule.Location = new System.Drawing.Point(1, 8);
            this.btExecuteXRule.Name = "btExecuteXRule";
            this.btExecuteXRule.Size = new System.Drawing.Size(210, 22);
            this.btExecuteXRule.TabIndex = 4;
            this.btExecuteXRule.Text = "Execute Selected XRule";
            this.btExecuteXRule.UseVisualStyleBackColor = true;
            this.btExecuteXRule.Click += new System.EventHandler(this.btExecuteXRule_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "XRule Methods available";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer3.Location = new System.Drawing.Point(3, 22);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer3.Size = new System.Drawing.Size(223, 306);
            this.splitContainer3.SplitterDistance = 159;
            this.splitContainer3.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.findingsViewer_XRulesExecution);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(223, 159);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Findings";
            // 
            // findingsViewer_XRulesExecution
            // 
            this.findingsViewer_XRulesExecution._ShowNoEnginesLoadedAlert = false;
            this.findingsViewer_XRulesExecution._SimpleViewMode = true;
            this.findingsViewer_XRulesExecution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.findingsViewer_XRulesExecution.Location = new System.Drawing.Point(3, 16);
            this.findingsViewer_XRulesExecution.Name = "findingsViewer_XRulesExecution";
            this.findingsViewer_XRulesExecution.Size = new System.Drawing.Size(217, 140);
            this.findingsViewer_XRulesExecution.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbXRuleExecutionLog);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(223, 143);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Execution Log";
            // 
            // tbXRuleExecutionLog
            // 
            this.tbXRuleExecutionLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbXRuleExecutionLog.Location = new System.Drawing.Point(3, 16);
            this.tbXRuleExecutionLog.Multiline = true;
            this.tbXRuleExecutionLog.Name = "tbXRuleExecutionLog";
            this.tbXRuleExecutionLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbXRuleExecutionLog.Size = new System.Drawing.Size(217, 124);
            this.tbXRuleExecutionLog.TabIndex = 0;
            this.tbXRuleExecutionLog.WordWrap = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "XRule results";
            // 
            // ascx_XRules_Execution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer9);
            this.Name = "ascx_XRules_Execution";
            this.Size = new System.Drawing.Size(699, 623);
            this.Load += new System.EventHandler(this.ascx_XRules_Execution_Load);
            this.splitContainer9.Panel1.ResumeLayout(false);
            this.splitContainer9.Panel1.PerformLayout();
            this.splitContainer9.Panel2.ResumeLayout(false);
            this.splitContainer9.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btReCompileRules;
        private System.Windows.Forms.SplitContainer splitContainer9;
        private System.Windows.Forms.Button btExecuteXRule;
        private System.Windows.Forms.ListBox lbCompiledXRules;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar progressBar_RulesCompilation;
        private System.Windows.Forms.Label lbCurrentTask;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lbLoadedArtifacts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListBox lbXRule_MethodsAvailable;
        private System.Windows.Forms.Label label2;
        private O2.Views.ASCX.O2Findings.ascx_FindingsViewer findingsViewer_XRulesExecution;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbXRuleExecutionLog;
        private System.Windows.Forms.LinkLabel llClearLoadedList;
        private System.Windows.Forms.CheckBox cbAutoExecuteLastMethod;
        private System.Windows.Forms.CheckBox cbLoadFileAsObject;
        private System.Windows.Forms.CheckBox cbRecompileRulesOnGlobalRecompileEvent;
    }
}