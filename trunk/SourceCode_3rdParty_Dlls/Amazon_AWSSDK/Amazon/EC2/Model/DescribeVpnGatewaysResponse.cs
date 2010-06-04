namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeVpnGatewaysResponse
    {
        private Amazon.EC2.Model.DescribeVpnGatewaysResult describeVpnGatewaysResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeVpnGatewaysResult()
        {
            return (this.describeVpnGatewaysResultField != null);
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

        public DescribeVpnGatewaysResponse WithDescribeVpnGatewaysResult(Amazon.EC2.Model.DescribeVpnGatewaysResult describeVpnGatewaysResult)
        {
            this.describeVpnGatewaysResultField = describeVpnGatewaysResult;
            return this;
        }

        public DescribeVpnGatewaysResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeVpnGatewaysResult")]
        public Amazon.EC2.Model.DescribeVpnGatewaysResult DescribeVpnGatewaysResult
        {
            get
            {
                return this.describeVpnGatewaysResultField;
            }
            set
            {
                this.describeVpnGatewaysResultField = value;
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

