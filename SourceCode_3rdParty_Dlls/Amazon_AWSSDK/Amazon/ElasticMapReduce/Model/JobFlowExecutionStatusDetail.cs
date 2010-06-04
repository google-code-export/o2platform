namespace Amazon.ElasticMapReduce.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticmapreduce.amazonaws.com/doc/2009-03-31", IsNullable=false)]
    public class JobFlowExecutionStatusDetail
    {
        private string creationDateTimeField;
        private string endDateTimeField;
        private string lastStateChangeReasonField;
        private string readyDateTimeField;
        private string startDateTimeField;
        private string stateField;

        public bool IsSetCreationDateTime()
        {
            return (this.creationDateTimeField != null);
        }

        public bool IsSetEndDateTime()
        {
            return (this.endDateTimeField != null);
        }

        public bool IsSetLastStateChangeReason()
        {
            return (this.lastStateChangeReasonField != null);
        }

        public bool IsSetReadyDateTime()
        {
            return (this.readyDateTimeField != null);
        }

        public bool IsSetStartDateTime()
        {
            return (this.startDateTimeField != null);
        }

        public bool IsSetState()
        {
            return (this.stateField != null);
        }

        public JobFlowExecutionStatusDetail WithCreationDateTime(string creationDateTime)
        {
            this.creationDateTimeField = creationDateTime;
            return this;
        }

        public JobFlowExecutionStatusDetail WithEndDateTime(string endDateTime)
        {
            this.endDateTimeField = endDateTime;
            return this;
        }

        public JobFlowExecutionStatusDetail WithLastStateChangeReason(string lastStateChangeReason)
        {
            this.lastStateChangeReasonField = lastStateChangeReason;
            return this;
        }

        public JobFlowExecutionStatusDetail WithReadyDateTime(string readyDateTime)
        {
            this.readyDateTimeField = readyDateTime;
            return this;
        }

        public JobFlowExecutionStatusDetail WithStartDateTime(string startDateTime)
        {
            this.startDateTimeField = startDateTime;
            return this;
        }

        public JobFlowExecutionStatusDetail WithState(string state)
        {
            this.stateField = state;
            return this;
        }

        [XmlElement(ElementName="CreationDateTime")]
        public string CreationDateTime
        {
            get
            {
                return this.creationDateTimeField;
            }
            set
            {
                this.creationDateTimeField = value;
            }
        }

        [XmlElement(ElementName="EndDateTime")]
        public string EndDateTime
        {
            get
            {
                return this.endDateTimeField;
            }
            set
            {
                this.endDateTimeField = value;
            }
        }

        [XmlElement(ElementName="LastStateChangeReason")]
        public string LastStateChangeReason
        {
            get
            {
                return this.lastStateChangeReasonField;
            }
            set
            {
                this.lastStateChangeReasonField = value;
            }
        }

        [XmlElement(ElementName="ReadyDateTime")]
        public string ReadyDateTime
        {
            get
            {
                return this.readyDateTimeField;
            }
            set
            {
                this.readyDateTimeField = value;
            }
        }

        [XmlElement(ElementName="StartDateTime")]
        public string StartDateTime
        {
            get
            {
                return this.startDateTimeField;
            }
            set
            {
                this.startDateTimeField = value;
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

