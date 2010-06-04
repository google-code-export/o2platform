namespace Amazon.S3.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlType(Namespace="http://s3.amazonaws.com/doc/2006-03-01/"), XmlRoot(Namespace="http://s3.amazonaws.com/doc/2006-03-01/", IsNullable=false)]
    public class GetBucketLocationResponse : S3Response
    {
        private string location;

        [XmlElement(ElementName="Location")]
        public string Location
        {
            get
            {
                return this.location;
            }
            set
            {
                this.location = value;
            }
        }
    }
}

