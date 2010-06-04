namespace Amazon.S3.Model
{
    using System;
    using System.Net;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://s3.amazonaws.com/doc/2006-03-01/", IsNullable=false), XmlType(Namespace="http://s3.amazonaws.com/doc/2006-03-01/")]
    public class CopyObjectResponse : S3Response
    {
        private string eTag;
        private string lastModified;
        private string srcVersionId;
        private string versionId;

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

        public override WebHeaderCollection Headers
        {
            set
            {
                base.Headers = value;
                string str = null;
                if (!string.IsNullOrEmpty(str = value.Get("x-amz-copy-source-version-id")))
                {
                    this.SourceVersionId = str;
                }
                if (!string.IsNullOrEmpty(str = value.Get("x-amz-version-id")))
                {
                    this.VersionId = str;
                }
            }
        }

        [XmlElement(ElementName="LastModified")]
        public string LastModified
        {
            get
            {
                return this.lastModified;
            }
            set
            {
                this.lastModified = value;
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

