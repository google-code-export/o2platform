namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class SecurityGroup
    {
        private string groupDescriptionField;
        private string groupNameField;
        private List<Amazon.EC2.Model.IpPermission> ipPermissionField;
        private string ownerIdField;

        public bool IsSetGroupDescription()
        {
            return (this.groupDescriptionField != null);
        }

        public bool IsSetGroupName()
        {
            return (this.groupNameField != null);
        }

        public bool IsSetIpPermission()
        {
            return (this.IpPermission.Count > 0);
        }

        public bool IsSetOwnerId()
        {
            return (this.ownerIdField != null);
        }

        public SecurityGroup WithGroupDescription(string groupDescription)
        {
            this.groupDescriptionField = groupDescription;
            return this;
        }

        public SecurityGroup WithGroupName(string groupName)
        {
            this.groupNameField = groupName;
            return this;
        }

        public SecurityGroup WithIpPermission(params Amazon.EC2.Model.IpPermission[] list)
        {
            foreach (Amazon.EC2.Model.IpPermission permission in list)
            {
                this.IpPermission.Add(permission);
            }
            return this;
        }

        public SecurityGroup WithOwnerId(string ownerId)
        {
            this.ownerIdField = ownerId;
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

        [XmlElement(ElementName="IpPermission")]
        public List<Amazon.EC2.Model.IpPermission> IpPermission
        {
            get
            {
                if (this.ipPermissionField == null)
                {
                    this.ipPermissionField = new List<Amazon.EC2.Model.IpPermission>();
                }
                return this.ipPermissionField;
            }
            set
            {
                this.ipPermissionField = value;
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

