namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class SpotDatafeedSubscription
    {
        private string bucketField;
        private SpotInstanceStateFault faultField;
        private string ownerIdField;
        private string prefixField;
        private string stateField;

        public bool IsSetBucket()
        {
            return (this.bucketField != null);
        }

        public bool IsSetFault()
        {
            return (this.faultField != null);
        }

        public bool IsSetOwnerId()
        {
            return (this.ownerIdField != null);
        }

        public bool IsSetPrefix()
        {
            return (this.prefixField != null);
        }

        public bool IsSetState()
        {
            return (this.stateField != null);
        }

        public SpotDatafeedSubscription WithBucket(string bucket)
        {
            this.bucketField = bucket;
            return this;
        }

        public SpotDatafeedSubscription WithFault(SpotInstanceStateFault fault)
        {
            this.faultField = fault;
            return this;
        }

        public SpotDatafeedSubscription WithOwnerId(string ownerId)
        {
            this.ownerIdField = ownerId;
            return this;
        }

        public SpotDatafeedSubscription WithPrefix(string prefix)
        {
            this.prefixField = prefix;
            return this;
        }

        public SpotDatafeedSubscription WithState(string state)
        {
            this.stateField = state;
            return this;
        }

        [XmlElement(ElementName="Bucket")]
        public string Bucket
        {
            get
            {
                return this.bucketField;
            }
            set
            {
                this.bucketField = value;
            }
        }

        [XmlElement(ElementName="Fault")]
        public SpotInstanceStateFault Fault
        {
            get
            {
                return this.faultField;
            }
            set
            {
                this.faultField = value;
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

        [XmlElement(ElementName="Prefix")]
        public string Prefix
        {
            get
            {
                return this.prefixField;
            }
            set
            {
                this.prefixField = value;
            }
        }

        [XmlElement(ElementName="State")]
        public string State
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }
    }
}

