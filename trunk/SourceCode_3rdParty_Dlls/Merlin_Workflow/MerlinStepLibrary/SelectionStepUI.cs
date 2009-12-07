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
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace MerlinStepLibrary
{
    internal partial class SelectionStepUI : UserControl
    {
        private List<Option> _options = new List<Option>();
        private int _lowestOptionBottom; //Indicates the bottom of 
        //the last option prior to resizing.
        private const int _minimumPaddingFromTop = 10; //Indicates the minimum
        //distance from the top of the container to the question text.
        private int _distanceBetweenOptions = 10;
        private int _distanceToFirstOption = 25; //Distance from the question
        //to the first option
        internal EventHandler StateChangeEvent { get; set; }

        internal SelectionStepUI()
        {
            InitializeComponent();
        }

        internal void initialize(IEnumerable possibleOptionValues)
        {
            int optionCount = 0;
            foreach (Object selectionObject in possibleOptionValues)
            {
                RadioButton newButton = new System.Windows.Forms.RadioButton();
                newButton.Click += new System.EventHandler(this.newButton_Click);
                newButton.KeyUp += new System.Windows.Forms.KeyEventHandler(this.newButton_KeyUp);
                newButton.Left = lblQuestionText.Left + lblQuestionText.Margin.Left;
                newButton.Text = selectionObject.ToString();
                newButton.TabStop = true;
                _options.Add(new Option(selectionObject, newButton));
                optionCount++;
            }
            _lowestOptionBottom = getLowestOptionBottom(optionCount);
            if (_lowestOptionBottom > pnlSelectionGroup.ClientRectangle.Height)
            {
                _distanceToFirstOption -= 7;
                _distanceBetweenOptions -= 5;
            }
        }

        internal Object Selected
        {
            get
            {
                foreach (Option selected in _options)
                {
                    if (selected.Checked)
                    {
                        return selected.Value;
                    }
                }
                return null;
            }
        }

        internal string PromptText
        {
            get
            {
                return this.lblQuestionText.Text;
            }
            set
            {
                this.lblQuestionText.Text = value;
            }
        }

        private void SelectionStepUI_Resize(object sender, EventArgs e)
        {
            this.pnlSelectionGroup.Height = pnlSelectionGroup.Parent.ClientRectangle.Height;
            this.pnlSelectionGroup.Width = pnlSelectionGroup.Parent.ClientRectangle.Width;
            this.pnlSelectionGroup.Top = 0;
            this.pnlSelectionGroup.Left = 0;
            int containerHeight = pnlSelectionGroup.ClientRectangle.Height;
            int downShift = (containerHeight - _lowestOptionBottom) > _minimumPaddingFromTop ?
                (containerHeight - _lowestOptionBottom) * 1 / 4 : _minimumPaddingFromTop;
            lblQuestionText.Top = downShift;
            int nextTop = lblQuestionText.Bottom + _distanceToFirstOption;
            foreach (var option in _options)
            {
                option.Button.Top = nextTop;
                nextTop = option.Button.Bottom + _distanceBetweenOptions;
                this.pnlSelectionGroup.Controls.Add(option.Button);
            }
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            StateChangeEvent(sender, EventArgs.Empty);
        }
        private void newButton_KeyUp(object sender, EventArgs e)
        {
            StateChangeEvent(sender, EventArgs.Empty);
        }

        /// <summary>
        /// Computes the bottom of the last option prior to resizing.
        /// </summary>
        /// <returns></returns>
        private int getLowestOptionBottom(int optionCount)
        {
            return (_distanceBetweenOptions + new RadioButton().Height) * (optionCount - 1)
                + lblQuestionText.Height + _distanceToFirstOption;
        }
    }
}
