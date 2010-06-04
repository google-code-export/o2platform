namespace Amazon.SimpleNotificationService
{
    using Amazon.SimpleNotificationService.Model;
    using Amazon.Util;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Reflection;
    using System.Security;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Xml.Xsl;

    public class AmazonSimpleNotificationServiceClient : AmazonSimpleNotificationService, IDisposable
    {
        private string awsAccessKeyId;
        private SecureString awsSecretAccessKey;
        private string clearAwsSecretAccessKey;
        private AmazonSimpleNotificationServiceConfig config;
        private bool disposed;

        public AmazonSimpleNotificationServiceClient(string awsAccessKeyId, string awsSecretAccessKey) : this(awsAccessKeyId, awsSecretAccessKey, new AmazonSimpleNotificationServiceConfig())
        {
        }

        public AmazonSimpleNotificationServiceClient(string awsAccessKeyId, SecureString awsSecretAccessKey, AmazonSimpleNotificationServiceConfig config)
        {
            this.awsAccessKeyId = awsAccessKeyId;
            this.awsSecretAccessKey = awsSecretAccessKey;
            this.config = config;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.UseNagleAlgorithm = false;
        }

        public AmazonSimpleNotificationServiceClient(string awsAccessKeyId, string awsSecretAccessKey, AmazonSimpleNotificationServiceConfig config)
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
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.UseNagleAlgorithm = false;
        }

        public AddPermissionResponse AddPermission(AddPermissionRequest request)
        {
            return this.Invoke<AddPermissionResponse>(ConvertAddPermission(request));
        }

        private void AddRequiredParameters(IDictionary<string, string> parameters)
        {
            string str2;
            if (string.IsNullOrEmpty(this.awsAccessKeyId))
            {
                throw new AmazonSimpleNotificationServiceException("The AWS Access Key ID cannot be NULL or a Zero length string");
            }
            parameters["AWSAccessKeyId"] = this.awsAccessKeyId;
            parameters["SignatureVersion"] = this.config.SignatureVersion;
            parameters["SignatureMethod"] = this.config.SignatureMethod;
            parameters["Timestamp"] = AWSSDKUtils.FormattedCurrentTimestampISO8601;
            parameters["Version"] = this.config.ServiceVersion;
            if (!this.config.SignatureVersion.Equals("2"))
            {
                throw new AmazonSimpleNotificationServiceException("Invalid Signature Version specified");
            }
            string data = AWSSDKUtils.CalculateStringToSignV2(parameters, this.config.ServiceURL);
            KeyedHashAlgorithm algorithm = KeyedHashAlgorithm.Create(this.config.SignatureMethod.ToUpper());
            if (this.config.UseSecureStringForAwsSecretKey)
            {
                str2 = AWSSDKUtils.HMACSign(data, this.awsSecretAccessKey, algorithm);
            }
            else
            {
                str2 = AWSSDKUtils.HMACSign(data, this.clearAwsSecretAccessKey, algorithm);
            }
            parameters["Signature"] = str2;
        }

        private static HttpWebRequest ConfigureWebRequest(int contentLength, AmazonSimpleNotificationServiceConfig config)
        {
            HttpWebRequest request = WebRequest.Create(config.ServiceURL) as HttpWebRequest;
            if (request != null)
            {
                if (config.IsSetProxyHost() && config.IsSetProxyPort())
                {
                    WebProxy proxy = new WebProxy(config.ProxyHost, config.ProxyPort);
                    if (config.IsSetProxyUsername())
                    {
                        proxy.Credentials = new NetworkCredential(config.ProxyUsername, config.ProxyPassword ?? string.Empty);
                    }
                    request.Proxy = proxy;
                }
                request.UserAgent = config.UserAgent;
                request.Method = "POST";
                request.Timeout = 0xc350;
                request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
                request.ContentLength = contentLength;
            }
            return request;
        }

        public ConfirmSubscriptionResponse ConfirmSubscription(ConfirmSubscriptionRequest request)
        {
            return this.Invoke<ConfirmSubscriptionResponse>(ConvertConfirmSubscription(request));
        }

        private static IDictionary<string, string> ConvertAddPermission(AddPermissionRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "AddPermission";
            if (request.IsSetTopicArn())
            {
                dictionary["TopicArn"] = request.TopicArn;
            }
            if (request.IsSetLabel())
            {
                dictionary["Label"] = request.Label;
            }
            List<string> aWSAccountIds = request.AWSAccountIds;
            int num = 1;
            foreach (string str in aWSAccountIds)
            {
                dictionary["AWSAccountIds" + ".member." + num] = str;
                num++;
            }
            List<string> actionNames = request.ActionNames;
            int num2 = 1;
            foreach (string str2 in actionNames)
            {
                dictionary["ActionNames" + ".member." + num2] = str2;
                num2++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertConfirmSubscription(ConfirmSubscriptionRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "ConfirmSubscription";
            if (request.IsSetTopicArn())
            {
                dictionary["TopicArn"] = request.TopicArn;
            }
            if (request.IsSetToken())
            {
                dictionary["Token"] = request.Token;
            }
            if (request.IsSetAuthenticateOnUnsubscribe())
            {
                dictionary["AuthenticateOnUnsubscribe"] = request.AuthenticateOnUnsubscribe;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertCreateTopic(CreateTopicRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "CreateTopic";
            if (request.IsSetName())
            {
                dictionary["Name"] = request.Name;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDeleteTopic(DeleteTopicRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DeleteTopic";
            if (request.IsSetTopicArn())
            {
                dictionary["TopicArn"] = request.TopicArn;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertGetTopicAttributes(GetTopicAttributesRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "GetTopicAttributes";
            if (request.IsSetTopicArn())
            {
                dictionary["TopicArn"] = request.TopicArn;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertListSubscriptions(ListSubscriptionsRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "ListSubscriptions";
            if (request.IsSetNextToken())
            {
                dictionary["NextToken"] = request.NextToken;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertListSubscriptionsByTopic(ListSubscriptionsByTopicRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "ListSubscriptionsByTopic";
            if (request.IsSetTopicArn())
            {
                dictionary["TopicArn"] = request.TopicArn;
            }
            if (request.IsSetNextToken())
            {
                dictionary["NextToken"] = request.NextToken;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertListTopics(ListTopicsRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "ListTopics";
            if (request.IsSetNextToken())
            {
                dictionary["NextToken"] = request.NextToken;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertPublish(PublishRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "Publish";
            if (request.IsSetTopicArn())
            {
                dictionary["TopicArn"] = request.TopicArn;
            }
            if (request.IsSetMessage())
            {
                dictionary["Message"] = request.Message;
            }
            if (request.IsSetSubject())
            {
                dictionary["Subject"] = request.Subject;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertRemovePermission(RemovePermissionRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "RemovePermission";
            if (request.IsSetTopicArn())
            {
                dictionary["TopicArn"] = request.TopicArn;
            }
            if (request.IsSetLabel())
            {
                dictionary["Label"] = request.Label;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertSetTopicAttributes(SetTopicAttributesRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "SetTopicAttributes";
            if (request.IsSetTopicArn())
            {
                dictionary["TopicArn"] = request.TopicArn;
            }
            if (request.IsSetAttributeName())
            {
                dictionary["AttributeName"] = request.AttributeName;
            }
            if (request.IsSetAttributeValue())
            {
                dictionary["AttributeValue"] = request.AttributeValue;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertSubscribe(SubscribeRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "Subscribe";
            if (request.IsSetTopicArn())
            {
                dictionary["TopicArn"] = request.TopicArn;
            }
            if (request.IsSetProtocol())
            {
                dictionary["Protocol"] = request.Protocol;
            }
            if (request.IsSetEndpoint())
            {
                dictionary["Endpoint"] = request.Endpoint;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertUnsubscribe(UnsubscribeRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "Unsubscribe";
            if (request.IsSetSubscriptionArn())
            {
                dictionary["SubscriptionArn"] = request.SubscriptionArn;
            }
            return dictionary;
        }

        public CreateTopicResponse CreateTopic(CreateTopicRequest request)
        {
            return this.Invoke<CreateTopicResponse>(ConvertCreateTopic(request));
        }

        public DeleteTopicResponse DeleteTopic(DeleteTopicRequest request)
        {
            return this.Invoke<DeleteTopicResponse>(ConvertDeleteTopic(request));
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

        ~AmazonSimpleNotificationServiceClient()
        {
            this.Dispose(false);
        }

        public GetTopicAttributesResponse GetTopicAttributes(GetTopicAttributesRequest request)
        {
            return this.Invoke<GetTopicAttributesResponse>(ConvertGetTopicAttributes(request));
        }

        private T Invoke<T>(IDictionary<string, string> parameters)
        {
            string str = parameters["Action"];
            T local = default(T);
            HttpStatusCode status = (HttpStatusCode) 0;
            this.AddRequiredParameters(parameters);
            string parametersAsString = AWSSDKUtils.GetParametersAsString(parameters);
            byte[] bytes = Encoding.UTF8.GetBytes(parametersAsString);
            bool flag = true;
            int num = 0;
            int maxRetries = this.config.IsSetMaxErrorRetry() ? this.config.MaxErrorRetry : 3;
            do
            {
                string responseBody = null;
                HttpWebRequest request = ConfigureWebRequest(bytes.Length, this.config);
                try
                {
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(bytes, 0, bytes.Length);
                    }
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        if (response == null)
                        {
                            throw new WebException("The Web Response for a successful request is null!", WebExceptionStatus.ProtocolError);
                        }
                        status = response.StatusCode;
                        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            responseBody = reader.ReadToEnd();
                        }
                    }
                    if (responseBody.Trim().EndsWith(str + "Response>"))
                    {
                        responseBody = Transform(responseBody, base.GetType());
                    }
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    using (XmlTextReader reader2 = new XmlTextReader(new StringReader(responseBody)))
                    {
                        local = (T) serializer.Deserialize(reader2);
                    }
                    flag = false;
                }
                catch (WebException exception)
                {
                    flag = false;
                    using (HttpWebResponse response2 = exception.Response as HttpWebResponse)
                    {
                        if (response2 == null)
                        {
                            request.Abort();
                            throw exception;
                        }
                        status = response2.StatusCode;
                        using (StreamReader reader3 = new StreamReader(response2.GetResponseStream(), Encoding.UTF8))
                        {
                            responseBody = reader3.ReadToEnd();
                        }
                        request.Abort();
                    }
                    if ((status == HttpStatusCode.InternalServerError) || (status == HttpStatusCode.ServiceUnavailable))
                    {
                        flag = true;
                        PauseOnRetry(++num, maxRetries, status);
                    }
                    else
                    {
                        try
                        {
                            using (XmlTextReader reader4 = new XmlTextReader(new StringReader(responseBody)))
                            {
                                XmlSerializer serializer2 = new XmlSerializer(typeof(ErrorResponse));
                                ErrorResponse response3 = (ErrorResponse) serializer2.Deserialize(reader4);
                                Error error = response3.Error[0];
                                throw new AmazonSimpleNotificationServiceException(error.Message, status, error.Code, error.Type, response3.RequestId, response3.ToXML());
                            }
                        }
                        catch (Exception exception2)
                        {
                            if (exception2 is AmazonSimpleNotificationServiceException)
                            {
                                throw;
                            }
                            throw ReportAnyErrors(responseBody, status);
                        }
                    }
                }
                catch (Exception)
                {
                    request.Abort();
                    throw;
                }
            }
            while (flag);
            return local;
        }

        public ListSubscriptionsResponse ListSubscriptions(ListSubscriptionsRequest request)
        {
            return this.Invoke<ListSubscriptionsResponse>(ConvertListSubscriptions(request));
        }

        public ListSubscriptionsByTopicResponse ListSubscriptionsByTopic(ListSubscriptionsByTopicRequest request)
        {
            return this.Invoke<ListSubscriptionsByTopicResponse>(ConvertListSubscriptionsByTopic(request));
        }

        public ListTopicsResponse ListTopics(ListTopicsRequest request)
        {
            return this.Invoke<ListTopicsResponse>(ConvertListTopics(request));
        }

        private static void PauseOnRetry(int retries, int maxRetries, HttpStatusCode status)
        {
            if (retries > maxRetries)
            {
                throw new AmazonSimpleNotificationServiceException("Maximum number of retry attempts reached : " + (retries - 1), status);
            }
            int millisecondsTimeout = ((int) Math.Pow(4.0, (double) retries)) * 100;
            Thread.Sleep(millisecondsTimeout);
        }

        public PublishResponse Publish(PublishRequest request)
        {
            return this.Invoke<PublishResponse>(ConvertPublish(request));
        }

        public RemovePermissionResponse RemovePermission(RemovePermissionRequest request)
        {
            return this.Invoke<RemovePermissionResponse>(ConvertRemovePermission(request));
        }

        private static AmazonSimpleNotificationServiceException ReportAnyErrors(string responseBody, HttpStatusCode status)
        {
            if ((responseBody != null) && responseBody.StartsWith("<"))
            {
                Match match = Regex.Match(responseBody, "<RequestId>(.*)</RequestId>.*<Error><Code>(.*)</Code><Message>(.*)</Message></Error>.*(<Error>)?", RegexOptions.Multiline);
                Match match2 = Regex.Match(responseBody, "<Error><Code>(.*)</Code><Message>(.*)</Message></Error>.*(<Error>)?.*<RequestID>(.*)</RequestID>", RegexOptions.Multiline);
                if (match.Success)
                {
                    string requestId = match.Groups[1].Value;
                    return new AmazonSimpleNotificationServiceException(match.Groups[3].Value, status, match.Groups[2].Value, "Unknown", requestId, responseBody);
                }
                if (match2.Success)
                {
                    string errorCode = match2.Groups[1].Value;
                    string message = match2.Groups[2].Value;
                    return new AmazonSimpleNotificationServiceException(message, status, errorCode, "Unknown", match2.Groups[4].Value, responseBody);
                }
                return new AmazonSimpleNotificationServiceException("Internal Error", status);
            }
            return new AmazonSimpleNotificationServiceException("Internal Error", status);
        }

        public SetTopicAttributesResponse SetTopicAttributes(SetTopicAttributesRequest request)
        {
            return this.Invoke<SetTopicAttributesResponse>(ConvertSetTopicAttributes(request));
        }

        public SubscribeResponse Subscribe(SubscribeRequest request)
        {
            return this.Invoke<SubscribeResponse>(ConvertSubscribe(request));
        }

        private static string Transform(string responseBody, Type t)
        {
            string str4;
            XslCompiledTransform transform = new XslCompiledTransform();
            char[] separator = new char[] { ',' };
            Assembly assembly = Assembly.GetAssembly(t);
            string str = assembly.FullName.Split(separator)[0];
            string str2 = t.Namespace;
            string name = str + "." + str2 + ".Model." + "ResponseTransformer.xslt";
            using (XmlTextReader reader = new XmlTextReader(assembly.GetManifestResourceStream(name)))
            {
                transform.Load(reader);
                StringBuilder sb = new StringBuilder(0x400);
                using (XmlTextReader reader2 = new XmlTextReader(new StringReader(responseBody)))
                {
                    using (StringWriter writer = new StringWriter(sb))
                    {
                        transform.Transform((XmlReader) reader2, null, (TextWriter) writer);
                        str4 = sb.ToString();
                    }
                }
            }
            return str4;
        }

        public UnsubscribeResponse Unsubscribe(UnsubscribeRequest request)
        {
            return this.Invoke<UnsubscribeResponse>(ConvertUnsubscribe(request));
        }
    }
}

