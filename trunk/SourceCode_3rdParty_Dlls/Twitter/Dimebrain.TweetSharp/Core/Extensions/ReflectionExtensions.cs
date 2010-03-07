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
using System.Collections.Generic;
using System.Reflection;
using TweetSharp.Core.Attributes;

namespace TweetSharp.Core.Extensions
{
    internal static class ReflectionExtensions
    {
        public static IEnumerable<T> GetCustomAttributes<T>(this PropertyInfo info, bool inherit) where T : class
        {
            var attributes = info.GetCustomAttributes(typeof (T), inherit);
            return attributes.ToEnumerable<T>();
        }

        public static bool RequiresAuthentication(this Enum value, bool isMandatory)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes =
                (RequiresAuthenticationAttribute[])
                field.GetCustomAttributes(typeof (RequiresAuthenticationAttribute), false);

            if (attributes.Length > 0)
            {
                var attribute = attributes[0];
                if (isMandatory) return attribute.IsRequired;
                return !attribute.IsRequired;
            }

            return false;
        }

        public static AuthenticationScheme GetScheme(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes =
                (RequiresAuthenticationAttribute[])
                field.GetCustomAttributes(typeof (RequiresAuthenticationAttribute), false);
            if (attributes.Length > 0)
            {
                var attribute = attributes[0];
                return attribute.Scheme;
            }
            return AuthenticationScheme.None;
        }

        public static bool RequiresApiKey(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes =
                (RequiresApiKeyAttribute[]) field.GetCustomAttributes(typeof (RequiresApiKeyAttribute), false);

            return attributes.Length > 0;
        }
    }
}