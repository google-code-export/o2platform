namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DescribeDBSnapshotsRequest
    {
        private string DBInstanceIdentifierField;
        private string DBSnapshotIdentifierField;
        private string markerField;
        private decimal? maxRecordsField;

        public bool IsSetDBInstanceIdentifier()
        {
            return (this.DBInstanceIdentifierField != null);
        }

        public bool IsSetDBSnapshotIdentifier()
        {
            return (this.DBSnapshotIdentifierField != null);
        }

        public bool IsSetMarker()
        {
            return (this.markerField != null);
        }

        public bool IsSetMaxRecords()
        {
            return this.maxRecordsField.HasValue;
        }

        public DescribeDBSnapshotsRequest WithDBInstanceIdentifier(string DBInstanceIdentifier)
        {
            this.DBInstanceIdentifierField = DBInstanceIdentifier;
            return this;
        }

        public DescribeDBSnapshotsRequest WithDBSnapshotIdentifier(string DBSnapshotIdentifier)
        {
            this.DBSnapshotIdentifierField = DBSnapshotIdentifier;
            return this;
        }

        public DescribeDBSnapshotsRequest WithMarker(string marker)
        {
            this.markerField = marker;
            return this;
        }

        public DescribeDBSnapshotsRequest WithMaxRecords(decimal maxRecords)
        {
            this.maxRecordsField = new decimal?(maxRecords);
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

        [XmlElement(ElementName="Marker")]
        public string Marker
        {
            get
            {
                return this.markerField;
            }
            set
            {
                this.markerField = value;
            }
        }

        [XmlElement(ElementName="MaxRecords")]
        public decimal MaxRecords
        {
            get
            {
                return this.maxRecordsField.GetValueOrDefault();
            }
            set
            {
                this.maxRecordsField = new decimal?(value);
            }
        }
    }
}

