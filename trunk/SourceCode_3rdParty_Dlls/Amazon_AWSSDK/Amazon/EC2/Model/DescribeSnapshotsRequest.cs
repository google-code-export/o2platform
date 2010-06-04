namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeSnapshotsRequest
    {
        private string ownerField;
        private string restorableByField;
        private List<string> snapshotIdField;

        public bool IsSetOwner()
        {
            return (this.ownerField != null);
        }

        public bool IsSetRestorableBy()
        {
            return (this.restorableByField != null);
        }

        public bool IsSetSnapshotId()
        {
            return (this.SnapshotId.Count > 0);
        }

        public DescribeSnapshotsRequest WithOwner(string owner)
        {
            this.ownerField = owner;
            return this;
        }

        public DescribeSnapshotsRequest WithRestorableBy(string restorableBy)
        {
            this.restorableByField = restorableBy;
            return this;
        }

        public DescribeSnapshotsRequest WithSnapshotId(params string[] list)
        {
            foreach (string str in list)
            {
                this.SnapshotId.Add(str);
            }
            return this;
        }

        [XmlElement(ElementName="Owner")]
        public string Owner
        {
            get
            {
                return this.ownerField;
            }
            set
            {
                this.ownerField = value;
            }
        }

        [XmlElement(ElementName="RestorableBy")]
        public string RestorableBy
        {
            get
            {
                return this.restorableByField;
            }
            set
            {
                this.restorableByField = value;
            }
        }

        [XmlElement(ElementName="SnapshotId")]
        public List<string> SnapshotId
        {
            get
            {
                if (this.snapshotIdField == null)
                {
                    this.snapshotIdField = new List<string>();
                }
                return this.snapshotIdField;
            }
            set
            {
                this.snapshotIdField = value;
            }
        }
    }
}

