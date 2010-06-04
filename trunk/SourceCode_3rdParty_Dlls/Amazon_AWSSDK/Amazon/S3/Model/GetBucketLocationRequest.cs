namespace Amazon.S3.Model
{
    using System;
    using System.Xml.Serialization;

    public class GetBucketLocationRequest : S3Request
    {
        private string bucketName;

        internal bool IsSetBucketName()
        {
            return !string.IsNullOrEmpty(this.bucketName);
        }

        public GetBucketLocationRequest WithBucketName(string bucketName)
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

