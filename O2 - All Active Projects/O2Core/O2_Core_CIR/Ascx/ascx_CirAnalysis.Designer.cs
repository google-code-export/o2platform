// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Views.ASCX.CoreControls;

namespace O2.Core.CIR.Ascx
{
    partial class ascx_CirAnalysis
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
            this.lbLoadedO2CirDataFiles = new System.Windows.Forms.ListBox();
            this.cbClearPreviousO2CirData = new System.Windows.Forms.CheckBox();
            this.ascx_DropObject1 = new O2.Views.ASCX.CoreControls.ascx_DropObject();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.llClearLoadedData = new System.Windows.Forms.LinkLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.laNumberOfMethods = new System.Windows.Forms.Label();
            this.laNumberOfClasses = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbLoadedO2CirDataFiles
            // 
            this.lbLoadedO2CirDataFiles.AllowDrop = true;
            this.lbLoadedO2CirDataFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLoadedO2CirDataFiles.FormattingEnabled = true;
            this.lbLoadedO2CirDataFiles.Location = new System.Drawing.Point(275, 60);
            this.lbLoadedO2CirDataFiles.Name = "lbLoadedO2CirDataFiles";
            this.lbLoadedO2CirDataFiles.Size = new System.Drawing.Size(324, 69);
            this.lbLoadedO2CirDataFiles.TabIndex = 108;
            this.lbLoadedO2CirDataFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbLoadedO2CirDataFiles_DragDrop);
            this.lbLoadedO2CirDataFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbLoadedO2CirDataFiles_DragEnter);
            // 
            // cbClearPreviousO2CirData
            // 
            this.cbClearPreviousO2CirData.AutoSize = true;
            this.cbClearPreviousO2CirData.Checked = true;
            this.cbClearPreviousO2CirData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbClearPreviousO2CirData.Location = new System.Drawing.Point(345, 21);
            this.cbClearPreviousO2CirData.Name = "cbClearPreviousO2CirData";
            this.cbClearPreviousO2CirData.Size = new System.Drawing.Size(191, 17);
            this.cbClearPreviousO2CirData.TabIndex = 107;
            this.cbClearPreviousO2CirData.Text = "On Drop, clear previous o2CirData ";
            this.cbClearPreviousO2CirData.UseVisualStyleBackColor = true;
            // 
            // ascx_DropObject1
            // 
            this.ascx_DropObject1.AllowDrop = true;
            this.ascx_DropObject1.BackColor = System.Drawing.Color.Maroon;
            this.ascx_DropObject1.ForeColor = System.Drawing.Color.White;
            this.ascx_DropObject1.Location = new System.Drawing.Point(171, 22);
            this.ascx_DropObject1.Name = "ascx_DropObject1";
            this.ascx_DropObject1.Size = new System.Drawing.Size(98, 81);
            this.ascx_DropObject1.TabIndex = 106;
            this.ascx_DropObject1.Text = "Drop here CirDump or CirData files to Load";
            this.ascx_DropObject1.Load += new System.EventHandler(this.ascx_DropObject1_Load);
            this.ascx_DropObject1.eDnDAction_ObjectDataReceived_Event += new O2.Kernel.CodeUtils.Callbacks.dMethod_Object(this.ascx_DropObject1_eDnDAction_ObjectDataReceived_Event);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.llClearLoadedData);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ascx_DropObject1);
            this.groupBox1.Controls.Add(this.lbLoadedO2CirDataFiles);
            this.groupBox1.Controls.Add(this.cbClearPreviousO2CirData);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(605, 134);
            this.groupBox1.TabIndex = 109;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Load CirData or CirDump (*.xml) Files";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // llClearLoadedData
            // 
            this.llClearLoadedData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llClearLoadedData.AutoSize = true;
            this.llClearLoadedData.Location = new System.Drawing.Point(506, 44);
            this.llClearLoadedData.Name = "llClearLoadedData";
            this.llClearLoadedData.Size = new System.Drawing.Size(96, 13);
            this.llClearLoadedData.TabIndex = 111;
            this.llClearLoadedData.TabStop = true;
            this.llClearLoadedData.Text = "Clear Loaded Data";
            this.llClearLoadedData.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llClearLoadedData_LinkClicked);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.laNumberOfMethods);
            this.groupBox2.Controls.Add(this.laNumberOfClasses);
            this.groupBox2.Location = new System.Drawing.Point(16, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(149, 84);
            this.groupBox2.TabIndex = 110;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Loaded CirData file stats";
            // 
            // laNumberOfMethods
            // 
            this.laNumberOfMethods.AutoSize = true;
            this.laNumberOfMethods.Location = new System.Drawing.Point(16, 45);
            this.laNumberOfMethods.Name = "laNumberOfMethods";
            this.laNumberOfMethods.Size = new System.Drawing.Size(16, 13);
            this.laNumberOfMethods.TabIndex = 1;
            this.laNumberOfMethods.Text = "...";
            // 
            // laNumberOfClasses
            // 
            this.laNumberOfClasses.AutoSize = true;
            this.laNumberOfClasses.Location = new System.Drawing.Point(16, 20);
            this.laNumberOfClasses.Name = "laNumberOfClasses";
            this.laNumberOfClasses.Size = new System.Drawing.Size(16, 13);
            this.laNumberOfClasses.TabIndex = 0;
            this.laNumberOfClasses.Text = "...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(275, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 109;
            this.label1.Text = "Loaded files";
            // 
            // ascx_CirAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ascx_CirAnalysis";
            this.Size = new System.Drawing.Size(605, 134);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbLoadedO2CirDataFiles;
        private System.Windows.Forms.CheckBox cbClearPreviousO2CirData;
        private ascx_DropObject ascx_DropObject1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label laNumberOfMethods;
        private System.Windows.Forms.Label laNumberOfClasses;
        private System.Windows.Forms.LinkLabel llClearLoadedData;
    }
}
