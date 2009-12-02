using O2.External.SharpDevelop.Ascx;

namespace O2.External.Evaluant.Ascx
{
    partial class ascx_OzasmtQuery
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
            this.components = new System.ComponentModel.Container();
            this.scQueryAndResults = new System.Windows.Forms.SplitContainer();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btRunNLinqQuery = new System.Windows.Forms.Button();
            this.tbNLinqQuery = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.sourceCodeEditor = new ascx_SourceCodeEditor();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.cbOnlySaveSelectedFindings = new System.Windows.Forms.CheckBox();
            this.llObjectWithAllFindings = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.llLoadWebGoat = new System.Windows.Forms.LinkLabel();
            this.llLoadHacmeBank = new System.Windows.Forms.LinkLabel();
            this.btSaveResults = new System.Windows.Forms.Button();
            this.lbDragAndDropHelpText = new System.Windows.Forms.Label();
            this.nLinqQueryResults = new System.Windows.Forms.DataGridView();
            this.lbNLinqQuery_NumberOfResults = new System.Windows.Forms.Label();
            this.lbNLinqQuery_ExecutionTime = new System.Windows.Forms.Label();
            this.lbNumberOfFindingsObjectsLoaded = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.scMainGuiAndTasksHost = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.llSaveAllQueries = new System.Windows.Forms.LinkLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbAllowDragOfFindingsLinks = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.llChangeViewMode = new System.Windows.Forms.LinkLabel();
            this.tbMaxRecordsToShow = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cbShowCompilationErrorDetails = new System.Windows.Forms.CheckBox();
            this.llHideTaskControlHost = new System.Windows.Forms.LinkLabel();
            this.taskHostControl = new System.Windows.Forms.FlowLayoutPanel();
            this.cbScriptLibrary = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.llClearLoadedFindingsList = new System.Windows.Forms.LinkLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.scQueryAndResults.Panel1.SuspendLayout();
            this.scQueryAndResults.Panel2.SuspendLayout();
            this.scQueryAndResults.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nLinqQueryResults)).BeginInit();
            this.scMainGuiAndTasksHost.Panel1.SuspendLayout();
            this.scMainGuiAndTasksHost.Panel2.SuspendLayout();
            this.scMainGuiAndTasksHost.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // scQueryAndResults
            // 
            this.scQueryAndResults.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scQueryAndResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scQueryAndResults.Location = new System.Drawing.Point(3, 3);
            this.scQueryAndResults.Name = "scQueryAndResults";
            this.scQueryAndResults.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scQueryAndResults.Panel1
            // 
            this.scQueryAndResults.Panel1.Controls.Add(this.tabControl2);
            // 
            // scQueryAndResults.Panel2
            // 
            this.scQueryAndResults.Panel2.Controls.Add(this.linkLabel1);
            this.scQueryAndResults.Panel2.Controls.Add(this.cbOnlySaveSelectedFindings);
            this.scQueryAndResults.Panel2.Controls.Add(this.llObjectWithAllFindings);
            this.scQueryAndResults.Panel2.Controls.Add(this.groupBox1);
            this.scQueryAndResults.Panel2.Controls.Add(this.btSaveResults);
            this.scQueryAndResults.Panel2.Controls.Add(this.lbDragAndDropHelpText);
            this.scQueryAndResults.Panel2.Controls.Add(this.nLinqQueryResults);
            this.scQueryAndResults.Panel2.Controls.Add(this.lbNLinqQuery_NumberOfResults);
            this.scQueryAndResults.Panel2.Controls.Add(this.lbNLinqQuery_ExecutionTime);
            this.scQueryAndResults.Size = new System.Drawing.Size(565, 410);
            this.scQueryAndResults.SplitterDistance = 231;
            this.scQueryAndResults.TabIndex = 0;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(561, 227);
            this.tabControl2.TabIndex = 38;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btRunNLinqQuery);
            this.tabPage3.Controls.Add(this.tbNLinqQuery);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(553, 201);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "LINQ query";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btRunNLinqQuery
            // 
            this.btRunNLinqQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btRunNLinqQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btRunNLinqQuery.Location = new System.Drawing.Point(415, 179);
            this.btRunNLinqQuery.Name = "btRunNLinqQuery";
            this.btRunNLinqQuery.Size = new System.Drawing.Size(122, 19);
            this.btRunNLinqQuery.TabIndex = 37;
            this.btRunNLinqQuery.Text = "run query (or press enter)";
            this.btRunNLinqQuery.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btRunNLinqQuery.UseVisualStyleBackColor = true;
            this.btRunNLinqQuery.Click += new System.EventHandler(this.btRunNLinqQuery_Click);
            // 
            // tbNLinqQuery
            // 
            this.tbNLinqQuery.AllowDrop = true;
            this.tbNLinqQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNLinqQuery.BackColor = System.Drawing.Color.White;
            this.tbNLinqQuery.Location = new System.Drawing.Point(0, 6);
            this.tbNLinqQuery.Multiline = true;
            this.tbNLinqQuery.Name = "tbNLinqQuery";
            this.tbNLinqQuery.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbNLinqQuery.Size = new System.Drawing.Size(553, 195);
            this.tbNLinqQuery.TabIndex = 33;
            this.tbNLinqQuery.TextChanged += new System.EventHandler(this.tbNLinqQuery_TextChanged);
            this.tbNLinqQuery.DragDrop += new System.Windows.Forms.DragEventHandler(this.tbNLinqQuery_DragDrop);
            this.tbNLinqQuery.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbNLinqQuery_KeyPress);
            this.tbNLinqQuery.DragEnter += new System.Windows.Forms.DragEventHandler(this.tbNLinqQuery_DragEnter);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.sourceCodeEditor);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(553, 201);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "CSharpScript query";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // sourceCodeEditor
            // 
            this.sourceCodeEditor.AllowDrop = true;
            this.sourceCodeEditor.BackColor = System.Drawing.SystemColors.Control;
            this.sourceCodeEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sourceCodeEditor.ForeColor = System.Drawing.Color.Black;
            this.sourceCodeEditor.Location = new System.Drawing.Point(3, 3);
            this.sourceCodeEditor.Name = "sourceCodeEditor";
            this.sourceCodeEditor.Size = new System.Drawing.Size(547, 195);
            this.sourceCodeEditor.TabIndex = 0;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(411, 4);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(91, 13);
            this.linkLabel1.TabIndex = 49;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Selected Findings";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // cbOnlySaveSelectedFindings
            // 
            this.cbOnlySaveSelectedFindings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbOnlySaveSelectedFindings.AutoSize = true;
            this.cbOnlySaveSelectedFindings.Location = new System.Drawing.Point(438, 151);
            this.cbOnlySaveSelectedFindings.Name = "cbOnlySaveSelectedFindings";
            this.cbOnlySaveSelectedFindings.Size = new System.Drawing.Size(120, 17);
            this.cbOnlySaveSelectedFindings.TabIndex = 48;
            this.cbOnlySaveSelectedFindings.Text = "Only Save Selected";
            this.cbOnlySaveSelectedFindings.UseVisualStyleBackColor = true;
            // 
            // llObjectWithAllFindings
            // 
            this.llObjectWithAllFindings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llObjectWithAllFindings.AutoSize = true;
            this.llObjectWithAllFindings.Location = new System.Drawing.Point(508, 4);
            this.llObjectWithAllFindings.Name = "llObjectWithAllFindings";
            this.llObjectWithAllFindings.Size = new System.Drawing.Size(60, 13);
            this.llObjectWithAllFindings.TabIndex = 47;
            this.llObjectWithAllFindings.TabStop = true;
            this.llObjectWithAllFindings.Text = "All Findings";
            this.llObjectWithAllFindings.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llObjectWithAllFindings_LinkClicked);
            this.llObjectWithAllFindings.MouseDown += new System.Windows.Forms.MouseEventHandler(this.llObjectWithAllFindings_MouseDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.llLoadWebGoat);
            this.groupBox1.Controls.Add(this.llLoadHacmeBank);
            this.groupBox1.Location = new System.Drawing.Point(6, 123);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(172, 45);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "download test assessments";
            // 
            // llLoadWebGoat
            // 
            this.llLoadWebGoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llLoadWebGoat.AutoSize = true;
            this.llLoadWebGoat.Location = new System.Drawing.Point(10, 19);
            this.llLoadWebGoat.Name = "llLoadWebGoat";
            this.llLoadWebGoat.Size = new System.Drawing.Size(53, 13);
            this.llLoadWebGoat.TabIndex = 37;
            this.llLoadWebGoat.TabStop = true;
            this.llLoadWebGoat.Text = "WebGoat";
            this.llLoadWebGoat.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llLoadWebGoat_LinkClicked);
            // 
            // llLoadHacmeBank
            // 
            this.llLoadHacmeBank.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llLoadHacmeBank.AutoSize = true;
            this.llLoadHacmeBank.Location = new System.Drawing.Point(69, 19);
            this.llLoadHacmeBank.Name = "llLoadHacmeBank";
            this.llLoadHacmeBank.Size = new System.Drawing.Size(66, 13);
            this.llLoadHacmeBank.TabIndex = 39;
            this.llLoadHacmeBank.TabStop = true;
            this.llLoadHacmeBank.Text = "HacmeBank";
            this.llLoadHacmeBank.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llLoadHacmeBank_LinkClicked);
            // 
            // btSaveResults
            // 
            this.btSaveResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSaveResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSaveResults.Location = new System.Drawing.Point(438, 123);
            this.btSaveResults.Name = "btSaveResults";
            this.btSaveResults.Size = new System.Drawing.Size(120, 24);
            this.btSaveResults.TabIndex = 38;
            this.btSaveResults.Text = "Save Results";
            this.btSaveResults.UseVisualStyleBackColor = true;
            this.btSaveResults.Click += new System.EventHandler(this.btSaveResults_Click);
            // 
            // lbDragAndDropHelpText
            // 
            this.lbDragAndDropHelpText.AllowDrop = true;
            this.lbDragAndDropHelpText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbDragAndDropHelpText.BackColor = System.Drawing.Color.Gray;
            this.lbDragAndDropHelpText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDragAndDropHelpText.ForeColor = System.Drawing.Color.LightBlue;
            this.lbDragAndDropHelpText.Location = new System.Drawing.Point(199, 41);
            this.lbDragAndDropHelpText.Name = "lbDragAndDropHelpText";
            this.lbDragAndDropHelpText.Size = new System.Drawing.Size(162, 63);
            this.lbDragAndDropHelpText.TabIndex = 38;
            this.lbDragAndDropHelpText.Text = "Please Drag and drop here the Ounce saved assessment files             (*.ozamst)" +
                " to load            (you can drop folders or zip files)";
            this.lbDragAndDropHelpText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbDragAndDropHelpText.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbDragAndDropHelpText_DragDrop);
            this.lbDragAndDropHelpText.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbDragAndDropHelpText_DragEnter);
            // 
            // nLinqQueryResults
            // 
            this.nLinqQueryResults.AllowDrop = true;
            this.nLinqQueryResults.AllowUserToAddRows = false;
            this.nLinqQueryResults.AllowUserToOrderColumns = true;
            this.nLinqQueryResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.nLinqQueryResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.nLinqQueryResults.Location = new System.Drawing.Point(4, 22);
            this.nLinqQueryResults.Name = "nLinqQueryResults";
            this.nLinqQueryResults.ReadOnly = true;
            this.nLinqQueryResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.nLinqQueryResults.Size = new System.Drawing.Size(554, 95);
            this.nLinqQueryResults.TabIndex = 36;
            this.nLinqQueryResults.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nLinqQueryResults_MouseDoubleClick);
            this.nLinqQueryResults.DragEnter += new System.Windows.Forms.DragEventHandler(this.nLinqQueryResults_DragEnter);
            this.nLinqQueryResults.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.nLinqQueryResults_RowsRemoved);
            this.nLinqQueryResults.SelectionChanged += new System.EventHandler(this.nLinqQueryResults_SelectionChanged);
            this.nLinqQueryResults.DragDrop += new System.Windows.Forms.DragEventHandler(this.nLinqQueryResults_DragDrop);
            // 
            // lbNLinqQuery_NumberOfResults
            // 
            this.lbNLinqQuery_NumberOfResults.AutoSize = true;
            this.lbNLinqQuery_NumberOfResults.Location = new System.Drawing.Point(89, 4);
            this.lbNLinqQuery_NumberOfResults.Name = "lbNLinqQuery_NumberOfResults";
            this.lbNLinqQuery_NumberOfResults.Size = new System.Drawing.Size(16, 13);
            this.lbNLinqQuery_NumberOfResults.TabIndex = 29;
            this.lbNLinqQuery_NumberOfResults.Text = "...";
            // 
            // lbNLinqQuery_ExecutionTime
            // 
            this.lbNLinqQuery_ExecutionTime.AutoSize = true;
            this.lbNLinqQuery_ExecutionTime.Location = new System.Drawing.Point(3, 4);
            this.lbNLinqQuery_ExecutionTime.Name = "lbNLinqQuery_ExecutionTime";
            this.lbNLinqQuery_ExecutionTime.Size = new System.Drawing.Size(16, 13);
            this.lbNLinqQuery_ExecutionTime.TabIndex = 27;
            this.lbNLinqQuery_ExecutionTime.Text = "...";
            // 
            // lbNumberOfFindingsObjectsLoaded
            // 
            this.lbNumberOfFindingsObjectsLoaded.AutoSize = true;
            this.lbNumberOfFindingsObjectsLoaded.Location = new System.Drawing.Point(82, 26);
            this.lbNumberOfFindingsObjectsLoaded.Name = "lbNumberOfFindingsObjectsLoaded";
            this.lbNumberOfFindingsObjectsLoaded.Size = new System.Drawing.Size(16, 13);
            this.lbNumberOfFindingsObjectsLoaded.TabIndex = 30;
            this.lbNumberOfFindingsObjectsLoaded.Text = "...";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(3, 26);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(84, 13);
            this.label16.TabIndex = 29;
            this.label16.Text = "findings loaded: ";
            // 
            // scMainGuiAndTasksHost
            // 
            this.scMainGuiAndTasksHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.scMainGuiAndTasksHost.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scMainGuiAndTasksHost.Location = new System.Drawing.Point(3, 42);
            this.scMainGuiAndTasksHost.Name = "scMainGuiAndTasksHost";
            this.scMainGuiAndTasksHost.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMainGuiAndTasksHost.Panel1
            // 
            this.scMainGuiAndTasksHost.Panel1.Controls.Add(this.tabControl1);
            // 
            // scMainGuiAndTasksHost.Panel2
            // 
            this.scMainGuiAndTasksHost.Panel2.Controls.Add(this.llHideTaskControlHost);
            this.scMainGuiAndTasksHost.Panel2.Controls.Add(this.taskHostControl);
            this.scMainGuiAndTasksHost.Size = new System.Drawing.Size(579, 513);
            this.scMainGuiAndTasksHost.SplitterDistance = 442;
            this.scMainGuiAndTasksHost.TabIndex = 31;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(579, 442);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.scQueryAndResults);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(571, 416);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Work Area";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(571, 416);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Config";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.llSaveAllQueries);
            this.groupBox3.Location = new System.Drawing.Point(7, 159);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(555, 61);
            this.groupBox3.TabIndex = 48;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Debug / Development options";
            // 
            // llSaveAllQueries
            // 
            this.llSaveAllQueries.AutoSize = true;
            this.llSaveAllQueries.Location = new System.Drawing.Point(6, 29);
            this.llSaveAllQueries.Name = "llSaveAllQueries";
            this.llSaveAllQueries.Size = new System.Drawing.Size(150, 13);
            this.llSaveAllQueries.TabIndex = 46;
            this.llSaveAllQueries.TabStop = true;
            this.llSaveAllQueries.Text = "Save all Queries into Temp Dir";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cbAllowDragOfFindingsLinks);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.llChangeViewMode);
            this.groupBox2.Controls.Add(this.tbMaxRecordsToShow);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.cbShowCompilationErrorDetails);
            this.groupBox2.Location = new System.Drawing.Point(6, 21);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(556, 132);
            this.groupBox2.TabIndex = 47;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Oazmt Query Settings";
            // 
            // cbAllowDragOfFindingsLinks
            // 
            this.cbAllowDragOfFindingsLinks.AutoSize = true;
            this.cbAllowDragOfFindingsLinks.Location = new System.Drawing.Point(8, 106);
            this.cbAllowDragOfFindingsLinks.Name = "cbAllowDragOfFindingsLinks";
            this.cbAllowDragOfFindingsLinks.Size = new System.Drawing.Size(155, 17);
            this.cbAllowDragOfFindingsLinks.TabIndex = 47;
            this.cbAllowDragOfFindingsLinks.Text = "Allow Drag of Findings links";
            this.cbAllowDragOfFindingsLinks.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 13);
            this.label2.TabIndex = 46;
            this.label2.Text = "Show or Hide the Linq Query:";
            // 
            // llChangeViewMode
            // 
            this.llChangeViewMode.AutoSize = true;
            this.llChangeViewMode.Location = new System.Drawing.Point(158, 84);
            this.llChangeViewMode.Name = "llChangeViewMode";
            this.llChangeViewMode.Size = new System.Drawing.Size(65, 13);
            this.llChangeViewMode.TabIndex = 45;
            this.llChangeViewMode.TabStop = true;
            this.llChangeViewMode.Text = "Show Query";
            // 
            // tbMaxRecordsToShow
            // 
            this.tbMaxRecordsToShow.Location = new System.Drawing.Point(149, 47);
            this.tbMaxRecordsToShow.Name = "tbMaxRecordsToShow";
            this.tbMaxRecordsToShow.Size = new System.Drawing.Size(44, 20);
            this.tbMaxRecordsToShow.TabIndex = 44;
            this.tbMaxRecordsToShow.Text = "1000";
            this.tbMaxRecordsToShow.TextChanged += new System.EventHandler(this.tbMaxRecordsToShow_TextChanged);
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(6, 44);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(137, 31);
            this.label14.TabIndex = 42;
            this.label14.Text = "Maximum number of results to show in DataGridView";
            // 
            // cbShowCompilationErrorDetails
            // 
            this.cbShowCompilationErrorDetails.AutoSize = true;
            this.cbShowCompilationErrorDetails.Checked = true;
            this.cbShowCompilationErrorDetails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowCompilationErrorDetails.Location = new System.Drawing.Point(9, 19);
            this.cbShowCompilationErrorDetails.Name = "cbShowCompilationErrorDetails";
            this.cbShowCompilationErrorDetails.Size = new System.Drawing.Size(263, 17);
            this.cbShowCompilationErrorDetails.TabIndex = 43;
            this.cbShowCompilationErrorDetails.Text = "When writing custom queries, on type, show errors";
            this.cbShowCompilationErrorDetails.UseVisualStyleBackColor = true;
            // 
            // llHideTaskControlHost
            // 
            this.llHideTaskControlHost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llHideTaskControlHost.AutoSize = true;
            this.llHideTaskControlHost.Location = new System.Drawing.Point(543, 4);
            this.llHideTaskControlHost.Name = "llHideTaskControlHost";
            this.llHideTaskControlHost.Size = new System.Drawing.Size(29, 13);
            this.llHideTaskControlHost.TabIndex = 33;
            this.llHideTaskControlHost.TabStop = true;
            this.llHideTaskControlHost.Text = "Hide";
            this.llHideTaskControlHost.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llHideTaskControlHost_LinkClicked);
            // 
            // taskHostControl
            // 
            this.taskHostControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.taskHostControl.Location = new System.Drawing.Point(2, 3);
            this.taskHostControl.Name = "taskHostControl";
            this.taskHostControl.Size = new System.Drawing.Size(573, 62);
            this.taskHostControl.TabIndex = 0;
            // 
            // cbScriptLibrary
            // 
            this.cbScriptLibrary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbScriptLibrary.DropDownHeight = 206;
            this.cbScriptLibrary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScriptLibrary.FormattingEnabled = true;
            this.cbScriptLibrary.IntegralHeight = false;
            this.cbScriptLibrary.Location = new System.Drawing.Point(85, 2);
            this.cbScriptLibrary.Name = "cbScriptLibrary";
            this.cbScriptLibrary.Size = new System.Drawing.Size(493, 21);
            this.cbScriptLibrary.TabIndex = 39;
            this.cbScriptLibrary.SelectedIndexChanged += new System.EventHandler(this.cbScriptLibrary_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "current filter:";
            // 
            // llClearLoadedFindingsList
            // 
            this.llClearLoadedFindingsList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llClearLoadedFindingsList.AutoSize = true;
            this.llClearLoadedFindingsList.Location = new System.Drawing.Point(528, 26);
            this.llClearLoadedFindingsList.Name = "llClearLoadedFindingsList";
            this.llClearLoadedFindingsList.Size = new System.Drawing.Size(50, 13);
            this.llClearLoadedFindingsList.TabIndex = 46;
            this.llClearLoadedFindingsList.TabStop = true;
            this.llClearLoadedFindingsList.Text = "Clear List";
            this.llClearLoadedFindingsList.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llClearLoadedFindingsList_LinkClicked_1);
            // 
            // ascx_OzasmtQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.llClearLoadedFindingsList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbScriptLibrary);
            this.Controls.Add(this.scMainGuiAndTasksHost);
            this.Controls.Add(this.lbNumberOfFindingsObjectsLoaded);
            this.Controls.Add(this.label16);
            this.Name = "ascx_OzasmtQuery";
            this.Size = new System.Drawing.Size(585, 558);
            this.Load += new System.EventHandler(this.ascx_OzasmtQuery_Load);
            this.scQueryAndResults.Panel1.ResumeLayout(false);
            this.scQueryAndResults.Panel2.ResumeLayout(false);
            this.scQueryAndResults.Panel2.PerformLayout();
            this.scQueryAndResults.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nLinqQueryResults)).EndInit();
            this.scMainGuiAndTasksHost.Panel1.ResumeLayout(false);
            this.scMainGuiAndTasksHost.Panel2.ResumeLayout(false);
            this.scMainGuiAndTasksHost.Panel2.PerformLayout();
            this.scMainGuiAndTasksHost.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer scQueryAndResults;
        private System.Windows.Forms.Label lbNumberOfFindingsObjectsLoaded;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lbNLinqQuery_NumberOfResults;
        private System.Windows.Forms.Label lbNLinqQuery_ExecutionTime;
        private System.Windows.Forms.DataGridView nLinqQueryResults;
        private System.Windows.Forms.SplitContainer scMainGuiAndTasksHost;
        private System.Windows.Forms.FlowLayoutPanel taskHostControl;
        private System.Windows.Forms.LinkLabel llHideTaskControlHost;
        private System.Windows.Forms.LinkLabel llLoadWebGoat;
        private System.Windows.Forms.Label lbDragAndDropHelpText;
        private System.Windows.Forms.ComboBox cbScriptLibrary;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel llLoadHacmeBank;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.LinkLabel llSaveAllQueries;
        private System.Windows.Forms.CheckBox cbShowCompilationErrorDetails;
        private System.Windows.Forms.TextBox tbMaxRecordsToShow;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btSaveResults;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel llChangeViewMode;
        private System.Windows.Forms.LinkLabel llClearLoadedFindingsList;
        private System.Windows.Forms.CheckBox cbOnlySaveSelectedFindings;
        private System.Windows.Forms.LinkLabel llObjectWithAllFindings;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox cbAllowDragOfFindingsLinks;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btRunNLinqQuery;
        private System.Windows.Forms.TextBox tbNLinqQuery;
        private System.Windows.Forms.TabPage tabPage4;
        private ascx_SourceCodeEditor sourceCodeEditor;
    }
}