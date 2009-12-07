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

using Merlin;

namespace MerlinStepLibrary
{
    internal partial class TextFormStepUI : UserControl
    {
        #region Positioning Constants
        Padding _padding = new Padding(25, 25, 30, 20);
        const int MINIMUM_QUESTION_HEIGHT = 36; //Question height = height of textbox + distance to the next textbox
        const int MAXIMUM_QUESTION_HEIGHT = 50;
        const int LABEL_TEXTBOX_PADDING = 5; //Minimum distance from a question label to the texbox

        #endregion

        internal TextFormStepUI()
        {
            InitializeComponent();
            _placeholder = new Panel();
            pnlControls.Controls.Add(_placeholder);

        }
        internal EventHandler StateChangeEvent { get; set; }

      
        private bool lineUpQuestions;
        private List<string> answersList = new List<String>();
        private List<QuestionUI> questionsUIList = new List<QuestionUI>();
        Panel _placeholder;

        internal string PromptText
        {
            get
            {
                return this.lblPrompt.Text;
            }
            set
            {
                this.lblPrompt.Text = value;
            }
        }

        private IEnumerable<String> answers;
        internal IEnumerable<String> Answers 
        {
            get
            {
                answersList.Clear();
                foreach (QuestionUI questionUI in questionsUIList)
                {
                    questionUI.QuestionInfo.Answer = questionUI.TextBox.Text;
                    this.answersList.Add(questionUI.QuestionInfo.Answer);
                    answers = this.answersList;
                }
                return answers;
            }
        }

        internal void doLineUpQuestions(bool lineup)
        {
            this.lineUpQuestions = lineup;
        }

        internal void addQuestion(Question newQuestion, bool lineUp)
        {
            this.lineUpQuestions = lineUp;
            QuestionUI newQuestionUI = new QuestionUI(newQuestion);
            questionsUIList.Add(newQuestionUI);
            pnlControls.Controls.Add(newQuestionUI.Label);
            pnlControls.Controls.Add(newQuestionUI.TextBox);
            newQuestionUI.TextBox.KeyUp += textbox_KeyUp;
            newQuestionUI.Label.SendToBack();
        }

        internal void initialize()
        {
        }

        private void TextFormStepUI_Resize(object sender, EventArgs e)
        {
            int labelHeight = getLabelHeight();

            //Position the panel
            pnlControls.Location = new Point(0, 0);
            pnlControls.Size = pnlControls.Parent.ClientRectangle.Size;

            //Position/hide the prompt
            bool noPromptText = !string.IsNullOrEmpty(this.PromptText);
            lblPrompt.Top = noPromptText ? _padding.Top : 0 - lblPrompt.Height;
            lblPrompt.Left = _padding.Left;
            
            //Determine first question position
            int firstQuestionTop = lblPrompt.Bottom + MAXIMUM_QUESTION_HEIGHT;

            //Determing the height of a question
            int availableSpaceForQuestions = pnlControls.Height - firstQuestionTop;
            int questionHeight = availableSpaceForQuestions/questionsUIList.Count;
            if (questionHeight < MINIMUM_QUESTION_HEIGHT)
            {
                questionHeight = MINIMUM_QUESTION_HEIGHT;
            }
            if (questionHeight > MAXIMUM_QUESTION_HEIGHT)
            {
                questionHeight = MAXIMUM_QUESTION_HEIGHT;
            }

            //Determine if we need to offset the top padding (if scrollbars are likely)
            int topPaddingOffset = questionHeight == MINIMUM_QUESTION_HEIGHT ? 10 : 0;
            lblPrompt.Top -= topPaddingOffset;

            
            //Revise first question position based on question height
            firstQuestionTop = lblPrompt.Top + (int)(questionHeight * 1.1) - (int)(topPaddingOffset * .5);

            //Determine the Left for textboxes when LineUpQuestion is true
            int textboxLeft=-10;
            int maximumQuestionWidth = pnlControls.Width * 3 / 5;
            if (lineUpQuestions)
            {
                int longestQuestionWidth = computeLongestQuestionWidth();
                textboxLeft = _padding.Left + LABEL_TEXTBOX_PADDING +
                    (longestQuestionWidth > maximumQuestionWidth ? maximumQuestionWidth : longestQuestionWidth);
            }

            //Arrange the questions
            int nextQuestionTop = firstQuestionTop;
            int lastTextboxBottom = 0;
            foreach (var questionUI in questionsUIList)
            {
                Label label = questionUI.Label;
                TextBox textbox = questionUI.TextBox;
                
                //Horriontal positioning
                label.Left = _padding.Left;
                if (!lineUpQuestions)
                {
                    int labelWidth = computeLabelWidth(questionUI.Label);
                    if (labelWidth > maximumQuestionWidth)
                    {
                        labelWidth = maximumQuestionWidth;
                    }
                    textbox.Left = label.Left + labelWidth + LABEL_TEXTBOX_PADDING;
                    label.Width = labelWidth;

                }
                else
                {
                    textbox.Left = textboxLeft;
                    label.Width = textboxLeft - LABEL_TEXTBOX_PADDING - label.Left;
                }
                textbox.Width = pnlControls.ClientRectangle.Width - _padding.Right - textbox.Left;

                //Vertical positioning
                textbox.Top = nextQuestionTop;
                nextQuestionTop += questionHeight;
                label.Top = textbox.Top + (textbox.Height - labelHeight) / 2;
                lastTextboxBottom = textbox.Bottom;

                //Apply password masking
                if (questionUI.QuestionInfo.PasswordChar != null)
                {
                    textbox.PasswordChar = questionUI.QuestionInfo.PasswordChar.Value;
                }
            }
            //Position placeholder panel to ensure adequate padding at the bottom
            _placeholder.Location = new Point(-10, lastTextboxBottom);
            _placeholder.Size = new Size(3, _padding.Bottom);
        }

            
           
        private void textbox_KeyUp(object sender, EventArgs e)
        {
            //Update the answer values
            foreach (var questionUI in questionsUIList)
            {
                questionUI.QuestionInfo.Answer = questionUI.TextBox.Text;
            }
            StateChangeEvent(sender, EventArgs.Empty);
        }

        private static int computeLabelWidth(Label l)
        {
            Graphics g = l.CreateGraphics();
            int result = (int)g.MeasureString(l.Text, l.Font).Width + l.Margin.Horizontal;
            g.Dispose();
            return result;
        }

        private int getLabelHeight()
        {
            var g = pnlControls.CreateGraphics();
            int result = (int)g.MeasureString("!|A^", lblPrompt.Font).Height;
            g.Dispose();
            return result;
        }

        private int computeLongestQuestionWidth()
        {
            int currentMax = 0;
            questionsUIList.ForEach(ui =>
            {
                int width = computeLabelWidth(ui.Label);
                if (width > currentMax)
                {
                    currentMax = width;
                }
            });
            return currentMax;
        }


        private class QuestionUI
        {
            internal QuestionUI(Question question)
            {
                this.Label = new Label();
                this.Label.AutoSize = false;
                this.Label.Text = question.QuestionText;
                Label.AutoEllipsis = true;
                this.TextBox = new TextBox();
                this.TextBox.Text = question.Answer;
                this.QuestionInfo = question;
            }
            internal Question QuestionInfo { get; private set; }
            internal Label Label { get; private set; }
            internal TextBox TextBox { get; private set; }
        }
    }
}
