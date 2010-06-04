namespace Amazon.S3.Model
{
    using System;
    using System.Collections.Specialized;
    using System.Xml.Serialization;

    public class CopyObjectRequest : S3Request
    {
        private S3CannedACL cannedACL;
        private string contentType;
        private S3MetadataDirective directive;
        private string dstBucket;
        private string dstKey;
        private string etagToMatch;
        private string etagToNotMatch;
        internal NameValueCollection metaData;
        private DateTime? modifiedSinceDate;
        private string srcBucket;
        private string srcKey;
        private string srcVersionId;
        private S3StorageClass storageClass;
        private int timeout = 0x124f80;
        private DateTime? unmodifiedSinceDate;

        internal bool IsSetCannedACL()
        {
            return (this.cannedACL != S3CannedACL.NoACL);
        }

        internal bool IsSetContentType()
        {
            return !string.IsNullOrEmpty(this.contentType);
        }

        internal bool IsSetDestinationBucket()
        {
            return !string.IsNullOrEmpty(this.dstBucket);
        }

        internal bool IsSetDestinationKey()
        {
            return !string.IsNullOrEmpty(this.dstKey);
        }

        internal bool IsSetETagToMatch()
        {
            return !string.IsNullOrEmpty(this.etagToMatch);
        }

        internal bool IsSetETagToNotMatch()
        {
            return !string.IsNullOrEmpty(this.etagToNotMatch);
        }

        internal bool IsSetMetaData()
        {
            return ((this.metaData != null) && (this.metaData.Count > 0));
        }

        internal bool IsSetModifiedSinceDate()
        {
            return this.modifiedSinceDate.HasValue;
        }

        internal bool IsSetSourceBucket()
        {
            return !string.IsNullOrEmpty(this.srcBucket);
        }

        internal bool IsSetSourceKey()
        {
            return !string.IsNullOrEmpty(this.srcKey);
        }

        internal bool IsSetSourceVersionId()
        {
            return !string.IsNullOrEmpty(this.srcVersionId);
        }

        internal bool IsSetUnmodifiedSinceDate()
        {
            return this.unmodifiedSinceDate.HasValue;
        }

        public void RemoveCannedACL()
        {
            this.cannedACL = S3CannedACL.NoACL;
        }

        public void RemoveMetaData(string key)
        {
            if ((this.metaData != null) && (this.metaData.Count != 0))
            {
                this.metaData.Remove(key);
            }
        }

        public CopyObjectRequest WithCannedACL(S3CannedACL acl)
        {
            this.cannedACL = acl;
            return this;
        }

        public CopyObjectRequest WithContentType(string contentType)
        {
            this.contentType = contentType;
            return this;
        }

        public CopyObjectRequest WithDestinationBucket(string dstBucket)
        {
            this.dstBucket = dstBucket;
            return this;
        }

        public CopyObjectRequest WithDestinationKey(string dstKey)
        {
            this.dstKey = dstKey;
            return this;
        }

        public CopyObjectRequest WithDirective(S3MetadataDirective directive)
        {
            this.directive = directive;
            return this;
        }

        public CopyObjectRequest WithETagToMatch(string etagToMatch)
        {
            this.etagToMatch = etagToMatch;
            return this;
        }

        public CopyObjectRequest WithETagToNotMatch(string etagToNotMatch)
        {
            this.etagToNotMatch = etagToNotMatch;
            return this;
        }

        public CopyObjectRequest WithMetaData(NameValueCollection metaInfo)
        {
            if ((metaInfo != null) && (metaInfo.Count != 0))
            {
                if (this.metaData == null)
                {
                    this.metaData = new NameValueCollection(metaInfo.Count);
                }
                this.metaData.Add(metaInfo);
            }
            return this;
        }

        public CopyObjectRequest WithMetaData(string key, string value)
        {
            if ((key != null) && (value != null))
            {
                if (this.metaData == null)
                {
                    this.metaData = new NameValueCollection(5);
                }
                this.metaData.Add(key, value);
            }
            return this;
        }

        public CopyObjectRequest WithModifiedSinceDate(DateTime modifiedSinceDate)
        {
            this.modifiedSinceDate = new DateTime?(modifiedSinceDate);
            return this;
        }

        public CopyObjectRequest WithSourceBucket(string srcBucket)
        {
            this.srcBucket = srcBucket;
            return this;
        }

        public CopyObjectRequest WithSourceKey(string srcKey)
        {
            this.srcKey = srcKey;
            return this;
        }

        public CopyObjectRequest WithSourceVersionId(string srcVersionId)
        {
            this.srcVersionId = srcVersionId;
            return this;
        }

        public CopyObjectRequest WithStorageClass(S3StorageClass sClass)
        {
            this.StorageClass = sClass;
            return this;
        }

        public CopyObjectRequest WithTimeout(int timeout)
        {
            this.Timeout = timeout;
            return this;
        }

        public CopyObjectRequest WithUnmodifiedSinceDate(DateTime unmodifiedSinceDate)
        {
            this.unmodifiedSinceDate = new DateTime?(unmodifiedSinceDate);
            return this;
        }

        public S3CannedACL CannedACL
        {
            get
            {
                return this.cannedACL;
            }
            set
            {
                this.cannedACL = value;
            }
        }

        [XmlElement(ElementName="ContentType")]
        public string ContentType
        {
            get
            {
                return this.contentType;
            }
            set
            {
                this.contentType = value;
            }
        }

        [XmlElement(ElementName="DestinationBucket")]
        public string DestinationBucket
        {
            get
            {
                return this.dstBucket;
            }
            set
            {
                this.dstBucket = value;
            }
        }

        [XmlElement(ElementName="DestinationKey")]
        public string DestinationKey
        {
            get
            {
                return this.dstKey;
            }
            set
            {
                this.dstKey = value;
            }
        }

        [XmlElement(ElementName="Directive")]
        public S3MetadataDirective Directive
        {
            get
            {
                return this.directive;
            }
            set
            {
                this.directive = value;
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

        [XmlElement(ElementName="SourceBucket")]
        public string SourceBucket
        {
            get
            {
                return this.srcBucket;
            }
            set
            {
                this.srcBucket = value;
            }
        }

        [XmlElement(ElementName="SourceKey")]
        public string SourceKey
        {
            get
            {
                return this.srcKey;
            }
            set
            {
                this.srcKey = value;
            }
        }

        [XmlElement(ElementName="SourceVersionId")]
        public string SourceVersionId
        {
            get
            {
                return this.srcVersionId;
            }
            set
            {
                this.srcVersionId = value;
            }
        }

        public S3StorageClass StorageClass
        {
            get
            {
                return this.storageClass;
            }
            set
            {
                if ((value >= S3StorageClass.Standard) && (value <= S3StorageClass.ReducedRedundancy))
                {
                    this.storageClass = value;
                }
            }
        }

        public int Timeout
        {
            get
            {
                return this.timeout;
            }
            set
            {
                if (this.timeout > 0)
                {
                    this.timeout = value;
                }
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
    }
}

