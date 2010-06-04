namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DhcpOptions
    {
        private List<DhcpConfiguration> configurationField;
        private string dhcpOptionsIdField;

        public bool IsSetConfiguration()
        {
            return (this.Configuration.Count > 0);
        }

        public bool IsSetDhcpOptionsId()
        {
            return (this.dhcpOptionsIdField != null);
        }

        public DhcpOptions WithConfiguration(params DhcpConfiguration[] list)
        {
            foreach (DhcpConfiguration configuration in list)
            {
                this.Configuration.Add(configuration);
            }
            return this;
        }

        public DhcpOptions WithDhcpOptionsId(string dhcpOptionsId)
        {
            this.dhcpOptionsIdField = dhcpOptionsId;
            return this;
        }

        [XmlElement(ElementName="Configuration")]
        public List<DhcpConfiguration> Configuration
        {
            get
            {
                if (this.configurationField == null)
                {
                    this.configurationField = new List<DhcpConfiguration>();
                }
                return this.configurationField;
            }
            set
            {
                this.configurationField = value;
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
    }
}

