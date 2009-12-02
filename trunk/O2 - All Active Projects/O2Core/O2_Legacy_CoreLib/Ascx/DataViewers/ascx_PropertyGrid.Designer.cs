using O2.Kernel.CodeUtils;
using O2.Kernel.CodeUtils;
using O2.Views.ASCX.CoreControls;

namespace O2.Legacy.CoreLib.Ascx.DataViewers
{
    partial class ascx_PropertyGrid
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
            this.pgPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.lbTypeOfObjectLoaded = new System.Windows.Forms.Label();
            this.ascx_DropObject1 = new ascx_DropObject();
            this.SuspendLayout();
            // 
            // pgPropertyGrid
            // 
            this.pgPropertyGrid.AllowDrop = true;
            this.pgPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                | System.Windows.Forms.AnchorStyles.Left)
                                                                               | System.Windows.Forms.AnchorStyles.Right)));
            this.pgPropertyGrid.Location = new System.Drawing.Point(3, 48);
            this.pgPropertyGrid.Name = "pgPropertyGrid";
            this.pgPropertyGrid.Size = new System.Drawing.Size(266, 359);
            this.pgPropertyGrid.TabIndex = 0;
            this.pgPropertyGrid.DragDrop += new System.Windows.Forms.DragEventHandler(this.propertyGrid1_DragDrop);
            this.pgPropertyGrid.DragEnter += new System.Windows.Forms.DragEventHandler(this.propertyGrid1_DragEnter);
            // 
            // lbTypeOfObjectLoaded
            // 
            this.lbTypeOfObjectLoaded.AutoSize = true;
            this.lbTypeOfObjectLoaded.Location = new System.Drawing.Point(3, 32);
            this.lbTypeOfObjectLoaded.Name = "lbTypeOfObjectLoaded";
            this.lbTypeOfObjectLoaded.Size = new System.Drawing.Size(35, 13);
            this.lbTypeOfObjectLoaded.TabIndex = 1;
            this.lbTypeOfObjectLoaded.Text = "label1";
            // 
            // ascx_DropObject1
            // 
            this.ascx_DropObject1.AllowDrop = true;
            this.ascx_DropObject1.BackColor = System.Drawing.Color.Maroon;
            this.ascx_DropObject1.ForeColor = System.Drawing.Color.White;
            this.ascx_DropObject1.Location = new System.Drawing.Point(134, 26);
            this.ascx_DropObject1.Name = "ascx_DropObject1";
            this.ascx_DropObject1.Size = new System.Drawing.Size(136, 21);
            this.ascx_DropObject1.TabIndex = 2;
            this.ascx_DropObject1.eDnDAction_ObjectDataReceived_Event += new Callbacks.dMethod_Object(this.ascx_DropObject1_eDnDAction_ObjectDataReceived_Event);
            // 
            // ascx_PropertyGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.ascx_DropObject1);
            this.Controls.Add(this.lbTypeOfObjectLoaded);
            this.Controls.Add(this.pgPropertyGrid);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ascx_PropertyGrid";
            this.Size = new System.Drawing.Size(272, 410);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid pgPropertyGrid;
        private System.Windows.Forms.Label lbTypeOfObjectLoaded;
        private ascx_DropObject ascx_DropObject1;
    }
}