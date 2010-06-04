namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class DeleteLoadBalancerPolicyRequest
    {
        private string loadBalancerNameField;
        private string policyNameField;

        public bool IsSetLoadBalancerName()
        {
            return (this.loadBalancerNameField != null);
        }

        public bool IsSetPolicyName()
        {
            return (this.policyNameField != null);
        }

        public DeleteLoadBalancerPolicyRequest WithLoadBalancerName(string loadBalancerName)
        {
            this.loadBalancerNameField = loadBalancerName;
            return this;
        }

        public DeleteLoadBalancerPolicyRequest WithPolicyName(string policyName)
        {
            this.policyNameField = policyName;
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

        [XmlElement(ElementName="PolicyName")]
        public string PolicyName
        {
            get
            {
                return this.policyNameField;
            }
            set
            {
                this.policyNameField = value;
            }
        }
    }
}

