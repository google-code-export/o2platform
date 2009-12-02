using O2.Kernel.CodeUtils;
using O2.Legacy.OunceV6.TraceViewer;
using O2.Views.ASCX.CoreControls;

namespace O2.Rnd.JavaVelocityAnalyzer.ascx
{
    partial class ascx_JavaVelocityAnalyzer
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
            this.btCreateFindingsFromVMFiles = new System.Windows.Forms.Button();
            this.cbDoubleClickToRemoveFile = new System.Windows.Forms.CheckBox();
            this.btLoadTestData = new System.Windows.Forms.Button();
            this.ascx_DropObject1 = new ascx_DropObject();
            this.lbLoadedFiles = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label7 = new System.Windows.Forms.Label();
            this.tbOriginalFile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tvProcessedVelocityFile = new System.Windows.Forms.TreeView();
            this.lbCallsToOtherVmFiles = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lbDirectives = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lbVars = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbIgnoreComments = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbMethods = new System.Windows.Forms.ListBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.lbCompleteListOfVars = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lbCompleteListOfMethods = new System.Windows.Forms.ListBox();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            //this.ascx_ViewAssessmentRun1 = new ascx_ViewAssessmentRun();
            this.ascx_TraceViewer1 = new ascx_TraceViewer();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.btCreateFindingsFromVMFiles);
            this.splitContainer1.Panel1.Controls.Add(this.cbDoubleClickToRemoveFile);
            this.splitContainer1.Panel1.Controls.Add(this.btLoadTestData);
            this.splitContainer1.Panel1.Controls.Add(this.ascx_DropObject1);
            this.splitContainer1.Panel1.Controls.Add(this.lbLoadedFiles);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(964, 253);
            this.splitContainer1.SplitterDistance = 320;
            this.splitContainer1.TabIndex = 0;
            // 
            // btCreateFindingsFromVMFiles
            // 
            this.btCreateFindingsFromVMFiles.Location = new System.Drawing.Point(4, 204);
            this.btCreateFindingsFromVMFiles.Name = "btCreateFindingsFromVMFiles";
            this.btCreateFindingsFromVMFiles.Size = new System.Drawing.Size(207, 23);
            this.btCreateFindingsFromVMFiles.TabIndex = 28;
            this.btCreateFindingsFromVMFiles.Text = "Create Findings from VM files";
            this.btCreateFindingsFromVMFiles.UseVisualStyleBackColor = true;
            this.btCreateFindingsFromVMFiles.Click += new System.EventHandler(this.btCreateFindingsFromVMFiles_Click);
            // 
            // cbDoubleClickToRemoveFile
            // 
            this.cbDoubleClickToRemoveFile.AutoSize = true;
            this.cbDoubleClickToRemoveFile.Location = new System.Drawing.Point(4, 410);
            this.cbDoubleClickToRemoveFile.Name = "cbDoubleClickToRemoveFile";
            this.cbDoubleClickToRemoveFile.Size = new System.Drawing.Size(151, 17);
            this.cbDoubleClickToRemoveFile.TabIndex = 27;
            this.cbDoubleClickToRemoveFile.Text = "Double-click to remove file";
            this.cbDoubleClickToRemoveFile.UseVisualStyleBackColor = true;
            // 
            // btLoadTestData
            // 
            this.btLoadTestData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                                                                               | System.Windows.Forms.AnchorStyles.Right)));
            this.btLoadTestData.Location = new System.Drawing.Point(3, 141);
            this.btLoadTestData.Name = "btLoadTestData";
            this.btLoadTestData.Size = new System.Drawing.Size(310, 43);
            this.btLoadTestData.TabIndex = 26;
            this.btLoadTestData.Text = "Load Test Data";
            this.btLoadTestData.UseVisualStyleBackColor = true;
            this.btLoadTestData.Click += new System.EventHandler(this.btLoadTestData_Click);
            // 
            // ascx_DropObject1
            // 
            this.ascx_DropObject1.AllowDrop = true;
            this.ascx_DropObject1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ascx_DropObject1.BackColor = System.Drawing.Color.Maroon;
            this.ascx_DropObject1.ForeColor = System.Drawing.Color.White;
            this.ascx_DropObject1.Location = new System.Drawing.Point(211, 3);
            this.ascx_DropObject1.Name = "ascx_DropObject1";
            this.ascx_DropObject1.Size = new System.Drawing.Size(102, 21);
            this.ascx_DropObject1.TabIndex = 25;
            this.ascx_DropObject1.eDnDAction_ObjectDataReceived_Event += new Callbacks.dMethod_Object(this.ascx_DropObject1_eDnDAction_ObjectDataReceived_Event);
            // 
            // lbLoadedFiles
            // 
            this.lbLoadedFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                               | System.Windows.Forms.AnchorStyles.Left)
                                                                              | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLoadedFiles.FormattingEnabled = true;
            this.lbLoadedFiles.Location = new System.Drawing.Point(1, 26);
            this.lbLoadedFiles.Name = "lbLoadedFiles";
            this.lbLoadedFiles.Size = new System.Drawing.Size(312, 82);
            this.lbLoadedFiles.TabIndex = 24;
            this.lbLoadedFiles.SelectedIndexChanged += new System.EventHandler(this.lbLoadedFiles_SelectedIndexChanged);
            this.lbLoadedFiles.DoubleClick += new System.EventHandler(this.lbLoadedFiles_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-2, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Loaded Velocity Files:";
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
            this.splitContainer2.Panel1.Controls.Add(this.label7);
            this.splitContainer2.Panel1.Controls.Add(this.tbOriginalFile);
            this.splitContainer2.Panel1.Controls.Add(this.label4);
            this.splitContainer2.Panel1.Controls.Add(this.tvProcessedVelocityFile);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lbCallsToOtherVmFiles);
            this.splitContainer2.Panel2.Controls.Add(this.label6);
            this.splitContainer2.Panel2.Controls.Add(this.lbDirectives);
            this.splitContainer2.Panel2.Controls.Add(this.label5);
            this.splitContainer2.Panel2.Controls.Add(this.lbVars);
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Panel2.Controls.Add(this.cbIgnoreComments);
            this.splitContainer2.Panel2.Controls.Add(this.label1);
            this.splitContainer2.Panel2.Controls.Add(this.lbMethods);
            this.splitContainer2.Size = new System.Drawing.Size(640, 253);
            this.splitContainer2.SplitterDistance = 423;
            this.splitContainer2.TabIndex = 30;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "Tree View";
            // 
            // tbOriginalFile
            // 
            this.tbOriginalFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                                                                               | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOriginalFile.Location = new System.Drawing.Point(3, 59);
            this.tbOriginalFile.Multiline = true;
            this.tbOriginalFile.Name = "tbOriginalFile";
            this.tbOriginalFile.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbOriginalFile.Size = new System.Drawing.Size(413, 185);
            this.tbOriginalFile.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Original File";
            // 
            // tvProcessedVelocityFile
            // 
            this.tvProcessedVelocityFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                         | System.Windows.Forms.AnchorStyles.Left)
                                                                                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvProcessedVelocityFile.HideSelection = false;
            this.tvProcessedVelocityFile.Location = new System.Drawing.Point(3, 24);
            this.tvProcessedVelocityFile.Name = "tvProcessedVelocityFile";
            this.tvProcessedVelocityFile.Size = new System.Drawing.Size(413, 13);
            this.tvProcessedVelocityFile.TabIndex = 0;
            // 
            // lbCallsToOtherVmFiles
            // 
            this.lbCallsToOtherVmFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                                      | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCallsToOtherVmFiles.FormattingEnabled = true;
            this.lbCallsToOtherVmFiles.Location = new System.Drawing.Point(3, 144);
            this.lbCallsToOtherVmFiles.Name = "lbCallsToOtherVmFiles";
            this.lbCallsToOtherVmFiles.Size = new System.Drawing.Size(203, 95);
            this.lbCallsToOtherVmFiles.TabIndex = 33;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 128);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 13);
            this.label6.TabIndex = 32;
            this.label6.Text = "Calls to other VM files";
            // 
            // lbDirectives
            // 
            this.lbDirectives.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                             | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDirectives.FormattingEnabled = true;
            this.lbDirectives.Location = new System.Drawing.Point(3, 40);
            this.lbDirectives.Name = "lbDirectives";
            this.lbDirectives.Size = new System.Drawing.Size(203, 82);
            this.lbDirectives.TabIndex = 31;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "Directive";
            // 
            // lbVars
            // 
            this.lbVars.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                       | System.Windows.Forms.AnchorStyles.Right)));
            this.lbVars.FormattingEnabled = true;
            this.lbVars.Location = new System.Drawing.Point(3, 258);
            this.lbVars.Name = "lbVars";
            this.lbVars.Size = new System.Drawing.Size(203, 121);
            this.lbVars.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 382);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Methods";
            // 
            // cbIgnoreComments
            // 
            this.cbIgnoreComments.AutoSize = true;
            this.cbIgnoreComments.Location = new System.Drawing.Point(9, 5);
            this.cbIgnoreComments.Name = "cbIgnoreComments";
            this.cbIgnoreComments.Size = new System.Drawing.Size(108, 17);
            this.cbIgnoreComments.TabIndex = 1;
            this.cbIgnoreComments.Text = "Ignore Comments";
            this.cbIgnoreComments.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 242);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Vars";
            // 
            // lbMethods
            // 
            this.lbMethods.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                           | System.Windows.Forms.AnchorStyles.Left)
                                                                          | System.Windows.Forms.AnchorStyles.Right)));
            this.lbMethods.FormattingEnabled = true;
            this.lbMethods.Location = new System.Drawing.Point(3, 398);
            this.lbMethods.Name = "lbMethods";
            this.lbMethods.Size = new System.Drawing.Size(203, 4);
            this.lbMethods.TabIndex = 28;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(964, 541);
            this.splitContainer3.SplitterDistance = 253;
            this.splitContainer3.TabIndex = 1;
            // 
            // splitContainer4
            // 
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.splitContainer5);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.splitContainer6);
            this.splitContainer4.Size = new System.Drawing.Size(964, 284);
            this.splitContainer4.SplitterDistance = 321;
            this.splitContainer4.TabIndex = 0;
            // 
            // splitContainer5
            // 
            this.splitContainer5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.lbCompleteListOfVars);
            this.splitContainer5.Panel1.Controls.Add(this.label8);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.label9);
            this.splitContainer5.Panel2.Controls.Add(this.lbCompleteListOfMethods);
            this.splitContainer5.Size = new System.Drawing.Size(321, 284);
            this.splitContainer5.SplitterDistance = 184;
            this.splitContainer5.TabIndex = 30;
            // 
            // lbCompleteListOfVars
            // 
            this.lbCompleteListOfVars.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                      | System.Windows.Forms.AnchorStyles.Left)
                                                                                     | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCompleteListOfVars.FormattingEnabled = true;
            this.lbCompleteListOfVars.Location = new System.Drawing.Point(2, 20);
            this.lbCompleteListOfVars.Name = "lbCompleteListOfVars";
            this.lbCompleteListOfVars.Size = new System.Drawing.Size(313, 160);
            this.lbCompleteListOfVars.Sorted = true;
            this.lbCompleteListOfVars.TabIndex = 29;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1, 2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Complete List of Vars";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(-1, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(126, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "Complete List of Methods";
            // 
            // lbCompleteListOfMethods
            // 
            this.lbCompleteListOfMethods.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                         | System.Windows.Forms.AnchorStyles.Left)
                                                                                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCompleteListOfMethods.FormattingEnabled = true;
            this.lbCompleteListOfMethods.Location = new System.Drawing.Point(1, 20);
            this.lbCompleteListOfMethods.Name = "lbCompleteListOfMethods";
            this.lbCompleteListOfMethods.Size = new System.Drawing.Size(315, 69);
            this.lbCompleteListOfMethods.TabIndex = 30;
            // 
            // splitContainer6
            // 
            this.splitContainer6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer6.Location = new System.Drawing.Point(0, 0);
            this.splitContainer6.Name = "splitContainer6";
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.ascx_TraceViewer1);
            // 
            // splitContainer6.Panel2
            // 
         /*   this.splitContainer6.Panel2.Controls.Add(this.ascx_ViewAssessmentRun1);
            this.splitContainer6.Size = new System.Drawing.Size(639, 284);
            this.splitContainer6.SplitterDistance = 186;
            this.splitContainer6.TabIndex = 1;
            // 
            // ascx_ViewAssessmentRun1
            // 
            this.ascx_ViewAssessmentRun1.BackColor = System.Drawing.SystemColors.Control;
            this.ascx_ViewAssessmentRun1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ascx_ViewAssessmentRun1.ForeColor = System.Drawing.Color.Black;
            this.ascx_ViewAssessmentRun1.Location = new System.Drawing.Point(0, 0);
            this.ascx_ViewAssessmentRun1.Name = "ascx_ViewAssessmentRun1";
            this.ascx_ViewAssessmentRun1.Size = new System.Drawing.Size(445, 280);
            this.ascx_ViewAssessmentRun1.TabIndex = 0;*/
            // 
            // ascx_TraceViewer1
            // 
            this.ascx_TraceViewer1.BackColor = System.Drawing.SystemColors.Control;
            this.ascx_TraceViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ascx_TraceViewer1.ForeColor = System.Drawing.Color.Black;
            this.ascx_TraceViewer1.Location = new System.Drawing.Point(0, 0);
            this.ascx_TraceViewer1.Name = "ascx_TraceViewer1";
            this.ascx_TraceViewer1.Size = new System.Drawing.Size(182, 280);
            this.ascx_TraceViewer1.TabIndex = 14;
            // 
            // ascx_JavaVelocityAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer3);
            this.Name = "ascx_JavaVelocityAnalyzer";
            this.Size = new System.Drawing.Size(964, 541);
            this.Load += new System.EventHandler(this.ascx_JavaVelocityAnalyzer_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel1.PerformLayout();
            this.splitContainer5.Panel2.ResumeLayout(false);
            this.splitContainer5.Panel2.PerformLayout();
            this.splitContainer5.ResumeLayout(false);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel2.ResumeLayout(false);
            this.splitContainer6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lbLoadedFiles;
        private System.Windows.Forms.Label label3;
        private ascx_DropObject ascx_DropObject1;
        private System.Windows.Forms.Button btLoadTestData;
        private System.Windows.Forms.TreeView tvProcessedVelocityFile;
        private System.Windows.Forms.CheckBox cbIgnoreComments;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbMethods;
        private System.Windows.Forms.ListBox lbVars;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox tbOriginalFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox lbCallsToOtherVmFiles;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox lbDirectives;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbDoubleClickToRemoveFile;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.ListBox lbCompleteListOfVars;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ListBox lbCompleteListOfMethods;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.SplitContainer splitContainer6;
        //private ascx_ViewAssessmentRun ascx_ViewAssessmentRun1;
        private System.Windows.Forms.Button btCreateFindingsFromVMFiles;
        private ascx_TraceViewer ascx_TraceViewer1;
    }
}