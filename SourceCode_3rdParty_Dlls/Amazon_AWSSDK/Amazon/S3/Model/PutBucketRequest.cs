namespace Amazon.S3.Model
{
    using System;
    using System.Xml.Serialization;

    public class PutBucketRequest : S3Request
    {
        private string bucketName;
        private S3Region bucketRegion;

        internal bool IsSetBucketName()
        {
            return !string.IsNullOrEmpty(this.bucketName);
        }

        public PutBucketRequest WithBucketName(string bucketName)
        {
            this.bucketName = bucketName;
            return this;
        }

        public PutBucketRequest WithBucketRegion(S3Region bucketRegion)
        {
            this.BucketRegion = bucketRegion;
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

        [XmlElement(ElementName="BucketRegion")]
        public S3Region BucketRegion
        {
            get
            {
                return this.bucketRegion;
            }
            set
            {
                if ((value >= S3Region.US) && (value <= S3Region.APS1))
                {
                    this.bucketRegion = value;
                }
            }
        }
    }
}

