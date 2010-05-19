using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;
using WMEncoderLib;

namespace DesktopRecorder
{
	//Prerequisites: Windows Media Player Encoder.
	//Download Link For Windows Media Player Encoder Software.
	//http://www.microsoft.com/downloads/details.aspx?FamilyID=5691ba02-e496-465a-bba9-b2f1182cdf24&displaylang=en
	//Or Search in Google for Windows Media Encoder 9 Series having size as 9.5MB and Install it.

	public partial class frmRecorder : Form
	{
		public static IWMEncoder DesktopEncoder;
		public static string strshowUI;
		public static DateTime  recordStarttime;
		public static WMEncoderApp DesktopEncoderAppln;
		public static IWMEncStatistics broadCastStats;
		public frmRecorder()
		{
			InitializeComponent();
		}
		private void frmRecorder_Load(object sender, EventArgs e)
		{
			//To Delete Previous Recordings,if it exists.
			if (File.Exists("C:\\TempRecording.wmv"))
			{
				File.Delete("C:\\TempRecording.wmv");
			}
			//To Show List of Existing Recordings.
			if (File.Exists("C:\\Desktop Items.txt"))
			{
				string item = "";
				StreamReader reader = new StreamReader("C:\\Desktop Items.txt");
				while ((item = reader.ReadLine()) != null)
				{
					if (item.StartsWith("---"))
					{
						strshowUI = item.ToLower().TrimStart('-');
						break;
					}
					if (File.Exists(item))
					{
						recentRecordingsToolStripMenuItem.DropDownItems.Add(item, null, recentRecordings_Click);
						recentRecordingsToolStripMenuItem1.DropDownItems.Add(item, null, recentRecordings_Click);
					}
				}
				reader.Close();
			}
			//Condition checking,whether Recorder should display UI or not on Startup.
			if (strshowUI == "no")
			{
				this.Opacity = 0;
				notifyRecorder.Text = "Recorder is in Invisible Mode.";
			}
			else if (strshowUI == "yes")
			{
				ShowUI.Checked = true;
				timer1.Enabled = false;
				notifyRecorder.Text = "Recorder is in Visible Mode.";
			}
			tsShowStatus.Checked = true;
		}
		private void recentRecordings_Click(object sender, EventArgs e)
		{
			try
			{
				//To Play Selected recent Recording.
				if (File.Exists(((ToolStripItem)sender).Text))
				{
					MediaPlayer.URL = ((ToolStripItem)sender).Text;
					this.Text = ((ToolStripItem)sender).Text;
				}
			}
			catch { }
		}
		private void playToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//To Play the Player.
			MediaPlayer.Ctlcontrols.play();
		}
		private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//To Pause the Player.
			MediaPlayer.Ctlcontrols.pause();
		}
		private void stopToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//To Stop the Player.
			MediaPlayer.Ctlcontrols.stop();
		}
		//To Control showing/hiding of Windows Media Player inbuilt UI.
		private void showUIToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (showUIToolStripMenuItem.Checked)
			{
				showUIToolStripMenuItem.Checked = false;
				MediaPlayer.uiMode = "none";
			}
			else
			{
				showUIToolStripMenuItem.Checked = true;
				MediaPlayer.uiMode = "full";
				tsShowStatus.Checked = false;
				statusStrip1.Visible = false;
			}
		}
		//To Exit the Application.
		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
		//To Start the Recording.
		private void startRecordingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			IWMEncProfile SelProfile;
			IWMEncSource AudioSrc;
			try
			{
				if (DesktopEncoder != null)
				{
					if (DesktopEncoder.RunState == WMENC_ENCODER_STATE.WMENC_ENCODER_PAUSED)
					{
						DesktopEncoder.Start();
						return;
					}
				}
				DesktopEncoderAppln = new WMEncoderApp();
				DesktopEncoder = DesktopEncoderAppln.Encoder;
				IWMEncSourceGroupCollection SrcGroupCollection = DesktopEncoder.SourceGroupCollection;
				IWMEncSourceGroup SrcGroup = SrcGroupCollection.Add("SG_1");
				IWMEncVideoSource2 VideoSrc = (IWMEncVideoSource2)SrcGroup.AddSource(WMENC_SOURCE_TYPE.WMENC_VIDEO);
				//Set Audio Source.
				if (addAudio.Checked)
				{
					AudioSrc = SrcGroup.AddSource(WMENC_SOURCE_TYPE.WMENC_AUDIO);
					if (txtAudioFile.Text.Trim() != "")
					{
						if (File.Exists(txtAudioFile.Text.Trim()))
						{
							AudioSrc.SetInput(txtAudioFile.Text.Trim(), "", "");
						}
						else
						{
							AudioSrc.SetInput("Default_Audio_Device", "Device", "");
						}
					}
					else
					{
						AudioSrc.SetInput("Default_Audio_Device", "Device", "");
					}
				}
				//Set Video Source:Desktop.
				VideoSrc.SetInput("ScreenCapture1", "ScreenCap", "");
				IWMEncProfileCollection ProfileCollection = DesktopEncoder.ProfileCollection;
				ProfileCollection = DesktopEncoder.ProfileCollection;
				int lLength = ProfileCollection.Count;
				//Set Profile.
				if (toolstripEnableBroadcast.Checked && txtPortNbr.Text.Trim() != "")
				{
					IWMEncBroadcast broadcast = DesktopEncoder.Broadcast;
					broadcast.set_PortNumber(WMENC_BROADCAST_PROTOCOL.WMENC_PROTOCOL_HTTP, Convert.ToInt32(txtPortNbr.Text.Trim()));
					for (int i = 0; i <= lLength - 1; i++)
					{
						SelProfile = ProfileCollection.Item(i);
						if (SelProfile.Name == "Windows Media Video 8 for Local Area Network (768 Kbps)")
						{
							SrcGroup.set_Profile((IWMEncProfile)SelProfile);
							break;
						}
					}
				}
				else
				{
					for (int i = 0; i <= lLength - 1; i++)
					{
						SelProfile = ProfileCollection.Item(i);
						if (SelProfile.Name == "Screen Video/Audio High (CBR)")
						{
							SrcGroup.set_Profile((IWMEncProfile)SelProfile);
							break;
						}
					}
				}
				//Local File to Store Recording temporarily.
				IWMEncFile inputFile = DesktopEncoder.File;
				inputFile.LocalFileName = "C:\\TempRecording.wmv";
				DesktopEncoder.PrepareToEncode(true);
				DesktopEncoder.Start();
				tmrRcCounter.Enabled = true;
				recordStarttime = DateTime.Now;
				if (toolstripEnableBroadcast.Checked && txtPortNbr.Text.Trim() != "")
				{
					//Start Timer to Count Viewers connected to Broadcast.
					tmrViewerCount.Enabled = true;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		//To Pause the Recording.
		private void pauseRecordingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (DesktopEncoder != null)
				{
					DesktopEncoder.Pause();
					tmrRcCounter.Enabled = false;
				}
			}
			catch
			{
				MessageBox.Show("Can't Pause it.Please,Save Current Recording and Restart Application.");
			}
		}
		//To Stop & Save the Recording.
		private void stopRecordingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (DesktopEncoder != null)
				{
					DesktopEncoder.Stop();
					SaveFileDialog dialog = new SaveFileDialog();
					dialog.Filter = "Video Files (*.wmv)|*.wmv";
					dialog.DefaultExt = "wmv";
					DialogResult res = dialog.ShowDialog();
					if (res != DialogResult.Cancel)
					{
						if (File.Exists("C:\\TempRecording.wmv"))
						{
							recentRecordingsToolStripMenuItem1.DropDownItems.Add(dialog.FileName, null, recentRecordings_Click);
							recentRecordingsToolStripMenuItem.DropDownItems.Add(dialog.FileName, null, recentRecordings_Click);
							File.Copy("C:\\TempRecording.wmv", dialog.FileName);
							File.Delete("C:\\TempRecording.wmv");
						}
					}
					tmrRcCounter.Enabled = false;
					tsRecDuration.Text = "";
					tmrViewerCount.Enabled = false;
					if (this.Visible)
					{
						notifyRecorder.Text = "Recorder is in Visible Mode.";
					}
					else
					{
						notifyRecorder.Text = "Recorder is in Invisible Mode.";
					}
					//Windows Media Encoder EXE.
					Process[] ps = Process.GetProcessesByName("wmenc");
					if (ps.Length != 0)
					{
						ps[0].Kill();
					}
					DesktopEncoder = null;
					tmrRcCounter.Enabled = false;
				}
			}
			catch
			{
				MessageBox.Show("Can't Stop it.Please,Save Current Recording and Restart Application.");
			}

		}
		//To Control adding of Audio to recording.
		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			if (addAudio.Checked)
			{
				addAudio.Checked = false;
			}
			else
			{
				addAudio.Checked = true;
			}
		}
		//To Save the Current Recording.
		private void saveCurrentRecordingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (DesktopEncoder != null)
				{
					if (DesktopEncoder.RunState == WMENC_ENCODER_STATE.WMENC_ENCODER_RUNNING)
					{
						MessageBox.Show("Can't Save Current Recording.Since,Recorder is in Working Mode.Please,Stop it and Try Again.");
						return;
					}
				}
				SaveFileDialog dialog = new SaveFileDialog();
				dialog.Filter = "Video Files (*.wmv)|*.wmv";
				dialog.DefaultExt = "wmv";
				DialogResult res = dialog.ShowDialog();
				if (res != DialogResult.Cancel)
				{
					if (File.Exists("C:\\TempRecording.wmv"))
					{
						recentRecordingsToolStripMenuItem1.DropDownItems.Add(dialog.FileName, null, recentRecordings_Click);
						recentRecordingsToolStripMenuItem.DropDownItems.Add(dialog.FileName, null, recentRecordings_Click);
						File.Copy("C:\\TempRecording.wmv", dialog.FileName);
						if (DesktopEncoder.RunState != WMENC_ENCODER_STATE.WMENC_ENCODER_RUNNING && DesktopEncoder.RunState != WMENC_ENCODER_STATE.WMENC_ENCODER_PAUSED)
						{
							File.Delete("C:\\TempRecording.wmv");
						}
					}
				}
				tmrRcCounter.Enabled = false;
				tsRecDuration.Text = "";
				tmrViewerCount.Enabled = false;
				if (this.Visible)
				{
					notifyRecorder.Text = "Recorder is in Visible Mode.";
				}
				else
				{
					notifyRecorder.Text = "Recorder is in Invisible Mode.";
				}
			}
			catch
			{
				MessageBox.Show("Can't Save Current Recording.Please,Restart Application.");
			}
		}
		private void frmRecorder_FormClosing(object sender, FormClosingEventArgs e)
		{
			StreamWriter writer = null;
			try
			{
				writer = new StreamWriter("C:\\Desktop Items.txt", false);
				foreach (ToolStripItem file in recentRecordingsToolStripMenuItem.DropDownItems)
				{
					writer.WriteLine(file.Text);
				}
				if (ShowUI.Checked)
				{
					writer.WriteLine("---yes");
				}
				else
				{
					writer.WriteLine("---no");
				}
				if (writer != null)
				{
					writer.Flush();
					writer.Close();
				}
				//This EXE should be killed explicitly,if it still exists.
				//Windows Media Encoder EXE.
				Process[] ps = Process.GetProcessesByName("wmenc");
				if (ps.Length != 0)
				{
					ps[0].Kill();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
			
			}
		}
		//To Control showing/hiding of the Player.
		private void notifyRecorder_DoubleClick(object sender, EventArgs e)
		{
			if (this.Opacity == 0)
			{
				this.Opacity = 1;
			}
			if (this.Visible)
			{
				this.Visible = false;
				notifyRecorder.Text = "Recorder is in Invisible Mode.";
			}
			else
			{
				this.Visible = true;
				notifyRecorder.Text = "Recorder is in Visible Mode.";
			}
		}
		private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
		private void ShowUI_Click(object sender, EventArgs e)
		{
			if (ShowUI.Checked)
			{
				ShowUI.Checked = false;
			}
			else
			{
				ShowUI.Checked = true;
			}
		}
		//To Hide Player on StartUp.
		private void timer1_Tick(object sender, EventArgs e)
		{
			this.Hide();
			timer1.Enabled = false;
		}
		//To Show Recording Duration.
		private void tmrRcCounter_Tick(object sender, EventArgs e)
		{
			if (recordStarttime != null)
			{
				int Mins = Convert.ToInt32(((TimeSpan)(DateTime.Now - recordStarttime)).Minutes);
				int Seconds = Convert.ToInt32(((TimeSpan)(DateTime.Now - recordStarttime)).Seconds);
				notifyRecorder.Text = Mins.ToString() + " Minutes :" + Seconds.ToString() + " Seconds";
				tsRecDuration.Text = Mins.ToString() + " Minutes :" + Seconds.ToString() + " Seconds";
			}
		}
		private void toolStripMenuItem4_Click(object sender, EventArgs e)
		{
			if (toolstripEnableBroadcast.Checked)
			{
				toolstripEnableBroadcast.Checked = false;
			}
			else
			{
				toolstripEnableBroadcast.Checked = true;
			}
		}
		//To Set Broadcast Port Number.
		private void txtBroadcastURL_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.Enter)
				{
					if (!txtBroadcastURL.Text.Trim().ToLower().StartsWith("http"))
					{
						MediaPlayer.URL = "http://" + txtBroadcastURL.Text.Trim();
					}
					else
					{
						MediaPlayer.URL = txtBroadcastURL.Text.Trim();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		//To Set User's count watching Broadcast.
		private void tmrViewerCount_Tick(object sender, EventArgs e)
		{
			try
			{
				tsddlUsrDetails.DropDownItems.Clear();
				broadCastStats = DesktopEncoder.Statistics;
				int ViewersCount = ((IWMEncNetConnectionStats2)broadCastStats.NetConnectionStats).ClientCount;
				WMENC_BROADCAST_PROTOCOL SelProtocol = WMENC_BROADCAST_PROTOCOL.WMENC_PROTOCOL_HTTP;
				switch (ViewersCount)
				{
					case 0: tsUsersCount.Text = "No Users Connected.";
						break;
					case 1: tsUsersCount.Text = "1 User Connected.";
						break;
					default:
						tsUsersCount.Text = ViewersCount.ToString() + "Connected.";
						break;
				}
				for (int i = 0; i < ViewersCount; i++)
				{
					tsddlUsrDetails.DropDownItems.Add(((IWMEncNetConnectionStats2)broadCastStats.NetConnectionStats).get_ClientInfo(i, out SelProtocol));
				}
			}
			catch{}
		}

		private void tsShowStatus_Click(object sender, EventArgs e)
		{
			if (tsShowStatus.Checked)
			{
				tsShowStatus.Checked = false;
				statusStrip1.Visible = false;
			}
			else
			{
				tsShowStatus.Checked = true;
				statusStrip1.Visible = true;
				showUIToolStripMenuItem.Checked = false;
				MediaPlayer.uiMode = "none";
			}

		}
	}
}