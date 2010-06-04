namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeSubnetsResult
    {
        private List<Amazon.EC2.Model.Subnet> subnetField;

        public bool IsSetSubnet()
        {
            return (this.Subnet.Count > 0);
        }

        public DescribeSubnetsResult WithSubnet(params Amazon.EC2.Model.Subnet[] list)
        {
            foreach (Amazon.EC2.Model.Subnet subnet in list)
            {
                this.Subnet.Add(subnet);
            }
            return this;
        }

        [XmlElement(ElementName="Subnet")]
        public List<Amazon.EC2.Model.Subnet> Subnet
        {
            get
            {
                if (this.subnetField == null)
                {
                    this.subnetField = new List<Amazon.EC2.Model.Subnet>();
                }
                return this.subnetField;
            }
            set
            {
                this.subnetField = value;
            }
        }
    }
}

