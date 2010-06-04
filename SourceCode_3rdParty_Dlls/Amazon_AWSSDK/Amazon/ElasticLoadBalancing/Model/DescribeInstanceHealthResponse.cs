namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class DescribeInstanceHealthResponse
    {
        private Amazon.ElasticLoadBalancing.Model.DescribeInstanceHealthResult describeInstanceHealthResultField;
        private Amazon.ElasticLoadBalancing.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeInstanceHealthResult()
        {
            return (this.describeInstanceHealthResultField != null);
        }

        public bool IsSetResponseMetadata()
        {
            return (this.responseMetadataField != null);
        }

        public override string ToString()
        {
            return this.ToXML();
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

        [XmlElement(ElementName="DescribeInstanceHealthResult")]
        public Amazon.ElasticLoadBalancing.Model.DescribeInstanceHealthResult DescribeInstanceHealthResult
        {
            get
            {
                return this.describeInstanceHealthResultField;
            }
            set
            {
                this.describeInstanceHealthResultField = value;
            }
        }

        [XmlElement(ElementName="ResponseMetadata")]
        public Amazon.ElasticLoadBalancing.Model.ResponseMetadata ResponseMetadata
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

