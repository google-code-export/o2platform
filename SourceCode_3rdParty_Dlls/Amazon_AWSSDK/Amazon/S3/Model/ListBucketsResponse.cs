namespace Amazon.S3.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://s3.amazonaws.com/doc/2006-03-01/", IsNullable=false), XmlType(Namespace="http://s3.amazonaws.com/doc/2006-03-01/")]
    public class ListBucketsResponse : S3Response
    {
        private List<S3Bucket> buckets = new List<S3Bucket>();
        private Amazon.S3.Model.Owner owner;

        [XmlIgnore, Obsolete("Use the Buckets property instead")]
        public List<S3Bucket> Bucket
        {
            get
            {
                return this.Buckets;
            }
        }

        [XmlElement(ElementName="Buckets")]
        public List<S3Bucket> Buckets
        {
            get
            {
                return this.buckets;
            }
        }

        [XmlElement(ElementName="Owner")]
        public Amazon.S3.Model.Owner Owner
        {
            get
            {
                return this.owner;
            }
            set
            {
                this.owner = value;
            }
        }
    }
}

