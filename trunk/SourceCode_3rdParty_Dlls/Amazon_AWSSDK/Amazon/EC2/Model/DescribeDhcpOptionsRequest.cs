namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeDhcpOptionsRequest
    {
        private List<string> dhcpOptionsIdField;

        public bool IsSetDhcpOptionsId()
        {
            return (this.DhcpOptionsId.Count > 0);
        }

        public DescribeDhcpOptionsRequest WithDhcpOptionsId(params string[] list)
        {
            foreach (string str in list)
            {
                this.DhcpOptionsId.Add(str);
            }
            return this;
        }

        [XmlElement(ElementName="DhcpOptionsId")]
        public List<string> DhcpOptionsId
        {
            get
            {
                if (this.dhcpOptionsIdField == null)
                {
                    this.dhcpOptionsIdField = new List<string>();
                }
                return this.dhcpOptionsIdField;
            }
            set
            {
                this.dhcpOptionsIdField = value;
            }
        }
    }
}

