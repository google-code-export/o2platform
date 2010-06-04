namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class RevokeDBSecurityGroupIngressResult
    {
        private Amazon.RDS.Model.DBSecurityGroup DBSecurityGroupField;

        public bool IsSetDBSecurityGroup()
        {
            return (this.DBSecurityGroupField != null);
        }

        public RevokeDBSecurityGroupIngressResult WithDBSecurityGroup(Amazon.RDS.Model.DBSecurityGroup DBSecurityGroup)
        {
            this.DBSecurityGroupField = DBSecurityGroup;
            return this;
        }

        [XmlElement(ElementName="DBSecurityGroup")]
        public Amazon.RDS.Model.DBSecurityGroup DBSecurityGroup
        {
            get
            {
                return this.DBSecurityGroupField;
            }
            set
            {
                this.DBSecurityGroupField = value;
            }
        }
    }
}

