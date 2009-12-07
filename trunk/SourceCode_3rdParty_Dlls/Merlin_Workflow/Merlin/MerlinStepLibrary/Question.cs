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

namespace MerlinStepLibrary
{
    /// <summary>
    /// Represents a question asked in TextFormStep
    /// </summary>
    public class Question
    {
        /// <summary>
        /// Creates a new Question
        /// </summary>
        /// <param name="text">The title to be shown in the header</param>
        public Question(string text)
        {
            this.QuestionText = text;
        }

        /// <summary>
        /// Creates a new Question
        /// </summary>
        /// <param name="text">The title to be shown in the header</param>
        /// <param name="validity">The condition for allowing the Next button to be enabled</param>
        public Question(string text, AnswerCondition validity)
        {
            this.QuestionText = text;
            this.ValidityCondition = validity;
        }

        /// <summary>
        /// Gets or sets the question text
        /// </summary>
        public string QuestionText { get; set; }

        /// <summary>
        /// Gets or sets a character to be used for masking passwords.
        /// </summary>
        public char? PasswordChar { get; set; }

        /// <summary>
        /// Gets or sets the answer to the question
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        /// The condition for allowing the Next button to be enabled
        /// </summary>
        public AnswerCondition ValidityCondition { get; set; }

        internal bool Validate()
        {
            return (this.ValidityCondition == null || ValidityCondition(this.Answer));
        }

    }
}
