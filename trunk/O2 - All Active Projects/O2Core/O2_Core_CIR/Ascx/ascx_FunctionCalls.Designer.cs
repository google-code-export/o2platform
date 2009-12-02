// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
namespace O2.Core.CIR.Ascx
{
    partial class ascx_FunctionCalls
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
            this.tvFunctionInfo = new System.Windows.Forms.TreeView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.tvFunctionMakesCallsTo = new System.Windows.Forms.TreeView();
            this.label3 = new System.Windows.Forms.Label();
            this.tvFunctionIsCalledBy = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.laFunctionViewed = new System.Windows.Forms.Label();
            this.cbCirFunction_IsTainted = new System.Windows.Forms.CheckBox();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.tvClassSuperClasses = new System.Windows.Forms.TreeView();
            this.label5 = new System.Windows.Forms.Label();
            this.tvClassIsSuperClassedBy = new System.Windows.Forms.TreeView();
            this.label6 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tvClassAttributes = new System.Windows.Forms.TreeView();
            this.label4 = new System.Windows.Forms.Label();
            this.tvFunctionAttributes = new System.Windows.Forms.TreeView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.cbIgnoreCoreObjectClass = new System.Windows.Forms.CheckBox();
            this.cbViewInheritedMethods = new System.Windows.Forms.CheckBox();
            this.functionViewerForClassMethods = new O2.Views.ASCX.DataViewers.ascx_FunctionsViewer();
            this.cbShowLineInSourceFile = new System.Windows.Forms.CheckBox();
            this.cbDontExpandRecursiveCalls = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvFunctionInfo
            // 
            this.tvFunctionInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvFunctionInfo.Location = new System.Drawing.Point(585, 23);
            this.tvFunctionInfo.Name = "tvFunctionInfo";
            this.tvFunctionInfo.Size = new System.Drawing.Size(186, 77);
            this.tvFunctionInfo.TabIndex = 0;
            this.tvFunctionInfo.Visible = false;
            this.tvFunctionInfo.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvFunctionInfo_BeforeExpand);
            this.tvFunctionInfo.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvFunctionInfo_AfterSelect);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.tvFunctionMakesCallsTo);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.label3);
            this.splitContainer2.Panel2.Controls.Add(this.tvFunctionIsCalledBy);
            this.splitContainer2.Size = new System.Drawing.Size(725, 400);
            this.splitContainer2.SplitterDistance = 358;
            this.splitContainer2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Function makes calls to:";
            // 
            // tvFunctionMakesCallsTo
            // 
            this.tvFunctionMakesCallsTo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvFunctionMakesCallsTo.Location = new System.Drawing.Point(3, 23);
            this.tvFunctionMakesCallsTo.Name = "tvFunctionMakesCallsTo";
            this.tvFunctionMakesCallsTo.Size = new System.Drawing.Size(348, 370);
            this.tvFunctionMakesCallsTo.TabIndex = 0;
            this.tvFunctionMakesCallsTo.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvFunctionMakesCallsTo_BeforeExpand);
            this.tvFunctionMakesCallsTo.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvFunctionMakesCallsTo_AfterSelect);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Function is called by:";
            // 
            // tvFunctionIsCalledBy
            // 
            this.tvFunctionIsCalledBy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvFunctionIsCalledBy.Location = new System.Drawing.Point(3, 23);
            this.tvFunctionIsCalledBy.Name = "tvFunctionIsCalledBy";
            this.tvFunctionIsCalledBy.Size = new System.Drawing.Size(353, 370);
            this.tvFunctionIsCalledBy.TabIndex = 0;
            this.tvFunctionIsCalledBy.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvFunctionIsCalledBy_BeforeExpand);
            this.tvFunctionIsCalledBy.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvFunctionIsCalledBy_AfterSelect);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Viewing Function:";
            // 
            // laFunctionViewed
            // 
            this.laFunctionViewed.AutoSize = true;
            this.laFunctionViewed.Location = new System.Drawing.Point(94, 4);
            this.laFunctionViewed.Name = "laFunctionViewed";
            this.laFunctionViewed.Size = new System.Drawing.Size(16, 13);
            this.laFunctionViewed.TabIndex = 13;
            this.laFunctionViewed.Text = "...";
            // 
            // cbCirFunction_IsTainted
            // 
            this.cbCirFunction_IsTainted.AutoSize = true;
            this.cbCirFunction_IsTainted.Location = new System.Drawing.Point(97, 23);
            this.cbCirFunction_IsTainted.Name = "cbCirFunction_IsTainted";
            this.cbCirFunction_IsTainted.Size = new System.Drawing.Size(73, 17);
            this.cbCirFunction_IsTainted.TabIndex = 14;
            this.cbCirFunction_IsTainted.Text = "Is Tainted";
            this.cbCirFunction_IsTainted.UseVisualStyleBackColor = true;
            this.cbCirFunction_IsTainted.CheckedChanged += new System.EventHandler(this.cbCirFunction_IsTainted_CheckedChanged);
            // 
            // splitContainer4
            // 
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(3, 3);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.tvClassSuperClasses);
            this.splitContainer4.Panel1.Controls.Add(this.label5);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.tvClassIsSuperClassedBy);
            this.splitContainer4.Panel2.Controls.Add(this.label6);
            this.splitContainer4.Size = new System.Drawing.Size(725, 400);
            this.splitContainer4.SplitterDistance = 337;
            this.splitContainer4.TabIndex = 0;
            // 
            // tvClassSuperClasses
            // 
            this.tvClassSuperClasses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvClassSuperClasses.Location = new System.Drawing.Point(6, 27);
            this.tvClassSuperClasses.Name = "tvClassSuperClasses";
            this.tvClassSuperClasses.Size = new System.Drawing.Size(324, 366);
            this.tvClassSuperClasses.TabIndex = 14;
            this.tvClassSuperClasses.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvClassSuperClasses_BeforeExpand);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(241, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Class superclasses (i.e. implements these classes)";
            // 
            // tvClassIsSuperClassedBy
            // 
            this.tvClassIsSuperClassedBy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvClassIsSuperClassedBy.Location = new System.Drawing.Point(3, 27);
            this.tvClassIsSuperClassedBy.Name = "tvClassIsSuperClassedBy";
            this.tvClassIsSuperClassedBy.Size = new System.Drawing.Size(374, 368);
            this.tvClassIsSuperClassedBy.TabIndex = 16;
            this.tvClassIsSuperClassedBy.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvClassIsSuperClassedBy_BeforeExpand);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(141, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Class is superclassed by (i.e.";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(3, 43);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(739, 432);
            this.tabControl1.TabIndex = 15;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(731, 406);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Function calls and callees graphs";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(731, 406);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Attributes";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tvClassAttributes
            // 
            this.tvClassAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvClassAttributes.Location = new System.Drawing.Point(3, 16);
            this.tvClassAttributes.Name = "tvClassAttributes";
            this.tvClassAttributes.Size = new System.Drawing.Size(719, 177);
            this.tvClassAttributes.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(582, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Function calls and callees";
            this.label4.Visible = false;
            // 
            // tvFunctionAttributes
            // 
            this.tvFunctionAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvFunctionAttributes.Location = new System.Drawing.Point(3, 16);
            this.tvFunctionAttributes.Name = "tvFunctionAttributes";
            this.tvFunctionAttributes.Size = new System.Drawing.Size(719, 181);
            this.tvFunctionAttributes.TabIndex = 16;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitContainer4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(731, 406);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Class\' SuperClass graphs";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.cbIgnoreCoreObjectClass);
            this.tabPage4.Controls.Add(this.cbViewInheritedMethods);
            this.tabPage4.Controls.Add(this.functionViewerForClassMethods);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(731, 406);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Class methods";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // cbIgnoreCoreObjectClass
            // 
            this.cbIgnoreCoreObjectClass.AutoSize = true;
            this.cbIgnoreCoreObjectClass.Checked = true;
            this.cbIgnoreCoreObjectClass.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIgnoreCoreObjectClass.Location = new System.Drawing.Point(149, 8);
            this.cbIgnoreCoreObjectClass.Name = "cbIgnoreCoreObjectClass";
            this.cbIgnoreCoreObjectClass.Size = new System.Drawing.Size(143, 17);
            this.cbIgnoreCoreObjectClass.TabIndex = 2;
            this.cbIgnoreCoreObjectClass.Text = "Ignore Core Object Class";
            this.cbIgnoreCoreObjectClass.UseVisualStyleBackColor = true;
            this.cbIgnoreCoreObjectClass.CheckedChanged += new System.EventHandler(this.cbIgnoreCoreObjectClass_CheckedChanged);
            // 
            // cbViewInheritedMethods
            // 
            this.cbViewInheritedMethods.AutoSize = true;
            this.cbViewInheritedMethods.Checked = true;
            this.cbViewInheritedMethods.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbViewInheritedMethods.Location = new System.Drawing.Point(7, 7);
            this.cbViewInheritedMethods.Name = "cbViewInheritedMethods";
            this.cbViewInheritedMethods.Size = new System.Drawing.Size(136, 17);
            this.cbViewInheritedMethods.TabIndex = 0;
            this.cbViewInheritedMethods.Text = "View Inherited methods";
            this.cbViewInheritedMethods.UseVisualStyleBackColor = true;
            this.cbViewInheritedMethods.CheckedChanged += new System.EventHandler(this.cbViewInheritedMethods_CheckedChanged);
            // 
            // functionViewerForClassMethods
            // 
            this.functionViewerForClassMethods._AdvancedModeViews = true;
            this.functionViewerForClassMethods.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.functionViewerForClassMethods.BackColor = System.Drawing.SystemColors.Control;
            this.functionViewerForClassMethods.ForeColor = System.Drawing.Color.Black;
            this.functionViewerForClassMethods.Location = new System.Drawing.Point(7, 31);
            this.functionViewerForClassMethods.Name = "functionViewerForClassMethods";
            this.functionViewerForClassMethods.NamespaceDepthValue = 2;
            this.functionViewerForClassMethods.Size = new System.Drawing.Size(654, 328);
            this.functionViewerForClassMethods.TabIndex = 1;
            // 
            // cbShowLineInSourceFile
            // 
            this.cbShowLineInSourceFile.AutoSize = true;
            this.cbShowLineInSourceFile.Location = new System.Drawing.Point(190, 23);
            this.cbShowLineInSourceFile.Name = "cbShowLineInSourceFile";
            this.cbShowLineInSourceFile.Size = new System.Drawing.Size(210, 17);
            this.cbShowLineInSourceFile.TabIndex = 52;
            this.cbShowLineInSourceFile.Text = "Open Source Code of selected method";
            this.cbShowLineInSourceFile.UseVisualStyleBackColor = true;
            // 
            // cbDontExpandRecursiveCalls
            // 
            this.cbDontExpandRecursiveCalls.AutoSize = true;
            this.cbDontExpandRecursiveCalls.Checked = true;
            this.cbDontExpandRecursiveCalls.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDontExpandRecursiveCalls.Location = new System.Drawing.Point(406, 23);
            this.cbDontExpandRecursiveCalls.Name = "cbDontExpandRecursiveCalls";
            this.cbDontExpandRecursiveCalls.Size = new System.Drawing.Size(278, 17);
            this.cbDontExpandRecursiveCalls.TabIndex = 53;
            this.cbDontExpandRecursiveCalls.Text = "Dont expand recursive calls (on \'Function calls graph)";
            this.cbDontExpandRecursiveCalls.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(725, 400);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.TabIndex = 19;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tvFunctionAttributes);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(725, 200);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Function Attributes";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tvClassAttributes);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(725, 196);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Class Attributes";
            // 
            // ascx_FunctionCalls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbDontExpandRecursiveCalls);
            this.Controls.Add(this.tvFunctionInfo);
            this.Controls.Add(this.cbShowLineInSourceFile);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.cbCirFunction_IsTainted);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.laFunctionViewed);
            this.Controls.Add(this.label1);
            this.Name = "ascx_FunctionCalls";
            this.Size = new System.Drawing.Size(745, 478);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            this.splitContainer4.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvFunctionInfo;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView tvFunctionMakesCallsTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView tvFunctionIsCalledBy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label laFunctionViewed;
        private System.Windows.Forms.CheckBox cbCirFunction_IsTainted;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.TreeView tvClassSuperClasses;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TreeView tvClassIsSuperClassedBy;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.CheckBox cbViewInheritedMethods;
        private O2.Views.ASCX.DataViewers.ascx_FunctionsViewer functionViewerForClassMethods;
        private System.Windows.Forms.CheckBox cbIgnoreCoreObjectClass;
        private System.Windows.Forms.CheckBox cbShowLineInSourceFile;
        private System.Windows.Forms.CheckBox cbDontExpandRecursiveCalls;
        private System.Windows.Forms.TreeView tvClassAttributes;
        private System.Windows.Forms.TreeView tvFunctionAttributes;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}
