namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class StartInstancesResult
    {
        private List<InstanceStateChange> startingInstancesField;

        public bool IsSetStartingInstances()
        {
            return (this.StartingInstances.Count > 0);
        }

        public StartInstancesResult WithStartingInstances(params InstanceStateChange[] list)
        {
            foreach (InstanceStateChange change in list)
            {
                this.StartingInstances.Add(change);
            }
            return this;
        }

        [XmlElement(ElementName="StartingInstances")]
        public List<InstanceStateChange> StartingInstances
        {
            get
            {
                if (this.startingInstancesField == null)
                {
                    this.startingInstancesField = new List<InstanceStateChange>();
                }
                return this.startingInstancesField;
            }
            set
            {
                this.startingInstancesField = value;
            }
        }
    }
}

