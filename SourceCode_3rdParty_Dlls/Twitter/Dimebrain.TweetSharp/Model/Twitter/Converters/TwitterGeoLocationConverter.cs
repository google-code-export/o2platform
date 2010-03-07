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
using System.Linq;
using TweetSharp.Core.Extensions;
using Newtonsoft.Json;

namespace TweetSharp.Model.Twitter.Converters
{
    /// <summary>
    /// This converter exists to convert geo-spatial coordinates.
    /// </summary>
    public class TwitterGeoLocationConverter : TwitterConverterBase
    {
        private const string _geoTemplate =
            "\"geo\":{{\"type\":\"Point\",\"coordinates\":[{0}, {1}]}}";

        /// <summary>
        /// Writes the JSON.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is GeoLocation))
            {
                return;
            }

            var location = (GeoLocation) value;
            var json = _geoTemplate.FormatWith(location.Latitude, location.Longitude);
            writer.WriteValue(json);
        }

        /// <summary>
        /// Reads the JSON.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="serializer">The serializer.</param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            if (reader.Value == null || reader.Value.ToString().IsNullOrBlank())
            {
                return null;
            }

            // 37.78029, -122.39697
            // note StringSplitOptions.RemoveEmptyEntries not supported on CE
            var coordinates = reader.Value.ToString()
                .Split(new[] {','}).Where(v => !v.IsNullOrBlank()).ToArray();

            if (coordinates.Length != 2)
            {
                return null;
            }

            var latitude = Convert.ToDouble(coordinates[0].Trim(), CultureInfo.InvariantCulture);
            var longitude = Convert.ToDouble(coordinates[1].Trim(), CultureInfo.InvariantCulture);

            return new GeoLocation(latitude, longitude);
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            var t = (IsNullableType(objectType))
                        ? Nullable.GetUnderlyingType(objectType)
                        : objectType;

            return typeof (GeoLocation).IsAssignableFrom(t);
        }
    }
}