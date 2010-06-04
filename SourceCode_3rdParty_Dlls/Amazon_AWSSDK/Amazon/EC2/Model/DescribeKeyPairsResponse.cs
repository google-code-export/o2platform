namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeKeyPairsResponse
    {
        private Amazon.EC2.Model.DescribeKeyPairsResult describeKeyPairsResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeKeyPairsResult()
        {
            return (this.describeKeyPairsResultField != null);
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

        public DescribeKeyPairsResponse WithDescribeKeyPairsResult(Amazon.EC2.Model.DescribeKeyPairsResult describeKeyPairsResult)
        {
            this.describeKeyPairsResultField = describeKeyPairsResult;
            return this;
        }

        public DescribeKeyPairsResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeKeyPairsResult")]
        public Amazon.EC2.Model.DescribeKeyPairsResult DescribeKeyPairsResult
        {
            get
            {
                return this.describeKeyPairsResultField;
            }
            set
            {
                this.describeKeyPairsResultField = value;
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

