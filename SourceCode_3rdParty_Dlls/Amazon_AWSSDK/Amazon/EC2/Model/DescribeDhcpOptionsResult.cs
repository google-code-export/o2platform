namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeDhcpOptionsResult
    {
        private List<Amazon.EC2.Model.DhcpOptions> dhcpOptionsField;

        public bool IsSetDhcpOptions()
        {
            return (this.DhcpOptions.Count > 0);
        }

        public DescribeDhcpOptionsResult WithDhcpOptions(params Amazon.EC2.Model.DhcpOptions[] list)
        {
            foreach (Amazon.EC2.Model.DhcpOptions options in list)
            {
                this.DhcpOptions.Add(options);
            }
            return this;
        }

        [XmlElement(ElementName="DhcpOptions")]
        public List<Amazon.EC2.Model.DhcpOptions> DhcpOptions
        {
            get
            {
                if (this.dhcpOptionsField == null)
                {
                    this.dhcpOptionsField = new List<Amazon.EC2.Model.DhcpOptions>();
                }
                return this.dhcpOptionsField;
            }
            set
            {
                this.dhcpOptionsField = value;
            }
        }
    }
}

