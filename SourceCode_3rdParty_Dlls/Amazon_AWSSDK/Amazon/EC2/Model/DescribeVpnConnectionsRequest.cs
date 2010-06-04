namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeVpnConnectionsRequest
    {
        private List<Amazon.EC2.Model.Filter> filterField;
        private List<string> vpnConnectionIdField;

        public bool IsSetFilter()
        {
            return (this.Filter.Count > 0);
        }

        public bool IsSetVpnConnectionId()
        {
            return (this.VpnConnectionId.Count > 0);
        }

        public DescribeVpnConnectionsRequest WithFilter(params Amazon.EC2.Model.Filter[] list)
        {
            foreach (Amazon.EC2.Model.Filter filter in list)
            {
                this.Filter.Add(filter);
            }
            return this;
        }

        public DescribeVpnConnectionsRequest WithVpnConnectionId(params string[] list)
        {
            foreach (string str in list)
            {
                this.VpnConnectionId.Add(str);
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

        [XmlElement(ElementName="VpnConnectionId")]
        public List<string> VpnConnectionId
        {
            get
            {
                if (this.vpnConnectionIdField == null)
                {
                    this.vpnConnectionIdField = new List<string>();
                }
                return this.vpnConnectionIdField;
            }
            set
            {
                this.vpnConnectionIdField = value;
            }
        }
    }
}

