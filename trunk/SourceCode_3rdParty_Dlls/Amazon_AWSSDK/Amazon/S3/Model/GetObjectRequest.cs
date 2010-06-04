namespace Amazon.S3.Model
{
    using System;
    using System.Xml.Serialization;

    public class GetObjectRequest : S3Request
    {
        private string bucketName;
        private Tuple<int, int> byteRange;
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

        internal bool IsSetByteRange()
        {
            return (this.ByteRange != null);
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

        public GetObjectRequest WithBucketName(string bucketName)
        {
            this.bucketName = bucketName;
            return this;
        }

        public GetObjectRequest WithByteRange(int startIndex, int endIndex)
        {
            if (startIndex > endIndex)
            {
                throw new ArgumentException("The Start Index of the range needs to be greater than the End Index");
            }
            if (startIndex < 0)
            {
                throw new ArgumentException("The Start Index of the range needs to be >= 0");
            }
            if (endIndex < 0)
            {
                throw new ArgumentException("The End Index of the range needs to be >= 0");
            }
            this.byteRange = new Tuple<int, int>(startIndex, endIndex);
            return this;
        }

        public GetObjectRequest WithETagToMatch(string etagToMatch)
        {
            this.etagToMatch = etagToMatch;
            return this;
        }

        public GetObjectRequest WithETagToNotMatch(string etagToNotMatch)
        {
            this.etagToNotMatch = etagToNotMatch;
            return this;
        }

        public GetObjectRequest WithKey(string key)
        {
            this.key = key;
            return this;
        }

        public GetObjectRequest WithModifiedSinceDate(DateTime modifiedSinceDate)
        {
            this.modifiedSinceDate = new DateTime?(modifiedSinceDate);
            return this;
        }

        public GetObjectRequest WithUnmodifiedSinceDate(DateTime unmodifiedSinceDate)
        {
            this.unmodifiedSinceDate = new DateTime?(unmodifiedSinceDate);
            return this;
        }

        public GetObjectRequest WithVersionId(string versionId)
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

        [XmlElement(ElementName="ByteRange")]
        public Tuple<int, int> ByteRange
        {
            get
            {
                return this.byteRange;
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

