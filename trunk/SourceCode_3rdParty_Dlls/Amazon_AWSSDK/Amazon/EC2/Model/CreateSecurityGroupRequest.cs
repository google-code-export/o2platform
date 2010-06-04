namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CreateSecurityGroupRequest
    {
        private string groupDescriptionField;
        private string groupNameField;

        public bool IsSetGroupDescription()
        {
            return (this.groupDescriptionField != null);
        }

        public bool IsSetGroupName()
        {
            return (this.groupNameField != null);
        }

        public CreateSecurityGroupRequest WithGroupDescription(string groupDescription)
        {
            this.groupDescriptionField = groupDescription;
            return this;
        }

        public CreateSecurityGroupRequest WithGroupName(string groupName)
        {
            this.groupNameField = groupName;
            return this;
        }

        [XmlElement(ElementName="GroupDescription")]
        public string GroupDescription
        {
            get
            {
                return this.groupDescriptionField;
            }
            set
            {
                this.groupDescriptionField = value;
            }
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

