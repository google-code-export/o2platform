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

namespace TweetSharp.Fluent
{
    public static class ITwitterSavedSearchesExtensions
    {
        [RequiresAuthentication]
        public static ITwitterSavedSearchesList List(this ITwitterSavedSearches instance)
        {
            return new TwitterSavedSearchesList(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterSavedSearchesCreate Create(this ITwitterSavedSearches instance, string query)
        {
            instance.Root.Parameters.Action = "create";
            instance.Root.SearchParameters.SearchPhrase = query;

            return new TwitterSavedSearchesCreate(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterSavedSearchesCreate Create(this ITwitterSavedSearches instance, ITwitterSearchQuery query)
        {
            instance.Root.Parameters.Action = "create";

            // Clean query URL
            var url = query.AsUrl();
            var index = url.IndexOf("?q=");
            var value = url.Substring(index + 3);

            // [DC]: Don't URL encode this even though it's a URL; Twitter will hard-code %23 hashtags
            value = Uri.UnescapeDataString(value);

            instance.Root.SearchParameters.SearchPhrase = value;
            return new TwitterSavedSearchesCreate(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterSavedSearchesDestroy Delete(this ITwitterSavedSearches instance, int id)
        {
            instance.Root.Parameters.Action = "destroy";
            instance.Root.Parameters.Id = id;
            return new TwitterSavedSearchesDestroy(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterSavedSearchesDestroy Delete(this ITwitterSavedSearches instance, long id)
        {
            instance.Root.Parameters.Action = "destroy";
            instance.Root.Parameters.Id = id;
            return new TwitterSavedSearchesDestroy(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterSavedSearchesShow Show(this ITwitterSavedSearches instance, int id)
        {
            instance.Root.Parameters.Action = "show";
            instance.Root.Parameters.UserId = id;
            return new TwitterSavedSearchesShow(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterSavedSearchesShow Show(this ITwitterSavedSearches instance, long id)
        {
            instance.Root.Parameters.Action = "show";
            instance.Root.Parameters.UserId = id;
            return new TwitterSavedSearchesShow(instance.Root);
        }
    }
}