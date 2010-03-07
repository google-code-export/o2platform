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
using System.Collections.Generic;
using System.Linq;

namespace TweetSharp.Model
{
    ///<summary>
    /// Represents a geospatial location, for APIs that support it.
    ///</summary>
#if !SILVERLIGHT
    [Serializable]
#endif

    public struct GeoLocation : IEquatable<GeoLocation>
    {
        private static readonly GeoLocation _none = new GeoLocation(0, 0);

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoLocation"/> struct.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        public GeoLocation(double latitude, double longitude)
            : this()
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        /// Gets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        public double Longitude { get; set; }

        public static GeoLocation None
        {
            get { return _none; }
        }

        #region IEquatable<GeoLocation> Members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        public bool Equals(GeoLocation other)
        {
            return other.Latitude == Latitude
                   && other.Longitude == Longitude;
        }

        #endregion

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="instance">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object instance)
        {
            if (ReferenceEquals(null, instance))
            {
                return false;
            }

            return instance.GetType() == typeof (GeoLocation) && Equals((GeoLocation) instance);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Latitude.GetHashCode()*397) ^ Longitude.GetHashCode();
            }
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(GeoLocation left, GeoLocation right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(GeoLocation left, GeoLocation right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="TweetSharp.Model.GeoLocation"/> to <see cref="System.Double[]"/>.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator double[](GeoLocation location)
        {
            return new[] {location.Latitude, location.Longitude};
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="double"/> to <see cref="TweetSharp.Model.GeoLocation"/>.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator GeoLocation(List<double> values)
        {
            return FromEnumerable(values);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Double[]"/> to <see cref="TweetSharp.Model.GeoLocation"/>.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator GeoLocation(double[] values)
        {
            return FromEnumerable(values);
        }

        /// <summary>
        /// Froms the enumerable.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        private static GeoLocation FromEnumerable(IEnumerable<double> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }

            var latitude = values.First();
            var longitude = values.Skip(1).Take(1).Single();

            return new GeoLocation(latitude, longitude);
        }
    }
}