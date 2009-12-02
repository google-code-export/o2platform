using O2.Views.ASCX.CoreControls;

namespace O2.Tool.DotNetCallbacksMaker.ascx
{
    partial class ascx_dotNetCallbacksMaker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ascx_dotNetCallbacksMaker));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.scHost = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.llDownloadDemoFile_HacmeBank_WebServices = new System.Windows.Forms.LinkLabel();
            this.cbLoadFromDirectory_Recursively = new System.Windows.Forms.CheckBox();
            this.adDirectory = new O2.Views.ASCX.CoreControls.ascx_Directory();
            this.label6 = new System.Windows.Forms.Label();
            this.btDeleteFile = new System.Windows.Forms.Button();
            this.ilDirectoriesAndFiles = new System.Windows.Forms.ImageList(this.components);
            this.lbFilesToSearchCallbackOn = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.pbAddingMethodsProgressBar = new System.Windows.Forms.ProgressBar();
            this.btAddAllMethodsAsCallbacks = new System.Windows.Forms.Button();
            this.scPotentialTargets = new System.Windows.Forms.SplitContainer();
            this.lbListOfPotentialTargets = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btRemoveMethodsFromLIst = new System.Windows.Forms.Button();
            this.btAddAllMethodsToList = new System.Windows.Forms.Button();
            this.btAddMethodToList = new System.Windows.Forms.Button();
            this.lbFilesToLoad = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.rbDotNet_SearchForPublicMethods = new System.Windows.Forms.RadioButton();
            this.rbDotNet_SearchForWebServicesMethods = new System.Windows.Forms.RadioButton();
            this.btAddSelectedMethodsAsCallbacks = new System.Windows.Forms.Button();
            this.btProcessFilesAndCalculateTargets = new System.Windows.Forms.Button();
            this.btDeleteAllCallbacks = new System.Windows.Forms.Button();
            this.btDeleteSelectedCallbaks = new System.Windows.Forms.Button();
            this.dgvCallbacksInLddbDatabase = new System.Windows.Forms.DataGridView();
            this.btLoadListOfCallbacksInLddbDatabase = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.scHost.Panel1.SuspendLayout();
            this.scHost.Panel2.SuspendLayout();
            this.scHost.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.scPotentialTargets.Panel1.SuspendLayout();
            this.scPotentialTargets.Panel2.SuspendLayout();
            this.scPotentialTargets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCallbacksInLddbDatabase)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(4, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Or chose file to process (Double click on file to add it)";
            // 
            // scHost
            // 
            this.scHost.BackColor = System.Drawing.SystemColors.Control;
            this.scHost.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scHost.Location = new System.Drawing.Point(0, 0);
            this.scHost.Name = "scHost";
            // 
            // scHost.Panel1
            // 
            this.scHost.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.scHost.Panel1.Controls.Add(this.splitContainer1);
            // 
            // scHost.Panel2
            // 
            this.scHost.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.scHost.Panel2.Controls.Add(this.splitContainer3);
            this.scHost.Size = new System.Drawing.Size(886, 582);
            this.scHost.SplitterDistance = 274;
            this.scHost.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.cbLoadFromDirectory_Recursively);
            this.splitContainer1.Panel1.Controls.Add(this.adDirectory);
            this.splitContainer1.Panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.btDeleteFile);
            this.splitContainer1.Panel2.Controls.Add(this.lbFilesToSearchCallbackOn);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(274, 582);
            this.splitContainer1.SplitterDistance = 227;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.llDownloadDemoFile_HacmeBank_WebServices);
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 44);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Load demo data:";
            // 
            // llDownloadDemoFile_HacmeBank_WebServices
            // 
            this.llDownloadDemoFile_HacmeBank_WebServices.AutoSize = true;
            this.llDownloadDemoFile_HacmeBank_WebServices.Location = new System.Drawing.Point(9, 16);
            this.llDownloadDemoFile_HacmeBank_WebServices.Name = "llDownloadDemoFile_HacmeBank_WebServices";
            this.llDownloadDemoFile_HacmeBank_WebServices.Size = new System.Drawing.Size(166, 13);
            this.llDownloadDemoFile_HacmeBank_WebServices.TabIndex = 0;
            this.llDownloadDemoFile_HacmeBank_WebServices.TabStop = true;
            this.llDownloadDemoFile_HacmeBank_WebServices.Text = "HacmeBank - WebServices (.net)";
            this.llDownloadDemoFile_HacmeBank_WebServices.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDownloadDemoFile_HacmeBank_WebServices_LinkClicked);
            // 
            // cbLoadFromDirectory_Recursively
            // 
            this.cbLoadFromDirectory_Recursively.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbLoadFromDirectory_Recursively.AutoSize = true;
            this.cbLoadFromDirectory_Recursively.Enabled = false;
            this.cbLoadFromDirectory_Recursively.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbLoadFromDirectory_Recursively.Location = new System.Drawing.Point(3, 203);
            this.cbLoadFromDirectory_Recursively.Name = "cbLoadFromDirectory_Recursively";
            this.cbLoadFromDirectory_Recursively.Size = new System.Drawing.Size(227, 17);
            this.cbLoadFromDirectory_Recursively.TabIndex = 3;
            this.cbLoadFromDirectory_Recursively.Text = "(On Double Click) search recursively  in Dir";
            this.cbLoadFromDirectory_Recursively.UseVisualStyleBackColor = true;
            // 
            // adDirectory
            // 
            this.adDirectory._ProcessDroppedObjects = true;
            this.adDirectory._ShowFileSize = false;
            this.adDirectory._ShowLinkToUpperFolder = true;
            this.adDirectory._ViewMode = O2.Views.ASCX.CoreControls.ascx_Directory.ViewMode.Simple_With_LocationBar;
            this.adDirectory._WatchFolder = false;
            this.adDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.adDirectory.BackColor = System.Drawing.SystemColors.Control;
            this.adDirectory.ForeColor = System.Drawing.SystemColors.ControlText;
            this.adDirectory.Location = new System.Drawing.Point(4, 74);
            this.adDirectory.Name = "adDirectory";
            this.adDirectory.Size = new System.Drawing.Size(263, 121);
            this.adDirectory.TabIndex = 0;
            this.adDirectory.eDirectoryEvent_Click += new O2.Views.ASCX.CoreControls.ascx_Directory.dDirectoryEvent(this.adDirectory_eDirectoryEvent_Click);
            this.adDirectory.eDirectoryEvent_DoubleClick += new O2.Views.ASCX.CoreControls.ascx_Directory.dDirectoryEvent(this.adDirectory_eDirectoryEvent_DoubleClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(3, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(212, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "You can drop .NET dll files to be processed";
            // 
            // btDeleteFile
            // 
            this.btDeleteFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btDeleteFile.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btDeleteFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btDeleteFile.ImageKey = "Edit_remove.ico";
            this.btDeleteFile.ImageList = this.ilDirectoriesAndFiles;
            this.btDeleteFile.Location = new System.Drawing.Point(235, 6);
            this.btDeleteFile.Name = "btDeleteFile";
            this.btDeleteFile.Size = new System.Drawing.Size(34, 19);
            this.btDeleteFile.TabIndex = 23;
            this.btDeleteFile.UseVisualStyleBackColor = true;
            this.btDeleteFile.Click += new System.EventHandler(this.btDeleteFile_Click);
            // 
            // ilDirectoriesAndFiles
            // 
            this.ilDirectoriesAndFiles.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilDirectoriesAndFiles.ImageStream")));
            this.ilDirectoriesAndFiles.TransparentColor = System.Drawing.Color.Transparent;
            this.ilDirectoriesAndFiles.Images.SetKeyName(0, "Explorer_Folder.ico");
            this.ilDirectoriesAndFiles.Images.SetKeyName(1, "Explorer_File.ico");
            this.ilDirectoriesAndFiles.Images.SetKeyName(2, "project_sourceRoot.ico");
            this.ilDirectoriesAndFiles.Images.SetKeyName(3, "Edit_remove.ico");
            this.ilDirectoriesAndFiles.Images.SetKeyName(4, "refresh_active.ico");
            // 
            // lbFilesToSearchCallbackOn
            // 
            this.lbFilesToSearchCallbackOn.AllowDrop = true;
            this.lbFilesToSearchCallbackOn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFilesToSearchCallbackOn.FormattingEnabled = true;
            this.lbFilesToSearchCallbackOn.Location = new System.Drawing.Point(3, 53);
            this.lbFilesToSearchCallbackOn.Name = "lbFilesToSearchCallbackOn";
            this.lbFilesToSearchCallbackOn.Size = new System.Drawing.Size(264, 290);
            this.lbFilesToSearchCallbackOn.TabIndex = 4;
            this.lbFilesToSearchCallbackOn.SelectedIndexChanged += new System.EventHandler(this.lbFilesToSearchCallbackOn_SelectedIndexChanged);
            this.lbFilesToSearchCallbackOn.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbFilesToSearchCallbackOn_DragDrop);
            this.lbFilesToSearchCallbackOn.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbFilesToSearchCallbackOn_DragEnter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(1, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(217, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Files to search for callback functions";
            // 
            // splitContainer3
            // 
            this.splitContainer3.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer3.Panel1.Controls.Add(this.pbAddingMethodsProgressBar);
            this.splitContainer3.Panel1.Controls.Add(this.btAddAllMethodsAsCallbacks);
            this.splitContainer3.Panel1.Controls.Add(this.scPotentialTargets);
            this.splitContainer3.Panel1.Controls.Add(this.rbDotNet_SearchForPublicMethods);
            this.splitContainer3.Panel1.Controls.Add(this.rbDotNet_SearchForWebServicesMethods);
            this.splitContainer3.Panel1.Controls.Add(this.btAddSelectedMethodsAsCallbacks);
            this.splitContainer3.Panel1.Controls.Add(this.btProcessFilesAndCalculateTargets);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer3.Panel2.Controls.Add(this.btDeleteAllCallbacks);
            this.splitContainer3.Panel2.Controls.Add(this.btDeleteSelectedCallbaks);
            this.splitContainer3.Panel2.Controls.Add(this.dgvCallbacksInLddbDatabase);
            this.splitContainer3.Panel2.Controls.Add(this.btLoadListOfCallbacksInLddbDatabase);
            this.splitContainer3.Panel2.Controls.Add(this.label3);
            this.splitContainer3.Size = new System.Drawing.Size(608, 582);
            this.splitContainer3.SplitterDistance = 347;
            this.splitContainer3.TabIndex = 0;
            // 
            // pbAddingMethodsProgressBar
            // 
            this.pbAddingMethodsProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbAddingMethodsProgressBar.Location = new System.Drawing.Point(5, 318);
            this.pbAddingMethodsProgressBar.Name = "pbAddingMethodsProgressBar";
            this.pbAddingMethodsProgressBar.Size = new System.Drawing.Size(155, 23);
            this.pbAddingMethodsProgressBar.TabIndex = 39;
            // 
            // btAddAllMethodsAsCallbacks
            // 
            this.btAddAllMethodsAsCallbacks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAddAllMethodsAsCallbacks.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btAddAllMethodsAsCallbacks.Location = new System.Drawing.Point(197, 317);
            this.btAddAllMethodsAsCallbacks.Name = "btAddAllMethodsAsCallbacks";
            this.btAddAllMethodsAsCallbacks.Size = new System.Drawing.Size(177, 23);
            this.btAddAllMethodsAsCallbacks.TabIndex = 38;
            this.btAddAllMethodsAsCallbacks.Text = "Add all methods as callbacks";
            this.btAddAllMethodsAsCallbacks.UseVisualStyleBackColor = true;
            this.btAddAllMethodsAsCallbacks.Click += new System.EventHandler(this.btAddAllMethodsAsCallbacks_Click);
            // 
            // scPotentialTargets
            // 
            this.scPotentialTargets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.scPotentialTargets.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scPotentialTargets.Location = new System.Drawing.Point(0, 53);
            this.scPotentialTargets.Name = "scPotentialTargets";
            // 
            // scPotentialTargets.Panel1
            // 
            this.scPotentialTargets.Panel1.Controls.Add(this.lbListOfPotentialTargets);
            this.scPotentialTargets.Panel1.Controls.Add(this.label4);
            // 
            // scPotentialTargets.Panel2
            // 
            this.scPotentialTargets.Panel2.Controls.Add(this.btRemoveMethodsFromLIst);
            this.scPotentialTargets.Panel2.Controls.Add(this.btAddAllMethodsToList);
            this.scPotentialTargets.Panel2.Controls.Add(this.btAddMethodToList);
            this.scPotentialTargets.Panel2.Controls.Add(this.lbFilesToLoad);
            this.scPotentialTargets.Panel2.Controls.Add(this.label5);
            this.scPotentialTargets.Size = new System.Drawing.Size(601, 258);
            this.scPotentialTargets.SplitterDistance = 419;
            this.scPotentialTargets.TabIndex = 37;
            // 
            // lbListOfPotentialTargets
            // 
            this.lbListOfPotentialTargets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbListOfPotentialTargets.FormattingEnabled = true;
            this.lbListOfPotentialTargets.Location = new System.Drawing.Point(3, 21);
            this.lbListOfPotentialTargets.Name = "lbListOfPotentialTargets";
            this.lbListOfPotentialTargets.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbListOfPotentialTargets.Size = new System.Drawing.Size(409, 225);
            this.lbListOfPotentialTargets.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(0, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(309, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "List of potential targets (showing only the functions with no rules)";
            // 
            // btRemoveMethodsFromLIst
            // 
            this.btRemoveMethodsFromLIst.ForeColor = System.Drawing.Color.Black;
            this.btRemoveMethodsFromLIst.Location = new System.Drawing.Point(-1, 50);
            this.btRemoveMethodsFromLIst.Name = "btRemoveMethodsFromLIst";
            this.btRemoveMethodsFromLIst.Size = new System.Drawing.Size(75, 23);
            this.btRemoveMethodsFromLIst.TabIndex = 31;
            this.btRemoveMethodsFromLIst.Text = "<<";
            this.btRemoveMethodsFromLIst.UseVisualStyleBackColor = true;
            this.btRemoveMethodsFromLIst.Click += new System.EventHandler(this.btRemoveMethodsFromLIst_Click);
            // 
            // btAddAllMethodsToList
            // 
            this.btAddAllMethodsToList.ForeColor = System.Drawing.Color.Black;
            this.btAddAllMethodsToList.Location = new System.Drawing.Point(40, 21);
            this.btAddAllMethodsToList.Name = "btAddAllMethodsToList";
            this.btAddAllMethodsToList.Size = new System.Drawing.Size(34, 23);
            this.btAddAllMethodsToList.TabIndex = 30;
            this.btAddAllMethodsToList.Text = ">>";
            this.btAddAllMethodsToList.UseVisualStyleBackColor = true;
            this.btAddAllMethodsToList.Click += new System.EventHandler(this.btAddAllMethodsToList_Click);
            // 
            // btAddMethodToList
            // 
            this.btAddMethodToList.ForeColor = System.Drawing.Color.Black;
            this.btAddMethodToList.Location = new System.Drawing.Point(-1, 21);
            this.btAddMethodToList.Name = "btAddMethodToList";
            this.btAddMethodToList.Size = new System.Drawing.Size(34, 23);
            this.btAddMethodToList.TabIndex = 34;
            this.btAddMethodToList.Text = ">";
            this.btAddMethodToList.UseVisualStyleBackColor = true;
            this.btAddMethodToList.Click += new System.EventHandler(this.btAddMethodToList_Click);
            // 
            // lbFilesToLoad
            // 
            this.lbFilesToLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFilesToLoad.FormattingEnabled = true;
            this.lbFilesToLoad.Location = new System.Drawing.Point(80, 21);
            this.lbFilesToLoad.Name = "lbFilesToLoad";
            this.lbFilesToLoad.Size = new System.Drawing.Size(91, 225);
            this.lbFilesToLoad.TabIndex = 32;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(77, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "Files To Load:";
            // 
            // rbDotNet_SearchForPublicMethods
            // 
            this.rbDotNet_SearchForPublicMethods.AutoSize = true;
            this.rbDotNet_SearchForPublicMethods.Checked = true;
            this.rbDotNet_SearchForPublicMethods.ForeColor = System.Drawing.Color.Black;
            this.rbDotNet_SearchForPublicMethods.Location = new System.Drawing.Point(324, 11);
            this.rbDotNet_SearchForPublicMethods.Name = "rbDotNet_SearchForPublicMethods";
            this.rbDotNet_SearchForPublicMethods.Size = new System.Drawing.Size(189, 17);
            this.rbDotNet_SearchForPublicMethods.TabIndex = 36;
            this.rbDotNet_SearchForPublicMethods.TabStop = true;
            this.rbDotNet_SearchForPublicMethods.Text = "dotNet - Search for public methods";
            this.rbDotNet_SearchForPublicMethods.UseVisualStyleBackColor = true;
            // 
            // rbDotNet_SearchForWebServicesMethods
            // 
            this.rbDotNet_SearchForWebServicesMethods.AutoSize = true;
            this.rbDotNet_SearchForWebServicesMethods.ForeColor = System.Drawing.Color.Black;
            this.rbDotNet_SearchForWebServicesMethods.Location = new System.Drawing.Point(324, 30);
            this.rbDotNet_SearchForWebServicesMethods.Name = "rbDotNet_SearchForWebServicesMethods";
            this.rbDotNet_SearchForWebServicesMethods.Size = new System.Drawing.Size(226, 17);
            this.rbDotNet_SearchForWebServicesMethods.TabIndex = 35;
            this.rbDotNet_SearchForWebServicesMethods.Text = "dotNet - Search for WebServices Methods";
            this.rbDotNet_SearchForWebServicesMethods.UseVisualStyleBackColor = true;
            // 
            // btAddSelectedMethodsAsCallbacks
            // 
            this.btAddSelectedMethodsAsCallbacks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAddSelectedMethodsAsCallbacks.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btAddSelectedMethodsAsCallbacks.Location = new System.Drawing.Point(395, 318);
            this.btAddSelectedMethodsAsCallbacks.Name = "btAddSelectedMethodsAsCallbacks";
            this.btAddSelectedMethodsAsCallbacks.Size = new System.Drawing.Size(201, 23);
            this.btAddSelectedMethodsAsCallbacks.TabIndex = 27;
            this.btAddSelectedMethodsAsCallbacks.Text = "Add selected methods as callbacks";
            this.btAddSelectedMethodsAsCallbacks.UseVisualStyleBackColor = true;
            this.btAddSelectedMethodsAsCallbacks.Click += new System.EventHandler(this.btAddSelectedMethodsAsCallbacks_Click);
            // 
            // btProcessFilesAndCalculateTargets
            // 
            this.btProcessFilesAndCalculateTargets.ForeColor = System.Drawing.Color.Black;
            this.btProcessFilesAndCalculateTargets.Location = new System.Drawing.Point(6, 11);
            this.btProcessFilesAndCalculateTargets.Name = "btProcessFilesAndCalculateTargets";
            this.btProcessFilesAndCalculateTargets.Size = new System.Drawing.Size(215, 25);
            this.btProcessFilesAndCalculateTargets.TabIndex = 0;
            this.btProcessFilesAndCalculateTargets.Text = "Process files and calculate targets";
            this.btProcessFilesAndCalculateTargets.UseVisualStyleBackColor = true;
            this.btProcessFilesAndCalculateTargets.Click += new System.EventHandler(this.btProcessFilesAndCalculateTargets_Click);
            // 
            // btDeleteAllCallbacks
            // 
            this.btDeleteAllCallbacks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btDeleteAllCallbacks.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btDeleteAllCallbacks.Location = new System.Drawing.Point(6, 200);
            this.btDeleteAllCallbacks.Name = "btDeleteAllCallbacks";
            this.btDeleteAllCallbacks.Size = new System.Drawing.Size(154, 23);
            this.btDeleteAllCallbacks.TabIndex = 28;
            this.btDeleteAllCallbacks.Text = "Delete All Callbacks";
            this.btDeleteAllCallbacks.UseVisualStyleBackColor = true;
            this.btDeleteAllCallbacks.Click += new System.EventHandler(this.btDeleteAllCallbacks_Click);
            // 
            // btDeleteSelectedCallbaks
            // 
            this.btDeleteSelectedCallbaks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btDeleteSelectedCallbaks.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btDeleteSelectedCallbaks.Location = new System.Drawing.Point(442, 200);
            this.btDeleteSelectedCallbaks.Name = "btDeleteSelectedCallbaks";
            this.btDeleteSelectedCallbaks.Size = new System.Drawing.Size(154, 23);
            this.btDeleteSelectedCallbaks.TabIndex = 27;
            this.btDeleteSelectedCallbaks.Text = "Delete Selected Callbacks";
            this.btDeleteSelectedCallbaks.UseVisualStyleBackColor = true;
            this.btDeleteSelectedCallbaks.Click += new System.EventHandler(this.btDeleteSelectedCallbaks_Click);
            // 
            // dgvCallbacksInLddbDatabase
            // 
            this.dgvCallbacksInLddbDatabase.AllowUserToAddRows = false;
            this.dgvCallbacksInLddbDatabase.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Gray;
            this.dgvCallbacksInLddbDatabase.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCallbacksInLddbDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCallbacksInLddbDatabase.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCallbacksInLddbDatabase.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.dgvCallbacksInLddbDatabase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCallbacksInLddbDatabase.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCallbacksInLddbDatabase.Location = new System.Drawing.Point(3, 37);
            this.dgvCallbacksInLddbDatabase.Name = "dgvCallbacksInLddbDatabase";
            this.dgvCallbacksInLddbDatabase.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCallbacksInLddbDatabase.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCallbacksInLddbDatabase.RowHeadersWidth = 4;
            this.dgvCallbacksInLddbDatabase.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCallbacksInLddbDatabase.Size = new System.Drawing.Size(598, 157);
            this.dgvCallbacksInLddbDatabase.TabIndex = 25;
            this.dgvCallbacksInLddbDatabase.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvCallbacksInLddbDatabase_DataError);
            // 
            // btLoadListOfCallbacksInLddbDatabase
            // 
            this.btLoadListOfCallbacksInLddbDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btLoadListOfCallbacksInLddbDatabase.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btLoadListOfCallbacksInLddbDatabase.Location = new System.Drawing.Point(526, 8);
            this.btLoadListOfCallbacksInLddbDatabase.Name = "btLoadListOfCallbacksInLddbDatabase";
            this.btLoadListOfCallbacksInLddbDatabase.Size = new System.Drawing.Size(75, 23);
            this.btLoadListOfCallbacksInLddbDatabase.TabIndex = 26;
            this.btLoadListOfCallbacksInLddbDatabase.Text = "Reload list";
            this.btLoadListOfCallbacksInLddbDatabase.UseVisualStyleBackColor = true;
            this.btLoadListOfCallbacksInLddbDatabase.Click += new System.EventHandler(this.btLoadListOfCallbacksInLddbDatabase_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(0, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Current callbacks in lddb";
            // 
            // ascx_dotNetCallbacksMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.scHost);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ascx_dotNetCallbacksMaker";
            this.Size = new System.Drawing.Size(886, 582);
            this.Load += new System.EventHandler(this.ascx_dotNetCallbacksMaker_Load);
            this.scHost.Panel1.ResumeLayout(false);
            this.scHost.Panel2.ResumeLayout(false);
            this.scHost.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            this.splitContainer3.ResumeLayout(false);
            this.scPotentialTargets.Panel1.ResumeLayout(false);
            this.scPotentialTargets.Panel1.PerformLayout();
            this.scPotentialTargets.Panel2.ResumeLayout(false);
            this.scPotentialTargets.Panel2.PerformLayout();
            this.scPotentialTargets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCallbacksInLddbDatabase)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ascx_Directory adDirectory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer scHost;
        private System.Windows.Forms.CheckBox cbLoadFromDirectory_Recursively;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btLoadListOfCallbacksInLddbDatabase;
        private System.Windows.Forms.DataGridView dgvCallbacksInLddbDatabase;
        private System.Windows.Forms.ListBox lbFilesToSearchCallbackOn;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button btDeleteFile;
        private System.Windows.Forms.ImageList ilDirectoriesAndFiles;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lbListOfPotentialTargets;
        private System.Windows.Forms.Button btProcessFilesAndCalculateTargets;
        private System.Windows.Forms.Button btAddSelectedMethodsAsCallbacks;
        private System.Windows.Forms.Button btAddMethodToList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lbFilesToLoad;
        private System.Windows.Forms.Button btRemoveMethodsFromLIst;
        private System.Windows.Forms.Button btAddAllMethodsToList;
        private System.Windows.Forms.RadioButton rbDotNet_SearchForPublicMethods;
        private System.Windows.Forms.RadioButton rbDotNet_SearchForWebServicesMethods;
        private System.Windows.Forms.SplitContainer scPotentialTargets;
        private System.Windows.Forms.Button btDeleteSelectedCallbaks;
        private System.Windows.Forms.Button btDeleteAllCallbacks;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btAddAllMethodsAsCallbacks;
        private System.Windows.Forms.ProgressBar pbAddingMethodsProgressBar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel llDownloadDemoFile_HacmeBank_WebServices;
    }
}