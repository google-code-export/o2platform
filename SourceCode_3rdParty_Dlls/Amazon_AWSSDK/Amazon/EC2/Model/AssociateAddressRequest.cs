namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class AssociateAddressRequest
    {
        private string instanceIdField;
        private string publicIpField;

        public bool IsSetInstanceId()
        {
            return (this.instanceIdField != null);
        }

        public bool IsSetPublicIp()
        {
            return (this.publicIpField != null);
        }

        public AssociateAddressRequest WithInstanceId(string instanceId)
        {
            this.instanceIdField = instanceId;
            return this;
        }

        public AssociateAddressRequest WithPublicIp(string publicIp)
        {
            this.publicIpField = publicIp;
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

        [XmlElement(ElementName="PublicIp")]
        public string PublicIp
        {
            get
            {
                return this.publicIpField;
            }
            set
            {
                this.publicIpField = value;
            }
        }
    }
}

