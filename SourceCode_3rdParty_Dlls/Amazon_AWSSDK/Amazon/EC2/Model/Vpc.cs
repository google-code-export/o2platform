namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class Vpc
    {
        private string cidrBlockField;
        private string dhcpOptionsIdField;
        private string vpcIdField;
        private string vpcStateField;

        public bool IsSetCidrBlock()
        {
            return (this.cidrBlockField != null);
        }

        public bool IsSetDhcpOptionsId()
        {
            return (this.dhcpOptionsIdField != null);
        }

        public bool IsSetVpcId()
        {
            return (this.vpcIdField != null);
        }

        public bool IsSetVpcState()
        {
            return (this.vpcStateField != null);
        }

        public Vpc WithCidrBlock(string cidrBlock)
        {
            this.cidrBlockField = cidrBlock;
            return this;
        }

        public Vpc WithDhcpOptionsId(string dhcpOptionsId)
        {
            this.dhcpOptionsIdField = dhcpOptionsId;
            return this;
        }

        public Vpc WithVpcId(string vpcId)
        {
            this.vpcIdField = vpcId;
            return this;
        }

        public Vpc WithVpcState(string vpcState)
        {
            this.vpcStateField = vpcState;
            return this;
        }

        [XmlElement(ElementName="CidrBlock")]
        public string CidrBlock
        {
            get
            {
                return this.cidrBlockField;
            }
            set
            {
                this.cidrBlockField = value;
            }
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

        [XmlElement(ElementName="VpcState")]
        public string VpcState
        {
            get
            {
                return this.vpcStateField;
            }
            set
            {
                this.vpcStateField = value;
            }
        }
    }
}

