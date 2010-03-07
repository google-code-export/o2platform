#region License

// TweetSharp
// Copyright (c) 2010 Daniel Crenna and Jason Diller
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
using TweetSharp.Model;

namespace TweetSharp.Fluent
{
#if(!SILVERLIGHT)
    [Serializable]
#endif

    public class FluentTwitterProfile : IFluentTwitterProfile
    {
        #region IFluentTwitterProfile Members

        public string ProfileName { get; set; }
        public string ProfileUrl { get; set; }
        public string ProfileLocation { get; set; }
        public string ProfileDescription { get; set; }
        public TwitterDeliveryDevice? ProfileDeliveryDevice { get; set; }
        public string ProfileBackgroundColor { get; set; }
        public string ProfileTextColor { get; set; }
        public string ProfileLinkColor { get; set; }
        public string ProfileSidebarFillColor { get; set; }
        public string ProfileSidebarBorderColor { get; set; }
        public string ProfileImagePath { get; set; }
        public string ProfileBackgroundImagePath { get; set; }

        #endregion
    }
}