namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class PendingModifiedValues
    {
        private decimal? allocatedStorageField;
        private decimal? backupRetentionPeriodField;
        private string DBInstanceClassField;
        private string masterUserPasswordField;
        private decimal? portField;

        public bool IsSetAllocatedStorage()
        {
            return this.allocatedStorageField.HasValue;
        }

        public bool IsSetBackupRetentionPeriod()
        {
            return this.backupRetentionPeriodField.HasValue;
        }

        public bool IsSetDBInstanceClass()
        {
            return (this.DBInstanceClassField != null);
        }

        public bool IsSetMasterUserPassword()
        {
            return (this.masterUserPasswordField != null);
        }

        public bool IsSetPort()
        {
            return this.portField.HasValue;
        }

        public PendingModifiedValues WithAllocatedStorage(decimal allocatedStorage)
        {
            this.allocatedStorageField = new decimal?(allocatedStorage);
            return this;
        }

        public PendingModifiedValues WithBackupRetentionPeriod(decimal backupRetentionPeriod)
        {
            this.backupRetentionPeriodField = new decimal?(backupRetentionPeriod);
            return this;
        }

        public PendingModifiedValues WithDBInstanceClass(string DBInstanceClass)
        {
            this.DBInstanceClassField = DBInstanceClass;
            return this;
        }

        public PendingModifiedValues WithMasterUserPassword(string masterUserPassword)
        {
            this.masterUserPasswordField = masterUserPassword;
            return this;
        }

        public PendingModifiedValues WithPort(decimal port)
        {
            this.portField = new decimal?(port);
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

        [XmlElement(ElementName="BackupRetentionPeriod")]
        public decimal BackupRetentionPeriod
        {
            get
            {
                return this.backupRetentionPeriodField.GetValueOrDefault();
            }
            set
            {
                this.backupRetentionPeriodField = new decimal?(value);
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

        [XmlElement(ElementName="MasterUserPassword")]
        public string MasterUserPassword
        {
            get
            {
                return this.masterUserPasswordField;
            }
            set
            {
                this.masterUserPasswordField = value;
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

