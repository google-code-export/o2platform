namespace Amazon.SQS
{
    using Amazon.SQS.Model;
    using Amazon.Util;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Security;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Xml;
    using System.Xml.Serialization;

    public class AmazonSQSClient : AmazonSQS, IDisposable
    {
        private string awsAccessKeyId;
        private SecureString awsSecretAccessKey;
        private string clearAwsSecretAccessKey;
        private AmazonSQSConfig config;
        private bool disposed;

        public AmazonSQSClient(string awsAccessKeyId, string awsSecretAccessKey) : this(awsAccessKeyId, awsSecretAccessKey, new AmazonSQSConfig())
        {
        }

        public AmazonSQSClient(string awsAccessKeyId, SecureString awsSecretAccessKey, AmazonSQSConfig config)
        {
            this.awsAccessKeyId = awsAccessKeyId;
            this.awsSecretAccessKey = awsSecretAccessKey;
            this.config = config;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.UseNagleAlgorithm = false;
        }

        public AmazonSQSClient(string awsAccessKeyId, string awsSecretAccessKey, AmazonSQSConfig config)
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

        private void AddRequiredParameters(IDictionary<string, string> parameters, string queueUrl)
        {
            string str2;
            if (string.IsNullOrEmpty(this.awsAccessKeyId))
            {
                throw new AmazonSQSException("The AWS Access Key ID cannot be NULL or a Zero length string");
            }
            parameters["AWSAccessKeyId"] = this.awsAccessKeyId;
            parameters["SignatureVersion"] = this.config.SignatureVersion;
            parameters["SignatureMethod"] = this.config.SignatureMethod;
            parameters["Timestamp"] = AWSSDKUtils.FormattedCurrentTimestampISO8601;
            parameters["Version"] = this.config.ServiceVersion;
            if (!this.config.SignatureVersion.Equals("2"))
            {
                throw new AmazonSQSException("Invalid Signature Version specified");
            }
            string data = AWSSDKUtils.CalculateStringToSignV2(parameters, queueUrl);
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

        public ChangeMessageVisibilityResponse ChangeMessageVisibility(ChangeMessageVisibilityRequest request)
        {
            return this.Invoke<ChangeMessageVisibilityResponse>(ConvertChangeMessageVisibility(request));
        }

        private static HttpWebRequest ConfigureWebRequest(int contentLength, string queueUrl, AmazonSQSConfig config)
        {
            HttpWebRequest request = WebRequest.Create(queueUrl) as HttpWebRequest;
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

        private static IDictionary<string, string> ConvertAddPermission(AddPermissionRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "AddPermission";
            if (request.IsSetQueueUrl())
            {
                dictionary["QueueUrl"] = request.QueueUrl;
            }
            if (request.IsSetLabel())
            {
                dictionary["Label"] = request.Label;
            }
            List<string> aWSAccountId = request.AWSAccountId;
            int num = 1;
            foreach (string str in aWSAccountId)
            {
                dictionary["AWSAccountId" + "." + num] = str;
                num++;
            }
            List<string> actionName = request.ActionName;
            int num2 = 1;
            foreach (string str2 in actionName)
            {
                dictionary["ActionName" + "." + num2] = str2;
                num2++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertChangeMessageVisibility(ChangeMessageVisibilityRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "ChangeMessageVisibility";
            if (request.IsSetQueueUrl())
            {
                dictionary["QueueUrl"] = request.QueueUrl;
            }
            if (request.IsSetReceiptHandle())
            {
                dictionary["ReceiptHandle"] = request.ReceiptHandle;
            }
            if (request.IsSetVisibilityTimeout())
            {
                dictionary["VisibilityTimeout"] = request.VisibilityTimeout.ToString();
            }
            List<Amazon.SQS.Model.Attribute> list = request.Attribute;
            int num = 1;
            foreach (Amazon.SQS.Model.Attribute attribute in list)
            {
                if (attribute.IsSetName())
                {
                    dictionary[string.Concat(new object[] { "Attribute", ".", num, ".", "Name" })] = attribute.Name;
                }
                if (attribute.IsSetValue())
                {
                    dictionary[string.Concat(new object[] { "Attribute", ".", num, ".", "Value" })] = attribute.Value;
                }
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertCreateQueue(CreateQueueRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "CreateQueue";
            if (request.IsSetQueueName())
            {
                dictionary["QueueName"] = request.QueueName;
            }
            if (request.IsSetDefaultVisibilityTimeout())
            {
                dictionary["DefaultVisibilityTimeout"] = request.DefaultVisibilityTimeout.ToString();
            }
            List<Amazon.SQS.Model.Attribute> list = request.Attribute;
            int num = 1;
            foreach (Amazon.SQS.Model.Attribute attribute in list)
            {
                if (attribute.IsSetName())
                {
                    dictionary[string.Concat(new object[] { "Attribute", ".", num, ".", "Name" })] = attribute.Name;
                }
                if (attribute.IsSetValue())
                {
                    dictionary[string.Concat(new object[] { "Attribute", ".", num, ".", "Value" })] = attribute.Value;
                }
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDeleteMessage(DeleteMessageRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DeleteMessage";
            if (request.IsSetQueueUrl())
            {
                dictionary["QueueUrl"] = request.QueueUrl;
            }
            if (request.IsSetReceiptHandle())
            {
                dictionary["ReceiptHandle"] = request.ReceiptHandle;
            }
            List<Amazon.SQS.Model.Attribute> list = request.Attribute;
            int num = 1;
            foreach (Amazon.SQS.Model.Attribute attribute in list)
            {
                if (attribute.IsSetName())
                {
                    dictionary[string.Concat(new object[] { "Attribute", ".", num, ".", "Name" })] = attribute.Name;
                }
                if (attribute.IsSetValue())
                {
                    dictionary[string.Concat(new object[] { "Attribute", ".", num, ".", "Value" })] = attribute.Value;
                }
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDeleteQueue(DeleteQueueRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DeleteQueue";
            if (request.IsSetQueueUrl())
            {
                dictionary["QueueUrl"] = request.QueueUrl;
            }
            List<Amazon.SQS.Model.Attribute> list = request.Attribute;
            int num = 1;
            foreach (Amazon.SQS.Model.Attribute attribute in list)
            {
                if (attribute.IsSetName())
                {
                    dictionary[string.Concat(new object[] { "Attribute", ".", num, ".", "Name" })] = attribute.Name;
                }
                if (attribute.IsSetValue())
                {
                    dictionary[string.Concat(new object[] { "Attribute", ".", num, ".", "Value" })] = attribute.Value;
                }
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertGetQueueAttributes(GetQueueAttributesRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "GetQueueAttributes";
            if (request.IsSetQueueUrl())
            {
                dictionary["QueueUrl"] = request.QueueUrl;
            }
            List<string> attributeName = request.AttributeName;
            int num = 1;
            foreach (string str in attributeName)
            {
                dictionary["AttributeName" + "." + num] = str;
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertListQueues(ListQueuesRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "ListQueues";
            if (request.IsSetQueueNamePrefix())
            {
                dictionary["QueueNamePrefix"] = request.QueueNamePrefix;
            }
            List<Amazon.SQS.Model.Attribute> list = request.Attribute;
            int num = 1;
            foreach (Amazon.SQS.Model.Attribute attribute in list)
            {
                if (attribute.IsSetName())
                {
                    dictionary[string.Concat(new object[] { "Attribute", ".", num, ".", "Name" })] = attribute.Name;
                }
                if (attribute.IsSetValue())
                {
                    dictionary[string.Concat(new object[] { "Attribute", ".", num, ".", "Value" })] = attribute.Value;
                }
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertReceiveMessage(ReceiveMessageRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "ReceiveMessage";
            if (request.IsSetQueueUrl())
            {
                dictionary["QueueUrl"] = request.QueueUrl;
            }
            if (request.IsSetMaxNumberOfMessages())
            {
                dictionary["MaxNumberOfMessages"] = request.MaxNumberOfMessages.ToString();
            }
            if (request.IsSetVisibilityTimeout())
            {
                dictionary["VisibilityTimeout"] = request.VisibilityTimeout.ToString();
            }
            List<string> attributeName = request.AttributeName;
            int num = 1;
            foreach (string str in attributeName)
            {
                dictionary["AttributeName" + "." + num] = str;
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertRemovePermission(RemovePermissionRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "RemovePermission";
            if (request.IsSetQueueUrl())
            {
                dictionary["QueueUrl"] = request.QueueUrl;
            }
            if (request.IsSetLabel())
            {
                dictionary["Label"] = request.Label;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertSendMessage(SendMessageRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "SendMessage";
            if (request.IsSetQueueUrl())
            {
                dictionary["QueueUrl"] = request.QueueUrl;
            }
            if (request.IsSetMessageBody())
            {
                dictionary["MessageBody"] = request.MessageBody;
            }
            List<Amazon.SQS.Model.Attribute> list = request.Attribute;
            int num = 1;
            foreach (Amazon.SQS.Model.Attribute attribute in list)
            {
                if (attribute.IsSetName())
                {
                    dictionary[string.Concat(new object[] { "Attribute", ".", num, ".", "Name" })] = attribute.Name;
                }
                if (attribute.IsSetValue())
                {
                    dictionary[string.Concat(new object[] { "Attribute", ".", num, ".", "Value" })] = attribute.Value;
                }
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertSetQueueAttributes(SetQueueAttributesRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "SetQueueAttributes";
            if (request.IsSetQueueUrl())
            {
                dictionary["QueueUrl"] = request.QueueUrl;
            }
            List<Amazon.SQS.Model.Attribute> list = request.Attribute;
            int num = 1;
            foreach (Amazon.SQS.Model.Attribute attribute in list)
            {
                if (attribute.IsSetName())
                {
                    dictionary[string.Concat(new object[] { "Attribute", ".", num, ".", "Name" })] = attribute.Name;
                }
                if (attribute.IsSetValue())
                {
                    dictionary[string.Concat(new object[] { "Attribute", ".", num, ".", "Value" })] = attribute.Value;
                }
                num++;
            }
            return dictionary;
        }

        public CreateQueueResponse CreateQueue(CreateQueueRequest request)
        {
            return this.Invoke<CreateQueueResponse>(ConvertCreateQueue(request));
        }

        public DeleteMessageResponse DeleteMessage(DeleteMessageRequest request)
        {
            return this.Invoke<DeleteMessageResponse>(ConvertDeleteMessage(request));
        }

        public DeleteQueueResponse DeleteQueue(DeleteQueueRequest request)
        {
            return this.Invoke<DeleteQueueResponse>(ConvertDeleteQueue(request));
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

        ~AmazonSQSClient()
        {
            this.Dispose(false);
        }

        public GetQueueAttributesResponse GetQueueAttributes(GetQueueAttributesRequest request)
        {
            return this.Invoke<GetQueueAttributesResponse>(ConvertGetQueueAttributes(request));
        }

        private T Invoke<T>(IDictionary<string, string> parameters)
        {
            string local1 = parameters["Action"];
            T local = default(T);
            string queueUrl = parameters.ContainsKey("QueueUrl") ? parameters["QueueUrl"] : this.config.ServiceURL;
            if (parameters.ContainsKey("QueueUrl"))
            {
                parameters.Remove("QueueUrl");
            }
            HttpStatusCode status = (HttpStatusCode) 0;
            this.AddRequiredParameters(parameters, queueUrl);
            string parametersAsString = AWSSDKUtils.GetParametersAsString(parameters);
            byte[] bytes = Encoding.UTF8.GetBytes(parametersAsString);
            bool flag = true;
            int num = 0;
            int maxRetries = this.config.IsSetMaxErrorRetry() ? this.config.MaxErrorRetry : 3;
            do
            {
                string s = null;
                HttpWebRequest request = ConfigureWebRequest(bytes.Length, queueUrl, this.config);
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
                            s = reader.ReadToEnd();
                        }
                    }
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    using (XmlTextReader reader2 = new XmlTextReader(new StringReader(s)))
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
                            s = reader3.ReadToEnd();
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
                            using (XmlTextReader reader4 = new XmlTextReader(new StringReader(s)))
                            {
                                XmlSerializer serializer2 = new XmlSerializer(typeof(ErrorResponse));
                                ErrorResponse response3 = (ErrorResponse) serializer2.Deserialize(reader4);
                                Error error = response3.Error[0];
                                throw new AmazonSQSException(error.Message, status, error.Code, error.Type, response3.RequestId, response3.ToXML());
                            }
                        }
                        catch (Exception exception2)
                        {
                            if (exception2 is AmazonSQSException)
                            {
                                throw;
                            }
                            throw ReportAnyErrors(s, status);
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

        public ListQueuesResponse ListQueues(ListQueuesRequest request)
        {
            return this.Invoke<ListQueuesResponse>(ConvertListQueues(request));
        }

        private static void PauseOnRetry(int retries, int maxRetries, HttpStatusCode status)
        {
            if (retries > maxRetries)
            {
                throw new AmazonSQSException("Maximum number of retry attempts reached : " + (retries - 1), status);
            }
            int millisecondsTimeout = ((int) Math.Pow(4.0, (double) retries)) * 100;
            Thread.Sleep(millisecondsTimeout);
        }

        public ReceiveMessageResponse ReceiveMessage(ReceiveMessageRequest request)
        {
            return this.Invoke<ReceiveMessageResponse>(ConvertReceiveMessage(request));
        }

        public RemovePermissionResponse RemovePermission(RemovePermissionRequest request)
        {
            return this.Invoke<RemovePermissionResponse>(ConvertRemovePermission(request));
        }

        private static AmazonSQSException ReportAnyErrors(string responseBody, HttpStatusCode status)
        {
            if ((responseBody != null) && responseBody.StartsWith("<"))
            {
                Match match = Regex.Match(responseBody, "<RequestId>(.*)</RequestId>.*<Error><Code>(.*)</Code><Message>(.*)</Message></Error>.*(<Error>)?", RegexOptions.Multiline);
                Match match2 = Regex.Match(responseBody, "<Error><Code>(.*)</Code><Message>(.*)</Message></Error>.*(<Error>)?.*<RequestID>(.*)</RequestID>", RegexOptions.Multiline);
                if (match.Success)
                {
                    string requestId = match.Groups[1].Value;
                    return new AmazonSQSException(match.Groups[3].Value, status, match.Groups[2].Value, "Unknown", requestId, responseBody);
                }
                if (match2.Success)
                {
                    string errorCode = match2.Groups[1].Value;
                    string message = match2.Groups[2].Value;
                    return new AmazonSQSException(message, status, errorCode, "Unknown", match2.Groups[4].Value, responseBody);
                }
                return new AmazonSQSException("Internal Error", status);
            }
            return new AmazonSQSException("Internal Error", status);
        }

        public SendMessageResponse SendMessage(SendMessageRequest request)
        {
            return this.Invoke<SendMessageResponse>(ConvertSendMessage(request));
        }

        public SetQueueAttributesResponse SetQueueAttributes(SetQueueAttributesRequest request)
        {
            return this.Invoke<SetQueueAttributesResponse>(ConvertSetQueueAttributes(request));
        }
    }
}

