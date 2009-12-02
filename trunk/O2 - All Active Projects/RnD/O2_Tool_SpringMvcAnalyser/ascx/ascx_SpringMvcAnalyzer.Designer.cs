using O2.Kernel.CodeUtils;
using O2.Views.ASCX.CoreControls;

namespace O2.RnD.SpringMVCAnalyzer.ascx
{
    partial class ascx_SpringMvcAnalyzer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.tbO2CirDataOfProject = new System.Windows.Forms.TextBox();
            this.btTest = new System.Windows.Forms.Button();
            this.tbWebRoot = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvBeans = new System.Windows.Forms.DataGridView();
            this.tvSpringMvcClasses = new System.Windows.Forms.TreeView();
            this.scHost = new System.Windows.Forms.SplitContainer();
            this.scTop = new System.Windows.Forms.SplitContainer();
            this.label7 = new System.Windows.Forms.Label();
            this.cbHideGetAndSetStrings = new System.Windows.Forms.CheckBox();
            this.dgvResolvedCommandName = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tvCommandNameObjectFields = new System.Windows.Forms.TreeView();
            this.scBottom = new System.Windows.Forms.SplitContainer();
            this.label6 = new System.Windows.Forms.Label();
            this.tvSpringMvc_SuperClasses = new System.Windows.Forms.TreeView();
            this.label8 = new System.Windows.Forms.Label();
            this.btMakeRulesSourcesOfTaintedData = new System.Windows.Forms.Button();
            this.lbRulesToAdd = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.asvp_VisiblePanels_CirAnalysis = new O2.Views.ASCX.CoreControls.ascx_SelectVisiblePanels();
            this.ascx_DropAreaForCirDataObject = new O2.Views.ASCX.CoreControls.ascx_DropObject();
            this.btLoadDataFromWebRoot = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBeans)).BeginInit();
            this.scHost.Panel1.SuspendLayout();
            this.scHost.Panel2.SuspendLayout();
            this.scHost.SuspendLayout();
            this.scTop.Panel1.SuspendLayout();
            this.scTop.Panel2.SuspendLayout();
            this.scTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResolvedCommandName)).BeginInit();
            this.scBottom.Panel1.SuspendLayout();
            this.scBottom.Panel2.SuspendLayout();
            this.scBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(160, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "CirData file Loaded:";
            // 
            // tbO2CirDataOfProject
            // 
            this.tbO2CirDataOfProject.Location = new System.Drawing.Point(281, 4);
            this.tbO2CirDataOfProject.Name = "tbO2CirDataOfProject";
            this.tbO2CirDataOfProject.Size = new System.Drawing.Size(386, 20);
            this.tbO2CirDataOfProject.TabIndex = 2;
            // 
            // btTest
            // 
            this.btTest.Location = new System.Drawing.Point(566, 56);
            this.btTest.Name = "btTest";
            this.btTest.Size = new System.Drawing.Size(91, 30);
            this.btTest.TabIndex = 3;
            this.btTest.Text = "Load Test data";
            this.btTest.UseVisualStyleBackColor = true;
            this.btTest.Click += new System.EventHandler(this.btTest_Click);
            // 
            // tbWebRoot
            // 
            this.tbWebRoot.Location = new System.Drawing.Point(281, 30);
            this.tbWebRoot.Name = "tbWebRoot";
            this.tbWebRoot.Size = new System.Drawing.Size(386, 20);
            this.tbWebRoot.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(160, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "WebRoot";
            // 
            // dgvBeans
            // 
            this.dgvBeans.AllowUserToAddRows = false;
            this.dgvBeans.AllowUserToDeleteRows = false;
            this.dgvBeans.AllowUserToOrderColumns = true;
            this.dgvBeans.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBeans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBeans.Location = new System.Drawing.Point(6, 20);
            this.dgvBeans.Name = "dgvBeans";
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvBeans.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBeans.Size = new System.Drawing.Size(702, 177);
            this.dgvBeans.TabIndex = 9;
            // 
            // tvSpringMvcClasses
            // 
            this.tvSpringMvcClasses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvSpringMvcClasses.Location = new System.Drawing.Point(6, 20);
            this.tvSpringMvcClasses.Name = "tvSpringMvcClasses";
            this.tvSpringMvcClasses.Size = new System.Drawing.Size(337, 242);
            this.tvSpringMvcClasses.TabIndex = 10;
            // 
            // scHost
            // 
            this.scHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.scHost.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scHost.Location = new System.Drawing.Point(6, 109);
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
            this.scHost.Size = new System.Drawing.Size(1071, 477);
            this.scHost.SplitterDistance = 269;
            this.scHost.TabIndex = 11;
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
            this.scTop.Panel1.Controls.Add(this.label7);
            this.scTop.Panel1.Controls.Add(this.tvSpringMvcClasses);
            // 
            // scTop.Panel2
            // 
            this.scTop.Panel2.Controls.Add(this.cbHideGetAndSetStrings);
            this.scTop.Panel2.Controls.Add(this.dgvResolvedCommandName);
            this.scTop.Panel2.Controls.Add(this.label5);
            this.scTop.Panel2.Controls.Add(this.label4);
            this.scTop.Panel2.Controls.Add(this.tvCommandNameObjectFields);
            this.scTop.Size = new System.Drawing.Size(1071, 269);
            this.scTop.SplitterDistance = 354;
            this.scTop.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "SpringMvc Classes";
            // 
            // cbHideGetAndSetStrings
            // 
            this.cbHideGetAndSetStrings.AutoSize = true;
            this.cbHideGetAndSetStrings.Location = new System.Drawing.Point(134, 105);
            this.cbHideGetAndSetStrings.Name = "cbHideGetAndSetStrings";
            this.cbHideGetAndSetStrings.Size = new System.Drawing.Size(143, 17);
            this.cbHideGetAndSetStrings.TabIndex = 4;
            this.cbHideGetAndSetStrings.Text = "hide \'get\' and \'set\' strings";
            this.cbHideGetAndSetStrings.UseVisualStyleBackColor = true;
            // 
            // dgvResolvedCommandName
            // 
            this.dgvResolvedCommandName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResolvedCommandName.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResolvedCommandName.Location = new System.Drawing.Point(3, 20);
            this.dgvResolvedCommandName.Name = "dgvResolvedCommandName";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            this.dgvResolvedCommandName.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvResolvedCommandName.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResolvedCommandName.Size = new System.Drawing.Size(703, 83);
            this.dgvResolvedCommandName.TabIndex = 0;
            this.dgvResolvedCommandName.SelectionChanged += new System.EventHandler(this.dgvResolvedCommandName_SelectionChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Bindable Object fields ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Resolved CommandName assignments";
            // 
            // tvCommandNameObjectFields
            // 
            this.tvCommandNameObjectFields.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvCommandNameObjectFields.Location = new System.Drawing.Point(6, 122);
            this.tvCommandNameObjectFields.Name = "tvCommandNameObjectFields";
            this.tvCommandNameObjectFields.Size = new System.Drawing.Size(700, 140);
            this.tvCommandNameObjectFields.TabIndex = 2;
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
            this.scBottom.Panel1.Controls.Add(this.label6);
            this.scBottom.Panel1.Controls.Add(this.tvSpringMvc_SuperClasses);
            // 
            // scBottom.Panel2
            // 
            this.scBottom.Panel2.Controls.Add(this.label8);
            this.scBottom.Panel2.Controls.Add(this.dgvBeans);
            this.scBottom.Size = new System.Drawing.Size(1071, 204);
            this.scBottom.SplitterDistance = 354;
            this.scBottom.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "SuperClasses Mappings";
            // 
            // tvSpringMvc_SuperClasses
            // 
            this.tvSpringMvc_SuperClasses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvSpringMvc_SuperClasses.Location = new System.Drawing.Point(3, 20);
            this.tvSpringMvc_SuperClasses.Name = "tvSpringMvc_SuperClasses";
            this.tvSpringMvc_SuperClasses.Size = new System.Drawing.Size(340, 301);
            this.tvSpringMvc_SuperClasses.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Beans";
            // 
            // btMakeRulesSourcesOfTaintedData
            // 
            this.btMakeRulesSourcesOfTaintedData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btMakeRulesSourcesOfTaintedData.Enabled = false;
            this.btMakeRulesSourcesOfTaintedData.Location = new System.Drawing.Point(827, 80);
            this.btMakeRulesSourcesOfTaintedData.Name = "btMakeRulesSourcesOfTaintedData";
            this.btMakeRulesSourcesOfTaintedData.Size = new System.Drawing.Size(245, 23);
            this.btMakeRulesSourcesOfTaintedData.TabIndex = 2;
            this.btMakeRulesSourcesOfTaintedData.Text = "Make Functions Sources of tainted data";
            this.btMakeRulesSourcesOfTaintedData.UseVisualStyleBackColor = true;
            this.btMakeRulesSourcesOfTaintedData.Click += new System.EventHandler(this.btMakeRulesSourcesOfTaintedData_Click);
            // 
            // lbRulesToAdd
            // 
            this.lbRulesToAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbRulesToAdd.FormattingEnabled = true;
            this.lbRulesToAdd.Location = new System.Drawing.Point(827, 20);
            this.lbRulesToAdd.Name = "lbRulesToAdd";
            this.lbRulesToAdd.Size = new System.Drawing.Size(250, 56);
            this.lbRulesToAdd.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(809, 4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(192, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Rules to add: Sources of Tainted Data ";
            // 
            // asvp_VisiblePanels_CirAnalysis
            // 
            this.asvp_VisiblePanels_CirAnalysis.BackColor = System.Drawing.SystemColors.Control;
            this.asvp_VisiblePanels_CirAnalysis.ForeColor = System.Drawing.Color.Black;
            this.asvp_VisiblePanels_CirAnalysis.Location = new System.Drawing.Point(11, 46);
            this.asvp_VisiblePanels_CirAnalysis.Name = "asvp_VisiblePanels_CirAnalysis";
            this.asvp_VisiblePanels_CirAnalysis.Size = new System.Drawing.Size(549, 40);
            this.asvp_VisiblePanels_CirAnalysis.TabIndex = 12;
            // 
            // ascx_DropAreaForCirDataObject
            // 
            this.ascx_DropAreaForCirDataObject.AllowDrop = true;
            this.ascx_DropAreaForCirDataObject.BackColor = System.Drawing.Color.Maroon;
            this.ascx_DropAreaForCirDataObject.ForeColor = System.Drawing.Color.White;
            this.ascx_DropAreaForCirDataObject.Location = new System.Drawing.Point(4, 4);
            this.ascx_DropAreaForCirDataObject.Name = "ascx_DropAreaForCirDataObject";
            this.ascx_DropAreaForCirDataObject.Size = new System.Drawing.Size(150, 46);
            this.ascx_DropAreaForCirDataObject.TabIndex = 14;
            this.ascx_DropAreaForCirDataObject.Text = "Drop Content Here!!";
            this.ascx_DropAreaForCirDataObject.eDnDAction_ObjectDataReceived_Event += new O2.Kernel.CodeUtils.Callbacks.dMethod_Object(this.ascx_DropAreaForCirDataObject_eDnDAction_ObjectDataReceived_Event);
            // 
            // btLoadDataFromWebRoot
            // 
            this.btLoadDataFromWebRoot.Location = new System.Drawing.Point(673, 30);
            this.btLoadDataFromWebRoot.Name = "btLoadDataFromWebRoot";
            this.btLoadDataFromWebRoot.Size = new System.Drawing.Size(145, 20);
            this.btLoadDataFromWebRoot.TabIndex = 15;
            this.btLoadDataFromWebRoot.Text = "Load Data from Web Root";
            this.btLoadDataFromWebRoot.UseVisualStyleBackColor = true;
            this.btLoadDataFromWebRoot.Click += new System.EventHandler(this.btLoadDataFromWebRoot_Click);
            // 
            // ascx_SpringMvcAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.btMakeRulesSourcesOfTaintedData);
            this.Controls.Add(this.btLoadDataFromWebRoot);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lbRulesToAdd);
            this.Controls.Add(this.tbWebRoot);
            this.Controls.Add(this.ascx_DropAreaForCirDataObject);
            this.Controls.Add(this.asvp_VisiblePanels_CirAnalysis);
            this.Controls.Add(this.scHost);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btTest);
            this.Controls.Add(this.tbO2CirDataOfProject);
            this.Controls.Add(this.label2);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ascx_SpringMvcAnalyzer";
            this.Size = new System.Drawing.Size(1090, 589);
            this.Load += new System.EventHandler(this.test_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBeans)).EndInit();
            this.scHost.Panel1.ResumeLayout(false);
            this.scHost.Panel2.ResumeLayout(false);
            this.scHost.ResumeLayout(false);
            this.scTop.Panel1.ResumeLayout(false);
            this.scTop.Panel1.PerformLayout();
            this.scTop.Panel2.ResumeLayout(false);
            this.scTop.Panel2.PerformLayout();
            this.scTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResolvedCommandName)).EndInit();
            this.scBottom.Panel1.ResumeLayout(false);
            this.scBottom.Panel1.PerformLayout();
            this.scBottom.Panel2.ResumeLayout(false);
            this.scBottom.Panel2.PerformLayout();
            this.scBottom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbO2CirDataOfProject;
        private System.Windows.Forms.Button btTest;
        private System.Windows.Forms.TextBox tbWebRoot;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvBeans;
        private System.Windows.Forms.TreeView tvSpringMvcClasses;
        private System.Windows.Forms.SplitContainer scHost;
        private System.Windows.Forms.SplitContainer scTop;
        private System.Windows.Forms.SplitContainer scBottom;
        private ascx_SelectVisiblePanels asvp_VisiblePanels_CirAnalysis;
        private System.Windows.Forms.TreeView tvSpringMvc_SuperClasses;
        private System.Windows.Forms.DataGridView dgvResolvedCommandName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TreeView tvCommandNameObjectFields;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbHideGetAndSetStrings;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox lbRulesToAdd;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btMakeRulesSourcesOfTaintedData;
        private ascx_DropObject ascx_DropAreaForCirDataObject;
        private System.Windows.Forms.Button btLoadDataFromWebRoot;
    }
}