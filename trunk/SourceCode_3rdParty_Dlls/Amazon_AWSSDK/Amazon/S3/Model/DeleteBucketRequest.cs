namespace Amazon.S3.Model
{
    using System;
    using System.Xml.Serialization;

    public class DeleteBucketRequest : S3Request
    {
        private string bucketName;

        internal bool IsSetBucketName()
        {
            return !string.IsNullOrEmpty(this.bucketName);
        }

        public DeleteBucketRequest WithBucketName(string bucketName)
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

