namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class TerminateInstancesRequest
    {
        private List<string> instanceIdField;

        public bool IsSetInstanceId()
        {
            return (this.InstanceId.Count > 0);
        }

        public TerminateInstancesRequest WithInstanceId(params string[] list)
        {
            foreach (string str in list)
            {
                this.InstanceId.Add(str);
            }
            return this;
        }

        [XmlElement(ElementName="InstanceId")]
        public List<string> InstanceId
        {
            get
            {
                if (this.instanceIdField == null)
                {
                    this.instanceIdField = new List<string>();
                }
                return this.instanceIdField;
            }
            set
            {
                this.instanceIdField = value;
            }
        }
    }
}

