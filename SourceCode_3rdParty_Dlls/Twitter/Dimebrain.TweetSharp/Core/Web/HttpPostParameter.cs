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

namespace TweetSharp.Core.Web
{
    ///<summary>
    /// A <see cref="WebParameter" /> that maps to HTTP POST
    /// parameters in an HTTP body.
    ///</summary>
    public class HttpPostParameter : WebParameter
    {
        public HttpPostParameter(string name, string value) : base(name, value)
        {
        }

        ///<summary>
        /// The HTTP POST parameter type.
        ///</summary>
        public HttpPostParameterType Type { get; private set; }

        /// <summary>
        /// The physical file name.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// The path to the physical file.
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        /// The content type of the file.
        /// </summary>
        public string ContentType { get; private set; }

        ///<summary>
        /// Creates a new HTTP POST parameter representing
        /// a file to transfer as multi-part form data.
        ///</summary>
        ///<param name="name">The logical name of the file</param>
        ///<param name="fileName">The physical file name</param>
        ///<param name="filePath">The path to the file</param>
        ///<param name="contentType">The file's content type</param>
        ///<returns>The created HTTP POST parameter</returns>
        public static HttpPostParameter CreateFile(string name, string fileName, string filePath, string contentType)
        {
            var parameter = new HttpPostParameter(name, string.Empty)
                                {
                                    Type = HttpPostParameterType.File,
                                    FileName = fileName,
                                    FilePath = filePath,
                                    ContentType = contentType,
                                };

            return parameter;
        }
    }
}