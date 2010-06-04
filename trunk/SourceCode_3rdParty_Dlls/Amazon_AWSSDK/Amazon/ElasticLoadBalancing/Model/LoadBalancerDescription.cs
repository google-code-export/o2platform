namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class LoadBalancerDescription
    {
        private List<string> availabilityZonesField;
        private string createdTimeField;
        private string DNSNameField;
        private Amazon.ElasticLoadBalancing.Model.HealthCheck healthCheckField;
        private List<Instance> instancesField;
        private List<ListenerDescription> listenerDescriptionsField;
        private string loadBalancerNameField;
        private Amazon.ElasticLoadBalancing.Model.Policies policiesField;

        public bool IsSetAvailabilityZones()
        {
            return (this.AvailabilityZones.Count > 0);
        }

        public bool IsSetCreatedTime()
        {
            return (this.createdTimeField != null);
        }

        public bool IsSetDNSName()
        {
            return (this.DNSNameField != null);
        }

        public bool IsSetHealthCheck()
        {
            return (this.healthCheckField != null);
        }

        public bool IsSetInstances()
        {
            return (this.Instances.Count > 0);
        }

        public bool IsSetListenerDescriptions()
        {
            return (this.ListenerDescriptions.Count > 0);
        }

        public bool IsSetLoadBalancerName()
        {
            return (this.loadBalancerNameField != null);
        }

        public bool IsSetPolicies()
        {
            return (this.policiesField != null);
        }

        public LoadBalancerDescription WithAvailabilityZones(params string[] list)
        {
            foreach (string str in list)
            {
                this.AvailabilityZones.Add(str);
            }
            return this;
        }

        public LoadBalancerDescription WithCreatedTime(string createdTime)
        {
            this.createdTimeField = createdTime;
            return this;
        }

        public LoadBalancerDescription WithDNSName(string DNSName)
        {
            this.DNSNameField = DNSName;
            return this;
        }

        public LoadBalancerDescription WithHealthCheck(Amazon.ElasticLoadBalancing.Model.HealthCheck healthCheck)
        {
            this.healthCheckField = healthCheck;
            return this;
        }

        public LoadBalancerDescription WithInstances(params Instance[] list)
        {
            foreach (Instance instance in list)
            {
                this.Instances.Add(instance);
            }
            return this;
        }

        public LoadBalancerDescription WithListenerDescriptions(params ListenerDescription[] list)
        {
            foreach (ListenerDescription description in list)
            {
                this.ListenerDescriptions.Add(description);
            }
            return this;
        }

        public LoadBalancerDescription WithLoadBalancerName(string loadBalancerName)
        {
            this.loadBalancerNameField = loadBalancerName;
            return this;
        }

        public LoadBalancerDescription WithPolicies(Amazon.ElasticLoadBalancing.Model.Policies policies)
        {
            this.policiesField = policies;
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

        [XmlElement(ElementName="CreatedTime")]
        public string CreatedTime
        {
            get
            {
                return this.createdTimeField;
            }
            set
            {
                this.createdTimeField = value;
            }
        }

        [XmlElement(ElementName="DNSName")]
        public string DNSName
        {
            get
            {
                return this.DNSNameField;
            }
            set
            {
                this.DNSNameField = value;
            }
        }

        [XmlElement(ElementName="HealthCheck")]
        public Amazon.ElasticLoadBalancing.Model.HealthCheck HealthCheck
        {
            get
            {
                return this.healthCheckField;
            }
            set
            {
                this.healthCheckField = value;
            }
        }

        [XmlElement(ElementName="Instances")]
        public List<Instance> Instances
        {
            get
            {
                if (this.instancesField == null)
                {
                    this.instancesField = new List<Instance>();
                }
                return this.instancesField;
            }
            set
            {
                this.instancesField = value;
            }
        }

        [XmlElement(ElementName="ListenerDescriptions")]
        public List<ListenerDescription> ListenerDescriptions
        {
            get
            {
                if (this.listenerDescriptionsField == null)
                {
                    this.listenerDescriptionsField = new List<ListenerDescription>();
                }
                return this.listenerDescriptionsField;
            }
            set
            {
                this.listenerDescriptionsField = value;
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

        [XmlElement(ElementName="Policies")]
        public Amazon.ElasticLoadBalancing.Model.Policies Policies
        {
            get
            {
                return this.policiesField;
            }
            set
            {
                this.policiesField = value;
            }
        }
    }
}

