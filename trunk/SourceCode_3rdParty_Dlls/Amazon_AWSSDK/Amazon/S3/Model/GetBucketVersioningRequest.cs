namespace Amazon.S3.Model
{
    using System;
    using System.Xml.Serialization;

    public class GetBucketVersioningRequest : S3Request
    {
        private string bucketName;

        internal bool IsSetBucketName()
        {
            return !string.IsNullOrEmpty(this.bucketName);
        }

        public GetBucketVersioningRequest WithBucketName(string bucketName)
        {
            this.bucketName = bucketName;
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
    }
}

