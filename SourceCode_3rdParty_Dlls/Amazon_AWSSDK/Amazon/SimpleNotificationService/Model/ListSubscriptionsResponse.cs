namespace Amazon.SimpleNotificationService.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sns.amazonaws.com/doc/2010-03-31/", IsNullable=false)]
    public class ListSubscriptionsResponse
    {
        private Amazon.SimpleNotificationService.Model.ListSubscriptionsResult listSubscriptionsResultField;
        private Amazon.SimpleNotificationService.Model.ResponseMetadata responseMetadataField;

        public bool IsSetListSubscriptionsResult()
        {
            return (this.listSubscriptionsResultField != null);
        }

        public bool IsSetResponseMetadata()
        {
            return (this.responseMetadataField != null);
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

        [XmlElement(ElementName="ListSubscriptionsResult")]
        public Amazon.SimpleNotificationService.Model.ListSubscriptionsResult ListSubscriptionsResult
        {
            get
            {
                return this.listSubscriptionsResultField;
            }
            set
            {
                this.listSubscriptionsResultField = value;
            }
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
    }
}

