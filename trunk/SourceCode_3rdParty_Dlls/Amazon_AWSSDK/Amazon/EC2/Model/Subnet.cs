namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class Subnet
    {
        private string availabilityZoneField;
        private decimal? availableIpAddressCountField;
        private string cidrBlockField;
        private string subnetIdField;
        private string subnetStateField;
        private string vpcIdField;

        public bool IsSetAvailabilityZone()
        {
            return (this.availabilityZoneField != null);
        }

        public bool IsSetAvailableIpAddressCount()
        {
            return this.availableIpAddressCountField.HasValue;
        }

        public bool IsSetCidrBlock()
        {
            return (this.cidrBlockField != null);
        }

        public bool IsSetSubnetId()
        {
            return (this.subnetIdField != null);
        }

        public bool IsSetSubnetState()
        {
            return (this.subnetStateField != null);
        }

        public bool IsSetVpcId()
        {
            return (this.vpcIdField != null);
        }

        public Subnet WithAvailabilityZone(string availabilityZone)
        {
            this.availabilityZoneField = availabilityZone;
            return this;
        }

        public Subnet WithAvailableIpAddressCount(decimal availableIpAddressCount)
        {
            this.availableIpAddressCountField = new decimal?(availableIpAddressCount);
            return this;
        }

        public Subnet WithCidrBlock(string cidrBlock)
        {
            this.cidrBlockField = cidrBlock;
            return this;
        }

        public Subnet WithSubnetId(string subnetId)
        {
            this.subnetIdField = subnetId;
            return this;
        }

        public Subnet WithSubnetState(string subnetState)
        {
            this.subnetStateField = subnetState;
            return this;
        }

        public Subnet WithVpcId(string vpcId)
        {
            this.vpcIdField = vpcId;
            return this;
        }

        [XmlElement(ElementName="AvailabilityZone")]
        public string AvailabilityZone
        {
            get
            {
                return this.availabilityZoneField;
            }
            set
            {
                this.availabilityZoneField = value;
            }
        }

        [XmlElement(ElementName="AvailableIpAddressCount")]
        public decimal AvailableIpAddressCount
        {
            get
            {
                return this.availableIpAddressCountField.GetValueOrDefault();
            }
            set
            {
                this.availableIpAddressCountField = new decimal?(value);
            }
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

        [XmlElement(ElementName="SubnetState")]
        public string SubnetState
        {
            get
            {
                return this.subnetStateField;
            }
            set
            {
                this.subnetStateField = value;
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

