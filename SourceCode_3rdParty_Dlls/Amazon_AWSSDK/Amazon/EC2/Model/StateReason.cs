namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class StateReason
    {
        private string codeField;
        private string messageField;

        public bool IsSetCode()
        {
            return (this.codeField != null);
        }

        public bool IsSetMessage()
        {
            return (this.messageField != null);
        }

        public StateReason WithCode(string code)
        {
            this.codeField = code;
            return this;
        }

        public StateReason WithMessage(string message)
        {
            this.messageField = message;
            return this;
        }

        [XmlElement(ElementName="Code")]
        public string Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
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
    }
}

