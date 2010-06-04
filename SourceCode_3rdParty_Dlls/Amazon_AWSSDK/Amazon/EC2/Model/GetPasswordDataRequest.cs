namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class GetPasswordDataRequest
    {
        private string instanceIdField;

        public bool IsSetInstanceId()
        {
            return (this.instanceIdField != null);
        }

        public GetPasswordDataRequest WithInstanceId(string instanceId)
        {
            this.instanceIdField = instanceId;
            return this;
        }

        [XmlElement(ElementName="InstanceId")]
        public string InstanceId
        {
            get
            {
                return this.instanceIdField;
            }
            set
            {
                this.instanceIdField = value;
            }
        }
    }
}

