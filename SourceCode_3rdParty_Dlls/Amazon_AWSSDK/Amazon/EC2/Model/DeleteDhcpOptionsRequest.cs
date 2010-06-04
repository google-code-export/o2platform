namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DeleteDhcpOptionsRequest
    {
        private string dhcpOptionsIdField;

        public bool IsSetDhcpOptionsId()
        {
            return (this.dhcpOptionsIdField != null);
        }

        public DeleteDhcpOptionsRequest WithDhcpOptionsId(string dhcpOptionsId)
        {
            this.dhcpOptionsIdField = dhcpOptionsId;
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
    }
}

