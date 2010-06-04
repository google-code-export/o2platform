namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class PasswordData
    {
        private string dataField;
        private string instanceIdField;
        private string timestampField;

        public bool IsSetData()
        {
            return (this.dataField != null);
        }

        public bool IsSetInstanceId()
        {
            return (this.instanceIdField != null);
        }

        public bool IsSetTimestamp()
        {
            return (this.timestampField != null);
        }

        public PasswordData WithData(string data)
        {
            this.dataField = data;
            return this;
        }

        public PasswordData WithInstanceId(string instanceId)
        {
            this.instanceIdField = instanceId;
            return this;
        }

        public PasswordData WithTimestamp(string timestamp)
        {
            this.timestampField = timestamp;
            return this;
        }

        [XmlElement(ElementName="Data")]
        public string Data
        {
            get
            {
                return this.dataField;
            }
            set
            {
                this.dataField = value;
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

        [XmlElement(ElementName="Timestamp")]
        public string Timestamp
        {
            get
            {
                return this.timestampField;
            }
            set
            {
                this.timestampField = value;
            }
        }
    }
}

