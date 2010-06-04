namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeVpcsResult
    {
        private List<Amazon.EC2.Model.Vpc> vpcField;

        public bool IsSetVpc()
        {
            return (this.Vpc.Count > 0);
        }

        public DescribeVpcsResult WithVpc(params Amazon.EC2.Model.Vpc[] list)
        {
            foreach (Amazon.EC2.Model.Vpc vpc in list)
            {
                this.Vpc.Add(vpc);
            }
            return this;
        }

        [XmlElement(ElementName="Vpc")]
        public List<Amazon.EC2.Model.Vpc> Vpc
        {
            get
            {
                if (this.vpcField == null)
                {
                    this.vpcField = new List<Amazon.EC2.Model.Vpc>();
                }
                return this.vpcField;
            }
            set
            {
                this.vpcField = value;
            }
        }
    }
}

