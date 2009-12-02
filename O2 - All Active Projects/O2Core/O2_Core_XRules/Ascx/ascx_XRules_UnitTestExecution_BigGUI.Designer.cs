namespace O2.Core.XRules.Ascx
{
    partial class ascx_XRules_UnitTestExecution_BigGUI
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tvAssembliesToLookForUnitTests = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.directory_targetAssemblies = new O2.Views.ASCX.CoreControls.ascx_Directory();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tvXRules = new System.Windows.Forms.TreeView();
            this.llClearFlowLayoutPanelWithResults = new System.Windows.Forms.LinkLabel();
            this.flowLayoutPanelWithResults = new System.Windows.Forms.FlowLayoutPanel();
            this.cbAutoExecuteUnitTestAfterLoad = new System.Windows.Forms.CheckBox();
            this.cbAddAssemblyOnAssemblyCompileEvent = new System.Windows.Forms.CheckBox();
            this.btExecuteSelected = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.llMapXRulesForUnitTests = new System.Windows.Forms.LinkLabel();
            this.llLoadAssembliesToLookForUnitTests = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(602, 482);
            this.splitContainer1.SplitterDistance = 235;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer2.Size = new System.Drawing.Size(231, 478);
            this.splitContainer2.SplitterDistance = 238;
            this.splitContainer2.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tvAssembliesToLookForUnitTests);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(231, 238);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Assemblies To look for Unit Tests";
            // 
            // tvAssembliesToLookForUnitTests
            // 
            this.tvAssembliesToLookForUnitTests.AllowDrop = true;
            this.tvAssembliesToLookForUnitTests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvAssembliesToLookForUnitTests.Location = new System.Drawing.Point(3, 16);
            this.tvAssembliesToLookForUnitTests.Name = "tvAssembliesToLookForUnitTests";
            this.tvAssembliesToLookForUnitTests.Size = new System.Drawing.Size(225, 219);
            this.tvAssembliesToLookForUnitTests.TabIndex = 0;
            this.tvAssembliesToLookForUnitTests.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvAssembliesToLookForUnitTests_DragDrop);
            this.tvAssembliesToLookForUnitTests.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvAssembliesToLookForUnitTests_DragEnter);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.directory_targetAssemblies);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(231, 236);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current Executable Directory";
            // 
            // directory_targetAssemblies
            // 
            this.directory_targetAssemblies._FileFilter = "*.dll";
            this.directory_targetAssemblies._ProcessDroppedObjects = false;
            this.directory_targetAssemblies._ShowFileSize = true;
            this.directory_targetAssemblies._ShowLinkToUpperFolder = true;
            this.directory_targetAssemblies._ViewMode = O2.Views.ASCX.CoreControls.ascx_Directory.ViewMode.Simple_With_LocationBar;
            this.directory_targetAssemblies._WatchFolder = true;
            this.directory_targetAssemblies.BackColor = System.Drawing.SystemColors.Control;
            this.directory_targetAssemblies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.directory_targetAssemblies.ForeColor = System.Drawing.Color.Black;
            this.directory_targetAssemblies.Location = new System.Drawing.Point(3, 16);
            this.directory_targetAssemblies.Name = "directory_targetAssemblies";
            this.directory_targetAssemblies.Size = new System.Drawing.Size(225, 217);
            this.directory_targetAssemblies.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox3);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.llClearFlowLayoutPanelWithResults);
            this.splitContainer3.Panel2.Controls.Add(this.flowLayoutPanelWithResults);
            this.splitContainer3.Panel2.Controls.Add(this.cbAutoExecuteUnitTestAfterLoad);
            this.splitContainer3.Panel2.Controls.Add(this.cbAddAssemblyOnAssemblyCompileEvent);
            this.splitContainer3.Panel2.Controls.Add(this.btExecuteSelected);
            this.splitContainer3.Panel2.Controls.Add(this.progressBar1);
            this.splitContainer3.Panel2.Controls.Add(this.llMapXRulesForUnitTests);
            this.splitContainer3.Panel2.Controls.Add(this.llLoadAssembliesToLookForUnitTests);
            this.splitContainer3.Panel2.Controls.Add(this.label1);
            this.splitContainer3.Size = new System.Drawing.Size(359, 478);
            this.splitContainer3.SplitterDistance = 238;
            this.splitContainer3.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tvXRules);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(359, 238);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "XRules";
            // 
            // tvXRules
            // 
            this.tvXRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvXRules.Location = new System.Drawing.Point(3, 16);
            this.tvXRules.Name = "tvXRules";
            this.tvXRules.Size = new System.Drawing.Size(353, 219);
            this.tvXRules.TabIndex = 2;
            this.tvXRules.DoubleClick += new System.EventHandler(this.tvXRules_DoubleClick);
            this.tvXRules.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvXRules_AfterSelect);
            // 
            // llClearFlowLayoutPanelWithResults
            // 
            this.llClearFlowLayoutPanelWithResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.llClearFlowLayoutPanelWithResults.AutoSize = true;
            this.llClearFlowLayoutPanelWithResults.Location = new System.Drawing.Point(259, 220);
            this.llClearFlowLayoutPanelWithResults.Name = "llClearFlowLayoutPanelWithResults";
            this.llClearFlowLayoutPanelWithResults.Size = new System.Drawing.Size(30, 13);
            this.llClearFlowLayoutPanelWithResults.TabIndex = 10;
            this.llClearFlowLayoutPanelWithResults.TabStop = true;
            this.llClearFlowLayoutPanelWithResults.Text = "clear";
            this.llClearFlowLayoutPanelWithResults.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llClearFlowLayoutPanelWithResults_LinkClicked);
            // 
            // flowLayoutPanelWithResults
            // 
            this.flowLayoutPanelWithResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelWithResults.Location = new System.Drawing.Point(295, 7);
            this.flowLayoutPanelWithResults.Name = "flowLayoutPanelWithResults";
            this.flowLayoutPanelWithResults.Size = new System.Drawing.Size(61, 226);
            this.flowLayoutPanelWithResults.TabIndex = 9;
            // 
            // cbAutoExecuteUnitTestAfterLoad
            // 
            this.cbAutoExecuteUnitTestAfterLoad.AutoSize = true;
            this.cbAutoExecuteUnitTestAfterLoad.Location = new System.Drawing.Point(20, 62);
            this.cbAutoExecuteUnitTestAfterLoad.Name = "cbAutoExecuteUnitTestAfterLoad";
            this.cbAutoExecuteUnitTestAfterLoad.Size = new System.Drawing.Size(183, 17);
            this.cbAutoExecuteUnitTestAfterLoad.TabIndex = 8;
            this.cbAutoExecuteUnitTestAfterLoad.Text = "Auto execute Unit tests after load";
            this.cbAutoExecuteUnitTestAfterLoad.UseVisualStyleBackColor = true;
            // 
            // cbAddAssemblyOnAssemblyCompileEvent
            // 
            this.cbAddAssemblyOnAssemblyCompileEvent.AutoSize = true;
            this.cbAddAssemblyOnAssemblyCompileEvent.Checked = true;
            this.cbAddAssemblyOnAssemblyCompileEvent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAddAssemblyOnAssemblyCompileEvent.Location = new System.Drawing.Point(20, 45);
            this.cbAddAssemblyOnAssemblyCompileEvent.Name = "cbAddAssemblyOnAssemblyCompileEvent";
            this.cbAddAssemblyOnAssemblyCompileEvent.Size = new System.Drawing.Size(225, 17);
            this.cbAddAssemblyOnAssemblyCompileEvent.TabIndex = 7;
            this.cbAddAssemblyOnAssemblyCompileEvent.Text = "Add Assembly on Assembly Compile Event";
            this.cbAddAssemblyOnAssemblyCompileEvent.UseVisualStyleBackColor = true;
            // 
            // btExecuteSelected
            // 
            this.btExecuteSelected.Location = new System.Drawing.Point(20, 85);
            this.btExecuteSelected.Name = "btExecuteSelected";
            this.btExecuteSelected.Size = new System.Drawing.Size(164, 23);
            this.btExecuteSelected.TabIndex = 5;
            this.btExecuteSelected.Text = "Execute Selected";
            this.btExecuteSelected.UseVisualStyleBackColor = true;
            this.btExecuteSelected.Click += new System.EventHandler(this.btExecuteSelected_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(20, 143);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(225, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // llMapXRulesForUnitTests
            // 
            this.llMapXRulesForUnitTests.AutoSize = true;
            this.llMapXRulesForUnitTests.Location = new System.Drawing.Point(17, 25);
            this.llMapXRulesForUnitTests.Name = "llMapXRulesForUnitTests";
            this.llMapXRulesForUnitTests.Size = new System.Drawing.Size(142, 13);
            this.llMapXRulesForUnitTests.TabIndex = 3;
            this.llMapXRulesForUnitTests.TabStop = true;
            this.llMapXRulesForUnitTests.Text = "Map XRules From Unit Tests";
            this.llMapXRulesForUnitTests.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llMapXRulesForUnitTests_LinkClicked);
            // 
            // llLoadAssembliesToLookForUnitTests
            // 
            this.llLoadAssembliesToLookForUnitTests.AutoSize = true;
            this.llLoadAssembliesToLookForUnitTests.Location = new System.Drawing.Point(17, 7);
            this.llLoadAssembliesToLookForUnitTests.Name = "llLoadAssembliesToLookForUnitTests";
            this.llLoadAssembliesToLookForUnitTests.Size = new System.Drawing.Size(180, 13);
            this.llLoadAssembliesToLookForUnitTests.TabIndex = 2;
            this.llLoadAssembliesToLookForUnitTests.TabStop = true;
            this.llLoadAssembliesToLookForUnitTests.Text = "LoadAssembliesToLookForUnitTests";
            this.llLoadAssembliesToLookForUnitTests.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llLoadAssembliesToLookForUnitTests_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 169);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "...";
            // 
            // ascx_XRules_UnitTestExecution_BigGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ascx_XRules_UnitTestExecution_BigGUI";
            this.Size = new System.Drawing.Size(602, 482);
            this.Load += new System.EventHandler(this.ascx_XRules_UnitTestExecution_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private O2.Views.ASCX.CoreControls.ascx_Directory directory_targetAssemblies;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TreeView tvAssembliesToLookForUnitTests;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TreeView tvXRules;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel llMapXRulesForUnitTests;
        private System.Windows.Forms.LinkLabel llLoadAssembliesToLookForUnitTests;
        private System.Windows.Forms.Button btExecuteSelected;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox cbAddAssemblyOnAssemblyCompileEvent;
        private System.Windows.Forms.LinkLabel llClearFlowLayoutPanelWithResults;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelWithResults;
        private System.Windows.Forms.CheckBox cbAutoExecuteUnitTestAfterLoad;
    }
}
