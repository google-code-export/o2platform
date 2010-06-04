namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class Policies
    {
        private List<AppCookieStickinessPolicy> appCookieStickinessPoliciesField;
        private List<LBCookieStickinessPolicy> LBCookieStickinessPoliciesField;

        public bool IsSetAppCookieStickinessPolicies()
        {
            return (this.AppCookieStickinessPolicies.Count > 0);
        }

        public bool IsSetLBCookieStickinessPolicies()
        {
            return (this.LBCookieStickinessPolicies.Count > 0);
        }

        public Policies WithAppCookieStickinessPolicies(params AppCookieStickinessPolicy[] list)
        {
            foreach (AppCookieStickinessPolicy policy in list)
            {
                this.AppCookieStickinessPolicies.Add(policy);
            }
            return this;
        }

        public Policies WithLBCookieStickinessPolicies(params LBCookieStickinessPolicy[] list)
        {
            foreach (LBCookieStickinessPolicy policy in list)
            {
                this.LBCookieStickinessPolicies.Add(policy);
            }
            return this;
        }

        [XmlElement(ElementName="AppCookieStickinessPolicies")]
        public List<AppCookieStickinessPolicy> AppCookieStickinessPolicies
        {
            get
            {
                if (this.appCookieStickinessPoliciesField == null)
                {
                    this.appCookieStickinessPoliciesField = new List<AppCookieStickinessPolicy>();
                }
                return this.appCookieStickinessPoliciesField;
            }
            set
            {
                this.appCookieStickinessPoliciesField = value;
            }
        }

        [XmlElement(ElementName="LBCookieStickinessPolicies")]
        public List<LBCookieStickinessPolicy> LBCookieStickinessPolicies
        {
            get
            {
                if (this.LBCookieStickinessPoliciesField == null)
                {
                    this.LBCookieStickinessPoliciesField = new List<LBCookieStickinessPolicy>();
                }
                return this.LBCookieStickinessPoliciesField;
            }
            set
            {
                this.LBCookieStickinessPoliciesField = value;
            }
        }
    }
}

