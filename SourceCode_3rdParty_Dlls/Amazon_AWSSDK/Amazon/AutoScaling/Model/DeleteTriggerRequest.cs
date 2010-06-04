namespace Amazon.AutoScaling.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://autoscaling.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class DeleteTriggerRequest
    {
        private string autoScalingGroupNameField;
        private string triggerNameField;

        public bool IsSetAutoScalingGroupName()
        {
            return (this.autoScalingGroupNameField != null);
        }

        public bool IsSetTriggerName()
        {
            return (this.triggerNameField != null);
        }

        public DeleteTriggerRequest WithAutoScalingGroupName(string autoScalingGroupName)
        {
            this.autoScalingGroupNameField = autoScalingGroupName;
            return this;
        }

        public DeleteTriggerRequest WithTriggerName(string triggerName)
        {
            this.triggerNameField = triggerName;
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

        [XmlElement(ElementName="TriggerName")]
        public string TriggerName
        {
            get
            {
                return this.triggerNameField;
            }
            set
            {
                this.triggerNameField = value;
            }
        }
    }
}

