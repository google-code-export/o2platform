namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class ModifySnapshotAttributeRequest
    {
        private string attributeField;
        private string operationTypeField;
        private string snapshotIdField;
        private List<string> userGroupField;
        private List<string> userIdField;

        public bool IsSetAttribute()
        {
            return (this.attributeField != null);
        }

        public bool IsSetOperationType()
        {
            return (this.operationTypeField != null);
        }

        public bool IsSetSnapshotId()
        {
            return (this.snapshotIdField != null);
        }

        public bool IsSetUserGroup()
        {
            return (this.UserGroup.Count > 0);
        }

        public bool IsSetUserId()
        {
            return (this.UserId.Count > 0);
        }

        public ModifySnapshotAttributeRequest WithAttribute(string attribute)
        {
            this.attributeField = attribute;
            return this;
        }

        public ModifySnapshotAttributeRequest WithOperationType(string operationType)
        {
            this.operationTypeField = operationType;
            return this;
        }

        public ModifySnapshotAttributeRequest WithSnapshotId(string snapshotId)
        {
            this.snapshotIdField = snapshotId;
            return this;
        }

        public ModifySnapshotAttributeRequest WithUserGroup(params string[] list)
        {
            foreach (string str in list)
            {
                this.UserGroup.Add(str);
            }
            return this;
        }

        public ModifySnapshotAttributeRequest WithUserId(params string[] list)
        {
            foreach (string str in list)
            {
                this.UserId.Add(str);
            }
            return this;
        }

        [XmlElement(ElementName="Attribute")]
        public string Attribute
        {
            get
            {
                return this.attributeField;
            }
            set
            {
                this.attributeField = value;
            }
        }

        [XmlElement(ElementName="OperationType")]
        public string OperationType
        {
            get
            {
                return this.operationTypeField;
            }
            set
            {
                this.operationTypeField = value;
            }
        }

        [XmlElement(ElementName="SnapshotId")]
        public string SnapshotId
        {
            get
            {
                return this.snapshotIdField;
            }
            set
            {
                this.snapshotIdField = value;
            }
        }

        [XmlElement(ElementName="UserGroup")]
        public List<string> UserGroup
        {
            get
            {
                if (this.userGroupField == null)
                {
                    this.userGroupField = new List<string>();
                }
                return this.userGroupField;
            }
            set
            {
                this.userGroupField = value;
            }
        }

        [XmlElement(ElementName="UserId")]
        public List<string> UserId
        {
            get
            {
                if (this.userIdField == null)
                {
                    this.userIdField = new List<string>();
                }
                return this.userIdField;
            }
            set
            {
                this.userIdField = value;
            }
        }
    }
}

