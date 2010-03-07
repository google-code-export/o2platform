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

namespace TweetSharp.Extensions
{
    public static partial class TimeExtensions
    {
        public static TimeSpan Ticks(this long value)
        {
            return TimeSpan.FromTicks(value);
        }

        // todo refactor this for accuracy
        public static TimeSpan Years(this int value)
        {
            return TimeSpan.FromDays(value*365);
        }

        // todo refactor this for accuracy
        public static TimeSpan Months(this int value)
        {
            return TimeSpan.FromDays(value*30);
        }

        public static TimeSpan Weeks(this int value)
        {
            return TimeSpan.FromDays(value*7);
        }

        public static TimeSpan Days(this int days)
        {
            return new TimeSpan(days, 0, 0, 0);
        }

        public static TimeSpan Day(this int days)
        {
            return new TimeSpan(days, 0, 0, 0);
        }

        public static TimeSpan Hours(this int hours)
        {
            return new TimeSpan(0, hours, 0, 0);
        }

        public static TimeSpan Hour(this int hours)
        {
            return new TimeSpan(0, hours, 0, 0);
        }

        public static TimeSpan Minutes(this int minutes)
        {
            return new TimeSpan(0, 0, minutes, 0);
        }

        public static TimeSpan Minute(this int minutes)
        {
            return new TimeSpan(0, 0, minutes, 0);
        }

        public static TimeSpan Second(this int seconds)
        {
            return new TimeSpan(0, 0, 0, seconds);
        }

        public static TimeSpan Seconds(this int seconds)
        {
            return new TimeSpan(0, 0, 0, seconds);
        }

        public static TimeSpan Millisecond(this int milliseconds)
        {
            return new TimeSpan(0, 0, 0, 0, milliseconds);
        }

        public static TimeSpan Milliseconds(this int milliseconds)
        {
            return new TimeSpan(0, 0, 0, 0, milliseconds);
        }

        public static DateTime Ago(this TimeSpan value)
        {
            return DateTime.UtcNow.Add(value.Negate());
        }

        public static DateTime FromNow(this TimeSpan value)
        {
            return new DateTime((DateTime.Now + value).Ticks);
            //return new DateTime((DateTime.UtcNow + value).Ticks);
        }

        public static DateTime FromUnixTime(this long seconds)
        {
            var time = new DateTime(1970, 1, 1);
            time = time.AddSeconds(seconds);

            return time.ToLocalTime();
        }

        public static long ToUnixTime(this DateTime dateTime)
        {
            var timeSpan = (dateTime - new DateTime(1970, 1, 1));
            var timestamp = (long) timeSpan.TotalSeconds;

            return timestamp;
        }
    }
}