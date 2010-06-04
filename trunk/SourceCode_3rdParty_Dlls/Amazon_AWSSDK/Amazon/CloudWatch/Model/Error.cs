namespace Amazon.CloudWatch.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://monitoring.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class Error
    {
        private string codeField;
        private object detailField;
        private string messageField;
        private string typeField;

        public bool IsSetCode()
        {
            return (this.codeField != null);
        }

        public bool IsSetDetail()
        {
            return (this.detailField != null);
        }

        public bool IsSetMessage()
        {
            return (this.messageField != null);
        }

        public bool IsSetType()
        {
            return (this.typeField != null);
        }

        public Error WithCode(string code)
        {
            this.codeField = code;
            return this;
        }

        public Error WithDetail(object detail)
        {
            this.detailField = detail;
            return this;
        }

        public Error WithMessage(string message)
        {
            this.messageField = message;
            return this;
        }

        public Error WithType(string type)
        {
            this.typeField = type;
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

        [XmlElement(ElementName="Detail")]
        public object Detail
        {
            get
            {
                return this.detailField;
            }
            set
            {
                this.detailField = value;
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

        [XmlElement(ElementName="Type")]
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }
}

