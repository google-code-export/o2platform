namespace Amazon.CloudFront.Util
{
    using System;

    internal static class CloudFrontConstants
    {
        internal const string AmzDateHeader = "x-amz-date";
        internal const string AmzRequestIdHeader = "x-amzn-RequestId";
        internal const string AuthorizationHeader = "Authorization";
        internal const string ConfigQuery = "/config";
        internal static readonly string DeleteVerb = Verbs[3];
        internal static readonly string GetVerb = Verbs[0];
        internal static readonly string PostVerb = Verbs[1];
        internal static readonly string PutVerb = Verbs[2];
        internal const string RequestParam = "request";
        internal const string ServiceResource = "/2010-03-01/";
        internal static readonly string[] Verbs = new string[] { "GET", "POST", "PUT", "DELETE" };
    }
}

