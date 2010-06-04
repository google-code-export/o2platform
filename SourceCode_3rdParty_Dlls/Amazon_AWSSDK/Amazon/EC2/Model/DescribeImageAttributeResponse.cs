namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeImageAttributeResponse
    {
        private Amazon.EC2.Model.DescribeImageAttributeResult describeImageAttributeResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeImageAttributeResult()
        {
            return (this.describeImageAttributeResultField != null);
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

        public DescribeImageAttributeResponse WithDescribeImageAttributeResult(Amazon.EC2.Model.DescribeImageAttributeResult describeImageAttributeResult)
        {
            this.describeImageAttributeResultField = describeImageAttributeResult;
            return this;
        }

        public DescribeImageAttributeResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeImageAttributeResult")]
        public Amazon.EC2.Model.DescribeImageAttributeResult DescribeImageAttributeResult
        {
            get
            {
                return this.describeImageAttributeResultField;
            }
            set
            {
                this.describeImageAttributeResultField = value;
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

