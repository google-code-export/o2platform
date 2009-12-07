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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Merlin;

namespace MerlinStepLibrary
{
    internal partial class FileSelectionStepUI : UserControl
    {
        /// <summary>
        /// A function that retrieves a string
        /// </summary>
        /// <returns></returns>
        internal delegate string StringRetriever();
        internal EventHandler StateChangeEvent { get; set; }
        StringRetriever _browseDialogTitleRetriever;

        internal FileSelectionStepUI(StringRetriever browseDialogTitleRetriever)
        {
            _browseDialogTitleRetriever = browseDialogTitleRetriever;
            InitializeComponent();
        }

        

        internal string SelectedPathFile
        {
            get
            {
                return txtFilePathSelected.Text;
            }
            set
            {
                txtFilePathSelected.Text = value;
                
            }
        }

        internal string Filter
        {
            get
            {
                return dlgFileSelection.Filter;
            }
            set
            {
                dlgFileSelection.Filter = value;
            }
        }

        internal string FileName
        {
            get
            {
                return dlgFileSelection.FileName;
            }
            set
            {
                dlgFileSelection.FileName = value;
            }
        }

        internal string PromptText
        {
            get
            {
                return lblSelectFile.Text;
            }
            set
            {
                lblSelectFile.Text = value;
            }
        }

        internal string InitialDirectory
        {
            get
            {
                return dlgFileSelection.InitialDirectory;
            }
            set
            {
                dlgFileSelection.InitialDirectory = value;
            }
        }

        /// <summary>
        /// The title of the "browse" dialog box.
        /// </summary>
        internal string BrowseDialogTitle { get; set; }

        #region Event Members

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            dlgFileSelection.Title = _browseDialogTitleRetriever();
            DialogResult result = dlgFileSelection.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtFilePathSelected.Text = dlgFileSelection.FileName;
                StateChangeEvent(sender, EventArgs.Empty);
            }
        }

        private void FileSelectionStepUI_Resize(object sender, EventArgs e)
        {
            int margin = 10;
            pnlControlsFileSelection.Height = pnlControlsFileSelection.Parent.ClientRectangle.Height - 2 * margin;
            pnlControlsFileSelection.Width = pnlControlsFileSelection.Parent.ClientRectangle.Width - 2 * margin;
            pnlControlsFileSelection.Top = margin;
            pnlControlsFileSelection.Left = margin;
            btnBrowse.Left = pnlControlsFileSelection.ClientRectangle.Width - btnBrowse.Width - 20;
            txtFilePathSelected.Width = btnBrowse.Left - txtFilePathSelected.Left - 5;
        }

        private void txtFilePathSelected_KeyUp(object sender, KeyEventArgs e)
        {
            StateChangeEvent(sender, EventArgs.Empty);
        }

        private void txtFilePathSelected_TextChanged(object sender, EventArgs e)
        {
            StateChangeEvent(sender, EventArgs.Empty);
        }

        #endregion


    }
}
