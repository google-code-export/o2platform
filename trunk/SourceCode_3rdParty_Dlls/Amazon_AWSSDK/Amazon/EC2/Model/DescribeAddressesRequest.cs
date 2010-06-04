namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeAddressesRequest
    {
        private List<string> publicIpField;

        public bool IsSetPublicIp()
        {
            return (this.PublicIp.Count > 0);
        }

        public DescribeAddressesRequest WithPublicIp(params string[] list)
        {
            foreach (string str in list)
            {
                this.PublicIp.Add(str);
            }
            return this;
        }

        [XmlElement(ElementName="PublicIp")]
        public List<string> PublicIp
        {
            get
            {
                if (this.publicIpField == null)
                {
                    this.publicIpField = new List<string>();
                }
                return this.publicIpField;
            }
            set
            {
                this.publicIpField = value;
            }
        }
    }
}

