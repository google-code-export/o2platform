namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class BundleInstanceRequest
    {
        private string instanceIdField;
        private Amazon.EC2.Model.Storage storageField;

        public bool IsSetInstanceId()
        {
            return (this.instanceIdField != null);
        }

        public bool IsSetStorage()
        {
            return (this.storageField != null);
        }

        public BundleInstanceRequest WithInstanceId(string instanceId)
        {
            this.instanceIdField = instanceId;
            return this;
        }

        public BundleInstanceRequest WithStorage(Amazon.EC2.Model.Storage storage)
        {
            this.storageField = storage;
            return this;
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
    }
}

