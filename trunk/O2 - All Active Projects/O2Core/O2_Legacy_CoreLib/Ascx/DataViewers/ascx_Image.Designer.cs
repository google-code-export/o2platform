// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Kernel.CodeUtils;
using O2.Kernel.CodeUtils;
using O2.Views.ASCX.CoreControls;

namespace O2.Legacy.CoreLib.Ascx.DataViewers
{
    partial class ascx_Image
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
            this.picBoxImage = new System.Windows.Forms.PictureBox();
            this.ascx_DropObject1 = new ascx_DropObject();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // picBoxImage
            // 
            this.picBoxImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                             | System.Windows.Forms.AnchorStyles.Left)
                                                                            | System.Windows.Forms.AnchorStyles.Right)));
            this.picBoxImage.BackColor = System.Drawing.Color.Gray;
            this.picBoxImage.Location = new System.Drawing.Point(0, 22);
            this.picBoxImage.Name = "picBoxImage";
            this.picBoxImage.Size = new System.Drawing.Size(375, 230);
            this.picBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxImage.TabIndex = 0;
            this.picBoxImage.TabStop = false;
            this.picBoxImage.DoubleClick += new System.EventHandler(this.picBoxImage_DoubleClick);
            this.picBoxImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picBoxImage_MouseMove);
            this.picBoxImage.Click += new System.EventHandler(this.picBoxImage_Click);
            this.picBoxImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxImage_MouseDown);
            this.picBoxImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxImage_MouseUp);
            // 
            // ascx_DropObject1
            // 
            this.ascx_DropObject1.AllowDrop = true;
            this.ascx_DropObject1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ascx_DropObject1.BackColor = System.Drawing.Color.Maroon;
            this.ascx_DropObject1.ForeColor = System.Drawing.Color.White;
            this.ascx_DropObject1.Location = new System.Drawing.Point(265, 22);
            this.ascx_DropObject1.Name = "ascx_DropObject1";
            this.ascx_DropObject1.Size = new System.Drawing.Size(110, 21);
            this.ascx_DropObject1.TabIndex = 1;
            this.ascx_DropObject1.Load += new System.EventHandler(this.ascx_DropObject1_Load);
            this.ascx_DropObject1.eDnDAction_ObjectDataReceived_Event += new Callbacks.dMethod_Object(this.ascx_DropObject1_eDnDAction_ObjectDataReceived_Event);
            // 
            // ascx_Image
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.ascx_DropObject1);
            this.Controls.Add(this.picBoxImage);
            this.Name = "ascx_Image";
            this.Size = new System.Drawing.Size(375, 252);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picBoxImage;
        private ascx_DropObject ascx_DropObject1;
    }
}
