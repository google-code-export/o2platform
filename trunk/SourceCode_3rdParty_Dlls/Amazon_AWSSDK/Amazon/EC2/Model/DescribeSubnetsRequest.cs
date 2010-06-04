namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeSubnetsRequest
    {
        private List<Amazon.EC2.Model.Filter> filterField;
        private List<string> subnetIdField;

        public bool IsSetFilter()
        {
            return (this.Filter.Count > 0);
        }

        public bool IsSetSubnetId()
        {
            return (this.SubnetId.Count > 0);
        }

        public DescribeSubnetsRequest WithFilter(params Amazon.EC2.Model.Filter[] list)
        {
            foreach (Amazon.EC2.Model.Filter filter in list)
            {
                this.Filter.Add(filter);
            }
            return this;
        }

        public DescribeSubnetsRequest WithSubnetId(params string[] list)
        {
            foreach (string str in list)
            {
                this.SubnetId.Add(str);
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

        [XmlElement(ElementName="SubnetId")]
        public List<string> SubnetId
        {
            get
            {
                if (this.subnetIdField == null)
                {
                    this.subnetIdField = new List<string>();
                }
                return this.subnetIdField;
            }
            set
            {
                this.subnetIdField = value;
            }
        }
    }
}

