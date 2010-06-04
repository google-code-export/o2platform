namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeAddressesResponse
    {
        private Amazon.EC2.Model.DescribeAddressesResult describeAddressesResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeAddressesResult()
        {
            return (this.describeAddressesResultField != null);
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

        public DescribeAddressesResponse WithDescribeAddressesResult(Amazon.EC2.Model.DescribeAddressesResult describeAddressesResult)
        {
            this.describeAddressesResultField = describeAddressesResult;
            return this;
        }

        public DescribeAddressesResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeAddressesResult")]
        public Amazon.EC2.Model.DescribeAddressesResult DescribeAddressesResult
        {
            get
            {
                return this.describeAddressesResultField;
            }
            set
            {
                this.describeAddressesResultField = value;
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

