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

#if !Mono

using System;
using System.Globalization;
#if(!SILVERLIGHT)
using System.Drawing;
#else
using System.Windows.Media;
#endif

namespace TweetSharp.Core.Extensions
{
    // note System.Drawing was never supported in ASP.NET
    internal static class ColorExtensions
    {
        public static string ToHexColor(this Color color)
        {
            return color.ToHexColor(false, false);
        }

        public static string ToHexColor(this Color color, bool includeAlpha, bool includeHash)
        {
            var a = Convert.ToInt32(color.A).ToHexDigit();
            var r = Convert.ToInt32(color.R).ToHexDigit();
            var g = Convert.ToInt32(color.G).ToHexDigit();
            var b = Convert.ToInt32(color.B).ToHexDigit();

            return includeAlpha && includeHash
                       ? String.Concat("#", a, r, g, b)
                       : includeAlpha
                             ? String.Concat(a, r, g, b)
                             : includeHash ? String.Concat("#", r, g, b) : String.Concat(r, g, b);
        }

#if !Smartphone && !SILVERLIGHT
        public static KnownColor ToKnownColor(this string hex)
        {
            return hex.ToColor().ToKnownColor();
        }
#endif

        public static Color ToColor(this string hex)
        {
            hex = hex.Replace("#", String.Empty);

            switch (hex.Length)
            {
                case 8:
                    break;
                case 6:
                    hex = hex.Insert(0, "ff");
                    break;
                default:
#if SILVERLIGHT
                    return Colors.Transparent;
#else
                    return Color.Transparent;
#endif
            }

#if !Smartphone
            var a = Byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
#endif
            var r = Byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
            var g = Byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
            var b = Byte.Parse(hex.Substring(6, 2), NumberStyles.HexNumber);

#if !Smartphone
            var color = Color.FromArgb(a, r, g, b);
#else
            var color = Color.FromArgb(r, g, b);
#endif
            return color;
        }

        public static string ToHexDigit(this int value)
        {
            return "{0:x2}".FormatWith(value);
        }
    }
}

#endif