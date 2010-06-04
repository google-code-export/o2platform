namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeSpotPriceHistoryResponse
    {
        private Amazon.EC2.Model.DescribeSpotPriceHistoryResult describeSpotPriceHistoryResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeSpotPriceHistoryResult()
        {
            return (this.describeSpotPriceHistoryResultField != null);
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

        public DescribeSpotPriceHistoryResponse WithDescribeSpotPriceHistoryResult(Amazon.EC2.Model.DescribeSpotPriceHistoryResult describeSpotPriceHistoryResult)
        {
            this.describeSpotPriceHistoryResultField = describeSpotPriceHistoryResult;
            return this;
        }

        public DescribeSpotPriceHistoryResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeSpotPriceHistoryResult")]
        public Amazon.EC2.Model.DescribeSpotPriceHistoryResult DescribeSpotPriceHistoryResult
        {
            get
            {
                return this.describeSpotPriceHistoryResultField;
            }
            set
            {
                this.describeSpotPriceHistoryResultField = value;
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

