namespace Amazon.CloudWatch.Model
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://monitoring.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class ErrorResponse
    {
        private List<Amazon.CloudWatch.Model.Error> errorField;
        private string requestIdField;

        public bool IsSetError()
        {
            return (this.Error.Count > 0);
        }

        public bool IsSetRequestId()
        {
            return (this.requestIdField != null);
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

        public ErrorResponse WithError(params Amazon.CloudWatch.Model.Error[] list)
        {
            foreach (Amazon.CloudWatch.Model.Error error in list)
            {
                this.Error.Add(error);
            }
            return this;
        }

        public ErrorResponse WithRequestId(string requestId)
        {
            this.requestIdField = requestId;
            return this;
        }

        [XmlElement(ElementName="Error")]
        public List<Amazon.CloudWatch.Model.Error> Error
        {
            get
            {
                if (this.errorField == null)
                {
                    this.errorField = new List<Amazon.CloudWatch.Model.Error>();
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

