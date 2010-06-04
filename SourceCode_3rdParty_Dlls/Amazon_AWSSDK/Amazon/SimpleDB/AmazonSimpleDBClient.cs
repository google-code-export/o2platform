namespace Amazon.SimpleDB
{
    using Amazon.SimpleDB.Model;
    using Amazon.SimpleDB.Util;
    using Amazon.Util;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
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

    public class AmazonSimpleDBClient : AmazonSimpleDB, IDisposable
    {
        private string awsAccessKeyId;
        private SecureString awsSecretAccessKey;
        private string clearAwsSecretAccessKey;
        private AmazonSimpleDBConfig config;
        private bool disposed;

        public AmazonSimpleDBClient(string awsAccessKeyId, string awsSecretAccessKey) : this(awsAccessKeyId, awsSecretAccessKey, new AmazonSimpleDBConfig())
        {
        }

        public AmazonSimpleDBClient(string awsAccessKeyId, SecureString awsSecretAccessKey, AmazonSimpleDBConfig config)
        {
            this.awsAccessKeyId = awsAccessKeyId;
            this.awsSecretAccessKey = awsSecretAccessKey;
            this.config = config;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.UseNagleAlgorithm = false;
        }

        public AmazonSimpleDBClient(string awsAccessKeyId, string awsSecretAccessKey, AmazonSimpleDBConfig config)
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

        private void AddRequiredParameters(IDictionary<string, string> parameters)
        {
            string str2;
            if (string.IsNullOrEmpty(this.awsAccessKeyId))
            {
                throw new AmazonSimpleDBException("The AWS Access Key ID cannot be NULL or a Zero length string");
            }
            parameters["AWSAccessKeyId"] = this.awsAccessKeyId;
            parameters["SignatureVersion"] = this.config.SignatureVersion;
            parameters["SignatureMethod"] = this.config.SignatureMethod;
            parameters["Timestamp"] = AmazonSimpleDBUtil.FormattedCurrentTimestamp;
            parameters["Version"] = this.config.ServiceVersion;
            if (!this.config.SignatureVersion.Equals("2"))
            {
                throw new AmazonSimpleDBException("Invalid Signature Version specified");
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

        public BatchPutAttributesResponse BatchPutAttributes(BatchPutAttributesRequest request)
        {
            return this.Invoke<BatchPutAttributesResponse>(ConvertBatchPutAttributes(request));
        }

        private static HttpWebRequest ConfigureWebRequest(int contentLength, AmazonSimpleDBConfig config)
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

        private static IDictionary<string, string> ConvertBatchPutAttributes(BatchPutAttributesRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "BatchPutAttributes";
            if (request.IsSetDomainName())
            {
                dictionary["DomainName"] = request.DomainName;
            }
            List<ReplaceableItem> list = request.Item;
            int num = 1;
            foreach (ReplaceableItem item in list)
            {
                if (item.IsSetItemName())
                {
                    dictionary[string.Concat(new object[] { "Item", ".", num, ".", "ItemName" })] = item.ItemName;
                }
                List<ReplaceableAttribute> list2 = item.Attribute;
                int num2 = 1;
                foreach (ReplaceableAttribute attribute in list2)
                {
                    if (attribute.IsSetName())
                    {
                        dictionary[string.Concat(new object[] { "Item", ".", num, ".", "Attribute", ".", num2, ".", "Name" })] = attribute.Name;
                    }
                    if (attribute.IsSetValue())
                    {
                        dictionary[string.Concat(new object[] { "Item", ".", num, ".", "Attribute", ".", num2, ".", "Value" })] = attribute.Value;
                    }
                    if (attribute.IsSetReplace())
                    {
                        dictionary[string.Concat(new object[] { "Item", ".", num, ".", "Attribute", ".", num2, ".", "Replace" })] = attribute.Replace.ToString().ToLower();
                    }
                    num2++;
                }
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertCreateDomain(CreateDomainRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "CreateDomain";
            if (request.IsSetDomainName())
            {
                dictionary["DomainName"] = request.DomainName;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDeleteAttributes(DeleteAttributesRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DeleteAttributes";
            if (request.IsSetDomainName())
            {
                dictionary["DomainName"] = request.DomainName;
            }
            if (request.IsSetItemName())
            {
                dictionary["ItemName"] = request.ItemName;
            }
            List<Amazon.SimpleDB.Model.Attribute> list = request.Attribute;
            int num = 1;
            foreach (Amazon.SimpleDB.Model.Attribute attribute in list)
            {
                if (attribute.IsSetName())
                {
                    dictionary[string.Concat(new object[] { "Attribute", ".", num, ".", "Name" })] = attribute.Name;
                }
                if (attribute.IsSetValue())
                {
                    dictionary[string.Concat(new object[] { "Attribute", ".", num, ".", "Value" })] = attribute.Value;
                }
                if (attribute.IsSetNameEncoding())
                {
                    dictionary[string.Concat(new object[] { "Attribute", ".", num, ".", "NameEncoding" })] = attribute.NameEncoding;
                }
                if (attribute.IsSetValueEncoding())
                {
                    dictionary[string.Concat(new object[] { "Attribute", ".", num, ".", "ValueEncoding" })] = attribute.ValueEncoding;
                }
                num++;
            }
            if (request.IsSetExpected())
            {
                UpdateCondition expected = request.Expected;
                if (expected.IsSetName())
                {
                    dictionary["Expected" + "." + "Name"] = expected.Name;
                }
                if (expected.IsSetValue())
                {
                    dictionary["Expected" + "." + "Value"] = expected.Value;
                }
                if (expected.IsSetExists())
                {
                    dictionary["Expected" + "." + "Exists"] = expected.Exists.ToString().ToLower();
                }
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDeleteDomain(DeleteDomainRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DeleteDomain";
            if (request.IsSetDomainName())
            {
                dictionary["DomainName"] = request.DomainName;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDomainMetadata(DomainMetadataRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DomainMetadata";
            if (request.IsSetDomainName())
            {
                dictionary["DomainName"] = request.DomainName;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertGetAttributes(GetAttributesRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "GetAttributes";
            if (request.IsSetDomainName())
            {
                dictionary["DomainName"] = request.DomainName;
            }
            if (request.IsSetItemName())
            {
                dictionary["ItemName"] = request.ItemName;
            }
            List<string> attributeName = request.AttributeName;
            int num = 1;
            foreach (string str in attributeName)
            {
                dictionary["AttributeName" + "." + num] = str;
                num++;
            }
            if (request.IsSetConsistentRead())
            {
                dictionary["ConsistentRead"] = request.ConsistentRead.ToString().ToLower();
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertListDomains(ListDomainsRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "ListDomains";
            if (request.IsSetMaxNumberOfDomains())
            {
                dictionary["MaxNumberOfDomains"] = request.MaxNumberOfDomains.ToString();
            }
            if (request.IsSetNextToken())
            {
                dictionary["NextToken"] = request.NextToken;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertPutAttributes(PutAttributesRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "PutAttributes";
            if (request.IsSetDomainName())
            {
                dictionary["DomainName"] = request.DomainName;
            }
            if (request.IsSetItemName())
            {
                dictionary["ItemName"] = request.ItemName;
            }
            List<ReplaceableAttribute> list = request.Attribute;
            int num = 1;
            foreach (ReplaceableAttribute attribute in list)
            {
                if (attribute.IsSetName())
                {
                    dictionary[string.Concat(new object[] { "Attribute", ".", num, ".", "Name" })] = attribute.Name;
                }
                if (attribute.IsSetValue())
                {
                    dictionary[string.Concat(new object[] { "Attribute", ".", num, ".", "Value" })] = attribute.Value;
                }
                if (attribute.IsSetReplace())
                {
                    dictionary[string.Concat(new object[] { "Attribute", ".", num, ".", "Replace" })] = attribute.Replace.ToString().ToLower();
                }
                num++;
            }
            if (request.IsSetExpected())
            {
                UpdateCondition expected = request.Expected;
                if (expected.IsSetName())
                {
                    dictionary["Expected" + "." + "Name"] = expected.Name;
                }
                if (expected.IsSetValue())
                {
                    dictionary["Expected" + "." + "Value"] = expected.Value;
                }
                if (expected.IsSetExists())
                {
                    dictionary["Expected" + "." + "Exists"] = expected.Exists.ToString().ToLower();
                }
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertSelect(SelectRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "Select";
            if (request.IsSetSelectExpression())
            {
                dictionary["SelectExpression"] = request.SelectExpression;
            }
            if (request.IsSetNextToken())
            {
                dictionary["NextToken"] = request.NextToken;
            }
            if (request.IsSetConsistentRead())
            {
                dictionary["ConsistentRead"] = request.ConsistentRead.ToString().ToLower();
            }
            return dictionary;
        }

        public CreateDomainResponse CreateDomain(CreateDomainRequest request)
        {
            return this.Invoke<CreateDomainResponse>(ConvertCreateDomain(request));
        }

        public DeleteAttributesResponse DeleteAttributes(DeleteAttributesRequest request)
        {
            return this.Invoke<DeleteAttributesResponse>(ConvertDeleteAttributes(request));
        }

        public DeleteDomainResponse DeleteDomain(DeleteDomainRequest request)
        {
            return this.Invoke<DeleteDomainResponse>(ConvertDeleteDomain(request));
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

        public DomainMetadataResponse DomainMetadata(DomainMetadataRequest request)
        {
            return this.Invoke<DomainMetadataResponse>(ConvertDomainMetadata(request));
        }

        ~AmazonSimpleDBClient()
        {
            this.Dispose(false);
        }

        public GetAttributesResponse GetAttributes(GetAttributesRequest request)
        {
            return this.Invoke<GetAttributesResponse>(ConvertGetAttributes(request));
        }

        private T Invoke<T>(IDictionary<string, string> parameters)
        {
            string str = parameters["Action"];
            T local = default(T);
            HttpStatusCode status = (HttpStatusCode) 0;
            DateTime utcNow = DateTime.UtcNow;
            Trace.Write(string.Format("{0}, {1}, ", str, utcNow));
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
                    DateTime time2 = DateTime.UtcNow;
                    TimeSpan span = (TimeSpan) (time2 - utcNow);
                    Trace.Write(string.Format("{0}, ", span.TotalMilliseconds));
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    DateTime time3 = DateTime.UtcNow;
                    TimeSpan span2 = (TimeSpan) (time3 - time2);
                    Trace.Write(string.Format("{0}, ", span2.TotalMilliseconds));
                    using (response)
                    {
                        if (response == null)
                        {
                            throw new WebException("The Web Response for a successful request is null!", WebExceptionStatus.ProtocolError);
                        }
                        status = response.StatusCode;
                        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            responseBody = reader.ReadToEnd().Trim();
                        }
                    }
                    DateTime time4 = DateTime.UtcNow;
                    if ((str.Equals("GetAttributes") || str.Equals("Select")) && responseBody.EndsWith(str + "Response>"))
                    {
                        responseBody = Transform(responseBody, str, base.GetType());
                    }
                    DateTime time5 = DateTime.UtcNow;
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    using (XmlTextReader reader2 = new XmlTextReader(new StringReader(responseBody)))
                    {
                        local = (T) serializer.Deserialize(reader2);
                        DateTime time6 = DateTime.UtcNow;
                        TimeSpan span3 = (TimeSpan) (time5 - time4);
                        TimeSpan span4 = (TimeSpan) (time6 - time5);
                        Trace.Write(string.Format("{0}, {1}, ", span3.TotalMilliseconds, span4.TotalMilliseconds));
                    }
                    flag = false;
                    DateTime time7 = DateTime.UtcNow;
                    Trace.Write(string.Format("{0}, ", time7));
                    TimeSpan span5 = (TimeSpan) (time7 - utcNow);
                    Trace.WriteLine(span5.TotalMilliseconds);
                    Trace.Flush();
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
                    if ((status != HttpStatusCode.InternalServerError) && (status != HttpStatusCode.ServiceUnavailable))
                    {
                        throw ReportAnyErrors(responseBody, status);
                    }
                    flag = true;
                    PauseOnRetry(++num, maxRetries, status);
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

        public ListDomainsResponse ListDomains(ListDomainsRequest request)
        {
            return this.Invoke<ListDomainsResponse>(ConvertListDomains(request));
        }

        private static void PauseOnRetry(int retries, int maxRetries, HttpStatusCode status)
        {
            if (retries > maxRetries)
            {
                throw new AmazonSimpleDBException("Maximum number of retry attempts reached : " + (retries - 1), status);
            }
            int millisecondsTimeout = ((int) Math.Pow(4.0, (double) retries)) * 100;
            Thread.Sleep(millisecondsTimeout);
        }

        public PutAttributesResponse PutAttributes(PutAttributesRequest request)
        {
            return this.Invoke<PutAttributesResponse>(ConvertPutAttributes(request));
        }

        private static AmazonSimpleDBException ReportAnyErrors(string responseBody, HttpStatusCode status)
        {
            if ((responseBody != null) && responseBody.StartsWith("<"))
            {
                Match match = Regex.Match(responseBody, "<RequestId>(.*)</RequestId>.*<Error><Code>(.*)</Code><Message>(.*)</Message></Error>.*(<Error>)?", RegexOptions.Multiline);
                Match match2 = Regex.Match(responseBody, "<Error><Code>(.*)</Code><Message>(.*)</Message></Error>.*(<Error>)?.*<RequestID>(.*)</RequestID>", RegexOptions.Multiline);
                Match match3 = Regex.Match(responseBody, "<Error><Code>(.*)</Code><Message>(.*)</Message><BoxUsage>(.*)</BoxUsage></Error>.*(<Error>)?.*<RequestID>(.*)</RequestID>", RegexOptions.Multiline);
                if (match.Success)
                {
                    string requestId = match.Groups[1].Value;
                    return new AmazonSimpleDBException(match.Groups[3].Value, status, match.Groups[2].Value, "Unknown", null, requestId, responseBody);
                }
                if (match2.Success)
                {
                    string errorCode = match2.Groups[1].Value;
                    string message = match2.Groups[2].Value;
                    return new AmazonSimpleDBException(message, status, errorCode, "Unknown", null, match2.Groups[4].Value, responseBody);
                }
                if (match3.Success)
                {
                    string str7 = match3.Groups[1].Value;
                    string str8 = match3.Groups[2].Value;
                    string boxUsage = match3.Groups[3].Value;
                    return new AmazonSimpleDBException(str8, status, str7, "Unknown", boxUsage, match3.Groups[5].Value, responseBody);
                }
                return new AmazonSimpleDBException("Internal Error", status);
            }
            return new AmazonSimpleDBException("Internal Error", status);
        }

        public SelectResponse Select(SelectRequest request)
        {
            return this.Invoke<SelectResponse>(ConvertSelect(request));
        }

        private static string Transform(string responseBody, string action, Type t)
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
    }
}

