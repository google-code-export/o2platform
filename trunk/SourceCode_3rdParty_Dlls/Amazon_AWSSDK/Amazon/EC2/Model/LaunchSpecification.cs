namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class LaunchSpecification
    {
        private string addressingTypeField;
        private List<Amazon.EC2.Model.BlockDeviceMapping> blockDeviceMappingField;
        private string imageIdField;
        private string instanceTypeField;
        private string kernelIdField;
        private string keyNameField;
        private MonitoringSpecification monitoringField;
        private Amazon.EC2.Model.Placement placementField;
        private string ramdiskIdField;
        private List<string> securityGroupField;
        private string subnetIdField;
        private string userDataField;

        public bool IsSetAddressingType()
        {
            return (this.addressingTypeField != null);
        }

        public bool IsSetBlockDeviceMapping()
        {
            return (this.BlockDeviceMapping.Count > 0);
        }

        public bool IsSetImageId()
        {
            return (this.imageIdField != null);
        }

        public bool IsSetInstanceType()
        {
            return (this.instanceTypeField != null);
        }

        public bool IsSetKernelId()
        {
            return (this.kernelIdField != null);
        }

        public bool IsSetKeyName()
        {
            return (this.keyNameField != null);
        }

        public bool IsSetMonitoring()
        {
            return (this.monitoringField != null);
        }

        public bool IsSetPlacement()
        {
            return (this.placementField != null);
        }

        public bool IsSetRamdiskId()
        {
            return (this.ramdiskIdField != null);
        }

        public bool IsSetSecurityGroup()
        {
            return (this.SecurityGroup.Count > 0);
        }

        public bool IsSetSubnetId()
        {
            return (this.subnetIdField != null);
        }

        public bool IsSetUserData()
        {
            return (this.userDataField != null);
        }

        public LaunchSpecification WithAddressingType(string addressingType)
        {
            this.addressingTypeField = addressingType;
            return this;
        }

        public LaunchSpecification WithBlockDeviceMapping(params Amazon.EC2.Model.BlockDeviceMapping[] list)
        {
            foreach (Amazon.EC2.Model.BlockDeviceMapping mapping in list)
            {
                this.BlockDeviceMapping.Add(mapping);
            }
            return this;
        }

        public LaunchSpecification WithImageId(string imageId)
        {
            this.imageIdField = imageId;
            return this;
        }

        public LaunchSpecification WithInstanceType(string instanceType)
        {
            this.instanceTypeField = instanceType;
            return this;
        }

        public LaunchSpecification WithKernelId(string kernelId)
        {
            this.kernelIdField = kernelId;
            return this;
        }

        public LaunchSpecification WithKeyName(string keyName)
        {
            this.keyNameField = keyName;
            return this;
        }

        public LaunchSpecification WithMonitoring(MonitoringSpecification monitoring)
        {
            this.monitoringField = monitoring;
            return this;
        }

        public LaunchSpecification WithPlacement(Amazon.EC2.Model.Placement placement)
        {
            this.placementField = placement;
            return this;
        }

        public LaunchSpecification WithRamdiskId(string ramdiskId)
        {
            this.ramdiskIdField = ramdiskId;
            return this;
        }

        public LaunchSpecification WithSecurityGroup(params string[] list)
        {
            foreach (string str in list)
            {
                this.SecurityGroup.Add(str);
            }
            return this;
        }

        public LaunchSpecification WithSubnetId(string subnetId)
        {
            this.subnetIdField = subnetId;
            return this;
        }

        public LaunchSpecification WithUserData(string userData)
        {
            this.userDataField = userData;
            return this;
        }

        [XmlElement(ElementName="AddressingType")]
        public string AddressingType
        {
            get
            {
                return this.addressingTypeField;
            }
            set
            {
                this.addressingTypeField = value;
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

        [XmlElement(ElementName="InstanceType")]
        public string InstanceType
        {
            get
            {
                return this.instanceTypeField;
            }
            set
            {
                this.instanceTypeField = value;
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

        [XmlElement(ElementName="KeyName")]
        public string KeyName
        {
            get
            {
                return this.keyNameField;
            }
            set
            {
                this.keyNameField = value;
            }
        }

        [XmlElement(ElementName="Monitoring")]
        public MonitoringSpecification Monitoring
        {
            get
            {
                return this.monitoringField;
            }
            set
            {
                this.monitoringField = value;
            }
        }

        [XmlElement(ElementName="Placement")]
        public Amazon.EC2.Model.Placement Placement
        {
            get
            {
                return this.placementField;
            }
            set
            {
                this.placementField = value;
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

        [XmlElement(ElementName="SecurityGroup")]
        public List<string> SecurityGroup
        {
            get
            {
                if (this.securityGroupField == null)
                {
                    this.securityGroupField = new List<string>();
                }
                return this.securityGroupField;
            }
            set
            {
                this.securityGroupField = value;
            }
        }

        [XmlElement(ElementName="SubnetId")]
        public string SubnetId
        {
            get
            {
                return this.subnetIdField;
            }
            set
            {
                this.subnetIdField = value;
            }
        }

        [XmlElement(ElementName="UserData")]
        public string UserData
        {
            get
            {
                return this.userDataField;
            }
            set
            {
                this.userDataField = value;
            }
        }
    }
}

