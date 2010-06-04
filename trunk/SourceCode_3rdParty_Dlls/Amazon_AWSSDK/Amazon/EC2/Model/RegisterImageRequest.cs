namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class RegisterImageRequest
    {
        private string architectureField;
        private List<Amazon.EC2.Model.BlockDeviceMapping> blockDeviceMappingField;
        private string descriptionField;
        private string imageLocationField;
        private string kernelIdField;
        private string nameField;
        private string ramdiskIdField;
        private string rootDeviceNameField;

        public bool IsSetArchitecture()
        {
            return (this.architectureField != null);
        }

        public bool IsSetBlockDeviceMapping()
        {
            return (this.BlockDeviceMapping.Count > 0);
        }

        public bool IsSetDescription()
        {
            return (this.descriptionField != null);
        }

        public bool IsSetImageLocation()
        {
            return (this.imageLocationField != null);
        }

        public bool IsSetKernelId()
        {
            return (this.kernelIdField != null);
        }

        public bool IsSetName()
        {
            return (this.nameField != null);
        }

        public bool IsSetRamdiskId()
        {
            return (this.ramdiskIdField != null);
        }

        public bool IsSetRootDeviceName()
        {
            return (this.rootDeviceNameField != null);
        }

        public RegisterImageRequest WithArchitecture(string architecture)
        {
            this.architectureField = architecture;
            return this;
        }

        public RegisterImageRequest WithBlockDeviceMapping(params Amazon.EC2.Model.BlockDeviceMapping[] list)
        {
            foreach (Amazon.EC2.Model.BlockDeviceMapping mapping in list)
            {
                this.BlockDeviceMapping.Add(mapping);
            }
            return this;
        }

        public RegisterImageRequest WithDescription(string description)
        {
            this.descriptionField = description;
            return this;
        }

        public RegisterImageRequest WithImageLocation(string imageLocation)
        {
            this.imageLocationField = imageLocation;
            return this;
        }

        public RegisterImageRequest WithKernelId(string kernelId)
        {
            this.kernelIdField = kernelId;
            return this;
        }

        public RegisterImageRequest WithName(string name)
        {
            this.nameField = name;
            return this;
        }

        public RegisterImageRequest WithRamdiskId(string ramdiskId)
        {
            this.ramdiskIdField = ramdiskId;
            return this;
        }

        public RegisterImageRequest WithRootDeviceName(string rootDeviceName)
        {
            this.rootDeviceNameField = rootDeviceName;
            return this;
        }

        [XmlElement(ElementName="Architecture")]
        public string Architecture
        {
            get
            {
                return this.architectureField;
            }
            set
            {
                this.architectureField = value;
            }
        }

        [XmlElement(ElementName="BlockDeviceMapping")]
        public List<Amazon.EC2.Model.BlockDeviceMapping> BlockDeviceMapping
        {
            get
            {
                if (this.blockDeviceMappingField == null)
                {
                    this.blockDeviceMappingField = new List<Amazon.EC2.Model.BlockDeviceMapping>();
                }
                return this.blockDeviceMappingField;
            }
            set
            {
                this.blockDeviceMappingField = value;
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

        [XmlElement(ElementName="ImageLocation")]
        public string ImageLocation
        {
            get
            {
                return this.imageLocationField;
            }
            set
            {
                this.imageLocationField = value;
            }
        }

        [XmlElement(ElementName="KernelId")]
        public string KernelId
        {
            get
            {
                return this.kernelIdField;
            }
            set
            {
                this.kernelIdField = value;
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

        [XmlElement(ElementName="RamdiskId")]
        public string RamdiskId
        {
            get
            {
                return this.ramdiskIdField;
            }
            set
            {
                this.ramdiskIdField = value;
            }
        }

        [XmlElement(ElementName="RootDeviceName")]
        public string RootDeviceName
        {
            get
            {
                return this.rootDeviceNameField;
            }
            set
            {
                this.rootDeviceNameField = value;
            }
        }
    }
}

