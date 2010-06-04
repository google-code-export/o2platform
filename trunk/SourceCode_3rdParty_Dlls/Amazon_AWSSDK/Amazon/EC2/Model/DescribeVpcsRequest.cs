namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeVpcsRequest
    {
        private List<Amazon.EC2.Model.Filter> filterField;
        private List<string> vpcIdField;

        public bool IsSetFilter()
        {
            return (this.Filter.Count > 0);
        }

        public bool IsSetVpcId()
        {
            return (this.VpcId.Count > 0);
        }

        public DescribeVpcsRequest WithFilter(params Amazon.EC2.Model.Filter[] list)
        {
            foreach (Amazon.EC2.Model.Filter filter in list)
            {
                this.Filter.Add(filter);
            }
            return this;
        }

        public DescribeVpcsRequest WithVpcId(params string[] list)
        {
            foreach (string str in list)
            {
                this.VpcId.Add(str);
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

        [XmlElement(ElementName="VpcId")]
        public List<string> VpcId
        {
            get
            {
                if (this.vpcIdField == null)
                {
                    this.vpcIdField = new List<string>();
                }
                return this.vpcIdField;
            }
            set
            {
                this.vpcIdField = value;
            }
        }
    }
}

