// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Kernel.CodeUtils;
using O2.Kernel.CodeUtils;
using O2.Views.ASCX.CoreControls;

namespace O2.Legacy.CoreLib.Ascx.DataViewers
{
    partial class ascx_TreeView
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
            this.tvObject = new System.Windows.Forms.TreeView();
            this.lbObjectLoaded = new System.Windows.Forms.Label();
            this.ascx_DropObject1 = new ascx_DropObject();
            this.SuspendLayout();
            // 
            // tvObject
            // 
            this.tvObject.Location = new System.Drawing.Point(3, 48);
            this.tvObject.Name = "tvObject";
            this.tvObject.Size = new System.Drawing.Size(349, 284);
            this.tvObject.TabIndex = 0;
            // 
            // lbObjectLoaded
            // 
            this.lbObjectLoaded.AutoSize = true;
            this.lbObjectLoaded.Location = new System.Drawing.Point(4, 30);
            this.lbObjectLoaded.Name = "lbObjectLoaded";
            this.lbObjectLoaded.Size = new System.Drawing.Size(16, 13);
            this.lbObjectLoaded.TabIndex = 1;
            this.lbObjectLoaded.Text = "...";
            // 
            // ascx_DropObject1
            // 
            this.ascx_DropObject1.AllowDrop = true;
            this.ascx_DropObject1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ascx_DropObject1.BackColor = System.Drawing.Color.Maroon;
            this.ascx_DropObject1.ForeColor = System.Drawing.Color.White;
            this.ascx_DropObject1.Location = new System.Drawing.Point(254, 27);
            this.ascx_DropObject1.Name = "ascx_DropObject1";
            this.ascx_DropObject1.Size = new System.Drawing.Size(101, 20);
            this.ascx_DropObject1.TabIndex = 15;
            this.ascx_DropObject1.eDnDAction_ObjectDataReceived_Event += new Callbacks.dMethod_Object(this.ascx_DropObject1_eDnDAction_ObjectDataReceived_Event);
            // 
            // ascx_TreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.ascx_DropObject1);
            this.Controls.Add(this.lbObjectLoaded);
            this.Controls.Add(this.tvObject);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ascx_TreeView";
            this.Size = new System.Drawing.Size(355, 335);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvObject;
        private System.Windows.Forms.Label lbObjectLoaded;
        private ascx_DropObject ascx_DropObject1;
    }
}
