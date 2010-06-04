namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeAvailabilityZonesResponse
    {
        private Amazon.EC2.Model.DescribeAvailabilityZonesResult describeAvailabilityZonesResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeAvailabilityZonesResult()
        {
            return (this.describeAvailabilityZonesResultField != null);
        }

        public bool IsSetResponseMetadata()
        {
            return (this.responseMetadataField != null);
        }

        public string ToXML()
        {
            StringBuilder sb = new StringBuilder(0x400);
            XmlSerializer serializer = new XmlSerializer(base.GetType());
            using (StringWriter writer = new StringWriter(sb))
            {
                serializer.Serialize((TextWriter) writer, this);
            }
            return sb.ToString();
        }

        public DescribeAvailabilityZonesResponse WithDescribeAvailabilityZonesResult(Amazon.EC2.Model.DescribeAvailabilityZonesResult describeAvailabilityZonesResult)
        {
            this.describeAvailabilityZonesResultField = describeAvailabilityZonesResult;
            return this;
        }

        public DescribeAvailabilityZonesResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeAvailabilityZonesResult")]
        public Amazon.EC2.Model.DescribeAvailabilityZonesResult DescribeAvailabilityZonesResult
        {
            get
            {
                return this.describeAvailabilityZonesResultField;
            }
            set
            {
                this.describeAvailabilityZonesResultField = value;
            }
        }

        [XmlElement(ElementName="ResponseMetadata")]
        public Amazon.EC2.Model.ResponseMetadata ResponseMetadata
        {
            get
            {
                return this.responseMetadataField;
            }
            set
            {
                this.responseMetadataField = value;
            }
        }
    }
}

