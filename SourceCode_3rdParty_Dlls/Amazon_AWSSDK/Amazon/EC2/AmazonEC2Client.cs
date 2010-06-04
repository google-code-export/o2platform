namespace Amazon.EC2
{
    using Amazon.EC2.Model;
    using Amazon.EC2.Util;
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

    public class AmazonEC2Client : AmazonEC2, IDisposable
    {
        private string awsAccessKeyId;
        private SecureString awsSecretAccessKey;
        private string clearAwsSecretAccessKey;
        private AmazonEC2Config config;
        private bool disposed;

        public AmazonEC2Client(string awsAccessKeyId, string awsSecretAccessKey) : this(awsAccessKeyId, awsSecretAccessKey, new AmazonEC2Config())
        {
        }

        public AmazonEC2Client(string awsAccessKeyId, SecureString awsSecretAccessKey, AmazonEC2Config config)
        {
            this.awsAccessKeyId = awsAccessKeyId;
            this.awsSecretAccessKey = awsSecretAccessKey;
            this.config = config;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.UseNagleAlgorithm = false;
        }

        public AmazonEC2Client(string awsAccessKeyId, string awsSecretAccessKey, AmazonEC2Config config)
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
                throw new AmazonEC2Exception("The AWS Access Key ID cannot be NULL or a Zero length string");
            }
            parameters["AWSAccessKeyId"] = this.awsAccessKeyId;
            parameters["SignatureVersion"] = this.config.SignatureVersion;
            parameters["SignatureMethod"] = this.config.SignatureMethod;
            parameters["Timestamp"] = AWSSDKUtils.FormattedCurrentTimestampISO8601;
            parameters["Version"] = this.config.ServiceVersion;
            if (!this.config.SignatureVersion.Equals("2"))
            {
                throw new AmazonEC2Exception("Invalid Signature Version specified");
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

        public AllocateAddressResponse AllocateAddress(AllocateAddressRequest request)
        {
            return this.Invoke<AllocateAddressResponse>(ConvertAllocateAddress(request));
        }

        public AssociateAddressResponse AssociateAddress(AssociateAddressRequest request)
        {
            return this.Invoke<AssociateAddressResponse>(ConvertAssociateAddress(request));
        }

        public AssociateDhcpOptionsResponse AssociateDhcpOptions(AssociateDhcpOptionsRequest request)
        {
            return this.Invoke<AssociateDhcpOptionsResponse>(ConvertAssociateDhcpOptions(request));
        }

        public AttachVolumeResponse AttachVolume(AttachVolumeRequest request)
        {
            return this.Invoke<AttachVolumeResponse>(ConvertAttachVolume(request));
        }

        public AttachVpnGatewayResponse AttachVpnGateway(AttachVpnGatewayRequest request)
        {
            return this.Invoke<AttachVpnGatewayResponse>(ConvertAttachVpnGateway(request));
        }

        public AuthorizeSecurityGroupIngressResponse AuthorizeSecurityGroupIngress(AuthorizeSecurityGroupIngressRequest request)
        {
            return this.Invoke<AuthorizeSecurityGroupIngressResponse>(ConvertAuthorizeSecurityGroupIngress(request));
        }

        public BundleInstanceResponse BundleInstance(BundleInstanceRequest request)
        {
            S3Storage storage = request.Storage.S3;
            if (!storage.IsSetUploadPolicy())
            {
                S3UploadPolicy policy;
                storage.AWSAccessKeyId = this.awsAccessKeyId;
                if (this.config.UseSecureStringForAwsSecretKey)
                {
                    policy = new S3UploadPolicy(this.awsAccessKeyId, this.awsSecretAccessKey, storage.Bucket, storage.Prefix, 0x5a0);
                }
                else
                {
                    policy = new S3UploadPolicy(this.clearAwsSecretAccessKey, storage.Bucket, storage.Prefix, 0x5a0);
                }
                storage.UploadPolicy = policy.PolicyString;
                storage.UploadPolicySignature = policy.PolicySignature;
            }
            return this.Invoke<BundleInstanceResponse>(ConvertBundleInstance(request));
        }

        public CancelBundleTaskResponse CancelBundleTask(CancelBundleTaskRequest request)
        {
            return this.Invoke<CancelBundleTaskResponse>(ConvertCancelBundleTask(request));
        }

        public CancelSpotInstanceRequestsResponse CancelSpotInstanceRequests(CancelSpotInstanceRequestsRequest request)
        {
            return this.Invoke<CancelSpotInstanceRequestsResponse>(ConvertCancelSpotInstanceRequests(request));
        }

        private static HttpWebRequest ConfigureWebRequest(int contentLength, AmazonEC2Config config)
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

        public ConfirmProductInstanceResponse ConfirmProductInstance(ConfirmProductInstanceRequest request)
        {
            return this.Invoke<ConfirmProductInstanceResponse>(ConvertConfirmProductInstance(request));
        }

        private static IDictionary<string, string> ConvertAllocateAddress(AllocateAddressRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "AllocateAddress";
            return dictionary;
        }

        private static IDictionary<string, string> ConvertAssociateAddress(AssociateAddressRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "AssociateAddress";
            if (request.IsSetInstanceId())
            {
                dictionary["InstanceId"] = request.InstanceId;
            }
            if (request.IsSetPublicIp())
            {
                dictionary["PublicIp"] = request.PublicIp;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertAssociateDhcpOptions(AssociateDhcpOptionsRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "AssociateDhcpOptions";
            if (request.IsSetDhcpOptionsId())
            {
                dictionary["DhcpOptionsId"] = request.DhcpOptionsId;
            }
            if (request.IsSetVpcId())
            {
                dictionary["VpcId"] = request.VpcId;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertAttachVolume(AttachVolumeRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "AttachVolume";
            if (request.IsSetVolumeId())
            {
                dictionary["VolumeId"] = request.VolumeId;
            }
            if (request.IsSetInstanceId())
            {
                dictionary["InstanceId"] = request.InstanceId;
            }
            if (request.IsSetDevice())
            {
                dictionary["Device"] = request.Device;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertAttachVpnGateway(AttachVpnGatewayRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "AttachVpnGateway";
            if (request.IsSetVpnGatewayId())
            {
                dictionary["VpnGatewayId"] = request.VpnGatewayId;
            }
            if (request.IsSetVpcId())
            {
                dictionary["VpcId"] = request.VpcId;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertAuthorizeSecurityGroupIngress(AuthorizeSecurityGroupIngressRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "AuthorizeSecurityGroupIngress";
            if (request.IsSetUserId())
            {
                dictionary["UserId"] = request.UserId;
            }
            if (request.IsSetGroupName())
            {
                dictionary["GroupName"] = request.GroupName;
            }
            if (request.IsSetSourceSecurityGroupName())
            {
                dictionary["SourceSecurityGroupName"] = request.SourceSecurityGroupName;
            }
            if (request.IsSetSourceSecurityGroupOwnerId())
            {
                dictionary["SourceSecurityGroupOwnerId"] = request.SourceSecurityGroupOwnerId;
            }
            if (request.IsSetIpProtocol())
            {
                dictionary["IpProtocol"] = request.IpProtocol;
            }
            if (request.IsSetFromPort())
            {
                dictionary["FromPort"] = request.FromPort.ToString();
            }
            if (request.IsSetToPort())
            {
                dictionary["ToPort"] = request.ToPort.ToString();
            }
            if (request.IsSetCidrIp())
            {
                dictionary["CidrIp"] = request.CidrIp;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertBundleInstance(BundleInstanceRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "BundleInstance";
            if (request.IsSetInstanceId())
            {
                dictionary["InstanceId"] = request.InstanceId;
            }
            if (request.IsSetStorage())
            {
                Storage storage = request.Storage;
                if (!storage.IsSetS3())
                {
                    return dictionary;
                }
                S3Storage storage2 = storage.S3;
                if (storage2.IsSetBucket())
                {
                    dictionary["Storage" + "." + "S3" + "." + "Bucket"] = storage2.Bucket;
                }
                if (storage2.IsSetPrefix())
                {
                    dictionary["Storage" + "." + "S3" + "." + "Prefix"] = storage2.Prefix;
                }
                if (storage2.IsSetAWSAccessKeyId())
                {
                    dictionary["Storage" + "." + "S3" + "." + "AWSAccessKeyId"] = storage2.AWSAccessKeyId;
                }
                if (storage2.IsSetUploadPolicy())
                {
                    dictionary["Storage" + "." + "S3" + "." + "UploadPolicy"] = storage2.UploadPolicy;
                }
                if (storage2.IsSetUploadPolicySignature())
                {
                    dictionary["Storage" + "." + "S3" + "." + "UploadPolicySignature"] = storage2.UploadPolicySignature;
                }
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertCancelBundleTask(CancelBundleTaskRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "CancelBundleTask";
            if (request.IsSetBundleId())
            {
                dictionary["BundleId"] = request.BundleId;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertCancelSpotInstanceRequests(CancelSpotInstanceRequestsRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "CancelSpotInstanceRequests";
            List<string> spotInstanceRequestId = request.SpotInstanceRequestId;
            int num = 1;
            foreach (string str in spotInstanceRequestId)
            {
                dictionary["SpotInstanceRequestId" + "." + num] = str;
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertConfirmProductInstance(ConfirmProductInstanceRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "ConfirmProductInstance";
            if (request.IsSetProductCode())
            {
                dictionary["ProductCode"] = request.ProductCode;
            }
            if (request.IsSetInstanceId())
            {
                dictionary["InstanceId"] = request.InstanceId;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertCreateCustomerGateway(CreateCustomerGatewayRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "CreateCustomerGateway";
            if (request.IsSetType())
            {
                dictionary["Type"] = request.Type;
            }
            if (request.IsSetIpAddress())
            {
                dictionary["IpAddress"] = request.IpAddress;
            }
            if (request.IsSetBgpAsn())
            {
                dictionary["BgpAsn"] = request.BgpAsn.ToString();
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertCreateDhcpOptions(CreateDhcpOptionsRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "CreateDhcpOptions";
            List<DhcpConfiguration> dhcpConfiguration = request.DhcpConfiguration;
            int num = 1;
            foreach (DhcpConfiguration configuration in dhcpConfiguration)
            {
                if (configuration.IsSetKey())
                {
                    dictionary[string.Concat(new object[] { "DhcpConfiguration", ".", num, ".", "Key" })] = configuration.Key;
                }
                List<string> list2 = configuration.Value;
                int num2 = 1;
                foreach (string str in list2)
                {
                    dictionary[string.Concat(new object[] { "DhcpConfiguration", ".", num, ".", "Value", ".", num2 })] = str;
                    num2++;
                }
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertCreateImage(CreateImageRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "CreateImage";
            if (request.IsSetInstanceId())
            {
                dictionary["InstanceId"] = request.InstanceId;
            }
            if (request.IsSetName())
            {
                dictionary["Name"] = request.Name;
            }
            if (request.IsSetDescription())
            {
                dictionary["Description"] = request.Description;
            }
            if (request.IsSetNoReboot())
            {
                dictionary["NoReboot"] = request.NoReboot.ToString().ToLower();
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertCreateKeyPair(CreateKeyPairRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "CreateKeyPair";
            if (request.IsSetKeyName())
            {
                dictionary["KeyName"] = request.KeyName;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertCreateSecurityGroup(CreateSecurityGroupRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "CreateSecurityGroup";
            if (request.IsSetGroupName())
            {
                dictionary["GroupName"] = request.GroupName;
            }
            if (request.IsSetGroupDescription())
            {
                dictionary["GroupDescription"] = request.GroupDescription;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertCreateSnapshot(CreateSnapshotRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "CreateSnapshot";
            if (request.IsSetVolumeId())
            {
                dictionary["VolumeId"] = request.VolumeId;
            }
            if (request.IsSetDescription())
            {
                dictionary["Description"] = request.Description;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertCreateSpotDatafeedSubscription(CreateSpotDatafeedSubscriptionRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "CreateSpotDatafeedSubscription";
            if (request.IsSetBucket())
            {
                dictionary["Bucket"] = request.Bucket;
            }
            if (request.IsSetPrefix())
            {
                dictionary["Prefix"] = request.Prefix;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertCreateSubnet(CreateSubnetRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "CreateSubnet";
            if (request.IsSetVpcId())
            {
                dictionary["VpcId"] = request.VpcId;
            }
            if (request.IsSetCidrBlock())
            {
                dictionary["CidrBlock"] = request.CidrBlock;
            }
            if (request.IsSetAvailabilityZone())
            {
                dictionary["AvailabilityZone"] = request.AvailabilityZone;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertCreateVolume(CreateVolumeRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "CreateVolume";
            if (request.IsSetSize())
            {
                dictionary["Size"] = request.Size;
            }
            if (request.IsSetSnapshotId())
            {
                dictionary["SnapshotId"] = request.SnapshotId;
            }
            if (request.IsSetAvailabilityZone())
            {
                dictionary["AvailabilityZone"] = request.AvailabilityZone;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertCreateVpc(CreateVpcRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "CreateVpc";
            if (request.IsSetCidrBlock())
            {
                dictionary["CidrBlock"] = request.CidrBlock;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertCreateVpnConnection(CreateVpnConnectionRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "CreateVpnConnection";
            if (request.IsSetType())
            {
                dictionary["Type"] = request.Type;
            }
            if (request.IsSetCustomerGatewayId())
            {
                dictionary["CustomerGatewayId"] = request.CustomerGatewayId;
            }
            if (request.IsSetVpnGatewayId())
            {
                dictionary["VpnGatewayId"] = request.VpnGatewayId;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertCreateVpnGateway(CreateVpnGatewayRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "CreateVpnGateway";
            if (request.IsSetType())
            {
                dictionary["Type"] = request.Type;
            }
            if (request.IsSetAvailabilityZone())
            {
                dictionary["AvailabilityZone"] = request.AvailabilityZone;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDeleteCustomerGateway(DeleteCustomerGatewayRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DeleteCustomerGateway";
            if (request.IsSetCustomerGatewayId())
            {
                dictionary["CustomerGatewayId"] = request.CustomerGatewayId;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDeleteDhcpOptions(DeleteDhcpOptionsRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DeleteDhcpOptions";
            if (request.IsSetDhcpOptionsId())
            {
                dictionary["DhcpOptionsId"] = request.DhcpOptionsId;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDeleteKeyPair(DeleteKeyPairRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DeleteKeyPair";
            if (request.IsSetKeyName())
            {
                dictionary["KeyName"] = request.KeyName;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDeleteSecurityGroup(DeleteSecurityGroupRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DeleteSecurityGroup";
            if (request.IsSetGroupName())
            {
                dictionary["GroupName"] = request.GroupName;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDeleteSnapshot(DeleteSnapshotRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DeleteSnapshot";
            if (request.IsSetSnapshotId())
            {
                dictionary["SnapshotId"] = request.SnapshotId;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDeleteSpotDatafeedSubscription(DeleteSpotDatafeedSubscriptionRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DeleteSpotDatafeedSubscription";
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDeleteSubnet(DeleteSubnetRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DeleteSubnet";
            if (request.IsSetSubnetId())
            {
                dictionary["SubnetId"] = request.SubnetId;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDeleteVolume(DeleteVolumeRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DeleteVolume";
            if (request.IsSetVolumeId())
            {
                dictionary["VolumeId"] = request.VolumeId;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDeleteVpc(DeleteVpcRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DeleteVpc";
            if (request.IsSetVpcId())
            {
                dictionary["VpcId"] = request.VpcId;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDeleteVpnConnection(DeleteVpnConnectionRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DeleteVpnConnection";
            if (request.IsSetVpnConnectionId())
            {
                dictionary["VpnConnectionId"] = request.VpnConnectionId;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDeleteVpnGateway(DeleteVpnGatewayRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DeleteVpnGateway";
            if (request.IsSetVpnGatewayId())
            {
                dictionary["VpnGatewayId"] = request.VpnGatewayId;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDeregisterImage(DeregisterImageRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DeregisterImage";
            if (request.IsSetImageId())
            {
                dictionary["ImageId"] = request.ImageId;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeAddresses(DescribeAddressesRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeAddresses";
            List<string> publicIp = request.PublicIp;
            int num = 1;
            foreach (string str in publicIp)
            {
                dictionary["PublicIp" + "." + num] = str;
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeAvailabilityZones(DescribeAvailabilityZonesRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeAvailabilityZones";
            List<string> zoneName = request.ZoneName;
            int num = 1;
            foreach (string str in zoneName)
            {
                dictionary["ZoneName" + "." + num] = str;
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeBundleTasks(DescribeBundleTasksRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeBundleTasks";
            List<string> bundleId = request.BundleId;
            int num = 1;
            foreach (string str in bundleId)
            {
                dictionary["BundleId" + "." + num] = str;
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeCustomerGateways(DescribeCustomerGatewaysRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeCustomerGateways";
            List<string> customerGatewayId = request.CustomerGatewayId;
            int num = 1;
            foreach (string str in customerGatewayId)
            {
                dictionary["CustomerGatewayId" + "." + num] = str;
                num++;
            }
            List<Amazon.EC2.Model.Filter> list2 = request.Filter;
            int num2 = 1;
            foreach (Amazon.EC2.Model.Filter filter in list2)
            {
                if (filter.IsSetName())
                {
                    dictionary[string.Concat(new object[] { "Filter", ".", num2, ".", "Name" })] = filter.Name;
                }
                List<string> list3 = filter.Value;
                int num3 = 1;
                foreach (string str2 in list3)
                {
                    dictionary[string.Concat(new object[] { "Filter", ".", num2, ".", "Value", ".", num3 })] = str2;
                    num3++;
                }
                num2++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeDhcpOptions(DescribeDhcpOptionsRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeDhcpOptions";
            List<string> dhcpOptionsId = request.DhcpOptionsId;
            int num = 1;
            foreach (string str in dhcpOptionsId)
            {
                dictionary["DhcpOptionsId" + "." + num] = str;
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeImageAttribute(DescribeImageAttributeRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeImageAttribute";
            if (request.IsSetImageId())
            {
                dictionary["ImageId"] = request.ImageId;
            }
            if (request.IsSetAttribute())
            {
                dictionary["Attribute"] = request.Attribute;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeImages(DescribeImagesRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeImages";
            List<string> imageId = request.ImageId;
            int num = 1;
            foreach (string str in imageId)
            {
                dictionary["ImageId" + "." + num] = str;
                num++;
            }
            List<string> owner = request.Owner;
            int num2 = 1;
            foreach (string str2 in owner)
            {
                dictionary["Owner" + "." + num2] = str2;
                num2++;
            }
            List<string> executableBy = request.ExecutableBy;
            int num3 = 1;
            foreach (string str3 in executableBy)
            {
                dictionary["ExecutableBy" + "." + num3] = str3;
                num3++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeInstanceAttribute(DescribeInstanceAttributeRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeInstanceAttribute";
            if (request.IsSetInstanceId())
            {
                dictionary["InstanceId"] = request.InstanceId;
            }
            if (request.IsSetAttribute())
            {
                dictionary["Attribute"] = request.Attribute;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeInstances(DescribeInstancesRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeInstances";
            List<string> instanceId = request.InstanceId;
            int num = 1;
            foreach (string str in instanceId)
            {
                dictionary["InstanceId" + "." + num] = str;
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeKeyPairs(DescribeKeyPairsRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeKeyPairs";
            List<string> keyName = request.KeyName;
            int num = 1;
            foreach (string str in keyName)
            {
                dictionary["KeyName" + "." + num] = str;
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeRegions(DescribeRegionsRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeRegions";
            List<string> regionName = request.RegionName;
            int num = 1;
            foreach (string str in regionName)
            {
                dictionary["RegionName" + "." + num] = str;
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeReservedInstances(DescribeReservedInstancesRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeReservedInstances";
            List<string> reservedInstancesId = request.ReservedInstancesId;
            int num = 1;
            foreach (string str in reservedInstancesId)
            {
                dictionary["ReservedInstancesId" + "." + num] = str;
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeReservedInstancesOfferings(DescribeReservedInstancesOfferingsRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeReservedInstancesOfferings";
            List<string> reservedInstancesId = request.ReservedInstancesId;
            int num = 1;
            foreach (string str in reservedInstancesId)
            {
                dictionary["ReservedInstancesId" + "." + num] = str;
                num++;
            }
            if (request.IsSetInstanceType())
            {
                dictionary["InstanceType"] = request.InstanceType;
            }
            if (request.IsSetAvailabilityZone())
            {
                dictionary["AvailabilityZone"] = request.AvailabilityZone;
            }
            if (request.IsSetProductDescription())
            {
                dictionary["ProductDescription"] = request.ProductDescription;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeSecurityGroups(DescribeSecurityGroupsRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeSecurityGroups";
            List<string> groupName = request.GroupName;
            int num = 1;
            foreach (string str in groupName)
            {
                dictionary["GroupName" + "." + num] = str;
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeSnapshotAttribute(DescribeSnapshotAttributeRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeSnapshotAttribute";
            if (request.IsSetSnapshotId())
            {
                dictionary["SnapshotId"] = request.SnapshotId;
            }
            if (request.IsSetAttribute())
            {
                dictionary["Attribute"] = request.Attribute;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeSnapshots(DescribeSnapshotsRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeSnapshots";
            List<string> snapshotId = request.SnapshotId;
            int num = 1;
            foreach (string str in snapshotId)
            {
                dictionary["SnapshotId" + "." + num] = str;
                num++;
            }
            if (request.IsSetOwner())
            {
                dictionary["Owner"] = request.Owner;
            }
            if (request.IsSetRestorableBy())
            {
                dictionary["RestorableBy"] = request.RestorableBy;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeSpotDatafeedSubscription(DescribeSpotDatafeedSubscriptionRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeSpotDatafeedSubscription";
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeSpotInstanceRequests(DescribeSpotInstanceRequestsRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeSpotInstanceRequests";
            List<string> spotInstanceRequestId = request.SpotInstanceRequestId;
            int num = 1;
            foreach (string str in spotInstanceRequestId)
            {
                dictionary["SpotInstanceRequestId" + "." + num] = str;
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeSpotPriceHistory(DescribeSpotPriceHistoryRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeSpotPriceHistory";
            if (request.IsSetStartTime())
            {
                dictionary["StartTime"] = request.StartTime;
            }
            if (request.IsSetEndTime())
            {
                dictionary["EndTime"] = request.EndTime;
            }
            List<string> instanceType = request.InstanceType;
            int num = 1;
            foreach (string str in instanceType)
            {
                dictionary["InstanceType" + "." + num] = str;
                num++;
            }
            List<string> productDescription = request.ProductDescription;
            int num2 = 1;
            foreach (string str2 in productDescription)
            {
                dictionary["ProductDescription" + "." + num2] = str2;
                num2++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeSubnets(DescribeSubnetsRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeSubnets";
            List<string> subnetId = request.SubnetId;
            int num = 1;
            foreach (string str in subnetId)
            {
                dictionary["SubnetId" + "." + num] = str;
                num++;
            }
            List<Amazon.EC2.Model.Filter> list2 = request.Filter;
            int num2 = 1;
            foreach (Amazon.EC2.Model.Filter filter in list2)
            {
                if (filter.IsSetName())
                {
                    dictionary[string.Concat(new object[] { "Filter", ".", num2, ".", "Name" })] = filter.Name;
                }
                List<string> list3 = filter.Value;
                int num3 = 1;
                foreach (string str2 in list3)
                {
                    dictionary[string.Concat(new object[] { "Filter", ".", num2, ".", "Value", ".", num3 })] = str2;
                    num3++;
                }
                num2++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeVolumes(DescribeVolumesRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeVolumes";
            List<string> volumeId = request.VolumeId;
            int num = 1;
            foreach (string str in volumeId)
            {
                dictionary["VolumeId" + "." + num] = str;
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeVpcs(DescribeVpcsRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeVpcs";
            List<string> vpcId = request.VpcId;
            int num = 1;
            foreach (string str in vpcId)
            {
                dictionary["VpcId" + "." + num] = str;
                num++;
            }
            List<Amazon.EC2.Model.Filter> list2 = request.Filter;
            int num2 = 1;
            foreach (Amazon.EC2.Model.Filter filter in list2)
            {
                if (filter.IsSetName())
                {
                    dictionary[string.Concat(new object[] { "Filter", ".", num2, ".", "Name" })] = filter.Name;
                }
                List<string> list3 = filter.Value;
                int num3 = 1;
                foreach (string str2 in list3)
                {
                    dictionary[string.Concat(new object[] { "Filter", ".", num2, ".", "Value", ".", num3 })] = str2;
                    num3++;
                }
                num2++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeVpnConnections(DescribeVpnConnectionsRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeVpnConnections";
            List<string> vpnConnectionId = request.VpnConnectionId;
            int num = 1;
            foreach (string str in vpnConnectionId)
            {
                dictionary["VpnConnectionId" + "." + num] = str;
                num++;
            }
            List<Amazon.EC2.Model.Filter> list2 = request.Filter;
            int num2 = 1;
            foreach (Amazon.EC2.Model.Filter filter in list2)
            {
                if (filter.IsSetName())
                {
                    dictionary[string.Concat(new object[] { "Filter", ".", num2, ".", "Name" })] = filter.Name;
                }
                List<string> list3 = filter.Value;
                int num3 = 1;
                foreach (string str2 in list3)
                {
                    dictionary[string.Concat(new object[] { "Filter", ".", num2, ".", "Value", ".", num3 })] = str2;
                    num3++;
                }
                num2++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeVpnGateways(DescribeVpnGatewaysRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeVpnGateways";
            List<string> vpnGatewayId = request.VpnGatewayId;
            int num = 1;
            foreach (string str in vpnGatewayId)
            {
                dictionary["VpnGatewayId" + "." + num] = str;
                num++;
            }
            List<Amazon.EC2.Model.Filter> list2 = request.Filter;
            int num2 = 1;
            foreach (Amazon.EC2.Model.Filter filter in list2)
            {
                if (filter.IsSetName())
                {
                    dictionary[string.Concat(new object[] { "Filter", ".", num2, ".", "Name" })] = filter.Name;
                }
                List<string> list3 = filter.Value;
                int num3 = 1;
                foreach (string str2 in list3)
                {
                    dictionary[string.Concat(new object[] { "Filter", ".", num2, ".", "Value", ".", num3 })] = str2;
                    num3++;
                }
                num2++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDetachVolume(DetachVolumeRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DetachVolume";
            if (request.IsSetVolumeId())
            {
                dictionary["VolumeId"] = request.VolumeId;
            }
            if (request.IsSetInstanceId())
            {
                dictionary["InstanceId"] = request.InstanceId;
            }
            if (request.IsSetDevice())
            {
                dictionary["Device"] = request.Device;
            }
            if (request.IsSetForce())
            {
                dictionary["Force"] = request.Force.ToString().ToLower();
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDetachVpnGateway(DetachVpnGatewayRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DetachVpnGateway";
            if (request.IsSetVpnGatewayId())
            {
                dictionary["VpnGatewayId"] = request.VpnGatewayId;
            }
            if (request.IsSetVpcId())
            {
                dictionary["VpcId"] = request.VpcId;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDisassociateAddress(DisassociateAddressRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DisassociateAddress";
            if (request.IsSetPublicIp())
            {
                dictionary["PublicIp"] = request.PublicIp;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertGetConsoleOutput(GetConsoleOutputRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "GetConsoleOutput";
            if (request.IsSetInstanceId())
            {
                dictionary["InstanceId"] = request.InstanceId;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertGetPasswordData(GetPasswordDataRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "GetPasswordData";
            if (request.IsSetInstanceId())
            {
                dictionary["InstanceId"] = request.InstanceId;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertModifyImageAttribute(ModifyImageAttributeRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "ModifyImageAttribute";
            if (request.IsSetImageId())
            {
                dictionary["ImageId"] = request.ImageId;
            }
            if (request.IsSetAttribute())
            {
                dictionary["Attribute"] = request.Attribute;
            }
            if (request.IsSetOperationType())
            {
                dictionary["OperationType"] = request.OperationType;
            }
            List<string> userId = request.UserId;
            int num = 1;
            foreach (string str in userId)
            {
                dictionary["UserId" + "." + num] = str;
                num++;
            }
            List<string> userGroup = request.UserGroup;
            int num2 = 1;
            foreach (string str2 in userGroup)
            {
                dictionary["UserGroup" + "." + num2] = str2;
                num2++;
            }
            List<string> productCode = request.ProductCode;
            int num3 = 1;
            foreach (string str3 in productCode)
            {
                dictionary["ProductCode" + "." + num3] = str3;
                num3++;
            }
            if (request.IsSetDescription())
            {
                dictionary["Description"] = request.Description;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertModifyInstanceAttribute(ModifyInstanceAttributeRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "ModifyInstanceAttribute";
            if (request.IsSetInstanceId())
            {
                dictionary["InstanceId"] = request.InstanceId;
            }
            if (request.IsSetAttribute())
            {
                dictionary["Attribute"] = request.Attribute;
            }
            if (request.IsSetValue())
            {
                dictionary["Value"] = request.Value;
            }
            List<InstanceBlockDeviceMappingParameter> blockDeviceMapping = request.BlockDeviceMapping;
            int num = 1;
            foreach (InstanceBlockDeviceMappingParameter parameter in blockDeviceMapping)
            {
                if (parameter.IsSetDeviceName())
                {
                    dictionary[string.Concat(new object[] { "BlockDeviceMapping", ".", num, ".", "DeviceName" })] = parameter.DeviceName;
                }
                if (parameter.IsSetVirtualName())
                {
                    dictionary[string.Concat(new object[] { "BlockDeviceMapping", ".", num, ".", "VirtualName" })] = parameter.VirtualName;
                }
                if (parameter.IsSetEbs())
                {
                    InstanceEbsBlockDeviceParameter ebs = parameter.Ebs;
                    if (ebs.IsSetVolumeId())
                    {
                        dictionary[string.Concat(new object[] { "BlockDeviceMapping", ".", num, ".", "Ebs", ".", "VolumeId" })] = ebs.VolumeId;
                    }
                    if (ebs.IsSetDeleteOnTermination())
                    {
                        dictionary[string.Concat(new object[] { "BlockDeviceMapping", ".", num, ".", "Ebs", ".", "DeleteOnTermination" })] = ebs.DeleteOnTermination.ToString().ToLower();
                    }
                }
                if (parameter.IsSetNoDevice())
                {
                    dictionary[string.Concat(new object[] { "BlockDeviceMapping", ".", num, ".", "NoDevice" })] = parameter.NoDevice;
                }
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertModifySnapshotAttribute(ModifySnapshotAttributeRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "ModifySnapshotAttribute";
            if (request.IsSetSnapshotId())
            {
                dictionary["SnapshotId"] = request.SnapshotId;
            }
            if (request.IsSetAttribute())
            {
                dictionary["Attribute"] = request.Attribute;
            }
            if (request.IsSetOperationType())
            {
                dictionary["OperationType"] = request.OperationType;
            }
            List<string> userId = request.UserId;
            int num = 1;
            foreach (string str in userId)
            {
                dictionary["UserId" + "." + num] = str;
                num++;
            }
            List<string> userGroup = request.UserGroup;
            int num2 = 1;
            foreach (string str2 in userGroup)
            {
                dictionary["UserGroup" + "." + num2] = str2;
                num2++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertMonitorInstances(MonitorInstancesRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "MonitorInstances";
            List<string> instanceId = request.InstanceId;
            int num = 1;
            foreach (string str in instanceId)
            {
                dictionary["InstanceId" + "." + num] = str;
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertPurchaseReservedInstancesOffering(PurchaseReservedInstancesOfferingRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "PurchaseReservedInstancesOffering";
            if (request.IsSetReservedInstancesOfferingId())
            {
                dictionary["ReservedInstancesOfferingId"] = request.ReservedInstancesOfferingId;
            }
            if (request.IsSetInstanceCount())
            {
                dictionary["InstanceCount"] = request.InstanceCount;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertRebootInstances(RebootInstancesRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "RebootInstances";
            List<string> instanceId = request.InstanceId;
            int num = 1;
            foreach (string str in instanceId)
            {
                dictionary["InstanceId" + "." + num] = str;
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertRegisterImage(RegisterImageRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "RegisterImage";
            if (request.IsSetImageLocation())
            {
                dictionary["ImageLocation"] = request.ImageLocation;
            }
            if (request.IsSetName())
            {
                dictionary["Name"] = request.Name;
            }
            if (request.IsSetDescription())
            {
                dictionary["Description"] = request.Description;
            }
            if (request.IsSetArchitecture())
            {
                dictionary["Architecture"] = request.Architecture;
            }
            if (request.IsSetKernelId())
            {
                dictionary["KernelId"] = request.KernelId;
            }
            if (request.IsSetRamdiskId())
            {
                dictionary["RamdiskId"] = request.RamdiskId;
            }
            if (request.IsSetRootDeviceName())
            {
                dictionary["RootDeviceName"] = request.RootDeviceName;
            }
            List<BlockDeviceMapping> blockDeviceMapping = request.BlockDeviceMapping;
            int num = 1;
            foreach (BlockDeviceMapping mapping in blockDeviceMapping)
            {
                if (mapping.IsSetDeviceName())
                {
                    dictionary[string.Concat(new object[] { "BlockDeviceMapping", ".", num, ".", "DeviceName" })] = mapping.DeviceName;
                }
                if (mapping.IsSetVirtualName())
                {
                    dictionary[string.Concat(new object[] { "BlockDeviceMapping", ".", num, ".", "VirtualName" })] = mapping.VirtualName;
                }
                if (mapping.IsSetEbs())
                {
                    EbsBlockDevice ebs = mapping.Ebs;
                    if (ebs.IsSetSnapshotId())
                    {
                        dictionary[string.Concat(new object[] { "BlockDeviceMapping", ".", num, ".", "Ebs", ".", "SnapshotId" })] = ebs.SnapshotId;
                    }
                    if (ebs.IsSetVolumeSize())
                    {
                        dictionary[string.Concat(new object[] { "BlockDeviceMapping", ".", num, ".", "Ebs", ".", "VolumeSize" })] = ebs.VolumeSize.ToString();
                    }
                    if (ebs.IsSetDeleteOnTermination())
                    {
                        dictionary[string.Concat(new object[] { "BlockDeviceMapping", ".", num, ".", "Ebs", ".", "DeleteOnTermination" })] = ebs.DeleteOnTermination.ToString().ToLower();
                    }
                }
                if (mapping.IsSetNoDevice())
                {
                    dictionary[string.Concat(new object[] { "BlockDeviceMapping", ".", num, ".", "NoDevice" })] = mapping.NoDevice;
                }
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertReleaseAddress(ReleaseAddressRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "ReleaseAddress";
            if (request.IsSetPublicIp())
            {
                dictionary["PublicIp"] = request.PublicIp;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertRequestSpotInstances(RequestSpotInstancesRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "RequestSpotInstances";
            if (request.IsSetSpotPrice())
            {
                dictionary["SpotPrice"] = request.SpotPrice;
            }
            if (request.IsSetInstanceCount())
            {
                dictionary["InstanceCount"] = request.InstanceCount.ToString();
            }
            if (request.IsSetType())
            {
                dictionary["Type"] = request.Type;
            }
            if (request.IsSetValidFrom())
            {
                dictionary["ValidFrom"] = request.ValidFrom;
            }
            if (request.IsSetValidUntil())
            {
                dictionary["ValidUntil"] = request.ValidUntil;
            }
            if (request.IsSetLaunchGroup())
            {
                dictionary["LaunchGroup"] = request.LaunchGroup;
            }
            if (request.IsSetAvailabilityZoneGroup())
            {
                dictionary["AvailabilityZoneGroup"] = request.AvailabilityZoneGroup;
            }
            if (request.IsSetLaunchSpecification())
            {
                LaunchSpecification launchSpecification = request.LaunchSpecification;
                if (launchSpecification.IsSetImageId())
                {
                    dictionary["LaunchSpecification" + "." + "ImageId"] = launchSpecification.ImageId;
                }
                if (launchSpecification.IsSetKeyName())
                {
                    dictionary["LaunchSpecification" + "." + "KeyName"] = launchSpecification.KeyName;
                }
                List<string> securityGroup = launchSpecification.SecurityGroup;
                int num = 1;
                foreach (string str in securityGroup)
                {
                    dictionary[string.Concat(new object[] { "LaunchSpecification", ".", "SecurityGroup", ".", num })] = str;
                    num++;
                }
                if (launchSpecification.IsSetUserData())
                {
                    dictionary["LaunchSpecification" + "." + "UserData"] = launchSpecification.UserData;
                }
                if (launchSpecification.IsSetAddressingType())
                {
                    dictionary["LaunchSpecification" + "." + "AddressingType"] = launchSpecification.AddressingType;
                }
                if (launchSpecification.IsSetInstanceType())
                {
                    dictionary["LaunchSpecification" + "." + "InstanceType"] = launchSpecification.InstanceType;
                }
                if (launchSpecification.IsSetPlacement())
                {
                    Placement placement = launchSpecification.Placement;
                    if (placement.IsSetAvailabilityZone())
                    {
                        dictionary["LaunchSpecification" + "." + "Placement" + "." + "AvailabilityZone"] = placement.AvailabilityZone;
                    }
                }
                if (launchSpecification.IsSetKernelId())
                {
                    dictionary["LaunchSpecification" + "." + "KernelId"] = launchSpecification.KernelId;
                }
                if (launchSpecification.IsSetRamdiskId())
                {
                    dictionary["LaunchSpecification" + "." + "RamdiskId"] = launchSpecification.RamdiskId;
                }
                List<BlockDeviceMapping> blockDeviceMapping = launchSpecification.BlockDeviceMapping;
                int num2 = 1;
                foreach (BlockDeviceMapping mapping in blockDeviceMapping)
                {
                    if (mapping.IsSetDeviceName())
                    {
                        dictionary[string.Concat(new object[] { "LaunchSpecification", ".", "BlockDeviceMapping", ".", num2, ".", "DeviceName" })] = mapping.DeviceName;
                    }
                    if (mapping.IsSetVirtualName())
                    {
                        dictionary[string.Concat(new object[] { "LaunchSpecification", ".", "BlockDeviceMapping", ".", num2, ".", "VirtualName" })] = mapping.VirtualName;
                    }
                    if (mapping.IsSetEbs())
                    {
                        EbsBlockDevice ebs = mapping.Ebs;
                        if (ebs.IsSetSnapshotId())
                        {
                            dictionary[string.Concat(new object[] { "LaunchSpecification", ".", "BlockDeviceMapping", ".", num2, ".", "Ebs", ".", "SnapshotId" })] = ebs.SnapshotId;
                        }
                        if (ebs.IsSetVolumeSize())
                        {
                            dictionary[string.Concat(new object[] { "LaunchSpecification", ".", "BlockDeviceMapping", ".", num2, ".", "Ebs", ".", "VolumeSize" })] = ebs.VolumeSize.ToString();
                        }
                        if (ebs.IsSetDeleteOnTermination())
                        {
                            dictionary[string.Concat(new object[] { "LaunchSpecification", ".", "BlockDeviceMapping", ".", num2, ".", "Ebs", ".", "DeleteOnTermination" })] = ebs.DeleteOnTermination.ToString().ToLower();
                        }
                    }
                    if (mapping.IsSetNoDevice())
                    {
                        dictionary[string.Concat(new object[] { "LaunchSpecification", ".", "BlockDeviceMapping", ".", num2, ".", "NoDevice" })] = mapping.NoDevice;
                    }
                    num2++;
                }
                if (launchSpecification.IsSetMonitoring())
                {
                    MonitoringSpecification monitoring = launchSpecification.Monitoring;
                    if (monitoring.IsSetEnabled())
                    {
                        dictionary["LaunchSpecification" + "." + "Monitoring" + "." + "Enabled"] = monitoring.Enabled.ToString().ToLower();
                    }
                }
                if (launchSpecification.IsSetSubnetId())
                {
                    dictionary["LaunchSpecification" + "." + "SubnetId"] = launchSpecification.SubnetId;
                }
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertResetImageAttribute(ResetImageAttributeRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "ResetImageAttribute";
            if (request.IsSetImageId())
            {
                dictionary["ImageId"] = request.ImageId;
            }
            if (request.IsSetAttribute())
            {
                dictionary["Attribute"] = request.Attribute;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertResetInstanceAttribute(ResetInstanceAttributeRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "ResetInstanceAttribute";
            if (request.IsSetInstanceId())
            {
                dictionary["InstanceId"] = request.InstanceId;
            }
            if (request.IsSetAttribute())
            {
                dictionary["Attribute"] = request.Attribute;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertResetSnapshotAttribute(ResetSnapshotAttributeRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "ResetSnapshotAttribute";
            if (request.IsSetSnapshotId())
            {
                dictionary["SnapshotId"] = request.SnapshotId;
            }
            if (request.IsSetAttribute())
            {
                dictionary["Attribute"] = request.Attribute;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertRevokeSecurityGroupIngress(RevokeSecurityGroupIngressRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "RevokeSecurityGroupIngress";
            if (request.IsSetUserId())
            {
                dictionary["UserId"] = request.UserId;
            }
            if (request.IsSetGroupName())
            {
                dictionary["GroupName"] = request.GroupName;
            }
            if (request.IsSetSourceSecurityGroupName())
            {
                dictionary["SourceSecurityGroupName"] = request.SourceSecurityGroupName;
            }
            if (request.IsSetSourceSecurityGroupOwnerId())
            {
                dictionary["SourceSecurityGroupOwnerId"] = request.SourceSecurityGroupOwnerId;
            }
            if (request.IsSetIpProtocol())
            {
                dictionary["IpProtocol"] = request.IpProtocol;
            }
            if (request.IsSetFromPort())
            {
                dictionary["FromPort"] = request.FromPort.ToString();
            }
            if (request.IsSetToPort())
            {
                dictionary["ToPort"] = request.ToPort.ToString();
            }
            if (request.IsSetCidrIp())
            {
                dictionary["CidrIp"] = request.CidrIp;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertRunInstances(RunInstancesRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "RunInstances";
            if (request.IsSetImageId())
            {
                dictionary["ImageId"] = request.ImageId;
            }
            if (request.IsSetMinCount())
            {
                dictionary["MinCount"] = request.MinCount.ToString();
            }
            if (request.IsSetMaxCount())
            {
                dictionary["MaxCount"] = request.MaxCount.ToString();
            }
            if (request.IsSetKeyName())
            {
                dictionary["KeyName"] = request.KeyName;
            }
            List<string> securityGroup = request.SecurityGroup;
            int num = 1;
            foreach (string str in securityGroup)
            {
                dictionary["SecurityGroup" + "." + num] = str;
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
            if (request.IsSetPlacement())
            {
                Placement placement = request.Placement;
                if (placement.IsSetAvailabilityZone())
                {
                    dictionary["Placement" + "." + "AvailabilityZone"] = placement.AvailabilityZone;
                }
            }
            if (request.IsSetKernelId())
            {
                dictionary["KernelId"] = request.KernelId;
            }
            if (request.IsSetRamdiskId())
            {
                dictionary["RamdiskId"] = request.RamdiskId;
            }
            List<BlockDeviceMapping> blockDeviceMapping = request.BlockDeviceMapping;
            int num2 = 1;
            foreach (BlockDeviceMapping mapping in blockDeviceMapping)
            {
                if (mapping.IsSetDeviceName())
                {
                    dictionary[string.Concat(new object[] { "BlockDeviceMapping", ".", num2, ".", "DeviceName" })] = mapping.DeviceName;
                }
                if (mapping.IsSetVirtualName())
                {
                    dictionary[string.Concat(new object[] { "BlockDeviceMapping", ".", num2, ".", "VirtualName" })] = mapping.VirtualName;
                }
                if (mapping.IsSetEbs())
                {
                    EbsBlockDevice ebs = mapping.Ebs;
                    if (ebs.IsSetSnapshotId())
                    {
                        dictionary[string.Concat(new object[] { "BlockDeviceMapping", ".", num2, ".", "Ebs", ".", "SnapshotId" })] = ebs.SnapshotId;
                    }
                    if (ebs.IsSetVolumeSize())
                    {
                        dictionary[string.Concat(new object[] { "BlockDeviceMapping", ".", num2, ".", "Ebs", ".", "VolumeSize" })] = ebs.VolumeSize.ToString();
                    }
                    if (ebs.IsSetDeleteOnTermination())
                    {
                        dictionary[string.Concat(new object[] { "BlockDeviceMapping", ".", num2, ".", "Ebs", ".", "DeleteOnTermination" })] = ebs.DeleteOnTermination.ToString().ToLower();
                    }
                }
                if (mapping.IsSetNoDevice())
                {
                    dictionary[string.Concat(new object[] { "BlockDeviceMapping", ".", num2, ".", "NoDevice" })] = mapping.NoDevice;
                }
                num2++;
            }
            if (request.IsSetMonitoring())
            {
                MonitoringSpecification monitoring = request.Monitoring;
                if (monitoring.IsSetEnabled())
                {
                    dictionary["Monitoring" + "." + "Enabled"] = monitoring.Enabled.ToString().ToLower();
                }
            }
            if (request.IsSetSubnetId())
            {
                dictionary["SubnetId"] = request.SubnetId;
            }
            if (request.IsSetAdditionalInfo())
            {
                dictionary["AdditionalInfo"] = request.AdditionalInfo;
            }
            if (request.IsSetDisableApiTermination())
            {
                dictionary["DisableApiTermination"] = request.DisableApiTermination.ToString().ToLower();
            }
            if (request.IsSetInstanceInitiatedShutdownBehavior())
            {
                dictionary["InstanceInitiatedShutdownBehavior"] = request.InstanceInitiatedShutdownBehavior;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertStartInstances(StartInstancesRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "StartInstances";
            List<string> instanceId = request.InstanceId;
            int num = 1;
            foreach (string str in instanceId)
            {
                dictionary["InstanceId" + "." + num] = str;
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertStopInstances(StopInstancesRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "StopInstances";
            List<string> instanceId = request.InstanceId;
            int num = 1;
            foreach (string str in instanceId)
            {
                dictionary["InstanceId" + "." + num] = str;
                num++;
            }
            if (request.IsSetForce())
            {
                dictionary["Force"] = request.Force.ToString().ToLower();
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertTerminateInstances(TerminateInstancesRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "TerminateInstances";
            List<string> instanceId = request.InstanceId;
            int num = 1;
            foreach (string str in instanceId)
            {
                dictionary["InstanceId" + "." + num] = str;
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertUnmonitorInstances(UnmonitorInstancesRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "UnmonitorInstances";
            List<string> instanceId = request.InstanceId;
            int num = 1;
            foreach (string str in instanceId)
            {
                dictionary["InstanceId" + "." + num] = str;
                num++;
            }
            return dictionary;
        }

        public CreateCustomerGatewayResponse CreateCustomerGateway(CreateCustomerGatewayRequest request)
        {
            return this.Invoke<CreateCustomerGatewayResponse>(ConvertCreateCustomerGateway(request));
        }

        public CreateDhcpOptionsResponse CreateDhcpOptions(CreateDhcpOptionsRequest request)
        {
            return this.Invoke<CreateDhcpOptionsResponse>(ConvertCreateDhcpOptions(request));
        }

        public CreateImageResponse CreateImage(CreateImageRequest request)
        {
            return this.Invoke<CreateImageResponse>(ConvertCreateImage(request));
        }

        public CreateKeyPairResponse CreateKeyPair(CreateKeyPairRequest request)
        {
            return this.Invoke<CreateKeyPairResponse>(ConvertCreateKeyPair(request));
        }

        public CreateSecurityGroupResponse CreateSecurityGroup(CreateSecurityGroupRequest request)
        {
            return this.Invoke<CreateSecurityGroupResponse>(ConvertCreateSecurityGroup(request));
        }

        public CreateSnapshotResponse CreateSnapshot(CreateSnapshotRequest request)
        {
            return this.Invoke<CreateSnapshotResponse>(ConvertCreateSnapshot(request));
        }

        public CreateSpotDatafeedSubscriptionResponse CreateSpotDatafeedSubscription(CreateSpotDatafeedSubscriptionRequest request)
        {
            return this.Invoke<CreateSpotDatafeedSubscriptionResponse>(ConvertCreateSpotDatafeedSubscription(request));
        }

        public CreateSubnetResponse CreateSubnet(CreateSubnetRequest request)
        {
            return this.Invoke<CreateSubnetResponse>(ConvertCreateSubnet(request));
        }

        public CreateVolumeResponse CreateVolume(CreateVolumeRequest request)
        {
            return this.Invoke<CreateVolumeResponse>(ConvertCreateVolume(request));
        }

        public CreateVpcResponse CreateVpc(CreateVpcRequest request)
        {
            return this.Invoke<CreateVpcResponse>(ConvertCreateVpc(request));
        }

        public CreateVpnConnectionResponse CreateVpnConnection(CreateVpnConnectionRequest request)
        {
            return this.Invoke<CreateVpnConnectionResponse>(ConvertCreateVpnConnection(request));
        }

        public CreateVpnGatewayResponse CreateVpnGateway(CreateVpnGatewayRequest request)
        {
            return this.Invoke<CreateVpnGatewayResponse>(ConvertCreateVpnGateway(request));
        }

        public DeleteCustomerGatewayResponse DeleteCustomerGateway(DeleteCustomerGatewayRequest request)
        {
            return this.Invoke<DeleteCustomerGatewayResponse>(ConvertDeleteCustomerGateway(request));
        }

        public DeleteDhcpOptionsResponse DeleteDhcpOptions(DeleteDhcpOptionsRequest request)
        {
            return this.Invoke<DeleteDhcpOptionsResponse>(ConvertDeleteDhcpOptions(request));
        }

        public DeleteKeyPairResponse DeleteKeyPair(DeleteKeyPairRequest request)
        {
            return this.Invoke<DeleteKeyPairResponse>(ConvertDeleteKeyPair(request));
        }

        public DeleteSecurityGroupResponse DeleteSecurityGroup(DeleteSecurityGroupRequest request)
        {
            return this.Invoke<DeleteSecurityGroupResponse>(ConvertDeleteSecurityGroup(request));
        }

        public DeleteSnapshotResponse DeleteSnapshot(DeleteSnapshotRequest request)
        {
            return this.Invoke<DeleteSnapshotResponse>(ConvertDeleteSnapshot(request));
        }

        public DeleteSpotDatafeedSubscriptionResponse DeleteSpotDatafeedSubscription(DeleteSpotDatafeedSubscriptionRequest request)
        {
            return this.Invoke<DeleteSpotDatafeedSubscriptionResponse>(ConvertDeleteSpotDatafeedSubscription(request));
        }

        public DeleteSubnetResponse DeleteSubnet(DeleteSubnetRequest request)
        {
            return this.Invoke<DeleteSubnetResponse>(ConvertDeleteSubnet(request));
        }

        public DeleteVolumeResponse DeleteVolume(DeleteVolumeRequest request)
        {
            return this.Invoke<DeleteVolumeResponse>(ConvertDeleteVolume(request));
        }

        public DeleteVpcResponse DeleteVpc(DeleteVpcRequest request)
        {
            return this.Invoke<DeleteVpcResponse>(ConvertDeleteVpc(request));
        }

        public DeleteVpnConnectionResponse DeleteVpnConnection(DeleteVpnConnectionRequest request)
        {
            return this.Invoke<DeleteVpnConnectionResponse>(ConvertDeleteVpnConnection(request));
        }

        public DeleteVpnGatewayResponse DeleteVpnGateway(DeleteVpnGatewayRequest request)
        {
            return this.Invoke<DeleteVpnGatewayResponse>(ConvertDeleteVpnGateway(request));
        }

        public DeregisterImageResponse DeregisterImage(DeregisterImageRequest request)
        {
            return this.Invoke<DeregisterImageResponse>(ConvertDeregisterImage(request));
        }

        public DescribeAddressesResponse DescribeAddresses(DescribeAddressesRequest request)
        {
            return this.Invoke<DescribeAddressesResponse>(ConvertDescribeAddresses(request));
        }

        public DescribeAvailabilityZonesResponse DescribeAvailabilityZones(DescribeAvailabilityZonesRequest request)
        {
            return this.Invoke<DescribeAvailabilityZonesResponse>(ConvertDescribeAvailabilityZones(request));
        }

        public DescribeBundleTasksResponse DescribeBundleTasks(DescribeBundleTasksRequest request)
        {
            return this.Invoke<DescribeBundleTasksResponse>(ConvertDescribeBundleTasks(request));
        }

        public DescribeCustomerGatewaysResponse DescribeCustomerGateways(DescribeCustomerGatewaysRequest request)
        {
            return this.Invoke<DescribeCustomerGatewaysResponse>(ConvertDescribeCustomerGateways(request));
        }

        public DescribeDhcpOptionsResponse DescribeDhcpOptions(DescribeDhcpOptionsRequest request)
        {
            return this.Invoke<DescribeDhcpOptionsResponse>(ConvertDescribeDhcpOptions(request));
        }

        public DescribeImageAttributeResponse DescribeImageAttribute(DescribeImageAttributeRequest request)
        {
            return this.Invoke<DescribeImageAttributeResponse>(ConvertDescribeImageAttribute(request));
        }

        public DescribeImagesResponse DescribeImages(DescribeImagesRequest request)
        {
            return this.Invoke<DescribeImagesResponse>(ConvertDescribeImages(request));
        }

        public DescribeInstanceAttributeResponse DescribeInstanceAttribute(DescribeInstanceAttributeRequest request)
        {
            return this.Invoke<DescribeInstanceAttributeResponse>(ConvertDescribeInstanceAttribute(request));
        }

        public DescribeInstancesResponse DescribeInstances(DescribeInstancesRequest request)
        {
            return this.Invoke<DescribeInstancesResponse>(ConvertDescribeInstances(request));
        }

        public DescribeKeyPairsResponse DescribeKeyPairs(DescribeKeyPairsRequest request)
        {
            return this.Invoke<DescribeKeyPairsResponse>(ConvertDescribeKeyPairs(request));
        }

        public DescribeRegionsResponse DescribeRegions(DescribeRegionsRequest request)
        {
            return this.Invoke<DescribeRegionsResponse>(ConvertDescribeRegions(request));
        }

        public DescribeReservedInstancesResponse DescribeReservedInstances(DescribeReservedInstancesRequest request)
        {
            return this.Invoke<DescribeReservedInstancesResponse>(ConvertDescribeReservedInstances(request));
        }

        public DescribeReservedInstancesOfferingsResponse DescribeReservedInstancesOfferings(DescribeReservedInstancesOfferingsRequest request)
        {
            return this.Invoke<DescribeReservedInstancesOfferingsResponse>(ConvertDescribeReservedInstancesOfferings(request));
        }

        public DescribeSecurityGroupsResponse DescribeSecurityGroups(DescribeSecurityGroupsRequest request)
        {
            return this.Invoke<DescribeSecurityGroupsResponse>(ConvertDescribeSecurityGroups(request));
        }

        public DescribeSnapshotAttributeResponse DescribeSnapshotAttribute(DescribeSnapshotAttributeRequest request)
        {
            return this.Invoke<DescribeSnapshotAttributeResponse>(ConvertDescribeSnapshotAttribute(request));
        }

        public DescribeSnapshotsResponse DescribeSnapshots(DescribeSnapshotsRequest request)
        {
            return this.Invoke<DescribeSnapshotsResponse>(ConvertDescribeSnapshots(request));
        }

        public DescribeSpotDatafeedSubscriptionResponse DescribeSpotDatafeedSubscription(DescribeSpotDatafeedSubscriptionRequest request)
        {
            return this.Invoke<DescribeSpotDatafeedSubscriptionResponse>(ConvertDescribeSpotDatafeedSubscription(request));
        }

        public DescribeSpotInstanceRequestsResponse DescribeSpotInstanceRequests(DescribeSpotInstanceRequestsRequest request)
        {
            return this.Invoke<DescribeSpotInstanceRequestsResponse>(ConvertDescribeSpotInstanceRequests(request));
        }

        public DescribeSpotPriceHistoryResponse DescribeSpotPriceHistory(DescribeSpotPriceHistoryRequest request)
        {
            return this.Invoke<DescribeSpotPriceHistoryResponse>(ConvertDescribeSpotPriceHistory(request));
        }

        public DescribeSubnetsResponse DescribeSubnets(DescribeSubnetsRequest request)
        {
            return this.Invoke<DescribeSubnetsResponse>(ConvertDescribeSubnets(request));
        }

        public DescribeVolumesResponse DescribeVolumes(DescribeVolumesRequest request)
        {
            return this.Invoke<DescribeVolumesResponse>(ConvertDescribeVolumes(request));
        }

        public DescribeVpcsResponse DescribeVpcs(DescribeVpcsRequest request)
        {
            return this.Invoke<DescribeVpcsResponse>(ConvertDescribeVpcs(request));
        }

        public DescribeVpnConnectionsResponse DescribeVpnConnections(DescribeVpnConnectionsRequest request)
        {
            return this.Invoke<DescribeVpnConnectionsResponse>(ConvertDescribeVpnConnections(request));
        }

        public DescribeVpnGatewaysResponse DescribeVpnGateways(DescribeVpnGatewaysRequest request)
        {
            return this.Invoke<DescribeVpnGatewaysResponse>(ConvertDescribeVpnGateways(request));
        }

        public DetachVolumeResponse DetachVolume(DetachVolumeRequest request)
        {
            return this.Invoke<DetachVolumeResponse>(ConvertDetachVolume(request));
        }

        public DetachVpnGatewayResponse DetachVpnGateway(DetachVpnGatewayRequest request)
        {
            return this.Invoke<DetachVpnGatewayResponse>(ConvertDetachVpnGateway(request));
        }

        public DisassociateAddressResponse DisassociateAddress(DisassociateAddressRequest request)
        {
            return this.Invoke<DisassociateAddressResponse>(ConvertDisassociateAddress(request));
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

        ~AmazonEC2Client()
        {
            this.Dispose(false);
        }

        public GetConsoleOutputResponse GetConsoleOutput(GetConsoleOutputRequest request)
        {
            return this.Invoke<GetConsoleOutputResponse>(ConvertGetConsoleOutput(request));
        }

        public GetPasswordDataResponse GetPasswordData(GetPasswordDataRequest request)
        {
            return this.Invoke<GetPasswordDataResponse>(ConvertGetPasswordData(request));
        }

        private T Invoke<T>(IDictionary<string, string> parameters)
        {
            string action = parameters["Action"];
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
                    if (responseBody.Trim().EndsWith(action + "Response>"))
                    {
                        responseBody = Transform(responseBody, action, base.GetType());
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
                    using (HttpWebResponse response2 = (HttpWebResponse) exception.Response)
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
                                throw new AmazonEC2Exception(error.Message, status, error.Code, error.Type, response3.RequestId, response3.ToXML());
                            }
                        }
                        catch (Exception exception2)
                        {
                            if (exception2 is AmazonEC2Exception)
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

        public ModifyImageAttributeResponse ModifyImageAttribute(ModifyImageAttributeRequest request)
        {
            return this.Invoke<ModifyImageAttributeResponse>(ConvertModifyImageAttribute(request));
        }

        public ModifyInstanceAttributeResponse ModifyInstanceAttribute(ModifyInstanceAttributeRequest request)
        {
            return this.Invoke<ModifyInstanceAttributeResponse>(ConvertModifyInstanceAttribute(request));
        }

        public ModifySnapshotAttributeResponse ModifySnapshotAttribute(ModifySnapshotAttributeRequest request)
        {
            return this.Invoke<ModifySnapshotAttributeResponse>(ConvertModifySnapshotAttribute(request));
        }

        public MonitorInstancesResponse MonitorInstances(MonitorInstancesRequest request)
        {
            return this.Invoke<MonitorInstancesResponse>(ConvertMonitorInstances(request));
        }

        private static void PauseOnRetry(int retries, int maxRetries, HttpStatusCode status)
        {
            if (retries > maxRetries)
            {
                throw new AmazonEC2Exception("Maximum number of retry attempts reached : " + (retries - 1), status);
            }
            int millisecondsTimeout = ((int) Math.Pow(4.0, (double) retries)) * 100;
            Thread.Sleep(millisecondsTimeout);
        }

        public PurchaseReservedInstancesOfferingResponse PurchaseReservedInstancesOffering(PurchaseReservedInstancesOfferingRequest request)
        {
            return this.Invoke<PurchaseReservedInstancesOfferingResponse>(ConvertPurchaseReservedInstancesOffering(request));
        }

        public RebootInstancesResponse RebootInstances(RebootInstancesRequest request)
        {
            return this.Invoke<RebootInstancesResponse>(ConvertRebootInstances(request));
        }

        public RegisterImageResponse RegisterImage(RegisterImageRequest request)
        {
            return this.Invoke<RegisterImageResponse>(ConvertRegisterImage(request));
        }

        public ReleaseAddressResponse ReleaseAddress(ReleaseAddressRequest request)
        {
            return this.Invoke<ReleaseAddressResponse>(ConvertReleaseAddress(request));
        }

        private static AmazonEC2Exception ReportAnyErrors(string responseBody, HttpStatusCode status)
        {
            if ((responseBody != null) && responseBody.StartsWith("<"))
            {
                Match match = Regex.Match(responseBody, "<RequestId>(.*)</RequestId>.*<Error><Code>(.*)</Code><Message>(.*)</Message></Error>.*(<Error>)?", RegexOptions.Multiline);
                Match match2 = Regex.Match(responseBody, "<Error><Code>(.*)</Code><Message>(.*)</Message></Error>.*(<Error>)?.*<RequestID>(.*)</RequestID>", RegexOptions.Multiline);
                if (match.Success)
                {
                    string requestId = match.Groups[1].Value;
                    return new AmazonEC2Exception(match.Groups[3].Value, status, match.Groups[2].Value, "Unknown", requestId, responseBody);
                }
                if (match2.Success)
                {
                    string errorCode = match2.Groups[1].Value;
                    string message = match2.Groups[2].Value;
                    return new AmazonEC2Exception(message, status, errorCode, "Unknown", match2.Groups[4].Value, responseBody);
                }
                return new AmazonEC2Exception("Internal Error", status);
            }
            return new AmazonEC2Exception("Internal Error", status);
        }

        public RequestSpotInstancesResponse RequestSpotInstances(RequestSpotInstancesRequest request)
        {
            return this.Invoke<RequestSpotInstancesResponse>(ConvertRequestSpotInstances(request));
        }

        public ResetImageAttributeResponse ResetImageAttribute(ResetImageAttributeRequest request)
        {
            return this.Invoke<ResetImageAttributeResponse>(ConvertResetImageAttribute(request));
        }

        public ResetInstanceAttributeResponse ResetInstanceAttribute(ResetInstanceAttributeRequest request)
        {
            return this.Invoke<ResetInstanceAttributeResponse>(ConvertResetInstanceAttribute(request));
        }

        public ResetSnapshotAttributeResponse ResetSnapshotAttribute(ResetSnapshotAttributeRequest request)
        {
            return this.Invoke<ResetSnapshotAttributeResponse>(ConvertResetSnapshotAttribute(request));
        }

        public RevokeSecurityGroupIngressResponse RevokeSecurityGroupIngress(RevokeSecurityGroupIngressRequest request)
        {
            return this.Invoke<RevokeSecurityGroupIngressResponse>(ConvertRevokeSecurityGroupIngress(request));
        }

        public RunInstancesResponse RunInstances(RunInstancesRequest request)
        {
            return this.Invoke<RunInstancesResponse>(ConvertRunInstances(request));
        }

        public StartInstancesResponse StartInstances(StartInstancesRequest request)
        {
            return this.Invoke<StartInstancesResponse>(ConvertStartInstances(request));
        }

        public StopInstancesResponse StopInstances(StopInstancesRequest request)
        {
            return this.Invoke<StopInstancesResponse>(ConvertStopInstances(request));
        }

        public TerminateInstancesResponse TerminateInstances(TerminateInstancesRequest request)
        {
            return this.Invoke<TerminateInstancesResponse>(ConvertTerminateInstances(request));
        }

        private static string Transform(string responseBody, string action, Type t)
        {
            string str4;
            XslCompiledTransform transform = new XslCompiledTransform();
            char[] separator = new char[] { ',' };
            Assembly assembly = Assembly.GetAssembly(t);
            string str = assembly.FullName.Split(separator)[0];
            string str2 = t.Namespace;
            string name = str + "." + str2 + ".Model." + action + "Response.xslt";
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

        public UnmonitorInstancesResponse UnmonitorInstances(UnmonitorInstancesRequest request)
        {
            return this.Invoke<UnmonitorInstancesResponse>(ConvertUnmonitorInstances(request));
        }
    }
}

