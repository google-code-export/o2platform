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

using System.Windows.Forms;
namespace MerlinStepLibrary
{
    partial class SelectionStepUI
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
            this.pnlSelectionGroup = new System.Windows.Forms.Panel();
            this.lblQuestionText = new System.Windows.Forms.Label();
            this.pnlSelectionGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSelectionGroup
            // 
            this.pnlSelectionGroup.AutoScroll = true;
            this.pnlSelectionGroup.Controls.Add(this.lblQuestionText);
            this.pnlSelectionGroup.Location = new System.Drawing.Point(81, 68);
            this.pnlSelectionGroup.Name = "pnlSelectionGroup";
            this.pnlSelectionGroup.Size = new System.Drawing.Size(256, 154);
            this.pnlSelectionGroup.TabIndex = 1;
            // 
            // lblQuestionText
            // 
            this.lblQuestionText.AutoSize = true;
            this.lblQuestionText.Location = new System.Drawing.Point(27, 0);
            this.lblQuestionText.Name = "lblQuestionText";
            this.lblQuestionText.Size = new System.Drawing.Size(400, 17);
            this.lblQuestionText.TabIndex = 2;
            // 
            // SelectionStepUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlSelectionGroup);
            this.Name = "SelectionStepUI";
            this.Size = new System.Drawing.Size(445, 298);
            this.Resize += new System.EventHandler(this.SelectionStepUI_Resize);
            this.pnlSelectionGroup.ResumeLayout(false);
            this.pnlSelectionGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel pnlSelectionGroup;
        private Label lblQuestionText;
    }
}
