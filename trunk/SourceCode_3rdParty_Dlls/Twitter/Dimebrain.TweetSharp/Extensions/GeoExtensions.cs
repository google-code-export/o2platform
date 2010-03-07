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

using TweetSharp.Core.Extensions;
using TweetSharp.Model;

namespace TweetSharp.Extensions
{
    public static class GeoExtensions
    {
        public static double Yards(this double miles)
        {
            return miles*1760;
        }

        public static double Yards(this int miles)
        {
            return miles*1760;
        }

        public static double Kilometres(this double miles)
        {
            return miles*1.609344;
        }

        public static double Kilometres(this int miles)
        {
            return miles*1.609344;
        }

        public static double Kilometers(this double miles)
        {
            return miles*1.609344;
        }

        public static double Kilometers(this int miles)
        {
            return miles*1.609344;
        }

        /// <summary>
        /// Determines whether the specified source is within.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="miles">The miles.</param>
        /// <param name="target">The target.</param>
        /// <returns>
        /// 	<c>true</c> if the specified source is within; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsWithin(this GeoLocation source, double miles, GeoLocation target)
        {
            return source.MilesFrom(target) <= miles;
        }

        public static double KilometresFrom(this GeoLocation origin, GeoLocation destination)
        {
            return origin.MilesFrom(destination).Kilometres();
        }

        public static double KilometersFrom(this GeoLocation origin, GeoLocation destination)
        {
            return origin.MilesFrom(destination).Kilometres();
        }

        public static double YardsFrom(this GeoLocation origin, GeoLocation destination)
        {
            return origin.MilesFrom(destination).Yards();
        }

        public static double MilesFrom(this GeoLocation origin, GeoLocation destination)
        {
            var theta = (destination.Longitude - origin.Longitude).ToRadians();
            var target = destination.Latitude.ToRadians();
            var source = origin.Latitude.ToRadians();

            var distance = target.Sin()*source.Sin() +
                           target.Cos()*source.Cos()*
                           theta.Cos();

            distance = distance.Acos().ToDegrees();

            return distance*60*1.1515; // statute miles
        }
    }
}