namespace Amazon.AutoScaling.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://autoscaling.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class SetDesiredCapacityRequest
    {
        private string autoScalingGroupNameField;
        private decimal? desiredCapacityField;

        public bool IsSetAutoScalingGroupName()
        {
            return (this.autoScalingGroupNameField != null);
        }

        public bool IsSetDesiredCapacity()
        {
            return this.desiredCapacityField.HasValue;
        }

        public SetDesiredCapacityRequest WithAutoScalingGroupName(string autoScalingGroupName)
        {
            this.autoScalingGroupNameField = autoScalingGroupName;
            return this;
        }

        public SetDesiredCapacityRequest WithDesiredCapacity(decimal desiredCapacity)
        {
            this.desiredCapacityField = new decimal?(desiredCapacity);
            return this;
        }

        [XmlElement(ElementName="AutoScalingGroupName")]
        public string AutoScalingGroupName
        {
            get
            {
                return this.autoScalingGroupNameField;
            }
            set
            {
                this.autoScalingGroupNameField = value;
            }
        }

        [XmlElement(ElementName="DesiredCapacity")]
        public decimal DesiredCapacity
        {
            get
            {
                return this.desiredCapacityField.GetValueOrDefault();
            }
            set
            {
                this.desiredCapacityField = new decimal?(value);
            }
        }
    }
}

