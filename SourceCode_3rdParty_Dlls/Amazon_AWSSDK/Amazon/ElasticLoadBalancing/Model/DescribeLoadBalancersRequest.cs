namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class DescribeLoadBalancersRequest
    {
        private List<string> loadBalancerNamesField;

        public bool IsSetLoadBalancerNames()
        {
            return (this.LoadBalancerNames.Count > 0);
        }

        public DescribeLoadBalancersRequest WithLoadBalancerNames(params string[] list)
        {
            foreach (string str in list)
            {
                this.LoadBalancerNames.Add(str);
            }
            return this;
        }

        [XmlElement(ElementName="LoadBalancerNames")]
        public List<string> LoadBalancerNames
        {
            get
            {
                if (this.loadBalancerNamesField == null)
                {
                    this.loadBalancerNamesField = new List<string>();
                }
                return this.loadBalancerNamesField;
            }
            set
            {
                this.loadBalancerNamesField = value;
            }
        }
    }
}

