namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeCustomerGatewaysResponse
    {
        private Amazon.EC2.Model.DescribeCustomerGatewaysResult describeCustomerGatewaysResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeCustomerGatewaysResult()
        {
            return (this.describeCustomerGatewaysResultField != null);
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

        public DescribeCustomerGatewaysResponse WithDescribeCustomerGatewaysResult(Amazon.EC2.Model.DescribeCustomerGatewaysResult describeCustomerGatewaysResult)
        {
            this.describeCustomerGatewaysResultField = describeCustomerGatewaysResult;
            return this;
        }

        public DescribeCustomerGatewaysResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeCustomerGatewaysResult")]
        public Amazon.EC2.Model.DescribeCustomerGatewaysResult DescribeCustomerGatewaysResult
        {
            get
            {
                return this.describeCustomerGatewaysResultField;
            }
            set
            {
                this.describeCustomerGatewaysResultField = value;
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

