namespace Amazon.Util
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Security.Cryptography;
    using System.Text;

    public static class AWSSDKUtils
    {
        internal const string ContentLengthHeader = "Content-Length";
        internal const string ContentMD5Header = "Content-MD5";
        internal const string ContentTypeHeader = "Content-Type";
        internal const int DefaultMaxRetry = 3;
        internal const string ETagHeader = "ETag";
        public const string GMTDateFormat = @"ddd, dd MMM yyyy HH:mm:ss \G\M\T";
        internal const string IfMatchHeader = "If-Match";
        internal const string IfModifiedSinceHeader = "IfModifiedSince";
        public const string ISO8601DateFormat = @"yyyy-MM-dd\THH:mm:ss.fff\Z";
        public const string SDKUserAgent = "AWS SDK for .NET/1.0.9";
        public const string UrlEncodedContent = "application/x-www-form-urlencoded; charset=utf-8";
        public const string ValidUrlCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

        internal static string CalculateStringToSignV2(IDictionary<string, string> parameters, string serviceUrl)
        {
            StringBuilder builder = new StringBuilder("POST\n", 0x200);
            IDictionary<string, string> dictionary = new SortedDictionary<string, string>(parameters, StringComparer.Ordinal);
            Uri uri = new Uri(serviceUrl);
            builder.Append(uri.Host);
            builder.Append("\n");
            string absolutePath = uri.AbsolutePath;
            if ((absolutePath == null) || (absolutePath.Length == 0))
            {
                absolutePath = "/";
            }
            builder.Append(UrlEncode(absolutePath, true));
            builder.Append("\n");
            foreach (KeyValuePair<string, string> pair in dictionary)
            {
                if (pair.Value != null)
                {
                    builder.Append(UrlEncode(pair.Key, false));
                    builder.Append("=");
                    builder.Append(UrlEncode(pair.Value, false));
                    builder.Append("&");
                }
            }
            string str2 = builder.ToString();
            return str2.Remove(str2.Length - 1);
        }

        public static string GetFormattedTimestampISO8601(int minutesFromNow)
        {
            DateTime time = DateTime.UtcNow.AddMinutes((double) minutesFromNow);
            DateTime time2 = new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second, time.Millisecond, DateTimeKind.Local);
            return time2.ToString(@"yyyy-MM-dd\THH:mm:ss.fff\Z", CultureInfo.InvariantCulture);
        }

        internal static string GetParametersAsString(IDictionary<string, string> parameters)
        {
            StringBuilder builder = new StringBuilder(0x200);
            foreach (string str in parameters.Keys)
            {
                string data = parameters[str];
                if (data != null)
                {
                    builder.Append(str);
                    builder.Append('=');
                    builder.Append(UrlEncode(data, false));
                    builder.Append('&');
                }
            }
            string str3 = builder.ToString();
            return str3.Remove(str3.Length - 1);
        }

        public static string HMACSign(string data, SecureString key, KeyedHashAlgorithm algorithm)
        {
            string str;
            if (key == null)
            {
                throw new ArgumentNullException("key", "Please specify a Secret Signing Key.");
            }
            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException("data", "Please specify data to sign.");
            }
            if (algorithm == null)
            {
                throw new ArgumentNullException("algorithm", "Please specify a KeyedHashAlgorithm to use.");
            }
            IntPtr zero = IntPtr.Zero;
            char[] destination = new char[key.Length];
            try
            {
                zero = Marshal.SecureStringToBSTR(key);
                Marshal.Copy(zero, destination, 0, destination.Length);
                algorithm.Key = Encoding.UTF8.GetBytes(destination);
                str = Convert.ToBase64String(algorithm.ComputeHash(Encoding.UTF8.GetBytes(data.ToCharArray())));
            }
            finally
            {
                Marshal.ZeroFreeBSTR(zero);
                algorithm.Clear();
                Array.Clear(destination, 0, destination.Length);
            }
            return str;
        }

        public static string HMACSign(string data, string key, KeyedHashAlgorithm algorithm)
        {
            string str;
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key", "Please specify a Secret Signing Key.");
            }
            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException("data", "Please specify data to sign.");
            }
            if (algorithm == null)
            {
                throw new ArgumentNullException("algorithm", "Please specify a KeyedHashAlgorithm to use.");
            }
            try
            {
                algorithm.Key = Encoding.UTF8.GetBytes(key);
                str = Convert.ToBase64String(algorithm.ComputeHash(Encoding.UTF8.GetBytes(data)));
            }
            finally
            {
                algorithm.Clear();
            }
            return str;
        }

        public static string UrlEncode(string data, bool path)
        {
            StringBuilder builder = new StringBuilder(data.Length * 2);
            string str = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~" + (path ? "/:+" : "");
            foreach (char ch in Encoding.UTF8.GetBytes(data))
            {
                if (str.IndexOf(ch) != -1)
                {
                    builder.Append(ch);
                }
                else
                {
                    builder.Append("%").Append(string.Format("{0:X2}", (int) ch));
                }
            }
            return builder.ToString();
        }

        public static string FormattedCurrentTimestampGMT
        {
            get
            {
                DateTime utcNow = DateTime.UtcNow;
                DateTime time2 = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, utcNow.Hour, utcNow.Minute, utcNow.Second, utcNow.Millisecond, DateTimeKind.Local);
                return time2.ToString(@"ddd, dd MMM yyyy HH:mm:ss \G\M\T", CultureInfo.InvariantCulture);
            }
        }

        public static string FormattedCurrentTimestampISO8601
        {
            get
            {
                return GetFormattedTimestampISO8601(0);
            }
        }
    }
}

