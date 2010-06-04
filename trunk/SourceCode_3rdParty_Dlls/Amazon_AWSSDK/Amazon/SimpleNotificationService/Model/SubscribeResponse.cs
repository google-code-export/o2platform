namespace Amazon.SimpleNotificationService.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sns.amazonaws.com/doc/2010-03-31/", IsNullable=false)]
    public class SubscribeResponse
    {
        private Amazon.SimpleNotificationService.Model.ResponseMetadata responseMetadataField;
        private Amazon.SimpleNotificationService.Model.SubscribeResult subscribeResultField;

        public bool IsSetResponseMetadata()
        {
            return (this.responseMetadataField != null);
        }

        public bool IsSetSubscribeResult()
        {
            return (this.subscribeResultField != null);
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

        [XmlElement(ElementName="ResponseMetadata")]
        public Amazon.SimpleNotificationService.Model.ResponseMetadata ResponseMetadata
        {
            get
            {
                return this.responseMetadataField;
            }
            set
            {
                this.responseMetadataField = value;
            }
        }

        [XmlElement(ElementName="SubscribeResult")]
        public Amazon.SimpleNotificationService.Model.SubscribeResult SubscribeResult
        {
            get
            {
                return this.subscribeResultField;
            }
            set
            {
                this.subscribeResultField = value;
            }
        }
    }
}

