namespace Amazon.RDS.Model
{
    using System;
    using System.Globalization;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class RestoreDBInstanceToPointInTimeRequest
    {
        private string availabilityZoneField;
        private string DBInstanceClassField;
        private decimal? portField;
        private DateTime? restoreTimeField;
        private string sourceDBInstanceIdentifierField;
        private string targetDBInstanceIdentifierField;
        private bool? useLatestRestorableTimeField;

        public bool IsSetAvailabilityZone()
        {
            return (this.availabilityZoneField != null);
        }

        public bool IsSetDBInstanceClass()
        {
            return (this.DBInstanceClassField != null);
        }

        public bool IsSetPort()
        {
            return this.portField.HasValue;
        }

        public bool IsSetRestoreTime()
        {
            return this.restoreTimeField.HasValue;
        }

        public bool IsSetSourceDBInstanceIdentifier()
        {
            return (this.sourceDBInstanceIdentifierField != null);
        }

        public bool IsSetTargetDBInstanceIdentifier()
        {
            return (this.targetDBInstanceIdentifierField != null);
        }

        public bool IsSetUseLatestRestorableTime()
        {
            return this.useLatestRestorableTimeField.HasValue;
        }

        public RestoreDBInstanceToPointInTimeRequest WithAvailabilityZone(string availabilityZone)
        {
            this.availabilityZoneField = availabilityZone;
            return this;
        }

        public RestoreDBInstanceToPointInTimeRequest WithDBInstanceClass(string DBInstanceClass)
        {
            this.DBInstanceClassField = DBInstanceClass;
            return this;
        }

        public RestoreDBInstanceToPointInTimeRequest WithPort(decimal port)
        {
            this.portField = new decimal?(port);
            return this;
        }

        public RestoreDBInstanceToPointInTimeRequest WithRestoreTime(DateTime restoreTime)
        {
            this.restoreTimeField = new DateTime?(restoreTime);
            return this;
        }

        public RestoreDBInstanceToPointInTimeRequest WithSourceDBInstanceIdentifier(string sourceDBInstanceIdentifier)
        {
            this.sourceDBInstanceIdentifierField = sourceDBInstanceIdentifier;
            return this;
        }

        public RestoreDBInstanceToPointInTimeRequest WithTargetDBInstanceIdentifier(string targetDBInstanceIdentifier)
        {
            this.targetDBInstanceIdentifierField = targetDBInstanceIdentifier;
            return this;
        }

        public RestoreDBInstanceToPointInTimeRequest WithUseLatestRestorableTime(bool useLatestRestorableTime)
        {
            this.useLatestRestorableTimeField = new bool?(useLatestRestorableTime);
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

        [XmlElement(ElementName="RestoreTime")]
        public DateTime RestoreTime
        {
            get
            {
                return this.restoreTimeField.GetValueOrDefault();
            }
            set
            {
                this.restoreTimeField = new DateTime?(DateTime.ParseExact(value.ToString(), @"yyyy-MM-dd\THH:mm:ss.fff\Z", CultureInfo.InvariantCulture));
            }
        }

        [XmlElement(ElementName="SourceDBInstanceIdentifier")]
        public string SourceDBInstanceIdentifier
        {
            get
            {
                return this.sourceDBInstanceIdentifierField;
            }
            set
            {
                this.sourceDBInstanceIdentifierField = value;
            }
        }

        [XmlElement(ElementName="TargetDBInstanceIdentifier")]
        public string TargetDBInstanceIdentifier
        {
            get
            {
                return this.targetDBInstanceIdentifierField;
            }
            set
            {
                this.targetDBInstanceIdentifierField = value;
            }
        }

        [XmlElement(ElementName="UseLatestRestorableTime")]
        public bool UseLatestRestorableTime
        {
            get
            {
                return this.useLatestRestorableTimeField.GetValueOrDefault();
            }
            set
            {
                this.useLatestRestorableTimeField = new bool?(value);
            }
        }
    }
}

