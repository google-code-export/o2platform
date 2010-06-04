namespace Amazon.CloudFront
{
    using System;

    public class AmazonCloudFrontConfig
    {
        private bool fUseSecureString = true;
        private int maxErrorRetry = 3;
        private string proxyHost;
        private string proxyPassword;
        private int proxyPort = -1;
        private string proxyUsername;
        private string serviceURL = "https://cloudfront.amazonaws.com";
        private string userAgent = "AWS SDK for .NET/1.0.9";

        internal bool IsSetMaxErrorRetry()
        {
            return (this.maxErrorRetry >= 0);
        }

        internal bool IsSetProxyHost()
        {
            return !string.IsNullOrEmpty(this.proxyHost);
        }

        internal bool IsSetProxyPassword()
        {
            return !string.IsNullOrEmpty(this.proxyPassword);
        }

        internal bool IsSetProxyPort()
        {
            return ((this.proxyPort >= 0) && (this.proxyPort <= 0xffff));
        }

        internal bool IsSetProxyUsername()
        {
            return !string.IsNullOrEmpty(this.proxyUsername);
        }

        internal bool IsSetServiceURL()
        {
            return !string.IsNullOrEmpty(this.serviceURL);
        }

        internal bool IsSetUserAgent()
        {
            return !string.IsNullOrEmpty(this.userAgent);
        }

        public AmazonCloudFrontConfig WithMaxErrorRetry(int maxErrorRetry)
        {
            this.maxErrorRetry = maxErrorRetry;
            return this;
        }

        public AmazonCloudFrontConfig WithProxyHost(string proxyHost)
        {
            this.proxyHost = proxyHost;
            return this;
        }

        public AmazonCloudFrontConfig WithProxyPassword(string password)
        {
            this.proxyPassword = password;
            return this;
        }

        public AmazonCloudFrontConfig WithProxyPort(int proxyPort)
        {
            this.proxyPort = proxyPort;
            return this;
        }

        public AmazonCloudFrontConfig WithProxyUsername(string userName)
        {
            this.proxyUsername = userName;
            return this;
        }

        public AmazonCloudFrontConfig WithServiceURL(string serviceURL)
        {
            this.serviceURL = serviceURL;
            return this;
        }

        public AmazonCloudFrontConfig WithUserAgent(string userAgent)
        {
            this.userAgent = userAgent;
            return this;
        }

        public AmazonCloudFrontConfig WithUseSecureStringForAwsSecretKey(bool fSecure)
        {
            this.fUseSecureString = fSecure;
            return this;
        }

        public int MaxErrorRetry
        {
            get
            {
                return this.maxErrorRetry;
            }
            set
            {
                this.maxErrorRetry = value;
            }
        }

        public string ProxyHost
        {
            get
            {
                return this.proxyHost;
            }
            set
            {
                this.proxyHost = value;
            }
        }

        public string ProxyPassword
        {
            get
            {
                return this.proxyPassword;
            }
            set
            {
                this.proxyPassword = value;
            }
        }

        public int ProxyPort
        {
            get
            {
                return this.proxyPort;
            }
            set
            {
                this.proxyPort = value;
            }
        }

        public string ProxyUsername
        {
            get
            {
                return this.proxyUsername;
            }
            set
            {
                this.proxyUsername = value;
            }
        }

        public string ServiceURL
        {
            get
            {
                return this.serviceURL;
            }
            set
            {
                this.serviceURL = value;
            }
        }

        public string UserAgent
        {
            get
            {
                return this.userAgent;
            }
            set
            {
                this.userAgent = value;
            }
        }

        public bool UseSecureStringForAwsSecretKey
        {
            get
            {
                return this.fUseSecureString;
            }
            set
            {
                this.fUseSecureString = value;
            }
        }
    }
}

