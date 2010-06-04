namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class MonitorInstancesResponse
    {
        private Amazon.EC2.Model.MonitorInstancesResult monitorInstancesResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetMonitorInstancesResult()
        {
            return (this.monitorInstancesResultField != null);
        }

        public bool IsSetResponseMetadata()
        {
            return (this.responseMetadataField != null);
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

        public MonitorInstancesResponse WithMonitorInstancesResult(Amazon.EC2.Model.MonitorInstancesResult monitorInstancesResult)
        {
            this.monitorInstancesResultField = monitorInstancesResult;
            return this;
        }

        public MonitorInstancesResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="MonitorInstancesResult")]
        public Amazon.EC2.Model.MonitorInstancesResult MonitorInstancesResult
        {
            get
            {
                return this.monitorInstancesResultField;
            }
            set
            {
                this.monitorInstancesResultField = value;
            }
        }

        [XmlElement(ElementName="ResponseMetadata")]
        public Amazon.EC2.Model.ResponseMetadata ResponseMetadata
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

