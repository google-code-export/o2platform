namespace Amazon.ElasticLoadBalancing
{
    using Amazon.ElasticLoadBalancing.Model;
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

    public class AmazonElasticLoadBalancingClient : AmazonElasticLoadBalancing, IDisposable
    {
        private string awsAccessKeyId;
        private SecureString awsSecretAccessKey;
        private string clearAwsSecretAccessKey;
        private AmazonElasticLoadBalancingConfig config;
        private bool disposed;

        public AmazonElasticLoadBalancingClient(string awsAccessKeyId, string awsSecretAccessKey) : this(awsAccessKeyId, awsSecretAccessKey, new AmazonElasticLoadBalancingConfig())
        {
        }

        public AmazonElasticLoadBalancingClient(string awsAccessKeyId, SecureString awsSecretAccessKey, AmazonElasticLoadBalancingConfig config)
        {
            this.awsAccessKeyId = awsAccessKeyId;
            this.awsSecretAccessKey = awsSecretAccessKey;
            this.config = config;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.UseNagleAlgorithm = false;
        }

        public AmazonElasticLoadBalancingClient(string awsAccessKeyId, string awsSecretAccessKey, AmazonElasticLoadBalancingConfig config)
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
                throw new AmazonElasticLoadBalancingException("The AWS Access Key ID cannot be NULL or a Zero length string");
            }
            parameters["AWSAccessKeyId"] = this.awsAccessKeyId;
            parameters["SignatureVersion"] = this.config.SignatureVersion;
            parameters["SignatureMethod"] = this.config.SignatureMethod;
            parameters["Timestamp"] = AWSSDKUtils.FormattedCurrentTimestampISO8601;
            parameters["Version"] = this.config.ServiceVersion;
            if (!this.config.SignatureVersion.Equals("2"))
            {
                throw new AmazonElasticLoadBalancingException("Invalid Signature Version specified");
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

        public ConfigureHealthCheckResponse ConfigureHealthCheck(ConfigureHealthCheckRequest request)
        {
            return this.Invoke<ConfigureHealthCheckResponse>(ConvertConfigureHealthCheck(request));
        }

        private static HttpWebRequest ConfigureWebRequest(int contentLength, AmazonElasticLoadBalancingConfig config)
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

        private static IDictionary<string, string> ConvertConfigureHealthCheck(ConfigureHealthCheckRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "ConfigureHealthCheck";
            if (request.IsSetLoadBalancerName())
            {
                dictionary["LoadBalancerName"] = request.LoadBalancerName;
            }
            if (request.IsSetHealthCheck())
            {
                HealthCheck healthCheck = request.HealthCheck;
                if (healthCheck.IsSetTarget())
                {
                    dictionary["HealthCheck" + "." + "Target"] = healthCheck.Target;
                }
                if (healthCheck.IsSetInterval())
                {
                    dictionary["HealthCheck" + "." + "Interval"] = healthCheck.Interval.ToString();
                }
                if (healthCheck.IsSetTimeout())
                {
                    dictionary["HealthCheck" + "." + "Timeout"] = healthCheck.Timeout.ToString();
                }
                if (healthCheck.IsSetUnhealthyThreshold())
                {
                    dictionary["HealthCheck" + "." + "UnhealthyThreshold"] = healthCheck.UnhealthyThreshold.ToString();
                }
                if (healthCheck.IsSetHealthyThreshold())
                {
                    dictionary["HealthCheck" + "." + "HealthyThreshold"] = healthCheck.HealthyThreshold.ToString();
                }
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertCreateAppCookieStickinessPolicy(CreateAppCookieStickinessPolicyRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "CreateAppCookieStickinessPolicy";
            if (request.IsSetLoadBalancerName())
            {
                dictionary["LoadBalancerName"] = request.LoadBalancerName;
            }
            if (request.IsSetPolicyName())
            {
                dictionary["PolicyName"] = request.PolicyName;
            }
            if (request.IsSetCookieName())
            {
                dictionary["CookieName"] = request.CookieName;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertCreateLBCookieStickinessPolicy(CreateLBCookieStickinessPolicyRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "CreateLBCookieStickinessPolicy";
            if (request.IsSetLoadBalancerName())
            {
                dictionary["LoadBalancerName"] = request.LoadBalancerName;
            }
            if (request.IsSetPolicyName())
            {
                dictionary["PolicyName"] = request.PolicyName;
            }
            if (request.IsSetCookieExpirationPeriod())
            {
                dictionary["CookieExpirationPeriod"] = request.CookieExpirationPeriod.ToString();
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertCreateLoadBalancer(CreateLoadBalancerRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "CreateLoadBalancer";
            if (request.IsSetLoadBalancerName())
            {
                dictionary["LoadBalancerName"] = request.LoadBalancerName;
            }
            List<Listener> listeners = request.Listeners;
            int num = 1;
            foreach (Listener listener in listeners)
            {
                if (listener.IsSetProtocol())
                {
                    dictionary[string.Concat(new object[] { "Listeners", ".member.", num, ".", "Protocol" })] = listener.Protocol;
                }
                if (listener.IsSetLoadBalancerPort())
                {
                    dictionary[string.Concat(new object[] { "Listeners", ".member.", num, ".", "LoadBalancerPort" })] = listener.LoadBalancerPort.ToString();
                }
                if (listener.IsSetInstancePort())
                {
                    dictionary[string.Concat(new object[] { "Listeners", ".member.", num, ".", "InstancePort" })] = listener.InstancePort.ToString();
                }
                num++;
            }
            List<string> availabilityZones = request.AvailabilityZones;
            int num2 = 1;
            foreach (string str in availabilityZones)
            {
                dictionary["AvailabilityZones" + ".member." + num2] = str;
                num2++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDeleteLoadBalancer(DeleteLoadBalancerRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DeleteLoadBalancer";
            if (request.IsSetLoadBalancerName())
            {
                dictionary["LoadBalancerName"] = request.LoadBalancerName;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDeleteLoadBalancerPolicy(DeleteLoadBalancerPolicyRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DeleteLoadBalancerPolicy";
            if (request.IsSetLoadBalancerName())
            {
                dictionary["LoadBalancerName"] = request.LoadBalancerName;
            }
            if (request.IsSetPolicyName())
            {
                dictionary["PolicyName"] = request.PolicyName;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDeregisterInstancesFromLoadBalancer(DeregisterInstancesFromLoadBalancerRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DeregisterInstancesFromLoadBalancer";
            if (request.IsSetLoadBalancerName())
            {
                dictionary["LoadBalancerName"] = request.LoadBalancerName;
            }
            List<Instance> instances = request.Instances;
            int num = 1;
            foreach (Instance instance in instances)
            {
                if (instance.IsSetInstanceId())
                {
                    dictionary[string.Concat(new object[] { "Instances", ".member.", num, ".", "InstanceId" })] = instance.InstanceId;
                }
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeInstanceHealth(DescribeInstanceHealthRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeInstanceHealth";
            if (request.IsSetLoadBalancerName())
            {
                dictionary["LoadBalancerName"] = request.LoadBalancerName;
            }
            List<Instance> instances = request.Instances;
            int num = 1;
            foreach (Instance instance in instances)
            {
                if (instance.IsSetInstanceId())
                {
                    dictionary[string.Concat(new object[] { "Instances", ".member.", num, ".", "InstanceId" })] = instance.InstanceId;
                }
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeLoadBalancers(DescribeLoadBalancersRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeLoadBalancers";
            List<string> loadBalancerNames = request.LoadBalancerNames;
            int num = 1;
            foreach (string str in loadBalancerNames)
            {
                dictionary["LoadBalancerNames" + ".member." + num] = str;
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDisableAvailabilityZonesForLoadBalancer(DisableAvailabilityZonesForLoadBalancerRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DisableAvailabilityZonesForLoadBalancer";
            if (request.IsSetLoadBalancerName())
            {
                dictionary["LoadBalancerName"] = request.LoadBalancerName;
            }
            List<string> availabilityZones = request.AvailabilityZones;
            int num = 1;
            foreach (string str in availabilityZones)
            {
                dictionary["AvailabilityZones" + ".member." + num] = str;
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertEnableAvailabilityZonesForLoadBalancer(EnableAvailabilityZonesForLoadBalancerRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "EnableAvailabilityZonesForLoadBalancer";
            if (request.IsSetLoadBalancerName())
            {
                dictionary["LoadBalancerName"] = request.LoadBalancerName;
            }
            List<string> availabilityZones = request.AvailabilityZones;
            int num = 1;
            foreach (string str in availabilityZones)
            {
                dictionary["AvailabilityZones" + ".member." + num] = str;
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertRegisterInstancesWithLoadBalancer(RegisterInstancesWithLoadBalancerRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "RegisterInstancesWithLoadBalancer";
            if (request.IsSetLoadBalancerName())
            {
                dictionary["LoadBalancerName"] = request.LoadBalancerName;
            }
            List<Instance> instances = request.Instances;
            int num = 1;
            foreach (Instance instance in instances)
            {
                if (instance.IsSetInstanceId())
                {
                    dictionary[string.Concat(new object[] { "Instances", ".member.", num, ".", "InstanceId" })] = instance.InstanceId;
                }
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertSetLoadBalancerPoliciesOfListener(SetLoadBalancerPoliciesOfListenerRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "SetLoadBalancerPoliciesOfListener";
            if (request.IsSetLoadBalancerName())
            {
                dictionary["LoadBalancerName"] = request.LoadBalancerName;
            }
            if (request.IsSetLoadBalancerPort())
            {
                dictionary["LoadBalancerPort"] = request.LoadBalancerPort.ToString();
            }
            List<string> policyNames = request.PolicyNames;
            int num = 1;
            foreach (string str in policyNames)
            {
                dictionary["PolicyNames" + ".member." + num] = str;
                num++;
            }
            return dictionary;
        }

        public CreateAppCookieStickinessPolicyResponse CreateAppCookieStickinessPolicy(CreateAppCookieStickinessPolicyRequest request)
        {
            return this.Invoke<CreateAppCookieStickinessPolicyResponse>(ConvertCreateAppCookieStickinessPolicy(request));
        }

        public CreateLBCookieStickinessPolicyResponse CreateLBCookieStickinessPolicy(CreateLBCookieStickinessPolicyRequest request)
        {
            return this.Invoke<CreateLBCookieStickinessPolicyResponse>(ConvertCreateLBCookieStickinessPolicy(request));
        }

        public CreateLoadBalancerResponse CreateLoadBalancer(CreateLoadBalancerRequest request)
        {
            return this.Invoke<CreateLoadBalancerResponse>(ConvertCreateLoadBalancer(request));
        }

        public DeleteLoadBalancerResponse DeleteLoadBalancer(DeleteLoadBalancerRequest request)
        {
            return this.Invoke<DeleteLoadBalancerResponse>(ConvertDeleteLoadBalancer(request));
        }

        public DeleteLoadBalancerPolicyResponse DeleteLoadBalancerPolicy(DeleteLoadBalancerPolicyRequest request)
        {
            return this.Invoke<DeleteLoadBalancerPolicyResponse>(ConvertDeleteLoadBalancerPolicy(request));
        }

        public DeregisterInstancesFromLoadBalancerResponse DeregisterInstancesFromLoadBalancer(DeregisterInstancesFromLoadBalancerRequest request)
        {
            return this.Invoke<DeregisterInstancesFromLoadBalancerResponse>(ConvertDeregisterInstancesFromLoadBalancer(request));
        }

        public DescribeInstanceHealthResponse DescribeInstanceHealth(DescribeInstanceHealthRequest request)
        {
            return this.Invoke<DescribeInstanceHealthResponse>(ConvertDescribeInstanceHealth(request));
        }

        public DescribeLoadBalancersResponse DescribeLoadBalancers(DescribeLoadBalancersRequest request)
        {
            return this.Invoke<DescribeLoadBalancersResponse>(ConvertDescribeLoadBalancers(request));
        }

        public DisableAvailabilityZonesForLoadBalancerResponse DisableAvailabilityZonesForLoadBalancer(DisableAvailabilityZonesForLoadBalancerRequest request)
        {
            return this.Invoke<DisableAvailabilityZonesForLoadBalancerResponse>(ConvertDisableAvailabilityZonesForLoadBalancer(request));
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

        public EnableAvailabilityZonesForLoadBalancerResponse EnableAvailabilityZonesForLoadBalancer(EnableAvailabilityZonesForLoadBalancerRequest request)
        {
            return this.Invoke<EnableAvailabilityZonesForLoadBalancerResponse>(ConvertEnableAvailabilityZonesForLoadBalancer(request));
        }

        ~AmazonElasticLoadBalancingClient()
        {
            this.Dispose(false);
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
                                throw new AmazonElasticLoadBalancingException(error.Message, status, error.Code, error.Type, response3.RequestId, response3.ToXML());
                            }
                        }
                        catch (Exception exception2)
                        {
                            if (exception2 is AmazonElasticLoadBalancingException)
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

        private static void PauseOnRetry(int retries, int maxRetries, HttpStatusCode status)
        {
            if (retries > maxRetries)
            {
                throw new AmazonElasticLoadBalancingException("Maximum number of retry attempts reached : " + (retries - 1), status);
            }
            int millisecondsTimeout = ((int) Math.Pow(4.0, (double) retries)) * 100;
            Thread.Sleep(millisecondsTimeout);
        }

        public RegisterInstancesWithLoadBalancerResponse RegisterInstancesWithLoadBalancer(RegisterInstancesWithLoadBalancerRequest request)
        {
            return this.Invoke<RegisterInstancesWithLoadBalancerResponse>(ConvertRegisterInstancesWithLoadBalancer(request));
        }

        private static AmazonElasticLoadBalancingException ReportAnyErrors(string responseBody, HttpStatusCode status)
        {
            if ((responseBody != null) && responseBody.StartsWith("<"))
            {
                Match match = Regex.Match(responseBody, "<RequestId>(.*)</RequestId>.*<Error><Code>(.*)</Code><Message>(.*)</Message></Error>.*(<Error>)?", RegexOptions.Multiline);
                Match match2 = Regex.Match(responseBody, "<Error><Code>(.*)</Code><Message>(.*)</Message></Error>.*(<Error>)?.*<RequestID>(.*)</RequestID>", RegexOptions.Multiline);
                if (match.Success)
                {
                    string requestId = match.Groups[1].Value;
                    return new AmazonElasticLoadBalancingException(match.Groups[3].Value, status, match.Groups[2].Value, "Unknown", requestId, responseBody);
                }
                if (match2.Success)
                {
                    string errorCode = match2.Groups[1].Value;
                    string message = match2.Groups[2].Value;
                    return new AmazonElasticLoadBalancingException(message, status, errorCode, "Unknown", match2.Groups[4].Value, responseBody);
                }
                return new AmazonElasticLoadBalancingException("Internal Error", status);
            }
            return new AmazonElasticLoadBalancingException("Internal Error", status);
        }

        public SetLoadBalancerPoliciesOfListenerResponse SetLoadBalancerPoliciesOfListener(SetLoadBalancerPoliciesOfListenerRequest request)
        {
            return this.Invoke<SetLoadBalancerPoliciesOfListenerResponse>(ConvertSetLoadBalancerPoliciesOfListener(request));
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
    }
}

