// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
namespace O2.Core.XRules.Ascx
{
    partial class ascx_XRules_UnitTests
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
            this.scTopLevel = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.llExecuteAllLoadedTests = new System.Windows.Forms.LinkLabel();
            this.progressBarExecutionStatus = new System.Windows.Forms.ProgressBar();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tvXRulesFromUnitTests = new System.Windows.Forms.TreeView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanelWithResults = new System.Windows.Forms.FlowLayoutPanel();
            this.cbAutoExecuteUnitTestAfterLoad = new System.Windows.Forms.CheckBox();
            this.scTopLevel.Panel1.SuspendLayout();
            this.scTopLevel.Panel2.SuspendLayout();
            this.scTopLevel.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // scTopLevel
            // 
            this.scTopLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scTopLevel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scTopLevel.Location = new System.Drawing.Point(0, 0);
            this.scTopLevel.Name = "scTopLevel";
            this.scTopLevel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scTopLevel.Panel1
            // 
            this.scTopLevel.Panel1.Controls.Add(this.groupBox3);
            // 
            // scTopLevel.Panel2
            // 
            this.scTopLevel.Panel2.Controls.Add(this.splitContainer2);
            this.scTopLevel.Size = new System.Drawing.Size(429, 394);
            this.scTopLevel.SplitterDistance = 65;
            this.scTopLevel.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbAutoExecuteUnitTestAfterLoad);
            this.groupBox3.Controls.Add(this.llExecuteAllLoadedTests);
            this.groupBox3.Controls.Add(this.progressBarExecutionStatus);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(429, 65);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Execution Status && Config";
            // 
            // llExecuteAllLoadedTests
            // 
            this.llExecuteAllLoadedTests.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llExecuteAllLoadedTests.AutoSize = true;
            this.llExecuteAllLoadedTests.Location = new System.Drawing.Point(308, 42);
            this.llExecuteAllLoadedTests.Name = "llExecuteAllLoadedTests";
            this.llExecuteAllLoadedTests.Size = new System.Drawing.Size(118, 13);
            this.llExecuteAllLoadedTests.TabIndex = 5;
            this.llExecuteAllLoadedTests.TabStop = true;
            this.llExecuteAllLoadedTests.Text = "execute all loaded tests";
            this.llExecuteAllLoadedTests.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llExecuteAllLoadedTests_LinkClicked);
            // 
            // progressBarExecutionStatus
            // 
            this.progressBarExecutionStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarExecutionStatus.Location = new System.Drawing.Point(6, 13);
            this.progressBarExecutionStatus.Name = "progressBarExecutionStatus";
            this.progressBarExecutionStatus.Size = new System.Drawing.Size(417, 23);
            this.progressBarExecutionStatus.TabIndex = 4;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Panel2MinSize = 100;
            this.splitContainer2.Size = new System.Drawing.Size(429, 325);
            this.splitContainer2.SplitterDistance = 274;
            this.splitContainer2.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tvXRulesFromUnitTests);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 325);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Loaded Unit Tests";
            // 
            // tvXRulesFromUnitTests
            // 
            this.tvXRulesFromUnitTests.AllowDrop = true;
            this.tvXRulesFromUnitTests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvXRulesFromUnitTests.Location = new System.Drawing.Point(3, 16);
            this.tvXRulesFromUnitTests.Name = "tvXRulesFromUnitTests";
            this.tvXRulesFromUnitTests.Size = new System.Drawing.Size(268, 306);
            this.tvXRulesFromUnitTests.TabIndex = 3;
            this.tvXRulesFromUnitTests.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tvXRulesFromUnitTests_MouseDoubleClick);
            this.tvXRulesFromUnitTests.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvXRulesFromUnitTests_DragDrop);
            this.tvXRulesFromUnitTests.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvXRulesFromUnitTests_DragEnter);
            this.tvXRulesFromUnitTests.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tvXRulesFromUnitTests_KeyUp);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.flowLayoutPanelWithResults);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(151, 325);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Execution Result";
            // 
            // flowLayoutPanelWithResults
            // 
            this.flowLayoutPanelWithResults.AutoScroll = true;
            this.flowLayoutPanelWithResults.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.flowLayoutPanelWithResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelWithResults.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanelWithResults.Name = "flowLayoutPanelWithResults";
            this.flowLayoutPanelWithResults.Size = new System.Drawing.Size(145, 306);
            this.flowLayoutPanelWithResults.TabIndex = 0;
            // 
            // cbAutoExecuteUnitTestAfterLoad
            // 
            this.cbAutoExecuteUnitTestAfterLoad.AutoSize = true;
            this.cbAutoExecuteUnitTestAfterLoad.Location = new System.Drawing.Point(6, 41);
            this.cbAutoExecuteUnitTestAfterLoad.Name = "cbAutoExecuteUnitTestAfterLoad";
            this.cbAutoExecuteUnitTestAfterLoad.Size = new System.Drawing.Size(183, 17);
            this.cbAutoExecuteUnitTestAfterLoad.TabIndex = 9;
            this.cbAutoExecuteUnitTestAfterLoad.Text = "Auto execute Unit tests after load";
            this.cbAutoExecuteUnitTestAfterLoad.UseVisualStyleBackColor = true;
            // 
            // ascx_XRules_UnitTests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scTopLevel);
            this.Name = "ascx_XRules_UnitTests";
            this.Size = new System.Drawing.Size(429, 394);
            this.Load += new System.EventHandler(this.ascx_XRules_UnitTests_Load);
            this.scTopLevel.Panel1.ResumeLayout(false);
            this.scTopLevel.Panel2.ResumeLayout(false);
            this.scTopLevel.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scTopLevel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelWithResults;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TreeView tvXRulesFromUnitTests;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ProgressBar progressBarExecutionStatus;
        private System.Windows.Forms.LinkLabel llExecuteAllLoadedTests;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.CheckBox cbAutoExecuteUnitTestAfterLoad;
    }
}
