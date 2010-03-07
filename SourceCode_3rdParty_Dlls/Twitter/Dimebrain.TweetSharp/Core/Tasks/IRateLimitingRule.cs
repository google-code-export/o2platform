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
using TweetSharp.Model;

namespace TweetSharp.Core.Tasks
{
    public interface IRateLimitingRule
    {
        /// <summary>
        /// Desired percentage of the total rate limit to allocate to this task this rule applies to
        /// </summary>
        double? LimitToPercentOfTotal { get; }

        /// <summary>
        /// Indicates the type of rate limiting used by this rule
        /// </summary>
        RateLimitingType RateLimitingType { get; }

        /// <summary>
        /// Method that provides the current rate limit status used to determine whether or not to run the method
        /// </summary>
        Func<TwitterRateLimitStatus> GetRateLimitStatus { get; set; }

        /// <summary>
        /// User-provided method that evaluates the current rate limit status and returns bool indicating whether or not to run the task
        /// </summary>
        Predicate<TwitterRateLimitStatus> RateLimitPredicate { get; }
    }
}