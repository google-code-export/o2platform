// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
namespace O2.Rules.OunceLabs.Ascx
{
    partial class ascx_RulesCreator
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvTargetMethods = new System.Windows.Forms.DataGridView();
            this.db_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.signature = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.btMarkMethodsAs_Source = new System.Windows.Forms.Button();
            this.btMarkMethodsAs_NotPropagateTaint = new System.Windows.Forms.Button();
            this.btMarkMethodsAs_Validator = new System.Windows.Forms.Button();
            this.btMarkMethodsAs_Callback = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btMarkMethodsAs_Sink = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbEditCustomRules_Type = new System.Windows.Forms.ComboBox();
            this.cbEditCustomRules_Severity = new System.Windows.Forms.ComboBox();
            this.cbEditCustomRules_Signature = new System.Windows.Forms.ComboBox();
            this.cbEditCustomRules_vuln_type = new System.Windows.Forms.ComboBox();
            this.cbEditCustomRules_Trace = new System.Windows.Forms.ComboBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.btMarkMethodsAs_TaintPropagator = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTargetMethods)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvTargetMethods);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btMarkMethodsAs_Source);
            this.splitContainer1.Panel2.Controls.Add(this.btMarkMethodsAs_NotPropagateTaint);
            this.splitContainer1.Panel2.Controls.Add(this.btMarkMethodsAs_Validator);
            this.splitContainer1.Panel2.Controls.Add(this.btMarkMethodsAs_Callback);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.btMarkMethodsAs_Sink);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.btMarkMethodsAs_TaintPropagator);
            this.splitContainer1.Size = new System.Drawing.Size(575, 162);
            this.splitContainer1.SplitterDistance = 116;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgvTargetMethods
            // 
            this.dgvTargetMethods.AllowUserToAddRows = false;
            this.dgvTargetMethods.AllowUserToDeleteRows = false;
            this.dgvTargetMethods.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                  | System.Windows.Forms.AnchorStyles.Left)
                                                                                 | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTargetMethods.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgvTargetMethods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTargetMethods.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                                                                                                     this.db_id,
                                                                                                     this.signature});
            this.dgvTargetMethods.Location = new System.Drawing.Point(6, 20);
            this.dgvTargetMethods.Name = "dgvTargetMethods";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTargetMethods.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTargetMethods.RowHeadersWidth = 4;
            this.dgvTargetMethods.ShowEditingIcon = false;
            this.dgvTargetMethods.Size = new System.Drawing.Size(105, 137);
            this.dgvTargetMethods.TabIndex = 3;
            // 
            // db_id
            // 
            this.db_id.HeaderText = "db_id";
            this.db_id.Name = "db_id";
            this.db_id.Width = 58;
            // 
            // signature
            // 
            this.signature.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.signature.HeaderText = "signature";
            this.signature.Name = "signature";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Target Methods";
            // 
            // btMarkMethodsAs_Source
            // 
            this.btMarkMethodsAs_Source.Location = new System.Drawing.Point(246, 4);
            this.btMarkMethodsAs_Source.Name = "btMarkMethodsAs_Source";
            this.btMarkMethodsAs_Source.Size = new System.Drawing.Size(153, 22);
            this.btMarkMethodsAs_Source.TabIndex = 59;
            this.btMarkMethodsAs_Source.Text = "Mark Method as Source";
            this.btMarkMethodsAs_Source.UseVisualStyleBackColor = true;
            this.btMarkMethodsAs_Source.Click += new System.EventHandler(this.btMarkMethodsAs_Source_Click);
            // 
            // btMarkMethodsAs_NotPropagateTaint
            // 
            this.btMarkMethodsAs_NotPropagateTaint.Location = new System.Drawing.Point(3, 87);
            this.btMarkMethodsAs_NotPropagateTaint.Name = "btMarkMethodsAs_NotPropagateTaint";
            this.btMarkMethodsAs_NotPropagateTaint.Size = new System.Drawing.Size(113, 31);
            this.btMarkMethodsAs_NotPropagateTaint.TabIndex = 58;
            this.btMarkMethodsAs_NotPropagateTaint.Text = "Not Propagate Taint";
            this.btMarkMethodsAs_NotPropagateTaint.UseVisualStyleBackColor = true;
            this.btMarkMethodsAs_NotPropagateTaint.Click += new System.EventHandler(this.btMarkMethodsAs_NotPropagateTaint_Click);
            // 
            // btMarkMethodsAs_Validator
            // 
            this.btMarkMethodsAs_Validator.Location = new System.Drawing.Point(3, 56);
            this.btMarkMethodsAs_Validator.Name = "btMarkMethodsAs_Validator";
            this.btMarkMethodsAs_Validator.Size = new System.Drawing.Size(113, 29);
            this.btMarkMethodsAs_Validator.TabIndex = 57;
            this.btMarkMethodsAs_Validator.Text = "Validator";
            this.btMarkMethodsAs_Validator.UseVisualStyleBackColor = true;
            this.btMarkMethodsAs_Validator.Click += new System.EventHandler(this.btMarkMethodsAs_Validator_Click);
            // 
            // btMarkMethodsAs_Callback
            // 
            this.btMarkMethodsAs_Callback.Location = new System.Drawing.Point(3, 124);
            this.btMarkMethodsAs_Callback.Name = "btMarkMethodsAs_Callback";
            this.btMarkMethodsAs_Callback.Size = new System.Drawing.Size(113, 31);
            this.btMarkMethodsAs_Callback.TabIndex = 56;
            this.btMarkMethodsAs_Callback.Text = "Callback";
            this.btMarkMethodsAs_Callback.UseVisualStyleBackColor = true;
            this.btMarkMethodsAs_Callback.Click += new System.EventHandler(this.btMarkMethodsAs_Callback_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 55;
            this.label1.Text = "Mark Methods as:";
            // 
            // btMarkMethodsAs_Sink
            // 
            this.btMarkMethodsAs_Sink.Location = new System.Drawing.Point(122, 4);
            this.btMarkMethodsAs_Sink.Name = "btMarkMethodsAs_Sink";
            this.btMarkMethodsAs_Sink.Size = new System.Drawing.Size(118, 22);
            this.btMarkMethodsAs_Sink.TabIndex = 54;
            this.btMarkMethodsAs_Sink.Text = "Mark Method as Sink";
            this.btMarkMethodsAs_Sink.UseVisualStyleBackColor = true;
            this.btMarkMethodsAs_Sink.Click += new System.EventHandler(this.btMarkMethodsAs_Sink_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                          | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cbEditCustomRules_Type);
            this.groupBox1.Controls.Add(this.cbEditCustomRules_Severity);
            this.groupBox1.Controls.Add(this.cbEditCustomRules_Signature);
            this.groupBox1.Controls.Add(this.cbEditCustomRules_vuln_type);
            this.groupBox1.Controls.Add(this.cbEditCustomRules_Trace);
            this.groupBox1.Controls.Add(this.label41);
            this.groupBox1.Controls.Add(this.label37);
            this.groupBox1.Controls.Add(this.label36);
            this.groupBox1.Controls.Add(this.label35);
            this.groupBox1.Controls.Add(this.label34);
            this.groupBox1.Location = new System.Drawing.Point(122, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(328, 123);
            this.groupBox1.TabIndex = 53;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source or Sink Action Object Properties";
            // 
            // cbEditCustomRules_Type
            // 
            this.cbEditCustomRules_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEditCustomRules_Type.FormattingEnabled = true;
            this.cbEditCustomRules_Type.Location = new System.Drawing.Point(190, 65);
            this.cbEditCustomRules_Type.Name = "cbEditCustomRules_Type";
            this.cbEditCustomRules_Type.Size = new System.Drawing.Size(61, 21);
            this.cbEditCustomRules_Type.TabIndex = 39;
            // 
            // cbEditCustomRules_Severity
            // 
            this.cbEditCustomRules_Severity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEditCustomRules_Severity.FormattingEnabled = true;
            this.cbEditCustomRules_Severity.Location = new System.Drawing.Point(75, 65);
            this.cbEditCustomRules_Severity.Name = "cbEditCustomRules_Severity";
            this.cbEditCustomRules_Severity.Size = new System.Drawing.Size(61, 21);
            this.cbEditCustomRules_Severity.TabIndex = 38;
            // 
            // cbEditCustomRules_Signature
            // 
            this.cbEditCustomRules_Signature.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                                            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEditCustomRules_Signature.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEditCustomRules_Signature.FormattingEnabled = true;
            this.cbEditCustomRules_Signature.Location = new System.Drawing.Point(75, 38);
            this.cbEditCustomRules_Signature.Name = "cbEditCustomRules_Signature";
            this.cbEditCustomRules_Signature.Size = new System.Drawing.Size(247, 21);
            this.cbEditCustomRules_Signature.Sorted = true;
            this.cbEditCustomRules_Signature.TabIndex = 37;
            // 
            // cbEditCustomRules_vuln_type
            // 
            this.cbEditCustomRules_vuln_type.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                                            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEditCustomRules_vuln_type.FormattingEnabled = true;
            this.cbEditCustomRules_vuln_type.Location = new System.Drawing.Point(75, 17);
            this.cbEditCustomRules_vuln_type.Name = "cbEditCustomRules_vuln_type";
            this.cbEditCustomRules_vuln_type.Size = new System.Drawing.Size(247, 21);
            this.cbEditCustomRules_vuln_type.Sorted = true;
            this.cbEditCustomRules_vuln_type.TabIndex = 36;
            // 
            // cbEditCustomRules_Trace
            // 
            this.cbEditCustomRules_Trace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEditCustomRules_Trace.FormattingEnabled = true;
            this.cbEditCustomRules_Trace.Location = new System.Drawing.Point(74, 92);
            this.cbEditCustomRules_Trace.Name = "cbEditCustomRules_Trace";
            this.cbEditCustomRules_Trace.Size = new System.Drawing.Size(44, 21);
            this.cbEditCustomRules_Trace.TabIndex = 35;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(33, 98);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(35, 13);
            this.label41.TabIndex = 8;
            this.label41.Text = "Trace";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(12, 45);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(52, 13);
            this.label37.TabIndex = 3;
            this.label37.Text = "Signature";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(150, 68);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(34, 13);
            this.label36.TabIndex = 2;
            this.label36.Text = "Type:";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(25, 70);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(45, 13);
            this.label35.TabIndex = 1;
            this.label35.Text = "Severity";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(12, 24);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(58, 13);
            this.label34.TabIndex = 0;
            this.label34.Text = "Vuln Type:";
            // 
            // btMarkMethodsAs_TaintPropagator
            // 
            this.btMarkMethodsAs_TaintPropagator.Location = new System.Drawing.Point(3, 20);
            this.btMarkMethodsAs_TaintPropagator.Name = "btMarkMethodsAs_TaintPropagator";
            this.btMarkMethodsAs_TaintPropagator.Size = new System.Drawing.Size(113, 30);
            this.btMarkMethodsAs_TaintPropagator.TabIndex = 52;
            this.btMarkMethodsAs_TaintPropagator.Text = "Taint Propagator";
            this.btMarkMethodsAs_TaintPropagator.UseVisualStyleBackColor = true;
            this.btMarkMethodsAs_TaintPropagator.Click += new System.EventHandler(this.btMarkMethodsAs_TaintPropagator_Click);
            // 
            // ascx_RulesCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.splitContainer1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ascx_RulesCreator";
            this.Size = new System.Drawing.Size(575, 162);
            this.Load += new System.EventHandler(this.ascx_OunceRulesCreator_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTargetMethods)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvTargetMethods;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btMarkMethodsAs_Validator;
        private System.Windows.Forms.Button btMarkMethodsAs_Callback;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btMarkMethodsAs_Sink;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbEditCustomRules_Type;
        private System.Windows.Forms.ComboBox cbEditCustomRules_Severity;
        private System.Windows.Forms.ComboBox cbEditCustomRules_Signature;
        private System.Windows.Forms.ComboBox cbEditCustomRules_vuln_type;
        private System.Windows.Forms.ComboBox cbEditCustomRules_Trace;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Button btMarkMethodsAs_TaintPropagator;
        private System.Windows.Forms.Button btMarkMethodsAs_Source;
        private System.Windows.Forms.Button btMarkMethodsAs_NotPropagateTaint;
        private System.Windows.Forms.DataGridViewTextBoxColumn db_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn signature;
    }
}
