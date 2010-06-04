namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class RegisterInstancesWithLoadBalancerResponse
    {
        private Amazon.ElasticLoadBalancing.Model.RegisterInstancesWithLoadBalancerResult registerInstancesWithLoadBalancerResultField;
        private Amazon.ElasticLoadBalancing.Model.ResponseMetadata responseMetadataField;

        public bool IsSetRegisterInstancesWithLoadBalancerResult()
        {
            return (this.registerInstancesWithLoadBalancerResultField != null);
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

        [XmlElement(ElementName="RegisterInstancesWithLoadBalancerResult")]
        public Amazon.ElasticLoadBalancing.Model.RegisterInstancesWithLoadBalancerResult RegisterInstancesWithLoadBalancerResult
        {
            get
            {
                return this.registerInstancesWithLoadBalancerResultField;
            }
            set
            {
                this.registerInstancesWithLoadBalancerResultField = value;
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

