// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Kernel.CodeUtils;
using O2.Kernel.CodeUtils;
using O2.Views.ASCX.CoreControls;

namespace O2.Legacy.CoreLib.Ascx.DataViewers
{
    partial class ascx_WebBrowser
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
            this.wbWebBrowser = new System.Windows.Forms.WebBrowser();
            this.btNavigate = new System.Windows.Forms.Button();
            this.tbUrl = new System.Windows.Forms.TextBox();
            this.ascx_DropObject1 = new ascx_DropObject();
            this.SuspendLayout();
            // 
            // wbWebBrowser
            // 
            this.wbWebBrowser.Location = new System.Drawing.Point(3, 46);
            this.wbWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbWebBrowser.Name = "wbWebBrowser";
            this.wbWebBrowser.Size = new System.Drawing.Size(699, 384);
            this.wbWebBrowser.TabIndex = 0;
            // 
            // btNavigate
            // 
            this.btNavigate.Location = new System.Drawing.Point(477, 23);
            this.btNavigate.Name = "btNavigate";
            this.btNavigate.Size = new System.Drawing.Size(68, 20);
            this.btNavigate.TabIndex = 1;
            this.btNavigate.Text = "Open";
            this.btNavigate.UseVisualStyleBackColor = true;
            this.btNavigate.Click += new System.EventHandler(this.btNavigate_Click);
            // 
            // tbUrl
            // 
            this.tbUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                      | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUrl.Location = new System.Drawing.Point(3, 24);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(468, 20);
            this.tbUrl.TabIndex = 2;
            this.tbUrl.Text = "http://www.ouncelabs.com";
            // 
            // ascx_DropObject1
            // 
            this.ascx_DropObject1.AllowDrop = true;
            this.ascx_DropObject1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ascx_DropObject1.BackColor = System.Drawing.Color.Maroon;
            this.ascx_DropObject1.ForeColor = System.Drawing.Color.White;
            this.ascx_DropObject1.Location = new System.Drawing.Point(601, 23);
            this.ascx_DropObject1.Name = "ascx_DropObject1";
            this.ascx_DropObject1.Size = new System.Drawing.Size(101, 20);
            this.ascx_DropObject1.TabIndex = 15;
            this.ascx_DropObject1.eDnDAction_ObjectDataReceived_Event += new Callbacks.dMethod_Object(this.ascx_DropObject1_eDnDAction_ObjectDataReceived_Event);
            // 
            // ascx_WebBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.ascx_DropObject1);
            this.Controls.Add(this.tbUrl);
            this.Controls.Add(this.btNavigate);
            this.Controls.Add(this.wbWebBrowser);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ascx_WebBrowser";
            this.Size = new System.Drawing.Size(705, 433);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbWebBrowser;
        private System.Windows.Forms.Button btNavigate;
        private System.Windows.Forms.TextBox tbUrl;
        private ascx_DropObject ascx_DropObject1;

    }
}
