namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CreateImageRequest
    {
        private string descriptionField;
        private string instanceIdField;
        private string nameField;
        private bool? noRebootField;

        public bool IsSetDescription()
        {
            return (this.descriptionField != null);
        }

        public bool IsSetInstanceId()
        {
            return (this.instanceIdField != null);
        }

        public bool IsSetName()
        {
            return (this.nameField != null);
        }

        public bool IsSetNoReboot()
        {
            return this.noRebootField.HasValue;
        }

        public CreateImageRequest WithDescription(string description)
        {
            this.descriptionField = description;
            return this;
        }

        public CreateImageRequest WithInstanceId(string instanceId)
        {
            this.instanceIdField = instanceId;
            return this;
        }

        public CreateImageRequest WithName(string name)
        {
            this.nameField = name;
            return this;
        }

        public CreateImageRequest WithNoReboot(bool noReboot)
        {
            this.noRebootField = new bool?(noReboot);
            return this;
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

        [XmlElement(ElementName="InstanceId")]
        public string InstanceId
        {
            get
            {
                return this.instanceIdField;
            }
            set
            {
                this.instanceIdField = value;
            }
        }

        [XmlElement(ElementName="Name")]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        [XmlElement(ElementName="NoReboot")]
        public bool NoReboot
        {
            get
            {
                return this.noRebootField.GetValueOrDefault();
            }
            set
            {
                this.noRebootField = new bool?(value);
            }
        }
    }
}

