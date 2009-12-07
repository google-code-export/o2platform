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

namespace Merlin
{

    /// <summary>
    /// Thrown when a conditional step is found at the end of a step sequence.
    /// </summary>
    public class ConditionalStepAtEndException 
        : Exception
    {

        /// <summary>
        /// Creates a new instance of ConditionalStepAtEndException
        /// </summary>
        public ConditionalStepAtEndException()
            : base("Conditional step found at the end of a step sequence")
        {
        }
    }
}
