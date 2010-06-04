namespace Amazon.S3.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://s3.amazonaws.com/doc/2006-03-01/", IsNullable=false), XmlType(Namespace="http://s3.amazonaws.com/doc/2006-03-01/")]
    public class ListVersionsRequest : S3Request
    {
        private string bucketName;
        private string delimiter;
        private string keyMarker;
        private int maxKeys = -1;
        private string prefix;
        private string versionIdMarker;

        internal bool IsSetBucketName()
        {
            return !string.IsNullOrEmpty(this.bucketName);
        }

        internal bool IsSetDelimiter()
        {
            return !string.IsNullOrEmpty(this.delimiter);
        }

        internal bool IsSetKeyMarker()
        {
            return !string.IsNullOrEmpty(this.KeyMarker);
        }

        internal bool IsSetMaxKeys()
        {
            return (this.maxKeys >= 0);
        }

        internal bool IsSetPrefix()
        {
            return !string.IsNullOrEmpty(this.prefix);
        }

        internal bool IsSetVersionIdMarker()
        {
            return !string.IsNullOrEmpty(this.VersionIdMarker);
        }

        public ListVersionsRequest WithBucketName(string bucketName)
        {
            this.bucketName = bucketName;
            return this;
        }

        public ListVersionsRequest WithDelimiter(string delimiter)
        {
            this.delimiter = delimiter;
            return this;
        }

        public ListVersionsRequest WithKeyMarker(string marker)
        {
            this.keyMarker = marker;
            return this;
        }

        public ListVersionsRequest WithMaxKeys(int maxKeys)
        {
            this.maxKeys = maxKeys;
            return this;
        }

        public ListVersionsRequest WithPrefix(string prefix)
        {
            this.prefix = prefix;
            return this;
        }

        public ListVersionsRequest WithVersionIdMarker(string marker)
        {
            this.versionIdMarker = marker;
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

        [XmlElement(ElementName="Delimiter")]
        public string Delimiter
        {
            get
            {
                return this.delimiter;
            }
            set
            {
                this.delimiter = value;
            }
        }

        [XmlElement(ElementName="KeyMarker")]
        public string KeyMarker
        {
            get
            {
                return this.keyMarker;
            }
            set
            {
                this.keyMarker = value;
            }
        }

        [XmlElement(ElementName="MaxKeys")]
        public int MaxKeys
        {
            get
            {
                return this.maxKeys;
            }
            set
            {
                this.maxKeys = value;
            }
        }

        [XmlElement(ElementName="Prefix")]
        public string Prefix
        {
            get
            {
                return this.prefix;
            }
            set
            {
                this.prefix = value;
            }
        }

        [XmlElement(ElementName="VersionIdMarker")]
        public string VersionIdMarker
        {
            get
            {
                return this.versionIdMarker;
            }
            set
            {
                this.versionIdMarker = value;
            }
        }
    }
}

