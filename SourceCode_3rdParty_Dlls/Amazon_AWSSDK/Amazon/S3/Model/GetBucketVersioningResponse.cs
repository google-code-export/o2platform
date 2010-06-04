namespace Amazon.S3.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlType(Namespace="http://s3.amazonaws.com/doc/2006-03-01/"), XmlRoot(Namespace="http://s3.amazonaws.com/doc/2006-03-01/", IsNullable=false)]
    public class GetBucketVersioningResponse : S3Response
    {
        private S3BucketVersioningConfig config;

        [XmlElement(ElementName="VersioningConfig")]
        public S3BucketVersioningConfig VersioningConfig
        {
            get
            {
                return this.config;
            }
            set
            {
                this.config = value;
            }
        }
    }
}

