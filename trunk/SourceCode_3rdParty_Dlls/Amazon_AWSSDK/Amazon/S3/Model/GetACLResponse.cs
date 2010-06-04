namespace Amazon.S3.Model
{
    using System;
    using System.Net;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://s3.amazonaws.com/doc/2006-03-01/", IsNullable=false), XmlType(Namespace="http://s3.amazonaws.com/doc/2006-03-01/")]
    public class GetACLResponse : S3Response
    {
        private S3AccessControlList accessControlList;
        private string versionId;

        [XmlElement(ElementName="AccessControlList")]
        public S3AccessControlList AccessControlList
        {
            get
            {
                return this.accessControlList;
            }
            set
            {
                this.accessControlList = value;
            }
        }

        public override WebHeaderCollection Headers
        {
            set
            {
                base.Headers = value;
                string str = null;
                if (!string.IsNullOrEmpty(str = value.Get("x-amz-version-id")))
                {
                    this.VersionId = str;
                }
            }
        }

        [XmlIgnore]
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

