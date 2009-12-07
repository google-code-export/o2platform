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

 
 
﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Merlin
{
    /// <summary>
    /// This step itself never appears in the wizard.
    /// Instead, it adds another step if a provided boolean
    /// function evaluates to true. When the Previous button is clicked,
    /// this step is skipped over. As a result, a conditional 
    /// step may only be the first step in the sequence if
    /// both the next step and the provided step have 
    /// their "AllowPrevious" returning false. A conditional step
    /// may not be the last step in a step sequence.
    /// </summary>
    public sealed class ConditionalStep : IStep
    {
        public delegate bool StepCondition();
        private StepCondition condition;
        private IStep underlyingStep;
        private bool LastWentNext=false;

        public void StepReached()
        {
        }

        /// <summary>
        /// Creates a ConditionalStep
        /// </summary>
        /// <param name="condition">The condition under which the step appears</param>
        /// <param name="step">The step that appears if the condition is true</param>
        public ConditionalStep(StepCondition condition, 
            IStep step)
        {
            this.condition = condition;
            this.underlyingStep = step;
        }

        internal StepCondition Condition
        {
            get { return condition; }
        }

        internal IStep UnderlyingStep
        {
            get { return underlyingStep; }
        }

        private bool displayed = false;
        internal bool Displayed
        {
            get { return displayed; }
            set { displayed = value; }
        }
        

        #region Miscellaneous Step Implementation


        public string Title { get { return "Conditional Step"; } }
        public string Subtitle { get { return null; } }

        public Control UI { get { return null; } }

        public bool AllowNext()
        {
            return UnderlyingStep.AllowNext();
        }

        public bool AllowPrevious()
        {
            return UnderlyingStep.AllowPrevious();
        }

        public bool AllowCancel()
        {
            return UnderlyingStep.AllowCancel();
        }

        public bool OnNext()
        {
            LastWentNext = true;
            return true;
        }

        public bool OnPrevious()
        {
            LastWentNext = false;
            return false;
        }

        public void OnCancel()
        { }


        public event EventHandler StepStateChanged;

        #endregion
    }
}
