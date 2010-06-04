namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class StopInstancesResult
    {
        private List<InstanceStateChange> stoppingInstancesField;

        public bool IsSetStoppingInstances()
        {
            return (this.StoppingInstances.Count > 0);
        }

        public StopInstancesResult WithStoppingInstances(params InstanceStateChange[] list)
        {
            foreach (InstanceStateChange change in list)
            {
                this.StoppingInstances.Add(change);
            }
            return this;
        }

        [XmlElement(ElementName="StoppingInstances")]
        public List<InstanceStateChange> StoppingInstances
        {
            get
            {
                if (this.stoppingInstancesField == null)
                {
                    this.stoppingInstancesField = new List<InstanceStateChange>();
                }
                return this.stoppingInstancesField;
            }
            set
            {
                this.stoppingInstancesField = value;
            }
        }
    }
}

