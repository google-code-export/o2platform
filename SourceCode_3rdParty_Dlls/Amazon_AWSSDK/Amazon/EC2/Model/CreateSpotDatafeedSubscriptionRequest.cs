namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CreateSpotDatafeedSubscriptionRequest
    {
        private string bucketField;
        private string prefixField;

        public bool IsSetBucket()
        {
            return (this.bucketField != null);
        }

        public bool IsSetPrefix()
        {
            return (this.prefixField != null);
        }

        public CreateSpotDatafeedSubscriptionRequest WithBucket(string bucket)
        {
            this.bucketField = bucket;
            return this;
        }

        public CreateSpotDatafeedSubscriptionRequest WithPrefix(string prefix)
        {
            this.prefixField = prefix;
            return this;
        }

        [XmlElement(ElementName="Bucket")]
        public string Bucket
        {
            get
            {
                return this.bucketField;
            }
            set
            {
                this.bucketField = value;
            }
        }

        [XmlElement(ElementName="Prefix")]
        public string Prefix
        {
            get
            {
                return this.prefixField;
            }
            set
            {
                this.prefixField = value;
            }
        }
    }
}

