namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CreateCustomerGatewayResult
    {
        private Amazon.EC2.Model.CustomerGateway customerGatewayField;

        public bool IsSetCustomerGateway()
        {
            return (this.customerGatewayField != null);
        }

        public CreateCustomerGatewayResult WithCustomerGateway(Amazon.EC2.Model.CustomerGateway customerGateway)
        {
            this.customerGatewayField = customerGateway;
            return this;
        }

        [XmlElement(ElementName="CustomerGateway")]
        public Amazon.EC2.Model.CustomerGateway CustomerGateway
        {
            get
            {
                return this.customerGatewayField;
            }
            set
            {
                this.customerGatewayField = value;
            }
        }
    }
}

