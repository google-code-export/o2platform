namespace Amazon.CloudFront.Util
{
    using Amazon.CloudFront;
    using Amazon.Util;
    using System;
    using System.Collections.Specialized;
    using System.Security;
    using System.Security.Cryptography;

    public static class AmazonCloudFrontUtil
    {
        public static NameValueCollection CreateHeaderEntry(string key, string value)
        {
            NameValueCollection values = new NameValueCollection();
            values.Add(key, value);
            return values;
        }

        public static string Sign(string data, SecureString key, KeyedHashAlgorithm algorithm)
        {
            if (key == null)
            {
                throw new AmazonCloudFrontException("The AWS Secret Access Key specified is NULL");
            }
            return AWSSDKUtils.HMACSign(data, key, algorithm);
        }

        public static string UrlEncode(string data, bool path)
        {
            return AWSSDKUtils.UrlEncode(data, path);
        }

        public static string FormattedCurrentTimestamp
        {
            get
            {
                return AWSSDKUtils.FormattedCurrentTimestampGMT;
            }
        }
    }
}

