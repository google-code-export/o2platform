namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeAddressesResult
    {
        private List<Amazon.EC2.Model.Address> addressField;

        public bool IsSetAddress()
        {
            return (this.Address.Count > 0);
        }

        public DescribeAddressesResult WithAddress(params Amazon.EC2.Model.Address[] list)
        {
            foreach (Amazon.EC2.Model.Address address in list)
            {
                this.Address.Add(address);
            }
            return this;
        }

        [XmlElement(ElementName="Address")]
        public List<Amazon.EC2.Model.Address> Address
        {
            get
            {
                if (this.addressField == null)
                {
                    this.addressField = new List<Amazon.EC2.Model.Address>();
                }
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }
    }
}

