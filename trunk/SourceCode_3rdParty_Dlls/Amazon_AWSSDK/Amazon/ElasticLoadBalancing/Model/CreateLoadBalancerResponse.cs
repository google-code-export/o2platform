namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class CreateLoadBalancerResponse
    {
        private Amazon.ElasticLoadBalancing.Model.CreateLoadBalancerResult createLoadBalancerResultField;
        private Amazon.ElasticLoadBalancing.Model.ResponseMetadata responseMetadataField;

        public bool IsSetCreateLoadBalancerResult()
        {
            return (this.createLoadBalancerResultField != null);
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

        [XmlElement(ElementName="CreateLoadBalancerResult")]
        public Amazon.ElasticLoadBalancing.Model.CreateLoadBalancerResult CreateLoadBalancerResult
        {
            get
            {
                return this.createLoadBalancerResultField;
            }
            set
            {
                this.createLoadBalancerResultField = value;
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

