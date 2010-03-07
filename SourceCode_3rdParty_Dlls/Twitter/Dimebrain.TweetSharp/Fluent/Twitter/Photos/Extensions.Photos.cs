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
        /// Posts a photo inline to TwitPic during a request before making an API call to Twitter.
        /// This method should only be used with updating status, retweeting, or sending direct messages,
        /// as the new photo's URL is injected in the outgoing message; otherwise, the photo is posted but 
        /// the URL is lost.
        /// </summary>
        /// <param name="instance">The current query expression</param>
        /// <param name="path">A path to the image</param>
        /// <returns>The current query expression</returns>
        public static IFluentTwitter PostPhoto(this ITwitterPhotos instance, string path)
        {
            instance.Root.Parameters.PostImageProvider = SendPhotoServiceProvider.TwitPic;
            instance.Root.Parameters.PostImagePath = path;
            return instance.Root;
        }

        /// <summary>
        /// Posts a photo inline during a request to a given provider, before making an API call to Twitter.
        /// This method should only be used with updating status, retweeting, or sending direct messages,
        /// as the new photo's URL is injected in the outgoing message; otherwise, the photo is posted but 
        /// the URL is lost.
        /// </summary>
        /// <param name="instance">The current query expression</param>
        /// <param name="path">A path to the image</param>
        /// <param name="provider">A photo posting service provider</param>
        /// <returns>The current query expression</returns>
        public static IFluentTwitter PostPhoto(this ITwitterPhotos instance, string path,
                                               SendPhotoServiceProvider provider)
        {
            instance.Root.Parameters.PostImageProvider = provider;
            instance.Root.Parameters.PostImagePath = path;
            return instance.Root;
        }
    }
}