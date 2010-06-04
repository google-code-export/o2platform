namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class CreateDBSecurityGroupRequest
    {
        private string DBSecurityGroupDescriptionField;
        private string DBSecurityGroupNameField;

        public bool IsSetDBSecurityGroupDescription()
        {
            return (this.DBSecurityGroupDescriptionField != null);
        }

        public bool IsSetDBSecurityGroupName()
        {
            return (this.DBSecurityGroupNameField != null);
        }

        public CreateDBSecurityGroupRequest WithDBSecurityGroupDescription(string DBSecurityGroupDescription)
        {
            this.DBSecurityGroupDescriptionField = DBSecurityGroupDescription;
            return this;
        }

        public CreateDBSecurityGroupRequest WithDBSecurityGroupName(string DBSecurityGroupName)
        {
            this.DBSecurityGroupNameField = DBSecurityGroupName;
            return this;
        }

        [XmlElement(ElementName="DBSecurityGroupDescription")]
        public string DBSecurityGroupDescription
        {
            get
            {
                return this.DBSecurityGroupDescriptionField;
            }
            set
            {
                this.DBSecurityGroupDescriptionField = value;
            }
        }

        [XmlElement(ElementName="DBSecurityGroupName")]
        public string DBSecurityGroupName
        {
            get
            {
                return this.DBSecurityGroupNameField;
            }
            set
            {
                this.DBSecurityGroupNameField = value;
            }
        }
    }
}

