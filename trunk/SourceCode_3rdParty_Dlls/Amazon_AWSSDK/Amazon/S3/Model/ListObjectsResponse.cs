namespace Amazon.S3.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://s3.amazonaws.com/doc/2006-03-01/", IsNullable=false), XmlType(Namespace="http://s3.amazonaws.com/doc/2006-03-01/")]
    public class ListObjectsResponse : S3Response
    {
        private List<string> commonPrefixes;
        private string delimiter;
        private bool fIsTruncated;
        private string maxKeys;
        private string name;
        private string nextMarker;
        private List<S3Object> objects = new List<S3Object>();
        private string prefix;

        [XmlIgnore, Obsolete("Use the CommonPrefixes property instead")]
        public List<string> CommonPrefix
        {
            get
            {
                return this.CommonPrefixes;
            }
        }

        [XmlElement(ElementName="CommonPrefixes")]
        public List<string> CommonPrefixes
        {
            get
            {
                if (this.commonPrefixes == null)
                {
                    this.commonPrefixes = new List<string>();
                }
                return this.commonPrefixes;
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

        [XmlElement(ElementName="IsTruncated")]
        public bool IsTruncated
        {
            get
            {
                return this.fIsTruncated;
            }
            set
            {
                this.fIsTruncated = value;
            }
        }

        [XmlElement(ElementName="MaxKeys")]
        public string MaxKeys
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

        [XmlElement(ElementName="Name")]
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        [XmlElement(ElementName="NextMarker")]
        public string NextMarker
        {
            get
            {
                if ((string.IsNullOrEmpty(this.nextMarker) && this.fIsTruncated) && (this.objects.Count > 0))
                {
                    int num = this.objects.Count - 1;
                    this.nextMarker = this.objects[num].Key;
                }
                return this.nextMarker;
            }
            set
            {
                this.nextMarker = value;
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

        [XmlElement(ElementName="S3Objects")]
        public List<S3Object> S3Objects
        {
            get
            {
                return this.objects;
            }
        }
    }
}

