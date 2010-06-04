namespace Amazon.ElasticMapReduce
{
    using Amazon.ElasticMapReduce.Model;
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

    public class AmazonElasticMapReduceClient : AmazonElasticMapReduce, IDisposable
    {
        private string awsAccessKeyId;
        private SecureString awsSecretAccessKey;
        private string clearAwsSecretAccessKey;
        private AmazonElasticMapReduceConfig config;
        private bool disposed;

        public AmazonElasticMapReduceClient(string awsAccessKeyId, string awsSecretAccessKey) : this(awsAccessKeyId, awsSecretAccessKey, new AmazonElasticMapReduceConfig())
        {
        }

        public AmazonElasticMapReduceClient(string awsAccessKeyId, SecureString awsSecretAccessKey, AmazonElasticMapReduceConfig config)
        {
            this.awsAccessKeyId = awsAccessKeyId;
            this.awsSecretAccessKey = awsSecretAccessKey;
            this.config = config;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.UseNagleAlgorithm = false;
        }

        public AmazonElasticMapReduceClient(string awsAccessKeyId, string awsSecretAccessKey, AmazonElasticMapReduceConfig config)
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

        public AddJobFlowStepsResponse AddJobFlowSteps(AddJobFlowStepsRequest request)
        {
            return this.Invoke<AddJobFlowStepsResponse>(ConvertAddJobFlowSteps(request));
        }

        private void AddRequiredParameters(IDictionary<string, string> parameters)
        {
            string str2;
            if (string.IsNullOrEmpty(this.awsAccessKeyId))
            {
                throw new AmazonElasticMapReduceException("The AWS Access Key ID cannot be NULL or a Zero length string");
            }
            parameters["AWSAccessKeyId"] = this.awsAccessKeyId;
            parameters["SignatureVersion"] = this.config.SignatureVersion;
            parameters["SignatureMethod"] = this.config.SignatureMethod;
            parameters["Timestamp"] = AWSSDKUtils.FormattedCurrentTimestampISO8601;
            parameters["Version"] = this.config.ServiceVersion;
            if (!this.config.SignatureVersion.Equals("2"))
            {
                throw new AmazonElasticMapReduceException("Invalid Signature Version specified");
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

        private static HttpWebRequest ConfigureWebRequest(int contentLength, AmazonElasticMapReduceConfig config)
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

        private static IDictionary<string, string> ConvertAddJobFlowSteps(AddJobFlowStepsRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "AddJobFlowSteps";
            if (request.IsSetJobFlowId())
            {
                dictionary["JobFlowId"] = request.JobFlowId;
            }
            List<StepConfig> steps = request.Steps;
            int num = 1;
            foreach (StepConfig config in steps)
            {
                if (config.IsSetName())
                {
                    dictionary[string.Concat(new object[] { "Steps", ".member.", num, ".", "Name" })] = config.Name;
                }
                if (config.IsSetActionOnFailure())
                {
                    dictionary[string.Concat(new object[] { "Steps", ".member.", num, ".", "ActionOnFailure" })] = config.ActionOnFailure;
                }
                if (config.IsSetHadoopJarStep())
                {
                    HadoopJarStepConfig hadoopJarStep = config.HadoopJarStep;
                    List<KeyValue> properties = hadoopJarStep.Properties;
                    int num2 = 1;
                    foreach (KeyValue value2 in properties)
                    {
                        if (value2.IsSetKey())
                        {
                            dictionary[string.Concat(new object[] { "Steps", ".member.", num, ".", "HadoopJarStep", ".", "Properties", ".member.", num2, ".", "Key" })] = value2.Key;
                        }
                        if (value2.IsSetValue())
                        {
                            dictionary[string.Concat(new object[] { "Steps", ".member.", num, ".", "HadoopJarStep", ".", "Properties", ".member.", num2, ".", "Value" })] = value2.Value;
                        }
                        num2++;
                    }
                    if (hadoopJarStep.IsSetJar())
                    {
                        dictionary[string.Concat(new object[] { "Steps", ".member.", num, ".", "HadoopJarStep", ".", "Jar" })] = hadoopJarStep.Jar;
                    }
                    if (hadoopJarStep.IsSetMainClass())
                    {
                        dictionary[string.Concat(new object[] { "Steps", ".member.", num, ".", "HadoopJarStep", ".", "MainClass" })] = hadoopJarStep.MainClass;
                    }
                    List<string> args = hadoopJarStep.Args;
                    int num3 = 1;
                    foreach (string str in args)
                    {
                        dictionary[string.Concat(new object[] { "Steps", ".member.", num, ".", "HadoopJarStep", ".", "Args", ".member.", num3 })] = str;
                        num3++;
                    }
                }
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeJobFlows(DescribeJobFlowsRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeJobFlows";
            if (request.IsSetCreatedAfter())
            {
                dictionary["CreatedAfter"] = request.CreatedAfter;
            }
            if (request.IsSetCreatedBefore())
            {
                dictionary["CreatedBefore"] = request.CreatedBefore;
            }
            List<string> jobFlowIds = request.JobFlowIds;
            int num = 1;
            foreach (string str in jobFlowIds)
            {
                dictionary["JobFlowIds" + ".member." + num] = str;
                num++;
            }
            List<string> jobFlowStates = request.JobFlowStates;
            int num2 = 1;
            foreach (string str2 in jobFlowStates)
            {
                dictionary["JobFlowStates" + ".member." + num2] = str2;
                num2++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertRunJobFlow(RunJobFlowRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "RunJobFlow";
            if (request.IsSetName())
            {
                dictionary["Name"] = request.Name;
            }
            if (request.IsSetLogUri())
            {
                dictionary["LogUri"] = request.LogUri;
            }
            if (request.IsSetAdditionalInfo())
            {
                dictionary["AdditionalInfo"] = request.AdditionalInfo;
            }
            if (request.IsSetInstances())
            {
                JobFlowInstancesConfig instances = request.Instances;
                if (instances.IsSetMasterInstanceType())
                {
                    dictionary["Instances" + "." + "MasterInstanceType"] = instances.MasterInstanceType;
                }
                if (instances.IsSetSlaveInstanceType())
                {
                    dictionary["Instances" + "." + "SlaveInstanceType"] = instances.SlaveInstanceType;
                }
                if (instances.IsSetInstanceCount())
                {
                    dictionary["Instances" + "." + "InstanceCount"] = instances.InstanceCount.ToString();
                }
                if (instances.IsSetEc2KeyName())
                {
                    dictionary["Instances" + "." + "Ec2KeyName"] = instances.Ec2KeyName;
                }
                if (instances.IsSetPlacement())
                {
                    PlacementType placement = instances.Placement;
                    if (placement.IsSetAvailabilityZone())
                    {
                        dictionary["Instances" + "." + "Placement" + "." + "AvailabilityZone"] = placement.AvailabilityZone;
                    }
                }
                if (instances.IsSetKeepJobFlowAliveWhenNoSteps())
                {
                    dictionary["Instances" + "." + "KeepJobFlowAliveWhenNoSteps"] = instances.KeepJobFlowAliveWhenNoSteps.ToString().ToLower();
                }
                if (instances.IsSetHadoopVersion())
                {
                    dictionary["Instances" + "." + "HadoopVersion"] = instances.HadoopVersion;
                }
            }
            List<StepConfig> steps = request.Steps;
            int num = 1;
            foreach (StepConfig config2 in steps)
            {
                if (config2.IsSetName())
                {
                    dictionary[string.Concat(new object[] { "Steps", ".member.", num, ".", "Name" })] = config2.Name;
                }
                if (config2.IsSetActionOnFailure())
                {
                    dictionary[string.Concat(new object[] { "Steps", ".member.", num, ".", "ActionOnFailure" })] = config2.ActionOnFailure;
                }
                if (config2.IsSetHadoopJarStep())
                {
                    HadoopJarStepConfig hadoopJarStep = config2.HadoopJarStep;
                    List<KeyValue> properties = hadoopJarStep.Properties;
                    int num2 = 1;
                    foreach (KeyValue value2 in properties)
                    {
                        if (value2.IsSetKey())
                        {
                            dictionary[string.Concat(new object[] { "Steps", ".member.", num, ".", "HadoopJarStep", ".", "Properties", ".member.", num2, ".", "Key" })] = value2.Key;
                        }
                        if (value2.IsSetValue())
                        {
                            dictionary[string.Concat(new object[] { "Steps", ".member.", num, ".", "HadoopJarStep", ".", "Properties", ".member.", num2, ".", "Value" })] = value2.Value;
                        }
                        num2++;
                    }
                    if (hadoopJarStep.IsSetJar())
                    {
                        dictionary[string.Concat(new object[] { "Steps", ".member.", num, ".", "HadoopJarStep", ".", "Jar" })] = hadoopJarStep.Jar;
                    }
                    if (hadoopJarStep.IsSetMainClass())
                    {
                        dictionary[string.Concat(new object[] { "Steps", ".member.", num, ".", "HadoopJarStep", ".", "MainClass" })] = hadoopJarStep.MainClass;
                    }
                    List<string> args = hadoopJarStep.Args;
                    int num3 = 1;
                    foreach (string str in args)
                    {
                        dictionary[string.Concat(new object[] { "Steps", ".member.", num, ".", "HadoopJarStep", ".", "Args", ".member.", num3 })] = str;
                        num3++;
                    }
                }
                num++;
            }
            List<BootstrapActionConfig> bootstrapActions = request.BootstrapActions;
            int num4 = 1;
            foreach (BootstrapActionConfig config4 in bootstrapActions)
            {
                if (config4.IsSetName())
                {
                    dictionary[string.Concat(new object[] { "BootstrapActions", ".member.", num4, ".", "Name" })] = config4.Name;
                }
                if (config4.IsSetScriptBootstrapAction())
                {
                    ScriptBootstrapActionConfig scriptBootstrapAction = config4.ScriptBootstrapAction;
                    if (scriptBootstrapAction.IsSetPath())
                    {
                        dictionary[string.Concat(new object[] { "BootstrapActions", ".member.", num4, ".", "ScriptBootstrapAction", ".", "Path" })] = scriptBootstrapAction.Path;
                    }
                    List<string> list5 = scriptBootstrapAction.Args;
                    int num5 = 1;
                    foreach (string str2 in list5)
                    {
                        dictionary[string.Concat(new object[] { "BootstrapActions", ".member.", num4, ".", "ScriptBootstrapAction", ".", "Args", ".member.", num5 })] = str2;
                        num5++;
                    }
                }
                num4++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertTerminateJobFlows(TerminateJobFlowsRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "TerminateJobFlows";
            List<string> jobFlowIds = request.JobFlowIds;
            int num = 1;
            foreach (string str in jobFlowIds)
            {
                dictionary["JobFlowIds" + ".member." + num] = str;
                num++;
            }
            return dictionary;
        }

        public DescribeJobFlowsResponse DescribeJobFlows(DescribeJobFlowsRequest request)
        {
            return this.Invoke<DescribeJobFlowsResponse>(ConvertDescribeJobFlows(request));
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

        ~AmazonElasticMapReduceClient()
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
                                throw new AmazonElasticMapReduceException(error.Message, status, error.Code, error.Type, response3.RequestId, response3.ToXML());
                            }
                        }
                        catch (Exception exception2)
                        {
                            if (exception2 is AmazonElasticMapReduceException)
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
                throw new AmazonElasticMapReduceException("Maximum number of retry attempts reached : " + (retries - 1), status);
            }
            int millisecondsTimeout = ((int) Math.Pow(4.0, (double) retries)) * 100;
            Thread.Sleep(millisecondsTimeout);
        }

        private static AmazonElasticMapReduceException ReportAnyErrors(string responseBody, HttpStatusCode status)
        {
            if ((responseBody != null) && responseBody.StartsWith("<"))
            {
                Match match = Regex.Match(responseBody, "<RequestId>(.*)</RequestId>.*<Error><Code>(.*)</Code><Message>(.*)</Message></Error>.*(<Error>)?", RegexOptions.Multiline);
                Match match2 = Regex.Match(responseBody, "<Error><Code>(.*)</Code><Message>(.*)</Message></Error>.*(<Error>)?.*<RequestID>(.*)</RequestID>", RegexOptions.Multiline);
                if (match.Success)
                {
                    string requestId = match.Groups[1].Value;
                    return new AmazonElasticMapReduceException(match.Groups[3].Value, status, match.Groups[2].Value, "Unknown", requestId, responseBody);
                }
                if (match2.Success)
                {
                    string errorCode = match2.Groups[1].Value;
                    string message = match2.Groups[2].Value;
                    return new AmazonElasticMapReduceException(message, status, errorCode, "Unknown", match2.Groups[4].Value, responseBody);
                }
                return new AmazonElasticMapReduceException("Internal Error", status);
            }
            return new AmazonElasticMapReduceException("Internal Error", status);
        }

        public RunJobFlowResponse RunJobFlow(RunJobFlowRequest request)
        {
            return this.Invoke<RunJobFlowResponse>(ConvertRunJobFlow(request));
        }

        public TerminateJobFlowsResponse TerminateJobFlows(TerminateJobFlowsRequest request)
        {
            return this.Invoke<TerminateJobFlowsResponse>(ConvertTerminateJobFlows(request));
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

