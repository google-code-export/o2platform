namespace Amazon.S3.Model
{
    using System;
    using System.Globalization;
    using System.Xml.Serialization;

    [Serializable]
    public class S3Bucket
    {
        private string bucketName;
        private DateTime? creationDate;

        internal bool IsSetBucketName()
        {
            return !string.IsNullOrEmpty(this.bucketName);
        }

        internal bool IsSetCreationDate()
        {
            return this.creationDate.HasValue;
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

        [XmlElement(ElementName="CreationDate")]
        public string CreationDate
        {
            get
            {
                return this.creationDate.GetValueOrDefault().ToString(@"ddd, dd MMM yyyy HH:mm:ss \G\M\T");
            }
            set
            {
                this.creationDate = new DateTime?(DateTime.ParseExact(value, @"yyyy-MM-dd\THH:mm:ss.fff\Z", CultureInfo.InvariantCulture));
            }
        }
    }
}

