// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
namespace O2.Debugger.Mdbg.O2Debugger.Ascx
{
    partial class ascx_Variables
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
            this.label1 = new System.Windows.Forms.Label();
            this.tvVariables = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btGetVariableValue = new System.Windows.Forms.Button();
            this.btExecuteOnFrame = new System.Windows.Forms.Button();
            this.tbExecuteOnFrame = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btChangeVariableValue = new System.Windows.Forms.Button();
            this.tbVariableValue = new System.Windows.Forms.TextBox();
            this.laVariableType = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.laVariableFullName = new System.Windows.Forms.Label();
            this.llReloadData = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Variables";
            // 
            // tvVariables
            // 
            this.tvVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvVariables.Location = new System.Drawing.Point(7, 21);
            this.tvVariables.Name = "tvVariables";
            this.tvVariables.Size = new System.Drawing.Size(344, 253);
            this.tvVariables.TabIndex = 6;
            this.tvVariables.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvVariables_BeforeExpand);
            this.tvVariables.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvVariables_AfterSelect);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btGetVariableValue);
            this.groupBox1.Controls.Add(this.btExecuteOnFrame);
            this.groupBox1.Controls.Add(this.tbExecuteOnFrame);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btChangeVariableValue);
            this.groupBox1.Controls.Add(this.tbVariableValue);
            this.groupBox1.Controls.Add(this.laVariableType);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.laVariableFullName);
            this.groupBox1.Location = new System.Drawing.Point(7, 281);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 130);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selected Variable Details";
            // 
            // btGetVariableValue
            // 
            this.btGetVariableValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btGetVariableValue.Location = new System.Drawing.Point(266, 10);
            this.btGetVariableValue.Name = "btGetVariableValue";
            this.btGetVariableValue.Size = new System.Drawing.Size(75, 23);
            this.btGetVariableValue.TabIndex = 8;
            this.btGetVariableValue.Text = "get Value";
            this.btGetVariableValue.UseVisualStyleBackColor = true;
            this.btGetVariableValue.Click += new System.EventHandler(this.btGetVariableValue_Click);
            // 
            // btExecuteOnFrame
            // 
            this.btExecuteOnFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btExecuteOnFrame.Location = new System.Drawing.Point(266, 101);
            this.btExecuteOnFrame.Name = "btExecuteOnFrame";
            this.btExecuteOnFrame.Size = new System.Drawing.Size(75, 23);
            this.btExecuteOnFrame.TabIndex = 7;
            this.btExecuteOnFrame.Text = "execute";
            this.btExecuteOnFrame.UseVisualStyleBackColor = true;
            this.btExecuteOnFrame.Click += new System.EventHandler(this.btExecuteOnFrame_Click);
            // 
            // tbExecuteOnFrame
            // 
            this.tbExecuteOnFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbExecuteOnFrame.Location = new System.Drawing.Point(6, 77);
            this.tbExecuteOnFrame.Multiline = true;
            this.tbExecuteOnFrame.Name = "tbExecuteOnFrame";
            this.tbExecuteOnFrame.Size = new System.Drawing.Size(254, 47);
            this.tbExecuteOnFrame.TabIndex = 6;
            this.tbExecuteOnFrame.TextChanged += new System.EventHandler(this.tbExecuteOnFrame_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Execute on Frame";
            // 
            // btChangeVariableValue
            // 
            this.btChangeVariableValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btChangeVariableValue.Location = new System.Drawing.Point(266, 38);
            this.btChangeVariableValue.Name = "btChangeVariableValue";
            this.btChangeVariableValue.Size = new System.Drawing.Size(75, 23);
            this.btChangeVariableValue.TabIndex = 4;
            this.btChangeVariableValue.Text = "set value";
            this.btChangeVariableValue.UseVisualStyleBackColor = true;
            this.btChangeVariableValue.Click += new System.EventHandler(this.btChangeVariableValue_Click);
            // 
            // tbVariableValue
            // 
            this.tbVariableValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbVariableValue.Location = new System.Drawing.Point(63, 40);
            this.tbVariableValue.Name = "tbVariableValue";
            this.tbVariableValue.Size = new System.Drawing.Size(197, 20);
            this.tbVariableValue.TabIndex = 3;
            // 
            // laVariableType
            // 
            this.laVariableType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.laVariableType.AutoSize = true;
            this.laVariableType.Location = new System.Drawing.Point(107, 20);
            this.laVariableType.Name = "laVariableType";
            this.laVariableType.Size = new System.Drawing.Size(75, 13);
            this.laVariableType.TabIndex = 2;
            this.laVariableType.Text = "{variable type}";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Value:";
            // 
            // laVariableFullName
            // 
            this.laVariableFullName.AutoSize = true;
            this.laVariableFullName.Location = new System.Drawing.Point(7, 20);
            this.laVariableFullName.Name = "laVariableFullName";
            this.laVariableFullName.Size = new System.Drawing.Size(94, 13);
            this.laVariableFullName.TabIndex = 0;
            this.laVariableFullName.Text = "{variable fullname}";
            // 
            // llReloadData
            // 
            this.llReloadData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llReloadData.AutoSize = true;
            this.llReloadData.Location = new System.Drawing.Point(281, 4);
            this.llReloadData.Name = "llReloadData";
            this.llReloadData.Size = new System.Drawing.Size(67, 13);
            this.llReloadData.TabIndex = 8;
            this.llReloadData.TabStop = true;
            this.llReloadData.Text = "Reload Data";
            this.llReloadData.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llReloadData_LinkClicked);
            // 
            // ascx_Variables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.llReloadData);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tvVariables);
            this.Controls.Add(this.label1);
            this.Name = "ascx_Variables";
            this.Size = new System.Drawing.Size(354, 414);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

            this.Load += new System.EventHandler(this.ascx_Variables_Load);
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView tvVariables;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btExecuteOnFrame;
        private System.Windows.Forms.TextBox tbExecuteOnFrame;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btChangeVariableValue;
        private System.Windows.Forms.TextBox tbVariableValue;
        private System.Windows.Forms.Label laVariableType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label laVariableFullName;
        private System.Windows.Forms.Button btGetVariableValue;
        private System.Windows.Forms.LinkLabel llReloadData;
    }
}
