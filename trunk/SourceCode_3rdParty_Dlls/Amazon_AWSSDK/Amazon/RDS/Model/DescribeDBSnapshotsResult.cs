namespace Amazon.RDS.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DescribeDBSnapshotsResult
    {
        private List<Amazon.RDS.Model.DBSnapshot> DBSnapshotField;
        private string markerField;

        public bool IsSetDBSnapshot()
        {
            return (this.DBSnapshot.Count > 0);
        }

        public bool IsSetMarker()
        {
            return (this.markerField != null);
        }

        public DescribeDBSnapshotsResult WithDBSnapshot(params Amazon.RDS.Model.DBSnapshot[] list)
        {
            foreach (Amazon.RDS.Model.DBSnapshot snapshot in list)
            {
                this.DBSnapshot.Add(snapshot);
            }
            return this;
        }

        public DescribeDBSnapshotsResult WithMarker(string marker)
        {
            this.markerField = marker;
            return this;
        }

        [XmlElement(ElementName="DBSnapshot")]
        public List<Amazon.RDS.Model.DBSnapshot> DBSnapshot
        {
            get
            {
                if (this.DBSnapshotField == null)
                {
                    this.DBSnapshotField = new List<Amazon.RDS.Model.DBSnapshot>();
                }
                return this.DBSnapshotField;
            }
            set
            {
                this.DBSnapshotField = value;
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
    }
}

