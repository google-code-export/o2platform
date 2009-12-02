// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
namespace O2.External.SharpDevelop.Ascx
{
    partial class ascx_SourceCodeEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ascx_SourceCodeEditor));
            this.tecSourceCode = new ICSharpCode.TextEditor.TextEditorControl();
            this.cboxVRuler = new System.Windows.Forms.CheckBox();
            this.cboxInvalidLines = new System.Windows.Forms.CheckBox();
            this.cboxHRuler = new System.Windows.Forms.CheckBox();
            this.cboxEOLMarkers = new System.Windows.Forms.CheckBox();
            this.cboxSpaces = new System.Windows.Forms.CheckBox();
            this.cboxTabs = new System.Windows.Forms.CheckBox();
            this.cboxLineNumbers = new System.Windows.Forms.CheckBox();
            this.lbSource_CodeFileSaved = new System.Windows.Forms.Label();
            this.lbSourceCode_UnsavedChanges = new System.Windows.Forms.Label();
            this.btSave = new System.Windows.Forms.Button();
            this.ilSourceCodeEdit = new System.Windows.Forms.ImageList(this.components);
            this.tbSourceCode_FileLoaded = new System.Windows.Forms.TextBox();
            this.tbTextSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbSearch_textNotFound = new System.Windows.Forms.Label();
            this.tbSourceCode_DirectoryOfFileLoaded = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.btSettings = new System.Windows.Forms.ToolStripButton();
            this.btSaveFile = new System.Windows.Forms.ToolStripButton();
            this.tbShowO2ObjectModel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.lbExecuteOnEngine = new System.Windows.Forms.ToolStripLabel();
            this.btCompileCode = new System.Windows.Forms.ToolStripButton();
            this.cbExternalEngineToUse = new System.Windows.Forms.ToolStripComboBox();
            this.btExecuteOnExternalEngine = new System.Windows.Forms.ToolStripButton();
            this.btShowHidePythonLogExecutionOutputData = new System.Windows.Forms.ToolStripButton();
            this.lbCompileCode = new System.Windows.Forms.ToolStripLabel();
            this.tsCompileStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.lbExecuteCode = new System.Windows.Forms.ToolStripLabel();
            this.cboxCompliledSourceCodeMethods = new System.Windows.Forms.ToolStripComboBox();
            this.lbCompilationErrors = new System.Windows.Forms.ToolStripLabel();
            this.btShowHideCompilationErrors = new System.Windows.Forms.ToolStripButton();
            this.btExecuteSelectedMethod = new System.Windows.Forms.ToolStripButton();
            this.btDragAssemblyCreated = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btSelectedLineHistory = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btShowLogs = new System.Windows.Forms.ToolStripButton();
            this.lbSampleScripts = new System.Windows.Forms.ToolStripLabel();
            this.cBoxSampleScripts = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBoxWithFileAndSaveSettings = new System.Windows.Forms.GroupBox();
            this.cbAutoTryToFixSourceCodeFileReferences = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.llReload = new System.Windows.Forms.LinkLabel();
            this.llCurrenlyLoadedObjectModel = new System.Windows.Forms.LinkLabel();
            this.tbMaxLoadSize = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lbPartialFileView = new System.Windows.Forms.ListBox();
            this.tbExecutionHistoryOrLog = new System.Windows.Forms.TextBox();
            this.lboxCompilationErrors = new System.Windows.Forms.ListBox();
            this.gbO2ObjectMode = new System.Windows.Forms.GroupBox();
            this.scrollBarHorizontalSize_O2ObjectModel = new System.Windows.Forms.HScrollBar();
            this.scrollBarVerticalSize_O2ObjectModel = new System.Windows.Forms.VScrollBar();
            this.o2ObjectModel = new O2.Views.ASCX.CoreControls.ascx_O2ObjectModel();
            this.toolStrip1.SuspendLayout();
            this.groupBoxWithFileAndSaveSettings.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbO2ObjectMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // tecSourceCode
            // 
            this.tecSourceCode.AllowDrop = true;
            this.tecSourceCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tecSourceCode.IsIconBarVisible = true;
            this.tecSourceCode.IsReadOnly = false;
            this.tecSourceCode.Location = new System.Drawing.Point(3, 28);
            this.tecSourceCode.Name = "tecSourceCode";
            this.tecSourceCode.ShowEOLMarkers = true;
            this.tecSourceCode.ShowSpaces = true;
            this.tecSourceCode.ShowTabs = true;
            this.tecSourceCode.Size = new System.Drawing.Size(1022, 359);
            this.tecSourceCode.TabIndex = 17;
            this.tecSourceCode.Load += new System.EventHandler(this.tecSourceCode_Load);
            this.tecSourceCode.DragDrop += new System.Windows.Forms.DragEventHandler(this.tecSourceCode_DragDrop);
            this.tecSourceCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tecSourceCode_KeyPress);
            this.tecSourceCode.DragEnter += new System.Windows.Forms.DragEventHandler(this.tecSourceCode_DragEnter);
            // 
            // cboxVRuler
            // 
            this.cboxVRuler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboxVRuler.AutoSize = true;
            this.cboxVRuler.Location = new System.Drawing.Point(258, 37);
            this.cboxVRuler.Name = "cboxVRuler";
            this.cboxVRuler.Size = new System.Drawing.Size(58, 17);
            this.cboxVRuler.TabIndex = 32;
            this.cboxVRuler.Text = "VRuler";
            this.cboxVRuler.UseVisualStyleBackColor = true;
            this.cboxVRuler.CheckedChanged += new System.EventHandler(this.cboxVRuler_CheckedChanged);
            // 
            // cboxInvalidLines
            // 
            this.cboxInvalidLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboxInvalidLines.AutoSize = true;
            this.cboxInvalidLines.Location = new System.Drawing.Point(14, 37);
            this.cboxInvalidLines.Name = "cboxInvalidLines";
            this.cboxInvalidLines.Size = new System.Drawing.Size(85, 17);
            this.cboxInvalidLines.TabIndex = 31;
            this.cboxInvalidLines.Text = "Invalid Lines";
            this.cboxInvalidLines.UseVisualStyleBackColor = true;
            this.cboxInvalidLines.CheckedChanged += new System.EventHandler(this.cboxInvalidLines_CheckedChanged);
            // 
            // cboxHRuler
            // 
            this.cboxHRuler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboxHRuler.AutoSize = true;
            this.cboxHRuler.Location = new System.Drawing.Point(193, 37);
            this.cboxHRuler.Name = "cboxHRuler";
            this.cboxHRuler.Size = new System.Drawing.Size(59, 17);
            this.cboxHRuler.TabIndex = 30;
            this.cboxHRuler.Text = "HRuler";
            this.cboxHRuler.UseVisualStyleBackColor = true;
            this.cboxHRuler.CheckedChanged += new System.EventHandler(this.cboxHRuler_CheckedChanged);
            // 
            // cboxEOLMarkers
            // 
            this.cboxEOLMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboxEOLMarkers.AutoSize = true;
            this.cboxEOLMarkers.Location = new System.Drawing.Point(105, 37);
            this.cboxEOLMarkers.Name = "cboxEOLMarkers";
            this.cboxEOLMarkers.Size = new System.Drawing.Size(88, 17);
            this.cboxEOLMarkers.TabIndex = 29;
            this.cboxEOLMarkers.Text = "EOL Markers";
            this.cboxEOLMarkers.UseVisualStyleBackColor = true;
            this.cboxEOLMarkers.CheckedChanged += new System.EventHandler(this.cboxEOLMarkers_CheckedChanged);
            // 
            // cboxSpaces
            // 
            this.cboxSpaces.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboxSpaces.AutoSize = true;
            this.cboxSpaces.Location = new System.Drawing.Point(193, 21);
            this.cboxSpaces.Name = "cboxSpaces";
            this.cboxSpaces.Size = new System.Drawing.Size(62, 17);
            this.cboxSpaces.TabIndex = 28;
            this.cboxSpaces.Text = "Spaces";
            this.cboxSpaces.UseVisualStyleBackColor = true;
            this.cboxSpaces.CheckedChanged += new System.EventHandler(this.cboxSpaces_CheckedChanged);
            // 
            // cboxTabs
            // 
            this.cboxTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboxTabs.AutoSize = true;
            this.cboxTabs.Location = new System.Drawing.Point(105, 21);
            this.cboxTabs.Name = "cboxTabs";
            this.cboxTabs.Size = new System.Drawing.Size(50, 17);
            this.cboxTabs.TabIndex = 27;
            this.cboxTabs.Text = "Tabs";
            this.cboxTabs.UseVisualStyleBackColor = true;
            this.cboxTabs.CheckedChanged += new System.EventHandler(this.cboxTabs_CheckedChanged);
            // 
            // cboxLineNumbers
            // 
            this.cboxLineNumbers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboxLineNumbers.AutoSize = true;
            this.cboxLineNumbers.Location = new System.Drawing.Point(14, 21);
            this.cboxLineNumbers.Name = "cboxLineNumbers";
            this.cboxLineNumbers.Size = new System.Drawing.Size(91, 17);
            this.cboxLineNumbers.TabIndex = 26;
            this.cboxLineNumbers.Text = "Line Numbers";
            this.cboxLineNumbers.UseVisualStyleBackColor = true;
            this.cboxLineNumbers.CheckedChanged += new System.EventHandler(this.cboxLineNumbers_CheckedChanged);
            // 
            // lbSource_CodeFileSaved
            // 
            this.lbSource_CodeFileSaved.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSource_CodeFileSaved.AutoSize = true;
            this.lbSource_CodeFileSaved.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSource_CodeFileSaved.ForeColor = System.Drawing.Color.Green;
            this.lbSource_CodeFileSaved.Location = new System.Drawing.Point(619, 45);
            this.lbSource_CodeFileSaved.Name = "lbSource_CodeFileSaved";
            this.lbSource_CodeFileSaved.Size = new System.Drawing.Size(74, 15);
            this.lbSource_CodeFileSaved.TabIndex = 35;
            this.lbSource_CodeFileSaved.Text = "File Saved";
            this.lbSource_CodeFileSaved.Visible = false;
            // 
            // lbSourceCode_UnsavedChanges
            // 
            this.lbSourceCode_UnsavedChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSourceCode_UnsavedChanges.AutoSize = true;
            this.lbSourceCode_UnsavedChanges.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSourceCode_UnsavedChanges.ForeColor = System.Drawing.Color.Red;
            this.lbSourceCode_UnsavedChanges.Location = new System.Drawing.Point(609, 45);
            this.lbSourceCode_UnsavedChanges.Name = "lbSourceCode_UnsavedChanges";
            this.lbSourceCode_UnsavedChanges.Size = new System.Drawing.Size(96, 15);
            this.lbSourceCode_UnsavedChanges.TabIndex = 34;
            this.lbSourceCode_UnsavedChanges.Text = "Unsaved Data";
            this.lbSourceCode_UnsavedChanges.Visible = false;
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.BackColor = System.Drawing.SystemColors.Control;
            this.btSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btSave.ImageKey = "Project_save.ico";
            this.btSave.ImageList = this.ilSourceCodeEdit;
            this.btSave.Location = new System.Drawing.Point(598, 19);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(123, 23);
            this.btSave.TabIndex = 36;
            this.btSave.Text = "Save Source Code";
            this.btSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btSave.UseVisualStyleBackColor = false;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // ilSourceCodeEdit
            // 
            this.ilSourceCodeEdit.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilSourceCodeEdit.ImageStream")));
            this.ilSourceCodeEdit.TransparentColor = System.Drawing.Color.Transparent;
            this.ilSourceCodeEdit.Images.SetKeyName(0, "Project_save.ico");
            // 
            // tbSourceCode_FileLoaded
            // 
            this.tbSourceCode_FileLoaded.AllowDrop = true;
            this.tbSourceCode_FileLoaded.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSourceCode_FileLoaded.BackColor = System.Drawing.Color.White;
            this.tbSourceCode_FileLoaded.ForeColor = System.Drawing.Color.Black;
            this.tbSourceCode_FileLoaded.Location = new System.Drawing.Point(463, 20);
            this.tbSourceCode_FileLoaded.Name = "tbSourceCode_FileLoaded";
            this.tbSourceCode_FileLoaded.Size = new System.Drawing.Size(126, 20);
            this.tbSourceCode_FileLoaded.TabIndex = 37;
            this.tbSourceCode_FileLoaded.TextChanged += new System.EventHandler(this.tbSourceCode_FileLoaded_TextChanged);
            this.tbSourceCode_FileLoaded.DragDrop += new System.Windows.Forms.DragEventHandler(this.tbSourceCode_FileLoaded_DragDrop);
            this.tbSourceCode_FileLoaded.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSourceCode_FileLoaded_KeyPress);
            this.tbSourceCode_FileLoaded.DragEnter += new System.Windows.Forms.DragEventHandler(this.tbSourceCode_FileLoaded_DragEnter);
            // 
            // tbTextSearch
            // 
            this.tbTextSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbTextSearch.Location = new System.Drawing.Point(153, 389);
            this.tbTextSearch.Name = "tbTextSearch";
            this.tbTextSearch.Size = new System.Drawing.Size(267, 20);
            this.tbTextSearch.TabIndex = 39;
            this.tbTextSearch.TextChanged += new System.EventHandler(this.tbTextSearch_TextChanged);
            this.tbTextSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbTextSearch_KeyPress);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 392);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "Text Search (case sensitive)";
            // 
            // lbSearch_textNotFound
            // 
            this.lbSearch_textNotFound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbSearch_textNotFound.AutoSize = true;
            this.lbSearch_textNotFound.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSearch_textNotFound.ForeColor = System.Drawing.Color.Red;
            this.lbSearch_textNotFound.Location = new System.Drawing.Point(426, 390);
            this.lbSearch_textNotFound.Name = "lbSearch_textNotFound";
            this.lbSearch_textNotFound.Size = new System.Drawing.Size(98, 15);
            this.lbSearch_textNotFound.TabIndex = 41;
            this.lbSearch_textNotFound.Text = "Text not found";
            this.lbSearch_textNotFound.Visible = false;
            // 
            // tbSourceCode_DirectoryOfFileLoaded
            // 
            this.tbSourceCode_DirectoryOfFileLoaded.AllowDrop = true;
            this.tbSourceCode_DirectoryOfFileLoaded.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSourceCode_DirectoryOfFileLoaded.BackColor = System.Drawing.Color.White;
            this.tbSourceCode_DirectoryOfFileLoaded.ForeColor = System.Drawing.Color.Black;
            this.tbSourceCode_DirectoryOfFileLoaded.Location = new System.Drawing.Point(79, 19);
            this.tbSourceCode_DirectoryOfFileLoaded.Name = "tbSourceCode_DirectoryOfFileLoaded";
            this.tbSourceCode_DirectoryOfFileLoaded.Size = new System.Drawing.Size(367, 20);
            this.tbSourceCode_DirectoryOfFileLoaded.TabIndex = 42;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(449, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 13);
            this.label2.TabIndex = 43;
            this.label2.Text = "\\";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 44;
            this.label3.Text = "File Loaded:";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.btSettings,
            this.btSaveFile,
            this.tbShowO2ObjectModel,
            this.toolStripSeparator3,
            this.lbExecuteOnEngine,
            this.btCompileCode,
            this.cbExternalEngineToUse,
            this.btExecuteOnExternalEngine,
            this.btShowHidePythonLogExecutionOutputData,
            this.lbCompileCode,
            this.tsCompileStripSeparator,
            this.lbExecuteCode,
            this.cboxCompliledSourceCodeMethods,
            this.lbCompilationErrors,
            this.btShowHideCompilationErrors,
            this.btExecuteSelectedMethod,
            this.btDragAssemblyCreated,
            this.toolStripSeparator1,
            this.btSelectedLineHistory,
            this.toolStripSeparator4,
            this.btShowLogs,
            this.lbSampleScripts,
            this.cBoxSampleScripts,
            this.toolStripSeparator5});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1028, 25);
            this.toolStrip1.TabIndex = 45;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(40, 22);
            this.toolStripLabel3.Text = "config:";
            // 
            // btSettings
            // 
            this.btSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btSettings.Image = ((System.Drawing.Image)(resources.GetObject("btSettings.Image")));
            this.btSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSettings.Name = "btSettings";
            this.btSettings.Size = new System.Drawing.Size(23, 22);
            this.btSettings.Text = "Settings, loaded file details and save into another file";
            this.btSettings.Click += new System.EventHandler(this.settings_Click);
            // 
            // btSaveFile
            // 
            this.btSaveFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btSaveFile.Enabled = false;
            this.btSaveFile.Image = ((System.Drawing.Image)(resources.GetObject("btSaveFile.Image")));
            this.btSaveFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSaveFile.Name = "btSaveFile";
            this.btSaveFile.Size = new System.Drawing.Size(23, 22);
            this.btSaveFile.Text = "Save file";
            this.btSaveFile.Click += new System.EventHandler(this.btSaveFile_Click);
            // 
            // tbShowO2ObjectModel
            // 
            this.tbShowO2ObjectModel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbShowO2ObjectModel.Image = ((System.Drawing.Image)(resources.GetObject("tbShowO2ObjectModel.Image")));
            this.tbShowO2ObjectModel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbShowO2ObjectModel.Name = "tbShowO2ObjectModel";
            this.tbShowO2ObjectModel.Size = new System.Drawing.Size(23, 22);
            this.tbShowO2ObjectModel.Text = "Show O2 Object Model";
            this.tbShowO2ObjectModel.Click += new System.EventHandler(this.tbShowO2ObjectModel_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // lbExecuteOnEngine
            // 
            this.lbExecuteOnEngine.Name = "lbExecuteOnEngine";
            this.lbExecuteOnEngine.Size = new System.Drawing.Size(100, 22);
            this.lbExecuteOnEngine.Text = "Execute on engine:";
            this.lbExecuteOnEngine.Visible = false;
            // 
            // btCompileCode
            // 
            this.btCompileCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btCompileCode.Image = ((System.Drawing.Image)(resources.GetObject("btCompileCode.Image")));
            this.btCompileCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btCompileCode.Name = "btCompileCode";
            this.btCompileCode.Size = new System.Drawing.Size(23, 22);
            this.btCompileCode.Text = "Compile Source Code";
            this.btCompileCode.Click += new System.EventHandler(this.compile_Click);
            // 
            // cbExternalEngineToUse
            // 
            this.cbExternalEngineToUse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExternalEngineToUse.DropDownWidth = 80;
            this.cbExternalEngineToUse.Name = "cbExternalEngineToUse";
            this.cbExternalEngineToUse.Size = new System.Drawing.Size(85, 25);
            this.cbExternalEngineToUse.Visible = false;
            // 
            // btExecuteOnExternalEngine
            // 
            this.btExecuteOnExternalEngine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btExecuteOnExternalEngine.Image = ((System.Drawing.Image)(resources.GetObject("btExecuteOnExternalEngine.Image")));
            this.btExecuteOnExternalEngine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btExecuteOnExternalEngine.Name = "btExecuteOnExternalEngine";
            this.btExecuteOnExternalEngine.Size = new System.Drawing.Size(23, 22);
            this.btExecuteOnExternalEngine.Text = "Execute On External Engine Script";
            this.btExecuteOnExternalEngine.Visible = false;
            this.btExecuteOnExternalEngine.Click += new System.EventHandler(this.btExecutePythonScript_Click);
            // 
            // btShowHidePythonLogExecutionOutputData
            // 
            this.btShowHidePythonLogExecutionOutputData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btShowHidePythonLogExecutionOutputData.Image = ((System.Drawing.Image)(resources.GetObject("btShowHidePythonLogExecutionOutputData.Image")));
            this.btShowHidePythonLogExecutionOutputData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btShowHidePythonLogExecutionOutputData.Name = "btShowHidePythonLogExecutionOutputData";
            this.btShowHidePythonLogExecutionOutputData.Size = new System.Drawing.Size(23, 22);
            this.btShowHidePythonLogExecutionOutputData.Text = "Hide/Show Python Execution Output Data";
            this.btShowHidePythonLogExecutionOutputData.Visible = false;
            this.btShowHidePythonLogExecutionOutputData.Click += new System.EventHandler(this.btShowHidePythonLogExecutionOutputData_Click);
            // 
            // lbCompileCode
            // 
            this.lbCompileCode.Name = "lbCompileCode";
            this.lbCompileCode.Size = new System.Drawing.Size(72, 22);
            this.lbCompileCode.Text = "compile code:";
            this.lbCompileCode.Click += new System.EventHandler(this.toolStripLabel2_Click);
            // 
            // tsCompileStripSeparator
            // 
            this.tsCompileStripSeparator.Name = "tsCompileStripSeparator";
            this.tsCompileStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // lbExecuteCode
            // 
            this.lbExecuteCode.Name = "lbExecuteCode";
            this.lbExecuteCode.Size = new System.Drawing.Size(76, 22);
            this.lbExecuteCode.Text = "execute code:";
            this.lbExecuteCode.Visible = false;
            // 
            // cboxCompliledSourceCodeMethods
            // 
            this.cboxCompliledSourceCodeMethods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxCompliledSourceCodeMethods.Name = "cboxCompliledSourceCodeMethods";
            this.cboxCompliledSourceCodeMethods.Size = new System.Drawing.Size(121, 25);
            this.cboxCompliledSourceCodeMethods.Visible = false;
            // 
            // lbCompilationErrors
            // 
            this.lbCompilationErrors.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCompilationErrors.ForeColor = System.Drawing.Color.Red;
            this.lbCompilationErrors.Name = "lbCompilationErrors";
            this.lbCompilationErrors.Size = new System.Drawing.Size(111, 22);
            this.lbCompilationErrors.Text = "Compilation Errors";
            this.lbCompilationErrors.Visible = false;
            // 
            // btShowHideCompilationErrors
            // 
            this.btShowHideCompilationErrors.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btShowHideCompilationErrors.Image = ((System.Drawing.Image)(resources.GetObject("btShowHideCompilationErrors.Image")));
            this.btShowHideCompilationErrors.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btShowHideCompilationErrors.Name = "btShowHideCompilationErrors";
            this.btShowHideCompilationErrors.Size = new System.Drawing.Size(23, 22);
            this.btShowHideCompilationErrors.Text = "Show / Hide compilation errors";
            this.btShowHideCompilationErrors.Visible = false;
            this.btShowHideCompilationErrors.Click += new System.EventHandler(this.btShowHideCompilationErrors_Click);
            // 
            // btExecuteSelectedMethod
            // 
            this.btExecuteSelectedMethod.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btExecuteSelectedMethod.Image = ((System.Drawing.Image)(resources.GetObject("btExecuteSelectedMethod.Image")));
            this.btExecuteSelectedMethod.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btExecuteSelectedMethod.Name = "btExecuteSelectedMethod";
            this.btExecuteSelectedMethod.Size = new System.Drawing.Size(23, 22);
            this.btExecuteSelectedMethod.Text = "Execute Selected Method";
            this.btExecuteSelectedMethod.Visible = false;
            this.btExecuteSelectedMethod.Click += new System.EventHandler(this.executeSelectedMethod_Click);
            // 
            // btDragAssemblyCreated
            // 
            this.btDragAssemblyCreated.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btDragAssemblyCreated.Image = ((System.Drawing.Image)(resources.GetObject("btDragAssemblyCreated.Image")));
            this.btDragAssemblyCreated.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btDragAssemblyCreated.Name = "btDragAssemblyCreated";
            this.btDragAssemblyCreated.Size = new System.Drawing.Size(23, 22);
            this.btDragAssemblyCreated.Text = "Drag Assembly Created";
            this.btDragAssemblyCreated.Visible = false;
            this.btDragAssemblyCreated.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btDragAssemblyCreated_MouseDown);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btSelectedLineHistory
            // 
            this.btSelectedLineHistory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btSelectedLineHistory.Image = ((System.Drawing.Image)(resources.GetObject("btSelectedLineHistory.Image")));
            this.btSelectedLineHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSelectedLineHistory.Name = "btSelectedLineHistory";
            this.btSelectedLineHistory.Size = new System.Drawing.Size(23, 22);
            this.btSelectedLineHistory.Text = "View Selected Line History";
            this.btSelectedLineHistory.Click += new System.EventHandler(this.btSelectedLineHistory_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btShowLogs
            // 
            this.btShowLogs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btShowLogs.Image = ((System.Drawing.Image)(resources.GetObject("btShowLogs.Image")));
            this.btShowLogs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btShowLogs.Name = "btShowLogs";
            this.btShowLogs.Size = new System.Drawing.Size(23, 22);
            this.btShowLogs.Text = "Show Logs";
            this.btShowLogs.Click += new System.EventHandler(this.showLogs_Click);
            // 
            // lbSampleScripts
            // 
            this.lbSampleScripts.Name = "lbSampleScripts";
            this.lbSampleScripts.Size = new System.Drawing.Size(74, 22);
            this.lbSampleScripts.Text = "sample scripts";
            this.lbSampleScripts.Visible = false;
            // 
            // cBoxSampleScripts
            // 
            this.cBoxSampleScripts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxSampleScripts.Name = "cBoxSampleScripts";
            this.cBoxSampleScripts.Size = new System.Drawing.Size(121, 25);
            this.cBoxSampleScripts.Visible = false;
            this.cBoxSampleScripts.SelectedIndexChanged += new System.EventHandler(this.cBoxSampleScripts_SelectedIndexChanged);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBoxWithFileAndSaveSettings
            // 
            this.groupBoxWithFileAndSaveSettings.Controls.Add(this.cbAutoTryToFixSourceCodeFileReferences);
            this.groupBoxWithFileAndSaveSettings.Controls.Add(this.groupBox1);
            this.groupBoxWithFileAndSaveSettings.Controls.Add(this.llReload);
            this.groupBoxWithFileAndSaveSettings.Controls.Add(this.llCurrenlyLoadedObjectModel);
            this.groupBoxWithFileAndSaveSettings.Controls.Add(this.tbMaxLoadSize);
            this.groupBoxWithFileAndSaveSettings.Controls.Add(this.label4);
            this.groupBoxWithFileAndSaveSettings.Controls.Add(this.label3);
            this.groupBoxWithFileAndSaveSettings.Controls.Add(this.tbSourceCode_DirectoryOfFileLoaded);
            this.groupBoxWithFileAndSaveSettings.Controls.Add(this.btSave);
            this.groupBoxWithFileAndSaveSettings.Controls.Add(this.lbSource_CodeFileSaved);
            this.groupBoxWithFileAndSaveSettings.Controls.Add(this.label2);
            this.groupBoxWithFileAndSaveSettings.Controls.Add(this.lbSourceCode_UnsavedChanges);
            this.groupBoxWithFileAndSaveSettings.Controls.Add(this.tbSourceCode_FileLoaded);
            this.groupBoxWithFileAndSaveSettings.Location = new System.Drawing.Point(3, 28);
            this.groupBoxWithFileAndSaveSettings.Name = "groupBoxWithFileAndSaveSettings";
            this.groupBoxWithFileAndSaveSettings.Size = new System.Drawing.Size(727, 134);
            this.groupBoxWithFileAndSaveSettings.TabIndex = 46;
            this.groupBoxWithFileAndSaveSettings.TabStop = false;
            this.groupBoxWithFileAndSaveSettings.Text = "Settings, loaded file details and save into another file";
            this.groupBoxWithFileAndSaveSettings.Visible = false;
            // 
            // cbAutoTryToFixSourceCodeFileReferences
            // 
            this.cbAutoTryToFixSourceCodeFileReferences.AutoSize = true;
            this.cbAutoTryToFixSourceCodeFileReferences.Checked = true;
            this.cbAutoTryToFixSourceCodeFileReferences.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoTryToFixSourceCodeFileReferences.Location = new System.Drawing.Point(367, 109);
            this.cbAutoTryToFixSourceCodeFileReferences.Name = "cbAutoTryToFixSourceCodeFileReferences";
            this.cbAutoTryToFixSourceCodeFileReferences.Size = new System.Drawing.Size(236, 17);
            this.cbAutoTryToFixSourceCodeFileReferences.TabIndex = 52;
            this.cbAutoTryToFixSourceCodeFileReferences.Text = "Auto Try to Fix Source Code File References";
            this.cbAutoTryToFixSourceCodeFileReferences.UseVisualStyleBackColor = true;
            this.cbAutoTryToFixSourceCodeFileReferences.CheckedChanged += new System.EventHandler(this.cbAutoTryToFixSourceCodeFileReferences_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboxLineNumbers);
            this.groupBox1.Controls.Add(this.cboxInvalidLines);
            this.groupBox1.Controls.Add(this.cboxTabs);
            this.groupBox1.Controls.Add(this.cboxEOLMarkers);
            this.groupBox1.Controls.Add(this.cboxSpaces);
            this.groupBox1.Controls.Add(this.cboxHRuler);
            this.groupBox1.Controls.Add(this.cboxVRuler);
            this.groupBox1.Location = new System.Drawing.Point(9, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(352, 60);
            this.groupBox1.TabIndex = 49;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SharpDevelop editor Preferences";
            // 
            // llReload
            // 
            this.llReload.AutoSize = true;
            this.llReload.Location = new System.Drawing.Point(662, 110);
            this.llReload.Name = "llReload";
            this.llReload.Size = new System.Drawing.Size(55, 13);
            this.llReload.TabIndex = 48;
            this.llReload.TabStop = true;
            this.llReload.Text = "reload File";
            this.llReload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llReload_LinkClicked);
            // 
            // llCurrenlyLoadedObjectModel
            // 
            this.llCurrenlyLoadedObjectModel.AutoSize = true;
            this.llCurrenlyLoadedObjectModel.Location = new System.Drawing.Point(609, 90);
            this.llCurrenlyLoadedObjectModel.Name = "llCurrenlyLoadedObjectModel";
            this.llCurrenlyLoadedObjectModel.Size = new System.Drawing.Size(108, 13);
            this.llCurrenlyLoadedObjectModel.TabIndex = 47;
            this.llCurrenlyLoadedObjectModel.TabStop = true;
            this.llCurrenlyLoadedObjectModel.Text = "drag C# object model";
            this.llCurrenlyLoadedObjectModel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.llCurrenlyLoadedObjectModel_MouseDown);
            // 
            // tbMaxLoadSize
            // 
            this.tbMaxLoadSize.AllowDrop = true;
            this.tbMaxLoadSize.BackColor = System.Drawing.Color.White;
            this.tbMaxLoadSize.ForeColor = System.Drawing.Color.Black;
            this.tbMaxLoadSize.Location = new System.Drawing.Point(110, 43);
            this.tbMaxLoadSize.Name = "tbMaxLoadSize";
            this.tbMaxLoadSize.Size = new System.Drawing.Size(93, 20);
            this.tbMaxLoadSize.TabIndex = 46;
            this.tbMaxLoadSize.TextChanged += new System.EventHandler(this.tbMaxLoadSize_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 45;
            this.label4.Text = "Max Load Size (kb)";
            // 
            // lbPartialFileView
            // 
            this.lbPartialFileView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPartialFileView.FormattingEnabled = true;
            this.lbPartialFileView.Location = new System.Drawing.Point(3, 30);
            this.lbPartialFileView.Name = "lbPartialFileView";
            this.lbPartialFileView.Size = new System.Drawing.Size(1025, 342);
            this.lbPartialFileView.TabIndex = 47;
            this.lbPartialFileView.Visible = false;
            // 
            // tbExecutionHistoryOrLog
            // 
            this.tbExecutionHistoryOrLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbExecutionHistoryOrLog.Location = new System.Drawing.Point(643, 28);
            this.tbExecutionHistoryOrLog.Multiline = true;
            this.tbExecutionHistoryOrLog.Name = "tbExecutionHistoryOrLog";
            this.tbExecutionHistoryOrLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbExecutionHistoryOrLog.Size = new System.Drawing.Size(364, 234);
            this.tbExecutionHistoryOrLog.TabIndex = 48;
            this.tbExecutionHistoryOrLog.Visible = false;
            // 
            // lboxCompilationErrors
            // 
            this.lboxCompilationErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lboxCompilationErrors.ForeColor = System.Drawing.Color.Red;
            this.lboxCompilationErrors.FormattingEnabled = true;
            this.lboxCompilationErrors.Location = new System.Drawing.Point(643, 28);
            this.lboxCompilationErrors.Name = "lboxCompilationErrors";
            this.lboxCompilationErrors.Size = new System.Drawing.Size(364, 173);
            this.lboxCompilationErrors.TabIndex = 49;
            this.lboxCompilationErrors.Visible = false;
            this.lboxCompilationErrors.SelectedIndexChanged += new System.EventHandler(this.cbCompilationErrors_SelectedIndexChanged);
            // 
            // gbO2ObjectMode
            // 
            this.gbO2ObjectMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gbO2ObjectMode.Controls.Add(this.scrollBarHorizontalSize_O2ObjectModel);
            this.gbO2ObjectMode.Controls.Add(this.scrollBarVerticalSize_O2ObjectModel);
            this.gbO2ObjectMode.Controls.Add(this.o2ObjectModel);
            this.gbO2ObjectMode.Location = new System.Drawing.Point(575, 91);
            this.gbO2ObjectMode.Name = "gbO2ObjectMode";
            this.gbO2ObjectMode.Size = new System.Drawing.Size(432, 281);
            this.gbO2ObjectMode.TabIndex = 51;
            this.gbO2ObjectMode.TabStop = false;
            this.gbO2ObjectMode.Text = "O2 Object Model";
            this.gbO2ObjectMode.Visible = false;
            this.gbO2ObjectMode.SizeChanged += new System.EventHandler(this.gbO2ObjectMode_SizeChanged);
            // 
            // scrollBarHorizontalSize_O2ObjectModel
            // 
            this.scrollBarHorizontalSize_O2ObjectModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.scrollBarHorizontalSize_O2ObjectModel.Location = new System.Drawing.Point(380, 268);
            this.scrollBarHorizontalSize_O2ObjectModel.Name = "scrollBarHorizontalSize_O2ObjectModel";
            this.scrollBarHorizontalSize_O2ObjectModel.Size = new System.Drawing.Size(38, 10);
            this.scrollBarHorizontalSize_O2ObjectModel.SmallChange = 10;
            this.scrollBarHorizontalSize_O2ObjectModel.TabIndex = 55;
            this.scrollBarHorizontalSize_O2ObjectModel.Visible = false;
            this.scrollBarHorizontalSize_O2ObjectModel.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBarHorizontalSize_O2ObjectModel_Scroll);
            // 
            // scrollBarVerticalSize_O2ObjectModel
            // 
            this.scrollBarVerticalSize_O2ObjectModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.scrollBarVerticalSize_O2ObjectModel.Location = new System.Drawing.Point(419, 240);
            this.scrollBarVerticalSize_O2ObjectModel.Name = "scrollBarVerticalSize_O2ObjectModel";
            this.scrollBarVerticalSize_O2ObjectModel.Size = new System.Drawing.Size(10, 38);
            this.scrollBarVerticalSize_O2ObjectModel.SmallChange = 10;
            this.scrollBarVerticalSize_O2ObjectModel.TabIndex = 54;
            this.scrollBarVerticalSize_O2ObjectModel.Visible = false;
            this.scrollBarVerticalSize_O2ObjectModel.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBarVerticalSize_O2ObjectModel_Scroll);
            // 
            // o2ObjectModel
            // 
            this.o2ObjectModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.o2ObjectModel.Location = new System.Drawing.Point(3, 16);
            this.o2ObjectModel.Name = "o2ObjectModel";
            this.o2ObjectModel.Size = new System.Drawing.Size(426, 262);
            this.o2ObjectModel.TabIndex = 0;
            // 
            // ascx_SourceCodeEditor
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.gbO2ObjectMode);
            this.Controls.Add(this.groupBoxWithFileAndSaveSettings);
            this.Controls.Add(this.lboxCompilationErrors);
            this.Controls.Add(this.tbExecutionHistoryOrLog);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tecSourceCode);
            this.Controls.Add(this.lbPartialFileView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbTextSearch);
            this.Controls.Add(this.lbSearch_textNotFound);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ascx_SourceCodeEditor";
            this.Size = new System.Drawing.Size(1028, 411);
            this.Load += new System.EventHandler(this.ascx_SourceCodeEditor_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ascx_SourceCodeEditor_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.ascx_SourceCodeEditor_DragEnter);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBoxWithFileAndSaveSettings.ResumeLayout(false);
            this.groupBoxWithFileAndSaveSettings.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbO2ObjectMode.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ICSharpCode.TextEditor.TextEditorControl tecSourceCode;
        private System.Windows.Forms.CheckBox cboxVRuler;
        private System.Windows.Forms.CheckBox cboxInvalidLines;
        private System.Windows.Forms.CheckBox cboxHRuler;
        private System.Windows.Forms.CheckBox cboxEOLMarkers;
        private System.Windows.Forms.CheckBox cboxSpaces;
        private System.Windows.Forms.CheckBox cboxTabs;
        private System.Windows.Forms.CheckBox cboxLineNumbers;
        private System.Windows.Forms.Label lbSource_CodeFileSaved;
        private System.Windows.Forms.Label lbSourceCode_UnsavedChanges;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.TextBox tbSourceCode_FileLoaded;
        private System.Windows.Forms.ImageList ilSourceCodeEdit;
        private System.Windows.Forms.TextBox tbTextSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbSearch_textNotFound;
        private System.Windows.Forms.TextBox tbSourceCode_DirectoryOfFileLoaded;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.GroupBox groupBoxWithFileAndSaveSettings;
        private System.Windows.Forms.ToolStripButton btSettings;
        private System.Windows.Forms.ToolStripButton btCompileCode;
        private System.Windows.Forms.ToolStripLabel lbExecuteCode;
        private System.Windows.Forms.ToolStripComboBox cboxCompliledSourceCodeMethods;
        private System.Windows.Forms.ToolStripButton btExecuteSelectedMethod;
        private System.Windows.Forms.ToolStripButton btShowLogs;
        private System.Windows.Forms.ToolStripLabel lbCompileCode;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel lbSampleScripts;
        private System.Windows.Forms.ToolStripComboBox cBoxSampleScripts;
        private System.Windows.Forms.ToolStripLabel lbCompilationErrors;
        private System.Windows.Forms.ToolStripSeparator tsCompileStripSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.TextBox tbMaxLoadSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lbPartialFileView;
        private System.Windows.Forms.ToolStripButton btSelectedLineHistory;
        private System.Windows.Forms.TextBox tbExecutionHistoryOrLog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ListBox lboxCompilationErrors;
        private System.Windows.Forms.LinkLabel llCurrenlyLoadedObjectModel;
        private System.Windows.Forms.LinkLabel llReload;
        private System.Windows.Forms.ToolStripButton btSaveFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripButton btDragAssemblyCreated;
        private System.Windows.Forms.ToolStripButton btExecuteOnExternalEngine;
        private System.Windows.Forms.ToolStripLabel lbExecuteOnEngine;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btShowHidePythonLogExecutionOutputData;
        private System.Windows.Forms.ToolStripComboBox cbExternalEngineToUse;
        private System.Windows.Forms.CheckBox cbAutoTryToFixSourceCodeFileReferences;
        private System.Windows.Forms.ToolStripButton btShowHideCompilationErrors;
        private System.Windows.Forms.GroupBox gbO2ObjectMode;
        private System.Windows.Forms.ToolStripButton tbShowO2ObjectModel;
        private O2.Views.ASCX.CoreControls.ascx_O2ObjectModel o2ObjectModel;
        private System.Windows.Forms.HScrollBar scrollBarHorizontalSize_O2ObjectModel;
        private System.Windows.Forms.VScrollBar scrollBarVerticalSize_O2ObjectModel;
    }
}
