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

namespace TweetSharp.Fluent
{
    partial class Extensions
    {
        /// <summary>
        /// Searches for tweets containing the given phrase.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="phrase"></param>
        /// <returns></returns>
        public static ITwitterSearchQuery Containing(this ITwitterSearchQuery instance, string phrase)
        {
            instance.Root.SearchParameters.SearchPhrase = phrase;
            return instance;
        }

        /// <summary>
        /// Searches for tweets that do not contain the given phrase.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="phrase"></param>
        /// <returns></returns>
        public static ITwitterSearchQuery NotContaining(this ITwitterSearchQuery instance, string phrase)
        {
            instance.Root.SearchParameters.SearchWithoutPhrase = phrase;
            return instance;
        }

        /// <summary>
        /// Searches for tweets from the given user.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="screenName"></param>
        /// <returns></returns>
        public static ITwitterSearchQuery FromUser(this ITwitterSearchQuery instance, string screenName)
        {
            instance.Root.SearchParameters.SearchFromUser = screenName;
            return instance;
        }

        /// <summary>
        /// Searches for tweets to the given user.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="screenName"></param>
        /// <returns></returns>
        public static ITwitterSearchQuery ToUser(this ITwitterSearchQuery instance, string screenName)
        {
            instance.Root.SearchParameters.SearchToUser = screenName;
            return instance;
        }

        /// <summary>
        /// Searches for tweets containing the given hashtag.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="hashTag"></param>
        /// <returns></returns>
        public static ITwitterSearchQuery ContainingHashTag(this ITwitterSearchQuery instance, string hashTag)
        {
            instance.Root.SearchParameters.SearchHashTag = hashTag;
            return instance;
        }

        /// <summary>
        /// Searches for tweets in reference to the given user.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="screenName"></param>
        /// <returns></returns>
        public static ITwitterSearchQuery ReferencingUser(this ITwitterSearchQuery instance, string screenName)
        {
            instance.Root.SearchParameters.SearchReferences = screenName;
            return instance;
        }

        /// <summary>
        /// Searches for tweets with negative wording.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static ITwitterSearchQuery WithNegativity(this ITwitterSearchQuery instance)
        {
            instance.Root.SearchParameters.SearchNegativity = true;
            instance.Root.SearchParameters.SearchPositivity = false;
            return instance;
        }

        /// <summary>
        /// Searches for tweets with positive wording.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static ITwitterSearchQuery WithPositivity(this ITwitterSearchQuery instance)
        {
            instance.Root.SearchParameters.SearchPositivity = true;
            instance.Root.SearchParameters.SearchNegativity = false;
            return instance;
        }

        /// <summary>
        /// Searches for tweets with neutral wording. Used as a way to
        /// reset previous sentiment selections on an existing query.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static ITwitterSearchQuery WithNeutrality(this ITwitterSearchQuery instance)
        {
            instance.Root.SearchParameters.SearchPositivity = false;
            instance.Root.SearchParameters.SearchNegativity = false;
            return instance;
        }

        /// <summary>
        /// Searches for tweets containing a question
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static ITwitterSearchQuery WithQuestion(this ITwitterSearchQuery instance)
        {
            instance.Root.SearchParameters.SearchQuestion = true;
            return instance;
        }

        /// <summary>
        /// Searches for tweets that contain embedded links.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static ITwitterSearchQuery ContainingLinks(this ITwitterSearchQuery instance)
        {
            instance.Root.SearchParameters.SearchContainingLinks = true;
            return instance;
        }
    }
}