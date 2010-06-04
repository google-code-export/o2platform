namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class DeregisterInstancesFromLoadBalancerResponse
    {
        private Amazon.ElasticLoadBalancing.Model.DeregisterInstancesFromLoadBalancerResult deregisterInstancesFromLoadBalancerResultField;
        private Amazon.ElasticLoadBalancing.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDeregisterInstancesFromLoadBalancerResult()
        {
            return (this.deregisterInstancesFromLoadBalancerResultField != null);
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

        [XmlElement(ElementName="DeregisterInstancesFromLoadBalancerResult")]
        public Amazon.ElasticLoadBalancing.Model.DeregisterInstancesFromLoadBalancerResult DeregisterInstancesFromLoadBalancerResult
        {
            get
            {
                return this.deregisterInstancesFromLoadBalancerResultField;
            }
            set
            {
                this.deregisterInstancesFromLoadBalancerResultField = value;
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

