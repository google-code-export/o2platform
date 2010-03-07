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
using System.Text;
using TweetSharp.Core.Extensions;

namespace TweetSharp.Extensions
{
    partial class TimeExtensions
    {
        internal static string Ones(this int value)
        {
            if (value == 0)
            {
                return "zero";
            }

            if (value < 0)
            {
                return "negative {0}".FormatWith(Math.Abs(value).Ones());
            }

            switch (value)
            {
                case 1:
                    return "one";
                case 2:
                    return "two";
                case 3:
                    return "three";
                case 4:
                    return "four";
                case 5:
                    return "five";
                case 6:
                    return "six";
                case 7:
                    return "seven";
                case 8:
                    return "eight";
                case 9:
                    return "nine";
                default:
                    return value.Tens();
            }
        }

        internal static string Tens(this int value)
        {
            if (value == 0)
            {
                return "zero";
            }

            if (value < 0)
            {
                return "negative {0}".FormatWith(Math.Abs(value).Tens());
            }

            if (value < 10)
            {
                return value.Ones();
            }

            switch (value)
            {
                case 10:
                    return "ten";
                case 11:
                    return "eleven";
                case 12:
                    return "twelve";
                case 13:
                    return "thirteen";
                case 14:
                    return "fourteen";
                case 15:
                    return "fifteen";
                case 16:
                    return "sixteen";
                case 17:
                    return "seventeen";
                case 18:
                    return "eighteen";
                case 19:
                    return "nineteen";
                case 20:
                    return "twenty";
                case 30:
                    return "thirty";
                case 40:
                    return "forty";
                case 50:
                    return "fifty";
                case 60:
                    return "sixty";
                case 70:
                    return "seventy";
                case 80:
                    return "eighty";
                case 90:
                    return "ninety";
                default:
                    if (value > 0)
                    {
                        if (value > 99)
                        {
                            return value.ToWord();
                        }

                        var mask = value.ToString();
                        var head = Convert.ToInt32("{0}0".FormatWith(mask.Substring(0, 1)), CultureInfo.InvariantCulture);
                        var remainder = Convert.ToInt32(mask.Substring(1), CultureInfo.InvariantCulture);

                        return "{0}-{1}".FormatWith(head.Tens(), remainder.Ones());
                    }
                    return "zero";
            }
        }

        internal static string WordSplit(this string format, string target, int length)
        {
            var word = Convert.ToInt32(target.Substring(0, length), CultureInfo.InvariantCulture).ToWord();
            var head = format.FormatWith(word);

            var tail = Convert.ToInt32(target.Substring(length), CultureInfo.InvariantCulture).ToWord();
            return String.Concat(head, tail);
        }

        public static string ToWord(this int value)
        {
            if (value == 0)
            {
                return "zero";
            }

            if (value < 0)
            {
                return "negative {0}".FormatWith(Math.Abs(value).ToWord());
            }

            if (value < 10)
            {
                return value.Ones();
            }

            if (value < 100)
            {
                return value.Tens();
            }

            var mask = value.ToString();
            var length = mask.Length;

            switch (length)
            {
                case 3:
                    {
                        return "{0} hundred ".WordSplit(mask, 1);
                    }
                case 4:
                case 5:
                case 6:
                    {
                        return "{0} thousand ".WordSplit(mask, (length - 4) + 1);
                    }
                case 7:
                case 8:
                case 9:
                    {
                        return "{0} million ".WordSplit(mask, (length - 7) + 1);
                    }
                default:
                    return "too many";
            }
        }

        public static string ToRelativeTime(this DateTime time)
        {
            return time.ToRelativeTime(false);
        }

        public static string ToRelativeTime(this DateTime time, bool numbersAsWords)
        {
            var sb = new StringBuilder();
            //if incoming time is UTC, compare to UTC, otherwise use local time
            var now = time.Kind == DateTimeKind.Utc ? DateTime.Now.ToUniversalTime() : DateTime.Now;
            var isComplete = false;

            if (time.Before(now))
            {
                var timePassed = time.Passed();

                if (timePassed.AtLeast(1.Days()))
                {
                    if (timePassed.AtLeast(3.Months()) && sb.IsEmpty())
                    {
                        sb.Append("awhile");
                    }
                    if (timePassed.AtLeast(2.Months()) && sb.IsEmpty())
                    {
                        sb.Append("a few months");
                    }
                    if (timePassed.AtLeast(1.Months()) && sb.IsEmpty())
                    {
                        sb.Append("about a month");
                    }
                    if (timePassed.AtLeast(2.Weeks()) && sb.IsEmpty())
                    {
                        sb.Append("a few weeks");
                    }
                    if (timePassed.MoreThan(1.Weeks()) && sb.IsEmpty())
                    {
                        sb.Append("about a week");
                    }
                    if (timePassed == 1.Weeks() && sb.IsEmpty())
                    {
                        sb.Append("a week");
                    }
                    if ((timePassed <= 1.Days() || timePassed.Days == 1) && sb.IsEmpty())
                    {
                        sb.Append("yesterday");
                        isComplete = true;
                    }

                    if (sb.IsEmpty())
                    {
                        sb.AppendFormat("{0} days",
                                        numbersAsWords ? timePassed.Days.ToWord() : timePassed.Days.ToString());
                    }
                }

                if (timePassed.AtLeast(1.Hours()) && sb.IsEmpty())
                {
                    sb.AppendFormat("{0} hour", numbersAsWords ? timePassed.Hours.ToWord() : timePassed.Hours.ToString());
                    if (timePassed.AtLeast(2.Hours()))
                    {
                        sb.Append("s");
                    }
                }

                if (timePassed.AtLeast(1.Minutes()) && sb.IsEmpty())
                {
                    sb.AppendFormat("{0} minute",
                                    numbersAsWords ? timePassed.Minutes.ToWord() : timePassed.Minutes.ToString());
                    if (timePassed.AtLeast(2.Minutes()))
                    {
                        sb.Append("s");
                    }
                }

                if (timePassed.AtLeast(1.Seconds()) && sb.IsEmpty())
                {
                    sb.AppendFormat("{0} second",
                                    numbersAsWords ? timePassed.Seconds.ToWord() : timePassed.Seconds.ToString());
                    if (timePassed.AtLeast(2.Seconds()))
                    {
                        sb.Append("s");
                    }
                }

                if (sb.IsEmpty())
                {
                    sb.Append("just moments");
                }

                if (!isComplete)
                {
                    sb.Append(" ago");
                }
            }
            else
            {
                sb.Append("from the future");
            }
            return sb.ToString();
        }

        internal static TimeSpan Passed(this DateTime time)
        {
            var now = time.Kind == DateTimeKind.Utc ? DateTime.Now.ToUniversalTime() : DateTime.Now;
            return now.Subtract(time).Duration();
        }

        internal static bool MoreThan<T>(this T instance, T value) where T : IComparable<T>
        {
            return instance.CompareTo(value) == 1;
        }

        internal static bool AtLeast<T>(this T instance, T value) where T : IComparable<T>
        {
            var result = instance.CompareTo(value);
            return result == 0 || result == 1;
        }

        internal static bool After<T>(this T instance, T value) where T : IComparable<T>
        {
            return instance.MoreThan(value);
        }

        internal static bool Before<T>(this T instance, T value) where T : IComparable<T>
        {
            return instance.CompareTo(value) == -1;
        }

        internal static double Elapsed(this long ticks)
        {
            return (DateTime.Now.Ticks - ticks).Ticks().TotalSeconds;
        }

        internal static double Elapsed(this DateTime dateTime)
        {
            var now = dateTime.Kind == DateTimeKind.Utc ? DateTime.Now.ToUniversalTime() : DateTime.Now;
            return (now.Ticks - dateTime.Ticks).Elapsed();
        }

        internal static bool IsEmpty(this StringBuilder instance)
        {
            return instance.Length < 1;
        }
    }
}