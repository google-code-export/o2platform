namespace Amazon.S3.Model
{
    using System;
    using System.Xml.Serialization;

    public class EnableBucketLoggingRequest : S3Request
    {
        private string bucketName;
        private S3BucketLoggingConfig loggingConfig;

        internal bool IsSetBucketName()
        {
            return !string.IsNullOrEmpty(this.bucketName);
        }

        internal bool IsSetLoggingConfig()
        {
            return (this.loggingConfig != null);
        }

        public EnableBucketLoggingRequest WithBucketName(string bucketName)
        {
            this.bucketName = bucketName;
            return this;
        }

        public EnableBucketLoggingRequest WithLoggingConfig(S3BucketLoggingConfig loggingConfig)
        {
            this.loggingConfig = loggingConfig;
            return this;
        }

        [XmlElement(ElementName="BucketName")]
        public string BucketName
        {
            get
            {
                return this.bucketName;
            }
            set
            {
                this.bucketName = value;
            }
        }

        [XmlElement(ElementName="LoggingConfig")]
        public S3BucketLoggingConfig LoggingConfig
        {
            get
            {
                if (this.loggingConfig == null)
                {
                    this.loggingConfig = new S3BucketLoggingConfig();
                }
                return this.loggingConfig;
            }
            set
            {
                this.loggingConfig = value;
            }
        }
    }
}

