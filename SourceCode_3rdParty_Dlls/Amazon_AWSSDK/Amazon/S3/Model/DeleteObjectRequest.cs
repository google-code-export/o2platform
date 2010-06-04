namespace Amazon.S3.Model
{
    using System;
    using System.Xml.Serialization;

    public class DeleteObjectRequest : S3Request
    {
        private string bucketName;
        private string key;
        private Tuple<string, string> mfaCodes;
        private string versionId;

        internal bool IsSetBucketName()
        {
            return !string.IsNullOrEmpty(this.bucketName);
        }

        internal bool IsSetKey()
        {
            return !string.IsNullOrEmpty(this.key);
        }

        internal bool IsSetMfaCodes()
        {
            return (((this.mfaCodes != null) && !string.IsNullOrEmpty(this.MfaCodes.First)) && !string.IsNullOrEmpty(this.MfaCodes.Second));
        }

        internal bool IsSetVersionId()
        {
            return !string.IsNullOrEmpty(this.versionId);
        }

        public DeleteObjectRequest WithBucketName(string bucketName)
        {
            this.bucketName = bucketName;
            return this;
        }

        public DeleteObjectRequest WithKey(string key)
        {
            this.key = key;
            return this;
        }

        public DeleteObjectRequest WithMfaCodes(string serial, string token)
        {
            this.mfaCodes = new Tuple<string, string>(serial, token);
            return this;
        }

        public DeleteObjectRequest WithVersionId(string versionId)
        {
            this.versionId = versionId;
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

        [XmlElement(ElementName="Key")]
        public string Key
        {
            get
            {
                return this.key;
            }
            set
            {
                this.key = value;
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

        [XmlElement(ElementName="VersionId")]
        public string VersionId
        {
            get
            {
                return this.versionId;
            }
            set
            {
                this.versionId = value;
            }
        }
    }
}

