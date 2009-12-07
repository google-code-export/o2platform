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
using System.Text;

using Merlin;
using System.Windows.Forms;
using System.Collections;

namespace MerlinStepLibrary
{
    /// <summary>
    /// Accepts a bunch of objects. Displays each object as a radio button
    /// labeled as that object's ToString()
    /// </summary>
    public class SelectionStep : TemplateStep
    {
        private SelectionStepUI ui;
        private List<Option> listSelections = new List<Option>();

        #region Constructors
        /// <summary>
        /// Creates a new SelectionStep with the specified options.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="selections"></param>
        public SelectionStep(string title, IEnumerable options)
            : this(options)
        {
            this.Title = title;
        }

        /// <summary>
        /// Creates a new SelectionStep with the specified title and options.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="options"></param>
        public SelectionStep(string title, params object[] options)
            : this(options)
        {
            this.Title = title;
        }

        /// <summary>
        /// Creates a new SelectionStep with the specified title, question, and options.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="guestionText"></param>
        /// <param name="options"></param>
        public SelectionStep(string title, string guestionText, params object[] options)
            :this(title, options)
        {
            this.PromptText = guestionText;
        }

        /// <summary>
        /// Creates a new SelectionStep with the specified options.
        /// </summary>
        /// <param name="selections"></param>
        public SelectionStep(params object[] options)
            : base(new SelectionStepUI())
        {
            initialize(options);
        }

        /// <summary>
        /// Creates a new SelectionStep with the specified options.
        /// </summary>
        /// <param name="options"></param>
        public SelectionStep(IEnumerable options)
            : base(new SelectionStepUI())
        {
            initialize(options);
        }

        #endregion

        /// <summary>
        /// Gets the Selected Object
        /// </summary>
        public object Selected {
            get
            {
                return this.ui.Selected;
            }
        }

        /// <summary>
        /// Gets or sets the question text introducing the selections
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

        public override bool AllowNext()
        {
            return this.Selected != null && base.AllowNext();
        }

        private void initialize(IEnumerable selections)
        {
            this.ui = (SelectionStepUI)this.UI;
            this.ui.StateChangeEvent = (object sender, EventArgs args) => 
            {
                this.StateUpdated(); 
            };
            this.ui.initialize(selections);
        }
    }
}
