namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeInstanceAttributeResponse
    {
        private Amazon.EC2.Model.DescribeInstanceAttributeResult describeInstanceAttributeResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeInstanceAttributeResult()
        {
            return (this.describeInstanceAttributeResultField != null);
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

        public DescribeInstanceAttributeResponse WithDescribeInstanceAttributeResult(Amazon.EC2.Model.DescribeInstanceAttributeResult describeInstanceAttributeResult)
        {
            this.describeInstanceAttributeResultField = describeInstanceAttributeResult;
            return this;
        }

        public DescribeInstanceAttributeResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeInstanceAttributeResult")]
        public Amazon.EC2.Model.DescribeInstanceAttributeResult DescribeInstanceAttributeResult
        {
            get
            {
                return this.describeInstanceAttributeResultField;
            }
            set
            {
                this.describeInstanceAttributeResultField = value;
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

