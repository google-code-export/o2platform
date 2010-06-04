namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DeleteSecurityGroupRequest
    {
        private string groupNameField;

        public bool IsSetGroupName()
        {
            return (this.groupNameField != null);
        }

        public DeleteSecurityGroupRequest WithGroupName(string groupName)
        {
            this.groupNameField = groupName;
            return this;
        }

        [XmlElement(ElementName="GroupName")]
        public string GroupName
        {
            get
            {
                return this.groupNameField;
            }
            set
            {
                this.groupNameField = value;
            }
        }
    }
}

