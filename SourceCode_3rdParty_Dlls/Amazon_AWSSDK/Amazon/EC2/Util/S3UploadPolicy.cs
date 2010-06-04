namespace Amazon.EC2.Util
{
    using Amazon.Util;
    using System;
    using System.Security;
    using System.Security.Cryptography;
    using System.Text;

    public class S3UploadPolicy
    {
        private SecureString awsSecretAccessKey;
        private string clearAwsSecretAccessKey;
        private string policySignature;
        private string policyString;

        public S3UploadPolicy(string awsSecretAccessKey, string bucketName, string prefix, int expireInMinutes)
        {
            string str = BuildPolicyString(bucketName, prefix, expireInMinutes);
            this.policyString = Convert.ToBase64String(Encoding.UTF8.GetBytes(str.ToCharArray()));
            this.clearAwsSecretAccessKey = awsSecretAccessKey;
        }

        public S3UploadPolicy(string awsAccessKeyId, SecureString awsSecretAccessKey, string bucketName, string prefix, int expireInMinutes)
        {
            string str = BuildPolicyString(bucketName, prefix, expireInMinutes);
            this.policyString = Convert.ToBase64String(Encoding.UTF8.GetBytes(str.ToCharArray()));
            this.awsSecretAccessKey = awsSecretAccessKey;
        }

        private static string BuildPolicyString(string bucketName, string prefix, int expireInMinutes)
        {
            StringBuilder builder = new StringBuilder("{", 0x200);
            builder.Append("\"expiration\": \"");
            builder.Append(AWSSDKUtils.GetFormattedTimestampISO8601(expireInMinutes));
            builder.Append("\",");
            builder.Append("\"conditions\": [");
            builder.Append("{\"bucket\": \"");
            builder.Append(bucketName);
            builder.Append("\"},");
            builder.Append("{\"acl\": \"");
            builder.Append("ec2-bundle-read");
            builder.Append("\"},");
            builder.Append("[\"starts-with\", \"$key\", \"");
            builder.Append(prefix);
            builder.Append("\"]");
            builder.Append("]}");
            return builder.ToString();
        }

        public string PolicySignature
        {
            get
            {
                if (this.policySignature == null)
                {
                    if (this.awsSecretAccessKey != null)
                    {
                        this.policySignature = AWSSDKUtils.HMACSign(this.policyString, this.awsSecretAccessKey, new HMACSHA1());
                    }
                    else
                    {
                        this.policySignature = AWSSDKUtils.HMACSign(this.policyString, this.clearAwsSecretAccessKey, new HMACSHA1());
                    }
                }
                return this.policySignature;
            }
        }

        public string PolicyString
        {
            get
            {
                return this.policyString;
            }
        }
    }
}

