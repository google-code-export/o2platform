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

using System.Windows.Forms;

namespace MerlinStepLibrary
{
    /// <summary>
    /// Represents an option for the selection control
    /// </summary>
    internal class Option
    {
        private RadioButton button;

        /// <summary>
        /// Creates a new Option
        /// </summary>
        /// <param name="select"></param>
        /// <param name="button"></param>
        internal Option(Object select, RadioButton button)
        {
            this.button = button;
            this.Value = select;
        }
        
        #region Properties

        /// <summary>
        /// Returns the object selected
        /// </summary>
        internal object Value { get; private set; }

        /// <summary>
        /// Returns true if a button is checked
        /// </summary>
        internal bool Checked
        {
            get
            {
                return button.Checked;
            }
        }

        internal RadioButton Button
        {
            get { return button; }
        }

        #endregion
    }
}
