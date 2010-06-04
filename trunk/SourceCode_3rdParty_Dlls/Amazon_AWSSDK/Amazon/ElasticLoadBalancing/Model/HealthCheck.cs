namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class HealthCheck
    {
        private decimal? healthyThresholdField;
        private decimal? intervalField;
        private string targetField;
        private decimal? timeoutField;
        private decimal? unhealthyThresholdField;

        public bool IsSetHealthyThreshold()
        {
            return this.healthyThresholdField.HasValue;
        }

        public bool IsSetInterval()
        {
            return this.intervalField.HasValue;
        }

        public bool IsSetTarget()
        {
            return (this.targetField != null);
        }

        public bool IsSetTimeout()
        {
            return this.timeoutField.HasValue;
        }

        public bool IsSetUnhealthyThreshold()
        {
            return this.unhealthyThresholdField.HasValue;
        }

        public HealthCheck WithHealthyThreshold(decimal healthyThreshold)
        {
            this.healthyThresholdField = new decimal?(healthyThreshold);
            return this;
        }

        public HealthCheck WithInterval(decimal interval)
        {
            this.intervalField = new decimal?(interval);
            return this;
        }

        public HealthCheck WithTarget(string target)
        {
            this.targetField = target;
            return this;
        }

        public HealthCheck WithTimeout(decimal timeout)
        {
            this.timeoutField = new decimal?(timeout);
            return this;
        }

        public HealthCheck WithUnhealthyThreshold(decimal unhealthyThreshold)
        {
            this.unhealthyThresholdField = new decimal?(unhealthyThreshold);
            return this;
        }

        [XmlElement(ElementName="HealthyThreshold")]
        public decimal HealthyThreshold
        {
            get
            {
                return this.healthyThresholdField.GetValueOrDefault();
            }
            set
            {
                this.healthyThresholdField = new decimal?(value);
            }
        }

        [XmlElement(ElementName="Interval")]
        public decimal Interval
        {
            get
            {
                return this.intervalField.GetValueOrDefault();
            }
            set
            {
                this.intervalField = new decimal?(value);
            }
        }

        [XmlElement(ElementName="Target")]
        public string Target
        {
            get
            {
                return this.targetField;
            }
            set
            {
                this.targetField = value;
            }
        }

        [XmlElement(ElementName="Timeout")]
        public decimal Timeout
        {
            get
            {
                return this.timeoutField.GetValueOrDefault();
            }
            set
            {
                this.timeoutField = new decimal?(value);
            }
        }

        [XmlElement(ElementName="UnhealthyThreshold")]
        public decimal UnhealthyThreshold
        {
            get
            {
                return this.unhealthyThresholdField.GetValueOrDefault();
            }
            set
            {
                this.unhealthyThresholdField = new decimal?(value);
            }
        }
    }
}

