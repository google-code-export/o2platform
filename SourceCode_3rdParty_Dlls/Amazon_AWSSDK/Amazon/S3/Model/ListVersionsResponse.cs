namespace Amazon.S3.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType(Namespace="http://s3.amazonaws.com/doc/2006-03-01/"), XmlRoot(Namespace="http://s3.amazonaws.com/doc/2006-03-01/", IsNullable=false)]
    public class ListVersionsResponse : S3Response
    {
        private string delimiter;
        private bool fIsTruncated;
        private string keyMarker;
        private string maxKeys;
        private string name;
        private string nextKeyMarker;
        private string nextVersionIdMarker;
        private string prefix;
        private string versionIdMarker;
        private List<S3ObjectVersion> versions = new List<S3ObjectVersion>();

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

        [XmlElement(ElementName="NextKeyMarker")]
        public string NextKeyMarker
        {
            get
            {
                return this.nextKeyMarker;
            }
            set
            {
                this.nextKeyMarker = value;
            }
        }

        [XmlElement(ElementName="NextVersionIdMarker")]
        public string NextVersionIdMarker
        {
            get
            {
                return this.nextVersionIdMarker;
            }
            set
            {
                this.nextVersionIdMarker = value;
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

        [XmlElement(ElementName="Versions")]
        public List<S3ObjectVersion> Versions
        {
            get
            {
                return this.versions;
            }
        }
    }
}

