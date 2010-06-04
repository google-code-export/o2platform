namespace Amazon.S3.Model
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Xml.Serialization;

    public class S3Object
    {
        private string bucketName;
        private string eTag;
        private string key;
        private DateTime? lastModified;
        private Amazon.S3.Model.Owner owner;
        private long size;
        private string storageClass;

        internal bool IsSetBucketName()
        {
            return !string.IsNullOrEmpty(this.bucketName);
        }

        internal bool IsSetETag()
        {
            return !string.IsNullOrEmpty(this.eTag);
        }

        internal bool IsSetKey()
        {
            return !string.IsNullOrEmpty(this.key);
        }

        internal bool IsSetLastModified()
        {
            return this.lastModified.HasValue;
        }

        internal bool IsSetOwner()
        {
            return (this.owner != null);
        }

        internal bool IsSetStorageClass()
        {
            return !string.IsNullOrEmpty(this.storageClass);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder("Properties: {");
            if (this.IsSetKey())
            {
                builder.Append("Key:" + this.Key);
            }
            builder.Append(", Bucket:" + this.BucketName);
            builder.Append(", LastModified:" + this.LastModified);
            builder.Append(", ETag:" + this.ETag);
            builder.Append(", Size:" + this.Size);
            builder.Append(", StorageClass:" + this.StorageClass);
            builder.Append(", Owner Properties: {");
            builder.Append("Id:" + this.Owner.Id);
            builder.Append(", DisplayName:" + this.Owner.DisplayName);
            builder.Append("}}");
            return builder.ToString();
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

        [XmlElement(ElementName="ETag")]
        public string ETag
        {
            get
            {
                return this.eTag;
            }
            set
            {
                this.eTag = value;
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

        [XmlElement(ElementName="LastModified")]
        public string LastModified
        {
            get
            {
                return this.lastModified.GetValueOrDefault().ToString(@"ddd, dd MMM yyyy HH:mm:ss \G\M\T");
            }
            set
            {
                this.lastModified = new DateTime?(DateTime.ParseExact(value, @"yyyy-MM-dd\THH:mm:ss.fff\Z", CultureInfo.InvariantCulture));
            }
        }

        [XmlElement(ElementName="Owner")]
        public Amazon.S3.Model.Owner Owner
        {
            get
            {
                return this.owner;
            }
            set
            {
                this.owner = value;
            }
        }

        [XmlElement(ElementName="Size")]
        public long Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
            }
        }

        [XmlElement(ElementName="StorageClass")]
        public string StorageClass
        {
            get
            {
                return this.storageClass;
            }
            set
            {
                this.storageClass = value;
            }
        }
    }
}

