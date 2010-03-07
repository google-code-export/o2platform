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
using System.Globalization;
using Newtonsoft.Json;

namespace TweetSharp.Model.Yammer.Converters
{
    internal class YammerDateConverter : YammerConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            var value = reader.Value.ToString();
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            DateTime date;
            //yammer claims to use rfc3339, only the xml return types do. json uses a different format
            if (Rfc3339DateTime.TryParse(value, out date))
            {
                return date;
            }
            //"2009/07/07 02:08:11 +0000"
            const string yammerJsonDateFormat = "yyyy/MM/dd HH:mm:ss zzzzz";
            if (DateTime.TryParseExact(value, new[] {yammerJsonDateFormat}, CultureInfo.InvariantCulture,
                                       DateTimeStyles.None, out date))
            {
                return date;
            }
            return default(DateTime);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is DateTime)
            {
                var dateTime = (DateTime) value;
                var converted = Rfc3339DateTime.ToString(dateTime);

                writer.WriteValue(converted);
            }
        }

        public override bool CanConvert(Type objectType)
        {
            var t = (IsNullableType(objectType))
                        ? Nullable.GetUnderlyingType(objectType)
                        : objectType;
#if !Smartphone
            return typeof (DateTime).IsAssignableFrom(t) ||
                   typeof (DateTimeOffset).IsAssignableFrom(t);
#else
            return typeof (DateTime).IsAssignableFrom(t);

#endif
        }

        #region Nested type: Rfc3339DateTime

        /// <summary>
        /// Provides methods for converting <see cref="DateTime"/> structures to and from the equivalent RFC 3339 string representation.
        /// </summary>
        private static class Rfc3339DateTime
        {
            //array of formats that RFC 3339 date-time representations conform to.

            // the DateTime format string for representing a DateTime in the RFC 3339 format.

            private const string format = "yyyy-MM-dd'T'HH:mm:ss.fffK";
            private static string[] formats = new string[0];

            private static string Rfc3339DateTimeFormat
            {
                get { return format; }
            }


            private static string[] Rfc3339DateTimePatterns
            {
                get
                {
                    if (formats.Length > 0)
                    {
                        return formats;
                    }
                    else
                    {
                        formats = new string[11];

                        // Rfc3339DateTimePatterns
                        formats[0] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK";
                        formats[1] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffffffK";
                        formats[2] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffK";
                        formats[3] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffffK";
                        formats[4] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK";
                        formats[5] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffK";
                        formats[6] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fK";
                        formats[7] = "yyyy'-'MM'-'dd'T'HH':'mm':'ssK";

                        // Fall back patterns
                        formats[8] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK"; // RoundtripDateTimePattern
                        formats[9] = DateTimeFormatInfo.InvariantInfo.UniversalSortableDateTimePattern;
                        formats[10] = DateTimeFormatInfo.InvariantInfo.SortableDateTimePattern;

                        return formats;
                    }
                }
            }


            /// <summary>
            /// Converts the specified string representation of a date and time to its <see cref="DateTime"/> equivalent.
            /// </summary>
            /// <param name="s">A string containing a date and time to convert.</param>
            /// <returns>A <see cref="DateTime"/> equivalent to the date and time contained in <paramref name="s"/>.</returns>
            /// <remarks>
            /// The string <paramref name="s"/> is parsed using formatting information in the <see cref="DateTimeFormatInfo.InvariantInfo"/> object.
            /// </remarks>
            /// <exception cref="ArgumentNullException"><paramref name="s"/> is a <b>null</b> reference (Nothing in Visual Basic).</exception>
            /// <exception cref="FormatException"><paramref name="s"/> does not contain a valid RFC 3339 string representation of a date and time.</exception>
            public static DateTime Parse(string s)
            {
                if (s == null)
                {
                    throw new ArgumentNullException("s");
                }

                DateTime result;
                if (TryParse(s, out result))
                {
                    return result;
                }
                else
                {
                    throw new FormatException(String.Format(null,
                                                            "{0} is not a valid RFC 3339 string representation of a date and time.",
                                                            s));
                }
            }


            /// <summary>
            /// Converts the value of the specified <see cref="DateTime"/> object to its equivalent string representation.
            /// </summary>
            /// <param name="utcDateTime">The Coordinated Universal Time (UTC) <see cref="DateTime"/> to convert.</param>
            /// <returns>A RFC 3339 string representation of the value of the <paramref name="utcDateTime"/>.</returns>
            /// <remarks>
            /// <para>
            /// This method returns a string representation of the <paramref name="utcDateTime"/> that 
            /// is precise to the three most significant digits of the seconds fraction; that is, it represents 
            /// the milliseconds in a date and time value.
            /// </para>
            /// <para>
            /// While it is possible to display higher precision fractions of a second component of a time value, 
            /// that value may not be meaningful. The precision of date and time values depends on the resolution 
            /// of the system clock. On Windows NT 3.5 and later, and Windows Vista operating systems, the clock's 
            /// resolution is approximately 10-15 milliseconds.
            /// </para>
            /// </remarks>
            /// <exception cref="ArgumentException">The specified <paramref name="utcDateTime"/> object does not represent a <see cref="DateTimeKind.Utc">Coordinated Universal Time (UTC)</see> value.</exception>
            public static string ToString(DateTime utcDateTime)
            {
                if (utcDateTime.Kind != DateTimeKind.Utc)
                {
                    throw new ArgumentException("utcDateTime");
                }

                return utcDateTime.ToString(Rfc3339DateTimeFormat, DateTimeFormatInfo.InvariantInfo);
            }


            /// <summary>
            /// Converts the specified string representation of a date and time to its <see cref="DateTime"/> equivalent.
            /// </summary>
            /// <param name="s">A string containing a date and time to convert.</param>
            /// <param name="result">
            /// When this method returns, contains the <see cref="DateTime"/> value equivalent to the date and time 
            /// contained in <paramref name="s"/>, if the conversion succeeded, 
            /// or <see cref="DateTime.MinValue">MinValue</see> if the conversion failed. 
            /// The conversion fails if the s parameter is a <b>null</b> reference (Nothing in Visual Basic), 
            /// or does not contain a valid string representation of a date and time. 
            /// This parameter is passed uninitialized.
            /// </param>
            /// <returns><b>true</b> if the <paramref name="s"/> parameter was converted successfully; otherwise, <b>false</b>.</returns>
            /// <remarks>
            /// The string <paramref name="s"/> is parsed using formatting information in the <see cref="DateTimeFormatInfo.InvariantInfo"/> object.
            /// </remarks>
            public static bool TryParse(string s, out DateTime result)
            {
                //------------------------------------------------------------
                //  Attempt to convert string representation
                //------------------------------------------------------------
                var wasConverted = false;
                result = DateTime.MinValue;

                if (!String.IsNullOrEmpty(s))
                {
                    DateTime parseResult;
                    if (DateTime.TryParseExact(s, Rfc3339DateTimePatterns, DateTimeFormatInfo.InvariantInfo,
                                               DateTimeStyles.AdjustToUniversal, out parseResult))
                    {
                        result = DateTime.SpecifyKind(parseResult, DateTimeKind.Utc);
                        wasConverted = true;
                    }
                }

                return wasConverted;
            }
        }

        #endregion
    }
}