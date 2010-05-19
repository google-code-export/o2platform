namespace DesktopRecorder
{
	partial class frmRecorder
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRecorder));
			this.MediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
			this.notifyRecorder = new System.Windows.Forms.NotifyIcon(this.components);
			this.cxtRecordingOpts = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.startRecordingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pauseRecordingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stopRecordingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addAudio = new System.Windows.Forms.ToolStripMenuItem();
			this.mnutxtAudioFile = new System.Windows.Forms.ToolStripMenuItem();
			this.txtAudioFile = new System.Windows.Forms.ToolStripTextBox();
			this.saveCurrentRecordingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolstripEnableBroadcast = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
			this.txtPortNbr = new System.Windows.Forms.ToolStripTextBox();
			this.recentRecordingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.txtBroadcastURL = new System.Windows.Forms.ToolStripTextBox();
			this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showUIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tsShowStatus = new System.Windows.Forms.ToolStripMenuItem();
			this.recentRecordingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ShowUI = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveRecording = new System.Windows.Forms.SaveFileDialog();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.tmrRcCounter = new System.Windows.Forms.Timer(this.components);
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tsRecDuration = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsUsersCount = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsddlUsrDetails = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.tmrViewerCount = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.MediaPlayer)).BeginInit();
			this.cxtRecordingOpts.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// MediaPlayer
			// 
			this.MediaPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MediaPlayer.Enabled = true;
			this.MediaPlayer.Location = new System.Drawing.Point(0, 24);
			this.MediaPlayer.Name = "MediaPlayer";
			this.MediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("MediaPlayer.OcxState")));
			this.MediaPlayer.Size = new System.Drawing.Size(342, 292);
			this.MediaPlayer.TabIndex = 0;
			// 
			// notifyRecorder
			// 
			this.notifyRecorder.ContextMenuStrip = this.cxtRecordingOpts;
			this.notifyRecorder.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyRecorder.Icon")));
			this.notifyRecorder.Visible = true;
			this.notifyRecorder.DoubleClick += new System.EventHandler(this.notifyRecorder_DoubleClick);
			// 
			// cxtRecordingOpts
			// 
			this.cxtRecordingOpts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startRecordingToolStripMenuItem,
            this.pauseRecordingToolStripMenuItem,
            this.stopRecordingToolStripMenuItem,
            this.addAudio,
            this.mnutxtAudioFile,
            this.saveCurrentRecordingToolStripMenuItem,
            this.toolStripMenuItem2,
            this.recentRecordingsToolStripMenuItem1,
            this.exitToolStripMenuItem1});
			this.cxtRecordingOpts.Name = "cxtRecordingOpts";
			this.cxtRecordingOpts.Size = new System.Drawing.Size(201, 202);
			// 
			// startRecordingToolStripMenuItem
			// 
			this.startRecordingToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("startRecordingToolStripMenuItem.Image")));
			this.startRecordingToolStripMenuItem.Name = "startRecordingToolStripMenuItem";
			this.startRecordingToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
			this.startRecordingToolStripMenuItem.Text = "Start Recording";
			this.startRecordingToolStripMenuItem.Click += new System.EventHandler(this.startRecordingToolStripMenuItem_Click);
			// 
			// pauseRecordingToolStripMenuItem
			// 
			this.pauseRecordingToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pauseRecordingToolStripMenuItem.Image")));
			this.pauseRecordingToolStripMenuItem.Name = "pauseRecordingToolStripMenuItem";
			this.pauseRecordingToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
			this.pauseRecordingToolStripMenuItem.Text = "Pause Recording";
			this.pauseRecordingToolStripMenuItem.Click += new System.EventHandler(this.pauseRecordingToolStripMenuItem_Click);
			// 
			// stopRecordingToolStripMenuItem
			// 
			this.stopRecordingToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("stopRecordingToolStripMenuItem.Image")));
			this.stopRecordingToolStripMenuItem.Name = "stopRecordingToolStripMenuItem";
			this.stopRecordingToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
			this.stopRecordingToolStripMenuItem.Text = "Stop Recording";
			this.stopRecordingToolStripMenuItem.Click += new System.EventHandler(this.stopRecordingToolStripMenuItem_Click);
			// 
			// addAudio
			// 
			this.addAudio.Checked = true;
			this.addAudio.CheckState = System.Windows.Forms.CheckState.Checked;
			this.addAudio.Name = "addAudio";
			this.addAudio.Size = new System.Drawing.Size(200, 22);
			this.addAudio.Text = "Add Audio";
			this.addAudio.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
			// 
			// mnutxtAudioFile
			// 
			this.mnutxtAudioFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtAudioFile});
			this.mnutxtAudioFile.Name = "mnutxtAudioFile";
			this.mnutxtAudioFile.Size = new System.Drawing.Size(200, 22);
			this.mnutxtAudioFile.Text = "Enter Audio File Path:";
			// 
			// txtAudioFile
			// 
			this.txtAudioFile.Name = "txtAudioFile";
			this.txtAudioFile.Size = new System.Drawing.Size(100, 21);
			// 
			// saveCurrentRecordingToolStripMenuItem
			// 
			this.saveCurrentRecordingToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveCurrentRecordingToolStripMenuItem.Image")));
			this.saveCurrentRecordingToolStripMenuItem.Name = "saveCurrentRecordingToolStripMenuItem";
			this.saveCurrentRecordingToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
			this.saveCurrentRecordingToolStripMenuItem.Text = "Save Current Recording";
			this.saveCurrentRecordingToolStripMenuItem.Click += new System.EventHandler(this.saveCurrentRecordingToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolstripEnableBroadcast,
            this.toolStripMenuItem3,
            this.txtPortNbr});
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(200, 22);
			this.toolStripMenuItem2.Text = "Broadcast";
			// 
			// toolstripEnableBroadcast
			// 
			this.toolstripEnableBroadcast.Name = "toolstripEnableBroadcast";
			this.toolstripEnableBroadcast.Size = new System.Drawing.Size(174, 22);
			this.toolstripEnableBroadcast.Text = "Enable Broadcast";
			this.toolstripEnableBroadcast.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(174, 22);
			this.toolStripMenuItem3.Text = "Enter Port Number";
			// 
			// txtPortNbr
			// 
			this.txtPortNbr.Name = "txtPortNbr";
			this.txtPortNbr.Size = new System.Drawing.Size(100, 21);
			this.txtPortNbr.Text = "8000";
			// 
			// recentRecordingsToolStripMenuItem1
			// 
			this.recentRecordingsToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("recentRecordingsToolStripMenuItem1.Image")));
			this.recentRecordingsToolStripMenuItem1.Name = "recentRecordingsToolStripMenuItem1";
			this.recentRecordingsToolStripMenuItem1.Size = new System.Drawing.Size(200, 22);
			this.recentRecordingsToolStripMenuItem1.Text = "Recent Recordings";
			// 
			// exitToolStripMenuItem1
			// 
			this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
			this.exitToolStripMenuItem1.Size = new System.Drawing.Size(200, 22);
			this.exitToolStripMenuItem1.Text = "Exit";
			this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(342, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.playToolStripMenuItem,
            this.pauseToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.showUIToolStripMenuItem,
            this.tsShowStatus,
            this.recentRecordingsToolStripMenuItem,
            this.ShowUI,
            this.exitToolStripMenuItem});
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
			this.optionsToolStripMenuItem.Text = "Options";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtBroadcastURL});
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(235, 22);
			this.toolStripMenuItem1.Text = "Open Broadcast URL Recording";
			// 
			// txtBroadcastURL
			// 
			this.txtBroadcastURL.Name = "txtBroadcastURL";
			this.txtBroadcastURL.Size = new System.Drawing.Size(100, 21);
			this.txtBroadcastURL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBroadcastURL_KeyDown);
			// 
			// playToolStripMenuItem
			// 
			this.playToolStripMenuItem.Name = "playToolStripMenuItem";
			this.playToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
			this.playToolStripMenuItem.Text = "Play";
			this.playToolStripMenuItem.Click += new System.EventHandler(this.playToolStripMenuItem_Click);
			// 
			// pauseToolStripMenuItem
			// 
			this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
			this.pauseToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
			this.pauseToolStripMenuItem.Text = "Pause";
			this.pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
			// 
			// stopToolStripMenuItem
			// 
			this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
			this.stopToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
			this.stopToolStripMenuItem.Text = "Stop";
			this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
			// 
			// showUIToolStripMenuItem
			// 
			this.showUIToolStripMenuItem.Name = "showUIToolStripMenuItem";
			this.showUIToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
			this.showUIToolStripMenuItem.Text = "Show Media Player UI";
			this.showUIToolStripMenuItem.Click += new System.EventHandler(this.showUIToolStripMenuItem_Click);
			// 
			// tsShowStatus
			// 
			this.tsShowStatus.Name = "tsShowStatus";
			this.tsShowStatus.Size = new System.Drawing.Size(235, 22);
			this.tsShowStatus.Text = "Show Status bar";
			this.tsShowStatus.Click += new System.EventHandler(this.tsShowStatus_Click);
			// 
			// recentRecordingsToolStripMenuItem
			// 
			this.recentRecordingsToolStripMenuItem.Name = "recentRecordingsToolStripMenuItem";
			this.recentRecordingsToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
			this.recentRecordingsToolStripMenuItem.Text = "Recent Recordings";
			// 
			// ShowUI
			// 
			this.ShowUI.Name = "ShowUI";
			this.ShowUI.Size = new System.Drawing.Size(235, 22);
			this.ShowUI.Text = "Show UI on Startup";
			this.ShowUI.Click += new System.EventHandler(this.ShowUI_Click);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// saveRecording
			// 
			this.saveRecording.AddExtension = false;
			this.saveRecording.CheckFileExists = true;
			this.saveRecording.DefaultExt = "wmv";
			this.saveRecording.Filter = "Video Files|*.wmv";
			this.saveRecording.RestoreDirectory = true;
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// tmrRcCounter
			// 
			this.tmrRcCounter.Interval = 1000;
			this.tmrRcCounter.Tick += new System.EventHandler(this.tmrRcCounter_Tick);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsRecDuration,
            this.tsUsersCount,
            this.tsddlUsrDetails,
            this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 294);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(342, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// tsRecDuration
			// 
			this.tsRecDuration.Name = "tsRecDuration";
			this.tsRecDuration.Size = new System.Drawing.Size(0, 17);
			// 
			// tsUsersCount
			// 
			this.tsUsersCount.Name = "tsUsersCount";
			this.tsUsersCount.Size = new System.Drawing.Size(0, 17);
			// 
			// tsddlUsrDetails
			// 
			this.tsddlUsrDetails.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.tsddlUsrDetails.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
			this.tsddlUsrDetails.Image = ((System.Drawing.Image)(resources.GetObject("tsddlUsrDetails.Image")));
			this.tsddlUsrDetails.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsddlUsrDetails.Name = "tsddlUsrDetails";
			this.tsddlUsrDetails.Size = new System.Drawing.Size(13, 20);
			this.tsddlUsrDetails.Text = "toolStripDropDownButton1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
			// 
			// tmrViewerCount
			// 
			this.tmrViewerCount.Interval = 2000;
			this.tmrViewerCount.Tick += new System.EventHandler(this.tmrViewerCount_Tick);
			// 
			// frmRecorder
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(342, 316);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.MediaPlayer);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "frmRecorder";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Recorder...";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.frmRecorder_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRecorder_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.MediaPlayer)).EndInit();
			this.cxtRecordingOpts.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private AxWMPLib.AxWindowsMediaPlayer MediaPlayer;
		private System.Windows.Forms.NotifyIcon notifyRecorder;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem recentRecordingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip cxtRecordingOpts;
		private System.Windows.Forms.ToolStripMenuItem startRecordingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pauseRecordingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem stopRecordingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveCurrentRecordingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem recentRecordingsToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem showUIToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addAudio;
		private System.Windows.Forms.SaveFileDialog saveRecording;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem ShowUI;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.ToolStripMenuItem mnutxtAudioFile;
		private System.Windows.Forms.ToolStripTextBox txtAudioFile;
		private System.Windows.Forms.Timer tmrRcCounter;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
		private System.Windows.Forms.ToolStripTextBox txtPortNbr;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem toolstripEnableBroadcast;
		private System.Windows.Forms.ToolStripTextBox txtBroadcastURL;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel tsRecDuration;
		private System.Windows.Forms.ToolStripStatusLabel tsUsersCount;
		private System.Windows.Forms.Timer tmrViewerCount;
		private System.Windows.Forms.ToolStripDropDownButton tsddlUsrDetails;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripMenuItem tsShowStatus;


	}
}

