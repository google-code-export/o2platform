namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CreateDhcpOptionsResult
    {
        private Amazon.EC2.Model.DhcpOptions dhcpOptionsField;

        public bool IsSetDhcpOptions()
        {
            return (this.dhcpOptionsField != null);
        }

        public CreateDhcpOptionsResult WithDhcpOptions(Amazon.EC2.Model.DhcpOptions dhcpOptions)
        {
            this.dhcpOptionsField = dhcpOptions;
            return this;
        }

        [XmlElement(ElementName="DhcpOptions")]
        public Amazon.EC2.Model.DhcpOptions DhcpOptions
        {
            get
            {
                return this.dhcpOptionsField;
            }
            set
            {
                this.dhcpOptionsField = value;
            }
        }
    }
}

