namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeCustomerGatewaysRequest
    {
        private List<string> customerGatewayIdField;
        private List<Amazon.EC2.Model.Filter> filterField;

        public bool IsSetCustomerGatewayId()
        {
            return (this.CustomerGatewayId.Count > 0);
        }

        public bool IsSetFilter()
        {
            return (this.Filter.Count > 0);
        }

        public DescribeCustomerGatewaysRequest WithCustomerGatewayId(params string[] list)
        {
            foreach (string str in list)
            {
                this.CustomerGatewayId.Add(str);
            }
            return this;
        }

        public DescribeCustomerGatewaysRequest WithFilter(params Amazon.EC2.Model.Filter[] list)
        {
            foreach (Amazon.EC2.Model.Filter filter in list)
            {
                this.Filter.Add(filter);
            }
            return this;
        }

        [XmlElement(ElementName="CustomerGatewayId")]
        public List<string> CustomerGatewayId
        {
            get
            {
                if (this.customerGatewayIdField == null)
                {
                    this.customerGatewayIdField = new List<string>();
                }
                return this.customerGatewayIdField;
            }
            set
            {
                this.customerGatewayIdField = value;
            }
        }

        [XmlElement(ElementName="Filter")]
        public List<Amazon.EC2.Model.Filter> Filter
        {
            get
            {
                if (this.filterField == null)
                {
                    this.filterField = new List<Amazon.EC2.Model.Filter>();
                }
                return this.filterField;
            }
            set
            {
                this.filterField = value;
            }
        }
    }
}

