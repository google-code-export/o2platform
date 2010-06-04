namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class SnapshotAttribute
    {
        private List<Amazon.EC2.Model.CreateVolumePermission> createVolumePermissionField;
        private string snapshotIdField;

        public bool IsSetCreateVolumePermission()
        {
            return (this.CreateVolumePermission.Count > 0);
        }

        public bool IsSetSnapshotId()
        {
            return (this.snapshotIdField != null);
        }

        public SnapshotAttribute WithCreateVolumePermission(params Amazon.EC2.Model.CreateVolumePermission[] list)
        {
            foreach (Amazon.EC2.Model.CreateVolumePermission permission in list)
            {
                this.CreateVolumePermission.Add(permission);
            }
            return this;
        }

        public SnapshotAttribute WithSnapshotId(string snapshotId)
        {
            this.snapshotIdField = snapshotId;
            return this;
        }

        [XmlElement(ElementName="CreateVolumePermission")]
        public List<Amazon.EC2.Model.CreateVolumePermission> CreateVolumePermission
        {
            get
            {
                if (this.createVolumePermissionField == null)
                {
                    this.createVolumePermissionField = new List<Amazon.EC2.Model.CreateVolumePermission>();
                }
                return this.createVolumePermissionField;
            }
            set
            {
                this.createVolumePermissionField = value;
            }
        }

        [XmlElement(ElementName="SnapshotId")]
        public string SnapshotId
        {
            get
            {
                return this.snapshotIdField;
            }
            set
            {
                this.snapshotIdField = value;
            }
        }
    }
}

