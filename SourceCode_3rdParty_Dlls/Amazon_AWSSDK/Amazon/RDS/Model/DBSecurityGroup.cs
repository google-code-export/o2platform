namespace Amazon.RDS.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DBSecurityGroup
    {
        private string DBSecurityGroupDescriptionField;
        private string DBSecurityGroupNameField;
        private List<Amazon.RDS.Model.EC2SecurityGroup> EC2SecurityGroupField;
        private List<Amazon.RDS.Model.IPRange> IPRangeField;
        private string ownerIdField;

        public bool IsSetDBSecurityGroupDescription()
        {
            return (this.DBSecurityGroupDescriptionField != null);
        }

        public bool IsSetDBSecurityGroupName()
        {
            return (this.DBSecurityGroupNameField != null);
        }

        public bool IsSetEC2SecurityGroup()
        {
            return (this.EC2SecurityGroup.Count > 0);
        }

        public bool IsSetIPRange()
        {
            return (this.IPRange.Count > 0);
        }

        public bool IsSetOwnerId()
        {
            return (this.ownerIdField != null);
        }

        public DBSecurityGroup WithDBSecurityGroupDescription(string DBSecurityGroupDescription)
        {
            this.DBSecurityGroupDescriptionField = DBSecurityGroupDescription;
            return this;
        }

        public DBSecurityGroup WithDBSecurityGroupName(string DBSecurityGroupName)
        {
            this.DBSecurityGroupNameField = DBSecurityGroupName;
            return this;
        }

        public DBSecurityGroup WithEC2SecurityGroup(params Amazon.RDS.Model.EC2SecurityGroup[] list)
        {
            foreach (Amazon.RDS.Model.EC2SecurityGroup group in list)
            {
                this.EC2SecurityGroup.Add(group);
            }
            return this;
        }

        public DBSecurityGroup WithIPRange(params Amazon.RDS.Model.IPRange[] list)
        {
            foreach (Amazon.RDS.Model.IPRange range in list)
            {
                this.IPRange.Add(range);
            }
            return this;
        }

        public DBSecurityGroup WithOwnerId(string ownerId)
        {
            this.ownerIdField = ownerId;
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

        [XmlElement(ElementName="EC2SecurityGroup")]
        public List<Amazon.RDS.Model.EC2SecurityGroup> EC2SecurityGroup
        {
            get
            {
                if (this.EC2SecurityGroupField == null)
                {
                    this.EC2SecurityGroupField = new List<Amazon.RDS.Model.EC2SecurityGroup>();
                }
                return this.EC2SecurityGroupField;
            }
            set
            {
                this.EC2SecurityGroupField = value;
            }
        }

        [XmlElement(ElementName="IPRange")]
        public List<Amazon.RDS.Model.IPRange> IPRange
        {
            get
            {
                if (this.IPRangeField == null)
                {
                    this.IPRangeField = new List<Amazon.RDS.Model.IPRange>();
                }
                return this.IPRangeField;
            }
            set
            {
                this.IPRangeField = value;
            }
        }

        [XmlElement(ElementName="OwnerId")]
        public string OwnerId
        {
            get
            {
                return this.ownerIdField;
            }
            set
            {
                this.ownerIdField = value;
            }
        }
    }
}

