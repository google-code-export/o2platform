namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class SetLoadBalancerPoliciesOfListenerRequest
    {
        private string loadBalancerNameField;
        private decimal? loadBalancerPortField;
        private List<string> policyNamesField;

        public bool IsSetLoadBalancerName()
        {
            return (this.loadBalancerNameField != null);
        }

        public bool IsSetLoadBalancerPort()
        {
            return this.loadBalancerPortField.HasValue;
        }

        public bool IsSetPolicyNames()
        {
            return (this.PolicyNames.Count > 0);
        }

        public SetLoadBalancerPoliciesOfListenerRequest WithLoadBalancerName(string loadBalancerName)
        {
            this.loadBalancerNameField = loadBalancerName;
            return this;
        }

        public SetLoadBalancerPoliciesOfListenerRequest WithLoadBalancerPort(decimal loadBalancerPort)
        {
            this.loadBalancerPortField = new decimal?(loadBalancerPort);
            return this;
        }

        public SetLoadBalancerPoliciesOfListenerRequest WithPolicyNames(params string[] list)
        {
            foreach (string str in list)
            {
                this.PolicyNames.Add(str);
            }
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

        [XmlElement(ElementName="LoadBalancerPort")]
        public decimal LoadBalancerPort
        {
            get
            {
                return this.loadBalancerPortField.GetValueOrDefault();
            }
            set
            {
                this.loadBalancerPortField = new decimal?(value);
            }
        }

        [XmlElement(ElementName="PolicyNames")]
        public List<string> PolicyNames
        {
            get
            {
                if (this.policyNamesField == null)
                {
                    this.policyNamesField = new List<string>();
                }
                return this.policyNamesField;
            }
            set
            {
                this.policyNamesField = value;
            }
        }
    }
}

