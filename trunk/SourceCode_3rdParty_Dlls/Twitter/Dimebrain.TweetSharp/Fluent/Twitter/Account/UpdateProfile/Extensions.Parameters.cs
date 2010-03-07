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
using TweetSharp.Core.Attributes;

#if !Smartphone && !SILVERLIGHT
using System.Net.Mail;
#endif

namespace TweetSharp.Fluent
{
    partial class Extensions
    {
        [CharacterLimit(40)]
        public static ITwitterAccountUpdateProfile UpdateName(this ITwitterAccountUpdateProfile instance, string name)
        {
            instance.Root.Profile.ProfileName = name;
            return instance;
        }

#if !Smartphone && !SILVERLIGHT
        [CharacterLimit(40)]
        public static ITwitterAccountUpdateProfile UpdateEmail(this ITwitterAccountUpdateProfile instance,
                                                                MailAddress email)
        {
            instance.Root.Parameters.Email = email.Address;
            return instance;
        }
#endif

        [CharacterLimit(40)]
        public static ITwitterAccountUpdateProfile UpdateEmail(this ITwitterAccountUpdateProfile instance,
                                                               string email)
        {
            instance.Root.Parameters.Email = email;
            return instance;
        }

        [CharacterLimit(100)]
        public static ITwitterAccountUpdateProfile UpdateUrl(this ITwitterAccountUpdateProfile instance, string url)
        {
            instance.Root.Profile.ProfileUrl = url;
            return instance;
        }

        [CharacterLimit(100)]
        public static ITwitterAccountUpdateProfile UpdateUrl(this ITwitterAccountUpdateProfile instance, Uri uri)
        {
            instance.Root.Profile.ProfileUrl = uri.ToString();
            return instance;
        }

        [CharacterLimit(30)]
        public static ITwitterAccountUpdateProfile UpdateLocation(this ITwitterAccountUpdateProfile instance,
                                                                  string location)
        {
            instance.Root.Profile.ProfileLocation = location;
            return instance;
        }

        [CharacterLimit(160)]
        public static ITwitterAccountUpdateProfile UpdateDescription(this ITwitterAccountUpdateProfile instance,
                                                                     string description)
        {
            instance.Root.Profile.ProfileDescription = description;
            return instance;
        }
    }
}