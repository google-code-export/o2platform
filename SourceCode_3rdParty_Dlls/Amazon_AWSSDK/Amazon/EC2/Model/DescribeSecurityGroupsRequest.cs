namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeSecurityGroupsRequest
    {
        private List<string> groupNameField;

        public bool IsSetGroupName()
        {
            return (this.GroupName.Count > 0);
        }

        public DescribeSecurityGroupsRequest WithGroupName(params string[] list)
        {
            foreach (string str in list)
            {
                this.GroupName.Add(str);
            }
            return this;
        }

        [XmlElement(ElementName="GroupName")]
        public List<string> GroupName
        {
            get
            {
                if (this.groupNameField == null)
                {
                    this.groupNameField = new List<string>();
                }
                return this.groupNameField;
            }
            set
            {
                this.groupNameField = value;
            }
        }
    }
}

