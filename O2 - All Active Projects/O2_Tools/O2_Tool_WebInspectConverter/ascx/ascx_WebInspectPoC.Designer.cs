// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Kernel.CodeUtils;
using O2.Views.ASCX.CoreControls;

namespace O2.Tool.WebInspectConverter.ascx
{
    partial class ascx_WebInspectPoC
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
            this.btStep_LoadWebInspectFiles = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dropObject_OunceOzasmt = new O2.Views.ASCX.CoreControls.ascx_DropObject();
            this.dropObject_WebInspectFiles = new O2.Views.ASCX.CoreControls.ascx_DropObject();
            this.btLoadMappingsGui = new System.Windows.Forms.Button();
            this.dropObject_WebInspectResults = new O2.Views.ASCX.CoreControls.ascx_DropObject();
            this.SuspendLayout();
            // 
            // btStep_LoadWebInspectFiles
            // 
            this.btStep_LoadWebInspectFiles.Location = new System.Drawing.Point(5, 46);
            this.btStep_LoadWebInspectFiles.Name = "btStep_LoadWebInspectFiles";
            this.btStep_LoadWebInspectFiles.Size = new System.Drawing.Size(154, 42);
            this.btStep_LoadWebInspectFiles.TabIndex = 5;
            this.btStep_LoadWebInspectFiles.Text = "Load Web Inspect Files";
            this.btStep_LoadWebInspectFiles.UseVisualStyleBackColor = true;
            this.btStep_LoadWebInspectFiles.Click += new System.EventHandler(this.btStep_LoadWebInspectFiles_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(395, 24);
            this.label1.TabIndex = 7;
            this.label1.Text = "Web Inspect PoC (Add Ounce and Hp Logos)";
            // 
            // dropObject_OunceOzasmt
            // 
            this.dropObject_OunceOzasmt.AllowDrop = true;
            this.dropObject_OunceOzasmt.BackColor = System.Drawing.Color.Maroon;
            this.dropObject_OunceOzasmt.ForeColor = System.Drawing.Color.White;
            this.dropObject_OunceOzasmt.Location = new System.Drawing.Point(496, 46);
            this.dropObject_OunceOzasmt.Name = "dropObject_OunceOzasmt";
            this.dropObject_OunceOzasmt.Size = new System.Drawing.Size(162, 44);
            this.dropObject_OunceOzasmt.TabIndex = 16;
            this.dropObject_OunceOzasmt.Text = "Drop ozamst files to create ozasmt mapping file";
            this.dropObject_OunceOzasmt.Load += new System.EventHandler(this.dropObject_OunceOzasmt_Load);
            this.dropObject_OunceOzasmt.eDnDAction_ObjectDataReceived_Event += new O2.Kernel.CodeUtils.Callbacks.dMethod_Object(this.dropObject_OunceOzasmt_eDnDAction_ObjectDataReceived_Event);
            // 
            // dropObject_WebInspectFiles
            // 
            this.dropObject_WebInspectFiles.AllowDrop = true;
            this.dropObject_WebInspectFiles.BackColor = System.Drawing.Color.Maroon;
            this.dropObject_WebInspectFiles.ForeColor = System.Drawing.Color.White;
            this.dropObject_WebInspectFiles.Location = new System.Drawing.Point(176, 46);
            this.dropObject_WebInspectFiles.Name = "dropObject_WebInspectFiles";
            this.dropObject_WebInspectFiles.Size = new System.Drawing.Size(154, 44);
            this.dropObject_WebInspectFiles.TabIndex = 14;
            this.dropObject_WebInspectFiles.Text = "Drop WebInspect Scan Files for Processing";
            this.dropObject_WebInspectFiles.Load += new System.EventHandler(this.dropObject_WebInspectFiles_Load);
            this.dropObject_WebInspectFiles.eDnDAction_ObjectDataReceived_Event += new O2.Kernel.CodeUtils.Callbacks.dMethod_Object(this.dropObject_WebInspectFiles_eDnDAction_ObjectDataReceived_Event);
            // 
            // btLoadMappingsGui
            // 
            this.btLoadMappingsGui.Location = new System.Drawing.Point(664, 46);
            this.btLoadMappingsGui.Name = "btLoadMappingsGui";
            this.btLoadMappingsGui.Size = new System.Drawing.Size(154, 42);
            this.btLoadMappingsGui.TabIndex = 18;
            this.btLoadMappingsGui.Text = "Load Mapping Gui";
            this.btLoadMappingsGui.UseVisualStyleBackColor = true;
            this.btLoadMappingsGui.Click += new System.EventHandler(this.btLoadMappingsGui_Click);
            // 
            // dropObject_WebInspectResults
            // 
            this.dropObject_WebInspectResults.AllowDrop = true;
            this.dropObject_WebInspectResults.BackColor = System.Drawing.Color.Maroon;
            this.dropObject_WebInspectResults.ForeColor = System.Drawing.Color.White;
            this.dropObject_WebInspectResults.Location = new System.Drawing.Point(336, 45);
            this.dropObject_WebInspectResults.Name = "dropObject_WebInspectResults";
            this.dropObject_WebInspectResults.Size = new System.Drawing.Size(154, 44);
            this.dropObject_WebInspectResults.TabIndex = 19;
            this.dropObject_WebInspectResults.Text = "Drop WebInspect Results here to see findings created";
            this.dropObject_WebInspectResults.eDnDAction_ObjectDataReceived_Event += new O2.Kernel.CodeUtils.Callbacks.dMethod_Object(this.dropObject_WebInspectResults_eDnDAction_ObjectDataReceived_Event);
            // 
            // ascx_WebInspectPoC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dropObject_WebInspectResults);
            this.Controls.Add(this.btLoadMappingsGui);
            this.Controls.Add(this.dropObject_OunceOzasmt);
            this.Controls.Add(this.dropObject_WebInspectFiles);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btStep_LoadWebInspectFiles);
            this.Name = "ascx_WebInspectPoC";
            this.Size = new System.Drawing.Size(1048, 154);
            this.Load += new System.EventHandler(this.ascx_WebInspectPoC_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btStep_LoadWebInspectFiles;
        private System.Windows.Forms.Label label1;
        private ascx_DropObject dropObject_WebInspectFiles;
        private ascx_DropObject dropObject_OunceOzasmt;
        private System.Windows.Forms.Button btLoadMappingsGui;
        private ascx_DropObject dropObject_WebInspectResults;

    }
}
