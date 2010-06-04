namespace Amazon.AutoScaling.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://autoscaling.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class TerminateInstanceInAutoScalingGroupResult
    {
        private Amazon.AutoScaling.Model.Activity activityField;

        public bool IsSetActivity()
        {
            return (this.activityField != null);
        }

        public TerminateInstanceInAutoScalingGroupResult WithActivity(Amazon.AutoScaling.Model.Activity activity)
        {
            this.activityField = activity;
            return this;
        }

        [XmlElement(ElementName="Activity")]
        public Amazon.AutoScaling.Model.Activity Activity
        {
            get
            {
                return this.activityField;
            }
            set
            {
                this.activityField = value;
            }
        }
    }
}

