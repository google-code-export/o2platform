// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Kernel.CodeUtils;
using O2.Views.ASCX.CoreControls;

namespace O2.Core.CIR.Ascx.Rnd
{
    partial class ascx_PatchCirDumps
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
// ReSharper disable ConvertToConstant
// ReSharper disable RedundantDefaultFieldInitializer
        private System.ComponentModel.IContainer components = null;
// ReSharper restore RedundantDefaultFieldInitializer
// ReSharper restore ConvertToConstant

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
            this.label1 = new System.Windows.Forms.Label();
            this.btFindClassesWithNoControlFlowGraphs = new System.Windows.Forms.Button();
            this.lbCirFileLoaded = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.ascx_DropObject1 = new ascx_DropObject();
            this.cbClearPreviousO2CirData = new System.Windows.Forms.CheckBox();
            this.cbFixCirDumpFiles = new System.Windows.Forms.CheckBox();
            this.cbOnlyMapWithExtra_Imp = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "This Control fixes common problems with CirDumps";
            // 
            // btFindClassesWithNoControlFlowGraphs
            // 
            this.btFindClassesWithNoControlFlowGraphs.Location = new System.Drawing.Point(7, 62);
            this.btFindClassesWithNoControlFlowGraphs.Name = "btFindClassesWithNoControlFlowGraphs";
            this.btFindClassesWithNoControlFlowGraphs.Size = new System.Drawing.Size(310, 23);
            this.btFindClassesWithNoControlFlowGraphs.TabIndex = 1;
            this.btFindClassesWithNoControlFlowGraphs.Text = "Find Classes with No ControlFlow Graphs";
            this.btFindClassesWithNoControlFlowGraphs.UseVisualStyleBackColor = true;
            this.btFindClassesWithNoControlFlowGraphs.Click += new System.EventHandler(this.btFindClassesWithNoControlFlowGraphs_Click);
            // 
            // lbCirFileLoaded
            // 
            this.lbCirFileLoaded.AutoSize = true;
            this.lbCirFileLoaded.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCirFileLoaded.Location = new System.Drawing.Point(108, 29);
            this.lbCirFileLoaded.Name = "lbCirFileLoaded";
            this.lbCirFileLoaded.Size = new System.Drawing.Size(15, 13);
            this.lbCirFileLoaded.TabIndex = 32;
            this.lbCirFileLoaded.Text = "..";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 31;
            this.label8.Text = "CirData fiile loaded:";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(7, 91);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(195, 147);
            this.listBox1.TabIndex = 33;
            this.listBox1.Visible = false;
            // 
            // ascx_DropObject1
            // 
            this.ascx_DropObject1.AllowDrop = true;
            this.ascx_DropObject1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ascx_DropObject1.BackColor = System.Drawing.Color.Maroon;
            this.ascx_DropObject1.ForeColor = System.Drawing.Color.White;
            this.ascx_DropObject1.Location = new System.Drawing.Point(255, 0);
            this.ascx_DropObject1.Name = "ascx_DropObject1";
            this.ascx_DropObject1.Size = new System.Drawing.Size(102, 21);
            this.ascx_DropObject1.TabIndex = 34;
            this.ascx_DropObject1.eDnDAction_ObjectDataReceived_Event += new Callbacks.dMethod_Object(this.ascx_DropObject1_eDnDAction_ObjectDataReceived_Event);
            // 
            // cbClearPreviousO2CirData
            // 
            this.cbClearPreviousO2CirData.AutoSize = true;
            this.cbClearPreviousO2CirData.Checked = true;
            this.cbClearPreviousO2CirData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbClearPreviousO2CirData.Location = new System.Drawing.Point(374, 3);
            this.cbClearPreviousO2CirData.Name = "cbClearPreviousO2CirData";
            this.cbClearPreviousO2CirData.Size = new System.Drawing.Size(146, 17);
            this.cbClearPreviousO2CirData.TabIndex = 105;
            this.cbClearPreviousO2CirData.Text = "Clear previous o2CirData ";
            this.cbClearPreviousO2CirData.UseVisualStyleBackColor = true;
            // 
            // cbFixCirDumpFiles
            // 
            this.cbFixCirDumpFiles.AutoSize = true;
            this.cbFixCirDumpFiles.Location = new System.Drawing.Point(324, 62);
            this.cbFixCirDumpFiles.Name = "cbFixCirDumpFiles";
            this.cbFixCirDumpFiles.Size = new System.Drawing.Size(106, 17);
            this.cbFixCirDumpFiles.TabIndex = 106;
            this.cbFixCirDumpFiles.Text = "Fix CirDump Files";
            this.cbFixCirDumpFiles.UseVisualStyleBackColor = true;
            // 
            // cbOnlyMapWithExtra_Imp
            // 
            this.cbOnlyMapWithExtra_Imp.AutoSize = true;
            this.cbOnlyMapWithExtra_Imp.Location = new System.Drawing.Point(324, 86);
            this.cbOnlyMapWithExtra_Imp.Name = "cbOnlyMapWithExtra_Imp";
            this.cbOnlyMapWithExtra_Imp.Size = new System.Drawing.Size(221, 17);
            this.cbOnlyMapWithExtra_Imp.TabIndex = 107;
            this.cbOnlyMapWithExtra_Imp.Text = "only Map With Extra Imp (for Java Spring)";
            this.cbOnlyMapWithExtra_Imp.UseVisualStyleBackColor = true;
            // 
            // ascx_PatchCirDumps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbOnlyMapWithExtra_Imp);
            this.Controls.Add(this.cbFixCirDumpFiles);
            this.Controls.Add(this.cbClearPreviousO2CirData);
            this.Controls.Add(this.ascx_DropObject1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.lbCirFileLoaded);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btFindClassesWithNoControlFlowGraphs);
            this.Controls.Add(this.label1);
            this.Name = "ascx_PatchCirDumps";
            this.Size = new System.Drawing.Size(705, 251);
            this.Load += new System.EventHandler(this.ascx_PatchCirDumps_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btFindClassesWithNoControlFlowGraphs;
        private System.Windows.Forms.Label lbCirFileLoaded;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox listBox1;
        private ascx_DropObject ascx_DropObject1;
        private System.Windows.Forms.CheckBox cbClearPreviousO2CirData;
        private System.Windows.Forms.CheckBox cbFixCirDumpFiles;
        private System.Windows.Forms.CheckBox cbOnlyMapWithExtra_Imp;
    }
}
