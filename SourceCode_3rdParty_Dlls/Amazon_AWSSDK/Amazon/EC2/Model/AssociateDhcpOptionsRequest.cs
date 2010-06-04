namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class AssociateDhcpOptionsRequest
    {
        private string dhcpOptionsIdField;
        private string vpcIdField;

        public bool IsSetDhcpOptionsId()
        {
            return (this.dhcpOptionsIdField != null);
        }

        public bool IsSetVpcId()
        {
            return (this.vpcIdField != null);
        }

        public AssociateDhcpOptionsRequest WithDhcpOptionsId(string dhcpOptionsId)
        {
            this.dhcpOptionsIdField = dhcpOptionsId;
            return this;
        }

        public AssociateDhcpOptionsRequest WithVpcId(string vpcId)
        {
            this.vpcIdField = vpcId;
            return this;
        }

        [XmlElement(ElementName="DhcpOptionsId")]
        public string DhcpOptionsId
        {
            get
            {
                return this.dhcpOptionsIdField;
            }
            set
            {
                this.dhcpOptionsIdField = value;
            }
        }

        [XmlElement(ElementName="VpcId")]
        public string VpcId
        {
            get
            {
                return this.vpcIdField;
            }
            set
            {
                this.vpcIdField = value;
            }
        }
    }
}

