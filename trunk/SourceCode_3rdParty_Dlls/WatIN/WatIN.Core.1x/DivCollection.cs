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

using System;
using System.Collections;
using WatiN.Core.Constraints;
using WatiN.Core.Interfaces;

namespace WatiN.Core
{
	/// <summary>
	/// A typed collection of <see cref="Div" /> instances within a <see cref="Document"/> or <see cref="Element"/>.
	/// </summary>
#if NET11
	public class DivCollection : BaseElementCollection
#else
    public class DivCollection : BaseElementCollection<Div>
#endif
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DivCollection"/> class.
		/// Mainly used by WatiN internally.
		/// </summary>
		/// <param name="domContainer">The DOM container.</param>
		/// <param name="finder">The finder.</param>
		public DivCollection(DomContainer domContainer, INativeElementFinder finder) : base(domContainer, finder, new CreateElementInstance(Div.New)) {}

		/// <summary>
		/// Initializes a new instance of the <see cref="DivCollection"/> class.
		/// Mainly used by WatiN internally.
		/// </summary>
		/// <param name="domContainer">The DOM container.</param>
		/// <param name="elements">The elements.</param>
		public DivCollection(DomContainer domContainer, ArrayList elements) : base(domContainer, elements, new CreateElementInstance(Div.New)) {}

		/// <summary>
		/// Gets the <see cref="Div"/> at the specified index.
		/// </summary>
		/// <value></value>
		public Div this[int index]
		{
			get { return (Div)ElementsTyped(index); }
		}

		public DivCollection Filter(BaseConstraint findBy)
		{
			return new DivCollection(domContainer, DoFilter(findBy));
		}

#if !NET11
        public DivCollection Filter(Predicate<Div> predicate)
        {
            return new DivCollection(domContainer, DoFilter(Find.ByElement(predicate)));
        }
#endif

	}
}