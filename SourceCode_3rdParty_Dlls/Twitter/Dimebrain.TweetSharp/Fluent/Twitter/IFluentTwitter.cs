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
using System.ComponentModel;
using System.Net;
using TweetSharp.Core.Tasks;
using TweetSharp.Core.Web;
using TweetSharp.Fluent.Base;
using TweetSharp.Model;

#if !SILVERLIGHT

#else
using HttpUtility = System.Windows.Browser.HttpUtility;
#endif

namespace TweetSharp.Fluent
{
    public interface IFluentTwitter : IFluentBase<TwitterResult>
    {
        TwitterClientInfo ClientInfo { get; set; }
        bool HasError { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        WebResponse Response { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        WebMethod Method { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        WebFormat Format { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        TwitterWebCallback Callback { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        IFluentTwitterAuthentication Authentication { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        IFluentTwitterAuthentication SecondaryAuthentication { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        IFluentTwitterProfile Profile { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        IFluentTwitterConfiguration Configuration { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        IFluentTwitterSearchParameters SearchParameters { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        IFluentTwitterStreamingParameters StreamingParameters { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        IFluentTwitterTrendsParameters TrendsParameters { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        IFluentTwitterParameters Parameters { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        ITimedTask RecurringTask { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        IRateLimitingRule RateLimitingRule { get; set; }

        // todo make another class like above or put in configuration
        TimeSpan RepeatInterval { get; set; }
        int RepeatTimes { get; set; }

        void Cancel();
        string AsUrl();
    }
}