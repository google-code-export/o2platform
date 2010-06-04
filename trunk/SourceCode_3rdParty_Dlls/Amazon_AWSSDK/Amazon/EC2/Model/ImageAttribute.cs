namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class ImageAttribute
    {
        private Amazon.EC2.Model.BlockDeviceMapping blockDeviceMappingField;
        private string descriptionField;
        private string imageIdField;
        private string kernelIdField;
        private List<Amazon.EC2.Model.LaunchPermission> launchPermissionField;
        private List<string> productCodeField;
        private string ramdiskIdField;

        public bool IsSetBlockDeviceMapping()
        {
            return (this.blockDeviceMappingField != null);
        }

        public bool IsSetDescription()
        {
            return (this.descriptionField != null);
        }

        public bool IsSetImageId()
        {
            return (this.imageIdField != null);
        }

        public bool IsSetKernelId()
        {
            return (this.kernelIdField != null);
        }

        public bool IsSetLaunchPermission()
        {
            return (this.LaunchPermission.Count > 0);
        }

        public bool IsSetProductCode()
        {
            return (this.ProductCode.Count > 0);
        }

        public bool IsSetRamdiskId()
        {
            return (this.ramdiskIdField != null);
        }

        public ImageAttribute WithBlockDeviceMapping(Amazon.EC2.Model.BlockDeviceMapping blockDeviceMapping)
        {
            this.blockDeviceMappingField = blockDeviceMapping;
            return this;
        }

        public ImageAttribute WithDescription(string description)
        {
            this.descriptionField = description;
            return this;
        }

        public ImageAttribute WithImageId(string imageId)
        {
            this.imageIdField = imageId;
            return this;
        }

        public ImageAttribute WithKernelId(string kernelId)
        {
            this.kernelIdField = kernelId;
            return this;
        }

        public ImageAttribute WithLaunchPermission(params Amazon.EC2.Model.LaunchPermission[] list)
        {
            foreach (Amazon.EC2.Model.LaunchPermission permission in list)
            {
                this.LaunchPermission.Add(permission);
            }
            return this;
        }

        public ImageAttribute WithProductCode(params string[] list)
        {
            foreach (string str in list)
            {
                this.ProductCode.Add(str);
            }
            return this;
        }

        public ImageAttribute WithRamdiskId(string ramdiskId)
        {
            this.ramdiskIdField = ramdiskId;
            return this;
        }

        [XmlElement(ElementName="BlockDeviceMapping")]
        public Amazon.EC2.Model.BlockDeviceMapping BlockDeviceMapping
        {
            get
            {
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

        [XmlElement(ElementName="LaunchPermission")]
        public List<Amazon.EC2.Model.LaunchPermission> LaunchPermission
        {
            get
            {
                if (this.launchPermissionField == null)
                {
                    this.launchPermissionField = new List<Amazon.EC2.Model.LaunchPermission>();
                }
                return this.launchPermissionField;
            }
            set
            {
                this.launchPermissionField = value;
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
    }
}

