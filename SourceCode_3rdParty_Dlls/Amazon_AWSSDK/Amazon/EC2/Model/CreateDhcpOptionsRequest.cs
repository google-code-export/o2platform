namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CreateDhcpOptionsRequest
    {
        private List<Amazon.EC2.Model.DhcpConfiguration> dhcpConfigurationField;

        public bool IsSetDhcpConfiguration()
        {
            return (this.DhcpConfiguration.Count > 0);
        }

        public CreateDhcpOptionsRequest WithDhcpConfiguration(params Amazon.EC2.Model.DhcpConfiguration[] list)
        {
            foreach (Amazon.EC2.Model.DhcpConfiguration configuration in list)
            {
                this.DhcpConfiguration.Add(configuration);
            }
            return this;
        }

        [XmlElement(ElementName="DhcpConfiguration")]
        public List<Amazon.EC2.Model.DhcpConfiguration> DhcpConfiguration
        {
            get
            {
                if (this.dhcpConfigurationField == null)
                {
                    this.dhcpConfigurationField = new List<Amazon.EC2.Model.DhcpConfiguration>();
                }
                return this.dhcpConfigurationField;
            }
            set
            {
                this.dhcpConfigurationField = value;
            }
        }
    }
}

