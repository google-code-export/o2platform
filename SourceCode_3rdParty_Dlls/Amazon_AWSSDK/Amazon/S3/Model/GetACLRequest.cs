namespace Amazon.S3.Model
{
    using System;
    using System.Xml.Serialization;

    public class GetACLRequest : S3Request
    {
        private string bucketName;
        private string key;
        private string versionId;

        internal bool IsSetBucketName()
        {
            return !string.IsNullOrEmpty(this.bucketName);
        }

        internal bool IsSetKey()
        {
            return !string.IsNullOrEmpty(this.key);
        }

        internal bool IsSetVersionId()
        {
            return !string.IsNullOrEmpty(this.versionId);
        }

        public GetACLRequest WithBucketName(string bucketName)
        {
            this.bucketName = bucketName;
            return this;
        }

        public GetACLRequest WithKey(string key)
        {
            this.key = key;
            return this;
        }

        public GetACLRequest WithVersionId(string versionId)
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

