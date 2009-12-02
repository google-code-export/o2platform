// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
namespace O2.Debugger.Mdbg.O2Debugger
{
    partial class ascx_O2MdbgShell
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ascx_O2MdbgShell));
            this.cbShowInternalMDbgErrors = new System.Windows.Forms.CheckBox();
            this.cbShowCommandExecutionMessage = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.laActiveStatus = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.llRefreshButtonsState = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.laDebuggedProcessName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.laRunningStatus = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.startOrAttachToProcess = new System.Windows.Forms.ToolStripButton();
            this.debugggedProcessInfo = new System.Windows.Forms.ToolStripButton();
            this.breakpoints = new System.Windows.Forms.ToolStripButton();
            this.breakpointCreator = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsbContinue = new System.Windows.Forms.ToolStripButton();
            this.tsbBreak = new System.Windows.Forms.ToolStripButton();
            this.showCurrentLocation = new System.Windows.Forms.ToolStripButton();
            this.tsbDetach = new System.Windows.Forms.ToolStripButton();
            this.tspStopProcess = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tsbStepInto = new System.Windows.Forms.ToolStripButton();
            this.tsbStepOver = new System.Windows.Forms.ToolStripButton();
            this.tsbStepOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsbStepIntoAnimated = new System.Windows.Forms.ToolStripButton();
            this.tsbStepOverAnimated = new System.Windows.Forms.ToolStripButton();
            this.tsbStepOutAnimated = new System.Windows.Forms.ToolStripButton();
            this.tsbStopAnimationAndContinue = new System.Windows.Forms.ToolStripButton();
            this.tbCommands = new System.Windows.Forms.TextBox();
            this.tbMDbgOutput = new System.Windows.Forms.TextBox();
            this.llClear = new System.Windows.Forms.LinkLabel();
            this.llHelp = new System.Windows.Forms.LinkLabel();
            this.btExecuteCommand = new System.Windows.Forms.Button();
            this.llResetOnCommandEvent = new System.Windows.Forms.LinkLabel();
            this.groupBox3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbShowInternalMDbgErrors
            // 
            this.cbShowInternalMDbgErrors.AutoSize = true;
            this.cbShowInternalMDbgErrors.Location = new System.Drawing.Point(7, 37);
            this.cbShowInternalMDbgErrors.Name = "cbShowInternalMDbgErrors";
            this.cbShowInternalMDbgErrors.Size = new System.Drawing.Size(152, 17);
            this.cbShowInternalMDbgErrors.TabIndex = 24;
            this.cbShowInternalMDbgErrors.Text = "Show Internal MDbg errors";
            this.cbShowInternalMDbgErrors.UseVisualStyleBackColor = true;
            this.cbShowInternalMDbgErrors.CheckedChanged += new System.EventHandler(this.cbShowInternalMDbgErrors_CheckedChanged);
            // 
            // cbShowCommandExecutionMessage
            // 
            this.cbShowCommandExecutionMessage.AutoSize = true;
            this.cbShowCommandExecutionMessage.Checked = true;
            this.cbShowCommandExecutionMessage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowCommandExecutionMessage.Location = new System.Drawing.Point(165, 37);
            this.cbShowCommandExecutionMessage.Name = "cbShowCommandExecutionMessage";
            this.cbShowCommandExecutionMessage.Size = new System.Drawing.Size(199, 17);
            this.cbShowCommandExecutionMessage.TabIndex = 25;
            this.cbShowCommandExecutionMessage.Text = "Show Command Execution Message";
            this.cbShowCommandExecutionMessage.UseVisualStyleBackColor = true;
            this.cbShowCommandExecutionMessage.CheckedChanged += new System.EventHandler(this.cbShowCommandExecutionMessage_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(386, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Active";
            // 
            // laActiveStatus
            // 
            this.laActiveStatus.AutoSize = true;
            this.laActiveStatus.Location = new System.Drawing.Point(442, 40);
            this.laActiveStatus.Name = "laActiveStatus";
            this.laActiveStatus.Size = new System.Drawing.Size(16, 13);
            this.laActiveStatus.TabIndex = 23;
            this.laActiveStatus.Text = "...";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.llResetOnCommandEvent);
            this.groupBox3.Controls.Add(this.llRefreshButtonsState);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.laDebuggedProcessName);
            this.groupBox3.Controls.Add(this.cbShowCommandExecutionMessage);
            this.groupBox3.Controls.Add(this.cbShowInternalMDbgErrors);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.laRunningStatus);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.laActiveStatus);
            this.groupBox3.Location = new System.Drawing.Point(4, 37);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(745, 66);
            this.groupBox3.TabIndex = 38;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Current Debugging Status";
            // 
            // llRefreshButtonsState
            // 
            this.llRefreshButtonsState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llRefreshButtonsState.Location = new System.Drawing.Point(651, 20);
            this.llRefreshButtonsState.Name = "llRefreshButtonsState";
            this.llRefreshButtonsState.Size = new System.Drawing.Size(88, 33);
            this.llRefreshButtonsState.TabIndex = 32;
            this.llRefreshButtonsState.TabStop = true;
            this.llRefreshButtonsState.Text = "Refresh Buttons enable state";
            this.llRefreshButtonsState.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llRefreshButtonsState_LinkClicked);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Debugging Process:";
            // 
            // laDebuggedProcessName
            // 
            this.laDebuggedProcessName.AutoSize = true;
            this.laDebuggedProcessName.Location = new System.Drawing.Point(125, 60);
            this.laDebuggedProcessName.Name = "laDebuggedProcessName";
            this.laDebuggedProcessName.Size = new System.Drawing.Size(16, 13);
            this.laDebuggedProcessName.TabIndex = 1;
            this.laDebuggedProcessName.Text = "...";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(386, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "Running:";
            // 
            // laRunningStatus
            // 
            this.laRunningStatus.AutoSize = true;
            this.laRunningStatus.Location = new System.Drawing.Point(442, 21);
            this.laRunningStatus.Name = "laRunningStatus";
            this.laRunningStatus.Size = new System.Drawing.Size(16, 13);
            this.laRunningStatus.TabIndex = 28;
            this.laRunningStatus.Text = "...";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel4,
            this.startOrAttachToProcess,
            this.debugggedProcessInfo,
            this.breakpoints,
            this.breakpointCreator,
            this.toolStripLabel1,
            this.tsbContinue,
            this.tsbBreak,
            this.showCurrentLocation,
            this.tsbDetach,
            this.tspStopProcess,
            this.toolStripLabel3,
            this.tsbStepInto,
            this.tsbStepOver,
            this.tsbStepOut,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.tsbStepIntoAnimated,
            this.tsbStepOverAnimated,
            this.tsbStepOutAnimated,
            this.tsbStopAnimationAndContinue});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(752, 25);
            this.toolStrip1.TabIndex = 39;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(68, 22);
            this.toolStripLabel4.Text = "Debug tools:";
            // 
            // startOrAttachToProcess
            // 
            this.startOrAttachToProcess.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.startOrAttachToProcess.Image = ((System.Drawing.Image)(resources.GetObject("startOrAttachToProcess.Image")));
            this.startOrAttachToProcess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startOrAttachToProcess.Name = "startOrAttachToProcess";
            this.startOrAttachToProcess.Size = new System.Drawing.Size(23, 22);
            this.startOrAttachToProcess.Text = "Start Or Attach To Process";
            this.startOrAttachToProcess.Click += new System.EventHandler(this.startOrAttachToProcess_Click);
            // 
            // debugggedProcessInfo
            // 
            this.debugggedProcessInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.debugggedProcessInfo.Image = ((System.Drawing.Image)(resources.GetObject("debugggedProcessInfo.Image")));
            this.debugggedProcessInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.debugggedProcessInfo.Name = "debugggedProcessInfo";
            this.debugggedProcessInfo.Size = new System.Drawing.Size(23, 22);
            this.debugggedProcessInfo.Text = "Debugged Process Info";
            this.debugggedProcessInfo.Click += new System.EventHandler(this.debugggedProcessInfo_Click);
            // 
            // breakpoints
            // 
            this.breakpoints.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.breakpoints.Image = ((System.Drawing.Image)(resources.GetObject("breakpoints.Image")));
            this.breakpoints.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.breakpoints.Name = "breakpoints";
            this.breakpoints.Size = new System.Drawing.Size(23, 22);
            this.breakpoints.Text = "breakpoints";
            this.breakpoints.Click += new System.EventHandler(this.breakpoints_Click);
            // 
            // breakpointCreator
            // 
            this.breakpointCreator.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.breakpointCreator.Image = ((System.Drawing.Image)(resources.GetObject("breakpointCreator.Image")));
            this.breakpointCreator.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.breakpointCreator.Name = "breakpointCreator";
            this.breakpointCreator.Size = new System.Drawing.Size(23, 22);
            this.breakpointCreator.Text = "Breakpoint Creator";
            this.breakpointCreator.Click += new System.EventHandler(this.breakpointCreator_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(98, 22);
            this.toolStripLabel1.Text = "Process Execution:";
            // 
            // tsbContinue
            // 
            this.tsbContinue.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbContinue.Image = ((System.Drawing.Image)(resources.GetObject("tsbContinue.Image")));
            this.tsbContinue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbContinue.Name = "tsbContinue";
            this.tsbContinue.Size = new System.Drawing.Size(23, 22);
            this.tsbContinue.Text = "toolStripButton1";
            this.tsbContinue.Click += new System.EventHandler(this.tsbContinue_Click);
            // 
            // tsbBreak
            // 
            this.tsbBreak.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBreak.Image = ((System.Drawing.Image)(resources.GetObject("tsbBreak.Image")));
            this.tsbBreak.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBreak.Name = "tsbBreak";
            this.tsbBreak.Size = new System.Drawing.Size(23, 22);
            this.tsbBreak.Text = "toolStripButton2";
            this.tsbBreak.Click += new System.EventHandler(this.tsbBreak_Click);
            // 
            // showCurrentLocation
            // 
            this.showCurrentLocation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showCurrentLocation.Image = ((System.Drawing.Image)(resources.GetObject("showCurrentLocation.Image")));
            this.showCurrentLocation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showCurrentLocation.Name = "showCurrentLocation";
            this.showCurrentLocation.Size = new System.Drawing.Size(23, 22);
            this.showCurrentLocation.Text = "Show Current Location";
            this.showCurrentLocation.Click += new System.EventHandler(this.showCurrentLocation_Click);
            // 
            // tsbDetach
            // 
            this.tsbDetach.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDetach.Image = ((System.Drawing.Image)(resources.GetObject("tsbDetach.Image")));
            this.tsbDetach.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDetach.Name = "tsbDetach";
            this.tsbDetach.Size = new System.Drawing.Size(23, 22);
            this.tsbDetach.Text = "Detach from process";
            this.tsbDetach.Click += new System.EventHandler(this.tsbDetach_Click);
            // 
            // tspStopProcess
            // 
            this.tspStopProcess.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tspStopProcess.Image = ((System.Drawing.Image)(resources.GetObject("tspStopProcess.Image")));
            this.tspStopProcess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspStopProcess.Name = "tspStopProcess";
            this.tspStopProcess.Size = new System.Drawing.Size(23, 22);
            this.tspStopProcess.Text = "Stop Process";
            this.tspStopProcess.Click += new System.EventHandler(this.tspStopProcess_Click);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(82, 22);
            this.toolStripLabel3.Text = "Normal Tracing:";
            // 
            // tsbStepInto
            // 
            this.tsbStepInto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStepInto.Image = ((System.Drawing.Image)(resources.GetObject("tsbStepInto.Image")));
            this.tsbStepInto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStepInto.Name = "tsbStepInto";
            this.tsbStepInto.Size = new System.Drawing.Size(23, 22);
            this.tsbStepInto.Text = "Step Into";
            this.tsbStepInto.Click += new System.EventHandler(this.tsbStepInto_Click);
            // 
            // tsbStepOver
            // 
            this.tsbStepOver.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStepOver.Image = ((System.Drawing.Image)(resources.GetObject("tsbStepOver.Image")));
            this.tsbStepOver.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStepOver.Name = "tsbStepOver";
            this.tsbStepOver.Size = new System.Drawing.Size(23, 22);
            this.tsbStepOver.Text = "Step Over";
            this.tsbStepOver.Click += new System.EventHandler(this.tsbStepOver_Click);
            // 
            // tsbStepOut
            // 
            this.tsbStepOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStepOut.Image = ((System.Drawing.Image)(resources.GetObject("tsbStepOut.Image")));
            this.tsbStepOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStepOut.Name = "tsbStepOut";
            this.tsbStepOut.Size = new System.Drawing.Size(23, 22);
            this.tsbStepOut.Text = "Step Out";
            this.tsbStepOut.Click += new System.EventHandler(this.tsbStepOut_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(94, 22);
            this.toolStripLabel2.Text = "Animated Tracing:";
            // 
            // tsbStepIntoAnimated
            // 
            this.tsbStepIntoAnimated.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStepIntoAnimated.Image = ((System.Drawing.Image)(resources.GetObject("tsbStepIntoAnimated.Image")));
            this.tsbStepIntoAnimated.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStepIntoAnimated.Name = "tsbStepIntoAnimated";
            this.tsbStepIntoAnimated.Size = new System.Drawing.Size(23, 22);
            this.tsbStepIntoAnimated.Text = "Step Into Animated";
            this.tsbStepIntoAnimated.Click += new System.EventHandler(this.tsbStepIntoAnimated_Click);
            // 
            // tsbStepOverAnimated
            // 
            this.tsbStepOverAnimated.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStepOverAnimated.Image = ((System.Drawing.Image)(resources.GetObject("tsbStepOverAnimated.Image")));
            this.tsbStepOverAnimated.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStepOverAnimated.Name = "tsbStepOverAnimated";
            this.tsbStepOverAnimated.Size = new System.Drawing.Size(23, 22);
            this.tsbStepOverAnimated.Text = "Step Over Animated";
            this.tsbStepOverAnimated.Click += new System.EventHandler(this.tsbStepOverAnimated_Click);
            // 
            // tsbStepOutAnimated
            // 
            this.tsbStepOutAnimated.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStepOutAnimated.Image = ((System.Drawing.Image)(resources.GetObject("tsbStepOutAnimated.Image")));
            this.tsbStepOutAnimated.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStepOutAnimated.Name = "tsbStepOutAnimated";
            this.tsbStepOutAnimated.Size = new System.Drawing.Size(23, 22);
            this.tsbStepOutAnimated.Text = "Step Out Animated";
            this.tsbStepOutAnimated.Click += new System.EventHandler(this.tsbStepOutAnimated_Click);
            // 
            // tsbStopAnimationAndContinue
            // 
            this.tsbStopAnimationAndContinue.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStopAnimationAndContinue.Image = ((System.Drawing.Image)(resources.GetObject("tsbStopAnimationAndContinue.Image")));
            this.tsbStopAnimationAndContinue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStopAnimationAndContinue.Name = "tsbStopAnimationAndContinue";
            this.tsbStopAnimationAndContinue.Size = new System.Drawing.Size(23, 22);
            this.tsbStopAnimationAndContinue.Text = "Stop Animation && continue";
            this.tsbStopAnimationAndContinue.Click += new System.EventHandler(this.tsbStopAnimationAndContinue_Click);
            // 
            // tbCommands
            // 
            this.tbCommands.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCommands.Location = new System.Drawing.Point(4, 113);
            this.tbCommands.Name = "tbCommands";
            this.tbCommands.Size = new System.Drawing.Size(679, 20);
            this.tbCommands.TabIndex = 14;
            this.tbCommands.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCommands_KeyPress);
            // 
            // tbMDbgOutput
            // 
            this.tbMDbgOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMDbgOutput.Location = new System.Drawing.Point(4, 152);
            this.tbMDbgOutput.Multiline = true;
            this.tbMDbgOutput.Name = "tbMDbgOutput";
            this.tbMDbgOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbMDbgOutput.Size = new System.Drawing.Size(745, 284);
            this.tbMDbgOutput.TabIndex = 13;
            this.tbMDbgOutput.WordWrap = false;
            // 
            // llClear
            // 
            this.llClear.AutoSize = true;
            this.llClear.Location = new System.Drawing.Point(2, 136);
            this.llClear.Name = "llClear";
            this.llClear.Size = new System.Drawing.Size(31, 13);
            this.llClear.TabIndex = 16;
            this.llClear.TabStop = true;
            this.llClear.Text = "Clear";
            this.llClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llClear_LinkClicked);
            // 
            // llHelp
            // 
            this.llHelp.AutoSize = true;
            this.llHelp.Location = new System.Drawing.Point(39, 136);
            this.llHelp.Name = "llHelp";
            this.llHelp.Size = new System.Drawing.Size(29, 13);
            this.llHelp.TabIndex = 18;
            this.llHelp.TabStop = true;
            this.llHelp.Text = "Help";
            this.llHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llHelp_LinkClicked);
            // 
            // btExecuteCommand
            // 
            this.btExecuteCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btExecuteCommand.Location = new System.Drawing.Point(689, 111);
            this.btExecuteCommand.Name = "btExecuteCommand";
            this.btExecuteCommand.Size = new System.Drawing.Size(54, 23);
            this.btExecuteCommand.TabIndex = 17;
            this.btExecuteCommand.Text = "Exec";
            this.btExecuteCommand.UseVisualStyleBackColor = true;
            this.btExecuteCommand.Click += new System.EventHandler(this.btExecuteCommand_Click);
            // 
            // llResetOnCommandEvent
            // 
            this.llResetOnCommandEvent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llResetOnCommandEvent.Location = new System.Drawing.Point(545, 21);
            this.llResetOnCommandEvent.Name = "llResetOnCommandEvent";
            this.llResetOnCommandEvent.Size = new System.Drawing.Size(100, 33);
            this.llResetOnCommandEvent.TabIndex = 33;
            this.llResetOnCommandEvent.TabStop = true;
            this.llResetOnCommandEvent.Text = "Reset OnCommand Event";
            this.llResetOnCommandEvent.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llResetOnCommandEvent_LinkClicked);
            // 
            // ascx_O2MdbgShell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbCommands);
            this.Controls.Add(this.tbMDbgOutput);
            this.Controls.Add(this.llClear);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.llHelp);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btExecuteCommand);
            this.Name = "ascx_O2MdbgShell";
            this.Size = new System.Drawing.Size(752, 439);
            this.Load += new System.EventHandler(this.ascx_O2MdbgShell_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbShowCommandExecutionMessage;
        private System.Windows.Forms.CheckBox cbShowInternalMDbgErrors;
        private System.Windows.Forms.Label laActiveStatus;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label laDebuggedProcessName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label laRunningStatus;
        private System.Windows.Forms.LinkLabel llRefreshButtonsState;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbContinue;
        private System.Windows.Forms.ToolStripButton tsbBreak;
        private System.Windows.Forms.ToolStripButton tspStopProcess;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripButton tsbStepOverAnimated;
        private System.Windows.Forms.ToolStripButton tsbStepOver;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton tsbStepInto;
        private System.Windows.Forms.ToolStripButton tsbStepOut;
        private System.Windows.Forms.ToolStripButton tsbDetach;
        private System.Windows.Forms.ToolStripButton tsbStepIntoAnimated;
        private System.Windows.Forms.ToolStripButton tsbStepOutAnimated;
        private System.Windows.Forms.TextBox tbCommands;
        private System.Windows.Forms.TextBox tbMDbgOutput;
        private System.Windows.Forms.LinkLabel llClear;
        private System.Windows.Forms.LinkLabel llHelp;
        private System.Windows.Forms.Button btExecuteCommand;
        private System.Windows.Forms.ToolStripButton tsbStopAnimationAndContinue;
        private System.Windows.Forms.ToolStripButton startOrAttachToProcess;
        private System.Windows.Forms.ToolStripButton breakpoints;
        private System.Windows.Forms.ToolStripButton debugggedProcessInfo;
        private System.Windows.Forms.ToolStripButton breakpointCreator;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripButton showCurrentLocation;
        private System.Windows.Forms.LinkLabel llResetOnCommandEvent;

    }
}
