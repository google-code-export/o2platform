namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class Image
    {
        private string architectureField;
        private List<Amazon.EC2.Model.BlockDeviceMapping> blockDeviceMappingField;
        private string descriptionField;
        private string imageIdField;
        private string imageLocationField;
        private string imageOwnerAliasField;
        private string imageStateField;
        private string imageTypeField;
        private string kernelIdField;
        private string nameField;
        private string ownerIdField;
        private string platformField;
        private List<string> productCodeField;
        private string ramdiskIdField;
        private string rootDeviceNameField;
        private string rootDeviceTypeField;
        private Amazon.EC2.Model.StateReason stateReasonField;
        private string visibilityField;

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

        public bool IsSetImageId()
        {
            return (this.imageIdField != null);
        }

        public bool IsSetImageLocation()
        {
            return (this.imageLocationField != null);
        }

        public bool IsSetImageOwnerAlias()
        {
            return (this.imageOwnerAliasField != null);
        }

        public bool IsSetImageState()
        {
            return (this.imageStateField != null);
        }

        public bool IsSetImageType()
        {
            return (this.imageTypeField != null);
        }

        public bool IsSetKernelId()
        {
            return (this.kernelIdField != null);
        }

        public bool IsSetName()
        {
            return (this.nameField != null);
        }

        public bool IsSetOwnerId()
        {
            return (this.ownerIdField != null);
        }

        public bool IsSetPlatform()
        {
            return (this.platformField != null);
        }

        public bool IsSetProductCode()
        {
            return (this.ProductCode.Count > 0);
        }

        public bool IsSetRamdiskId()
        {
            return (this.ramdiskIdField != null);
        }

        public bool IsSetRootDeviceName()
        {
            return (this.rootDeviceNameField != null);
        }

        public bool IsSetRootDeviceType()
        {
            return (this.rootDeviceTypeField != null);
        }

        public bool IsSetStateReason()
        {
            return (this.stateReasonField != null);
        }

        public bool IsSetVisibility()
        {
            return (this.visibilityField != null);
        }

        public Image WithArchitecture(string architecture)
        {
            this.architectureField = architecture;
            return this;
        }

        public Image WithBlockDeviceMapping(params Amazon.EC2.Model.BlockDeviceMapping[] list)
        {
            foreach (Amazon.EC2.Model.BlockDeviceMapping mapping in list)
            {
                this.BlockDeviceMapping.Add(mapping);
            }
            return this;
        }

        public Image WithDescription(string description)
        {
            this.descriptionField = description;
            return this;
        }

        public Image WithImageId(string imageId)
        {
            this.imageIdField = imageId;
            return this;
        }

        public Image WithImageLocation(string imageLocation)
        {
            this.imageLocationField = imageLocation;
            return this;
        }

        public Image WithImageOwnerAlias(string imageOwnerAlias)
        {
            this.imageOwnerAliasField = imageOwnerAlias;
            return this;
        }

        public Image WithImageState(string imageState)
        {
            this.imageStateField = imageState;
            return this;
        }

        public Image WithImageType(string imageType)
        {
            this.imageTypeField = imageType;
            return this;
        }

        public Image WithKernelId(string kernelId)
        {
            this.kernelIdField = kernelId;
            return this;
        }

        public Image WithName(string name)
        {
            this.nameField = name;
            return this;
        }

        public Image WithOwnerId(string ownerId)
        {
            this.ownerIdField = ownerId;
            return this;
        }

        public Image WithPlatform(string platform)
        {
            this.platformField = platform;
            return this;
        }

        public Image WithProductCode(params string[] list)
        {
            foreach (string str in list)
            {
                this.ProductCode.Add(str);
            }
            return this;
        }

        public Image WithRamdiskId(string ramdiskId)
        {
            this.ramdiskIdField = ramdiskId;
            return this;
        }

        public Image WithRootDeviceName(string rootDeviceName)
        {
            this.rootDeviceNameField = rootDeviceName;
            return this;
        }

        public Image WithRootDeviceType(string rootDeviceType)
        {
            this.rootDeviceTypeField = rootDeviceType;
            return this;
        }

        public Image WithStateReason(Amazon.EC2.Model.StateReason stateReason)
        {
            this.stateReasonField = stateReason;
            return this;
        }

        public Image WithVisibility(string visibility)
        {
            this.visibilityField = visibility;
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

        [XmlElement(ElementName="ImageOwnerAlias")]
        public string ImageOwnerAlias
        {
            get
            {
                return this.imageOwnerAliasField;
            }
            set
            {
                this.imageOwnerAliasField = value;
            }
        }

        [XmlElement(ElementName="ImageState")]
        public string ImageState
        {
            get
            {
                return this.imageStateField;
            }
            set
            {
                this.imageStateField = value;
            }
        }

        [XmlElement(ElementName="ImageType")]
        public string ImageType
        {
            get
            {
                return this.imageTypeField;
            }
            set
            {
                this.imageTypeField = value;
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

        [XmlElement(ElementName="Platform")]
        public string Platform
        {
            get
            {
                return this.platformField;
            }
            set
            {
                this.platformField = value;
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

        [XmlElement(ElementName="RootDeviceType")]
        public string RootDeviceType
        {
            get
            {
                return this.rootDeviceTypeField;
            }
            set
            {
                this.rootDeviceTypeField = value;
            }
        }

        [XmlElement(ElementName="StateReason")]
        public Amazon.EC2.Model.StateReason StateReason
        {
            get
            {
                return this.stateReasonField;
            }
            set
            {
                this.stateReasonField = value;
            }
        }

        [XmlElement(ElementName="Visibility")]
        public string Visibility
        {
            get
            {
                return this.visibilityField;
            }
            set
            {
                this.visibilityField = value;
            }
        }
    }
}

