// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Kernel.CodeUtils;
using O2.Views.ASCX.CoreControls;

namespace O2.Core.CIR.Ascx.Rnd
{
    partial class ascx_CirAnalyzer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
// ReSharper disable RedundantDefaultFieldInitializer
        private System.ComponentModel.IContainer components = null;
// ReSharper restore RedundantDefaultFieldInitializer

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ascx_CirAnalyzer));
            this.lbFilesInSelectedDirectory = new System.Windows.Forms.ListBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btSelectBaseDirectory = new System.Windows.Forms.Button();
            this.cbBaseDirectory = new System.Windows.Forms.ComboBox();
            this.ilCirDump = new System.Windows.Forms.ImageList(this.components);
            this.cbAutoLoadOnSelection = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbTreeViewMaxNumberOfArrayItemsToFetch = new System.Windows.Forms.TextBox();
            this.cbTreeViewRecursiveLevel = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btCirAnalyzer_Refresh = new System.Windows.Forms.Button();
            this.tvAllClassesAndMethods = new System.Windows.Forms.TreeView();
            this.ilClassesAndMethods = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.splitContainer8 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.lbO2CirDataFilesInSelectedDir = new System.Windows.Forms.ListBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbOnSelectUpdateIsCalledByMappigns = new System.Windows.Forms.CheckBox();
            this.cbCirAnaLyzer_DontUpdateOnLoad = new System.Windows.Forms.CheckBox();
            this.btLoadAllFiles = new System.Windows.Forms.Button();
            this.btTestToSeeIfAllFilesCanBeLoaded = new System.Windows.Forms.Button();
            this.lbFileLoaded = new System.Windows.Forms.Label();
            this.btMaxView = new System.Windows.Forms.Button();
            this.scCirAnalyzer = new System.Windows.Forms.SplitContainer();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.tbCirAnalyszer_TextSearchFilter_MakesCallsTo = new System.Windows.Forms.TextBox();
            this.tbCirAnalyszer_TextSearchFilter_RemoveMakesCallsTo = new System.Windows.Forms.TextBox();
            this.cbCirAnalyzer_TextFilter_MakesCallsTo = new System.Windows.Forms.CheckBox();
            this.cbCirAnalyzer_TextFilter_RemoveMakesCallsTo = new System.Windows.Forms.CheckBox();
            this.cbCirAnalyzer_TextFilter_Classes = new System.Windows.Forms.CheckBox();
            this.tbCirAnalyszer_TextSearchFilter_Class = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbCirAnalyszer_TextSearchFilter_Function = new System.Windows.Forms.TextBox();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.cbShowArgsAndReturntype = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.rbShowFieldsAndVariables = new System.Windows.Forms.CheckBox();
            this.cbViewByFunction_FullSignature = new System.Windows.Forms.CheckBox();
            this.btViewByFunction_SelectAll = new System.Windows.Forms.Button();
            this.lbO2CirData_Functions = new System.Windows.Forms.ListBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbCirAnalyszer_TextSearchFilter_Parameter = new System.Windows.Forms.TextBox();
            this.btCirAnalyzer_LoadCirDataFile = new System.Windows.Forms.Button();
            this.cbCirAnalyzer_TextFilter_Functions = new System.Windows.Forms.CheckBox();
            this.tbCirAnalyzer_PathToSavedCirDataFile = new System.Windows.Forms.TextBox();
            this.cbCirAnalyzer_TextFilter_Parameters = new System.Windows.Forms.CheckBox();
            this.rb_ClearO2CirDataObjectOnLoad = new System.Windows.Forms.CheckBox();
            this.cbCirAnalyzer_TextFilter_SuperClass = new System.Windows.Forms.CheckBox();
            this.btCirAnalyzer_SaveCirData = new System.Windows.Forms.Button();
            this.tbCirAnalyszer_TextSearchFilter_SuperClass = new System.Windows.Forms.TextBox();
            this.splitContainer7 = new System.Windows.Forms.SplitContainer();
            this.lbCirAnalyzer_Files = new System.Windows.Forms.ListBox();
            this.ascx_DropObject1 = new ascx_DropObject();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.splitContainer8.Panel1.SuspendLayout();
            this.splitContainer8.Panel2.SuspendLayout();
            this.splitContainer8.SuspendLayout();
            this.scCirAnalyzer.Panel1.SuspendLayout();
            this.scCirAnalyzer.Panel2.SuspendLayout();
            this.scCirAnalyzer.SuspendLayout();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.splitContainer7.Panel1.SuspendLayout();
            this.splitContainer7.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbFilesInSelectedDirectory
            // 
            this.lbFilesInSelectedDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                            | System.Windows.Forms.AnchorStyles.Left)
                                                                                           | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFilesInSelectedDirectory.FormattingEnabled = true;
            this.lbFilesInSelectedDirectory.Location = new System.Drawing.Point(3, 24);
            this.lbFilesInSelectedDirectory.Name = "lbFilesInSelectedDirectory";
            this.lbFilesInSelectedDirectory.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbFilesInSelectedDirectory.Size = new System.Drawing.Size(197, 147);
            this.lbFilesInSelectedDirectory.Sorted = true;
            this.lbFilesInSelectedDirectory.TabIndex = 47;
            this.lbFilesInSelectedDirectory.SelectedIndexChanged += new System.EventHandler(this.lbFilesInSelectedDirectory_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 10);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(76, 13);
            this.label15.TabIndex = 51;
            this.label15.Text = "Base Directory";
            // 
            // btSelectBaseDirectory
            // 
            this.btSelectBaseDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSelectBaseDirectory.BackColor = System.Drawing.SystemColors.Control;
            this.btSelectBaseDirectory.Location = new System.Drawing.Point(186, 4);
            this.btSelectBaseDirectory.Name = "btSelectBaseDirectory";
            this.btSelectBaseDirectory.Size = new System.Drawing.Size(29, 23);
            this.btSelectBaseDirectory.TabIndex = 50;
            this.btSelectBaseDirectory.Text = "....";
            this.btSelectBaseDirectory.UseVisualStyleBackColor = false;
            this.btSelectBaseDirectory.Click += new System.EventHandler(this.btSelectBaseDirectory_Click);
            // 
            // cbBaseDirectory
            // 
            this.cbBaseDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                                | System.Windows.Forms.AnchorStyles.Right)));
            this.cbBaseDirectory.FormattingEnabled = true;
            this.cbBaseDirectory.Location = new System.Drawing.Point(85, 6);
            this.cbBaseDirectory.Name = "cbBaseDirectory";
            this.cbBaseDirectory.Size = new System.Drawing.Size(95, 21);
            this.cbBaseDirectory.TabIndex = 49;
            this.cbBaseDirectory.SelectedIndexChanged += new System.EventHandler(this.cbBaseDirectory_SelectedIndexChanged);
            this.cbBaseDirectory.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbBaseDirectory_KeyPress);
            // 
            // ilCirDump
            // 
            this.ilCirDump.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilCirDump.ImageStream")));
            this.ilCirDump.TransparentColor = System.Drawing.Color.Transparent;
            this.ilCirDump.Images.SetKeyName(0, "class.ico");
            this.ilCirDump.Images.SetKeyName(1, "folder.ico");
            this.ilCirDump.Images.SetKeyName(2, "string.ico");
            // 
            // cbAutoLoadOnSelection
            // 
            this.cbAutoLoadOnSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbAutoLoadOnSelection.AutoSize = true;
            this.cbAutoLoadOnSelection.Checked = true;
            this.cbAutoLoadOnSelection.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoLoadOnSelection.Location = new System.Drawing.Point(3, 176);
            this.cbAutoLoadOnSelection.Name = "cbAutoLoadOnSelection";
            this.cbAutoLoadOnSelection.Size = new System.Drawing.Size(165, 17);
            this.cbAutoLoadOnSelection.TabIndex = 53;
            this.cbAutoLoadOnSelection.Text = "Auto Process file on selection";
            this.cbAutoLoadOnSelection.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 557);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "Recursive level";
            // 
            // tbTreeViewMaxNumberOfArrayItemsToFetch
            // 
            this.tbTreeViewMaxNumberOfArrayItemsToFetch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbTreeViewMaxNumberOfArrayItemsToFetch.Location = new System.Drawing.Point(178, 578);
            this.tbTreeViewMaxNumberOfArrayItemsToFetch.Name = "tbTreeViewMaxNumberOfArrayItemsToFetch";
            this.tbTreeViewMaxNumberOfArrayItemsToFetch.Size = new System.Drawing.Size(52, 20);
            this.tbTreeViewMaxNumberOfArrayItemsToFetch.TabIndex = 55;
            this.tbTreeViewMaxNumberOfArrayItemsToFetch.Text = "300";
            // 
            // cbTreeViewRecursiveLevel
            // 
            this.cbTreeViewRecursiveLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbTreeViewRecursiveLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTreeViewRecursiveLevel.FormattingEnabled = true;
            this.cbTreeViewRecursiveLevel.Items.AddRange(new object[] {
                                                                          "1",
                                                                          "2"});
            this.cbTreeViewRecursiveLevel.Location = new System.Drawing.Point(89, 554);
            this.cbTreeViewRecursiveLevel.Name = "cbTreeViewRecursiveLevel";
            this.cbTreeViewRecursiveLevel.Size = new System.Drawing.Size(62, 21);
            this.cbTreeViewRecursiveLevel.TabIndex = 57;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 578);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(172, 13);
            this.label3.TabIndex = 58;
            this.label3.Text = "Max number of array items to fetch:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Files processed";
            // 
            // btCirAnalyzer_Refresh
            // 
            this.btCirAnalyzer_Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCirAnalyzer_Refresh.BackColor = System.Drawing.SystemColors.Control;
            this.btCirAnalyzer_Refresh.Location = new System.Drawing.Point(305, 2);
            this.btCirAnalyzer_Refresh.Name = "btCirAnalyzer_Refresh";
            this.btCirAnalyzer_Refresh.Size = new System.Drawing.Size(137, 24);
            this.btCirAnalyzer_Refresh.TabIndex = 2;
            this.btCirAnalyzer_Refresh.Text = "Refresh";
            this.btCirAnalyzer_Refresh.UseVisualStyleBackColor = false;
            // 
            // tvAllClassesAndMethods
            // 
            this.tvAllClassesAndMethods.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                        | System.Windows.Forms.AnchorStyles.Left)
                                                                                       | System.Windows.Forms.AnchorStyles.Right)));
            this.tvAllClassesAndMethods.HideSelection = false;
            this.tvAllClassesAndMethods.ImageIndex = 0;
            this.tvAllClassesAndMethods.ImageList = this.ilClassesAndMethods;
            this.tvAllClassesAndMethods.Location = new System.Drawing.Point(3, 25);
            this.tvAllClassesAndMethods.Name = "tvAllClassesAndMethods";
            this.tvAllClassesAndMethods.SelectedImageIndex = 0;
            this.tvAllClassesAndMethods.Size = new System.Drawing.Size(443, 387);
            this.tvAllClassesAndMethods.TabIndex = 53;
            this.tvAllClassesAndMethods.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvAllClassesAndMethods_AfterSelect);
            // 
            // ilClassesAndMethods
            // 
            this.ilClassesAndMethods.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilClassesAndMethods.ImageStream")));
            this.ilClassesAndMethods.TransparentColor = System.Drawing.Color.Transparent;
            this.ilClassesAndMethods.Images.SetKeyName(0, "class");
            this.ilClassesAndMethods.Images.SetKeyName(1, "method_member");
            this.ilClassesAndMethods.Images.SetKeyName(2, "method_static");
            this.ilClassesAndMethods.Images.SetKeyName(3, "smartTrace");
            this.ilClassesAndMethods.Images.SetKeyName(4, "string.ico");
            this.ilClassesAndMethods.Images.SetKeyName(5, "redBall.ico");
            this.ilClassesAndMethods.Images.SetKeyName(6, "engineWithTick.ico");
            this.ilClassesAndMethods.Images.SetKeyName(7, "sort_descending.ico");
            this.ilClassesAndMethods.Images.SetKeyName(8, "sort_ascending.ico");
            this.ilClassesAndMethods.Images.SetKeyName(9, "Edit_copy.ico");
            this.ilClassesAndMethods.Images.SetKeyName(10, "Findings_Medium.ico");
            this.ilClassesAndMethods.Images.SetKeyName(11, "Findings_High.ico");
            this.ilClassesAndMethods.Images.SetKeyName(12, "Findings_Info.ico");
            // 
            // splitContainer5
            // 
            this.splitContainer5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                 | System.Windows.Forms.AnchorStyles.Left)
                                                                                | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer5.Location = new System.Drawing.Point(0, 21);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.splitContainer8);
            this.splitContainer5.Panel1.Controls.Add(this.btLoadAllFiles);
            this.splitContainer5.Panel1.Controls.Add(this.btTestToSeeIfAllFilesCanBeLoaded);
            this.splitContainer5.Panel1.Controls.Add(this.tbTreeViewMaxNumberOfArrayItemsToFetch);
            this.splitContainer5.Panel1.Controls.Add(this.label3);
            this.splitContainer5.Panel1.Controls.Add(this.label1);
            this.splitContainer5.Panel1.Controls.Add(this.cbTreeViewRecursiveLevel);
            this.splitContainer5.Panel1.Controls.Add(this.btSelectBaseDirectory);
            this.splitContainer5.Panel1.Controls.Add(this.label15);
            this.splitContainer5.Panel1.Controls.Add(this.cbBaseDirectory);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.lbFileLoaded);
            this.splitContainer5.Panel2.Controls.Add(this.btMaxView);
            this.splitContainer5.Panel2.Controls.Add(this.scCirAnalyzer);
            this.splitContainer5.Panel2.Controls.Add(this.btCirAnalyzer_Refresh);
            this.splitContainer5.Size = new System.Drawing.Size(1038, 617);
            this.splitContainer5.SplitterDistance = 222;
            this.splitContainer5.TabIndex = 63;
            // 
            // splitContainer8
            // 
            this.splitContainer8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                 | System.Windows.Forms.AnchorStyles.Left)
                                                                                | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer8.Location = new System.Drawing.Point(3, 31);
            this.splitContainer8.Name = "splitContainer8";
            this.splitContainer8.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer8.Panel1
            // 
            this.splitContainer8.Panel1.Controls.Add(this.lbFilesInSelectedDirectory);
            this.splitContainer8.Panel1.Controls.Add(this.label2);
            this.splitContainer8.Panel1.Controls.Add(this.cbAutoLoadOnSelection);
            // 
            // splitContainer8.Panel2
            // 
            this.splitContainer8.Panel2.Controls.Add(this.lbO2CirDataFilesInSelectedDir);
            this.splitContainer8.Panel2.Controls.Add(this.label11);
            this.splitContainer8.Panel2.Controls.Add(this.cbOnSelectUpdateIsCalledByMappigns);
            this.splitContainer8.Panel2.Controls.Add(this.cbCirAnaLyzer_DontUpdateOnLoad);
            this.splitContainer8.Size = new System.Drawing.Size(207, 470);
            this.splitContainer8.SplitterDistance = 200;
            this.splitContainer8.TabIndex = 66;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 13);
            this.label2.TabIndex = 54;
            this.label2.Text = "Select Cir Dump file to load";
            // 
            // lbO2CirDataFilesInSelectedDir
            // 
            this.lbO2CirDataFilesInSelectedDir.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                               | System.Windows.Forms.AnchorStyles.Left)
                                                                                              | System.Windows.Forms.AnchorStyles.Right)));
            this.lbO2CirDataFilesInSelectedDir.FormattingEnabled = true;
            this.lbO2CirDataFilesInSelectedDir.Location = new System.Drawing.Point(3, 18);
            this.lbO2CirDataFilesInSelectedDir.Name = "lbO2CirDataFilesInSelectedDir";
            this.lbO2CirDataFilesInSelectedDir.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbO2CirDataFilesInSelectedDir.Size = new System.Drawing.Size(197, 186);
            this.lbO2CirDataFilesInSelectedDir.Sorted = true;
            this.lbO2CirDataFilesInSelectedDir.TabIndex = 60;
            this.lbO2CirDataFilesInSelectedDir.SelectedIndexChanged += new System.EventHandler(this.lbO2CirDataFilesInSelectedDir_SelectedIndexChanged);
            this.lbO2CirDataFilesInSelectedDir.DoubleClick += new System.EventHandler(this.lbO2CirDataFilesInSelectedDir_DoubleClick);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(0, 2);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(140, 13);
            this.label11.TabIndex = 61;
            this.label11.Text = "Select O2CirData file to load";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // cbOnSelectUpdateIsCalledByMappigns
            // 
            this.cbOnSelectUpdateIsCalledByMappigns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbOnSelectUpdateIsCalledByMappigns.AutoSize = true;
            this.cbOnSelectUpdateIsCalledByMappigns.Location = new System.Drawing.Point(3, 238);
            this.cbOnSelectUpdateIsCalledByMappigns.Name = "cbOnSelectUpdateIsCalledByMappigns";
            this.cbOnSelectUpdateIsCalledByMappigns.Size = new System.Drawing.Size(214, 17);
            this.cbOnSelectUpdateIsCalledByMappigns.TabIndex = 65;
            this.cbOnSelectUpdateIsCalledByMappigns.Text = "On Select  update isCalled By mappings";
            this.cbOnSelectUpdateIsCalledByMappigns.UseVisualStyleBackColor = true;
            // 
            // cbCirAnaLyzer_DontUpdateOnLoad
            // 
            this.cbCirAnaLyzer_DontUpdateOnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbCirAnaLyzer_DontUpdateOnLoad.AutoSize = true;
            this.cbCirAnaLyzer_DontUpdateOnLoad.Location = new System.Drawing.Point(3, 215);
            this.cbCirAnaLyzer_DontUpdateOnLoad.Name = "cbCirAnaLyzer_DontUpdateOnLoad";
            this.cbCirAnaLyzer_DontUpdateOnLoad.Size = new System.Drawing.Size(190, 17);
            this.cbCirAnaLyzer_DontUpdateOnLoad.TabIndex = 63;
            this.cbCirAnaLyzer_DontUpdateOnLoad.Text = "Don\'t update on load (only on filter)";
            this.cbCirAnaLyzer_DontUpdateOnLoad.UseVisualStyleBackColor = true;
            // 
            // btLoadAllFiles
            // 
            this.btLoadAllFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                               | System.Windows.Forms.AnchorStyles.Right)));
            this.btLoadAllFiles.BackColor = System.Drawing.SystemColors.Control;
            this.btLoadAllFiles.Location = new System.Drawing.Point(-1, 507);
            this.btLoadAllFiles.Name = "btLoadAllFiles";
            this.btLoadAllFiles.Size = new System.Drawing.Size(212, 24);
            this.btLoadAllFiles.TabIndex = 59;
            this.btLoadAllFiles.Text = "Load all files";
            this.btLoadAllFiles.UseVisualStyleBackColor = false;
            this.btLoadAllFiles.Click += new System.EventHandler(this.btLoadAllFiles_Click);
            // 
            // btTestToSeeIfAllFilesCanBeLoaded
            // 
            this.btTestToSeeIfAllFilesCanBeLoaded.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                                                 | System.Windows.Forms.AnchorStyles.Right)));
            this.btTestToSeeIfAllFilesCanBeLoaded.BackColor = System.Drawing.SystemColors.Control;
            this.btTestToSeeIfAllFilesCanBeLoaded.Location = new System.Drawing.Point(-2, 536);
            this.btTestToSeeIfAllFilesCanBeLoaded.Name = "btTestToSeeIfAllFilesCanBeLoaded";
            this.btTestToSeeIfAllFilesCanBeLoaded.Size = new System.Drawing.Size(213, 24);
            this.btTestToSeeIfAllFilesCanBeLoaded.TabIndex = 64;
            this.btTestToSeeIfAllFilesCanBeLoaded.Text = "Test To See If All Files Can Be Loaded";
            this.btTestToSeeIfAllFilesCanBeLoaded.UseVisualStyleBackColor = false;
            this.btTestToSeeIfAllFilesCanBeLoaded.Visible = false;
            this.btTestToSeeIfAllFilesCanBeLoaded.Click += new System.EventHandler(this.btTestToSeeIfAllFilesCanBeLoaded_Click);
            // 
            // lbFileLoaded
            // 
            this.lbFileLoaded.AutoSize = true;
            this.lbFileLoaded.Location = new System.Drawing.Point(172, 6);
            this.lbFileLoaded.Name = "lbFileLoaded";
            this.lbFileLoaded.Size = new System.Drawing.Size(16, 13);
            this.lbFileLoaded.TabIndex = 60;
            this.lbFileLoaded.Text = "...";
            // 
            // btMaxView
            // 
            this.btMaxView.BackColor = System.Drawing.SystemColors.Control;
            this.btMaxView.ForeColor = System.Drawing.Color.Red;
            this.btMaxView.Location = new System.Drawing.Point(3, 3);
            this.btMaxView.Name = "btMaxView";
            this.btMaxView.Size = new System.Drawing.Size(145, 23);
            this.btMaxView.TabIndex = 59;
            this.btMaxView.Text = "Maximize View Area";
            this.btMaxView.UseVisualStyleBackColor = false;
            this.btMaxView.Click += new System.EventHandler(this.btMaxView_Click);
            // 
            // scCirAnalyzer
            // 
            this.scCirAnalyzer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                               | System.Windows.Forms.AnchorStyles.Left)
                                                                              | System.Windows.Forms.AnchorStyles.Right)));
            this.scCirAnalyzer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scCirAnalyzer.Location = new System.Drawing.Point(3, 29);
            this.scCirAnalyzer.Name = "scCirAnalyzer";
            // 
            // scCirAnalyzer.Panel1
            // 
            this.scCirAnalyzer.Panel1.Controls.Add(this.splitContainer6);
            // 
            // scCirAnalyzer.Panel2
            // 
            this.scCirAnalyzer.Panel2.Controls.Add(this.splitContainer7);
            this.scCirAnalyzer.Size = new System.Drawing.Size(802, 586);
            this.scCirAnalyzer.SplitterDistance = 759;
            this.scCirAnalyzer.TabIndex = 58;
            // 
            // splitContainer6
            // 
            this.splitContainer6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer6.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer6.Location = new System.Drawing.Point(0, 0);
            this.splitContainer6.Name = "splitContainer6";
            this.splitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.tbCirAnalyszer_TextSearchFilter_MakesCallsTo);
            this.splitContainer6.Panel1.Controls.Add(this.tbCirAnalyszer_TextSearchFilter_RemoveMakesCallsTo);
            this.splitContainer6.Panel1.Controls.Add(this.cbCirAnalyzer_TextFilter_MakesCallsTo);
            this.splitContainer6.Panel1.Controls.Add(this.cbCirAnalyzer_TextFilter_RemoveMakesCallsTo);
            this.splitContainer6.Panel1.Controls.Add(this.cbCirAnalyzer_TextFilter_Classes);
            this.splitContainer6.Panel1.Controls.Add(this.tbCirAnalyszer_TextSearchFilter_Class);
            this.splitContainer6.Panel1.Controls.Add(this.label10);
            this.splitContainer6.Panel1.Controls.Add(this.tbCirAnalyszer_TextSearchFilter_Function);
            this.splitContainer6.Panel1.Controls.Add(this.splitContainer4);
            this.splitContainer6.Panel1.Controls.Add(this.tbCirAnalyszer_TextSearchFilter_Parameter);
            this.splitContainer6.Panel1.Controls.Add(this.btCirAnalyzer_LoadCirDataFile);
            this.splitContainer6.Panel1.Controls.Add(this.cbCirAnalyzer_TextFilter_Functions);
            this.splitContainer6.Panel1.Controls.Add(this.tbCirAnalyzer_PathToSavedCirDataFile);
            this.splitContainer6.Panel1.Controls.Add(this.cbCirAnalyzer_TextFilter_Parameters);
            this.splitContainer6.Panel1.Controls.Add(this.rb_ClearO2CirDataObjectOnLoad);
            this.splitContainer6.Panel1.Controls.Add(this.cbCirAnalyzer_TextFilter_SuperClass);
            this.splitContainer6.Panel1.Controls.Add(this.btCirAnalyzer_SaveCirData);
            this.splitContainer6.Panel1.Controls.Add(this.tbCirAnalyszer_TextSearchFilter_SuperClass);
            this.splitContainer6.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer6_Panel1_Paint);
            this.splitContainer6.Size = new System.Drawing.Size(759, 586);
            this.splitContainer6.SplitterDistance = 553;
            this.splitContainer6.TabIndex = 0;
            // 
            // tbCirAnalyszer_TextSearchFilter_MakesCallsTo
            // 
            this.tbCirAnalyszer_TextSearchFilter_MakesCallsTo.Location = new System.Drawing.Point(588, 23);
            this.tbCirAnalyszer_TextSearchFilter_MakesCallsTo.Name = "tbCirAnalyszer_TextSearchFilter_MakesCallsTo";
            this.tbCirAnalyszer_TextSearchFilter_MakesCallsTo.Size = new System.Drawing.Size(96, 20);
            this.tbCirAnalyszer_TextSearchFilter_MakesCallsTo.TabIndex = 68;
            this.tbCirAnalyszer_TextSearchFilter_MakesCallsTo.TextChanged += new System.EventHandler(this.tbCirAnalyszer_TextSearchFilter_IsCalledBy_TextChanged);
            this.tbCirAnalyszer_TextSearchFilter_MakesCallsTo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbCirAnalyszer_TextSearchFilter_KeyUp);
            // 
            // tbCirAnalyszer_TextSearchFilter_RemoveMakesCallsTo
            // 
            this.tbCirAnalyszer_TextSearchFilter_RemoveMakesCallsTo.Location = new System.Drawing.Point(588, 42);
            this.tbCirAnalyszer_TextSearchFilter_RemoveMakesCallsTo.Name = "tbCirAnalyszer_TextSearchFilter_RemoveMakesCallsTo";
            this.tbCirAnalyszer_TextSearchFilter_RemoveMakesCallsTo.Size = new System.Drawing.Size(96, 20);
            this.tbCirAnalyszer_TextSearchFilter_RemoveMakesCallsTo.TabIndex = 67;
            this.tbCirAnalyszer_TextSearchFilter_RemoveMakesCallsTo.TextChanged += new System.EventHandler(this.tbCirAnalyszer_TextSearchFilter_RemoveIsCalledBy_TextChanged);
            this.tbCirAnalyszer_TextSearchFilter_RemoveMakesCallsTo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbCirAnalyszer_TextSearchFilter_KeyUp);
            // 
            // cbCirAnalyzer_TextFilter_MakesCallsTo
            // 
            this.cbCirAnalyzer_TextFilter_MakesCallsTo.AutoSize = true;
            this.cbCirAnalyzer_TextFilter_MakesCallsTo.Location = new System.Drawing.Point(441, 23);
            this.cbCirAnalyzer_TextFilter_MakesCallsTo.Name = "cbCirAnalyzer_TextFilter_MakesCallsTo";
            this.cbCirAnalyzer_TextFilter_MakesCallsTo.Size = new System.Drawing.Size(133, 17);
            this.cbCirAnalyzer_TextFilter_MakesCallsTo.TabIndex = 66;
            this.cbCirAnalyzer_TextFilter_MakesCallsTo.Text = "Filter on MakesCallsTo";
            this.cbCirAnalyzer_TextFilter_MakesCallsTo.UseVisualStyleBackColor = true;
            // 
            // cbCirAnalyzer_TextFilter_RemoveMakesCallsTo
            // 
            this.cbCirAnalyzer_TextFilter_RemoveMakesCallsTo.AutoSize = true;
            this.cbCirAnalyzer_TextFilter_RemoveMakesCallsTo.Location = new System.Drawing.Point(441, 41);
            this.cbCirAnalyzer_TextFilter_RemoveMakesCallsTo.Name = "cbCirAnalyzer_TextFilter_RemoveMakesCallsTo";
            this.cbCirAnalyzer_TextFilter_RemoveMakesCallsTo.Size = new System.Drawing.Size(136, 17);
            this.cbCirAnalyzer_TextFilter_RemoveMakesCallsTo.TabIndex = 65;
            this.cbCirAnalyzer_TextFilter_RemoveMakesCallsTo.Text = "Remove MakesCallsTo";
            this.cbCirAnalyzer_TextFilter_RemoveMakesCallsTo.UseVisualStyleBackColor = true;
            // 
            // cbCirAnalyzer_TextFilter_Classes
            // 
            this.cbCirAnalyzer_TextFilter_Classes.AutoSize = true;
            this.cbCirAnalyzer_TextFilter_Classes.Location = new System.Drawing.Point(441, 6);
            this.cbCirAnalyzer_TextFilter_Classes.Name = "cbCirAnalyzer_TextFilter_Classes";
            this.cbCirAnalyzer_TextFilter_Classes.Size = new System.Drawing.Size(122, 17);
            this.cbCirAnalyzer_TextFilter_Classes.TabIndex = 64;
            this.cbCirAnalyzer_TextFilter_Classes.Text = "Filter on Class Name";
            this.cbCirAnalyzer_TextFilter_Classes.UseVisualStyleBackColor = true;
            // 
            // tbCirAnalyszer_TextSearchFilter_Class
            // 
            this.tbCirAnalyszer_TextSearchFilter_Class.Location = new System.Drawing.Point(588, 4);
            this.tbCirAnalyszer_TextSearchFilter_Class.Name = "tbCirAnalyszer_TextSearchFilter_Class";
            this.tbCirAnalyszer_TextSearchFilter_Class.Size = new System.Drawing.Size(96, 20);
            this.tbCirAnalyszer_TextSearchFilter_Class.TabIndex = 63;
            this.tbCirAnalyszer_TextSearchFilter_Class.TextChanged += new System.EventHandler(this.tbCirAnalyszer_TextSearchFilter_Class_TextChanged);
            this.tbCirAnalyszer_TextSearchFilter_Class.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbCirAnalyszer_TextSearchFilter_KeyUp);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(171, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Text Search Filter (Enter to search)";
            // 
            // tbCirAnalyszer_TextSearchFilter_Function
            // 
            this.tbCirAnalyszer_TextSearchFilter_Function.Location = new System.Drawing.Point(339, 21);
            this.tbCirAnalyszer_TextSearchFilter_Function.Name = "tbCirAnalyszer_TextSearchFilter_Function";
            this.tbCirAnalyszer_TextSearchFilter_Function.Size = new System.Drawing.Size(96, 20);
            this.tbCirAnalyszer_TextSearchFilter_Function.TabIndex = 7;
            this.tbCirAnalyszer_TextSearchFilter_Function.TextChanged += new System.EventHandler(this.tbCirAnalyszer_TextSearchFilter_Function_TextChanged);
            this.tbCirAnalyszer_TextSearchFilter_Function.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbCirAnalyszer_TextSearchFilter_KeyUp);
            // 
            // splitContainer4
            // 
            this.splitContainer4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                 | System.Windows.Forms.AnchorStyles.Left)
                                                                                | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer4.Location = new System.Drawing.Point(6, 65);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.cbShowArgsAndReturntype);
            this.splitContainer4.Panel1.Controls.Add(this.tvAllClassesAndMethods);
            this.splitContainer4.Panel1.Controls.Add(this.label9);
            this.splitContainer4.Panel1.Controls.Add(this.rbShowFieldsAndVariables);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.cbViewByFunction_FullSignature);
            this.splitContainer4.Panel2.Controls.Add(this.btViewByFunction_SelectAll);
            this.splitContainer4.Panel2.Controls.Add(this.lbO2CirData_Functions);
            this.splitContainer4.Panel2.Controls.Add(this.label12);
            this.splitContainer4.Size = new System.Drawing.Size(746, 445);
            this.splitContainer4.SplitterDistance = 451;
            this.splitContainer4.TabIndex = 62;
            // 
            // cbShowArgsAndReturntype
            // 
            this.cbShowArgsAndReturntype.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbShowArgsAndReturntype.AutoSize = true;
            this.cbShowArgsAndReturntype.Location = new System.Drawing.Point(173, 416);
            this.cbShowArgsAndReturntype.Name = "cbShowArgsAndReturntype";
            this.cbShowArgsAndReturntype.Size = new System.Drawing.Size(189, 17);
            this.cbShowArgsAndReturntype.TabIndex = 59;
            this.cbShowArgsAndReturntype.Text = "Show Arguments and Return Type";
            this.cbShowArgsAndReturntype.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 13);
            this.label9.TabIndex = 56;
            this.label9.Text = "View by Classses ";
            // 
            // rbShowFieldsAndVariables
            // 
            this.rbShowFieldsAndVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbShowFieldsAndVariables.AutoSize = true;
            this.rbShowFieldsAndVariables.Location = new System.Drawing.Point(6, 416);
            this.rbShowFieldsAndVariables.Name = "rbShowFieldsAndVariables";
            this.rbShowFieldsAndVariables.Size = new System.Drawing.Size(150, 17);
            this.rbShowFieldsAndVariables.TabIndex = 58;
            this.rbShowFieldsAndVariables.Text = "Show Fields and Variables";
            this.rbShowFieldsAndVariables.UseVisualStyleBackColor = true;
            // 
            // cbViewByFunction_FullSignature
            // 
            this.cbViewByFunction_FullSignature.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbViewByFunction_FullSignature.AutoSize = true;
            this.cbViewByFunction_FullSignature.Location = new System.Drawing.Point(6, 419);
            this.cbViewByFunction_FullSignature.Name = "cbViewByFunction_FullSignature";
            this.cbViewByFunction_FullSignature.Size = new System.Drawing.Size(120, 17);
            this.cbViewByFunction_FullSignature.TabIndex = 64;
            this.cbViewByFunction_FullSignature.Text = "Show Full Signature";
            this.cbViewByFunction_FullSignature.UseVisualStyleBackColor = true;
            this.cbViewByFunction_FullSignature.CheckedChanged += new System.EventHandler(this.cbViewByFunction_FullSignature_CheckedChanged);
            // 
            // btViewByFunction_SelectAll
            // 
            this.btViewByFunction_SelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btViewByFunction_SelectAll.BackColor = System.Drawing.SystemColors.Control;
            this.btViewByFunction_SelectAll.Location = new System.Drawing.Point(206, 414);
            this.btViewByFunction_SelectAll.Name = "btViewByFunction_SelectAll";
            this.btViewByFunction_SelectAll.Size = new System.Drawing.Size(78, 24);
            this.btViewByFunction_SelectAll.TabIndex = 63;
            this.btViewByFunction_SelectAll.Text = "Select All";
            this.btViewByFunction_SelectAll.UseVisualStyleBackColor = false;
            this.btViewByFunction_SelectAll.Click += new System.EventHandler(this.btViewByFunction_SelectAll_Click);
            // 
            // lbO2CirData_Functions
            // 
            this.lbO2CirData_Functions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                       | System.Windows.Forms.AnchorStyles.Left)
                                                                                      | System.Windows.Forms.AnchorStyles.Right)));
            this.lbO2CirData_Functions.FormattingEnabled = true;
            this.lbO2CirData_Functions.Location = new System.Drawing.Point(6, 25);
            this.lbO2CirData_Functions.Name = "lbO2CirData_Functions";
            this.lbO2CirData_Functions.ScrollAlwaysVisible = true;
            this.lbO2CirData_Functions.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbO2CirData_Functions.Size = new System.Drawing.Size(278, 368);
            this.lbO2CirData_Functions.Sorted = true;
            this.lbO2CirData_Functions.TabIndex = 58;
            this.lbO2CirData_Functions.SelectedIndexChanged += new System.EventHandler(this.lbO2CirData_Functions_SelectedIndexChanged);
            this.lbO2CirData_Functions.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lbO2CirData_Functions_KeyUp);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(88, 13);
            this.label12.TabIndex = 57;
            this.label12.Text = "View by Function";
            // 
            // tbCirAnalyszer_TextSearchFilter_Parameter
            // 
            this.tbCirAnalyszer_TextSearchFilter_Parameter.Location = new System.Drawing.Point(339, 41);
            this.tbCirAnalyszer_TextSearchFilter_Parameter.Name = "tbCirAnalyszer_TextSearchFilter_Parameter";
            this.tbCirAnalyszer_TextSearchFilter_Parameter.Size = new System.Drawing.Size(96, 20);
            this.tbCirAnalyszer_TextSearchFilter_Parameter.TabIndex = 6;
            this.tbCirAnalyszer_TextSearchFilter_Parameter.TextChanged += new System.EventHandler(this.tbCirAnalyszer_TextSearchFilter_Parameter_TextChanged);
            this.tbCirAnalyszer_TextSearchFilter_Parameter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbCirAnalyszer_TextSearchFilter_KeyUp);
            // 
            // btCirAnalyzer_LoadCirDataFile
            // 
            this.btCirAnalyzer_LoadCirDataFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCirAnalyzer_LoadCirDataFile.BackColor = System.Drawing.SystemColors.Control;
            this.btCirAnalyzer_LoadCirDataFile.Location = new System.Drawing.Point(615, 516);
            this.btCirAnalyzer_LoadCirDataFile.Name = "btCirAnalyzer_LoadCirDataFile";
            this.btCirAnalyzer_LoadCirDataFile.Size = new System.Drawing.Size(137, 24);
            this.btCirAnalyzer_LoadCirDataFile.TabIndex = 61;
            this.btCirAnalyzer_LoadCirDataFile.Text = "Load CirData Object";
            this.btCirAnalyzer_LoadCirDataFile.UseVisualStyleBackColor = false;
            this.btCirAnalyzer_LoadCirDataFile.Click += new System.EventHandler(this.btCirAnalyzer_LoadCirDataFile_Click);
            // 
            // cbCirAnalyzer_TextFilter_Functions
            // 
            this.cbCirAnalyzer_TextFilter_Functions.AutoSize = true;
            this.cbCirAnalyzer_TextFilter_Functions.Location = new System.Drawing.Point(193, 22);
            this.cbCirAnalyzer_TextFilter_Functions.Name = "cbCirAnalyzer_TextFilter_Functions";
            this.cbCirAnalyzer_TextFilter_Functions.Size = new System.Drawing.Size(138, 17);
            this.cbCirAnalyzer_TextFilter_Functions.TabIndex = 5;
            this.cbCirAnalyzer_TextFilter_Functions.Text = "Filter on Function Name";
            this.cbCirAnalyzer_TextFilter_Functions.UseVisualStyleBackColor = true;
            // 
            // tbCirAnalyzer_PathToSavedCirDataFile
            // 
            this.tbCirAnalyzer_PathToSavedCirDataFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                                                                                                     | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCirAnalyzer_PathToSavedCirDataFile.BackColor = System.Drawing.SystemColors.Window;
            this.tbCirAnalyzer_PathToSavedCirDataFile.ForeColor = System.Drawing.Color.Black;
            this.tbCirAnalyzer_PathToSavedCirDataFile.Location = new System.Drawing.Point(154, 519);
            this.tbCirAnalyzer_PathToSavedCirDataFile.Name = "tbCirAnalyzer_PathToSavedCirDataFile";
            this.tbCirAnalyzer_PathToSavedCirDataFile.Size = new System.Drawing.Size(445, 20);
            this.tbCirAnalyzer_PathToSavedCirDataFile.TabIndex = 60;
            // 
            // cbCirAnalyzer_TextFilter_Parameters
            // 
            this.cbCirAnalyzer_TextFilter_Parameters.AutoSize = true;
            this.cbCirAnalyzer_TextFilter_Parameters.Location = new System.Drawing.Point(193, 40);
            this.cbCirAnalyzer_TextFilter_Parameters.Name = "cbCirAnalyzer_TextFilter_Parameters";
            this.cbCirAnalyzer_TextFilter_Parameters.Size = new System.Drawing.Size(141, 17);
            this.cbCirAnalyzer_TextFilter_Parameters.TabIndex = 4;
            this.cbCirAnalyzer_TextFilter_Parameters.Text = "Filter on Parameter Type";
            this.cbCirAnalyzer_TextFilter_Parameters.UseVisualStyleBackColor = true;
            // 
            // rb_ClearO2CirDataObjectOnLoad
            // 
            this.rb_ClearO2CirDataObjectOnLoad.AutoSize = true;
            this.rb_ClearO2CirDataObjectOnLoad.Checked = true;
            this.rb_ClearO2CirDataObjectOnLoad.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rb_ClearO2CirDataObjectOnLoad.Location = new System.Drawing.Point(11, 44);
            this.rb_ClearO2CirDataObjectOnLoad.Name = "rb_ClearO2CirDataObjectOnLoad";
            this.rb_ClearO2CirDataObjectOnLoad.Size = new System.Drawing.Size(163, 17);
            this.rb_ClearO2CirDataObjectOnLoad.TabIndex = 57;
            this.rb_ClearO2CirDataObjectOnLoad.Text = "Clear O2CirData on File Load";
            this.rb_ClearO2CirDataObjectOnLoad.UseVisualStyleBackColor = true;
            // 
            // cbCirAnalyzer_TextFilter_SuperClass
            // 
            this.cbCirAnalyzer_TextFilter_SuperClass.AutoSize = true;
            this.cbCirAnalyzer_TextFilter_SuperClass.Location = new System.Drawing.Point(193, 5);
            this.cbCirAnalyzer_TextFilter_SuperClass.Name = "cbCirAnalyzer_TextFilter_SuperClass";
            this.cbCirAnalyzer_TextFilter_SuperClass.Size = new System.Drawing.Size(119, 17);
            this.cbCirAnalyzer_TextFilter_SuperClass.TabIndex = 3;
            this.cbCirAnalyzer_TextFilter_SuperClass.Text = "Filter on SuperClass";
            this.cbCirAnalyzer_TextFilter_SuperClass.UseVisualStyleBackColor = true;
            // 
            // btCirAnalyzer_SaveCirData
            // 
            this.btCirAnalyzer_SaveCirData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btCirAnalyzer_SaveCirData.BackColor = System.Drawing.SystemColors.Control;
            this.btCirAnalyzer_SaveCirData.Location = new System.Drawing.Point(6, 516);
            this.btCirAnalyzer_SaveCirData.Name = "btCirAnalyzer_SaveCirData";
            this.btCirAnalyzer_SaveCirData.Size = new System.Drawing.Size(137, 24);
            this.btCirAnalyzer_SaveCirData.TabIndex = 59;
            this.btCirAnalyzer_SaveCirData.Text = "Save CirData Object";
            this.btCirAnalyzer_SaveCirData.UseVisualStyleBackColor = false;
            this.btCirAnalyzer_SaveCirData.Click += new System.EventHandler(this.btCirAnalyzer_SaveCirData_Click);
            // 
            // tbCirAnalyszer_TextSearchFilter_SuperClass
            // 
            this.tbCirAnalyszer_TextSearchFilter_SuperClass.Enabled = false;
            this.tbCirAnalyszer_TextSearchFilter_SuperClass.Location = new System.Drawing.Point(339, 3);
            this.tbCirAnalyszer_TextSearchFilter_SuperClass.Name = "tbCirAnalyszer_TextSearchFilter_SuperClass";
            this.tbCirAnalyszer_TextSearchFilter_SuperClass.Size = new System.Drawing.Size(96, 20);
            this.tbCirAnalyszer_TextSearchFilter_SuperClass.TabIndex = 2;
            this.tbCirAnalyszer_TextSearchFilter_SuperClass.TextChanged += new System.EventHandler(this.tbCirAnalyszer_TextSearchFilter_SuperClass_TextChanged);
            this.tbCirAnalyszer_TextSearchFilter_SuperClass.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbCirAnalyszer_TextSearchFilter_KeyUp);
            // 
            // splitContainer7
            // 
            this.splitContainer7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer7.Location = new System.Drawing.Point(0, 0);
            this.splitContainer7.Name = "splitContainer7";
            this.splitContainer7.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer7.Panel1
            // 
            this.splitContainer7.Panel1.Controls.Add(this.label6);
            this.splitContainer7.Panel1.Controls.Add(this.lbCirAnalyzer_Files);
            this.splitContainer7.Size = new System.Drawing.Size(39, 586);
            this.splitContainer7.SplitterDistance = 553;
            this.splitContainer7.TabIndex = 0;
            // 
            // lbCirAnalyzer_Files
            // 
            this.lbCirAnalyzer_Files.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                     | System.Windows.Forms.AnchorStyles.Left)
                                                                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCirAnalyzer_Files.FormattingEnabled = true;
            this.lbCirAnalyzer_Files.Location = new System.Drawing.Point(0, 22);
            this.lbCirAnalyzer_Files.Name = "lbCirAnalyzer_Files";
            this.lbCirAnalyzer_Files.Size = new System.Drawing.Size(33, 251);
            this.lbCirAnalyzer_Files.TabIndex = 57;            
            // 
            // ascx_DropObject1
            // 
            this.ascx_DropObject1.AllowDrop = true;
            this.ascx_DropObject1.BackColor = System.Drawing.Color.Maroon;
            this.ascx_DropObject1.ForeColor = System.Drawing.Color.White;
            this.ascx_DropObject1.Location = new System.Drawing.Point(0, 0);
            this.ascx_DropObject1.Name = "ascx_DropObject1";
            this.ascx_DropObject1.Size = new System.Drawing.Size(136, 21);
            this.ascx_DropObject1.TabIndex = 64;
            this.ascx_DropObject1.eDnDAction_ObjectDataReceived_Event += new Callbacks.dMethod_Object(this.ascx_DropObject1_eDnDAction_ObjectDataReceived_Event);
            // 
            // ascx_CirAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.ascx_DropObject1);
            this.Controls.Add(this.splitContainer5);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ascx_CirAnalyzer";
            this.Size = new System.Drawing.Size(1041, 638);
            this.Load += new System.EventHandler(this.ascx_ClrAnalyzer_Load);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel1.PerformLayout();
            this.splitContainer5.Panel2.ResumeLayout(false);
            this.splitContainer5.Panel2.PerformLayout();
            this.splitContainer5.ResumeLayout(false);
            this.splitContainer8.Panel1.ResumeLayout(false);
            this.splitContainer8.Panel1.PerformLayout();
            this.splitContainer8.Panel2.ResumeLayout(false);
            this.splitContainer8.Panel2.PerformLayout();
            this.splitContainer8.ResumeLayout(false);
            this.scCirAnalyzer.Panel1.ResumeLayout(false);
            this.scCirAnalyzer.Panel2.ResumeLayout(false);
            this.scCirAnalyzer.ResumeLayout(false);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel1.PerformLayout();
            this.splitContainer6.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer7.Panel1.ResumeLayout(false);
            this.splitContainer7.Panel1.PerformLayout();
            this.splitContainer7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbFilesInSelectedDirectory;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btSelectBaseDirectory;
        private System.Windows.Forms.ComboBox cbBaseDirectory;
        private System.Windows.Forms.CheckBox cbAutoLoadOnSelection;
        private System.Windows.Forms.ImageList ilCirDump;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTreeViewMaxNumberOfArrayItemsToFetch;
        private System.Windows.Forms.ComboBox cbTreeViewRecursiveLevel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btCirAnalyzer_Refresh;
        private System.Windows.Forms.TreeView tvAllClassesAndMethods;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ImageList ilClassesAndMethods;
        private System.Windows.Forms.ListBox lbCirAnalyzer_Files;
        private System.Windows.Forms.SplitContainer scCirAnalyzer;
        private System.Windows.Forms.SplitContainer splitContainer6;
        private System.Windows.Forms.SplitContainer splitContainer7;
        private System.Windows.Forms.CheckBox rb_ClearO2CirDataObjectOnLoad;
        private System.Windows.Forms.CheckBox rbShowFieldsAndVariables;
        private System.Windows.Forms.Button btLoadAllFiles;
        private System.Windows.Forms.Button btCirAnalyzer_LoadCirDataFile;
        private System.Windows.Forms.TextBox tbCirAnalyzer_PathToSavedCirDataFile;
        private System.Windows.Forms.Button btCirAnalyzer_SaveCirData;
        private System.Windows.Forms.TextBox tbCirAnalyszer_TextSearchFilter_SuperClass;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListBox lbO2CirDataFilesInSelectedDir;
        private System.Windows.Forms.CheckBox cbCirAnalyzer_TextFilter_Functions;
        private System.Windows.Forms.CheckBox cbCirAnalyzer_TextFilter_Parameters;
        private System.Windows.Forms.CheckBox cbCirAnalyzer_TextFilter_SuperClass;
        private System.Windows.Forms.TextBox tbCirAnalyszer_TextSearchFilter_Function;
        private System.Windows.Forms.TextBox tbCirAnalyszer_TextSearchFilter_Parameter;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ListBox lbO2CirData_Functions;
        private System.Windows.Forms.Button btViewByFunction_SelectAll;
        private System.Windows.Forms.CheckBox cbViewByFunction_FullSignature;
        private System.Windows.Forms.CheckBox cbCirAnaLyzer_DontUpdateOnLoad;
        private ascx_DropObject ascx_DropObject1;
        private System.Windows.Forms.CheckBox cbOnSelectUpdateIsCalledByMappigns;
        private System.Windows.Forms.Button btTestToSeeIfAllFilesCanBeLoaded;
        private System.Windows.Forms.TextBox tbCirAnalyszer_TextSearchFilter_MakesCallsTo;
        private System.Windows.Forms.TextBox tbCirAnalyszer_TextSearchFilter_RemoveMakesCallsTo;
        private System.Windows.Forms.CheckBox cbCirAnalyzer_TextFilter_MakesCallsTo;
        private System.Windows.Forms.CheckBox cbCirAnalyzer_TextFilter_RemoveMakesCallsTo;
        private System.Windows.Forms.CheckBox cbCirAnalyzer_TextFilter_Classes;
        private System.Windows.Forms.TextBox tbCirAnalyszer_TextSearchFilter_Class;
        private System.Windows.Forms.CheckBox cbShowArgsAndReturntype;
        private System.Windows.Forms.Button btMaxView;
        private System.Windows.Forms.Label lbFileLoaded;
        private System.Windows.Forms.SplitContainer splitContainer8;
    }
}
