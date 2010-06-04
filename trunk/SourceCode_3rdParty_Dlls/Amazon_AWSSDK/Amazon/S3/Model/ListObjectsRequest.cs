namespace Amazon.S3.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://s3.amazonaws.com/doc/2006-03-01/", IsNullable=false), XmlType(Namespace="http://s3.amazonaws.com/doc/2006-03-01/")]
    public class ListObjectsRequest : S3Request
    {
        private string bucketName;
        private string delimiter;
        private string marker;
        private int maxKeys = -1;
        private string prefix;

        internal bool IsSetBucketName()
        {
            return !string.IsNullOrEmpty(this.bucketName);
        }

        internal bool IsSetDelimiter()
        {
            return !string.IsNullOrEmpty(this.delimiter);
        }

        internal bool IsSetMarker()
        {
            return !string.IsNullOrEmpty(this.marker);
        }

        internal bool IsSetMaxKeys()
        {
            return (this.maxKeys >= 0);
        }

        internal bool IsSetPrefix()
        {
            return !string.IsNullOrEmpty(this.prefix);
        }

        public ListObjectsRequest WithBucketName(string bucketName)
        {
            this.bucketName = bucketName;
            return this;
        }

        public ListObjectsRequest WithDelimiter(string delimiter)
        {
            this.delimiter = delimiter;
            return this;
        }

        public ListObjectsRequest WithMarker(string marker)
        {
            this.marker = marker;
            return this;
        }

        public ListObjectsRequest WithMaxKeys(int maxKeys)
        {
            this.maxKeys = maxKeys;
            return this;
        }

        public ListObjectsRequest WithPrefix(string prefix)
        {
            this.prefix = prefix;
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

        [XmlElement(ElementName="Marker")]
        public string Marker
        {
            get
            {
                return this.marker;
            }
            set
            {
                this.marker = value;
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
    }
}

