namespace Amazon.AutoScaling.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://autoscaling.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class DescribeAutoScalingGroupsRequest
    {
        private List<string> autoScalingGroupNamesField;

        public bool IsSetAutoScalingGroupNames()
        {
            return (this.AutoScalingGroupNames.Count > 0);
        }

        public DescribeAutoScalingGroupsRequest WithAutoScalingGroupNames(params string[] list)
        {
            foreach (string str in list)
            {
                this.AutoScalingGroupNames.Add(str);
            }
            return this;
        }

        [XmlElement(ElementName="AutoScalingGroupNames")]
        public List<string> AutoScalingGroupNames
        {
            get
            {
                if (this.autoScalingGroupNamesField == null)
                {
                    this.autoScalingGroupNamesField = new List<string>();
                }
                return this.autoScalingGroupNamesField;
            }
            set
            {
                this.autoScalingGroupNamesField = value;
            }
        }
    }
}

