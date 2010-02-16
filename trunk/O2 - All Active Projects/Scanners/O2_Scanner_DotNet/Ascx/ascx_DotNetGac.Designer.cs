// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Interfaces.CIR;
using O2.Interfaces.DotNet;

namespace O2.Scanner.DotNet.Ascx
{
    partial class ascx_DotNetGac
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ascx_DotNetGac));
            this.label1 = new System.Windows.Forms.Label();
            this.btLoadListOfGacAssemblies = new System.Windows.Forms.Button();
            this.btBackupGacToZipFile = new System.Windows.Forms.Button();
            this.gbSelectedGacAssembly = new System.Windows.Forms.GroupBox();
            this.btTestDllCopy = new System.Windows.Forms.Button();
            this.cbHookOnMethod = new System.Windows.Forms.CheckBox();
            this.cbHookOnType = new System.Windows.Forms.CheckBox();
            this.tbHookOn_Method = new System.Windows.Forms.TextBox();
            this.tbHookOn_Type = new System.Windows.Forms.TextBox();
            this.btUnInstallPostSharpHooks = new System.Windows.Forms.Button();
            this.btInstallPostSharpHooks = new System.Windows.Forms.Button();
            this.directoryOfSelectedAssembly = new O2.Views.ASCX.CoreControls.ascx_Directory();
            this.lbPostSharpHooksState = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbSelectedGacAssembly_fullPath = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbSelectedGacAssembly_version = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbSelectedGacAssembly_name = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbEnableBackupButton = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gacBrowser = new O2.Core.CIR.Ascx.DotNet.ascx_GAC_Browser();
            this.btUnInstallHooksOnAllFiltered = new System.Windows.Forms.Button();
            this.btInstallHooksOnAllFiltered = new System.Windows.Forms.Button();
            this.cirDataViewer = new O2.Core.CIR.Ascx.ascx_CirDataViewer();
            this.cbLoadCirDataForSelectedAssembly = new System.Windows.Forms.CheckBox();
            this.btStopIIS = new System.Windows.Forms.Button();
            this.btLoadDllsFromDirectory = new System.Windows.Forms.Button();
            this.tbDirectoryToLoadAssembliesFrom = new System.Windows.Forms.TextBox();
            this.btDeployHostAssemblyToGac = new System.Windows.Forms.Button();
            this.gbSelectedGacAssembly.SuspendLayout();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(291, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "DotNet GAC assemblies (currently only for dlls in GAC_MSIL)";
            // 
            // btLoadListOfGacAssemblies
            // 
            this.btLoadListOfGacAssemblies.Location = new System.Drawing.Point(7, 20);
            this.btLoadListOfGacAssemblies.Name = "btLoadListOfGacAssemblies";
            this.btLoadListOfGacAssemblies.Size = new System.Drawing.Size(188, 23);
            this.btLoadListOfGacAssemblies.TabIndex = 1;
            this.btLoadListOfGacAssemblies.Text = "Load list of GAC assemblies";
            this.btLoadListOfGacAssemblies.UseVisualStyleBackColor = true;
            this.btLoadListOfGacAssemblies.Click += new System.EventHandler(this.btLoadListOfGacAssemblies_Click);
            // 
            // btBackupGacToZipFile
            // 
            this.btBackupGacToZipFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btBackupGacToZipFile.Enabled = false;
            this.btBackupGacToZipFile.Location = new System.Drawing.Point(3, 23);
            this.btBackupGacToZipFile.Name = "btBackupGacToZipFile";
            this.btBackupGacToZipFile.Size = new System.Drawing.Size(146, 23);
            this.btBackupGacToZipFile.TabIndex = 3;
            this.btBackupGacToZipFile.Text = "Backup Gac to zip file";
            this.btBackupGacToZipFile.UseVisualStyleBackColor = true;
            // 
            // gbSelectedGacAssembly
            // 
            this.gbSelectedGacAssembly.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSelectedGacAssembly.Controls.Add(this.btTestDllCopy);
            this.gbSelectedGacAssembly.Controls.Add(this.cbHookOnMethod);
            this.gbSelectedGacAssembly.Controls.Add(this.cbHookOnType);
            this.gbSelectedGacAssembly.Controls.Add(this.tbHookOn_Method);
            this.gbSelectedGacAssembly.Controls.Add(this.tbHookOn_Type);
            this.gbSelectedGacAssembly.Controls.Add(this.btUnInstallPostSharpHooks);
            this.gbSelectedGacAssembly.Controls.Add(this.btInstallPostSharpHooks);
            this.gbSelectedGacAssembly.Controls.Add(this.directoryOfSelectedAssembly);
            this.gbSelectedGacAssembly.Controls.Add(this.lbPostSharpHooksState);
            this.gbSelectedGacAssembly.Controls.Add(this.label3);
            this.gbSelectedGacAssembly.Controls.Add(this.lbSelectedGacAssembly_fullPath);
            this.gbSelectedGacAssembly.Controls.Add(this.label6);
            this.gbSelectedGacAssembly.Controls.Add(this.lbSelectedGacAssembly_version);
            this.gbSelectedGacAssembly.Controls.Add(this.label4);
            this.gbSelectedGacAssembly.Controls.Add(this.lbSelectedGacAssembly_name);
            this.gbSelectedGacAssembly.Controls.Add(this.label2);
            this.gbSelectedGacAssembly.Location = new System.Drawing.Point(447, 50);
            this.gbSelectedGacAssembly.Name = "gbSelectedGacAssembly";
            this.gbSelectedGacAssembly.Size = new System.Drawing.Size(240, 329);
            this.gbSelectedGacAssembly.TabIndex = 4;
            this.gbSelectedGacAssembly.TabStop = false;
            this.gbSelectedGacAssembly.Text = "Selected Gac Assembly";
            // 
            // btTestDllCopy
            // 
            this.btTestDllCopy.Location = new System.Drawing.Point(127, 197);
            this.btTestDllCopy.Name = "btTestDllCopy";
            this.btTestDllCopy.Size = new System.Drawing.Size(91, 23);
            this.btTestDllCopy.TabIndex = 17;
            this.btTestDllCopy.Text = "Test Dll Copy";
            this.btTestDllCopy.UseVisualStyleBackColor = true;
            this.btTestDllCopy.Click += new System.EventHandler(this.btTestDllCopy_Click);
            // 
            // cbHookOnMethod
            // 
            this.cbHookOnMethod.AutoSize = true;
            this.cbHookOnMethod.Location = new System.Drawing.Point(10, 247);
            this.cbHookOnMethod.Name = "cbHookOnMethod";
            this.cbHookOnMethod.Size = new System.Drawing.Size(91, 17);
            this.cbHookOnMethod.TabIndex = 16;
            this.cbHookOnMethod.Text = "Hook Method";
            this.cbHookOnMethod.UseVisualStyleBackColor = true;
            this.cbHookOnMethod.CheckedChanged += new System.EventHandler(this.cbHookOnMethod_CheckedChanged);
            // 
            // cbHookOnType
            // 
            this.cbHookOnType.AutoSize = true;
            this.cbHookOnType.Location = new System.Drawing.Point(10, 228);
            this.cbHookOnType.Name = "cbHookOnType";
            this.cbHookOnType.Size = new System.Drawing.Size(79, 17);
            this.cbHookOnType.TabIndex = 15;
            this.cbHookOnType.Text = "Hook Type";
            this.cbHookOnType.UseVisualStyleBackColor = true;
            // 
            // tbHookOn_Method
            // 
            this.tbHookOn_Method.Location = new System.Drawing.Point(104, 244);
            this.tbHookOn_Method.Name = "tbHookOn_Method";
            this.tbHookOn_Method.Size = new System.Drawing.Size(129, 20);
            this.tbHookOn_Method.TabIndex = 14;
            this.tbHookOn_Method.TextChanged += new System.EventHandler(this.tbHookOn_Method_TextChanged);
            // 
            // tbHookOn_Type
            // 
            this.tbHookOn_Type.Location = new System.Drawing.Point(104, 226);
            this.tbHookOn_Type.Name = "tbHookOn_Type";
            this.tbHookOn_Type.Size = new System.Drawing.Size(129, 20);
            this.tbHookOn_Type.TabIndex = 13;
            this.tbHookOn_Type.TextChanged += new System.EventHandler(this.tbHookOn_Type_TextChanged);
            // 
            // btUnInstallPostSharpHooks
            // 
            this.btUnInstallPostSharpHooks.Enabled = false;
            this.btUnInstallPostSharpHooks.Location = new System.Drawing.Point(128, 171);
            this.btUnInstallPostSharpHooks.Name = "btUnInstallPostSharpHooks";
            this.btUnInstallPostSharpHooks.Size = new System.Drawing.Size(91, 23);
            this.btUnInstallPostSharpHooks.TabIndex = 10;
            this.btUnInstallPostSharpHooks.Text = "UnInstall Hooks";
            this.btUnInstallPostSharpHooks.UseVisualStyleBackColor = true;
            this.btUnInstallPostSharpHooks.Click += new System.EventHandler(this.btUnInstallPostSharpHooks_Click);
            // 
            // btInstallPostSharpHooks
            // 
            this.btInstallPostSharpHooks.Enabled = false;
            this.btInstallPostSharpHooks.Location = new System.Drawing.Point(128, 146);
            this.btInstallPostSharpHooks.Name = "btInstallPostSharpHooks";
            this.btInstallPostSharpHooks.Size = new System.Drawing.Size(91, 23);
            this.btInstallPostSharpHooks.TabIndex = 9;
            this.btInstallPostSharpHooks.Text = "Install Hooks";
            this.btInstallPostSharpHooks.UseVisualStyleBackColor = true;
            this.btInstallPostSharpHooks.Click += new System.EventHandler(this.btInstallPostSharpHooks_Click);
            // 
            // directoryOfSelectedAssembly
            // 
            this.directoryOfSelectedAssembly._ProcessDroppedObjects = true;
            this.directoryOfSelectedAssembly._ShowFileSize = true;
            this.directoryOfSelectedAssembly._ShowLinkToUpperFolder = true;
            this.directoryOfSelectedAssembly._ViewMode = O2.Views.ASCX.CoreControls.ascx_Directory.ViewMode.Simple;
            this.directoryOfSelectedAssembly._WatchFolder = true;
            this.directoryOfSelectedAssembly.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.directoryOfSelectedAssembly.BackColor = System.Drawing.SystemColors.Control;
            this.directoryOfSelectedAssembly.ForeColor = System.Drawing.Color.Black;
            this.directoryOfSelectedAssembly.Location = new System.Drawing.Point(6, 265);
            this.directoryOfSelectedAssembly.Name = "directoryOfSelectedAssembly";
            this.directoryOfSelectedAssembly.Size = new System.Drawing.Size(227, 58);
            this.directoryOfSelectedAssembly.TabIndex = 8;
            // 
            // lbPostSharpHooksState
            // 
            this.lbPostSharpHooksState.AutoSize = true;
            this.lbPostSharpHooksState.Location = new System.Drawing.Point(101, 151);
            this.lbPostSharpHooksState.Name = "lbPostSharpHooksState";
            this.lbPostSharpHooksState.Size = new System.Drawing.Size(16, 13);
            this.lbPostSharpHooksState.TabIndex = 7;
            this.lbPostSharpHooksState.Text = "...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "PostSharp Hooks:";
            // 
            // lbSelectedGacAssembly_fullPath
            // 
            this.lbSelectedGacAssembly_fullPath.Location = new System.Drawing.Point(54, 56);
            this.lbSelectedGacAssembly_fullPath.Name = "lbSelectedGacAssembly_fullPath";
            this.lbSelectedGacAssembly_fullPath.Size = new System.Drawing.Size(180, 76);
            this.lbSelectedGacAssembly_fullPath.TabIndex = 5;
            this.lbSelectedGacAssembly_fullPath.Text = "...";
            this.lbSelectedGacAssembly_fullPath.Click += new System.EventHandler(this.lbSelectedGacAssembly_fullPath_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "full path:";
            // 
            // lbSelectedGacAssembly_version
            // 
            this.lbSelectedGacAssembly_version.AutoSize = true;
            this.lbSelectedGacAssembly_version.Location = new System.Drawing.Point(53, 39);
            this.lbSelectedGacAssembly_version.Name = "lbSelectedGacAssembly_version";
            this.lbSelectedGacAssembly_version.Size = new System.Drawing.Size(16, 13);
            this.lbSelectedGacAssembly_version.TabIndex = 3;
            this.lbSelectedGacAssembly_version.Text = "...";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "version:";
            // 
            // lbSelectedGacAssembly_name
            // 
            this.lbSelectedGacAssembly_name.AutoSize = true;
            this.lbSelectedGacAssembly_name.Location = new System.Drawing.Point(53, 20);
            this.lbSelectedGacAssembly_name.Name = "lbSelectedGacAssembly_name";
            this.lbSelectedGacAssembly_name.Size = new System.Drawing.Size(16, 13);
            this.lbSelectedGacAssembly_name.TabIndex = 1;
            this.lbSelectedGacAssembly_name.Text = "...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "name:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.cbEnableBackupButton);
            this.panel1.Controls.Add(this.btBackupGacToZipFile);
            this.panel1.Location = new System.Drawing.Point(7, 331);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 53);
            this.panel1.TabIndex = 5;
            // 
            // cbEnableBackupButton
            // 
            this.cbEnableBackupButton.AutoSize = true;
            this.cbEnableBackupButton.Location = new System.Drawing.Point(3, 4);
            this.cbEnableBackupButton.Name = "cbEnableBackupButton";
            this.cbEnableBackupButton.Size = new System.Drawing.Size(133, 17);
            this.cbEnableBackupButton.TabIndex = 4;
            this.cbEnableBackupButton.Text = "Enable Backup Button";
            this.cbEnableBackupButton.UseVisualStyleBackColor = true;
            this.cbEnableBackupButton.CheckedChanged += new System.EventHandler(this.cbEnableBackupButton_CheckedChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(7, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gacBrowser);
            this.splitContainer1.Panel1.Controls.Add(this.btUnInstallHooksOnAllFiltered);
            this.splitContainer1.Panel1.Controls.Add(this.btInstallHooksOnAllFiltered);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cirDataViewer);
            this.splitContainer1.Panel2.Controls.Add(this.cbLoadCirDataForSelectedAssembly);
            this.splitContainer1.Size = new System.Drawing.Size(434, 262);
            this.splitContainer1.SplitterDistance = 186;
            this.splitContainer1.TabIndex = 6;
            // 
            // gacBrowser
            // 
            this.gacBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gacBrowser.Location = new System.Drawing.Point(-2, -2);
            this.gacBrowser.Name = "gacBrowser";
            this.gacBrowser.Size = new System.Drawing.Size(186, 205);
            this.gacBrowser.TabIndex = 12;
            this.gacBrowser._onGacDllSelected += new O2.DotNetWrappers.DotNet.O2Thread.FuncVoidT1<IGacDll>(this.gacBrowser__onGacDllSelected);
            // 
            // btUnInstallHooksOnAllFiltered
            // 
            this.btUnInstallHooksOnAllFiltered.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btUnInstallHooksOnAllFiltered.Enabled = false;
            this.btUnInstallHooksOnAllFiltered.Location = new System.Drawing.Point(6, 235);
            this.btUnInstallHooksOnAllFiltered.Name = "btUnInstallHooksOnAllFiltered";
            this.btUnInstallHooksOnAllFiltered.Size = new System.Drawing.Size(157, 23);
            this.btUnInstallHooksOnAllFiltered.TabIndex = 11;
            this.btUnInstallHooksOnAllFiltered.Text = "UnInstall Hooks on all filtered";
            this.btUnInstallHooksOnAllFiltered.UseVisualStyleBackColor = true;
            this.btUnInstallHooksOnAllFiltered.Click += new System.EventHandler(this.btUnInstallHooksOnAllFiltered_Click);
            // 
            // btInstallHooksOnAllFiltered
            // 
            this.btInstallHooksOnAllFiltered.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btInstallHooksOnAllFiltered.Enabled = false;
            this.btInstallHooksOnAllFiltered.Location = new System.Drawing.Point(6, 209);
            this.btInstallHooksOnAllFiltered.Name = "btInstallHooksOnAllFiltered";
            this.btInstallHooksOnAllFiltered.Size = new System.Drawing.Size(157, 23);
            this.btInstallHooksOnAllFiltered.TabIndex = 10;
            this.btInstallHooksOnAllFiltered.Text = "Install Hooks on all filtered";
            this.btInstallHooksOnAllFiltered.UseVisualStyleBackColor = true;
            this.btInstallHooksOnAllFiltered.Click += new System.EventHandler(this.btInstallHooksOnAllFiltered_Click);
            // 
            // cirDataViewer
            // 
            this.cirDataViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cirDataViewer.cirDataAnalysis = ((ICirDataAnalysis)(resources.GetObject("cirDataViewer.cirDataAnalysis")));
            this.cirDataViewer.Location = new System.Drawing.Point(0, 26);
            this.cirDataViewer.Name = "cirDataViewer";
            this.cirDataViewer.Size = new System.Drawing.Size(240, 232);
            this.cirDataViewer.TabIndex = 0;
            // 
            // cbLoadCirDataForSelectedAssembly
            // 
            this.cbLoadCirDataForSelectedAssembly.AutoSize = true;
            this.cbLoadCirDataForSelectedAssembly.Checked = true;
            this.cbLoadCirDataForSelectedAssembly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLoadCirDataForSelectedAssembly.Location = new System.Drawing.Point(1, 5);
            this.cbLoadCirDataForSelectedAssembly.Name = "cbLoadCirDataForSelectedAssembly";
            this.cbLoadCirDataForSelectedAssembly.Size = new System.Drawing.Size(193, 17);
            this.cbLoadCirDataForSelectedAssembly.TabIndex = 7;
            this.cbLoadCirDataForSelectedAssembly.Text = "Load CirData for selected Assembly";
            this.cbLoadCirDataForSelectedAssembly.UseVisualStyleBackColor = true;
            // 
            // btStopIIS
            // 
            this.btStopIIS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btStopIIS.Location = new System.Drawing.Point(220, 331);
            this.btStopIIS.Name = "btStopIIS";
            this.btStopIIS.Size = new System.Drawing.Size(75, 23);
            this.btStopIIS.TabIndex = 8;
            this.btStopIIS.Text = "Stop IIS";
            this.btStopIIS.UseVisualStyleBackColor = true;
            this.btStopIIS.Click += new System.EventHandler(this.btStopIIS_Click);
            // 
            // btLoadDllsFromDirectory
            // 
            this.btLoadDllsFromDirectory.Location = new System.Drawing.Point(201, 20);
            this.btLoadDllsFromDirectory.Name = "btLoadDllsFromDirectory";
            this.btLoadDllsFromDirectory.Size = new System.Drawing.Size(179, 23);
            this.btLoadDllsFromDirectory.TabIndex = 10;
            this.btLoadDllsFromDirectory.Text = "Load Assemblies From Directory ->";
            this.btLoadDllsFromDirectory.UseVisualStyleBackColor = true;
            this.btLoadDllsFromDirectory.Click += new System.EventHandler(this.btLoadDllsFromDirectory_Click);
            // 
            // tbDirectoryToLoadAssembliesFrom
            // 
            this.tbDirectoryToLoadAssembliesFrom.Location = new System.Drawing.Point(386, 22);
            this.tbDirectoryToLoadAssembliesFrom.Name = "tbDirectoryToLoadAssembliesFrom";
            this.tbDirectoryToLoadAssembliesFrom.Size = new System.Drawing.Size(194, 20);
            this.tbDirectoryToLoadAssembliesFrom.TabIndex = 11;
            // 
            // btDeployHostAssemblyToGac
            // 
            this.btDeployHostAssemblyToGac.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btDeployHostAssemblyToGac.Location = new System.Drawing.Point(217, 358);
            this.btDeployHostAssemblyToGac.Name = "btDeployHostAssemblyToGac";
            this.btDeployHostAssemblyToGac.Size = new System.Drawing.Size(176, 23);
            this.btDeployHostAssemblyToGac.TabIndex = 12;
            this.btDeployHostAssemblyToGac.Text = "Deploy Host Assembly to GAC";
            this.btDeployHostAssemblyToGac.UseVisualStyleBackColor = true;
            this.btDeployHostAssemblyToGac.Click += new System.EventHandler(this.btDeployHostAssemblyToGac_Click);
            // 
            // ascx_DotNetGac
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btDeployHostAssemblyToGac);
            this.Controls.Add(this.tbDirectoryToLoadAssembliesFrom);
            this.Controls.Add(this.btLoadDllsFromDirectory);
            this.Controls.Add(this.btStopIIS);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gbSelectedGacAssembly);
            this.Controls.Add(this.btLoadListOfGacAssemblies);
            this.Controls.Add(this.label1);
            this.Name = "ascx_DotNetGac";
            this.Size = new System.Drawing.Size(702, 387);
            this.Load += new System.EventHandler(this.ascx_DotNetGac_Load);
            this.gbSelectedGacAssembly.ResumeLayout(false);
            this.gbSelectedGacAssembly.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btLoadListOfGacAssemblies;
        private System.Windows.Forms.Button btBackupGacToZipFile;
        private System.Windows.Forms.GroupBox gbSelectedGacAssembly;
        private System.Windows.Forms.Label lbSelectedGacAssembly_name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbSelectedGacAssembly_fullPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbSelectedGacAssembly_version;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbEnableBackupButton;
        private System.Windows.Forms.Label lbPostSharpHooksState;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private O2.Core.CIR.Ascx.ascx_CirDataViewer cirDataViewer;
        private System.Windows.Forms.CheckBox cbLoadCirDataForSelectedAssembly;
        private O2.Views.ASCX.CoreControls.ascx_Directory directoryOfSelectedAssembly;
        private System.Windows.Forms.Button btUnInstallPostSharpHooks;
        private System.Windows.Forms.Button btInstallPostSharpHooks;
        private System.Windows.Forms.Button btStopIIS;
        private System.Windows.Forms.Button btUnInstallHooksOnAllFiltered;
        private System.Windows.Forms.Button btInstallHooksOnAllFiltered;
        private System.Windows.Forms.Button btLoadDllsFromDirectory;
        private System.Windows.Forms.TextBox tbDirectoryToLoadAssembliesFrom;
        private System.Windows.Forms.Button btDeployHostAssemblyToGac;
        private System.Windows.Forms.TextBox tbHookOn_Method;
        private System.Windows.Forms.TextBox tbHookOn_Type;
        private System.Windows.Forms.CheckBox cbHookOnType;
        private System.Windows.Forms.CheckBox cbHookOnMethod;
        private System.Windows.Forms.Button btTestDllCopy;
        private O2.Core.CIR.Ascx.DotNet.ascx_GAC_Browser gacBrowser;
    }
}
