namespace Amazon.AutoScaling.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://autoscaling.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class TerminateInstanceInAutoScalingGroupRequest
    {
        private string instanceIdField;
        private bool? shouldDecrementDesiredCapacityField;

        public bool IsSetInstanceId()
        {
            return (this.instanceIdField != null);
        }

        public bool IsSetShouldDecrementDesiredCapacity()
        {
            return this.shouldDecrementDesiredCapacityField.HasValue;
        }

        public TerminateInstanceInAutoScalingGroupRequest WithInstanceId(string instanceId)
        {
            this.instanceIdField = instanceId;
            return this;
        }

        public TerminateInstanceInAutoScalingGroupRequest WithShouldDecrementDesiredCapacity(bool shouldDecrementDesiredCapacity)
        {
            this.shouldDecrementDesiredCapacityField = new bool?(shouldDecrementDesiredCapacity);
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

        [XmlElement(ElementName="ShouldDecrementDesiredCapacity")]
        public bool ShouldDecrementDesiredCapacity
        {
            get
            {
                return this.shouldDecrementDesiredCapacityField.GetValueOrDefault();
            }
            set
            {
                this.shouldDecrementDesiredCapacityField = new bool?(value);
            }
        }
    }
}

