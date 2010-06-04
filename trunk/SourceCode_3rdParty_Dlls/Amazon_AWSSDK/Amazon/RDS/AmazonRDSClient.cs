namespace Amazon.RDS
{
    using Amazon.RDS.Model;
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

    public class AmazonRDSClient : AmazonRDS, IDisposable
    {
        private string awsAccessKeyId;
        private SecureString awsSecretAccessKey;
        private string clearAwsSecretAccessKey;
        private AmazonRDSConfig config;
        private bool disposed;

        public AmazonRDSClient(string awsAccessKeyId, string awsSecretAccessKey) : this(awsAccessKeyId, awsSecretAccessKey, new AmazonRDSConfig())
        {
        }

        public AmazonRDSClient(string awsAccessKeyId, SecureString awsSecretAccessKey, AmazonRDSConfig config)
        {
            this.awsAccessKeyId = awsAccessKeyId;
            this.awsSecretAccessKey = awsSecretAccessKey;
            this.config = config;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.UseNagleAlgorithm = false;
        }

        public AmazonRDSClient(string awsAccessKeyId, string awsSecretAccessKey, AmazonRDSConfig config)
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
                throw new AmazonRDSException("The AWS Access Key ID cannot be NULL or a Zero length string");
            }
            parameters["AWSAccessKeyId"] = this.awsAccessKeyId;
            parameters["SignatureVersion"] = this.config.SignatureVersion;
            parameters["SignatureMethod"] = this.config.SignatureMethod;
            parameters["Timestamp"] = AWSSDKUtils.FormattedCurrentTimestampISO8601;
            parameters["Version"] = this.config.ServiceVersion;
            if (!this.config.SignatureVersion.Equals("2"))
            {
                throw new AmazonRDSException("Invalid Signature Version specified");
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

        public AuthorizeDBSecurityGroupIngressResponse AuthorizeDBSecurityGroupIngress(AuthorizeDBSecurityGroupIngressRequest request)
        {
            return this.Invoke<AuthorizeDBSecurityGroupIngressResponse>(ConvertAuthorizeDBSecurityGroupIngress(request));
        }

        private static HttpWebRequest ConfigureWebRequest(int contentLength, AmazonRDSConfig config)
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

        private static IDictionary<string, string> ConvertAuthorizeDBSecurityGroupIngress(AuthorizeDBSecurityGroupIngressRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "AuthorizeDBSecurityGroupIngress";
            if (request.IsSetDBSecurityGroupName())
            {
                dictionary["DBSecurityGroupName"] = request.DBSecurityGroupName;
            }
            if (request.IsSetCIDRIP())
            {
                dictionary["CIDRIP"] = request.CIDRIP;
            }
            if (request.IsSetEC2SecurityGroupName())
            {
                dictionary["EC2SecurityGroupName"] = request.EC2SecurityGroupName;
            }
            if (request.IsSetEC2SecurityGroupOwnerId())
            {
                dictionary["EC2SecurityGroupOwnerId"] = request.EC2SecurityGroupOwnerId;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertCreateDBInstance(CreateDBInstanceRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "CreateDBInstance";
            if (request.IsSetDBName())
            {
                dictionary["DBName"] = request.DBName;
            }
            if (request.IsSetDBInstanceIdentifier())
            {
                dictionary["DBInstanceIdentifier"] = request.DBInstanceIdentifier;
            }
            if (request.IsSetAllocatedStorage())
            {
                dictionary["AllocatedStorage"] = request.AllocatedStorage.ToString();
            }
            if (request.IsSetDBInstanceClass())
            {
                dictionary["DBInstanceClass"] = request.DBInstanceClass;
            }
            if (request.IsSetEngine())
            {
                dictionary["Engine"] = request.Engine;
            }
            if (request.IsSetMasterUsername())
            {
                dictionary["MasterUsername"] = request.MasterUsername;
            }
            if (request.IsSetMasterUserPassword())
            {
                dictionary["MasterUserPassword"] = request.MasterUserPassword;
            }
            List<string> dBSecurityGroups = request.DBSecurityGroups;
            int num = 1;
            foreach (string str in dBSecurityGroups)
            {
                dictionary["DBSecurityGroups" + ".member." + num] = str;
                num++;
            }
            if (request.IsSetAvailabilityZone())
            {
                dictionary["AvailabilityZone"] = request.AvailabilityZone;
            }
            if (request.IsSetPreferredMaintenanceWindow())
            {
                dictionary["PreferredMaintenanceWindow"] = request.PreferredMaintenanceWindow;
            }
            if (request.IsSetDBParameterGroupName())
            {
                dictionary["DBParameterGroupName"] = request.DBParameterGroupName;
            }
            if (request.IsSetBackupRetentionPeriod())
            {
                dictionary["BackupRetentionPeriod"] = request.BackupRetentionPeriod.ToString();
            }
            if (request.IsSetPreferredBackupWindow())
            {
                dictionary["PreferredBackupWindow"] = request.PreferredBackupWindow;
            }
            if (request.IsSetPort())
            {
                dictionary["Port"] = request.Port.ToString();
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertCreateDBParameterGroup(CreateDBParameterGroupRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "CreateDBParameterGroup";
            if (request.IsSetDBParameterGroupName())
            {
                dictionary["DBParameterGroupName"] = request.DBParameterGroupName;
            }
            if (request.IsSetEngine())
            {
                dictionary["Engine"] = request.Engine;
            }
            if (request.IsSetDescription())
            {
                dictionary["Description"] = request.Description;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertCreateDBSecurityGroup(CreateDBSecurityGroupRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "CreateDBSecurityGroup";
            if (request.IsSetDBSecurityGroupName())
            {
                dictionary["DBSecurityGroupName"] = request.DBSecurityGroupName;
            }
            if (request.IsSetDBSecurityGroupDescription())
            {
                dictionary["DBSecurityGroupDescription"] = request.DBSecurityGroupDescription;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertCreateDBSnapshot(CreateDBSnapshotRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "CreateDBSnapshot";
            if (request.IsSetDBSnapshotIdentifier())
            {
                dictionary["DBSnapshotIdentifier"] = request.DBSnapshotIdentifier;
            }
            if (request.IsSetDBInstanceIdentifier())
            {
                dictionary["DBInstanceIdentifier"] = request.DBInstanceIdentifier;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDeleteDBInstance(DeleteDBInstanceRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DeleteDBInstance";
            if (request.IsSetDBInstanceIdentifier())
            {
                dictionary["DBInstanceIdentifier"] = request.DBInstanceIdentifier;
            }
            if (request.IsSetSkipFinalSnapshot())
            {
                dictionary["SkipFinalSnapshot"] = request.SkipFinalSnapshot.ToString().ToLower();
            }
            if (request.IsSetFinalDBSnapshotIdentifier())
            {
                dictionary["FinalDBSnapshotIdentifier"] = request.FinalDBSnapshotIdentifier;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDeleteDBParameterGroup(DeleteDBParameterGroupRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DeleteDBParameterGroup";
            if (request.IsSetDBParameterGroupName())
            {
                dictionary["DBParameterGroupName"] = request.DBParameterGroupName;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDeleteDBSecurityGroup(DeleteDBSecurityGroupRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DeleteDBSecurityGroup";
            if (request.IsSetDBSecurityGroupName())
            {
                dictionary["DBSecurityGroupName"] = request.DBSecurityGroupName;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDeleteDBSnapshot(DeleteDBSnapshotRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DeleteDBSnapshot";
            if (request.IsSetDBSnapshotIdentifier())
            {
                dictionary["DBSnapshotIdentifier"] = request.DBSnapshotIdentifier;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeDBInstances(DescribeDBInstancesRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeDBInstances";
            if (request.IsSetDBInstanceIdentifier())
            {
                dictionary["DBInstanceIdentifier"] = request.DBInstanceIdentifier;
            }
            if (request.IsSetMaxRecords())
            {
                dictionary["MaxRecords"] = request.MaxRecords.ToString();
            }
            if (request.IsSetMarker())
            {
                dictionary["Marker"] = request.Marker;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeDBParameterGroups(DescribeDBParameterGroupsRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeDBParameterGroups";
            if (request.IsSetDBParameterGroupName())
            {
                dictionary["DBParameterGroupName"] = request.DBParameterGroupName;
            }
            if (request.IsSetMaxRecords())
            {
                dictionary["MaxRecords"] = request.MaxRecords.ToString();
            }
            if (request.IsSetMarker())
            {
                dictionary["Marker"] = request.Marker;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeDBParameters(DescribeDBParametersRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeDBParameters";
            if (request.IsSetDBParameterGroupName())
            {
                dictionary["DBParameterGroupName"] = request.DBParameterGroupName;
            }
            if (request.IsSetSource())
            {
                dictionary["Source"] = request.Source;
            }
            if (request.IsSetMaxRecords())
            {
                dictionary["MaxRecords"] = request.MaxRecords.ToString();
            }
            if (request.IsSetMarker())
            {
                dictionary["Marker"] = request.Marker;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeDBSecurityGroups(DescribeDBSecurityGroupsRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeDBSecurityGroups";
            if (request.IsSetDBSecurityGroupName())
            {
                dictionary["DBSecurityGroupName"] = request.DBSecurityGroupName;
            }
            if (request.IsSetMaxRecords())
            {
                dictionary["MaxRecords"] = request.MaxRecords.ToString();
            }
            if (request.IsSetMarker())
            {
                dictionary["Marker"] = request.Marker;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeDBSnapshots(DescribeDBSnapshotsRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeDBSnapshots";
            if (request.IsSetDBInstanceIdentifier())
            {
                dictionary["DBInstanceIdentifier"] = request.DBInstanceIdentifier;
            }
            if (request.IsSetDBSnapshotIdentifier())
            {
                dictionary["DBSnapshotIdentifier"] = request.DBSnapshotIdentifier;
            }
            if (request.IsSetMaxRecords())
            {
                dictionary["MaxRecords"] = request.MaxRecords.ToString();
            }
            if (request.IsSetMarker())
            {
                dictionary["Marker"] = request.Marker;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeEngineDefaultParameters(DescribeEngineDefaultParametersRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeEngineDefaultParameters";
            if (request.IsSetEngine())
            {
                dictionary["Engine"] = request.Engine;
            }
            if (request.IsSetMaxRecords())
            {
                dictionary["MaxRecords"] = request.MaxRecords.ToString();
            }
            if (request.IsSetMarker())
            {
                dictionary["Marker"] = request.Marker;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertDescribeEvents(DescribeEventsRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "DescribeEvents";
            if (request.IsSetSourceIdentifier())
            {
                dictionary["SourceIdentifier"] = request.SourceIdentifier;
            }
            if (request.IsSetSourceType())
            {
                dictionary["SourceType"] = request.SourceType;
            }
            if (request.IsSetStartTime())
            {
                dictionary["StartTime"] = request.StartTime.ToString();
            }
            if (request.IsSetEndTime())
            {
                dictionary["EndTime"] = request.EndTime.ToString();
            }
            if (request.IsSetDuration())
            {
                dictionary["Duration"] = request.Duration.ToString();
            }
            if (request.IsSetMaxRecords())
            {
                dictionary["MaxRecords"] = request.MaxRecords.ToString();
            }
            if (request.IsSetMarker())
            {
                dictionary["Marker"] = request.Marker;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertModifyDBInstance(ModifyDBInstanceRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "ModifyDBInstance";
            if (request.IsSetDBInstanceIdentifier())
            {
                dictionary["DBInstanceIdentifier"] = request.DBInstanceIdentifier;
            }
            if (request.IsSetAllocatedStorage())
            {
                dictionary["AllocatedStorage"] = request.AllocatedStorage.ToString();
            }
            if (request.IsSetDBInstanceClass())
            {
                dictionary["DBInstanceClass"] = request.DBInstanceClass;
            }
            List<string> dBSecurityGroups = request.DBSecurityGroups;
            int num = 1;
            foreach (string str in dBSecurityGroups)
            {
                dictionary["DBSecurityGroups" + ".member." + num] = str;
                num++;
            }
            if (request.IsSetApplyImmediately())
            {
                dictionary["ApplyImmediately"] = request.ApplyImmediately.ToString().ToLower();
            }
            if (request.IsSetMasterUserPassword())
            {
                dictionary["MasterUserPassword"] = request.MasterUserPassword;
            }
            if (request.IsSetDBParameterGroupName())
            {
                dictionary["DBParameterGroupName"] = request.DBParameterGroupName;
            }
            if (request.IsSetBackupRetentionPeriod())
            {
                dictionary["BackupRetentionPeriod"] = request.BackupRetentionPeriod.ToString();
            }
            if (request.IsSetPreferredBackupWindow())
            {
                dictionary["PreferredBackupWindow"] = request.PreferredBackupWindow;
            }
            if (request.IsSetPreferredMaintenanceWindow())
            {
                dictionary["PreferredMaintenanceWindow"] = request.PreferredMaintenanceWindow;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertModifyDBParameterGroup(ModifyDBParameterGroupRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "ModifyDBParameterGroup";
            if (request.IsSetDBParameterGroupName())
            {
                dictionary["DBParameterGroupName"] = request.DBParameterGroupName;
            }
            List<Parameter> parameters = request.Parameters;
            int num = 1;
            foreach (Parameter parameter in parameters)
            {
                if (parameter.IsSetParameterName())
                {
                    dictionary[string.Concat(new object[] { "Parameters", ".member.", num, ".", "ParameterName" })] = parameter.ParameterName;
                }
                if (parameter.IsSetParameterValue())
                {
                    dictionary[string.Concat(new object[] { "Parameters", ".member.", num, ".", "ParameterValue" })] = parameter.ParameterValue;
                }
                if (parameter.IsSetDescription())
                {
                    dictionary[string.Concat(new object[] { "Parameters", ".member.", num, ".", "Description" })] = parameter.Description;
                }
                if (parameter.IsSetSource())
                {
                    dictionary[string.Concat(new object[] { "Parameters", ".member.", num, ".", "Source" })] = parameter.Source;
                }
                if (parameter.IsSetApplyType())
                {
                    dictionary[string.Concat(new object[] { "Parameters", ".member.", num, ".", "ApplyType" })] = parameter.ApplyType;
                }
                if (parameter.IsSetDataType())
                {
                    dictionary[string.Concat(new object[] { "Parameters", ".member.", num, ".", "DataType" })] = parameter.DataType;
                }
                if (parameter.IsSetAllowedValues())
                {
                    dictionary[string.Concat(new object[] { "Parameters", ".member.", num, ".", "AllowedValues" })] = parameter.AllowedValues;
                }
                if (parameter.IsSetIsModifiable())
                {
                    dictionary[string.Concat(new object[] { "Parameters", ".member.", num, ".", "IsModifiable" })] = parameter.IsModifiable.ToString().ToLower();
                }
                if (parameter.IsSetApplyMethod())
                {
                    dictionary[string.Concat(new object[] { "Parameters", ".member.", num, ".", "ApplyMethod" })] = parameter.ApplyMethod;
                }
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertRebootDBInstance(RebootDBInstanceRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "RebootDBInstance";
            if (request.IsSetDBInstanceIdentifier())
            {
                dictionary["DBInstanceIdentifier"] = request.DBInstanceIdentifier;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertResetDBParameterGroup(ResetDBParameterGroupRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "ResetDBParameterGroup";
            if (request.IsSetDBParameterGroupName())
            {
                dictionary["DBParameterGroupName"] = request.DBParameterGroupName;
            }
            if (request.IsSetResetAllParameters())
            {
                dictionary["ResetAllParameters"] = request.ResetAllParameters.ToString().ToLower();
            }
            List<Parameter> parameters = request.Parameters;
            int num = 1;
            foreach (Parameter parameter in parameters)
            {
                if (parameter.IsSetParameterName())
                {
                    dictionary[string.Concat(new object[] { "Parameters", ".member.", num, ".", "ParameterName" })] = parameter.ParameterName;
                }
                if (parameter.IsSetParameterValue())
                {
                    dictionary[string.Concat(new object[] { "Parameters", ".member.", num, ".", "ParameterValue" })] = parameter.ParameterValue;
                }
                if (parameter.IsSetDescription())
                {
                    dictionary[string.Concat(new object[] { "Parameters", ".member.", num, ".", "Description" })] = parameter.Description;
                }
                if (parameter.IsSetSource())
                {
                    dictionary[string.Concat(new object[] { "Parameters", ".member.", num, ".", "Source" })] = parameter.Source;
                }
                if (parameter.IsSetApplyType())
                {
                    dictionary[string.Concat(new object[] { "Parameters", ".member.", num, ".", "ApplyType" })] = parameter.ApplyType;
                }
                if (parameter.IsSetDataType())
                {
                    dictionary[string.Concat(new object[] { "Parameters", ".member.", num, ".", "DataType" })] = parameter.DataType;
                }
                if (parameter.IsSetAllowedValues())
                {
                    dictionary[string.Concat(new object[] { "Parameters", ".member.", num, ".", "AllowedValues" })] = parameter.AllowedValues;
                }
                if (parameter.IsSetIsModifiable())
                {
                    dictionary[string.Concat(new object[] { "Parameters", ".member.", num, ".", "IsModifiable" })] = parameter.IsModifiable.ToString().ToLower();
                }
                if (parameter.IsSetApplyMethod())
                {
                    dictionary[string.Concat(new object[] { "Parameters", ".member.", num, ".", "ApplyMethod" })] = parameter.ApplyMethod;
                }
                num++;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertRestoreDBInstanceFromDBSnapshot(RestoreDBInstanceFromDBSnapshotRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "RestoreDBInstanceFromDBSnapshot";
            if (request.IsSetDBInstanceIdentifier())
            {
                dictionary["DBInstanceIdentifier"] = request.DBInstanceIdentifier;
            }
            if (request.IsSetDBSnapshotIdentifier())
            {
                dictionary["DBSnapshotIdentifier"] = request.DBSnapshotIdentifier;
            }
            if (request.IsSetDBInstanceClass())
            {
                dictionary["DBInstanceClass"] = request.DBInstanceClass;
            }
            if (request.IsSetPort())
            {
                dictionary["Port"] = request.Port.ToString();
            }
            if (request.IsSetAvailabilityZone())
            {
                dictionary["AvailabilityZone"] = request.AvailabilityZone;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertRestoreDBInstanceToPointInTime(RestoreDBInstanceToPointInTimeRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "RestoreDBInstanceToPointInTime";
            if (request.IsSetSourceDBInstanceIdentifier())
            {
                dictionary["SourceDBInstanceIdentifier"] = request.SourceDBInstanceIdentifier;
            }
            if (request.IsSetTargetDBInstanceIdentifier())
            {
                dictionary["TargetDBInstanceIdentifier"] = request.TargetDBInstanceIdentifier;
            }
            if (request.IsSetRestoreTime())
            {
                dictionary["RestoreTime"] = request.RestoreTime.ToString();
            }
            if (request.IsSetUseLatestRestorableTime())
            {
                dictionary["UseLatestRestorableTime"] = request.UseLatestRestorableTime.ToString().ToLower();
            }
            if (request.IsSetDBInstanceClass())
            {
                dictionary["DBInstanceClass"] = request.DBInstanceClass;
            }
            if (request.IsSetPort())
            {
                dictionary["Port"] = request.Port.ToString();
            }
            if (request.IsSetAvailabilityZone())
            {
                dictionary["AvailabilityZone"] = request.AvailabilityZone;
            }
            return dictionary;
        }

        private static IDictionary<string, string> ConvertRevokeDBSecurityGroupIngress(RevokeDBSecurityGroupIngressRequest request)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Action"] = "RevokeDBSecurityGroupIngress";
            if (request.IsSetDBSecurityGroupName())
            {
                dictionary["DBSecurityGroupName"] = request.DBSecurityGroupName;
            }
            if (request.IsSetCIDRIP())
            {
                dictionary["CIDRIP"] = request.CIDRIP;
            }
            if (request.IsSetEC2SecurityGroupName())
            {
                dictionary["EC2SecurityGroupName"] = request.EC2SecurityGroupName;
            }
            if (request.IsSetEC2SecurityGroupOwnerId())
            {
                dictionary["EC2SecurityGroupOwnerId"] = request.EC2SecurityGroupOwnerId;
            }
            return dictionary;
        }

        public CreateDBInstanceResponse CreateDBInstance(CreateDBInstanceRequest request)
        {
            return this.Invoke<CreateDBInstanceResponse>(ConvertCreateDBInstance(request));
        }

        public CreateDBParameterGroupResponse CreateDBParameterGroup(CreateDBParameterGroupRequest request)
        {
            return this.Invoke<CreateDBParameterGroupResponse>(ConvertCreateDBParameterGroup(request));
        }

        public CreateDBSecurityGroupResponse CreateDBSecurityGroup(CreateDBSecurityGroupRequest request)
        {
            return this.Invoke<CreateDBSecurityGroupResponse>(ConvertCreateDBSecurityGroup(request));
        }

        public CreateDBSnapshotResponse CreateDBSnapshot(CreateDBSnapshotRequest request)
        {
            return this.Invoke<CreateDBSnapshotResponse>(ConvertCreateDBSnapshot(request));
        }

        public DeleteDBInstanceResponse DeleteDBInstance(DeleteDBInstanceRequest request)
        {
            return this.Invoke<DeleteDBInstanceResponse>(ConvertDeleteDBInstance(request));
        }

        public DeleteDBParameterGroupResponse DeleteDBParameterGroup(DeleteDBParameterGroupRequest request)
        {
            return this.Invoke<DeleteDBParameterGroupResponse>(ConvertDeleteDBParameterGroup(request));
        }

        public DeleteDBSecurityGroupResponse DeleteDBSecurityGroup(DeleteDBSecurityGroupRequest request)
        {
            return this.Invoke<DeleteDBSecurityGroupResponse>(ConvertDeleteDBSecurityGroup(request));
        }

        public DeleteDBSnapshotResponse DeleteDBSnapshot(DeleteDBSnapshotRequest request)
        {
            return this.Invoke<DeleteDBSnapshotResponse>(ConvertDeleteDBSnapshot(request));
        }

        public DescribeDBInstancesResponse DescribeDBInstances(DescribeDBInstancesRequest request)
        {
            return this.Invoke<DescribeDBInstancesResponse>(ConvertDescribeDBInstances(request));
        }

        public DescribeDBParameterGroupsResponse DescribeDBParameterGroups(DescribeDBParameterGroupsRequest request)
        {
            return this.Invoke<DescribeDBParameterGroupsResponse>(ConvertDescribeDBParameterGroups(request));
        }

        public DescribeDBParametersResponse DescribeDBParameters(DescribeDBParametersRequest request)
        {
            return this.Invoke<DescribeDBParametersResponse>(ConvertDescribeDBParameters(request));
        }

        public DescribeDBSecurityGroupsResponse DescribeDBSecurityGroups(DescribeDBSecurityGroupsRequest request)
        {
            return this.Invoke<DescribeDBSecurityGroupsResponse>(ConvertDescribeDBSecurityGroups(request));
        }

        public DescribeDBSnapshotsResponse DescribeDBSnapshots(DescribeDBSnapshotsRequest request)
        {
            return this.Invoke<DescribeDBSnapshotsResponse>(ConvertDescribeDBSnapshots(request));
        }

        public DescribeEngineDefaultParametersResponse DescribeEngineDefaultParameters(DescribeEngineDefaultParametersRequest request)
        {
            return this.Invoke<DescribeEngineDefaultParametersResponse>(ConvertDescribeEngineDefaultParameters(request));
        }

        public DescribeEventsResponse DescribeEvents(DescribeEventsRequest request)
        {
            return this.Invoke<DescribeEventsResponse>(ConvertDescribeEvents(request));
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

        ~AmazonRDSClient()
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
                                throw new AmazonRDSException(error.Message, status, error.Code, error.Type, response3.RequestId, response3.ToXML());
                            }
                        }
                        catch (Exception exception2)
                        {
                            if (exception2 is AmazonRDSException)
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

        public ModifyDBInstanceResponse ModifyDBInstance(ModifyDBInstanceRequest request)
        {
            return this.Invoke<ModifyDBInstanceResponse>(ConvertModifyDBInstance(request));
        }

        public ModifyDBParameterGroupResponse ModifyDBParameterGroup(ModifyDBParameterGroupRequest request)
        {
            return this.Invoke<ModifyDBParameterGroupResponse>(ConvertModifyDBParameterGroup(request));
        }

        private static void PauseOnRetry(int retries, int maxRetries, HttpStatusCode status)
        {
            if (retries > maxRetries)
            {
                throw new AmazonRDSException("Maximum number of retry attempts reached : " + (retries - 1), status);
            }
            int millisecondsTimeout = ((int) Math.Pow(4.0, (double) retries)) * 100;
            Thread.Sleep(millisecondsTimeout);
        }

        public RebootDBInstanceResponse RebootDBInstance(RebootDBInstanceRequest request)
        {
            return this.Invoke<RebootDBInstanceResponse>(ConvertRebootDBInstance(request));
        }

        private static AmazonRDSException ReportAnyErrors(string responseBody, HttpStatusCode status)
        {
            if ((responseBody != null) && responseBody.StartsWith("<"))
            {
                Match match = Regex.Match(responseBody, "<RequestId>(.*)</RequestId>.*<Error><Code>(.*)</Code><Message>(.*)</Message></Error>.*(<Error>)?", RegexOptions.Multiline);
                Match match2 = Regex.Match(responseBody, "<Error><Code>(.*)</Code><Message>(.*)</Message></Error>.*(<Error>)?.*<RequestID>(.*)</RequestID>", RegexOptions.Multiline);
                if (match.Success)
                {
                    string requestId = match.Groups[1].Value;
                    return new AmazonRDSException(match.Groups[3].Value, status, match.Groups[2].Value, "Unknown", requestId, responseBody);
                }
                if (match2.Success)
                {
                    string errorCode = match2.Groups[1].Value;
                    string message = match2.Groups[2].Value;
                    return new AmazonRDSException(message, status, errorCode, "Unknown", match2.Groups[4].Value, responseBody);
                }
                return new AmazonRDSException("Internal Error", status);
            }
            return new AmazonRDSException("Internal Error", status);
        }

        public ResetDBParameterGroupResponse ResetDBParameterGroup(ResetDBParameterGroupRequest request)
        {
            return this.Invoke<ResetDBParameterGroupResponse>(ConvertResetDBParameterGroup(request));
        }

        public RestoreDBInstanceFromDBSnapshotResponse RestoreDBInstanceFromDBSnapshot(RestoreDBInstanceFromDBSnapshotRequest request)
        {
            return this.Invoke<RestoreDBInstanceFromDBSnapshotResponse>(ConvertRestoreDBInstanceFromDBSnapshot(request));
        }

        public RestoreDBInstanceToPointInTimeResponse RestoreDBInstanceToPointInTime(RestoreDBInstanceToPointInTimeRequest request)
        {
            return this.Invoke<RestoreDBInstanceToPointInTimeResponse>(ConvertRestoreDBInstanceToPointInTime(request));
        }

        public RevokeDBSecurityGroupIngressResponse RevokeDBSecurityGroupIngress(RevokeDBSecurityGroupIngressRequest request)
        {
            return this.Invoke<RevokeDBSecurityGroupIngressResponse>(ConvertRevokeDBSecurityGroupIngress(request));
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

