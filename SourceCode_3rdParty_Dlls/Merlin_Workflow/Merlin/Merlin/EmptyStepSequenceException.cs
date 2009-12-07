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

namespace Merlin
{
    /// <summary>
    /// Thrown when the step sequence provided to the
    /// wizard controller contains no steps.
    /// </summary>
    public class EmptyStepSequenceException
        : Exception
    {
        private const string message = "Attempting to execute wizard with 0 steps";
        /// <summary>
        /// Creates a new instance of EmptyStepSequenceException
        /// </summary>
        public EmptyStepSequenceException()
            : base(message)
        {
        }
    }
}
