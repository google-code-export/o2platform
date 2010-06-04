namespace Amazon.AutoScaling.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://autoscaling.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class Instance
    {
        private string availabilityZoneField;
        private string instanceIdField;
        private string lifecycleStateField;

        public bool IsSetAvailabilityZone()
        {
            return (this.availabilityZoneField != null);
        }

        public bool IsSetInstanceId()
        {
            return (this.instanceIdField != null);
        }

        public bool IsSetLifecycleState()
        {
            return (this.lifecycleStateField != null);
        }

        public Instance WithAvailabilityZone(string availabilityZone)
        {
            this.availabilityZoneField = availabilityZone;
            return this;
        }

        public Instance WithInstanceId(string instanceId)
        {
            this.instanceIdField = instanceId;
            return this;
        }

        public Instance WithLifecycleState(string lifecycleState)
        {
            this.lifecycleStateField = lifecycleState;
            return this;
        }

        [XmlElement(ElementName="AvailabilityZone")]
        public string AvailabilityZone
        {
            get
            {
                return this.availabilityZoneField;
            }
            set
            {
                this.availabilityZoneField = value;
            }
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

        [XmlElement(ElementName="LifecycleState")]
        public string LifecycleState
        {
            get
            {
                return this.lifecycleStateField;
            }
            set
            {
                this.lifecycleStateField = value;
            }
        }
    }
}

