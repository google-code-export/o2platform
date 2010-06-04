namespace Amazon.RDS.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class CreateDBSnapshotResponse
    {
        private Amazon.RDS.Model.CreateDBSnapshotResult createDBSnapshotResultField;
        private Amazon.RDS.Model.ResponseMetadata responseMetadataField;

        public bool IsSetCreateDBSnapshotResult()
        {
            return (this.createDBSnapshotResultField != null);
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

        public CreateDBSnapshotResponse WithCreateDBSnapshotResult(Amazon.RDS.Model.CreateDBSnapshotResult createDBSnapshotResult)
        {
            this.createDBSnapshotResultField = createDBSnapshotResult;
            return this;
        }

        public CreateDBSnapshotResponse WithResponseMetadata(Amazon.RDS.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="CreateDBSnapshotResult")]
        public Amazon.RDS.Model.CreateDBSnapshotResult CreateDBSnapshotResult
        {
            get
            {
                return this.createDBSnapshotResultField;
            }
            set
            {
                this.createDBSnapshotResultField = value;
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

