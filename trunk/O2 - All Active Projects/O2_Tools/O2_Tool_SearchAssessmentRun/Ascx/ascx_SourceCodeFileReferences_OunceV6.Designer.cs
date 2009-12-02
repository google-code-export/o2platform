using O2.Kernel.CodeUtils;
using O2.Views.ASCX.CoreControls;

namespace O2.Tool.SearchAssessmentRun.Ascx
{
    partial class ascx_SourceCodeFileReferences
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbLoadedAssessmentFile = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label7 = new System.Windows.Forms.Label();
            this.lbFilesNOTFoundOnThisComputer = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btAllDoneCloseModule = new System.Windows.Forms.Button();
            this.lbFilesFoundOnThisComputer = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.label5 = new System.Windows.Forms.Label();
            this.lbFix_PathToReplace = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbSelectedFile = new System.Windows.Forms.Label();
            this.lbFix_PathToFind = new System.Windows.Forms.Label();
            this.btFileInLocalDisk = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.btFixAllFilesWithMapping = new System.Windows.Forms.Button();
            this.lbMappedFile = new System.Windows.Forms.Label();
            this.btExistingMappingsSaveChanges = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.ascx_DropObject1 = new O2.Views.ASCX.CoreControls.ascx_DropObject();
            this.btSaveAssessmentFileWithFixes = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(386, 26);
            this.label2.TabIndex = 42;
            this.label2.Text = "Use this module to fix source code references (which are usually broken after loa" +
                "ding assesment files on different computers/VMs)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "Loaded Assessment file:";
            // 
            // lbLoadedAssessmentFile
            // 
            this.lbLoadedAssessmentFile.AutoSize = true;
            this.lbLoadedAssessmentFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLoadedAssessmentFile.Location = new System.Drawing.Point(127, 46);
            this.lbLoadedAssessmentFile.Name = "lbLoadedAssessmentFile";
            this.lbLoadedAssessmentFile.Size = new System.Drawing.Size(19, 13);
            this.lbLoadedAssessmentFile.TabIndex = 44;
            this.lbLoadedAssessmentFile.Text = "...";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(6, 62);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(741, 404);
            this.splitContainer1.SplitterDistance = 316;
            this.splitContainer1.TabIndex = 45;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label7);
            this.splitContainer2.Panel1.Controls.Add(this.lbFilesNOTFoundOnThisComputer);
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btAllDoneCloseModule);
            this.splitContainer2.Panel2.Controls.Add(this.lbFilesFoundOnThisComputer);
            this.splitContainer2.Panel2.Controls.Add(this.label4);
            this.splitContainer2.Size = new System.Drawing.Size(316, 404);
            this.splitContainer2.SplitterDistance = 215;
            this.splitContainer2.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 13);
            this.label7.TabIndex = 48;
            this.label7.Text = "1) Chose file to map";
            // 
            // lbFilesNOTFoundOnThisComputer
            // 
            this.lbFilesNOTFoundOnThisComputer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFilesNOTFoundOnThisComputer.FormattingEnabled = true;
            this.lbFilesNOTFoundOnThisComputer.HorizontalScrollbar = true;
            this.lbFilesNOTFoundOnThisComputer.Location = new System.Drawing.Point(6, 43);
            this.lbFilesNOTFoundOnThisComputer.Name = "lbFilesNOTFoundOnThisComputer";
            this.lbFilesNOTFoundOnThisComputer.Size = new System.Drawing.Size(302, 160);
            this.lbFilesNOTFoundOnThisComputer.TabIndex = 47;
            this.lbFilesNOTFoundOnThisComputer.SelectedIndexChanged += new System.EventHandler(this.lbFilesNOTFoundOnThisComputer_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(201, 13);
            this.label3.TabIndex = 46;
            this.label3.Text = "Files NOT found on this computer:";
            // 
            // btAllDoneCloseModule
            // 
            this.btAllDoneCloseModule.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btAllDoneCloseModule.Location = new System.Drawing.Point(25, 158);
            this.btAllDoneCloseModule.Name = "btAllDoneCloseModule";
            this.btAllDoneCloseModule.Size = new System.Drawing.Size(263, 20);
            this.btAllDoneCloseModule.TabIndex = 54;
            this.btAllDoneCloseModule.Text = "5) All Done - Close Module";
            this.btAllDoneCloseModule.UseVisualStyleBackColor = true;
            this.btAllDoneCloseModule.Visible = false;
            this.btAllDoneCloseModule.Click += new System.EventHandler(this.btAllDoneCloseModule_Click);
            // 
            // lbFilesFoundOnThisComputer
            // 
            this.lbFilesFoundOnThisComputer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFilesFoundOnThisComputer.FormattingEnabled = true;
            this.lbFilesFoundOnThisComputer.HorizontalScrollbar = true;
            this.lbFilesFoundOnThisComputer.Location = new System.Drawing.Point(5, 16);
            this.lbFilesFoundOnThisComputer.Name = "lbFilesFoundOnThisComputer";
            this.lbFilesFoundOnThisComputer.Size = new System.Drawing.Size(303, 134);
            this.lbFilesFoundOnThisComputer.TabIndex = 48;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkGreen;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(171, 13);
            this.label4.TabIndex = 47;
            this.label4.Text = "Files found on this computer:";
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.label5);
            this.splitContainer3.Panel1.Controls.Add(this.lbFix_PathToReplace);
            this.splitContainer3.Panel1.Controls.Add(this.label6);
            this.splitContainer3.Panel1.Controls.Add(this.label11);
            this.splitContainer3.Panel1.Controls.Add(this.lbSelectedFile);
            this.splitContainer3.Panel1.Controls.Add(this.lbFix_PathToFind);
            this.splitContainer3.Panel1.Controls.Add(this.btFileInLocalDisk);
            this.splitContainer3.Panel1.Controls.Add(this.label9);
            this.splitContainer3.Panel1.Controls.Add(this.btFixAllFilesWithMapping);
            this.splitContainer3.Panel1.Controls.Add(this.lbMappedFile);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.btExistingMappingsSaveChanges);
            this.splitContainer3.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer3.Panel2.Controls.Add(this.label8);
            this.splitContainer3.Size = new System.Drawing.Size(421, 404);
            this.splitContainer3.SplitterDistance = 215;
            this.splitContainer3.TabIndex = 54;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Selected File:";
            // 
            // lbFix_PathToReplace
            // 
            this.lbFix_PathToReplace.AutoSize = true;
            this.lbFix_PathToReplace.Location = new System.Drawing.Point(130, 147);
            this.lbFix_PathToReplace.Name = "lbFix_PathToReplace";
            this.lbFix_PathToReplace.Size = new System.Drawing.Size(16, 13);
            this.lbFix_PathToReplace.TabIndex = 53;
            this.lbFix_PathToReplace.Text = "...";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Maps to:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 147);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(121, 13);
            this.label11.TabIndex = 52;
            this.label11.Text = "Should be Replaced by:";
            // 
            // lbSelectedFile
            // 
            this.lbSelectedFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSelectedFile.Location = new System.Drawing.Point(81, 7);
            this.lbSelectedFile.Name = "lbSelectedFile";
            this.lbSelectedFile.Size = new System.Drawing.Size(334, 33);
            this.lbSelectedFile.TabIndex = 2;
            this.lbSelectedFile.Text = "...";
            // 
            // lbFix_PathToFind
            // 
            this.lbFix_PathToFind.AutoSize = true;
            this.lbFix_PathToFind.Location = new System.Drawing.Point(130, 124);
            this.lbFix_PathToFind.Name = "lbFix_PathToFind";
            this.lbFix_PathToFind.Size = new System.Drawing.Size(16, 13);
            this.lbFix_PathToFind.TabIndex = 51;
            this.lbFix_PathToFind.Text = "...";
            // 
            // btFileInLocalDisk
            // 
            this.btFileInLocalDisk.Location = new System.Drawing.Point(84, 43);
            this.btFileInLocalDisk.Name = "btFileInLocalDisk";
            this.btFileInLocalDisk.Size = new System.Drawing.Size(298, 23);
            this.btFileInLocalDisk.TabIndex = 47;
            this.btFileInLocalDisk.Text = "2) Find file in local disk";
            this.btFileInLocalDisk.UseVisualStyleBackColor = true;
            this.btFileInLocalDisk.Click += new System.EventHandler(this.btFileInLocalDisk_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(2, 124);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 13);
            this.label9.TabIndex = 50;
            this.label9.Text = "To fix this, the path:";
            // 
            // btFixAllFilesWithMapping
            // 
            this.btFixAllFilesWithMapping.Location = new System.Drawing.Point(84, 174);
            this.btFixAllFilesWithMapping.Name = "btFixAllFilesWithMapping";
            this.btFixAllFilesWithMapping.Size = new System.Drawing.Size(297, 24);
            this.btFixAllFilesWithMapping.TabIndex = 48;
            this.btFixAllFilesWithMapping.Text = "3) Fix all files with mapping";
            this.btFixAllFilesWithMapping.UseVisualStyleBackColor = true;
            this.btFixAllFilesWithMapping.Click += new System.EventHandler(this.btFixAllFilesWithMapping_Click);
            // 
            // lbMappedFile
            // 
            this.lbMappedFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbMappedFile.Location = new System.Drawing.Point(81, 85);
            this.lbMappedFile.Name = "lbMappedFile";
            this.lbMappedFile.Size = new System.Drawing.Size(334, 39);
            this.lbMappedFile.TabIndex = 49;
            this.lbMappedFile.Text = "...";
            // 
            // btExistingMappingsSaveChanges
            // 
            this.btExistingMappingsSaveChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btExistingMappingsSaveChanges.Location = new System.Drawing.Point(289, 158);
            this.btExistingMappingsSaveChanges.Name = "btExistingMappingsSaveChanges";
            this.btExistingMappingsSaveChanges.Size = new System.Drawing.Size(125, 20);
            this.btExistingMappingsSaveChanges.TabIndex = 55;
            this.btExistingMappingsSaveChanges.Text = "4) Save Changes";
            this.btExistingMappingsSaveChanges.UseVisualStyleBackColor = true;
            this.btExistingMappingsSaveChanges.Visible = false;
            this.btExistingMappingsSaveChanges.Click += new System.EventHandler(this.btExistingMappingsSaveChanges_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(4, 18);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(411, 136);
            this.dataGridView1.TabIndex = 55;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 13);
            this.label8.TabIndex = 54;
            this.label8.Text = "Existing Mappings";
            // 
            // ascx_DropObject1
            // 
            this.ascx_DropObject1.AllowDrop = true;
            this.ascx_DropObject1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ascx_DropObject1.BackColor = System.Drawing.Color.Maroon;
            this.ascx_DropObject1.ForeColor = System.Drawing.Color.White;
            this.ascx_DropObject1.Location = new System.Drawing.Point(424, 3);
            this.ascx_DropObject1.Name = "ascx_DropObject1";
            this.ascx_DropObject1.Size = new System.Drawing.Size(319, 21);
            this.ascx_DropObject1.TabIndex = 47;
            this.ascx_DropObject1.Text = "Drop Content Here!!";
            this.ascx_DropObject1.Visible = false;
            this.ascx_DropObject1.eDnDAction_ObjectDataReceived_Event += new O2.Kernel.CodeUtils.Callbacks.dMethod_Object(this.ascx_DropObject1_eDnDAction_ObjectDataReceived_Event);
            // 
            // btSaveAssessmentFileWithFixes
            // 
            this.btSaveAssessmentFileWithFixes.Location = new System.Drawing.Point(525, 30);
            this.btSaveAssessmentFileWithFixes.Name = "btSaveAssessmentFileWithFixes";
            this.btSaveAssessmentFileWithFixes.Size = new System.Drawing.Size(164, 23);
            this.btSaveAssessmentFileWithFixes.TabIndex = 54;
            this.btSaveAssessmentFileWithFixes.Text = "Save assessment file with fixes";
            this.btSaveAssessmentFileWithFixes.UseVisualStyleBackColor = true;
            this.btSaveAssessmentFileWithFixes.Visible = false;
            this.btSaveAssessmentFileWithFixes.Click += new System.EventHandler(this.btSaveAssessmentFileWithFixes_Click);
            // 
            // ascx_SourceCodeFileReferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btSaveAssessmentFileWithFixes);
            this.Controls.Add(this.ascx_DropObject1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.lbLoadedAssessmentFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "ascx_SourceCodeFileReferences";
            this.Size = new System.Drawing.Size(750, 469);
            this.Load += new System.EventHandler(this.ascx_SourceCodeFileReferences_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbLoadedAssessmentFile;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListBox lbFilesNOTFoundOnThisComputer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lbFilesFoundOnThisComputer;
        private System.Windows.Forms.Label label4;
        private ascx_DropObject ascx_DropObject1;
        private System.Windows.Forms.Button btFileInLocalDisk;
        private System.Windows.Forms.Label lbSelectedFile;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbFix_PathToReplace;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbFix_PathToFind;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbMappedFile;
        private System.Windows.Forms.Button btFixAllFilesWithMapping;
        private System.Windows.Forms.Button btSaveAssessmentFileWithFixes;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btAllDoneCloseModule;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btExistingMappingsSaveChanges;
    }
}