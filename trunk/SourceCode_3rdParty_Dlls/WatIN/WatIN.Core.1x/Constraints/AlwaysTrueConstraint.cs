#region WatiN Copyright (C) 2006-2008 Jeroen van Menen

//Copyright 2006-2008 Jeroen van Menen
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

#endregion Copyright

using WatiN.Core.Interfaces;

namespace WatiN.Core.Constraints
{
	/// <summary>
	/// This class is only used in the ElementsSupport Class to 
	/// create a collection of all elements.
	/// </summary>
	public class AlwaysTrueConstraint : BaseConstraint
	{
        /// <summary>
        /// Does the compare without calling <see cref="BaseConstraint.LockCompare"/> and <see cref="BaseConstraint.UnLockCompare"/>.
        /// </summary>
        /// <param name="attributeBag">The attribute bag.</param>
        /// <returns>Will always return <c>true</c></returns>
		protected override bool DoCompare(IAttributeBag attributeBag)
		{
			return true;
		}

        /// <summary>
        /// Writes out the constraint into a <see cref="string"/>.
        /// </summary>
        /// <returns>The constraint text</returns>
		public override string ConstraintToString()
		{
			return "any";
		}
	}
}
