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

using mshtml;
using SHDocVw;
using WatiN.Core.Interfaces;

namespace WatiN.Core
{
	internal class FrameByIndexProcessor : IWebBrowser2Processor
	{
		private HTMLDocument htmlDocument;
		private int index;
		private int counter = 0;
		private IWebBrowser2 iWebBrowser2 = null;

		public FrameByIndexProcessor(int index, HTMLDocument htmlDocument)
		{
			this.index = index;
			this.htmlDocument = htmlDocument;
		}

		public HTMLDocument HTMLDocument()
		{
			return htmlDocument;
		}

		public void Process(IWebBrowser2 webBrowser2)
		{
			if (counter == index)
			{
				iWebBrowser2 = webBrowser2;
			}
			counter++;
		}

		public bool Continue()
		{
			return (iWebBrowser2 == null);
		}

		public IWebBrowser2 IWebBrowser2()
		{
			return iWebBrowser2;
		}
	}
}