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

using TweetSharp.Core.Extensions;
#if(SILVERLIGHT)
using System.Windows.Media;
#else
using System.Drawing;
#endif

namespace TweetSharp.Fluent
{
    partial class Extensions
    {
#if !Mono
        public static ITwitterAccountUpdateProfileColors UpdateProfileBackgroundColor(
            this ITwitterAccountUpdateProfileColors instance, Color color)
        {
            instance.Root.Profile.ProfileBackgroundColor = color.ToHexColor();
            return instance;
        }


        public static ITwitterAccountUpdateProfileColors UpdateProfileTextColor(
            this ITwitterAccountUpdateProfileColors instance, Color color)
        {
            instance.Root.Profile.ProfileTextColor = color.ToHexColor();
            return instance;
        }

        public static ITwitterAccountUpdateProfileColors UpdateProfileLinkColor(
            this ITwitterAccountUpdateProfileColors instance, Color color)
        {
            instance.Root.Profile.ProfileLinkColor = color.ToHexColor();
            return instance;
        }

        public static ITwitterAccountUpdateProfileColors UpdateProfileSidebarFillColor(
            this ITwitterAccountUpdateProfileColors instance, Color color)
        {
            instance.Root.Profile.ProfileSidebarFillColor = color.ToHexColor();
            return instance;
        }

        public static ITwitterAccountUpdateProfileColors UpdateProfileSidebarBorderColor(
            this ITwitterAccountUpdateProfileColors instance, Color color)
        {
            instance.Root.Profile.ProfileSidebarBorderColor = color.ToHexColor();
            return instance;
        }
#endif

        public static ITwitterAccountUpdateProfileColors UpdateProfileBackgroundColor(
            this ITwitterAccountUpdateProfileColors instance, string color)
        {
            instance.Root.Profile.ProfileBackgroundColor = color.Replace("#", "");
            return instance;
        }

        public static ITwitterAccountUpdateProfileColors UpdateProfileTextColor(
            this ITwitterAccountUpdateProfileColors instance, string color)
        {
            instance.Root.Profile.ProfileTextColor = color.Replace("#", "");
            return instance;
        }

        public static ITwitterAccountUpdateProfileColors UpdateProfileLinkColor(
            this ITwitterAccountUpdateProfileColors instance, string color)
        {
            instance.Root.Profile.ProfileLinkColor = color.Replace("#", "");
            return instance;
        }

        public static ITwitterAccountUpdateProfileColors UpdateProfileSidebarFillColor(
            this ITwitterAccountUpdateProfileColors instance, string color)
        {
            instance.Root.Profile.ProfileSidebarFillColor = color.Replace("#", "");
            return instance;
        }

        public static ITwitterAccountUpdateProfileColors UpdateProfileSidebarBorderColor(
            this ITwitterAccountUpdateProfileColors instance, string color)
        {
            instance.Root.Profile.ProfileSidebarBorderColor = color.Replace("#", "");
            return instance;
        }
    }
}