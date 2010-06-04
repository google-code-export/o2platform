namespace Amazon.S3.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://s3.amazonaws.com/doc/2006-03-01/", IsNullable=false), XmlType(Namespace="http://s3.amazonaws.com/doc/2006-03-01/")]
    public class GetBucketLoggingResponse : S3Response
    {
        private S3BucketLoggingConfig bucketLoggingConfig;

        [XmlElement(ElementName="BucketLoggingConfig")]
        public S3BucketLoggingConfig BucketLoggingConfig
        {
            get
            {
                if (this.bucketLoggingConfig == null)
                {
                    this.bucketLoggingConfig = new S3BucketLoggingConfig();
                }
                return this.bucketLoggingConfig;
            }
            set
            {
                this.bucketLoggingConfig = value;
            }
        }
    }
}

