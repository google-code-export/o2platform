namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeSecurityGroupsResult
    {
        private List<Amazon.EC2.Model.SecurityGroup> securityGroupField;

        public bool IsSetSecurityGroup()
        {
            return (this.SecurityGroup.Count > 0);
        }

        public DescribeSecurityGroupsResult WithSecurityGroup(params Amazon.EC2.Model.SecurityGroup[] list)
        {
            foreach (Amazon.EC2.Model.SecurityGroup group in list)
            {
                this.SecurityGroup.Add(group);
            }
            return this;
        }

        [XmlElement(ElementName="SecurityGroup")]
        public List<Amazon.EC2.Model.SecurityGroup> SecurityGroup
        {
            get
            {
                if (this.securityGroupField == null)
                {
                    this.securityGroupField = new List<Amazon.EC2.Model.SecurityGroup>();
                }
                return this.securityGroupField;
            }
            set
            {
                this.securityGroupField = value;
            }
        }
    }
}

