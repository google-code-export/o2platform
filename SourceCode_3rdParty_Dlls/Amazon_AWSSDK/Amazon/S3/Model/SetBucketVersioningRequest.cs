namespace Amazon.S3.Model
{
    using System;
    using System.Xml.Serialization;

    public class SetBucketVersioningRequest : S3Request
    {
        private string bucketName;
        private S3BucketVersioningConfig config;
        private Tuple<string, string> mfaCodes;

        internal bool IsSetBucketName()
        {
            return !string.IsNullOrEmpty(this.bucketName);
        }

        internal bool IsSetMfaCodes()
        {
            return (((this.mfaCodes != null) && !string.IsNullOrEmpty(this.MfaCodes.First)) && !string.IsNullOrEmpty(this.MfaCodes.Second));
        }

        internal bool IsSetVersioningConfig()
        {
            return (this.config != null);
        }

        public SetBucketVersioningRequest WithBucketName(string bucketName)
        {
            this.bucketName = bucketName;
            return this;
        }

        public SetBucketVersioningRequest WithMfaCodes(string serial, string token)
        {
            this.mfaCodes = new Tuple<string, string>(serial, token);
            return this;
        }

        public SetBucketVersioningRequest WithVersioningConfig(S3BucketVersioningConfig config)
        {
            this.VersioningConfig = config;
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

        [XmlIgnore]
        public Tuple<string, string> MfaCodes
        {
            get
            {
                if (this.mfaCodes == null)
                {
                    this.mfaCodes = new Tuple<string, string>("", "");
                }
                return this.mfaCodes;
            }
            set
            {
                this.mfaCodes = value;
            }
        }

        [XmlElement(ElementName="VersioningConfig")]
        public S3BucketVersioningConfig VersioningConfig
        {
            get
            {
                return this.config;
            }
            set
            {
                if (value != null)
                {
                    string status = value.Status;
                    if (!status.Equals("Enabled") && !status.Equals("Suspended"))
                    {
                        throw new ArgumentException("Invalid Versioning Configuration Status - can either be Enabled or Suspended!");
                    }
                    this.config = value;
                }
                else
                {
                    this.config = value;
                }
            }
        }
    }
}

