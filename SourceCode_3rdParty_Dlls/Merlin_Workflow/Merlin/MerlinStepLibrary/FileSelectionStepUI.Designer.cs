/*
 * The contributors of Merlin license this file to You under the Apache 
 * License, Version 2.0 (the "License"); you may not use this file except 
 * in compliance with the License.  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace MerlinStepLibrary
{
    partial class FileSelectionStepUI
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
            this.txtFilePathSelected = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblSelectFile = new System.Windows.Forms.Label();
            this.dlgFileSelection = new System.Windows.Forms.OpenFileDialog();
            this.pnlControlsFileSelection = new System.Windows.Forms.Panel();
            this.pnlControlsFileSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFilePathSelected
            // 
            this.txtFilePathSelected.Location = new System.Drawing.Point(15, 45);
            this.txtFilePathSelected.Name = "txtFilePathSelected";
            this.txtFilePathSelected.Size = new System.Drawing.Size(310, 20);
            this.txtFilePathSelected.TabIndex = 5;
            this.txtFilePathSelected.TextChanged += new System.EventHandler(this.txtFilePathSelected_TextChanged);
            this.txtFilePathSelected.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFilePathSelected_KeyUp);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(331, 41);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(96, 26);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "&Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblSelectFile
            // 
            this.lblSelectFile.AutoSize = true;
            this.lblSelectFile.Location = new System.Drawing.Point(12, 19);
            this.lblSelectFile.Name = "lblSelectFile";
            this.lblSelectFile.Size = new System.Drawing.Size(118, 13);
            this.lblSelectFile.TabIndex = 4;
            this.lblSelectFile.Text = "&Please select your file : ";
            // 
            // pnlControlsFileSelection
            // 
            this.pnlControlsFileSelection.Controls.Add(this.lblSelectFile);
            this.pnlControlsFileSelection.Controls.Add(this.btnBrowse);
            this.pnlControlsFileSelection.Controls.Add(this.txtFilePathSelected);
            this.pnlControlsFileSelection.Location = new System.Drawing.Point(9, 13);
            this.pnlControlsFileSelection.Name = "pnlControlsFileSelection";
            this.pnlControlsFileSelection.Size = new System.Drawing.Size(439, 250);
            this.pnlControlsFileSelection.TabIndex = 7;
            // 
            // FileSelectionStepUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlControlsFileSelection);
            this.Name = "FileSelectionStepUI";
            this.Size = new System.Drawing.Size(469, 292);
            this.Resize += new System.EventHandler(this.FileSelectionStepUI_Resize);
            this.pnlControlsFileSelection.ResumeLayout(false);
            this.pnlControlsFileSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog dlgFileSelection;
        private System.Windows.Forms.TextBox txtFilePathSelected;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblSelectFile;
        private System.Windows.Forms.Panel pnlControlsFileSelection;
    }
}
