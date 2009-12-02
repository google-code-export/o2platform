// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
namespace O2.Tool.JoinTraces.ascx
{
    partial class ascx_JoinTracesOnInterfaces
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ascx_JoinTracesOnInterfaces));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.findingsViewer_BaseFindings = new O2.Views.ASCX.O2Findings.ascx_FindingsViewer();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.btCalculateSourcesMappedToInterfaces = new System.Windows.Forms.Button();
            this.findingsViewer_SourcesMappedToInterfaces = new O2.Views.ASCX.O2Findings.ascx_FindingsViewer();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.findingsViewer_DynamicJoin = new O2.Views.ASCX.O2Findings.ascx_FindingsViewer();
            this.label4 = new System.Windows.Forms.Label();
            this.findingsViewers_withSourcesForInterfaces = new O2.Views.ASCX.O2Findings.ascx_FindingsViewer();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.label6 = new System.Windows.Forms.Label();
            this.findingsViewer_AutoMappingOfInterfaces = new O2.Views.ASCX.O2Findings.ascx_FindingsViewer();
            this.cirDataViewer = new O2.Core.CIR.Ascx.ascx_CirDataViewer();
            this.cbIncludeOriginalFindings = new System.Windows.Forms.CheckBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
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
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(829, 405);
            this.splitContainer1.SplitterDistance = 275;
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
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.findingsViewer_BaseFindings);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Panel2.Controls.Add(this.cirDataViewer);
            this.splitContainer2.Size = new System.Drawing.Size(275, 405);
            this.splitContainer2.SplitterDistance = 172;
            this.splitContainer2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Base Findings";
            // 
            // findingsViewer_BaseFindings
            // 
            this.findingsViewer_BaseFindings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.findingsViewer_BaseFindings.Location = new System.Drawing.Point(4, 23);
            this.findingsViewer_BaseFindings.Name = "findingsViewer_BaseFindings";
            this.findingsViewer_BaseFindings.Size = new System.Drawing.Size(258, 142);
            this.findingsViewer_BaseFindings.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Base Cir";
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer5);
            this.splitContainer3.Size = new System.Drawing.Size(550, 405);
            this.splitContainer3.SplitterDistance = 193;
            this.splitContainer3.TabIndex = 0;
            // 
            // splitContainer4
            // 
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.btCalculateSourcesMappedToInterfaces);
            this.splitContainer4.Panel1.Controls.Add(this.findingsViewer_SourcesMappedToInterfaces);
            this.splitContainer4.Panel1.Controls.Add(this.label3);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.cbIncludeOriginalFindings);
            this.splitContainer4.Panel2.Controls.Add(this.findingsViewer_AutoMappingOfInterfaces);
            this.splitContainer4.Panel2.Controls.Add(this.label6);
            this.splitContainer4.Size = new System.Drawing.Size(550, 193);
            this.splitContainer4.SplitterDistance = 275;
            this.splitContainer4.TabIndex = 0;
            // 
            // btCalculateSourcesMappedToInterfaces
            // 
            this.btCalculateSourcesMappedToInterfaces.Location = new System.Drawing.Point(6, 23);
            this.btCalculateSourcesMappedToInterfaces.Name = "btCalculateSourcesMappedToInterfaces";
            this.btCalculateSourcesMappedToInterfaces.Size = new System.Drawing.Size(174, 23);
            this.btCalculateSourcesMappedToInterfaces.TabIndex = 4;
            this.btCalculateSourcesMappedToInterfaces.Text = "Calculate";
            this.btCalculateSourcesMappedToInterfaces.UseVisualStyleBackColor = true;
            this.btCalculateSourcesMappedToInterfaces.Click += new System.EventHandler(this.btCalculateSourcesMappedToInterfaces_Click);
            // 
            // findingsViewer_SourcesMappedToInterfaces
            // 
            this.findingsViewer_SourcesMappedToInterfaces.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.findingsViewer_SourcesMappedToInterfaces.Location = new System.Drawing.Point(6, 54);
            this.findingsViewer_SourcesMappedToInterfaces.Name = "findingsViewer_SourcesMappedToInterfaces";
            this.findingsViewer_SourcesMappedToInterfaces.Size = new System.Drawing.Size(266, 132);
            this.findingsViewer_SourcesMappedToInterfaces.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Sources mapped to Interfaces";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(225, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Findings with calls for current lost sink";
            // 
            // findingsViewer_DynamicJoin
            // 
            this.findingsViewer_DynamicJoin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.findingsViewer_DynamicJoin.Location = new System.Drawing.Point(3, 22);
            this.findingsViewer_DynamicJoin.Name = "findingsViewer_DynamicJoin";
            this.findingsViewer_DynamicJoin.Size = new System.Drawing.Size(176, 179);
            this.findingsViewer_DynamicJoin.TabIndex = 5;
            this.findingsViewer_DynamicJoin._onTraceSelected += new O2.DotNetWrappers.DotNet.O2Thread.FuncVoidT1<O2.Kernel.Interfaces.O2Findings.IO2Trace>(this.findingsViewer_DynamicJoin__onTraceSelected);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Dynamic Join";
            // 
            // findingsViewers_withSourcesForInterfaces
            // 
            this.findingsViewers_withSourcesForInterfaces.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.findingsViewers_withSourcesForInterfaces.Location = new System.Drawing.Point(3, 22);
            this.findingsViewers_withSourcesForInterfaces.Name = "findingsViewers_withSourcesForInterfaces";
            this.findingsViewers_withSourcesForInterfaces.Size = new System.Drawing.Size(354, 179);
            this.findingsViewers_withSourcesForInterfaces.TabIndex = 6;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.findingsViewer_DynamicJoin);
            this.splitContainer5.Panel1.Controls.Add(this.label4);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.label5);
            this.splitContainer5.Panel2.Controls.Add(this.findingsViewers_withSourcesForInterfaces);
            this.splitContainer5.Size = new System.Drawing.Size(546, 204);
            this.splitContainer5.SplitterDistance = 182;
            this.splitContainer5.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(161, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Auto Mapping of Interfaces";
            // 
            // findingsViewer_AutoMappingOfInterfaces
            // 
            this.findingsViewer_AutoMappingOfInterfaces.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.findingsViewer_AutoMappingOfInterfaces.Location = new System.Drawing.Point(3, 54);
            this.findingsViewer_AutoMappingOfInterfaces.Name = "findingsViewer_AutoMappingOfInterfaces";
            this.findingsViewer_AutoMappingOfInterfaces.Size = new System.Drawing.Size(261, 132);
            this.findingsViewer_AutoMappingOfInterfaces.TabIndex = 5;
            // 
            // cirDataViewer
            // 
            this.cirDataViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cirDataViewer.cirDataAnalysis = ((O2.Kernel.Interfaces.CIR.ICirDataAnalysis)(resources.GetObject("cirDataViewer.cirDataAnalysis")));
            this.cirDataViewer.Location = new System.Drawing.Point(1, 22);
            this.cirDataViewer.Name = "cirDataViewer";
            this.cirDataViewer.Size = new System.Drawing.Size(267, 200);
            this.cirDataViewer.TabIndex = 0;
            // 
            // cbIncludeOriginalFindings
            // 
            this.cbIncludeOriginalFindings.AutoSize = true;
            this.cbIncludeOriginalFindings.Location = new System.Drawing.Point(3, 31);
            this.cbIncludeOriginalFindings.Name = "cbIncludeOriginalFindings";
            this.cbIncludeOriginalFindings.Size = new System.Drawing.Size(141, 17);
            this.cbIncludeOriginalFindings.TabIndex = 6;
            this.cbIncludeOriginalFindings.Text = "Include Original Findings";
            this.cbIncludeOriginalFindings.UseVisualStyleBackColor = true;
            // 
            // ascx_JoinTracesOnInterfaces
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ascx_JoinTracesOnInterfaces";
            this.Size = new System.Drawing.Size(829, 405);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel1.PerformLayout();
            this.splitContainer5.Panel2.ResumeLayout(false);
            this.splitContainer5.Panel2.PerformLayout();
            this.splitContainer5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label1;
        private O2.Views.ASCX.O2Findings.ascx_FindingsViewer findingsViewer_BaseFindings;
        private System.Windows.Forms.Label label2;
        private O2.Core.CIR.Ascx.ascx_CirDataViewer cirDataViewer;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Label label3;
        private O2.Views.ASCX.O2Findings.ascx_FindingsViewer findingsViewer_SourcesMappedToInterfaces;
        private System.Windows.Forms.Button btCalculateSourcesMappedToInterfaces;
        private O2.Views.ASCX.O2Findings.ascx_FindingsViewer findingsViewer_DynamicJoin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private O2.Views.ASCX.O2Findings.ascx_FindingsViewer findingsViewers_withSourcesForInterfaces;
        private O2.Views.ASCX.O2Findings.ascx_FindingsViewer findingsViewer_AutoMappingOfInterfaces;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.CheckBox cbIncludeOriginalFindings;
    }
}
