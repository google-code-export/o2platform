namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class TerminateInstancesResult
    {
        private List<InstanceStateChange> terminatingInstanceField;

        public bool IsSetTerminatingInstance()
        {
            return (this.TerminatingInstance.Count > 0);
        }

        public TerminateInstancesResult WithTerminatingInstance(params InstanceStateChange[] list)
        {
            foreach (InstanceStateChange change in list)
            {
                this.TerminatingInstance.Add(change);
            }
            return this;
        }

        [XmlElement(ElementName="TerminatingInstance")]
        public List<InstanceStateChange> TerminatingInstance
        {
            get
            {
                if (this.terminatingInstanceField == null)
                {
                    this.terminatingInstanceField = new List<InstanceStateChange>();
                }
                return this.terminatingInstanceField;
            }
            set
            {
                this.terminatingInstanceField = value;
            }
        }
    }
}

