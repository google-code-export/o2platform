namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class AppCookieStickinessPolicy
    {
        private string cookieNameField;
        private string policyNameField;

        public bool IsSetCookieName()
        {
            return (this.cookieNameField != null);
        }

        public bool IsSetPolicyName()
        {
            return (this.policyNameField != null);
        }

        public AppCookieStickinessPolicy WithCookieName(string cookieName)
        {
            this.cookieNameField = cookieName;
            return this;
        }

        public AppCookieStickinessPolicy WithPolicyName(string policyName)
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

