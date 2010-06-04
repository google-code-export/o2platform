namespace Amazon.S3.Model
{
    using System;
    using System.Xml.Serialization;

    public class GetPreSignedUrlRequest : S3Request
    {
        private string bucketName;
        private DateTime? expires;
        private string key;
        private Amazon.S3.Model.Protocol protocol;
        private HttpVerb verb;
        private string versionId;

        internal bool IsSetBucketName()
        {
            return !string.IsNullOrEmpty(this.bucketName);
        }

        public bool IsSetExpires()
        {
            return this.expires.HasValue;
        }

        internal bool IsSetKey()
        {
            return !string.IsNullOrEmpty(this.key);
        }

        internal bool IsSetVersionId()
        {
            return !string.IsNullOrEmpty(this.versionId);
        }

        public GetPreSignedUrlRequest WithBucketName(string bucketName)
        {
            this.bucketName = bucketName;
            return this;
        }

        public GetPreSignedUrlRequest WithExpires(DateTime expires)
        {
            this.expires = new DateTime?(expires);
            return this;
        }

        public GetPreSignedUrlRequest WithKey(string key)
        {
            this.key = key;
            return this;
        }

        public GetPreSignedUrlRequest WithProtocol(Amazon.S3.Model.Protocol protocol)
        {
            this.protocol = protocol;
            return this;
        }

        public GetPreSignedUrlRequest WithVerb(HttpVerb verb)
        {
            this.Verb = verb;
            return this;
        }

        public GetPreSignedUrlRequest WithVersionId(string versionId)
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

        [XmlElement(ElementName="Expires")]
        public DateTime Expires
        {
            get
            {
                return this.expires.GetValueOrDefault();
            }
            set
            {
                this.expires = new DateTime?(value);
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

        [XmlElement(ElementName="Protocol")]
        public Amazon.S3.Model.Protocol Protocol
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

        [XmlElement(ElementName="Verb")]
        public HttpVerb Verb
        {
            get
            {
                return this.verb;
            }
            set
            {
                this.verb = value;
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

