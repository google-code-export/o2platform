namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class InstanceStateChange
    {
        private InstanceState currentStateField;
        private string instanceIdField;
        private InstanceState previousStateField;

        public bool IsSetCurrentState()
        {
            return (this.currentStateField != null);
        }

        public bool IsSetInstanceId()
        {
            return (this.instanceIdField != null);
        }

        public bool IsSetPreviousState()
        {
            return (this.previousStateField != null);
        }

        public InstanceStateChange WithCurrentState(InstanceState currentState)
        {
            this.currentStateField = currentState;
            return this;
        }

        public InstanceStateChange WithInstanceId(string instanceId)
        {
            this.instanceIdField = instanceId;
            return this;
        }

        public InstanceStateChange WithPreviousState(InstanceState previousState)
        {
            this.previousStateField = previousState;
            return this;
        }

        [XmlElement(ElementName="CurrentState")]
        public InstanceState CurrentState
        {
            get
            {
                return this.currentStateField;
            }
            set
            {
                this.currentStateField = value;
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

        [XmlElement(ElementName="PreviousState")]
        public InstanceState PreviousState
        {
            get
            {
                return this.previousStateField;
            }
            set
            {
                this.previousStateField = value;
            }
        }
    }
}

