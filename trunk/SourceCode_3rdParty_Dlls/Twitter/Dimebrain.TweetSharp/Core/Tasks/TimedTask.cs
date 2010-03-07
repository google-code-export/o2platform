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
using System.Threading;
using TweetSharp.Model;

namespace TweetSharp.Core.Tasks
{
    /// <summary>
    /// Class representing a recurring task
    /// </summary>
    public class TimedTask : ITimedTask
    {
        private readonly int _iterations;
        private readonly Timer _timer;
        private TimeSpan _due;
        private TimeSpan _interval;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimedTask"/> class.
        /// </summary>
        /// <param name="due">The next time to perform the task.</param>
        /// <param name="interval">The recurrance interval for the task.</param>
        /// <param name="continueOnError">if set to <c>true</c> [continue on error].</param>
        /// <param name="iterations">The number of times to perform the task. If zero, performs the task continuously.</param>
        /// <param name="rateLimitingRule">The rule to use for ratelimiting this periodic task</param>
        /// <param name="action">The action to perform.</param>
        public TimedTask(TimeSpan due, TimeSpan interval, bool continueOnError, int iterations,
                         IRateLimitingRule rateLimitingRule, Action<bool> action)
        {
            Action = action;

            _due = due;
            _interval = interval;
            _iterations = iterations;

            RateLimitingRule = rateLimitingRule;

            var count = 0;

            _timer = new Timer(state =>
                                   {
                                       try
                                       {
                                           var skip = ShouldSkipForRateLimiting();
                                           Action(skip);

                                           count++;

                                           if (_iterations > 0 && count >= _iterations)
                                           {
                                               Stop();
                                           }
                                       }
                                       catch (Exception ex)
                                       {
                                           Exception = ex;
                                           if (!continueOnError)
                                           {
                                               Stop();
                                           }
                                       }
                                   }, null, _due, _interval);
        }

        #region ITimedTask Members

        /// <summary>
        /// Gets or sets the action to perform periodically.
        /// </summary>
        /// <value>The action.</value>
        public Action<bool> Action { get; private set; }

        /// <summary>
        /// The last exception that occurred during a timed action.
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// true if this task is using rate limiting
        /// </summary>
        public bool RateLimited
        {
            get { return RateLimitingRule != null; }
        }

        public TimeSpan DueTime
        {
            get { return _due; }
        }

        public TimeSpan Interval
        {
            get { return _interval; }
        }

        public IRateLimitingRule RateLimitingRule { get; set; }

        /// <summary>
        /// Stops this task instance.
        /// </summary>
        public void Stop()
        {
            _timer.Change(-1, -1);
        }

        /// <summary>
        /// Starts this task instance.
        /// </summary>
        public void Start()
        {
            _timer.Change(_due, _interval);
        }

        public void Start(TimeSpan dueTime, TimeSpan interval)
        {
            _due = dueTime;
            _interval = interval;
            _timer.Change(_due, _interval);
        }

        public void Dispose()
        {
            _timer.Dispose();
        }

        #endregion

        private bool ShouldSkipForRateLimiting()
        {
            //only pre-skip via predicate. Percentage based adjusts rate after the call
            if (RateLimitingRule == null
                || RateLimitingRule.RateLimitingType != RateLimitingType.ByPredicate)
            {
                return false;
            }

            if (RateLimitingRule.RateLimitPredicate == null)
            {
                throw new InvalidOperationException("Rule is set to use predicate, but no predicate is defined.");
            }
            TwitterRateLimitStatus status = null;
            if (RateLimitingRule.GetRateLimitStatus != null)
            {
                status = RateLimitingRule.GetRateLimitStatus();
            }
            return !RateLimitingRule.RateLimitPredicate(status);
        }
    }
}