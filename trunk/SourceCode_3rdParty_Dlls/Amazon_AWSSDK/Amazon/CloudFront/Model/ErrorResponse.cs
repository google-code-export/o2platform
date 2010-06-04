namespace Amazon.CloudFront.Model
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://cloudfront.amazonaws.com/doc/2010-03-01/", IsNullable=false)]
    public class ErrorResponse
    {
        private List<CloudFrontError> errorField;
        private string requestIdField;

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
        public List<CloudFrontError> Error
        {
            get
            {
                if (this.errorField == null)
                {
                    this.errorField = new List<CloudFrontError>();
                }
                foreach (CloudFrontError error in this.errorField)
                {
                    error.RequestId = this.requestIdField;
                }
                return this.errorField;
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

