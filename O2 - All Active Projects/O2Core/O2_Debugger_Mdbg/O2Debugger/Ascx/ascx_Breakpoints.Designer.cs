// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
namespace O2.Debugger.Mdbg.O2Debugger.Ascx
{
    partial class ascx_Breakpoints
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
            this.tbBreakPointSignature = new System.Windows.Forms.TextBox();
            this.lbRemoveSelectedBreakpoint = new System.Windows.Forms.LinkLabel();
            this.llRefreshBreakpointList = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.cbAutoAddBreakPointsOnSignatureChange = new System.Windows.Forms.CheckBox();
            this.btCreateBreakPoint = new System.Windows.Forms.Button();
            this.lbCurrentBreakpoints = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbAutoContinueOnBreakPointEvent = new System.Windows.Forms.CheckBox();
            this.llDeleteAllBreakpoints = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // tbBreakPointSignature
            // 
            this.tbBreakPointSignature.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBreakPointSignature.Enabled = false;
            this.tbBreakPointSignature.Location = new System.Drawing.Point(118, 7);
            this.tbBreakPointSignature.Name = "tbBreakPointSignature";
            this.tbBreakPointSignature.Size = new System.Drawing.Size(319, 20);
            this.tbBreakPointSignature.TabIndex = 33;
            this.tbBreakPointSignature.TextChanged += new System.EventHandler(this.tbBreakPointSignature_TextChanged);
            // 
            // lbRemoveSelectedBreakpoint
            // 
            this.lbRemoveSelectedBreakpoint.AutoSize = true;
            this.lbRemoveSelectedBreakpoint.Enabled = false;
            this.lbRemoveSelectedBreakpoint.Location = new System.Drawing.Point(413, 56);
            this.lbRemoveSelectedBreakpoint.Name = "lbRemoveSelectedBreakpoint";
            this.lbRemoveSelectedBreakpoint.Size = new System.Drawing.Size(146, 13);
            this.lbRemoveSelectedBreakpoint.TabIndex = 35;
            this.lbRemoveSelectedBreakpoint.TabStop = true;
            this.lbRemoveSelectedBreakpoint.Text = "Remove Selected Breakpoint";
            this.lbRemoveSelectedBreakpoint.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbRemoveSelectedBreakpoint_LinkClicked);
            // 
            // llRefreshBreakpointList
            // 
            this.llRefreshBreakpointList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llRefreshBreakpointList.AutoSize = true;
            this.llRefreshBreakpointList.Location = new System.Drawing.Point(442, 88);
            this.llRefreshBreakpointList.Name = "llRefreshBreakpointList";
            this.llRefreshBreakpointList.Size = new System.Drawing.Size(117, 13);
            this.llRefreshBreakpointList.TabIndex = 34;
            this.llRefreshBreakpointList.TabStop = true;
            this.llRefreshBreakpointList.Text = "Refresh Breakpoint List";
            this.llRefreshBreakpointList.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llRefreshBreakpointList_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Current Breakpoints";
            // 
            // cbAutoAddBreakPointsOnSignatureChange
            // 
            this.cbAutoAddBreakPointsOnSignatureChange.AutoSize = true;
            this.cbAutoAddBreakPointsOnSignatureChange.Checked = true;
            this.cbAutoAddBreakPointsOnSignatureChange.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoAddBreakPointsOnSignatureChange.Location = new System.Drawing.Point(118, 29);
            this.cbAutoAddBreakPointsOnSignatureChange.Name = "cbAutoAddBreakPointsOnSignatureChange";
            this.cbAutoAddBreakPointsOnSignatureChange.Size = new System.Drawing.Size(227, 17);
            this.cbAutoAddBreakPointsOnSignatureChange.TabIndex = 37;
            this.cbAutoAddBreakPointsOnSignatureChange.Text = "Auto Add BreakPoint on Signature change";
            this.cbAutoAddBreakPointsOnSignatureChange.UseVisualStyleBackColor = true;
            // 
            // btCreateBreakPoint
            // 
            this.btCreateBreakPoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCreateBreakPoint.Location = new System.Drawing.Point(456, 5);
            this.btCreateBreakPoint.Name = "btCreateBreakPoint";
            this.btCreateBreakPoint.Size = new System.Drawing.Size(103, 23);
            this.btCreateBreakPoint.TabIndex = 36;
            this.btCreateBreakPoint.Text = "Create BreakPoint";
            this.btCreateBreakPoint.UseVisualStyleBackColor = true;
            this.btCreateBreakPoint.Click += new System.EventHandler(this.btCreateBreakPoint_Click);
            // 
            // lbCurrentBreakpoints
            // 
            this.lbCurrentBreakpoints.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCurrentBreakpoints.FormattingEnabled = true;
            this.lbCurrentBreakpoints.Location = new System.Drawing.Point(3, 104);
            this.lbCurrentBreakpoints.Name = "lbCurrentBreakpoints";
            this.lbCurrentBreakpoints.Size = new System.Drawing.Size(556, 199);
            this.lbCurrentBreakpoints.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "BreakPoint signature:";
            // 
            // cbAutoContinueOnBreakPointEvent
            // 
            this.cbAutoContinueOnBreakPointEvent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAutoContinueOnBreakPointEvent.AutoSize = true;
            this.cbAutoContinueOnBreakPointEvent.Location = new System.Drawing.Point(118, 56);
            this.cbAutoContinueOnBreakPointEvent.Name = "cbAutoContinueOnBreakPointEvent";
            this.cbAutoContinueOnBreakPointEvent.Size = new System.Drawing.Size(192, 17);
            this.cbAutoContinueOnBreakPointEvent.TabIndex = 38;
            this.cbAutoContinueOnBreakPointEvent.Text = "Auto Continue on Breakpoint event";
            this.cbAutoContinueOnBreakPointEvent.UseVisualStyleBackColor = true;
            this.cbAutoContinueOnBreakPointEvent.CheckedChanged += new System.EventHandler(this.cbAutoContinueOnBreakPointEvent_CheckedChanged);
            // 
            // llDeleteAllBreakpoints
            // 
            this.llDeleteAllBreakpoints.AutoSize = true;
            this.llDeleteAllBreakpoints.Location = new System.Drawing.Point(110, 88);
            this.llDeleteAllBreakpoints.Name = "llDeleteAllBreakpoints";
            this.llDeleteAllBreakpoints.Size = new System.Drawing.Size(119, 13);
            this.llDeleteAllBreakpoints.TabIndex = 39;
            this.llDeleteAllBreakpoints.TabStop = true;
            this.llDeleteAllBreakpoints.Text = "Delete ALL Breakpoints";
            this.llDeleteAllBreakpoints.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDeleteAllBreakpoints_LinkClicked);
            // 
            // ascx_Breakpoints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.llDeleteAllBreakpoints);
            this.Controls.Add(this.cbAutoContinueOnBreakPointEvent);
            this.Controls.Add(this.tbBreakPointSignature);
            this.Controls.Add(this.lbRemoveSelectedBreakpoint);
            this.Controls.Add(this.llRefreshBreakpointList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbAutoAddBreakPointsOnSignatureChange);
            this.Controls.Add(this.btCreateBreakPoint);
            this.Controls.Add(this.lbCurrentBreakpoints);
            this.Controls.Add(this.label1);
            this.Name = "ascx_Breakpoints";
            this.Size = new System.Drawing.Size(562, 310);
            this.Load += new System.EventHandler(this.ascx_Breakpoints_Load);
            this.Enter += new System.EventHandler(this.ascx_Breakpoints_Enter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbBreakPointSignature;
        private System.Windows.Forms.LinkLabel lbRemoveSelectedBreakpoint;
        private System.Windows.Forms.LinkLabel llRefreshBreakpointList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbAutoAddBreakPointsOnSignatureChange;
        private System.Windows.Forms.Button btCreateBreakPoint;
        private System.Windows.Forms.ListBox lbCurrentBreakpoints;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbAutoContinueOnBreakPointEvent;
        private System.Windows.Forms.LinkLabel llDeleteAllBreakpoints;
    }
}
