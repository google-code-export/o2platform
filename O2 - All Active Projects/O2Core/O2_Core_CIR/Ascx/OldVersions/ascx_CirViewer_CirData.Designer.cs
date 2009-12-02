// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Core.CIR.Ascx.OldVersions;
using O2.Kernel.CodeUtils;
using O2.Kernel.CodeUtils;
using O2.Views.ASCX.CoreControls;
using O2.Views.ASCX.DataViewers;

namespace O2.Core.CIR.Ascx
{
    partial class ascx_CirViewer_CirData
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
            this.scHost = new System.Windows.Forms.SplitContainer();
            this.scTop = new System.Windows.Forms.SplitContainer();
            this.cbOnlyShowClassesWithFunctionsWithControlFlowGraphs = new System.Windows.Forms.CheckBox();
            this.cbCalculateSuperClasses = new System.Windows.Forms.CheckBox();
            this.scBottom = new System.Windows.Forms.SplitContainer();
            this.asvp_Panels = new O2.Views.ASCX.CoreControls.ascx_SelectVisiblePanels();
            this.afv_Classes = new O2.Views.ASCX.DataViewers.ascx_FunctionsViewer();
            this.afv_SuperClasses = new O2.Views.ASCX.DataViewers.ascx_FunctionsViewer();
            this.acv_Class = new ascx_CirViewer_Class();
            this.acv_Signature = new O2.Core.CIR.Ascx.ascx_CirViewer_Signature();
            this.scHost.Panel1.SuspendLayout();
            this.scHost.Panel2.SuspendLayout();
            this.scHost.SuspendLayout();
            this.scTop.Panel1.SuspendLayout();
            this.scTop.Panel2.SuspendLayout();
            this.scTop.SuspendLayout();
            this.scBottom.Panel1.SuspendLayout();
            this.scBottom.Panel2.SuspendLayout();
            this.scBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // scHost
            // 
            this.scHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.scHost.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scHost.Location = new System.Drawing.Point(6, 41);
            this.scHost.Name = "scHost";
            this.scHost.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scHost.Panel1
            // 
            this.scHost.Panel1.Controls.Add(this.scTop);
            // 
            // scHost.Panel2
            // 
            this.scHost.Panel2.Controls.Add(this.scBottom);
            this.scHost.Size = new System.Drawing.Size(1011, 610);
            this.scHost.SplitterDistance = 200;
            this.scHost.TabIndex = 4;
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
            this.scTop.Panel1.Controls.Add(this.cbOnlyShowClassesWithFunctionsWithControlFlowGraphs);
            this.scTop.Panel1.Controls.Add(this.afv_Classes);
            // 
            // scTop.Panel2
            // 
            this.scTop.Panel2.Controls.Add(this.cbCalculateSuperClasses);
            this.scTop.Panel2.Controls.Add(this.afv_SuperClasses);
            this.scTop.Size = new System.Drawing.Size(1011, 200);
            this.scTop.SplitterDistance = 439;
            this.scTop.TabIndex = 0;
            // 
            // cbOnlyShowClassesWithFunctionsWithControlFlowGraphs
            // 
            this.cbOnlyShowClassesWithFunctionsWithControlFlowGraphs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbOnlyShowClassesWithFunctionsWithControlFlowGraphs.AutoSize = true;
            this.cbOnlyShowClassesWithFunctionsWithControlFlowGraphs.Checked = true;
            this.cbOnlyShowClassesWithFunctionsWithControlFlowGraphs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOnlyShowClassesWithFunctionsWithControlFlowGraphs.Location = new System.Drawing.Point(3, 176);
            this.cbOnlyShowClassesWithFunctionsWithControlFlowGraphs.Name = "cbOnlyShowClassesWithFunctionsWithControlFlowGraphs";
            this.cbOnlyShowClassesWithFunctionsWithControlFlowGraphs.Size = new System.Drawing.Size(337, 17);
            this.cbOnlyShowClassesWithFunctionsWithControlFlowGraphs.TabIndex = 1;
            this.cbOnlyShowClassesWithFunctionsWithControlFlowGraphs.Text = "Only show classes that contains functions with control flow graphs";
            this.cbOnlyShowClassesWithFunctionsWithControlFlowGraphs.UseVisualStyleBackColor = true;
            this.cbOnlyShowClassesWithFunctionsWithControlFlowGraphs.CheckedChanged += new System.EventHandler(this.cbOnlyShowClassesWithFunctionsWithControlFlowGraphs_CheckedChanged);
            // 
            // cbCalculateSuperClasses
            // 
            this.cbCalculateSuperClasses.AutoSize = true;
            this.cbCalculateSuperClasses.Location = new System.Drawing.Point(3, 4);
            this.cbCalculateSuperClasses.Name = "cbCalculateSuperClasses";
            this.cbCalculateSuperClasses.Size = new System.Drawing.Size(196, 17);
            this.cbCalculateSuperClasses.TabIndex = 2;
            this.cbCalculateSuperClasses.Text = "Calculate SuperClasses (recursively)";
            this.cbCalculateSuperClasses.UseVisualStyleBackColor = true;
            this.cbCalculateSuperClasses.CheckedChanged += new System.EventHandler(this.cbCalculateSuperClasses_CheckedChanged);
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
            this.scBottom.Panel1.Controls.Add(this.acv_Class);
            // 
            // scBottom.Panel2
            // 
            this.scBottom.Panel2.Controls.Add(this.acv_Signature);
            this.scBottom.Size = new System.Drawing.Size(1011, 406);
            this.scBottom.SplitterDistance = 436;
            this.scBottom.TabIndex = 1;
            // 
            // asvp_Panels
            // 
            this.asvp_Panels.BackColor = System.Drawing.SystemColors.Control;
            this.asvp_Panels.ForeColor = System.Drawing.Color.Black;
            this.asvp_Panels.Location = new System.Drawing.Point(3, 0);
            this.asvp_Panels.Name = "asvp_Panels";
            this.asvp_Panels.Size = new System.Drawing.Size(570, 40);
            this.asvp_Panels.TabIndex = 103;
            this.asvp_Panels.Load += new System.EventHandler(this.asvp_Panels_Load);
            // 
            // afv_Classes
            // 
            this.afv_Classes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.afv_Classes.BackColor = System.Drawing.SystemColors.Control;
            this.afv_Classes.ForeColor = System.Drawing.Color.Black;
            this.afv_Classes.Location = new System.Drawing.Point(1, 4);
            this.afv_Classes.Name = "afv_Classes";
            this.afv_Classes.Size = new System.Drawing.Size(429, 167);
            this.afv_Classes.TabIndex = 0;
            // 
            // afv_SuperClasses
            // 
            this.afv_SuperClasses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.afv_SuperClasses.BackColor = System.Drawing.SystemColors.Control;
            this.afv_SuperClasses.ForeColor = System.Drawing.Color.Black;
            this.afv_SuperClasses.Location = new System.Drawing.Point(2, 26);
            this.afv_SuperClasses.Name = "afv_SuperClasses";
            this.afv_SuperClasses.Size = new System.Drawing.Size(564, 167);
            this.afv_SuperClasses.TabIndex = 0;
            // 
            // acv_Class
            // 
            this.acv_Class.BackColor = System.Drawing.SystemColors.Control;
            this.acv_Class.Dock = System.Windows.Forms.DockStyle.Fill;
            this.acv_Class.ForeColor = System.Drawing.Color.Black;
            this.acv_Class.Location = new System.Drawing.Point(0, 0);
            this.acv_Class.Name = "acv_Class";
            this.acv_Class.Size = new System.Drawing.Size(432, 402);
            this.acv_Class.TabIndex = 0;
            // 
            // acv_Signature
            // 
            this.acv_Signature.BackColor = System.Drawing.SystemColors.Control;
            this.acv_Signature.Dock = System.Windows.Forms.DockStyle.Fill;
            this.acv_Signature.ForeColor = System.Drawing.Color.Black;
            this.acv_Signature.Location = new System.Drawing.Point(0, 0);
            this.acv_Signature.Name = "acv_Signature";
            this.acv_Signature.Size = new System.Drawing.Size(567, 402);
            this.acv_Signature.TabIndex = 0;
            // 
            // ascx_CirViewer_CirData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.asvp_Panels);
            this.Controls.Add(this.scHost);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ascx_CirViewer_CirData";
            this.Size = new System.Drawing.Size(1020, 654);
            this.Load += new System.EventHandler(this.ascx_CirViewer_O2CirData_Load);
            this.scHost.Panel1.ResumeLayout(false);
            this.scHost.Panel2.ResumeLayout(false);
            this.scHost.ResumeLayout(false);
            this.scTop.Panel1.ResumeLayout(false);
            this.scTop.Panel1.PerformLayout();
            this.scTop.Panel2.ResumeLayout(false);
            this.scTop.Panel2.PerformLayout();
            this.scTop.ResumeLayout(false);
            this.scBottom.Panel1.ResumeLayout(false);
            this.scBottom.Panel2.ResumeLayout(false);
            this.scBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scHost;
        private System.Windows.Forms.SplitContainer scTop;
        private ascx_FunctionsViewer afv_Classes;
        private ascx_FunctionsViewer afv_SuperClasses;
        private ascx_CirViewer_Class acv_Class;
        private ascx_CirViewer_Signature acv_Signature;
        private ascx_SelectVisiblePanels asvp_Panels;
        private System.Windows.Forms.SplitContainer scBottom;
        private System.Windows.Forms.CheckBox cbOnlyShowClassesWithFunctionsWithControlFlowGraphs;
        private System.Windows.Forms.CheckBox cbCalculateSuperClasses;
    }
}
