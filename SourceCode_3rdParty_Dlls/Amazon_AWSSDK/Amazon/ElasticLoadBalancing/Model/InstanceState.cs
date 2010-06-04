namespace Amazon.ElasticLoadBalancing.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticloadbalancing.amazonaws.com/doc/2009-11-25/", IsNullable=false)]
    public class InstanceState
    {
        private string descriptionField;
        private string instanceIdField;
        private string reasonCodeField;
        private string stateField;

        public bool IsSetDescription()
        {
            return (this.descriptionField != null);
        }

        public bool IsSetInstanceId()
        {
            return (this.instanceIdField != null);
        }

        public bool IsSetReasonCode()
        {
            return (this.reasonCodeField != null);
        }

        public bool IsSetState()
        {
            return (this.stateField != null);
        }

        public InstanceState WithDescription(string description)
        {
            this.descriptionField = description;
            return this;
        }

        public InstanceState WithInstanceId(string instanceId)
        {
            this.instanceIdField = instanceId;
            return this;
        }

        public InstanceState WithReasonCode(string reasonCode)
        {
            this.reasonCodeField = reasonCode;
            return this;
        }

        public InstanceState WithState(string state)
        {
            this.stateField = state;
            return this;
        }

        [XmlElement(ElementName="Description")]
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
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

        [XmlElement(ElementName="ReasonCode")]
        public string ReasonCode
        {
            get
            {
                return this.reasonCodeField;
            }
            set
            {
                this.reasonCodeField = value;
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

