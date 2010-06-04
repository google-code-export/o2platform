namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class DisableAvailabilityZonesForLoadBalancerResponse
    {
        private Amazon.ElasticLoadBalancing.Model.DisableAvailabilityZonesForLoadBalancerResult disableAvailabilityZonesForLoadBalancerResultField;
        private Amazon.ElasticLoadBalancing.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDisableAvailabilityZonesForLoadBalancerResult()
        {
            return (this.disableAvailabilityZonesForLoadBalancerResultField != null);
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

        [XmlElement(ElementName="DisableAvailabilityZonesForLoadBalancerResult")]
        public Amazon.ElasticLoadBalancing.Model.DisableAvailabilityZonesForLoadBalancerResult DisableAvailabilityZonesForLoadBalancerResult
        {
            get
            {
                return this.disableAvailabilityZonesForLoadBalancerResultField;
            }
            set
            {
                this.disableAvailabilityZonesForLoadBalancerResultField = value;
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

