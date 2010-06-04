namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class AuthorizeDBSecurityGroupIngressRequest
    {
        private string CIDRIPField;
        private string DBSecurityGroupNameField;
        private string EC2SecurityGroupNameField;
        private string EC2SecurityGroupOwnerIdField;

        public bool IsSetCIDRIP()
        {
            return (this.CIDRIPField != null);
        }

        public bool IsSetDBSecurityGroupName()
        {
            return (this.DBSecurityGroupNameField != null);
        }

        public bool IsSetEC2SecurityGroupName()
        {
            return (this.EC2SecurityGroupNameField != null);
        }

        public bool IsSetEC2SecurityGroupOwnerId()
        {
            return (this.EC2SecurityGroupOwnerIdField != null);
        }

        public AuthorizeDBSecurityGroupIngressRequest WithCIDRIP(string CIDRIP)
        {
            this.CIDRIPField = CIDRIP;
            return this;
        }

        public AuthorizeDBSecurityGroupIngressRequest WithDBSecurityGroupName(string DBSecurityGroupName)
        {
            this.DBSecurityGroupNameField = DBSecurityGroupName;
            return this;
        }

        public AuthorizeDBSecurityGroupIngressRequest WithEC2SecurityGroupName(string EC2SecurityGroupName)
        {
            this.EC2SecurityGroupNameField = EC2SecurityGroupName;
            return this;
        }

        public AuthorizeDBSecurityGroupIngressRequest WithEC2SecurityGroupOwnerId(string EC2SecurityGroupOwnerId)
        {
            this.EC2SecurityGroupOwnerIdField = EC2SecurityGroupOwnerId;
            return this;
        }

        [XmlElement(ElementName="CIDRIP")]
        public string CIDRIP
        {
            get
            {
                return this.CIDRIPField;
            }
            set
            {
                this.CIDRIPField = value;
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

        [XmlElement(ElementName="EC2SecurityGroupName")]
        public string EC2SecurityGroupName
        {
            get
            {
                return this.EC2SecurityGroupNameField;
            }
            set
            {
                this.EC2SecurityGroupNameField = value;
            }
        }

        [XmlElement(ElementName="EC2SecurityGroupOwnerId")]
        public string EC2SecurityGroupOwnerId
        {
            get
            {
                return this.EC2SecurityGroupOwnerIdField;
            }
            set
            {
                this.EC2SecurityGroupOwnerIdField = value;
            }
        }
    }
}

