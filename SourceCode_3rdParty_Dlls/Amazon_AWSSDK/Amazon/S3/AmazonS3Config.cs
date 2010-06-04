namespace Amazon.S3
{
    using Amazon.S3.Model;
    using System;

    public class AmazonS3Config
    {
        private bool fUseSecureString = true;
        private int maxErrorRetry = 3;
        private Protocol protocol;
        private string proxyHost;
        private string proxyPassword;
        private int proxyPort = -1;
        private string proxyUsername;
        private string serviceURL = "s3.amazonaws.com";
        private string userAgent = "AWS SDK for .NET/1.0.9";

        internal bool IsSetMaxErrorRetry()
        {
            return (this.MaxErrorRetry > -1);
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
            return ((this.ProxyPort > -1) && (this.ProxyPort <= 0xffff));
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

        public AmazonS3Config WithCommunicationProtocol(Protocol protocol)
        {
            this.protocol = protocol;
            return this;
        }

        public AmazonS3Config WithMaxErrorRetry(int maxErrorRetry)
        {
            this.maxErrorRetry = maxErrorRetry;
            return this;
        }

        public AmazonS3Config WithProxyHost(string proxyHost)
        {
            this.proxyHost = proxyHost;
            return this;
        }

        public AmazonS3Config WithProxyPassword(string password)
        {
            this.proxyPassword = password;
            return this;
        }

        public AmazonS3Config WithProxyPort(int proxyPort)
        {
            this.proxyPort = proxyPort;
            return this;
        }

        public AmazonS3Config WithProxyUsername(string userName)
        {
            this.proxyUsername = userName;
            return this;
        }

        public AmazonS3Config WithServiceURL(string serviceURL)
        {
            this.serviceURL = serviceURL;
            return this;
        }

        public AmazonS3Config WithUserAgent(string userAgent)
        {
            this.userAgent = userAgent;
            return this;
        }

        public AmazonS3Config WithUseSecureStringForAwsSecretKey(bool fSecure)
        {
            this.fUseSecureString = fSecure;
            return this;
        }

        public Protocol CommunicationProtocol
        {
            get
            {
                return this.protocol;
            }
            set
            {
                this.protocol = value;
            }
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

