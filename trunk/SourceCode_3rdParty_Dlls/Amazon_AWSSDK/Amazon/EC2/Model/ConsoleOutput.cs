namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class ConsoleOutput
    {
        private string instanceIdField;
        private string outputField;
        private string timestampField;

        public bool IsSetInstanceId()
        {
            return (this.instanceIdField != null);
        }

        public bool IsSetOutput()
        {
            return (this.outputField != null);
        }

        public bool IsSetTimestamp()
        {
            return (this.timestampField != null);
        }

        public ConsoleOutput WithInstanceId(string instanceId)
        {
            this.instanceIdField = instanceId;
            return this;
        }

        public ConsoleOutput WithOutput(string output)
        {
            this.outputField = output;
            return this;
        }

        public ConsoleOutput WithTimestamp(string timestamp)
        {
            this.timestampField = timestamp;
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

        [XmlElement(ElementName="Output")]
        public string Output
        {
            get
            {
                return this.outputField;
            }
            set
            {
                this.outputField = value;
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

