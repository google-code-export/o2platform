namespace Amazon.AutoScaling.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://autoscaling.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class CreateAutoScalingGroupRequest
    {
        private string autoScalingGroupNameField;
        private List<string> availabilityZonesField;
        private decimal? cooldownField;
        private string launchConfigurationNameField;
        private List<string> loadBalancerNamesField;
        private decimal? maxSizeField;
        private decimal? minSizeField;

        public bool IsSetAutoScalingGroupName()
        {
            return (this.autoScalingGroupNameField != null);
        }

        public bool IsSetAvailabilityZones()
        {
            return (this.AvailabilityZones.Count > 0);
        }

        public bool IsSetCooldown()
        {
            return this.cooldownField.HasValue;
        }

        public bool IsSetLaunchConfigurationName()
        {
            return (this.launchConfigurationNameField != null);
        }

        public bool IsSetLoadBalancerNames()
        {
            return (this.LoadBalancerNames.Count > 0);
        }

        public bool IsSetMaxSize()
        {
            return this.maxSizeField.HasValue;
        }

        public bool IsSetMinSize()
        {
            return this.minSizeField.HasValue;
        }

        public CreateAutoScalingGroupRequest WithAutoScalingGroupName(string autoScalingGroupName)
        {
            this.autoScalingGroupNameField = autoScalingGroupName;
            return this;
        }

        public CreateAutoScalingGroupRequest WithAvailabilityZones(params string[] list)
        {
            foreach (string str in list)
            {
                this.AvailabilityZones.Add(str);
            }
            return this;
        }

        public CreateAutoScalingGroupRequest WithCooldown(decimal cooldown)
        {
            this.cooldownField = new decimal?(cooldown);
            return this;
        }

        public CreateAutoScalingGroupRequest WithLaunchConfigurationName(string launchConfigurationName)
        {
            this.launchConfigurationNameField = launchConfigurationName;
            return this;
        }

        public CreateAutoScalingGroupRequest WithLoadBalancerNames(params string[] list)
        {
            foreach (string str in list)
            {
                this.LoadBalancerNames.Add(str);
            }
            return this;
        }

        public CreateAutoScalingGroupRequest WithMaxSize(decimal maxSize)
        {
            this.maxSizeField = new decimal?(maxSize);
            return this;
        }

        public CreateAutoScalingGroupRequest WithMinSize(decimal minSize)
        {
            this.minSizeField = new decimal?(minSize);
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

        [XmlElement(ElementName="AvailabilityZones")]
        public List<string> AvailabilityZones
        {
            get
            {
                if (this.availabilityZonesField == null)
                {
                    this.availabilityZonesField = new List<string>();
                }
                return this.availabilityZonesField;
            }
            set
            {
                this.availabilityZonesField = value;
            }
        }

        [XmlElement(ElementName="Cooldown")]
        public decimal Cooldown
        {
            get
            {
                return this.cooldownField.GetValueOrDefault();
            }
            set
            {
                this.cooldownField = new decimal?(value);
            }
        }

        [XmlElement(ElementName="LaunchConfigurationName")]
        public string LaunchConfigurationName
        {
            get
            {
                return this.launchConfigurationNameField;
            }
            set
            {
                this.launchConfigurationNameField = value;
            }
        }

        [XmlElement(ElementName="LoadBalancerNames")]
        public List<string> LoadBalancerNames
        {
            get
            {
                if (this.loadBalancerNamesField == null)
                {
                    this.loadBalancerNamesField = new List<string>();
                }
                return this.loadBalancerNamesField;
            }
            set
            {
                this.loadBalancerNamesField = value;
            }
        }

        [XmlElement(ElementName="MaxSize")]
        public decimal MaxSize
        {
            get
            {
                return this.maxSizeField.GetValueOrDefault();
            }
            set
            {
                this.maxSizeField = new decimal?(value);
            }
        }

        [XmlElement(ElementName="MinSize")]
        public decimal MinSize
        {
            get
            {
                return this.minSizeField.GetValueOrDefault();
            }
            set
            {
                this.minSizeField = new decimal?(value);
            }
        }
    }
}

