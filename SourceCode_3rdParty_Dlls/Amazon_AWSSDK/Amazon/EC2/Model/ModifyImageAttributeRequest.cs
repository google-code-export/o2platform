namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class ModifyImageAttributeRequest
    {
        private string attributeField;
        private string descriptionField;
        private string imageIdField;
        private string operationTypeField;
        private List<string> productCodeField;
        private List<string> userGroupField;
        private List<string> userIdField;

        public bool IsSetAttribute()
        {
            return (this.attributeField != null);
        }

        public bool IsSetDescription()
        {
            return (this.descriptionField != null);
        }

        public bool IsSetImageId()
        {
            return (this.imageIdField != null);
        }

        public bool IsSetOperationType()
        {
            return (this.operationTypeField != null);
        }

        public bool IsSetProductCode()
        {
            return (this.ProductCode.Count > 0);
        }

        public bool IsSetUserGroup()
        {
            return (this.UserGroup.Count > 0);
        }

        public bool IsSetUserId()
        {
            return (this.UserId.Count > 0);
        }

        public ModifyImageAttributeRequest WithAttribute(string attribute)
        {
            this.attributeField = attribute;
            return this;
        }

        public ModifyImageAttributeRequest WithDescription(string description)
        {
            this.descriptionField = description;
            return this;
        }

        public ModifyImageAttributeRequest WithImageId(string imageId)
        {
            this.imageIdField = imageId;
            return this;
        }

        public ModifyImageAttributeRequest WithOperationType(string operationType)
        {
            this.operationTypeField = operationType;
            return this;
        }

        public ModifyImageAttributeRequest WithProductCode(params string[] list)
        {
            foreach (string str in list)
            {
                this.ProductCode.Add(str);
            }
            return this;
        }

        public ModifyImageAttributeRequest WithUserGroup(params string[] list)
        {
            foreach (string str in list)
            {
                this.UserGroup.Add(str);
            }
            return this;
        }

        public ModifyImageAttributeRequest WithUserId(params string[] list)
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

        [XmlElement(ElementName="Description")]
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        [XmlElement(ElementName="ImageId")]
        public string ImageId
        {
            get
            {
                return this.imageIdField;
            }
            set
            {
                this.imageIdField = value;
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

        [XmlElement(ElementName="ProductCode")]
        public List<string> ProductCode
        {
            get
            {
                if (this.productCodeField == null)
                {
                    this.productCodeField = new List<string>();
                }
                return this.productCodeField;
            }
            set
            {
                this.productCodeField = value;
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

