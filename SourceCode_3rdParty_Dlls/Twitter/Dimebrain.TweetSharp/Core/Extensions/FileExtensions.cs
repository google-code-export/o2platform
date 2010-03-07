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


#if !SILVERLIGHT
using System.Drawing.Imaging;
#endif

namespace TweetSharp.Core.Extensions
{
    internal static class FileExtensions
    {
        public static int Megabytes(this int value)
        {
            return value*1024*1024;
        }

        public static int Kilobytes(this int value)
        {
            return value*1024;
        }

        public static int Bytes(this int value)
        {
            return value;
        }

#if !SILVERLIGHT
        public static string ToContentType(this ImageFormat format)
        {
            if(format == ImageFormat.Jpeg
#if !Smartphone
                || format.Guid == "{b96b3cae-0728-11d3-9d7b-0000f81ef32e}".AsGuid()
#endif
                )
            {
                return "image/jpeg";
            }

            if (format == ImageFormat.Gif 
#if !Smartphone
                || format.Guid == "{b96b3cb0-0728-11d3-9d7b-0000f81ef32e}".AsGuid()
#endif
                )
            {
                return "image/gif";
            }

            if (format == ImageFormat.Png 
#if !Smartphone
                || format.Guid == "{b96b3caf-0728-11d3-9d7b-0000f81ef32e}".AsGuid()
#endif
                )
            {
                return "image/png";
            }

            if (format == ImageFormat.Bmp 
#if !Smartphone
                || format.Guid == "{b96b3cab-0728-11d3-9d7b-0000f81ef32e}".AsGuid() 
                || format.Guid == "{b96b3caa-0728-11d3-9d7b-0000f81ef32e}".AsGuid()
#endif
                )
            {
                return "image/bmp";
            }

#if !Smartphone
            return format == ImageFormat.Tiff ? "image/tiff" : "application/octet-stream";
#else
            return "application/octet-stream";
#endif
        }
#endif
    }
}