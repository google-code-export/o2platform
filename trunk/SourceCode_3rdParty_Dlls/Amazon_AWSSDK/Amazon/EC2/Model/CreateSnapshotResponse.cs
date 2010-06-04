namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CreateSnapshotResponse
    {
        private Amazon.EC2.Model.CreateSnapshotResult createSnapshotResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetCreateSnapshotResult()
        {
            return (this.createSnapshotResultField != null);
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

        public CreateSnapshotResponse WithCreateSnapshotResult(Amazon.EC2.Model.CreateSnapshotResult createSnapshotResult)
        {
            this.createSnapshotResultField = createSnapshotResult;
            return this;
        }

        public CreateSnapshotResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="CreateSnapshotResult")]
        public Amazon.EC2.Model.CreateSnapshotResult CreateSnapshotResult
        {
            get
            {
                return this.createSnapshotResultField;
            }
            set
            {
                this.createSnapshotResultField = value;
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

