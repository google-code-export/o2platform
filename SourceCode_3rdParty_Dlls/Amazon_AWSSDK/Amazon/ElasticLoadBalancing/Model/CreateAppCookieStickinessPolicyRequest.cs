namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class CreateAppCookieStickinessPolicyRequest
    {
        private string cookieNameField;
        private string loadBalancerNameField;
        private string policyNameField;

        public bool IsSetCookieName()
        {
            return (this.cookieNameField != null);
        }

        public bool IsSetLoadBalancerName()
        {
            return (this.loadBalancerNameField != null);
        }

        public bool IsSetPolicyName()
        {
            return (this.policyNameField != null);
        }

        public CreateAppCookieStickinessPolicyRequest WithCookieName(string cookieName)
        {
            this.cookieNameField = cookieName;
            return this;
        }

        public CreateAppCookieStickinessPolicyRequest WithLoadBalancerName(string loadBalancerName)
        {
            this.loadBalancerNameField = loadBalancerName;
            return this;
        }

        public CreateAppCookieStickinessPolicyRequest WithPolicyName(string policyName)
        {
            this.policyNameField = policyName;
            return this;
        }

        [XmlElement(ElementName="CookieName")]
        public string CookieName
        {
            get
            {
                return this.cookieNameField;
            }
            set
            {
                this.cookieNameField = value;
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

