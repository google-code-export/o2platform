namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class Instance
    {
        private string instanceIdField;

        public bool IsSetInstanceId()
        {
            return (this.instanceIdField != null);
        }

        public Instance WithInstanceId(string instanceId)
        {
            this.instanceIdField = instanceId;
            return this;
        }

        [XmlElement(ElementName="InstanceId")]
        public string InstanceId
        {
            get
            {
                return this.instanceIdField;
            }
            set
            {
                this.instanceIdField = value;
            }
        }
    }
}

