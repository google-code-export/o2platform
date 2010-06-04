namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DeleteDBInstanceRequest
    {
        private string DBInstanceIdentifierField;
        private string finalDBSnapshotIdentifierField;
        private bool? skipFinalSnapshotField;

        public bool IsSetDBInstanceIdentifier()
        {
            return (this.DBInstanceIdentifierField != null);
        }

        public bool IsSetFinalDBSnapshotIdentifier()
        {
            return (this.finalDBSnapshotIdentifierField != null);
        }

        public bool IsSetSkipFinalSnapshot()
        {
            return this.skipFinalSnapshotField.HasValue;
        }

        public DeleteDBInstanceRequest WithDBInstanceIdentifier(string DBInstanceIdentifier)
        {
            this.DBInstanceIdentifierField = DBInstanceIdentifier;
            return this;
        }

        public DeleteDBInstanceRequest WithFinalDBSnapshotIdentifier(string finalDBSnapshotIdentifier)
        {
            this.finalDBSnapshotIdentifierField = finalDBSnapshotIdentifier;
            return this;
        }

        public DeleteDBInstanceRequest WithSkipFinalSnapshot(bool skipFinalSnapshot)
        {
            this.skipFinalSnapshotField = new bool?(skipFinalSnapshot);
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

        [XmlElement(ElementName="FinalDBSnapshotIdentifier")]
        public string FinalDBSnapshotIdentifier
        {
            get
            {
                return this.finalDBSnapshotIdentifierField;
            }
            set
            {
                this.finalDBSnapshotIdentifierField = value;
            }
        }

        [XmlElement(ElementName="SkipFinalSnapshot")]
        public bool SkipFinalSnapshot
        {
            get
            {
                return this.skipFinalSnapshotField.GetValueOrDefault();
            }
            set
            {
                this.skipFinalSnapshotField = new bool?(value);
            }
        }
    }
}

