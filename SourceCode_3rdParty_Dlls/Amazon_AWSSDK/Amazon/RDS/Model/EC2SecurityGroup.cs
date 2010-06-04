namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class EC2SecurityGroup
    {
        private string EC2SecurityGroupNameField;
        private string EC2SecurityGroupOwnerIdField;
        private string statusField;

        public bool IsSetEC2SecurityGroupName()
        {
            return (this.EC2SecurityGroupNameField != null);
        }

        public bool IsSetEC2SecurityGroupOwnerId()
        {
            return (this.EC2SecurityGroupOwnerIdField != null);
        }

        public bool IsSetStatus()
        {
            return (this.statusField != null);
        }

        public EC2SecurityGroup WithEC2SecurityGroupName(string EC2SecurityGroupName)
        {
            this.EC2SecurityGroupNameField = EC2SecurityGroupName;
            return this;
        }

        public EC2SecurityGroup WithEC2SecurityGroupOwnerId(string EC2SecurityGroupOwnerId)
        {
            this.EC2SecurityGroupOwnerIdField = EC2SecurityGroupOwnerId;
            return this;
        }

        public EC2SecurityGroup WithStatus(string status)
        {
            this.statusField = status;
            return this;
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

        [XmlElement(ElementName="Status")]
        public string Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }
    }
}

