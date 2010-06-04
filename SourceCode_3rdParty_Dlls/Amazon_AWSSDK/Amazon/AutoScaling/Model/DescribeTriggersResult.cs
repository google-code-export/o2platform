namespace Amazon.AutoScaling.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://autoscaling.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class DescribeTriggersResult
    {
        private List<Trigger> triggersField;

        public bool IsSetTriggers()
        {
            return (this.Triggers.Count > 0);
        }

        public DescribeTriggersResult WithTriggers(params Trigger[] list)
        {
            foreach (Trigger trigger in list)
            {
                this.Triggers.Add(trigger);
            }
            return this;
        }

        [XmlElement(ElementName="Triggers")]
        public List<Trigger> Triggers
        {
            get
            {
                if (this.triggersField == null)
                {
                    this.triggersField = new List<Trigger>();
                }
                return this.triggersField;
            }
            set
            {
                this.triggersField = value;
            }
        }
    }
}

