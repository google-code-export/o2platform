namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeSnapshotsResult
    {
        private List<Amazon.EC2.Model.Snapshot> snapshotField;

        public bool IsSetSnapshot()
        {
            return (this.Snapshot.Count > 0);
        }

        public DescribeSnapshotsResult WithSnapshot(params Amazon.EC2.Model.Snapshot[] list)
        {
            foreach (Amazon.EC2.Model.Snapshot snapshot in list)
            {
                this.Snapshot.Add(snapshot);
            }
            return this;
        }

        [XmlElement(ElementName="Snapshot")]
        public List<Amazon.EC2.Model.Snapshot> Snapshot
        {
            get
            {
                if (this.snapshotField == null)
                {
                    this.snapshotField = new List<Amazon.EC2.Model.Snapshot>();
                }
                return this.snapshotField;
            }
            set
            {
                this.snapshotField = value;
            }
        }
    }
}

