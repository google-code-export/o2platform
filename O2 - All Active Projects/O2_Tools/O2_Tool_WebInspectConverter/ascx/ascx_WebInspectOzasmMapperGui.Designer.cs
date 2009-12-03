// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Kernel.CodeUtils;
using O2.Views.ASCX.CoreControls;
using O2.Views.ASCX.O2Findings;

namespace O2.Tool.WebInspectConverter.ascx
{
    partial class ascx_WebInspectOzasmMapperGui
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dropObject_WebInspectFile = new O2.Views.ASCX.CoreControls.ascx_DropObject();
            this.fidingsViewer_WebInspectOzasmt = new O2.Views.ASCX.O2Findings.ascx_FindingsViewer();
            this.label1 = new System.Windows.Forms.Label();
            this.dropObject_OunceOzasmtFile = new O2.Views.ASCX.CoreControls.ascx_DropObject();
            this.fidingsViewer_OunceOzasmt = new O2.Views.ASCX.O2Findings.ascx_FindingsViewer();
            this.label2 = new System.Windows.Forms.Label();
            this.btGlueTraces = new System.Windows.Forms.Button();
            this.fidingsViewer_MappedFile = new O2.Views.ASCX.O2Findings.ascx_FindingsViewer();
            this.label3 = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btGlueTraces);
            this.splitContainer1.Panel2.Controls.Add(this.fidingsViewer_MappedFile);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Size = new System.Drawing.Size(789, 536);
            this.splitContainer1.SplitterDistance = 332;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dropObject_WebInspectFile);
            this.splitContainer2.Panel1.Controls.Add(this.fidingsViewer_WebInspectOzasmt);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dropObject_OunceOzasmtFile);
            this.splitContainer2.Panel2.Controls.Add(this.fidingsViewer_OunceOzasmt);
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Size = new System.Drawing.Size(332, 536);
            this.splitContainer2.SplitterDistance = 275;
            this.splitContainer2.TabIndex = 0;
            // 
            // dropObject_WebInspectFile
            // 
            this.dropObject_WebInspectFile.AllowDrop = true;
            this.dropObject_WebInspectFile.BackColor = System.Drawing.Color.Maroon;
            this.dropObject_WebInspectFile.ForeColor = System.Drawing.Color.White;
            this.dropObject_WebInspectFile.Location = new System.Drawing.Point(8, 28);
            this.dropObject_WebInspectFile.Name = "dropObject_WebInspectFile";
            this.dropObject_WebInspectFile.Size = new System.Drawing.Size(218, 21);
            this.dropObject_WebInspectFile.TabIndex = 2;
            this.dropObject_WebInspectFile.Text = "Drop WebInspect Scan file here";
            this.dropObject_WebInspectFile.eDnDAction_ObjectDataReceived_Event += new O2.Kernel.CodeUtils.Callbacks.dMethod_Object(this.dropObject_WebInspectFile_eDnDAction_ObjectDataReceived_Event);
            // 
            // fidingsViewer_WebInspectOzasmt
            // 
            this.fidingsViewer_WebInspectOzasmt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fidingsViewer_WebInspectOzasmt.Location = new System.Drawing.Point(2, 55);
            this.fidingsViewer_WebInspectOzasmt.Name = "fidingsViewer_WebInspectOzasmt";
            this.fidingsViewer_WebInspectOzasmt.Size = new System.Drawing.Size(323, 213);
            this.fidingsViewer_WebInspectOzasmt.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(266, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "WebInspect Mapped ozasmt file";
            // 
            // dropObject_OunceOzasmtFile
            // 
            this.dropObject_OunceOzasmtFile.AllowDrop = true;
            this.dropObject_OunceOzasmtFile.BackColor = System.Drawing.Color.Maroon;
            this.dropObject_OunceOzasmtFile.ForeColor = System.Drawing.Color.White;
            this.dropObject_OunceOzasmtFile.Location = new System.Drawing.Point(8, 32);
            this.dropObject_OunceOzasmtFile.Name = "dropObject_OunceOzasmtFile";
            this.dropObject_OunceOzasmtFile.Size = new System.Drawing.Size(218, 21);
            this.dropObject_OunceOzasmtFile.TabIndex = 3;
            this.dropObject_OunceOzasmtFile.Text = "Drop Ounce ozasmt Scan file here";
            this.dropObject_OunceOzasmtFile.Load += new System.EventHandler(this.dropObject_OunceOzasmtFile_Load);
            this.dropObject_OunceOzasmtFile.eDnDAction_ObjectDataReceived_Event += new O2.Kernel.CodeUtils.Callbacks.dMethod_Object(this.dropObject_OunceOzasmtFile_eDnDAction_ObjectDataReceived_Event);
            // 
            // fidingsViewer_OunceOzasmt
            // 
            this.fidingsViewer_OunceOzasmt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fidingsViewer_OunceOzasmt.Location = new System.Drawing.Point(2, 59);
            this.fidingsViewer_OunceOzasmt.Name = "fidingsViewer_OunceOzasmt";
            this.fidingsViewer_OunceOzasmt.Size = new System.Drawing.Size(323, 191);
            this.fidingsViewer_OunceOzasmt.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(222, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ounce Mapped ozasmt file";
            // 
            // btGlueTraces
            // 
            this.btGlueTraces.Location = new System.Drawing.Point(246, 3);
            this.btGlueTraces.Name = "btGlueTraces";
            this.btGlueTraces.Size = new System.Drawing.Size(131, 23);
            this.btGlueTraces.TabIndex = 4;
            this.btGlueTraces.Text = "Glue Traces";
            this.btGlueTraces.UseVisualStyleBackColor = true;
            this.btGlueTraces.Click += new System.EventHandler(this.btGlueTraces_Click);
            // 
            // fidingsViewer_MappedFile
            // 
            this.fidingsViewer_MappedFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fidingsViewer_MappedFile.Location = new System.Drawing.Point(6, 40);
            this.fidingsViewer_MappedFile.Name = "fidingsViewer_MappedFile";
            this.fidingsViewer_MappedFile.Size = new System.Drawing.Size(440, 489);
            this.fidingsViewer_MappedFile.TabIndex = 3;
            this.fidingsViewer_MappedFile.Load += new System.EventHandler(this.fidingsViewer_MappedFile_Load);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(237, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "WebInspect -- > Ozasmt File";
            // 
            // ascx_WebInspectOzasmMapperGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ascx_WebInspectOzasmMapperGui";
            this.Size = new System.Drawing.Size(789, 536);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ascx_FindingsViewer fidingsViewer_WebInspectOzasmt;
        private System.Windows.Forms.Label label1;
        private ascx_FindingsViewer fidingsViewer_OunceOzasmt;
        private System.Windows.Forms.Label label2;
        private ascx_FindingsViewer fidingsViewer_MappedFile;
        private System.Windows.Forms.Label label3;
        private ascx_DropObject dropObject_WebInspectFile;
        private ascx_DropObject dropObject_OunceOzasmtFile;
        private System.Windows.Forms.Button btGlueTraces;
    }
}
