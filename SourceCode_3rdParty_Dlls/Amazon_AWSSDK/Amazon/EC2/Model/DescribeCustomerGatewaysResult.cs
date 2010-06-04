namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeCustomerGatewaysResult
    {
        private List<Amazon.EC2.Model.CustomerGateway> customerGatewayField;

        public bool IsSetCustomerGateway()
        {
            return (this.CustomerGateway.Count > 0);
        }

        public DescribeCustomerGatewaysResult WithCustomerGateway(params Amazon.EC2.Model.CustomerGateway[] list)
        {
            foreach (Amazon.EC2.Model.CustomerGateway gateway in list)
            {
                this.CustomerGateway.Add(gateway);
            }
            return this;
        }

        [XmlElement(ElementName="CustomerGateway")]
        public List<Amazon.EC2.Model.CustomerGateway> CustomerGateway
        {
            get
            {
                if (this.customerGatewayField == null)
                {
                    this.customerGatewayField = new List<Amazon.EC2.Model.CustomerGateway>();
                }
                return this.customerGatewayField;
            }
            set
            {
                this.customerGatewayField = value;
            }
        }
    }
}

