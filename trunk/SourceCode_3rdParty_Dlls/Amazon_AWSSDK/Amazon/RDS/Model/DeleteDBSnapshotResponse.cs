namespace Amazon.RDS.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DeleteDBSnapshotResponse
    {
        private Amazon.RDS.Model.DeleteDBSnapshotResult deleteDBSnapshotResultField;
        private Amazon.RDS.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDeleteDBSnapshotResult()
        {
            return (this.deleteDBSnapshotResultField != null);
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

        public DeleteDBSnapshotResponse WithDeleteDBSnapshotResult(Amazon.RDS.Model.DeleteDBSnapshotResult deleteDBSnapshotResult)
        {
            this.deleteDBSnapshotResultField = deleteDBSnapshotResult;
            return this;
        }

        public DeleteDBSnapshotResponse WithResponseMetadata(Amazon.RDS.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DeleteDBSnapshotResult")]
        public Amazon.RDS.Model.DeleteDBSnapshotResult DeleteDBSnapshotResult
        {
            get
            {
                return this.deleteDBSnapshotResultField;
            }
            set
            {
                this.deleteDBSnapshotResultField = value;
            }
        }

        [XmlElement(ElementName="ResponseMetadata")]
        public Amazon.RDS.Model.ResponseMetadata ResponseMetadata
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

