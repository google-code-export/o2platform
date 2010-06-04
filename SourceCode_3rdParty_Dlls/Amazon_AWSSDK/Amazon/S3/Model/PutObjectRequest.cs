namespace Amazon.S3.Model
{
    using System;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class PutObjectRequest : S3Request
    {
        private string bucketName;
        private S3CannedACL cannedACL;
        private string contentBody;
        private string contentType;
        private bool fGenerateMD5Digest;
        private string filePath;
        private string key;
        private string md5Digest;
        internal NameValueCollection metaData;
        private S3StorageClass storageClass;
        private int timeout = 0x124f80;

        public event EventHandler<PutObjectProgressArgs> PutObjectProgressEvent;

        internal bool IsSetBucketName()
        {
            return !string.IsNullOrEmpty(this.bucketName);
        }

        internal bool IsSetCannedACL()
        {
            return (this.cannedACL != S3CannedACL.NoACL);
        }

        internal bool IsSetContentBody()
        {
            return !string.IsNullOrEmpty(this.contentBody);
        }

        internal bool IsSetContentType()
        {
            return !string.IsNullOrEmpty(this.contentType);
        }

        internal bool IsSetFilePath()
        {
            return !string.IsNullOrEmpty(this.filePath);
        }

        internal bool IsSetKey()
        {
            return !string.IsNullOrEmpty(this.key);
        }

        internal bool IsSetMD5Digest()
        {
            return !string.IsNullOrEmpty(this.md5Digest);
        }

        internal bool IsSetMetaData()
        {
            return ((this.metaData != null) && (this.metaData.Count > 0));
        }

        internal void OnRaiseProgressEvent(PutObjectProgressArgs e)
        {
            EventHandler<PutObjectProgressArgs> putObjectProgressEvent = this.PutObjectProgressEvent;
            try
            {
                if (putObjectProgressEvent != null)
                {
                    putObjectProgressEvent(this, e);
                }
            }
            catch
            {
            }
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

        public PutObjectRequest WithBucketName(string bucketName)
        {
            this.bucketName = bucketName;
            return this;
        }

        public PutObjectRequest WithCannedACL(S3CannedACL acl)
        {
            this.cannedACL = acl;
            return this;
        }

        public PutObjectRequest WithContentBody(string contentBody)
        {
            this.contentBody = contentBody;
            this.contentType = "text/plain";
            return this;
        }

        public PutObjectRequest WithContentType(string contentType)
        {
            this.contentType = contentType;
            return this;
        }

        public PutObjectRequest WithFilePath(string filePath)
        {
            this.filePath = filePath;
            return this;
        }

        public PutObjectRequest WithGenerateChecksum(bool fGenerateMD5Digest)
        {
            this.fGenerateMD5Digest = fGenerateMD5Digest;
            return this;
        }

        public PutObjectRequest WithKey(string key)
        {
            this.key = key;
            return this;
        }

        public PutObjectRequest WithMD5Digest(string digest)
        {
            this.md5Digest = digest;
            return this;
        }

        public PutObjectRequest WithMetaData(NameValueCollection metaInfo)
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

        public PutObjectRequest WithMetaData(string key, string value)
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

        public PutObjectRequest WithStorageClass(S3StorageClass sClass)
        {
            this.storageClass = sClass;
            return this;
        }

        public PutObjectRequest WithSubscriber(EventHandler<PutObjectProgressArgs> handler)
        {
            this.PutObjectProgressEvent = (EventHandler<PutObjectProgressArgs>) Delegate.Combine(this.PutObjectProgressEvent, handler);
            return this;
        }

        public PutObjectRequest WithTimeout(int timeout)
        {
            this.Timeout = timeout;
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

        [XmlElement(ElementName="ContentBody")]
        public string ContentBody
        {
            get
            {
                return this.contentBody;
            }
            set
            {
                this.contentBody = value;
                if (value != null)
                {
                    this.contentType = "text/plain";
                }
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

        [XmlElement(ElementName="FilePath")]
        public string FilePath
        {
            get
            {
                return this.filePath;
            }
            set
            {
                this.filePath = value;
            }
        }

        [XmlElement(ElementName="GenerateMD5Digest")]
        public bool GenerateMD5Digest
        {
            get
            {
                return this.fGenerateMD5Digest;
            }
            set
            {
                this.fGenerateMD5Digest = value;
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

        [XmlElement(ElementName="MD5Digest")]
        public string MD5Digest
        {
            get
            {
                return this.md5Digest;
            }
            set
            {
                this.md5Digest = value;
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
    }
}

