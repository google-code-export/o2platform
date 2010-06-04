namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DeleteSubnetRequest
    {
        private string subnetIdField;

        public bool IsSetSubnetId()
        {
            return (this.subnetIdField != null);
        }

        public DeleteSubnetRequest WithSubnetId(string subnetId)
        {
            this.subnetIdField = subnetId;
            return this;
        }

        [XmlElement(ElementName="SubnetId")]
        public string SubnetId
        {
            get
            {
                return this.subnetIdField;
            }
            set
            {
                this.subnetIdField = value;
            }
        }
    }
}

