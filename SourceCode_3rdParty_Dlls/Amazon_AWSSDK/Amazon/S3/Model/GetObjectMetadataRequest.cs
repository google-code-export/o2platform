namespace Amazon.S3.Model
{
    using System;
    using System.Xml.Serialization;

    public class GetObjectMetadataRequest : S3Request
    {
        private string bucketName;
        private string etagToMatch;
        private string etagToNotMatch;
        private string key;
        private DateTime? modifiedSinceDate;
        private DateTime? unmodifiedSinceDate;
        private string versionId;

        internal bool IsSetBucketName()
        {
            return !string.IsNullOrEmpty(this.bucketName);
        }

        internal bool IsSetETagToMatch()
        {
            return !string.IsNullOrEmpty(this.etagToMatch);
        }

        internal bool IsSetETagToNotMatch()
        {
            return !string.IsNullOrEmpty(this.etagToNotMatch);
        }

        internal bool IsSetKey()
        {
            return !string.IsNullOrEmpty(this.key);
        }

        internal bool IsSetModifiedSinceDate()
        {
            return this.modifiedSinceDate.HasValue;
        }

        internal bool IsSetUnmodifiedSinceDate()
        {
            return this.unmodifiedSinceDate.HasValue;
        }

        internal bool IsSetVersionId()
        {
            return !string.IsNullOrEmpty(this.versionId);
        }

        public GetObjectMetadataRequest WithBucketName(string bucketName)
        {
            this.bucketName = bucketName;
            return this;
        }

        public GetObjectMetadataRequest WithETagToMatch(string etagToMatch)
        {
            this.etagToMatch = etagToMatch;
            return this;
        }

        public GetObjectMetadataRequest WithETagToNotMatch(string etagToNotMatch)
        {
            this.etagToNotMatch = etagToNotMatch;
            return this;
        }

        public GetObjectMetadataRequest WithKey(string key)
        {
            this.key = key;
            return this;
        }

        public GetObjectMetadataRequest WithModifiedSinceDate(DateTime modifiedSinceDate)
        {
            this.modifiedSinceDate = new DateTime?(modifiedSinceDate);
            return this;
        }

        public GetObjectMetadataRequest WithUnmodifiedSinceDate(DateTime unmodifiedSinceDate)
        {
            this.unmodifiedSinceDate = new DateTime?(unmodifiedSinceDate);
            return this;
        }

        public GetObjectMetadataRequest WithVersionId(string versionId)
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

        [XmlElement(ElementName="ETagToMatch")]
        public string ETagToMatch
        {
            get
            {
                return this.etagToMatch;
            }
            set
            {
                this.etagToMatch = value;
            }
        }

        [XmlElement(ElementName="ETagToNotMatch")]
        public string ETagToNotMatch
        {
            get
            {
                return this.etagToNotMatch;
            }
            set
            {
                this.etagToNotMatch = value;
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

        [XmlElement(ElementName="ModifiedSinceDate")]
        public DateTime ModifiedSinceDate
        {
            get
            {
                return this.modifiedSinceDate.GetValueOrDefault();
            }
            set
            {
                this.modifiedSinceDate = new DateTime?(value);
            }
        }

        [XmlElement(ElementName="UnmodifiedSinceDate")]
        public DateTime UnmodifiedSinceDate
        {
            get
            {
                return this.unmodifiedSinceDate.GetValueOrDefault();
            }
            set
            {
                this.unmodifiedSinceDate = new DateTime?(value);
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

