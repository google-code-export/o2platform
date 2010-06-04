namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class CreateLBCookieStickinessPolicyRequest
    {
        private decimal? cookieExpirationPeriodField;
        private string loadBalancerNameField;
        private string policyNameField;

        public bool IsSetCookieExpirationPeriod()
        {
            return this.cookieExpirationPeriodField.HasValue;
        }

        public bool IsSetLoadBalancerName()
        {
            return (this.loadBalancerNameField != null);
        }

        public bool IsSetPolicyName()
        {
            return (this.policyNameField != null);
        }

        public CreateLBCookieStickinessPolicyRequest WithCookieExpirationPeriod(decimal cookieExpirationPeriod)
        {
            this.cookieExpirationPeriodField = new decimal?(cookieExpirationPeriod);
            return this;
        }

        public CreateLBCookieStickinessPolicyRequest WithLoadBalancerName(string loadBalancerName)
        {
            this.loadBalancerNameField = loadBalancerName;
            return this;
        }

        public CreateLBCookieStickinessPolicyRequest WithPolicyName(string policyName)
        {
            this.policyNameField = policyName;
            return this;
        }

        [XmlElement(ElementName="CookieExpirationPeriod")]
        public decimal CookieExpirationPeriod
        {
            get
            {
                return this.cookieExpirationPeriodField.GetValueOrDefault();
            }
            set
            {
                this.cookieExpirationPeriodField = new decimal?(value);
            }
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

