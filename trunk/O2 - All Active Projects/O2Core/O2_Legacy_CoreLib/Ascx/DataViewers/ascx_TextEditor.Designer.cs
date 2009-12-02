// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Kernel.CodeUtils;
using O2.Kernel.CodeUtils;
using O2.Views.ASCX.CoreControls;

namespace O2.Legacy.CoreLib.Ascx.DataViewers
{
    partial class ascx_TextEditor
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
            this.btSelectFileToOpen = new System.Windows.Forms.Button();
            this.tbPathToFileToOpen = new System.Windows.Forms.TextBox();
            this.rtbText = new System.Windows.Forms.RichTextBox();
            this.ascx_DropObject1 = new ascx_DropObject();
            this.SuspendLayout();
            // 
            // btSelectFileToOpen
            // 
            this.btSelectFileToOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSelectFileToOpen.Location = new System.Drawing.Point(522, 29);
            this.btSelectFileToOpen.Name = "btSelectFileToOpen";
            this.btSelectFileToOpen.Size = new System.Drawing.Size(24, 21);
            this.btSelectFileToOpen.TabIndex = 11;
            this.btSelectFileToOpen.Text = "...";
            this.btSelectFileToOpen.UseVisualStyleBackColor = true;
            this.btSelectFileToOpen.Click += new System.EventHandler(this.btSelectFileToOpen_Click);
            // 
            // tbPathToFileToOpen
            // 
            this.tbPathToFileToOpen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                                   | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPathToFileToOpen.BackColor = System.Drawing.Color.White;
            this.tbPathToFileToOpen.ForeColor = System.Drawing.Color.White;
            this.tbPathToFileToOpen.Location = new System.Drawing.Point(5, 30);
            this.tbPathToFileToOpen.Name = "tbPathToFileToOpen";
            this.tbPathToFileToOpen.Size = new System.Drawing.Size(511, 20);
            this.tbPathToFileToOpen.TabIndex = 12;
            this.tbPathToFileToOpen.TextChanged += new System.EventHandler(this.tbPathToFileToOpen_TextChanged);
            // 
            // rtbText
            // 
            this.rtbText.Location = new System.Drawing.Point(4, 52);
            this.rtbText.Name = "rtbText";
            this.rtbText.Size = new System.Drawing.Size(698, 378);
            this.rtbText.TabIndex = 13;
            this.rtbText.Text = "";
            // 
            // ascx_DropObject1
            // 
            this.ascx_DropObject1.AllowDrop = true;
            this.ascx_DropObject1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ascx_DropObject1.BackColor = System.Drawing.Color.Maroon;
            this.ascx_DropObject1.ForeColor = System.Drawing.Color.White;
            this.ascx_DropObject1.Location = new System.Drawing.Point(601, 30);
            this.ascx_DropObject1.Name = "ascx_DropObject1";
            this.ascx_DropObject1.Size = new System.Drawing.Size(101, 20);
            this.ascx_DropObject1.TabIndex = 14;
            this.ascx_DropObject1.eDnDAction_ObjectDataReceived_Event += new Callbacks.dMethod_Object(this.ascx_DropObject1_eDnDAction_ObjectDataReceived_Event);
            // 
            // ascx_TextEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.ascx_DropObject1);
            this.Controls.Add(this.rtbText);
            this.Controls.Add(this.tbPathToFileToOpen);
            this.Controls.Add(this.btSelectFileToOpen);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ascx_TextEditor";
            this.Size = new System.Drawing.Size(705, 433);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btSelectFileToOpen;
        private System.Windows.Forms.TextBox tbPathToFileToOpen;
        private System.Windows.Forms.RichTextBox rtbText;
        private ascx_DropObject ascx_DropObject1;

    }
}
