namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class BundleTask
    {
        private string bundleIdField;
        private string bundleStateField;
        private Amazon.EC2.Model.BundleTaskError bundleTaskErrorField;
        private string instanceIdField;
        private string progressField;
        private string startTimeField;
        private Amazon.EC2.Model.Storage storageField;
        private string updateTimeField;

        public bool IsSetBundleId()
        {
            return (this.bundleIdField != null);
        }

        public bool IsSetBundleState()
        {
            return (this.bundleStateField != null);
        }

        public bool IsSetBundleTaskError()
        {
            return (this.bundleTaskErrorField != null);
        }

        public bool IsSetInstanceId()
        {
            return (this.instanceIdField != null);
        }

        public bool IsSetProgress()
        {
            return (this.progressField != null);
        }

        public bool IsSetStartTime()
        {
            return (this.startTimeField != null);
        }

        public bool IsSetStorage()
        {
            return (this.storageField != null);
        }

        public bool IsSetUpdateTime()
        {
            return (this.updateTimeField != null);
        }

        public BundleTask WithBundleId(string bundleId)
        {
            this.bundleIdField = bundleId;
            return this;
        }

        public BundleTask WithBundleState(string bundleState)
        {
            this.bundleStateField = bundleState;
            return this;
        }

        public BundleTask WithBundleTaskError(Amazon.EC2.Model.BundleTaskError bundleTaskError)
        {
            this.bundleTaskErrorField = bundleTaskError;
            return this;
        }

        public BundleTask WithInstanceId(string instanceId)
        {
            this.instanceIdField = instanceId;
            return this;
        }

        public BundleTask WithProgress(string progress)
        {
            this.progressField = progress;
            return this;
        }

        public BundleTask WithStartTime(string startTime)
        {
            this.startTimeField = startTime;
            return this;
        }

        public BundleTask WithStorage(Amazon.EC2.Model.Storage storage)
        {
            this.storageField = storage;
            return this;
        }

        public BundleTask WithUpdateTime(string updateTime)
        {
            this.updateTimeField = updateTime;
            return this;
        }

        [XmlElement(ElementName="BundleId")]
        public string BundleId
        {
            get
            {
                return this.bundleIdField;
            }
            set
            {
                this.bundleIdField = value;
            }
        }

        [XmlElement(ElementName="BundleState")]
        public string BundleState
        {
            get
            {
                return this.bundleStateField;
            }
            set
            {
                this.bundleStateField = value;
            }
        }

        [XmlElement(ElementName="BundleTaskError")]
        public Amazon.EC2.Model.BundleTaskError BundleTaskError
        {
            get
            {
                return this.bundleTaskErrorField;
            }
            set
            {
                this.bundleTaskErrorField = value;
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

        [XmlElement(ElementName="Progress")]
        public string Progress
        {
            get
            {
                return this.progressField;
            }
            set
            {
                this.progressField = value;
            }
        }

        [XmlElement(ElementName="StartTime")]
        public string StartTime
        {
            get
            {
                return this.startTimeField;
            }
            set
            {
                this.startTimeField = value;
            }
        }

        [XmlElement(ElementName="Storage")]
        public Amazon.EC2.Model.Storage Storage
        {
            get
            {
                return this.storageField;
            }
            set
            {
                this.storageField = value;
            }
        }

        [XmlElement(ElementName="UpdateTime")]
        public string UpdateTime
        {
            get
            {
                return this.updateTimeField;
            }
            set
            {
                this.updateTimeField = value;
            }
        }
    }
}

