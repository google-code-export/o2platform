namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class DeleteLoadBalancerRequest
    {
        private string loadBalancerNameField;

        public bool IsSetLoadBalancerName()
        {
            return (this.loadBalancerNameField != null);
        }

        public DeleteLoadBalancerRequest WithLoadBalancerName(string loadBalancerName)
        {
            this.loadBalancerNameField = loadBalancerName;
            return this;
        }

        [XmlElement(ElementName="LoadBalancerName")]
        public string LoadBalancerName
        {
            get
            {
                return this.loadBalancerNameField;
            }
            set
            {
                this.loadBalancerNameField = value;
            }
        }
    }
}

