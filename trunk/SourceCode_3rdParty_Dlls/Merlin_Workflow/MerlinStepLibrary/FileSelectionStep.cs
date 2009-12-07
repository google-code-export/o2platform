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

using Merlin;

namespace MerlinStepLibrary
{
    /// <summary>
    /// This step presents a file selection interface.
    /// </summary>
    public class FileSelectionStep : TemplateStep
    {
        private FileSelectionStepUI ui;

        /// <summary>
        /// Create a new FileSelectionStep
        /// </summary>
        public FileSelectionStep()
            : base()
        {
            ui = new FileSelectionStepUI(()=>this.BrowseDialogTitle);
            this.UI = ui;
            ui.StateChangeEvent = (object sender, EventArgs args) => { this.StateUpdated(); };
        }

        /// <summary>
        /// Creates a new FileSelectionStep with the specified title
        /// </summary>
        /// <param name="title"></param>
        public FileSelectionStep(string title) :
            this()
        {
            this.Title = title;
        }

        /// <summary>
        /// Creates a new FileSelectionStep with the specified title and question text.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="question"></param>
        public FileSelectionStep(string title, string question) :
            this(title)
        {
            this.PromptText = question;
        }

        /// <summary>
        /// Gets or sets the full path of the selected file.
        /// </summary>
        public string SelectedFullPath
        {
            get
            {
                return this.ui.SelectedPathFile;
            }
            set
            {
                this.ui.SelectedPathFile = value;
            }
        }

        /// <summary>
        /// Gets or sets the current filename filter string.
        /// The format of the string is the same as that of the Filter
        /// property of System.Windows.Forms.OpenFileDialog
        /// </summary>
        public string Filter
        {
            get
            {
                return this.ui.Filter;
            }
            set
            {
                this.ui.Filter = value;
            }
        }

        /// <summary>
        /// Gets or sets the selected file name
        /// </summary>
        public string SelectedFileName
        {
            get
            {
                return this.ui.FileName;
            }
            set
            {
                this.ui.FileName = value;
            }
        }

        /// <summary>
        /// Gets or sets the question text asking for the file.
        /// </summary>
        public string PromptText
        {
            get
            {
                return this.ui.PromptText;
            }
            set
            {
                this.ui.PromptText = value;
            }
        }

        /// <summary>
        /// Gets or sets the initial directory to be shown in the browse dialog.
        /// </summary>
        public string InitialDirectory
        {
            get
            {
                return this.ui.InitialDirectory;
            }
            set
            {
                this.ui.InitialDirectory = value;
            }
        }

        string _browseDialogTitle = null;
        /// <summary>
        /// Gets or sets the custom title for the browse dialog.
        /// If set to null (default), the title of the step will be 
        /// used for the dialog title.
        /// </summary>
        public string BrowseDialogTitle
        {
            get
            {
                if (string.IsNullOrEmpty(_browseDialogTitle))
                {
                    return this.Title;
                }
                else return _browseDialogTitle;
            }
            set
            {
                _browseDialogTitle = value;
            }
        }
    }
}
