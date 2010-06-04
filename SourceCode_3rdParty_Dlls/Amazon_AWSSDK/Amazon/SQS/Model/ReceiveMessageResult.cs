namespace Amazon.SQS.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://queue.amazonaws.com/doc/2009-02-01/", IsNullable=false)]
    public class ReceiveMessageResult
    {
        private List<Amazon.SQS.Model.Message> messageField;

        public bool IsSetMessage()
        {
            return (this.Message.Count > 0);
        }

        public ReceiveMessageResult WithMessage(params Amazon.SQS.Model.Message[] list)
        {
            foreach (Amazon.SQS.Model.Message message in list)
            {
                this.Message.Add(message);
            }
            return this;
        }

        [XmlElement(ElementName="Message")]
        public List<Amazon.SQS.Model.Message> Message
        {
            get
            {
                if (this.messageField == null)
                {
                    this.messageField = new List<Amazon.SQS.Model.Message>();
                }
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
    }
}

