namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DeleteDBSnapshotRequest
    {
        private string DBSnapshotIdentifierField;

        public bool IsSetDBSnapshotIdentifier()
        {
            return (this.DBSnapshotIdentifierField != null);
        }

        public DeleteDBSnapshotRequest WithDBSnapshotIdentifier(string DBSnapshotIdentifier)
        {
            this.DBSnapshotIdentifierField = DBSnapshotIdentifier;
            return this;
        }

        [XmlElement(ElementName="DBSnapshotIdentifier")]
        public string DBSnapshotIdentifier
        {
            get
            {
                return this.DBSnapshotIdentifierField;
            }
            set
            {
                this.DBSnapshotIdentifierField = value;
            }
        }
    }
}

