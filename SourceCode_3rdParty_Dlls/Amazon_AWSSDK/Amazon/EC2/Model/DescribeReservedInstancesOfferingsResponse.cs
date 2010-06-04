namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeReservedInstancesOfferingsResponse
    {
        private Amazon.EC2.Model.DescribeReservedInstancesOfferingsResult describeReservedInstancesOfferingsResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeReservedInstancesOfferingsResult()
        {
            return (this.describeReservedInstancesOfferingsResultField != null);
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

        public DescribeReservedInstancesOfferingsResponse WithDescribeReservedInstancesOfferingsResult(Amazon.EC2.Model.DescribeReservedInstancesOfferingsResult describeReservedInstancesOfferingsResult)
        {
            this.describeReservedInstancesOfferingsResultField = describeReservedInstancesOfferingsResult;
            return this;
        }

        public DescribeReservedInstancesOfferingsResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeReservedInstancesOfferingsResult")]
        public Amazon.EC2.Model.DescribeReservedInstancesOfferingsResult DescribeReservedInstancesOfferingsResult
        {
            get
            {
                return this.describeReservedInstancesOfferingsResultField;
            }
            set
            {
                this.describeReservedInstancesOfferingsResultField = value;
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

