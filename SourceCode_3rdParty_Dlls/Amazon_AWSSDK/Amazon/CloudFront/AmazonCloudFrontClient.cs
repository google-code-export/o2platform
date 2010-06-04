namespace Amazon.CloudFront
{
    using Amazon.CloudFront.Model;
    using Amazon.CloudFront.Util;
    using Amazon.Util;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Xml.Xsl;

    public class AmazonCloudFrontClient : AmazonCloudFront, IDisposable
    {
        private string awsAccessKeyId;
        private SecureString awsSecretAccessKey;
        private string clearAwsSecretAccessKey;
        private AmazonCloudFrontConfig config;
        private static int defaultRetry = 3;
        private bool disposed;

        public AmazonCloudFrontClient(string awsAccessKeyId, string awsSecretAccessKey) : this(awsAccessKeyId, awsSecretAccessKey, new AmazonCloudFrontConfig())
        {
        }

        public AmazonCloudFrontClient(string awsAccessKeyId, SecureString awsSecretAccessKey, AmazonCloudFrontConfig config)
        {
            this.awsAccessKeyId = awsAccessKeyId;
            this.awsSecretAccessKey = awsSecretAccessKey;
            this.config = config;
        }

        public AmazonCloudFrontClient(string awsAccessKeyId, string awsSecretAccessKey, AmazonCloudFrontConfig config)
        {
            if (!string.IsNullOrEmpty(awsSecretAccessKey))
            {
                if (config.UseSecureStringForAwsSecretKey)
                {
                    this.awsSecretAccessKey = new SecureString();
                    foreach (char ch in awsSecretAccessKey.ToCharArray())
                    {
                        this.awsSecretAccessKey.AppendChar(ch);
                    }
                    this.awsSecretAccessKey.MakeReadOnly();
                }
            }
            else
            {
                this.clearAwsSecretAccessKey = awsSecretAccessKey;
            }
            this.awsAccessKeyId = awsAccessKeyId;
            this.config = config;
        }

        private void AddCloudFrontQueryParameters(IDictionary<CloudFrontQueryParameter, string> parameters, WebHeaderCollection webHeaders)
        {
            string str2;
            if (webHeaders != null)
            {
                webHeaders["x-amz-date"] = AmazonCloudFrontUtil.FormattedCurrentTimestamp;
            }
            string str = parameters[CloudFrontQueryParameter.Action];
            StringBuilder builder = new StringBuilder("/2010-03-01/", 0x100);
            if (str == null)
            {
                throw new AmazonCloudFrontException("No ACTION set for the operation");
            }
            if (str.Contains("StreamingDistribution"))
            {
                builder.Append("streaming-distribution");
            }
            else if (str.Contains("Distribution"))
            {
                builder.Append("distribution");
            }
            else if (str.Contains("OriginAccessIdentit"))
            {
                builder.Append("origin-access-identity/cloudfront");
            }
            if (parameters.ContainsKey(CloudFrontQueryParameter.Id))
            {
                builder.Append("/");
                builder.Append(parameters[CloudFrontQueryParameter.Id]);
            }
            if (parameters.ContainsKey(CloudFrontQueryParameter.Query))
            {
                builder.Append(parameters[CloudFrontQueryParameter.Query]);
            }
            parameters.Add(CloudFrontQueryParameter.CanonicalizedResource, builder.ToString());
            if (this.config.UseSecureStringForAwsSecretKey)
            {
                str2 = BuildSigningString(webHeaders, this.awsSecretAccessKey);
            }
            else
            {
                str2 = BuildSigningString(webHeaders, this.clearAwsSecretAccessKey);
            }
            parameters.Add(CloudFrontQueryParameter.Authorization, str2);
        }

        private static string BuildSigningString(WebHeaderCollection headers, SecureString key)
        {
            return AWSSDKUtils.HMACSign(headers.Get("x-amz-date"), key, new HMACSHA1());
        }

        private static string BuildSigningString(WebHeaderCollection headers, string key)
        {
            return AWSSDKUtils.HMACSign(headers.Get("x-amz-date"), key, new HMACSHA1());
        }

        private HttpWebRequest ConfigureWebRequest(IDictionary<CloudFrontQueryParameter, string> parameters, WebHeaderCollection headers, long contentLength)
        {
            string requestUriString = AWSSDKUtils.UrlEncode(this.config.ServiceURL, true);
            if (parameters.ContainsKey(CloudFrontQueryParameter.CanonicalizedResource))
            {
                requestUriString = requestUriString + parameters[CloudFrontQueryParameter.CanonicalizedResource];
            }
            HttpWebRequest request = WebRequest.Create(requestUriString) as HttpWebRequest;
            if (request != null)
            {
                if (this.config.IsSetProxyHost() && this.config.IsSetProxyPort())
                {
                    WebProxy proxy = new WebProxy(this.config.ProxyHost, this.config.ProxyPort);
                    if (this.config.IsSetProxyUsername())
                    {
                        proxy.Credentials = new NetworkCredential(this.config.ProxyUsername, this.config.ProxyPassword ?? string.Empty);
                    }
                    request.Proxy = proxy;
                }
                request.UserAgent = this.config.UserAgent;
                request.Headers["Authorization"] = "AWS " + this.awsAccessKeyId + ":" + parameters[CloudFrontQueryParameter.Authorization];
                request.Headers.Add(headers);
                request.Method = parameters[CloudFrontQueryParameter.Verb];
                if (parameters.ContainsKey(CloudFrontQueryParameter.ContentType))
                {
                    request.ContentType = parameters[CloudFrontQueryParameter.ContentType];
                }
                request.ContentLength = contentLength;
                request.KeepAlive = false;
                request.AllowAutoRedirect = true;
                request.MaximumAutomaticRedirections = 2;
                request.AllowWriteStreamBuffering = false;
            }
            return request;
        }

        private IDictionary<CloudFrontQueryParameter, string> ConvertCreateDistribution(CreateDistributionRequest request)
        {
            IDictionary<CloudFrontQueryParameter, string> parameters = new Dictionary<CloudFrontQueryParameter, string>(5);
            parameters.Add(CloudFrontQueryParameter.Verb, CloudFrontConstants.PostVerb);
            parameters.Add(CloudFrontQueryParameter.Action, "CreateDistribution");
            parameters.Add(CloudFrontQueryParameter.ContentBody, request.DistributionConfig.ToString());
            parameters.Add(CloudFrontQueryParameter.ContentType, "application/x-www-form-urlencoded; charset=utf-8");
            this.AddCloudFrontQueryParameters(parameters, request.Headers);
            return parameters;
        }

        private IDictionary<CloudFrontQueryParameter, string> ConvertCreateOriginAccessIdentity(CreateOriginAccessIdentityRequest request)
        {
            IDictionary<CloudFrontQueryParameter, string> parameters = new Dictionary<CloudFrontQueryParameter, string>(5);
            parameters.Add(CloudFrontQueryParameter.Verb, CloudFrontConstants.PostVerb);
            parameters.Add(CloudFrontQueryParameter.Action, "CreateOriginAccessIdentity");
            parameters.Add(CloudFrontQueryParameter.ContentBody, request.OriginAccessIdentityConfig.ToString());
            parameters.Add(CloudFrontQueryParameter.ContentType, "application/x-www-form-urlencoded; charset=utf-8");
            this.AddCloudFrontQueryParameters(parameters, request.Headers);
            return parameters;
        }

        private IDictionary<CloudFrontQueryParameter, string> ConvertCreateStreamingDistribution(CreateStreamingDistributionRequest request)
        {
            IDictionary<CloudFrontQueryParameter, string> parameters = new Dictionary<CloudFrontQueryParameter, string>(5);
            parameters.Add(CloudFrontQueryParameter.Verb, CloudFrontConstants.PostVerb);
            parameters.Add(CloudFrontQueryParameter.Action, "CreateStreamingDistribution");
            parameters.Add(CloudFrontQueryParameter.ContentBody, request.StreamingDistributionConfig.ToString());
            parameters.Add(CloudFrontQueryParameter.ContentType, "application/x-www-form-urlencoded; charset=utf-8");
            this.AddCloudFrontQueryParameters(parameters, request.Headers);
            return parameters;
        }

        private IDictionary<CloudFrontQueryParameter, string> ConvertDeleteDistribution(DeleteDistributionRequest request)
        {
            IDictionary<CloudFrontQueryParameter, string> parameters = new Dictionary<CloudFrontQueryParameter, string>(5);
            parameters.Add(CloudFrontQueryParameter.Verb, CloudFrontConstants.DeleteVerb);
            parameters.Add(CloudFrontQueryParameter.Action, "DeleteDistribution");
            parameters.Add(CloudFrontQueryParameter.Id, request.Id);
            request.Headers["If-Match"] = request.ETag;
            this.AddCloudFrontQueryParameters(parameters, request.Headers);
            return parameters;
        }

        private IDictionary<CloudFrontQueryParameter, string> ConvertDeleteOriginAccessIdentity(DeleteOriginAccessIdentityRequest request)
        {
            IDictionary<CloudFrontQueryParameter, string> parameters = new Dictionary<CloudFrontQueryParameter, string>(5);
            parameters.Add(CloudFrontQueryParameter.Verb, CloudFrontConstants.DeleteVerb);
            parameters.Add(CloudFrontQueryParameter.Action, "DeleteOriginAccessIdentity");
            parameters.Add(CloudFrontQueryParameter.Id, request.Id);
            request.Headers["If-Match"] = request.ETag;
            this.AddCloudFrontQueryParameters(parameters, request.Headers);
            return parameters;
        }

        private IDictionary<CloudFrontQueryParameter, string> ConvertDeleteStreamingDistribution(DeleteStreamingDistributionRequest request)
        {
            IDictionary<CloudFrontQueryParameter, string> parameters = new Dictionary<CloudFrontQueryParameter, string>(5);
            parameters.Add(CloudFrontQueryParameter.Verb, CloudFrontConstants.DeleteVerb);
            parameters.Add(CloudFrontQueryParameter.Action, "DeleteStreamingDistribution");
            parameters.Add(CloudFrontQueryParameter.Id, request.Id);
            request.Headers["If-Match"] = request.ETag;
            this.AddCloudFrontQueryParameters(parameters, request.Headers);
            return parameters;
        }

        private IDictionary<CloudFrontQueryParameter, string> ConvertGetDistributionConfig(GetDistributionConfigRequest request)
        {
            IDictionary<CloudFrontQueryParameter, string> parameters = new Dictionary<CloudFrontQueryParameter, string>(5);
            parameters.Add(CloudFrontQueryParameter.Verb, CloudFrontConstants.GetVerb);
            parameters.Add(CloudFrontQueryParameter.Action, "GetDistributionConfig");
            parameters.Add(CloudFrontQueryParameter.Query, "/config");
            parameters.Add(CloudFrontQueryParameter.Id, request.Id);
            this.AddCloudFrontQueryParameters(parameters, request.Headers);
            return parameters;
        }

        private IDictionary<CloudFrontQueryParameter, string> ConvertGetDistributionInfo(GetDistributionInfoRequest request)
        {
            IDictionary<CloudFrontQueryParameter, string> parameters = new Dictionary<CloudFrontQueryParameter, string>(5);
            parameters.Add(CloudFrontQueryParameter.Verb, CloudFrontConstants.GetVerb);
            parameters.Add(CloudFrontQueryParameter.Action, "GetDistributionInfo");
            parameters.Add(CloudFrontQueryParameter.Id, request.Id);
            this.AddCloudFrontQueryParameters(parameters, request.Headers);
            return parameters;
        }

        private IDictionary<CloudFrontQueryParameter, string> ConvertGetOriginAccessIdentityConfig(GetOriginAccessIdentityConfigRequest request)
        {
            IDictionary<CloudFrontQueryParameter, string> parameters = new Dictionary<CloudFrontQueryParameter, string>(5);
            parameters.Add(CloudFrontQueryParameter.Verb, CloudFrontConstants.GetVerb);
            parameters.Add(CloudFrontQueryParameter.Action, "GetOriginAccessIdentityConfig");
            parameters.Add(CloudFrontQueryParameter.Query, "/config");
            parameters.Add(CloudFrontQueryParameter.Id, request.Id);
            this.AddCloudFrontQueryParameters(parameters, request.Headers);
            return parameters;
        }

        private IDictionary<CloudFrontQueryParameter, string> ConvertGetOriginAccessIdentityInfo(GetOriginAccessIdentityInfoRequest request)
        {
            IDictionary<CloudFrontQueryParameter, string> parameters = new Dictionary<CloudFrontQueryParameter, string>(5);
            parameters.Add(CloudFrontQueryParameter.Verb, CloudFrontConstants.GetVerb);
            parameters.Add(CloudFrontQueryParameter.Action, "GetOriginAccessIdentityInfo");
            parameters.Add(CloudFrontQueryParameter.Id, request.Id);
            this.AddCloudFrontQueryParameters(parameters, request.Headers);
            return parameters;
        }

        private IDictionary<CloudFrontQueryParameter, string> ConvertGetStreamingDistributionConfig(GetStreamingDistributionConfigRequest request)
        {
            IDictionary<CloudFrontQueryParameter, string> parameters = new Dictionary<CloudFrontQueryParameter, string>(5);
            parameters.Add(CloudFrontQueryParameter.Verb, CloudFrontConstants.GetVerb);
            parameters.Add(CloudFrontQueryParameter.Action, "GetStreamingDistributionConfig");
            parameters.Add(CloudFrontQueryParameter.Query, "/config");
            parameters.Add(CloudFrontQueryParameter.Id, request.Id);
            this.AddCloudFrontQueryParameters(parameters, request.Headers);
            return parameters;
        }

        private IDictionary<CloudFrontQueryParameter, string> ConvertGetStreamingDistributionInfo(GetStreamingDistributionInfoRequest request)
        {
            IDictionary<CloudFrontQueryParameter, string> parameters = new Dictionary<CloudFrontQueryParameter, string>(5);
            parameters.Add(CloudFrontQueryParameter.Verb, CloudFrontConstants.GetVerb);
            parameters.Add(CloudFrontQueryParameter.Action, "GetStreamingDistributionInfo");
            parameters.Add(CloudFrontQueryParameter.Id, request.Id);
            this.AddCloudFrontQueryParameters(parameters, request.Headers);
            return parameters;
        }

        private IDictionary<CloudFrontQueryParameter, string> ConvertListDistributions(ListDistributionsRequest request)
        {
            IDictionary<CloudFrontQueryParameter, string> parameters = new Dictionary<CloudFrontQueryParameter, string>(5);
            parameters.Add(CloudFrontQueryParameter.Verb, CloudFrontConstants.GetVerb);
            parameters.Add(CloudFrontQueryParameter.Action, "ListDistributions");
            StringBuilder builder = new StringBuilder(0x80);
            if (request.IsSetMarker())
            {
                builder.Append("?Marker=").Append(request.Marker);
            }
            if (request.IsSetMaxItems())
            {
                builder.Append("&MaxItems=").Append(request.MaxItems);
            }
            if (builder.Length > 0)
            {
                parameters.Add(CloudFrontQueryParameter.Query, builder.ToString());
            }
            this.AddCloudFrontQueryParameters(parameters, request.Headers);
            return parameters;
        }

        private IDictionary<CloudFrontQueryParameter, string> ConvertListOriginAccessIdentities(ListOriginAccessIdentitiesRequest request)
        {
            IDictionary<CloudFrontQueryParameter, string> parameters = new Dictionary<CloudFrontQueryParameter, string>(5);
            parameters.Add(CloudFrontQueryParameter.Verb, CloudFrontConstants.GetVerb);
            parameters.Add(CloudFrontQueryParameter.Action, "ListOriginAccessIdentities");
            StringBuilder builder = new StringBuilder(0x80);
            if (request.IsSetMarker())
            {
                builder.Append("?Marker=").Append(request.Marker);
            }
            if (request.IsSetMaxItems())
            {
                builder.Append("&MaxItems=").Append(request.MaxItems);
            }
            if (builder.Length > 0)
            {
                parameters.Add(CloudFrontQueryParameter.Query, builder.ToString());
            }
            this.AddCloudFrontQueryParameters(parameters, request.Headers);
            return parameters;
        }

        private IDictionary<CloudFrontQueryParameter, string> ConvertListStreamingDistributions(ListStreamingDistributionsRequest request)
        {
            IDictionary<CloudFrontQueryParameter, string> parameters = new Dictionary<CloudFrontQueryParameter, string>(5);
            parameters.Add(CloudFrontQueryParameter.Verb, CloudFrontConstants.GetVerb);
            parameters.Add(CloudFrontQueryParameter.Action, "ListStreamingDistributions");
            StringBuilder builder = new StringBuilder(0x80);
            if (request.IsSetMarker())
            {
                builder.Append("?Marker=").Append(request.Marker);
            }
            if (request.IsSetMaxItems())
            {
                builder.Append("&MaxItems=").Append(request.MaxItems);
            }
            if (builder.Length > 0)
            {
                parameters.Add(CloudFrontQueryParameter.Query, builder.ToString());
            }
            this.AddCloudFrontQueryParameters(parameters, request.Headers);
            return parameters;
        }

        private IDictionary<CloudFrontQueryParameter, string> ConvertSetDistributionConfig(SetDistributionConfigRequest request)
        {
            IDictionary<CloudFrontQueryParameter, string> parameters = new Dictionary<CloudFrontQueryParameter, string>(5);
            parameters.Add(CloudFrontQueryParameter.Verb, CloudFrontConstants.PutVerb);
            parameters.Add(CloudFrontQueryParameter.Action, "SetDistributionConfig");
            parameters.Add(CloudFrontQueryParameter.Query, "/config");
            parameters.Add(CloudFrontQueryParameter.ContentBody, request.DistributionConfig.ToString());
            parameters.Add(CloudFrontQueryParameter.ContentType, "application/x-www-form-urlencoded; charset=utf-8");
            parameters.Add(CloudFrontQueryParameter.Id, request.Id);
            request.Headers["If-Match"] = request.ETag;
            this.AddCloudFrontQueryParameters(parameters, request.Headers);
            return parameters;
        }

        private IDictionary<CloudFrontQueryParameter, string> ConvertSetOriginAccessIdentityConfig(SetOriginAccessIdentityConfigRequest request)
        {
            IDictionary<CloudFrontQueryParameter, string> parameters = new Dictionary<CloudFrontQueryParameter, string>(5);
            parameters.Add(CloudFrontQueryParameter.Verb, CloudFrontConstants.PutVerb);
            parameters.Add(CloudFrontQueryParameter.Action, "SetOriginAccessIdentityConfig");
            parameters.Add(CloudFrontQueryParameter.Query, "/config");
            parameters.Add(CloudFrontQueryParameter.ContentBody, request.OriginAccessIdentityConfig.ToString());
            parameters.Add(CloudFrontQueryParameter.ContentType, "application/x-www-form-urlencoded; charset=utf-8");
            parameters.Add(CloudFrontQueryParameter.Id, request.Id);
            request.Headers["If-Match"] = request.ETag;
            this.AddCloudFrontQueryParameters(parameters, request.Headers);
            return parameters;
        }

        private IDictionary<CloudFrontQueryParameter, string> ConvertSetStreamingDistributionConfig(SetStreamingDistributionConfigRequest request)
        {
            IDictionary<CloudFrontQueryParameter, string> parameters = new Dictionary<CloudFrontQueryParameter, string>(5);
            parameters.Add(CloudFrontQueryParameter.Verb, CloudFrontConstants.PutVerb);
            parameters.Add(CloudFrontQueryParameter.Action, "SetStreamingDistributionConfig");
            parameters.Add(CloudFrontQueryParameter.Query, "/config");
            parameters.Add(CloudFrontQueryParameter.ContentBody, request.StreamingDistributionConfig.ToString());
            parameters.Add(CloudFrontQueryParameter.ContentType, "application/x-www-form-urlencoded; charset=utf-8");
            parameters.Add(CloudFrontQueryParameter.Id, request.Id);
            request.Headers["If-Match"] = request.ETag;
            this.AddCloudFrontQueryParameters(parameters, request.Headers);
            return parameters;
        }

        public CreateDistributionResponse CreateDistribution(CreateDistributionRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The CreateDistributionRequest specified is null!");
            }
            CloudFrontDistributionConfig distributionConfig = request.DistributionConfig;
            if (distributionConfig == null)
            {
                throw new ArgumentNullException("request", "The request's DistributionConfig is null!");
            }
            if (!distributionConfig.IsSetOrigin())
            {
                throw new ArgumentNullException("request", "The Origin Server Bucket to create the distribution with is null or empty!");
            }
            if (!distributionConfig.IsSetCallerReference())
            {
                throw new ArgumentNullException("request", "The CallerReference to create the distribution with is null or empty!");
            }
            return this.Invoke<CreateDistributionResponse>(this.ConvertCreateDistribution(request), request.Headers);
        }

        public CreateOriginAccessIdentityResponse CreateOriginAccessIdentity(CreateOriginAccessIdentityRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The CreateOriginAccessIdentityRequest specified is null!");
            }
            CloudFrontOriginAccessIdentityConfig originAccessIdentityConfig = request.OriginAccessIdentityConfig;
            if (originAccessIdentityConfig == null)
            {
                throw new ArgumentNullException("request", "The request's OriginAccessIdentityConfig is null!");
            }
            if (!originAccessIdentityConfig.IsSetCallerReference())
            {
                throw new ArgumentNullException("request", "The CallerReference to create the distribution with is null or empty!");
            }
            return this.Invoke<CreateOriginAccessIdentityResponse>(this.ConvertCreateOriginAccessIdentity(request), request.Headers);
        }

        public CreateStreamingDistributionResponse CreateStreamingDistribution(CreateStreamingDistributionRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The CreateStreamingDistributionRequest specified is null!");
            }
            CloudFrontStreamingDistributionConfig streamingDistributionConfig = request.StreamingDistributionConfig;
            if (streamingDistributionConfig == null)
            {
                throw new ArgumentNullException("request", "The request's StreamingDistributionConfig is null!");
            }
            if (!streamingDistributionConfig.IsSetOrigin())
            {
                throw new ArgumentNullException("request", "The Origin Server Bucket to create the distribution with is null or empty!");
            }
            if (!streamingDistributionConfig.IsSetCallerReference())
            {
                throw new ArgumentNullException("request", "The CallerReference to create the distribution with is null or empty!");
            }
            return this.Invoke<CreateStreamingDistributionResponse>(this.ConvertCreateStreamingDistribution(request), request.Headers);
        }

        public DeleteDistributionResponse DeleteDistribution(DeleteDistributionRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The DeleteDistributionRequest specified is null!");
            }
            if (!request.IsSetId())
            {
                throw new ArgumentNullException("request", "The Distribution Id specified is null or empty!");
            }
            if (!request.IsSetETag())
            {
                throw new ArgumentNullException("request", "The Distribution ETag specified is null or empty!");
            }
            return this.Invoke<DeleteDistributionResponse>(this.ConvertDeleteDistribution(request), request.Headers);
        }

        public DeleteOriginAccessIdentityResponse DeleteOriginAccessIdentity(DeleteOriginAccessIdentityRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The DeleteOriginAccessIdentityRequest specified is null!");
            }
            if (!request.IsSetId())
            {
                throw new ArgumentNullException("request", "The OriginAccessIdentity Id specified is null or empty!");
            }
            if (!request.IsSetETag())
            {
                throw new ArgumentNullException("request", "The OriginAccessIdentity ETag specified is null or empty!");
            }
            return this.Invoke<DeleteOriginAccessIdentityResponse>(this.ConvertDeleteOriginAccessIdentity(request), request.Headers);
        }

        public DeleteStreamingDistributionResponse DeleteStreamingDistribution(DeleteStreamingDistributionRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The DeleteStreamingDistributionRequest specified is null!");
            }
            if (!request.IsSetId())
            {
                throw new ArgumentNullException("request", "The StreamingDistribution Id specified is null or empty!");
            }
            if (!request.IsSetETag())
            {
                throw new ArgumentNullException("request", "The StreamingDistribution ETag specified is null or empty!");
            }
            return this.Invoke<DeleteStreamingDistributionResponse>(this.ConvertDeleteStreamingDistribution(request), request.Headers);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool fDisposing)
        {
            if (!this.disposed)
            {
                if (fDisposing && (this.awsSecretAccessKey != null))
                {
                    this.awsSecretAccessKey.Dispose();
                    this.awsSecretAccessKey = null;
                }
                this.disposed = true;
            }
        }

        ~AmazonCloudFrontClient()
        {
            this.Dispose(false);
        }

        public GetDistributionConfigResponse GetDistributionConfig(GetDistributionConfigRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The GetDistributionConfigRequest specified is null!");
            }
            if (!request.IsSetId())
            {
                throw new ArgumentNullException("request", "The Distribution Id specified is null or empty!");
            }
            return this.Invoke<GetDistributionConfigResponse>(this.ConvertGetDistributionConfig(request), request.Headers);
        }

        public GetDistributionInfoResponse GetDistributionInfo(GetDistributionInfoRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The GetDistributionInfoRequest specified is null!");
            }
            if (!request.IsSetId())
            {
                throw new ArgumentNullException("request", "The Distribution Id specified is null or empty!");
            }
            return this.Invoke<GetDistributionInfoResponse>(this.ConvertGetDistributionInfo(request), request.Headers);
        }

        public GetOriginAccessIdentityConfigResponse GetOriginAccessIdentityConfig(GetOriginAccessIdentityConfigRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The GetOriginAccessIdentityConfigRequest specified is null!");
            }
            if (!request.IsSetId())
            {
                throw new ArgumentNullException("request", "The Origin Access Identity Id specified is null or empty!");
            }
            return this.Invoke<GetOriginAccessIdentityConfigResponse>(this.ConvertGetOriginAccessIdentityConfig(request), request.Headers);
        }

        public GetOriginAccessIdentityInfoResponse GetOriginAccessIdentityInfo(GetOriginAccessIdentityInfoRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The GetOriginAccessIdentityInfoRequest specified is null!");
            }
            if (!request.IsSetId())
            {
                throw new ArgumentNullException("request", "The Origin Access Identity Id specified is null or empty!");
            }
            return this.Invoke<GetOriginAccessIdentityInfoResponse>(this.ConvertGetOriginAccessIdentityInfo(request), request.Headers);
        }

        private static void GetServiceSpecificDataFromHeader(CloudFrontResponse response)
        {
            string str;
            if (!string.IsNullOrEmpty(str = response.Headers.Get("x-amzn-RequestId")))
            {
                response.RequestId = str;
            }
            if (!string.IsNullOrEmpty(str = response.Headers.Get("ETag")))
            {
                response.ETag = str;
            }
        }

        public GetStreamingDistributionConfigResponse GetStreamingDistributionConfig(GetStreamingDistributionConfigRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The GetStreamingDistributionConfigRequest specified is null!");
            }
            if (!request.IsSetId())
            {
                throw new ArgumentNullException("request", "The StreamingDistribution Id specified is null or empty!");
            }
            return this.Invoke<GetStreamingDistributionConfigResponse>(this.ConvertGetStreamingDistributionConfig(request), request.Headers);
        }

        public GetStreamingDistributionInfoResponse GetStreamingDistributionInfo(GetStreamingDistributionInfoRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The GetStreamingDistributionInfoRequest specified is null!");
            }
            if (!request.IsSetId())
            {
                throw new ArgumentNullException("request", "The StreamingDistribution Id specified is null or empty!");
            }
            return this.Invoke<GetStreamingDistributionInfoResponse>(this.ConvertGetStreamingDistributionInfo(request), request.Headers);
        }

        private T Invoke<T>(IDictionary<CloudFrontQueryParameter, string> parameters, WebHeaderCollection headers) where T: CloudFrontResponse, new()
        {
            string actionName = parameters[CloudFrontQueryParameter.Action];
            T local = default(T);
            HttpStatusCode status = (HttpStatusCode) 0;
            string str2 = parameters[CloudFrontQueryParameter.Verb];
            byte[] bytes = Encoding.UTF8.GetBytes("");
            long contentLength = 0L;
            if (string.IsNullOrEmpty(this.awsAccessKeyId))
            {
                throw new AmazonCloudFrontException("The AWS Access Key ID cannot be NULL or a Zero length string");
            }
            if ((!str2.Equals(CloudFrontConstants.PostVerb) && !str2.Equals(CloudFrontConstants.PutVerb)) && (!str2.Equals(CloudFrontConstants.GetVerb) && !str2.Equals(CloudFrontConstants.DeleteVerb)))
            {
                throw new AmazonCloudFrontException("Invalid HTTP Operation attempted! Supported operations - GET, POST, PUT, DELETE");
            }
            if (parameters.ContainsKey(CloudFrontQueryParameter.ContentBody))
            {
                string s = parameters[CloudFrontQueryParameter.ContentBody];
                bytes = Encoding.UTF8.GetBytes(s);
                contentLength = bytes.Length;
            }
            bool flag = false;
            int num2 = 0;
            if (this.config.IsSetMaxErrorRetry())
            {
                int maxErrorRetry = this.config.MaxErrorRetry;
            }
            do
            {
                HttpWebRequest request = this.ConfigureWebRequest(parameters, headers, contentLength);
                string requestAddr = request.Address.ToString();
                try
                {
                    if (contentLength > 0L)
                    {
                        using (Stream stream = request.GetRequestStream())
                        {
                            using (MemoryStream stream2 = new MemoryStream(bytes))
                            {
                                byte[] buffer = new byte[0x8000];
                                int count = 0;
                                while ((count = stream2.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    stream.Write(buffer, 0, count);
                                }
                            }
                        }
                    }
                    local = this.ProcessRequestResponse<T>(request, actionName);
                    flag = false;
                }
                catch (WebException exception)
                {
                    WebHeaderCollection headers2;
                    if (ProcessRequestError(request, exception, requestAddr, out headers2))
                    {
                        PauseOnRetry(++num2, this.config.IsSetMaxErrorRetry() ? this.config.MaxErrorRetry : defaultRetry, status, requestAddr, headers2);
                    }
                }
                catch (IOException)
                {
                    request.Abort();
                    throw;
                }
            }
            while (flag && (num2 <= this.config.MaxErrorRetry));
            return local;
        }

        public ListDistributionsResponse ListDistributions()
        {
            return this.ListDistributions(new ListDistributionsRequest());
        }

        public ListDistributionsResponse ListDistributions(ListDistributionsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The ListDistributionsRequest specified is null!");
            }
            return this.Invoke<ListDistributionsResponse>(this.ConvertListDistributions(request), request.Headers);
        }

        public ListOriginAccessIdentitiesResponse ListOriginAccessIdentities()
        {
            return this.ListOriginAccessIdentities(new ListOriginAccessIdentitiesRequest());
        }

        public ListOriginAccessIdentitiesResponse ListOriginAccessIdentities(ListOriginAccessIdentitiesRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The ListOriginAccessIdentitiesRequest specified is null!");
            }
            return this.Invoke<ListOriginAccessIdentitiesResponse>(this.ConvertListOriginAccessIdentities(request), request.Headers);
        }

        public ListStreamingDistributionsResponse ListStreamingDistributions()
        {
            return this.ListStreamingDistributions(new ListStreamingDistributionsRequest());
        }

        public ListStreamingDistributionsResponse ListStreamingDistributions(ListStreamingDistributionsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The ListStreamingDistributionsRequest specified is null!");
            }
            return this.Invoke<ListStreamingDistributionsResponse>(this.ConvertListStreamingDistributions(request), request.Headers);
        }

        private static void PauseOnRetry(int retries, int maxRetries, HttpStatusCode status, string requestAddr, WebHeaderCollection headers)
        {
            if (retries > maxRetries)
            {
                throw new AmazonCloudFrontException("Maximum number of retry attempts reached : " + (retries - 1), status, "", "", "", "", requestAddr, headers);
            }
            int millisecondsTimeout = ((int) Math.Pow(4.0, (double) retries)) * 100;
            Thread.Sleep(millisecondsTimeout);
        }

        private static bool ProcessRequestError(HttpWebRequest request, WebException we, string requestAddr, out WebHeaderCollection respHdrs)
        {
            bool flag = false;
            HttpStatusCode statusCode = (HttpStatusCode) 0;
            string str = null;
            respHdrs = null;
            using (HttpWebResponse response = we.Response as HttpWebResponse)
            {
                if (response == null)
                {
                    request.Abort();
                    throw we;
                }
                respHdrs = response.Headers;
                statusCode = response.StatusCode;
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    str = reader.ReadToEnd();
                }
                request.Abort();
            }
            switch (statusCode)
            {
                case HttpStatusCode.InternalServerError:
                case HttpStatusCode.ServiceUnavailable:
                    return true;
            }
            if (string.IsNullOrEmpty(str))
            {
                return flag;
            }
            using (XmlTextReader reader2 = new XmlTextReader(new StringReader(str)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ErrorResponse));
                ErrorResponse response2 = (ErrorResponse) serializer.Deserialize(reader2);
                CloudFrontError error = response2.Error[0];
                throw new AmazonCloudFrontException(error.Message, statusCode, error.Code, error.RequestId, error.Type, str, requestAddr, respHdrs);
            }
        }

        private T ProcessRequestResponse<T>(HttpWebRequest request, string actionName) where T: CloudFrontResponse, new()
        {
            T local = default(T);
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response == null)
                {
                    throw new WebException("The Web Response for a successful request is null!", WebExceptionStatus.ProtocolError);
                }
                WebHeaderCollection headers = response.Headers;
                string str = null;
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    str = reader.ReadToEnd();
                }
                if (!string.IsNullOrEmpty(str) && str.EndsWith(">"))
                {
                    string s = Transform(str, actionName, base.GetType());
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    using (XmlTextReader reader2 = new XmlTextReader(new StringReader(s)))
                    {
                        local = (T) serializer.Deserialize(reader2);
                        goto Label_00BA;
                    }
                }
                local = Activator.CreateInstance<T>();
            Label_00BA:
                if (local != null)
                {
                    local.Headers = headers;
                    local.XML = str;
                    GetServiceSpecificDataFromHeader(local);
                }
            }
            return local;
        }

        public SetDistributionConfigResponse SetDistributionConfig(SetDistributionConfigRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The SetDistributionConfigRequest specified is null!");
            }
            if (!request.IsSetId())
            {
                throw new ArgumentNullException("request", "The Distribution Id specified is null or empty!");
            }
            if (!request.IsSetDistributionConfig())
            {
                throw new ArgumentNullException("request", "No Distribution Config specified!");
            }
            if (!request.IsSetETag())
            {
                throw new ArgumentNullException("request", "The Distribution ETag specified is null or empty!");
            }
            return this.Invoke<SetDistributionConfigResponse>(this.ConvertSetDistributionConfig(request), request.Headers);
        }

        public SetOriginAccessIdentityConfigResponse SetOriginAccessIdentityConfig(SetOriginAccessIdentityConfigRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The SetOriginAccessIdentityConfigRequest specified is null!");
            }
            if (!request.IsSetId())
            {
                throw new ArgumentNullException("request", "The Origin Access Identity Id specified is null or empty!");
            }
            if (!request.IsSetOriginAccessIdentityConfig())
            {
                throw new ArgumentNullException("request", "No OriginAccessIdentity Config specified!");
            }
            if (!request.IsSetETag())
            {
                throw new ArgumentNullException("request", "The OriginAccessIdentity ETag specified is null or empty!");
            }
            return this.Invoke<SetOriginAccessIdentityConfigResponse>(this.ConvertSetOriginAccessIdentityConfig(request), request.Headers);
        }

        public SetStreamingDistributionConfigResponse SetStreamingDistributionConfig(SetStreamingDistributionConfigRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The SetStreamingDistributionConfigRequest specified is null!");
            }
            if (!request.IsSetId())
            {
                throw new ArgumentNullException("request", "The StreamingDistribution Id specified is null or empty!");
            }
            if (!request.IsSetStreamingDistributionConfig())
            {
                throw new ArgumentNullException("request", "No StreamingDistribution Config specified!");
            }
            if (!request.IsSetETag())
            {
                throw new ArgumentNullException("request", "The StreamingDistribution ETag specified is null or empty!");
            }
            return this.Invoke<SetStreamingDistributionConfigResponse>(this.ConvertSetStreamingDistributionConfig(request), request.Headers);
        }

        private static string Transform(string responseBody, string actionName, Type t)
        {
            string str4;
            XslCompiledTransform transform = new XslCompiledTransform();
            char[] separator = new char[] { ',' };
            Assembly assembly = Assembly.GetAssembly(t);
            string str = assembly.FullName.Split(separator)[0];
            string str2 = t.Namespace;
            string name = str + "." + str2 + ".Model." + actionName + "Response.xslt";
            using (XmlTextReader reader = new XmlTextReader(assembly.GetManifestResourceStream(name)))
            {
                transform.Load(reader);
                using (XmlTextReader reader2 = new XmlTextReader(new StringReader(responseBody)))
                {
                    StringBuilder sb = new StringBuilder(0x400);
                    using (StringWriter writer = new StringWriter(sb))
                    {
                        transform.Transform((XmlReader) reader2, null, (TextWriter) writer);
                        str4 = sb.ToString();
                    }
                }
            }
            return str4;
        }
    }
}

