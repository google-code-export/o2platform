// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
namespace O2.Cmd.SpringMvc.Ascx
{
    partial class ascx_CreateSpringMvcMappings
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
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.cbOnDropProcessJarFiles = new System.Windows.Forms.CheckBox();
            this.tbFolderToSaveMappedMvcControllers = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dropObject = new O2.Views.ASCX.CoreControls.ascx_DropObject();
            this.btSaveMappedControllers = new System.Windows.Forms.Button();
            this.lbLoadedCirDataFile = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpViewSourceCodeMappings = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cirFunctionDetails = new O2.Core.CIR.Ascx.ascx_FunctionCalls();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbCreateFindingForUsesOfGetParameter = new System.Windows.Forms.CheckBox();
            this.cbCreateFindingForUsesOfModelAttribute = new System.Windows.Forms.CheckBox();
            this.btCreateFindingsFromSpringMvcMappings = new System.Windows.Forms.Button();
            this.findingsViewer = new O2.Views.ASCX.O2Findings.ascx_FindingsViewer();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.findingsViewerFor_SelectedController = new O2.Views.ASCX.O2Findings.ascx_FindingsViewer();
            this.sourceCodeView = new O2.External.SharpDevelop.Ascx.ascx_SourceCodeEditor();
            this.cbDeleteTempFiles = new System.Windows.Forms.CheckBox();
            this.springMvcMappings = new O2.Cmd.SpringMvc.Ascx.ascx_SpringMvcMappings();
            this.springMvcAutoBindClassesView = new O2.Cmd.SpringMvc.Ascx.ascx_SpringMvcAutoBindClassesView();
            this.tabControl2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpViewSourceCodeMappings.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Location = new System.Drawing.Point(8, 7);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(778, 107);
            this.tabControl2.TabIndex = 13;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.cbDeleteTempFiles);
            this.tabPage4.Controls.Add(this.cbOnDropProcessJarFiles);
            this.tabPage4.Controls.Add(this.tbFolderToSaveMappedMvcControllers);
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Controls.Add(this.dropObject);
            this.tabPage4.Controls.Add(this.btSaveMappedControllers);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(770, 81);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Drop zip file with data files here";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // cbOnDropProcessJarFiles
            // 
            this.cbOnDropProcessJarFiles.AutoSize = true;
            this.cbOnDropProcessJarFiles.Location = new System.Drawing.Point(314, 47);
            this.cbOnDropProcessJarFiles.Name = "cbOnDropProcessJarFiles";
            this.cbOnDropProcessJarFiles.Size = new System.Drawing.Size(151, 17);
            this.cbOnDropProcessJarFiles.TabIndex = 26;
            this.cbOnDropProcessJarFiles.Text = "On Drop, Process Jar Files";
            this.cbOnDropProcessJarFiles.UseVisualStyleBackColor = true;
            // 
            // tbFolderToSaveMappedMvcControllers
            // 
            this.tbFolderToSaveMappedMvcControllers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFolderToSaveMappedMvcControllers.Location = new System.Drawing.Point(492, 35);
            this.tbFolderToSaveMappedMvcControllers.Name = "tbFolderToSaveMappedMvcControllers";
            this.tbFolderToSaveMappedMvcControllers.Size = new System.Drawing.Size(272, 20);
            this.tbFolderToSaveMappedMvcControllers.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(313, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(173, 50);
            this.label7.TabIndex = 16;
            this.label7.Text = "If you have a zip file with both CirData file and Xml Fies with Java metadata:";
            // 
            // dropObject
            // 
            this.dropObject.AllowDrop = true;
            this.dropObject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dropObject.BackColor = System.Drawing.Color.Maroon;
            this.dropObject.ForeColor = System.Drawing.Color.White;
            this.dropObject.Location = new System.Drawing.Point(6, 7);
            this.dropObject.Name = "dropObject";
            this.dropObject.Size = new System.Drawing.Size(302, 68);
            this.dropObject.TabIndex = 0;
            this.dropObject.Text = "Drop folders or *.war (with *.class files);         zip or Jar files ;       \'Dat" +
                "a files for Spring Mvc O2 Module\' here";
            this.dropObject.eDnDAction_ObjectDataReceived_Event += new O2.Kernel.CodeUtils.Callbacks.dMethod_Object(this.dropObject_eDnDAction_ObjectDataReceived_Event);
            // 
            // btSaveMappedControllers
            // 
            this.btSaveMappedControllers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSaveMappedControllers.Location = new System.Drawing.Point(492, 6);
            this.btSaveMappedControllers.Name = "btSaveMappedControllers";
            this.btSaveMappedControllers.Size = new System.Drawing.Size(272, 23);
            this.btSaveMappedControllers.TabIndex = 14;
            this.btSaveMappedControllers.Text = "Save Mapped Spring Mvc controllers to folder:";
            this.btSaveMappedControllers.UseVisualStyleBackColor = true;
            this.btSaveMappedControllers.Click += new System.EventHandler(this.btSaveMappedControllers_Click);
            // 
            // lbLoadedCirDataFile
            // 
            this.lbLoadedCirDataFile.AutoSize = true;
            this.lbLoadedCirDataFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLoadedCirDataFile.Location = new System.Drawing.Point(118, 121);
            this.lbLoadedCirDataFile.Name = "lbLoadedCirDataFile";
            this.lbLoadedCirDataFile.Size = new System.Drawing.Size(19, 13);
            this.lbLoadedCirDataFile.TabIndex = 24;
            this.lbLoadedCirDataFile.Text = "...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Loaded CirData File:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(0, 159);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.springMvcMappings);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(905, 296);
            this.splitContainer1.SplitterDistance = 300;
            this.splitContainer1.TabIndex = 11;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpViewSourceCodeMappings);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(597, 292);
            this.tabControl1.TabIndex = 1;
            // 
            // tpViewSourceCodeMappings
            // 
            this.tpViewSourceCodeMappings.Controls.Add(this.sourceCodeView);
            this.tpViewSourceCodeMappings.Location = new System.Drawing.Point(4, 22);
            this.tpViewSourceCodeMappings.Name = "tpViewSourceCodeMappings";
            this.tpViewSourceCodeMappings.Padding = new System.Windows.Forms.Padding(3);
            this.tpViewSourceCodeMappings.Size = new System.Drawing.Size(589, 266);
            this.tpViewSourceCodeMappings.TabIndex = 4;
            this.tpViewSourceCodeMappings.Text = "View Source Code mappings";
            this.tpViewSourceCodeMappings.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cirFunctionDetails);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(589, 266);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Function & Class Details";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cirFunctionDetails
            // 
            this.cirFunctionDetails.currentCirClass = null;
            this.cirFunctionDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cirFunctionDetails.Location = new System.Drawing.Point(3, 3);
            this.cirFunctionDetails.Name = "cirFunctionDetails";
            this.cirFunctionDetails.rootCirFunction = null;
            this.cirFunctionDetails.Size = new System.Drawing.Size(583, 260);
            this.cirFunctionDetails.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.springMvcAutoBindClassesView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(589, 266);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Spring MVC Bindable fields for selected class";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.btCreateFindingsFromSpringMvcMappings);
            this.tabPage3.Controls.Add(this.findingsViewer);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(589, 266);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Create Findings From Spring Mvc mappings";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbCreateFindingForUsesOfGetParameter);
            this.groupBox1.Controls.Add(this.cbCreateFindingForUsesOfModelAttribute);
            this.groupBox1.Location = new System.Drawing.Point(158, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(443, 70);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Findings creation rules";
            // 
            // cbCreateFindingForUsesOfGetParameter
            // 
            this.cbCreateFindingForUsesOfGetParameter.AutoSize = true;
            this.cbCreateFindingForUsesOfGetParameter.Checked = true;
            this.cbCreateFindingForUsesOfGetParameter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCreateFindingForUsesOfGetParameter.Location = new System.Drawing.Point(6, 45);
            this.cbCreateFindingForUsesOfGetParameter.Name = "cbCreateFindingForUsesOfGetParameter";
            this.cbCreateFindingForUsesOfGetParameter.Size = new System.Drawing.Size(214, 17);
            this.cbCreateFindingForUsesOfGetParameter.TabIndex = 2;
            this.cbCreateFindingForUsesOfGetParameter.Text = "Create Finding for uses of GetParameter";
            this.cbCreateFindingForUsesOfGetParameter.UseVisualStyleBackColor = true;
            // 
            // cbCreateFindingForUsesOfModelAttribute
            // 
            this.cbCreateFindingForUsesOfModelAttribute.AutoSize = true;
            this.cbCreateFindingForUsesOfModelAttribute.Checked = true;
            this.cbCreateFindingForUsesOfModelAttribute.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCreateFindingForUsesOfModelAttribute.Location = new System.Drawing.Point(6, 21);
            this.cbCreateFindingForUsesOfModelAttribute.Name = "cbCreateFindingForUsesOfModelAttribute";
            this.cbCreateFindingForUsesOfModelAttribute.Size = new System.Drawing.Size(232, 17);
            this.cbCreateFindingForUsesOfModelAttribute.TabIndex = 1;
            this.cbCreateFindingForUsesOfModelAttribute.Text = "Create Findings For Uses Of Model Attribute";
            this.cbCreateFindingForUsesOfModelAttribute.UseVisualStyleBackColor = true;
            // 
            // btCreateFindingsFromSpringMvcMappings
            // 
            this.btCreateFindingsFromSpringMvcMappings.Location = new System.Drawing.Point(6, 7);
            this.btCreateFindingsFromSpringMvcMappings.Name = "btCreateFindingsFromSpringMvcMappings";
            this.btCreateFindingsFromSpringMvcMappings.Size = new System.Drawing.Size(135, 70);
            this.btCreateFindingsFromSpringMvcMappings.TabIndex = 0;
            this.btCreateFindingsFromSpringMvcMappings.Text = "Create Findings";
            this.btCreateFindingsFromSpringMvcMappings.UseVisualStyleBackColor = true;
            this.btCreateFindingsFromSpringMvcMappings.Click += new System.EventHandler(this.btCreateFimdingsFromSpringMvcMappings_Click);
            // 
            // findingsViewer
            // 
            this.findingsViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.findingsViewer.Location = new System.Drawing.Point(0, 83);
            this.findingsViewer.Name = "findingsViewer";
            this.findingsViewer.Size = new System.Drawing.Size(604, 230);
            this.findingsViewer.TabIndex = 2;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.findingsViewerFor_SelectedController);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(589, 266);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "View Findings that match Controller";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // findingsViewerFor_SelectedController
            // 
            this.findingsViewerFor_SelectedController.Dock = System.Windows.Forms.DockStyle.Fill;
            this.findingsViewerFor_SelectedController.Location = new System.Drawing.Point(3, 3);
            this.findingsViewerFor_SelectedController.Name = "findingsViewerFor_SelectedController";
            this.findingsViewerFor_SelectedController.Size = new System.Drawing.Size(583, 260);
            this.findingsViewerFor_SelectedController.TabIndex = 0;
            // 
            // sourceCodeView
            // 
            this.sourceCodeView.AllowDrop = true;
            this.sourceCodeView.BackColor = System.Drawing.SystemColors.Control;
            this.sourceCodeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sourceCodeView.ForeColor = System.Drawing.Color.Black;
            this.sourceCodeView.Location = new System.Drawing.Point(3, 3);
            this.sourceCodeView.Name = "sourceCodeView";
            this.sourceCodeView.Size = new System.Drawing.Size(583, 260);
            this.sourceCodeView.TabIndex = 0;
            // 
            // cbDeleteTempFiles
            // 
            this.cbDeleteTempFiles.AutoSize = true;
            this.cbDeleteTempFiles.Checked = true;
            this.cbDeleteTempFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDeleteTempFiles.Location = new System.Drawing.Point(314, 62);
            this.cbDeleteTempFiles.Name = "cbDeleteTempFiles";
            this.cbDeleteTempFiles.Size = new System.Drawing.Size(111, 17);
            this.cbDeleteTempFiles.TabIndex = 27;
            this.cbDeleteTempFiles.Text = "Delete Temp Files";
            this.cbDeleteTempFiles.UseVisualStyleBackColor = true;
            // 
            // springMvcMappings
            // 
            this.springMvcMappings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.springMvcMappings.Location = new System.Drawing.Point(0, 0);
            this.springMvcMappings.Name = "springMvcMappings";
            this.springMvcMappings.Size = new System.Drawing.Size(296, 292);
            this.springMvcMappings.TabIndex = 0;
            // 
            // springMvcAutoBindClassesView
            // 
            this.springMvcAutoBindClassesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.springMvcAutoBindClassesView.Location = new System.Drawing.Point(3, 3);
            this.springMvcAutoBindClassesView.Name = "springMvcAutoBindClassesView";
            this.springMvcAutoBindClassesView.Size = new System.Drawing.Size(583, 260);
            this.springMvcAutoBindClassesView.TabIndex = 0;
            // 
            // ascx_CreateSpringMvcMappings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbLoadedCirDataFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ascx_CreateSpringMvcMappings";
            this.Size = new System.Drawing.Size(912, 458);
            this.Load += new System.EventHandler(this.ascx_ViewSpringMvcControler_Load);
            this.tabControl2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpViewSourceCodeMappings.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private O2.Core.CIR.Ascx.ascx_FunctionCalls cirFunctionDetails;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private ascx_SpringMvcAutoBindClassesView springMvcAutoBindClassesView;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox cbCreateFindingForUsesOfModelAttribute;
        private System.Windows.Forms.Button btCreateFindingsFromSpringMvcMappings;
        private System.Windows.Forms.GroupBox groupBox1;
        private O2.Views.ASCX.O2Findings.ascx_FindingsViewer findingsViewer;
        private System.Windows.Forms.CheckBox cbCreateFindingForUsesOfGetParameter;
        private System.Windows.Forms.TabPage tpViewSourceCodeMappings;
        private O2.External.SharpDevelop.Ascx.ascx_SourceCodeEditor sourceCodeView;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage4;
        private O2.Views.ASCX.CoreControls.ascx_DropObject dropObject;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btSaveMappedControllers;
        private System.Windows.Forms.Label lbLoadedCirDataFile;
        private System.Windows.Forms.Label label3;
        private ascx_SpringMvcMappings springMvcMappings;
        private System.Windows.Forms.TextBox tbFolderToSaveMappedMvcControllers;
        private System.Windows.Forms.TabPage tabPage6;
        private O2.Views.ASCX.O2Findings.ascx_FindingsViewer findingsViewerFor_SelectedController;
        private System.Windows.Forms.CheckBox cbOnDropProcessJarFiles;
        private System.Windows.Forms.CheckBox cbDeleteTempFiles;
    }
}
