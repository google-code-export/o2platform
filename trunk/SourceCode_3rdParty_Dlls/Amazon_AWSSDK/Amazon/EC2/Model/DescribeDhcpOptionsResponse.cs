namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeDhcpOptionsResponse
    {
        private Amazon.EC2.Model.DescribeDhcpOptionsResult describeDhcpOptionsResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeDhcpOptionsResult()
        {
            return (this.describeDhcpOptionsResultField != null);
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

        public DescribeDhcpOptionsResponse WithDescribeDhcpOptionsResult(Amazon.EC2.Model.DescribeDhcpOptionsResult describeDhcpOptionsResult)
        {
            this.describeDhcpOptionsResultField = describeDhcpOptionsResult;
            return this;
        }

        public DescribeDhcpOptionsResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeDhcpOptionsResult")]
        public Amazon.EC2.Model.DescribeDhcpOptionsResult DescribeDhcpOptionsResult
        {
            get
            {
                return this.describeDhcpOptionsResultField;
            }
            set
            {
                this.describeDhcpOptionsResultField = value;
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

