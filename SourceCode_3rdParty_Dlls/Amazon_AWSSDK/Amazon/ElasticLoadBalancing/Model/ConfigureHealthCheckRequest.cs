namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class ConfigureHealthCheckRequest
    {
        private Amazon.ElasticLoadBalancing.Model.HealthCheck healthCheckField;
        private string loadBalancerNameField;

        public bool IsSetHealthCheck()
        {
            return (this.healthCheckField != null);
        }

        public bool IsSetLoadBalancerName()
        {
            return (this.loadBalancerNameField != null);
        }

        public ConfigureHealthCheckRequest WithHealthCheck(Amazon.ElasticLoadBalancing.Model.HealthCheck healthCheck)
        {
            this.healthCheckField = healthCheck;
            return this;
        }

        public ConfigureHealthCheckRequest WithLoadBalancerName(string loadBalancerName)
        {
            this.loadBalancerNameField = loadBalancerName;
            return this;
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

