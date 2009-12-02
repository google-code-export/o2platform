// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Views.ASCX.CoreControls;
using O2.Views.ASCX.DataViewers;

namespace O2.Core.CIR.Ascx.OldVersions
{
    partial class ascx_CirViewer_Class
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
            this.scHostControl = new System.Windows.Forms.SplitContainer();
            this.scTop = new System.Windows.Forms.SplitContainer();
            this.cbOnlyShowFunctionsCalledBySelectedFunction = new System.Windows.Forms.CheckBox();
            this.afv_Functions = new ascx_FunctionsViewer();
            this.cbCallsMade_OnlyShowExternal = new System.Windows.Forms.CheckBox();
            this.afv_MakesCallsTo = new ascx_FunctionsViewer();
            this.scBottom = new System.Windows.Forms.SplitContainer();
            this.afv_HaveRuleInDb = new ascx_FunctionsViewer();
            this.afv_DontHaveRuleInDb = new ascx_FunctionsViewer();
            this.cbVisibleControls_Class = new System.Windows.Forms.CheckBox();
            this.cbVisibleControls_IsSuperClassedBy = new System.Windows.Forms.CheckBox();
            this.cbVisibleControls_RulesCreator = new System.Windows.Forms.CheckBox();
            this.cbVisibleControls_SuperClass = new System.Windows.Forms.CheckBox();
            this.asv_SelectVisiblePanels = new ascx_SelectVisiblePanels();
            this.scHostControl.Panel1.SuspendLayout();
            this.scHostControl.Panel2.SuspendLayout();
            this.scHostControl.SuspendLayout();
            this.scTop.Panel1.SuspendLayout();
            this.scTop.Panel2.SuspendLayout();
            this.scTop.SuspendLayout();
            this.scBottom.Panel1.SuspendLayout();
            this.scBottom.Panel2.SuspendLayout();
            this.scBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // scHostControl
            // 
            this.scHostControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                               | System.Windows.Forms.AnchorStyles.Left)
                                                                              | System.Windows.Forms.AnchorStyles.Right)));
            this.scHostControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scHostControl.Location = new System.Drawing.Point(4, 43);
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
            this.scHostControl.Size = new System.Drawing.Size(626, 435);
            this.scHostControl.SplitterDistance = 217;
            this.scHostControl.TabIndex = 2;
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
            this.scTop.Panel1.Controls.Add(this.cbOnlyShowFunctionsCalledBySelectedFunction);
            this.scTop.Panel1.Controls.Add(this.afv_Functions);
            // 
            // scTop.Panel2
            // 
            this.scTop.Panel2.Controls.Add(this.cbCallsMade_OnlyShowExternal);
            this.scTop.Panel2.Controls.Add(this.afv_MakesCallsTo);
            this.scTop.Size = new System.Drawing.Size(626, 217);
            this.scTop.SplitterDistance = 298;
            this.scTop.TabIndex = 0;
            // 
            // cbOnlyShowFunctionsCalledBySelectedFunction
            // 
            this.cbOnlyShowFunctionsCalledBySelectedFunction.AutoSize = true;
            this.cbOnlyShowFunctionsCalledBySelectedFunction.Checked = true;
            this.cbOnlyShowFunctionsCalledBySelectedFunction.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOnlyShowFunctionsCalledBySelectedFunction.Location = new System.Drawing.Point(3, 2);
            this.cbOnlyShowFunctionsCalledBySelectedFunction.Name = "cbOnlyShowFunctionsCalledBySelectedFunction";
            this.cbOnlyShowFunctionsCalledBySelectedFunction.Size = new System.Drawing.Size(250, 17);
            this.cbOnlyShowFunctionsCalledBySelectedFunction.TabIndex = 3;
            this.cbOnlyShowFunctionsCalledBySelectedFunction.Text = "Only show functions called by selected function";
            this.cbOnlyShowFunctionsCalledBySelectedFunction.UseVisualStyleBackColor = true;
            this.cbOnlyShowFunctionsCalledBySelectedFunction.CheckedChanged += new System.EventHandler(this.cbOnlyShowFunctionsCalledBySelectedFunction_CheckedChanged);
            // 
            // afv_Functions
            // 
            this.afv_Functions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                               | System.Windows.Forms.AnchorStyles.Left)
                                                                              | System.Windows.Forms.AnchorStyles.Right)));
            this.afv_Functions.BackColor = System.Drawing.SystemColors.Control;
            this.afv_Functions.ForeColor = System.Drawing.Color.Black;
            this.afv_Functions.Location = new System.Drawing.Point(0, 25);
            this.afv_Functions.Name = "afv_Functions";
            this.afv_Functions.Size = new System.Drawing.Size(294, 188);
            this.afv_Functions.TabIndex = 6;
            // 
            // cbCallsMade_OnlyShowExternal
            // 
            this.cbCallsMade_OnlyShowExternal.AutoSize = true;
            this.cbCallsMade_OnlyShowExternal.Location = new System.Drawing.Point(3, 3);
            this.cbCallsMade_OnlyShowExternal.Name = "cbCallsMade_OnlyShowExternal";
            this.cbCallsMade_OnlyShowExternal.Size = new System.Drawing.Size(258, 17);
            this.cbCallsMade_OnlyShowExternal.TabIndex = 2;
            this.cbCallsMade_OnlyShowExternal.Text = "Only show external calls to the current o2CirData ";
            this.cbCallsMade_OnlyShowExternal.UseVisualStyleBackColor = true;
            this.cbCallsMade_OnlyShowExternal.CheckedChanged += new System.EventHandler(this.cbCallsMade_OnlyShowExternal_CheckedChanged);
            // 
            // afv_MakesCallsTo
            // 
            this.afv_MakesCallsTo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                  | System.Windows.Forms.AnchorStyles.Left)
                                                                                 | System.Windows.Forms.AnchorStyles.Right)));
            this.afv_MakesCallsTo.BackColor = System.Drawing.SystemColors.Control;
            this.afv_MakesCallsTo.ForeColor = System.Drawing.Color.Black;
            this.afv_MakesCallsTo.Location = new System.Drawing.Point(0, 25);
            this.afv_MakesCallsTo.Name = "afv_MakesCallsTo";
            this.afv_MakesCallsTo.Size = new System.Drawing.Size(320, 188);
            this.afv_MakesCallsTo.TabIndex = 1;
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
            this.scBottom.Panel1.Controls.Add(this.afv_HaveRuleInDb);
            // 
            // scBottom.Panel2
            // 
            this.scBottom.Panel2.Controls.Add(this.afv_DontHaveRuleInDb);
            this.scBottom.Size = new System.Drawing.Size(626, 214);
            this.scBottom.SplitterDistance = 297;
            this.scBottom.TabIndex = 0;
            // 
            // afv_HaveRuleInDb
            // 
            this.afv_HaveRuleInDb.BackColor = System.Drawing.SystemColors.Control;
            this.afv_HaveRuleInDb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.afv_HaveRuleInDb.ForeColor = System.Drawing.Color.Black;
            this.afv_HaveRuleInDb.Location = new System.Drawing.Point(0, 0);
            this.afv_HaveRuleInDb.Name = "afv_HaveRuleInDb";
            this.afv_HaveRuleInDb.Size = new System.Drawing.Size(293, 210);
            this.afv_HaveRuleInDb.TabIndex = 4;
            // 
            // afv_DontHaveRuleInDb
            // 
            this.afv_DontHaveRuleInDb.BackColor = System.Drawing.SystemColors.Control;
            this.afv_DontHaveRuleInDb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.afv_DontHaveRuleInDb.ForeColor = System.Drawing.Color.Black;
            this.afv_DontHaveRuleInDb.Location = new System.Drawing.Point(0, 0);
            this.afv_DontHaveRuleInDb.Name = "afv_DontHaveRuleInDb";
            this.afv_DontHaveRuleInDb.Size = new System.Drawing.Size(321, 210);
            this.afv_DontHaveRuleInDb.TabIndex = 3;
            // 
            // cbVisibleControls_Class
            // 
            this.cbVisibleControls_Class.AutoSize = true;
            this.cbVisibleControls_Class.Checked = true;
            this.cbVisibleControls_Class.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbVisibleControls_Class.Location = new System.Drawing.Point(12, 14);
            this.cbVisibleControls_Class.Name = "cbVisibleControls_Class";
            this.cbVisibleControls_Class.Size = new System.Drawing.Size(51, 17);
            this.cbVisibleControls_Class.TabIndex = 95;
            this.cbVisibleControls_Class.Text = "Class";
            this.cbVisibleControls_Class.UseVisualStyleBackColor = true;
            // 
            // cbVisibleControls_IsSuperClassedBy
            // 
            this.cbVisibleControls_IsSuperClassedBy.AutoSize = true;
            this.cbVisibleControls_IsSuperClassedBy.Location = new System.Drawing.Point(181, 14);
            this.cbVisibleControls_IsSuperClassedBy.Name = "cbVisibleControls_IsSuperClassedBy";
            this.cbVisibleControls_IsSuperClassedBy.Size = new System.Drawing.Size(120, 17);
            this.cbVisibleControls_IsSuperClassedBy.TabIndex = 96;
            this.cbVisibleControls_IsSuperClassedBy.Text = "Is Super Classed By";
            this.cbVisibleControls_IsSuperClassedBy.UseVisualStyleBackColor = true;
            // 
            // cbVisibleControls_RulesCreator
            // 
            this.cbVisibleControls_RulesCreator.AutoSize = true;
            this.cbVisibleControls_RulesCreator.Location = new System.Drawing.Point(317, 14);
            this.cbVisibleControls_RulesCreator.Name = "cbVisibleControls_RulesCreator";
            this.cbVisibleControls_RulesCreator.Size = new System.Drawing.Size(90, 17);
            this.cbVisibleControls_RulesCreator.TabIndex = 97;
            this.cbVisibleControls_RulesCreator.Text = "Rules Creator";
            this.cbVisibleControls_RulesCreator.UseVisualStyleBackColor = true;
            // 
            // cbVisibleControls_SuperClass
            // 
            this.cbVisibleControls_SuperClass.AutoSize = true;
            this.cbVisibleControls_SuperClass.Location = new System.Drawing.Point(85, 14);
            this.cbVisibleControls_SuperClass.Name = "cbVisibleControls_SuperClass";
            this.cbVisibleControls_SuperClass.Size = new System.Drawing.Size(79, 17);
            this.cbVisibleControls_SuperClass.TabIndex = 94;
            this.cbVisibleControls_SuperClass.Text = "SuperClass";
            this.cbVisibleControls_SuperClass.UseVisualStyleBackColor = true;
            // 
            // asv_SelectVisiblePanels
            // 
            this.asv_SelectVisiblePanels.BackColor = System.Drawing.SystemColors.Control;
            this.asv_SelectVisiblePanels.ForeColor = System.Drawing.Color.Black;
            this.asv_SelectVisiblePanels.Location = new System.Drawing.Point(4, -3);
            this.asv_SelectVisiblePanels.Name = "asv_SelectVisiblePanels";
            this.asv_SelectVisiblePanels.Size = new System.Drawing.Size(570, 40);
            this.asv_SelectVisiblePanels.TabIndex = 3;
            // 
            // ascx_CirViewer_Class
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.asv_SelectVisiblePanels);
            this.Controls.Add(this.scHostControl);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ascx_CirViewer_Class";
            this.Size = new System.Drawing.Size(636, 482);
            this.Load += new System.EventHandler(this.ascx_CirViewer_Class_Load);
            this.scHostControl.Panel1.ResumeLayout(false);
            this.scHostControl.Panel2.ResumeLayout(false);
            this.scHostControl.ResumeLayout(false);
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

        private System.Windows.Forms.SplitContainer scHostControl;
        private System.Windows.Forms.SplitContainer scTop;
        private System.Windows.Forms.SplitContainer scBottom;
        private ascx_FunctionsViewer afv_Functions;
        private ascx_FunctionsViewer afv_MakesCallsTo;
        private ascx_FunctionsViewer afv_DontHaveRuleInDb;
        private ascx_FunctionsViewer afv_HaveRuleInDb;
        private System.Windows.Forms.CheckBox cbVisibleControls_Class;
        private System.Windows.Forms.CheckBox cbVisibleControls_IsSuperClassedBy;
        private System.Windows.Forms.CheckBox cbVisibleControls_RulesCreator;
        private System.Windows.Forms.CheckBox cbVisibleControls_SuperClass;
        private ascx_SelectVisiblePanels asv_SelectVisiblePanels;
        private System.Windows.Forms.CheckBox cbCallsMade_OnlyShowExternal;
        private System.Windows.Forms.CheckBox cbOnlyShowFunctionsCalledBySelectedFunction;
    }
}
