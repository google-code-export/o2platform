namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CancelBundleTaskResponse
    {
        private Amazon.EC2.Model.CancelBundleTaskResult cancelBundleTaskResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetCancelBundleTaskResult()
        {
            return (this.cancelBundleTaskResultField != null);
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

        public CancelBundleTaskResponse WithCancelBundleTaskResult(Amazon.EC2.Model.CancelBundleTaskResult cancelBundleTaskResult)
        {
            this.cancelBundleTaskResultField = cancelBundleTaskResult;
            return this;
        }

        public CancelBundleTaskResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="CancelBundleTaskResult")]
        public Amazon.EC2.Model.CancelBundleTaskResult CancelBundleTaskResult
        {
            get
            {
                return this.cancelBundleTaskResultField;
            }
            set
            {
                this.cancelBundleTaskResultField = value;
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

