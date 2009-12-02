// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
namespace O2.Cmd.SpringMvc.Ascx
{
    partial class ascx_SpringMvcAutoBindClassesView
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
            this.cbHideGetAndSetStrings = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tvBindableFields = new System.Windows.Forms.TreeView();
            this.lbCurrentRootClass = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbHideGetAndSetStrings
            // 
            this.cbHideGetAndSetStrings.AutoSize = true;
            this.cbHideGetAndSetStrings.Location = new System.Drawing.Point(3, 24);
            this.cbHideGetAndSetStrings.Name = "cbHideGetAndSetStrings";
            this.cbHideGetAndSetStrings.Size = new System.Drawing.Size(143, 17);
            this.cbHideGetAndSetStrings.TabIndex = 7;
            this.cbHideGetAndSetStrings.Text = "hide \'get\' and \'set\' strings";
            this.cbHideGetAndSetStrings.UseVisualStyleBackColor = true;
            this.cbHideGetAndSetStrings.CheckedChanged += new System.EventHandler(this.cbHideGetAndSetStrings_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(174, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Bindable Object fields  for class(es):";
            // 
            // tvBindableFields
            // 
            this.tvBindableFields.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvBindableFields.Location = new System.Drawing.Point(3, 41);
            this.tvBindableFields.Name = "tvBindableFields";
            this.tvBindableFields.Size = new System.Drawing.Size(686, 376);
            this.tvBindableFields.TabIndex = 5;
            // 
            // lbCurrentRootClass
            // 
            this.lbCurrentRootClass.AutoSize = true;
            this.lbCurrentRootClass.Location = new System.Drawing.Point(171, 2);
            this.lbCurrentRootClass.Name = "lbCurrentRootClass";
            this.lbCurrentRootClass.Size = new System.Drawing.Size(16, 13);
            this.lbCurrentRootClass.TabIndex = 8;
            this.lbCurrentRootClass.Text = "...";
            // 
            // ascx_SpringMvcAutoBindClassesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbCurrentRootClass);
            this.Controls.Add(this.cbHideGetAndSetStrings);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tvBindableFields);
            this.Name = "ascx_SpringMvcAutoBindClassesView";
            this.Size = new System.Drawing.Size(692, 420);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbHideGetAndSetStrings;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TreeView tvBindableFields;
        private System.Windows.Forms.Label lbCurrentRootClass;
    }
}
