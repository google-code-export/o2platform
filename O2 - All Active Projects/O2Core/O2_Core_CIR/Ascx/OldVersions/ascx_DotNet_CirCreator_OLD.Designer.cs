// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Kernel.CodeUtils;
using O2.Views.ASCX.CoreControls;

namespace O2.Core.CIR.Ascx
{
    partial class ascx_DotNet_CirCreator_OLD
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
            this.lbTypes = new System.Windows.Forms.ListBox();
            this.lbFunctions = new System.Windows.Forms.ListBox();
            this.lbFunctionsCalled = new System.Windows.Forms.ListBox();
            this.lbVariables = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.lbOpCodes = new System.Windows.Forms.Label();
            this.tbOpCodes = new System.Windows.Forms.ListBox();
            this.lbAssemblyLoaded = new System.Windows.Forms.Label();
            this.btCreateCir = new System.Windows.Forms.Button();
            this.tbCirDumpCreate_filter = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbTcpPortRemoteF1 = new System.Windows.Forms.TextBox();
            this.cbLoadCreatedCirFileIn_LoadInExtrenalF1Viewer = new System.Windows.Forms.CheckBox();
            this.btCreateF1CirData = new System.Windows.Forms.Button();
            this.cbAddAllAssembliesFromSelectedDirectory_RecursiveSearch = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btAddSelectedDirectory = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btCreateCirDumpFile = new System.Windows.Forms.Button();
            this.lbCreatedFiles_CirDump = new System.Windows.Forms.ListBox();
            this.ad_DirectoryToSaveCreatedFiles = new O2.Views.ASCX.CoreControls.ascx_Directory();
            this.btClearTargetsList = new System.Windows.Forms.Button();
            this.cbAddAllAssembliesFromSelectedDirectory = new System.Windows.Forms.CheckBox();
            this.lbTargetDotNetAssesmblies = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ascx_DropObject1 = new O2.Views.ASCX.CoreControls.ascx_DropObject();
            this.ad_Directory = new O2.Views.ASCX.CoreControls.ascx_Directory();
            this.label12 = new System.Windows.Forms.Label();
            this.tbPathsToSearchForReferencedAssemblies = new System.Windows.Forms.TextBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTypes
            // 
            this.lbTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTypes.FormattingEnabled = true;
            this.lbTypes.Location = new System.Drawing.Point(4, 17);
            this.lbTypes.Name = "lbTypes";
            this.lbTypes.Size = new System.Drawing.Size(311, 160);
            this.lbTypes.TabIndex = 67;
            this.lbTypes.SelectedIndexChanged += new System.EventHandler(this.lbTypes_SelectedIndexChanged);
            // 
            // lbFunctions
            // 
            this.lbFunctions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFunctions.FormattingEnabled = true;
            this.lbFunctions.Location = new System.Drawing.Point(3, 20);
            this.lbFunctions.Name = "lbFunctions";
            this.lbFunctions.Size = new System.Drawing.Size(225, 82);
            this.lbFunctions.TabIndex = 68;
            this.lbFunctions.SelectedIndexChanged += new System.EventHandler(this.lbFunctions_SelectedIndexChanged);
            // 
            // lbFunctionsCalled
            // 
            this.lbFunctionsCalled.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFunctionsCalled.FormattingEnabled = true;
            this.lbFunctionsCalled.Location = new System.Drawing.Point(3, 19);
            this.lbFunctionsCalled.Name = "lbFunctionsCalled";
            this.lbFunctionsCalled.Size = new System.Drawing.Size(175, 4);
            this.lbFunctionsCalled.TabIndex = 69;
            this.lbFunctionsCalled.SelectedIndexChanged += new System.EventHandler(this.lbFunctionsCalled_SelectedIndexChanged);
            // 
            // lbVariables
            // 
            this.lbVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbVariables.FormattingEnabled = true;
            this.lbVariables.Location = new System.Drawing.Point(6, 17);
            this.lbVariables.Name = "lbVariables";
            this.lbVariables.Size = new System.Drawing.Size(392, 4);
            this.lbVariables.TabIndex = 70;
            this.lbVariables.SelectedIndexChanged += new System.EventHandler(this.lbVariables_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 71;
            this.label1.Text = "Types";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 72;
            this.label2.Text = "Functions";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, -2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 73;
            this.label3.Text = "Variables";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 74;
            this.label4.Text = "functions called";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(292, 72);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lbTypes);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(970, 189);
            this.splitContainer1.SplitterDistance = 322;
            this.splitContainer1.TabIndex = 75;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lbFunctions);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(644, 189);
            this.splitContainer2.SplitterDistance = 235;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.lbVariables);
            this.splitContainer3.Panel1.Controls.Add(this.label3);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(405, 189);
            this.splitContainer3.SplitterDistance = 81;
            this.splitContainer3.TabIndex = 0;
            // 
            // splitContainer4
            // 
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.label4);
            this.splitContainer4.Panel1.Controls.Add(this.lbFunctionsCalled);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.lbOpCodes);
            this.splitContainer4.Panel2.Controls.Add(this.tbOpCodes);
            this.splitContainer4.Size = new System.Drawing.Size(405, 104);
            this.splitContainer4.SplitterDistance = 185;
            this.splitContainer4.TabIndex = 0;
            // 
            // lbOpCodes
            // 
            this.lbOpCodes.AutoSize = true;
            this.lbOpCodes.Location = new System.Drawing.Point(3, 3);
            this.lbOpCodes.Name = "lbOpCodes";
            this.lbOpCodes.Size = new System.Drawing.Size(50, 13);
            this.lbOpCodes.TabIndex = 76;
            this.lbOpCodes.Text = "Opcodes";
            // 
            // tbOpCodes
            // 
            this.tbOpCodes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOpCodes.FormattingEnabled = true;
            this.tbOpCodes.Location = new System.Drawing.Point(3, 19);
            this.tbOpCodes.Name = "tbOpCodes";
            this.tbOpCodes.Size = new System.Drawing.Size(206, 4);
            this.tbOpCodes.TabIndex = 75;
            // 
            // lbAssemblyLoaded
            // 
            this.lbAssemblyLoaded.AutoSize = true;
            this.lbAssemblyLoaded.Location = new System.Drawing.Point(503, 10);
            this.lbAssemblyLoaded.Name = "lbAssemblyLoaded";
            this.lbAssemblyLoaded.Size = new System.Drawing.Size(16, 13);
            this.lbAssemblyLoaded.TabIndex = 76;
            this.lbAssemblyLoaded.Text = "...";
            // 
            // btCreateCir
            // 
            this.btCreateCir.Location = new System.Drawing.Point(292, 268);
            this.btCreateCir.Name = "btCreateCir";
            this.btCreateCir.Size = new System.Drawing.Size(141, 23);
            this.btCreateCir.TabIndex = 78;
            this.btCreateCir.Text = "Create CirDump File";
            this.btCreateCir.UseVisualStyleBackColor = true;
            this.btCreateCir.Click += new System.EventHandler(this.btCreateCir_Click);
            // 
            // tbCirDumpCreate_filter
            // 
            this.tbCirDumpCreate_filter.Location = new System.Drawing.Point(507, 269);
            this.tbCirDumpCreate_filter.Name = "tbCirDumpCreate_filter";
            this.tbCirDumpCreate_filter.Size = new System.Drawing.Size(135, 20);
            this.tbCirDumpCreate_filter.TabIndex = 79;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(444, 272);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 80;
            this.label5.Text = "Class Filter";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(650, 273);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 13);
            this.label6.TabIndex = 82;
            this.label6.Text = "Tcp port of remote F1 viewer";
            // 
            // tbTcpPortRemoteF1
            // 
            this.tbTcpPortRemoteF1.Location = new System.Drawing.Point(799, 269);
            this.tbTcpPortRemoteF1.Name = "tbTcpPortRemoteF1";
            this.tbTcpPortRemoteF1.Size = new System.Drawing.Size(64, 20);
            this.tbTcpPortRemoteF1.TabIndex = 81;
            // 
            // cbLoadCreatedCirFileIn_LoadInExtrenalF1Viewer
            // 
            this.cbLoadCreatedCirFileIn_LoadInExtrenalF1Viewer.AutoSize = true;
            this.cbLoadCreatedCirFileIn_LoadInExtrenalF1Viewer.Checked = true;
            this.cbLoadCreatedCirFileIn_LoadInExtrenalF1Viewer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLoadCreatedCirFileIn_LoadInExtrenalF1Viewer.Location = new System.Drawing.Point(869, 272);
            this.cbLoadCreatedCirFileIn_LoadInExtrenalF1Viewer.Name = "cbLoadCreatedCirFileIn_LoadInExtrenalF1Viewer";
            this.cbLoadCreatedCirFileIn_LoadInExtrenalF1Viewer.Size = new System.Drawing.Size(151, 17);
            this.cbLoadCreatedCirFileIn_LoadInExtrenalF1Viewer.TabIndex = 84;
            this.cbLoadCreatedCirFileIn_LoadInExtrenalF1Viewer.Text = "Load in external F1 Viewer";
            this.cbLoadCreatedCirFileIn_LoadInExtrenalF1Viewer.UseVisualStyleBackColor = true;
            // 
            // btCreateF1CirData
            // 
            this.btCreateF1CirData.Location = new System.Drawing.Point(14, 90);
            this.btCreateF1CirData.Name = "btCreateF1CirData";
            this.btCreateF1CirData.Size = new System.Drawing.Size(166, 38);
            this.btCreateF1CirData.TabIndex = 85;
            this.btCreateF1CirData.Text = "Create F1CirData from CirDumps";
            this.btCreateF1CirData.UseVisualStyleBackColor = true;
            // 
            // cbAddAllAssembliesFromSelectedDirectory_RecursiveSearch
            // 
            this.cbAddAllAssembliesFromSelectedDirectory_RecursiveSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbAddAllAssembliesFromSelectedDirectory_RecursiveSearch.AutoSize = true;
            this.cbAddAllAssembliesFromSelectedDirectory_RecursiveSearch.Enabled = false;
            this.cbAddAllAssembliesFromSelectedDirectory_RecursiveSearch.Location = new System.Drawing.Point(11, 270);
            this.cbAddAllAssembliesFromSelectedDirectory_RecursiveSearch.Name = "cbAddAllAssembliesFromSelectedDirectory_RecursiveSearch";
            this.cbAddAllAssembliesFromSelectedDirectory_RecursiveSearch.Size = new System.Drawing.Size(111, 17);
            this.cbAddAllAssembliesFromSelectedDirectory_RecursiveSearch.TabIndex = 86;
            this.cbAddAllAssembliesFromSelectedDirectory_RecursiveSearch.Text = "Recursive Search";
            this.cbAddAllAssembliesFromSelectedDirectory_RecursiveSearch.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btAddSelectedDirectory);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btCreateCirDumpFile);
            this.groupBox1.Controls.Add(this.lbCreatedFiles_CirDump);
            this.groupBox1.Controls.Add(this.ad_DirectoryToSaveCreatedFiles);
            this.groupBox1.Controls.Add(this.cbAddAllAssembliesFromSelectedDirectory_RecursiveSearch);
            this.groupBox1.Controls.Add(this.btClearTargetsList);
            this.groupBox1.Controls.Add(this.cbAddAllAssembliesFromSelectedDirectory);
            this.groupBox1.Controls.Add(this.lbTargetDotNetAssesmblies);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(292, 297);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(955, 324);
            this.groupBox1.TabIndex = 87;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "F1CirData Creator Wizard";
            // 
            // btAddSelectedDirectory
            // 
            this.btAddSelectedDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btAddSelectedDirectory.Location = new System.Drawing.Point(6, 293);
            this.btAddSelectedDirectory.Name = "btAddSelectedDirectory";
            this.btAddSelectedDirectory.Size = new System.Drawing.Size(151, 23);
            this.btAddSelectedDirectory.TabIndex = 98;
            this.btAddSelectedDirectory.Text = "Add Selected Directory";
            this.btAddSelectedDirectory.UseVisualStyleBackColor = true;
            this.btAddSelectedDirectory.Click += new System.EventHandler(this.btAddSelectedDirectory_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(690, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(147, 13);
            this.label9.TabIndex = 90;
            this.label9.Text = "Directory to save created files";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.btCreateF1CirData);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Location = new System.Drawing.Point(507, 172);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(203, 188);
            this.groupBox2.TabIndex = 97;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "F1 Add-on Connector";
            this.groupBox2.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(75, 54);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 96;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 31);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 93;
            this.label10.Text = "IP Address";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 57);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(26, 13);
            this.label11.TabIndex = 95;
            this.label11.Text = "Port";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(75, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 94;
            this.textBox1.Text = "127.0.0.1";
            // 
            // btCreateCirDumpFile
            // 
            this.btCreateCirDumpFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCreateCirDumpFile.Location = new System.Drawing.Point(505, 36);
            this.btCreateCirDumpFile.Name = "btCreateCirDumpFile";
            this.btCreateCirDumpFile.Size = new System.Drawing.Size(150, 59);
            this.btCreateCirDumpFile.TabIndex = 92;
            this.btCreateCirDumpFile.Text = "Create CirDump Xml files";
            this.btCreateCirDumpFile.UseVisualStyleBackColor = true;
            this.btCreateCirDumpFile.Click += new System.EventHandler(this.btCreateCirDumpFile_Click);
            // 
            // lbCreatedFiles_CirDump
            // 
            this.lbCreatedFiles_CirDump.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lbCreatedFiles_CirDump.FormattingEnabled = true;
            this.lbCreatedFiles_CirDump.Location = new System.Drawing.Point(261, 252);
            this.lbCreatedFiles_CirDump.Name = "lbCreatedFiles_CirDump";
            this.lbCreatedFiles_CirDump.Size = new System.Drawing.Size(216, 43);
            this.lbCreatedFiles_CirDump.TabIndex = 91;
            this.lbCreatedFiles_CirDump.Visible = false;
            // 
            // ad_DirectoryToSaveCreatedFiles
            // 
            this.ad_DirectoryToSaveCreatedFiles._ProcessDroppedObjects = true;
            this.ad_DirectoryToSaveCreatedFiles._ShowFileSize = false;
            this.ad_DirectoryToSaveCreatedFiles._ShowLinkToUpperFolder = true;
            this.ad_DirectoryToSaveCreatedFiles._ViewMode = O2.Views.ASCX.CoreControls.ascx_Directory.ViewMode.Advanced;
            this.ad_DirectoryToSaveCreatedFiles._WatchFolder = false;
            this.ad_DirectoryToSaveCreatedFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ad_DirectoryToSaveCreatedFiles.BackColor = System.Drawing.Color.Black;
            this.ad_DirectoryToSaveCreatedFiles.ForeColor = System.Drawing.Color.White;
            this.ad_DirectoryToSaveCreatedFiles.Location = new System.Drawing.Point(694, 17);
            this.ad_DirectoryToSaveCreatedFiles.Name = "ad_DirectoryToSaveCreatedFiles";
            this.ad_DirectoryToSaveCreatedFiles.Size = new System.Drawing.Size(246, 299);
            this.ad_DirectoryToSaveCreatedFiles.TabIndex = 89;
            // 
            // btClearTargetsList
            // 
            this.btClearTargetsList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btClearTargetsList.Location = new System.Drawing.Point(174, 293);
            this.btClearTargetsList.Name = "btClearTargetsList";
            this.btClearTargetsList.Size = new System.Drawing.Size(122, 23);
            this.btClearTargetsList.TabIndex = 3;
            this.btClearTargetsList.Text = "Clear Targets List";
            this.btClearTargetsList.UseVisualStyleBackColor = true;
            this.btClearTargetsList.Click += new System.EventHandler(this.btClearTargetsList_Click);
            // 
            // cbAddAllAssembliesFromSelectedDirectory
            // 
            this.cbAddAllAssembliesFromSelectedDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbAddAllAssembliesFromSelectedDirectory.AutoSize = true;
            this.cbAddAllAssembliesFromSelectedDirectory.Location = new System.Drawing.Point(11, 252);
            this.cbAddAllAssembliesFromSelectedDirectory.Name = "cbAddAllAssembliesFromSelectedDirectory";
            this.cbAddAllAssembliesFromSelectedDirectory.Size = new System.Drawing.Size(227, 17);
            this.cbAddAllAssembliesFromSelectedDirectory.TabIndex = 2;
            this.cbAddAllAssembliesFromSelectedDirectory.Text = "Add All Assemblies from Selected Directory";
            this.cbAddAllAssembliesFromSelectedDirectory.UseVisualStyleBackColor = true;
            this.cbAddAllAssembliesFromSelectedDirectory.CheckedChanged += new System.EventHandler(this.cbAddAllAssembliesFromSelectedDirectory_CheckedChanged);
            // 
            // lbTargetDotNetAssesmblies
            // 
            this.lbTargetDotNetAssesmblies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTargetDotNetAssesmblies.FormattingEnabled = true;
            this.lbTargetDotNetAssesmblies.Location = new System.Drawing.Point(11, 36);
            this.lbTargetDotNetAssesmblies.Name = "lbTargetDotNetAssesmblies";
            this.lbTargetDotNetAssesmblies.Size = new System.Drawing.Size(485, 199);
            this.lbTargetDotNetAssesmblies.TabIndex = 1;
            this.lbTargetDotNetAssesmblies.SelectedIndexChanged += new System.EventHandler(this.lbTargetDotNetAssesmblies_SelectedIndexChanged);
            this.lbTargetDotNetAssesmblies.DoubleClick += new System.EventHandler(this.lbTargetDotNetAssesmblies_DoubleClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Target .NET assesmblies";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(176, 13);
            this.label8.TabIndex = 88;
            this.label8.Text = "Choose File or Directory To Process";
            // 
            // ascx_DropObject1
            // 
            this.ascx_DropObject1.AllowDrop = true;
            this.ascx_DropObject1.BackColor = System.Drawing.Color.Maroon;
            this.ascx_DropObject1.ForeColor = System.Drawing.Color.White;
            this.ascx_DropObject1.Location = new System.Drawing.Point(4, 3);
            this.ascx_DropObject1.Name = "ascx_DropObject1";
            this.ascx_DropObject1.Size = new System.Drawing.Size(200, 21);
            this.ascx_DropObject1.TabIndex = 65;
            this.ascx_DropObject1.Text = "Drop Content Here!!";
            this.ascx_DropObject1.eDnDAction_ObjectDataReceived_Event += new O2.Kernel.CodeUtils.Callbacks.dMethod_Object(this.ascx_DropObject1_eDnDAction_ObjectDataReceived_Event);
            // 
            // ad_Directory
            // 
            this.ad_Directory._ProcessDroppedObjects = true;
            this.ad_Directory._ShowFileSize = false;
            this.ad_Directory._ShowLinkToUpperFolder = true;
            this.ad_Directory._ViewMode = O2.Views.ASCX.CoreControls.ascx_Directory.ViewMode.Advanced;
            this.ad_Directory._WatchFolder = false;
            this.ad_Directory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.ad_Directory.BackColor = System.Drawing.Color.Black;
            this.ad_Directory.ForeColor = System.Drawing.Color.White;
            this.ad_Directory.Location = new System.Drawing.Point(4, 72);
            this.ad_Directory.Name = "ad_Directory";
            this.ad_Directory.Size = new System.Drawing.Size(282, 435);
            this.ad_Directory.TabIndex = 0;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(4, 510);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(211, 13);
            this.label12.TabIndex = 89;
            this.label12.Text = "Also try to load referenced Assemblies from:";
            // 
            // tbPathsToSearchForReferencedAssemblies
            // 
            this.tbPathsToSearchForReferencedAssemblies.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbPathsToSearchForReferencedAssemblies.Location = new System.Drawing.Point(7, 527);
            this.tbPathsToSearchForReferencedAssemblies.Multiline = true;
            this.tbPathsToSearchForReferencedAssemblies.Name = "tbPathsToSearchForReferencedAssemblies";
            this.tbPathsToSearchForReferencedAssemblies.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbPathsToSearchForReferencedAssemblies.Size = new System.Drawing.Size(279, 88);
            this.tbPathsToSearchForReferencedAssemblies.TabIndex = 90;
            this.tbPathsToSearchForReferencedAssemblies.WordWrap = false;
            // 
            // ascx_DotNet_CirCreator_OLD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.tbPathsToSearchForReferencedAssemblies);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbLoadCreatedCirFileIn_LoadInExtrenalF1Viewer);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbTcpPortRemoteF1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbCirDumpCreate_filter);
            this.Controls.Add(this.btCreateCir);
            this.Controls.Add(this.lbAssemblyLoaded);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ascx_DropObject1);
            this.Controls.Add(this.ad_Directory);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ascx_DotNet_CirCreator_OLD";
            this.Size = new System.Drawing.Size(1265, 624);
            this.Load += new System.EventHandler(this.ascx_DotNet_CirCreator_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            this.splitContainer4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ascx_Directory ad_Directory;
        private ascx_DropObject ascx_DropObject1;
        private System.Windows.Forms.ListBox lbTypes;
        private System.Windows.Forms.ListBox lbFunctions;
        private System.Windows.Forms.ListBox lbFunctionsCalled;
        private System.Windows.Forms.ListBox lbVariables;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Label lbAssemblyLoaded;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Label lbOpCodes;
        private System.Windows.Forms.ListBox tbOpCodes;
        private System.Windows.Forms.Button btCreateCir;
        private System.Windows.Forms.TextBox tbCirDumpCreate_filter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbTcpPortRemoteF1;
        private System.Windows.Forms.CheckBox cbLoadCreatedCirFileIn_LoadInExtrenalF1Viewer;
        private System.Windows.Forms.Button btCreateF1CirData;
        private System.Windows.Forms.CheckBox cbAddAllAssembliesFromSelectedDirectory_RecursiveSearch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbAddAllAssembliesFromSelectedDirectory;
        private System.Windows.Forms.ListBox lbTargetDotNetAssesmblies;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btClearTargetsList;
        private System.Windows.Forms.Label label9;
        private ascx_Directory ad_DirectoryToSaveCreatedFiles;
        private System.Windows.Forms.Button btCreateCirDumpFile;
        private System.Windows.Forms.ListBox lbCreatedFiles_CirDump;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbPathsToSearchForReferencedAssemblies;
        private System.Windows.Forms.Button btAddSelectedDirectory;
    }
}
