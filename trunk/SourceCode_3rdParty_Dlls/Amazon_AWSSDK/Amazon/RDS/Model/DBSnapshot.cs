namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DBSnapshot
    {
        private decimal? allocatedStorageField;
        private string availabilityZoneField;
        private string DBInstanceIdentifierField;
        private string DBSnapshotIdentifierField;
        private string engineField;
        private DateTime? instanceCreateTimeField;
        private string masterUsernameField;
        private decimal? portField;
        private DateTime? snapshotCreateTimeField;
        private string statusField;

        public bool IsSetAllocatedStorage()
        {
            return this.allocatedStorageField.HasValue;
        }

        public bool IsSetAvailabilityZone()
        {
            return (this.availabilityZoneField != null);
        }

        public bool IsSetDBInstanceIdentifier()
        {
            return (this.DBInstanceIdentifierField != null);
        }

        public bool IsSetDBSnapshotIdentifier()
        {
            return (this.DBSnapshotIdentifierField != null);
        }

        public bool IsSetEngine()
        {
            return (this.engineField != null);
        }

        public bool IsSetInstanceCreateTime()
        {
            return this.instanceCreateTimeField.HasValue;
        }

        public bool IsSetMasterUsername()
        {
            return (this.masterUsernameField != null);
        }

        public bool IsSetPort()
        {
            return this.portField.HasValue;
        }

        public bool IsSetSnapshotCreateTime()
        {
            return this.snapshotCreateTimeField.HasValue;
        }

        public bool IsSetStatus()
        {
            return (this.statusField != null);
        }

        public DBSnapshot WithAllocatedStorage(decimal allocatedStorage)
        {
            this.allocatedStorageField = new decimal?(allocatedStorage);
            return this;
        }

        public DBSnapshot WithAvailabilityZone(string availabilityZone)
        {
            this.availabilityZoneField = availabilityZone;
            return this;
        }

        public DBSnapshot WithDBInstanceIdentifier(string DBInstanceIdentifier)
        {
            this.DBInstanceIdentifierField = DBInstanceIdentifier;
            return this;
        }

        public DBSnapshot WithDBSnapshotIdentifier(string DBSnapshotIdentifier)
        {
            this.DBSnapshotIdentifierField = DBSnapshotIdentifier;
            return this;
        }

        public DBSnapshot WithEngine(string engine)
        {
            this.engineField = engine;
            return this;
        }

        public DBSnapshot WithInstanceCreateTime(DateTime instanceCreateTime)
        {
            this.instanceCreateTimeField = new DateTime?(instanceCreateTime);
            return this;
        }

        public DBSnapshot WithMasterUsername(string masterUsername)
        {
            this.masterUsernameField = masterUsername;
            return this;
        }

        public DBSnapshot WithPort(decimal port)
        {
            this.portField = new decimal?(port);
            return this;
        }

        public DBSnapshot WithSnapshotCreateTime(DateTime snapshotCreateTime)
        {
            this.snapshotCreateTimeField = new DateTime?(snapshotCreateTime);
            return this;
        }

        public DBSnapshot WithStatus(string status)
        {
            this.statusField = status;
            return this;
        }

        [XmlElement(ElementName="AllocatedStorage")]
        public decimal AllocatedStorage
        {
            get
            {
                return this.allocatedStorageField.GetValueOrDefault();
            }
            set
            {
                this.allocatedStorageField = new decimal?(value);
            }
        }

        [XmlElement(ElementName="AvailabilityZone")]
        public string AvailabilityZone
        {
            get
            {
                return this.availabilityZoneField;
            }
            set
            {
                this.availabilityZoneField = value;
            }
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

        [XmlElement(ElementName="Engine")]
        public string Engine
        {
            get
            {
                return this.engineField;
            }
            set
            {
                this.engineField = value;
            }
        }

        [XmlElement(ElementName="InstanceCreateTime")]
        public DateTime InstanceCreateTime
        {
            get
            {
                return this.instanceCreateTimeField.GetValueOrDefault();
            }
            set
            {
                this.instanceCreateTimeField = new DateTime?(value);
            }
        }

        [XmlElement(ElementName="MasterUsername")]
        public string MasterUsername
        {
            get
            {
                return this.masterUsernameField;
            }
            set
            {
                this.masterUsernameField = value;
            }
        }

        [XmlElement(ElementName="Port")]
        public decimal Port
        {
            get
            {
                return this.portField.GetValueOrDefault();
            }
            set
            {
                this.portField = new decimal?(value);
            }
        }

        [XmlElement(ElementName="SnapshotCreateTime")]
        public DateTime SnapshotCreateTime
        {
            get
            {
                return this.snapshotCreateTimeField.GetValueOrDefault();
            }
            set
            {
                this.snapshotCreateTimeField = new DateTime?(value);
            }
        }

        [XmlElement(ElementName="Status")]
        public string Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }
    }
}

