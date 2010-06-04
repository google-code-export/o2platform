namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeSubnetsResponse
    {
        private Amazon.EC2.Model.DescribeSubnetsResult describeSubnetsResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeSubnetsResult()
        {
            return (this.describeSubnetsResultField != null);
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

        public DescribeSubnetsResponse WithDescribeSubnetsResult(Amazon.EC2.Model.DescribeSubnetsResult describeSubnetsResult)
        {
            this.describeSubnetsResultField = describeSubnetsResult;
            return this;
        }

        public DescribeSubnetsResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeSubnetsResult")]
        public Amazon.EC2.Model.DescribeSubnetsResult DescribeSubnetsResult
        {
            get
            {
                return this.describeSubnetsResultField;
            }
            set
            {
                this.describeSubnetsResultField = value;
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

