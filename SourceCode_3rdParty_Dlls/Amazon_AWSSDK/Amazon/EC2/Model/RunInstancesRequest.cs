namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class RunInstancesRequest
    {
        private string additionalInfoField;
        private List<Amazon.EC2.Model.BlockDeviceMapping> blockDeviceMappingField;
        private bool? disableApiTerminationField;
        private string imageIdField;
        private string instanceInitiatedShutdownBehaviorField;
        private string instanceTypeField;
        private string kernelIdField;
        private string keyNameField;
        private decimal? maxCountField;
        private decimal? minCountField;
        private MonitoringSpecification monitoringField;
        private Amazon.EC2.Model.Placement placementField;
        private string ramdiskIdField;
        private List<string> securityGroupField;
        private string subnetIdField;
        private string userDataField;

        public bool IsSetAdditionalInfo()
        {
            return (this.additionalInfoField != null);
        }

        public bool IsSetBlockDeviceMapping()
        {
            return (this.BlockDeviceMapping.Count > 0);
        }

        public bool IsSetDisableApiTermination()
        {
            return this.disableApiTerminationField.HasValue;
        }

        public bool IsSetImageId()
        {
            return (this.imageIdField != null);
        }

        public bool IsSetInstanceInitiatedShutdownBehavior()
        {
            return (this.instanceInitiatedShutdownBehaviorField != null);
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

        public bool IsSetMaxCount()
        {
            return this.maxCountField.HasValue;
        }

        public bool IsSetMinCount()
        {
            return this.minCountField.HasValue;
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

        public RunInstancesRequest WithAdditionalInfo(string additionalInfo)
        {
            this.additionalInfoField = additionalInfo;
            return this;
        }

        public RunInstancesRequest WithBlockDeviceMapping(params Amazon.EC2.Model.BlockDeviceMapping[] list)
        {
            foreach (Amazon.EC2.Model.BlockDeviceMapping mapping in list)
            {
                this.BlockDeviceMapping.Add(mapping);
            }
            return this;
        }

        public RunInstancesRequest WithDisableApiTermination(bool disableApiTermination)
        {
            this.disableApiTerminationField = new bool?(disableApiTermination);
            return this;
        }

        public RunInstancesRequest WithImageId(string imageId)
        {
            this.imageIdField = imageId;
            return this;
        }

        public RunInstancesRequest WithInstanceInitiatedShutdownBehavior(string instanceInitiatedShutdownBehavior)
        {
            this.instanceInitiatedShutdownBehaviorField = instanceInitiatedShutdownBehavior;
            return this;
        }

        public RunInstancesRequest WithInstanceType(string instanceType)
        {
            this.instanceTypeField = instanceType;
            return this;
        }

        public RunInstancesRequest WithKernelId(string kernelId)
        {
            this.kernelIdField = kernelId;
            return this;
        }

        public RunInstancesRequest WithKeyName(string keyName)
        {
            this.keyNameField = keyName;
            return this;
        }

        public RunInstancesRequest WithMaxCount(decimal maxCount)
        {
            this.maxCountField = new decimal?(maxCount);
            return this;
        }

        public RunInstancesRequest WithMinCount(decimal minCount)
        {
            this.minCountField = new decimal?(minCount);
            return this;
        }

        public RunInstancesRequest WithMonitoring(MonitoringSpecification monitoring)
        {
            this.monitoringField = monitoring;
            return this;
        }

        public RunInstancesRequest WithPlacement(Amazon.EC2.Model.Placement placement)
        {
            this.placementField = placement;
            return this;
        }

        public RunInstancesRequest WithRamdiskId(string ramdiskId)
        {
            this.ramdiskIdField = ramdiskId;
            return this;
        }

        public RunInstancesRequest WithSecurityGroup(params string[] list)
        {
            foreach (string str in list)
            {
                this.SecurityGroup.Add(str);
            }
            return this;
        }

        public RunInstancesRequest WithSubnetId(string subnetId)
        {
            this.subnetIdField = subnetId;
            return this;
        }

        public RunInstancesRequest WithUserData(string userData)
        {
            this.userDataField = userData;
            return this;
        }

        [XmlElement(ElementName="AdditionalInfo")]
        public string AdditionalInfo
        {
            get
            {
                return this.additionalInfoField;
            }
            set
            {
                this.additionalInfoField = value;
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

        [XmlElement(ElementName="DisableApiTermination")]
        public bool DisableApiTermination
        {
            get
            {
                return this.disableApiTerminationField.GetValueOrDefault();
            }
            set
            {
                this.disableApiTerminationField = new bool?(value);
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

        [XmlElement(ElementName="InstanceInitiatedShutdownBehavior")]
        public string InstanceInitiatedShutdownBehavior
        {
            get
            {
                return this.instanceInitiatedShutdownBehaviorField;
            }
            set
            {
                this.instanceInitiatedShutdownBehaviorField = value;
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

        [XmlElement(ElementName="MaxCount")]
        public decimal MaxCount
        {
            get
            {
                return this.maxCountField.GetValueOrDefault();
            }
            set
            {
                this.maxCountField = new decimal?(value);
            }
        }

        [XmlElement(ElementName="MinCount")]
        public decimal MinCount
        {
            get
            {
                return this.minCountField.GetValueOrDefault();
            }
            set
            {
                this.minCountField = new decimal?(value);
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

