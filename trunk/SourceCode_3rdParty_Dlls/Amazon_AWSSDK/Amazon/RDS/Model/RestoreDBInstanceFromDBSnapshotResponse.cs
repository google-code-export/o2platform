namespace Amazon.RDS.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class RestoreDBInstanceFromDBSnapshotResponse
    {
        private Amazon.RDS.Model.ResponseMetadata responseMetadataField;
        private Amazon.RDS.Model.RestoreDBInstanceFromDBSnapshotResult restoreDBInstanceFromDBSnapshotResultField;

        public bool IsSetResponseMetadata()
        {
            return (this.responseMetadataField != null);
        }

        public bool IsSetRestoreDBInstanceFromDBSnapshotResult()
        {
            return (this.restoreDBInstanceFromDBSnapshotResultField != null);
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

        public RestoreDBInstanceFromDBSnapshotResponse WithResponseMetadata(Amazon.RDS.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        public RestoreDBInstanceFromDBSnapshotResponse WithRestoreDBInstanceFromDBSnapshotResult(Amazon.RDS.Model.RestoreDBInstanceFromDBSnapshotResult restoreDBInstanceFromDBSnapshotResult)
        {
            this.restoreDBInstanceFromDBSnapshotResultField = restoreDBInstanceFromDBSnapshotResult;
            return this;
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

        [XmlElement(ElementName="RestoreDBInstanceFromDBSnapshotResult")]
        public Amazon.RDS.Model.RestoreDBInstanceFromDBSnapshotResult RestoreDBInstanceFromDBSnapshotResult
        {
            get
            {
                return this.restoreDBInstanceFromDBSnapshotResultField;
            }
            set
            {
                this.restoreDBInstanceFromDBSnapshotResultField = value;
            }
        }
    }
}

