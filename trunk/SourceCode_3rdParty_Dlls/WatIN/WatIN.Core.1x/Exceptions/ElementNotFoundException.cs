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

namespace WatiN.Core.Exceptions
{
	/// <summary>
	/// Thrown if the searched for element can't be found.
	/// </summary>
	public class ElementNotFoundException : WatiNException
	{
        [Obsolete("Use new constructor which also accepts a url")]
		public ElementNotFoundException(string tagName, string criteria) :
			base(CreateMessage(tagName, criteria, null, null)) {}

		public ElementNotFoundException(string tagName, string criteria, string url) :
			base(CreateMessage(tagName, criteria, url, null)) {}

        [Obsolete("Use new constructor which also accepts a url")]
        public ElementNotFoundException(string tagName, string criteria, Exception innerexception) :
			base(CreateMessage(tagName, criteria, null, innerexception.Message), innerexception) {}

		public ElementNotFoundException(string tagName, string criteria, string url, Exception innerexception) :
			base(CreateMessage(tagName, criteria, url, innerexception.Message), innerexception) {}

		private static string CreateMessage(string tagName, string criteria, string url, string innerException)
		{
			string message = "Could not find " + UtilityClass.ToString(tagName) + " element tag";

            if (UtilityClass.IsNotNullOrEmpty(criteria))
			{
				message += " matching criteria: " + criteria;
			}

            if (UtilityClass.IsNotNullOrEmpty(url))
            {
                message += " at " + url;
            }
            
			if (UtilityClass.IsNotNullOrEmpty(innerException))
			{
				message += " (inner exception: "+ innerException + ")";
			}

			
			return message;
		}
	}
}