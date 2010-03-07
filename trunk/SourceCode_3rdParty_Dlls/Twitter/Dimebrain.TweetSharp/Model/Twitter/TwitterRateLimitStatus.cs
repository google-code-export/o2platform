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
using System.Diagnostics;
using System.Runtime.Serialization;
using TweetSharp.Model.Twitter.Converters;
using Newtonsoft.Json;

namespace TweetSharp.Model
{
#if !SILVERLIGHT
    [Serializable]
#endif
#if !Smartphone
    [DataContract]
    [DebuggerDisplay("{RemainingHits} / {HourlyLimit} remaining.")]
#endif
    [JsonObject(MemberSerialization.OptIn)]
    public class TwitterRateLimitStatus :
        PropertyChangedBase,
        IComparable<TwitterRateLimitStatus>,
        IEquatable<TwitterRateLimitStatus>,
        ITwitterModel
    {
        private int _remainingHits;
        private int _hourlyLimit;
        private long _resetTimeInSeconds;
        private DateTime _resetTime;

        [JsonProperty("remaining_hits", Required = Required.Always)]
#if !Smartphone
        [DataMember]
#endif
        public virtual int RemainingHits
        {
            get { return _remainingHits; }
            set
            {
                if (_remainingHits == value)
                {
                    return;
                }

                _remainingHits = value;
                OnPropertyChanged("RemainingHits");
            }
        }

        [JsonProperty("hourly_limit", Required = Required.Always)]
#if !Smartphone
        [DataMember]
#endif
        public virtual int HourlyLimit
        {
            get { return _hourlyLimit; }
            set
            {
                if (_hourlyLimit == value)
                {
                    return;
                }

                _hourlyLimit = value;
                OnPropertyChanged("HourlyLimit");
            }
        }

        [JsonProperty("reset_time_in_seconds")]
#if !Smartphone
        [DataMember]
#endif
        public virtual long ResetTimeInSeconds
        {
            get { return _resetTimeInSeconds; }
            set
            {
                if (_resetTimeInSeconds == value)
                {
                    return;
                }

                _resetTimeInSeconds = value;
                OnPropertyChanged("ResetTimeInSeconds");
            }
        }

        [JsonProperty("reset_time")]
        [JsonConverter(typeof (TwitterDateTimeConverter))]
#if !Smartphone
        [DataMember]
#endif
        public virtual DateTime ResetTime
        {
            get { return _resetTime; }
            set
            {
                if (_resetTime == value)
                {
                    return;
                }

                _resetTime = value;
                OnPropertyChanged("ResetTime");
            }
        }

        #region Implementation of IComparable<TwitterRateLimitStatus>

        public int CompareTo(TwitterRateLimitStatus other)
        {
            return other.HourlyLimit.CompareTo(HourlyLimit) == 0 &&
                   other.ResetTime.CompareTo(ResetTime) == 0 &&
                   other.RemainingHits.CompareTo(RemainingHits) == 0
                       ? 0
                       : 1;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof (TwitterRateLimitStatus) &&
                   Equals((TwitterRateLimitStatus) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var result = _remainingHits;
                result = (result*397) ^ _hourlyLimit;
                result = (result*397) ^ _resetTimeInSeconds.GetHashCode();
                result = (result*397) ^ _resetTime.GetHashCode();
                return result;
            }
        }

        #endregion

        #region Implementation of IEquatable<TwitterRateLimitStatus>

        public bool Equals(TwitterRateLimitStatus other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return other._remainingHits == _remainingHits &&
                   other._hourlyLimit == _hourlyLimit &&
                   other._resetTimeInSeconds == _resetTimeInSeconds &&
                   other._resetTime.Equals(_resetTime);
        }

        #endregion
    }
}