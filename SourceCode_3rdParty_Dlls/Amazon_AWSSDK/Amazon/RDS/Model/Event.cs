namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class Event
    {
        private DateTime? dateField;
        private string messageField;
        private string sourceIdentifierField;
        private string sourceTypeField;

        public bool IsSetDate()
        {
            return this.dateField.HasValue;
        }

        public bool IsSetMessage()
        {
            return (this.messageField != null);
        }

        public bool IsSetSourceIdentifier()
        {
            return (this.sourceIdentifierField != null);
        }

        public bool IsSetSourceType()
        {
            return (this.sourceTypeField != null);
        }

        public Event WithDate(DateTime date)
        {
            this.dateField = new DateTime?(date);
            return this;
        }

        public Event WithMessage(string message)
        {
            this.messageField = message;
            return this;
        }

        public Event WithSourceIdentifier(string sourceIdentifier)
        {
            this.sourceIdentifierField = sourceIdentifier;
            return this;
        }

        public Event WithSourceType(string sourceType)
        {
            this.sourceTypeField = sourceType;
            return this;
        }

        [XmlElement(ElementName="Date")]
        public DateTime Date
        {
            get
            {
                return this.dateField.GetValueOrDefault();
            }
            set
            {
                this.dateField = new DateTime?(value);
            }
        }

        [XmlElement(ElementName="Message")]
        public string Message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }

        [XmlElement(ElementName="SourceIdentifier")]
        public string SourceIdentifier
        {
            get
            {
                return this.sourceIdentifierField;
            }
            set
            {
                this.sourceIdentifierField = value;
            }
        }

        [XmlElement(ElementName="SourceType")]
        public string SourceType
        {
            get
            {
                return this.sourceTypeField;
            }
            set
            {
                this.sourceTypeField = value;
            }
        }
    }
}

