namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class LBCookieStickinessPolicy
    {
        private decimal? cookieExpirationPeriodField;
        private string policyNameField;

        public bool IsSetCookieExpirationPeriod()
        {
            return this.cookieExpirationPeriodField.HasValue;
        }

        public bool IsSetPolicyName()
        {
            return (this.policyNameField != null);
        }

        public LBCookieStickinessPolicy WithCookieExpirationPeriod(decimal cookieExpirationPeriod)
        {
            this.cookieExpirationPeriodField = new decimal?(cookieExpirationPeriod);
            return this;
        }

        public LBCookieStickinessPolicy WithPolicyName(string policyName)
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

