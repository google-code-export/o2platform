namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class CreateLoadBalancerRequest
    {
        private List<string> availabilityZonesField;
        private List<Listener> listenersField;
        private string loadBalancerNameField;

        public bool IsSetAvailabilityZones()
        {
            return (this.AvailabilityZones.Count > 0);
        }

        public bool IsSetListeners()
        {
            return (this.Listeners.Count > 0);
        }

        public bool IsSetLoadBalancerName()
        {
            return (this.loadBalancerNameField != null);
        }

        public CreateLoadBalancerRequest WithAvailabilityZones(params string[] list)
        {
            foreach (string str in list)
            {
                this.AvailabilityZones.Add(str);
            }
            return this;
        }

        public CreateLoadBalancerRequest WithListeners(params Listener[] list)
        {
            foreach (Listener listener in list)
            {
                this.Listeners.Add(listener);
            }
            return this;
        }

        public CreateLoadBalancerRequest WithLoadBalancerName(string loadBalancerName)
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

        [XmlElement(ElementName="Listeners")]
        public List<Listener> Listeners
        {
            get
            {
                if (this.listenersField == null)
                {
                    this.listenersField = new List<Listener>();
                }
                return this.listenersField;
            }
            set
            {
                this.listenersField = value;
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

