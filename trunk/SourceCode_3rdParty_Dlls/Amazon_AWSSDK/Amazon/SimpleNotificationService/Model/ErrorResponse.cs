namespace Amazon.SimpleNotificationService.Model
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sns.amazonaws.com/doc/2010-03-31/", IsNullable=false)]
    public class ErrorResponse
    {
        private List<Amazon.SimpleNotificationService.Model.Error> errorField;
        private string requestIdField;

        public bool IsSetError()
        {
            return (this.Error.Count > 0);
        }

        public bool IsSetRequestId()
        {
            return (this.requestIdField != null);
        }

        public override string ToString()
        {
            return this.ToXML();
        }

        public string ToXML()
        {
            StringBuilder sb = new StringBuilder(0x400);
            XmlSerializer serializer = new XmlSerializer(base.GetType());
            using (StringWriter writer = new StringWriter(sb))
            {
                serializer.Serialize((TextWriter) writer, this);
            }
            return sb.ToString();
        }

        [XmlElement(ElementName="Error")]
        public List<Amazon.SimpleNotificationService.Model.Error> Error
        {
            get
            {
                if (this.errorField == null)
                {
                    this.errorField = new List<Amazon.SimpleNotificationService.Model.Error>();
                }
                return this.errorField;
            }
            set
            {
                this.errorField = value;
            }
        }

        [XmlElement(ElementName="RequestId")]
        public string RequestId
        {
            get
            {
                return this.requestIdField;
            }
            set
            {
                this.requestIdField = value;
            }
        }
    }
}

