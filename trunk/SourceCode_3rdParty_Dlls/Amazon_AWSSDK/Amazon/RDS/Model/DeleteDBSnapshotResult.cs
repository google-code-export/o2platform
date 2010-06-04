namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DeleteDBSnapshotResult
    {
        private Amazon.RDS.Model.DBSnapshot DBSnapshotField;

        public bool IsSetDBSnapshot()
        {
            return (this.DBSnapshotField != null);
        }

        public DeleteDBSnapshotResult WithDBSnapshot(Amazon.RDS.Model.DBSnapshot DBSnapshot)
        {
            this.DBSnapshotField = DBSnapshot;
            return this;
        }

        [XmlElement(ElementName="DBSnapshot")]
        public Amazon.RDS.Model.DBSnapshot DBSnapshot
        {
            get
            {
                return this.DBSnapshotField;
            }
            set
            {
                this.DBSnapshotField = value;
            }
        }
    }
}

