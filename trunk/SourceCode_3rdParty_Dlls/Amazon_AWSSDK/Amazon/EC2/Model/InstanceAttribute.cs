namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class InstanceAttribute
    {
        private List<InstanceBlockDeviceMapping> blockDeviceMappingField;
        private bool? disableApiTerminationField;
        private string instanceIdField;
        private string instanceInitiatedShutdownBehaviorField;
        private string instanceTypeField;
        private string kernelIdField;
        private string ramdiskIdField;
        private string rootDeviceNameField;
        private string userDataField;

        public bool IsSetBlockDeviceMapping()
        {
            return (this.BlockDeviceMapping.Count > 0);
        }

        public bool IsSetDisableApiTermination()
        {
            return this.disableApiTerminationField.HasValue;
        }

        public bool IsSetInstanceId()
        {
            return (this.instanceIdField != null);
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

        public bool IsSetRamdiskId()
        {
            return (this.ramdiskIdField != null);
        }

        public bool IsSetRootDeviceName()
        {
            return (this.rootDeviceNameField != null);
        }

        public bool IsSetUserData()
        {
            return (this.userDataField != null);
        }

        public InstanceAttribute WithBlockDeviceMapping(params InstanceBlockDeviceMapping[] list)
        {
            foreach (InstanceBlockDeviceMapping mapping in list)
            {
                this.BlockDeviceMapping.Add(mapping);
            }
            return this;
        }

        public InstanceAttribute WithDisableApiTermination(bool disableApiTermination)
        {
            this.disableApiTerminationField = new bool?(disableApiTermination);
            return this;
        }

        public InstanceAttribute WithInstanceId(string instanceId)
        {
            this.instanceIdField = instanceId;
            return this;
        }

        public InstanceAttribute WithInstanceInitiatedShutdownBehavior(string instanceInitiatedShutdownBehavior)
        {
            this.instanceInitiatedShutdownBehaviorField = instanceInitiatedShutdownBehavior;
            return this;
        }

        public InstanceAttribute WithInstanceType(string instanceType)
        {
            this.instanceTypeField = instanceType;
            return this;
        }

        public InstanceAttribute WithKernelId(string kernelId)
        {
            this.kernelIdField = kernelId;
            return this;
        }

        public InstanceAttribute WithRamdiskId(string ramdiskId)
        {
            this.ramdiskIdField = ramdiskId;
            return this;
        }

        public InstanceAttribute WithRootDeviceName(string rootDeviceName)
        {
            this.rootDeviceNameField = rootDeviceName;
            return this;
        }

        public InstanceAttribute WithUserData(string userData)
        {
            this.userDataField = userData;
            return this;
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

