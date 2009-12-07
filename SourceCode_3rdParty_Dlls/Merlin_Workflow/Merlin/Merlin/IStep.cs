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
using System.Windows.Forms;
using System.Text;

namespace Merlin
{
    /// <summary>
    /// Defines the minimum requirements for a  "step" that can be displayed by the 
    /// WizardController. Consider inheriting from TemplateStep rather 
    /// than implementing manually.
    /// </summary>
    public interface IStep
    {
        /// <summary>
        /// Returns the user-facing title of this step.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Returns the user-facing subtitle of this step. If the subtitle is not 
        /// null or empty, it appears beneath the title in the header section of the wizard
        /// </summary>
        string Subtitle { get; }

        /// <summary>
        /// Executes every time this step is reached.
        /// </summary>
        void StepReached();

        /// <summary>
        /// Occurs when the user clicks the next button.
        /// Returns true if the current state permits to advance
        /// to the next screen.
        /// </summary>
        /// <returns></returns>
        bool OnNext();

        /// <summary>
        /// Occurs when the user clicks the previous button.
        /// Returns true if the current state permits to return
        /// to the previous screen.
        /// </summary>
        /// <returns></returns>
        bool OnPrevious();

        /// <summary>
        /// Occurs when the user clicks the cancel button.
        /// </summary>
        void OnCancel();

        /// <summary>
        /// True when the state of the step makes it 
        /// permissible for the "Next" button to be clicked.
        /// For example, a settings step would have this 
        /// method return true when all fields are populated
        /// with legitimate values.
        /// </summary>
        bool AllowNext();
        
        /// <summary>
        /// True when it is permissible for the previous
        /// button to be clicked.
        /// </summary>
        bool AllowPrevious();


        /// <summary>
        /// True when it is permissible for the Cancel button
        /// to be clicked. This property should always return true,
        /// except under extreme circumstances when aborting the wizard
        /// would somehow create an inconsistent state.
        /// </summary>
        bool AllowCancel();

        /// <summary>
        /// Occurs when the state of the component is changed.
        /// </summary>
        event EventHandler StepStateChanged;

        /// <summary>
        /// The control that is displayed in the wizard when this
        /// step is reached.
        /// </summary>
        Control UI
        {
            get;
        }

        
    }
}
