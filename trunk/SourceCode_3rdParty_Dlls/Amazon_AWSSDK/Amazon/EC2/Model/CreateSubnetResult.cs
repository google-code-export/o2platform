namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CreateSubnetResult
    {
        private Amazon.EC2.Model.Subnet subnetField;

        public bool IsSetSubnet()
        {
            return (this.subnetField != null);
        }

        public CreateSubnetResult WithSubnet(Amazon.EC2.Model.Subnet subnet)
        {
            this.subnetField = subnet;
            return this;
        }

        [XmlElement(ElementName="Subnet")]
        public Amazon.EC2.Model.Subnet Subnet
        {
            get
            {
                return this.subnetField;
            }
            set
            {
                this.subnetField = value;
            }
        }
    }
}

