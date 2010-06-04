namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeSpotDatafeedSubscriptionResponse
    {
        private Amazon.EC2.Model.DescribeSpotDatafeedSubscriptionResult describeSpotDatafeedSubscriptionResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeSpotDatafeedSubscriptionResult()
        {
            return (this.describeSpotDatafeedSubscriptionResultField != null);
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

        public DescribeSpotDatafeedSubscriptionResponse WithDescribeSpotDatafeedSubscriptionResult(Amazon.EC2.Model.DescribeSpotDatafeedSubscriptionResult describeSpotDatafeedSubscriptionResult)
        {
            this.describeSpotDatafeedSubscriptionResultField = describeSpotDatafeedSubscriptionResult;
            return this;
        }

        public DescribeSpotDatafeedSubscriptionResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeSpotDatafeedSubscriptionResult")]
        public Amazon.EC2.Model.DescribeSpotDatafeedSubscriptionResult DescribeSpotDatafeedSubscriptionResult
        {
            get
            {
                return this.describeSpotDatafeedSubscriptionResultField;
            }
            set
            {
                this.describeSpotDatafeedSubscriptionResultField = value;
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

