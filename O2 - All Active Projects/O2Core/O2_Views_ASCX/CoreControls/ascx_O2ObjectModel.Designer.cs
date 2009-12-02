// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
namespace O2.Views.ASCX.CoreControls
{
    partial class ascx_O2ObjectModel
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
            this.tcO2ObjectModel = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tbFilterBy_ReturnType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbFilterBy_ParameterType = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbFilterBy_MethodName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbFilterBy_MethodType = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tvAssembliesLoaded = new System.Windows.Forms.TreeView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.cbHideCSharpGeneratedMethods = new System.Windows.Forms.CheckBox();
            this.llRefreshFunctionsViewer = new System.Windows.Forms.LinkLabel();
            this.filteredFunctionsViewer = new O2.Views.ASCX.DataViewers.ascx_FunctionsViewer();
            this.functionsViewer = new O2.Views.ASCX.DataViewers.ascx_FunctionsViewer();
            this.tcO2ObjectModel.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcO2ObjectModel
            // 
            this.tcO2ObjectModel.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tcO2ObjectModel.Controls.Add(this.tabPage3);
            this.tcO2ObjectModel.Controls.Add(this.tabPage1);
            this.tcO2ObjectModel.Controls.Add(this.tabPage2);
            this.tcO2ObjectModel.Controls.Add(this.tabPage4);
            this.tcO2ObjectModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcO2ObjectModel.Location = new System.Drawing.Point(0, 0);
            this.tcO2ObjectModel.Name = "tcO2ObjectModel";
            this.tcO2ObjectModel.SelectedIndex = 0;
            this.tcO2ObjectModel.Size = new System.Drawing.Size(484, 325);
            this.tcO2ObjectModel.TabIndex = 51;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.filteredFunctionsViewer);
            this.tabPage3.Controls.Add(this.tbFilterBy_ReturnType);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.tbFilterBy_ParameterType);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.tbFilterBy_MethodName);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.tbFilterBy_MethodType);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Location = new System.Drawing.Point(4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(476, 299);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Find Method";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tbFilterBy_ReturnType
            // 
            this.tbFilterBy_ReturnType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilterBy_ReturnType.Location = new System.Drawing.Point(376, 21);
            this.tbFilterBy_ReturnType.Name = "tbFilterBy_ReturnType";
            this.tbFilterBy_ReturnType.Size = new System.Drawing.Size(97, 20);
            this.tbFilterBy_ReturnType.TabIndex = 7;
            this.tbFilterBy_ReturnType.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbFilterBy_ReturnType_KeyUp);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(376, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Return Type";
            // 
            // tbFilterBy_ParameterType
            // 
            this.tbFilterBy_ParameterType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilterBy_ParameterType.Location = new System.Drawing.Point(270, 21);
            this.tbFilterBy_ParameterType.Name = "tbFilterBy_ParameterType";
            this.tbFilterBy_ParameterType.Size = new System.Drawing.Size(100, 20);
            this.tbFilterBy_ParameterType.TabIndex = 5;
            this.tbFilterBy_ParameterType.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbFilterBy_ParameterType_KeyUp);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(270, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Parameter Type";
            // 
            // tbFilterBy_MethodName
            // 
            this.tbFilterBy_MethodName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilterBy_MethodName.Location = new System.Drawing.Point(109, 21);
            this.tbFilterBy_MethodName.Name = "tbFilterBy_MethodName";
            this.tbFilterBy_MethodName.Size = new System.Drawing.Size(155, 20);
            this.tbFilterBy_MethodName.TabIndex = 3;
            this.tbFilterBy_MethodName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbFilterBy_MethodName_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(109, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Method Name";
            // 
            // tbFilterBy_MethodType
            // 
            this.tbFilterBy_MethodType.Location = new System.Drawing.Point(4, 21);
            this.tbFilterBy_MethodType.Name = "tbFilterBy_MethodType";
            this.tbFilterBy_MethodType.Size = new System.Drawing.Size(100, 20);
            this.tbFilterBy_MethodType.TabIndex = 1;
            this.tbFilterBy_MethodType.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbFilterBy_MethodType_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Method Type";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.functionsViewer);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(476, 299);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "All Methods & Classes";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tvAssembliesLoaded);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(476, 299);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Assemblies Loaded";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tvAssembliesLoaded
            // 
            this.tvAssembliesLoaded.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvAssembliesLoaded.Location = new System.Drawing.Point(3, 3);
            this.tvAssembliesLoaded.Name = "tvAssembliesLoaded";
            this.tvAssembliesLoaded.Size = new System.Drawing.Size(470, 293);
            this.tvAssembliesLoaded.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.cbHideCSharpGeneratedMethods);
            this.tabPage4.Controls.Add(this.llRefreshFunctionsViewer);
            this.tabPage4.Location = new System.Drawing.Point(4, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(476, 299);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "config";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // cbHideCSharpGeneratedMethods
            // 
            this.cbHideCSharpGeneratedMethods.AutoSize = true;
            this.cbHideCSharpGeneratedMethods.Checked = true;
            this.cbHideCSharpGeneratedMethods.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbHideCSharpGeneratedMethods.Location = new System.Drawing.Point(6, 9);
            this.cbHideCSharpGeneratedMethods.Name = "cbHideCSharpGeneratedMethods";
            this.cbHideCSharpGeneratedMethods.Size = new System.Drawing.Size(162, 17);
            this.cbHideCSharpGeneratedMethods.TabIndex = 2;
            this.cbHideCSharpGeneratedMethods.Text = "Hide C# Generated Methods";
            this.cbHideCSharpGeneratedMethods.UseVisualStyleBackColor = true;
            this.cbHideCSharpGeneratedMethods.CheckedChanged += new System.EventHandler(this.cbHideCSharpGeneratedMethods_CheckedChanged);
            // 
            // llRefreshFunctionsViewer
            // 
            this.llRefreshFunctionsViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llRefreshFunctionsViewer.AutoSize = true;
            this.llRefreshFunctionsViewer.Location = new System.Drawing.Point(431, 13);
            this.llRefreshFunctionsViewer.Name = "llRefreshFunctionsViewer";
            this.llRefreshFunctionsViewer.Size = new System.Drawing.Size(39, 13);
            this.llRefreshFunctionsViewer.TabIndex = 1;
            this.llRefreshFunctionsViewer.TabStop = true;
            this.llRefreshFunctionsViewer.Text = "refresh";
            this.llRefreshFunctionsViewer.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llRefreshFunctionsViewer_LinkClicked);
            // 
            // filteredFunctionsViewer
            // 
            this.filteredFunctionsViewer._AdvancedModeViews = false;
            this.filteredFunctionsViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.filteredFunctionsViewer.BackColor = System.Drawing.SystemColors.Control;
            this.filteredFunctionsViewer.ForeColor = System.Drawing.Color.Black;
            this.filteredFunctionsViewer.Location = new System.Drawing.Point(3, 48);
            this.filteredFunctionsViewer.Name = "filteredFunctionsViewer";
            this.filteredFunctionsViewer.NamespaceDepthValue = 2;
            this.filteredFunctionsViewer.Size = new System.Drawing.Size(470, 250);
            this.filteredFunctionsViewer.TabIndex = 8;
            // 
            // functionsViewer
            // 
            this.functionsViewer._AdvancedModeViews = true;
            this.functionsViewer.BackColor = System.Drawing.SystemColors.Control;
            this.functionsViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.functionsViewer.ForeColor = System.Drawing.Color.Black;
            this.functionsViewer.Location = new System.Drawing.Point(3, 3);
            this.functionsViewer.Name = "functionsViewer";
            this.functionsViewer.NamespaceDepthValue = 2;
            this.functionsViewer.Size = new System.Drawing.Size(470, 293);
            this.functionsViewer.TabIndex = 0;
            // 
            // ascx_O2ObjectModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcO2ObjectModel);
            this.Name = "ascx_O2ObjectModel";
            this.Size = new System.Drawing.Size(484, 325);
            this.Load += new System.EventHandler(this.ascx_O2ObjectModel_Load);
            this.tcO2ObjectModel.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcO2ObjectModel;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private O2.Views.ASCX.DataViewers.ascx_FunctionsViewer functionsViewer;
        private System.Windows.Forms.LinkLabel llRefreshFunctionsViewer;
        private System.Windows.Forms.CheckBox cbHideCSharpGeneratedMethods;
        private System.Windows.Forms.TreeView tvAssembliesLoaded;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox tbFilterBy_ReturnType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbFilterBy_ParameterType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFilterBy_MethodName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbFilterBy_MethodType;
        private System.Windows.Forms.Label label1;
        private O2.Views.ASCX.DataViewers.ascx_FunctionsViewer filteredFunctionsViewer;
        private System.Windows.Forms.TabPage tabPage4;
    }
}
