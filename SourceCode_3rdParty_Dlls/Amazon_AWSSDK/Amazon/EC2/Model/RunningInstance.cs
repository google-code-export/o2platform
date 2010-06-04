namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class RunningInstance
    {
        private string amiLaunchIndexField;
        private string architectureField;
        private List<InstanceBlockDeviceMapping> blockDeviceMappingField;
        private string imageIdField;
        private string instanceIdField;
        private string instanceLifecycleField;
        private Amazon.EC2.Model.InstanceState instanceStateField;
        private string instanceTypeField;
        private string ipAddressField;
        private string kernelIdField;
        private string keyNameField;
        private string launchTimeField;
        private Amazon.EC2.Model.Monitoring monitoringField;
        private Amazon.EC2.Model.Placement placementField;
        private string platformField;
        private string privateDnsNameField;
        private string privateIpAddressField;
        private List<string> productCodeField;
        private string publicDnsNameField;
        private string ramdiskIdField;
        private string rootDeviceNameField;
        private string rootDeviceTypeField;
        private string spotInstanceRequestIdField;
        private Amazon.EC2.Model.StateReason stateReasonField;
        private string stateTransitionReasonField;
        private string subnetIdField;
        private string vpcIdField;

        public bool IsSetAmiLaunchIndex()
        {
            return (this.amiLaunchIndexField != null);
        }

        public bool IsSetArchitecture()
        {
            return (this.architectureField != null);
        }

        public bool IsSetBlockDeviceMapping()
        {
            return (this.BlockDeviceMapping.Count > 0);
        }

        public bool IsSetImageId()
        {
            return (this.imageIdField != null);
        }

        public bool IsSetInstanceId()
        {
            return (this.instanceIdField != null);
        }

        public bool IsSetInstanceLifecycle()
        {
            return (this.instanceLifecycleField != null);
        }

        public bool IsSetInstanceState()
        {
            return (this.instanceStateField != null);
        }

        public bool IsSetInstanceType()
        {
            return (this.instanceTypeField != null);
        }

        public bool IsSetIpAddress()
        {
            return (this.ipAddressField != null);
        }

        public bool IsSetKernelId()
        {
            return (this.kernelIdField != null);
        }

        public bool IsSetKeyName()
        {
            return (this.keyNameField != null);
        }

        public bool IsSetLaunchTime()
        {
            return (this.launchTimeField != null);
        }

        public bool IsSetMonitoring()
        {
            return (this.monitoringField != null);
        }

        public bool IsSetPlacement()
        {
            return (this.placementField != null);
        }

        public bool IsSetPlatform()
        {
            return (this.platformField != null);
        }

        public bool IsSetPrivateDnsName()
        {
            return (this.privateDnsNameField != null);
        }

        public bool IsSetPrivateIpAddress()
        {
            return (this.privateIpAddressField != null);
        }

        public bool IsSetProductCode()
        {
            return (this.ProductCode.Count > 0);
        }

        public bool IsSetPublicDnsName()
        {
            return (this.publicDnsNameField != null);
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

        public bool IsSetSpotInstanceRequestId()
        {
            return (this.spotInstanceRequestIdField != null);
        }

        public bool IsSetStateReason()
        {
            return (this.stateReasonField != null);
        }

        public bool IsSetStateTransitionReason()
        {
            return (this.stateTransitionReasonField != null);
        }

        public bool IsSetSubnetId()
        {
            return (this.subnetIdField != null);
        }

        public bool IsSetVpcId()
        {
            return (this.vpcIdField != null);
        }

        public RunningInstance WithAmiLaunchIndex(string amiLaunchIndex)
        {
            this.amiLaunchIndexField = amiLaunchIndex;
            return this;
        }

        public RunningInstance WithArchitecture(string architecture)
        {
            this.architectureField = architecture;
            return this;
        }

        public RunningInstance WithBlockDeviceMapping(params InstanceBlockDeviceMapping[] list)
        {
            foreach (InstanceBlockDeviceMapping mapping in list)
            {
                this.BlockDeviceMapping.Add(mapping);
            }
            return this;
        }

        public RunningInstance WithImageId(string imageId)
        {
            this.imageIdField = imageId;
            return this;
        }

        public RunningInstance WithInstanceId(string instanceId)
        {
            this.instanceIdField = instanceId;
            return this;
        }

        public RunningInstance WithInstanceLifecycle(string instanceLifecycle)
        {
            this.instanceLifecycleField = instanceLifecycle;
            return this;
        }

        public RunningInstance WithInstanceState(Amazon.EC2.Model.InstanceState instanceState)
        {
            this.instanceStateField = instanceState;
            return this;
        }

        public RunningInstance WithInstanceType(string instanceType)
        {
            this.instanceTypeField = instanceType;
            return this;
        }

        public RunningInstance WithIpAddress(string ipAddress)
        {
            this.ipAddressField = ipAddress;
            return this;
        }

        public RunningInstance WithKernelId(string kernelId)
        {
            this.kernelIdField = kernelId;
            return this;
        }

        public RunningInstance WithKeyName(string keyName)
        {
            this.keyNameField = keyName;
            return this;
        }

        public RunningInstance WithLaunchTime(string launchTime)
        {
            this.launchTimeField = launchTime;
            return this;
        }

        public RunningInstance WithMonitoring(Amazon.EC2.Model.Monitoring monitoring)
        {
            this.monitoringField = monitoring;
            return this;
        }

        public RunningInstance WithPlacement(Amazon.EC2.Model.Placement placement)
        {
            this.placementField = placement;
            return this;
        }

        public RunningInstance WithPlatform(string platform)
        {
            this.platformField = platform;
            return this;
        }

        public RunningInstance WithPrivateDnsName(string privateDnsName)
        {
            this.privateDnsNameField = privateDnsName;
            return this;
        }

        public RunningInstance WithPrivateIpAddress(string privateIpAddress)
        {
            this.privateIpAddressField = privateIpAddress;
            return this;
        }

        public RunningInstance WithProductCode(params string[] list)
        {
            foreach (string str in list)
            {
                this.ProductCode.Add(str);
            }
            return this;
        }

        public RunningInstance WithPublicDnsName(string publicDnsName)
        {
            this.publicDnsNameField = publicDnsName;
            return this;
        }

        public RunningInstance WithRamdiskId(string ramdiskId)
        {
            this.ramdiskIdField = ramdiskId;
            return this;
        }

        public RunningInstance WithRootDeviceName(string rootDeviceName)
        {
            this.rootDeviceNameField = rootDeviceName;
            return this;
        }

        public RunningInstance WithRootDeviceType(string rootDeviceType)
        {
            this.rootDeviceTypeField = rootDeviceType;
            return this;
        }

        public RunningInstance WithSpotInstanceRequestId(string spotInstanceRequestId)
        {
            this.spotInstanceRequestIdField = spotInstanceRequestId;
            return this;
        }

        public RunningInstance WithStateReason(Amazon.EC2.Model.StateReason stateReason)
        {
            this.stateReasonField = stateReason;
            return this;
        }

        public RunningInstance WithStateTransitionReason(string stateTransitionReason)
        {
            this.stateTransitionReasonField = stateTransitionReason;
            return this;
        }

        public RunningInstance WithSubnetId(string subnetId)
        {
            this.subnetIdField = subnetId;
            return this;
        }

        public RunningInstance WithVpcId(string vpcId)
        {
            this.vpcIdField = vpcId;
            return this;
        }

        [XmlElement(ElementName="AmiLaunchIndex")]
        public string AmiLaunchIndex
        {
            get
            {
                return this.amiLaunchIndexField;
            }
            set
            {
                this.amiLaunchIndexField = value;
            }
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
        public List<InstanceBlockDeviceMapping> BlockDeviceMapping
        {
            get
            {
                if (this.blockDeviceMappingField == null)
                {
                    this.blockDeviceMappingField = new List<InstanceBlockDeviceMapping>();
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

        [XmlElement(ElementName="InstanceLifecycle")]
        public string InstanceLifecycle
        {
            get
            {
                return this.instanceLifecycleField;
            }
            set
            {
                this.instanceLifecycleField = value;
            }
        }

        [XmlElement(ElementName="InstanceState")]
        public Amazon.EC2.Model.InstanceState InstanceState
        {
            get
            {
                return this.instanceStateField;
            }
            set
            {
                this.instanceStateField = value;
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

        [XmlElement(ElementName="IpAddress")]
        public string IpAddress
        {
            get
            {
                return this.ipAddressField;
            }
            set
            {
                this.ipAddressField = value;
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

        [XmlElement(ElementName="LaunchTime")]
        public string LaunchTime
        {
            get
            {
                return this.launchTimeField;
            }
            set
            {
                this.launchTimeField = value;
            }
        }

        [XmlElement(ElementName="Monitoring")]
        public Amazon.EC2.Model.Monitoring Monitoring
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

        [XmlElement(ElementName="PrivateDnsName")]
        public string PrivateDnsName
        {
            get
            {
                return this.privateDnsNameField;
            }
            set
            {
                this.privateDnsNameField = value;
            }
        }

        [XmlElement(ElementName="PrivateIpAddress")]
        public string PrivateIpAddress
        {
            get
            {
                return this.privateIpAddressField;
            }
            set
            {
                this.privateIpAddressField = value;
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

        [XmlElement(ElementName="PublicDnsName")]
        public string PublicDnsName
        {
            get
            {
                return this.publicDnsNameField;
            }
            set
            {
                this.publicDnsNameField = value;
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

        [XmlElement(ElementName="SpotInstanceRequestId")]
        public string SpotInstanceRequestId
        {
            get
            {
                return this.spotInstanceRequestIdField;
            }
            set
            {
                this.spotInstanceRequestIdField = value;
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

        [XmlElement(ElementName="StateTransitionReason")]
        public string StateTransitionReason
        {
            get
            {
                return this.stateTransitionReasonField;
            }
            set
            {
                this.stateTransitionReasonField = value;
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

        [XmlElement(ElementName="VpcId")]
        public string VpcId
        {
            get
            {
                return this.vpcIdField;
            }
            set
            {
                this.vpcIdField = value;
            }
        }
    }
}

