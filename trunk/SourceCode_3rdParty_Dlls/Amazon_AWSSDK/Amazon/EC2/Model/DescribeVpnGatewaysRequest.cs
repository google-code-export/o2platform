namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeVpnGatewaysRequest
    {
        private List<Amazon.EC2.Model.Filter> filterField;
        private List<string> vpnGatewayIdField;

        public bool IsSetFilter()
        {
            return (this.Filter.Count > 0);
        }

        public bool IsSetVpnGatewayId()
        {
            return (this.VpnGatewayId.Count > 0);
        }

        public DescribeVpnGatewaysRequest WithFilter(params Amazon.EC2.Model.Filter[] list)
        {
            foreach (Amazon.EC2.Model.Filter filter in list)
            {
                this.Filter.Add(filter);
            }
            return this;
        }

        public DescribeVpnGatewaysRequest WithVpnGatewayId(params string[] list)
        {
            foreach (string str in list)
            {
                this.VpnGatewayId.Add(str);
            }
            return this;
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

        [XmlElement(ElementName="VpnGatewayId")]
        public List<string> VpnGatewayId
        {
            get
            {
                if (this.vpnGatewayIdField == null)
                {
                    this.vpnGatewayIdField = new List<string>();
                }
                return this.vpnGatewayIdField;
            }
            set
            {
                this.vpnGatewayIdField = value;
            }
        }
    }
}

