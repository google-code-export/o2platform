namespace Amazon.AutoScaling
{
    using Amazon.AutoScaling.Model;
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

    public class AmazonAutoScalingClient : AmazonAutoScaling, IDisposable
    {
        private string awsAccessKeyId;
        private SecureString awsSecretAccessKey;
        private string clearAwsSecretAccessKey;
        private AmazonAutoScalingConfig config;
        private bool disposed;

        public AmazonAutoScalingClient(string awsAccessKeyId, string awsSecretAccessKey) : this(awsAccessKeyId, awsSecretAccessKey, new AmazonAutoScalingConfig())
        {
        }

        public AmazonAutoScalingClient(string awsAccessKeyId, SecureString awsSecretAccessKey, AmazonAutoScalingConfig config)
        {
            this.awsAccessKeyId = awsAccessKeyId;
            this.awsSecretAccessKey = awsSecretAccessKey;
            this.config = config;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.UseNagleAlgorithm = false;
        }

        public AmazonAutoScalingClient(string awsAccessKeyId, string awsSecretAccessKey, AmazonAutoScalingConfig config)
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
                throw new AmazonAutoScalingException("The AWS Access Key ID cannot be NULL or a Zero length string");
            }
            parameters["AWSAccessKeyId"] = this.awsAccessKeyId;
            parameters["SignatureVersion"] = this.config.SignatureVersion;
            parameters["SignatureMethod"] = this.config.SignatureMethod;
            parameters["Timestamp"] = AWSSDKUtils.FormattedCurrentTimestampISO8601;
            parameters["Version"] = this.config.ServiceVersion;
            if (!this.config.SignatureVersion.Equals("2"))
            {
                throw new AmazonAutoScalingException("Invalid Signature Version specified");
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

        private static HttpWebRequest ConfigureWebRequest(int contentLength, AmazonAutoScalingConfig config)
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

        private static IDictionary<string, string> ConvertCreateAutoScalingGroup(CreateAutoScalingGroupRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "CreateAutoScalingGroup";
            if (request.IsSetAutoScalingGroupName())
            {
                dictionary["AutoScalingGroupName"] = request.AutoScalingGroupName;
            }
            if (request.IsSetLaunchConfigurationName())
            {
                dictionary["LaunchConfigurationName"] = request.LaunchConfigurationName;
            }
            if (request.IsSetMinSize())
            {
                dictionary["MinSize"] = request.MinSize.ToString();
            }
            if (request.IsSetMaxSize())
            {
                dictionary["MaxSize"] = request.MaxSize.ToString();
            }
            if (request.IsSetCooldown())
            {
                dictionary["Cooldown"] = request.Cooldown.ToString();
            }
            List<string> availabilityZones = request.AvailabilityZones;
            int num = 1;
            foreach (string str in availabilityZones)
            {
                dictionary["AvailabilityZones" + ".member." + num] = str;
                num++;
            }
            List<string> loadBalancerNames = request.LoadBalancerNames;
            int num2 = 1;
            foreach (string str2 in loadBalancerNames)
            {
                dictionary["LoadBalancerNames" + ".member." + num2] = str2;
                num2++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertCreateLaunchConfiguration(CreateLaunchConfigurationRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "CreateLaunchConfiguration";
            if (request.IsSetLaunchConfigurationName())
            {
                dictionary["LaunchConfigurationName"] = request.LaunchConfigurationName;
            }
            if (request.IsSetImageId())
            {
                dictionary["ImageId"] = request.ImageId;
            }
            if (request.IsSetKeyName())
            {
                dictionary["KeyName"] = request.KeyName;
            }
            List<string> securityGroups = request.SecurityGroups;
            int num = 1;
            foreach (string str in securityGroups)
            {
                dictionary["SecurityGroups" + ".member." + num] = str;
                num++;
            }
            if (request.IsSetUserData())
            {
                dictionary["UserData"] = request.UserData;
            }
            if (request.IsSetInstanceType())
            {
                dictionary["InstanceType"] = request.InstanceType;
            }
            if (request.IsSetKernelId())
            {
                dictionary["KernelId"] = request.KernelId;
            }
            if (request.IsSetRamdiskId())
            {
                dictionary["RamdiskId"] = request.RamdiskId;
            }
            List<BlockDeviceMapping> blockDeviceMappings = request.BlockDeviceMappings;
            int num2 = 1;
            foreach (BlockDeviceMapping mapping in blockDeviceMappings)
            {
                if (mapping.IsSetVirtualName())
                {
                    dictionary[string.Concat(new object[] { "BlockDeviceMappings", ".member.", num2, ".", "VirtualName" })] = mapping.VirtualName;
                }
                if (mapping.IsSetDeviceName())
                {
                    dictionary[string.Concat(new object[] { "BlockDeviceMappings", ".member.", num2, ".", "DeviceName" })] = mapping.DeviceName;
                }
                num2++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertCreateOrUpdateScalingTrigger(CreateOrUpdateScalingTriggerRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "CreateOrUpdateScalingTrigger";
            if (request.IsSetTriggerName())
            {
                dictionary["TriggerName"] = request.TriggerName;
            }
            if (request.IsSetAutoScalingGroupName())
            {
                dictionary["AutoScalingGroupName"] = request.AutoScalingGroupName;
            }
            if (request.IsSetMeasureName())
            {
                dictionary["MeasureName"] = request.MeasureName;
            }
            if (request.IsSetStatistic())
            {
                dictionary["Statistic"] = request.Statistic;
            }
            List<Dimension> dimensions = request.Dimensions;
            int num = 1;
            foreach (Dimension dimension in dimensions)
            {
                if (dimension.IsSetName())
                {
                    dictionary[string.Concat(new object[] { "Dimensions", ".member.", num, ".", "Name" })] = dimension.Name;
                }
                if (dimension.IsSetValue())
                {
                    dictionary[string.Concat(new object[] { "Dimensions", ".member.", num, ".", "Value" })] = dimension.Value;
                }
                num++;
            }
            if (request.IsSetPeriod())
            {
                dictionary["Period"] = request.Period.ToString();
            }
            if (request.IsSetUnit())
            {
                dictionary["Unit"] = request.Unit;
            }
            if (request.IsSetCustomUnit())
            {
                dictionary["CustomUnit"] = request.CustomUnit;
            }
            if (request.IsSetNamespace())
            {
                dictionary["Namespace"] = request.Namespace;
            }
            if (request.IsSetLowerThreshold())
            {
                dictionary["LowerThreshold"] = request.LowerThreshold.ToString();
            }
            if (request.IsSetLowerBreachScaleIncrement())
            {
                dictionary["LowerBreachScaleIncrement"] = request.LowerBreachScaleIncrement;
            }
            if (request.IsSetUpperThreshold())
            {
                dictionary["UpperThreshold"] = request.UpperThreshold.ToString();
            }
            if (request.IsSetUpperBreachScaleIncrement())
            {
                dictionary["UpperBreachScaleIncrement"] = request.UpperBreachScaleIncrement;
            }
            if (request.IsSetBreachDuration())
            {
                dictionary["BreachDuration"] = request.BreachDuration.ToString();
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDeleteAutoScalingGroup(DeleteAutoScalingGroupRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DeleteAutoScalingGroup";
            if (request.IsSetAutoScalingGroupName())
            {
                dictionary["AutoScalingGroupName"] = request.AutoScalingGroupName;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDeleteLaunchConfiguration(DeleteLaunchConfigurationRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DeleteLaunchConfiguration";
            if (request.IsSetLaunchConfigurationName())
            {
                dictionary["LaunchConfigurationName"] = request.LaunchConfigurationName;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDeleteTrigger(DeleteTriggerRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DeleteTrigger";
            if (request.IsSetAutoScalingGroupName())
            {
                dictionary["AutoScalingGroupName"] = request.AutoScalingGroupName;
            }
            if (request.IsSetTriggerName())
            {
                dictionary["TriggerName"] = request.TriggerName;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeAutoScalingGroups(DescribeAutoScalingGroupsRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeAutoScalingGroups";
            List<string> autoScalingGroupNames = request.AutoScalingGroupNames;
            int num = 1;
            foreach (string str in autoScalingGroupNames)
            {
                dictionary["AutoScalingGroupNames" + ".member." + num] = str;
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeLaunchConfigurations(DescribeLaunchConfigurationsRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeLaunchConfigurations";
            List<string> launchConfigurationNames = request.LaunchConfigurationNames;
            int num = 1;
            foreach (string str in launchConfigurationNames)
            {
                dictionary["LaunchConfigurationNames" + ".member." + num] = str;
                num++;
            }
            if (request.IsSetNextToken())
            {
                dictionary["NextToken"] = request.NextToken;
            }
            if (request.IsSetMaxRecords())
            {
                dictionary["MaxRecords"] = request.MaxRecords.ToString();
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeScalingActivities(DescribeScalingActivitiesRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeScalingActivities";
            List<string> activityIds = request.ActivityIds;
            int num = 1;
            foreach (string str in activityIds)
            {
                dictionary["ActivityIds" + ".member." + num] = str;
                num++;
            }
            if (request.IsSetAutoScalingGroupName())
            {
                dictionary["AutoScalingGroupName"] = request.AutoScalingGroupName;
            }
            if (request.IsSetMaxRecords())
            {
                dictionary["MaxRecords"] = request.MaxRecords.ToString();
            }
            if (request.IsSetNextToken())
            {
                dictionary["NextToken"] = request.NextToken;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeTriggers(DescribeTriggersRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeTriggers";
            if (request.IsSetAutoScalingGroupName())
            {
                dictionary["AutoScalingGroupName"] = request.AutoScalingGroupName;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertSetDesiredCapacity(SetDesiredCapacityRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "SetDesiredCapacity";
            if (request.IsSetAutoScalingGroupName())
            {
                dictionary["AutoScalingGroupName"] = request.AutoScalingGroupName;
            }
            if (request.IsSetDesiredCapacity())
            {
                dictionary["DesiredCapacity"] = request.DesiredCapacity.ToString();
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertTerminateInstanceInAutoScalingGroup(TerminateInstanceInAutoScalingGroupRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "TerminateInstanceInAutoScalingGroup";
            if (request.IsSetInstanceId())
            {
                dictionary["InstanceId"] = request.InstanceId;
            }
            if (request.IsSetShouldDecrementDesiredCapacity())
            {
                dictionary["ShouldDecrementDesiredCapacity"] = request.ShouldDecrementDesiredCapacity.ToString().ToLower();
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertUpdateAutoScalingGroup(UpdateAutoScalingGroupRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "UpdateAutoScalingGroup";
            if (request.IsSetAutoScalingGroupName())
            {
                dictionary["AutoScalingGroupName"] = request.AutoScalingGroupName;
            }
            if (request.IsSetLaunchConfigurationName())
            {
                dictionary["LaunchConfigurationName"] = request.LaunchConfigurationName;
            }
            if (request.IsSetMinSize())
            {
                dictionary["MinSize"] = request.MinSize.ToString();
            }
            if (request.IsSetMaxSize())
            {
                dictionary["MaxSize"] = request.MaxSize.ToString();
            }
            if (request.IsSetCooldown())
            {
                dictionary["Cooldown"] = request.Cooldown.ToString();
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

        public CreateAutoScalingGroupResponse CreateAutoScalingGroup(CreateAutoScalingGroupRequest request)
        {
            return this.Invoke<CreateAutoScalingGroupResponse>(ConvertCreateAutoScalingGroup(request));
        }

        public CreateLaunchConfigurationResponse CreateLaunchConfiguration(CreateLaunchConfigurationRequest request)
        {
            return this.Invoke<CreateLaunchConfigurationResponse>(ConvertCreateLaunchConfiguration(request));
        }

        public CreateOrUpdateScalingTriggerResponse CreateOrUpdateScalingTrigger(CreateOrUpdateScalingTriggerRequest request)
        {
            return this.Invoke<CreateOrUpdateScalingTriggerResponse>(ConvertCreateOrUpdateScalingTrigger(request));
        }

        public DeleteAutoScalingGroupResponse DeleteAutoScalingGroup(DeleteAutoScalingGroupRequest request)
        {
            return this.Invoke<DeleteAutoScalingGroupResponse>(ConvertDeleteAutoScalingGroup(request));
        }

        public DeleteLaunchConfigurationResponse DeleteLaunchConfiguration(DeleteLaunchConfigurationRequest request)
        {
            return this.Invoke<DeleteLaunchConfigurationResponse>(ConvertDeleteLaunchConfiguration(request));
        }

        public DeleteTriggerResponse DeleteTrigger(DeleteTriggerRequest request)
        {
            return this.Invoke<DeleteTriggerResponse>(ConvertDeleteTrigger(request));
        }

        public DescribeAutoScalingGroupsResponse DescribeAutoScalingGroups(DescribeAutoScalingGroupsRequest request)
        {
            return this.Invoke<DescribeAutoScalingGroupsResponse>(ConvertDescribeAutoScalingGroups(request));
        }

        public DescribeLaunchConfigurationsResponse DescribeLaunchConfigurations(DescribeLaunchConfigurationsRequest request)
        {
            return this.Invoke<DescribeLaunchConfigurationsResponse>(ConvertDescribeLaunchConfigurations(request));
        }

        public DescribeScalingActivitiesResponse DescribeScalingActivities(DescribeScalingActivitiesRequest request)
        {
            return this.Invoke<DescribeScalingActivitiesResponse>(ConvertDescribeScalingActivities(request));
        }

        public DescribeTriggersResponse DescribeTriggers(DescribeTriggersRequest request)
        {
            return this.Invoke<DescribeTriggersResponse>(ConvertDescribeTriggers(request));
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

        ~AmazonAutoScalingClient()
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
                                throw new AmazonAutoScalingException(error.Message, status, error.Code, error.Type, response3.RequestId, response3.ToXML());
                            }
                        }
                        catch (Exception exception2)
                        {
                            if (exception2 is AmazonAutoScalingException)
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
                throw new AmazonAutoScalingException("Maximum number of retry attempts reached : " + (retries - 1), status);
            }
            int millisecondsTimeout = ((int) Math.Pow(4.0, (double) retries)) * 100;
            Thread.Sleep(millisecondsTimeout);
        }

        private static AmazonAutoScalingException ReportAnyErrors(string responseBody, HttpStatusCode status)
        {
            if ((responseBody != null) && responseBody.StartsWith("<"))
            {
                Match match = Regex.Match(responseBody, "<RequestId>(.*)</RequestId>.*<Error><Code>(.*)</Code><Message>(.*)</Message></Error>.*(<Error>)?", RegexOptions.Multiline);
                Match match2 = Regex.Match(responseBody, "<Error><Code>(.*)</Code><Message>(.*)</Message></Error>.*(<Error>)?.*<RequestID>(.*)</RequestID>", RegexOptions.Multiline);
                if (match.Success)
                {
                    string requestId = match.Groups[1].Value;
                    return new AmazonAutoScalingException(match.Groups[3].Value, status, match.Groups[2].Value, "Unknown", requestId, responseBody);
                }
                if (match2.Success)
                {
                    string errorCode = match2.Groups[1].Value;
                    string message = match2.Groups[2].Value;
                    return new AmazonAutoScalingException(message, status, errorCode, "Unknown", match2.Groups[4].Value, responseBody);
                }
                return new AmazonAutoScalingException("Internal Error", status);
            }
            return new AmazonAutoScalingException("Internal Error", status);
        }

        public SetDesiredCapacityResponse SetDesiredCapacity(SetDesiredCapacityRequest request)
        {
            return this.Invoke<SetDesiredCapacityResponse>(ConvertSetDesiredCapacity(request));
        }

        public TerminateInstanceInAutoScalingGroupResponse TerminateInstanceInAutoScalingGroup(TerminateInstanceInAutoScalingGroupRequest request)
        {
            return this.Invoke<TerminateInstanceInAutoScalingGroupResponse>(ConvertTerminateInstanceInAutoScalingGroup(request));
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

        public UpdateAutoScalingGroupResponse UpdateAutoScalingGroup(UpdateAutoScalingGroupRequest request)
        {
            return this.Invoke<UpdateAutoScalingGroupResponse>(ConvertUpdateAutoScalingGroup(request));
        }
    }
}

