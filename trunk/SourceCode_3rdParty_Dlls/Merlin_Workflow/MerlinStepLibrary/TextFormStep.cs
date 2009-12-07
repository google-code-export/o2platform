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
using System.Collections;
using System.Collections.Generic;
using System.Text;


using Merlin;

namespace MerlinStepLibrary
{
    /// <summary>
    /// Used to display a form consisting of several text fields 
    /// preceeded by short question prompts.
    /// </summary>
    public class TextFormStep : TemplateStep
    {
        private TextFormStepUI ui;
        private List<Question> questions;

        #region Constructors
        /// <summary>
        /// Create a new TextFormStep
        /// </summary>
        public TextFormStep()
            : base(new TextFormStepUI())
        {
            questions = new List<Question>();
            this.lineUpQuestions = true;
            this.ui = (TextFormStepUI)this.UI;
            this.ui.StateChangeEvent = (object sender, EventArgs args) =>
            {
                this.StateUpdated();
            };
        }

        /// <summary>
        /// Creates a new TextFormStep with the specified title
        /// </summary>
        /// <param name="title"></param>
        public TextFormStep(string title)
            : this()
        {
            this.Title = title;
        }

        /// <summary>
        /// Creates a new TextFormStep with the specified title and prompt
        /// </summary>
        /// <param name="title"></param>
        /// <param name="prompt"></param>
        public TextFormStep(string title, string prompt)
            : this(title)
        {
            this.Prompt = prompt;
        }


        /// <summary>
        /// Creates a new TextFormStep with the specified title, prompt, and questions
        /// </summary>
        /// <param name="title"></param>
        /// <param name="promptText"></param>
        /// <param name="questions"></param>
        public TextFormStep(string title, string promptText, IEnumerable<Question> questions)
            : this(title, promptText)
        {
            populateQuestions(questions);
        }

        /// <summary>
        /// Creates a new TextFormStep with the specified title, prompt, and questions.
        /// The Answers will contain the answer provided in the order in which the
        /// questions appear in this constructor.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="prompt"></param>
        /// <param name="questions"></param>
        public TextFormStep(string title, string prompt, params string[] questions)
            : this(title, prompt)
        {
            foreach (string question in questions)
            {
                this.AddQuestion(question);
            }
        }

        private void populateQuestions(IEnumerable<Question> questions)
        {
            foreach (var question in questions)
            {
                this.registerQuestion(question);
            }
        }

        #endregion

        /// <summary>
        /// Returns the answers in the order of appearance.
        /// </summary>
        public IEnumerable<string> Answers
        {
            get
            {
                foreach (Question q in questions)
                {
                    yield return q.Answer;
                }
            }
        }

        private bool lineUpQuestions;
        /// <summary>
        /// Gets or sets a value indicating whether all the textboxes should
        /// be lined up to the same X coordinate.
        /// </summary>
        public bool LineUpQuestions
        {
            get
            {
                return lineUpQuestions;
            }
            set
            {
                lineUpQuestions = value;
                this.ui.doLineUpQuestions(value);
            }
        }

        /// <summary>
        /// Gets or sets the prompt text, which appears above the first question
        /// </summary>
        public string Prompt
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
            bool result = true;
            questions.ForEach(question => result = result && question.Validate());
            return result;
        }

        /// <summary>
        /// Adds a new question to the UI, question list.
        /// Must be called for every question added in any way.
        /// </summary>
        private void registerQuestion(Question q)
        {
            questions.Add(q);
            ui.addQuestion(q, this.LineUpQuestions);
        }

        /// <summary>
        /// Add a Question into the TextFormStep
        /// </summary>
        /// <param name="textQuestion"></param>
        public Question AddQuestion(string textQuestion)
        {
            var newQuestion = new Question(textQuestion);
            registerQuestion(newQuestion);
            return newQuestion;
        }

        /// <summary>
        /// Add a Question into the TextFormStep
        /// </summary>
        /// <param name="textQuestion">The question of the text</param>
        /// <param name="answer">A default or previously-provided answer</param>
        /// <returns></returns>
        public Question AddQuestion(string textQuestion, string answer)
        {
            var result = new Question(textQuestion);
            result.Answer = answer;
            registerQuestion(result);
            return result;
        }

        /// <summary>
        /// Add a Question into the TextFormStep
        /// </summary>
        /// <param name="textQuestion"></param>
        /// <param name="validityCondition"></param>
        public Question AddQuestion(string textQuestion, AnswerCondition validityCondition)
        {
            var result = AddQuestion(textQuestion);
            result.ValidityCondition = validityCondition;
            return result;
        }

        /// <summary>
        /// Add a question to the TextFormStep
        /// </summary>
        /// <param name="questionText"></param>
        /// <param name="validityCondition">A delegate applied to an answer that determines if the answer is valid</param>
        /// <param name="passwordChar">Password masking character</param>
        /// <returns></returns>
        public Question AddQuestion(string questionText, AnswerCondition validityCondition, char passwordChar)
        {
            var result = AddQuestion(questionText, validityCondition);
            result.PasswordChar = passwordChar;
            return result;
        }
    }
}
