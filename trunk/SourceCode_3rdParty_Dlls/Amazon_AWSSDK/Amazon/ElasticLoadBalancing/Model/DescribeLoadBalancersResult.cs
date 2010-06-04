namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class DescribeLoadBalancersResult
    {
        private List<LoadBalancerDescription> loadBalancerDescriptionsField;

        public bool IsSetLoadBalancerDescriptions()
        {
            return (this.LoadBalancerDescriptions.Count > 0);
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

        [XmlElement(ElementName="LoadBalancerDescriptions")]
        public List<LoadBalancerDescription> LoadBalancerDescriptions
        {
            get
            {
                if (this.loadBalancerDescriptionsField == null)
                {
                    this.loadBalancerDescriptionsField = new List<LoadBalancerDescription>();
                }
                return this.loadBalancerDescriptionsField;
            }
            set
            {
                this.loadBalancerDescriptionsField = value;
            }
        }
    }
}

