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

#if(!SILVERLIGHT)
using System.Drawing.Imaging;
using System.Xml.Linq;
using System;
using System.Text.RegularExpressions;
using TweetSharp.Core.Extensions;
using TweetSharp.Core.Web;
using TweetSharp.Core.Web.Query;
#endif

namespace TweetSharp.Fluent.Twitter.Services
{
#if !SILVERLIGHT
    [Serializable]
    internal static class SendPhotoService
    {
        public static string SendPhoto(WebQueryBase query, SendPhotoServiceProvider provider, ImageFormat format, string path, string username, string password)
        {
            string response;
            switch (provider)
            {
                case SendPhotoServiceProvider.TwitPic:
                    // http://twitpic.com/api.do
                    response = SendPhoto(format, query, path, "http://twitpic.com/api/upload", username, password);
                    break;
                case SendPhotoServiceProvider.YFrog:
                    // http://yfrog.com/upload_and_post.html
                    response = SendPhoto(format, query, path, "http://yfrog.com/api/upload", username, password);
                    break;
                case SendPhotoServiceProvider.TwitGoo:
                    // http://twitgoo.com/upload_and_post.html
                    response = SendPhoto(format, query, path, "http://twitgoo.com/api/upload", username, password);
                    break;
                default:
                    throw new NotSupportedException("Unknown photo service provider specified");
            }

            if (!response.Contains("mediaurl"))
            {
                return null;
            }

            var match = Regex.Match(response, "<mediaurl[^>]*>(.*?)</mediaurl>");
            var mediaUrl = XElement.Parse(match.Value).Value;

            return mediaUrl;
        }

        public static string SendPhoto(ImageFormat format, WebQueryBase query, string path, string url, string username, string password)
        {
            var contentType = format.ToContentType();

            var mediaParameter = HttpPostParameter.CreateFile("media", "photo", path, contentType);
            var usernameParameter = new HttpPostParameter("username", username);
            var passwordParameter = new HttpPostParameter("password", password);

            var parameters = new[] { mediaParameter, usernameParameter, passwordParameter };

            return query.Request(url, parameters);
        }
    }
#endif
}