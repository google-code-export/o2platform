using O2.Core.CIR.Ascx.OldVersions;
using O2.Views.ASCX.CoreControls;
using O2.Views.ASCX.DataViewers;

namespace O2.Core.CIR.Ascx
{
    partial class ascx_CirViewer_Signature
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
            this.lbClassesBeingViewed = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lBoxVariables = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lbBoxSsaVariables = new System.Windows.Forms.ListBox();
            this.asv_SelectVisiblePanels = new ascx_SelectVisiblePanels();
            this.scBottom = new System.Windows.Forms.SplitContainer();
            this.afv_IsCalledBy = new ascx_FunctionsViewer();
            this.scHostControl = new System.Windows.Forms.SplitContainer();
            this.scTop = new System.Windows.Forms.SplitContainer();
            this.afv_Calls = new ascx_FunctionsViewer();
            this.scViewersAndVariables = new System.Windows.Forms.SplitContainer();
            this.label4 = new System.Windows.Forms.Label();
            this.asv_CalledFunctions = new ascx_SignatureViewer();
            this.asv_IsCalledBy = new ascx_SignatureViewer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.wbControlFlowGraphsOFSelectedMethod = new System.Windows.Forms.WebBrowser();
            this.scBottom.Panel1.SuspendLayout();
            this.scBottom.Panel2.SuspendLayout();
            this.scBottom.SuspendLayout();
            this.scHostControl.Panel1.SuspendLayout();
            this.scHostControl.Panel2.SuspendLayout();
            this.scHostControl.SuspendLayout();
            this.scTop.Panel1.SuspendLayout();
            this.scTop.Panel2.SuspendLayout();
            this.scTop.SuspendLayout();
            this.scViewersAndVariables.Panel1.SuspendLayout();
            this.scViewersAndVariables.Panel2.SuspendLayout();
            this.scViewersAndVariables.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbClassesBeingViewed
            // 
            this.lbClassesBeingViewed.AutoSize = true;
            this.lbClassesBeingViewed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbClassesBeingViewed.Location = new System.Drawing.Point(169, 51);
            this.lbClassesBeingViewed.Name = "lbClassesBeingViewed";
            this.lbClassesBeingViewed.Size = new System.Drawing.Size(19, 13);
            this.lbClassesBeingViewed.TabIndex = 3;
            this.lbClassesBeingViewed.Text = "...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "CirViewer For Signature(s):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Variables";
            // 
            // lBoxVariables
            // 
            this.lBoxVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                               | System.Windows.Forms.AnchorStyles.Left)
                                                                              | System.Windows.Forms.AnchorStyles.Right)));
            this.lBoxVariables.FormattingEnabled = true;
            this.lBoxVariables.Location = new System.Drawing.Point(4, 18);
            this.lBoxVariables.Name = "lBoxVariables";
            this.lBoxVariables.Size = new System.Drawing.Size(150, 82);
            this.lBoxVariables.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "SsaVariables";
            // 
            // lbBoxSsaVariables
            // 
            this.lbBoxSsaVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                   | System.Windows.Forms.AnchorStyles.Left)
                                                                                  | System.Windows.Forms.AnchorStyles.Right)));
            this.lbBoxSsaVariables.FormattingEnabled = true;
            this.lbBoxSsaVariables.Location = new System.Drawing.Point(5, 17);
            this.lbBoxSsaVariables.Name = "lbBoxSsaVariables";
            this.lbBoxSsaVariables.Size = new System.Drawing.Size(149, 95);
            this.lbBoxSsaVariables.TabIndex = 2;
            // 
            // asv_SelectVisiblePanels
            // 
            this.asv_SelectVisiblePanels.BackColor = System.Drawing.SystemColors.Control;
            this.asv_SelectVisiblePanels.ForeColor = System.Drawing.Color.Black;
            this.asv_SelectVisiblePanels.Location = new System.Drawing.Point(8, 4);
            this.asv_SelectVisiblePanels.Name = "asv_SelectVisiblePanels";
            this.asv_SelectVisiblePanels.Size = new System.Drawing.Size(570, 40);
            this.asv_SelectVisiblePanels.TabIndex = 5;
            // 
            // scBottom
            // 
            this.scBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scBottom.Location = new System.Drawing.Point(0, 0);
            this.scBottom.Name = "scBottom";
            // 
            // scBottom.Panel1
            // 
            this.scBottom.Panel1.Controls.Add(this.afv_IsCalledBy);
            // 
            // scBottom.Panel2
            // 
            this.scBottom.Panel2.Controls.Add(this.asv_IsCalledBy);
            this.scBottom.Size = new System.Drawing.Size(716, 190);
            this.scBottom.SplitterDistance = 337;
            this.scBottom.TabIndex = 0;
            // 
            // afv_IsCalledBy
            // 
            this.afv_IsCalledBy.BackColor = System.Drawing.SystemColors.Control;
            this.afv_IsCalledBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.afv_IsCalledBy.ForeColor = System.Drawing.Color.Black;
            this.afv_IsCalledBy.Location = new System.Drawing.Point(0, 0);
            this.afv_IsCalledBy.Name = "afv_IsCalledBy";
            this.afv_IsCalledBy.Size = new System.Drawing.Size(333, 186);
            this.afv_IsCalledBy.TabIndex = 2;
            // 
            // scHostControl
            // 
            this.scHostControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scHostControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scHostControl.Location = new System.Drawing.Point(0, 0);
            this.scHostControl.Name = "scHostControl";
            this.scHostControl.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scHostControl.Panel1
            // 
            this.scHostControl.Panel1.Controls.Add(this.scTop);
            // 
            // scHostControl.Panel2
            // 
            this.scHostControl.Panel2.Controls.Add(this.scBottom);
            this.scHostControl.Size = new System.Drawing.Size(716, 386);
            this.scHostControl.SplitterDistance = 192;
            this.scHostControl.TabIndex = 6;
            // 
            // scTop
            // 
            this.scTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scTop.Location = new System.Drawing.Point(0, 0);
            this.scTop.Name = "scTop";
            // 
            // scTop.Panel1
            // 
            this.scTop.Panel1.Controls.Add(this.afv_Calls);
            // 
            // scTop.Panel2
            // 
            this.scTop.Panel2.Controls.Add(this.asv_CalledFunctions);
            this.scTop.Size = new System.Drawing.Size(716, 192);
            this.scTop.SplitterDistance = 338;
            this.scTop.TabIndex = 0;
            // 
            // afv_Calls
            // 
            this.afv_Calls.BackColor = System.Drawing.SystemColors.Control;
            this.afv_Calls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.afv_Calls.ForeColor = System.Drawing.Color.Black;
            this.afv_Calls.Location = new System.Drawing.Point(0, 0);
            this.afv_Calls.Name = "afv_Calls";
            this.afv_Calls.Size = new System.Drawing.Size(334, 188);
            this.afv_Calls.TabIndex = 1;
            // 
            // scViewersAndVariables
            // 
            this.scViewersAndVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                       | System.Windows.Forms.AnchorStyles.Left)
                                                                                      | System.Windows.Forms.AnchorStyles.Right)));
            this.scViewersAndVariables.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scViewersAndVariables.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scViewersAndVariables.Location = new System.Drawing.Point(8, 76);
            this.scViewersAndVariables.Name = "scViewersAndVariables";
            // 
            // scViewersAndVariables.Panel1
            // 
            this.scViewersAndVariables.Panel1.Controls.Add(this.scHostControl);
            // 
            // scViewersAndVariables.Panel2
            // 
            this.scViewersAndVariables.Panel2.Controls.Add(this.splitContainer1);
            this.scViewersAndVariables.Size = new System.Drawing.Size(881, 386);
            this.scViewersAndVariables.SplitterDistance = 716;
            this.scViewersAndVariables.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "ControlFlow Graphs";
            // 
            // asv_CalledFunctions
            // 
            this.asv_CalledFunctions.BackColor = System.Drawing.SystemColors.Control;
            this.asv_CalledFunctions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.asv_CalledFunctions.ForeColor = System.Drawing.Color.Black;
            this.asv_CalledFunctions.Location = new System.Drawing.Point(0, 0);
            this.asv_CalledFunctions.Name = "asv_CalledFunctions";
            this.asv_CalledFunctions.Size = new System.Drawing.Size(370, 188);
            this.asv_CalledFunctions.TabIndex = 7;
            // 
            // asv_IsCalledBy
            // 
            this.asv_IsCalledBy.BackColor = System.Drawing.SystemColors.Control;
            this.asv_IsCalledBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.asv_IsCalledBy.ForeColor = System.Drawing.Color.Transparent;
            this.asv_IsCalledBy.Location = new System.Drawing.Point(0, 0);
            this.asv_IsCalledBy.Name = "asv_IsCalledBy";
            this.asv_IsCalledBy.Size = new System.Drawing.Size(371, 186);
            this.asv_IsCalledBy.TabIndex = 8;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lBoxVariables);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(161, 386);
            this.splitContainer1.SplitterDistance = 274;
            this.splitContainer1.TabIndex = 8;
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
            this.splitContainer2.Panel1.Controls.Add(this.lbBoxSsaVariables);
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.wbControlFlowGraphsOFSelectedMethod);
            this.splitContainer2.Panel2.Controls.Add(this.label4);
            this.splitContainer2.Size = new System.Drawing.Size(161, 274);
            this.splitContainer2.SplitterDistance = 121;
            this.splitContainer2.TabIndex = 2;
            // 
            // wbControlFlowGraphsOFSelectedMethod
            // 
            this.wbControlFlowGraphsOFSelectedMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                                     | System.Windows.Forms.AnchorStyles.Left)
                                                                                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.wbControlFlowGraphsOFSelectedMethod.Location = new System.Drawing.Point(5, 20);
            this.wbControlFlowGraphsOFSelectedMethod.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbControlFlowGraphsOFSelectedMethod.Name = "wbControlFlowGraphsOFSelectedMethod";
            this.wbControlFlowGraphsOFSelectedMethod.Size = new System.Drawing.Size(149, 127);
            this.wbControlFlowGraphsOFSelectedMethod.TabIndex = 6;
            // 
            // ascx_CirViewer_Signature
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.scViewersAndVariables);
            this.Controls.Add(this.asv_SelectVisiblePanels);
            this.Controls.Add(this.lbClassesBeingViewed);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ascx_CirViewer_Signature";
            this.Size = new System.Drawing.Size(892, 468);
            this.Load += new System.EventHandler(this.ascx_CirViewer_Signature_Load);
            this.scBottom.Panel1.ResumeLayout(false);
            this.scBottom.Panel2.ResumeLayout(false);
            this.scBottom.ResumeLayout(false);
            this.scHostControl.Panel1.ResumeLayout(false);
            this.scHostControl.Panel2.ResumeLayout(false);
            this.scHostControl.ResumeLayout(false);
            this.scTop.Panel1.ResumeLayout(false);
            this.scTop.Panel2.ResumeLayout(false);
            this.scTop.ResumeLayout(false);
            this.scViewersAndVariables.Panel1.ResumeLayout(false);
            this.scViewersAndVariables.Panel2.ResumeLayout(false);
            this.scViewersAndVariables.ResumeLayout(false);
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
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbClassesBeingViewed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lBoxVariables;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lbBoxSsaVariables;
        private ascx_SelectVisiblePanels asv_SelectVisiblePanels;
        private System.Windows.Forms.SplitContainer scBottom;
        private System.Windows.Forms.SplitContainer scHostControl;
        private System.Windows.Forms.SplitContainer scTop;
        private ascx_FunctionsViewer afv_Calls;
        private ascx_SignatureViewer asv_CalledFunctions;
        private ascx_FunctionsViewer afv_IsCalledBy;
        private ascx_SignatureViewer asv_IsCalledBy;
        private System.Windows.Forms.SplitContainer scViewersAndVariables;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.WebBrowser wbControlFlowGraphsOFSelectedMethod;
    }
}