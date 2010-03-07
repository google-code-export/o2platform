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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using TweetSharp.Core;
using TweetSharp.Core.Extensions;
using TweetSharp.Core.Tasks;
using TweetSharp.Core.Web;
using TweetSharp.Core.Web.Query;
using TweetSharp.Extensions;
using TweetSharp.Fluent.Base;
using TweetSharp.Fluent.Yammer;
using TweetSharp.Model;

#if !SILVERLIGHT

#endif

namespace TweetSharp.Fluent
{
#if !SILVERLIGHT
    /// <summary>
    /// This is the main fluent class for building expressions
    /// bound for the Yammer API.
    /// </summary>
    [Serializable]
#endif
    public sealed class FluentYammer : FluentBase<YammerResult>, IFluentYammer
    {
        private const string YammerClientDefaultName = "tweetsharp";
        private const string YammerClientDefaultUrl = "http://tweetsharp.com";
        private const string YammerClientDefaultVersion = "1.0.0.0";
        private const int YammerMaxUpdateLength = int.MaxValue;

        private const string UrlActionBase = "https://www.yammer.com/api/v1/{0}/{1}.{2}";
        private const string UrlActionIdBase = "https://www.yammer.com/api/v1/{0}/{1}/{2}.{3}";
        private const string UrlBase = "https://www.yammer.com/api/v1/{0}.{1}";
        private const string UrlIdBase = "https://www.yammer.com/api/v1/{0}/{1}.{2}";

        protected override string UrlOAuthAuthority
        {
            get { return "https://www.yammer.com/oauth/{0}"; }
        }

        private FluentYammer(IClientInfo clientInfo)
        {
#if !SILVERLIGHT && !Smartphone
            SetLibraryWebPermissions();
#endif
            ClientInfo = clientInfo;
            Authentication = new FluentYammerAuthentication(this);
            Configuration = new FluentYammerConfiguration(this);
            Parameters = new FluentYammerParameters();

            // http://groups.google.com/group/twitter-development-talk/browse_thread/thread/7c67ff1a2407dee7
            ServicePointManager.Expect100Continue = false;
        }

        /// <summary>
        /// Gets the authentication pair used to authenticate to yammer
        /// </summary>
        /// <value>The authentication pair, typically a username and password or a oauth token and tokensecret.</value>
        public override Pair<string, string> AuthenticationPair
        {
            get
            {
                if (Authentication == null)
                {
                    return null;
                }

                var authenticator = Authentication.Authenticator;
                if (authenticator == null)
                {
                    return null;
                }

                switch (Authentication.Mode)
                {
                    case AuthenticationMode.OAuth:
                        return new Pair<string, string>
                                   {
                                       First = ((FluentBaseOAuth) authenticator).Token,
                                       Second = ((FluentBaseOAuth) authenticator).TokenSecret
                                   };
                    default:
                        throw new NotSupportedException("Unsupported authentication mode");
                }
            }
        }

        public IFluentYammerParameters Parameters { get; private set; }

        /// <summary>
        /// Sets the client info.
        /// </summary>
        /// <param name="clientInfo">The client info.</param>
        public static void SetClientInfo(YammerClientInfo clientInfo)
        {
            _clientInfo = clientInfo;
        }

        protected override Action<object, YammerResult> InternalCallback
        {
            get { return InternalCallbackImpl; }
        }

        IFluentYammerAuthentication IFluentYammer.Authentication
        {
            get { return (IFluentYammerAuthentication) Authentication; }
            set { Authentication = value; }
        }

        IFluentYammerConfiguration IFluentYammer.Configuration
        {
            get { return (IFluentYammerConfiguration) Configuration; }
        }

        /// <summary>
        /// Gets or sets the callback.
        /// </summary>
        /// <value>The callback.</value>
        public YammerWebCallback Callback { get; set; }

        private void InternalCallbackImpl(object sender, TweetSharpResult result)
        {
            if (Callback != null)
            {
                Callback(sender, result as YammerResult);
            }
        }

#if !SILVERLIGHT

        YammerResult IFluentBase<YammerResult>.Request()
        {
            return Request();
        }

        /// <summary>
        /// Makes a sequential call to the service to get the results of this query.
        /// </summary>
        /// <returns></returns>
        public override YammerResult Request()
        {
            ValidateUpdateText();

            var query = CreateWebQuery();

            // Default cache
            EnsureDefaultCache();
            var files = ValidateAttachments();
            var remainingRetries = Configuration.MaxRetries + 1;
            YammerResult result = null;
            YammerResult previousResult = null;
            while (remainingRetries > 0)
            {
                switch (Method)
                {
                    case WebMethod.Get:
                        result = RequestGet(query);
                        break;
                    case WebMethod.Post:
                        result = RequestPostOrPut(PostOrPut.Post, query, files);
                        break;
                    case WebMethod.Put:
                        result = RequestPostOrPut(PostOrPut.Put, query, files);
                        break;
                    case WebMethod.Delete:
                        result = RequestDelete(query);
                        break;
                    default:
                        throw new NotSupportedException("Unknown or unsupported web method");
                }
                result.PreviousResult = previousResult;
                //handle retries
                if (Configuration.RetryConditions == RetryOn.Never)
                {
                    //retry policy is to never retry, so whatever we got is the result
                    remainingRetries = 0;
                }
                else
                {
                    //retry based on policy and response result 
                    if ((result.IsServiceError &&
                         (Configuration.RetryConditions & RetryOn.ServiceError) == RetryOn.ServiceError)
                        || (result.TimedOut) && (Configuration.RetryConditions & RetryOn.Timeout) == RetryOn.Timeout)
                    {
                        previousResult = result;
                        remainingRetries--;
                    }
                    else
                    {
                        //none of the retry conditions were met
                        remainingRetries = 0;
                    }
                }
            }

            return result;
        }

        private IEnumerable<string> ValidateAttachments()
        {
            foreach (var file in Parameters.Attachments)
            {
                if (File.Exists(file))
                {
                    yield return file;
                }
                else
                {
                    throw new TweetSharpException("Attachment file {0} was not found".FormatWith(file));
                }
            }
        }
#endif

        YammerClientInfo IFluentYammer.ClientInfo
        {
            get { return ClientInfo as YammerClientInfo; }
            set { ClientInfo = value; }
        }

        public override void ValidateUpdateText()
        {
            if (Parameters == null || Parameters.Body == null)
            {
                // non-participant
                return;
            }

            if (Parameters.Body.Length <= YammerMaxUpdateLength)
            {
                // valid
                return;
            }
        }

        /// <summary>
        /// Creates a new composable query, using the default client and platform.
        /// </summary>
        public static IFluentYammer CreateRequest()
        {
            if (_clientInfo == null)
            {
                _clientInfo = new YammerClientInfo
                                  {
                                      ClientName = YammerClientDefaultName,
                                      ClientUrl = YammerClientDefaultUrl,
                                      ClientVersion = YammerClientDefaultVersion
                                  };
            }

            return new FluentYammer(_clientInfo);
        }


        protected override YammerResult BuildResult(WebQueryBase query, WebException exception)
        {
            var statusDescription = Response != null && Response is HttpWebResponse
                                        ? ((Response as HttpWebResponse).StatusDescription)
                                        : default(string);

            var statusCode = Response != null && Response is HttpWebResponse
                                 ? Convert.ToInt32((Response as HttpWebResponse).StatusCode,
                                                   CultureInfo.InvariantCulture)
                                 : default(int);

            var response = query.WebResponse ?? (exception != null ? exception.Response : null);
            return new YammerResult(query.Result, exception)
                       {
                           // WebResponse is null in the event of an error or a cached result
                           ResponseType = response != null ? response.ContentType : "",
                           ResponseLength = response != null ? response.ContentLength : 0,
                           ResponseUri = response != null ? response.ResponseUri : null,
                           ResponseHttpStatusCode = statusCode,
                           ResponseHttpStatusDescription = statusDescription,
                           RequestHttpMethod = query.Method.ToUpper(),
                       };
        }

#if !SILVERLIGHT && !Smartphone
        private static void SetLibraryWebPermissions()
        {
            var permissions = new WebPermission();
            var baseUrl = new Regex(@"http://yammer\.com/.*");
            var apiUrl = new Regex(@"http://api.yammer\.com/.*");

            permissions.AddPermission(NetworkAccess.Connect, baseUrl);
            permissions.AddPermission(NetworkAccess.Connect, apiUrl);
            try
            {
                permissions.Demand();
            }
            catch (SecurityException)
            {
                var message =
                    "You cannot use TweetSharp in partial trust without a policy that allows connecting to API endpoints." +
                    Environment.NewLine +
                    "The following policy information (or equivalent) must be added to your trust policy:" +
                    Environment.NewLine +
                    permissions.ToXml();

                throw new SecurityException(message);
            }
        }
#endif


        protected override void QueryQueryResponse(object sender, WebQueryResponseEventArgs args)
        {
            if (Callback == null)
            {
                return;
            }

            var query = sender as WebQueryBase;
            if (query != null)
            {
                Response = query.WebResponse;
            }

            args.Response = args.Response.ToTwitterResponseString();

            if (query == null)
            {
                return;
            }

            TweetSharpResult result = BuildResult(query, null);
            Callback(this, result as YammerResult);
        }

        public override string AsUrl(bool ignoreTransparentProxy)
        {
            var hasAuthAction = IsOAuthProcessCall;
            var hasAction = !Parameters.Action.IsNullOrBlank();
            var format = Format.ToLower();
            var activity = Parameters.Activity.IsNullOrBlank() ? "?" : Parameters.Activity;
            var action = hasAction ? Parameters.Action : "?";

            // this is an oauth call
            if (hasAuthAction)
            {
                return BuildOAuthQuery();
            }

            // this is a rest api call
            return BuildQuery(hasAction, format, activity, action);
        }

        public override string AsUrl()
        {
            return AsUrl(false /* ignoreTransparentProxy */);
        }

        protected override string BuildQuery(bool hasAction, string format, string activity, string action)
        {
            var id = Parameters.Id.HasValue
                         ? Parameters.Id.Value.ToString()
                         : String.Empty;


            var hasId = !id.IsNullOrBlank() || Parameters.UseCurrentAsUserId;
            object idOrCurrent = Parameters.UseCurrentAsUserId ? "current" : id;
            string url;
            if (hasAction)
            {
                url = hasId ? UrlActionIdBase : UrlActionBase;
                url = string.Format(url, hasId
                                             ? new[] {activity, action, idOrCurrent, format}
                                             : new object[] {activity, action, format});
            }
            else
            {
                url = hasId ? UrlIdBase : UrlBase;
            }

            url = String.Format(url, hasId
                                         ?
                                             new[] {activity, idOrCurrent, format}
                                         : new object[] {activity, format});

            var sb = new StringBuilder(url);

            var parameters = new List<string>(BuildParameters());
            for (var i = 0; i < parameters.Count(); i++)
            {
                sb.Append(i > 0 ? "&" : "?");
                sb.Append(parameters[i]);
            }

            var resultUrl = sb.ToString();
            //hack
            var split = resultUrl.Split('?');
            resultUrl = split[0].Replace(".none", "") + '?';
            for (var j = 1; j < split.Length; j++)
            {
                resultUrl += split[j];
            }


            return resultUrl;
        }

        /// <summary>
        /// Sends the query asynchronously.
        /// </summary>
        public override void RequestAsync()
        {
            ValidateUpdateText();

            var query = CreateWebQuery();

            if (InternalCallback != null)
            {
                query.QueryResponse += QueryQueryResponse;
            }

            if (RepeatInterval > TimeSpan.Zero)
            {
                // Continuous async operation
                RecurringTask = new TimedTask(TimeSpan.Zero, RepeatInterval, true, RepeatTimes,
                                              RateLimitingRule, skip => RequestAsyncAction(query));
            }
            else
            {
                // Normal async operation
                RequestAsyncAction(query);
            }
        }

        private void RequestAsyncAction(WebQueryBase query)
        {
            // Default cache
            EnsureDefaultCache();

            switch (Method)
            {
                case WebMethod.Get:
                    RequestGetAsync(query);
                    return;
                case WebMethod.Post:
                    RequestPostAsync(query);
                    return;
                case WebMethod.Delete:
                    RequestDeleteAsync(query);
                    return;
                default:
                    throw new NotSupportedException("Unknown web method");
            }
        }

        private IEnumerable<string> BuildParameters()
        {
            if (!Parameters.Body.IsNullOrBlank())
            {
                const string _format = "body={0}";

                var content = Parameters.Body.UrlEncode();
                yield return string.Format(_format, content);
            }

            if (Parameters.InReplyTo.HasValue)
            {
                yield return string.Format("replied_to_id={0}", Parameters.InReplyTo.Value);
            }

            if (Parameters.ToGroupID.HasValue)
            {
                yield return string.Format("group_id={0}", Parameters.ToGroupID.Value);
            }

            if (Parameters.GroupID.HasValue)
            {
                yield return string.Format("group_id={0}", Parameters.GroupID.Value);
            }

            if (Parameters.DirectToUser.HasValue)
            {
                yield return string.Format("direct_to_id={0}", Parameters.DirectToUser.Value);
            }

            if (Parameters.MessageId.HasValue)
            {
                yield return string.Format("message_id={0}", Parameters.MessageId.Value);
            }

            if (Parameters.SortGroupsBy.HasValue)
            {
                yield return string.Format("sort_by={0}", Parameters.SortGroupsBy.Value.ToLower());
            }

            if (Parameters.SortUsersBy.HasValue)
            {
                yield return string.Format("sort_by={0}", Parameters.SortUsersBy.Value.ToLower());
            }

            if (Parameters.StartingWith.HasValue)
            {
                yield return string.Format("letter={0}", Parameters.StartingWith.Value);
            }

            if (Parameters.Page.HasValue)
            {
                yield return string.Format("page={0}", Parameters.Page.Value);
            }

            if (!string.IsNullOrEmpty(Parameters.Email))
            {
                yield return string.Format("email={0}", Parameters.Email);
            }

            if (Parameters.Reverse.HasValue && Parameters.Reverse.Value)
            {
                yield return string.Format("reverse=true");
            }
            if (!string.IsNullOrEmpty(Parameters.GroupName))
            {
                yield return string.Format("name={0}", Parameters.GroupName);
            }
            if (Parameters.Private.HasValue)
            {
                yield return string.Format("private={0}", Parameters.Private.Value);
            }
            if (!string.IsNullOrEmpty(Parameters.Subordinate))
            {
                yield return string.Format("subordinate={0}", Parameters.Subordinate);
            }
            if (!string.IsNullOrEmpty(Parameters.Superior))
            {
                yield return string.Format("superior={0}", Parameters.Superior);
            }
            if (!string.IsNullOrEmpty(Parameters.Colleague))
            {
                yield return string.Format("colleague={0}", Parameters.Colleague);
            }
            if (Parameters.RelationshipType.HasValue)
            {
                yield return string.Format("type={0}", Parameters.RelationshipType.Value);
            }

            if (Parameters.TargetId.HasValue)
            {
                yield return string.Format("target_id={0}", Parameters.TargetId.Value);
            }
            if (!string.IsNullOrEmpty(Parameters.TargetType))
            {
                yield return string.Format("target_type={0}", Parameters.TargetType);
            }
            if (!string.IsNullOrEmpty(Parameters.Prefix))
            {
                yield return string.Format("prefix={0}", Parameters.Prefix);
            }
            if (!string.IsNullOrEmpty(Parameters.Search))
            {
                yield return string.Format("search={0}", Parameters.Search);
            }
            if (Parameters.UserData != null)
            {
                var userParams = ParamaterizeUserData(Parameters.UserData);
                foreach (var s in userParams)
                {
                    yield return s;
                }
            }
        }

        private static IEnumerable<string> ParamaterizeUserData(YammerUser user)
        {
            var primaryAddresses =
                user.ContactInfo.EmailAddresses.Where(a => a.EmailType.ToLower() == "primary");
            var address = primaryAddresses.Any()
                              ? primaryAddresses.First()
                              : user.ContactInfo.EmailAddresses.FirstOrDefault();
            if (address != null)
            {
                yield return string.Format("email={0}", address.Address);
            }
            if (!string.IsNullOrEmpty(user.FullName))
            {
                yield return string.Format("full_name={0}", user.FullName);
            }
            if (!string.IsNullOrEmpty(user.JobTitle))
            {
                yield return string.Format("job_title={0}", user.JobTitle);
            }
            if (!string.IsNullOrEmpty(user.Location))
            {
                yield return string.Format("location={0}", user.Location);
            }
            if (!string.IsNullOrEmpty(user.Interests))
            {
                yield return string.Format("interests={0}", user.Interests);
            }
            if (!string.IsNullOrEmpty(user.Summary))
            {
                yield return string.Format("summary={0}", user.Summary);
            }
            if (!string.IsNullOrEmpty(user.Expertise))
            {
                yield return string.Format("expertise={0}", user.Expertise);
            }
            if (user.ContactInfo != null)
            {
                if (user.ContactInfo.Im != null)
                {
                    if (!string.IsNullOrEmpty(user.ContactInfo.Im.Provider))
                    {
                        yield return string.Format("im_provider={0}", user.ContactInfo.Im.Provider);
                    }
                    if (!string.IsNullOrEmpty(user.ContactInfo.Im.UserName))
                    {
                        yield return string.Format("im_username={0}", user.ContactInfo.Im.UserName);
                    }
                }
                if (user.ContactInfo.PhoneNumbers != null && user.ContactInfo.PhoneNumbers.Any())
                {
                    var workNumber =
                        user.ContactInfo.PhoneNumbers.Where(n => n.NumberType.ToLower() == "work").FirstOrDefault();
                    if (workNumber != null)
                    {
                        yield return string.Format("work_telephone={0}", workNumber.Number);
                    }
                    var mobileNumber =
                        user.ContactInfo.PhoneNumbers.Where(n => n.NumberType.ToLower() == "mobile").FirstOrDefault();
                    if (mobileNumber != null)
                    {
                        yield return string.Format("mobile_telephone={0}", mobileNumber.Number);
                    }
                }
                if (!string.IsNullOrEmpty(user.Interests))
                {
                    yield return string.Format("interests={0}", user.Interests);
                }
                if (!string.IsNullOrEmpty(user.Summary))
                {
                    yield return string.Format("summary={0}", user.Summary);
                }
                if (!string.IsNullOrEmpty(user.Expertise))
                {
                    yield return string.Format("expertise={0}", user.Expertise);
                }

                if (!string.IsNullOrEmpty(user.KidsNames))
                {
                    yield return string.Format("kids_names={0}", user.KidsNames);
                }

                if (!string.IsNullOrEmpty(user.SignificantOther))
                {
                    yield return string.Format("significant_other={0}", user.SignificantOther);
                }
                foreach (var school in user.Schools)
                {
                    yield return
                        string.Format("education[]={0},{1},{2},{3},{4}", school.School, school.Degree,
                                      school.Description, school.StartYear, school.EndYear);
                }
                foreach (var company in user.PreviousCompanies)
                {
                    yield return
                        string.Format("previous_companies[]={0},{1},{2},{3},{4}", company.Employer, company.Position,
                                      company.Description, company.StartYear, company.EndYear);
                }
            }
        }
    }
}