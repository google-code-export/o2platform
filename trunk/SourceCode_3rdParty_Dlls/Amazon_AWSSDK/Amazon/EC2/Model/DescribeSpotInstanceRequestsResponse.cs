namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeSpotInstanceRequestsResponse
    {
        private Amazon.EC2.Model.DescribeSpotInstanceRequestsResult describeSpotInstanceRequestsResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeSpotInstanceRequestsResult()
        {
            return (this.describeSpotInstanceRequestsResultField != null);
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

        public DescribeSpotInstanceRequestsResponse WithDescribeSpotInstanceRequestsResult(Amazon.EC2.Model.DescribeSpotInstanceRequestsResult describeSpotInstanceRequestsResult)
        {
            this.describeSpotInstanceRequestsResultField = describeSpotInstanceRequestsResult;
            return this;
        }

        public DescribeSpotInstanceRequestsResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeSpotInstanceRequestsResult")]
        public Amazon.EC2.Model.DescribeSpotInstanceRequestsResult DescribeSpotInstanceRequestsResult
        {
            get
            {
                return this.describeSpotInstanceRequestsResultField;
            }
            set
            {
                this.describeSpotInstanceRequestsResultField = value;
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

