namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class DescribeInstanceHealthRequest
    {
        private List<Instance> instancesField;
        private string loadBalancerNameField;

        public bool IsSetInstances()
        {
            return (this.Instances.Count > 0);
        }

        public bool IsSetLoadBalancerName()
        {
            return (this.loadBalancerNameField != null);
        }

        public DescribeInstanceHealthRequest WithInstances(params Instance[] list)
        {
            foreach (Instance instance in list)
            {
                this.Instances.Add(instance);
            }
            return this;
        }

        public DescribeInstanceHealthRequest WithLoadBalancerName(string loadBalancerName)
        {
            this.loadBalancerNameField = loadBalancerName;
            return this;
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

