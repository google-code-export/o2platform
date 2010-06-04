namespace Amazon.AutoScaling.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://autoscaling.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class DescribeAutoScalingGroupsResult
    {
        private List<AutoScalingGroup> autoScalingGroupsField;

        public bool IsSetAutoScalingGroups()
        {
            return (this.AutoScalingGroups.Count > 0);
        }

        public DescribeAutoScalingGroupsResult WithAutoScalingGroups(params AutoScalingGroup[] list)
        {
            foreach (AutoScalingGroup group in list)
            {
                this.AutoScalingGroups.Add(group);
            }
            return this;
        }

        [XmlElement(ElementName="AutoScalingGroups")]
        public List<AutoScalingGroup> AutoScalingGroups
        {
            get
            {
                if (this.autoScalingGroupsField == null)
                {
                    this.autoScalingGroupsField = new List<AutoScalingGroup>();
                }
                return this.autoScalingGroupsField;
            }
            set
            {
                this.autoScalingGroupsField = value;
            }
        }
    }
}

