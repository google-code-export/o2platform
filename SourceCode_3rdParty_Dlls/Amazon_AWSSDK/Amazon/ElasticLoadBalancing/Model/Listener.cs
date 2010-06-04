namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class Listener
    {
        private decimal? instancePortField;
        private decimal? loadBalancerPortField;
        private string protocolField;

        public bool IsSetInstancePort()
        {
            return this.instancePortField.HasValue;
        }

        public bool IsSetLoadBalancerPort()
        {
            return this.loadBalancerPortField.HasValue;
        }

        public bool IsSetProtocol()
        {
            return (this.protocolField != null);
        }

        public Listener WithInstancePort(decimal instancePort)
        {
            this.instancePortField = new decimal?(instancePort);
            return this;
        }

        public Listener WithLoadBalancerPort(decimal loadBalancerPort)
        {
            this.loadBalancerPortField = new decimal?(loadBalancerPort);
            return this;
        }

        public Listener WithProtocol(string protocol)
        {
            this.protocolField = protocol;
            return this;
        }

        [XmlElement(ElementName="InstancePort")]
        public decimal InstancePort
        {
            get
            {
                return this.instancePortField.GetValueOrDefault();
            }
            set
            {
                this.instancePortField = new decimal?(value);
            }
        }

        [XmlElement(ElementName="LoadBalancerPort")]
        public decimal LoadBalancerPort
        {
            get
            {
                return this.loadBalancerPortField.GetValueOrDefault();
            }
            set
            {
                this.loadBalancerPortField = new decimal?(value);
            }
        }

        [XmlElement(ElementName="Protocol")]
        public string Protocol
        {
            get
            {
                return this.protocolField;
            }
            set
            {
                this.protocolField = value;
            }
        }
    }
}

