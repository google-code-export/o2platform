namespace O2.Legacy.OunceV6.GLEEGraphWiz.Ascx
{
    partial class ascx_Glee
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
            this.lbGLEE_SelectedNode = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbGLEE_order = new System.Windows.Forms.CheckBox();
            this.tvGLEE_NodesToGraph = new System.Windows.Forms.TreeView();
            this.cbLDDB_ShowInsideNode_MethodName = new System.Windows.Forms.CheckBox();
            this.cbLDDB_ShowInsideNode_SourceCode = new System.Windows.Forms.CheckBox();
            this.cbLDDB_ShowInsideNode_Context = new System.Windows.Forms.CheckBox();
            this.cbGLEE_ShowReturnClass = new System.Windows.Forms.CheckBox();
            this.cbGLEE_ShowNamespace = new System.Windows.Forms.CheckBox();
            this.gvSmartTrace = new Microsoft.Glee.GraphViewerGdi.GViewer();
            this.cbGlee_NodeShape = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbGLEE_ShowParameters = new System.Windows.Forms.CheckBox();
            this.cbGLEE_MultiNodes = new System.Windows.Forms.CheckBox();
            this.scTopLeftAndRight = new System.Windows.Forms.SplitContainer();
            this.scTracesAndScripts = new System.Windows.Forms.SplitContainer();
            this.rbGraphAllNodes = new System.Windows.Forms.RadioButton();
            this.rbGraphSelectedNode = new System.Windows.Forms.RadioButton();
            this.scGleeAndAnalysis = new System.Windows.Forms.SplitContainer();
            this.btRemoveSelectedNode = new System.Windows.Forms.Button();
            this.cbDrawGleeGraph = new System.Windows.Forms.CheckBox();
            this.cbOnlyShowDataFor_SourcesAndSinks = new System.Windows.Forms.CheckBox();
            this.cbGLEE_ConsolidateTraces = new System.Windows.Forms.CheckBox();
            this.btAnimateTrace = new System.Windows.Forms.Button();
            this.cbVisibleControls_TracesGraphed = new System.Windows.Forms.CheckBox();
            this.cbVisibleControls_GleeDrawingOptions = new System.Windows.Forms.CheckBox();
            this.cbVisibleControls_GraphStats = new System.Windows.Forms.CheckBox();
            this.cbVisibleControls_CustomFilters = new System.Windows.Forms.CheckBox();
            this.gbVisibleControls = new System.Windows.Forms.GroupBox();
            this.scTopAndBottom = new System.Windows.Forms.SplitContainer();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMaxToPlot = new System.Windows.Forms.TextBox();
            this.btCreateCustomSavedAssessmentRunFile = new System.Windows.Forms.Button();
            this.btRemoveNodes_NotSelected = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbOnFunctionSelect_Show = new System.Windows.Forms.RadioButton();
            this.rbOnFunctionSelect_HighlightAllCalls_To = new System.Windows.Forms.RadioButton();
            this.rbOnFunctionSelect_HighlighAllCalls_From = new System.Windows.Forms.RadioButton();
            this.rbOnFunctionSelect_Colapse = new System.Windows.Forms.RadioButton();
            this.rbOnFunctionSelect_Expand = new System.Windows.Forms.RadioButton();
            this.rbOnFunctionSelect_Remove = new System.Windows.Forms.RadioButton();
            this.rbOnFunctionSelect_Clear = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cBoxShowFunctionClass_Depth = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbConsolidateDepth = new System.Windows.Forms.ComboBox();
            this.cbOnlyShowDataFor_LostSources = new System.Windows.Forms.CheckBox();
            this.tbOnlyShowDataFor_Class = new System.Windows.Forms.TextBox();
            this.cbOnlyShowDataFor_ConsolidateNonVisibleNodes = new System.Windows.Forms.CheckBox();
            this.cbOnlyShowDataFor_Class = new System.Windows.Forms.CheckBox();
            this.btClearGraph = new System.Windows.Forms.Button();
            this.btRefreshGraph = new System.Windows.Forms.Button();
            this.gdmcrGraphDataMappedToCustomRules = new O2.Legacy.OunceV6.GLEEGraphWiz.Ascx.ascx_GraphDataMappedToCustomRules();
            this.scTopLeftAndRight.Panel1.SuspendLayout();
            this.scTopLeftAndRight.Panel2.SuspendLayout();
            this.scTopLeftAndRight.SuspendLayout();
            this.scTracesAndScripts.Panel1.SuspendLayout();
            this.scTracesAndScripts.SuspendLayout();
            this.scGleeAndAnalysis.Panel1.SuspendLayout();
            this.scGleeAndAnalysis.Panel2.SuspendLayout();
            this.scGleeAndAnalysis.SuspendLayout();
            this.gbVisibleControls.SuspendLayout();
            this.scTopAndBottom.Panel1.SuspendLayout();
            this.scTopAndBottom.Panel2.SuspendLayout();
            this.scTopAndBottom.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbGLEE_SelectedNode
            // 
            this.lbGLEE_SelectedNode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbGLEE_SelectedNode.AutoSize = true;
            this.lbGLEE_SelectedNode.Location = new System.Drawing.Point(91, 4);
            this.lbGLEE_SelectedNode.Name = "lbGLEE_SelectedNode";
            this.lbGLEE_SelectedNode.Size = new System.Drawing.Size(16, 13);
            this.lbGLEE_SelectedNode.TabIndex = 84;
            this.lbGLEE_SelectedNode.Text = "...";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 83;
            this.label3.Text = "Selected node:";
            // 
            // cbGLEE_order
            // 
            this.cbGLEE_order.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbGLEE_order.AutoSize = true;
            this.cbGLEE_order.Location = new System.Drawing.Point(696, 42);
            this.cbGLEE_order.Name = "cbGLEE_order";
            this.cbGLEE_order.Size = new System.Drawing.Size(133, 17);
            this.cbGLEE_order.TabIndex = 81;
            this.cbGLEE_order.Text = "Order (of arrow\'s flows)";
            this.cbGLEE_order.UseVisualStyleBackColor = true;
            this.cbGLEE_order.CheckedChanged += new System.EventHandler(this.cbGLEE_order_CheckedChanged);
            // 
            // tvGLEE_NodesToGraph
            // 
            this.tvGLEE_NodesToGraph.AllowDrop = true;
            this.tvGLEE_NodesToGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvGLEE_NodesToGraph.Location = new System.Drawing.Point(3, 22);
            this.tvGLEE_NodesToGraph.Name = "tvGLEE_NodesToGraph";
            this.tvGLEE_NodesToGraph.Size = new System.Drawing.Size(157, 87);
            this.tvGLEE_NodesToGraph.TabIndex = 80;
            this.tvGLEE_NodesToGraph.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvGLEE_NodesToGraph_DragDrop);
            this.tvGLEE_NodesToGraph.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvGLEE_NodesToGraph_AfterSelect);
            this.tvGLEE_NodesToGraph.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvGLEE_NodesToGraph_DragEnter);
            // 
            // cbLDDB_ShowInsideNode_MethodName
            // 
            this.cbLDDB_ShowInsideNode_MethodName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbLDDB_ShowInsideNode_MethodName.AutoSize = true;
            this.cbLDDB_ShowInsideNode_MethodName.Checked = true;
            this.cbLDDB_ShowInsideNode_MethodName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLDDB_ShowInsideNode_MethodName.Location = new System.Drawing.Point(696, 65);
            this.cbLDDB_ShowInsideNode_MethodName.Name = "cbLDDB_ShowInsideNode_MethodName";
            this.cbLDDB_ShowInsideNode_MethodName.Size = new System.Drawing.Size(184, 17);
            this.cbLDDB_ShowInsideNode_MethodName.TabIndex = 78;
            this.cbLDDB_ShowInsideNode_MethodName.Text = "Show Method name (inside node)";
            this.cbLDDB_ShowInsideNode_MethodName.UseVisualStyleBackColor = true;
            this.cbLDDB_ShowInsideNode_MethodName.CheckedChanged += new System.EventHandler(this.cbLDDB_ShowInsideNode_MethodName_CheckedChanged);
            // 
            // cbLDDB_ShowInsideNode_SourceCode
            // 
            this.cbLDDB_ShowInsideNode_SourceCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbLDDB_ShowInsideNode_SourceCode.AutoSize = true;
            this.cbLDDB_ShowInsideNode_SourceCode.Location = new System.Drawing.Point(696, 103);
            this.cbLDDB_ShowInsideNode_SourceCode.Name = "cbLDDB_ShowInsideNode_SourceCode";
            this.cbLDDB_ShowInsideNode_SourceCode.Size = new System.Drawing.Size(178, 17);
            this.cbLDDB_ShowInsideNode_SourceCode.TabIndex = 77;
            this.cbLDDB_ShowInsideNode_SourceCode.Text = "Show source code (inside node)";
            this.cbLDDB_ShowInsideNode_SourceCode.UseVisualStyleBackColor = true;
            this.cbLDDB_ShowInsideNode_SourceCode.CheckedChanged += new System.EventHandler(this.cbLDDB_ShowInsideNode_SourceCode_CheckedChanged);
            // 
            // cbLDDB_ShowInsideNode_Context
            // 
            this.cbLDDB_ShowInsideNode_Context.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbLDDB_ShowInsideNode_Context.AutoSize = true;
            this.cbLDDB_ShowInsideNode_Context.Location = new System.Drawing.Point(696, 84);
            this.cbLDDB_ShowInsideNode_Context.Name = "cbLDDB_ShowInsideNode_Context";
            this.cbLDDB_ShowInsideNode_Context.Size = new System.Drawing.Size(155, 17);
            this.cbLDDB_ShowInsideNode_Context.TabIndex = 76;
            this.cbLDDB_ShowInsideNode_Context.Text = "Show Context (inside node)";
            this.cbLDDB_ShowInsideNode_Context.UseVisualStyleBackColor = true;
            this.cbLDDB_ShowInsideNode_Context.CheckedChanged += new System.EventHandler(this.cbLDDB_ShowInsideNode_Context_CheckedChanged);
            // 
            // cbGLEE_ShowReturnClass
            // 
            this.cbGLEE_ShowReturnClass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbGLEE_ShowReturnClass.AutoSize = true;
            this.cbGLEE_ShowReturnClass.Location = new System.Drawing.Point(568, 60);
            this.cbGLEE_ShowReturnClass.Name = "cbGLEE_ShowReturnClass";
            this.cbGLEE_ShowReturnClass.Size = new System.Drawing.Size(116, 17);
            this.cbGLEE_ShowReturnClass.TabIndex = 75;
            this.cbGLEE_ShowReturnClass.Text = "Show Return Class";
            this.cbGLEE_ShowReturnClass.UseVisualStyleBackColor = true;
            this.cbGLEE_ShowReturnClass.CheckedChanged += new System.EventHandler(this.cbGLEE_ShowReturnClass_CheckedChanged);
            // 
            // cbGLEE_ShowNamespace
            // 
            this.cbGLEE_ShowNamespace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbGLEE_ShowNamespace.AutoSize = true;
            this.cbGLEE_ShowNamespace.Location = new System.Drawing.Point(568, 81);
            this.cbGLEE_ShowNamespace.Name = "cbGLEE_ShowNamespace";
            this.cbGLEE_ShowNamespace.Size = new System.Drawing.Size(125, 17);
            this.cbGLEE_ShowNamespace.TabIndex = 74;
            this.cbGLEE_ShowNamespace.Text = "Show Function Class";
            this.cbGLEE_ShowNamespace.UseVisualStyleBackColor = true;
            this.cbGLEE_ShowNamespace.CheckedChanged += new System.EventHandler(this.cbGLEE_ShowNamespace_CheckedChanged);
            // 
            // gvSmartTrace
            // 
            this.gvSmartTrace.AsyncLayout = false;
            this.gvSmartTrace.AutoScroll = true;
            this.gvSmartTrace.BackwardEnabled = false;
            this.gvSmartTrace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvSmartTrace.ForwardEnabled = false;
            this.gvSmartTrace.Graph = null;
            this.gvSmartTrace.Location = new System.Drawing.Point(0, 0);
            this.gvSmartTrace.MouseHitDistance = 0.05;
            this.gvSmartTrace.Name = "gvSmartTrace";
            this.gvSmartTrace.NavigationVisible = true;
            this.gvSmartTrace.PanButtonPressed = false;
            this.gvSmartTrace.SaveButtonVisible = true;
            this.gvSmartTrace.Size = new System.Drawing.Size(727, 189);
            this.gvSmartTrace.TabIndex = 71;
            this.gvSmartTrace.ZoomF = 1;
            this.gvSmartTrace.ZoomFraction = 0.5;
            this.gvSmartTrace.ZoomWindowThreshold = 0.05;
            this.gvSmartTrace.DoubleClick += new System.EventHandler(this.gvSmartTrace_DoubleClick);
            this.gvSmartTrace.SelectionChanged += new System.EventHandler(this.gvSmartTrace_SelectionChanged);
            this.gvSmartTrace.Click += new System.EventHandler(this.gvSmartTrace_Click);
            this.gvSmartTrace.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gvSmartTrace_MouseMove_1);
            this.gvSmartTrace.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gvSmartTrace_MouseDoubleClick_1);
            this.gvSmartTrace.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gvSmartTrace_MouseClick);
            this.gvSmartTrace.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvSmartTrace_MouseDown);
            this.gvSmartTrace.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gvSmartTrace_MouseUp);
            // 
            // cbGlee_NodeShape
            // 
            this.cbGlee_NodeShape.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbGlee_NodeShape.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGlee_NodeShape.FormattingEnabled = true;
            this.cbGlee_NodeShape.Location = new System.Drawing.Point(835, 40);
            this.cbGlee_NodeShape.Name = "cbGlee_NodeShape";
            this.cbGlee_NodeShape.Size = new System.Drawing.Size(57, 21);
            this.cbGlee_NodeShape.TabIndex = 72;
            this.cbGlee_NodeShape.SelectedIndexChanged += new System.EventHandler(this.cbGlee_NodeShape_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 87;
            this.label2.Text = "Traces Graphed";
            // 
            // cbGLEE_ShowParameters
            // 
            this.cbGLEE_ShowParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbGLEE_ShowParameters.AutoSize = true;
            this.cbGLEE_ShowParameters.Location = new System.Drawing.Point(568, 40);
            this.cbGLEE_ShowParameters.Name = "cbGLEE_ShowParameters";
            this.cbGLEE_ShowParameters.Size = new System.Drawing.Size(109, 17);
            this.cbGLEE_ShowParameters.TabIndex = 85;
            this.cbGLEE_ShowParameters.Text = "Show Parameters";
            this.cbGLEE_ShowParameters.UseVisualStyleBackColor = true;
            this.cbGLEE_ShowParameters.CheckedChanged += new System.EventHandler(this.cbGLEE_ShowParameters_CheckedChanged);
            // 
            // cbGLEE_MultiNodes
            // 
            this.cbGLEE_MultiNodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbGLEE_MultiNodes.AutoSize = true;
            this.cbGLEE_MultiNodes.Location = new System.Drawing.Point(568, 17);
            this.cbGLEE_MultiNodes.Name = "cbGLEE_MultiNodes";
            this.cbGLEE_MultiNodes.Size = new System.Drawing.Size(82, 17);
            this.cbGLEE_MultiNodes.TabIndex = 88;
            this.cbGLEE_MultiNodes.Text = "Multi Nodes";
            this.cbGLEE_MultiNodes.UseVisualStyleBackColor = true;
            this.cbGLEE_MultiNodes.CheckedChanged += new System.EventHandler(this.cbGLEE_MultiNodes_CheckedChanged);
            // 
            // scTopLeftAndRight
            // 
            this.scTopLeftAndRight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scTopLeftAndRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scTopLeftAndRight.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scTopLeftAndRight.Location = new System.Drawing.Point(0, 0);
            this.scTopLeftAndRight.Name = "scTopLeftAndRight";
            // 
            // scTopLeftAndRight.Panel1
            // 
            this.scTopLeftAndRight.Panel1.Controls.Add(this.scTracesAndScripts);
            // 
            // scTopLeftAndRight.Panel2
            // 
            this.scTopLeftAndRight.Panel2.Controls.Add(this.scGleeAndAnalysis);
            this.scTopLeftAndRight.Size = new System.Drawing.Size(900, 308);
            this.scTopLeftAndRight.SplitterDistance = 165;
            this.scTopLeftAndRight.TabIndex = 89;
            // 
            // scTracesAndScripts
            // 
            this.scTracesAndScripts.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scTracesAndScripts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scTracesAndScripts.Location = new System.Drawing.Point(0, 0);
            this.scTracesAndScripts.Name = "scTracesAndScripts";
            this.scTracesAndScripts.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scTracesAndScripts.Panel1
            // 
            this.scTracesAndScripts.Panel1.Controls.Add(this.rbGraphAllNodes);
            this.scTracesAndScripts.Panel1.Controls.Add(this.rbGraphSelectedNode);
            this.scTracesAndScripts.Panel1.Controls.Add(this.tvGLEE_NodesToGraph);
            this.scTracesAndScripts.Panel1.Controls.Add(this.label2);
            this.scTracesAndScripts.Size = new System.Drawing.Size(165, 308);
            this.scTracesAndScripts.SplitterDistance = 162;
            this.scTracesAndScripts.TabIndex = 0;
            // 
            // rbGraphAllNodes
            // 
            this.rbGraphAllNodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbGraphAllNodes.AutoSize = true;
            this.rbGraphAllNodes.Checked = true;
            this.rbGraphAllNodes.Location = new System.Drawing.Point(6, 115);
            this.rbGraphAllNodes.Name = "rbGraphAllNodes";
            this.rbGraphAllNodes.Size = new System.Drawing.Size(102, 17);
            this.rbGraphAllNodes.TabIndex = 92;
            this.rbGraphAllNodes.TabStop = true;
            this.rbGraphAllNodes.Text = "Graph All Nodes";
            this.rbGraphAllNodes.UseVisualStyleBackColor = true;
            this.rbGraphAllNodes.CheckedChanged += new System.EventHandler(this.rbGraphAllNodes_CheckedChanged);
            // 
            // rbGraphSelectedNode
            // 
            this.rbGraphSelectedNode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbGraphSelectedNode.AutoSize = true;
            this.rbGraphSelectedNode.Location = new System.Drawing.Point(6, 138);
            this.rbGraphSelectedNode.Name = "rbGraphSelectedNode";
            this.rbGraphSelectedNode.Size = new System.Drawing.Size(128, 17);
            this.rbGraphSelectedNode.TabIndex = 91;
            this.rbGraphSelectedNode.Text = "Graph Selected Node";
            this.rbGraphSelectedNode.UseVisualStyleBackColor = true;
            this.rbGraphSelectedNode.CheckedChanged += new System.EventHandler(this.rbGraphSelectedNode_CheckedChanged);
            // 
            // scGleeAndAnalysis
            // 
            this.scGleeAndAnalysis.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scGleeAndAnalysis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scGleeAndAnalysis.Location = new System.Drawing.Point(0, 0);
            this.scGleeAndAnalysis.Name = "scGleeAndAnalysis";
            this.scGleeAndAnalysis.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scGleeAndAnalysis.Panel1
            // 
            this.scGleeAndAnalysis.Panel1.Controls.Add(this.btRemoveSelectedNode);
            this.scGleeAndAnalysis.Panel1.Controls.Add(this.gvSmartTrace);
            // 
            // scGleeAndAnalysis.Panel2
            // 
            this.scGleeAndAnalysis.Panel2.Controls.Add(this.gdmcrGraphDataMappedToCustomRules);
            this.scGleeAndAnalysis.Size = new System.Drawing.Size(731, 308);
            this.scGleeAndAnalysis.SplitterDistance = 193;
            this.scGleeAndAnalysis.TabIndex = 0;
            // 
            // btRemoveSelectedNode
            // 
            this.btRemoveSelectedNode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btRemoveSelectedNode.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btRemoveSelectedNode.Location = new System.Drawing.Point(447, 3);
            this.btRemoveSelectedNode.Name = "btRemoveSelectedNode";
            this.btRemoveSelectedNode.Size = new System.Drawing.Size(165, 19);
            this.btRemoveSelectedNode.TabIndex = 100;
            this.btRemoveSelectedNode.Text = "Remove Selected Node";
            this.btRemoveSelectedNode.UseVisualStyleBackColor = false;
            this.btRemoveSelectedNode.Click += new System.EventHandler(this.btRemoveSelectedNode_Click);
            // 
            // cbDrawGleeGraph
            // 
            this.cbDrawGleeGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDrawGleeGraph.AutoSize = true;
            this.cbDrawGleeGraph.Checked = true;
            this.cbDrawGleeGraph.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDrawGleeGraph.Location = new System.Drawing.Point(580, 14);
            this.cbDrawGleeGraph.Name = "cbDrawGleeGraph";
            this.cbDrawGleeGraph.Size = new System.Drawing.Size(108, 17);
            this.cbDrawGleeGraph.TabIndex = 72;
            this.cbDrawGleeGraph.Text = "Draw Glee Graph";
            this.cbDrawGleeGraph.UseVisualStyleBackColor = true;
            this.cbDrawGleeGraph.CheckedChanged += new System.EventHandler(this.cbDrawGleeGraph_CheckedChanged);
            // 
            // cbOnlyShowDataFor_SourcesAndSinks
            // 
            this.cbOnlyShowDataFor_SourcesAndSinks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbOnlyShowDataFor_SourcesAndSinks.AutoSize = true;
            this.cbOnlyShowDataFor_SourcesAndSinks.Checked = true;
            this.cbOnlyShowDataFor_SourcesAndSinks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOnlyShowDataFor_SourcesAndSinks.Location = new System.Drawing.Point(6, 16);
            this.cbOnlyShowDataFor_SourcesAndSinks.Name = "cbOnlyShowDataFor_SourcesAndSinks";
            this.cbOnlyShowDataFor_SourcesAndSinks.Size = new System.Drawing.Size(113, 17);
            this.cbOnlyShowDataFor_SourcesAndSinks.TabIndex = 90;
            this.cbOnlyShowDataFor_SourcesAndSinks.Text = "Sources and sinks";
            this.cbOnlyShowDataFor_SourcesAndSinks.UseVisualStyleBackColor = true;
            this.cbOnlyShowDataFor_SourcesAndSinks.CheckedChanged += new System.EventHandler(this.cbOnlyShowDataFor_SourcesAndSinks_CheckedChanged);
            // 
            // cbGLEE_ConsolidateTraces
            // 
            this.cbGLEE_ConsolidateTraces.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbGLEE_ConsolidateTraces.AutoSize = true;
            this.cbGLEE_ConsolidateTraces.Location = new System.Drawing.Point(696, 19);
            this.cbGLEE_ConsolidateTraces.Name = "cbGLEE_ConsolidateTraces";
            this.cbGLEE_ConsolidateTraces.Size = new System.Drawing.Size(117, 17);
            this.cbGLEE_ConsolidateTraces.TabIndex = 91;
            this.cbGLEE_ConsolidateTraces.Text = "Consolidate Traces";
            this.cbGLEE_ConsolidateTraces.UseVisualStyleBackColor = true;
            this.cbGLEE_ConsolidateTraces.CheckedChanged += new System.EventHandler(this.cbGLEE_ConsolidateTraces_CheckedChanged);
            // 
            // btAnimateTrace
            // 
            this.btAnimateTrace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btAnimateTrace.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btAnimateTrace.Enabled = false;
            this.btAnimateTrace.Location = new System.Drawing.Point(3, 43);
            this.btAnimateTrace.Name = "btAnimateTrace";
            this.btAnimateTrace.Size = new System.Drawing.Size(86, 19);
            this.btAnimateTrace.TabIndex = 92;
            this.btAnimateTrace.Text = "Animate Trace";
            this.btAnimateTrace.UseVisualStyleBackColor = false;
            this.btAnimateTrace.Click += new System.EventHandler(this.btAnimateTrace_Click);
            // 
            // cbVisibleControls_TracesGraphed
            // 
            this.cbVisibleControls_TracesGraphed.AutoSize = true;
            this.cbVisibleControls_TracesGraphed.Location = new System.Drawing.Point(147, 14);
            this.cbVisibleControls_TracesGraphed.Name = "cbVisibleControls_TracesGraphed";
            this.cbVisibleControls_TracesGraphed.Size = new System.Drawing.Size(103, 17);
            this.cbVisibleControls_TracesGraphed.TabIndex = 94;
            this.cbVisibleControls_TracesGraphed.Text = "Traces Graphed";
            this.cbVisibleControls_TracesGraphed.UseVisualStyleBackColor = true;
            this.cbVisibleControls_TracesGraphed.CheckedChanged += new System.EventHandler(this.cbVisibleControls_TracesGraphed_CheckedChanged);
            // 
            // cbVisibleControls_GleeDrawingOptions
            // 
            this.cbVisibleControls_GleeDrawingOptions.AutoSize = true;
            this.cbVisibleControls_GleeDrawingOptions.Checked = true;
            this.cbVisibleControls_GleeDrawingOptions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbVisibleControls_GleeDrawingOptions.Location = new System.Drawing.Point(12, 14);
            this.cbVisibleControls_GleeDrawingOptions.Name = "cbVisibleControls_GleeDrawingOptions";
            this.cbVisibleControls_GleeDrawingOptions.Size = new System.Drawing.Size(129, 17);
            this.cbVisibleControls_GleeDrawingOptions.TabIndex = 95;
            this.cbVisibleControls_GleeDrawingOptions.Text = "Glee Drawing Options";
            this.cbVisibleControls_GleeDrawingOptions.UseVisualStyleBackColor = true;
            this.cbVisibleControls_GleeDrawingOptions.CheckedChanged += new System.EventHandler(this.cbVisibleControls_GleeDrawingOptions_CheckedChanged);
            // 
            // cbVisibleControls_GraphStats
            // 
            this.cbVisibleControls_GraphStats.AutoSize = true;
            this.cbVisibleControls_GraphStats.Location = new System.Drawing.Point(257, 14);
            this.cbVisibleControls_GraphStats.Name = "cbVisibleControls_GraphStats";
            this.cbVisibleControls_GraphStats.Size = new System.Drawing.Size(206, 17);
            this.cbVisibleControls_GraphStats.TabIndex = 96;
            this.cbVisibleControls_GraphStats.Text = "Graph Stats (Vertices, Sources, Sinks)";
            this.cbVisibleControls_GraphStats.UseVisualStyleBackColor = true;
            this.cbVisibleControls_GraphStats.CheckedChanged += new System.EventHandler(this.cbVisibleControls_GraphStats_CheckedChanged);
            // 
            // cbVisibleControls_CustomFilters
            // 
            this.cbVisibleControls_CustomFilters.AutoSize = true;
            this.cbVisibleControls_CustomFilters.Location = new System.Drawing.Point(466, 14);
            this.cbVisibleControls_CustomFilters.Name = "cbVisibleControls_CustomFilters";
            this.cbVisibleControls_CustomFilters.Size = new System.Drawing.Size(91, 17);
            this.cbVisibleControls_CustomFilters.TabIndex = 97;
            this.cbVisibleControls_CustomFilters.Text = "Custom Filters";
            this.cbVisibleControls_CustomFilters.UseVisualStyleBackColor = true;
            this.cbVisibleControls_CustomFilters.CheckedChanged += new System.EventHandler(this.cbVisibleControls_CustomFilters_CheckedChanged);
            // 
            // gbVisibleControls
            // 
            this.gbVisibleControls.Controls.Add(this.cbVisibleControls_GleeDrawingOptions);
            this.gbVisibleControls.Controls.Add(this.cbDrawGleeGraph);
            this.gbVisibleControls.Controls.Add(this.cbVisibleControls_GraphStats);
            this.gbVisibleControls.Controls.Add(this.cbVisibleControls_CustomFilters);
            this.gbVisibleControls.Controls.Add(this.cbVisibleControls_TracesGraphed);
            this.gbVisibleControls.ForeColor = System.Drawing.Color.Gray;
            this.gbVisibleControls.Location = new System.Drawing.Point(3, 3);
            this.gbVisibleControls.Name = "gbVisibleControls";
            this.gbVisibleControls.Size = new System.Drawing.Size(702, 35);
            this.gbVisibleControls.TabIndex = 98;
            this.gbVisibleControls.TabStop = false;
            this.gbVisibleControls.Text = "GLEE Viewer: Select Visible Controls";
            // 
            // scTopAndBottom
            // 
            this.scTopAndBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.scTopAndBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scTopAndBottom.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scTopAndBottom.Location = new System.Drawing.Point(3, 40);
            this.scTopAndBottom.Name = "scTopAndBottom";
            this.scTopAndBottom.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scTopAndBottom.Panel1
            // 
            this.scTopAndBottom.Panel1.Controls.Add(this.scTopLeftAndRight);
            // 
            // scTopAndBottom.Panel2
            // 
            this.scTopAndBottom.Panel2.Controls.Add(this.label4);
            this.scTopAndBottom.Panel2.Controls.Add(this.tbMaxToPlot);
            this.scTopAndBottom.Panel2.Controls.Add(this.btCreateCustomSavedAssessmentRunFile);
            this.scTopAndBottom.Panel2.Controls.Add(this.btRemoveNodes_NotSelected);
            this.scTopAndBottom.Panel2.Controls.Add(this.groupBox2);
            this.scTopAndBottom.Panel2.Controls.Add(this.label1);
            this.scTopAndBottom.Panel2.Controls.Add(this.cBoxShowFunctionClass_Depth);
            this.scTopAndBottom.Panel2.Controls.Add(this.groupBox1);
            this.scTopAndBottom.Panel2.Controls.Add(this.btClearGraph);
            this.scTopAndBottom.Panel2.Controls.Add(this.btRefreshGraph);
            this.scTopAndBottom.Panel2.Controls.Add(this.cbGLEE_MultiNodes);
            this.scTopAndBottom.Panel2.Controls.Add(this.cbGlee_NodeShape);
            this.scTopAndBottom.Panel2.Controls.Add(this.btAnimateTrace);
            this.scTopAndBottom.Panel2.Controls.Add(this.cbGLEE_ShowNamespace);
            this.scTopAndBottom.Panel2.Controls.Add(this.cbGLEE_ConsolidateTraces);
            this.scTopAndBottom.Panel2.Controls.Add(this.cbGLEE_ShowReturnClass);
            this.scTopAndBottom.Panel2.Controls.Add(this.cbLDDB_ShowInsideNode_Context);
            this.scTopAndBottom.Panel2.Controls.Add(this.cbLDDB_ShowInsideNode_SourceCode);
            this.scTopAndBottom.Panel2.Controls.Add(this.cbLDDB_ShowInsideNode_MethodName);
            this.scTopAndBottom.Panel2.Controls.Add(this.cbGLEE_ShowParameters);
            this.scTopAndBottom.Panel2.Controls.Add(this.cbGLEE_order);
            this.scTopAndBottom.Panel2.Controls.Add(this.lbGLEE_SelectedNode);
            this.scTopAndBottom.Panel2.Controls.Add(this.label3);
            this.scTopAndBottom.Size = new System.Drawing.Size(900, 441);
            this.scTopAndBottom.SplitterDistance = 308;
            this.scTopAndBottom.TabIndex = 99;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(821, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 103;
            this.label4.Text = "Max:";
            // 
            // tbMaxToPlot
            // 
            this.tbMaxToPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbMaxToPlot.Location = new System.Drawing.Point(853, 18);
            this.tbMaxToPlot.Name = "tbMaxToPlot";
            this.tbMaxToPlot.Size = new System.Drawing.Size(39, 20);
            this.tbMaxToPlot.TabIndex = 102;
            this.tbMaxToPlot.Text = "80";
            this.tbMaxToPlot.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbMaxToPlot_KeyPress);
            // 
            // btCreateCustomSavedAssessmentRunFile
            // 
            this.btCreateCustomSavedAssessmentRunFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btCreateCustomSavedAssessmentRunFile.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btCreateCustomSavedAssessmentRunFile.Location = new System.Drawing.Point(432, 88);
            this.btCreateCustomSavedAssessmentRunFile.Name = "btCreateCustomSavedAssessmentRunFile";
            this.btCreateCustomSavedAssessmentRunFile.Size = new System.Drawing.Size(125, 28);
            this.btCreateCustomSavedAssessmentRunFile.TabIndex = 101;
            this.btCreateCustomSavedAssessmentRunFile.Text = "Create Assessment";
            this.btCreateCustomSavedAssessmentRunFile.UseVisualStyleBackColor = false;
            this.btCreateCustomSavedAssessmentRunFile.Click += new System.EventHandler(this.btCreateCustomSavedAssessmentRunFile_Click);
            // 
            // btRemoveNodes_NotSelected
            // 
            this.btRemoveNodes_NotSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btRemoveNodes_NotSelected.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btRemoveNodes_NotSelected.Location = new System.Drawing.Point(276, 88);
            this.btRemoveNodes_NotSelected.Name = "btRemoveNodes_NotSelected";
            this.btRemoveNodes_NotSelected.Size = new System.Drawing.Size(150, 28);
            this.btRemoveNodes_NotSelected.TabIndex = 100;
            this.btRemoveNodes_NotSelected.Text = "Remove Nodes not selected";
            this.btRemoveNodes_NotSelected.UseVisualStyleBackColor = false;
            this.btRemoveNodes_NotSelected.Click += new System.EventHandler(this.btRemoveNodes_NotSelected_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbOnFunctionSelect_Show);
            this.groupBox2.Controls.Add(this.rbOnFunctionSelect_HighlightAllCalls_To);
            this.groupBox2.Controls.Add(this.rbOnFunctionSelect_HighlighAllCalls_From);
            this.groupBox2.Controls.Add(this.rbOnFunctionSelect_Colapse);
            this.groupBox2.Controls.Add(this.rbOnFunctionSelect_Expand);
            this.groupBox2.Controls.Add(this.rbOnFunctionSelect_Remove);
            this.groupBox2.Controls.Add(this.rbOnFunctionSelect_Clear);
            this.groupBox2.Location = new System.Drawing.Point(273, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(289, 61);
            this.groupBox2.TabIndex = 99;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "On Function Select";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // rbOnFunctionSelect_Show
            // 
            this.rbOnFunctionSelect_Show.AutoSize = true;
            this.rbOnFunctionSelect_Show.Checked = true;
            this.rbOnFunctionSelect_Show.Location = new System.Drawing.Point(7, 13);
            this.rbOnFunctionSelect_Show.Name = "rbOnFunctionSelect_Show";
            this.rbOnFunctionSelect_Show.Size = new System.Drawing.Size(52, 17);
            this.rbOnFunctionSelect_Show.TabIndex = 6;
            this.rbOnFunctionSelect_Show.TabStop = true;
            this.rbOnFunctionSelect_Show.Text = "Show";
            this.rbOnFunctionSelect_Show.UseVisualStyleBackColor = true;
            // 
            // rbOnFunctionSelect_HighlightAllCalls_To
            // 
            this.rbOnFunctionSelect_HighlightAllCalls_To.AutoSize = true;
            this.rbOnFunctionSelect_HighlightAllCalls_To.Location = new System.Drawing.Point(138, 35);
            this.rbOnFunctionSelect_HighlightAllCalls_To.Name = "rbOnFunctionSelect_HighlightAllCalls_To";
            this.rbOnFunctionSelect_HighlightAllCalls_To.Size = new System.Drawing.Size(112, 17);
            this.rbOnFunctionSelect_HighlightAllCalls_To.TabIndex = 5;
            this.rbOnFunctionSelect_HighlightAllCalls_To.Text = "Highligh all calls to";
            this.rbOnFunctionSelect_HighlightAllCalls_To.UseVisualStyleBackColor = true;
            // 
            // rbOnFunctionSelect_HighlighAllCalls_From
            // 
            this.rbOnFunctionSelect_HighlighAllCalls_From.AutoSize = true;
            this.rbOnFunctionSelect_HighlighAllCalls_From.Enabled = false;
            this.rbOnFunctionSelect_HighlighAllCalls_From.Location = new System.Drawing.Point(7, 35);
            this.rbOnFunctionSelect_HighlighAllCalls_From.Name = "rbOnFunctionSelect_HighlighAllCalls_From";
            this.rbOnFunctionSelect_HighlighAllCalls_From.Size = new System.Drawing.Size(126, 17);
            this.rbOnFunctionSelect_HighlighAllCalls_From.TabIndex = 4;
            this.rbOnFunctionSelect_HighlighAllCalls_From.Text = "Highligh all calls From";
            this.rbOnFunctionSelect_HighlighAllCalls_From.UseVisualStyleBackColor = true;
            // 
            // rbOnFunctionSelect_Colapse
            // 
            this.rbOnFunctionSelect_Colapse.AutoSize = true;
            this.rbOnFunctionSelect_Colapse.Enabled = false;
            this.rbOnFunctionSelect_Colapse.Location = new System.Drawing.Point(225, 13);
            this.rbOnFunctionSelect_Colapse.Name = "rbOnFunctionSelect_Colapse";
            this.rbOnFunctionSelect_Colapse.Size = new System.Drawing.Size(63, 17);
            this.rbOnFunctionSelect_Colapse.TabIndex = 3;
            this.rbOnFunctionSelect_Colapse.Text = "Colapse";
            this.rbOnFunctionSelect_Colapse.UseVisualStyleBackColor = true;
            // 
            // rbOnFunctionSelect_Expand
            // 
            this.rbOnFunctionSelect_Expand.AutoSize = true;
            this.rbOnFunctionSelect_Expand.Enabled = false;
            this.rbOnFunctionSelect_Expand.Location = new System.Drawing.Point(168, 13);
            this.rbOnFunctionSelect_Expand.Name = "rbOnFunctionSelect_Expand";
            this.rbOnFunctionSelect_Expand.Size = new System.Drawing.Size(61, 17);
            this.rbOnFunctionSelect_Expand.TabIndex = 2;
            this.rbOnFunctionSelect_Expand.Text = "Expand";
            this.rbOnFunctionSelect_Expand.UseVisualStyleBackColor = true;
            // 
            // rbOnFunctionSelect_Remove
            // 
            this.rbOnFunctionSelect_Remove.AutoSize = true;
            this.rbOnFunctionSelect_Remove.Location = new System.Drawing.Point(106, 13);
            this.rbOnFunctionSelect_Remove.Name = "rbOnFunctionSelect_Remove";
            this.rbOnFunctionSelect_Remove.Size = new System.Drawing.Size(65, 17);
            this.rbOnFunctionSelect_Remove.TabIndex = 1;
            this.rbOnFunctionSelect_Remove.Text = "Remove";
            this.rbOnFunctionSelect_Remove.UseVisualStyleBackColor = true;
            // 
            // rbOnFunctionSelect_Clear
            // 
            this.rbOnFunctionSelect_Clear.AutoSize = true;
            this.rbOnFunctionSelect_Clear.Location = new System.Drawing.Point(59, 13);
            this.rbOnFunctionSelect_Clear.Name = "rbOnFunctionSelect_Clear";
            this.rbOnFunctionSelect_Clear.Size = new System.Drawing.Size(49, 17);
            this.rbOnFunctionSelect_Clear.TabIndex = 0;
            this.rbOnFunctionSelect_Clear.Text = "Clear";
            this.rbOnFunctionSelect_Clear.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(583, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 98;
            this.label1.Text = "Depth";
            // 
            // cBoxShowFunctionClass_Depth
            // 
            this.cBoxShowFunctionClass_Depth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cBoxShowFunctionClass_Depth.FormattingEnabled = true;
            this.cBoxShowFunctionClass_Depth.Location = new System.Drawing.Point(645, 99);
            this.cBoxShowFunctionClass_Depth.Name = "cBoxShowFunctionClass_Depth";
            this.cBoxShowFunctionClass_Depth.Size = new System.Drawing.Size(41, 21);
            this.cBoxShowFunctionClass_Depth.TabIndex = 97;
            this.cBoxShowFunctionClass_Depth.Text = "1";
            this.cBoxShowFunctionClass_Depth.SelectedIndexChanged += new System.EventHandler(this.cBoxShowFunctionClass_Depth_SelectedIndexChanged);
            this.cBoxShowFunctionClass_Depth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cBoxShowFunctionClass_Depth_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbConsolidateDepth);
            this.groupBox1.Controls.Add(this.cbOnlyShowDataFor_LostSources);
            this.groupBox1.Controls.Add(this.tbOnlyShowDataFor_Class);
            this.groupBox1.Controls.Add(this.cbOnlyShowDataFor_ConsolidateNonVisibleNodes);
            this.groupBox1.Controls.Add(this.cbOnlyShowDataFor_SourcesAndSinks);
            this.groupBox1.Controls.Add(this.cbOnlyShowDataFor_Class);
            this.groupBox1.Location = new System.Drawing.Point(95, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(175, 96);
            this.groupBox1.TabIndex = 96;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Only Show Data for:";
            // 
            // cbConsolidateDepth
            // 
            this.cbConsolidateDepth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbConsolidateDepth.FormattingEnabled = true;
            this.cbConsolidateDepth.Location = new System.Drawing.Point(125, 14);
            this.cbConsolidateDepth.Name = "cbConsolidateDepth";
            this.cbConsolidateDepth.Size = new System.Drawing.Size(41, 21);
            this.cbConsolidateDepth.TabIndex = 99;
            this.cbConsolidateDepth.Text = "-1";
            // 
            // cbOnlyShowDataFor_LostSources
            // 
            this.cbOnlyShowDataFor_LostSources.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbOnlyShowDataFor_LostSources.AutoSize = true;
            this.cbOnlyShowDataFor_LostSources.Location = new System.Drawing.Point(6, 36);
            this.cbOnlyShowDataFor_LostSources.Name = "cbOnlyShowDataFor_LostSources";
            this.cbOnlyShowDataFor_LostSources.Size = new System.Drawing.Size(88, 17);
            this.cbOnlyShowDataFor_LostSources.TabIndex = 98;
            this.cbOnlyShowDataFor_LostSources.Text = "Lost Sources";
            this.cbOnlyShowDataFor_LostSources.UseVisualStyleBackColor = true;
            this.cbOnlyShowDataFor_LostSources.Visible = false;
            this.cbOnlyShowDataFor_LostSources.CheckedChanged += new System.EventHandler(this.cbOnlyShowDataFor_LostSources_CheckedChanged);
            // 
            // tbOnlyShowDataFor_Class
            // 
            this.tbOnlyShowDataFor_Class.Location = new System.Drawing.Point(64, 57);
            this.tbOnlyShowDataFor_Class.Name = "tbOnlyShowDataFor_Class";
            this.tbOnlyShowDataFor_Class.Size = new System.Drawing.Size(70, 20);
            this.tbOnlyShowDataFor_Class.TabIndex = 97;
            // 
            // cbOnlyShowDataFor_ConsolidateNonVisibleNodes
            // 
            this.cbOnlyShowDataFor_ConsolidateNonVisibleNodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbOnlyShowDataFor_ConsolidateNonVisibleNodes.AutoSize = true;
            this.cbOnlyShowDataFor_ConsolidateNonVisibleNodes.Checked = true;
            this.cbOnlyShowDataFor_ConsolidateNonVisibleNodes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOnlyShowDataFor_ConsolidateNonVisibleNodes.Location = new System.Drawing.Point(6, 77);
            this.cbOnlyShowDataFor_ConsolidateNonVisibleNodes.Name = "cbOnlyShowDataFor_ConsolidateNonVisibleNodes";
            this.cbOnlyShowDataFor_ConsolidateNonVisibleNodes.Size = new System.Drawing.Size(166, 17);
            this.cbOnlyShowDataFor_ConsolidateNonVisibleNodes.TabIndex = 96;
            this.cbOnlyShowDataFor_ConsolidateNonVisibleNodes.Text = "Consolidate non visible nodes";
            this.cbOnlyShowDataFor_ConsolidateNonVisibleNodes.UseVisualStyleBackColor = true;
            this.cbOnlyShowDataFor_ConsolidateNonVisibleNodes.CheckedChanged += new System.EventHandler(this.cbOnlyShowDataFor_ConsolidateNonVisibleNodes_CheckedChanged);
            // 
            // cbOnlyShowDataFor_Class
            // 
            this.cbOnlyShowDataFor_Class.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbOnlyShowDataFor_Class.AutoSize = true;
            this.cbOnlyShowDataFor_Class.Location = new System.Drawing.Point(6, 58);
            this.cbOnlyShowDataFor_Class.Name = "cbOnlyShowDataFor_Class";
            this.cbOnlyShowDataFor_Class.Size = new System.Drawing.Size(51, 17);
            this.cbOnlyShowDataFor_Class.TabIndex = 95;
            this.cbOnlyShowDataFor_Class.Text = "Class";
            this.cbOnlyShowDataFor_Class.UseVisualStyleBackColor = true;
            this.cbOnlyShowDataFor_Class.CheckedChanged += new System.EventHandler(this.cbOnlyShowDataFor_Class_CheckedChanged);
            // 
            // btClearGraph
            // 
            this.btClearGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btClearGraph.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btClearGraph.Location = new System.Drawing.Point(3, 92);
            this.btClearGraph.Name = "btClearGraph";
            this.btClearGraph.Size = new System.Drawing.Size(86, 19);
            this.btClearGraph.TabIndex = 94;
            this.btClearGraph.Text = "Clear Graph";
            this.btClearGraph.UseVisualStyleBackColor = false;
            this.btClearGraph.Click += new System.EventHandler(this.btClearGraph_Click);
            // 
            // btRefreshGraph
            // 
            this.btRefreshGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btRefreshGraph.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btRefreshGraph.Location = new System.Drawing.Point(3, 67);
            this.btRefreshGraph.Name = "btRefreshGraph";
            this.btRefreshGraph.Size = new System.Drawing.Size(86, 19);
            this.btRefreshGraph.TabIndex = 93;
            this.btRefreshGraph.Text = "Refresh Graph";
            this.btRefreshGraph.UseVisualStyleBackColor = false;
            this.btRefreshGraph.Click += new System.EventHandler(this.btRefreshGraph_Click);
            // 
            // gdmcrGraphDataMappedToCustomRules
            // 
            this.gdmcrGraphDataMappedToCustomRules.BackColor = System.Drawing.SystemColors.Control;
            this.gdmcrGraphDataMappedToCustomRules.Dock = System.Windows.Forms.DockStyle.Top;
            this.gdmcrGraphDataMappedToCustomRules.ForeColor = System.Drawing.Color.DimGray;
            this.gdmcrGraphDataMappedToCustomRules.Location = new System.Drawing.Point(0, 0);
            this.gdmcrGraphDataMappedToCustomRules.Name = "gdmcrGraphDataMappedToCustomRules";
            this.gdmcrGraphDataMappedToCustomRules.Size = new System.Drawing.Size(727, 109);
            this.gdmcrGraphDataMappedToCustomRules.TabIndex = 0;
            this.gdmcrGraphDataMappedToCustomRules.Load += new System.EventHandler(this.gdmcrGraphDataMappedToCustomRules_Load);
            // 
            // ascx_Glee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.scTopAndBottom);
            this.Controls.Add(this.gbVisibleControls);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ascx_Glee";
            this.Size = new System.Drawing.Size(906, 481);
            this.Load += new System.EventHandler(this.ascx_Glee_Load);
            this.scTopLeftAndRight.Panel1.ResumeLayout(false);
            this.scTopLeftAndRight.Panel2.ResumeLayout(false);
            this.scTopLeftAndRight.ResumeLayout(false);
            this.scTracesAndScripts.Panel1.ResumeLayout(false);
            this.scTracesAndScripts.Panel1.PerformLayout();
            this.scTracesAndScripts.ResumeLayout(false);
            this.scGleeAndAnalysis.Panel1.ResumeLayout(false);
            this.scGleeAndAnalysis.Panel2.ResumeLayout(false);
            this.scGleeAndAnalysis.ResumeLayout(false);
            this.gbVisibleControls.ResumeLayout(false);
            this.gbVisibleControls.PerformLayout();
            this.scTopAndBottom.Panel1.ResumeLayout(false);
            this.scTopAndBottom.Panel2.ResumeLayout(false);
            this.scTopAndBottom.Panel2.PerformLayout();
            this.scTopAndBottom.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbGLEE_SelectedNode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbGLEE_order;
        private System.Windows.Forms.TreeView tvGLEE_NodesToGraph;
        private System.Windows.Forms.CheckBox cbLDDB_ShowInsideNode_MethodName;
        private System.Windows.Forms.CheckBox cbLDDB_ShowInsideNode_SourceCode;
        private System.Windows.Forms.CheckBox cbLDDB_ShowInsideNode_Context;
        private System.Windows.Forms.CheckBox cbGLEE_ShowReturnClass;
        private System.Windows.Forms.CheckBox cbGLEE_ShowNamespace;
        private Microsoft.Glee.GraphViewerGdi.GViewer gvSmartTrace;
        private System.Windows.Forms.ComboBox cbGlee_NodeShape;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbGLEE_ShowParameters;
        private System.Windows.Forms.CheckBox cbGLEE_MultiNodes;
        private System.Windows.Forms.SplitContainer scTopLeftAndRight;
        private System.Windows.Forms.SplitContainer scTracesAndScripts;
        //     private ascx_InvokeDynamicFilters ascx_InvokeDynamicFilters1;
        private System.Windows.Forms.SplitContainer scGleeAndAnalysis;
        private ascx_GraphDataMappedToCustomRules gdmcrGraphDataMappedToCustomRules;
        private System.Windows.Forms.RadioButton rbGraphAllNodes;
        private System.Windows.Forms.RadioButton rbGraphSelectedNode;
        private System.Windows.Forms.CheckBox cbOnlyShowDataFor_SourcesAndSinks;
        private System.Windows.Forms.CheckBox cbGLEE_ConsolidateTraces;
        private System.Windows.Forms.Button btAnimateTrace;
        private System.Windows.Forms.CheckBox cbVisibleControls_TracesGraphed;
        private System.Windows.Forms.CheckBox cbVisibleControls_GleeDrawingOptions;
        private System.Windows.Forms.CheckBox cbVisibleControls_GraphStats;
        private System.Windows.Forms.CheckBox cbVisibleControls_CustomFilters;
        private System.Windows.Forms.GroupBox gbVisibleControls;
        private System.Windows.Forms.SplitContainer scTopAndBottom;
        private System.Windows.Forms.Button btClearGraph;
        private System.Windows.Forms.Button btRefreshGraph;
        private System.Windows.Forms.CheckBox cbOnlyShowDataFor_Class;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbOnlyShowDataFor_ConsolidateNonVisibleNodes;
        private System.Windows.Forms.ComboBox cBoxShowFunctionClass_Depth;
        private System.Windows.Forms.TextBox tbOnlyShowDataFor_Class;
        private System.Windows.Forms.CheckBox cbDrawGleeGraph;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btRemoveSelectedNode;
        private System.Windows.Forms.ComboBox cbConsolidateDepth;
        private System.Windows.Forms.CheckBox cbOnlyShowDataFor_LostSources;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbOnFunctionSelect_HighlighAllCalls_From;
        private System.Windows.Forms.RadioButton rbOnFunctionSelect_Colapse;
        private System.Windows.Forms.RadioButton rbOnFunctionSelect_Expand;
        private System.Windows.Forms.RadioButton rbOnFunctionSelect_Remove;
        private System.Windows.Forms.RadioButton rbOnFunctionSelect_Clear;
        private System.Windows.Forms.RadioButton rbOnFunctionSelect_HighlightAllCalls_To;
        private System.Windows.Forms.RadioButton rbOnFunctionSelect_Show;
        private System.Windows.Forms.Button btRemoveNodes_NotSelected;
        private System.Windows.Forms.Button btCreateCustomSavedAssessmentRunFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbMaxToPlot;

    }
}