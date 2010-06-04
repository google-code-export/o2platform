namespace Amazon.S3
{
    using Amazon.S3.Model;
    using Amazon.S3.Util;
    using Amazon.Util;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
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

    public class AmazonS3Client : AmazonS3, IDisposable
    {
        private readonly string awsAccessKeyId;
        private SecureString awsSecretAccessKey;
        private readonly string clearAwsSecretAccessKey;
        private AmazonS3Config config;
        private bool disposed;
        private Type myType;

        public AmazonS3Client(string awsAccessKeyId, string awsSecretAccessKey) : this(awsAccessKeyId, awsSecretAccessKey, new AmazonS3Config())
        {
        }

        public AmazonS3Client(string awsAccessKeyId, SecureString awsSecretAccessKey, AmazonS3Config config)
        {
            this.awsAccessKeyId = awsAccessKeyId;
            this.awsSecretAccessKey = awsSecretAccessKey;
            this.config = config;
            this.myType = base.GetType();
        }

        public AmazonS3Client(string awsAccessKeyId, string awsSecretAccessKey, AmazonS3Config config)
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
                else
                {
                    this.clearAwsSecretAccessKey = awsSecretAccessKey;
                }
            }
            this.awsAccessKeyId = awsAccessKeyId;
            this.config = config;
            this.myType = base.GetType();
        }

        private void AddS3QueryParameters(S3Request request, string destinationBucket)
        {
            if (request != null)
            {
                string str3;
                IDictionary<S3QueryParameter, string> parameters = request.parameters;
                WebHeaderCollection webHeaders = request.Headers;
                if (webHeaders != null)
                {
                    webHeaders["x-amz-date"] = AmazonS3Util.FormattedCurrentTimestamp;
                }
                StringBuilder builder = new StringBuilder("/", 0x200);
                if (!string.IsNullOrEmpty(destinationBucket))
                {
                    parameters[S3QueryParameter.DestinationBucket] = destinationBucket;
                    if (AmazonS3Util.ValidateV2Bucket(destinationBucket))
                    {
                        parameters[S3QueryParameter.BucketVersion] = S3Constants.BucketVersions[2];
                    }
                    else
                    {
                        parameters[S3QueryParameter.BucketVersion] = S3Constants.BucketVersions[1];
                    }
                    builder.Append(destinationBucket);
                    if (!destinationBucket.EndsWith("/"))
                    {
                        builder.Append("/");
                    }
                }
                else
                {
                    parameters[S3QueryParameter.BucketVersion] = S3Constants.BucketVersions[2];
                }
                if (parameters.ContainsKey(S3QueryParameter.Key))
                {
                    builder.Append(parameters[S3QueryParameter.Key]);
                }
                parameters[S3QueryParameter.CanonicalizedResource] = builder.ToString();
                string str = webHeaders["Content-Type"];
                if (!string.IsNullOrEmpty(str))
                {
                    parameters[S3QueryParameter.ContentType] = str;
                    webHeaders.Remove("Content-Type");
                }
                string data = BuildSigningString(parameters, webHeaders);
                if (this.config.UseSecureStringForAwsSecretKey)
                {
                    KeyedHashAlgorithm algorithm = new HMACSHA1();
                    str3 = AWSSDKUtils.HMACSign(data, this.awsSecretAccessKey, algorithm);
                }
                else
                {
                    KeyedHashAlgorithm algorithm2 = new HMACSHA1();
                    str3 = AWSSDKUtils.HMACSign(data, this.clearAwsSecretAccessKey, algorithm2);
                }
                parameters[S3QueryParameter.Authorization] = str3;
                AddUrlToParameters(request, this.config);
            }
        }

        private static void AddUrlToParameters(S3Request request, AmazonS3Config config)
        {
            IDictionary<S3QueryParameter, string> parameters = request.parameters;
            if (!config.IsSetServiceURL())
            {
                throw new AmazonS3Exception("The Amazon S3 Service URL is either null or empty");
            }
            string serviceURL = config.ServiceURL;
            if (parameters[S3QueryParameter.BucketVersion].Equals(S3Constants.BucketVersions[1]))
            {
                serviceURL = serviceURL + parameters[S3QueryParameter.CanonicalizedResource];
            }
            else if (parameters.ContainsKey(S3QueryParameter.DestinationBucket))
            {
                serviceURL = parameters[S3QueryParameter.DestinationBucket] + "." + serviceURL + "/";
                if (parameters.ContainsKey(S3QueryParameter.Key))
                {
                    serviceURL = serviceURL + parameters[S3QueryParameter.Key];
                }
            }
            string str2 = "https://";
            if (config.CommunicationProtocol == Protocol.HTTP)
            {
                str2 = "http://";
            }
            serviceURL = AmazonS3Util.UrlEncode(str2 + serviceURL, true);
            if (parameters.ContainsKey(S3QueryParameter.Query))
            {
                serviceURL = serviceURL + parameters[S3QueryParameter.Query];
            }
            parameters[S3QueryParameter.Url] = serviceURL;
        }

        private static StringBuilder BuildCanonicalizedHeaders(WebHeaderCollection headers)
        {
            List<string> list = new List<string>(headers.Count);
            foreach (string str in headers.AllKeys)
            {
                string item = str.ToLower();
                if (item.StartsWith("x-amz-"))
                {
                    list.Add(item);
                }
            }
            list.Sort(StringComparer.Ordinal);
            StringBuilder builder = new StringBuilder(0x100);
            foreach (string str3 in list)
            {
                builder.Append(str3 + ":" + headers[str3] + "\n");
            }
            return builder;
        }

        private static string BuildSigningString(IDictionary<S3QueryParameter, string> parameters, WebHeaderCollection webHeaders)
        {
            StringBuilder builder = new StringBuilder("", 0x100);
            string str = null;
            builder.Append(parameters[S3QueryParameter.Verb]);
            builder.Append("\n");
            if (webHeaders != null)
            {
                if (!string.IsNullOrEmpty(str = webHeaders["Content-MD5"]))
                {
                    builder.Append(str);
                }
                builder.Append("\n");
                if (parameters.ContainsKey(S3QueryParameter.ContentType))
                {
                    builder.Append(parameters[S3QueryParameter.ContentType]);
                }
                builder.Append("\n");
            }
            else
            {
                builder.Append("\n\n");
            }
            if (parameters.ContainsKey(S3QueryParameter.Expires))
            {
                builder.Append(parameters[S3QueryParameter.Expires]);
                builder.Append("\n");
            }
            else
            {
                builder.Append("\n");
                builder.Append(BuildCanonicalizedHeaders(webHeaders));
            }
            if (parameters.ContainsKey(S3QueryParameter.CanonicalizedResource))
            {
                builder.Append(AmazonS3Util.UrlEncode(parameters[S3QueryParameter.CanonicalizedResource], true));
            }
            string local1 = parameters[S3QueryParameter.Action];
            if (parameters.ContainsKey(S3QueryParameter.QueryToSign))
            {
                builder.Append(parameters[S3QueryParameter.QueryToSign]);
            }
            return builder.ToString();
        }

        private HttpWebRequest ConfigureWebRequest(S3Request request, long contentLength)
        {
            WebHeaderCollection c = request.Headers;
            IDictionary<S3QueryParameter, string> parameters = request.parameters;
            if (!parameters.ContainsKey(S3QueryParameter.Url))
            {
                throw new AmazonS3Exception("The Amazon S3 URL is either null or empty");
            }
            string requestUriString = parameters[S3QueryParameter.Url];
            HttpWebRequest request2 = WebRequest.Create(requestUriString) as HttpWebRequest;
            if (request != null)
            {
                if (this.config.IsSetProxyHost() && this.config.IsSetProxyPort())
                {
                    WebProxy proxy = new WebProxy(this.config.ProxyHost, this.config.ProxyPort);
                    if (this.config.IsSetProxyUsername())
                    {
                        proxy.Credentials = new NetworkCredential(this.config.ProxyUsername, this.config.ProxyPassword ?? string.Empty);
                    }
                    request2.Proxy = proxy;
                }
                request2.UserAgent = this.config.UserAgent;
                string str2 = c["IfModifiedSince"];
                if (!string.IsNullOrEmpty(str2))
                {
                    DateTime time = DateTime.ParseExact(str2, @"ddd, dd MMM yyyy HH:mm:ss \G\M\T", null);
                    request2.IfModifiedSince = time;
                    c.Remove("IfModifiedSince");
                    request.removedHeaders["IfModifiedSince"] = str2;
                }
                str2 = c["Content-Type"];
                if (!string.IsNullOrEmpty(str2))
                {
                    request2.ContentType = str2;
                    c.Remove("Content-Type");
                    request.removedHeaders["Content-Type"] = str2;
                }
                if (parameters.ContainsKey(S3QueryParameter.ContentType))
                {
                    request2.ContentType = parameters[S3QueryParameter.ContentType];
                }
                if (parameters.ContainsKey(S3QueryParameter.Range))
                {
                    string str3 = parameters[S3QueryParameter.Range];
                    char[] separator = new char[] { ':' };
                    string[] strArray = str3.Split(separator);
                    request2.AddRange(int.Parse(strArray[0]), int.Parse(strArray[1]));
                }
                request2.Headers["Authorization"] = "AWS " + this.awsAccessKeyId + ":" + parameters[S3QueryParameter.Authorization];
                if (this.config.IsSetUserAgent())
                {
                    request2.UserAgent = this.config.UserAgent;
                }
                str2 = parameters[S3QueryParameter.Action];
                request2.ServicePoint.Expect100Continue = str2.Equals("PutObject");
                if (str2.Equals("PutObject") || str2.Equals("CopyObject"))
                {
                    int result = 0x124f80;
                    int.TryParse(parameters[S3QueryParameter.RequestTimeout], out result);
                    request2.ReadWriteTimeout = result;
                    request2.Timeout = result;
                }
                request2.Headers.Add(c);
                request2.Method = parameters[S3QueryParameter.Verb];
                request2.ContentLength = contentLength;
                request2.KeepAlive = false;
                request2.AllowWriteStreamBuffering = false;
                request2.AllowAutoRedirect = false;
            }
            return request2;
        }

        private void ConvertCopyObject(CopyObjectRequest request)
        {
            IDictionary<S3QueryParameter, string> parameters = request.parameters;
            WebHeaderCollection headers = request.Headers;
            parameters[S3QueryParameter.Verb] = S3Constants.PutVerb;
            parameters[S3QueryParameter.Action] = "CopyObject";
            if (request.IsSetDestinationKey())
            {
                parameters[S3QueryParameter.Key] = request.DestinationKey;
            }
            else
            {
                parameters[S3QueryParameter.Key] = request.SourceKey;
            }
            parameters[S3QueryParameter.RequestTimeout] = request.Timeout.ToString();
            if (request.IsSetETagToMatch())
            {
                SetIfMatchCopyHeader(headers, request.ETagToMatch);
            }
            if (request.IsSetETagToNotMatch())
            {
                SetIfNoneMatchCopyHeader(headers, request.ETagToNotMatch);
            }
            if (request.IsSetModifiedSinceDate())
            {
                SetIfModifiedSinceCopyHeader(headers, request.ModifiedSinceDate);
            }
            if (request.IsSetUnmodifiedSinceDate())
            {
                SetIfUnmodifiedSinceCopyHeader(headers, request.UnmodifiedSinceDate);
            }
            string sourceKey = request.SourceKey;
            if (request.IsSetSourceVersionId())
            {
                sourceKey = sourceKey + "?versionId=" + request.SourceVersionId;
            }
            SetCopySourceHeader(headers, request.SourceBucket, sourceKey);
            SetMetadataDirectiveHeader(headers, request.Directive);
            if (request.Directive == S3MetadataDirective.REPLACE)
            {
                if (request.IsSetMetaData())
                {
                    foreach (string str2 in request.metaData)
                    {
                        headers["x-amz-meta-" + str2] = request.metaData[str2];
                    }
                }
                if (request.IsSetContentType())
                {
                    parameters[S3QueryParameter.ContentType] = request.ContentType;
                }
                else if (request.IsSetDestinationKey())
                {
                    string extension = Path.GetExtension(request.DestinationKey);
                    if (string.IsNullOrEmpty(extension))
                    {
                        extension = Path.GetExtension(request.SourceKey);
                    }
                    if (!string.IsNullOrEmpty(extension))
                    {
                        parameters[S3QueryParameter.ContentType] = AmazonS3Util.MimeTypeFromExtension(extension);
                    }
                }
            }
            if (request.IsSetCannedACL())
            {
                SetCannedACLHeader(headers, request.CannedACL);
            }
            headers["x-amz-storage-class"] = S3Constants.StorageClasses[(int) request.StorageClass];
            this.AddS3QueryParameters(request, request.DestinationBucket);
        }

        private void ConvertDeleteBucket(DeleteBucketRequest request)
        {
            IDictionary<S3QueryParameter, string> parameters = request.parameters;
            parameters[S3QueryParameter.Verb] = S3Constants.DeleteVerb;
            parameters[S3QueryParameter.Action] = "DeleteBucket";
            this.AddS3QueryParameters(request, request.BucketName);
        }

        private void ConvertDeleteObject(DeleteObjectRequest request)
        {
            IDictionary<S3QueryParameter, string> parameters = request.parameters;
            parameters[S3QueryParameter.Verb] = S3Constants.DeleteVerb;
            parameters[S3QueryParameter.Action] = "DeleteObject";
            parameters[S3QueryParameter.Key] = request.Key;
            if (request.IsSetVersionId())
            {
                string str = "?versionId=" + request.VersionId;
                parameters[S3QueryParameter.Query] = str;
                parameters[S3QueryParameter.QueryToSign] = str;
            }
            if (request.IsSetMfaCodes())
            {
                SetMfaHeader(request.Headers, request.MfaCodes);
            }
            this.AddS3QueryParameters(request, request.BucketName);
        }

        private void ConvertDisableBucketLogging(DisableBucketLoggingRequest request)
        {
            this.ConvertEnableBucketLogging(request);
        }

        private void ConvertEnableBucketLogging(EnableBucketLoggingRequest request)
        {
            IDictionary<S3QueryParameter, string> parameters = request.parameters;
            WebHeaderCollection headers = request.Headers;
            parameters[S3QueryParameter.ContentBody] = request.LoggingConfig.ToString();
            parameters[S3QueryParameter.ContentType] = "application/x-www-form-urlencoded; charset=utf-8";
            parameters[S3QueryParameter.Verb] = S3Constants.PutVerb;
            parameters[S3QueryParameter.Action] = "SetBucketLogging";
            parameters[S3QueryParameter.Query] = "?logging";
            parameters[S3QueryParameter.QueryToSign] = "?logging";
            this.AddS3QueryParameters(request, request.BucketName);
        }

        private void ConvertGetACL(GetACLRequest request)
        {
            IDictionary<S3QueryParameter, string> parameters = request.parameters;
            parameters[S3QueryParameter.Verb] = S3Constants.GetVerb;
            parameters[S3QueryParameter.Action] = "GetACL";
            string str = "?acl";
            if (request.IsSetKey())
            {
                parameters[S3QueryParameter.Key] = request.Key;
                if (request.IsSetVersionId())
                {
                    str = str + "&versionId=" + request.VersionId;
                }
            }
            parameters[S3QueryParameter.Query] = str;
            parameters[S3QueryParameter.QueryToSign] = str;
            this.AddS3QueryParameters(request, request.BucketName);
        }

        private void ConvertGetBucketLocation(GetBucketLocationRequest request)
        {
            IDictionary<S3QueryParameter, string> parameters = request.parameters;
            parameters[S3QueryParameter.Verb] = S3Constants.GetVerb;
            parameters[S3QueryParameter.Action] = "GetBucketLocation";
            parameters[S3QueryParameter.Query] = parameters[S3QueryParameter.QueryToSign] = "?location";
            this.AddS3QueryParameters(request, request.BucketName);
        }

        private void ConvertGetBucketLogging(GetBucketLoggingRequest request)
        {
            IDictionary<S3QueryParameter, string> parameters = request.parameters;
            parameters[S3QueryParameter.Verb] = S3Constants.GetVerb;
            parameters[S3QueryParameter.Action] = "GetBucketLogging";
            parameters[S3QueryParameter.Query] = "?logging";
            parameters[S3QueryParameter.QueryToSign] = "?logging";
            this.AddS3QueryParameters(request, request.BucketName);
        }

        private void ConvertGetBucketVersioning(GetBucketVersioningRequest request)
        {
            IDictionary<S3QueryParameter, string> parameters = request.parameters;
            parameters[S3QueryParameter.Verb] = HttpVerb.GET.ToString();
            parameters[S3QueryParameter.Action] = "GetBucketVersioning";
            parameters[S3QueryParameter.Query] = "?versioning";
            parameters[S3QueryParameter.QueryToSign] = "?versioning";
            this.AddS3QueryParameters(request, request.BucketName);
        }

        private void ConvertGetObject(GetObjectRequest request)
        {
            IDictionary<S3QueryParameter, string> parameters = request.parameters;
            WebHeaderCollection headers = request.Headers;
            parameters[S3QueryParameter.Verb] = S3Constants.GetVerb;
            parameters[S3QueryParameter.Action] = "GetObject";
            parameters[S3QueryParameter.Key] = request.Key;
            if (request.IsSetByteRange())
            {
                parameters[S3QueryParameter.Range] = request.ByteRange.First + ":" + request.ByteRange.Second;
            }
            if (request.IsSetETagToMatch())
            {
                SetIfMatchHeader(headers, request.ETagToMatch);
            }
            if (request.IsSetETagToNotMatch())
            {
                SetIfNoneMatchHeader(headers, request.ETagToNotMatch);
            }
            if (request.IsSetModifiedSinceDate())
            {
                SetIfModifiedSinceHeader(headers, request.ModifiedSinceDate);
            }
            if (request.IsSetUnmodifiedSinceDate())
            {
                SetIfUnmodifiedSinceHeader(headers, request.UnmodifiedSinceDate);
            }
            if (request.IsSetVersionId())
            {
                string str = "?versionId=" + request.VersionId;
                parameters[S3QueryParameter.Query] = str;
                parameters[S3QueryParameter.QueryToSign] = str;
            }
            this.AddS3QueryParameters(request, request.BucketName);
        }

        private void ConvertGetObjectMetadata(GetObjectMetadataRequest request)
        {
            IDictionary<S3QueryParameter, string> parameters = request.parameters;
            WebHeaderCollection headers = request.Headers;
            parameters[S3QueryParameter.Verb] = S3Constants.HeadVerb;
            parameters[S3QueryParameter.Action] = "GetObjectMetadata";
            parameters[S3QueryParameter.Key] = request.Key;
            if (request.IsSetETagToNotMatch())
            {
                SetIfNoneMatchHeader(headers, request.ETagToNotMatch);
            }
            if (request.IsSetModifiedSinceDate())
            {
                SetIfModifiedSinceHeader(headers, request.ModifiedSinceDate);
            }
            if (request.IsSetUnmodifiedSinceDate())
            {
                SetIfUnmodifiedSinceHeader(headers, request.UnmodifiedSinceDate);
            }
            if (request.IsSetVersionId())
            {
                string str = "?versionId=" + request.VersionId;
                parameters[S3QueryParameter.Query] = str;
                parameters[S3QueryParameter.QueryToSign] = str;
            }
            this.AddS3QueryParameters(request, request.BucketName);
        }

        private void ConvertGetPreSignedUrl(GetPreSignedUrlRequest request)
        {
            IDictionary<S3QueryParameter, string> parameters = request.parameters;
            parameters[S3QueryParameter.Verb] = S3Constants.Verbs[(int) request.Verb];
            parameters[S3QueryParameter.Action] = "GetPreSignedUrl";
            StringBuilder builder = new StringBuilder("?AWSAccessKeyId=", 0x200);
            builder.Append(this.awsAccessKeyId);
            if (request.IsSetKey())
            {
                parameters[S3QueryParameter.Key] = request.Key;
            }
            else
            {
                if (request.Verb != HttpVerb.HEAD)
                {
                    throw new ArgumentNullException("request", "The Key must be set for GET and PUT requests");
                }
                builder.Append("&max-keys=0");
            }
            builder.Append("&Expires=");
            TimeSpan span = (TimeSpan) (request.Expires.ToUniversalTime() - new DateTime(0x7b2, 1, 1));
            string str = Convert.ToInt64(span.TotalSeconds).ToString();
            builder.Append(str);
            parameters[S3QueryParameter.Expires] = str;
            if ((request.IsSetKey() && request.IsSetVersionId()) && (request.Verb < HttpVerb.PUT))
            {
                string str2 = "?versionId=" + request.VersionId;
                parameters[S3QueryParameter.QueryToSign] = str2;
                builder.Append("&versionId=" + request.VersionId);
            }
            parameters[S3QueryParameter.Query] = builder.ToString();
            this.AddS3QueryParameters(request, request.BucketName);
            string str3 = request.parameters[S3QueryParameter.Url];
            if (request.Protocol != this.config.CommunicationProtocol)
            {
                switch (this.config.CommunicationProtocol)
                {
                    case Protocol.HTTPS:
                        str3 = str3.Replace("https://", "http://");
                        break;

                    case Protocol.HTTP:
                        str3 = str3.Replace("http://", "https://");
                        break;
                }
            }
            parameters[S3QueryParameter.Url] = str3 + "&Signature=" + AmazonS3Util.UrlEncode(request.parameters[S3QueryParameter.Authorization], false);
        }

        private void ConvertListBuckets(ListBucketsRequest request)
        {
            IDictionary<S3QueryParameter, string> parameters = request.parameters;
            parameters[S3QueryParameter.Verb] = S3Constants.GetVerb;
            parameters[S3QueryParameter.Action] = "ListBuckets";
            this.AddS3QueryParameters(request, null);
        }

        private void ConvertListObjects(ListObjectsRequest request)
        {
            IDictionary<S3QueryParameter, string> parameters = request.parameters;
            StringBuilder builder = new StringBuilder("?", 0x100);
            if (request.IsSetPrefix())
            {
                builder.Append("prefix=" + AmazonS3Util.UrlEncode(request.Prefix, false) + "&");
            }
            if (request.IsSetMarker())
            {
                builder.Append("marker=" + AmazonS3Util.UrlEncode(request.Marker, false) + "&");
            }
            if (request.IsSetDelimiter())
            {
                builder.Append("delimiter=" + AmazonS3Util.UrlEncode(request.Delimiter, false) + "&");
            }
            if (request.IsSetMaxKeys())
            {
                builder.Append("max-keys=" + request.MaxKeys + "&");
            }
            string str = builder.ToString();
            if (str.EndsWith("&"))
            {
                str = str.Remove(str.Length - 1);
            }
            if (str.Length > 1)
            {
                parameters[S3QueryParameter.Query] = str;
            }
            parameters[S3QueryParameter.Verb] = S3Constants.GetVerb;
            parameters[S3QueryParameter.Action] = "ListObjects";
            this.AddS3QueryParameters(request, request.BucketName);
        }

        private void ConvertListVersions(ListVersionsRequest request)
        {
            IDictionary<S3QueryParameter, string> parameters = request.parameters;
            StringBuilder builder = new StringBuilder("?versions", 0x100);
            parameters[S3QueryParameter.QueryToSign] = builder.ToString();
            if (request.IsSetPrefix())
            {
                builder.Append("&prefix=" + AmazonS3Util.UrlEncode(request.Prefix, false));
            }
            if (request.IsSetKeyMarker())
            {
                builder.Append("&key-marker=" + AmazonS3Util.UrlEncode(request.KeyMarker, false));
            }
            if (request.IsSetVersionIdMarker())
            {
                builder.Append("&version-id-marker=" + AmazonS3Util.UrlEncode(request.VersionIdMarker, false));
            }
            if (request.IsSetDelimiter())
            {
                builder.Append("&delimiter=" + AmazonS3Util.UrlEncode(request.Delimiter, false));
            }
            if (request.IsSetMaxKeys())
            {
                builder.Append("&max-keys=" + request.MaxKeys);
            }
            parameters[S3QueryParameter.Query] = builder.ToString();
            parameters[S3QueryParameter.Verb] = S3Constants.GetVerb;
            parameters[S3QueryParameter.Action] = "ListVersions";
            this.AddS3QueryParameters(request, request.BucketName);
        }

        private void ConvertPutBucket(PutBucketRequest request)
        {
            IDictionary<S3QueryParameter, string> parameters = request.parameters;
            parameters[S3QueryParameter.Verb] = S3Constants.PutVerb;
            parameters[S3QueryParameter.Action] = "PutBucket";
            if (request.BucketRegion > S3Region.US)
            {
                string str = string.Format("<CreateBucketConstraint><LocationConstraint>{0}</LocationConstraint></CreateBucketConstraint>", S3Constants.LocationConstraints[(int) request.BucketRegion]);
                parameters[S3QueryParameter.ContentBody] = str;
                parameters[S3QueryParameter.ContentType] = "application/x-www-form-urlencoded; charset=utf-8";
            }
            this.AddS3QueryParameters(request, request.BucketName);
        }

        private void ConvertPutObject(PutObjectRequest request)
        {
            IDictionary<S3QueryParameter, string> parameters = request.parameters;
            WebHeaderCollection headers = request.Headers;
            parameters[S3QueryParameter.Verb] = S3Constants.PutVerb;
            parameters[S3QueryParameter.Action] = "PutObject";
            parameters[S3QueryParameter.Key] = request.Key;
            if (request.IsSetMD5Digest())
            {
                headers["Content-MD5"] = request.MD5Digest;
            }
            else if (request.GenerateMD5Digest)
            {
                string str = null;
                if (request.IsSetInputStream())
                {
                    str = AmazonS3Util.GenerateChecksumForStream(request.InputStream, true);
                }
                else
                {
                    str = AmazonS3Util.GenerateChecksumForContent(request.ContentBody, true);
                }
                headers["Content-MD5"] = str;
            }
            if (request.IsSetContentType())
            {
                parameters[S3QueryParameter.ContentType] = request.ContentType;
            }
            else if (request.IsSetFilePath() || request.IsSetKey())
            {
                string extension = Path.GetExtension(request.FilePath);
                if (string.IsNullOrEmpty(extension) && request.IsSetKey())
                {
                    extension = Path.GetExtension(request.Key);
                }
                if (!string.IsNullOrEmpty(extension))
                {
                    parameters[S3QueryParameter.ContentType] = AmazonS3Util.MimeTypeFromExtension(extension);
                }
            }
            if (request.IsSetInputStream())
            {
                parameters[S3QueryParameter.ContentLength] = request.InputStream.Length.ToString();
            }
            if (request.IsSetContentBody())
            {
                parameters[S3QueryParameter.ContentBody] = request.ContentBody;
                if (!parameters.ContainsKey(S3QueryParameter.ContentType))
                {
                    parameters[S3QueryParameter.ContentType] = "application/x-www-form-urlencoded; charset=utf-8";
                }
            }
            parameters[S3QueryParameter.RequestTimeout] = request.Timeout.ToString();
            if (request.IsSetCannedACL())
            {
                SetCannedACLHeader(headers, request.CannedACL);
            }
            if (request.IsSetMetaData())
            {
                foreach (string str3 in request.metaData)
                {
                    headers["x-amz-meta-" + str3] = request.metaData[str3];
                }
            }
            headers["x-amz-storage-class"] = S3Constants.StorageClasses[(int) request.StorageClass];
            this.AddS3QueryParameters(request, request.BucketName);
        }

        private void ConvertSetACL(SetACLRequest request)
        {
            IDictionary<S3QueryParameter, string> parameters = request.parameters;
            WebHeaderCollection headers = request.Headers;
            if (request.IsSetACL())
            {
                parameters[S3QueryParameter.ContentBody] = request.ACL.ToString();
                parameters[S3QueryParameter.ContentType] = "application/x-www-form-urlencoded; charset=utf-8";
            }
            if (request.IsSetCannedACL())
            {
                SetCannedACLHeader(headers, request.CannedACL);
            }
            parameters[S3QueryParameter.Verb] = S3Constants.PutVerb;
            parameters[S3QueryParameter.Action] = "SetACL";
            string str = "?acl";
            if (request.IsSetKey())
            {
                parameters[S3QueryParameter.Key] = request.Key;
                if (request.IsSetVersionId())
                {
                    str = str + "&versionId=" + request.VersionId;
                }
            }
            parameters[S3QueryParameter.Query] = str;
            parameters[S3QueryParameter.QueryToSign] = str;
            this.AddS3QueryParameters(request, request.BucketName);
        }

        private void ConvertSetBucketVersioning(SetBucketVersioningRequest request)
        {
            IDictionary<S3QueryParameter, string> parameters = request.parameters;
            WebHeaderCollection headers = request.Headers;
            parameters[S3QueryParameter.Verb] = HttpVerb.PUT.ToString();
            parameters[S3QueryParameter.Action] = "SetBucketVersioning";
            parameters[S3QueryParameter.Query] = "?versioning";
            parameters[S3QueryParameter.QueryToSign] = "?versioning";
            parameters[S3QueryParameter.ContentBody] = request.VersioningConfig.ToString();
            parameters[S3QueryParameter.ContentType] = "application/x-www-form-urlencoded; charset=utf-8";
            if (request.VersioningConfig.IsSetEnableMfaDelete())
            {
                SetMfaHeader(headers, request.MfaCodes);
            }
            this.AddS3QueryParameters(request, request.BucketName);
        }

        public CopyObjectResponse CopyObject(CopyObjectRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The CopyObjectRequest specified is null!");
            }
            if (!request.IsSetDestinationBucket())
            {
                throw new ArgumentNullException("request", "The Destination S3Bucket must be specified!");
            }
            if (!request.IsSetSourceBucket())
            {
                throw new ArgumentNullException("request", "The Source S3Bucket must be specified!");
            }
            if (!request.IsSetSourceKey())
            {
                throw new ArgumentNullException("request", "The Source Key must be specified!");
            }
            this.ConvertCopyObject(request);
            return this.Invoke<CopyObjectResponse>(request);
        }

        public DeleteBucketResponse DeleteBucket(DeleteBucketRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The DeleteBucketRequest specified is null!");
            }
            if (!request.IsSetBucketName())
            {
                throw new ArgumentNullException("request", "The BucketName specified is null or empty!");
            }
            this.ConvertDeleteBucket(request);
            return this.Invoke<DeleteBucketResponse>(request);
        }

        public DeleteObjectResponse DeleteObject(DeleteObjectRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The DeleteObjectRequest is null!");
            }
            if (!request.IsSetBucketName())
            {
                throw new ArgumentNullException("request", "The S3 BucketName specified is null or empty!");
            }
            if (!request.IsSetKey())
            {
                throw new ArgumentNullException("request", "The S3 Key Specified is null or empty!");
            }
            Protocol communicationProtocol = this.config.CommunicationProtocol;
            if (request.IsSetMfaCodes())
            {
                this.config.CommunicationProtocol = Protocol.HTTPS;
            }
            this.ConvertDeleteObject(request);
            DeleteObjectResponse response = this.Invoke<DeleteObjectResponse>(request);
            this.config.CommunicationProtocol = communicationProtocol;
            return response;
        }

        public DisableBucketLoggingResponse DisableBucketLogging(DisableBucketLoggingRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The SetBucketLoggingRequest specified is null!");
            }
            if (!request.IsSetBucketName())
            {
                throw new ArgumentNullException("request", "The BucketName specified is null or empty!");
            }
            this.ConvertDisableBucketLogging(request);
            return this.Invoke<DisableBucketLoggingResponse>(request);
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

        public EnableBucketLoggingResponse EnableBucketLogging(EnableBucketLoggingRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The SetBucketLoggingRequest specified is null!");
            }
            if (!request.IsSetBucketName())
            {
                throw new ArgumentNullException("request", "The BucketName specified is null or empty!");
            }
            S3BucketLoggingConfig loggingConfig = request.LoggingConfig;
            if (loggingConfig == null)
            {
                throw new ArgumentNullException("request", "The LoggingConfig is null!");
            }
            if (!loggingConfig.IsSetGrants())
            {
                throw new ArgumentNullException("request", "The Grants of the LoggingConfig is null!");
            }
            if (!loggingConfig.IsSetTargetBucketName())
            {
                throw new ArgumentNullException("request", "The BucketName of the LoggingConfig is null or empty!");
            }
            if (!loggingConfig.IsSetTargetPrefix())
            {
                throw new ArgumentNullException("request", "The TargetPrefix of the LoggingConfig is null!");
            }
            this.ConvertEnableBucketLogging(request);
            return this.Invoke<EnableBucketLoggingResponse>(request);
        }

        ~AmazonS3Client()
        {
            this.Dispose(false);
        }

        public GetACLResponse GetACL(GetACLRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The GetACLRequest specified is null!");
            }
            if (!request.IsSetBucketName())
            {
                throw new ArgumentNullException("request", "The BucketName specified is null or empty!");
            }
            this.ConvertGetACL(request);
            return this.Invoke<GetACLResponse>(request);
        }

        public GetBucketLocationResponse GetBucketLocation(GetBucketLocationRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The GetBucketLocationRequest specified is null!");
            }
            if (!request.IsSetBucketName())
            {
                throw new ArgumentNullException("request", "The BucketName specified is null or empty!");
            }
            this.ConvertGetBucketLocation(request);
            return this.Invoke<GetBucketLocationResponse>(request);
        }

        public GetBucketLoggingResponse GetBucketLogging(GetBucketLoggingRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The GetBucketLoggingRequest specified is null!");
            }
            if (!request.IsSetBucketName())
            {
                throw new ArgumentNullException("request", "The BucketName specified is null or empty!");
            }
            this.ConvertGetBucketLogging(request);
            return this.Invoke<GetBucketLoggingResponse>(request);
        }

        public GetBucketVersioningResponse GetBucketVersioning(GetBucketVersioningRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The GetBucketVersioningRequest specified is null!");
            }
            if (!request.IsSetBucketName())
            {
                throw new ArgumentNullException("request", "The BucketName specified is null or empty!");
            }
            this.ConvertGetBucketVersioning(request);
            return this.Invoke<GetBucketVersioningResponse>(request);
        }

        public GetObjectResponse GetObject(GetObjectRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The GetObjectRequest specified is null!");
            }
            if (!request.IsSetBucketName())
            {
                throw new ArgumentNullException("request", "The BucketName specified is null or empty!");
            }
            if (!request.IsSetKey())
            {
                throw new ArgumentNullException("request", "The Key Specified is null or empty!");
            }
            this.ConvertGetObject(request);
            return this.Invoke<GetObjectResponse>(request);
        }

        public GetObjectMetadataResponse GetObjectMetadata(GetObjectMetadataRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The GetObjectMetadataRequest specified is null!");
            }
            if (!request.IsSetBucketName())
            {
                throw new ArgumentNullException("request", "The BucketName specified is null or empty!");
            }
            if (!request.IsSetKey())
            {
                throw new ArgumentNullException("request", "The Key Specified is null or empty!");
            }
            this.ConvertGetObjectMetadata(request);
            return this.Invoke<GetObjectMetadataResponse>(request);
        }

        public string GetPreSignedURL(GetPreSignedUrlRequest request)
        {
            if (string.IsNullOrEmpty(this.awsAccessKeyId))
            {
                throw new AmazonS3Exception("The AWS Access Key ID cannot be NULL or a Zero length string");
            }
            if (request == null)
            {
                throw new ArgumentNullException("request", "The PreSignedUrlRequest specified is null!");
            }
            if (!request.IsSetBucketName())
            {
                throw new ArgumentNullException("request", "The BucketName Specified is null or empty!");
            }
            if (!request.IsSetExpires())
            {
                throw new ArgumentNullException("request", "The Expires Specified is null!");
            }
            if (request.Verb > HttpVerb.PUT)
            {
                throw new ArgumentException("An Invalid HttpVerb was specified for the GetPreSignedURL request. Valid - GET, HEAD, PUT", "request");
            }
            this.ConvertGetPreSignedUrl(request);
            return request.parameters[S3QueryParameter.Url];
        }

        private T Invoke<T>(S3Request userRequest) where T: S3Response, new()
        {
            if (userRequest == null)
            {
                throw new AmazonS3Exception("No request specified for the S3 operation!");
            }
            WebHeaderCollection collection1 = userRequest.Headers;
            IDictionary<S3QueryParameter, string> parameters = userRequest.parameters;
            Stream inputStream = userRequest.InputStream;
            string str = parameters[S3QueryParameter.Action];
            T local = default(T);
            HttpStatusCode status = (HttpStatusCode) 0;
            string str2 = parameters[S3QueryParameter.Verb];
            DateTime utcNow = DateTime.UtcNow;
            Trace.Write(string.Format("{0}, {1}, ", str, utcNow));
            byte[] bytes = Encoding.UTF8.GetBytes("");
            long contentLength = 0L;
            if (string.IsNullOrEmpty(this.awsAccessKeyId))
            {
                throw new AmazonS3Exception("The AWS Access Key ID cannot be NULL or a Zero length string");
            }
            if ((!str2.Equals(S3Constants.PutVerb) && !str2.Equals(S3Constants.GetVerb)) && (!str2.Equals(S3Constants.DeleteVerb) && !str2.Equals(S3Constants.HeadVerb)))
            {
                throw new AmazonS3Exception("Invalid HTTP Operation attempted! Supported operations - GET, HEAD, PUT, DELETE");
            }
            if (str2.Equals(S3Constants.PutVerb))
            {
                if (parameters.ContainsKey(S3QueryParameter.ContentBody))
                {
                    string s = parameters[S3QueryParameter.ContentBody];
                    bytes = Encoding.UTF8.GetBytes(s);
                    contentLength = bytes.Length;
                    parameters[S3QueryParameter.ContentLength] = contentLength.ToString();
                }
                if (parameters.ContainsKey(S3QueryParameter.ContentLength))
                {
                    contentLength = long.Parse(parameters[S3QueryParameter.ContentLength]);
                    if (contentLength > S3Constants.MaxS3ObjectSize)
                    {
                        throw new AmazonS3Exception("Your proposed upload exceeds the maximum allowed object size", HttpStatusCode.BadRequest, "EntityTooLarge", "", "", "", this.config.ServiceURL, null);
                    }
                }
            }
            int num2 = 0;
            int maxRetries = this.config.IsSetMaxErrorRetry() ? this.config.MaxErrorRetry : 3;
            try
            {
                bool flag;
                HttpWebResponse httpResponse = null;
                do
                {
                    flag = false;
                    HttpWebRequest request = this.ConfigureWebRequest(userRequest, contentLength);
                    string requestAddr = request.Address.ToString();
                    parameters[S3QueryParameter.RequestAddress] = requestAddr;
                    try
                    {
                        if (contentLength > 0L)
                        {
                            using (Stream stream2 = request.GetRequestStream())
                            {
                                if (inputStream != null)
                                {
                                    this.WriteStreamToService(userRequest, contentLength, inputStream, stream2);
                                }
                                else
                                {
                                    using (MemoryStream stream3 = new MemoryStream(bytes))
                                    {
                                        this.WriteStreamToService(userRequest, contentLength, stream3, stream2);
                                    }
                                }
                            }
                        }
                        DateTime time2 = DateTime.UtcNow;
                        TimeSpan span = (TimeSpan) (time2 - utcNow);
                        Trace.Write(string.Format("{0}, {1}, ", time2, span.TotalMilliseconds));
                        httpResponse = request.GetResponse() as HttpWebResponse;
                        DateTime time3 = DateTime.UtcNow;
                        Trace.Write(string.Format("{0}, ", time3));
                        TimeSpan span2 = (TimeSpan) (time3 - time2);
                        Trace.Write(string.Format("{0}, ", span2.TotalMilliseconds));
                        if (httpResponse != null)
                        {
                            status = httpResponse.StatusCode;
                            if (!IsRedirect(httpResponse))
                            {
                                local = ProcessRequestResponse<T>(httpResponse, parameters, this.myType);
                            }
                            else
                            {
                                flag = true;
                                ProcessRedirect(userRequest, httpResponse);
                                PauseOnRetry(++num2, maxRetries, status, requestAddr, httpResponse.Headers);
                                httpResponse.Close();
                                httpResponse = null;
                                request.Abort();
                            }
                            DateTime time4 = DateTime.UtcNow;
                            Trace.Write(string.Format("{0}, ", time4));
                            TimeSpan span3 = (TimeSpan) (time4 - time3);
                            Trace.Write(string.Format("{0}", span3.TotalMilliseconds));
                            TimeSpan span4 = (TimeSpan) (time4 - utcNow);
                            Trace.WriteLine(string.Format("{0}", span4.TotalMilliseconds));
                            Trace.Flush();
                        }
                    }
                    catch (WebException exception)
                    {
                        using (HttpWebResponse response2 = exception.Response as HttpWebResponse)
                        {
                            WebHeaderCollection headers;
                            flag = ProcessRequestError(request, exception, response2, requestAddr, out headers, this.myType);
                            if (httpResponse != null)
                            {
                                httpResponse.Close();
                                httpResponse = null;
                            }
                            request.Abort();
                            if (response2 != null)
                            {
                                status = response2.StatusCode;
                            }
                            else
                            {
                                status = HttpStatusCode.BadRequest;
                            }
                            if (flag)
                            {
                                PauseOnRetry(++num2, maxRetries, status, requestAddr, headers);
                            }
                        }
                    }
                    catch (IOException)
                    {
                        if (httpResponse != null)
                        {
                            httpResponse.Close();
                            httpResponse = null;
                        }
                        request.Abort();
                        throw;
                    }
                    if (flag)
                    {
                        this.PrepareRequestForRetry(userRequest);
                    }
                }
                while (flag && (num2 <= maxRetries));
            }
            finally
            {
                if (inputStream != null)
                {
                    inputStream.Close();
                }
            }
            return local;
        }

        private static bool IsRedirect(HttpWebResponse httpResponse)
        {
            if (httpResponse == null)
            {
                throw new ArgumentNullException("httpResponse", "Input parameter is null");
            }
            HttpStatusCode statusCode = httpResponse.StatusCode;
            return ((statusCode >= HttpStatusCode.MovedPermanently) && (statusCode < HttpStatusCode.BadRequest));
        }

        public ListBucketsResponse ListBuckets()
        {
            return this.ListBuckets(new ListBucketsRequest());
        }

        public ListBucketsResponse ListBuckets(ListBucketsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The ListObjectsRequest is null!");
            }
            this.ConvertListBuckets(request);
            return this.Invoke<ListBucketsResponse>(request);
        }

        public ListObjectsResponse ListObjects(ListObjectsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The ListObjectsRequest specified is null!");
            }
            if (!request.IsSetBucketName())
            {
                throw new ArgumentNullException("request", "The BucketName specified is null or empty!");
            }
            this.ConvertListObjects(request);
            return this.Invoke<ListObjectsResponse>(request);
        }

        public ListVersionsResponse ListVersions(ListVersionsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The ListVersionsRequest specified is null!");
            }
            if (!request.IsSetBucketName())
            {
                throw new ArgumentNullException("request", "The BucketName specified is null or empty!");
            }
            this.ConvertListVersions(request);
            return this.Invoke<ListVersionsResponse>(request);
        }

        private static void PauseOnRetry(int retries, int maxRetries, HttpStatusCode status, string requestAddr, WebHeaderCollection headers)
        {
            if (retries > maxRetries)
            {
                throw new AmazonS3Exception("Maximum number of retry attempts reached : " + (retries - 1), status, "", "", "", "", requestAddr, headers);
            }
            int millisecondsTimeout = ((int) Math.Pow(4.0, (double) retries)) * 100;
            Thread.Sleep(millisecondsTimeout);
        }

        private void PrepareRequestForRetry(S3Request request)
        {
            if (request.InputStream != null)
            {
                request.InputStream.Position = 0L;
            }
            if (request.removedHeaders.Count > 0)
            {
                request.Headers.Add(request.removedHeaders);
            }
        }

        private static void ProcessRedirect(S3Request userRequest, HttpWebResponse httpResponse)
        {
            string str;
            if (httpResponse == null)
            {
                throw new WebException("The Web Response for a redirected request is null!", WebExceptionStatus.ProtocolError);
            }
            if (!string.IsNullOrEmpty(str = httpResponse.Headers["Location"]))
            {
                userRequest.parameters[S3QueryParameter.Url] = str;
            }
        }

        private static bool ProcessRequestError(HttpWebRequest request, WebException we, HttpWebResponse errorResponse, string requestAddr, out WebHeaderCollection respHdrs, Type t)
        {
            HttpStatusCode statusCode = (HttpStatusCode) 0;
            string responseBody = null;
            respHdrs = null;
            if (errorResponse == null)
            {
                throw we;
            }
            respHdrs = errorResponse.Headers;
            statusCode = errorResponse.StatusCode;
            using (StreamReader reader = new StreamReader(errorResponse.GetResponseStream(), Encoding.UTF8))
            {
                responseBody = reader.ReadToEnd();
            }
            if (request.Method.Equals("HEAD") && (statusCode == HttpStatusCode.NotFound))
            {
                throw new AmazonS3Exception(we.Message, statusCode, "NoSuchKey", respHdrs["x-amz-request-id"], "", "", requestAddr, respHdrs);
            }
            if ((statusCode == HttpStatusCode.InternalServerError) || (statusCode == HttpStatusCode.ServiceUnavailable))
            {
                return true;
            }
            using (XmlTextReader reader2 = new XmlTextReader(new StringReader(Transform(responseBody, "S3Error", t))))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(S3Error));
                S3Error error = (S3Error) serializer.Deserialize(reader2);
                throw new AmazonS3Exception(error.Message, statusCode, error.Code, error.RequestId, error.HostId, responseBody, requestAddr, respHdrs);
            }
        }

        private static T ProcessRequestResponse<T>(HttpWebResponse httpResponse, IDictionary<S3QueryParameter, string> parameters, Type t) where T: S3Response, new()
        {
            T local = default(T);
            string actionName = parameters[S3QueryParameter.Action];
            if (httpResponse == null)
            {
                throw new WebException("The Web Response for a successful request is null!", WebExceptionStatus.ProtocolError);
            }
            WebHeaderCollection headers = httpResponse.Headers;
            HttpStatusCode statusCode = httpResponse.StatusCode;
            string responseBody = null;
            try
            {
                if (actionName.Equals("GetObject"))
                {
                    local = Activator.CreateInstance<T>();
                    Stream responseStream = httpResponse.GetResponseStream();
                    if (parameters.ContainsKey(S3QueryParameter.VerifyChecksum))
                    {
                        try
                        {
                            string y = headers["ETag"];
                            y = y.Replace("\"", string.Empty);
                            if (responseStream.CanSeek)
                            {
                                local.ResponseStream = responseStream;
                            }
                            else
                            {
                                local.ResponseStream = AmazonS3Util.MakeStreamSeekable(responseStream);
                            }
                            string x = AmazonS3Util.GenerateChecksumForStream(local.ResponseStream, false);
                            if (StringComparer.OrdinalIgnoreCase.Compare(x, y) != 0)
                            {
                                throw new AmazonS3Exception("The calculated md5Digest '" + x + "' differs from the md5Digest returned by S3 '" + y, HttpStatusCode.BadRequest, "BadDigest", "", "", "", parameters[S3QueryParameter.RequestAddress], httpResponse.Headers);
                            }
                            return local;
                        }
                        catch (Exception)
                        {
                            local = default(T);
                            throw;
                        }
                    }
                    local.ResponseStream = responseStream;
                    return local;
                }
                using (httpResponse)
                {
                    using (StreamReader reader = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8))
                    {
                        responseBody = reader.ReadToEnd().Trim();
                    }
                    DateTime utcNow = DateTime.UtcNow;
                    if (responseBody.EndsWith(">"))
                    {
                        string s = Transform(responseBody, actionName, t);
                        DateTime time2 = DateTime.UtcNow;
                        XmlSerializer serializer = new XmlSerializer(typeof(T));
                        using (XmlTextReader reader2 = new XmlTextReader(new StringReader(s)))
                        {
                            local = (T) serializer.Deserialize(reader2);
                        }
                        DateTime time3 = DateTime.UtcNow;
                        TimeSpan span = (TimeSpan) (time2 - utcNow);
                        TimeSpan span2 = (TimeSpan) (time3 - time2);
                        Trace.Write(string.Format("{0}, {1}, ", span.TotalMilliseconds, span2.TotalMilliseconds));
                    }
                    else
                    {
                        local = Activator.CreateInstance<T>();
                    }
                }
                httpResponse = null;
            }
            finally
            {
                if (actionName.Equals("GetObject") && (local != null))
                {
                    local.httpResponse = httpResponse;
                }
                else if (httpResponse != null)
                {
                    httpResponse.Close();
                    httpResponse = null;
                }
                if (local != null)
                {
                    local.Headers = headers;
                    local.ResponseXml = responseBody;
                }
            }
            return local;
        }

        public PutBucketResponse PutBucket(PutBucketRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The PutBucketRequest specified is null!");
            }
            if (!request.IsSetBucketName())
            {
                throw new ArgumentNullException("request", "The BucketName specified is null or empty!");
            }
            this.ConvertPutBucket(request);
            return this.Invoke<PutBucketResponse>(request);
        }

        public PutObjectResponse PutObject(PutObjectRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The PutObjectRequest specified is null!");
            }
            if (!request.IsSetBucketName())
            {
                throw new ArgumentNullException("request", "An S3 Bucket must be specified for S3 PUT object.");
            }
            if (!request.IsSetKey() && !request.IsSetFilePath())
            {
                throw new ArgumentException("Either a Key or a Filename need to be specified for S3 PUT object.", "request");
            }
            if ((!request.IsSetFilePath() && !request.IsSetInputStream()) && !request.IsSetContentBody())
            {
                throw new ArgumentException("Please specify either a Filename, provide a FileStream or provide a ContentBody to PUT an object into S3.", "request");
            }
            if (request.IsSetInputStream() && request.IsSetContentBody())
            {
                throw new ArgumentException("Please specify one of either an Input FileStream or the ContentBody to be PUT as an S3 object.", "request");
            }
            if (request.IsSetInputStream() && request.IsSetFilePath())
            {
                throw new ArgumentException("Please specify one of either an Input FileStream or a Filename to be PUT as an S3 object.", "request");
            }
            if (request.IsSetFilePath() && request.IsSetContentBody())
            {
                throw new ArgumentException("Please specify one of either a Filename or the ContentBody to be PUT as an S3 object.", "request");
            }
            if (request.IsSetFilePath())
            {
                if (!System.IO.File.Exists(request.FilePath))
                {
                    throw new FileNotFoundException("The specified file does not exist");
                }
                request.InputStream = new FileStream(request.FilePath, FileMode.Open, FileAccess.Read);
                if (!request.IsSetKey())
                {
                    string filePath = request.FilePath;
                    request.Key = filePath.Substring(filePath.LastIndexOf(@"\") + 1);
                }
            }
            this.ConvertPutObject(request);
            return this.Invoke<PutObjectResponse>(request);
        }

        public SetACLResponse SetACL(SetACLRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The SetACLRequest specified is null!");
            }
            if (!request.IsSetBucketName())
            {
                throw new ArgumentNullException("request", "The BucketName specified is null or empty!");
            }
            if (!request.IsSetACL() && !request.IsSetCannedACL())
            {
                throw new ArgumentNullException("request", "No ACL or CannedACL specified!");
            }
            this.ConvertSetACL(request);
            return this.Invoke<SetACLResponse>(request);
        }

        public SetBucketVersioningResponse SetBucketVersioning(SetBucketVersioningRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The SetBucketVersioningRequest specified is null!");
            }
            if (!request.IsSetBucketName())
            {
                throw new ArgumentNullException("request", "The BucketName specified is null or empty!");
            }
            if (!request.IsSetVersioningConfig())
            {
                throw new ArgumentException("Invalid Versioning Config specified", "request");
            }
            Protocol communicationProtocol = this.config.CommunicationProtocol;
            if (request.VersioningConfig.IsSetEnableMfaDelete())
            {
                if (!request.IsSetMfaCodes())
                {
                    throw new ArgumentNullException("request", "MfaDelete has been enabled, but the Mfa Codes haven't been supplied in the request.");
                }
                this.config.CommunicationProtocol = Protocol.HTTPS;
            }
            this.ConvertSetBucketVersioning(request);
            SetBucketVersioningResponse response = this.Invoke<SetBucketVersioningResponse>(request);
            this.config.CommunicationProtocol = communicationProtocol;
            return response;
        }

        private static void SetCannedACLHeader(WebHeaderCollection headers, S3CannedACL acl)
        {
            headers["x-amz-acl"] = S3Constants.CannedAcls[(int) acl];
        }

        private static void SetCopySourceHeader(WebHeaderCollection headers, string bucket, string key)
        {
            string data = bucket;
            if (key != null)
            {
                data = "/" + bucket + "/" + key;
            }
            headers["x-amz-copy-source"] = AmazonS3Util.UrlEncode(data, true);
        }

        private static void SetIfMatchCopyHeader(WebHeaderCollection headers, string eTag)
        {
            headers["x-amz-copy-source-if-match"] = eTag;
        }

        private static void SetIfMatchHeader(WebHeaderCollection headers, string eTag)
        {
            headers["If-Match"] = eTag;
        }

        private static void SetIfModifiedSinceCopyHeader(WebHeaderCollection headers, DateTime date)
        {
            headers["x-amz-copy-source-if-modified-since"] = date.ToUniversalTime().ToString(@"ddd, dd MMM yyyy HH:mm:ss \G\M\T");
        }

        private static void SetIfModifiedSinceHeader(WebHeaderCollection headers, DateTime date)
        {
            headers["IfModifiedSince"] = date.ToUniversalTime().ToString(@"ddd, dd MMM yyyy HH:mm:ss \G\M\T");
        }

        private static void SetIfNoneMatchCopyHeader(WebHeaderCollection headers, string eTag)
        {
            headers["x-amz-copy-source-if-none-match"] = eTag;
        }

        private static void SetIfNoneMatchHeader(WebHeaderCollection headers, string eTag)
        {
            headers["If-None-Match"] = eTag;
        }

        private static void SetIfUnmodifiedSinceCopyHeader(WebHeaderCollection headers, DateTime date)
        {
            headers["x-amz-copy-source-if-unmodified-since"] = date.ToUniversalTime().ToString(@"ddd, dd MMM yyyy HH:mm:ss \G\M\T");
        }

        private static void SetIfUnmodifiedSinceHeader(WebHeaderCollection headers, DateTime date)
        {
            headers["If-Unmodified-Since"] = date.ToUniversalTime().ToString(@"ddd, dd MMM yyyy HH:mm:ss \G\M\T");
        }

        private static void SetMetadataDirectiveHeader(WebHeaderCollection headers, S3MetadataDirective directive)
        {
            headers["x-amz-metadata-directive"] = S3Constants.MetaDataDirectives[(int) directive];
        }

        private static void SetMfaHeader(WebHeaderCollection headers, Tuple<string, string> mfaCodes)
        {
            headers["x-amz-mfa"] = mfaCodes.First + " " + mfaCodes.Second;
        }

        private static void SetVersionIdHeader(WebHeaderCollection headers, string versionId)
        {
            headers["x-amz-version-id"] = versionId;
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

        private void WriteStreamToService(S3Request request, long reqDataLen, Stream inputStream, Stream requestStream)
        {
            PutObjectRequest request2 = request as PutObjectRequest;
            if (inputStream != null)
            {
                long transferred = 0L;
                inputStream.Position = 0L;
                byte[] buffer = new byte[0x10000];
                int count = 0;
                while ((count = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    transferred += count;
                    requestStream.Write(buffer, 0, count);
                    if (request2 != null)
                    {
                        request2.OnRaiseProgressEvent(new PutObjectProgressArgs(transferred, reqDataLen));
                    }
                }
            }
        }
    }
}

