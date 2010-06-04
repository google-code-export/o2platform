namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class EnableAvailabilityZonesForLoadBalancerRequest
    {
        private List<string> availabilityZonesField;
        private string loadBalancerNameField;

        public bool IsSetAvailabilityZones()
        {
            return (this.AvailabilityZones.Count > 0);
        }

        public bool IsSetLoadBalancerName()
        {
            return (this.loadBalancerNameField != null);
        }

        public EnableAvailabilityZonesForLoadBalancerRequest WithAvailabilityZones(params string[] list)
        {
            foreach (string str in list)
            {
                this.AvailabilityZones.Add(str);
            }
            return this;
        }

        public EnableAvailabilityZonesForLoadBalancerRequest WithLoadBalancerName(string loadBalancerName)
        {
            this.loadBalancerNameField = loadBalancerName;
            return this;
        }

        [XmlElement(ElementName="AvailabilityZones")]
        public List<string> AvailabilityZones
        {
            get
            {
                if (this.availabilityZonesField == null)
                {
                    this.availabilityZonesField = new List<string>();
                }
                return this.availabilityZonesField;
            }
            set
            {
                this.availabilityZonesField = value;
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
    }
}

