using O2.External.O2Mono.Ascx;
using O2.External.SharpDevelop.Ascx;

namespace O2.Rnd.Misc_Code_Experiments
{
    partial class ascx_NUnitWrapper
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
            this.tcNunit = new System.Windows.Forms.TabControl();
            this.tpNUnitWrapper = new System.Windows.Forms.TabPage();
            this.assemblyInvoke = new ascx_AssemblyInvoke();
            this.tvAssembliesInAppDomain = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.btStartNUnit = new System.Windows.Forms.Button();
            this.tpConfig = new System.Windows.Forms.TabPage();
            this.llGoToNUnitWrapper = new System.Windows.Forms.LinkLabel();
            this.lbCouldntFindNUnitError = new System.Windows.Forms.Label();
            this.tbPathToNUnitBinFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btRefreshAppDomainAssemblyList = new System.Windows.Forms.Button();
            this.tcNunit.SuspendLayout();
            this.tpNUnitWrapper.SuspendLayout();
            this.tpConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcNunit
            // 
            this.tcNunit.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tcNunit.Controls.Add(this.tpNUnitWrapper);
            this.tcNunit.Controls.Add(this.tpConfig);
            this.tcNunit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcNunit.Location = new System.Drawing.Point(0, 0);
            this.tcNunit.Name = "tcNunit";
            this.tcNunit.SelectedIndex = 0;
            this.tcNunit.Size = new System.Drawing.Size(761, 547);
            this.tcNunit.TabIndex = 0;
            // 
            // tpNUnitWrapper
            // 
            this.tpNUnitWrapper.Controls.Add(this.btRefreshAppDomainAssemblyList);
            this.tpNUnitWrapper.Controls.Add(this.assemblyInvoke);
            this.tpNUnitWrapper.Controls.Add(this.tvAssembliesInAppDomain);
            this.tpNUnitWrapper.Controls.Add(this.label1);
            this.tpNUnitWrapper.Controls.Add(this.btStartNUnit);
            this.tpNUnitWrapper.Location = new System.Drawing.Point(4, 4);
            this.tpNUnitWrapper.Name = "tpNUnitWrapper";
            this.tpNUnitWrapper.Padding = new System.Windows.Forms.Padding(3);
            this.tpNUnitWrapper.Size = new System.Drawing.Size(753, 521);
            this.tpNUnitWrapper.TabIndex = 0;
            this.tpNUnitWrapper.Text = "NUnit Wrapper";
            this.tpNUnitWrapper.UseVisualStyleBackColor = true;
            // 
            // assemblyInvoke
            // 
            this.assemblyInvoke.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                | System.Windows.Forms.AnchorStyles.Left)
                                                                               | System.Windows.Forms.AnchorStyles.Right)));
            this.assemblyInvoke.Location = new System.Drawing.Point(338, 89);
            this.assemblyInvoke.Name = "assemblyInvoke";
            this.assemblyInvoke.Size = new System.Drawing.Size(409, 429);
            this.assemblyInvoke.TabIndex = 4;
            // 
            // tvAssembliesInAppDomain
            // 
            this.tvAssembliesInAppDomain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tvAssembliesInAppDomain.Location = new System.Drawing.Point(31, 89);
            this.tvAssembliesInAppDomain.Name = "tvAssembliesInAppDomain";
            this.tvAssembliesInAppDomain.Size = new System.Drawing.Size(290, 426);
            this.tvAssembliesInAppDomain.TabIndex = 3;
            this.tvAssembliesInAppDomain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvAssembliesInAppDomain_AfterSelect);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Assemblies In AppDomain";
            // 
            // btStartNUnit
            // 
            this.btStartNUnit.Location = new System.Drawing.Point(31, 21);
            this.btStartNUnit.Name = "btStartNUnit";
            this.btStartNUnit.Size = new System.Drawing.Size(147, 35);
            this.btStartNUnit.TabIndex = 0;
            this.btStartNUnit.Text = "Start NUNIT";
            this.btStartNUnit.UseVisualStyleBackColor = true;
            this.btStartNUnit.Click += new System.EventHandler(this.btStartNUnit_Click);
            // 
            // tpConfig
            // 
            this.tpConfig.Controls.Add(this.llGoToNUnitWrapper);
            this.tpConfig.Controls.Add(this.lbCouldntFindNUnitError);
            this.tpConfig.Controls.Add(this.tbPathToNUnitBinFolder);
            this.tpConfig.Controls.Add(this.label2);
            this.tpConfig.Location = new System.Drawing.Point(4, 4);
            this.tpConfig.Name = "tpConfig";
            this.tpConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tpConfig.Size = new System.Drawing.Size(753, 521);
            this.tpConfig.TabIndex = 1;
            this.tpConfig.Text = "Config";
            this.tpConfig.UseVisualStyleBackColor = true;
            // 
            // llGoToNUnitWrapper
            // 
            this.llGoToNUnitWrapper.AutoSize = true;
            this.llGoToNUnitWrapper.Location = new System.Drawing.Point(158, 102);
            this.llGoToNUnitWrapper.Name = "llGoToNUnitWrapper";
            this.llGoToNUnitWrapper.Size = new System.Drawing.Size(123, 13);
            this.llGoToNUnitWrapper.TabIndex = 58;
            this.llGoToNUnitWrapper.TabStop = true;
            this.llGoToNUnitWrapper.Text = "Go to Nunit Wrapper tab";
            this.llGoToNUnitWrapper.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llGoToNUnitWrapper_LinkClicked);
            // 
            // lbCouldntFindNUnitError
            // 
            this.lbCouldntFindNUnitError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCouldntFindNUnitError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbCouldntFindNUnitError.Location = new System.Drawing.Point(6, 14);
            this.lbCouldntFindNUnitError.Name = "lbCouldntFindNUnitError";
            this.lbCouldntFindNUnitError.Size = new System.Drawing.Size(387, 57);
            this.lbCouldntFindNUnitError.TabIndex = 57;
            this.lbCouldntFindNUnitError.Text = "NUNIT doesn\'t seem to be installed on this computer. Please install it from the l" +
                                                "ink below, or provide a path to its installation";
            // 
            // tbPathToNUnitBinFolder
            // 
            this.tbPathToNUnitBinFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                                       | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPathToNUnitBinFolder.Location = new System.Drawing.Point(158, 75);
            this.tbPathToNUnitBinFolder.Name = "tbPathToNUnitBinFolder";
            this.tbPathToNUnitBinFolder.Size = new System.Drawing.Size(352, 20);
            this.tbPathToNUnitBinFolder.TabIndex = 56;
            this.tbPathToNUnitBinFolder.TextChanged += new System.EventHandler(this.tbPathToNUnitBinFolder_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 55;
            this.label2.Text = "Path to NUnit bin folder";
            // 
            // btRefreshAppDomainAssemblyList
            // 
            this.btRefreshAppDomainAssemblyList.Location = new System.Drawing.Point(195, 21);
            this.btRefreshAppDomainAssemblyList.Name = "btRefreshAppDomainAssemblyList";
            this.btRefreshAppDomainAssemblyList.Size = new System.Drawing.Size(200, 35);
            this.btRefreshAppDomainAssemblyList.TabIndex = 5;
            this.btRefreshAppDomainAssemblyList.Text = "Refresh AppDomain Assembly list";
            this.btRefreshAppDomainAssemblyList.UseVisualStyleBackColor = true;
            this.btRefreshAppDomainAssemblyList.Click += new System.EventHandler(this.btRefreshAppDomainAssemblyList_Click);
            // 
            // ascx_NUnitWrapper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcNunit);
            this.Name = "ascx_NUnitWrapper";
            this.Size = new System.Drawing.Size(761, 547);
            this.tcNunit.ResumeLayout(false);
            this.tpNUnitWrapper.ResumeLayout(false);
            this.tpNUnitWrapper.PerformLayout();
            this.tpConfig.ResumeLayout(false);
            this.tpConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcNunit;
        private System.Windows.Forms.TabPage tpNUnitWrapper;
        private System.Windows.Forms.TabPage tpConfig;
        private System.Windows.Forms.Label lbCouldntFindNUnitError;
        private System.Windows.Forms.TextBox tbPathToNUnitBinFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel llGoToNUnitWrapper;
        private System.Windows.Forms.Button btStartNUnit;
        private ascx_AssemblyInvoke assemblyInvoke;
        private System.Windows.Forms.TreeView tvAssembliesInAppDomain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btRefreshAppDomainAssemblyList;
    }
}