namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DeleteDBSecurityGroupRequest
    {
        private string DBSecurityGroupNameField;

        public bool IsSetDBSecurityGroupName()
        {
            return (this.DBSecurityGroupNameField != null);
        }

        public DeleteDBSecurityGroupRequest WithDBSecurityGroupName(string DBSecurityGroupName)
        {
            this.DBSecurityGroupNameField = DBSecurityGroupName;
            return this;
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

