namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class CreateDBSnapshotRequest
    {
        private string DBInstanceIdentifierField;
        private string DBSnapshotIdentifierField;

        public bool IsSetDBInstanceIdentifier()
        {
            return (this.DBInstanceIdentifierField != null);
        }

        public bool IsSetDBSnapshotIdentifier()
        {
            return (this.DBSnapshotIdentifierField != null);
        }

        public CreateDBSnapshotRequest WithDBInstanceIdentifier(string DBInstanceIdentifier)
        {
            this.DBInstanceIdentifierField = DBInstanceIdentifier;
            return this;
        }

        public CreateDBSnapshotRequest WithDBSnapshotIdentifier(string DBSnapshotIdentifier)
        {
            this.DBSnapshotIdentifierField = DBSnapshotIdentifier;
            return this;
        }

        [XmlElement(ElementName="DBInstanceIdentifier")]
        public string DBInstanceIdentifier
        {
            get
            {
                return this.DBInstanceIdentifierField;
            }
            set
            {
                this.DBInstanceIdentifierField = value;
            }
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

