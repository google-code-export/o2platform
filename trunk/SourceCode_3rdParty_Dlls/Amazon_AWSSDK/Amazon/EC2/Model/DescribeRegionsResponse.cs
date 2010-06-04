namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeRegionsResponse
    {
        private Amazon.EC2.Model.DescribeRegionsResult describeRegionsResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeRegionsResult()
        {
            return (this.describeRegionsResultField != null);
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

        public DescribeRegionsResponse WithDescribeRegionsResult(Amazon.EC2.Model.DescribeRegionsResult describeRegionsResult)
        {
            this.describeRegionsResultField = describeRegionsResult;
            return this;
        }

        public DescribeRegionsResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeRegionsResult")]
        public Amazon.EC2.Model.DescribeRegionsResult DescribeRegionsResult
        {
            get
            {
                return this.describeRegionsResultField;
            }
            set
            {
                this.describeRegionsResultField = value;
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

