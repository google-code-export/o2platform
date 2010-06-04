namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class RestoreDBInstanceFromDBSnapshotRequest
    {
        private string availabilityZoneField;
        private string DBInstanceClassField;
        private string DBInstanceIdentifierField;
        private string DBSnapshotIdentifierField;
        private decimal? portField;

        public bool IsSetAvailabilityZone()
        {
            return (this.availabilityZoneField != null);
        }

        public bool IsSetDBInstanceClass()
        {
            return (this.DBInstanceClassField != null);
        }

        public bool IsSetDBInstanceIdentifier()
        {
            return (this.DBInstanceIdentifierField != null);
        }

        public bool IsSetDBSnapshotIdentifier()
        {
            return (this.DBSnapshotIdentifierField != null);
        }

        public bool IsSetPort()
        {
            return this.portField.HasValue;
        }

        public RestoreDBInstanceFromDBSnapshotRequest WithAvailabilityZone(string availabilityZone)
        {
            this.availabilityZoneField = availabilityZone;
            return this;
        }

        public RestoreDBInstanceFromDBSnapshotRequest WithDBInstanceClass(string DBInstanceClass)
        {
            this.DBInstanceClassField = DBInstanceClass;
            return this;
        }

        public RestoreDBInstanceFromDBSnapshotRequest WithDBInstanceIdentifier(string DBInstanceIdentifier)
        {
            this.DBInstanceIdentifierField = DBInstanceIdentifier;
            return this;
        }

        public RestoreDBInstanceFromDBSnapshotRequest WithDBSnapshotIdentifier(string DBSnapshotIdentifier)
        {
            this.DBSnapshotIdentifierField = DBSnapshotIdentifier;
            return this;
        }

        public RestoreDBInstanceFromDBSnapshotRequest WithPort(decimal port)
        {
            this.portField = new decimal?(port);
            return this;
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

        [XmlElement(ElementName="DBInstanceClass")]
        public string DBInstanceClass
        {
            get
            {
                return this.DBInstanceClassField;
            }
            set
            {
                this.DBInstanceClassField = value;
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
    }
}

