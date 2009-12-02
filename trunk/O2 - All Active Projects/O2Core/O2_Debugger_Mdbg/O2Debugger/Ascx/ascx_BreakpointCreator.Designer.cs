// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Views.ASCX.DataViewers;

namespace O2.Debugger.Mdbg.O2Debugger.Ascx
{
    partial class ascx_BreakpointCreator
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
            this.lbModulesInDebuggeeProcess = new System.Windows.Forms.ListBox();
            this.btCalculate = new System.Windows.Forms.Button();
            this.functionsViewer = new ascx_FunctionsViewer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbAddBreakpointsVerboseMessages = new System.Windows.Forms.CheckBox();
            this.btAddBreakpointOnAllMethods = new System.Windows.Forms.Button();
            this.laNumberOfMethods = new System.Windows.Forms.Label();
            this.laNumberOfTypes = new System.Windows.Forms.Label();
            this.btShowBreakpoints = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Modules in Debugge Process";
            // 
            // lbModulesInDebuggeeProcess
            // 
            this.lbModulesInDebuggeeProcess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbModulesInDebuggeeProcess.FormattingEnabled = true;
            this.lbModulesInDebuggeeProcess.Location = new System.Drawing.Point(7, 59);
            this.lbModulesInDebuggeeProcess.Name = "lbModulesInDebuggeeProcess";
            this.lbModulesInDebuggeeProcess.Size = new System.Drawing.Size(353, 134);
            this.lbModulesInDebuggeeProcess.TabIndex = 1;
            this.lbModulesInDebuggeeProcess.SelectedIndexChanged += new System.EventHandler(this.lbModulesInDebuggeeProcess_SelectedIndexChanged);
            // 
            // btCalculate
            // 
            this.btCalculate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCalculate.Location = new System.Drawing.Point(254, 4);
            this.btCalculate.Name = "btCalculate";
            this.btCalculate.Size = new System.Drawing.Size(75, 23);
            this.btCalculate.TabIndex = 2;
            this.btCalculate.Text = "Calculate";
            this.btCalculate.UseVisualStyleBackColor = true;
            this.btCalculate.Click += new System.EventHandler(this.btCalculate_Click);
            // 
            // functionsViewer
            // 
            this.functionsViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.functionsViewer.BackColor = System.Drawing.SystemColors.Control;
            this.functionsViewer.ForeColor = System.Drawing.Color.Black;
            this.functionsViewer.Location = new System.Drawing.Point(7, 300);
            this.functionsViewer.Name = "functionsViewer";
            this.functionsViewer.Size = new System.Drawing.Size(353, 113);
            this.functionsViewer.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btShowBreakpoints);
            this.groupBox1.Controls.Add(this.cbAddBreakpointsVerboseMessages);
            this.groupBox1.Controls.Add(this.btAddBreakpointOnAllMethods);
            this.groupBox1.Controls.Add(this.laNumberOfMethods);
            this.groupBox1.Controls.Add(this.laNumberOfTypes);
            this.groupBox1.Location = new System.Drawing.Point(7, 209);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(353, 85);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stats of selected assembly";
            // 
            // cbAddBreakpointsVerboseMessages
            // 
            this.cbAddBreakpointsVerboseMessages.AutoSize = true;
            this.cbAddBreakpointsVerboseMessages.Location = new System.Drawing.Point(247, 65);
            this.cbAddBreakpointsVerboseMessages.Name = "cbAddBreakpointsVerboseMessages";
            this.cbAddBreakpointsVerboseMessages.Size = new System.Drawing.Size(64, 17);
            this.cbAddBreakpointsVerboseMessages.TabIndex = 3;
            this.cbAddBreakpointsVerboseMessages.Text = "verbose";
            this.cbAddBreakpointsVerboseMessages.UseVisualStyleBackColor = true;
            // 
            // btAddBreakpointOnAllMethods
            // 
            this.btAddBreakpointOnAllMethods.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btAddBreakpointOnAllMethods.Location = new System.Drawing.Point(246, 11);
            this.btAddBreakpointOnAllMethods.Name = "btAddBreakpointOnAllMethods";
            this.btAddBreakpointOnAllMethods.Size = new System.Drawing.Size(101, 48);
            this.btAddBreakpointOnAllMethods.TabIndex = 2;
            this.btAddBreakpointOnAllMethods.Text = "Add breakpoint on all methods";
            this.btAddBreakpointOnAllMethods.UseVisualStyleBackColor = true;
            this.btAddBreakpointOnAllMethods.Click += new System.EventHandler(this.btAddBreakpointOnAllMethods_Click);
            // 
            // laNumberOfMethods
            // 
            this.laNumberOfMethods.AutoSize = true;
            this.laNumberOfMethods.Location = new System.Drawing.Point(7, 43);
            this.laNumberOfMethods.Name = "laNumberOfMethods";
            this.laNumberOfMethods.Size = new System.Drawing.Size(16, 13);
            this.laNumberOfMethods.TabIndex = 1;
            this.laNumberOfMethods.Text = "...";
            // 
            // laNumberOfTypes
            // 
            this.laNumberOfTypes.AutoSize = true;
            this.laNumberOfTypes.Location = new System.Drawing.Point(7, 20);
            this.laNumberOfTypes.Name = "laNumberOfTypes";
            this.laNumberOfTypes.Size = new System.Drawing.Size(16, 13);
            this.laNumberOfTypes.TabIndex = 0;
            this.laNumberOfTypes.Text = "...";
            // 
            // btShowBreakpoints
            // 
            this.btShowBreakpoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btShowBreakpoints.Location = new System.Drawing.Point(182, 11);
            this.btShowBreakpoints.Name = "btShowBreakpoints";
            this.btShowBreakpoints.Size = new System.Drawing.Size(58, 48);
            this.btShowBreakpoints.TabIndex = 4;
            this.btShowBreakpoints.Text = "Show";
            this.btShowBreakpoints.UseVisualStyleBackColor = true;
            this.btShowBreakpoints.Click += new System.EventHandler(this.btShowBreakpoints_Click);
            // 
            // ascx_BreakpointCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.functionsViewer);
            this.Controls.Add(this.btCalculate);
            this.Controls.Add(this.lbModulesInDebuggeeProcess);
            this.Controls.Add(this.label1);
            this.Name = "ascx_BreakpointCreator";
            this.Size = new System.Drawing.Size(363, 416);
            this.Load += new System.EventHandler(this.ascx_BreakpointCreator_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbModulesInDebuggeeProcess;
        private System.Windows.Forms.Button btCalculate;
        private ascx_FunctionsViewer functionsViewer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label laNumberOfMethods;
        private System.Windows.Forms.Label laNumberOfTypes;
        private System.Windows.Forms.Button btAddBreakpointOnAllMethods;
        private System.Windows.Forms.CheckBox cbAddBreakpointsVerboseMessages;
        private System.Windows.Forms.Button btShowBreakpoints;
    }
}
